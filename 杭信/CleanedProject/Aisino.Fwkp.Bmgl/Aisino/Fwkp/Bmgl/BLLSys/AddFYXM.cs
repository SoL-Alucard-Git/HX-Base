namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class AddFYXM : AbstractService
    {
        private BMFYXMManager fyxmmanager = new BMFYXMManager();

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
                string str3 = "MC,BM,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
                if (((param != null) && (param.Length > 1)) && ("" != ((string) param[1])))
                {
                    str3 = (string) param[1];
                }
                string[] strArray = str3.Split(new char[] { ',' });
                List<object> list = new List<object>();
                bool sucdialog = false;
                BMFYXM_Edit edit = new BMFYXM_Edit(bM, mC, isupdate, sucdialog);
                if (DialogResult.OK == edit.ShowDialog())
                {
                    DataTable fYXM = this.fyxmmanager.GetFYXM(edit.retCode);
                    for (int i = 0; i < fYXM.Rows.Count; i++)
                    {
                        for (int k = i + 1; k < fYXM.Rows.Count; k++)
                        {
                            if (fYXM.Rows[k]["BM"].ToString() == fYXM.Rows[i]["BM"].ToString())
                            {
                                foreach (string str4 in fYXM.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                                {
                                    if (!string.IsNullOrEmpty(str4) && !fYXM.Rows[i]["YHZC_SLV"].ToString().Contains(str4))
                                    {
                                        fYXM.Rows[i]["YHZC_SLV"] = fYXM.Rows[i]["YHZC_SLV"].ToString() + "，" + str4;
                                    }
                                }
                                fYXM.Rows.RemoveAt(k);
                            }
                        }
                    }
                    DataRow row = fYXM.Rows[0];
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (fYXM.Columns.Contains(strArray[j]))
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

