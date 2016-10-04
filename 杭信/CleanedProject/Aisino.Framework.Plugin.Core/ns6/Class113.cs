namespace ns6
{
    using Aisino.Framework.Plugin.Core;
    using Npgsql;
    using System;
    using System.Data;

    internal class Class113
    {
        public Class113()
        {
            
        }

        public static DataTable smethod_0(string string_0)
        {
            DataTable table = new DataTable();
            IDbCommand command = null;
            IDbConnection connection = null;
            try
            {
                connection = new NpgsqlConnection {
                    ConnectionString = smethod_1()
                };
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = string_0;
                IDataReader reader = command.ExecuteReader();
                table.Load(reader);
                reader.Close();
            }
            catch (ConstraintException)
            {
            }
            catch (Exception exception)
            {
                throw new DateBaseException("", "Aisino.Framework.Plugin.Core.Util.RunSQL.Excute", "", exception);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        private static string smethod_1()
        {
            return "Server=127.0.0.1;Port=5432;User Id=fwkp;Password=AiSinO_618;Database=fwkp;";
        }
    }
}

