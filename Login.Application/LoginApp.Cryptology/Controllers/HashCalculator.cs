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
        static HashCalculator()
        {
            hashKey = new HashKey();
            userKey = new UserKey();
            builder = new StringBuilder();

        } //Static Constructor sayesinde nesnelerin sadece bir kere (new) olmasını sağladım.

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

        private static void CreateHashKey(string date)
        {
            int strLenght = date.Length;
            string key = date.Substring(strLenght - 2, 1) + date.Substring(strLenght - 8, 2) +
                          date.Substring(strLenght - 5, 2) + date.Substring(strLenght - 2, 2) +
                          date.Substring(strLenght - 1, 1);

            string tempKey = StringConvert.ToBinary(StringConvert.ConvertToByteArray(key, Encoding.ASCII)).Trim().Replace(" ", string.Empty);

            hashKey.Key16 = new List<string>();

            for (int i = 0; i < 64; i += 16)
                hashKey.Key16.Add(tempKey.Substring(i, 16));

        }  //İlk açılışta kullanıcıya özel key(64-bit) oluşturuyorum.(Hash Func Key)
        private static void CreateUserKey(string value)
        {
            string userPwd = StringConvert.ToBinary(StringConvert.ConvertToByteArray(value, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
            userPwd = StringRotate.Padding(userPwd);

            userKey.Key16 = new List<string>();

            for (int i = 0; i < 192; i += 16)
                userKey.Key16.Add(userPwd.Substring(i, 16));


        }    //İlk açılışta kullanıcının şifresini (16-bit) bloklara ayırdım.(User Key)        


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

                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, j % 4));

                    userKey.Template.Add(mesaj);
                }
                hashKey.Template[i / 4] = StringRotate.LeftRotateShift(hashKey.Template[i / 4], 1);
            }
        }
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
        private static void CircleKey64()
        {
            userKey.Template.AddRange(userKey.Key64);
            hashKey.Template.AddRange(hashKey.Key64);

            for (int i = 0; i < 16; i++)
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


        private static void CreateUserAndKey32()
        {
            userKey.Key32 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
            {
                //userKey.Key32.Add(userKey.Template[i] + hashKey.Template[i%4]);
                userKey.Key32.Add(userKey.Template[i] + userKey.Key16[i]);
            }
            hashKey.Key32 = new List<string>();
            for (int i = 0; i < hashKey.Template.Count; i++)
            {
                hashKey.Key32.Add(hashKey.Template[i] + hashKey.Template[i]);
                //hashKey.Key32.Add(hashKey.Template[i] + userKey.Template[i]);
            }
            userKey.Template.Clear();
            hashKey.Template.Clear();
        }
        private static void CreateUserAndKey64()
        {
            userKey.Key64 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
            {
                //userKey.Key64.Add(hashKey.Template[i%4] + userKey.Key32[i]);
                userKey.Key64.Add(userKey.Template[i] + userKey.Key32[i]);
            }
            hashKey.Key64 = new List<string>();
            for (int i = 0; i < hashKey.Template.Count; i++)
            {
                //hashKey.Key64.Add(hashKey.Template[i] + userKey.Key32[i]);
                hashKey.Key64.Add(hashKey.Template[i] + hashKey.Key32[i]);
            }
            userKey.Template.Clear();
            hashKey.Template.Clear();

        }


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
        }  //Her 4 turda 1 fonksiyon değişiyor.
    }
}
