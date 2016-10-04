namespace InvAutomation.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance;

        static Settings()
        {
            
            defaultInstance = (Settings) SettingsBase.Synchronized(new Settings());
        }

        public Settings()
        {
            
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}

