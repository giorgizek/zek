using System.IO.Compression;

namespace Zek.Utils
{
    public enum CompressionType
    {
        //None = 0,
        GZip = 1,
        //GzipOldCore = -1,
    }

    public class CompressHelper
    {
        public static byte[] Compress(byte[] buffer, CompressionType? compressionType = null)
        {
            switch (compressionType)
            {
                case CompressionType.GZip:
                    return GZipHelper.Compress(buffer);

                default:
                    return buffer;
            }
        }



        ///// <summary>
        ///// დეკომპრესია ბაიტების მასივის
        ///// </summary>
        ///// <param name="gzbuffer">დაკომპრესული ბაიტების მასივი (GZip)</param>
        ///// <returns>ჩვეულებრივი ბაიტების მასივი</returns>
        //private static byte[] DecompressOld(byte[] gzbuffer)
        //{
        //    if (gzbuffer == null) return null;

        //    using (var ms = new MemoryStream())
        //    {
        //        var msgLength = BitConverter.ToInt32(gzbuffer, 0);
        //        ms.Write(gzbuffer, 4, gzbuffer.Length - 4);

        //        var buffer = new byte[msgLength];

        //        ms.Position = 0;
        //        using (var zip = new GZipStream(ms, CompressionMode.Decompress))
        //        {
        //            zip.Read(buffer, 0, buffer.Length);

        //            ms.Flush();

        //            return buffer;
        //        }
        //    }
        //}
        public static byte[] Decompress(byte[] compressedBuffer, CompressionType? compressionType = null)
        {
            switch (compressionType)
            {
                case CompressionType.GZip:
                    return GZipHelper.Decompress(compressedBuffer);
                //case CompressType.GzipOldCore:
                //    return DecompressOld(compressedBuffer);
                default:
                    return compressedBuffer;
            }
        }
    }

    public class GZipHelper
    {
        public static byte[] Compress(byte[] buffer)
        {
            if (buffer == null) return null;

            using (var memStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(buffer, 0, buffer.Length);
                }
                return memStream.ToArray();
            }
        }
        public static byte[] Decompress(byte[] gzip)
        {
            if (gzip == null) return null;

            using (var memStream = new MemoryStream(gzip))
            using (var gzipStream = new GZipStream(memStream, CompressionMode.Decompress))
            {
                const int size = 4096;
                var buffer = new byte[size];
                using (var decompressedStream = new MemoryStream())
                {
                    int count;
                    do
                    {
                        count = gzipStream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            decompressedStream.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return decompressedStream.ToArray();
                }
            }
        }
    }
}
