namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;

    public class YCCBService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            return this.StartYCCB(param);
        }

        private object[] StartYCCB(object[] para)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("CBBZ", "");
            dictionary.Add("Message", "");
            TaxCard card = TaxCardFactory.CreateTaxCard();
            card.get_TaxClock().ToString("yyyyMM");
            try
            {
                RemoteReport report = new RemoteReport {
                    bByAuto = true
                };
                if (report.CheckStatus())
                {
                    if (report.ISZP)
                    {
                        report.ProcessMsg(FPZL.ZP, OPTYPE.ZPCB);
                        if (report.ISCBZP)
                        {
                            PropertyUtil.SetValue(AttributeName.ZPCSDateName, report.dtZPLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISHY)
                    {
                        report.ProcessMsg(FPZL.HY, OPTYPE.HYCB);
                        if (report.ISCBHy)
                        {
                            PropertyUtil.SetValue(AttributeName.HYCSDateName, report.dtHYLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISJDC)
                    {
                        report.ProcessMsg(FPZL.JDC, OPTYPE.JDCCB);
                        if (report.ISCBJDC)
                        {
                            PropertyUtil.SetValue(AttributeName.JDCCSDateName, report.dtJDCLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISJSP)
                    {
                        report.ProcessMsg(FPZL.JSFP, OPTYPE.JSPCB);
                        if (report.ISCBJSFP)
                        {
                            PropertyUtil.SetValue(AttributeName.JSPCSDateName, report.dtJSPLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    PropertyUtil.Save();
                    if ((report.ISCBZP && report.ISCBHy) && (report.ISCBJDC && report.ISCBJSFP))
                    {
                        dictionary["CBBZ"] = "0";
                        dictionary["Message"] = "上报汇总成功！";
                    }
                    else
                    {
                        dictionary["CBBZ"] = "1";
                        if ((card.get_RetCode() == 250) || (card.get_RetCode() == 0x309))
                        {
                            dictionary["Message"] = "金税盘错误,错误码：" + card.get_RetCode();
                        }
                        else
                        {
                            string str = "本次上报汇总情况如下：\r\n";
                            if (card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP)
                            {
                                if (!report.ISCBZP)
                                {
                                    if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票及电子增值税普通发票抄报失败原因：";
                                    }
                                    else if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (!card.get_QYLX().ISPTFPDZ || (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x33))))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票抄报失败原因：";
                                    }
                                    else if ((!card.get_QYLX().ISPTFP && !card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)电子增值税普通发票抄报失败原因：";
                                    }
                                    str = str + report.CBZPMESS + "\r\n";
                                }
                                else
                                {
                                    if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票及电子增值税普通发票抄报成功：";
                                    }
                                    else if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (!card.get_QYLX().ISPTFPDZ || (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x33))))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票抄报成功：";
                                    }
                                    else if ((!card.get_QYLX().ISPTFP && !card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                    {
                                        str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)电子增值税普通发票抄报成功：";
                                    }
                                    str = str + report.CBZPMESS + "\r\n";
                                }
                            }
                            if (card.get_QYLX().ISHY)
                            {
                                if (!report.ISCBHy)
                                {
                                    str = (str + "(" + report.dtHYLastCSDate.ToString("yyyy年MM月") + "份征期)货物运输业增值税专用发票抄报失败原因：") + report.CBHYMESS + "\r\n";
                                }
                                else if (card.get_QYLX().ISHY)
                                {
                                    str = (str + "(" + report.dtHYLastCSDate.ToString("yyyy年MM月") + "份征期)货物运输业增值税专用发票抄报成功：") + report.CBHYMESS + "\r\n";
                                }
                            }
                            if (card.get_QYLX().ISJDC || card.get_QYLX().ISPTFPDZ)
                            {
                                if (card.get_QYLX().ISJDC)
                                {
                                    if (!str.Contains("机动车"))
                                    {
                                        str = str + "(" + report.dtJDCLastCSDate.ToString("yyyy年MM月") + "份征期)机动车销售统一发票";
                                    }
                                    else
                                    {
                                        str = str + "及机动车销售统一发票";
                                    }
                                }
                                if (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x33))
                                {
                                    if (!str.Contains("机动车"))
                                    {
                                        str = str + "(" + report.dtJDCLastCSDate.ToString("yyyy年MM月") + "份征期)电子增值税普通发票";
                                    }
                                    else
                                    {
                                        str = str + "及电子增值税普通发票";
                                    }
                                }
                                if (!report.ISCBJDC)
                                {
                                    str = (str + "抄报失败原因：") + report.CBJDCMESS + "\r\n";
                                }
                                else
                                {
                                    str = (str + "抄报成功：") + report.CBJDCMESS + "\r\n";
                                }
                            }
                            if (card.get_QYLX().ISPTFPJSP)
                            {
                                if (!report.ISCBJSFP)
                                {
                                    if (card.get_QYLX().ISPTFPJSP)
                                    {
                                        str = (str + "(" + report.dtJSPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税普通发票(卷票)抄报失败原因：") + report.CBJSPMESS + "\r\n";
                                    }
                                }
                                else if (card.get_QYLX().ISPTFPJSP)
                                {
                                    str = (str + "(" + report.dtJSPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税普通发票(卷票)抄报成功：") + report.CBJSPMESS + "\r\n";
                                }
                            }
                            dictionary["Message"] = str;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                dictionary["CBBZ"] = "1";
                dictionary["Message"] = exception.Message;
            }
            return new object[] { dictionary };
        }
    }
}

