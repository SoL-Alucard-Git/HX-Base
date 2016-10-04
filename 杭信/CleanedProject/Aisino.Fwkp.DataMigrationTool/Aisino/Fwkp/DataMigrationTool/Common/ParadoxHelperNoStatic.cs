namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using System;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.SqlClient;

    public class ParadoxHelperNoStatic
    {
        private OdbcConnection Connection;

        public void CloseConn()
        {
            this.Connection.Close();
        }

        public OdbcConnection ConnValue(string connectionString)
        {
            if (this.Connection == null)
            {
                this.Connection = new OdbcConnection(connectionString);
                this.Connection.Open();
            }
            else if (this.Connection.State == ConnectionState.Closed)
            {
                this.Connection = new OdbcConnection(connectionString);
                this.Connection.Open();
            }
            else if (this.Connection.State == ConnectionState.Broken)
            {
                this.Connection.Close();
                this.Connection.Open();
            }
            else if (this.Connection != null)
            {
                this.Connection.Close();
                this.Connection = new OdbcConnection(connectionString);
                this.Connection.Open();
            }
            return this.Connection;
        }

        public int ExecuteCommand(string safeSql)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public int ExecuteCommand(string safeSql, params OdbcParameter[] values)
        {
            int num;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public int ExecuteCommand(string safeSql, CommandType type, params OdbcParameter[] values)
        {
            int num;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public int ExecuteCommand(string safeSql, CommandType type, int index)
        {
            int num;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public DataTable GetDataSet(string safeSql)
        {
            DataSet dataSet = new DataSet();
            OdbcCommand selectCommand = new OdbcCommand(safeSql, this.Connection);
            new OdbcDataAdapter(selectCommand).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public DataTable GetDataSet(string sql, params OdbcParameter[] values)
        {
            DataSet dataSet = new DataSet();
            OdbcCommand selectCommand = new OdbcCommand(sql, this.Connection);
            selectCommand.Parameters.AddRange(values);
            new OdbcDataAdapter(selectCommand).Fill(dataSet);
            return dataSet.Tables[0];
        }

        public OdbcDataReader GetReader(string safeSql)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public OdbcDataReader GetReader(string sql, params OdbcParameter[] values)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, this.Connection))
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

        public OdbcDataReader GetReader(string safeSql, CommandType cmdType, params OdbcParameter[] values)
        {
            OdbcDataReader reader2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public int GetScalar(string safeSql)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(safeSql, this.Connection))
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

        public int GetScalar(string sql, params OdbcParameter[] values)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, this.Connection))
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

        public int GetScalar(string sql, CommandType type, params OdbcParameter[] values)
        {
            int num2;
            try
            {
                using (OdbcCommand command = new OdbcCommand(sql, this.Connection))
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

