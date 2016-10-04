namespace Aisino.Framework.Dao
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using Npgsql;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class BaseDAONpgSQL : IBaseDAO
    {
        private static readonly ILog ilog_0;
        private int int_0;
        private int int_1;
        private NpgsqlTransaction npgsqlTransaction_0;
        private static ReaderWriterLock readerWriterLock_0;
        private string string_0;

        static BaseDAONpgSQL()
        {
            
            ilog_0 = LogUtil.GetLogger<BaseDAONpgSQL>();
            readerWriterLock_0 = new ReaderWriterLock();
        }

        internal BaseDAONpgSQL(string string_1, bool bool_0)
        {
            
            this.int_0 = 0x7530;
            this.int_1 = 0x7530;
            this.string_0 = string_1;
        }

        public void ClearResource()
        {
        }

        public void Close()
        {
        }

        public void Commit()
        {
        }

        public static DataTable ConvertDataReaderToDataTable(IDataReader idataReader_0)
        {
            DataTable table = new DataTable();
            int fieldCount = idataReader_0.FieldCount;
            for (int i = 0; i <= (fieldCount - 1); i++)
            {
                table.Columns.Add(idataReader_0.GetName(i).ToUpper(), idataReader_0.GetFieldType(i));
            }
            table.BeginLoadData();
            object[] values = new object[fieldCount];
            while (idataReader_0.Read())
            {
                idataReader_0.GetValues(values);
                table.LoadDataRow(values, true);
            }
            idataReader_0.Close();
            table.EndLoadData();
            return table;
        }

        public ConnectionState GetConnState()
        {
            return ConnectionState.Open;
        }

        public int 未确认DAO方法1(string[] string_1, List<Dictionary<string, object>> parameter)
        {
            int num = 0;
            int index = 0;
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        index = 0;
                        while (index < string_1.Length)
                        {
                            Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1[index]);
                            command.CommandType = ToolUtil.ParseCommandType(sQL["type"]);
                            command.CommandText = sQL["value"];
                            command.Parameters.Clear();
                            if (sQL["param"].Length > 0)
                            {
                                foreach (string str in sQL["param"].Split(new char[] { ';' }))
                                {
                                    string[] strArray = str.Split(new char[] { ',' });
                                    IDbDataParameter parameter2 = command.CreateParameter();
                                    parameter2.ParameterName = strArray[0];
                                    parameter2.DbType = ToolUtil.ParseDbType(strArray[1]);
                                    parameter2.Value = parameter[index][strArray[0]];
                                    command.Parameters.Add(parameter2);
                                }
                            }
                            num += command.ExecuteNonQuery();
                            index++;
                        }
                    }
                    catch (Exception exception)
                    {
                        ilog_0.Error(exception.ToString());
                        ilog_0.Error("执行SQL失败（" + ((index < string_1.Length) ? string_1[index] : Convert.ToString(index)) + ")");
                        throw;
                    }
                    return num;
                }
            }
            catch (Exception exception2)
            {
                ilog_0.Error(exception2.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        //imethod1 改为updatSQL 修改依据 DKFP  Add
        public int 未确认DAO方法2_疑似updateSQL(string string_1, Dictionary<string, object> parameter)
        {
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
                    command.CommandType = ToolUtil.ParseCommandType(sQL["type"]);
                    command.CommandText = sQL["value"];
                    command.Parameters.Clear();
                    if (sQL["param"].Length > 0)
                    {
                        foreach (string str in sQL["param"].Split(new char[] { ';' }))
                        {
                            string[] strArray = str.Split(new char[] { ',' });
                            IDbDataParameter parameter2 = command.CreateParameter();
                            parameter2.ParameterName = strArray[0];
                            parameter2.DbType = ToolUtil.ParseDbType(strArray[1]);
                            parameter2.Value = parameter[strArray[0]];
                            command.Parameters.Add(parameter2);
                        }
                    }
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }

        public int 未确认DAO方法3(string string_1, List<Dictionary<string, object>> parameter)
        {
            int num = 0;
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
                    command.CommandType = ToolUtil.ParseCommandType(sQL["type"]);
                    command.CommandText = sQL["value"];
                    command.Parameters.Clear();
                    if (sQL["param"].Length > 0)
                    {
                        Dictionary<string, object>[] dictionaryArray = parameter.ToArray();
                        for (int i = 0; i < dictionaryArray.Length; i++)
                        {
                            command.Parameters.Clear();
                            Dictionary<string, object> dictionary2 = dictionaryArray[i];
                            foreach (string str in sQL["param"].Split(new char[] { ';' }))
                            {
                                string[] strArray = str.Split(new char[] { ',' });
                                IDbDataParameter parameter2 = command.CreateParameter();
                                parameter2.ParameterName = strArray[0];
                                parameter2.DbType = ToolUtil.ParseDbType(strArray[1]);
                                parameter2.Value = dictionary2[strArray[0]];
                                command.Parameters.Add(parameter2);
                            }
                            num += command.ExecuteNonQuery();
                        }
                        return num;
                    }
                    return (num + command.ExecuteNonQuery());
                }
            }
            catch (Exception exception)
            {
                num = -1;
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        public int method_0(string string_1)
        {
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string_1;
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                ilog_0.Error(string_1);
                ilog_0.Error(exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }

        private bool method_1(Type type_0)
        {
            bool flag = false;
            if (type_0.FullName.Equals("System.Int16"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.Int32"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.Int64"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.UInt64"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.UInt32"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.UInt16"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.Double"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.Single"))
            {
                return true;
            }
            if (type_0.FullName.Equals("System.Decimal"))
            {
                flag = true;
            }
            return flag;
        }

        private T method_2<T>(string string_1)
        {
            try
            {
                T local = default(T);
                object obj2 = null;
                if (local.GetType().FullName.Equals("System.Int32"))
                {
                    obj2 = Convert.ToInt32(string_1);
                }
                else if (local.GetType().FullName.Equals("System.Int64"))
                {
                    obj2 = Convert.ToInt64(string_1);
                }
                else if (local.GetType().FullName.Equals("System.Int16"))
                {
                    obj2 = Convert.ToInt16(string_1);
                }
                else if (local.GetType().FullName.Equals("System.UInt16"))
                {
                    obj2 = Convert.ToUInt16(string_1);
                }
                else if (local.GetType().FullName.Equals("System.UInt32"))
                {
                    obj2 = Convert.ToUInt32(string_1);
                }
                else if (local.GetType().FullName.Equals("System.UInt64"))
                {
                    obj2 = Convert.ToUInt64(string_1);
                }
                else if (local.GetType().FullName.Equals("System.Single"))
                {
                    obj2 = Convert.ToSingle(string_1);
                }
                else if (local.GetType().FullName.Equals("System.Double"))
                {
                    obj2 = Convert.ToDouble(string_1);
                }
                else if (local.GetType().FullName.Equals("System.Decimal"))
                {
                    obj2 = Convert.ToDecimal(string_1);
                }
                return (T) obj2;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private void method_3(IDbCommand idbCommand_0, string string_1, string string_2, string string_3, Dictionary<string, object> parameter)
        {
            if (string_2.Length > 0)
            {
                foreach (string str in string_2.Split(new char[] { ';' }))
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    string str2 = strArray[0];
                    object obj2 = parameter[strArray[0]];
                    if (obj2 is List<object>)
                    {
                        List<object> list = (List<object>) obj2;
                        string str3 = string_1.Substring(0, string_1.IndexOf("@" + str2) - 1);
                        string str4 = str3.Substring(str3.LastIndexOf(" ") + 1).Trim();
                        if (str4.ToLower().Equals("in"))
                        {
                            int num = 0;
                            foreach (object obj3 in list)
                            {
                                string str5 = "TMP" + str2 + num;
                                string_1 = string_1.Replace("@" + str2, "@" + str2 + ",@" + str5);
                                IDbDataParameter parameter2 = idbCommand_0.CreateParameter();
                                parameter2.ParameterName = str5;
                                parameter2.DbType = ToolUtil.ParseDbType(strArray[1]);
                                parameter2.Value = obj3;
                                idbCommand_0.Parameters.Add(parameter2);
                                num++;
                            }
                            string_1 = string_1.Replace("@" + str2 + ",", "");
                        }
                        if (str4.ToLower().Equals("like"))
                        {
                            StringBuilder builder = new StringBuilder("(");
                            string str6 = str3.Substring(0, str3.LastIndexOf(" "));
                            string str7 = str6.Substring(str6.LastIndexOf(" ") + 1).Trim();
                            int num2 = 0;
                            foreach (object obj4 in list)
                            {
                                if (num2 > 0)
                                {
                                    if (str4.Equals("like"))
                                    {
                                        builder.Append(" or ");
                                    }
                                    if (str4.Equals("LIKE"))
                                    {
                                        builder.Append(" and ");
                                    }
                                }
                                builder.Append(str7).Append(" like @").Append(str2).Append(num2);
                                IDbDataParameter parameter3 = idbCommand_0.CreateParameter();
                                parameter3.ParameterName = str2 + num2;
                                parameter3.DbType = ToolUtil.ParseDbType(strArray[1]);
                                parameter3.Value = obj4;
                                idbCommand_0.Parameters.Add(parameter3);
                                num2++;
                            }
                            builder.Append(")");
                            string_1 = string_1.Replace(str7 + " " + str4 + "(@" + str2 + ")", builder.ToString());
                        }
                    }
                    else
                    {
                        IDbDataParameter parameter4 = idbCommand_0.CreateParameter();
                        parameter4.ParameterName = strArray[0];
                        parameter4.DbType = ToolUtil.ParseDbType(strArray[1]);
                        parameter4.Value = parameter[strArray[0]];
                        idbCommand_0.Parameters.Add(parameter4);
                    }
                }
            }
            idbCommand_0.CommandText = string_1;
            idbCommand_0.CommandType = ToolUtil.ParseCommandType(string_3);
        }

        private void method_4(IDbCommand idbCommand_0, string string_1, Dictionary<string, object> parameter)
        {
            Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
            string str = sQL["value"];
            string str2 = sQL["param"];
            string str3 = sQL["type"];
            this.method_3(idbCommand_0, str, str2, str3, parameter);
        }

        public void Open()
        {
        }

        public ArrayList querySQL(string string_1)
        {
            ArrayList list = new ArrayList();
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string_1;
                    command.CommandType = CommandType.Text;
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dictionary.Add(reader.GetName(i).ToUpper(), reader.GetValue(i));
                        }
                        list.Add(dictionary);
                    }
                    reader.Close();
                }
                return list;
            }
            catch (Exception exception)
            {
                ilog_0.Error(string_1);
                ilog_0.Error(exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public ArrayList querySQL(string string_1, Dictionary<string, object> parameter)
        {
            ArrayList list = new ArrayList();
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    this.method_4(command, string_1, parameter);
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dictionary.Add(reader.GetName(i).ToUpper(), reader.GetValue(i));
                        }
                        list.Add(dictionary);
                    }
                    reader.Close();
                }
                return list;
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "）:" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public AisinoDataSet querySQLDataSet(string string_1, Dictionary<string, object> parameter, int int_2, int int_3)
        {
            AisinoDataSet set = new AisinoDataSet();
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    IDataReader reader = null;
                    Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
                    string str = sQL["value"];
                    string[] strArray = str.Split(new char[] { ';' });
                    int num = 0;
                    string str2 = "0";
                    string str3 = "0";
                    if (strArray.Length >= 2)
                    {
                        this.method_3(command, strArray[0], sQL["param"], sQL["type"], parameter);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                        reader.Close();
                        if (int_2 <= 0)
                        {
                            int_2 = num;
                        }
                        int num2 = num / int_2;
                        if ((num2 * int_2) < num)
                        {
                            num2++;
                        }
                        if (num == 0)
                        {
                            num2 = 0;
                        }
                        if (int_3 > num2)
                        {
                            int_3 = num2;
                        }
                        if (int_3 < 1)
                        {
                            int_3 = 1;
                        }
                        str2 = (((int_3 - 1) * int_2) + 1).ToString();
                        str3 = (int_3 * int_2).ToString();
                        set.AllPageNum = num2;
                        set.AllRows = num;
                        set.CurrentPage = int_3;
                        set.PageSize = int_2;
                        if ((int_3 == 1) && (strArray.Length >= 3))
                        {
                            str = strArray[2];
                        }
                        else
                        {
                            str = strArray[1];
                        }
                    }
                    command.Parameters.Clear();
                    this.method_3(command, str, sQL["param"], sQL["type"], parameter);
                    if (int_2 > 0)
                    {
                        IDbDataParameter parameter2 = command.CreateParameter();
                        parameter2.ParameterName = "beginNO";
                        parameter2.DbType = DbType.Int32;
                        parameter2.Value = str2;
                        command.Parameters.Add(parameter2);
                        IDbDataParameter parameter3 = command.CreateParameter();
                        parameter3.ParameterName = "endNO";
                        parameter3.DbType = DbType.Int32;
                        parameter3.Value = str3;
                        command.Parameters.Add(parameter3);
                    }
                    reader = command.ExecuteReader();
                    set.Data = ConvertDataReaderToDataTable(reader);
                    reader.Close();
                }
                return set;
            }
            catch (ConstraintException exception)
            {
                ilog_0.Warn(exception.ToString());
            }
            catch (Exception exception2)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception2.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return set;
        }

        public AisinoDataSet querySQLDataSetExtend(string string_1, Dictionary<string, object> parameter, int int_2, int int_3, int int_4 = -1)
        {
            return new AisinoDataSet();
        }

        public DataTable querySQLDataTable(string string_1)
        {
            DataTable table = new DataTable();
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string_1;
                    command.CommandType = CommandType.Text;
                    IDataReader reader = command.ExecuteReader();
                    table = ConvertDataReaderToDataTable(reader);
                    reader.Close();
                }
                return table;
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "）:" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public DataTable querySQLDataTable(string string_1, Dictionary<string, object> parameter)
        {
            DataTable table = new DataTable();
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    this.method_4(command, string_1, parameter);
                    IDataReader reader = command.ExecuteReader();
                    table = ConvertDataReaderToDataTable(reader);
                    reader.Close();
                }
                return table;
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "）:" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public T queryValueSQL<T>(string string_1, Dictionary<string, object> parameter)
        {
            T objB = default(T);
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    this.method_4(command, string_1, parameter);
                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        object objA = reader.GetValue(0);
                        objA.GetType();
                        if (!object.Equals(objA, objB))
                        {
                            objA = objA.ToString();
                        }
                        if (objB != null)
                        {
                            if (this.method_1(objB.GetType()))
                            {
                                objB = this.method_2<T>(objA.ToString());
                            }
                        }
                        else
                        {
                            objB = (T) objA;
                        }
                    }
                    reader.Close();
                }
                return objB;
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return objB;
        }

        public void RollBack()
        {
        }

        public int updateSQLTransaction(string[] string_1, List<Dictionary<string, object>> parameter)
        {
            int num = 0;
            int index = 0;
            NpgsqlConnection connection = new NpgsqlConnection();
            try
            {
                connection.ConnectionString = this.string_0;
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    try
                    {
                        index = 0;
                        while (index < string_1.Length)
                        {
                            Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1[index]);
                            command.CommandType = ToolUtil.ParseCommandType(sQL["type"]);
                            command.CommandText = sQL["value"];
                            command.Parameters.Clear();
                            if (sQL["param"].Length > 0)
                            {
                                foreach (string str in sQL["param"].Split(new char[] { ';' }))
                                {
                                    string[] strArray = str.Split(new char[] { ',' });
                                    IDbDataParameter parameter2 = command.CreateParameter();
                                    parameter2.ParameterName = strArray[0];
                                    parameter2.DbType = ToolUtil.ParseDbType(strArray[1]);
                                    parameter2.Value = parameter[index][strArray[0]];
                                    command.Parameters.Add(parameter2);
                                }
                            }
                            num += command.ExecuteNonQuery();
                            index++;
                        }
                        if (command.Transaction != null)
                        {
                            command.Transaction.Commit();
                        }
                    }
                    catch (Exception exception)
                    {
                        ilog_0.Error(exception.ToString());
                        ilog_0.Error("执行SQL失败（" + ((index < string_1.Length) ? string_1[index] : Convert.ToString(index)) + ")");
                        if (command.Transaction != null)
                        {
                            command.Transaction.Rollback();
                        }
                        throw;
                    }
                    return num;
                }
            }
            catch (Exception exception2)
            {
                ilog_0.Error(exception2.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
            return num;
        }

        public string ConnectionString
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

