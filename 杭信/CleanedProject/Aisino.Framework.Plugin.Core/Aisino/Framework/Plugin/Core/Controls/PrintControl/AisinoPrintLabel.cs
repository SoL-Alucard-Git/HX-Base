namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class AisinoPrintLabel : AisinoPrintPanel
    {
        protected float _backgauge;
        protected string _DataBind;
        protected Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType _dataType;
        protected Font _font;
        protected float _FontSpacing;
        protected string _Format;
        protected HorizontalType _HorizontalAlign;
        protected int _Length;
        protected int _LineLength;
        protected float _LineSpacing;
        protected string _PrintText;
        protected string _text;
        protected VerticalType _VerticalAlign;

        public AisinoPrintLabel()
        {
            
            this._text = string.Empty;
            this._VerticalAlign = VerticalType.Center;
            this._font = new Font("宋体", 12f);
            this._DataBind = string.Empty;
            this._Format = string.Empty;
            this._PrintText = string.Empty;
            this._backgauge = 5.2f;
        }

        public AisinoPrintLabel(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            string str14;
            
            this._text = string.Empty;
            this._VerticalAlign = VerticalType.Center;
            this._font = new Font("宋体", 12f);
            this._DataBind = string.Empty;
            this._Format = string.Empty;
            this._PrintText = string.Empty;
            this._backgauge = 5.2f;
            string innerText = xmlElement_0.InnerText;
            string attribute = xmlElement_0.GetAttribute("Font");
            string str3 = xmlElement_0.GetAttribute("DataBind");
            string str4 = xmlElement_0.GetAttribute("Horizontal");
            string str5 = xmlElement_0.GetAttribute("Vertical");
            string str6 = xmlElement_0.GetAttribute("FontSpacing");
            string str7 = xmlElement_0.GetAttribute("LineSpacing");
            string str8 = xmlElement_0.GetAttribute("Format");
            string str9 = xmlElement_0.GetAttribute("DataType");
            string str10 = xmlElement_0.GetAttribute("Lenth");
            string str11 = xmlElement_0.GetAttribute("Backgauge");
            string str12 = xmlElement_0.GetAttribute("LineLength");
            base._ID = xmlElement_0.GetAttribute("Id");
            string str13 = str4;
            if (str13 != null)
            {
                if (!(str13 == "Right"))
                {
                    if (!(str13 == "Center"))
                    {
                        goto Label_0137;
                    }
                    this.HorizontalAlign = HorizontalType.Center;
                }
                else
                {
                    this.HorizontalAlign = HorizontalType.Right;
                }
                goto Label_013E;
            }
        Label_0137:
            this.HorizontalAlign = HorizontalType.Left;
        Label_013E:
            if ((str14 = str5) != null)
            {
                if (!(str14 == "Botton"))
                {
                    if (!(str14 == "Center"))
                    {
                        goto Label_0173;
                    }
                    this.VerticalAlign = VerticalType.Center;
                }
                else
                {
                    this.VerticalAlign = VerticalType.Botton;
                }
                goto Label_017A;
            }
        Label_0173:
            this.VerticalAlign = VerticalType.Top;
        Label_017A:
            this.Text = innerText;
            this.font = Common.ToFont(attribute);
            this.DataBind = str3;
            if (str6 == string.Empty)
            {
                this._FontSpacing = 0f;
            }
            else
            {
                this._FontSpacing = Common.ToFloat(str6);
            }
            if (str7 == string.Empty)
            {
                this._LineSpacing = 0f;
            }
            else
            {
                this._LineSpacing = Common.ToFloat(str7);
            }
            this._Format = str8;
            switch (str9)
            {
                case "Int":
                case "I":
                case "i":
                    this._dataType = Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.Int;
                    break;

                case "Double":
                case "D":
                case "d":
                    this._dataType = Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.Double;
                    break;

                case "DateTime":
                case "DT":
                case "dt":
                    this._dataType = Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.DateTime;
                    break;

                case null:
                    break;

                default:
                    this._dataType = Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.String;
                    break;
            }
            if (!string.IsNullOrEmpty(str10))
            {
                this._Length = Common.ToInt(str10);
            }
            if (!string.IsNullOrEmpty(str11))
            {
                this._backgauge = Common.ToFloat(str11);
            }
            if (!string.IsNullOrEmpty(str12))
            {
                this._LineLength = Common.ToInt(str12);
            }
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Label");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            base.Draw(pointF_0, graphics_0, bool_0);
            if (base._isPrint)
            {
                RectangleF ef = new RectangleF(new PointF(this.absoluteLocation.X + this._backgauge, this.absoluteLocation.Y), new SizeF(base.Width - (2f * this._backgauge), base.Height));
                Font font = this._font;
                FontPrint print = new FontPrint(this._PrintText, this._Format, this._dataType);
                List<FontPrint.LineZH> list = print.ZheHang(ef.Width, this._FontSpacing, this._LineSpacing, font, graphics_0, this.LineLength);
                float num = (list.Count * (font.Height + this._LineSpacing)) - this._LineSpacing;
                while (num > ef.Height)
                {
                    font = new Font(font.FontFamily, font.Size - 1f);
                    list = print.ZheHang(ef.Width, this._FontSpacing, this._LineSpacing, font, graphics_0, this.LineLength);
                    num = (list.Count * (font.Height + this._LineSpacing)) - this._LineSpacing;
                    if (font.Size <= 5f)
                    {
                        break;
                    }
                }
                PointF empty = PointF.Empty;
                switch (this._VerticalAlign)
                {
                    case VerticalType.Top:
                        empty.Y = 0f;
                        break;

                    case VerticalType.Botton:
                        if (num <= ef.Height)
                        {
                            empty.Y = ef.Height - num;
                        }
                        break;

                    case VerticalType.Center:
                        if (num <= ef.Height)
                        {
                            empty.Y = (ef.Height - num) / 2f;
                        }
                        break;
                }
                foreach (FontPrint.LineZH ezh in list)
                {
                    switch (this._HorizontalAlign)
                    {
                        case HorizontalType.Left:
                            empty.X = 0f;
                            break;

                        case HorizontalType.Right:
                            if (ezh.Width <= ef.Width)
                            {
                                empty.X = ef.Width - ezh.Width;
                            }
                            break;

                        case HorizontalType.Center:
                            if (ezh.Width <= ef.Width)
                            {
                                empty.X = (ef.Width - ezh.Width) / 2f;
                            }
                            break;
                    }
                    print.DrawString(ezh, this._FontSpacing, font, new SolidBrush(base._foreColor), graphics_0, new PointF(empty.X + ef.X, empty.Y + ef.Y), bool_0, base._ID);
                    empty.Y = (empty.Y + font.Height) + this._LineSpacing;
                    if (empty.Y < 0f)
                    {
                        empty.Y = 1f;
                    }
                    if ((empty.Y - this._LineSpacing) > ef.Height)
                    {
                        break;
                    }
                }
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            this._PrintText = this.Text;
            if ((this.DataBind.Length > 0) && dict.ContainsKey(this.DataBind))
            {
                this._PrintText = dict[this.DataBind].ToString();
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
                else
                {
                    element.InnerText = this.Text.ToString();
                }
                if (this.FontSpacing > 0f)
                {
                    element.SetAttribute("FontSpacing", this.FontSpacing.ToString());
                }
                if (this.LineSpacing > 0f)
                {
                    element.SetAttribute("LineSpacing", this.LineSpacing.ToString());
                }
                element.SetAttribute("Font", string.Concat(new object[] { this.font.FontFamily.Name, ",", this.font.Size, "," }));
                switch (this.HorizontalAlign)
                {
                    case HorizontalType.Right:
                        element.SetAttribute("Horizontal", "Right");
                        break;

                    case HorizontalType.Center:
                        element.SetAttribute("Horizontal", "Center");
                        break;
                }
                switch (this.VerticalAlign)
                {
                    case VerticalType.Botton:
                        element.SetAttribute("Vertical", "Botton");
                        break;

                    case VerticalType.Center:
                        element.SetAttribute("Vertical", "Center");
                        break;
                }
                if (!string.IsNullOrEmpty(this._Format))
                {
                    element.SetAttribute("Format", this._Format);
                }
                if (this._dataType != Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.String)
                {
                    element.SetAttribute("DataType", this._dataType.ToString());
                }
                if (this._Length > 0)
                {
                    element.SetAttribute("Length", this._Length.ToString());
                }
                element.SetAttribute("Backgauge", this._backgauge.ToString());
                if (this._LineLength > 0)
                {
                    element.SetAttribute("LineLength", this._LineLength.ToString());
                }
            }
            return element;
        }

        public string[] ZheHang(string string_0)
        {
            Graphics graphics = Graphics.FromImage(new Bitmap(100, 100));
            List<FontPrint.LineZH> list = new FontPrint(string_0, string.Empty, Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType.String).ZheHang(base.Width - (2f * this._backgauge), this.FontSpacing, this.LineSpacing, this.font, graphics, this._LineLength);
            graphics.Dispose();
            List<string> list2 = new List<string>();
            foreach (FontPrint.LineZH ezh in list)
            {
                list2.Add(ezh.ToString());
            }
            return list2.ToArray();
        }

        [Category("外观"), Description("文本框的边距")]
        public float Backgauge
        {
            get
            {
                return this._backgauge;
            }
            set
            {
                this._backgauge = value;
            }
        }

        [Description("Dpi文本框的边距"), Category("Dpi度量单位(设计使用)")]
        public float BackgaugeDpi
        {
            get
            {
                return base.ThousandthsToDpi(this.Backgauge, base.gDpi.DpiX);
            }
            set
            {
                this.Backgauge = base.DpiToThousandths(value, base.gDpi.DpiY);
            }
        }

        [Category("外观"), Description("设置或获取控件的绑定字段")]
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

        [Description("设置或获取控件打印的数据的数据类型"), Category("外观")]
        public Aisino.Framework.Plugin.Core.Controls.PrintControl.DataType DataType
        {
            get
            {
                return this._dataType;
            }
            set
            {
                this._dataType = value;
            }
        }

        public Font font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        public float FontSpacing
        {
            get
            {
                return this._FontSpacing;
            }
            set
            {
                this._FontSpacing = value;
            }
        }

        [Description("Dpi字间距"), Category("Dpi度量单位(设计使用)")]
        public float FontSpacingDpi
        {
            get
            {
                return base.ThousandthsToDpi(this.FontSpacing, base.gDpi.DpiX);
            }
            set
            {
                this.FontSpacing = base.DpiToThousandths(value, base.gDpi.DpiX);
            }
        }

        [Description("设置或获取控件打印的格式化信息"), Category("外观")]
        public string Format
        {
            get
            {
                return this._Format;
            }
            set
            {
                this._Format = value;
            }
        }

        [Description("设置或获取控件的水平对其方式"), Category("外观")]
        public HorizontalType HorizontalAlign
        {
            get
            {
                return this._HorizontalAlign;
            }
            set
            {
                this._HorizontalAlign = value;
            }
        }

        [Description("设置或获取控件打印的数据的最大长度"), Category("外观")]
        public int Length
        {
            get
            {
                return this._Length;
            }
            set
            {
                this._Length = value;
            }
        }

        [Category("外观"), Description("每行打印的字符数")]
        public int LineLength
        {
            get
            {
                return this._LineLength;
            }
            set
            {
                this._LineLength = value;
            }
        }

        public float LineSpacing
        {
            get
            {
                return this._LineSpacing;
            }
            set
            {
                this._LineSpacing = value;
            }
        }

        [Description("Dpi行间距"), Category("Dpi度量单位(设计使用)")]
        public float LineSpacingDpi
        {
            get
            {
                return base.ThousandthsToDpi(this.LineSpacing, base.gDpi.DpiY);
            }
            set
            {
                this.LineSpacing = base.DpiToThousandths(value, base.gDpi.DpiY);
            }
        }

        [Description("设置或获取控件的垂直对其方式")]
        public string PrintText
        {
            get
            {
                return this._PrintText;
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        [Description("设置或获取控件的垂直对其方式")]
        public VerticalType VerticalAlign
        {
            get
            {
                return this._VerticalAlign;
            }
            set
            {
                this._VerticalAlign = value;
            }
        }

        public enum CharPrintEnum
        {
            None,
            Char,
            Number,
            Chinese,
            Enter
        }

        public class CharPrintEvent
        {
            private AisinoPrintLabel.CharPrintEnum charPrintEnum_0;
            private string string_0;

            public CharPrintEvent(string string_1)
            {
                
                this.string_0 = string.Empty;
                this.string_0 = string_1;
                this.charPrintEnum_0 = AisinoPrintLabel.CharPrintEnum.None;
            }

            public CharPrintEvent(string string_1, AisinoPrintLabel.CharPrintEnum charPrintEnum_1)
            {
                
                this.string_0 = string.Empty;
                this.string_0 = string_1;
                this.charPrintEnum_0 = charPrintEnum_1;
            }

            public AisinoPrintLabel.CharPrintEnum CharStyle
            {
                get
                {
                    return this.charPrintEnum_0;
                }
                set
                {
                    this.charPrintEnum_0 = value;
                }
            }

            public string e
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

        public class FontPrint
        {
            private List<AisinoPrintLabel.CharPrintEvent> list_0;
            private string string_0;
            private string string_1;

            public FontPrint(string string_2, string string_3, DataType dataType_0)
            {
                
                this.string_0 = string_2;
                this.string_1 = string_3;
                this.list_0 = this.method_0(dataType_0);
            }

            public void DrawString(LineZH lineZH_0, float float_0, Font font_0, Brush brush_0, Graphics graphics_0, PointF pointF_0, bool bool_0, string string_2)
            {
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                float num = 0f;
                foreach (AisinoPrintLabel.CharPrintEvent event2 in lineZH_0.e)
                {
                    float num2 = this.method_2(event2, font_0, brush_0, pointF_0.X + num, pointF_0.Y, genericTypographic, graphics_0, bool_0, string_2);
                    num = (num + num2) + float_0;
                }
            }

            private List<AisinoPrintLabel.CharPrintEvent> method_0(DataType dataType_0)
            {
                string str = this.string_0;
                if (this.string_1 != string.Empty)
                {
                    object obj2;
                    if (!Regex.IsMatch(this.string_1, @"\{[\d\D]*\}"))
                    {
                        this.string_1 = "{0:" + this.string_1 + "}";
                    }
                    else
                    {
                        this.string_1 = new Regex(@"\{[\d\D]*\}").Replace(this.string_1, new MatchEvaluator(this.method_1));
                    }
                    switch (dataType_0)
                    {
                        case DataType.Int:
                            try
                            {
                                obj2 = Convert.ToInt32(str);
                            }
                            catch
                            {
                                obj2 = str;
                            }
                            break;

                        case DataType.Double:
                            try
                            {
                                obj2 = Convert.ToDouble(str);
                            }
                            catch
                            {
                                obj2 = str;
                            }
                            break;

                        case DataType.DateTime:
                            try
                            {
                                obj2 = Convert.ToDateTime(str);
                            }
                            catch
                            {
                                obj2 = str;
                            }
                            break;

                        default:
                            obj2 = str;
                            break;
                    }
                    str = string.Format(new AisinoFormat(), this.string_1, new object[] { obj2 });
                }
                List<AisinoPrintLabel.CharPrintEvent> list = new List<AisinoPrintLabel.CharPrintEvent>();
                for (int i = 0; i < str.Length; i++)
                {
                    char ch;
                    if (((str[i] == '\r') && ((i + 1) < str.Length)) && (str[i + 1] == '\n'))
                    {
                        list.Add(new AisinoPrintLabel.CharPrintEvent("\r\n", AisinoPrintLabel.CharPrintEnum.Enter));
                        i++;
                        continue;
                    }
                    if (str[i] != '[')
                    {
                        goto Label_024F;
                    }
                    int num2 = i;
                    int num3 = i + 1;
                    while (num3 < str.Length)
                    {
                        if (str[num3] == '[')
                        {
                            break;
                        }
                        if (str[num3] == ']')
                        {
                            goto Label_0180;
                        }
                        num3++;
                    }
                    goto Label_0184;
                Label_0180:
                    num2 = num3;
                Label_0184:
                    if (num2 > i)
                    {
                        string[] strArray = str.Substring(i, (num2 - i) + 1).Trim(new char[] { '[', ']' }).Split(new char[] { ':' });
                        if (strArray.Length > 1)
                        {
                            if (strArray[0] == "0")
                            {
                                list.Add(new AisinoPrintLabel.CharPrintEvent(strArray[1], AisinoPrintLabel.CharPrintEnum.Number));
                                i = num2;
                                continue;
                            }
                            if (strArray[0] == "1")
                            {
                                list.Add(new AisinoPrintLabel.CharPrintEvent(strArray[1], AisinoPrintLabel.CharPrintEnum.Char));
                                i = num2;
                                continue;
                            }
                            if (strArray[0] == "2")
                            {
                                list.Add(new AisinoPrintLabel.CharPrintEvent(strArray[1], AisinoPrintLabel.CharPrintEnum.Chinese));
                                i = num2;
                                continue;
                            }
                        }
                    }
                Label_024F:
                    ch = str[i];
                    list.Add(new AisinoPrintLabel.CharPrintEvent(ch.ToString()));
                }
                return list;
            }

            private string method_1(Match match_0)
            {
                string str = match_0.ToString();
                if (((str.Length > 0) && (str[0] == '{')) && (str[str.Length - 1] == '}'))
                {
                    str = str.Insert(1, "0:");
                }
                return str;
            }

            private float method_2(AisinoPrintLabel.CharPrintEvent charPrintEvent_0, Font font_0, Brush brush_0, float float_0, float float_1, StringFormat stringFormat_0, Graphics graphics_0, bool bool_0, string string_2)
            {
                SizeF ef;
                switch (charPrintEvent_0.CharStyle)
                {
                    case AisinoPrintLabel.CharPrintEnum.Char:
                        ef = graphics_0.MeasureString("a", font_0, (PointF) Point.Empty, stringFormat_0);
                        goto Label_059C;

                    case AisinoPrintLabel.CharPrintEnum.Number:
                        ef = graphics_0.MeasureString("0", font_0, (PointF) Point.Empty, stringFormat_0);
                        goto Label_059C;

                    case AisinoPrintLabel.CharPrintEnum.Chinese:
                        ef = graphics_0.MeasureString("国", font_0, (PointF) Point.Empty, stringFormat_0);
                        if (charPrintEvent_0.e.ToLower() == "ox")
                        {
                            float width = (ef.Width > ef.Height) ? ef.Height : ef.Width;
                            float num2 = width / 2f;
                            PointF tf = new PointF(float_0 + (ef.Width / 2f), (float_1 + (ef.Height / 2f)) - 2f);
                            graphics_0.DrawEllipse(new Pen(brush_0), tf.X - num2, tf.Y - num2, width, width);
                            graphics_0.DrawLine(new Pen(brush_0), new PointF(tf.X - (0.707f * num2), tf.Y - (0.707f * num2)), new PointF(tf.X + (0.707f * num2), tf.Y + (0.707f * num2)));
                            graphics_0.DrawLine(new Pen(brush_0), new PointF(tf.X + (0.707f * num2), tf.Y - (0.707f * num2)), new PointF(tf.X - (0.707f * num2), tf.Y + (0.707f * num2)));
                        }
                        goto Label_059C;

                    default:
                    {
                        string e = charPrintEvent_0.e;
                        if (e != null)
                        {
                            if (e == " ")
                            {
                                ef = graphics_0.MeasureString("a", font_0, (PointF) Point.Empty, stringFormat_0);
                                graphics_0.DrawString(charPrintEvent_0.e, font_0, brush_0, float_0, float_1, stringFormat_0);
                            }
                            else if (e == "*")
                            {
                                Font font = null;
                                if (!string.IsNullOrEmpty(string_2) && "ZPMW".Equals(string_2.ToUpper()))
                                {
                                    font = new Font("OCR A Extended", font_0.Size, font_0.Style);
                                }
                                else
                                {
                                    font = font_0;
                                }
                                ef = graphics_0.MeasureString("0", font, (PointF) Point.Empty, stringFormat_0);
                                graphics_0.DrawString(charPrintEvent_0.e, font, brush_0, float_0, float_1, stringFormat_0);
                            }
                            else
                            {
                                if (!(e == "￥"))
                                {
                                    break;
                                }
                                if (bool_0)
                                {
                                    ef = graphics_0.MeasureString("￥", font_0, (PointF) Point.Empty, stringFormat_0);
                                    float height = font_0.Height;
                                    float num8 = (height * 4.6f) / 8f;
                                    int num7 = (int) (((double) height) / 8.0);
                                    if (num7 < 1)
                                    {
                                        num7 = 1;
                                    }
                                    float num3 = graphics_0.MeasureString("0", font_0, (PointF) Point.Empty, stringFormat_0).Width;
                                    float num4 = (int) (((double) num3) / 2.0);
                                    float x = float_0 + (ef.Width / 2f);
                                    float num6 = float_1 + (ef.Height / 2f);
                                    Pen pen = new Pen(brush_0, 0.6f);
                                    graphics_0.DrawLine(pen, new PointF(x - num4, num6 - num7), new PointF(x + num4, num6 - num7));
                                    graphics_0.DrawLine(pen, new PointF(x - num4, (num6 + (num8 / 4f)) - num7), new PointF(x + num4, (num6 + (num8 / 4f)) - num7));
                                    graphics_0.DrawLine(pen, new PointF(x - (num3 / 4f), num6 + (num8 / 2f)), new PointF(x + (num3 / 4f), num6 + (num8 / 2f)));
                                    graphics_0.DrawLine(pen, new PointF(x, num6 - num7), new PointF(x, num6 + (num8 / 2f)));
                                    num4 = (3f * num3) / 8f;
                                    graphics_0.DrawLine(pen, new PointF(x, num6 - num7), new PointF(x - num4, num6 - (num8 / 2f)));
                                    graphics_0.DrawLine(pen, new PointF(x, num6 - num7), new PointF(x + num4, num6 - (num8 / 2f)));
                                    graphics_0.DrawLine(pen, new PointF((x - num4) - ((1.5f * num3) / 8f), num6 - (num8 / 2f)), new PointF((x - num4) + ((1.5f * num3) / 8f), num6 - (num8 / 2f)));
                                    graphics_0.DrawLine(pen, new PointF((x + num4) - ((1.5f * num3) / 8f), num6 - (num8 / 2f)), new PointF((x + num4) + ((1.5f * num3) / 8f), num6 - (num8 / 2f)));
                                }
                                else
                                {
                                    ef = graphics_0.MeasureString(charPrintEvent_0.e, font_0, PointF.Empty, stringFormat_0);
                                    ef = new SizeF(ef.Width + 1f, ef.Height);
                                    graphics_0.DrawString(charPrintEvent_0.e, font_0, brush_0, float_0, float_1, stringFormat_0);
                                }
                            }
                            goto Label_059C;
                        }
                        break;
                    }
                }
                ef = graphics_0.MeasureString(charPrintEvent_0.e, font_0, PointF.Empty, stringFormat_0);
                graphics_0.DrawString(charPrintEvent_0.e, font_0, brush_0, float_0, float_1, stringFormat_0);
            Label_059C:
                return ef.Width;
            }

            public List<LineZH> ZheHang(float float_0, float float_1, float float_2, Font font_0, Graphics graphics_0, int int_0)
            {
                Graphics graphics = Graphics.FromImage(new Bitmap(100, 100));
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                List<LineZH> list = new List<LineZH>();
                LineZH item = new LineZH();
                float num = 0f;
                float num2 = 0f;
                if (int_0 > 0)
                {
                    int num3 = 0;
                    foreach (AisinoPrintLabel.CharPrintEvent event2 in this.list_0)
                    {
                        item.e.Add(event2);
                        item.Width += (int) graphics.MeasureString(event2.e, font_0, (PointF) Point.Empty, genericTypographic).Width;
                        num3++;
                        if (num3 >= int_0)
                        {
                            list.Add(item);
                            item = new LineZH();
                            num3 = 0;
                        }
                    }
                    if (item.e.Count > 0)
                    {
                        list.Add(item);
                    }
                    return list;
                }
                foreach (AisinoPrintLabel.CharPrintEvent event3 in this.list_0)
                {
                    SizeF ef;
                    switch (event3.CharStyle)
                    {
                        case AisinoPrintLabel.CharPrintEnum.Char:
                            ef = graphics.MeasureString("a", font_0, (PointF) Point.Empty, genericTypographic);
                            break;

                        case AisinoPrintLabel.CharPrintEnum.Number:
                            ef = graphics.MeasureString("0", font_0, (PointF) Point.Empty, genericTypographic);
                            break;

                        case AisinoPrintLabel.CharPrintEnum.Chinese:
                            ef = graphics.MeasureString("国", font_0, (PointF) Point.Empty, genericTypographic);
                            break;

                        case AisinoPrintLabel.CharPrintEnum.Enter:
                        {
                            num = 0f;
                            num2 = (num2 + font_0.Height) + float_2;
                            list.Add(item);
                            item = new LineZH();
                            continue;
                        }
                        default:
                            if (event3.e == " ")
                            {
                                ef = graphics.MeasureString("0", font_0, (PointF) Point.Empty, genericTypographic);
                            }
                            else
                            {
                                ef = graphics.MeasureString(event3.e, font_0, (PointF) Point.Empty, genericTypographic);
                            }
                            break;
                    }
                    if ((num + ef.Width) > float_0)
                    {
                        num = 0f;
                        num2 = (num2 + font_0.Height) + float_2;
                        list.Add(item);
                        item = new LineZH();
                    }
                    num += ef.Width;
                    item.e.Add(event3);
                    item.Width = (int) num;
                    num += float_1;
                }
                if (item.e.Count > 0)
                {
                    num2 += font_0.Height;
                    list.Add(item);
                }
                return list;
            }

            public class LineZH
            {
                public List<AisinoPrintLabel.CharPrintEvent> e;
                public int Width;

                public LineZH()
                {
                    
                    this.e = new List<AisinoPrintLabel.CharPrintEvent>();
                }

                public override string ToString()
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (AisinoPrintLabel.CharPrintEvent event2 in this.e)
                    {
                        builder.Append(event2.e);
                    }
                    return builder.ToString();
                }
            }
        }

        public class ZH
        {
            private float float_0;
            private float float_1;
            private float float_2;
            private Font font_0;
            private Graphics graphics_0;
            public float Height;
            public List<listStr> list;
            private string string_0;

            public ZH(string string_1, float float_3, float float_4, float float_5, Font font_1, Graphics graphics_1)
            {
                
                this.list = new List<listStr>();
                this.string_0 = string_1;
                this.float_0 = float_3;
                this.float_1 = float_4;
                this.float_2 = float_5;
                this.font_0 = font_1;
                this.graphics_0 = graphics_1;
                this.method_0();
            }

            public string[] GetZheHang()
            {
                List<string> list = new List<string>();
                foreach (listStr str in this.list)
                {
                    list.Add(str.Test);
                }
                return list.ToArray();
            }

            private void method_0()
            {
                string[] strArray = Regex.Split(this.string_0, Environment.NewLine);
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                foreach (string str2 in strArray)
                {
                    StringBuilder builder = new StringBuilder();
                    float width = 0f;
                    foreach (char ch in str2)
                    {
                        SizeF ef;
                        if (ch == ' ')
                        {
                            ef = this.graphics_0.MeasureString("0", this.font_0, (PointF) Point.Empty, genericTypographic);
                        }
                        else
                        {
                            ef = this.graphics_0.MeasureString(ch.ToString(), this.font_0, PointF.Empty, genericTypographic);
                        }
                        width += ef.Width + this.float_1;
                        if ((width - this.float_1) > this.float_0)
                        {
                            listStr item = new listStr {
                                Test = builder.ToString(),
                                width = (width - ef.Width) - this.float_1
                            };
                            this.list.Add(item);
                            builder.Remove(0, builder.Length);
                            width = ef.Width;
                        }
                        builder.Append(ch);
                    }
                    if (builder.Length > 0)
                    {
                        listStr str4 = new listStr {
                            Test = builder.ToString(),
                            width = width
                        };
                        this.list.Add(str4);
                    }
                }
                this.Height = (this.list.Count * (this.font_0.Height + this.float_2)) - this.float_2;
            }

            public class listStr
            {
                public string Test;
                public float width;

                public listStr()
                {
                    
                }
            }
        }
    }
}

