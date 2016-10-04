namespace ns15
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    [Serializable]
    internal class Class136
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private bool bool_4;
        private Color color_0;
        private Color color_1;
        private Font font_0;
        private Font font_1;
        private HorizontalAlignment horizontalAlignment_0;
        private HorizontalAlignment horizontalAlignment_1;
        private int int_0;
        private PageSettings pageSettings_0;
        private PrinterSettings printerSettings_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public Class136()
        {
            
            this.string_0 = string.Empty;
            this.horizontalAlignment_0 = HorizontalAlignment.Center;
            this.horizontalAlignment_1 = HorizontalAlignment.Center;
            this.string_1 = "黑体 大小：14";
            this.string_2 = "宋体 大小：10";
            this.font_0 = new Font("黑体", 14f);
            this.font_1 = new Font("宋体", 10f);
            this.color_0 = Color.Black;
            this.color_1 = Color.Black;
            this.bool_2 = false;
            this.bool_1 = false;
            this.bool_0 = true;
            this.bool_3 = true;
            this.bool_4 = true;
            this.int_0 = 30;
            this.pageSettings_0 = new PageSettings();
            this.printerSettings_0 = new PrinterSettings();
        }

        public string method_0()
        {
            return this.string_0;
        }

        public void method_1(string string_3)
        {
            this.string_0 = string_3;
        }

        public string method_10()
        {
            return this.string_1;
        }

        public void method_11(string string_3)
        {
            this.string_1 = string_3;
        }

        public Font method_12()
        {
            return this.font_1;
        }

        public void method_13(Font font_2)
        {
            this.font_1 = font_2;
        }

        public Color method_14()
        {
            return this.color_1;
        }

        public void method_15(Color color_2)
        {
            this.color_1 = color_2;
        }

        public string method_16()
        {
            return this.string_2;
        }

        public void method_17(string string_3)
        {
            this.string_2 = string_3;
        }

        public bool method_18()
        {
            return this.bool_0;
        }

        public void method_19(bool bool_5)
        {
            this.bool_0 = bool_5;
        }

        public HorizontalAlignment method_2()
        {
            return this.horizontalAlignment_0;
        }

        public bool method_20()
        {
            return this.bool_1;
        }

        public void method_21(bool bool_5)
        {
            this.bool_1 = bool_5;
        }

        public bool method_22()
        {
            return this.bool_2;
        }

        public void method_23(bool bool_5)
        {
            this.bool_2 = bool_5;
        }

        public bool method_24()
        {
            return this.bool_3;
        }

        public void method_25(bool bool_5)
        {
            this.bool_3 = bool_5;
        }

        public bool method_26()
        {
            return this.bool_4;
        }

        public void method_27(bool bool_5)
        {
            this.bool_4 = bool_5;
        }

        public int method_28()
        {
            return this.int_0;
        }

        public void method_29(int int_1)
        {
            this.int_0 = int_1;
        }

        public void method_3(HorizontalAlignment horizontalAlignment_2)
        {
            this.horizontalAlignment_0 = horizontalAlignment_2;
        }

        public PageSettings method_30()
        {
            return this.pageSettings_0;
        }

        public void method_31(PageSettings pageSettings_1)
        {
            this.pageSettings_0 = pageSettings_1;
        }

        public PrinterSettings method_32()
        {
            return this.printerSettings_0;
        }

        public void method_33(PrinterSettings printerSettings_1)
        {
            this.printerSettings_0 = printerSettings_1;
        }

        public HorizontalAlignment method_4()
        {
            return this.horizontalAlignment_1;
        }

        public void method_5(HorizontalAlignment horizontalAlignment_2)
        {
            this.horizontalAlignment_1 = horizontalAlignment_2;
        }

        public Font method_6()
        {
            return this.font_0;
        }

        public void method_7(Font font_2)
        {
            this.font_0 = font_2;
        }

        public Color method_8()
        {
            return this.color_0;
        }

        public void method_9(Color color_2)
        {
            this.color_0 = color_2;
        }
    }
}

