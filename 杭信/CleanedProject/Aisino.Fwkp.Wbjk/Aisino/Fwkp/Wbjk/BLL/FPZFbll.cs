namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using log4net;
    using System;

    internal class FPZFbll
    {
        private int _currentPage = 1;
        private FPZFdal fpzfDAL = new FPZFdal();
        private static ILog loger = LogUtil.GetLogger<FPZFbll>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public AisinoDataSet GetZuoFeiXSDJ(int pagesize, int pageno, bool IsAdmin, string mc)
        {
            return this.fpzfDAL.GetZuoFeiXSDJ(pagesize, pageno, IsAdmin, mc);
        }

        public bool IsAntiFake()
        {
            return (this.taxCard.get_StateInfo().CompanyType > 0);
        }

        public bool IsOldType()
        {
            return (this.taxCard.get_ECardType() == 0);
        }

        public bool IsRepReached()
        {
            return (this.taxCard.get_StateInfo().IsLockReached > 0);
        }

        public int RollBackState(string strInvType, string strInvCode, string strInvNum)
        {
            return this.fpzfDAL.RollBackRecord(strInvType, strInvCode, strInvNum);
        }

        public int WasteDataBase(string strInvType, string strInvCode, string strInvNum)
        {
            return this.fpzfDAL.ZuoFei(strInvType, strInvCode, strInvNum);
        }

        public bool WasteHard(string strInvType, string strInvCode, string strInvNum)
        {
            string str = this.taxCard.GetCardClock().ToString();
            FPLX fplx = 2;
            if (strInvType == "c")
            {
                fplx = 2;
            }
            else if (strInvType == "s")
            {
                fplx = 0;
            }
            else if (strInvType == "f")
            {
                fplx = 11;
            }
            else if (strInvType == "j")
            {
                fplx = 12;
            }
            InvoiceType invType = (InvoiceType) CommonTool.GetInvType(strInvType);
            InvoiceDataDetail detail = this.fpzfDAL.GetInvInfo(strInvType, strInvCode, strInvNum);
            string str2 = strInvCode;
            string str3 = strInvNum;
            string str4 = detail.m_dtInvDate.ToString();
            if (detail.m_strBuyerCode.Length == 0)
            {
                return false;
            }
            string dbAmount = detail.m_dbAmount;
            dbAmount = (dbAmount == "") ? "0.00" : dbAmount;
            string dbTaxRate = "0.00";
            if (detail.m_dbTaxRate.Length == 0)
            {
                if (fplx == 11)
                {
                    dbTaxRate = "0.05";
                }
                else
                {
                    dbTaxRate = "0.04";
                }
            }
            else
            {
                dbTaxRate = detail.m_dbTaxRate;
            }
            string dbTax = detail.m_dbTax;
            dbTax = (dbTax == "") ? "0.00" : dbTax;
            string strBuyerName = detail.m_strBuyerName;
            strBuyerName = (strBuyerName == "") ? " " : strBuyerName;
            byte[] sourceArray = Invoice.get_TypeByte();
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(ToolUtil.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            object[] objArray = new object[] { strInvType, str2, Convert.ToInt32(str3) };
            Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray)[0] as Fpxx;
            Invoice invoice = new Invoice(false, fpxx, buffer4, null);
            invoice.set_Hjje(fpxx.je);
            invoice.set_Hjse(fpxx.se);
            Fpxx fpData = invoice.GetFpData();
            fpData.kprq = fpxx.kprq;
            fpData.sLv = (detail.m_dbTaxRate.Length == 0) ? "0.00" : fpxx.sLv;
            byte[] bytes = ToolUtil.GetBytes(MD5_Crypt.GetHashStr("Aisino.Fwkp.Invoice" + invoice.get_Fpdm() + invoice.get_Fphm()));
            byte[] buffer6 = new byte[0x20];
            Array.Copy(bytes, 0, buffer6, 0, 0x20);
            byte[] buffer7 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer7, 0, 0x10);
            byte[] inArray = AES_Crypt.Encrypt(ToolUtil.GetBytes(DateTime.Now.ToString("F")), buffer6, buffer7);
            fpData.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(ToolUtil.GetBytes(Convert.ToBase64String(inArray) + invoice.get_Gfmc()), buffer6, buffer7));
            fpData.zfsj = str;
            fpData.zfbz = true;
            fpData.bszt = 0;
            return invoice.MakeCardInvoice(fpData, true);
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

