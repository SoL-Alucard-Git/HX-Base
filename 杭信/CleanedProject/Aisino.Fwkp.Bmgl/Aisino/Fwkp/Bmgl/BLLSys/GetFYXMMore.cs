namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class GetFYXMMore : AbstractService
    {
        private BMFYXMManager fyxmmanager = new BMFYXMManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("FYXM"))
            {
                return null;
            }
            if (param == null)
            {
                string str = "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV";
                List<object> list = new List<object>();
                string[] strArray = str.Split(new char[] { ',' });
                for (int j = 0; j < strArray.Length; j++)
                {
                    list.Add(strArray[j]);
                }
                return list.ToArray();
            }
            string keyWord = (string) param[0];
            int topNo = (int) param[1];
            DataTable table = this.fyxmmanager.AppendByKey(keyWord, topNo);
            table.Constraints.Clear();
            if (param.Length == 3)
            {
                string[] collection = ((string) param[2]).Split(new char[] { ',' });
                List<string> list2 = new List<string>();
                list2.AddRange(collection);
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    if (!list2.Contains(table.Columns[k].ColumnName))
                    {
                        table.Columns.RemoveAt(k);
                        k--;
                    }
                }
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int m = i + 1; m < table.Rows.Count; m++)
                {
                    if (table.Rows[m]["BM"].ToString() == table.Rows[i]["BM"].ToString())
                    {
                        foreach (string str4 in table.Rows[m]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                        {
                            if (!string.IsNullOrEmpty(str4) && !table.Rows[i]["YHZC_SLV"].ToString().Contains(str4))
                            {
                                table.Rows[i]["YHZC_SLV"] = table.Rows[i]["YHZC_SLV"].ToString() + "，" + str4;
                            }
                        }
                        table.Rows.RemoveAt(m);
                    }
                }
            }
            return new object[] { table };
        }
    }
}

