namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Net;

    public static class FileExtensions
    {
        public static int CompareFile(string string_0, string string_1)
        {
            bool flag = System.IO.File.Exists(string_0);
            bool flag2 = System.IO.File.Exists(string_1);
            if (!flag && !flag2)
            {
                return 0;
            }
            if (!flag && flag2)
            {
                return 1;
            }
            if (!flag || flag2)
            {
                DateTime lastWriteTime = System.IO.File.GetLastWriteTime(string_0);
                DateTime time2 = System.IO.File.GetLastWriteTime(string_1);
                if (lastWriteTime == time2)
                {
                    return 0;
                }
                if (lastWriteTime < time2)
                {
                    return 1;
                }
            }
            return -1;
        }

        public static string GetRelativePath(string string_0, string string_1)
        {
            int num;
            if (string_0 == null)
            {
                throw new ArgumentNullException("fromDirectory");
            }
            if (string_1 == null)
            {
                throw new ArgumentNullException("toPath");
            }
            if ((Path.IsPathRooted(string_0) && Path.IsPathRooted(string_1)) && (string.Compare(Path.GetPathRoot(string_0), Path.GetPathRoot(string_1), true, CultureInfo.CurrentCulture) != 0))
            {
                return string_1;
            }
            StringCollection strings = new StringCollection();
            string[] strArray = string_0.Split(new char[] { Path.DirectorySeparatorChar });
            string[] strArray2 = string_1.Split(new char[] { Path.DirectorySeparatorChar });
            int num3 = Math.Min(strArray.Length, strArray2.Length);
            int num2 = -1;
            for (num = 0; num < num3; num++)
            {
                if (string.Compare(strArray[num], strArray2[num], true, CultureInfo.CurrentCulture) != 0)
                {
                    break;
                }
                num2 = num;
            }
            if (num2 == -1)
            {
                return string_1;
            }
            for (num = num2 + 1; num < strArray.Length; num++)
            {
                if (strArray[num].Length > 0)
                {
                    strings.Add("..");
                }
            }
            for (num = num2 + 1; num < strArray2.Length; num++)
            {
                strings.Add(strArray2[num]);
            }
            string[] array = new string[strings.Count];
            strings.CopyTo(array, 0);
            return string.Join(Path.DirectorySeparatorChar.ToString(), array);
        }

        public static bool MergeFiles(string string_0, string string_1, string string_2)
        {
            bool flag;
            try
            {
                byte[] buffer = System.IO.File.ReadAllBytes(string_0);
                byte[] buffer2 = System.IO.File.ReadAllBytes(string_1);
                BinaryWriter writer = new BinaryWriter(System.IO.File.Open(string_2, FileMode.Create, FileAccess.Write));
                writer.Write(buffer);
                writer.Write(buffer2);
                writer.Close();
                return true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static int NumberOfLines(string string_0)
        {
            try
            {
                TextReader reader = new StreamReader(string_0);
                string str = reader.ReadToEnd();
                int num = (str.Length - str.Replace("\n", "").Length) + 1;
                reader.Close();
                return num;
            }
            catch
            {
                return -1;
            }
        }

        public static void smethod_0(string string_0, string string_1, string string_2, string string_3)
        {
            using (FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Uri requestUri = new Uri(Path.Combine(string_1, Path.GetFileName(string_0)));
                FtpWebRequest request = (FtpWebRequest) WebRequest.Create(requestUri);
                request.Credentials = new NetworkCredential(string_2, string_3);
                request.KeepAlive = false;
                request.Method = "STOR";
                request.UseBinary = true;
                request.ContentLength = stream.Length;
                request.Proxy = null;
                stream.Position = 0L;
                int count = 0x800;
                byte[] buffer = new byte[0x800];
                using (Stream stream2 = request.GetRequestStream())
                {
                    for (int i = stream.Read(buffer, 0, count); i != 0; i = stream.Read(buffer, 0, count))
                    {
                        stream2.Write(buffer, 0, i);
                    }
                }
            }
        }
    }
}

