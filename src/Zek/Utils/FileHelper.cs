using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zek.Utils
{
    public static class FileHelper
    {
        public static string ToComputerSize(string fileName, bool useAbbreviations = false)
        {
            return ToComputerSize(new FileInfo(fileName).Length, useAbbreviations);
        }

        public static string ToComputerSize(long fileSize, bool useAbbreviations = false)
        {
            double valor = fileSize;
            long i;
            var names = useAbbreviations
                ? new[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" }
                : new[] { "Bytes", "KBytes", "MBytes", "GBytes", "TBytes", "PBytes", "EBytes", "ZBytes", "YBytes" };
            for (i = 0; i < names.Length && valor >= 1024; i++)
                valor /= 1024.0;
            return $"{valor:#,###.00} {names[i]}";
        }



        public static string ParseFileName(string folderName)
        {
            if (string.IsNullOrEmpty(folderName)) return folderName;

            foreach (var c in Path.GetInvalidFileNameChars())
                folderName = folderName.Replace(c.ToString(), string.Empty);

            //foreach (var c in Path.GetInvalidPathChars())
            //    folderName = folderName.Replace(c.ToString(), string.Empty);

            return folderName;
        }


        public static string GetAvailableFileName(string path)
        {
            if (!File.Exists(path))
                return path;
            var pathWithoutExtension = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            var extension = Path.GetExtension(path);
            for (var i = 1; ; i++)
            {
                var fullPath = $"{pathWithoutExtension}({i}){extension}";
                if (!File.Exists(fullPath))
                    return fullPath;
            }
        }


        public static void CreateDirectoryIfNotExists(string file)
        {
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static bool CreateFileIfNotExists(string file)
        {
            CreateDirectoryIfNotExists(file);

            if (!File.Exists(file))
            {
                File.Create(file).Close();
                return true;
            }

            return false;
        }
    }
}
