﻿using System.Security.Cryptography;
using System.Text;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;

//long x = 1234561123432137567;

//var code = Base62Convert.Encode(x);

//var decoded = Base62Convert.Decode(code);


//Console.WriteLine(x);
//Console.WriteLine(code);
//Console.WriteLine(decoded);


var a1 = new DateTime(2025, 1, 10);
var a2 = new DateTime(2025, 1, 20);

var b1 = new DateTime(2025, 1, 20);
var b2 = new DateTime(2025, 1, 30);

Console.WriteLine(OverlapHelper.Overlaps(a1, a2, b1, b2));



return;

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