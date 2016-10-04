namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetFYXM : AbstractService
    {
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private BMFYXMManager fyxmmanager = new BMFYXMManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("FYXM"))
            {
                return null;
            }
            string str = "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
            List<object> list = new List<object>();
            if (param == null)
            {
                string[] strArray = str.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    list.Add(strArray[i]);
                }
                return list.ToArray();
            }
            string keyWord = ((string) param[0]).Trim();
            int num2 = (int) param[1];
            if (param.Length == 3)
            {
                str = (string) param[2];
            }
            string[] strArray2 = str.Split(new char[] { ',' });
            switch (num2)
            {
                case 0:
                {
                    BMFYXMSelect select = new BMFYXMSelect(keyWord);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }
                    DataTable fYXM = this.fyxmmanager.GetFYXM(select.SelectedBM);
                    DataRow row = fYXM.Rows[0];
                    for (int j = 0; j < fYXM.Rows.Count; j++)
                    {
                        for (int m = j + 1; m < fYXM.Rows.Count; m++)
                        {
                            if (fYXM.Rows[m]["BM"].ToString() == fYXM.Rows[j]["BM"].ToString())
                            {
                                foreach (string str3 in fYXM.Rows[m]["YHZC_SLV"].ToString().Split(new char[] { (char)0xff0c }))
                                {
                                    if (!string.IsNullOrEmpty(str3) && !fYXM.Rows[j]["YHZC_SLV"].ToString().Contains(str3))
                                    {
                                        fYXM.Rows[j]["YHZC_SLV"] = fYXM.Rows[j]["YHZC_SLV"].ToString() + "，" + str3;
                                    }
                                }
                                fYXM.Rows.RemoveAt(m);
                            }
                        }
                    }
                    for (int k = 0; k < strArray2.Length; k++)
                    {
                        if (fYXM.Columns.Contains(strArray2[k]))
                        {
                            list.Add(row[strArray2[k]]);
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
                    DataTable table2 = this.fyxmmanager.AppendByKey(keyWord, 1);
                    if ((table2.Rows.Count > 0) && table2.Rows[0]["WJ"].Equals(1))
                    {
                        DataRow row2 = table2.Rows[0];
                        for (int n = 0; n < strArray2.Length; n++)
                        {
                            if (table2.Columns.Contains(strArray2[n]))
                            {
                                list.Add(row2[strArray2[n]]);
                            }
                            else
                            {
                                list.Add(" ");
                            }
                        }
                    }
                    else
                    {
                        BMFYXMSelect select2 = new BMFYXMSelect(keyWord);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        DataTable table3 = this.fyxmmanager.GetFYXM(select2.SelectedBM);
                        DataRow row3 = table3.Rows[0];
                        for (int num7 = 0; num7 < strArray2.Length; num7++)
                        {
                            if (table3.Columns.Contains(strArray2[num7]))
                            {
                                list.Add(row3[strArray2[num7]]);
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
                    DataTable table4 = this.fyxmmanager.AppendByKeyWJ(keyWord, 1);
                    if ((table4.Rows.Count <= 0) || !table4.Rows[0]["WJ"].Equals(1))
                    {
                        return new object[] { "" };
                    }
                    DataRow row4 = table4.Rows[0];
                    for (int num8 = 0; num8 < strArray2.Length; num8++)
                    {
                        if (table4.Columns.Contains(strArray2[num8]))
                        {
                            list.Add(row4[strArray2[num8]]);
                        }
                        else
                        {
                            list.Add(" ");
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

