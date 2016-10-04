namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class XmlComponentUtil
    {
        public XmlComponentUtil()
        {
            
        }

        public static void GenerateDataGridView(XmlTextWriter xmlTextWriter_0, DataGridView dataGridView_0)
        {
            GenerateDataGridView(xmlTextWriter_0, dataGridView_0, false);
        }

        public static void GenerateDataGridView(XmlTextWriter xmlTextWriter_0, DataGridView dataGridView_0, bool bool_0)
        {
            GenerateDataGridView(xmlTextWriter_0, dataGridView_0, string.Empty, string.Empty, bool_0);
        }

        public static void GenerateDataGridView(XmlTextWriter xmlTextWriter_0, DataGridView dataGridView_0, string string_0, string string_1, bool bool_0)
        {
            DataGridView view = new DataGridView();
            xmlTextWriter_0.WriteStartElement("DataGridViewItem");
            if (!string.Empty.Equals(string_0))
            {
                WriteAttribute(xmlTextWriter_0, "id", string_0);
            }
            else
            {
                WriteAttribute(xmlTextWriter_0, "id", dataGridView_0.Name);
            }
            if (!string.Empty.Equals(string_1))
            {
                WriteAttribute(xmlTextWriter_0, "control.type", string_1);
            }
            else
            {
                WriteAttribute(xmlTextWriter_0, "control.type", dataGridView_0.GetType().FullName);
            }
            WriteAttribute(xmlTextWriter_0, "Dock", GetString("Dock", dataGridView_0.Dock));
            WriteAttribute(xmlTextWriter_0, "Anchor", GetString("Anchor", dataGridView_0.Anchor));
            if (!view.BackgroundColor.Equals(dataGridView_0.BackgroundColor))
            {
                WriteAttribute(xmlTextWriter_0, "BackgroundColor", GetString("RGBColor", dataGridView_0.BackgroundColor));
            }
            WriteAttribute(xmlTextWriter_0, "BorderStyle", GetString("BorderStyle", dataGridView_0.BorderStyle));
            WriteAttribute(xmlTextWriter_0, "CellBorderStyle", GetString("DataGridViewCellBorderStyle", dataGridView_0.CellBorderStyle));
            WriteAttribute(xmlTextWriter_0, "ColumnHeadersBorderStyle", GetString("DataGridViewHeaderBorderStyle", dataGridView_0.ColumnHeadersBorderStyle));
            WriteAttribute(xmlTextWriter_0, "ColumnHeadersHeight", GetString("Int32", dataGridView_0.ColumnHeadersHeight));
            WriteAttribute(xmlTextWriter_0, "ColumnHeadersVisible", GetString("Bool", dataGridView_0.ColumnHeadersVisible));
            WriteAttribute(xmlTextWriter_0, "EnableHeadersVisualStyles", GetString("Bool", dataGridView_0.EnableHeadersVisualStyles));
            if (!view.GridColor.Equals(dataGridView_0.GridColor))
            {
                WriteAttribute(xmlTextWriter_0, "GridColor", GetString("RGBColor", dataGridView_0.GridColor));
            }
            WriteAttribute(xmlTextWriter_0, "RowHeadersBorderStyle", GetString("DataGridViewHeaderBorderStyle", dataGridView_0.RowHeadersBorderStyle));
            WriteAttribute(xmlTextWriter_0, "RowHeadersVisible", GetString("Bool", dataGridView_0.RowHeadersVisible));
            if ((dataGridView_0.Columns != null) && (dataGridView_0.Columns.Count > 0))
            {
                xmlTextWriter_0.WriteStartElement("DataGridViewColumnCollection");
                foreach (DataGridViewColumn column in dataGridView_0.Columns)
                {
                    xmlTextWriter_0.WriteStartElement("DataGridViewColumnItem");
                    WriteAttribute(xmlTextWriter_0, "id", column.Name);
                    WriteAttribute(xmlTextWriter_0, "control.type", column.GetType().FullName);
                    WriteAttribute(xmlTextWriter_0, "AutoSizeMode", GetString("DataGridViewAutoSizeColumnMode", column.AutoSizeMode));
                    WriteAttribute(xmlTextWriter_0, "DividerWidth", GetString("Int32", column.DividerWidth));
                    WriteAttribute(xmlTextWriter_0, "FillWeight", GetString("Int32", column.FillWeight));
                    WriteAttribute(xmlTextWriter_0, "Frozen", GetString("Bool", column.Frozen));
                    WriteAttribute(xmlTextWriter_0, "MinimumWidth", GetString("Int32", column.MinimumWidth));
                    WriteAttribute(xmlTextWriter_0, "Width", GetString("Int32", column.Width));
                    WriteAttribute(xmlTextWriter_0, "DataPropertyName", GetString("string", column.DataPropertyName));
                    WriteAttribute(xmlTextWriter_0, "DisplayIndex", GetString("Int32", column.DisplayIndex));
                    WriteAttribute(xmlTextWriter_0, "HeaderText", GetString("string", column.HeaderText));
                    WriteAttribute(xmlTextWriter_0, "ToolTipText", GetString("string", column.ToolTipText));
                    WriteAttribute(xmlTextWriter_0, "Visible", GetString("Bool", column.Visible));
                    WriteAttribute(xmlTextWriter_0, "ReadOnly", GetString("Bool", column.ReadOnly));
                    if (!(column is DataGridViewTextBoxColumn))
                    {
                        if (column is DataGridViewLinkColumn)
                        {
                            WriteAttribute(xmlTextWriter_0, "ActiveLinkColor", GetString("RGBColor", ((DataGridViewLinkColumn) column).ActiveLinkColor));
                            WriteAttribute(xmlTextWriter_0, "LinkColor", GetString("RGBColor", ((DataGridViewLinkColumn) column).LinkColor));
                            WriteAttribute(xmlTextWriter_0, "Text", GetString("string", ((DataGridViewLinkColumn) column).Text));
                            WriteAttribute(xmlTextWriter_0, "UseColumnTextForLinkValue", GetString("Bool", ((DataGridViewLinkColumn) column).UseColumnTextForLinkValue));
                            WriteAttribute(xmlTextWriter_0, "VisitedLinkColor", GetString("RGBColor", ((DataGridViewLinkColumn) column).VisitedLinkColor));
                        }
                        else if (column is DataGridViewImageColumn)
                        {
                            WriteAttribute(xmlTextWriter_0, "Description", GetString("string", ((DataGridViewImageColumn) column).Description));
                            WriteAttribute(xmlTextWriter_0, "Image", GetString("Image", ((DataGridViewImageColumn) column).Image));
                            WriteAttribute(xmlTextWriter_0, "ImageLayout", GetString("DataGridViewImageCellLayout", ((DataGridViewImageColumn) column).ImageLayout));
                        }
                        else if (column is DataGridViewComboBoxColumn)
                        {
                            WriteAttribute(xmlTextWriter_0, "DisplayStyle", GetString("DataGridViewComboBoxDisplayStyle", ((DataGridViewComboBoxColumn) column).DisplayStyle));
                            WriteAttribute(xmlTextWriter_0, "DisplayStyleForCurrentCellOnly", GetString("Bool", ((DataGridViewComboBoxColumn) column).DisplayStyleForCurrentCellOnly));
                            WriteAttribute(xmlTextWriter_0, "FlatStyle", GetString("FlatStyle", ((DataGridViewComboBoxColumn) column).FlatStyle));
                        }
                        else if (column is DataGridViewCheckBoxColumn)
                        {
                            WriteAttribute(xmlTextWriter_0, "FlatStyle", GetString("FlatStyle", ((DataGridViewCheckBoxColumn) column).FlatStyle));
                        }
                        else if (column is DataGridViewButtonColumn)
                        {
                            WriteAttribute(xmlTextWriter_0, "FlatStyle", GetString("FlatStyle", ((DataGridViewButtonColumn) column).FlatStyle));
                            WriteAttribute(xmlTextWriter_0, "Text", GetString("string", ((DataGridViewButtonColumn) column).Text));
                            WriteAttribute(xmlTextWriter_0, "UseColumnTextForButtonValue", GetString("Bool", ((DataGridViewButtonColumn) column).UseColumnTextForButtonValue));
                        }
                    }
                    if (bool_0)
                    {
                        WriteDataGridViewCellStyle(xmlTextWriter_0, column.DefaultCellStyle, "DefaultCellStyle");
                    }
                    xmlTextWriter_0.WriteEndElement();
                }
                xmlTextWriter_0.WriteEndElement();
            }
            xmlTextWriter_0.WriteEndElement();
        }

        public static DataGridViewCellStyle GetDataGridCellStyle(XmlNode xmlNode_0, object object_0)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            if (style != null)
            {
                foreach (XmlAttribute attribute in attributes)
                {
                    string name = attribute.Name;
                    if (!"id".Equals(name) && !"control.type".Equals(name))
                    {
                        PropertyInfo property = style.GetType().GetProperty(name);
                        if (property != null)
                        {
                            string str2 = attribute.Value;
                            int index = str2.IndexOf(":");
                            string str3 = str2.Substring(0, index);
                            string str4 = (str2.Length > (index + 1)) ? str2.Substring(index + 1) : null;
                            if (str4 != null)
                            {
                                object obj2 = GetObjectValue(null, str3, str4);
                                if (obj2 != null)
                                {
                                    property.SetValue(style, obj2, null);
                                }
                            }
                        }
                    }
                }
            }
            return style;
        }

        public static DataGridViewColumn GetDataGridViewColumnItem(XmlNode xmlNode_0, DataGridView dataGridView_0)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            string str = attributes["id"].Value;
            string str2 = attributes["control.type"].Value;
            DataGridViewColumn column = null;
            if ("System.Windows.Forms.DataGridViewTextBoxColumn".Equals(str2))
            {
                column = new DataGridViewTextBoxColumn();
            }
            else if ("System.Windows.Forms.DataGridViewLinkColumn".Equals(str2))
            {
                column = new DataGridViewLinkColumn();
            }
            else if ("System.Windows.Forms.DataGridViewImageColumn".Equals(str2))
            {
                column = new DataGridViewImageColumn();
            }
            else if ("System.Windows.Forms.DataGridViewComboBoxColumn".Equals(str2))
            {
                column = new DataGridViewComboBoxColumn();
            }
            else if ("System.Windows.Forms.DataGridViewCheckBoxColumn".Equals(str2))
            {
                column = new DataGridViewCheckBoxColumn();
            }
            else if ("System.Windows.Forms.DataGridViewButtonColumn".Equals(str2))
            {
                column = new DataGridViewButtonColumn();
            }
            if (column != null)
            {
                column.Name = str;
                foreach (XmlAttribute attribute in attributes)
                {
                    string name = attribute.Name;
                    if (!"id".Equals(name) && !"control.type".Equals(name))
                    {
                        PropertyInfo property = column.GetType().GetProperty(name);
                        if (property != null)
                        {
                            string str4 = attribute.Value;
                            int index = str4.IndexOf(":");
                            string str5 = str4.Substring(0, index);
                            string str6 = (str4.Length > (index + 1)) ? str4.Substring(index + 1) : null;
                            if (str6 != null)
                            {
                                object obj2 = GetObjectValue(null, str5, str6);
                                if (obj2 != null)
                                {
                                    property.SetValue(column, obj2, null);
                                }
                            }
                        }
                    }
                }
            }
            if (xmlNode_0.HasChildNodes)
            {
                foreach (XmlNode node in xmlNode_0)
                {
                    if ("DefaultCellStyle".Equals(node.Name))
                    {
                        column.DefaultCellStyle = GetDataGridCellStyle(node, column);
                    }
                }
            }
            return column;
        }

        public static object GetObjectValue(Control control_0, string string_0, string string_1)
        {
            if ("Int32".Equals(string_0))
            {
                return Convert.ToInt32(string_1);
            }
            if ("Int64".Equals(string_0))
            {
                return Convert.ToInt64(string_1);
            }
            if ("Color".Equals(string_0))
            {
                return Color.FromName(string_1);
            }
            if ("RGBColor".Equals(string_0))
            {
                string[] strArray3 = string_1.Split(",".ToCharArray());
                int red = Convert.ToInt32(strArray3[0]);
                int green = Convert.ToInt32(strArray3[1]);
                int blue = Convert.ToInt32(strArray3[2]);
                return Color.FromArgb(red, green, blue);
            }
            if (!"Font".Equals(string_0))
            {
                if ("Point".Equals(string_0))
                {
                    int index = string_1.IndexOf(",");
                    int width = Convert.ToInt32(string_1.Substring(0, index));
                    return new Size(width, Convert.ToInt32(string_1.Substring(index + 1)));
                }
                if ("LocationPoint".Equals(string_0))
                {
                    int length = string_1.IndexOf(",");
                    int x = Convert.ToInt32(string_1.Substring(0, length));
                    return new Point(x, Convert.ToInt32(string_1.Substring(length + 1)));
                }
                if ("Bool".Equals(string_0))
                {
                    return Convert.ToBoolean(string_1);
                }
                if ("IList".Equals(string_0))
                {
                    int num17 = string_1.IndexOf("{");
                    int num18 = string_1.IndexOf("}");
                    string[] strArray8 = string_1.Substring(num17 + 1, (num18 - num17) - 1).Split(",".ToCharArray());
                    IList list = new ArrayList();
                    foreach (string str2 in strArray8)
                    {
                        list.Add(str2);
                    }
                    return list;
                }
                if ("BorderStyle".Equals(string_0))
                {
                    if ("FixedSingle".Equals(string_1))
                    {
                        return BorderStyle.FixedSingle;
                    }
                    if ("Fixed3D".Equals(string_1))
                    {
                        return BorderStyle.Fixed3D;
                    }
                    return BorderStyle.None;
                }
                if ("Columns".Equals(string_0))
                {
                    ListView view2 = control_0 as ListView;
                    foreach (string str7 in string_1.Split(",".ToCharArray()))
                    {
                        ColumnHeader header = new ColumnHeader {
                            Text = str7
                        };
                        view2.Columns.Add(header);
                    }
                    return null;
                }
                if ("ColumnWidths".Equals(string_0))
                {
                    ListView view = control_0 as ListView;
                    string[] strArray6 = string_1.Split(",".ToCharArray());
                    int num8 = 0;
                    foreach (string str4 in strArray6)
                    {
                        int num7 = Convert.ToInt32(str4);
                        view.Columns[num8++].Width = num7;
                    }
                    return null;
                }
                if ("View".Equals(string_0))
                {
                    if ("Details".Equals(string_1))
                    {
                        return View.Details;
                    }
                    if ("LargeIcon".Equals(string_1))
                    {
                        return View.LargeIcon;
                    }
                    if ("SmallIcon".Equals(string_1))
                    {
                        return View.SmallIcon;
                    }
                    if ("List".Equals(string_1))
                    {
                        return View.List;
                    }
                }
                if ("Dock".Equals(string_0))
                {
                    if ("Fill".Equals(string_1))
                    {
                        return DockStyle.Fill;
                    }
                    if ("Bottom".Equals(string_1))
                    {
                        return DockStyle.Bottom;
                    }
                    if ("Top".Equals(string_1))
                    {
                        return DockStyle.Top;
                    }
                    if ("Left".Equals(string_1))
                    {
                        return DockStyle.Left;
                    }
                    if ("Right".Equals(string_1))
                    {
                        return DockStyle.Right;
                    }
                    if ("None".Equals(string_1))
                    {
                        return DockStyle.None;
                    }
                }
                if ("Anchor".Equals(string_0))
                {
                    string[] strArray7 = string_1.Split(",".ToCharArray());
                    AnchorStyles none = AnchorStyles.None;
                    bool flag = true;
                    foreach (string str6 in strArray7)
                    {
                        AnchorStyles top = AnchorStyles.None;
                        if ("Top".Equals(str6))
                        {
                            top = AnchorStyles.Top;
                        }
                        else if ("Bottom".Equals(str6))
                        {
                            top = AnchorStyles.Bottom;
                        }
                        else if ("Left".Equals(str6))
                        {
                            top = AnchorStyles.Left;
                        }
                        else if ("Right".Equals(str6))
                        {
                            top = AnchorStyles.Right;
                        }
                        else if ("None".Equals(str6))
                        {
                            top = AnchorStyles.None;
                        }
                        if (flag)
                        {
                            flag = false;
                            none = top;
                        }
                        else
                        {
                            none |= top;
                        }
                    }
                    return none;
                }
                if ("Image".Equals(string_0))
                {
                    return Image.FromStream(new MemoryStream(Convert.FromBase64String(string_1.ToString())));
                }
                if ("char".Equals(string_0))
                {
                    return Convert.ToChar(string_1);
                }
                if ("DialogResult".Equals(string_0))
                {
                    string str8 = string_1.ToString();
                    if ("OK".Equals(str8))
                    {
                        return DialogResult.OK;
                    }
                    if ("Cancel".Equals(str8))
                    {
                        return DialogResult.Cancel;
                    }
                    return DialogResult.None;
                }
                if ("SplitterDistance".Equals(string_0))
                {
                    return Convert.ToInt32(string_1);
                }
                if ("SplitterIncrement".Equals(string_0))
                {
                    return Convert.ToInt32(string_1);
                }
                if ("SplitterWidth".Equals(string_0))
                {
                    return Convert.ToInt32(string_1);
                }
                if ("Orientation".Equals(string_0))
                {
                    if ("Vertical".Equals(string_1))
                    {
                        return Orientation.Vertical;
                    }
                    if ("Horizontal".Equals(string_1))
                    {
                        return Orientation.Horizontal;
                    }
                }
                if ("RowCount".Equals(string_0))
                {
                    return Convert.ToInt32(string_1);
                }
                if ("ColumnCount".Equals(string_0))
                {
                    return Convert.ToInt32(string_1);
                }
                if ("SizeType".Equals(string_0))
                {
                    if ("Percent".Equals(string_1))
                    {
                        return SizeType.Percent;
                    }
                    if ("Absolute".Equals(string_1))
                    {
                        return SizeType.Absolute;
                    }
                    if ("AutoSize".Equals(string_1))
                    {
                        return SizeType.AutoSize;
                    }
                }
                if ("TableLayoutPanelSizeTypeValue".Equals(string_0))
                {
                    return Convert.ToSingle(string_1);
                }
                if ("FlatStyle".Equals(string_0))
                {
                    if ("Flat".Equals(string_1))
                    {
                        return FlatStyle.Flat;
                    }
                    if ("Popup".Equals(string_1))
                    {
                        return FlatStyle.Popup;
                    }
                    if ("Standard".Equals(string_1))
                    {
                        return FlatStyle.Standard;
                    }
                    if ("System".Equals(string_1))
                    {
                        return FlatStyle.System;
                    }
                }
                if ("HorizontalAlignment".Equals(string_0))
                {
                    if ("Center".Equals(string_1))
                    {
                        return HorizontalAlignment.Center;
                    }
                    if ("Left".Equals(string_1))
                    {
                        return HorizontalAlignment.Left;
                    }
                    if ("Right".Equals(string_1))
                    {
                        return HorizontalAlignment.Right;
                    }
                }
                if ("ContentAlignment".Equals(string_0))
                {
                    if ("BottomCenter".Equals(string_1))
                    {
                        return ContentAlignment.BottomCenter;
                    }
                    if ("BottomLeft".Equals(string_1))
                    {
                        return ContentAlignment.BottomLeft;
                    }
                    if ("BottomRight".Equals(string_1))
                    {
                        return ContentAlignment.BottomRight;
                    }
                    if ("MiddleCenter".Equals(string_1))
                    {
                        return ContentAlignment.MiddleCenter;
                    }
                    if ("MiddleLeft".Equals(string_1))
                    {
                        return ContentAlignment.MiddleLeft;
                    }
                    if ("MiddleRight".Equals(string_1))
                    {
                        return ContentAlignment.MiddleRight;
                    }
                    if ("TopCenter".Equals(string_1))
                    {
                        return ContentAlignment.TopCenter;
                    }
                    if ("TopLeft".Equals(string_1))
                    {
                        return ContentAlignment.TopLeft;
                    }
                    if ("TopRight".Equals(string_1))
                    {
                        return ContentAlignment.TopRight;
                    }
                }
                if ("DataGridViewAutoSizeColumnMode".Equals(string_0))
                {
                    if ("AllCells".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    if ("AllCellsExceptHeader".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    }
                    if ("ColumnHeader".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.ColumnHeader;
                    }
                    if ("DisplayedCells".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }
                    if ("DisplayedCellsExceptHeader".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                    }
                    if ("Fill".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if ("None".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.None;
                    }
                    if ("NotSet".Equals(string_1))
                    {
                        return DataGridViewAutoSizeColumnMode.NotSet;
                    }
                }
                if ("DataGridViewImageCellLayout".Equals(string_0))
                {
                    if ("Normal".Equals(string_1))
                    {
                        return DataGridViewImageCellLayout.Normal;
                    }
                    if ("NotSet".Equals(string_1))
                    {
                        return DataGridViewImageCellLayout.NotSet;
                    }
                    if ("Stretch".Equals(string_1))
                    {
                        return DataGridViewImageCellLayout.Stretch;
                    }
                    if ("Zoom".Equals(string_1))
                    {
                        return DataGridViewImageCellLayout.Zoom;
                    }
                }
                if ("DataGridViewComboBoxDisplayStyle".Equals(string_0))
                {
                    if ("ComboBox".Equals(string_1))
                    {
                        return DataGridViewComboBoxDisplayStyle.ComboBox;
                    }
                    if ("DropDownButton".Equals(string_1))
                    {
                        return DataGridViewComboBoxDisplayStyle.DropDownButton;
                    }
                    if ("Nothing".Equals(string_1))
                    {
                        return DataGridViewComboBoxDisplayStyle.Nothing;
                    }
                }
                if ("DataGridViewContentAlignment".Equals(string_0))
                {
                    if ("BottomCenter".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.BottomCenter;
                    }
                    if ("BottomLeft".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.BottomLeft;
                    }
                    if ("BottomRight".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.BottomRight;
                    }
                    if ("MiddleCenter".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.MiddleCenter;
                    }
                    if ("MiddleLeft".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.MiddleLeft;
                    }
                    if ("MiddleRight".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.MiddleRight;
                    }
                    if ("NotSet".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.NotSet;
                    }
                    if ("TopCenter".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.TopCenter;
                    }
                    if ("TopLeft".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.TopLeft;
                    }
                    if ("TopRight".Equals(string_1))
                    {
                        return DataGridViewContentAlignment.TopRight;
                    }
                }
                if ("DataGridViewTriState".Equals(string_0))
                {
                    if ("False".Equals(string_1))
                    {
                        return DataGridViewTriState.False;
                    }
                    if ("NotSet".Equals(string_1))
                    {
                        return DataGridViewTriState.NotSet;
                    }
                    if ("True".Equals(string_1))
                    {
                        return DataGridViewTriState.True;
                    }
                }
                if ("DataGridViewHeaderBorderStyle".Equals(string_0))
                {
                    if ("Custom".Equals(string_1))
                    {
                        return DataGridViewHeaderBorderStyle.Custom;
                    }
                    if ("None".Equals(string_1))
                    {
                        return DataGridViewHeaderBorderStyle.None;
                    }
                    if ("Raised".Equals(string_1))
                    {
                        return DataGridViewHeaderBorderStyle.Raised;
                    }
                    if ("Single".Equals(string_1))
                    {
                        return DataGridViewHeaderBorderStyle.Single;
                    }
                    if ("Sunken".Equals(string_1))
                    {
                        return DataGridViewHeaderBorderStyle.Sunken;
                    }
                }
                if ("DataGridViewCellBorderStyle".Equals(string_0))
                {
                    if ("Custom".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.Custom;
                    }
                    if ("None".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.None;
                    }
                    if ("Raised".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.Raised;
                    }
                    if ("RaisedHorizontal".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.RaisedHorizontal;
                    }
                    if ("RaisedVertical".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.RaisedVertical;
                    }
                    if ("Single".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.Single;
                    }
                    if ("SingleHorizontal".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.SingleHorizontal;
                    }
                    if ("SingleVertical".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.SingleVertical;
                    }
                    if ("Sunken".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.Sunken;
                    }
                    if ("SunkenHorizontal".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.SunkenHorizontal;
                    }
                    if ("SunkenVertical".Equals(string_1))
                    {
                        return DataGridViewCellBorderStyle.SunkenVertical;
                    }
                }
                if ("TextImageRelation".Equals(string_0))
                {
                    if ("ImageAboveText".Equals(string_1))
                    {
                        return TextImageRelation.ImageAboveText;
                    }
                    if ("ImageBeforeText".Equals(string_1))
                    {
                        return TextImageRelation.ImageBeforeText;
                    }
                    if ("Overlay".Equals(string_1))
                    {
                        return TextImageRelation.Overlay;
                    }
                    if ("TextAboveImage".Equals(string_1))
                    {
                        return TextImageRelation.TextAboveImage;
                    }
                    if ("TextBeforeImage".Equals(string_1))
                    {
                        return TextImageRelation.TextBeforeImage;
                    }
                }
                return string_1;
            }
            string[] strArray9 = string_1.Split(new char[] { ',' });
            string familyName = strArray9[0];
            double num9 = Convert.ToDouble(strArray9[1]);
            string str3 = strArray9[2];
            if ((str3 == null) || !(str3 != ""))
            {
                return new Font(familyName, (float) num9);
            }
            FontStyle regular = FontStyle.Regular;
            string str = str3;
            if (str != null)
            {
                if (str == "Bold")
                {
                    regular = FontStyle.Bold;
                }
                else if (str == "Italic")
                {
                    regular = FontStyle.Italic;
                }
                else if (str == "Regular")
                {
                    regular = FontStyle.Regular;
                }
                else if (!(str == "Strikeout"))
                {
                    if (!(str == "Underline"))
                    {
                        goto Label_0153;
                    }
                    regular = FontStyle.Underline;
                }
                else
                {
                    regular = FontStyle.Strikeout;
                }
                goto Label_0156;
            }
        Label_0153:
            regular = FontStyle.Regular;
        Label_0156:
            return new Font(familyName, (float) num9, regular);
        }

        public static string GetString(string string_0, object object_0)
        {
            if (object_0 == null)
            {
                return (string_0 + ":");
            }
            if ("LocationPoint".Equals(string_0))
            {
                Point point = (Point) object_0;
                return string.Concat(new object[] { "LocationPoint:", point.X, ",", point.Y });
            }
            if ("Point".Equals(string_0))
            {
                Size size = (Size) object_0;
                return string.Concat(new object[] { "Point:", size.Width, ",", size.Height });
            }
            if ("Int32".Equals(string_0))
            {
                return ("Int32:" + object_0.ToString());
            }
            if ("Int64".Equals(string_0))
            {
                return ("Int64:" + object_0.ToString());
            }
            if ("Color".Equals(string_0))
            {
                Color color2 = (Color) object_0;
                return ("Color:" + color2.Name);
            }
            if ("RGBColor".Equals(string_0))
            {
                Color color = (Color) object_0;
                return string.Concat(new object[] { "RGBColor:", Convert.ToInt32(color.R), ",", Convert.ToInt32(color.G), ",", Convert.ToInt32(color.B) });
            }
            if ("Font".Equals(string_0))
            {
                Font font = object_0 as Font;
                return string.Concat(new object[] { "Font:", font.FontFamily.Name, ",", font.Size, ",", font.Style });
            }
            if ("Bool".Equals(string_0))
            {
                return ("Bool:" + object_0.ToString());
            }
            if ("IList".Equals(string_0))
            {
                IList list = object_0 as IList;
                StringBuilder builder4 = new StringBuilder();
                bool flag4 = true;
                foreach (object obj2 in list)
                {
                    if (flag4)
                    {
                        flag4 = false;
                    }
                    else
                    {
                        builder4.Append(",");
                    }
                    builder4.Append(obj2.ToString());
                }
                return ("IList:{" + builder4.ToString() + "}");
            }
            if ("BorderStyle".Equals(string_0))
            {
                return ("BorderStyle:" + object_0.ToString());
            }
            if ("Columns".Equals(string_0))
            {
                StringBuilder builder2 = new StringBuilder();
                ListView.ColumnHeaderCollection headers2 = object_0 as ListView.ColumnHeaderCollection;
                bool flag2 = true;
                foreach (ColumnHeader header in headers2)
                {
                    if (flag2)
                    {
                        flag2 = false;
                    }
                    else
                    {
                        builder2.Append(",");
                    }
                    builder2.Append(header.Text);
                }
                return ("Columns:" + builder2.ToString());
            }
            if ("ColumnWidths".Equals(string_0))
            {
                StringBuilder builder3 = new StringBuilder();
                ListView.ColumnHeaderCollection headers = object_0 as ListView.ColumnHeaderCollection;
                bool flag3 = true;
                foreach (ColumnHeader header2 in headers)
                {
                    if (flag3)
                    {
                        flag3 = false;
                    }
                    else
                    {
                        builder3.Append(",");
                    }
                    builder3.Append(header2.Width);
                }
                return ("ColumnWidths:" + builder3.ToString());
            }
            if ("View".Equals(string_0))
            {
                return ("View:" + object_0.ToString());
            }
            if ("Dock".Equals(string_0))
            {
                switch (((DockStyle) object_0))
                {
                    case DockStyle.Bottom:
                        return "Dock:Bottom";

                    case DockStyle.Fill:
                        return "Dock:Fill";

                    case DockStyle.Left:
                        return "Dock:Left";

                    case DockStyle.Right:
                        return "Dock:Right";

                    case DockStyle.Top:
                        return "Dock:Top";
                }
                return "Dock:None";
            }
            if ("Anchor".Equals(string_0))
            {
                AnchorStyles styles = (AnchorStyles) object_0;
                StringBuilder builder = new StringBuilder();
                bool flag = true;
                if ((styles & AnchorStyles.Bottom) != AnchorStyles.None)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    builder.Append("Bottom");
                }
                if ((styles & AnchorStyles.Left) != AnchorStyles.None)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    builder.Append("Left");
                }
                if ((styles & AnchorStyles.Right) != AnchorStyles.None)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    builder.Append("Right");
                }
                if ((styles & AnchorStyles.Top) != AnchorStyles.None)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    builder.Append("Top");
                }
                return ("Anchor:" + builder.ToString());
            }
            if ("Image".Equals(string_0))
            {
                Image image = object_0 as Image;
                string filename = image.GetHashCode().ToString();
                image.Save(filename);
                FileInfo info = new FileInfo(filename);
                byte[] buffer = new byte[info.Length];
                FileStream stream = new FileStream(filename, FileMode.Open);
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                File.Delete(filename);
                return ("Image:" + Convert.ToBase64String(buffer));
            }
            if ("DialogResult".Equals(string_0))
            {
                switch (((DialogResult) object_0))
                {
                    case DialogResult.OK:
                        return "DialogResult:OK";

                    case DialogResult.Cancel:
                        return "DialogResult:Cancel";
                }
                return "DialogResult:None";
            }
            if ("ContentAlignment".Equals(string_0))
            {
                switch (((ContentAlignment) object_0))
                {
                    case ContentAlignment.BottomCenter:
                        return "ContentAlignment:BottomCenter";

                    case ContentAlignment.BottomLeft:
                        return "ContentAlignment:BottomLeft";

                    case ContentAlignment.BottomRight:
                        return "ContentAlignment:BottomRight";

                    case ContentAlignment.MiddleCenter:
                        return "ContentAlignment:MiddleCenter";

                    case ContentAlignment.MiddleLeft:
                        return "ContentAlignment:MiddleLeft";

                    case ContentAlignment.MiddleRight:
                        return "ContentAlignment:MiddleRight";

                    case ContentAlignment.TopCenter:
                        return "ContentAlignment:TopCenter";

                    case ContentAlignment.TopLeft:
                        return "ContentAlignment:TopLeft";

                    case ContentAlignment.TopRight:
                        return "ContentAlignment:TopRight";
                }
                return "ContentAlignment:MiddleCenter";
            }
            if ("HorizontalAlignment".Equals(string_0))
            {
                switch (((HorizontalAlignment) object_0))
                {
                    case HorizontalAlignment.Center:
                        return "HorizontalAlignment:Center";

                    case HorizontalAlignment.Left:
                        return "HorizontalAlignment:Left";

                    case HorizontalAlignment.Right:
                        return "HorizontalAlignment:Right";
                }
                return "HorizontalAlignment:Center";
            }
            if ("DisplayStyle".Equals(string_0))
            {
                switch (((ToolStripItemDisplayStyle) object_0))
                {
                    case ToolStripItemDisplayStyle.Image:
                        return "DisplayStyle:Image";

                    case ToolStripItemDisplayStyle.ImageAndText:
                        return "DisplayStyle:ImageAndText";

                    case ToolStripItemDisplayStyle.Text:
                        return "DisplayStyle:Text";
                }
                return "DisplayStyle:None";
            }
            if ("TextImageRelation".Equals(string_0))
            {
                switch (((TextImageRelation) object_0))
                {
                    case TextImageRelation.ImageAboveText:
                        return "TextImageRelation:ImageAboveText";

                    case TextImageRelation.ImageBeforeText:
                        return "TextImageRelation:ImageBeforeText";

                    case TextImageRelation.Overlay:
                        return "TextImageRelation:Overlay";

                    case TextImageRelation.TextAboveImage:
                        return "TextImageRelation:TextAboveImage";

                    case TextImageRelation.TextBeforeImage:
                        return "TextImageRelation:TextBeforeImage";
                }
            }
            else if ("FlatStyle".Equals(string_0))
            {
                switch (((FlatStyle) object_0))
                {
                    case FlatStyle.Flat:
                        return "FlatStyle:Flat";

                    case FlatStyle.Popup:
                        return "FlatStyle:Popup";

                    case FlatStyle.Standard:
                        return "FlatStyle:Standard";

                    case FlatStyle.System:
                        return "FlatStyle:System";
                }
            }
            else
            {
                if ("DataGridViewAutoSizeColumnMode".Equals(string_0))
                {
                    switch (((DataGridViewAutoSizeColumnMode) object_0))
                    {
                        case DataGridViewAutoSizeColumnMode.AllCells:
                            return "DataGridViewAutoSizeColumnMode:AllCells";

                        case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
                            return "DataGridViewAutoSizeColumnMode:AllCellsExceptHeader";

                        case DataGridViewAutoSizeColumnMode.ColumnHeader:
                            return "DataGridViewAutoSizeColumnMode:ColumnHeader";

                        case DataGridViewAutoSizeColumnMode.DisplayedCells:
                            return "DataGridViewAutoSizeColumnMode:DisplayedCells";

                        case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
                            return "DataGridViewAutoSizeColumnMode:DisplayedCellsExceptHeader";

                        case DataGridViewAutoSizeColumnMode.Fill:
                            return "DataGridViewAutoSizeColumnMode:Fill";

                        case DataGridViewAutoSizeColumnMode.NotSet:
                            return "DataGridViewAutoSizeColumnMode:NotSet";
                    }
                    return "DataGridViewAutoSizeColumnMode:None";
                }
                if ("DataGridViewImageCellLayout".Equals(string_0))
                {
                    switch (((DataGridViewImageCellLayout) object_0))
                    {
                        case DataGridViewImageCellLayout.Normal:
                            return "DataGridViewImageCellLayout:Normal";

                        case DataGridViewImageCellLayout.NotSet:
                            return "DataGridViewImageCellLayout:NotSet";

                        case DataGridViewImageCellLayout.Stretch:
                            return "DataGridViewImageCellLayout:Stretch";

                        case DataGridViewImageCellLayout.Zoom:
                            return "DataGridViewImageCellLayout:Zoom";
                    }
                }
                else if ("DataGridViewComboBoxDisplayStyle".Equals(string_0))
                {
                    switch (((DataGridViewComboBoxDisplayStyle) object_0))
                    {
                        case DataGridViewComboBoxDisplayStyle.ComboBox:
                            return "DataGridViewComboBoxDisplayStyle:ComboBox";

                        case DataGridViewComboBoxDisplayStyle.DropDownButton:
                            return "DataGridViewComboBoxDisplayStyle:DropDownButton";

                        case DataGridViewComboBoxDisplayStyle.Nothing:
                            return "DataGridViewComboBoxDisplayStyle:Nothing";
                    }
                }
                else if ("DataGridViewContentAlignment".Equals(string_0))
                {
                    switch (((DataGridViewContentAlignment) object_0))
                    {
                        case DataGridViewContentAlignment.BottomCenter:
                            return (string_0 + ":BottomCenter");

                        case DataGridViewContentAlignment.BottomLeft:
                            return (string_0 + ":BottomLeft");

                        case DataGridViewContentAlignment.BottomRight:
                            return (string_0 + ":BottomRight");

                        case DataGridViewContentAlignment.MiddleCenter:
                            return (string_0 + ":MiddleCenter");

                        case DataGridViewContentAlignment.MiddleLeft:
                            return (string_0 + ":MiddleLeft");

                        case DataGridViewContentAlignment.MiddleRight:
                            return (string_0 + ":MiddleRight");

                        case DataGridViewContentAlignment.NotSet:
                            return (string_0 + ":NotSet");

                        case DataGridViewContentAlignment.TopCenter:
                            return (string_0 + ":TopCenter");

                        case DataGridViewContentAlignment.TopLeft:
                            return (string_0 + ":TopLeft");

                        case DataGridViewContentAlignment.TopRight:
                            return (string_0 + ":TopRight");
                    }
                }
                else if ("DataGridViewTriState".Equals(string_0))
                {
                    switch (((DataGridViewTriState) object_0))
                    {
                        case DataGridViewTriState.False:
                            return (string_0 + ":False");

                        case DataGridViewTriState.NotSet:
                            return (string_0 + ":NotSet");

                        case DataGridViewTriState.True:
                            return (string_0 + ":True");
                    }
                }
                else if ("DataGridViewHeaderBorderStyle".Equals(string_0))
                {
                    switch (((DataGridViewHeaderBorderStyle) object_0))
                    {
                        case DataGridViewHeaderBorderStyle.Custom:
                            return (string_0 + ":Custom");

                        case DataGridViewHeaderBorderStyle.None:
                            return (string_0 + ":None");

                        case DataGridViewHeaderBorderStyle.Raised:
                            return (string_0 + ":Raised");

                        case DataGridViewHeaderBorderStyle.Single:
                            return (string_0 + ":Single");

                        case DataGridViewHeaderBorderStyle.Sunken:
                            return (string_0 + ":Sunken");
                    }
                }
                else if ("DataGridViewCellBorderStyle".Equals(string_0))
                {
                    switch (((DataGridViewCellBorderStyle) object_0))
                    {
                        case DataGridViewCellBorderStyle.Custom:
                            return (string_0 + ":Custom");

                        case DataGridViewCellBorderStyle.None:
                            return (string_0 + ":None");

                        case DataGridViewCellBorderStyle.Raised:
                            return (string_0 + ":Raised");

                        case DataGridViewCellBorderStyle.RaisedHorizontal:
                            return (string_0 + ":RaisedHorizontal");

                        case DataGridViewCellBorderStyle.RaisedVertical:
                            return (string_0 + ":RaisedVertical");

                        case DataGridViewCellBorderStyle.Single:
                            return (string_0 + ":Single");

                        case DataGridViewCellBorderStyle.SingleHorizontal:
                            return (string_0 + ":SingleHorizontal");

                        case DataGridViewCellBorderStyle.SingleVertical:
                            return (string_0 + ":SingleVertical");

                        case DataGridViewCellBorderStyle.Sunken:
                            return (string_0 + ":Sunken");

                        case DataGridViewCellBorderStyle.SunkenHorizontal:
                            return (string_0 + ":SunkenHorizontal");

                        case DataGridViewCellBorderStyle.SunkenVertical:
                            return (string_0 + ":SunkenVertical");
                    }
                }
            }
            return (string_0 + ":" + object_0);
        }

        public static void SaveDataGridViewStyles(string string_0, DataGridView dataGridView_0)
        {
            SaveDataGridViewStyles(string_0, dataGridView_0, string.Empty, string.Empty);
        }

        public static void SaveDataGridViewStyles(string string_0, DataGridView dataGridView_0, string string_1, string string_2)
        {
            FileStream w = File.Create("TextTemp.xml");
            try
            {
                XmlTextWriter writer = new XmlTextWriter(w, Encoding.Unicode) {
                    Formatting = Formatting.Indented
                };
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Controls");
                GenerateDataGridView(writer, dataGridView_0, string_1, string_2, true);
                writer.WriteEndElement();
                writer.Flush();
                w.Flush();
                writer.Close();
                w.Close();
                XmlDocument document = new XmlDocument();
                document.Load("TextTemp.xml");
                if (document.HasChildNodes)
                {
                    foreach (XmlNode node in document.ChildNodes)
                    {
                        if ("Controls".Equals(node.Name) && node.HasChildNodes)
                        {
                            XmlDocument document2 = new XmlDocument();
                            byte[] sourceArray = Convert.FromBase64String("FZoo0+wH8AgXWEjMAFRnOVt+ZImrQik1jiVirx3SQzoTTc8H/D9o32mIm2Fb6CnC");
                            byte[] destinationArray = new byte[0x20];
                            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                            byte[] buffer3 = new byte[0x10];
                            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                            byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("FkC25FGxr7ANG8kSXdMQ1dc1q5h2nMtkTSy90S2NQks6FTRmwMwaGUhrgVdlpMrhTSdJ9l7s5jbUyGMhyCd26w=="), destinationArray, buffer3, null);
                            byte[] buffer5 = new byte[0x20];
                            Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                            byte[] buffer6 = new byte[0x10];
                            Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                            FileStream stream2 = new FileStream(string_0, FileMode.Open);
                            byte[] buffer = new byte[stream2.Length];
                            stream2.Read(buffer, 0, buffer.Length);
                            stream2.Close();
                            byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                            document2.Load(new XmlTextReader(new MemoryStream(buffer8)));
                            XmlNodeList list = document2.SelectNodes("Controls/DataGridViewCollection");
                            if ((list != null) && (list.Count > 0))
                            {
                                foreach (XmlNode node2 in node.ChildNodes)
                                {
                                    if ("DataGridViewItem".Equals(node2.Name))
                                    {
                                        XmlNode node4 = smethod_2(node2.Attributes["id"].Value, list);
                                        if (node4 != null)
                                        {
                                            node4.InnerXml = node2.InnerXml;
                                        }
                                        else
                                        {
                                            XmlNode node3 = document2.SelectSingleNode("Controls/DataGridViewCollection");
                                            XmlElement newChild = document2.CreateElement("DataGridViewItem");
                                            newChild.InnerXml = node2.InnerXml;
                                            node3.AppendChild(newChild);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                XmlElement element2 = document2.CreateElement("DataGridViewCollection");
                                element2.InnerXml = node.InnerXml;
                                foreach (XmlNode node5 in document2.ChildNodes)
                                {
                                    if (node5.Name == "Controls")
                                    {
                                        node5.AppendChild(element2);
                                    }
                                }
                            }
                            document2.Save(string_0);
                            string str2 = "ikAJxQPU3bNUWK0fMgeHxMFk5wjhSQPYnARPgkVEKVU4yjA7KoD3eo7c6tLP745I";
                            FileInfo info = new FileInfo(string_0);
                            smethod_1(new FileInfo[] { info }, str2);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                File.Delete("TextTemp.xml");
            }
        }

        public static void SetDataGridProperty(XmlAttributeCollection xmlAttributeCollection_0, Control control_0)
        {
            foreach (XmlAttribute attribute in xmlAttributeCollection_0)
            {
                string name = attribute.Name;
                if (!"id".Equals(name) && !"control.type".Equals(name))
                {
                    PropertyInfo property = control_0.GetType().GetProperty(name);
                    if (property != null)
                    {
                        string str2 = attribute.Value;
                        int index = str2.IndexOf(":");
                        string str3 = str2.Substring(0, index);
                        string str4 = (str2.Length > (index + 1)) ? str2.Substring(index + 1) : null;
                        if (str4 != null)
                        {
                            object obj2 = GetObjectValue(control_0, str3, str4);
                            if (obj2 != null)
                            {
                                property.SetValue(control_0, obj2, null);
                            }
                        }
                    }
                }
            }
        }

        internal static void smethod_0(string string_0, DataGridView dataGridView_0, string string_1)
        {
            FileStream w = File.Create("TextTemp.xml");
            try
            {
                XmlTextWriter writer = new XmlTextWriter(w, Encoding.Unicode) {
                    Formatting = Formatting.Indented
                };
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Controls");
                GenerateDataGridView(writer, dataGridView_0);
                writer.WriteEndElement();
                writer.Flush();
                w.Flush();
                writer.Close();
                w.Close();
                XmlDocument document = new XmlDocument();
                document.Load("TextTemp.xml");
                if (document.HasChildNodes)
                {
                    foreach (XmlNode node in document.ChildNodes)
                    {
                        if ("Controls".Equals(node.Name) && node.HasChildNodes)
                        {
                            foreach (XmlNode node2 in node.ChildNodes)
                            {
                                if ("DataGridViewItem".Equals(node2.Name))
                                {
                                    XmlElement newChild = (XmlElement) node2;
                                    if (newChild.HasAttribute("id"))
                                    {
                                        newChild.SetAttribute("id", string_1);
                                    }
                                    if (newChild.HasAttribute("control.type"))
                                    {
                                        newChild.SetAttribute("control.type", "Aisino.Framework.Plugin.Core.Controls.CustomStyleDataGrid");
                                    }
                                    XmlDocument document2 = new XmlDocument();
                                    byte[] sourceArray = Convert.FromBase64String("FZoo0+wH8AgXWEjMAFRnOVt+ZImrQik1jiVirx3SQzoTTc8H/D9o32mIm2Fb6CnC");
                                    byte[] destinationArray = new byte[0x20];
                                    Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                                    byte[] buffer3 = new byte[0x10];
                                    Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                                    byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("FkC25FGxr7ANG8kSXdMQ1dc1q5h2nMtkTSy90S2NQks6FTRmwMwaGUhrgVdlpMrhTSdJ9l7s5jbUyGMhyCd26w=="), destinationArray, buffer3, null);
                                    byte[] buffer5 = new byte[0x20];
                                    Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                                    byte[] buffer6 = new byte[0x10];
                                    Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                                    FileStream stream2 = new FileStream(string_0, FileMode.Open);
                                    byte[] buffer = new byte[stream2.Length];
                                    stream2.Read(buffer, 0, buffer.Length);
                                    stream2.Close();
                                    byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                                    document2.Load(new XmlTextReader(new MemoryStream(buffer8)));
                                    XmlNodeList list = document2.SelectNodes("Controls/DataGridViewCollection");
                                    if ((list != null) && (list.Count > 0))
                                    {
                                        foreach (XmlNode node3 in list)
                                        {
                                            bool flag = false;
                                            if (node3.HasChildNodes)
                                            {
                                                foreach (XmlNode node4 in node3)
                                                {
                                                    if (node4.Attributes["id"].Value.Equals(string_1))
                                                    {
                                                        flag = true;
                                                        node4.InnerXml = newChild.InnerXml;
                                                    }
                                                }
                                            }
                                            if (!flag)
                                            {
                                                XmlElement element2 = document2.CreateElement("DataGridViewCollection");
                                                element2.AppendChild(newChild);
                                                document2["Controls"].AppendChild(element2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        XmlElement element3 = document2.CreateElement("DataGridViewCollection");
                                        element3.AppendChild(newChild);
                                        document2["Controls"].AppendChild(element3);
                                    }
                                    document2.Save(string_0);
                                    string str2 = "ikAJxQPU3bNUWK0fMgeHxMFk5wjhSQPYnARPgkVEKVU4yjA7KoD3eo7c6tLP745I";
                                    FileInfo info = new FileInfo(string_0);
                                    smethod_1(new FileInfo[] { info }, str2);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                File.Delete("TextTemp.xml");
            }
        }

        private static void smethod_1(FileInfo[] object_0, string string_0)
        {
            for (int i = 0; i < object_0.Length; i++)
            {
                FileInfo info = (FileInfo) object_0[i];
                byte[] sourceArray = Convert.FromBase64String(string_0);
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                FileStream stream = info.OpenRead();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer5 = AES_Crypt.Encrypt(buffer, destinationArray, buffer3);
                FileStream stream2 = new FileStream(info.FullName, FileMode.Create);
                stream2.Write(buffer5, 0, buffer5.Length);
                stream2.Close();
                Console.WriteLine(info.FullName);
            }
        }

        private static XmlNode smethod_2(string string_0, XmlNodeList xmlNodeList_0)
        {
            XmlNode node = null;
            foreach (XmlNode node2 in xmlNodeList_0)
            {
                if (node2.HasChildNodes)
                {
                    foreach (XmlNode node3 in node2)
                    {
                        if (node3.Attributes["id"].Value.Equals(string_0))
                        {
                            node = node3;
                        }
                    }
                }
            }
            return node;
        }

        public static void WriteAttribute(XmlTextWriter xmlTextWriter_0, string string_0, string string_1)
        {
            xmlTextWriter_0.WriteStartAttribute(string_0, null);
            xmlTextWriter_0.WriteString(string_1);
            xmlTextWriter_0.WriteEndAttribute();
        }

        public static void WriteDataGridViewCellStyle(XmlTextWriter xmlTextWriter_0, DataGridViewCellStyle dataGridViewCellStyle_0, string string_0)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            xmlTextWriter_0.WriteStartElement(string_0);
            WriteAttribute(xmlTextWriter_0, "id", dataGridViewCellStyle_0.GetType().Name);
            WriteAttribute(xmlTextWriter_0, "control.type", dataGridViewCellStyle_0.GetType().FullName);
            if (!style.Alignment.Equals(dataGridViewCellStyle_0.Alignment))
            {
                WriteAttribute(xmlTextWriter_0, "Alignment", GetString("DataGridViewContentAlignment", dataGridViewCellStyle_0.Alignment));
            }
            if (!style.BackColor.Equals(dataGridViewCellStyle_0.BackColor))
            {
                WriteAttribute(xmlTextWriter_0, "BackColor", GetString("RGBColor", dataGridViewCellStyle_0.BackColor));
            }
            WriteAttribute(xmlTextWriter_0, "Font", GetString("Font", dataGridViewCellStyle_0.Font));
            if (!style.ForeColor.Equals(dataGridViewCellStyle_0.ForeColor))
            {
                WriteAttribute(xmlTextWriter_0, "ForeColor", GetString("RGBColor", dataGridViewCellStyle_0.ForeColor));
            }
            if (!style.SelectionBackColor.Equals(dataGridViewCellStyle_0.SelectionBackColor))
            {
                WriteAttribute(xmlTextWriter_0, "SelectionBackColor", GetString("RGBColor", dataGridViewCellStyle_0.SelectionBackColor));
            }
            if (!style.SelectionForeColor.Equals(dataGridViewCellStyle_0.SelectionForeColor))
            {
                WriteAttribute(xmlTextWriter_0, "SelectionForeColor", GetString("RGBColor", dataGridViewCellStyle_0.SelectionForeColor));
            }
            WriteAttribute(xmlTextWriter_0, "WrapMode", GetString("DataGridViewTriState", dataGridViewCellStyle_0.WrapMode));
            xmlTextWriter_0.WriteEndElement();
        }
    }
}

