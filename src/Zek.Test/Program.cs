using BenchmarkDotNet.Running;
using System.Security.Cryptography;
using System.Text;
using Zek.Office;
using Zek.Test;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;



for (int i = 0; i < 1000; i++)
{
    //var s = PasswordHelper.Generate(10, 10, 10, 0);
    var s = RandomNumberGenerator.GetString(PasswordHelper.Uppercase + PasswordHelper.Lowercase + PasswordHelper.Digits, 22);
    Console.WriteLine(s);
}

//BenchmarkRunner.Run<BenchmarkExecutor>();
Console.ReadKey();


public class Z
{
    public static byte[] GenerateRandomKey(int length)
    {
        byte[] array = new byte[length];
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(array);
        return array;
    }
}