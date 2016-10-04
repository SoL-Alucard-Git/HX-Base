namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.HzfpHy.BLL;
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class HzfpHyService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            string sQDH = param[0].ToString();
            IHZFPHY_SQD ihzfphy_sqd = new Aisino.Fwkp.HzfpHy.BLL.HZFPHY_SQD();
            IHZFPHY_SQD_MX ihzfphy_sqd_mx = new Aisino.Fwkp.HzfpHy.BLL.HZFPHY_SQD_MX();
            Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD hzfphy_sqd = ihzfphy_sqd.Select(sQDH);
            DataTable table = ihzfphy_sqd_mx.SelectList(sQDH);
            Fpxx fpxx = new Fpxx((FPLX)11, hzfphy_sqd.FPDM, hzfphy_sqd.FPHM) {
                kprq = hzfphy_sqd.TKRQ.ToString(),
                kpr = hzfphy_sqd.JBR,
                skr = hzfphy_sqd.SQDH,
                fhr = hzfphy_sqd.SQXZ,
                xfmc = hzfphy_sqd.XFMC,
                xfsh = hzfphy_sqd.XFSH,
                xfdzdh = hzfphy_sqd.SQRDH,
                gfmc = hzfphy_sqd.GFMC,
                gfsh = hzfphy_sqd.GFSH
            };
            if (hzfphy_sqd.XXBBH == null)
            {
                fpxx.hxm = "";
            }
            else
            {
                fpxx.hxm = hzfphy_sqd.XXBBH;
            }
            fpxx.cyrmc = hzfphy_sqd.XFMC;
            fpxx.cyrnsrsbh = hzfphy_sqd.XFSH;
            fpxx.spfmc = hzfphy_sqd.GFMC;
            fpxx.spfnsrsbh = hzfphy_sqd.GFSH;
            fpxx.fhrmc = hzfphy_sqd.FHFMC;
            fpxx.fhrnsrsbh = hzfphy_sqd.FHFSH;
            fpxx.shrmc = hzfphy_sqd.SHFMC;
            fpxx.shrnsrsbh = hzfphy_sqd.SHFSH;
            fpxx.je = hzfphy_sqd.HJJE.ToString();
            fpxx.se = hzfphy_sqd.HJSE.ToString();
            fpxx.sLv = hzfphy_sqd.SL.ToString();
            fpxx.kpjh = hzfphy_sqd.KPJH;
            fpxx.jqbh = hzfphy_sqd.JQBH;
            fpxx.czch = hzfphy_sqd.CZCH;
            fpxx.ccdw = hzfphy_sqd.CCDW;
            fpxx.yshwxx = hzfphy_sqd.YSHWXX;
            fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
            foreach (DataRow row in table.Rows)
            {
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item[(SPXX)0] = row["SPMC"].ToString();
                item[(SPXX)7] = row["JE"].ToString();
                item[(SPXX)11] = row["HSJBZ"].ToString();
                item[(SPXX)10] = row["FPHXZ"].ToString();
                fpxx.Qdxx.Add(item);
            }
            return new object[] { fpxx };
        }
    }
}

