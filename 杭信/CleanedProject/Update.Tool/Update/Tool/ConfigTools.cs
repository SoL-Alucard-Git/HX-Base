namespace Update.Tool
{
    using System;
    using System.Configuration;
    using System.IO;

    public class ConfigTools
    {
        public ConfigTools()
        {
           
        }

        public static string Get(string configFilePath, string key)
        {
            if (File.Exists(configFilePath))
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                    ExeConfigFilename = configFilePath
                };
                System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                foreach (string str in configuration.AppSettings.Settings.AllKeys)
                {
                    if (str == key)
                    {
                        return configuration.AppSettings.Settings[key].Value.ToString();
                    }
                }
            }
            return "";
        }

        public static void Set(string configFilePath, string key, string value)
        {
            if (File.Exists(configFilePath))
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {
                    ExeConfigFilename = configFilePath
                };
                System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                bool flag = false;
                foreach (string str in configuration.AppSettings.Settings.AllKeys)
                {
                    if (str == key)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    configuration.AppSettings.Settings.Remove(key);
                }
                configuration.AppSettings.Settings.Add(key, value);
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}

