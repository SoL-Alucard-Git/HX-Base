namespace Update.Tool
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Text;

    public class JsonTools
    {
        public JsonTools()
        {
           
        }

        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            DataRowCollection rows = dt.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                builder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ColumnName;
                    string str = rows[i][j].ToString();
                    Type dataType = dt.Columns[j].DataType;
                    builder.Append("\"" + columnName + "\":");
                    str = StringFormat(str, dataType);
                    if (j < (dt.Columns.Count - 1))
                    {
                        builder.Append(str + ",");
                    }
                    else
                    {
                        builder.Append(str);
                    }
                }
                builder.Append("},");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string ListToJson<T>(IList<T> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    PropertyInfo[] properties = Activator.CreateInstance<T>().GetType().GetProperties();
                    bool flag = true;
                    builder.Append("{");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        if (properties[j].GetValue(list[i], null) != null)
                        {
                            if (!((j >= properties.Length) || flag))
                            {
                                builder.Append(",");
                            }
                            flag = false;
                            Type type = properties[j].GetValue(list[i], null).GetType();
                            builder.Append("\"" + properties[j].Name.ToString() + "\":" + StringFormat(properties[j].GetValue(list[i], null).ToString(), type));
                        }
                    }
                    builder.Append("}");
                    if (i < (list.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]");
            return builder.ToString();
        }

        public static string ListToJson<T>(IList<T> list, int page, int rows)
        {
            page = (page == 0) ? 1 : page;
            rows = (rows == 0) ? 10 : rows;
            int num = (page - 1) * rows;
            int num2 = page * rows;
            num2 = (num2 > list.Count) ? list.Count : num2;
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"total\":" + list.Count + ",\"rows\":[");
            for (int i = num; i < num2; i++)
            {
                PropertyInfo[] properties = Activator.CreateInstance<T>().GetType().GetProperties();
                bool flag = true;
                builder.Append("{");
                for (int j = 0; j < properties.Length; j++)
                {
                    if (properties[j].GetValue(list[i], null) != null)
                    {
                        if (!((j >= properties.Length) || flag))
                        {
                            builder.Append(",");
                        }
                        flag = false;
                        Type type = properties[j].GetValue(list[i], null).GetType();
                        string str = string.Empty;
                        if (type == typeof(DateTime))
                        {
                            str = ((DateTime) properties[j].GetValue(list[i], null)).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            str = properties[j].GetValue(list[i], null).ToString();
                        }
                        builder.Append("\"" + properties[j].Name.ToString() + "\":" + StringFormat(str, type));
                    }
                }
                builder.Append("}");
                if (i < (num2 - 1))
                {
                    builder.Append(",");
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }

        public static string smethod_0(string s)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s.ToCharArray()[i];
                char ch2 = ch;
                if (ch2 <= '"')
                {
                    switch (ch2)
                    {
                        case '\b':
                        {
                            builder.Append(@"\b");
                            continue;
                        }
                        case '\t':
                        {
                            builder.Append(@"\t");
                            continue;
                        }
                        case '\n':
                        {
                            builder.Append(@"\n");
                            continue;
                        }
                        case '\v':
                            goto Label_00A2;

                        case '\f':
                        {
                            builder.Append(@"\f");
                            continue;
                        }
                        case '\r':
                        {
                            builder.Append(@"\r");
                            continue;
                        }
                    }
                    if (ch2 != '"')
                    {
                        goto Label_00A2;
                    }
                    builder.Append("\\\"");
                    continue;
                }
                switch (ch2)
                {
                    case '/':
                    {
                        builder.Append(@"\/");
                        continue;
                    }
                    case '\\':
                    {
                        builder.Append(@"\\");
                        continue;
                    }
                }
            Label_00A2:
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = smethod_0(str);
                str = "\"" + str + "\"";
                return str;
            }
            if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
                return str;
            }
            if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            return str;
        }

        public static string ToArrayString(IEnumerable array)
        {
            string str = "[";
            foreach (object obj2 in array)
            {
                str = ToJson((IEnumerable) obj2.ToString()) + ",";
            }
            str.Remove(str.Length - 1, str.Length);
            return (str + "]");
        }

        public static string ToJson(IEnumerable array)
        {
            string str = "[";
            foreach (object obj2 in array)
            {
                str = str + ToJson(obj2) + ",";
            }
            str.Remove(str.Length - 1, str.Length);
            return (str + "]");
        }

        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            while (dataReader.Read())
            {
                builder.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type fieldType = dataReader.GetFieldType(i);
                    string name = dataReader.GetName(i);
                    string str = dataReader[i].ToString();
                    builder.Append("\"" + name + "\":");
                    str = StringFormat(str, fieldType);
                    if (i < (dataReader.FieldCount - 1))
                    {
                        builder.Append(str + ",");
                    }
                    else
                    {
                        builder.Append(str);
                    }
                }
                builder.Append("},");
            }
            dataReader.Close();
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public static string ToJson(DataSet dataSet)
        {
            string str = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                string str2 = str;
                str = str2 + "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            return (str.TrimEnd(new char[] { ',' }) + "}");
        }

        public static string ToJson(object jsonObject)
        {
            string str = "{";
            PropertyInfo[] properties = jsonObject.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                object obj2 = properties[i].GetGetMethod().Invoke(jsonObject, null);
                string str2 = string.Empty;
                if (((obj2 is DateTime) || (obj2 is Guid)) || (obj2 is TimeSpan))
                {
                    str2 = "'" + obj2.ToString() + "'";
                }
                else if (obj2 is string)
                {
                    str2 = "'" + ToJson((IEnumerable) obj2.ToString()) + "'";
                }
                else if (obj2 is IEnumerable)
                {
                    str2 = ToJson((IEnumerable) obj2);
                }
                else
                {
                    str2 = ToJson((IEnumerable) obj2.ToString());
                }
                string str3 = str;
                str = str3 + "\"" + ToJson((IEnumerable) properties[i].Name) + "\":" + str2 + ",";
            }
            str.Remove(str.Length - 1, str.Length);
            return (str + "}");
        }

        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
            {
                jsonName = dt.TableName;
            }
            builder.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        builder.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < (dt.Columns.Count - 1))
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("}");
                    if (i < (dt.Rows.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }
    }
}

