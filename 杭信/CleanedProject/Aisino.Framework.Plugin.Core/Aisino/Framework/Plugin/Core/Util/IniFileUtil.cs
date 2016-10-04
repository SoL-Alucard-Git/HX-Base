namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IniFileUtil
    {
        public IniFileUtil()
        {
            
        }

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string string_0, string string_1, string string_2, StringBuilder stringBuilder_0, int int_0, string string_3);
        public static string ReadIniData(string string_0, string string_1, string string_2, string string_3)
        {
            if (File.Exists(string_3))
            {
                StringBuilder builder = new StringBuilder(0x400);
                GetPrivateProfileString(string_0, string_1, string_2, builder, 0x400, string_3);
                return builder.ToString();
            }
            return string.Empty;
        }

        public static List<SetupFile> ReadSetupConfig(string string_0)
        {
            List<SetupFile> list = new List<SetupFile>();
            int num = Convert.ToInt32(ReadIniData("Settings", "NumFields", "0", string_0));
            for (int i = 1; i <= num; i++)
            {
                SetupFile item = new SetupFile();
                string str = "Field" + i.ToString();
                item.Name = ReadIniData(str, "Type", "", string_0);
                item.Ver = ReadIniData(str, "Ver", "", string_0);
                item.Kind = ReadIniData(str, "SoftKind", "", string_0);
                item.Folder = ReadIniData(str, "Path", "", string_0);
                list.Add(item);
            }
            return list;
        }

        public static bool WriteIniData(string string_0, string string_1, string string_2, string string_3)
        {
            if (!File.Exists(string_3))
            {
                return false;
            }
            if (WritePrivateProfileString(string_0, string_1, string_2, string_3) == 0L)
            {
                return false;
            }
            return true;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string string_0, string string_1, string string_2, string string_3);
        public static bool WriteSetupConfig(SetupFile setupFile_0, string string_0)
        {
            try
            {
                List<SetupFile> list = ReadSetupConfig(string_0);
                bool flag2 = false;
                string str = string.Empty;
                int num = 1;
                while (num <= list.Count)
                {
                    SetupFile file = list[num - 1];
                    if ((file.Name == setupFile_0.Name) && (file.Kind == setupFile_0.Kind))
                    {
                        goto Label_005C;
                    }
                    num++;
                }
                goto Label_0070;
            Label_005C:
                flag2 = true;
                str = "Field" + num.ToString();
            Label_0070:
                if (flag2)
                {
                    str = "Field" + list.Count.ToString();
                    WriteIniData("Settings", "NumFields", list.Count.ToString(), string_0);
                }
                else
                {
                    WriteIniData("Settings", "NumFields", (list.Count + 1).ToString(), string_0);
                }
                WriteIniData(str, "Type", setupFile_0.Name, string_0);
                WriteIniData(str, "Ver", setupFile_0.Ver, string_0);
                WriteIniData(str, "SoftKind", setupFile_0.Kind, string_0);
                WriteIniData(str, "Path", setupFile_0.Folder, string_0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class SetupFile
        {
            public bool bNormal;
            public string Folder;
            public string Kind;
            public string Name;
            public string Ver;

            public SetupFile()
            {
                
                this.Name = string.Empty;
                this.Ver = string.Empty;
                this.Kind = string.Empty;
                this.Folder = string.Empty;
            }
        }
    }
}

