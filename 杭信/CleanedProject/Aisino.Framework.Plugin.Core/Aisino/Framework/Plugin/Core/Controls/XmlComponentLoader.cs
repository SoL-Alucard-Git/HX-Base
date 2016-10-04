namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public sealed class XmlComponentLoader : AisinoPNL
    {
        private Color color_0;
        private Color color_1;
        private Hashtable hashtable_0;
        private Hashtable hashtable_1;
        private IContainer icontainer_0;
        private IList ilist_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public XmlComponentLoader()
        {
            
            this.color_0 = Color.Yellow;
            this.color_1 = Color.Red;
            this.ilist_0 = new ArrayList();
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.hashtable_1 = new Hashtable();
            this.method_0();
            this.hashtable_0 = new Hashtable();
            base.ParentChanged += new EventHandler(this.XmlComponentLoader_ParentChanged);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            this.BackColor = Color.Transparent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public object GetControlByName(string string_3)
        {
            try
            {
                return this.hashtable_0[string_3];
            }
            catch
            {
                return null;
            }
        }

        public T GetControlByName<T>(string string_3)
        {
            object obj2 = this.hashtable_0[string_3];
            T local = default(T);
            if (obj2 == null)
            {
                return local;
            }
            try
            {
                return (T) obj2;
            }
            catch
            {
                return default(T);
            }
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
        }

        private bool method_1(string string_3)
        {
            if (string.Empty.Equals(string_3.Trim()))
            {
                return false;
            }
            if (!string_3.EndsWith(".xml"))
            {
                return false;
            }
            if (!File.Exists(string_3))
            {
                return false;
            }
            this.string_1 = string_3.Replace(".xml", "_layout.xml");
            if (!File.Exists(this.string_1))
            {
                return false;
            }
            this.string_2 = string_3.Replace(".xml", "_method.xml");
            return true;
        }

        private Hashtable method_10(string string_3, string string_4)
        {
            if (!File.Exists(string_3))
            {
                return null;
            }
            XmlNode node3 = this.method_9(string_3);
            Hashtable hashtable = new Hashtable();
            if (node3 == null)
            {
                return null;
            }
            XmlNodeList list4 = node3.SelectNodes(string_4);
            if (list4 == null)
            {
                return null;
            }
            ArrayList list = new ArrayList();
            foreach (XmlNode node in list4)
            {
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    list.Add(node2);
                }
            }
            while (list.Count > 0)
            {
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                foreach (XmlNode node4 in list)
                {
                    XmlAttributeCollection attributes = node4.Attributes;
                    hashtable.Add(attributes["id"].Value, attributes);
                    list3.Add(node4);
                    if (node4.HasChildNodes)
                    {
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            list2.Add(node5);
                        }
                    }
                }
                foreach (XmlNode node7 in list3)
                {
                    list.Remove(node7);
                }
                foreach (XmlNode node6 in list2)
                {
                    list.Add(node6);
                }
            }
            return hashtable;
        }

        private Hashtable method_11(string string_3)
        {
            XmlNode node = this.method_9(string_3);
            Hashtable hashtable = new Hashtable();
            if (node == null)
            {
                return null;
            }
            ArrayList list = new ArrayList();
            foreach (XmlNode node3 in node.ChildNodes)
            {
                list.Add(node3);
            }
            while (list.Count > 0)
            {
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                foreach (XmlNode node4 in list)
                {
                    XmlAttributeCollection attributes = node4.Attributes;
                    hashtable.Add(attributes["id"].Value, attributes);
                    list3.Add(node4);
                    if (node4.HasChildNodes)
                    {
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            list2.Add(node5);
                        }
                    }
                }
                foreach (XmlNode node2 in list3)
                {
                    list.Remove(node2);
                }
                foreach (XmlNode node6 in list2)
                {
                    list.Add(node6);
                }
            }
            return hashtable;
        }

        private bool method_12(XmlNode xmlNode_0)
        {
            return "Menu".Equals(xmlNode_0.Name);
        }

        private void method_13(XmlNode xmlNode_0, ToolStripMenuItem toolStripMenuItem_0)
        {
            try
            {
                XmlAttributeCollection attributes = xmlNode_0.Attributes;
                string str = attributes["Text"].Value;
                string str2 = attributes["ShortcutKey"].Value;
                ToolStripMenuItem item = new ToolStripMenuItem {
                    Text = str
                };
                if ("None".Equals(str))
                {
                    item.ShortcutKeys = Keys.None;
                }
                else
                {
                    item.ShortcutKeys = (Keys) Convert.ToInt32(str2);
                }
                if (toolStripMenuItem_0 != null)
                {
                    toolStripMenuItem_0.DropDownItems.Add(item);
                }
                else
                {
                    this.ilist_0.Add(item);
                }
                foreach (XmlNode node in xmlNode_0.ChildNodes)
                {
                    this.method_13(node, item);
                }
            }
            catch
            {
            }
        }

        private bool method_14(XmlNode xmlNode_0, string string_3)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            string str = attributes["id"].Value;
            string str2 = attributes["control.type"].Value;
            return ("System.Windows.Forms.SplitterPanel".Equals(str2) && str.Equals(string_3 + "Panel1"));
        }

        private bool method_15(XmlNode xmlNode_0, string string_3)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            string str = attributes["id"].Value;
            string str2 = attributes["control.type"].Value;
            return ("System.Windows.Forms.SplitterPanel".Equals(str2) && str.Equals(string_3 + "Panel2"));
        }

        private bool method_16(XmlNode xmlNode_0)
        {
            XmlAttributeCollection attributes = xmlNode_0.Attributes;
            string text1 = attributes["id"].Value;
            string str = attributes["control.type"].Value;
            return "Aisino.Framework.Plugin.Core.Controls.AisinoSPL".Equals(str);
        }

        private bool method_17(XmlNode xmlNode_0)
        {
            return "ToolStripCollection".Equals(xmlNode_0.Name);
        }

        private bool method_18(XmlNode xmlNode_0)
        {
            return "StatusStripCollection".Equals(xmlNode_0.Name);
        }

        private bool method_19(XmlNode xmlNode_0)
        {
            return "DataGridViewCollection".Equals(xmlNode_0.Name);
        }

        private void method_2()
        {
            XmlNode node = this.method_9(ToolUtil.CheckPath(this.string_0));
            if (node != null)
            {
                Hashtable hashtable = this.method_11(ToolUtil.CheckPath(this.string_1));
                if (File.Exists(this.string_2))
                {
                    this.hashtable_1 = this.method_10(ToolUtil.CheckPath(this.string_2), "DynamicMethod");
                }
                this.method_4(this, node, hashtable);
            }
        }

        private void method_20(XmlNode xmlNode_0, Control control_0, Hashtable hashtable_2)
        {
            try
            {
                XmlAttributeCollection attributes = xmlNode_0.Attributes;
                string str = attributes["id"].Value;
                string str2 = attributes["control.type"].Value;
                string str3 = "";
                if (attributes["assembly"] != null)
                {
                    str3 = attributes["assembly"].Value;
                }
                if ("System.Windows.Forms.ToolStrip".Equals(str2))
                {
                    Control control = this.method_5(str, str2, str3) as Control;
                    if (control != null)
                    {
                        control.Name = str;
                        this.hashtable_0.Add(str, control);
                        XmlAttributeCollection attributes2 = hashtable_2[str] as XmlAttributeCollection;
                        if (attributes2 != null)
                        {
                            this.method_7(attributes2, control);
                        }
                        if ((attributes2["x"] != null) && (attributes2["y"] != null))
                        {
                            int x = Convert.ToInt32(attributes2["x"].Value);
                            int y = Convert.ToInt32(attributes2["y"].Value);
                            control.Location = new Point(x, y);
                        }
                        if (xmlNode_0.HasChildNodes)
                        {
                            foreach (XmlNode node in xmlNode_0)
                            {
                                ToolStripItem item = this.method_23(node, hashtable_2);
                                if (item != null)
                                {
                                    ((ToolStrip) control).Items.Add(item);
                                }
                            }
                        }
                        control_0.Controls.Add(control);
                    }
                    else
                    {
                        MessageBoxHelper.Show(str + ":未得到实例。\n Assembly:" + str3 + "\n type:" + str2);
                    }
                }
            }
            catch
            {
            }
        }

        private void method_21(XmlNode xmlNode_0, Control control_0, Hashtable hashtable_2)
        {
            try
            {
                XmlAttributeCollection attributes = xmlNode_0.Attributes;
                string str = attributes["id"].Value;
                string str2 = attributes["control.type"].Value;
                string str3 = "";
                if (attributes["assembly"] != null)
                {
                    str3 = attributes["assembly"].Value;
                }
                if ("System.Windows.Forms.StatusStrip".Equals(str2))
                {
                    Control control = this.method_5(str, str2, str3) as Control;
                    if (control != null)
                    {
                        control.Name = str;
                        this.hashtable_0.Add(str, control);
                        XmlAttributeCollection attributes2 = hashtable_2[str] as XmlAttributeCollection;
                        if (attributes2 != null)
                        {
                            this.method_7(attributes2, control);
                        }
                        if ((attributes2["x"] != null) && (attributes2["y"] != null))
                        {
                            int x = Convert.ToInt32(attributes2["x"].Value);
                            int y = Convert.ToInt32(attributes2["y"].Value);
                            control.Location = new Point(x, y);
                        }
                        if (xmlNode_0.HasChildNodes)
                        {
                            foreach (XmlNode node in xmlNode_0)
                            {
                                ToolStripItem item = this.method_23(node, hashtable_2);
                                if (item != null)
                                {
                                    ((StatusStrip) control).Items.Add(item);
                                }
                            }
                        }
                        control_0.Controls.Add(control);
                    }
                    else
                    {
                        MessageBoxHelper.Show(str + ":未得到实例。\n Assembly:" + str3 + "\n type:" + str2);
                    }
                }
            }
            catch
            {
            }
        }

        private void method_22(XmlNode xmlNode_0, Control control_0)
        {
            try
            {
                if ("DataGridViewItem".Equals(xmlNode_0.Name))
                {
                    XmlAttributeCollection attributes = xmlNode_0.Attributes;
                    string str = attributes["id"].Value;
                    string str2 = attributes["control.type"].Value;
                    if ("Aisino.Framework.Plugin.Core.Controls.AisinoDataGrid".Equals(str2))
                    {
                        AisinoDataGrid controlByName = this.GetControlByName<AisinoDataGrid>(str);
                        if (controlByName != null)
                        {
                            controlByName.LoadGridStyles(xmlNode_0);
                        }
                    }
                    else
                    {
                        DataGridView view = this.GetControlByName<DataGridView>(str);
                        if (view != null)
                        {
                            XmlComponentUtil.SetDataGridProperty(attributes, view);
                            if (xmlNode_0.HasChildNodes)
                            {
                                foreach (XmlNode node in xmlNode_0)
                                {
                                    if ("DataGridViewColumnCollection".Equals(node.Name))
                                    {
                                        if (node.HasChildNodes)
                                        {
                                            foreach (XmlNode node2 in node)
                                            {
                                                if ("DataGridViewColumnItem".Equals(node2.Name))
                                                {
                                                    DataGridViewColumn dataGridViewColumnItem = XmlComponentUtil.GetDataGridViewColumnItem(node2, view);
                                                    if (dataGridViewColumnItem.ReadOnly)
                                                    {
                                                        dataGridViewColumnItem.DefaultCellStyle.ForeColor = this.color_1;
                                                        dataGridViewColumnItem.DefaultCellStyle.BackColor = this.color_0;
                                                    }
                                                    if (dataGridViewColumnItem != null)
                                                    {
                                                        view.Columns.Add(dataGridViewColumnItem);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DataGridViewCellStyle dataGridCellStyle = XmlComponentUtil.GetDataGridCellStyle(node, view);
                                        if (dataGridCellStyle != null)
                                        {
                                            PropertyInfo property = view.GetType().GetProperty(node.Name);
                                            if (property != null)
                                            {
                                                property.SetValue(view, dataGridCellStyle, null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private ToolStripItem method_23(XmlNode xmlNode_0, Hashtable hashtable_2)
        {
            ToolStripItem item;
            try
            {
                XmlAttributeCollection attributes = xmlNode_0.Attributes;
                string str = attributes["id"].Value;
                string str2 = attributes["control.type"].Value;
                string str3 = "";
                if (attributes["assembly"] != null)
                {
                    str3 = attributes["assembly"].Value;
                }
                item = this.method_6(str, str2, str3);
                if (item != null)
                {
                    item.Name = str;
                    this.hashtable_0.Add(str, item);
                    XmlAttributeCollection attributes2 = hashtable_2[str] as XmlAttributeCollection;
                    if (attributes2 != null)
                    {
                        this.method_24(attributes2, item);
                    }
                    if (xmlNode_0.HasChildNodes)
                    {
                        foreach (XmlNode node in xmlNode_0)
                        {
                            ToolStripItem item2 = this.method_23(node, hashtable_2);
                            if (item2 != null)
                            {
                                if (item is ToolStripDropDownButton)
                                {
                                    ((ToolStripDropDownButton) item).DropDownItems.Add(item2);
                                }
                                else if (item is ToolStripMenuItem)
                                {
                                    ((ToolStripMenuItem) item).DropDownItems.Add(item2);
                                }
                                else if (item is ToolStripSplitButton)
                                {
                                    ((ToolStripSplitButton) item).DropDownItems.Add(item2);
                                }
                            }
                        }
                    }
                    return item;
                }
                MessageBoxHelper.Show(str + ":未得到实例。\n Assembly:" + str3 + "\n type:" + str2);
            }
            catch
            {
                item = null;
            }
            return item;
        }

        private void method_24(XmlAttributeCollection xmlAttributeCollection_0, ToolStripItem toolStripItem_0)
        {
            foreach (XmlAttribute attribute in xmlAttributeCollection_0)
            {
                string name = attribute.Name;
                if (!"Items".Equals(name))
                {
                    PropertyInfo property = toolStripItem_0.GetType().GetProperty(name);
                    if (property != null)
                    {
                        string str2 = attribute.Value;
                        int index = str2.IndexOf(":");
                        string str4 = str2.Substring(0, index);
                        string str3 = (str2.Length > (index + 1)) ? str2.Substring(index + 1) : null;
                        if (str3 != null)
                        {
                            object obj2 = this.method_26(toolStripItem_0, str4, str3);
                            if (obj2 != null)
                            {
                                property.SetValue(toolStripItem_0, obj2, null);
                            }
                        }
                    }
                    else
                    {
                        this.method_25(toolStripItem_0, attribute);
                    }
                }
                else
                {
                    string str5 = attribute.Value;
                    if (toolStripItem_0 is ToolStripComboBox)
                    {
                        string[] items = str5.Split(new char[] { ',' });
                        ((ToolStripComboBox) toolStripItem_0).Items.AddRange(items);
                    }
                }
            }
        }

        private void method_25(ToolStripItem toolStripItem_0, XmlAttribute xmlAttribute_0)
        {
            if (toolStripItem_0.Tag == null)
            {
                toolStripItem_0.Tag = new Hashtable();
                ((Hashtable) toolStripItem_0.Tag).Add(xmlAttribute_0.Name, xmlAttribute_0.Value);
            }
        }

        private object method_26(ToolStripItem toolStripItem_0, string string_3, string string_4)
        {
            if ("Int32".Equals(string_3))
            {
                return Convert.ToInt32(string_4);
            }
            if ("Int64".Equals(string_3))
            {
                return Convert.ToInt64(string_4);
            }
            if ("Color".Equals(string_3))
            {
                return Color.FromName(string_4);
            }
            if ("RGBColor".Equals(string_3))
            {
                string[] strArray4 = string_4.Split(",".ToCharArray());
                int red = Convert.ToInt32(strArray4[0]);
                int green = Convert.ToInt32(strArray4[1]);
                int blue = Convert.ToInt32(strArray4[2]);
                return Color.FromArgb(red, green, blue);
            }
            if (!"Font".Equals(string_3))
            {
                if ("Point".Equals(string_3))
                {
                    int index = string_4.IndexOf(",");
                    int width = Convert.ToInt32(string_4.Substring(0, index));
                    return new Size(width, Convert.ToInt32(string_4.Substring(index + 1)));
                }
                if ("LocationPoint".Equals(string_3))
                {
                    int length = string_4.IndexOf(",");
                    int x = Convert.ToInt32(string_4.Substring(0, length));
                    return new Point(x, Convert.ToInt32(string_4.Substring(length + 1)));
                }
                if ("Bool".Equals(string_3))
                {
                    return Convert.ToBoolean(string_4);
                }
                if ("IList".Equals(string_3))
                {
                    int num5 = string_4.IndexOf("{");
                    int num6 = string_4.IndexOf("}");
                    string[] strArray2 = string_4.Substring(num5 + 1, (num6 - num5) - 1).Split(",".ToCharArray());
                    IList list = new ArrayList();
                    foreach (string str7 in strArray2)
                    {
                        list.Add(str7);
                    }
                    return list;
                }
                if ("BorderStyle".Equals(string_3))
                {
                    if ("FixedSingle".Equals(string_4))
                    {
                        return BorderStyle.FixedSingle;
                    }
                    if ("Fixed3D".Equals(string_4))
                    {
                        return BorderStyle.Fixed3D;
                    }
                    return BorderStyle.None;
                }
                if ("DisplayStyle".Equals(string_3))
                {
                    if ("Image".Equals(string_4))
                    {
                        return ToolStripItemDisplayStyle.Image;
                    }
                    if ("ImageAndText".Equals(string_4))
                    {
                        return ToolStripItemDisplayStyle.ImageAndText;
                    }
                    if ("Text".Equals(string_4))
                    {
                        return ToolStripItemDisplayStyle.Text;
                    }
                    return BorderStyle.None;
                }
                if ("TextImageRelation".Equals(string_3))
                {
                    if ("ImageAboveText".Equals(string_4))
                    {
                        return TextImageRelation.ImageAboveText;
                    }
                    if ("ImageBeforeText".Equals(string_4))
                    {
                        return TextImageRelation.ImageBeforeText;
                    }
                    if ("Overlay".Equals(string_4))
                    {
                        return TextImageRelation.Overlay;
                    }
                    if ("TextAboveImage".Equals(string_4))
                    {
                        return TextImageRelation.TextAboveImage;
                    }
                    if ("TextBeforeImage".Equals(string_4))
                    {
                        return TextImageRelation.TextBeforeImage;
                    }
                }
                if ("Dock".Equals(string_3))
                {
                    if ("Fill".Equals(string_4))
                    {
                        return DockStyle.Fill;
                    }
                    if ("Bottom".Equals(string_4))
                    {
                        return DockStyle.Bottom;
                    }
                    if ("Top".Equals(string_4))
                    {
                        return DockStyle.Top;
                    }
                    if ("Left".Equals(string_4))
                    {
                        return DockStyle.Left;
                    }
                    if ("Right".Equals(string_4))
                    {
                        return DockStyle.Right;
                    }
                    if ("None".Equals(string_4))
                    {
                        return DockStyle.None;
                    }
                }
                if ("Anchor".Equals(string_3))
                {
                    string[] strArray5 = string_4.Split(",".ToCharArray());
                    AnchorStyles none = AnchorStyles.None;
                    bool flag = true;
                    foreach (string str in strArray5)
                    {
                        AnchorStyles top = AnchorStyles.None;
                        if ("Top".Equals(str))
                        {
                            top = AnchorStyles.Top;
                        }
                        else if ("Bottom".Equals(str))
                        {
                            top = AnchorStyles.Bottom;
                        }
                        else if ("Left".Equals(str))
                        {
                            top = AnchorStyles.Left;
                        }
                        else if ("Right".Equals(str))
                        {
                            top = AnchorStyles.Right;
                        }
                        else if ("None".Equals(str))
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
                if ("Image".Equals(string_3))
                {
                    return Image.FromStream(new MemoryStream(Convert.FromBase64String(string_4.ToString())));
                }
                if ("char".Equals(string_3))
                {
                    return Convert.ToChar(string_4);
                }
                if (!"DialogResult".Equals(string_3))
                {
                    return string_4;
                }
                string str6 = string_4.ToString();
                if ("OK".Equals(str6))
                {
                    return DialogResult.OK;
                }
                if ("Cancel".Equals(str6))
                {
                    return DialogResult.Cancel;
                }
                return DialogResult.None;
            }
            string[] strArray6 = string_4.Split(new char[] { ',' });
            string familyName = strArray6[0];
            double num8 = Convert.ToDouble(strArray6[1]);
            string str2 = strArray6[2];
            if ((str2 == null) || !(str2 != ""))
            {
                return new Font(familyName, (float) num8);
            }
            FontStyle regular = FontStyle.Regular;
            string str3 = str2;
            if (str3 != null)
            {
                if (str3 == "Bold")
                {
                    regular = FontStyle.Bold;
                }
                else if (str3 == "Italic")
                {
                    regular = FontStyle.Italic;
                }
                else if (str3 == "Regular")
                {
                    regular = FontStyle.Regular;
                }
                else if (!(str3 == "Strikeout"))
                {
                    if (!(str3 == "Underline"))
                    {
                        goto Label_0159;
                    }
                    regular = FontStyle.Underline;
                }
                else
                {
                    regular = FontStyle.Strikeout;
                }
                goto Label_015C;
            }
        Label_0159:
            regular = FontStyle.Regular;
        Label_015C:
            return new Font(familyName, (float) num8, regular);
        }

        private void method_27(Control control_0)
        {
            if (control_0 != null)
            {
                control_0.Click += new EventHandler(this.method_68);
                control_0.DoubleClick += new EventHandler(this.method_67);
                control_0.Move += new EventHandler(this.method_66);
                control_0.SizeChanged += new EventHandler(this.method_51);
                control_0.TabIndexChanged += new EventHandler(this.method_50);
                control_0.VisibleChanged += new EventHandler(this.method_49);
                control_0.TabStopChanged += new EventHandler(this.method_48);
                control_0.Resize += new EventHandler(this.method_47);
                control_0.Paint += new PaintEventHandler(this.method_45);
                control_0.PreviewKeyDown += new PreviewKeyDownEventHandler(this.method_46);
                control_0.MouseClick += new MouseEventHandler(this.method_65);
                control_0.MouseDoubleClick += new MouseEventHandler(this.method_64);
                control_0.MouseDown += new MouseEventHandler(this.method_63);
                control_0.MouseEnter += new EventHandler(this.method_62);
                control_0.MouseHover += new EventHandler(this.method_61);
                control_0.MouseLeave += new EventHandler(this.method_60);
                control_0.MouseMove += new MouseEventHandler(this.method_59);
                control_0.MouseUp += new MouseEventHandler(this.method_58);
                control_0.MouseWheel += new MouseEventHandler(this.method_57);
                control_0.KeyDown += new KeyEventHandler(this.method_56);
                control_0.KeyPress += new KeyPressEventHandler(this.method_55);
                control_0.KeyUp += new KeyEventHandler(this.method_54);
                control_0.TextChanged += new EventHandler(this.method_42);
                control_0.LostFocus += new EventHandler(this.method_44);
                control_0.GotFocus += new EventHandler(this.method_43);
                control_0.Leave += new EventHandler(this.method_53);
                control_0.Enter += new EventHandler(this.method_52);
                control_0.BackColorChanged += new EventHandler(this.method_41);
                control_0.ForeColorChanged += new EventHandler(this.method_40);
                control_0.FontChanged += new EventHandler(this.method_39);
                control_0.HelpRequested += new HelpEventHandler(this.method_36);
                if (control_0 is ComboBox)
                {
                    ((ComboBox) control_0).SelectedIndexChanged += new EventHandler(this.method_38);
                    ((ComboBox) control_0).SelectedValueChanged += new EventHandler(this.method_37);
                }
                else if (control_0 is AisinoDataGrid)
                {
                    ((AisinoDataGrid) control_0).GoToPageEvent += new EventHandler<GoToPageEventArgs>(this.method_34);
                    ((AisinoDataGrid) control_0).DataGridRowClickEvent += new EventHandler<DataGridRowEventArgs>(this.method_33);
                    ((AisinoDataGrid) control_0).DataGridRowDbClickEvent += new EventHandler<DataGridRowEventArgs>(this.method_32);
                    ((AisinoDataGrid) control_0).DataGridCellEndEditEvent += new EventHandler<DataGridRowEventArgs>(this.method_31);
                }
            }
        }

        private XmlAttributeCollection method_28(string string_3)
        {
            if (this.hashtable_1 != null)
            {
                return (this.hashtable_1[string_3] as XmlAttributeCollection);
            }
            return null;
        }

        private void method_29(object object_0, EventArgs eventArgs_0, string string_3, string string_4, string string_5, string string_6)
        {
            object obj2 = Assembly.LoadFile(new FileInfo(string_3).FullName).CreateInstance(string_6);
            obj2.GetType().GetMethod(string_5).Invoke(obj2, new object[] { this, object_0, eventArgs_0 });
        }

        private void method_3()
        {
            if (!string.Empty.Equals(this.string_2) && File.Exists(this.string_2))
            {
                Hashtable hashtable = this.method_10(this.string_2, "BindMethod");
                if (hashtable != null)
                {
                    foreach (object obj2 in hashtable.Keys)
                    {
                        string str = obj2.ToString();
                        XmlAttributeCollection attributes = hashtable[obj2] as XmlAttributeCollection;
                        if ((str != null) && (str != ""))
                        {
                            object controlByName = this.GetControlByName(str);
                            if (controlByName != null)
                            {
                                foreach (string str4 in attributes["Method"].Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] strArray3 = str4.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (strArray3 != null)
                                    {
                                        string name = strArray3[0];
                                        string str5 = strArray3[1];
                                        MethodInfo method = base.Parent.GetType().GetMethod(str5);
                                        if (method != null)
                                        {
                                            EventInfo info = controlByName.GetType().GetEvent(name);
                                            Delegate handler = Delegate.CreateDelegate(info.EventHandlerType, base.Parent, method);
                                            info.AddEventHandler(controlByName, handler);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private string method_30(object object_0)
        {
            if (object_0 is Control)
            {
                return ((Control) object_0).Name;
            }
            return string.Empty;
        }

        private void method_31(object sender, DataGridRowEventArgs e)
        {
            string str = "DataGridCellEndEditEvent";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_32(object sender, DataGridRowEventArgs e)
        {
            string str = "DataGridRowDbClickEvent";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_33(object sender, DataGridRowEventArgs e)
        {
            string str = "DataGridRowClickEvent";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_34(object sender, GoToPageEventArgs e)
        {
            string str = "GoToPageEvent";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_35(object sender, EventArgs e)
        {
            string str = "AisinoDataChange";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_36(object sender, HelpEventArgs e)
        {
            string str = "HelpRequested";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_37(object sender, EventArgs e)
        {
            string str = "SelectedValueChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_38(object sender, EventArgs e)
        {
            string str = "SelectedIndexChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_39(object sender, EventArgs e)
        {
            string str = "FontChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_4(Control control_0, XmlNode xmlNode_0, Hashtable hashtable_2)
        {
            control_0.SuspendLayout();
            foreach (XmlNode node in xmlNode_0)
            {
                if (!this.method_12(node))
                {
                    if (this.method_17(node))
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            this.method_20(node2, control_0, hashtable_2);
                        }
                    }
                    else if (this.method_18(node))
                    {
                        foreach (XmlNode node3 in node.ChildNodes)
                        {
                            this.method_21(node3, control_0, hashtable_2);
                        }
                    }
                    else if (this.method_19(node))
                    {
                        foreach (XmlNode node4 in node.ChildNodes)
                        {
                            this.method_22(node4, control_0);
                        }
                    }
                    else
                    {
                        XmlAttributeCollection attributes = node.Attributes;
                        string str = attributes["id"].Value;
                        string str2 = attributes["control.type"].Value;
                        string str3 = "";
                        if (attributes["assembly"] != null)
                        {
                            str3 = attributes["assembly"].Value;
                        }
                        if (str == "topPanel")
                        {
                            if (base.GetType().GetProperty("Size") != null)
                            {
                                XmlAttributeCollection attributes2 = hashtable_2[str] as XmlAttributeCollection;
                                if (attributes2 != null)
                                {
                                    if (base.Parent != null)
                                    {
                                        this.method_7(attributes2, base.Parent);
                                    }
                                    this.method_7(attributes2, this);
                                }
                                if ((attributes2["x"] != null) && (attributes2["y"] != null))
                                {
                                    int x = Convert.ToInt32(attributes2["x"].Value);
                                    int y = Convert.ToInt32(attributes2["y"].Value);
                                    if (base.Parent != null)
                                    {
                                        base.Parent.Location = new Point(x, y);
                                    }
                                }
                            }
                            this.method_27(this);
                            if (node.HasChildNodes)
                            {
                                this.method_4(this, node, hashtable_2);
                            }
                        }
                        else
                        {
                            Control control = this.method_5(str, str2, str3) as Control;
                            if (control != null)
                            {
                                control.Name = str;
                                this.hashtable_0.Add(str, control);
                                XmlAttributeCollection attributes3 = hashtable_2[str] as XmlAttributeCollection;
                                if (control_0 is TableLayoutPanel)
                                {
                                    XmlAttribute attribute = attributes3["TableLayoutPanelRow"];
                                    XmlAttribute attribute2 = attributes3["TableLayoutPanelColumn"];
                                    XmlAttribute attribute3 = attributes3["TableLayoutPanelRowSpan"];
                                    XmlAttribute attribute4 = attributes3["TableLayoutPanelColumnSpan"];
                                    if ((attribute != null) && (attribute2 != null))
                                    {
                                        int row = Convert.ToInt32(attribute.Value);
                                        int column = Convert.ToInt32(attribute2.Value);
                                        ((TableLayoutPanel) control_0).Controls.Add(control, column, row);
                                        if (attribute3 != null)
                                        {
                                            int num5 = Convert.ToInt32(attribute3.Value);
                                            ((TableLayoutPanel) control_0).SetRowSpan(control, num5);
                                        }
                                        if (attribute4 != null)
                                        {
                                            int num6 = Convert.ToInt32(attribute4.Value);
                                            ((TableLayoutPanel) control_0).SetColumnSpan(control, num6);
                                        }
                                    }
                                    else
                                    {
                                        control_0.Controls.Add(control);
                                    }
                                }
                                else
                                {
                                    control_0.Controls.Add(control);
                                }
                                if (attributes3 != null)
                                {
                                    this.method_7(attributes3, control);
                                }
                                if ((attributes3["x"] != null) && (attributes3["y"] != null))
                                {
                                    int num7 = Convert.ToInt32(attributes3["x"].Value);
                                    int num8 = Convert.ToInt32(attributes3["y"].Value);
                                    control.Location = new Point(num7, num8);
                                }
                                control.ImeMode = ImeMode.Hangul;
                                this.method_27(control);
                            }
                            else
                            {
                                MessageBoxHelper.Show(str + ":未得到实例。\n Assembly:" + str3 + "\n type:" + str2);
                            }
                            if (node.HasChildNodes)
                            {
                                if (this.method_16(node))
                                {
                                    foreach (XmlNode node5 in node.ChildNodes)
                                    {
                                        if (this.method_14(node5, ((AisinoSPL) control).Name))
                                        {
                                            XmlAttributeCollection attributes5 = hashtable_2[((AisinoSPL) control).Name + "Panel1"] as XmlAttributeCollection;
                                            this.method_7(attributes5, ((AisinoSPL) control).Panel1);
                                            this.method_4(((AisinoSPL) control).Panel1, node5, hashtable_2);
                                        }
                                        else if (this.method_15(node5, ((AisinoSPL) control).Name))
                                        {
                                            XmlAttributeCollection attributes4 = hashtable_2[((AisinoSPL) control).Name + "Panel2"] as XmlAttributeCollection;
                                            this.method_7(attributes4, ((AisinoSPL) control).Panel2);
                                            this.method_4(((AisinoSPL) control).Panel2, node5, hashtable_2);
                                        }
                                    }
                                }
                                else
                                {
                                    this.method_4(control, node, hashtable_2);
                                }
                            }
                        }
                    }
                }
            }
            control_0.ResumeLayout(false);
        }

        private void method_40(object sender, EventArgs e)
        {
            string str = "ForeColorChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_41(object sender, EventArgs e)
        {
            string str = "BackColorChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_42(object sender, EventArgs e)
        {
            string str = "TextChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_43(object sender, EventArgs e)
        {
            string str = "GotFocus";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_44(object sender, EventArgs e)
        {
            string str = "LostFocus";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_45(object sender, PaintEventArgs e)
        {
            string str = "Paint";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_46(object sender, PreviewKeyDownEventArgs e)
        {
            string str = "PreviewKeyDown";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_47(object sender, EventArgs e)
        {
            string str = "Resize";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_48(object sender, EventArgs e)
        {
            string str = "TabStopChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_49(object sender, EventArgs e)
        {
            string str = "VisibleChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private object method_5(string string_3, string string_4, string string_5)
        {
            if ((string_5 != null) && !(string_5 == ""))
            {
                return base.GetType().Assembly.CreateInstance(string_4);
            }
            System.Type type = typeof(Form);
            return type.Assembly.CreateInstance(string_4);
        }

        private void method_50(object sender, EventArgs e)
        {
            string str = "TabIndexChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_51(object sender, EventArgs e)
        {
            string str = "SizeChanged";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_52(object sender, EventArgs e)
        {
            string str = "Enter";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_53(object sender, EventArgs e)
        {
            string str = "Leave";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_54(object sender, KeyEventArgs e)
        {
            string str = "KeyUp";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_55(object sender, KeyPressEventArgs e)
        {
            string str = "KeyPress";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_56(object sender, KeyEventArgs e)
        {
            string str = "KeyDown";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_57(object sender, MouseEventArgs e)
        {
            string str = "MouseWheel";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_58(object sender, MouseEventArgs e)
        {
            string str = "MouseUp";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_59(object sender, MouseEventArgs e)
        {
            string str = "MouseMove";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private ToolStripItem method_6(string string_3, string string_4, string string_5)
        {
            if ((string_5 != null) && !(string_5 == ""))
            {
                return (base.GetType().Assembly.CreateInstance(string_4) as ToolStripItem);
            }
            System.Type type = typeof(Form);
            return (type.Assembly.CreateInstance(string_4) as ToolStripItem);
        }

        private void method_60(object sender, EventArgs e)
        {
            string str = "MouseLeave";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_61(object sender, EventArgs e)
        {
            string str = "MouseHover";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_62(object sender, EventArgs e)
        {
            string str = "MouseEnter";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_63(object sender, MouseEventArgs e)
        {
            string str = "MouseDown";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_64(object sender, MouseEventArgs e)
        {
            string str = "MouseDoubleClick";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_65(object sender, MouseEventArgs e)
        {
            string str = "MouseClick";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_66(object sender, EventArgs e)
        {
            string str = "Move";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_67(object sender, EventArgs e)
        {
            string str = "DoubleClick";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_68(object sender, EventArgs e)
        {
            string str = "Click";
            XmlAttributeCollection attributes = this.method_28(this.method_30(sender) + "_" + str);
            if (attributes != null)
            {
                string str3 = attributes["Assembly"].Value;
                string str4 = attributes["EventName"].Value;
                string str5 = attributes["MethodName"].Value;
                string str6 = attributes["Domain"].Value;
                if (str.Equals(str4))
                {
                    this.method_29(sender, e, str3, str4, str5, str6);
                }
            }
        }

        private void method_7(XmlAttributeCollection xmlAttributeCollection_0, Control control_0)
        {
            foreach (XmlAttribute attribute in xmlAttributeCollection_0)
            {
                string name = attribute.Name;
                if (!"RowStyleItems".Equals(name) && !"ColumnStyleItems".Equals(name))
                {
                    if (!"TableLayoutPanelRow".Equals(name) && !"TableLayoutPanelColumn".Equals(name))
                    {
                        PropertyInfo property = control_0.GetType().GetProperty(name);
                        if (property != null)
                        {
                            string str8 = attribute.Value;
                            int index = str8.IndexOf(":");
                            string str9 = str8.Substring(0, index);
                            string str11 = (str8.Length > (index + 1)) ? str8.Substring(index + 1) : null;
                            if (str11 != null)
                            {
                                object obj4 = XmlComponentUtil.GetObjectValue(control_0, str9, str11);
                                if (obj4 != null)
                                {
                                    property.SetValue(control_0, obj4, null);
                                }
                            }
                        }
                        else
                        {
                            this.method_8(control_0, attribute);
                        }
                    }
                }
                else
                {
                    string str2 = attribute.Value;
                    if (control_0 is TableLayoutPanel)
                    {
                        if ("RowStyleItems".Equals(name))
                        {
                            foreach (string str5 in str2.Split(new char[] { ';' }))
                            {
                                int length = str5.IndexOf(",");
                                string str6 = str5.Substring(0, length);
                                string str7 = (str5.Length > (length + 1)) ? str5.Substring(length + 1) : null;
                                if ((str6 != null) && (str7 != null))
                                {
                                    object obj2 = XmlComponentUtil.GetObjectValue(control_0, "SizeType", str6);
                                    object obj3 = XmlComponentUtil.GetObjectValue(control_0, "TableLayoutPanelSizeTypeValue", str7);
                                    ((TableLayoutPanel) control_0).RowStyles.Add(new RowStyle((SizeType) obj2, Convert.ToSingle(obj3)));
                                }
                            }
                        }
                        else if ("ColumnStyleItems".Equals(name))
                        {
                            foreach (string str3 in str2.Split(new char[] { ';' }))
                            {
                                int num2 = str3.IndexOf(",");
                                string str4 = str3.Substring(0, num2);
                                string str10 = (str3.Length > (num2 + 1)) ? str3.Substring(num2 + 1) : null;
                                if ((str4 != null) && (str10 != null))
                                {
                                    object obj5 = XmlComponentUtil.GetObjectValue(control_0, "SizeType", str4);
                                    object obj6 = XmlComponentUtil.GetObjectValue(control_0, "TableLayoutPanelSizeTypeValue", str10);
                                    ((TableLayoutPanel) control_0).ColumnStyles.Add(new ColumnStyle((SizeType) obj5, Convert.ToSingle(obj6)));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_8(Control control_0, XmlAttribute xmlAttribute_0)
        {
            if (control_0.Tag == null)
            {
                control_0.Tag = new Hashtable();
                ((Hashtable) control_0.Tag).Add(xmlAttribute_0.Name, xmlAttribute_0.Value);
            }
        }

        private XmlNode method_9(string string_3)
        {
            try
            {
                XmlDocument document = new XmlDocument();
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
                FileStream stream = new FileStream(string_3, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                if (!document.HasChildNodes)
                {
                    return document;
                }
                if (document.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    if (document.ChildNodes.Count <= 1)
                    {
                        return document;
                    }
                    return document.ChildNodes[1];
                }
                return document.ChildNodes[0];
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return null;
        }

        private void XmlComponentLoader_ParentChanged(object sender, EventArgs e)
        {
            this.method_3();
        }

        public string XMLPath
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (!value.Equals(this.string_0))
                {
                    this.string_0 = value;
                    this.hashtable_0.Clear();
                    base.Controls.Clear();
                    if (this.hashtable_1 != null)
                    {
                        this.hashtable_1.Clear();
                    }
                    if (string.Empty.Equals(this.string_0))
                    {
                        this.string_1 = string.Empty;
                        this.string_2 = string.Empty;
                    }
                    else if (this.method_1(ToolUtil.CheckPath(this.string_0)))
                    {
                        this.method_2();
                    }
                }
            }
        }
    }
}

