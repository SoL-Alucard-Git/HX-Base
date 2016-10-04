namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.IO;

    public static class FolderExtensions
    {
        public static string[] GetFileList(string string_0)
        {
            return GetFileList(string_0, null);
        }

        public static string[] GetFileList(string string_0, string string_1)
        {
            if (string_0.IsNullOrEmpty())
            {
                string_1 = "*.*";
            }
            try
            {
                return Directory.GetFiles(string_0, string_1, SearchOption.AllDirectories);
            }
            catch
            {
                return null;
            }
        }
    }
}

