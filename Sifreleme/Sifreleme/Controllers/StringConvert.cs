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
                for (int i = 0; i < keyler.Length; i += 16)
                {
                    int x = Convert.ToInt32(keyler.Substring(i, 4));
                    string tt = Convert.ToInt32(x.ToString(), 2).ToString();
                    string txt = Convert.ToString(Convert.ToInt32(tt), 16);
                    builder.Append(txt);
                }

            }
            Console.WriteLine(builder.ToString().Length);
            Console.WriteLine("\n\n"+builder);
        }
    }
}
