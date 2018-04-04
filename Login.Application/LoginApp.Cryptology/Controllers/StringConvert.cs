using LoginApp.Cryptology.Models;
using System;
using System.Linq;
using System.Text;

namespace LoginApp.Cryptology.Controllers
{
    public class StringConvert
    {
        public static byte[] ConvertToByteArray(string str, Encoding encoding) => encoding.GetBytes(str);
        public static String ToBinary(Byte[] data) =>
                             string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        public static string ConvertMessage(UserKey userKey)
        {
            StringBuilder builder = new StringBuilder();

            string gecici;
            foreach (var keyler in userKey.Key64)
            {
                for (int i = 0; i < keyler.Length; i += 16)
                {
                    gecici = Formatter(keyler.Substring(i, 16).ToString());
                    //int Binary = Convert.ToInt32(keyler.Substring(i, 4));
                    int Binary = Convert.ToInt32(gecici);
                    string Decimal = Convert.ToInt32(Binary.ToString(), 2).ToString();
                    string txtHexadecimal = Convert.ToString(Convert.ToInt32(Decimal), 16);
                    builder.Append(txtHexadecimal);
                }
            }
            return builder.ToString();
        }

        private static string Formatter(string message)
        {
            string value = message.Substring(0, 4);
            for (int i = 4; i < message.Length; i += 4)
                value = StringRotate.XOR(value, message.Substring(i, 4));

            return value;
        }
    }
}
