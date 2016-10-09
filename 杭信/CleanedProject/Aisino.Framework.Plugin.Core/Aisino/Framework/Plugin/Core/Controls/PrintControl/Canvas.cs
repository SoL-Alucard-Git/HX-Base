namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Reflection;
    using System.Xml;

    public class Canvas
    {
        public bool _Landscape;
        public Margins _Margin;
        public AisinoPrintPanel aisinoPrintPanel_0;
        private Size size_0;
        public PointF startPoint;
        private string string_0;

        public Canvas(string string_1)
        {
            
            this.size_0 = new Size(800, 600);
            this._Margin = new Margins(0, 0, 0, 0);
            this.startPoint = new PointF(0f, 0f);
            this.string_0 = string.Empty;
            if (!File.Exists(string_1))
            {
                throw new FileNotFoundException();
            }
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
            FileStream stream = new FileStream(string_1, FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
            document.Load(new XmlTextReader(new MemoryStream(buffer8)));
            this.string_0 = Path.GetFileName(string_1);
            this.method_1(document);
        }

        internal Canvas(XmlDocument xmlDocument_0, string string_1)
        {
            
            this.size_0 = new Size(800, 600);
            this._Margin = new Margins(0, 0, 0, 0);
            this.startPoint = new PointF(0f, 0f);
            this.string_0 = string.Empty;
            this.string_0 = string_1;
            this.method_1(xmlDocument_0);
        }

        public void AddNode(AisinoPrintControl aisinoPrintControl_0)
        {
            if (this.aisinoPrintPanel_0 != null)
            {
                this.aisinoPrintPanel_0.AddNode(aisinoPrintControl_0);
            }
        }

        private AisinoPrintControl method_0(string string_1, AisinoPrintControl aisinoPrintControl_0)
        {
            foreach (AisinoPrintControl control in aisinoPrintControl_0.Nodes)
            {
                if (control.ID == string_1)
                {
                    return control;
                }
                AisinoPrintControl control3 = this.method_0(string_1, control);
                if (control3 != null)
                {
                    return control3;
                }
            }
            return null;
        }

        private void method_1(XmlDocument xmlDocument_0)
        {
            XmlElement element = xmlDocument_0.SelectSingleNode("/Template") as XmlElement;
            string attribute = element.GetAttribute("Height");
            string str2 = element.GetAttribute("Width");
            string str3 = element.GetAttribute("Landscape");
            string str4 = element.GetAttribute("Location");
            this.aisinoPrintPanel_0 = new AisinoPrintPanel(element);
            this.PageSize = new Size(Common.ToInt(str2), Common.ToInt(attribute));
            this._Landscape = Common.ToBool(str3);
            this.StartPoint = (PointF) Common.ToPoint(str4);
        }

        public void Print(Graphics graphics_0, Dictionary<string, object> dict, bool bool_0)
        {
            this.aisinoPrintPanel_0.Print(this.startPoint, dict, graphics_0, bool_0);
        }

        public XmlDocument ToXml()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("Template");
            element.SetAttribute("Height", this.aisinoPrintPanel_0.Height.ToString());
            element.SetAttribute("Width", this.aisinoPrintPanel_0.Width.ToString());
            if (this._Landscape)
            {
                element.SetAttribute("Landscape", "True");
            }
            element.SetAttribute("Location", this.startPoint.X + "," + this.startPoint.Y);
            document.AppendChild(element);
            foreach (AisinoPrintControl control in this.aisinoPrintPanel_0.Nodes)
            {
                XmlElement element2 = control.ToXmlNode(document);
                element.AppendChild(element2);
            }
            document.AppendChild(element);
            return document;
        }

        public AisinoPrintControl Control
        {
            get
            {
                return this.aisinoPrintPanel_0;
            }
        }

        public AisinoPrintControl this[string string_1]
        {
            get
            {
                return this.method_0(string_1, this.aisinoPrintPanel_0);
            }
        }

        public bool Landscape
        {
            get
            {
                return this._Landscape;
            }
            set
            {
                this._Landscape = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
        }

        public List<AisinoPrintControl> Nodes
        {
            get
            {
                return this.aisinoPrintPanel_0.Nodes;
            }
        }

        public Size PageSize
        {
            get
            {
                return this.size_0;
            }
            set
            {
                this.size_0 = value;
                if (this.aisinoPrintPanel_0 != null)
                {
                    this.aisinoPrintPanel_0.Width = value.Width;
                    this.aisinoPrintPanel_0.Height = value.Height;
                }
            }
        }

        public PointF StartPoint
        {
            get
            {
                return this.startPoint;
            }
            set
            {
                this.startPoint = new PointF((float) this._Margin.Left, (float) this._Margin.Top);
            }
        }
    }
}

