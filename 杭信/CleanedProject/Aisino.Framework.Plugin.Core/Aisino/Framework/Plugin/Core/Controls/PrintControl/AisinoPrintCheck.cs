namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Xml;

    public class AisinoPrintCheck : AisinoPrintPanel
    {
        protected string _DataBind;
        protected bool _IsCheck;
        protected bool _PrintCheck;

        public AisinoPrintCheck()
        {
            
            this._DataBind = string.Empty;
            base._LineWidth = 1;
        }

        public AisinoPrintCheck(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._DataBind = string.Empty;
            base._LineWidth = 1;
            string attribute = xmlElement_0.GetAttribute("Check");
            string str2 = xmlElement_0.GetAttribute("DataBind");
            this._IsCheck = Common.ToBool(attribute);
            this.DataBind = str2;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Check");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            base.Draw(pointF_0, graphics_0, bool_0);
            if (this._PrintCheck)
            {
                RectangleF ef = new RectangleF(base.absoluteLocation, new SizeF(base.Width, base.Height));
                float num = ef.Width / 4f;
                float num2 = ef.Height / 4f;
                PointF[] points = new PointF[] { new PointF(ef.X + num, ef.Y + (2f * num2)), new PointF(ef.X + (2f * num), ef.Y + (3f * num2)), new PointF(ef.X + (3f * num), ef.Y + num2) };
                graphics_0.DrawLines(new Pen(Color.Black), points);
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            this._PrintCheck = this._IsCheck;
            if ((this._DataBind.Length > 0) && dict.ContainsKey(this.DataBind))
            {
                Console.WriteLine("a:" + dict[this._DataBind]);
                Console.WriteLine("b:" + dict[this._DataBind].ToString());
                Console.WriteLine("c:" + Common.ToBool(dict[this._DataBind].ToString()));
                this._PrintCheck = Common.ToBool(dict[this._DataBind].ToString());
            }
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if (this._IsCheck)
            {
                element.SetAttribute("Check", this._IsCheck.ToString());
            }
            if (!string.IsNullOrEmpty(this._DataBind))
            {
                element.SetAttribute("DataBind", this._DataBind);
            }
            return element;
        }

        [Description("设置或获取控件的绑定字段"), Category("值")]
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

        [Category("值"), Description("设置或获取控件是否被选择")]
        public bool IsCheck
        {
            get
            {
                return this._IsCheck;
            }
            set
            {
                this._IsCheck = value;
            }
        }

        [Category("值"), Description("或获取控件的打印的值")]
        public bool PrintCheck
        {
            get
            {
                return this._PrintCheck;
            }
        }
    }
}

