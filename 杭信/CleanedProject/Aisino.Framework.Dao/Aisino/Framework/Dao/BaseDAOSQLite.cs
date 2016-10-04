namespace Aisino.Framework.Dao
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class BaseDAOSQLite : IBaseDAO
    {
        private static readonly ILog ilog_0;
        private int int_0;
        private int int_1;
        private static ReaderWriterLock readerWriterLock_0;
        private static SQLiteConnection sqliteConnection_0;
        private SQLiteDataAdapter sqliteDataAdapter_0;
        private SQLiteTransaction sqliteTransaction_0;
        private string string_0;

        static BaseDAOSQLite()
        {
            
            ilog_0 = LogUtil.GetLogger<BaseDAOSQLite>();
            readerWriterLock_0 = new ReaderWriterLock();
        }

        internal BaseDAOSQLite(string string_1, bool bool_0)
        {
            
            this.int_0 = 0x7530;
            this.int_1 = 0x7530;
            this.string_0 = string_1;
            sqliteConnection_0 = new SQLiteConnection();
            sqliteConnection_0.ConnectionString = this.string_0;
            sqliteConnection_0.Open();
        }

        public void ClearResource()
        {
            readerWriterLock_0.ReleaseLock();
            if (sqliteConnection_0 != null)
            {
                if (sqliteConnection_0.State != ConnectionState.Closed)
                {
                    sqliteConnection_0.Close();
                }
                sqliteConnection_0.Dispose();
            }
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
                table.Columns.Add(idataReader_0.GetName(i), idataReader_0.GetFieldType(i));
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
            return sqliteConnection_0.State;
        }

        //09-20 从imethod_0改为updateSQL
        //依据XXFP的DeleteAll等方法里面等调用做出判断
        public int 未确认DAO方法1(string[] string_1, List<Dictionary<string, object>> parameter)
        {
            return this.updateSQLTransaction(string_1, parameter);
        }

        //09-20 从imethod_1改为updateSQL
        //依据DKFP的Add等方法里面等调用做出判断
        public int 未确认DAO方法2_疑似updateSQL(string string_1, Dictionary<string, object> parameter)
        {
            int num = 0;
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireWriterLock(this.int_1);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
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
                        num = command.ExecuteNonQuery();
                    }
                }
                return num;
            }
            catch (Exception exception)
            {
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                this.method_0();
            }
            return num;
        }

        public int 未确认DAO方法3(string string_1, List<Dictionary<string, object>> parameter)
        {
            int num = 0;
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireWriterLock(this.int_1);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
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
                return num;
            }
            catch (Exception exception)
            {
                num = -1;
                ilog_0.Error("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                this.method_0();
            }
            return num;
        }

        private void method_0()
        {
            if (readerWriterLock_0 != null)
            {
                if (readerWriterLock_0.IsReaderLockHeld)
                {
                    readerWriterLock_0.ReleaseReaderLock();
                }
                if (readerWriterLock_0.IsWriterLockHeld)
                {
                    readerWriterLock_0.ReleaseWriterLock();
                }
            }
        }

        public int method_1(string string_1)
        {
            int num = 0;
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireWriterLock(this.int_1);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        command.CommandText = string_1;
                        command.CommandType = CommandType.Text;
                        num = command.ExecuteNonQuery();
                    }
                }
                return num;
            }
            catch (Exception exception)
            {
                ilog_0.Error(string_1);
                ilog_0.Error(exception.ToString());
                throw;
            }
            finally
            {
                this.method_0();
            }
            return num;
        }

        private bool method_2(Type type_0)
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

        private T method_3<T>(string string_1)
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

        private void method_4(IDbCommand idbCommand_0, string string_1, string string_2, string string_3, Dictionary<string, object> parameter)
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

        private void method_5(IDbCommand idbCommand_0, string string_1, Dictionary<string, object> parameter)
        {
            Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
            string str = sQL["value"];
            string str2 = sQL["param"];
            string str3 = sQL["type"];
            this.method_4(idbCommand_0, str, str2, str3, parameter);
        }

        public void Open()
        {
        }

        public ArrayList querySQL(string string_1)
        {
            ArrayList list = new ArrayList();
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireReaderLock(this.int_0);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        command.CommandText = string_1;
                        command.CommandType = CommandType.Text;
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dictionary.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            list.Add(dictionary);
                        }
                        reader.Close();
                    }
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
                this.method_0();
            }
            return list;
        }

        public ArrayList querySQL(string string_1, Dictionary<string, object> parameter)
        {
            ArrayList list = new ArrayList();
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireReaderLock(this.int_0);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        this.method_5(command, string_1, parameter);
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dictionary.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            list.Add(dictionary);
                        }
                        reader.Close();
                    }
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
                this.method_0();
            }
            return list;
        }

        public AisinoDataSet querySQLDataSet(string string_1, Dictionary<string, object> parameter, int int_2, int int_3)
        {
            AisinoDataSet set = new AisinoDataSet();
            try
            {
                DataSet dataSet = new DataSet();
                if (sqliteConnection_0 == null)
                {
                    return set;
                }
                if (sqliteConnection_0.State == ConnectionState.Closed)
                {
                    sqliteConnection_0.Open();
                }
                readerWriterLock_0.AcquireReaderLock(this.int_0);
                using (SQLiteCommand command = sqliteConnection_0.CreateCommand())
                {
                    Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
                    string str = sQL["value"];
                    string[] strArray = str.Split(new char[] { ';' });
                    int result = 0;
                    string str2 = "0";
                    string str3 = "0";
                    if (strArray.Length >= 2)
                    {
                        ilog_0.Info("开始查询总票数。。。。");
                        this.method_4(command, strArray[0], sQL["param"], sQL["type"], parameter);
                        object obj2 = command.ExecuteScalar();
                        result = 0;
                        if (obj2 != null)
                        {
                            int.TryParse(obj2.ToString(), out result);
                        }
                        ilog_0.Info("结束查询总票数。。。。");
                        if (int_2 <= 0)
                        {
                            int_2 = result;
                        }
                        int num2 = result / int_2;
                        if ((num2 * int_2) < result)
                        {
                            num2++;
                        }
                        if (result == 0)
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
                        set.AllRows = result;
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
                    this.method_4(command, str, sQL["param"], sQL["type"], parameter);
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
                    this.sqliteDataAdapter_0 = new SQLiteDataAdapter(command);
                    this.sqliteDataAdapter_0.Fill(dataSet);
                    set.Data = dataSet.Tables[0];
                    dataSet.Dispose();
                    this.sqliteDataAdapter_0.Dispose();
                }
                ilog_0.Info("结束查询。。。");
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
                this.method_0();
            }
            return set;
        }

        public AisinoDataSet querySQLDataSetExtend(string string_1, Dictionary<string, object> parameter, int int_2, int int_3, int int_4 = -1)
        {
            AisinoDataSet set = new AisinoDataSet();
            try
            {
                DataSet dataSet = new DataSet();
                if (sqliteConnection_0 == null)
                {
                    return set;
                }
                if (sqliteConnection_0.State == ConnectionState.Closed)
                {
                    sqliteConnection_0.Open();
                }
                readerWriterLock_0.AcquireReaderLock(this.int_0);
                using (SQLiteCommand command = sqliteConnection_0.CreateCommand())
                {
                    Dictionary<string, string> sQL = SQLUtil.GetSQL(string_1);
                    string str = sQL["value"];
                    string[] strArray = str.Split(new char[] { ';' });
                    int result = 0;
                    string str2 = "0";
                    string str3 = "0";
                    if (strArray.Length >= 2)
                    {
                        ilog_0.Info("开始查询总票数。。。。");
                        if (int_4 == -1)
                        {
                            this.method_4(command, strArray[0], sQL["param"], sQL["type"], parameter);
                            object obj2 = command.ExecuteScalar();
                            result = 0;
                            if (obj2 != null)
                            {
                                int.TryParse(obj2.ToString(), out result);
                            }
                            PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_ALLNum", result.ToString());
                        }
                        else
                        {
                            string s = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXun_ALLNum");
                            if (s != null)
                            {
                                int.TryParse(s, out result);
                            }
                        }
                        ilog_0.Info("结束查询总票数。。。。");
                        if (int_2 <= 0)
                        {
                            int_2 = result;
                        }
                        int num2 = result / int_2;
                        if ((num2 * int_2) < result)
                        {
                            num2++;
                        }
                        if (result == 0)
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
                        set.AllRows = result;
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
                    this.method_4(command, str, sQL["param"], sQL["type"], parameter);
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
                    this.sqliteDataAdapter_0 = new SQLiteDataAdapter(command);
                    this.sqliteDataAdapter_0.Fill(dataSet);
                    set.Data = dataSet.Tables[0];
                    dataSet.Dispose();
                    this.sqliteDataAdapter_0.Dispose();
                }
                ilog_0.Info("结束查询。。。");
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
                this.method_0();
            }
            return set;
        }

        public DataTable querySQLDataTable(string string_1)
        {
            DataTable table = new DataTable();
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireReaderLock(this.int_0);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        command.CommandText = string_1;
                        command.CommandType = CommandType.Text;
                        IDataReader reader = command.ExecuteReader();
                        table = ConvertDataReaderToDataTable(reader);
                        reader.Close();
                    }
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
                this.method_0();
            }
            return table;
        }

        public DataTable querySQLDataTable(string string_1, Dictionary<string, object> parameter)
        {
            DataTable table = new DataTable();
            try
            {
                if (sqliteConnection_0 != null)
                {
                    SQLUtil.GetSQL(string_1);
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireReaderLock(this.int_0);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        this.method_5(command, string_1, parameter);
                        IDataReader reader = command.ExecuteReader();
                        table = ConvertDataReaderToDataTable(reader);
                        reader.Close();
                    }
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
                this.method_0();
            }
            return table;
        }

        public T queryValueSQL<T>(string string_1, Dictionary<string, object> parameter)
        {
            T objB = default(T);
            try
            {
                if (sqliteConnection_0 != null)
                {
                    SQLUtil.GetSQL(string_1);
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireReaderLock(this.int_0);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        this.method_5(command, string_1, parameter);
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
                                if (this.method_2(objB.GetType()))
                                {
                                    objB = this.method_3<T>(objA.ToString());
                                }
                            }
                            else
                            {
                                objB = (T) objA;
                            }
                        }
                        reader.Close();
                    }
                }
                return objB;
            }
            catch (Exception exception)
            {
                Console.WriteLine("执行SQL失败（" + string_1 + "):" + exception.ToString());
                throw;
            }
            finally
            {
                this.method_0();
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
            try
            {
                if (sqliteConnection_0 != null)
                {
                    if (sqliteConnection_0.State == ConnectionState.Closed)
                    {
                        sqliteConnection_0.Open();
                    }
                    readerWriterLock_0.AcquireWriterLock(this.int_1);
                    using (IDbCommand command = sqliteConnection_0.CreateCommand())
                    {
                        try
                        {
                            command.Transaction = sqliteConnection_0.BeginTransaction();
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
                            num = 0;
                            throw;
                        }
                        return num;
                    }
                }
                return num;
            }
            catch (Exception exception2)
            {
                ilog_0.Error(exception2.ToString());
                num = 0;
            }
            finally
            {
                this.method_0();
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

