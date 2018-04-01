using Sifreleme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sifreleme.Controllers
{
    public class StringCalculator
    {
        private static HashKey hashKey = new HashKey();
        private static UserKey userKey = new UserKey();
        private static StringBuilder builder = new StringBuilder();
        private static int MesajBlokSayisi = 0;
        public static void Cryptology(string sifre)
        {
            CreateHashKey(DateTime.Now.ToString());
            //CreateHashKey(new DateTime(1996, 2, 10, 10, 35, 40).ToString());
            CreateUserKey(sifre);

            CircleKey16();
            CreateUserAndKey32();  //32 bitlik bloklar (keyler ve mesaj blokları için) oluşturuluyor.
            CircleKey32();
            CreateUserAndKey64(); //64 bitlik bloklar (keyler ve mesaj blokları için) oluşturuluyor.
            CircleKey64();
            StringConvert.ConvertMessage(userKey);
        }

        private static void CreateUserKey(string value)
        {
            string userPwd = StringConvert.ToBinary(StringConvert.ConvertToByteArray(value, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
            userPwd = StringRotate.Padding(userPwd);

            userKey.Key16 = new List<string>();

            for (int i = 1; i <= userPwd.Length; i++)
            {
                builder.Append(userPwd[i - 1]);
                if (i % 16 == 0 && i != 0)
                {
                    userKey.Key16.Add(builder.ToString());
                    builder.Clear();
                }
            }
            builder.Clear();
            MesajBlokSayisi = userKey.Key16.Count;           //Kaç tane mesaj bloğu olduğunu söylüyor.
        }    //İlk açılışta kullanıcının şifresini (16-bit) bloklara ayırdım.(User Key)        
     
        private static void CreateHashKey(string date)
        {
            int strLenght = date.Length;
            string key =  date.Substring(strLenght - 2, 1) + date.Substring(strLenght - 8, 2) +
                          date.Substring(strLenght - 5, 2) + date.Substring(strLenght - 2, 2) +
                          date.Substring(strLenght - 1, 1);
             
            string tempKey = StringConvert.ToBinary(StringConvert.ConvertToByteArray(key, Encoding.ASCII)).Trim().Replace(" ", string.Empty);

            hashKey.Key16 = new List<string>();

            for (int i = 1; i <= 64; i++)
            {
                builder.Append(tempKey[i - 1]);
                if (i % 16 == 0 && i != 0)
                {
                    hashKey.Key16.Add(builder.ToString());
                    builder.Clear();
                }
            }
            builder.Clear();
        }  //İlk açılışta kullanıcıya özel key(64-bit) oluşturuyorum.(Hash Func Key)


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

                    //Mesaj blok indexi çift ise sola(1-bit) tek ise sağa(1-bit) kaydır. 
                    mesaj = j % 2 == 0 ? StringRotate.LeftRotateShift(mesaj, 1) : StringRotate.RightRotateShift(mesaj, 1);

                    //Mesaj ile Keyi XOR işlemine tabi tuttum.
                    mesaj = StringRotate.XOR(mesaj, hashKey.Template[i / 4]);

                    //key'i sola 1-bit kaydırdım. 
                    hashKey.Template[i / 4] = (i % 4) % 2 == 0 ? StringRotate.LeftRotateShift(hashKey.Template[i / 4], 1) : StringRotate.RightRotateShift(hashKey.Template[i / 4], 1);

                    mesaj = StringRotate.AND(hashKey.Template[i / 4], mesaj);

                    //Mesaj blok indexi çift ise sola(2-bit) tek ise sağa(2-bit) kaydır. 
                    mesaj = j % 2 == 0 ? StringRotate.LeftRotateShift(mesaj, 2) : StringRotate.RightRotateShift(mesaj, 2);

                    //mesajın ilk bloguyla geriye kalanları XOR luyorum (Her 4 turda bir fonksiyon değişiyor.)
                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, i / 4));
                    userKey.Template.Add(mesaj);
                }
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

                    //Mesaj blok indexi çift ise sola(1-bit) tek ise sağa(1-bit) kaydır. 
                    mesaj = j % 2 == 1 ? StringRotate.LeftRotateShift(mesaj, 1) : StringRotate.RightRotateShift(mesaj, 1);

                    //Mesaj ile Keyi XOR işlemine tabi tuttum.
                    mesaj = StringRotate.XOR(mesaj, hashKey.Template[i / 8]);

                    //key'i sola 1-bit kaydırdım. 
                    hashKey.Template[i / 8] = (i % 8) % 2 == 1 ? StringRotate.LeftRotateShift(hashKey.Template[i / 8], 1) : StringRotate.RightRotateShift(hashKey.Template[i / 8], 1);

                    mesaj = StringRotate.AND(hashKey.Template[i / 8], StringRotate.NOT(mesaj));

                    //Mesaj blok indexi çift ise sola(2-bit) tek ise sağa(2-bit) kaydır. 
                    mesaj = j % 2 == 1 ? StringRotate.LeftRotateShift(mesaj, 2) : StringRotate.RightRotateShift(mesaj, 2);

                    //mesajın ilk bloguyla geriye kalanları XOR luyorum (Her 4 turda bir fonksiyon değişiyor.)
                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, i / 8));
                    userKey.Template.Add(mesaj);
                }
            }
        }
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

                    //Mesaj blok indexi çift ise sola(1-bit) tek ise sağa(1-bit) kaydır. 
                    mesaj = j % 2 == 1 ? StringRotate.LeftRotateShift(mesaj, 1) : StringRotate.RightRotateShift(mesaj, 1);

                    //Mesaj ile Keyi XOR işlemine tabi tuttum.
                    mesaj = StringRotate.XOR(mesaj, hashKey.Template[i / 16]);

                    //key'i sola 1-bit kaydırdım. 
                    hashKey.Template[i / 16] = (i % 16) % 2 == 1 ? StringRotate.LeftRotateShift(hashKey.Template[i / 16], 1) : StringRotate.RightRotateShift(hashKey.Template[i / 16], 1);

                    mesaj = StringRotate.AND(hashKey.Template[i / 16], StringRotate.NOT(mesaj));

                    //Mesaj blok indexi çift ise sola(2-bit) tek ise sağa(2-bit) kaydır. 
                    mesaj = j % 2 == 1 ? StringRotate.LeftRotateShift(mesaj, 2) : StringRotate.RightRotateShift(mesaj, 2);

                    //mesajın ilk bloguyla geriye kalanları XOR luyorum (Her 4 turda bir fonksiyon değişiyor.)
                    mesaj = StringRotate.XOR(mesaj, F_Function(userKey.Template, i / 16));
                    userKey.Template.Add(mesaj);
                }
            }
        }


        private static void CreateUserAndKey32()
        {
            userKey.Key32 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
            {
                //userKey.Key32.Add(userKey.Template[i] + StringRotate.NOT(userKey.Key16[i]));
                userKey.Key32.Add(userKey.Template[i]+userKey.Key16[i]);
            }
            hashKey.Key32 = new List<string>();
            for (int i = 0; i < hashKey.Template.Count; i++)
            {
                hashKey.Key32.Add(hashKey.Template[i] + StringRotate.NOT(hashKey.Key16[i]));
            }
            userKey.Template.Clear();
            hashKey.Template.Clear();


        }
        private static void CreateUserAndKey64()
        {
            userKey.Key64 = new List<string>();
            for (int i = 0; i < userKey.Template.Count; i++)
            {
                userKey.Key64.Add(userKey.Template[i] + userKey.Key32[i]);
                //userKey.Key32.Add(userKey.Template[i] + userKey.Key16[i]);
            }
            hashKey.Key64 = new List<string>();
            for (int i = 0; i < hashKey.Template.Count; i++)
            {
                hashKey.Key64.Add(hashKey.Template[i] + StringRotate.NOT(hashKey.Key32[i]));
            }
            userKey.Template.Clear();
            hashKey.Template.Clear();

        }

        private static string F_Function(List<string> list, int count)
        {
            string temp;
            temp = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (count % 3 == 0) temp = StringRotate.AND(temp, StringRotate.NOT(list[i]));
                else if (count % 3 == 1) temp = StringRotate.NOT(StringRotate.OR(temp, list[i]));
                else temp = StringRotate.XOR(temp, StringRotate.OR(temp, StringRotate.NOT(list[i])));
            }
            return temp;
        }  //Her 4 turda 1 fonksiyon değişiyor.
    }
}
