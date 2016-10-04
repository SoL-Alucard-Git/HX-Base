namespace Aisino.Fwkp.Xtsz.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using log4net;
    using System;

    public class TaxReportRegisterBLL
    {
        private ILog loger = LogUtil.GetLogger<InitialDAL>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private TaxStateInfo taxStateInfo = new TaxStateInfo();

        public TaxReportRegisterBLL()
        {
            this.taxStateInfo = this.taxCard.get_StateInfo();
        }

        public bool CheckIsExistPan()
        {
            try
            {
                if (this.taxStateInfo.IsTBEnable == 0)
                {
                    this.loger.Debug("不存在报税盘");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("存在报税盘");
            return true;
        }

        public bool CheckIsIncludeInvoice()
        {
            try
            {
                this.loger.Debug(this.taxStateInfo.ICBuyInv);
                if (this.taxStateInfo.ICBuyInv == 0)
                {
                    this.loger.Debug("IC卡上没有购票信息");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("IC卡上有购票信息");
            return true;
        }

        public bool CheckIsIncludeReturnInvoice()
        {
            try
            {
                if (this.taxStateInfo.ICRetInv == 0)
                {
                    this.loger.Debug("IC卡上没有退票信息");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("IC卡上有退票信息");
            return true;
        }

        public bool CheckIsIncludeTaxReportData()
        {
            try
            {
                if (this.taxStateInfo.ICRepInfo == 0)
                {
                    this.loger.Debug("IC卡上没有报税资料");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("IC卡上有报税资料");
            return true;
        }

        public bool CheckIsLocalCard()
        {
            try
            {
                if (this.taxCard.get_Machine() == Convert.ToInt32(this.taxStateInfo.ICCardNo))
                {
                    this.loger.Debug("是本机IC卡");
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("不是本机IC卡");
            return false;
        }

        public bool CheckIsLocalPan()
        {
            try
            {
                if (this.taxCard.get_Machine() == this.taxStateInfo.TBCardNo)
                {
                    this.loger.Debug("是本机报税盘");
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("不是本机报税盘");
            return false;
        }

        public bool GetRegeisteredSign()
        {
            try
            {
                this.loger.Debug(this.taxStateInfo.TBRegFlag);
                if (this.taxStateInfo.TBRegFlag == 0)
                {
                    this.loger.Debug("尚未注册");
                    return false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("已经注册");
            return true;
        }

        public bool IsIncludeSucceedSign()
        {
            try
            {
                if (this.taxStateInfo.TBRepDone == 0)
                {
                    this.loger.Debug("IC卡上没有报税成功标志");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("产生异常");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            this.loger.Debug("IC卡上有报税成功标志");
            return true;
        }

        public bool Register()
        {
            try
            {
                RegResult result = this.taxCard.TBRegister();
                if (result == null)
                {
                    this.loger.Debug("报税盘注册失败");
                    return false;
                }
                if (result == 1)
                {
                    this.loger.Debug("报税盘已经注册");
                    return false;
                }
                if (result == 2)
                {
                    this.loger.Debug("报税盘注册成功");
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            this.loger.Debug("其他情况:返回true");
            return true;
        }
    }
}

