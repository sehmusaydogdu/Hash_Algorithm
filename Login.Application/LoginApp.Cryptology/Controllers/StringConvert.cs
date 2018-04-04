using LoginApp.Cryptology.Models;
using System;
using System.Linq;
using System.Text;

namespace LoginApp.Cryptology.Controllers
{
    public class StringConvert
    {

        /// <summary>
        /// Girilen şifrede her bir karakteri ASCII tablosunda Decimal'e çeviriyorum. (Örneğin; A = 65)
        /// </summary>
        /// <param name="str">Girilen şifre</param>
        /// <param name="encoding">Encoding Type</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(string str, Encoding encoding) => encoding.GetBytes(str);

        /// <summary>
        /// Byte[] dizisindeki her 1 elemanı 8-bitlik tabana çeviriyor ve 
        /// her 8 bitlik veriyi uç uca ekleyip tek String metin haline çeviriyor.
        /// </summary>
        /// <param name="data">Çevirilen Byte[] dizisi</param>
        /// <returns>Uç uca eklenen String ifadeyi geri döndürüyor.</returns>
        public static String ToBinary(Byte[] data) =>
                             string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));


        /// <summary>
        /// Tüm şifreleme işlemleri bittikten sonra bitleri 16 'lık tabanda çevirip (toplamda 48-bit olacak şekilde) 
        /// gelen veriyi String ifadeye çeviriyorum.
        /// </summary>
        /// <param name="userKey">Kullanıcının girdiği mesajın şifrelenmiş ve bloklanmış hali</param>
        /// <returns></returns>
        public static string ConvertMessage(UserKey userKey)
        {
            StringBuilder builder = new StringBuilder();
            string gecici;
            foreach (var keyler in userKey.Key64)
            {
                //Her bir 64-bitlik blogu 16-bitlik parçalara bölüyorum.
                //Her 16-bitlik parçadan ilk 4-biti alıp kendi içerisinde XOR işlemine katıyorum
                //Örneğin; [0,3] ile [4-7] XOR işelmine tutuyorum. ([0,3] ^ [4,7] ^ [8,11] ^ .... ^[61,64])
                for (int i = 0; i < keyler.Length; i += 16)
                {
                    gecici = Formatter(keyler.Substring(i, 16).ToString());                 //Xor işlemini yapan metot.
                    int Binary = Convert.ToInt32(gecici);                                   //"0010"veriyi 10luk tabana çevirmek için ilk önce int 'e dönüştürüyorum
                    string Decimal = Convert.ToInt32(Binary.ToString(), 2).ToString();      //10luk tabanı ikilik tabana dönüştürdüm.
                    string txtHexadecimal = Convert.ToString(Convert.ToInt32(Decimal), 16); //2lik tabanı 16'lık tabana dönüştürüyorum.
                    builder.Append(txtHexadecimal);
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// Her blogu kendisinden sonraki diğer blok ile XOR işlemine tabi tutuyorum
        /// </summary>
        /// <param name="message">Her bir blok</param>
        /// <returns></returns>
        private static string Formatter(string message)
        {
            string value = message.Substring(0, 4);
            for (int i = 4; i < message.Length; i += 4)
                value = StringRotate.XOR(value, message.Substring(i, 4));

            return value;
        }
    }
}
