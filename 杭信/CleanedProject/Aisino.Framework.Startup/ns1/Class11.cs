namespace ns1
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Data.SQLite;

    internal class Class11 : Interface0
    {
        private static readonly ILog ilog_0;

        static Class11()
        {
            
            ilog_0 = LogUtil.GetLogger<Class11>();
        }

        public Class11()
        {
            
        }

        public List<string> imethod_0()
        {
            List<string> list = new List<string>();
            string commandText = "select ZSXM from QX_YHXX";
            SQLiteConnection connection = null;
            try
            {
                connection = this.method_0();
                SQLiteCommand command = new SQLiteCommand(commandText, connection);
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string item = Convert.ToString(reader["ZSXM"]);
                    list.Add(item);
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message + "\r\n执行SQL失败（" + commandText + "):", exception);
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
            }
            return list;
        }

        public bool imethod_1(string string_0, string string_1)
        {
            bool flag;
            string commandText = "select MM from QX_YHXX where ZSXM = @name";
            using (SQLiteConnection connection = this.method_0())
            {
                SQLiteCommand command = new SQLiteCommand(commandText, connection);
                command.Parameters.Add("@name", DbType.String).Value = string_0;
                try
                {
                    connection.Open();
                    object obj2 = command.ExecuteScalar();
                    flag = ((obj2 != null) && (string_1 != null)) && string_1.Equals(obj2.ToString());
                }
                catch (Exception exception)
                {
                    connection.Close();
                    ilog_0.Error("执行SQL失败（" + commandText + "):", exception);
                    throw;
                }
            }
            return flag;
        }

        public ListDictionary imethod_2(string string_0)
        {
            ListDictionary dictionary2;
            ListDictionary dictionary = new ListDictionary();
            bool flag = false;
            string str = "";
            new List<string>();
            List<string> list = new List<string>();
            string commandText = "select DM, ISADMIN from QX_YHXX where ZSXM = @name";
            string str3 = "select GNXX_DM from QX_YHXX_JSXX UR, QX_JSXX_GNXX RF where UR.JSXX_DM = RF.JSXX_DM and UR.YHXX_DM = @userId";
            using (SQLiteConnection connection = this.method_0())
            {
                SQLiteCommand command = new SQLiteCommand(commandText, connection);
                command.Parameters.Add("@name", DbType.String).Value = string_0;
                try
                {
                    connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        str = Convert.ToString(reader["DM"]);
                        flag = (bool) reader["ISADMIN"];
                    }
                    reader.Close();
                    dictionary.Add("DM", str);
                    dictionary.Add("ISADMIN", flag);
                    if (!flag)
                    {
                        command = new SQLiteCommand(str3, connection);
                        command.Parameters.Add("@userId", DbType.String).Value = str;
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string item = Convert.ToString(reader["GNXX_DM"]);
                            list.Add(item);
                        }
                        reader.Close();
                        dictionary.Add("GNQX", list);
                    }
                    connection.Close();
                    dictionary2 = dictionary;
                }
                catch (Exception exception)
                {
                    connection.Close();
                    ilog_0.Error("执行SQL失败:\n", exception);
                    throw;
                }
            }
            return dictionary2;
        }

        private SQLiteConnection method_0()
        {
            return new SQLiteConnection { ConnectionString = BaseDAOFactory.GetConnString(1) };
        }
    }
}

