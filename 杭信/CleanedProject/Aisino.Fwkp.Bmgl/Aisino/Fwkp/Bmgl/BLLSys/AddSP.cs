namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class AddSP : AbstractService
    {
        private BLL.BMSPManager spManager = new BLL.BMSPManager();

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
                string str3 = "BM,MC,JM,SLV,SPSM,GGXH,JLDW,DJ,HSJBZ,XTHASH,HYSY,SPFL,YHZC,SPFL_ZZSTSGL,YHZC_SLV,YHZCMC";
                if (((param != null) && (param.Length > 1)) && ("" != ((string) param[1])))
                {
                    str3 = (string) param[1];
                }
                string[] strArray = str3.Split(new char[] { ',' });
                List<object> list = new List<object>();
                if (bM != "")
                {
                    isupdate = true;
                }
                bool sucdialog = false;
                BMSP_Edit edit = new BMSP_Edit(bM, mC, isupdate, sucdialog);
                DAL.BMSPManager manager = new DAL.BMSPManager();
                if (manager.GetXTCodeByName(mC) != string.Empty)
                {
                    edit = new BMSP_Edit(manager.GetXTCodeByName(mC), mC, true, sucdialog);
                }
                else
                {
                    edit = new BMSP_Edit(bM, mC, isupdate, sucdialog);
                }
                if (DialogResult.OK == edit.ShowDialog())
                {
                    DataTable sP = this.spManager.GetSP(edit.retCode);
                    for (int i = 0; i < sP.Rows.Count; i++)
                    {
                        for (int k = i + 1; k < sP.Rows.Count; k++)
                        {
                            if (sP.Rows[k]["BM"].ToString() == sP.Rows[i]["BM"].ToString())
                            {
                                foreach (string str4 in sP.Rows[k]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                                {
                                    if (!string.IsNullOrEmpty(str4) && !sP.Rows[i]["YHZC_SLV"].ToString().Contains(str4))
                                    {
                                        sP.Rows[i]["YHZC_SLV"] = sP.Rows[i]["YHZC_SLV"].ToString() + "，" + str4;
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

