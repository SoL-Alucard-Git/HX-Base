namespace Update.Tool
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class HttpTool
    {
        public HttpTool()
        {
           
        }

        public static string PostHttp(string url, string body, string contentType)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.ContentType = contentType;
                request.Method = "POST";
                request.Timeout = 0x1d4c0;
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                char[] buffer = new char[0x2800];
                int length = reader.Read(buffer, 0, 0x2800);
                StringBuilder builder = new StringBuilder("");
                while (length > 0)
                {
                    string str = new string(buffer, 0, length);
                    builder.Append(str);
                    length = reader.Read(buffer, 0, 0x100);
                }
                string str2 = builder.ToString();
                response.Close();
                reader.Close();
                request.Abort();
                response.Close();
                return str2;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}

