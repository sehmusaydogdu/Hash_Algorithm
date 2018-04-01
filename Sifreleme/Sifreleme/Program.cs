using Sifreleme.Controllers;
using System;
using System.Text;

namespace Sifreleme
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Şifrenizi giriniz  : ");
            string sifre = Console.ReadLine();

            StringCalculator.Cryptology(sifre);
            //string tempKey = StringConvert.ToBinary(StringConvert.ConvertToByteArray(sifre, Encoding.ASCII)).Trim().Replace(" ", string.Empty);
           // Console.WriteLine(tempKey);
            Console.Read();
        }
    }
}
