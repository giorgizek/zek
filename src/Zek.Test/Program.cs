using System.Text;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;

//long x = 1234561123432137567;

//var code = Base62Convert.Encode(x);

//var decoded = Base62Convert.Decode(code);


//Console.WriteLine(x);
//Console.WriteLine(code);
//Console.WriteLine(decoded);


for (int i = 0; i < 100; i++)
{
    var guid = Guid.NewGuid();
    var code1 = UrlEncoder.Encode(Base62Convert.Encode(guid));
    var encoded = UrlEncoder.Encode(Convert.ToBase64String(guid.ToByteArray()));
    Console.WriteLine(code1 + " = " + encoded);
}





Console.ReadKey();