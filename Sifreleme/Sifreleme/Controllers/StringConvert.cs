using Sifreleme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sifreleme.Controllers
{
    public class StringConvert
    {
        public static byte[] ConvertToByteArray(string str, Encoding encoding) => encoding.GetBytes(str);
        public static String ToBinary(Byte[] data) =>
                             string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));

        public static void ConvertMessage(UserKey userKey)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            foreach (var keyler in userKey.Key64)
            {
                for (int i = 0; i < keyler.Length; i += 32)
                {
                    string txt = Convert.ToString(Convert.ToInt32(keyler.Substring(i+12, 8)), 16);
                    builder.Append(txt);
                }

            }
            // Console.WriteLine(builder);
            Console.WriteLine("Hash Çıktısı :  "+ builder.ToString().Substring(0, 30).Length);
            Console.WriteLine("\n\n"+builder.ToString().Substring(0,30));
        }
    }
}
