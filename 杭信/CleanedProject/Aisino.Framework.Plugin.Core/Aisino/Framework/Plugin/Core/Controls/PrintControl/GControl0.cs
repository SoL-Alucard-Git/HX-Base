namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Xml;
    using Pdf417.Code;
    public class GControl0 : AisinoPrintControl
    {
        protected string _DataBind;
        protected string _PrintText;
        protected string _text;

        public GControl0()
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
        }

        public GControl0(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
            string attribute = xmlElement_0.GetAttribute("DataBind");
            this.DataBind = attribute;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("PDF417");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if ((base._isPrint && (this.DataBind != string.Empty)) && !string.IsNullOrEmpty(this.PrintText))
            {
                RectangleF rect = new RectangleF(base.absoluteLocation, (SizeF) new Size((int) base.Width, (int) base.Height));
                int width = 2;
                int height = 2;
                PDF417Class class2 = new PDF417Class();
                class2.setText(this._PrintText);
                class2.Options = 0x80;
                class2.paintCode();
                Bitmap image = new Bitmap((class2.BitColumns * 2) + 1, (class2.CodeRows * 2) + 1);
                Graphics graphics = Graphics.FromImage(image);
                byte[] outBits = class2.OutBits;
                int num4 = (class2.BitColumns / 8) + 1;
                int num5 = -1;
                int num6 = 0;
                for (int i = 0; i < outBits.Length; i++)
                {
                    if ((i % num4) == 0)
                    {
                        num5++;
                        num6 = 0;
                        Console.Write("\n");
                    }
                    int num9 = outBits[i];
                    for (int j = 7; j >= 0; j--)
                    {
                        int num8 = (int) Math.Pow(2.0, (double) j);
                        if ((num9 & num8) != 0)
                        {
                            graphics.FillRectangle(Brushes.White, num6 * width, num5 * height, width, height);
                            Console.Write("1");
                        }
                        else
                        {
                            graphics.FillRectangle(Brushes.Black, num6 * width, num5 * height, width, height);
                            Console.Write("0");
                        }
                        num6++;
                    }
                }
                graphics_0.DrawImage(image, rect);
                image.Dispose();
                graphics.Dispose();
            }
        }

        public IntPtr GetStrPtr(byte[] byte_0)
        {
            int cb = Marshal.SizeOf(byte_0[0]) * byte_0.Length;
            IntPtr destination = Marshal.AllocHGlobal(cb);
            Marshal.Copy(byte_0, 0, destination, byte_0.Length);
            return destination;
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            this.PrintText = string.Empty;
            if ((this.DataBind.Length > 0) && dict.ContainsKey(this.DataBind))
            {
                object obj2 = dict[this.DataBind];
                this.PrintText = obj2 as string;
            }
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if ((this != null) && (this.DataBind != string.Empty))
            {
                element.SetAttribute("DataBind", this.DataBind);
            }
            return element;
        }

        public string DataBind
        {
            get
            {
                return this._DataBind;
            }
            set
            {
                this._DataBind = value;
            }
        }

        [Description("要打印的字符 在打印的时候临时计算")]
        public string PrintText
        {
            get
            {
                return this._PrintText;
            }
            protected set
            {
                if ((value != null) && (value.Length < 500))
                {
                    this._PrintText = value;
                }
                else
                {
                    this._PrintText = "";
                }
            }
        }
    }
}

