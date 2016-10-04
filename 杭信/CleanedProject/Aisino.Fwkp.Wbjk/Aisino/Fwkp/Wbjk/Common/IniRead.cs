namespace Aisino.Fwkp.Wbjk.Common
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IniRead
    {
        public static string path = ConfigFile.GetIniConfigPath;
        public static string path_1 = ConfigFile.GetIniConfigPath_1;
        public static string path_2 = ConfigFile.GetIniConfigPath_2;
        public static string path_3 = ConfigFile.GetIniConfigPath_3;
        public static string type = "c";

        public static string GetPrivateProfileString(string section, string key)
        {
            StringBuilder builder;
            if ((type == "c") || (type == "s"))
            {
                if (!File.Exists(path))
                {
                    CreateIniFile.Create();
                }
                builder = new StringBuilder(0x400);
                GetPrivateProfileString(section, key, string.Empty, builder, 0x400, path);
                return builder.ToString();
            }
            if (type == "f")
            {
                if (!File.Exists(path_2))
                {
                    CreateIniFile.Create_2();
                }
                builder = new StringBuilder(0x400);
                GetPrivateProfileString(section, key, string.Empty, builder, 0x400, path_2);
                return builder.ToString();
            }
            if (!File.Exists(path_3))
            {
                CreateIniFile.Create_3();
            }
            builder = new StringBuilder(0x400);
            GetPrivateProfileString(section, key, string.Empty, builder, 0x400, path_3);
            return builder.ToString();
        }

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        public static long WritePrivateProfileString(string section, string key, string val)
        {
            if ((type == "c") || (type == "s"))
            {
                return WritePrivateProfileString(section, key, val, path);
            }
            if (type == "f")
            {
                return WritePrivateProfileString(section, key, val, path_2);
            }
            return WritePrivateProfileString(section, key, val, path_3);
        }

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

