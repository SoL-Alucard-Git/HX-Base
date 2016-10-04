namespace Aisino.Framework.Plugin.Core.Util
{
    using ICSharpCode.SharpZipLib.BZip2;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class ZipUtil
    {
        private ZipUtil()
        {
            
        }

        public static byte[] Compress(byte[] byte_0)
        {
            MemoryStream stream = new MemoryStream();
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true);
            stream2.Write(byte_0, 0, byte_0.Length);
            stream2.Close();
            return stream.ToArray();
        }

        private static bool smethod_0(string string_0, string string_1, object object_0)
        {
            if (!File.Exists(string_0))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + string_0 + " 不存在!");
            }
            FileStream baseOutputStream = null;
            ZipOutputStream stream2 = null;
            ZipEntry entry = null;
            bool flag = true;
            try
            {
                baseOutputStream = File.OpenRead(string_0);
                byte[] buffer = new byte[baseOutputStream.Length];
                baseOutputStream.Read(buffer, 0, buffer.Length);
                baseOutputStream.Close();
                baseOutputStream = File.Create(string_1);
                stream2 = new ZipOutputStream(baseOutputStream);
                entry = new ZipEntry(Path.GetFileName(string_0));
                stream2.PutNextEntry(entry);
                stream2.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (entry != null)
                {
                    entry = null;
                }
                if (stream2 != null)
                {
                    stream2.Finish();
                    stream2.Close();
                }
                if (baseOutputStream != null)
                {
                    baseOutputStream.Close();
                    baseOutputStream = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return flag;
        }

        public static byte[] UnCompress(byte[] byte_0)
        {
            int num;
            MemoryStream stream = new MemoryStream(byte_0);
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
            MemoryStream stream3 = new MemoryStream();
            byte[] buffer = new byte[0x400];
        Label_002C:
            num = stream2.Read(buffer, 0, buffer.Length);
            if (num > 0)
            {
                stream3.Write(buffer, 0, num);
                goto Label_002C;
            }
            stream2.Close();
            return stream3.ToArray();
        }

        public static void UnZip(string string_0, string string_1, string string_2)
        {
            new FastZip().ExtractZip(string_0, string_1, FastZip.Overwrite.Always, null, "", "", true);
        }

        public static bool Zip(string string_0, string string_1, string string_2)
        {
            if (Directory.Exists(string_0))
            {
                new FastZip().CreateZip(string_1, string_0, true, "");
                return true;
            }
            return (File.Exists(string_0) && smethod_0(string_0, string_1, string_2));
        }

        public static byte[] ZipCompress(byte[] byte_0)
        {
            MemoryStream stream = new MemoryStream();
            Stream stream2 = new BZip2OutputStream(stream);
            try
            {
                stream2.Write(byte_0, 0, byte_0.Length);
            }
            finally
            {
                stream2.Close();
                stream.Close();
            }
            return stream.ToArray();
        }
    }
}

