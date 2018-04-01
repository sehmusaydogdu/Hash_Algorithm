namespace Sifreleme.Controllers
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
                if (key[i] == '0') temp += "0";
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
        }  // Bitleri sola kaydırma işlemini yapıyor.
        public static string RightRotateShift(string key, int shift)
        {
            shift %= key.Length;
            return key.Substring(key.Length - shift) + key.Substring(0, key.Length - shift);
        }  //Bitleri sağa kaydırma işlemini yapıyor.

        public static string Padding(string value)
        {
            int length = value.Length;
            while (length % 16 != 0)
            {
                if (length % 3 == 0) value += "0";
                else value += "1";

                length++;
            }
            return value;
        }  //Padding olayını hallettim. eksik olanları (100100100 şeklinde tamamlıyor.)
    }
}
