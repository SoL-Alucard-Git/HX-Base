namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Xml;

    public class AisinoPrintRepeater : AisinoPrintPanel
    {
        protected string _DataBind;
        private DataTable dataTable_0;

        public AisinoPrintRepeater()
        {
            
        }

        public AisinoPrintRepeater(XmlElement xmlElement_0) : base(xmlElement_0)
        {
            
            string attribute = xmlElement_0.GetAttribute("DataBind");
            this.DataBind = attribute;
        }

        public override XmlElement CreateXmlElement(XmlDocument xmlDocument_0)
        {
            return xmlDocument_0.CreateElement("Repeater");
        }

        protected override void Draw(PointF pointF_0, Graphics graphics_0, bool bool_0)
        {
            base.Draw(pointF_0, graphics_0, bool_0);
            float height = 0f;
            foreach (AisinoPrintControl control3 in base.Nodes)
            {
                if (control3.Height > height)
                {
                    height = control3.Height;
                }
            }
            List<AisinoPrintControl> list = new List<AisinoPrintControl>();
            foreach (AisinoPrintControl control in base.Nodes)
            {
                control.Height = height;
                bool flag = false;
                using (List<AisinoPrintControl>.Enumerator enumerator3 = list.GetEnumerator())
                {
                    AisinoPrintControl current;
                    while (enumerator3.MoveNext())
                    {
                        current = enumerator3.Current;
                        if (current.Location.X > control.Location.X)
                        {
                            goto Label_00BE;
                        }
                    }
                    goto Label_00E2;
                Label_00BE:
                    list.Insert(list.IndexOf(current), control);
                    flag = true;
                }
            Label_00E2:
                if (!flag)
                {
                    list.Add(control);
                }
            }
            float x = 0f;
            foreach (AisinoPrintControl control4 in list)
            {
                control4.Location = new PointF(x, 0f);
                x += control4.Width;
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
            if (this.dataTable_0 != null)
            {
                float height = 0f;
                foreach (AisinoPrintControl control in base.Nodes)
                {
                    if (control.Height > height)
                    {
                        height = control.Height;
                    }
                }
                PointF absoluteLocation = base.absoluteLocation;
                PointF tf2 = absoluteLocation;
                foreach (DataRow row in this.dataTable_0.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    foreach (DataColumn column in this.dataTable_0.Columns)
                    {
                        dictionary.Add(column.ColumnName, row[column.ColumnName]);
                    }
                    foreach (AisinoPrintControl control2 in base.Nodes)
                    {
                        control2.Print(tf2, dictionary, graphics_0, bool_0);
                    }
                    tf2.Y += height;
                }
                tf2 = absoluteLocation;
            }
        }

        public override XmlElement ToXmlNode(XmlDocument xmlDocument_0)
        {
            XmlElement element = base.ToXmlNode(xmlDocument_0);
            if (this != null)
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
    }
}

