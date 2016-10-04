namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.HomePage.AisinoDock.Docks;
    using Aisino.Fwkp.HomePage.Properties;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class PageXml
    {
        private XmlDocument xmlDoc = new XmlDocument();

        public PageXml()
        {
            if (!File.Exists(PageConfigFile))
            {
                throw new FileNotFoundException();
            }
            this.xmlDoc.Load(PageConfigFile);
        }

        private XmlElement GetUserNode(string name)
        {
            try
            {
                XmlElement element = null;
                foreach (XmlNode node in this.xmlDoc.SelectNodes("/Config/User"))
                {
                    XmlElement element2 = node as XmlElement;
                    if (element2 != null)
                    {
                        if (element2.GetAttribute("Name") == name)
                        {
                            return element2;
                        }
                        if (element2.GetAttribute("Name") == "_common_")
                        {
                            element = element2;
                        }
                    }
                }
                if (element != null)
                {
                    XmlElement newChild = element.Clone() as XmlElement;
                    newChild.SetAttribute("Name", name);
                    this.xmlDoc.SelectSingleNode("Config").AppendChild(newChild);
                    this.xmlDoc.Save(PageConfigFile);
                    return newChild;
                }
                return element;
            }
            catch
            {
                return null;
            }
        }

        public void LoadDock(string name, PageControl control)
        {
            foreach (XmlNode node in this.GetUserNode(name).SelectNodes("DockList/Dock"))
            {
                XmlElement element2 = node as XmlElement;
                if (element2 != null)
                {
                    string attribute = element2.GetAttribute("name");
                    element2.GetAttribute("value");
                    string s = element2.GetAttribute("column");
                    IDock dock = Common.DockFactory(attribute, control);
                    int result = 0;
                    int.TryParse(s, out result);
                    if (result > Common.HeadControl)
                    {
                        result = 0;
                    }
                    dock.ColumnsValue = result;
                }
            }
        }

        public void LoadIcon(string name, IconDock control)
        {
            bool flag = false;
            XmlElement userNode = this.GetUserNode(name);
            if (userNode != null)
            {
                foreach (XmlNode node in userNode.SelectNodes("IconDock/Icon"))
                {
                    XmlElement element2 = node as XmlElement;
                    if (element2 == null)
                    {
                        continue;
                    }
                    string attribute = element2.GetAttribute("style");
                    string str2 = element2.GetAttribute("title");
                    string str3 = element2.GetAttribute("icon");
                    string path = element2.GetAttribute("value");
                    string str5 = element2.GetAttribute("row");
                    string str6 = element2.GetAttribute("column");
                    ButtonControl control2 = new ButtonControl(control) {
                        DockRow = this.ToInt(str5),
                        DockColumn = this.ToInt(str6),
                        IconName = str3
                    };
                    string str7 = attribute;
                    if (str7 != null)
                    {
                        if (!(str7 == "exe"))
                        {
                            if (str7 == "system")
                            {
                                goto Label_015B;
                            }
                            if (str7 == "url")
                            {
                                goto Label_01A6;
                            }
                            if (str7 == "add")
                            {
                                goto Label_01BC;
                            }
                        }
                        else
                        {
                            control2.ClickStyle = ButtonClickStyle.Exe;
                            if (File.Exists(path))
                            {
                                control2.Icon = GetIcon.GetFileIcon(control2.ClickText).ToBitmap();
                            }
                            if (Directory.Exists(path))
                            {
                                control2.Icon = GetIcon.GetDirectoryIcon().ToBitmap();
                            }
                        }
                    }
                    goto Label_01D5;
                Label_015B:
                    control2.ClickStyle = ButtonClickStyle.System;
                    if (str3 != string.Empty)
                    {
                        control2.Icon = ResourceUtil.GetBitmap(str3);
                    }
                    List<string> list2 = UserInfo.get_Gnqx();
                    if ((!UserInfo.get_IsAdmin() && (list2 != null)) && !list2.Contains(path))
                    {
                        control.DeleteButton(control2);
                    }
                    goto Label_01D5;
                Label_01A6:
                    control2.ClickStyle = ButtonClickStyle.URL;
                    control2.Icon = Resources.net;
                    goto Label_01D5;
                Label_01BC:
                    if (!flag)
                    {
                        control2.ClickStyle = ButtonClickStyle.Add;
                        control2.Icon = Resources.addIcon;
                        flag = true;
                    }
                Label_01D5:
                    control2.Title = str2;
                    control2.ClickText = path;
                }
                if (!flag)
                {
                    ButtonControl control3 = new ButtonControl(control) {
                        ClickStyle = ButtonClickStyle.Add,
                        ClickText = "添加",
                        Icon = Resources.addIcon
                    };
                }
            }
        }

        public void SaveDock(string name, List<IDock> Docks)
        {
            XmlNode node = this.GetUserNode(name).SelectSingleNode("DockList");
            node.RemoveAll();
            foreach (IDock dock in Docks)
            {
                XmlElement newChild = this.xmlDoc.CreateElement("Dock");
                newChild.SetAttribute("name", dock.GetType().Name);
                newChild.SetAttribute("value", dock.Title);
                newChild.SetAttribute("column", dock.ColumnsValue.ToString());
                node.AppendChild(newChild);
            }
            this.xmlDoc.Save(PageConfigFile);
        }

        public void SaveIcon(string name, List<ButtonControl> Buttons)
        {
            XmlElement userNode = this.GetUserNode(name);
            if (userNode != null)
            {
                XmlNode node = userNode.SelectSingleNode("IconDock");
                node.RemoveAll();
                foreach (ButtonControl control in Buttons)
                {
                    XmlElement newChild = this.xmlDoc.CreateElement("Icon");
                    newChild.SetAttribute("title", control.Title);
                    newChild.SetAttribute("style", control.ClickStyle.ToString().ToLower());
                    newChild.SetAttribute("value", control.ClickText);
                    newChild.SetAttribute("icon", control.IconName);
                    newChild.SetAttribute("row", control.DockRow.ToString());
                    newChild.SetAttribute("column", control.DockColumn.ToString());
                    node.AppendChild(newChild);
                }
                this.xmlDoc.Save(PageConfigFile);
            }
        }

        private int ToInt(string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }

        public static string PageConfigFile
        {
            get
            {
                string path = Path.Combine(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\航天信息\新版开票\路径", string.Empty, string.Empty).ToString(), @"Config\PlugIn\PageConfig.xml");
                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path) {
                        IsReadOnly = false
                    };
                }
                return path;
            }
        }
    }
}

