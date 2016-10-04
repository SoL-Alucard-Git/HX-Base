using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

internal class Class66
{
    private static SQLiteConnection sqliteConnection_0;

    public Class66()
    {
    }

    public static SQLiteConnection smethod_0(string string_0)
    {
        if (sqliteConnection_0 == null)
        {
            sqliteConnection_0 = new SQLiteConnection(string_0);
            sqliteConnection_0.Open();
        }
        else if (sqliteConnection_0.State == ConnectionState.Closed)
        {
            sqliteConnection_0 = new SQLiteConnection(string_0);
            sqliteConnection_0.Open();
        }
        else if (sqliteConnection_0.State == ConnectionState.Broken)
        {
            sqliteConnection_0.Close();
            sqliteConnection_0.Open();
        }
        return sqliteConnection_0;
    }

    public static int smethod_1(string string_0)
    {
        int num2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static SQLiteDataReader smethod_10(string string_0, params SQLiteParameter[] values)
    {
        SQLiteDataReader reader2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static SQLiteDataReader smethod_11(string string_0, CommandType commandType_0, params SQLiteParameter[] values)
    {
        SQLiteDataReader reader2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
            {
                command.CommandType = commandType_0;
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

    public static DataTable smethod_12(string string_0)
    {
        DataSet dataSet = new DataSet();
        SQLiteCommand cmd = new SQLiteCommand(string_0, sqliteConnection_0);
        new SQLiteDataAdapter(cmd).Fill(dataSet);
        return dataSet.Tables[0];
    }

    public static DataTable smethod_13(string string_0, params SQLiteParameter[] values)
    {
        DataSet dataSet = new DataSet();
        SQLiteCommand cmd = new SQLiteCommand(string_0, sqliteConnection_0);
        cmd.Parameters.AddRange(values);
        new SQLiteDataAdapter(cmd).Fill(dataSet);
        return dataSet.Tables[0];
    }

    public static void smethod_14()
    {
        if (sqliteConnection_0 != null)
        {
            sqliteConnection_0.Close();
        }
    }

    public static int smethod_2(string string_0, params SQLiteParameter[] values)
    {
        int num;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static int smethod_3(string string_0, CommandType commandType_0, params SQLiteParameter[] values)
    {
        int num;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
            {
                command.CommandType = commandType_0;
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

    public static int smethod_4(string string_0, CommandType commandType_0, int int_0)
    {
        int num;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
            {
                command.CommandType = commandType_0;
                SQLiteParameter parameter = new SQLiteParameter("@rid", SqlDbType.Int) {
                    Value = int_0
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

    public static int smethod_5(string string_0)
    {
        int num2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static int smethod_6(string string_0)
    {
        int num;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static int smethod_7(string string_0, params SQLiteParameter[] values)
    {
        int num2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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

    public static int smethod_8(string string_0, CommandType commandType_0, params SQLiteParameter[] values)
    {
        int num2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
            {
                command.CommandType = commandType_0;
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

    public static SQLiteDataReader smethod_9(string string_0)
    {
        SQLiteDataReader reader2;
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(string_0, sqliteConnection_0))
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
}

