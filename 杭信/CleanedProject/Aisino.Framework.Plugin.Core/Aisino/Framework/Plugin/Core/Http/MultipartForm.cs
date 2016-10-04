namespace Aisino.Framework.Plugin.Core.Http
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Text;

    public class MultipartForm
    {
        private byte[] byte_0;
        private Encoding encoding_0;
        private MemoryStream memoryStream_0;
        private string string_0;

        public MultipartForm()
        {
            
            this.string_0 = string.Format("--{0}--", Guid.NewGuid());
            this.memoryStream_0 = new MemoryStream();
            this.encoding_0 = Encoding.Default;
        }

        public void AddFlie(string string_1, string string_2)
        {
            if (!File.Exists(string_2))
            {
                throw new FileNotFoundException("尝试添加不存在的文件。", string_2);
            }
            FileStream stream = null;
            byte[] buffer = new byte[0];
            try
            {
                stream = new FileStream(string_2, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.AddFlie(string_1, Path.GetFileName(string_2), buffer, buffer.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public void AddFlie(string string_1, string string_2, byte[] byte_1, int int_0)
        {
            if ((int_0 <= 0) || (int_0 > byte_1.Length))
            {
                int_0 = byte_1.Length;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("--{0}\r\n", this.string_0);
            builder.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", string_1, string_2);
            builder.AppendFormat("Content-Type: {0}\r\n", this.method_0(string_2));
            builder.Append("\r\n");
            byte[] bytes = this.encoding_0.GetBytes(builder.ToString());
            this.memoryStream_0.Write(bytes, 0, bytes.Length);
            this.memoryStream_0.Write(byte_1, 0, int_0);
            byte[] buffer = this.encoding_0.GetBytes("\r\n");
            this.memoryStream_0.Write(buffer, 0, buffer.Length);
        }

        public void AddString(string string_1, string string_2)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("--{0}\r\n", this.string_0);
            builder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", string_1);
            builder.Append("\r\n");
            builder.AppendFormat("{0}\r\n", string_2);
            byte[] bytes = this.encoding_0.GetBytes(builder.ToString());
            this.memoryStream_0.Write(bytes, 0, bytes.Length);
        }

        private string method_0(string string_1)
        {
            RegistryKey key = null;
            string defaultValue = "application/stream";
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(string_1));
                defaultValue = key.GetValue("Content Type", defaultValue).ToString();
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
            return defaultValue;
        }

        public string ContentType
        {
            get
            {
                return string.Format("multipart/form-data; boundary={0}", this.string_0);
            }
        }

        public byte[] FormData
        {
            get
            {
                if (this.byte_0 == null)
                {
                    byte[] bytes = this.encoding_0.GetBytes("--" + this.string_0 + "--\r\n");
                    this.memoryStream_0.Write(bytes, 0, bytes.Length);
                    this.byte_0 = this.memoryStream_0.ToArray();
                }
                return this.byte_0;
            }
        }

        public Encoding StringEncoding
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
    }
}

