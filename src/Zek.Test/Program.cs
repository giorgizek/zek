using System.Text;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;

long x = 1234561123432137567;

var code = Base62Convert.Encode(x);

var decoded = Base62Convert.Decode(code);


Console.WriteLine(x);
Console.WriteLine(code);
Console.WriteLine(decoded);


Console.ReadKey();