namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Fwkp.HomePage.AisinoDock.Docks;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class Common
    {
        public static int HeadControl = 2;
        public static int HeadHeight = 300;
        public static int HeadWidth = 400;
        public static int Length = 30;
        public static int Span = 10;
        public static string UserName = string.Empty;

        public static IDock DockFactory(string DockName, PageControl page)
        {
            IDock dock;
            string str = DockName;
            if (str != null)
            {
                if (!(str == "IconDock"))
                {
                    if (str == "InfoDock")
                    {
                        dock = new InfoDock(page);
                        goto Label_0061;
                    }
                    if (str == "JScardDock")
                    {
                        dock = new JScardDock(page);
                        goto Label_0061;
                    }
                    if (str == "TongjiDock")
                    {
                        dock = new TongjiDock(page);
                        goto Label_0061;
                    }
                }
                else
                {
                    dock = new IconDock(page);
                    goto Label_0061;
                }
            }
            dock = null;
        Label_0061:
            if (dock != null)
            {
                dock.Size = new Size(HeadWidth, HeadHeight);
                dock.LoadUser(UserName);
            }
            return dock;
        }

        public static void MenuDockManager(Point pt, PageControl page, EventHandler headler)
        {
            ContextMenuStrip strip = new ContextMenuStrip();
            strip.Items.Clear();
            string pageConfigFile = PageXml.PageConfigFile;
            if (File.Exists(pageConfigFile))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(pageConfigFile);
                    foreach (XmlNode node in document.SelectNodes("/Config/List/Dock"))
                    {
                        XmlElement element = node as XmlElement;
                        if (element != null)
                        {
                            ToolStripMenuItem item = new ToolStripMenuItem {
                                Text = element.GetAttribute("value")
                            };
                            string attribute = element.GetAttribute("name");
                            item.Tag = attribute;
                            item.Checked = page.DockExists(element.GetAttribute("name"));
                            strip.Items.Add(item);
                            item.Click += headler;
                        }
                    }
                }
                catch
                {
                }
                strip.Show(pt);
            }
        }
    }
}

