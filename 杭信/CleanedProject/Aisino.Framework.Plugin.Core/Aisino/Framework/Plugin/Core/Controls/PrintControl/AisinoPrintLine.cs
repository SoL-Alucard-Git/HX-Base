namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Xml;

    public class AisinoPrintLine : AisinoPrintControl
    {
        protected DashStyle _lineStyle;
        protected LineType _lineType;

        public AisinoPrintLine()
        {
            
            base.ForeColor = Color.Black;
        }

        public AisinoPrintLine(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            base.ForeColor = Color.Black;
            string attribute = xmlElement_0.GetAttribute("LineType");
            string str2 = xmlElement_0.GetAttribute("LineStyle");
            try
            {
                this.lineType = (LineType) Common.ToInt(attribute);
            }
            catch
            {
                this.lineType = LineType.Horizontal;
            }
            switch (str2)
            {
                case "Dash":
                    this.lineStyle = DashStyle.Dash;
                    return;

                case "Dot":
                    this.lineStyle = DashStyle.Dot;
                    return;

                case "DashDot":
                    this.lineStyle = DashStyle.DashDot;
                    return;

                case "DashDotDot":
                    this.lineStyle = DashStyle.DashDotDot;
                    return;
            }
            this.lineStyle = DashStyle.Solid;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Line");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            if (base._isPrint)
            {
                PointF tf;
                PointF tf3;
                PointF absoluteLocation = base.absoluteLocation;
                switch (this.lineType)
                {
                    case LineType.Vertical:
                        tf = new PointF(absoluteLocation.X + (base.Width / 2f), absoluteLocation.Y);
                        tf3 = new PointF(absoluteLocation.X + (base.Width / 2f), absoluteLocation.Y + base.Height);
                        break;

                    case LineType.LiftInclined:
                        tf = absoluteLocation;
                        tf3 = new PointF(absoluteLocation.X + base.Width, absoluteLocation.Y + base.Height);
                        break;

                    case LineType.RighIinclined:
                        tf = new PointF(absoluteLocation.X + base.Width, absoluteLocation.Y);
                        tf3 = new PointF(absoluteLocation.X, absoluteLocation.Y + base.Height);
                        break;

                    default:
                        tf = new PointF(absoluteLocation.X, absoluteLocation.Y + (base.Height / 2f));
                        tf3 = new PointF(absoluteLocation.X + base.Width, absoluteLocation.Y + (base.Height / 2f));
                        break;
                }
                Pen pen = new Pen(base._foreColor, (float) base._LineWidth);
                if (base._lineColor == Color.Transparent)
                {
                    pen = new Pen(Color.Black, (float) base._LineWidth);
                }
                pen.DashStyle = this.lineStyle;
                graphics_0.DrawLine(pen, tf, tf3);
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if (this != null)
            {
                element.SetAttribute("LineType", Convert.ToInt16(this.lineType).ToString());
                if (this.lineStyle != DashStyle.Solid)
                {
                    element.SetAttribute("LineStyle", this.lineStyle.ToString());
                }
            }
            return element;
        }

        [Description("线条样式")]
        public DashStyle lineStyle
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

        public LineType lineType
        {
            get
            {
                return this._lineType;
            }
            set
            {
                this._lineType = value;
            }
        }

        public enum LineType
        {
            Horizontal,
            Vertical,
            LiftInclined,
            RighIinclined
        }
    }
}

