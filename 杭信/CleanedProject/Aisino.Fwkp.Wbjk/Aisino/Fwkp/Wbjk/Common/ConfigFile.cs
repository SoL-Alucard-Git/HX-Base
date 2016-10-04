namespace Aisino.Fwkp.Wbjk.Common
{
    using System;

    public class ConfigFile
    {
        public static string FileTableHead = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_FileTableHead.xml");
        public static string GetIniConfigPath = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_config_c.ini");
        public static string GetIniConfigPath_1 = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_config_s.ini");
        public static string GetIniConfigPath_2 = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_config_f.ini");
        public static string GetIniConfigPath_3 = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_config_j.ini");
        public static string GetXmlFilePath = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk.xml");
    }
}

