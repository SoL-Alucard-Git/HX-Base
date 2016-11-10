namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetSP : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            //if (!CheckPermission.Check("SP"))
            //{
            //    return null;
            //}
            if (param.Length < 4)
            {
                throw new ArgumentException("参数错误,至少有4个参数");
            }
            string keyWord = ((string) param[0]).Trim().ToUpper();
            Type type = param[1].GetType();
            double result = 0.0;
            if (type == typeof(double))
            {
                result = (double) param[1];
            }
            else if (type == typeof(string))
            {
                double.TryParse((string) param[1], out result);
            }
            result = (double) param[1];
            int num2 = (int) param[2];
            int showCanselect = (int) param[3];
            string str2 = "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
            if ((param.Length >= 5) && ("" != ((string) param[4])))
            {
                str2 = (string) param[4];
            }
            string[] strArray = str2.Split(new char[] { ',' });
            List<object> list = new List<object>();
            string specialSP = string.Empty;
            if (param.Length >= 6)
            {
                specialSP = (string) param[5];
            }
            string specialFlag = string.Empty;
            if (param.Length >= 7)
            {
                specialFlag = (string) param[6];
            }
            switch (num2)
            {
                case 0:
                {
                    BMSPSelect select = new BMSPSelect(keyWord, result, showCanselect, specialSP, specialFlag);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }
                    DataTable sP = this.spManager.GetSP(select.SelectBM);
                    for (int i = 0; i < sP.Rows.Count; i++)
                    {
                        for (int k = i + 1; k < sP.Rows.Count; k++)
                        {
                            if (sP.Rows[k]["BM"].ToString() == sP.Rows[i]["BM"].ToString())
                            {
                                foreach (string str5 in sP.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                                {
                                    if (!string.IsNullOrEmpty(str5) && !sP.Rows[i]["YHZC_SLV"].ToString().Contains(str5))
                                    {
                                        sP.Rows[i]["YHZC_SLV"] = sP.Rows[i]["YHZC_SLV"].ToString() + "，" + str5;
                                    }
                                }
                                sP.Rows.RemoveAt(k);
                            }
                        }
                    }
                    DataRow row = sP.Rows[0];
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (sP.Columns.Contains(strArray[j]))
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
                    DataTable table2 = new DataTable();
                    if (specialSP == string.Empty)
                    {
                        table2 = this.spManager.QueryByKeyAndSlvSEL(keyWord, result, -1, 5);
                    }
                    else
                    {
                        table2 = this.spManager.QueryByKeyAndSpecialSPSEL(keyWord, specialSP, -1, 5);
                    }
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
                        BMSPSelect select2 = new BMSPSelect(keyWord, result, showCanselect, specialSP, specialFlag);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        DataTable table3 = this.spManager.GetSP(select2.SelectBM);
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
                case 2:
                {
                    DataTable table4 = new DataTable();
                    if (specialSP == string.Empty)
                    {
                        table4 = this.spManager.QueryByKeyAndSlvSEL(keyWord, result, 1, 1);
                    }
                    else
                    {
                        table4 = this.spManager.QueryByKeyAndSpecialSPSEL(keyWord, specialSP, 1, 1);
                    }
                    if (table4.Rows.Count <= 0)
                    {
                        return new object[] { "" };
                    }
                    DataRow row4 = table4.Rows[0];
                    for (int num9 = 0; num9 < strArray.Length; num9++)
                    {
                        if (table4.Columns.Contains(strArray[num9]))
                        {
                            list.Add(row4[strArray[num9]]);
                        }
                        else
                        {
                            list.Add(" ");
                        }
                    }
                    break;
                }
                default:
                    return new object[] { "第三个参数没有这种状态.仅有0与1" };
            }
            return list.ToArray();
        }
    }
}

