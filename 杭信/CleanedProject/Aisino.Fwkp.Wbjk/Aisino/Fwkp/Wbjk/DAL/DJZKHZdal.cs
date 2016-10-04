namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class DJZKHZdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        public AisinoDataSet QueryXSDJ(string DJMonth, string DJType, string KeyWord, int pagesize, int pageno, int type)
        {
            int num;
            List<object> list = new List<object>();
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.ZKHZGetBH", null);
            List<object> list2 = new List<object>();
            for (num = 0; num < table.Rows.Count; num++)
            {
                string str = table.Rows[num][0].ToString();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("XSDJBH", str);
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.CFNeed", dictionary) <= 1)
                {
                    int num3 = this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.ZKHZGetCount", dictionary);
                    if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.ZKHZGetCountDJHXZ3", dictionary) != (num3 - 1))
                    {
                        list2.Add(str);
                    }
                }
            }
            List<object> list3 = new List<object>();
            if (type == 1)
            {
                for (num = 0; num < list2.Count; num++)
                {
                    string item = list2[num].ToString();
                    if (item.Contains(KeyWord))
                    {
                        list3.Add(item);
                    }
                }
            }
            AisinoDataSet set = new AisinoDataSet();
            if (list2.Count > 0)
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("DJYF", DJMonth);
                dictionary2.Add("DJZL", DJType);
                dictionary2.Add("Key", "%" + KeyWord + "%");
                if (type == 1)
                {
                    dictionary2.Add("BH", list3);
                    if (list3.Count > 0)
                    {
                        return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.ZKHZXSDJ_BH", dictionary2, pagesize, pageno);
                    }
                    dictionary2.Clear();
                    dictionary2.Add("DJYF", DJMonth);
                    dictionary2.Add("DJZL", DJType);
                    dictionary2.Add("Key", "NoExist");
                    dictionary2.Add("BH", list2);
                    return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.ZKHZXSDJ_GFSH", dictionary2, pagesize, pageno);
                }
                if (type == 2)
                {
                    dictionary2.Add("BH", list2);
                    return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.ZKHZXSDJ_GFMC", dictionary2, pagesize, pageno);
                }
                if (type == 3)
                {
                    dictionary2.Add("BH", list2);
                    set = this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.ZKHZXSDJ_GFSH", dictionary2, pagesize, pageno);
                }
            }
            return set;
        }

        public AisinoDataSet QueryXSDJMX(string SelectedBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", SelectedBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.ZKHZMXGet", dictionary, pagesize, pageno);
        }
    }
}

