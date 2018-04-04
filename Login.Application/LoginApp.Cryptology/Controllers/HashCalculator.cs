using LoginApp.Cryptology.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginApp.Cryptology.Controllers
{
    public class HashCalculator
    {
        private static HashKey hashKey;           // Key'in değerlerini tutuyorum.
        private static UserKey userKey;          //Kullanıcının girdiği şifreyi tutuyorum.
        private static StringBuilder builder; //Geçici String değerim.
        private static int MesajBlokSayisi = 12;    //Toplam 12 bloktan oluşuyor.

        /// <summary>
        /// //Static Constructor sayesinde nesnelerin sadece bir kere (new) olmasını sağladım.
        /// </summary>
        static HashCalculator()
        {
            hashKey = new HashKey();
            userKey = new UserKey();
            builder = new StringBuilder();

        } 

        public static string Cryptology(string sifre,DateTime dateTime)
        {
            //Şifrelemek için (KEY: 64-bit) oluşturuyorum. Daha sonra (16-bitlik) bloglara bölüyorum. 
            CreateHashKey(dateTime.ToString()); 
            CreateUserKey(sifre); 

            CircleKey16();
            CreateUserAndKey32();  //32 bitlik bloklar (keyler ve mesaj blokları için) oluşturuluyor.
            CircleKey32();
            CreateUserAndKey64(); //64 bitlik bloklar (keyler ve mesaj blokları için) oluşturuluyor.
            CircleKey64();

            userKey.Template.Clear();
            hashKey.Template.Clear();

            return StringConvert.ConvertMessage(userKey);
        }

        
        
        /// <summary>
        ///  İlk açılışta kullanıcıya özel key(64-bit) oluşturuyorum.(Hash Func Key)
        ///  İlk kayıt olduğu tarih : 04.04.2018 10.12.16 olsun.
        ///  key1 : (saat olan kısmı alıp en sondaki 16 2yı alıp 1'i en başa 6'yı en sona atıyorum.) => 11012166
        ///  key2 : (tarih olan kısmı alıyorum.) => 04042018 
        ///  Eğer tarih 18 karakter ise (4.04.2018) başa "0" karakterini ben ekliyorum
        ///  En son key1^key2 işlemine sokup tek bir unique key oluşturuyorum
        /// </summary>
        /// <param name="date">Kullanıcının ilk kayıt olduğu tarih</param>
        private static void CreateHashKey(string date)
        {
            string key1=string.Empty, key2=string.Empty;
            int strLenght = date.Length;
            key1 = date.Substring(strLenght - 2, 1) + date.Substring(strLenght - 8, 2) +
                   date.Substring(strLenght - 5, 2) + date.Substring(strLenght - 2, 2) +
                   date.Substring(strLenght - 1, 1);
            
            if (strLenght == 18)
                key2 = "0"+date.Substring(0, 1) + date.Substring(2, 2) + date.Substring(5, 4);

            if (strLenght == 19)
                key2 = date.Substring(0, 2) + date.Substring(3, 2) + date.Substring(6, 4);

            key1= StringConvert.ToBinary(StringConvert.ConvertToByteArray(key1, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
            key2= StringConvert.ToBinary(StringConvert.ConvertToByteArray(key2, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
            string tempKey = StringRotate.XOR(key1,key2);

            hashKey.Key16 = new List<string>();

            for (int i = 0; i < 64; i += 16)
                hashKey.Key16.Add(tempKey.Substring(i, 16));

        }

        /// <summary>
        /// Kullanıcının girdiği şifreyi alıyorum. Bit tabanına çeviriyorum. 
        /// Girilen şifre bit tabanında 192-bit olacak şekilde ayarlıyorum. (Padding ekliyorum.)
        /// Daha sonra 16-bitlik bloklara bölüyorum.
        /// </summary>
        /// <param name="value">Kullanıcının Girdiği Şifre</param>
        private static void CreateUserKey(string value)
        {
            string userPwd = StringConvert.ToBinary(StringConvert.ConvertToByteArray(value, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
            userPwd = StringRotate.Padding(userPwd);

            userKey.Key16 = new List<string>();

            for (int i = 0; i < 192; i += 16)
                userKey.Key16.Add(userPwd.Substring(i, 16));

        }        

        
        
        /// <summary>
        /// Mesajı daha önce 16-bitlik bloklara ayırmıştım. Şimdi her blogu kendi içerisinde şifreleme yapılacak.
        /// Örneğin; A-B-C-D tipinde bir mesaj bloğunda;
        /// 1 -->> A bloğunu alıyorum. Listeden siliyorum.
        /// 2 -->> A bloğunu 2 kere sola Shift ediyorum
        /// 3 -->> Anahtar ile XOR işlemine tabi tutuyorum.
        /// 4 -->> Listeden Kalan B-C-D bloklarını F fonksiyonuna tabi tutuyorum.
        /// 5 -->> F fonksiyonundan çıkan sonucu A bloğu ile XOR işlemini yapıyorum.
        /// 6 -->> En sonda A bloğunu listenin sonuna ekliyorum. (B-C-D-A)
        /// 7 -->> Bu işlemi Mesaj bloğu (12) tur boyunca yapıyorum.
        /// 8 -->> Fakat her turda ilgili anahtar sola 1 bit kaydırılıyor. 
        /// 9 -->> Bu işlemler 16 tur boyunca devam ediyor.
        /// </summary>
        private static void CircleKey16()
        {
            userKey.Template.AddRange(userKey.Key16);
            hashKey.Template.AddRange(hashKey.Key16);

            for (int i = 0; i < 16; i++)   // 16 tur dönecek.
            {
                for (int j = 0; j < MesajBlokSayisi; j++)  //Mesaj-Blok sayısı kadar dönecek.
                {
                    string mesaj = userKey.Template[j]; //Bloktaki mesajı alıyorum.
                    userKey.Template.RemoveAt(0);       //Listeden elemanı sil 

                    mesaj = StringRotate.LeftRotateShift(mesaj, 2);  //Sola kaydırıyorum.

                    mesaj = StringRotate.OR(mesaj, hashKey.Template[i / 4]); //Key (XOR) mesaj

                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, j % 4)); //mesajı F fonksiyonu ile XOR işlemi yapıyorum.

                    userKey.Template.Add(mesaj);  //Listeye ekliyorum.
                }
                //Her 4 turda bir anahtar değişecek. Fakat her turun ilgili anahtar sola 1 bit kaydırılıyor.
                hashKey.Template[i / 4] = StringRotate.LeftRotateShift(hashKey.Template[i / 4], 1); 
            }
        }

        /// <summary>
        /// Mesajı daha önce 32-bitlik bloklara ayırmıştım. Şimdi her blogu kendi içerisinde şifreleme yapılacak.
        /// Örneğin; A-B-C-D tipinde bir mesaj bloğunda;
        /// 1 -->> A bloğunu alıyorum. Listeden siliyorum.
        /// 2 -->> A bloğunu 1 kere sola Shift ediyorum
        /// 3 -->> Anahtar ile AND işlemine tabi tutuyorum.
        /// 4 -->> Listeden Kalan B-C-D bloklarını F fonksiyonuna tabi tutuyorum.
        /// 5 -->> F fonksiyonundan çıkan sonucu A bloğu ile XOR işlemini yapıyorum.
        /// 6 -->> En sonda A bloğunu listenin sonuna ekliyorum. (B-C-D-A)
        /// 7 -->> Bu işlemi Mesaj bloğu (12) tur boyunca yapıyorum.
        /// 8 -->> Fakat her turda ilgili anahtar sola 1 bit kaydırılıyor. 
        /// 9 -->> Bu işlemler 32 tur boyunca devam ediyor.
        /// </summary>
        private static void CircleKey32()
        {
            userKey.Template.AddRange(userKey.Key32);
            hashKey.Template.AddRange(hashKey.Key32);
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < MesajBlokSayisi; j++)
                {
                    string mesaj = userKey.Template[j];
                    userKey.Template.RemoveAt(0);

                    mesaj = StringRotate.LeftRotateShift(mesaj, 1);

                    mesaj = StringRotate.AND(mesaj, hashKey.Template[i / 8]);

                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, j % 4));

                    userKey.Template.Add(mesaj);
                }

                hashKey.Template[i / 8] = StringRotate.LeftRotateShift(hashKey.Template[i / 8], 1);
            }
        }

        /// <summary>
        /// Mesajı daha önce 64-bitlik bloklara ayırmıştım. Şimdi her blogu kendi içerisinde şifreleme yapılacak.
        /// Örneğin; A-B-C-D tipinde bir mesaj bloğunda;
        /// 1 -->> A bloğunu alıyorum. Listeden siliyorum.
        /// 2 -->> A bloğunu 2 kere sola Shift ediyorum
        /// 3 -->> Anahtar ile AND işlemine tabi tutuyorum.
        /// 4 -->> Listeden Kalan B-C-D bloklarını F fonksiyonuna tabi tutuyorum.
        /// 5 -->> F fonksiyonundan çıkan sonucu A bloğu ile XOR işlemini yapıyorum.
        /// 6 -->> En sonda A bloğunu listenin sonuna ekliyorum. (B-C-D-A)
        /// 7 -->> Bu işlemi Mesaj bloğu (12) tur boyunca yapıyorum.
        /// 8 -->> Fakat her turda ilgili anahtar sola 1 bit kaydırılıyor. 
        /// 9 -->> Bu işlemler 64 tur boyunca devam ediyor.
        /// </summary>
        private static void CircleKey64()
        {
            userKey.Template.AddRange(userKey.Key64);
            hashKey.Template.AddRange(hashKey.Key64);

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < MesajBlokSayisi; j++)
                {
                    string mesaj = userKey.Template[j];
                    userKey.Template.RemoveAt(0);

                    mesaj = StringRotate.LeftRotateShift(mesaj, 2);

                    mesaj = StringRotate.AND(mesaj, hashKey.Template[i / 16]);

                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, j % 4));

                    userKey.Template.Add(mesaj);
                }
                hashKey.Template[i / 16] = StringRotate.LeftRotateShift(hashKey.Template[i / 16], 1);
            }
        }


        /// <summary>
        /// 16-bitlik blokları 32-bitlik bloklara çıkarmak için 
        /// şifrelenmemiş olan 16-bitlik bloklar ile şifrelenmiş blokları birleştiriyorum.
        /// </summary>
        private static void CreateUserAndKey32()
        {
            userKey.Key32 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
                userKey.Key32.Add(userKey.Template[i] + userKey.Key16[i]);
            
            hashKey.Key32 = new List<string>();

            for (int i = 0; i < hashKey.Template.Count; i++)
                hashKey.Key32.Add(hashKey.Template[i] + hashKey.Template[i]);
 
            userKey.Template.Clear();
            hashKey.Template.Clear();
        }

        /// <summary>
        /// 32-bitlik blokları 64-bitlik bloklara çıkarmak için 
        /// şifrelenmemiş olan 32-bitlik bloklar ile şifrelenmiş blokları birleştiriyorum.
        /// </summary>
        private static void CreateUserAndKey64()
        {
            userKey.Key64 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
                userKey.Key64.Add(userKey.Template[i] + userKey.Key32[i]);

            hashKey.Key64 = new List<string>();
            for (int i = 0; i < hashKey.Template.Count; i++)
                hashKey.Key64.Add(hashKey.Template[i] + hashKey.Key32[i]);

            userKey.Template.Clear();
            hashKey.Template.Clear();
        }



        /// <summary>
        /// Gelen count parametresine göre ilgili bloklar ilgili fonksiyonlar ile şifrelenecektir.
        /// </summary>
        /// <param name="list">Şifrelenecek olan bloklar</param>
        /// <param name="count">Şifreleme fonksiyonunu belirleyecek olan değer</param>
        /// <returns>Şifrelenmiş olan blok</returns>
        private static string F_Function(List<string> list, int count)
        {
            string temp = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                switch (count)
                {
                    case 0: temp = StringRotate.AND(temp, (list[i]));break;
                    case 1: temp = StringRotate.XOR(temp, StringRotate.NOT(list[i]));break;
                    case 2: temp = StringRotate.NOT(StringRotate.XOR(temp, list[i]));break;
                    case 3: temp = StringRotate.XOR(temp, StringRotate.OR(temp, StringRotate.NOT(list[i])));break;
                }
            }
            return temp;
        } 
    }
}
