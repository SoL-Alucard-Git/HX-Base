namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class FPTKdal
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog log = LogUtil.GetLogger<FPTKdal>();

        public AisinoDataSet GetXSDJ(InvType fpType, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string invTypeStr = CommonTool.GetInvTypeStr(fpType);
            dictionary.Add("DJZL", invTypeStr);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.FPSCGetXSDJ", dictionary, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJMX(string XSDJBH, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.CFXSDJMXGet", dictionary, pagesize, pageno);
        }

        public bool UpdateXSDJ_KPZT(string BH, string KPZT)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", BH);
            dictionary.Add("KPZT", KPZT);
            if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.FPSC_UpdateXSDJ", dictionary) <= 0)
            {
                return false;
            }
            return true;
        }

        public void UpdateXSDJMX_FPDM_FPHM(FPGenerateResult result)
        {
            List<string> list = new List<string>();
            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
            for (int i = 0; i < result.ListXSDJ_MX.Count; i++)
            {
                string bH = "";
                int xH = 0;
                if (string.IsNullOrEmpty(result.YBH))
                {
                    bH = result.BH;
                    xH = result.ListXSDJ_MX[i].XH;
                }
                else
                {
                    bH = result.YBH;
                    xH = Convert.ToInt32(result.ListXSDJ_MX[i].Reserve);
                }
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("XSDJBH", bH);
                item.Add("XH", xH);
                item.Add("FPZL", result.DJZL);
                item.Add("FPDM", result.FPDM);
                item.Add("FPHM", result.FPHM);
                item.Add("SCFPXH", i + 1);
                list.Add("aisino.Fwkp.Wbjk.FPSC_UpdateXSDJMX");
                list2.Add(item);
            }
            if ((list.Count > 0) && (list2.Count > 0))
            {
                this.baseDAO.updateSQLTransaction(list.ToArray(), list2);
            }
        }

        public bool UpdateXSDJMX_FPDM_FPHM(string XSDJBH, int XH, string FPZL, string FPDM, long FPHM, int SCFPXH)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XSDJBH", XSDJBH);
            dictionary.Add("XH", XH);
            dictionary.Add("FPZL", FPZL);
            dictionary.Add("FPDM", FPDM);
            dictionary.Add("FPHM", FPHM);
            dictionary.Add("SCFPXH", SCFPXH);
            if (this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.FPSC_UpdateXSDJMX", dictionary) <= 0)
            {
                return false;
            }
            return true;
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                return PropValue.Pagesize;
            }
            set
            {
                PropValue.Pagesize = value;
            }
        }
    }
}

