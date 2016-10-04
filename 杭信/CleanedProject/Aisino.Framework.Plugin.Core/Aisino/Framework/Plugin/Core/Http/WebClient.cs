namespace Aisino.Framework.Plugin.Core.Http
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Net.Cache;
    using System.Net.Security;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;

    public class WebClient
    {
        private System.Text.Encoding encoding_0;
        private ILog ilog_0;
        private int int_0;
        private string string_0;
        private WebHeaderCollection webHeaderCollection_0;
        private WebHeaderCollection webHeaderCollection_1;
        private WebProxy webProxy_0;

        public event EventHandler<DownloadEventArgs> DownloadProgressChanged;

        public event EventHandler<UploadEventArgs> UploadProgressChanged;

        static WebClient()
        {
            
            smethod_1();
        }

        public WebClient()
        {
            
            this.encoding_0 = ToolUtil.GetEncoding();
            this.string_0 = "";
            this.int_0 = 0x3b88;
            this.ilog_0 = LogUtil.GetLogger<Aisino.Framework.Plugin.Core.Http.WebClient>();
            this.webHeaderCollection_0 = new WebHeaderCollection();
            this.webHeaderCollection_1 = new WebHeaderCollection();
        }

        public void DownloadFile(string string_1, string string_2, out int int_1)
        {
            FileStream stream = null;
            try
            {
                HttpWebRequest request = this.method_2(string_1, "GET");
                byte[] buffer = this.method_0(request, out int_1);
                stream = new FileStream(string_2, FileMode.Create, FileAccess.Write);
                stream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public byte[] GetData(string string_1, out int int_1)
        {
            HttpWebRequest request = this.method_2(string_1, "GET");
            return this.method_0(request, out int_1);
        }

        public string GetHtml(string string_1, out int int_1)
        {
            HttpWebRequest request = this.method_2(string_1, "GET");
            this.string_0 = this.encoding_0.GetString(this.method_0(request, out int_1));
            return this.string_0;
        }

        private byte[] method_0(HttpWebRequest httpWebRequest_0, out int int_1)
        {
            int_1 = 0;
            try
            {
                HttpWebResponse response = (HttpWebResponse) httpWebRequest_0.GetResponse();
                Stream responseStream = response.GetResponseStream();
                this.webHeaderCollection_1 = response.Headers;
                smethod_0();
                DownloadEventArgs e = new DownloadEventArgs();
                if (this.webHeaderCollection_1[HttpResponseHeader.ContentLength] != null)
                {
                    e.TotalBytes = Convert.ToInt32(this.webHeaderCollection_1[HttpResponseHeader.ContentLength]);
                }
                MemoryStream stream = new MemoryStream();
                int count = 0;
                byte[] buffer = new byte[this.int_0];
                while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, count);
                    if (this.DownloadProgressChanged != null)
                    {
                        e.BytesReceived += count;
                        e.ReceivedData = new byte[count];
                        Array.Copy(buffer, e.ReceivedData, count);
                        this.DownloadProgressChanged(this, e);
                    }
                }
                responseStream.Close();
                stream.Position = 0L;
                if (this.ResponseHeaders[HttpResponseHeader.ContentEncoding] != null)
                {
                    MemoryStream stream4 = new MemoryStream();
                    count = 0;
                    buffer = new byte[100];
                    switch (this.ResponseHeaders[HttpResponseHeader.ContentEncoding].ToLower())
                    {
                        case "gzip":
                        {
                            GZipStream stream3 = new GZipStream(stream, CompressionMode.Decompress);
                            while ((count = stream3.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream4.Write(buffer, 0, count);
                            }
                            return stream4.ToArray();
                        }
                        case "deflate":
                        {
                            DeflateStream stream5 = new DeflateStream(stream, CompressionMode.Decompress);
                            while ((count = stream5.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream4.Write(buffer, 0, count);
                            }
                            return stream4.ToArray();
                        }
                    }
                }
                return stream.ToArray();
            }
            catch (Exception exception)
            {
                this.ilog_0.ErrorFormat("网络连接响应异常:{0},{1}", httpWebRequest_0.RequestUri, exception);
                int_1 = -1;
                return this.encoding_0.GetBytes(string.Concat(new object[] { "连接异常：", httpWebRequest_0.RequestUri, ",", exception.Message }));
            }
        }

        private void method_1(HttpWebRequest httpWebRequest_0, byte[] byte_0)
        {
            int offset = 0;
            int count = this.int_0;
            int num3 = 0;
            Stream requestStream = httpWebRequest_0.GetRequestStream();
            UploadEventArgs e = new UploadEventArgs {
                TotalBytes = byte_0.Length
            };
            while ((num3 = byte_0.Length - offset) > 0)
            {
                if (count > num3)
                {
                    count = num3;
                }
                requestStream.Write(byte_0, offset, count);
                offset += count;
                if (this.UploadProgressChanged != null)
                {
                    e.BytesSent = offset;
                    this.UploadProgressChanged(this, e);
                }
            }
            requestStream.Close();
        }

        private HttpWebRequest method_2(string string_1, string string_2)
        {
            Uri requestUri = new Uri(string_1);
            if (requestUri.Scheme == "https")
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.method_3);
            }
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestUri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = string_2;
            if (this.webProxy_0 != null)
            {
                request.Proxy = this.webProxy_0;
            }
            foreach (string str in this.webHeaderCollection_0.Keys)
            {
                request.Headers.Add(str, this.webHeaderCollection_0[str]);
            }
            this.webHeaderCollection_0.Clear();
            return request;
        }

        private bool method_3(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
        {
            return true;
        }

        public string Post(string string_1, MultipartForm multipartForm_0, out int int_1)
        {
            HttpWebRequest request = this.method_2(string_1, "POST");
            request.ContentType = multipartForm_0.ContentType;
            request.ContentLength = multipartForm_0.FormData.Length;
            request.KeepAlive = true;
            this.method_1(request, multipartForm_0.FormData);
            this.string_0 = this.encoding_0.GetString(this.method_0(request, out int_1));
            return this.string_0;
        }

        public string Post(string string_1, string string_2, out int int_1)
        {
            byte[] bytes = this.encoding_0.GetBytes(string_2);
            return this.Post(string_1, bytes, out int_1);
        }

        public string Post(string string_1, byte[] byte_0, out int int_1)
        {
            this.string_0 = this.encoding_0.GetString(this.Post_Byte(string_1, byte_0, out int_1));
            return this.string_0;
        }

        public byte[] Post_Byte(string string_1, byte[] byte_0, out int int_1)
        {
            HttpWebRequest request = this.method_2(string_1, "POST");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byte_0.Length;
            request.KeepAlive = true;
            this.method_1(request, byte_0);
            return this.method_0(request, out int_1);
        }

        public byte[] Post_KPS(string string_1, byte[] byte_0, out int int_1)
        {
            return this.Post_Byte(string_1, byte_0, out int_1);
        }

        private static void smethod_0()
        {
        }

        private static void smethod_1()
        {
        }

        public int BufferSize
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public System.Text.Encoding Encoding
        {
            get
            {
                return this.encoding_0;
            }
            set
            {
                this.encoding_0 = value;
            }
        }

        public WebProxy Proxy
        {
            get
            {
                return this.webProxy_0;
            }
            set
            {
                this.webProxy_0 = value;
            }
        }

        public WebHeaderCollection RequestHeaders
        {
            get
            {
                return this.webHeaderCollection_0;
            }
        }

        public string RespHtml
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public WebHeaderCollection ResponseHeaders
        {
            get
            {
                return this.webHeaderCollection_1;
            }
        }
    }
}

