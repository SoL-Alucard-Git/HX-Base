namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TextBoxWaterMark : AisinoTXT
    {
        private Font font_0;
        private string string_0;

        public TextBoxWaterMark()
        {
            
            this.font_0 = new Font("微软雅黑", 8f, FontStyle.Italic);
            this.string_0 = "水印文字";
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (((m.Msg == 15) && (this.Text.Length == 0)) && !this.Focused)
            {
                Brush brush = new SolidBrush(Color.LightGray);
                Rectangle clientRectangle = base.ClientRectangle;
                using (Graphics graphics = base.CreateGraphics())
                {
                    graphics.DrawString(this.string_0, this.font_0, brush, clientRectangle);
                }
                brush.Dispose();
            }
        }

        [Category("Appearance"), Description("水印字体")]
        public Font FontWater
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
            }
        }

        [Description("水印默认文字"), Category("Appearance")]
        public string WaterMarkString
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

