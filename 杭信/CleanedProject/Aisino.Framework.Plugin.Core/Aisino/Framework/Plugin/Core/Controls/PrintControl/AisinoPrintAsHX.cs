namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class AisinoPrintAsHX : AisinoPrintControl
    {
        protected byte[] _Data1;
        protected byte[] _Data2;
        protected byte[] _Data3;
        protected byte[] _Data4;
        protected string _DataBind;
        protected string _PrintText;
        protected string _text;

        public AisinoPrintAsHX()
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
            this._Data1 = new byte[0x6d];
            this._Data2 = new byte[0x6d];
            this._Data3 = new byte[0x6d];
            this._Data4 = new byte[0x6d];
        }

        public AisinoPrintAsHX(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
            this._Data1 = new byte[0x6d];
            this._Data2 = new byte[0x6d];
            this._Data3 = new byte[0x6d];
            this._Data4 = new byte[0x6d];
            string attribute = xmlElement_0.GetAttribute("DataBind");
            this.DataBind = attribute;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("HXM");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if ((base._isPrint && (this.DataBind != string.Empty)) && !string.IsNullOrEmpty(this.PrintText))
            {
                RectangleF rect = new RectangleF(base.absoluteLocation, (SizeF) new Size((int) base.Width, (int) base.Height));
                AsHSEncode.ASHS_CODEINFO ashs_codeinfo = new AsHSEncode.ASHS_CODEINFO {
                    pData1 = this.GetStrPtr(this._Data1),
                    pData2 = this.GetStrPtr(this._Data2),
                    pData3 = this.GetStrPtr(this._Data3),
                    pData4 = this.GetStrPtr(this._Data4),
                    dwDataLength1 = this._Data1.Length,
                    dwDataLength2 = this._Data2.Length,
                    dwDataLength3 = this._Data3.Length,
                    dwDataLength4 = this._Data4.Length,
                    nEccelvel = 2,
                    nVersion = 11,
                    nNarrow = 0x683,
                    nadjBwc = 0x22c
                };
                int num = (int) PrinterUnitConvert.Convert((double) (rect.X * 10f), PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);
                int num2 = (int) PrinterUnitConvert.Convert((double) (rect.Y * 10f), PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);
                ashs_codeinfo.nPageLeft = num;
                ashs_codeinfo.nPageTop = num2;
                ashs_codeinfo.nPosX = 0;
                ashs_codeinfo.nPosY = 0;
                ashs_codeinfo.nSymbolSpace = 100;
                ashs_codeinfo.szPrinterName = string.Empty;
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                ashs_codeinfo.szJPEGFilePathName = path;
                AsHSEncode.HS_CreateJPEGFile(ref ashs_codeinfo);
                AsHSEncode.HS_CreateJPEGHandle(ref ashs_codeinfo);
                Image image = Image.FromFile(ashs_codeinfo.szJPEGFilePathName);
                graphics_0.DrawImage(image, rect);
                Point point = new Point((int) (this.absoluteLocation.X - 2f), (int) this.absoluteLocation.Y);
                Point[] points = new Point[] { new Point(point.X + 0x142, point.Y), new Point(point.X + 0x138, point.Y + 12), new Point(point.X + 0x142, point.Y + 0x18), new Point(point.X + 0x138, point.Y + 0x24), new Point(point.X + 0x142, point.Y + 0x30), new Point(point.X + 0x138, point.Y + 60), new Point(point.X + 0x142, point.Y + 0x48) };
                graphics_0.DrawLines(new Pen(Color.Black), points);
                image.Dispose();
                File.Delete(path);
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
                this._PrintText = value;
                try
                {
                    byte[] sourceArray = Convert.FromBase64String(value);
                    byte[] destinationArray = new byte[0x1b4];
                    if (sourceArray.Length > 0x1b4)
                    {
                        Array.Copy(sourceArray, 0, destinationArray, 0, 0x1b4);
                    }
                    else
                    {
                        Array.Copy(sourceArray, 0, destinationArray, 0, sourceArray.Length);
                    }
                    Array.Copy(destinationArray, 0, this._Data1, 0, 0x6d);
                    Array.Copy(destinationArray, 0x6d, this._Data2, 0, 0x6d);
                    Array.Copy(destinationArray, 0xda, this._Data3, 0, 0x6d);
                    Array.Copy(destinationArray, 0x147, this._Data4, 0, 0x6d);
                }
                catch
                {
                }
            }
        }
    }
}

