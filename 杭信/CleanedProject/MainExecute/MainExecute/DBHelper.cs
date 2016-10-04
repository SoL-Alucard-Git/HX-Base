namespace MainExecute
{
    using System;
    using System.Data;
    using System.Data.SQLite;

    public static class DBHelper
    {
        private static SQLiteConnection Connection;

        public static void CloseConn()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
        }

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
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }
    }
}

