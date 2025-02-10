using System.Text;
using Zek.Cryptography;

Console.OutputEncoding = Encoding.UTF8;

var key = "33DCE30260A646AB90331DCBB0241F25";
for (int i = 0; i < 1000; i++)
{
    var cyper = AesHelper.Encrypt("გიორგი ზექალაშვილი", key);
    
    var text = AesHelper.Decrypt(cyper, key);

    Console.WriteLine(cyper + text);
}

Console.ReadKey();