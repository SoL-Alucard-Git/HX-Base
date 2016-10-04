namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Xml;

    public class AisinoPrintGrid : AisinoPrintPanel
    {
        protected string _DataBind;
        protected int _HeadHeight;
        protected bool _IsTitle;
        protected int _LineHeight;
        private DataTable dataTable_0;

        public AisinoPrintGrid()
        {
            
            this._HeadHeight = 0x10;
            this._LineHeight = 0x10;
            this._DataBind = string.Empty;
        }

        public AisinoPrintGrid(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            this._HeadHeight = 0x10;
            this._LineHeight = 0x10;
            this._DataBind = string.Empty;
            string attribute = xmlElement_0.GetAttribute("LineHeight");
            string str2 = xmlElement_0.GetAttribute("IsTitle");
            string str3 = xmlElement_0.GetAttribute("DataBind");
            string str4 = xmlElement_0.GetAttribute("HeadHeight");
            if (string.IsNullOrEmpty(attribute))
            {
                this._LineHeight = 0x10;
            }
            else
            {
                this._LineHeight = Common.ToInt(attribute);
            }
            if (string.IsNullOrEmpty(str4))
            {
                this._HeadHeight = 20;
            }
            else
            {
                this._HeadHeight = Common.ToInt(str4);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                this._IsTitle = Common.ToBool(str2);
            }
            XmlNodeList childNodes = xmlElement_0.ChildNodes;
            if (childNodes.Count > 0)
            {
                foreach (XmlNode node in childNodes)
                {
                    XmlElement element = node as XmlElement;
                    if (node.Name == "GridLabel")
                    {
                        AisinoPrintControl control = new AisinoPrintGridLabel(element, this);
                        base.AddNode(control);
                    }
                }
            }
            this.DataBind = str3;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Grid");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            base.Draw(pointF_0, graphics_0, bool_0);
            List<AisinoPrintControl> list = new List<AisinoPrintControl>();
            foreach (AisinoPrintControl control in base.Nodes)
            {
                control.Height = this.LineHeight;
                bool flag = false;
                using (List<AisinoPrintControl>.Enumerator enumerator2 = list.GetEnumerator())
                {
                    AisinoPrintControl current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.Location.X > control.Location.X)
                        {
                            goto Label_0078;
                        }
                    }
                    goto Label_0099;
                Label_0078:
                    list.Insert(list.IndexOf(current), control);
                    flag = true;
                }
            Label_0099:
                if (!flag)
                {
                    list.Add(control);
                }
            }
            float x = 0f;
            foreach (AisinoPrintControl control3 in list)
            {
                control3.Location = new PointF(x, 0f);
                x += control3.Width;
            }
        }

        protected override void Load(Dictionary<string, object> dict)
        {
            if ((this.DataBind.Length > 0) && dict.ContainsKey(this.DataBind))
            {
                this.dataTable_0 = dict[this.DataBind] as DataTable;
            }
        }

        public override void Print(PointF pointF_0, Dictionary<string, object> dict, Graphics graphics_0, bool bool_0)
        {
            base.Print(pointF_0, dict, graphics_0, bool_0);
            if (base._isPrint)
            {
                PointF absoluteLocation = base.absoluteLocation;
                if (this.IsTitle)
                {
                    foreach (AisinoPrintControl control in base.Nodes)
                    {
                        AisinoPrintGridLabel label = control as AisinoPrintGridLabel;
                        if (label != null)
                        {
                            label.PrintHead(absoluteLocation, graphics_0);
                        }
                    }
                    absoluteLocation.Y += this.HeadHeight;
                }
                if (this.dataTable_0 != null)
                {
                    foreach (DataRow row in this.dataTable_0.Rows)
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        foreach (DataColumn column in this.dataTable_0.Columns)
                        {
                            dictionary.Add(column.ColumnName, row[column.ColumnName]);
                        }
                        foreach (AisinoPrintControl control2 in base.Nodes)
                        {
                            control2.Print(absoluteLocation, dictionary, graphics_0, bool_0);
                        }
                        absoluteLocation.Y += this.LineHeight;
                    }
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
                if (this.IsTitle)
                {
                    element.SetAttribute("IsTitle", this.IsTitle.ToString());
                }
                element.SetAttribute("LineHeight", this.LineHeight.ToString());
                element.SetAttribute("HeadHeight", this.HeadHeight.ToString());
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

        public int HeadHeight
        {
            get
            {
                return this._HeadHeight;
            }
            set
            {
                this._HeadHeight = value;
            }
        }

        public bool IsTitle
        {
            get
            {
                return this._IsTitle;
            }
            set
            {
                this._IsTitle = value;
            }
        }

        public int LineHeight
        {
            get
            {
                return this._LineHeight;
            }
            set
            {
                this._LineHeight = value;
            }
        }

        public class AisinoPrintGridLabel : AisinoPrintLabel
        {
            protected string _HeadText;
            private AisinoPrintGrid aisinoPrintGrid_0;

            public AisinoPrintGridLabel(XmlElement xmlElement_0, AisinoPrintGrid aisinoPrintGrid_1) : base(xmlElement_0)
            {
                
                this._HeadText = string.Empty;
                string attribute = xmlElement_0.GetAttribute("HeadText");
                this._HeadText = attribute;
                this.aisinoPrintGrid_0 = aisinoPrintGrid_1;
            }

            public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
            {
                return xmlDocument_0.CreateElement("GridLabel");
            }

            public void PrintHead(PointF pointF_0, Graphics graphics_0)
            {
                AisinoPrintLabel label = this;
                label.Text = this.HeadText;
                label.DataBind = string.Empty;
                label.Print(pointF_0, null, graphics_0, false);
            }

            public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
            {
                return base.ToXmlNode(xmlDocument_0);
            }

            public string HeadText
            {
                get
                {
                    return this._HeadText;
                }
                set
                {
                    this._HeadText = value;
                }
            }
        }
    }
}

