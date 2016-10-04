namespace ns6
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    internal class Class123
    {
        public Class123()
        {
            
        }

        public static DataTable smethod_0(string string_0)
        {
            DataTable table = new DataTable();
            IDbCommand command = null;
            IDbConnection connection = null;
            try
            {
                connection = new SQLiteConnection {
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
            string str2 = string.Empty;
            str2 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH", ""), @"Bin\cc3268.dll");
            return string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", str2);
        }
    }
}

