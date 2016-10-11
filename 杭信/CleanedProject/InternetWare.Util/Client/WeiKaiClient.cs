using Aisino.Fwkp.BusinessObject;
using Aisino.Fwkp.Fpkj.Common;
using Aisino.Fwkp.Fpkj.Form.FPZF;
using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;

namespace InternetWare.Util.Client
{
    internal class WeiKaiClient : BaseClient
    {
        private WeiKaiArgs _args;
        public WeiKaiClient(WeiKaiArgs args)
        {
            _args = args;
        }

        internal override ResultBase DoService()
        {
            WeiKaiChaXunResult fpInfo = new WeiKaiChaXunClient(new WeiKaiChaXunArgs() { FpType = _args.FpType }).DoService() as WeiKaiChaXunResult;
            ResultBase res = new ResultBase();
            if( !CheckFpInfo(fpInfo,out res))
            {
                return res;
            }
            else
            {
                return DoWeiKaiZuoFei(fpInfo);
            }
        }

        private bool CheckFpInfo(WeiKaiChaXunResult fpInfo,out ResultBase result)
        {
            result = new ResultBase();
            if (fpInfo.HasError == true)
            {
                result = new ResultBase(_args, null, true, fpInfo.ErrorInfo);
            }
            else if (fpInfo.FpHasNum <= 0)
            {
                result = new ResultBase(_args, null, true, new ErrorBase("无可用发票"));
            }
            else if (_args.Count <= 0)
            {
                result = new ResultBase(_args, null, true, new ErrorBase("作废数量填写有误"));
            }
            else if (_args.Count > fpInfo.FpHasNum)
            {
                result = new ResultBase(_args, null, true, new ErrorBase("作废数量超过现有数量"));
            }
            else
            {
                //没有问题时返回True，此时result的值已经没有意义了
                return true;
            }
            //只要产生错误最终就会进入此分支
            return false;
        }

        /// <summary>
        /// 对应 FaPiaoZuoFei_WeiKai 的 ZuoFeiMainFunction 方法
        /// </summary>
        private ResultBase DoWeiKaiZuoFei(WeiKaiChaXunResult fpInfo)
        {
            int zuoFeiNum = _args.Count;
            try
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0x1770;
                List<Fpxx> FpList = new List<Fpxx>();
                FaPiaoZuoFei_WeiKai form = new FaPiaoZuoFei_WeiKai();
                for (int i = 0; i < zuoFeiNum; i++)
                {
                    string dbfpzl = form.GetInvoiceType(CommonMethods.ParseFplx(_args.FpType)).dbfpzl;
                    string str3 = fpInfo.Fpdm;
                    string str4 = ShareMethods.FPHMTo8Wei(fpInfo.InvNum);
                    string str5 = form.IsEmpty_DengYu(form.TaxCardInstance.Address) + " " + form.TaxCardInstance.Telephone;
                    string str6 = form.IsEmpty_DengYu(form.TaxCardInstance.BankAccount);
                    string title = "正在作废发票代码：" + str3 + "发票号码：" + str4;
                    object[] param = new object[] { dbfpzl, str3, str4, DingYiZhiFuChuan1._UserMsg.MC, str5, str6 };
                    Fpxx item = form.BlankWasteTaxCardZuoFei(param);
                    if ((item == null) || !(item.retCode == "0000"))
                    {
                        break;
                    }
                    FpList.Add(item);
                    num++;
                }
                form.xxfpChaXunBll.SaveXxfp(FpList);
                num2 = zuoFeiNum - num;
                return new CountableResult(_args, null, false, null, zuoFeiNum, num, num2);
                //MessageManager.ShowMsgBox("FPZF-000010", new string[] { ZuoFeiNum.ToString(), num.ToString(), num2.ToString() });
            }
            catch (Exception exception)
            {
                //this.loger.Error("[ZuoFeiMainFunction函数异常]" + exception.ToString());
                return new ResultBase(_args, null, true, new ErrorBase($"错误类型：{exception.GetType()} || 错误信息:{exception.Message}"));
            }
        }
    }
}
