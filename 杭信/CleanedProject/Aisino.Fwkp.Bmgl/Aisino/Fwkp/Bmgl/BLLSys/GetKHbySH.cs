namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetKHbySH : AbstractService
    {
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private BMKHManager khmanager = new BMKHManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("KH"))
            {
                return null;
            }
            string keyWord = ((string) param[0]).Trim();
            int num = (int) param[1];
            string str2 = "MC,SH,DZDH,YHZH,YJDZ";
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
                    BMKHSelect select = new BMKHSelect(keyWord);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return new object[] { "Error" };
                    }
                    DataTable kH = this.khmanager.GetKH(select.SelectedBM);
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
                    DataTable table2 = this.khmanager.QueryByTaxCode(keyWord);
                    if ((table2.Rows.Count <= 0) || !table2.Rows[0]["WJ"].Equals(1))
                    {
                        BMKHSelect select2 = new BMKHSelect(keyWord);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return new object[] { "Error" };
                        }
                        DataTable table3 = this.khmanager.GetKH(select2.SelectedBM);
                        DataRow row3 = table3.Rows[0];
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (table3.Columns.Contains(strArray[j]))
                            {
                                list.Add(row3[strArray[j]]);
                            }
                            else
                            {
                                list.Add(" ");
                            }
                        }
                    }
                    else
                    {
                        DataRow row2 = table2.Rows[0];
                        for (int k = 0; k < strArray.Length; k++)
                        {
                            if (table2.Columns.Contains(strArray[k]))
                            {
                                list.Add(row2[strArray[k]]);
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

