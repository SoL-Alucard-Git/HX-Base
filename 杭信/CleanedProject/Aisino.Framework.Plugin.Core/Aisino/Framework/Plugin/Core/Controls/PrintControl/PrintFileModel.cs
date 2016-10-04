namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using Aisino.Framework.Plugin.Core;
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PrintFileModel
    {
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;

        public PrintFileModel()
        {
            
        }

        public string AssemblyName
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public string CanvasName
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string CanvasPath
        {
            get
            {
                string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Server"))
                {
                    directoryName = Path.Combine(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe").GetValue("Path").ToString(), @"Bin\");
                }
                return Path.Combine(directoryName, "../Config/Print/" + this.CanvasName);
            }
        }

        public string ClassName
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public string Id
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

