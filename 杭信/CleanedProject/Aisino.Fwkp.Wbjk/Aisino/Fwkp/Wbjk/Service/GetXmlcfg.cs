namespace Aisino.Fwkp.Wbjk.Service
{
    using System;

    public class GetXmlcfg
    {
        public static string FileTableHead = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_FileTableHead.xml");
        public static string IniConfig = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk_config.ini");
        public static string XmlFilePath = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Wbjk.xml");
    }
}

