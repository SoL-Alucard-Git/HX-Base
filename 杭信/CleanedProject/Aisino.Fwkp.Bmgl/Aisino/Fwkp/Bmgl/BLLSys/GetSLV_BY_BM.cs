namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Data;

    internal sealed class GetSLV_BY_BM : AbstractService
    {
        private BMSPFLManager spflManager = new BMSPFLManager();

        protected override object[] doService(object[] param)
        {
            string bM = (string) param[0];
            DataTable table = this.spflManager.GetSLV_BY_BM(bM);
            table.Constraints.Clear();
            for (int i = 1; i < table.Rows.Count; i++)
            {
                foreach (string str2 in table.Rows[i]["YHZC_SLV"].ToString().Split(new char[] { '，', '、', '；', ',', ';' }))
                {
                    if (!string.IsNullOrEmpty(str2) && !table.Rows[0]["YHZC_SLV"].ToString().Contains(str2))
                    {
                        table.Rows[0]["YHZC_SLV"] = table.Rows[0]["YHZC_SLV"].ToString() + "，" + str2;
                    }
                }
            }
            while (table.Rows.Count > 1)
            {
                table.Rows.RemoveAt(1);
            }
            return new object[] { table };
        }
    }
}

