namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net;
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Xml;

    public class SerializeUtil
    {
        private static ILog ilog_0;

        static SerializeUtil()
        {
            
            ilog_0 = LogUtil.GetLogger<SerializeUtil>();
        }

        public SerializeUtil()
        {
            
        }

        public static object Deserialize(byte[] byte_0)
        {
            object obj2 = null;
            try
            {
                obj2 = new BinaryFormatter().Deserialize(new MemoryStream(byte_0));
            }
            catch (Exception exception)
            {
                ilog_0.Error("[Deserialize] 出现异常：" + exception.Message);
            }
            return obj2;
        }

        public static object Deserialize(bool bool_0, string string_0)
        {
            if (!File.Exists(string_0))
            {
                return null;
            }
            object obj2 = null;
            Stream serializationStream = File.Open(string_0, FileMode.Open);
            IFormatter formatter2 = bool_0 ? ((IFormatter) new BinaryFormatter()) : ((IFormatter) new SoapFormatter());
            try
            {
                obj2 = formatter2.Deserialize(serializationStream);
            }
            catch (SerializationException)
            {
                Console.WriteLine("Could not deserialize file.  Check that the input file is valid and the requested serialization format is correct.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Could not deserialize file.  Check that the input file is valid and the requested serialization format is correct.");
            }
            catch (XmlException)
            {
                Console.WriteLine("Could not deserialize file.  Check that the input file is valid and the requested serialization format is correct.");
            }
            serializationStream.Close();
            return obj2;
        }

        public static byte[] Serialize(object object_0)
        {
            byte[] buffer = null;
            try
            {
                MemoryStream serializationStream = new MemoryStream();
                new BinaryFormatter().Serialize(serializationStream, object_0);
                buffer = serializationStream.GetBuffer();
            }
            catch (Exception exception)
            {
                ilog_0.Error("[Serialize] 出现异常：" + exception.Message);
            }
            return buffer;
        }

        public static void Serialize(bool bool_0, string string_0, object object_0)
        {
            Stream serializationStream = File.Open(string_0, FileMode.Create);
            (bool_0 ? ((IFormatter) new BinaryFormatter()) : ((IFormatter) new SoapFormatter())).Serialize(serializationStream, object_0);
            serializationStream.Close();
        }
    }
}

