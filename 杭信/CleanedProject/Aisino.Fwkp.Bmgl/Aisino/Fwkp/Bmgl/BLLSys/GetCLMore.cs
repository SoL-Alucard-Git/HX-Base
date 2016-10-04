namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class GetCLMore : AbstractService
    {
        private BMCLManager clManager = new BMCLManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("CL"))
            {
                return null;
            }
            string keyWord = (string) param[0];
            int topNo = (int) param[1];
            DataTable table = this.clManager.AppendByKey(keyWord, topNo);
            table.Constraints.Clear();
            if (param.Length == 3)
            {
                string[] collection = ((string) param[2]).Split(new char[] { ',' });
                List<string> list = new List<string>();
                list.AddRange(collection);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (!list.Contains(table.Columns[j].ColumnName))
                    {
                        table.Columns.RemoveAt(j);
                        j--;
                    }
                }
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int k = i + 1; k < table.Rows.Count; k++)
                {
                    if (table.Rows[k]["BM"].ToString() == table.Rows[i]["BM"].ToString())
                    {
                        foreach (string str3 in table.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                        {
                            if (!string.IsNullOrEmpty(str3) && !table.Rows[i]["YHZC_SLV"].ToString().Contains(str3))
                            {
                                table.Rows[i]["YHZC_SLV"] = table.Rows[i]["YHZC_SLV"].ToString() + "，" + str3;
                            }
                        }
                        table.Rows.RemoveAt(k);
                    }
                }
            }
            return new object[] { table };
        }
    }
}

