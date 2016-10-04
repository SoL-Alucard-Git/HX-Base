namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    internal class DJHYdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        public DataTable CFHYCheck1(string BH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", BH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.CFHYCheck1", dictionary);
        }

        public DataTable CFHYCheck2(string BH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BHlike", BH + "_%");
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.CFHYCheck2", dictionary);
        }

        public DataTable CFHYCheck3(string BH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BHlike", BH + "_%");
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.CFHYCheck3", dictionary);
        }

        public AisinoDataSet GetDJMX(string CurrentBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", CurrentBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYXSDJMXGet", dictionary, pagesize, pageno);
        }

        public string GetReferDanJu(string BH, string DJBH)
        {
            StringBuilder builder = new StringBuilder();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", BH);
            dictionary.Add("DJBH", DJBH + "_%");
            DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.HYGetReferDanJu", dictionary);
            if (table.Rows.Count > 0)
            {
                if (table.Rows[0][0].Equals("YKP"))
                {
                    return ("ReferKped" + table.Rows[0][1].ToString());
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    builder.Append(table.Rows[i][0]);
                    builder.Append("\n");
                }
                return builder.ToString();
            }
            return "ERROR";
        }

        public AisinoDataSet GetYSDJ(string CurrentBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (CurrentBH.Contains("_") && CurrentBH.Contains("~"))
            {
                if (CurrentBH.LastIndexOf("_") > CurrentBH.LastIndexOf("~"))
                {
                    CurrentBH = CurrentBH.Substring(0, CurrentBH.LastIndexOf("_"));
                    dictionary.Add("BH", CurrentBH);
                    return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYGetYSDJFromCF", dictionary, pagesize, pageno);
                }
                dictionary.Add("BH", CurrentBH);
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYGetYSDJFromHB", dictionary, pagesize, pageno);
            }
            if (CurrentBH.Contains("_"))
            {
                CurrentBH = CurrentBH.Substring(0, CurrentBH.LastIndexOf("_"));
                dictionary.Add("BH", CurrentBH);
                return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYGetYSDJFromCF", dictionary, pagesize, pageno);
            }
            dictionary.Add("BH", CurrentBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYGetYSDJFromHB", dictionary, pagesize, pageno);
        }

        public AisinoDataSet GetYSDJMX(string OriginalBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("YSBH", OriginalBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYGetYSDJMX", dictionary, pagesize, pageno);
        }

        public DataTable HBHYCheck1(string BH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", BH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.HBHYCheck1", dictionary);
        }

        public DataTable HBHYCheck2(string BH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", BH);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.HBHYCheck2", dictionary);
        }

        public AisinoDataSet QueryXSDJ(string KeyWord, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("Key", "%" + KeyWord + "%");
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.HYQueryXSDJ", dictionary, pagesize, pageno);
        }

        public int SaveHuanYuan(string CFBH, string HBBH, string FlagChar, List<string> XhList)
        {
            if (FlagChar == "_")
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("BH", CFBH);
                dictionary.Add("BHlike", CFBH + "_%");
                return this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.HYFromChaiFen_new", dictionary);
            }
            if (FlagChar == "~")
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("BH", HBBH);
                return this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.HYFromHeBing2", dictionary2);
            }
            return 0;
        }
    }
}

