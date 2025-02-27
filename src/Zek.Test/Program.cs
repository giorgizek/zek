using System.Text;
using Zek.Utils;

Console.OutputEncoding = Encoding.UTF8;

//long x = 1234561123432137567;

//var code = Base62Convert.Encode(x);

//var decoded = Base62Convert.Decode(code);


//Console.WriteLine(x);
//Console.WriteLine(code);
//Console.WriteLine(decoded);


var hashset = new HashSet<string>();

for (int i = 0; i < 10000000; i++)
{
    var guid = Guid.NewGuid();
    var code1 = Base62Convert.Encode(guid);
    if (!hashset.Add(code1))
    {
        Console.WriteLine($"{code1} = duplicated");
    }

    Console.WriteLine(hashset.Count);
}





Console.ReadKey();