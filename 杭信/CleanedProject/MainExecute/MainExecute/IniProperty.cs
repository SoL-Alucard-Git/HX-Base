namespace MainExecute
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class IniProperty
    {
        internal static string defaultSectionName = "OldNewTaxCode";
        internal static string defaultUnInstallSectionName = "InstallInfo";
        internal static Dictionary<string, Dictionary<string, string>> dictTaxCode = new Dictionary<string, Dictionary<string, string>>();
        internal static Dictionary<string, Dictionary<string, string>> dictUninstallInfo = new Dictionary<string, Dictionary<string, string>>();
        internal static string iniFilePath = "";
        internal static string uninstallFilePath = "";

        private static void GetIniFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            iniFilePath = folderPath.Substring(0, folderPath.IndexOf(@"\")) + @"\新旧税号对照.ini";
        }

        public static void GetUninstallInfo(string uninstallPath)
        {
            if (File.Exists(uninstallPath))
            {
                uninstallFilePath = uninstallPath;
            }
        }

        public static void InitProperty()
        {
            GetIniFilePath();
            if (File.Exists(iniFilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(iniFilePath))
                    {
                        string str = "";
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        string key = reader.ReadLine();
                        if ((key != null) && (key != ""))
                        {
                            key = key.Substring(key.IndexOf('[') + 1, (key.IndexOf(']') - key.IndexOf('[')) - 1);
                        }
                        while ((str = reader.ReadLine()) != null)
                        {
                            if (str.Trim() != "")
                            {
                                if ((str.IndexOf('[') > -1) && (str.IndexOf(']') > -1))
                                {
                                    dictTaxCode.Add(key, dictionary);
                                    key = str.Substring(str.IndexOf('[') + 1, (str.IndexOf(']') - str.IndexOf('[')) - 1);
                                    dictionary = new Dictionary<string, string>();
                                }
                                else
                                {
                                    dictionary.Add(str.Substring(0, str.IndexOf('=')), str.Substring(str.IndexOf('=') + 1));
                                }
                            }
                        }
                        if ((key != null) && (key.Trim() != ""))
                        {
                            dictTaxCode.Add(key, dictionary);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static void InitUninstallInfo()
        {
            if (File.Exists(uninstallFilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(uninstallFilePath, Encoding.Default))
                    {
                        string str = "";
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        string key = reader.ReadLine();
                        if ((key != null) && (key != ""))
                        {
                            key = key.Substring(key.IndexOf('[') + 1, (key.IndexOf(']') - key.IndexOf('[')) - 1);
                        }
                        while ((str = reader.ReadLine()) != null)
                        {
                            if (str.Trim() != "")
                            {
                                if ((str.IndexOf('[') > -1) && (str.IndexOf(']') > -1))
                                {
                                    dictTaxCode.Add(key, dictionary);
                                    key = str.Substring(str.IndexOf('[') + 1, (str.IndexOf(']') - str.IndexOf('[')) - 1);
                                    dictionary = new Dictionary<string, string>();
                                }
                                else
                                {
                                    dictionary.Add(str.Substring(0, str.IndexOf('=')), str.Substring(str.IndexOf('=') + 1));
                                }
                            }
                        }
                        if ((key != null) && (key.Trim() != ""))
                        {
                            dictUninstallInfo.Add(key, dictionary);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static void SaveIniFile()
        {
            if (File.Exists(iniFilePath))
            {
                File.Delete(iniFilePath);
            }
            using (StreamWriter writer = new StreamWriter(iniFilePath))
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> pair in dictTaxCode)
                {
                    writer.WriteLine("[" + pair.Key + "]");
                    foreach (KeyValuePair<string, string> pair2 in pair.Value)
                    {
                        writer.WriteLine(pair2.Key + "=" + pair2.Value);
                    }
                }
                writer.Flush();
            }
        }

        public static void SaveUninstallInfo()
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in dictUninstallInfo)
            {
                builder.Append("[" + pair.Key + "]\r\n");
                foreach (KeyValuePair<string, string> pair2 in pair.Value)
                {
                    builder.Append(pair2.Key + "=" + pair2.Value + "\r\n");
                }
            }
            if (builder.ToString() != "")
            {
                File.WriteAllText(uninstallFilePath, builder.ToString(), Encoding.Default);
            }
        }

        public static void SetProperty(string key, string value)
        {
            SetProperty(defaultSectionName, key, value);
        }

        public static void SetProperty(string sectionName, string key, string value)
        {
            if (dictTaxCode.Keys.Contains<string>(sectionName))
            {
                if (dictTaxCode[sectionName].Keys.Contains<string>(key))
                {
                    dictTaxCode[sectionName][key] = value;
                }
                else
                {
                    dictTaxCode[sectionName].Add(key, value);
                }
            }
            else
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add(key, value);
                dictTaxCode.Add(sectionName, dictionary);
            }
        }

        public static void SetUninstallInfo(string newFolderName, string oldFolderName)
        {
            SetUninstallInfo(defaultUnInstallSectionName, newFolderName, oldFolderName);
        }

        public static void SetUninstallInfo(string sectionName, string newFolderName, string oldFolderName)
        {
            if (dictUninstallInfo.Keys.Contains<string>(sectionName))
            {
                if (dictUninstallInfo[sectionName].Keys.Contains<string>("INSTDIR"))
                {
                    dictUninstallInfo[sectionName]["INSTDIR"] = dictUninstallInfo[sectionName]["INSTDIR"].Replace(oldFolderName, newFolderName);
                }
                if (dictUninstallInfo[sectionName].Keys.Contains<string>("InstallDirRegKey"))
                {
                    dictUninstallInfo[sectionName]["InstallDirRegKey"] = dictUninstallInfo[sectionName]["InstallDirRegKey"].Replace(oldFolderName, newFolderName);
                }
            }
        }
    }
}

