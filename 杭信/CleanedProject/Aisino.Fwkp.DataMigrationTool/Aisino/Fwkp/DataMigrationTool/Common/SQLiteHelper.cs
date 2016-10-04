namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SQLite;

    public class SQLiteHelper
    {
        private static SQLiteConnection Connection;

        public static SQLiteConnection ConnValue(string connectionString)
        {
            if (Connection == null)
            {
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Closed)
            {
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }
            return Connection;
        }

        public static int ExecuteCommand(string safeSql)
        {
            int num2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    num2 = command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int ExecuteCommand(string safeSql, params SQLiteParameter[] values)
        {
            int num;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    command.get_Parameters().AddRange(values);
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static int ExecuteCommand(string safeSql, CommandType type, params SQLiteParameter[] values)
        {
            int num;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    command.CommandType = type;
                    command.get_Parameters().AddRange(values);
                    command.ExecuteNonQuery();
                    num = command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num;
        }

        public static int ExecuteCommand(string safeSql, CommandType type, int index)
        {
            int num;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    command.CommandType = type;
                    SQLiteParameter parameter = new SQLiteParameter("@rid", SqlDbType.Int) {
                        Value = index
                    };
                    command.get_Parameters().Add(parameter);
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static DataTable GetDataSet(string safeSql)
        {
            DataSet dataSet = new DataSet();
            SQLiteCommand command = new SQLiteCommand(safeSql, Connection);
            new SQLiteDataAdapter(command).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public static DataTable GetDataSet(string sql, params SQLiteParameter[] values)
        {
            DataSet dataSet = new DataSet();
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.get_Parameters().AddRange(values);
            new SQLiteDataAdapter(command).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public static SQLiteDataReader GetReader(string safeSql)
        {
            SQLiteDataReader reader2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    reader2 = command.ExecuteReader();
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static SQLiteDataReader GetReader(string sql, params SQLiteParameter[] values)
        {
            SQLiteDataReader reader2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    command.get_Parameters().AddRange(values);
                    reader2 = command.ExecuteReader();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return reader2;
        }

        public static SQLiteDataReader GetReader(string safeSql, CommandType cmdType, params SQLiteParameter[] values)
        {
            SQLiteDataReader reader2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    command.CommandType = cmdType;
                    command.get_Parameters().AddRange(values);
                    reader2 = command.ExecuteReader();
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static int GetScalar(string safeSql)
        {
            int num2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    num2 = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int GetScalar(string sql, params SQLiteParameter[] values)
        {
            int num2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    command.get_Parameters().AddRange(values);
                    num2 = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int GetScalar(string sql, CommandType type, params SQLiteParameter[] values)
        {
            int num2;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    command.CommandType = type;
                    command.get_Parameters().AddRange(values);
                    num2 = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int IsEmpty(string safeSql)
        {
            int num;
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(safeSql, Connection))
                {
                    num = command.ExecuteReader().HasRows ? 1 : 0;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }
    }
}

