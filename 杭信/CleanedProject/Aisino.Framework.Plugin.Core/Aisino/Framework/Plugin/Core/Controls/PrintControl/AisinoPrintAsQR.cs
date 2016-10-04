namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using Aisino.Framework.QRCode.Codec;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class AisinoPrintAsQR : AisinoPrintControl
    {
        protected string _DataBind;
        protected string _PrintText;
        protected string _text;

        public AisinoPrintAsQR()
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
        }

        public AisinoPrintAsQR(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._DataBind = string.Empty;
            this._text = string.Empty;
            this._PrintText = string.Empty;
            string attribute = xmlElement_0.GetAttribute("DataBind");
            this.DataBind = attribute;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("QRM");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if ((base._isPrint && (this.DataBind != string.Empty)) && !string.IsNullOrEmpty(this.PrintText))
            {
                RectangleF rect = new RectangleF(base.absoluteLocation, (SizeF) new Size((int) base.Width, (int) base.Height));
                QRCodeEncoder encoder = new QRCodeEncoder();
                switch (this._PrintText)
                {
                    case "Byte":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                        break;

                    case "AlphaNumeric":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                        break;

                    case "Numeric":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                        break;
                }
                encoder.QRCodeVersion = 5;
                encoder.QRCodeScale = 6;
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                string content = this._PrintText;
                Bitmap image = encoder.Encode(content);
                graphics_0.DrawImage(image, rect);
                image.Dispose();
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
                if ((value != null) && (value.Length < 100))
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

