﻿namespace LoginApp.Cryptology.Controllers
{
    public class StringRotate
    {
        public static string XOR(string key, string value)
        {
            string temp = string.Empty;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == value[i]) temp += "0";
                else temp += "1";
            }

            return temp;
        }  // Verilen iki string ifadeye XOR işlemi uyguluyor.
        public static string AND(string key, string value)
        {
            string temp = string.Empty;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == '0' || value[i] == '0') temp += "0";
                else temp += "1";
            }

            return temp;
        }  // Verilen iki string ifadeye AND işlemi uyguluyor.
        public static string OR(string key, string value)
        {
            string temp = string.Empty;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == '0' && value[i] == '0') temp += "0";
                else temp += "1";
            }

            return temp;
        }   // Verilen iki string ifadeye OR işlemi uyguluyor.
        public static string NOT(string value)
        {
            string temp = string.Empty;
            foreach (char item in value)
            {
                if (item == '0') temp += "1";
                else temp += "0";
            }
            return temp;
        }              // Verilen string ifadeyi tersine çevirir.

        public static string LeftRotateShift(string key, int shift)
        {
            shift %= key.Length;
            return key.Substring(shift) + key.Substring(0, shift);
        }  // Bitleri (shift) adeti kadar sola kaydırma işlemini yapıyor.
        public static string RightRotateShift(string key, int shift)
        {
            shift %= key.Length;
            return key.Substring(key.Length - shift) + key.Substring(0, key.Length - shift);
        }  //Bitleri (shift) adeti kadar sağa kaydırma işlemini yapıyor.

        public static string Padding(string value)
        {
            int length = value.Length;
            while (length++ < 192)
            {
                switch (length%5)
                {
                    case 0:value += "0";break;
                    case 1:value += "1";break;
                    case 2:value += "0";break;
                    case 3:value += "1";break;
                    case 4:value += "1";break;
                }
            }
            return value;
        }  //Padding olayı gerçekleşiyor. Eksik olanları (100100100...) şeklinde tamamlıyor.
    }
}
