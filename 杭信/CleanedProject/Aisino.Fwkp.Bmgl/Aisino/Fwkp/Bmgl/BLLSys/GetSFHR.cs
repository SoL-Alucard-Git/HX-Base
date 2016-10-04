namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetSFHR : AbstractService
    {
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private BMSFHRManager sfhrManager = new BMSFHRManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("SFHR"))
            {
                return null;
            }
            string keyWord = ((string) param[0]).Trim();
            int num = (int) param[1];
            string str2 = "MC,SH";
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
                    BMSFHRSelect select = new BMSFHRSelect(keyWord);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }
                    DataTable kH = this.sfhrManager.GetKH(select.SelectedBM);
                    DataRow row = kH.Rows[0];
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (kH.Columns.Contains(strArray[i]))
                        {
                            list.Add(row[strArray[i]]);
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
                    DataTable table2 = this.sfhrManager.AppendByKey(keyWord, 1);
                    if ((table2.Rows.Count > 0) && table2.Rows[0]["WJ"].Equals(1))
                    {
                        DataRow row2 = table2.Rows[0];
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (table2.Columns.Contains(strArray[j]))
                            {
                                list.Add(row2[strArray[j]]);
                            }
                            else
                            {
                                list.Add(" ");
                            }
                        }
                    }
                    else
                    {
                        BMSFHRSelect select2 = new BMSFHRSelect(keyWord);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        DataTable table3 = this.sfhrManager.GetKH(select2.SelectedBM);
                        DataRow row3 = table3.Rows[0];
                        for (int k = 0; k < strArray.Length; k++)
                        {
                            if (table3.Columns.Contains(strArray[k]))
                            {
                                list.Add(row3[strArray[k]]);
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
                    DataTable table4 = this.sfhrManager.AppendByKeyWJ(keyWord, 1);
                    if ((table4.Rows.Count <= 0) || !table4.Rows[0]["WJ"].Equals(1))
                    {
                        return new object[] { "" };
                    }
                    DataRow row4 = table4.Rows[0];
                    for (int m = 0; m < strArray.Length; m++)
                    {
                        if (table4.Columns.Contains(strArray[m]))
                        {
                            list.Add(row4[strArray[m]]);
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

