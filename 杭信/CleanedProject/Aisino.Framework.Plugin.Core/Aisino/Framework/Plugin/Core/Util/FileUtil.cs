namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class FileUtil
    {
        private static string string_0;

        public static List<string> SearchDirectory(string string_1, string string_2)
        {
            List<string> list = new List<string>();
            DirectoryInfo info = new DirectoryInfo(string_1);
            foreach (FileInfo info2 in info.GetFiles())
            {
                if (info2.Extension.ToUpper() == string_2.ToUpper())
                {
                    list.Add(Path.Combine(info2.DirectoryName, info2.Name));
                }
            }
            return list;
        }

        public static string ApplicationRootPath
        {
            get
            {
                return string_0;
            }
            set
            {
                string_0 = value;
            }
        }
    }
}

