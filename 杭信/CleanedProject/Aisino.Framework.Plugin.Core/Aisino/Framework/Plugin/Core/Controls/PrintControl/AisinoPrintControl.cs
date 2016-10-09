namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Xml;

    public abstract class AisinoPrintControl
    {
        protected Color _backColor;
        protected Color _foreColor;
        protected float _height;
        protected string _ID;
        protected bool _isPrint;
        protected Color _lineColor;
        protected int _LineWidth;
        protected PointF _Location;
        protected List<AisinoPrintControl> _Nodes;
        protected AisinoPrintControl _parent;
        protected float _width;
        protected PointF absoluteLocation;
        public Graphics gDpi;

        public event ChangeNode AddChangeNode;

        public event ChangeNode DeleteChangeNode;

        public AisinoPrintControl()
        {
            
            this._ID = string.Empty;
            this._width = 100f;
            this._height = 25f;
            this._Location = new PointF(0f, 0f);
            this._backColor = Color.Transparent;
            this._foreColor = Color.Black;
            this._lineColor = Color.Transparent;
            this._isPrint = true;
            this._Nodes = new List<AisinoPrintControl>();
            this.absoluteLocation = new PointF(0f, 0f);
            this.gDpi = Graphics.FromImage(new Bitmap(10, 10));
        }

        public AisinoPrintControl(XmlElement xmlElement_0)
        {
            
            this._ID = string.Empty;
            this._width = 100f;
            this._height = 25f;
            this._Location = new PointF(0f, 0f);
            this._backColor = Color.Transparent;
            this._foreColor = Color.Black;
            this._lineColor = Color.Transparent;
            this._isPrint = true;
            this._Nodes = new List<AisinoPrintControl>();
            this.absoluteLocation = new PointF(0f, 0f);
            this.gDpi = Graphics.FromImage(new Bitmap(10, 10));
            string attribute = xmlElement_0.GetAttribute("Id");
            string str2 = xmlElement_0.GetAttribute("Height");
            string str3 = xmlElement_0.GetAttribute("Width");
            string str4 = xmlElement_0.GetAttribute("BackColor");
            string str5 = xmlElement_0.GetAttribute("ForeColor");
            string str6 = xmlElement_0.GetAttribute("TaoDa");
            string str7 = xmlElement_0.GetAttribute("LineWidth");
            string str8 = xmlElement_0.GetAttribute("LineColor");
            string str9 = xmlElement_0.GetAttribute("Location");
            if (attribute != string.Empty)
            {
                this._ID = attribute;
            }
            if (str2 != string.Empty)
            {
                this.Height = Common.ToFloat(str2);
            }
            else if (this._parent != null)
            {
                this.Height = this._parent.Height;
            }
            if (str3 != string.Empty)
            {
                this.Width = Common.ToFloat(str3);
            }
            else if (this._parent != null)
            {
                this.Width = this._parent.Width;
            }
            if (str4 != string.Empty)
            {
                this.BackColor = Common.ToColor(str4);
            }
            if (str5 != string.Empty)
            {
                this.ForeColor = Common.ToColor(str5);
            }
            if (str8 != string.Empty)
            {
                this.LineColor = Common.ToColor(str8);
            }
            if (str6 != string.Empty)
            {
                this.IsPrint = Common.ToBool(str6);
            }
            this.LineWidth = 0;
            if (str7 != string.Empty)
            {
                this.LineWidth = Common.ToInt(str7);
            }
            if (str9 != string.Empty)
            {
                this.Location = Common.ToPointF(str9);
            }
            XmlNodeList childNodes = xmlElement_0.ChildNodes;
            if (childNodes.Count > 0)
            {
                foreach (XmlNode node in childNodes)
                {
                    AisinoPrintControl control;
                    XmlElement element = node as XmlElement;
                    switch (node.Name)
                    {
                        case "Label":
                            control = new AisinoPrintLabel(element);
                            break;

                        case "Line":
                            control = new AisinoPrintLine(element);
                            break;

                        case "Panel":
                            control = new AisinoPrintPanel(element);
                            break;

                        case "Repeater":
                            control = new AisinoPrintRepeater(element);
                            break;

                        case "Image":
                            control = new AisinoPrintImage(element);
                            break;

                        case "Check":
                            control = new AisinoPrintCheck(element);
                            break;

                        case "Grid":
                            control = new AisinoPrintGrid(element);
                            break;

                        case "HXM":
                            control = new AisinoPrintAsHX(element);
                            break;

                        case "QRM":
                            control = new AisinoPrintAsQR(element);
                            break;

                        case "PDF417":
                            control = new GControl0(element);
                            break;

                        default:
                            control = null;
                            break;
                    }
                    if (control != null)
                    {
                        this.AddNode(control);
                    }
                }
            }
        }

        public void AddNode(AisinoPrintControl aisinoPrintControl_0)
        {
            if (aisinoPrintControl_0 != null)
            {
                if (aisinoPrintControl_0.Parent != null)
                {
                    aisinoPrintControl_0.Parent.Remove(aisinoPrintControl_0);
                }
                this._Nodes.Add(aisinoPrintControl_0);
                aisinoPrintControl_0._parent = this;
                if (this.AddChangeNode != null)
                {
                    this.AddChangeNode(this, aisinoPrintControl_0);
                }
            }
        }

        public abstract XmlElement CreateXmlElement(XmlDocument xmlDocument_0);
        public float DpiToThousandths(float float_0, float float_1)
        {
            return ((float_0 * 100f) / float_1);
        }

        protected abstract void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0);
        protected abstract void Load(Dictionary<string, object> dict);
        public virtual void Print(PointF pointF_0, Dictionary<string, object> dict, Graphics graphics_0, bool bool_0)
        {
            this.absoluteLocation = new PointF(this.Location.X + pointF_0.X, this.Location.Y + pointF_0.Y);
            this.Load(dict);
            this.Draw(pointF_0, graphics_0, bool_0);
        }

        public void Remove(AisinoPrintControl aisinoPrintControl_0)
        {
            if (aisinoPrintControl_0 != null)
            {
                this._Nodes.Remove(aisinoPrintControl_0);
                if (this.DeleteChangeNode != null)
                {
                    this.DeleteChangeNode(this, aisinoPrintControl_0);
                }
            }
        }

        public float ThousandthsToDpi(float float_0, float float_1)
        {
            return ((float_0 * float_1) * 0.01f);
        }

        public virtual XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = this.CreateXmlElement(xmlDocument_0);
            if (!string.IsNullOrEmpty(this._ID))
            {
                element.SetAttribute("Id", this._ID);
            }
            element.SetAttribute("Height", this.Height.ToString());
            element.SetAttribute("Width", this.Width.ToString());
            if (this.BackColor != Color.Transparent)
            {
                element.SetAttribute("BackColor", this.BackColor.Name);
            }
            if (this.ForeColor != Color.Black)
            {
                element.SetAttribute("ForeColor", this.ForeColor.Name);
            }
            if (this.LineColor != Color.Transparent)
            {
                element.SetAttribute("LineColor", this.LineColor.Name);
            }
            if (this.LineWidth != 0)
            {
                element.SetAttribute("LineWidth", this.LineWidth.ToString());
            }
            if (!this.Location.Equals(new Point(0, 0)))
            {
                element.SetAttribute("Location", this.Location.X + "," + this.Location.Y);
            }
            foreach (AisinoPrintControl control in this._Nodes)
            {
                XmlElement newChild = control.ToXmlNode(xmlDocument_0);
                element.AppendChild(newChild);
            }
            return element;
        }

        [Description("绝对定位起点 ，在执行打印后得到 ")]
        public PointF AbsoluteLocation
        {
            get
            {
                return this.absoluteLocation;
            }
        }

        [Description("背景色")]
        public Color BackColor
        {
            get
            {
                return this._backColor;
            }
            set
            {
                this._backColor = value;
            }
        }

        [Description("前景色")]
        public Color ForeColor
        {
            get
            {
                return this._foreColor;
            }
            set
            {
                this._foreColor = value;
            }
        }

        [Description("设置或获取控件的高度")]
        public float Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        [Description("Dpi高度"), Category("Dpi度量单位(设计使用)")]
        public float HeightDpi
        {
            get
            {
                return this.ThousandthsToDpi(this.Height, this.gDpi.DpiY);
            }
            set
            {
                this.Height = this.DpiToThousandths(value, this.gDpi.DpiY);
            }
        }

        [Description("设置或获取控件的宽度")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [Description("指定是否打印默认为打印")]
        public bool IsPrint
        {
            get
            {
                return this._isPrint;
            }
            set
            {
                this._isPrint = value;
            }
        }

        [Description("边框颜色")]
        public Color LineColor
        {
            get
            {
                return this._lineColor;
            }
            set
            {
                this._lineColor = value;
            }
        }

        [Description("线宽")]
        public int LineWidth
        {
            get
            {
                return this._LineWidth;
            }
            set
            {
                this._LineWidth = value;
            }
        }

        [TypeConverter(typeof(PointFConverter)), Description("控件左上角相对于容器左上角的坐标")]
        public PointF Location
        {
            get
            {
                return this._Location;
            }
            set
            {
                this._Location = value;
            }
        }

        [TypeConverter(typeof(PointFConverter)), Description("Dpi定位"), Category("Dpi度量单位(设计使用)")]
        public PointF LocationDpi
        {
            get
            {
                return new PointF(this.ThousandthsToDpi(this.Location.X, this.gDpi.DpiX), this.ThousandthsToDpi(this.Location.Y, this.gDpi.DpiY));
            }
            set
            {
                this.Location = new PointF(this.DpiToThousandths(value.X, this.gDpi.DpiX), this.DpiToThousandths(value.Y, this.gDpi.DpiY));
            }
        }

        [Description("子节点")]
        public List<AisinoPrintControl> Nodes
        {
            get
            {
                return this._Nodes;
            }
        }

        [Description("当前控件的父控件")]
        public AisinoPrintControl Parent
        {
            get
            {
                return this._parent;
            }
        }

        [TypeConverter(typeof(Aisino.Framework.Plugin.Core.Controls.PrintControl.SizeFConverter)), Description("控件的高度宽度信息")]
        public SizeF Size
        {
            get
            {
                return new SizeF(this._width, this._height);
            }
            set
            {
                this._width = value.Width;
                this._height = value.Height;
            }
        }

        [Category("Dpi度量单位(设计使用)"), Description("Dpi区域大小")]
        public SizeF SizeDpi
        {
            get
            {
                return new SizeF(this.WidthDpi, this.HeightDpi);
            }
            set
            {
                this.WidthDpi = value.Width;
                this.HeightDpi = value.Height;
            }
        }

        [Description("设置或获取控件的宽度")]
        public float Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        [Category("Dpi度量单位(设计使用)"), Description("Dpi宽度")]
        public float WidthDpi
        {
            get
            {
                return this.ThousandthsToDpi(this.Width, this.gDpi.DpiX);
            }
            set
            {
                this.Width = this.DpiToThousandths(value, this.gDpi.DpiX);
            }
        }

        public delegate void ChangeNode(object sender, AisinoPrintControl control);
    }
}

