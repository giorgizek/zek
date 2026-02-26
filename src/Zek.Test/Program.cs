using BenchmarkDotNet.Running;
using System.Numerics;
using System.Text;
using Zek.Contracts;
using Zek.Test;
using Zek.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;


        // მაგალითი მონაცემები
        var nodes = new List<NodeDto>
        {
            new NodeDto { Id = 889989776, ParentId = 222222, Name = "ZZZZZZZZZZZ 1", Checked = true },


            // Root nodes
            new NodeDto { Id = 1, ParentId = null, Name = "Root 1", Checked = true },
            new NodeDto { Id = 2, ParentId = null, Name = "Root 2", Checked = false },
            new NodeDto { Id = 3, ParentId = null, Name = "Root 3", Checked = true },

            // Children of Root 1
            new NodeDto { Id = 4, ParentId = 1, Name = "Child 1.1", Checked = false },
            new NodeDto { Id = 5, ParentId = 1, Name = "Child 1.2", Checked = true },

            // Grandchildren of Child 1.1
            new NodeDto { Id = 6, ParentId = 4, Name = "Grandchild 1.1.1", Checked = true },
            new NodeDto { Id = 7, ParentId = 4, Name = "Grandchild 1.1.2", Checked = false },

            // Children of Root 2
            new NodeDto { Id = 8, ParentId = 2, Name = "Child 2.1", Checked = true },
            new NodeDto { Id = 9, ParentId = 2, Name = "Child 2.2", Checked = false },

            // Grandchildren of Child 2.1
            new NodeDto { Id = 10, ParentId = 8, Name = "Grandchild 2.1.1", Checked = true },
            new NodeDto { Id = 11, ParentId = 8, Name = "Grandchild 2.1.2", Checked = false },

            // Children of Root 3
            new NodeDto { Id = 12, ParentId = 3, Name = "Child 3.1", Checked = true },

            // Orphan node (parent does not exist)
            new NodeDto { Id = 13, ParentId = 99, Name = "Orphan Node", Checked = false },

            // Deeper level
            new NodeDto { Id = 14, ParentId = 6, Name = "Great-Grandchild 1.1.1.1", Checked = true },
            new NodeDto { Id = 15, ParentId = 6, Name = "Great-Grandchild 1.1.1.2", Checked = false },
        };

        var tree = TreeHelper.BuildTree(nodes);
        var ids = TreeHelper.FindIdsByIdLazy(tree, 8);

        PrintTree(tree);
        foreach (var ii in ids)
        {
            Console.WriteLine(ii);
        }


        Console.ReadKey();
        return;
        
        
        var r = RandomHelper.GetRandom().Next(123456, 99999999);

        var u1 = UrlShortener.Encode(r);
        var id = UrlShortener.Decode("48aUGpDio1S");
        Console.WriteLine(u1);
        Console.WriteLine(id);




        //BenchmarkRunner.Run<BenchmarkExecutor>();
        Console.ReadKey();
    }


    static void PrintTree(IEnumerable<TreeNodeDto> nodes, string indent = " ")
    {
        foreach (var node in nodes)
        {
            Console.WriteLine($"{indent}- {node.Name} (Id={node.Id})");
            if (node.Children.Count > 0)
            {
                PrintTree(node.Children, indent + "  ");
            }
        }
    }
}



public static class Base62Encoder
{
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string Encode(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        byte[] bytes = Encoding.UTF8.GetBytes(input);
        BigInteger value = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        StringBuilder result = new StringBuilder();

        while (value > 0)
        {
            value = BigInteger.DivRem(value, 62, out var remainder);
            result.Insert(0, Base62Chars[(int)remainder]);
        }

        return result.ToString();
    }

    public static string Decode(string base62)
    {
        if (string.IsNullOrEmpty(base62))
            return string.Empty;

        BigInteger value = BigInteger.Zero;
        foreach (char c in base62)
        {
            int index = Base62Chars.IndexOf(c);
            if (index < 0)
                throw new ArgumentException("Invalid Base62 character: " + c);

            value = value * 62 + index;
        }

        byte[] bytes = value.ToByteArray(isUnsigned: true, isBigEndian: true);
        return Encoding.UTF8.GetString(bytes);
    }
}