namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using System;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.SqlClient;

    public class ParadoxHelper
    {
        private static OdbcConnection Connection;

        public static void CloseConn()
        {
            Connection.Close();
        }

        public static OdbcConnection ConnValue(string connectionString)
        {
            if (Connection == null)
            {
                Connection = new OdbcConnection(connectionString);
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Closed)
            {
                Connection = new OdbcConnection(connectionString);
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }
            else if (Connection != null)
            {
                Connection.Close();
                Connection = new OdbcConnection(connectionString);
                Connection.Open();
            }
            return Connection;
        }

        public static int ExecuteCommand(string safeSql)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
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

        public static int ExecuteCommand(string safeSql, params OdbcParameter[] values)
        {
            int num;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
                {
                    command.Parameters.AddRange(values);
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public static int ExecuteCommand(string safeSql, CommandType type, params OdbcParameter[] values)
        {
            int num;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
                {
                    command.CommandType = type;
                    command.Parameters.AddRange(values);
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
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
                {
                    command.CommandType = type;
                    OdbcParameter parameter = new OdbcParameter("@rid", SqlDbType.Int) {
                        Value = index
                    };
                    command.Parameters.Add(parameter);
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
            OdbcCommand selectCommand = new OdbcCommand(safeSql, Connection);
            new OdbcDataAdapter(selectCommand).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public static DataTable GetDataSet(string sql, params OdbcParameter[] values)
        {
            DataSet dataSet = new DataSet();
            OdbcCommand selectCommand = new OdbcCommand(sql, Connection);
            selectCommand.Parameters.AddRange(values);
            new OdbcDataAdapter(selectCommand).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public static OdbcDataReader GetReader(string safeSql)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
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

        public static OdbcDataReader GetReader(string sql, params OdbcParameter[] values)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, Connection))
                {
                    command.Parameters.AddRange(values);
                    reader2 = command.ExecuteReader();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return reader2;
        }

        public static OdbcDataReader GetReader(string safeSql, CommandType cmdType, params OdbcParameter[] values)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
                {
                    command.CommandType = cmdType;
                    command.Parameters.AddRange(values);
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
                using (OdbcCommand command = new OdbcCommand(safeSql, Connection))
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

        public static int GetScalar(string sql, params OdbcParameter[] values)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, Connection))
                {
                    command.Parameters.AddRange(values);
                    num2 = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int GetScalar(string sql, CommandType type, params OdbcParameter[] values)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, Connection))
                {
                    command.CommandType = type;
                    command.Parameters.AddRange(values);
                    num2 = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return num2;
        }
    }
}

