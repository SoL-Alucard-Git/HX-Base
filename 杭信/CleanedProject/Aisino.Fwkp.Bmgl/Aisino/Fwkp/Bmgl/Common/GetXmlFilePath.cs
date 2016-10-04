namespace Aisino.Fwkp.Bmgl.Common
{
    using System;

    public class GetXmlFilePath
    {
        public static string XmlFilePath = (AppDomain.CurrentDomain.BaseDirectory.Replace(@"\Bin\", "") + @"\Config\PlugIn\Aisino.Fwkp.Bmgl.xml");
    }
}

