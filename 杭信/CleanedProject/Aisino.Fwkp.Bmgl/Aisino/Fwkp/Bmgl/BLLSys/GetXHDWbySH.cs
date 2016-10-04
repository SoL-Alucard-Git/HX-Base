namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetXHDWbySH : AbstractService
    {
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private BMXHDWManager xhdwManager = new BMXHDWManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("XHDW"))
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
                    BMXHDWSelect select = new BMXHDWSelect(keyWord);
                    if (select.ShowDialog() != DialogResult.OK)
                    {
                        return null;
                    }
                    DataTable kH = this.xhdwManager.GetKH(select.SelectedBM);
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
                    DataTable table2 = this.xhdwManager.QueryByTaxCode(keyWord);
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
                        BMXHDWSelect select2 = new BMXHDWSelect(keyWord);
                        if (select2.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        DataTable table3 = this.xhdwManager.GetKH(select2.SelectedBM);
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
                default:
                    return new object[] { "第二个参数没有这种状态" };
            }
            return list.ToArray();
        }
    }
}

