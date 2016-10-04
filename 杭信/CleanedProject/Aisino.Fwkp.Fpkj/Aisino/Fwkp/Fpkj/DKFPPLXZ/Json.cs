namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class Json
    {
        private string _jsonText;
        private ILog loger;

        public Json()
        {
            this._jsonText = "";
            this.loger = LogUtil.GetLogger<Json>();
        }

        public Json(string jsonText)
        {
            this._jsonText = "";
            this.loger = LogUtil.GetLogger<Json>();
            this._jsonText = jsonText;
        }

        public Project Deserializer()
        {
            Project project = null;
            if (this._jsonText != "")
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(this._jsonText)))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Project));
                        project = (Project) serializer.ReadObject(stream);
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error("Json.Deserializer异常：" + exception.ToString());
                    project = null;
                }
            }
            return project;
        }

        public Project Deserializer(string jsonText)
        {
            this._jsonText = jsonText;
            return this.Deserializer();
        }

        public string Serializer(object obj)
        {
            string str = "";
            if (obj != null)
            {
                try
                {
                    Project project = new Project();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        new DataContractJsonSerializer(project.GetType()).WriteObject(stream, obj);
                        str = Encoding.UTF8.GetString(stream.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error("Json.Serialize异常：" + exception.ToString());
                }
            }
            return str;
        }
    }
}

