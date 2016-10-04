namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;

    public class ShareMethod
    {
        private static ILog _Loger = LogUtil.GetLogger<ShareMethod>();

        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding dstEncoding = Encoding.GetEncoding(0xfde9);
                Encoding encoding = ToolUtil.GetEncoding();
                byte[] bytes = encoding.GetBytes(str);
                byte[] buffer2 = Encoding.Convert(encoding, dstEncoding, bytes);
                return dstEncoding.GetString(buffer2);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        public static byte[] getByte(object value)
        {
            try
            {
                if (value != null)
                {
                    switch (value.GetType().ToString())
                    {
                        case "System.String":
                            return ToolUtil.GetBytes((string) value);

                        case "System.Int64":
                            return ToolUtil.GetBytes(getString(value));

                        case "System.Int32":
                            return ToolUtil.GetBytes(getString(value));

                        case "System.Boolean":
                            return ToolUtil.GetBytes(getString(value));

                        case "System.DateTime":
                            return ToolUtil.GetBytes(getString(value));

                        case "System.IO.MemoryStream":
                            return ToolUtil.GetBytes(getString(value));
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static DateTime getDateTime(object value)
        {
            try
            {
                if (value != null)
                {
                    switch (value.GetType().ToString())
                    {
                        case "System.String":
                            return DateTime.Parse((string) value);

                        case "System.Int64":
                            return getDateTime(getString(value));

                        case "System.Int32":
                            return getDateTime(getString(value));

                        case "System.Boolean":
                            return getDateTime(getString(value));

                        case "System.Double":
                            return getDateTime(getString(value));

                        case "System.IO.MemoryStream":
                            return getDateTime(getString(value));

                        case "System.Byte[]":
                            return getDateTime(getString(value));
                    }
                }
                return DateTime.Parse("0000/00/00 00:00:00");
            }
            catch (Exception)
            {
                return DateTime.Parse("0000/00/00 00:00:00");
            }
        }

        public static double getDouble(object value)
        {
            try
            {
                if (value != null)
                {
                    switch (value.GetType().ToString())
                    {
                        case "System.String":
                            return double.Parse((string) value);

                        case "System.Int64":
                        {
                            long num3 = (long) value;
                            return double.Parse(num3.ToString());
                        }
                        case "System.Int32":
                        {
                            int num4 = (int) value;
                            return double.Parse(num4.ToString());
                        }
                        case "System.Boolean":
                            return 0.0;

                        case "System.Double":
                            return (double) ((int) Math.Round((double) value, 0));

                        case "System.DateTime":
                            return 0.0;

                        case "System.IO.MemoryStream":
                            return (double) getInt(getString(value));

                        case "System.Byte[]":
                            return (double) getInt(getString(value));
                    }
                }
                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public static int getInt(object value)
        {
            try
            {
                if (value != null)
                {
                    switch (value.GetType().ToString())
                    {
                        case "System.String":
                            return int.Parse((string) value);

                        case "System.Int64":
                        {
                            long num3 = (long) value;
                            return int.Parse(num3.ToString());
                        }
                        case "System.Int32":
                            return (int) value;

                        case "System.Int16":
                            return (short) value;

                        case "System.Double":
                            return (int) Math.Round((double) value, 0);

                        case "System.Boolean":
                            return 0;

                        case "System.DateTime":
                            return 0;

                        case "System.IO.MemoryStream":
                            return getInt(getString(value));

                        case "System.Byte[]":
                            return getInt(getString(value));
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static Stream getStream(object value)
        {
            try
            {
                if (value == null)
                {
                    return new MemoryStream(ToolUtil.GetBytes("null"));
                }
                switch (value.GetType().ToString())
                {
                    case "System.String":
                        return new MemoryStream(ToolUtil.GetBytes((string) value));

                    case "System.Int64":
                        return new MemoryStream(ToolUtil.GetBytes(getString(value)));

                    case "System.Int32":
                        return new MemoryStream(ToolUtil.GetBytes(getString(value)));

                    case "System.Boolean":
                        return new MemoryStream(ToolUtil.GetBytes(getString(value)));

                    case "System.DateTime":
                        return new MemoryStream(ToolUtil.GetBytes(getString(value)));

                    case "System.Byte[]":
                        return new MemoryStream(ToolUtil.GetBytes(getString(value)));
                }
                return new MemoryStream(ToolUtil.GetBytes("null"));
            }
            catch
            {
                return new MemoryStream(ToolUtil.GetBytes("null"));
            }
        }

        public static string getString(object obj)
        {
            obj.GetType().ToString();
            switch (obj.GetType().ToString())
            {
                case "System.DateTime":
                {
                    Console.WriteLine(((DateTime) obj).ToLongDateString());
                    DateTime time3 = (DateTime) obj;
                    Console.WriteLine(time3.ToLongDateString().GetType());
                    string str = ((DateTime) obj).ToString(DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart);
                    if (string.IsNullOrEmpty(str))
                    {
                        str = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b).ToString(DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart);
                    }
                    return str;
                }
                case "System.Int16":
                {
                    Console.WriteLine(((short) obj).ToString());
                    short num3 = (short) obj;
                    Console.WriteLine(num3.ToString().GetType());
                    short num4 = (short) obj;
                    return num4.ToString();
                }
                case "System.Int32":
                {
                    Console.WriteLine(((int) obj).ToString());
                    int num6 = (int) obj;
                    Console.WriteLine(num6.ToString().GetType());
                    int num7 = (int) obj;
                    return num7.ToString();
                }
                case "System.Int64":
                {
                    Console.WriteLine(((long) obj).ToString());
                    long num9 = (long) obj;
                    Console.WriteLine(num9.ToString().GetType());
                    long num10 = (long) obj;
                    return num10.ToString();
                }
                case "System.Double":
                {
                    Console.WriteLine(((double) obj).ToString());
                    double num12 = (double) obj;
                    Console.WriteLine(num12.ToString().GetType());
                    double num13 = (double) obj;
                    return num13.ToString();
                }
                case "System.IO.MemoryStream":
                {
                    Stream stream = (Stream) obj;
                    StreamReader reader = new StreamReader(stream);
                    string str2 = string.Empty;
                    while (reader.Peek() > -1)
                    {
                        string str3 = reader.ReadLine();
                        str2 = str2 + str3;
                    }
                    Console.WriteLine(str2);
                    Console.WriteLine(str2.GetType());
                    return str2;
                }
                case "System.Byte[]":
                    return ToolUtil.GetString((byte[]) obj);

                case "System.Boolean":
                    if (!((bool) obj))
                    {
                        return "0";
                    }
                    return "1";

                case "System.DBNull":
                    return string.Empty;

                case "System.String":
                    return (string) obj;
            }
            MessageManager.ShowMsgBox("INP-241017");
            return string.Empty;
        }

        public static double GetValueDouble(DataRow row, DataColumnCollection Cols, string strFieldName)
        {
            try
            {
                double num = 0.0;
                if (Cols.Contains(strFieldName))
                {
                    num = getDouble(row[Cols[strFieldName]]);
                }
                return num;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return 0.0;
        }

        public static int GetValueInt(DataRow row, DataColumnCollection Cols, string strFieldName)
        {
            try
            {
                int num = 0;
                if (Cols.Contains(strFieldName))
                {
                    num = getInt(row[Cols[strFieldName]]);
                }
                return num;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return 0;
        }

        public static string GetValueString(DataRow row, DataColumnCollection Cols, string strFieldName, string strDefaultValue)
        {
            try
            {
                string str = string.Empty;
                if (Cols.Contains(strFieldName))
                {
                    str = getString(row[Cols[strFieldName]]);
                    if (((("凭证日期" == strFieldName) || ("到离港日期" == strFieldName)) || ("开票日期" == strFieldName)) && (string.Empty == str))
                    {
                        str = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b).ToString(DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart);
                    }
                    if (("税率" == strFieldName) && (string.Empty == str))
                    {
                        object obj2 = null;
                        str = (string) obj2;
                    }
                }
                else if ("到离港日期" == strFieldName)
                {
                    str = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b).ToString(DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart);
                }
                else
                {
                    str = strDefaultValue;
                }
                return str;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        public static string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding srcEncoding = Encoding.GetEncoding(0xfde9);
                Encoding encoding = ToolUtil.GetEncoding();
                byte[] bytes = srcEncoding.GetBytes(str);
                byte[] buffer2 = Encoding.Convert(srcEncoding, encoding, bytes);
                return encoding.GetString(buffer2);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }
    }
}

