using System.Security.Cryptography;
using System.Text;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;

//long x = 1234561123432137567;

//var code = Base62Convert.Encode(x);

//var decoded = Base62Convert.Decode(code);


//Console.WriteLine(x);
//Console.WriteLine(code);
//Console.WriteLine(decoded);


using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
{
    // Generate 32 random bytes
    byte[] randomBytes = new byte[32];
    rng.GetBytes(randomBytes);


    // Convert the bytes to a hexadecimal string
    string hexString = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

    // Output the result
    Console.WriteLine(hexString);

}

for (int i = 0; i < 10; i++)
{
    var guid = Guid.NewGuid();
    var code1 = UrlEncoder.Encode(Base62Convert.Encode(guid));
    var encoded = UrlEncoder.Encode(Convert.ToBase64String(guid.ToByteArray()));
    Console.WriteLine(code1 + " = " + encoded);
}





Console.ReadKey();