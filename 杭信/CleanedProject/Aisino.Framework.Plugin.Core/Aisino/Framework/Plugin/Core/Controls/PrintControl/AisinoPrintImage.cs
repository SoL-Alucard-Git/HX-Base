namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml;

    public class AisinoPrintImage : AisinoPrintControl
    {
        protected string _DataBind;
        protected string _imageUrl;
        protected Image newImage;

        public AisinoPrintImage()
        {
            
            this._imageUrl = string.Empty;
            this._DataBind = string.Empty;
        }

        public AisinoPrintImage(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._imageUrl = string.Empty;
            this._DataBind = string.Empty;
            string innerText = xmlElement_0.InnerText;
            string attribute = xmlElement_0.GetAttribute("DataBind");
            this.ImageUrl = innerText;
            this.DataBind = attribute;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Image");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if (base._isPrint && (this.newImage != null))
            {
                RectangleF rect = new RectangleF(base.absoluteLocation, (SizeF) new Size((int) base.Width, (int) base.Height));
                graphics_0.DrawImage(this.newImage, rect);
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            try
            {
                this.newImage = Image.FromFile(this._imageUrl);
            }
            catch
            {
            }
            if ((this.DataBind.Length > 0) && dict.ContainsKey(this.DataBind))
            {
                object obj2 = dict[this.DataBind];
                Image image = obj2 as Bitmap;
                if (image != null)
                {
                    this.newImage = image;
                }
            }
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if (this != null)
            {
                if (this.DataBind != string.Empty)
                {
                    element.SetAttribute("DataBind", this.DataBind);
                }
                if (this.ImageUrl != string.Empty)
                {
                    element.InnerText = this.ImageUrl;
                }
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

        public Image DrawImage
        {
            get
            {
                return this.newImage;
            }
            set
            {
                this.newImage = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return this._imageUrl;
            }
            set
            {
                this._imageUrl = value;
            }
        }
    }
}

