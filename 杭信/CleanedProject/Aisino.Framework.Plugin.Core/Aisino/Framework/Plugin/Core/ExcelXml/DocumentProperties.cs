namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class DocumentProperties
    {
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;
        [CompilerGenerated]
        private string string_4;
        [CompilerGenerated]
        private string string_5;

        public DocumentProperties()
        {
            
            this.Author = "";
            this.LastAuthor = "";
            this.Manager = "";
            this.Company = "";
            this.Subject = "";
            this.Title = "";
        }

        internal void method_0(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("", "DocumentProperties", "urn:schemas-microsoft-com:office:office");
            if (!this.Author.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("Author", this.Author);
            }
            if (!this.LastAuthor.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("LastAuthor", this.LastAuthor);
            }
            if (!this.Manager.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("Manager", this.Manager);
            }
            if (!this.Company.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("Company", this.Company);
            }
            if (!this.Subject.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("Subject", this.Subject);
            }
            if (!this.Title.IsNullOrEmpty())
            {
                xmlWriter_0.WriteElementString("Title", this.Title);
            }
            xmlWriter_0.WriteEndElement();
        }

        internal void method_1(XmlReader xmlReader_0)
        {
            while (xmlReader_0.Read())
            {
                string str;
                if ((xmlReader_0.Name == "DocumentProperties") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                {
                    break;
                }
                if ((xmlReader_0.NodeType == XmlNodeType.Element) && ((str = xmlReader_0.Name) != null))
                {
                    if (str == "Author")
                    {
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            xmlReader_0.Read();
                            if (xmlReader_0.NodeType == XmlNodeType.Text)
                            {
                                this.Author = xmlReader_0.Value;
                            }
                        }
                    }
                    else if (str == "LastAuthor")
                    {
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            xmlReader_0.Read();
                            if (xmlReader_0.NodeType == XmlNodeType.Text)
                            {
                                this.LastAuthor = xmlReader_0.Value;
                            }
                        }
                    }
                    else if (str == "Manager")
                    {
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            xmlReader_0.Read();
                            if (xmlReader_0.NodeType == XmlNodeType.Text)
                            {
                                this.Manager = xmlReader_0.Value;
                            }
                        }
                    }
                    else if (str == "Company")
                    {
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            xmlReader_0.Read();
                            if (xmlReader_0.NodeType == XmlNodeType.Text)
                            {
                                this.Company = xmlReader_0.Value;
                            }
                        }
                    }
                    else if (!(str == "Subject"))
                    {
                        if ((str == "Title") && !xmlReader_0.IsEmptyElement)
                        {
                            xmlReader_0.Read();
                            if (xmlReader_0.NodeType == XmlNodeType.Text)
                            {
                                this.Title = xmlReader_0.Value;
                            }
                        }
                    }
                    else if (!xmlReader_0.IsEmptyElement)
                    {
                        xmlReader_0.Read();
                        if (xmlReader_0.NodeType == XmlNodeType.Text)
                        {
                            this.Subject = xmlReader_0.Value;
                        }
                    }
                }
            }
        }

        public string Author
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public string Company
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public string LastAuthor
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string Manager
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public string Subject
        {
            [CompilerGenerated]
            get
            {
                return this.string_4;
            }
            [CompilerGenerated]
            set
            {
                this.string_4 = value;
            }
        }

        public string Title
        {
            [CompilerGenerated]
            get
            {
                return this.string_5;
            }
            [CompilerGenerated]
            set
            {
                this.string_5 = value;
            }
        }
    }
}

