namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core;
    internal sealed class FPXiuFuSingleShareMethods : AbstractService
    {
        private XXFP dal = new XXFP(true);
        private ILog loger = LogUtil.GetLogger<FPXiuFuSingleShareMethods>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        protected override object[] doService(object[] param)
        {
            object[] objArray = new object[] { 0, "存库失败" };
            try
            {
                if ((param == null) || (param.Length < 2))
                {
                    this.loger.Error("单张发票修复发票参数个数不正确，参数个数为：" + param.Length);
                    objArray[0] = 0;
                    objArray[1] = "单张发票修复发票参数个数不正确，参数个数为：" + param.Length;
                    return objArray;
                }
                int length = param.Length;
                string str = "";
                int num2 = 0;
                string str2 = "6666666";
                string str3 = "6666666";
                DateTime now = DateTime.Now;
                if (length >= 1)
                {
                    str = param[0].ToString();
                }
                if (length >= 2)
                {
                    num2 = Tool.ObjectToInt(param[1].ToString());
                }
                if (length >= 3)
                {
                    str2 = param[2].ToString();
                }
                if (length >= 4)
                {
                    str3 = param[3].ToString();
                }
                if (length >= 5)
                {
                    now = Tool.ObjectToDateTime(param[4].ToString());
                }
                this.loger.Debug(string.Concat(new object[] { "发票代码：", str, "发票号码：", num2, "syh:", str2, "sxh:", str3 }));
                InvDetail detail = this.taxCard.QueryInvInfo(str, num2, str2, str3, now);
                if (this.taxCard.RetCode != 0)
                {
                    this.loger.Error("单张修复底层读取发票失败，错误号：" + this.taxCard.RetCode);
                    this.loger.Debug(string.Concat(new object[] { "发票代码：", str, "发票号码：", num2, "syh:", str2, "sxh:", str3, "开票日期：", now.ToString() }));
                    objArray[0] = 0;
                    objArray[1] = "单张修复底层读取发票失败，错误号：" + this.taxCard.RetCode;
                    return objArray;
                }
                Fpxx item = null;
                if (this.taxCard.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    item = new Fpxx();
                    item.RepairInv(detail, -1);
                }
                else
                {
                    item = Fpxx.DeSeriealize_Linux(ToolUtil.FromBase64String(detail.OldInvNo));
                }
                if (item != null)
                {
                    List<Fpxx> fpList = new List<Fpxx>();
                    item.dybz = true;
                    item.xfbz = true;
                    if (item.fplx == (FPLX)0x29)
                    {
                        item.gfdzdh = "";
                        item.gfyhzh = "";
                        item.xfdzdh = "";
                        item.xfyhzh = "";
                        item.mw = "";
                        item.kpr = "";
                        item.fhr = "";
                    }
                    fpList.Add(item);
                    this.dal.SaveXxfp(fpList);
                    objArray[0] = 1;
                    objArray[1] = "";
                    this.loger.Error("单张发票修复成功");
                    return objArray;
                }
                objArray[0] = 0;
                objArray[1] = "单张发票修复失败";
                this.loger.Error("单张发票修复失败");
                return objArray;
            }
            catch (Exception exception)
            {
                objArray[0] = 0;
                objArray[1] = exception.ToString();
                this.loger.Error("[FPXiuFuSingleShareMethods函数异常]" + exception.ToString());
                return objArray;
            }
        }
    }
}

