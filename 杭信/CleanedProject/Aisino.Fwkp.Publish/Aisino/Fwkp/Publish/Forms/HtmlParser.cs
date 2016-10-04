namespace Aisino.Fwkp.Publish.Forms
{
    using System;
    using System.Text;

    internal class HtmlParser
    {
        private bool _inTag;
        private string[] htmlcode;
        private string[] keepTag;
        private bool needContent = true;
        private StringBuilder result = new StringBuilder();
        private int seek;
        private string[] specialTag = new string[] { "script", "style", "!--" };
        private string tagName;

        public HtmlParser(string html)
        {
            this.htmlcode = new string[html.Length];
            for (int i = 0; i < html.Length; i++)
            {
                this.htmlcode[i] = html[i].ToString();
            }
            this.KeepTag(new string[0]);
        }

        private bool iskeepTag(string tag)
        {
            foreach (string str in this.keepTag)
            {
                if (tag.ToLower() == str.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public void KeepTag(string[] tags)
        {
            this.keepTag = tags;
        }

        private string read()
        {
            return this.htmlcode[this.seek++];
        }

        public string Text()
        {
            int seek = 0;
            int num2 = 0;
            while (this.seek < this.htmlcode.Length)
            {
                string str = this.read();
                if (str.ToLower() == "<")
                {
                    seek = this.seek;
                    this.inTag = true;
                }
                else
                {
                    if (str.ToLower() == ">")
                    {
                        num2 = this.seek;
                        this.inTag = false;
                        if (this.iskeepTag(this.tagName.Replace("/", "")))
                        {
                            for (int i = seek - 1; i < num2; i++)
                            {
                                this.result.Append(this.htmlcode[i].ToString());
                            }
                        }
                        else if (this.tagName.StartsWith("!--"))
                        {
                            bool flag = true;
                            while (flag)
                            {
                                if ((this.read() == "-") && (this.read() == "-"))
                                {
                                    if (this.read() == ">")
                                    {
                                        flag = false;
                                    }
                                    else
                                    {
                                        this.seek--;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (string str2 in this.specialTag)
                            {
                                if (this.tagName == str2)
                                {
                                    this.needContent = false;
                                    break;
                                }
                                this.needContent = true;
                            }
                        }
                        continue;
                    }
                    if (!this.inTag && this.needContent)
                    {
                        this.result.Append(str);
                    }
                }
            }
            return this.result.ToString().Replace("&nbsp;", "");
        }

        public bool inTag
        {
            get
            {
                return this._inTag;
            }
            set
            {
                this._inTag = value;
                if (value)
                {
                    bool flag = true;
                    this.tagName = "";
                    while (flag)
                    {
                        string str = this.read();
                        if ((str != " ") && (str != ">"))
                        {
                            this.tagName = this.tagName + str;
                        }
                        else
                        {
                            if ((str == " ") && (this.tagName.Length > 0))
                            {
                                flag = false;
                                continue;
                            }
                            if (str == ">")
                            {
                                flag = false;
                                this.inTag = false;
                                this.seek--;
                            }
                        }
                    }
                }
            }
        }
    }
}

