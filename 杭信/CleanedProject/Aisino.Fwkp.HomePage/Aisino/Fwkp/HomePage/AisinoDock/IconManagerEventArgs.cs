namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HomePage.Properties;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class IconManagerEventArgs
    {
        public IconManagerEventArgs(string value)
        {
            this.Style = IconType.None;
            this.Value = value;
            if (Directory.Exists(value))
            {
                this.Style = IconType.Directory;
                string str = value;
                int startIndex = str.LastIndexOf('\\') + 1;
                if (startIndex >= str.Length)
                {
                    startIndex = 0;
                }
                this.Name = value.Substring(startIndex);
                this.Image = GetIcon.GetDirectoryIcon().ToBitmap();
            }
            if (System.IO.File.Exists(value))
            {
                this.Style = IconType.File;
                this.Name = Path.GetFileNameWithoutExtension(value);
                this.Image = GetIcon.GetFileIcon(value).ToBitmap();
            }
            if (Regex.IsMatch(value, @"^(https?://)?([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"))
            {
                if (!Regex.IsMatch(value, "^http://"))
                {
                    value = "http://" + value;
                }
                this.Value = value;
                this.Style = IconType.Url;
                this.SetUrl(value);
            }
        }

        private void SetUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;
                if (response.CharacterSet.ToLower() == "gb2312")
                {
                    encoding = ToolUtil.GetEncoding();
                }
                Match match = Regex.Match(new StreamReader(responseStream, encoding).ReadToEnd(), @"<title>[\w|\W]+?</title>");
                this.Name = match.Value.Substring(7, match.Value.Length - 15);
                this.Image = Resources.net;
            }
            else
            {
                this.Name = request.Headers[HttpRequestHeader.Host];
                this.Image = null;
            }
        }

        public System.Drawing.Image Image { get; set; }

        public string Name { get; set; }

        public IconType Style { get; private set; }

        public string Value { get; set; }

        public enum IconType
        {
            None,
            Directory,
            File,
            Url
        }
    }
}

