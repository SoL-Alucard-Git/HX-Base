namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;

    public class QKService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            return this.StartQK(param);
        }

        private object[] StartQK(object[] param)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("QKBZ", "");
            dictionary.Add("Message", "");
            try
            {
                RemoteReport report = new RemoteReport();
                TaxCard card = TaxCardFactory.CreateTaxCard();
                card.get_TaxClock().ToString("yyyyMM");
                if (report.CheckStatus())
                {
                    if (report.ISZP)
                    {
                        report.ProcessMsg(FPZL.ZP, OPTYPE.ZPQK);
                        if (report.ISQKZP)
                        {
                            PropertyUtil.SetValue(AttributeName.ZPQKDateName, report.dtZPLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISHY)
                    {
                        report.ProcessMsg(FPZL.HY, OPTYPE.HYQK);
                        if (report.ISQKHY)
                        {
                            PropertyUtil.SetValue(AttributeName.HYQKDateName, report.dtHYLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISJDC)
                    {
                        report.ProcessMsg(FPZL.JDC, OPTYPE.JDCQK);
                        if (report.ISQKJDC)
                        {
                            PropertyUtil.SetValue(AttributeName.JDCQKDateName, report.dtJDCLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    if (report.ISJSP)
                    {
                        report.ProcessMsg(FPZL.JSFP, OPTYPE.JSPQK);
                        if (report.ISQKJSP)
                        {
                            PropertyUtil.SetValue(AttributeName.JSPQKDateName, report.dtJSPLastCSDate.ToString("yyyyMM"));
                        }
                    }
                    PropertyUtil.Save();
                    if ((report.ISQKZP && report.ISQKHY) && (report.ISQKJDC && report.ISQKJSP))
                    {
                        dictionary["QKBZ"] = "0";
                        dictionary["Message"] = "已成功执行清卡！";
                    }
                    else
                    {
                        dictionary["QKBZ"] = "1";
                        string str = "本次远程清卡情况如下：\r\n";
                        if (card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP)
                        {
                            if (!report.ISQKZP)
                            {
                                if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票及电子增值税普通发票清卡失败，原因：";
                                }
                                else if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (!card.get_QYLX().ISPTFPDZ || (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x33))))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票清卡失败，原因：";
                                }
                                else if ((!card.get_QYLX().ISPTFP && !card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)电子增值税普通发票清卡失败，原因：";
                                }
                                str = str + report.QKZPMESS + "\r\n";
                            }
                            else
                            {
                                if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票及电子增值税普通发票清卡成功：";
                                }
                                else if ((card.get_QYLX().ISPTFP || card.get_QYLX().ISZYFP) && (!card.get_QYLX().ISPTFPDZ || (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x33))))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税专普票清卡成功：";
                                }
                                else if ((!card.get_QYLX().ISPTFP && !card.get_QYLX().ISZYFP) && (card.get_QYLX().ISPTFPDZ && (card.get_InvEleKindCode() == 0x3d)))
                                {
                                    str = str + "(" + report.dtZPLastCSDate.ToString("yyyy年MM月") + "份征期)电子增值税普通发票清卡成功：";
                                }
                                str = str + report.QKZPMESS + "\r\n";
                            }
                        }
                        if (card.get_QYLX().ISHY)
                        {
                            if (!report.ISQKHY)
                            {
                                str = (str + "(" + report.dtHYLastCSDate.ToString("yyyy年MM月") + "份征期)货物运输业增值税专用发票清卡失败，原因：") + report.QKHYMESS + "\r\n";
                            }
                            else if (card.get_QYLX().ISHY)
                            {
                                str = (str + "(" + report.dtHYLastCSDate.ToString("yyyy年MM月") + "份征期)货物运输业增值税专用发票清卡成功：") + report.QKHYMESS + "\r\n";
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
                            if (!report.ISQKJDC)
                            {
                                str = (str + "清卡失败，原因：") + report.QKJDCMESS + "\r\n";
                            }
                            else if (card.get_QYLX().ISJDC || card.get_QYLX().ISPTFPDZ)
                            {
                                str = (str + "清卡成功：") + report.QKJDCMESS + "\r\n";
                            }
                        }
                        if (card.get_QYLX().ISPTFPJSP)
                        {
                            if (!report.ISQKJSP)
                            {
                                str = (str + "(" + report.dtJSPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税普通发票(卷票)清卡失败，原因：") + report.QKJSFPMESS + "\r\n";
                            }
                            else if (card.get_QYLX().ISPTFPJSP)
                            {
                                str = (str + "(" + report.dtJSPLastCSDate.ToString("yyyy年MM月") + "份征期)增值税普通发票(卷票)清卡成功：") + report.QKJSFPMESS + "\r\n";
                            }
                        }
                        dictionary["Message"] = str;
                    }
                }
            }
            catch (Exception exception)
            {
                dictionary["QKBZ"] = "1";
                dictionary["Message"] = exception.Message;
            }
            return new object[] { dictionary };
        }
    }
}

