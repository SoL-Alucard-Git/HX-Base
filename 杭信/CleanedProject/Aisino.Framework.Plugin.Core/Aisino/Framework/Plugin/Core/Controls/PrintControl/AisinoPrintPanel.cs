namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Xml;

    public class AisinoPrintPanel : AisinoPrintControl
    {
        protected DashStyle _lineStyle;
        private Dictionary<string, object> dictionary_0;

        public AisinoPrintPanel()
        {
            
        }

        public AisinoPrintPanel(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            switch (xmlElement_0.GetAttribute("LineType"))
            {
                case "Dash":
                    this.LineStyle = DashStyle.Dash;
                    return;

                case "Dot":
                    this.LineStyle = DashStyle.Dot;
                    return;

                case "DashDot":
                    this.LineStyle = DashStyle.DashDot;
                    return;

                case "DashDotDot":
                    this.LineStyle = DashStyle.DashDotDot;
                    return;
            }
            this.LineStyle = DashStyle.Solid;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Panel");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if (base._isPrint && (base._LineWidth > 0))
            {
                Pen pen = new Pen(base._foreColor, (float) base._LineWidth) {
                    DashStyle = this._lineStyle
                };
                RectangleF rect = new RectangleF(base.absoluteLocation, new SizeF(base.Width, base.Height));
                graphics_0.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                if (base.BackColor != Color.Transparent)
                {
                    graphics_0.FillRectangle(new SolidBrush(base.BackColor), rect);
                }
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            this.dictionary_0 = dict;
        }

        public override void Print(PointF pointF_0, Dictionary<string, object> dict, Graphics graphics_0, bool bool_0)
        {
            base.Print(pointF_0, dict, graphics_0, bool_0);
            foreach (AisinoPrintControl control in base.Nodes)
            {
                control.Print(base.absoluteLocation, dict, graphics_0, bool_0);
            }
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if (this != null)
            {
                switch (this.LineStyle)
                {
                    case DashStyle.Dash:
                        element.SetAttribute("LineType", "Dash");
                        return element;

                    case DashStyle.Dot:
                        element.SetAttribute("LineType", "Dot");
                        return element;

                    case DashStyle.DashDot:
                        element.SetAttribute("LineType", "DashDot");
                        return element;

                    case DashStyle.DashDotDot:
                        element.SetAttribute("LineType", "DashDotDot");
                        return element;
                }
                element.SetAttribute("LineType", "Solid");
            }
            return element;
        }

        [Description("线条样式")]
        public DashStyle LineStyle
        {
            get
            {
                return this._lineStyle;
            }
            set
            {
                this._lineStyle = value;
            }
        }
    }
}

