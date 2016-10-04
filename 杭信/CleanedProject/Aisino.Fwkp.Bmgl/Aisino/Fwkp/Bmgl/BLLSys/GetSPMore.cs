namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class GetSPMore : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("SP"))
            {
                return null;
            }
            string keyWord = (string) param[0];
            int topNo = (int) param[1];
            double result = -1.0;
            if (param.Length >= 4)
            {
                Type type = param[3].GetType();
                if (type == typeof(double))
                {
                    result = (double) param[3];
                }
                else if (type == typeof(string))
                {
                    double.TryParse((string) param[3], out result);
                }
            }
            string specialSP = string.Empty;
            if (param.Length >= 5)
            {
                specialSP = (string) param[4];
            }
            string specialFlag = string.Empty;
            if (param.Length >= 6)
            {
                specialFlag = (string) param[5];
            }
            DataTable table = this.spManager.AppendByKey(keyWord, topNo, result, specialSP, specialFlag);
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
                        foreach (string str5 in table.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                        {
                            if (!string.IsNullOrEmpty(str5) && !table.Rows[i]["YHZC_SLV"].ToString().Contains(str5))
                            {
                                table.Rows[i]["YHZC_SLV"] = table.Rows[i]["YHZC_SLV"].ToString() + "，" + str5;
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

