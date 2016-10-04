namespace Update.Tool
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
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder builder = new StringBuilder(0x400);
                GetPrivateProfileString(Section, Key, NoText, builder, 0x400, iniFilePath);
                return builder.ToString();
            }
            return string.Empty;
        }

        public static List<SetupFile> ReadSetupConfig(string filePath)
        {
            List<SetupFile> list = new List<SetupFile>();
            int num = 0;
            string str = ReadIniData("Settings", "NumFields", "0", filePath);
            if (str != "")
            {
                num = Convert.ToInt32(str);
            }
            for (int i = 1; i <= num; i++)
            {
                SetupFile item = new SetupFile();
                string section = "Field" + i.ToString();
                item.Name = ReadIniData(section, "Type", "", filePath);
                item.Ver = ReadIniData(section, "Ver", "", filePath);
                item.Kind = ReadIniData(section, "SoftKind", "", filePath);
                item.Folder = ReadIniData(section, "Path", "", filePath);
                list.Add(item);
            }
            return list;
        }

        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                if (WritePrivateProfileString(Section, Key, Value, iniFilePath) == 0L)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string string_0, string string_1, string string_2, string string_3);
        public static bool WriteSetupConfig(SetupFile stFile, string filePath)
        {
            try
            {
                List<SetupFile> list = ReadSetupConfig(filePath);
                bool flag2 = false;
                string section = string.Empty;
                int num = 1;
                while (num <= list.Count)
                {
                    SetupFile file = list[num - 1];
                    if ((file.Name == stFile.Name) && (file.Kind == stFile.Kind))
                    {
                        goto Label_0067;
                    }
                    num++;
                }
                goto Label_007B;
            Label_0067:
                flag2 = true;
                section = "Field" + num.ToString();
            Label_007B:
                if (flag2)
                {
                    WriteIniData("Settings", "NumFields", list.Count.ToString(), filePath);
                }
                else
                {
                    WriteIniData("Settings", "NumFields", (list.Count + 1).ToString(), filePath);
                }
                WriteIniData(section, "Type", stFile.Name, filePath);
                WriteIniData(section, "Ver", stFile.Ver, filePath);
                WriteIniData(section, "SoftKind", stFile.Kind, filePath);
                WriteIniData(section, "Path", stFile.Folder, filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

