namespace Aisino.Fwkp.Hzfp.BLL
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Hzfp.Common;
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class HzfpService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            string sQDH = param[0].ToString();
            IHZFP_SQD ihzfp_sqd = new Aisino.Fwkp.Hzfp.BLL.HZFP_SQD();
            IHZFP_SQD_MX ihzfp_sqd_mx = new Aisino.Fwkp.Hzfp.BLL.HZFP_SQD_MX();
            Aisino.Fwkp.Hzfp.Model.HZFP_SQD hzfp_sqd = ihzfp_sqd.Select(sQDH);
            DataTable table = ihzfp_sqd_mx.SelectList(sQDH);
            string str2 = hzfp_sqd.FPHM.ToString();
            if (Convert.ToDouble(str2) != 0.0)
            {
                while (str2.Length < 8)
                {
                    str2 = "0" + str2;
                }
            }
            Fpxx fpxx = new Fpxx(0, hzfp_sqd.FPDM, str2) {
                yysbz = hzfp_sqd.YYSBZ
            };
            if (((Convert.ToString(hzfp_sqd.SL) == "0.05") || (Convert.ToString(hzfp_sqd.SL) == "0.050")) && (hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0"))
            {
                fpxx.Zyfplx =(ZYFP_LX) 1;
            }
            if (Convert.ToString(hzfp_sqd.SL) == "0.015")
            {
                fpxx.Zyfplx = (ZYFP_LX)10;
            }
            fpxx.kprq = hzfp_sqd.TKRQ.ToString();
            fpxx.kpr = hzfp_sqd.JBR;
            fpxx.skr = hzfp_sqd.SQDH;
            fpxx.fhr = hzfp_sqd.SQXZ;
            fpxx.xfmc = hzfp_sqd.XFMC;
            fpxx.xfsh = hzfp_sqd.XFSH;
            fpxx.xfdzdh = hzfp_sqd.SQRDH;
            fpxx.gfmc = hzfp_sqd.GFMC;
            fpxx.gfsh = hzfp_sqd.GFSH;
            if (hzfp_sqd.XXBBH == null)
            {
                fpxx.hxm = "";
            }
            else
            {
                fpxx.hxm = hzfp_sqd.XXBBH;
            }
            fpxx.je = hzfp_sqd.HJJE.ToString();
            fpxx.se = hzfp_sqd.HJSE.ToString();
            if (hzfp_sqd.SL == -1.0)
            {
                fpxx.sLv = "";
            }
            else
            {
                fpxx.sLv = hzfp_sqd.SL.ToString();
            }
            fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
            foreach (DataRow row in table.Rows)
            {
                double num = 0.0;
                num = GetSafeData.ObjectToDouble(row["DJ"].ToString());
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item[(SPXX)7] = row["JE"].ToString();
                item[(SPXX)8] = row["SLV"].ToString();
                item[(SPXX)9] = row["SE"].ToString();
                item[(SPXX)2] = row["SPSM"].ToString();
                item[(SPXX)0] = row["SPMC"].ToString();
                item[(SPXX)3] = row["GGXH"].ToString();
                item[(SPXX)4] = row["JLDW"].ToString();
                item[(SPXX)6] = row["SL"].ToString();
                if (num == 0.0)
                {
                    item[(SPXX)5] = row["DJ"].ToString();
                }
                else
                {
                    item[(SPXX)5] = num.ToString();
                }
                item[(SPXX)11] = (row["HSJBZ"].ToString().Trim() == "True") ? "1" : "0";
                item[(SPXX)10] = row["FPHXZ"].ToString();
                item[(SPXX)1] = (row["SPBH"] == null) ? "" : row["SPBH"].ToString();
                item[(SPXX)14] = (row["XTHASH"] == null) ? "" : row["XTHASH"].ToString();
                fpxx.Qdxx.Add(item);
            }
            return new object[] { fpxx };
        }
    }
}

