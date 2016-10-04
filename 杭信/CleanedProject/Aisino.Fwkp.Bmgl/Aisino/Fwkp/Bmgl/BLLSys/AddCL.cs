namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class AddCL : AbstractService
    {
        private BMCLManager clManager = new BMCLManager();

        protected override object[] doService(object[] param)
        {
            try
            {
                bool isupdate = false;
                string bM = string.Empty;
                string mC = string.Empty;
                if ((param != null) && (param.Length > 0))
                {
                    mC = (string) param[0];
                }
                if ((param != null) && (param.Length >= 4))
                {
                    mC = (string) param[0];
                    bM = (string) param[2];
                    isupdate = (bool) param[3];
                }
                string str3 = "MC,CPXH,CD,SCCJMC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
                if (((param != null) && (param.Length > 1)) && ("" != ((string) param[1])))
                {
                    str3 = (string) param[1];
                }
                string[] strArray = str3.Split(new char[] { ',' });
                List<object> list = new List<object>();
                bool sucdialog = false;
                BMCL_Edit edit = new BMCL_Edit(bM, mC, isupdate, sucdialog);
                if (DialogResult.OK == edit.ShowDialog())
                {
                    DataTable cL = this.clManager.GetCL(edit.retCode);
                    for (int i = 0; i < cL.Rows.Count; i++)
                    {
                        for (int k = i + 1; k < cL.Rows.Count; k++)
                        {
                            if (cL.Rows[k]["BM"].ToString() == cL.Rows[i]["BM"].ToString())
                            {
                                foreach (string str4 in cL.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                                {
                                    if (!string.IsNullOrEmpty(str4) && !cL.Rows[i]["YHZC_SLV"].ToString().Contains(str4))
                                    {
                                        cL.Rows[i]["YHZC_SLV"] = cL.Rows[i]["YHZC_SLV"].ToString() + "，" + str4;
                                    }
                                }
                                cL.Rows.RemoveAt(k);
                            }
                        }
                    }
                    DataRow row = cL.Rows[0];
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
                    return list.ToArray();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

