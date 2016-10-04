namespace Aisino.Framework.Plugin.Core.PrintGrid
{
    using System;
    using System.Windows.Forms;

    public class PrinterItems
    {
        private HorizontalAlignment horizontalAlignment_0;
        private string string_0;

        public PrinterItems(string string_1, HorizontalAlignment horizontalAlignment_1)
        {
            
            this.string_0 = string_1;
            this.horizontalAlignment_0 = horizontalAlignment_1;
        }

        public HorizontalAlignment Align
        {
            get
            {
                return this.horizontalAlignment_0;
            }
            set
            {
                this.horizontalAlignment_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

