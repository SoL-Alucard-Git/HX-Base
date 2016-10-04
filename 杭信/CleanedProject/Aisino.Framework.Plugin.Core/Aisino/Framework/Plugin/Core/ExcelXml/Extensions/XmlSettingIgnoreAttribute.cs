namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
    public sealed class XmlSettingIgnoreAttribute : Attribute
    {
        public XmlSettingIgnoreAttribute()
        {
            
        }
    }
}

