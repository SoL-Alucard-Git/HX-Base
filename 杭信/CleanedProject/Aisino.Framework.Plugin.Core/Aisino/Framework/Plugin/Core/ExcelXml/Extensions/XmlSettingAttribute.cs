namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
    public sealed class XmlSettingAttribute : Attribute
    {
        [CompilerGenerated]
        private bool bool_0;
        private string string_0;

        public XmlSettingAttribute()
        {
            
            this.string_0 = "";
            this.Encrypt = false;
        }

        public XmlSettingAttribute(string string_1)
        {
            
            this.string_0 = string_1;
            this.Encrypt = false;
        }

        public bool Encrypt
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

