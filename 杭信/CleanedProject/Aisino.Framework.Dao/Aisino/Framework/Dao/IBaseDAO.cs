namespace Aisino.Framework.Dao
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public interface IBaseDAO
    {
        void ClearResource();
        void Close();
        void Commit();
        ConnectionState GetConnState();
        int 未确认DAO方法1(string[] string_0, List<Dictionary<string, object>> parameter);
        int 未确认DAO方法2_疑似updateSQL(string string_0, Dictionary<string, object> parameter);
        int 未确认DAO方法3(string string_0, List<Dictionary<string, object>> parameter);
        void Open();
        ArrayList querySQL(string string_0, Dictionary<string, object> parameter);
        AisinoDataSet querySQLDataSet(string string_0, Dictionary<string, object> parameter, int int_0, int int_1);
        AisinoDataSet querySQLDataSetExtend(string string_0, Dictionary<string, object> parameter, int int_0, int int_1, int int_2);
        DataTable querySQLDataTable(string string_0);
        DataTable querySQLDataTable(string string_0, Dictionary<string, object> parameter);
        T queryValueSQL<T>(string string_0, Dictionary<string, object> parameter);
        void RollBack();
        int updateSQLTransaction(string[] string_0, List<Dictionary<string, object>> parameter);
    }
}

