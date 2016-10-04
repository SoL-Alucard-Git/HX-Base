namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetCL : AbstractService
    {
        private BMCLManager clManager = new BMCLManager();
        private Dictionary<string, object> condition = new Dictionary<string, object>();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("CL"))
            {
                return null;
            }
            string keyWord = ((string) param[0]).Trim();
            int num = (int) param[1];
            string str2 = "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
            if (param.Length == 3)
            {
                str2 = (string) param[2];
            }
            string[] strArray = str2.Split(new char[] { ',' });
            List<object> list = new List<object>();
            switch (num)
            {
                case 0:
                {
                    BMCLSelect select = new BMCLSelect(keyWord);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }
                    DataTable cL = this.clManager.GetCL(select.SelectedBM);
                    DataRow row = cL.Rows[0];
                    for (int i = 0; i < cL.Rows.Count; i++)
                    {
                        for (int k = i + 1; k < cL.Rows.Count; k++)
                        {
                            if (cL.Rows[k]["BM"].ToString() == cL.Rows[i]["BM"].ToString())
                            {
                                foreach (string str3 in cL.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { (char)0xff0c }))
                                {
                                    if (!string.IsNullOrEmpty(str3) && !cL.Rows[i]["YHZC_SLV"].ToString().Contains(str3))
                                    {
                                        cL.Rows[i]["YHZC_SLV"] = cL.Rows[i]["YHZC_SLV"].ToString() + "，" + str3;
                                    }
                                }
                                cL.Rows.RemoveAt(k);
                            }
                        }
                    }
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (cL.Columns.Contains(strArray[j]))
                        {
                            list.Add(row[strArray[j]]);
                        }
                        else
                        {
                            list.Add(" ");
                        }
                    }
                    break;
                }
                case 1:
                {
                    DataTable table2 = this.clManager.QueryByMC(keyWord);
                    if ((table2.Rows.Count > 0) && table2.Rows[0]["WJ"].Equals(1))
                    {
                        DataRow row2 = table2.Rows[0];
                        for (int m = 0; m < strArray.Length; m++)
                        {
                            if (table2.Columns.Contains(strArray[m]))
                            {
                                list.Add(row2[strArray[m]]);
                            }
                            else
                            {
                                list.Add(" ");
                            }
                        }
                    }
                    else
                    {
                        BMCLSelect select2 = new BMCLSelect(keyWord);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        DataTable table3 = this.clManager.GetCL(select2.SelectedBM);
                        DataRow row3 = table3.Rows[0];
                        for (int n = 0; n < strArray.Length; n++)
                        {
                            if (table3.Columns.Contains(strArray[n]))
                            {
                                list.Add(row3[strArray[n]]);
                            }
                            else
                            {
                                list.Add(" ");
                            }
                        }
                    }
                    break;
                }
                default:
                    return new object[] { "第二个参数没有这种状态" };
            }
            return list.ToArray();
        }
    }
}

