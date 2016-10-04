namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class RemoteReport
    {
        private byte[] _bytesHZSJ;
        private Dictionary<string, string> _dictSenderToServer = new Dictionary<string, string>();
        private bool _isJDCtwice;
        public bool bByAuto;
        private string cbHyMess;
        private string cbJdcMess;
        private string cbJspMess;
        private string cbZpMess;
        private string code;
        private bool isCbHy;
        private bool isCbJdc;
        private bool isCbJSFP;
        private bool isCbZp;
        private bool isHy;
        private bool isJdc;
        private bool isJsp;
        private bool isQkHy;
        private bool isQkJdc;
        private bool isQkJsp;
        private bool isQkZp;
        private bool isZp;
        private ILog loger = LogUtil.GetLogger<RemoteReport>();
        private string qkHyMess;
        private string qkJdcMess;
        private string qkJSFPMess;
        private string qkZpMess;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        private bool CheckIfCanCS(int fpType)
        {
            bool flag = false;
            try
            {
                this.loger.Debug("【专普抄税判断条件：上次抄税时间】：" + PropertyUtil.GetValue("Aisino.Fwkp.Yccb.cbrq", "198801"));
                DateTime cardClock = this.taxCard.GetCardClock();
                int num = Convert.ToInt32(cardClock.ToString("yyyyMM"));
                this.loger.Debug("【抄税判断条件：金税盘当前时间】：" + cardClock.ToString());
                if (fpType == FPZL.ZP)
                {
                    List<string> cSDate = this.taxCard.GetCSDate(0);
                    DateTime time2 = Convert.ToDateTime(cSDate[1]);
                    this.loger.Debug("【抄税判断条件：专普锁死期】：" + cSDate[1]);
                    int num2 = Convert.ToInt32(time2.ToString("yyyyMM"));
                    DateTime time3 = Convert.ToDateTime(cSDate[0]);
                    this.loger.Debug("【抄税判断条件：专普上次抄税期】：" + time3.ToString());
                    int num3 = Convert.ToInt32(time3.ToString("yyyyMM"));
                    if ((num3 >= num) && ((num3 != num) || (num != num2)))
                    {
                        return flag;
                    }
                    return true;
                }
                if (((fpType != FPZL.HY) && (fpType != FPZL.JDC)) && (fpType != FPZL.JSFP))
                {
                    return flag;
                }
                foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
                {
                    if (info.InvType == fpType)
                    {
                        DateTime time4 = Convert.ToDateTime(info.LockedDate);
                        int num4 = Convert.ToInt32(time4.ToString("yyyyMM"));
                        if (fpType == FPZL.HY)
                        {
                            this.loger.Debug("【抄税判断条件：货运机动车锁死期】：" + time4.ToString());
                            this.loger.Debug("【抄税判断条件：货运机动车上次抄税期】：" + info.LastRepDate);
                        }
                        else if (fpType == FPZL.JDC)
                        {
                            this.loger.Debug("【抄税判断条件：机动车锁死期】：" + time4.ToString());
                            this.loger.Debug("【抄税判断条件：机动车上次抄税期】：" + info.LastRepDate);
                        }
                        int num5 = Convert.ToInt32(Convert.ToDateTime(info.LastRepDate).ToString("yyyyMM"));
                        if ((num5 < num) || ((num5 == num) && (num == num4)))
                        {
                            flag = true;
                        }
                    }
                    if ((info.InvType == FPZL.DZFP) && (this.taxCard.get_InvEleKindCode() == 0x33))
                    {
                        List<string> list2 = this.taxCard.GetCSDate(0x33);
                        DateTime time5 = Convert.ToDateTime(list2[1]);
                        this.loger.Debug("【抄税判断条件：电子发票锁死期】：" + time5.ToString());
                        int num6 = Convert.ToInt32(time5.ToString("yyyyMM"));
                        DateTime time6 = Convert.ToDateTime(list2[0]);
                        this.loger.Debug("【抄税判断条件：电子发票上次抄税期】：" + time6.ToString());
                        int num7 = Convert.ToInt32(time6.ToString("yyyyMM"));
                        if ((num7 < num) || ((num7 == num) && (num6 == num)))
                        {
                            flag = true;
                        }
                    }
                    if (info.InvType == FPZL.JSFP)
                    {
                        List<string> list3 = this.taxCard.GetCSDate(0x29);
                        DateTime time7 = Convert.ToDateTime(list3[1]);
                        this.loger.Debug("【抄税判断条件：卷式发票锁死期】：" + time7.ToString());
                        int num8 = Convert.ToInt32(time7.ToString("yyyyMM"));
                        DateTime time8 = Convert.ToDateTime(list3[0]);
                        this.loger.Debug("【抄税判断条件：卷式发票上次抄税期】：" + time8.ToString());
                        int num9 = Convert.ToInt32(time8.ToString("yyyyMM"));
                        if ((num9 < num) || ((num9 == num) && (num8 == num)))
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                flag = false;
            }
            return flag;
        }

        private bool CheckIfCanQK(int fpType)
        {
            bool flag = false;
            try
            {
                DateTime cardClock = this.taxCard.GetCardClock();
                this.loger.Debug("【清卡-金税盘当前日期】：" + cardClock.ToShortDateString());
                int num = Convert.ToInt32(cardClock.ToString("yyyyMM"));
                if (fpType == FPZL.ZP)
                {
                    List<string> cSDate = this.taxCard.GetCSDate(0);
                    this.loger.Debug("【专普清卡-锁死期】：" + cSDate[1]);
                    if (Convert.ToInt32(Convert.ToDateTime(cSDate[1]).ToString("yyyyMM")) <= num)
                    {
                        flag = true;
                    }
                    return flag;
                }
                if (((fpType != FPZL.HY) && (fpType != FPZL.JDC)) && (fpType != FPZL.JSFP))
                {
                    return flag;
                }
                foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
                {
                    if (info.InvType == fpType)
                    {
                        if (fpType == FPZL.HY)
                        {
                            this.loger.Debug("货运清卡-锁死期：" + info.LockedDate);
                        }
                        else if (fpType == FPZL.JDC)
                        {
                            this.loger.Debug("机动车清卡-锁死期：" + info.LockedDate);
                        }
                        if (Convert.ToInt32(Convert.ToDateTime(info.LockedDate).ToString("yyyyMM")) <= num)
                        {
                            flag = true;
                        }
                    }
                    if ((info.InvType == FPZL.DZFP) && (this.taxCard.get_InvEleKindCode() == 0x33))
                    {
                        List<string> list2 = this.taxCard.GetCSDate(0x33);
                        int num4 = Convert.ToInt32(Convert.ToDateTime(list2[1]).ToString("yyyyMM"));
                        this.loger.Debug("电子清卡-锁死期：" + list2[1]);
                        if (num4 <= num)
                        {
                            flag = true;
                        }
                    }
                    if (info.InvType == FPZL.JSFP)
                    {
                        List<string> list3 = this.taxCard.GetCSDate(0x29);
                        int num5 = Convert.ToInt32(Convert.ToDateTime(list3[1]).ToString("yyyyMM"));
                        this.loger.Debug("卷式清卡-锁死期：" + list3[1]);
                        if (num5 <= num)
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                flag = false;
            }
            return flag;
        }

        private bool CheckIFCardCanUse()
        {
            bool flag = false;
            try
            {
                DateTime cardClock = this.taxCard.GetCardClock();
                if (this.taxCard.get_CardBeginDate() <= cardClock)
                {
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
            return flag;
        }

        private bool CheckQKCondition(int fpType)
        {
            bool flag = false;
            try
            {
                string hYCSDateName = "";
                string hYQKDateName = "";
                switch (fpType)
                {
                    case 11:
                        hYCSDateName = AttributeName.HYCSDateName;
                        hYQKDateName = AttributeName.HYQKDateName;
                        break;

                    case 12:
                        hYCSDateName = AttributeName.JDCCSDateName;
                        hYQKDateName = AttributeName.JDCQKDateName;
                        break;

                    case 0x29:
                        hYCSDateName = AttributeName.JSPCSDateName;
                        hYQKDateName = AttributeName.JSPQKDateName;
                        break;

                    case 0:
                        hYCSDateName = AttributeName.ZPCSDateName;
                        hYQKDateName = AttributeName.ZPQKDateName;
                        break;
                }
                string s = PropertyUtil.GetValue(hYCSDateName, "188801");
                string str4 = PropertyUtil.GetValue(hYQKDateName, "188801");
                this.loger.Debug(hYCSDateName + ":" + s + "    " + hYQKDateName + ":" + str4);
                if (int.Parse(s) > int.Parse(str4))
                {
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("CheckQKCondition异常：" + exception.ToString());
            }
            return flag;
        }

        public bool CheckStatus()
        {
            try
            {
                this.isZp = (this.taxCard.get_QYLX().ISPTFP || this.taxCard.get_QYLX().ISZYFP) || (this.taxCard.get_QYLX().ISPTFPDZ && (this.taxCard.get_InvEleKindCode() == 0x3d));
                this.isHy = this.taxCard.get_QYLX().ISHY;
                this.isJdc = this.taxCard.get_QYLX().ISJDC || (this.taxCard.get_QYLX().ISPTFPDZ && (this.taxCard.get_InvEleKindCode() == 0x33));
                this.isJsp = this.taxCard.get_QYLX().ISPTFPJSP;
                if (((!this.taxCard.get_QYLX().ISPTFP && !this.taxCard.get_QYLX().ISZYFP) && (!this.taxCard.get_QYLX().ISHY && !this.taxCard.get_QYLX().ISJDC)) && (!this.taxCard.get_QYLX().ISPTFPDZ && !this.taxCard.get_QYLX().ISPTFPJSP))
                {
                    this.loger.Error("无法进行上报汇总或者清卡操作：没有票种授权");
                    this.cbHyMess = "无法进行上报汇总：没有票种授权";
                    this.cbJdcMess = "无法进行上报汇总：没有票种授权";
                    this.cbZpMess = "无法进行上报汇总：没有票种授权";
                    this.cbJspMess = "无法进行上报汇总：没有票种授权";
                    this.qkHyMess = "无法进行清卡操作：没有票种授权";
                    this.qkJdcMess = "无法进行清卡操作：没有票种授权";
                    this.qkZpMess = "无法进行清卡操作：没有票种授权";
                    return false;
                }
                if ((this.taxCard.get_QYLX().ISJDC || this.taxCard.get_QYLX().ISPTFPDZ) && this.taxCard.get_QYLX().ISPTFPJSP)
                {
                    this._isJDCtwice = true;
                }
                this.isQkZp = !this.isZp;
                this.isQkHy = !this.isHy;
                this.isQkJdc = !this.isJdc;
                this.isQkJsp = !this.isJsp;
                this.isCbZp = !this.isZp;
                this.isCbHy = !this.isHy;
                this.isCbJdc = !this.isJdc;
                this.isCbJSFP = !this.isJsp;
                return true;
            }
            catch (Exception exception)
            {
                this.code = exception.Message;
                this.loger.Debug(exception.ToString());
                MessageManager.ShowMsgBox(this.code);
                return false;
            }
        }

        public string GetCode()
        {
            return this.code;
        }

        private byte[] GetFPHZB(byte[] byteOrigin, string fptype)
        {
            byte[] inArray = new byte[320];
            try
            {
                for (int i = 0; i < 320; i++)
                {
                    if (i < 0x138)
                    {
                        inArray[i] = byteOrigin[i];
                    }
                    else
                    {
                        inArray[i] = 0xff;
                    }
                }
                List<int> list = new List<int>();
                if (fptype == OPTYPE.HYCB)
                {
                    if (!this.taxCard.get_QYLX().ISHY)
                    {
                        this.loger.Debug("组装发票汇总信息：没有货运授权，不抄报货运。");
                        return inArray;
                    }
                    list.Add(11);
                }
                else if (fptype == OPTYPE.JDCCB)
                {
                    if (!this.taxCard.get_QYLX().ISJDC && !this.taxCard.get_QYLX().ISPTFPDZ)
                    {
                        this.loger.Debug("组装发票汇总信息：没有机动车和电子授权，不抄报机动车电子。");
                        return inArray;
                    }
                    if (this.taxCard.get_QYLX().ISJDC)
                    {
                        list.Add(12);
                    }
                    if (this.taxCard.get_QYLX().ISPTFPDZ)
                    {
                        list.Add(0x33);
                    }
                }
                else if (fptype == OPTYPE.JSPCB)
                {
                    if (!this.taxCard.get_QYLX().ISPTFPJSP)
                    {
                        this.loger.Debug("组装发票汇总信息：没有机动车和电子授权，不抄报机动车电子。");
                        return inArray;
                    }
                    if (this.taxCard.get_QYLX().ISPTFPJSP)
                    {
                        list.Add(0x29);
                    }
                }
                for (int j = 0; j < list.Count; j++)
                {
                    inArray[0x138 + j] = (byte) list[j];
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("组装发票汇总信息byte数组异常：" + exception.ToString());
            }
            this.loger.Debug("组装发票汇总信息：组装票种为：" + fptype + "    组装后数据为：" + Convert.ToBase64String(inArray));
            return inArray;
        }

        private DateTime GetLastRepDate(int fptye)
        {
            DateTime time = new DateTime(0x758, 11, 1);
            try
            {
                List<string> cSDate = new List<string>();
                switch (fptye)
                {
                    case 11:
                        cSDate = this.taxCard.GetCSDate(11);
                        if ((cSDate == null) || (cSDate.Count < 1))
                        {
                            throw new Exception("GetCSDate货运异常");
                        }
                        break;

                    case 12:
                        if (!this.taxCard.get_QYLX().ISJDC)
                        {
                            goto Label_00E7;
                        }
                        cSDate = this.taxCard.GetCSDate(12);
                        goto Label_00F5;

                    case 0x29:
                        cSDate = this.taxCard.GetCSDate(0x29);
                        if ((cSDate == null) || (cSDate.Count < 1))
                        {
                            throw new Exception("GetCSDate卷式票异常");
                        }
                        goto Label_0162;

                    case 0:
                        this.loger.Debug("dtZPLastCSDate:" + this.taxCard.get_LastRepDate().ToString());
                        goto Label_01B1;

                    default:
                        goto Label_01B1;
                }
                this.loger.Debug("dtHYLastCSDate:" + DateTime.Parse(cSDate[0]).ToString());
                goto Label_01B1;
            Label_00E7:
                cSDate = this.taxCard.GetCSDate(0x29);
            Label_00F5:
                if ((cSDate == null) || (cSDate.Count < 1))
                {
                    throw new Exception("GetCSDate机动车或电子异常");
                }
                this.loger.Debug("dtJDCLastCSDate:" + DateTime.Parse(cSDate[0]).ToString());
                goto Label_01B1;
            Label_0162:
                time = DateTime.Parse(cSDate[0]);
                this.loger.Debug("dtJSPLastCSDate:" + time.ToString());
            }
            catch (Exception exception)
            {
                this.loger.Error("GetLastRepDate异常：" + exception.ToString());
            }
        Label_01B1:
            return time.AddMonths(-1);
        }

        public string GetRequestMsg(int fpzl, string optype)
        {
            try
            {
                XmlElement element;
                XmlElement element2;
                XmlElement element3;
                this.loger.Debug("进入：" + optype);
                DateTime time = new DateTime();
                DateTime time2 = new DateTime();
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                List<string> cSDate = new List<string>();
                if (this.taxCard.get_QYLX().ISPTFPDZ)
                {
                    this.loger.Debug("电子发票进入");
                    cSDate = this.taxCard.GetCSDate(0x33);
                    this.loger.Debug("电子发票离开");
                }
                if (fpzl == FPZL.ZP)
                {
                    this.loger.Debug("进入专普：" + optype);
                    element = document.CreateElement("FPXT");
                    document.AppendChild(element);
                    element2 = document.CreateElement("INPUT");
                    element.AppendChild(element2);
                    element3 = document.CreateElement("NSRSBH");
                    element3.InnerText = this.taxCard.get_TaxCode();
                    element.AppendChild(element3);
                    element3 = document.CreateElement("KPJH");
                    element3.InnerText = this.taxCard.get_Machine().ToString();
                    element.AppendChild(element3);
                    element3 = document.CreateElement("SBBH");
                    element3.InnerText = this.taxCard.GetInvControlNum();
                    element.AppendChild(element3);
                    element3 = document.CreateElement("HASH");
                    element3.InnerText = this.taxCard.GetHashTaxCode();
                    element.AppendChild(element3);
                    if (optype == OPTYPE.ZPCB)
                    {
                        element3 = document.CreateElement("ISLOCK");
                        element3.InnerText = (this.taxCard.get_StateInfo().IsLockReached == 0) ? "N" : "Y";
                        element.AppendChild(element3);
                        element3 = document.CreateElement("SSQ");
                        element3.InnerText = this.taxCard.get_StateInfo().LockedDays.ToString();
                        element.AppendChild(element3);
                        element3 = document.CreateElement("ZQBZ");
                        element3.InnerText = (this.taxCard.get_StateInfo().IsRepReached == 0) ? "N" : "Y";
                        element.AppendChild(element3);
                        int length = 0x138;
                        byte[] sourceArray = this.taxCard.RemoteRep(fpzl, ref time, ref time2);
                        if (this.taxCard.get_RetCode() != 0)
                        {
                            return "";
                        }
                        byte[] destinationArray = new byte[0x138];
                        Array.Copy(sourceArray, 0, destinationArray, 0, length);
                        element3 = document.CreateElement("HZXX");
                        element3.InnerText = BitConverter.ToString(destinationArray).Replace("-", "");
                        element.AppendChild(element3);
                    }
                    element3 = document.CreateElement("CSSJ");
                    element3.InnerText = this.taxCard.get_LastRepDate().ToString("yyyyMMddHHmm");
                    element.AppendChild(element3);
                    this.loger.Debug("离开专普：" + optype);
                }
                else
                {
                    XmlElement element4;
                    XmlElement element5;
                    XmlElement element6;
                    if (((fpzl == FPZL.HY) || (optype == OPTYPE.JDCQK)) || (optype == OPTYPE.JSPQK))
                    {
                        this.loger.Debug("进入货运抄报清卡组包：" + optype);
                        byte[] byteOrigin = null;
                        this.loger.Debug("1");
                        int num2 = 0x138;
                        if (optype == OPTYPE.HYCB)
                        {
                            this.loger.Debug("2货运获取抄税密文报和抄税起始终止时间");
                            byteOrigin = this.taxCard.RemoteRep(fpzl, ref time, ref time2);
                            if (this.taxCard.get_RetCode() != 0)
                            {
                                this.code = this.taxCard.get_ErrCode();
                                this.loger.Debug("code:" + this.code);
                                string messageInfo = MessageManager.GetMessageInfo(this.code);
                                this.loger.Error("底层返回信息：ErrCode：" + this.code + "  描述信息：" + messageInfo);
                                this.cbHyMess = "底层返回信息：ErrCode：" + this.code + "  描述信息：" + messageInfo;
                                return "";
                            }
                            byteOrigin = this.GetFPHZB(byteOrigin, OPTYPE.HYCB);
                        }
                        element = document.CreateElement("business");
                        if ((optype == OPTYPE.HYCB) || (optype == OPTYPE.JDCCB))
                        {
                            element.SetAttribute("id", "HX_KPXXSC");
                            element.SetAttribute("comment", "开票信息上传");
                        }
                        else if (((optype == OPTYPE.HYQK) || (optype == OPTYPE.JDCQK)) || (optype == OPTYPE.JSPQK))
                        {
                            element.SetAttribute("id", "HX_JKHCQQ");
                            element.SetAttribute("comment", "监控回传请求");
                        }
                        document.AppendChild(element);
                        element4 = document.CreateElement("body");
                        element4.SetAttribute("count", "1");
                        element4.SetAttribute("skph", this.taxCard.GetInvControlNum());
                        element4.SetAttribute("nsrsbh", this.taxCard.get_TaxCode());
                        element4.SetAttribute("kpjh", this.taxCard.get_Machine().ToString());
                        string s = string.Empty;
                        if (fpzl == FPZL.HY)
                        {
                            this.loger.Debug("HY-上次抄税期进入");
                            foreach (InvTypeInfo info in this.taxCard.get_StateInfo().InvTypeInfo)
                            {
                                this.loger.Debug("4 HY invtype:" + info.InvType);
                                if (info.InvType == 11)
                                {
                                    s = info.LastRepDate;
                                    this.loger.Debug("货运上次报税日期：" + s);
                                    break;
                                }
                            }
                            this.loger.Debug("HY-上次抄税期离开");
                        }
                        else if ((optype == OPTYPE.JDCQK) || (optype == OPTYPE.JSPQK))
                        {
                            this.loger.Debug("机动车清卡-上次抄税期进入");
                            foreach (InvTypeInfo info2 in this.taxCard.get_StateInfo().InvTypeInfo)
                            {
                                this.loger.Debug("机动车清卡 invtype:" + info2.InvType);
                                if (((info2.InvType == 12) || (info2.InvType == FPZL.DZFP)) || (info2.InvType == FPZL.JSFP))
                                {
                                    s = info2.LastRepDate;
                                    this.loger.Debug("机动车清卡上次报税日期：" + s);
                                    break;
                                }
                            }
                            this.loger.Debug("机动车清卡-上次抄税期离开");
                        }
                        element4.SetAttribute("bssj", DateTime.Parse(s).ToString("yyyy-MM-dd HH:mm"));
                        element.AppendChild(element4);
                        element5 = document.CreateElement("group");
                        element5.SetAttribute("xh", "1");
                        element4.AppendChild(element5);
                        element3 = document.CreateElement("data");
                        element3.SetAttribute("name", "fplx_dm");
                        if (fpzl == FPZL.HY)
                        {
                            element3.SetAttribute("value", "009");
                        }
                        else if (fpzl == FPZL.JDC)
                        {
                            element3.SetAttribute("value", "005");
                        }
                        else if (optype == OPTYPE.JSPQK)
                        {
                            element3.SetAttribute("value", "026");
                        }
                        element5.AppendChild(element3);
                        if ((optype == OPTYPE.HYCB) || (optype == OPTYPE.JDCCB))
                        {
                            this.loger.Debug("6");
                            XmlDocument document2 = new XmlDocument();
                            XmlDeclaration declaration2 = document2.CreateXmlDeclaration("1.0", "GBK", null);
                            document2.PreserveWhitespace = false;
                            document2.AppendChild(declaration2);
                            element6 = document2.CreateElement("hzData");
                            document2.AppendChild(element6);
                            element3 = document2.CreateElement("nsrsbh");
                            element3.InnerText = this.taxCard.get_TaxCode();
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("kpjh");
                            element3.InnerText = this.taxCard.get_Machine().ToString();
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("bssj");
                            element3.InnerText = s;
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("sbbh");
                            element3.InnerText = this.taxCard.GetInvControlNum();
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("hxsh");
                            element3.InnerText = this.taxCard.GetHashTaxCode();
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("fplxdm");
                            if (fpzl == FPZL.HY)
                            {
                                element3.InnerText = "009";
                            }
                            else if (fpzl == FPZL.JDC)
                            {
                                element3.InnerText = "005";
                            }
                            element6.AppendChild(element3);
                            element3 = document2.CreateElement("ssq");
                            if (fpzl == FPZL.HY)
                            {
                                this.loger.Debug("HY-锁死日期进入");
                                string lockedDate = "";
                                foreach (InvTypeInfo info3 in this.taxCard.get_StateInfo().InvTypeInfo)
                                {
                                    this.loger.Debug("7 HY invtype:" + info3.InvType);
                                    if (info3.InvType == 11)
                                    {
                                        lockedDate = info3.LockedDate;
                                        this.loger.Debug("货运锁死日期：" + lockedDate);
                                        break;
                                    }
                                }
                                this.loger.Debug("HY-锁死日期离开");
                                element3.InnerText = lockedDate.Substring(lockedDate.Length - 2, 2);
                                this.loger.Debug("10");
                            }
                            element6.AppendChild(element3);
                            byte[] buffer4 = new byte[num2];
                            Array.Copy(byteOrigin, 0, buffer4, 0, num2);
                            element3 = document2.CreateElement("hzsj");
                            element3.InnerText = BitConverter.ToString(buffer4).Replace("-", "");
                            element6.AppendChild(element3);
                            element3 = document.CreateElement("data");
                            element3.SetAttribute("name", "fpmx");
                            byte[] bytes = ToolUtil.GetBytes(document2.InnerXml);
                            byte[] buffer6 = ToolUtil.GetBytes("lxdyccb123456wjh");
                            byte[] buffer7 = ToolUtil.GetBytes("wjhyccb123456lxd");
                            byte[] inArray = AES_Crypt.Encrypt(bytes, buffer6, buffer7);
                            element3.SetAttribute("value", Convert.ToBase64String(inArray));
                            element5.AppendChild(element3);
                            this.loger.Debug("离开货运抄报：" + optype);
                        }
                    }
                    else if ((fpzl == FPZL.JDC) && (optype == OPTYPE.JDCCB))
                    {
                        if (this.taxCard.get_QYLX().ISJDC || this.taxCard.get_QYLX().ISPTFPDZ)
                        {
                            this.loger.Debug("进入机动车电子抄报：" + optype);
                            byte[] fPHZB = null;
                            int num3 = 320;
                            this.loger.Debug("2机动车电子抄报获取抄税密文报和抄税起始终止时间");
                            fPHZB = this.taxCard.RemoteRep(fpzl, ref time, ref time2);
                            if (this.taxCard.get_RetCode() != 0)
                            {
                                this.code = this.taxCard.get_ErrCode();
                                this.loger.Debug("code:" + this.code);
                                string str4 = MessageManager.GetMessageInfo(this.code);
                                this.loger.Error("RetCode:" + this.taxCard.get_RetCode());
                                this.cbJdcMess = str4;
                                this.loger.Error("底层返回信息：ErrCode：" + this.code + "  描述信息：" + str4);
                                return "";
                            }
                            this._bytesHZSJ = fPHZB;
                            fPHZB = this.GetFPHZB(this._bytesHZSJ, OPTYPE.JDCCB);
                            element = document.CreateElement("business");
                            if ((optype == OPTYPE.HYCB) || (optype == OPTYPE.JDCCB))
                            {
                                element.SetAttribute("id", "HX_KPXXSC");
                                element.SetAttribute("comment", "开票信息上传");
                            }
                            else if ((optype == OPTYPE.HYQK) || (optype == OPTYPE.JDCQK))
                            {
                                element.SetAttribute("id", "HX_JKHCQQ");
                                element.SetAttribute("comment", "监控回传请求");
                            }
                            document.AppendChild(element);
                            element4 = document.CreateElement("body");
                            element4.SetAttribute("count", "1");
                            element4.SetAttribute("skph", this.taxCard.GetInvControlNum());
                            element4.SetAttribute("nsrsbh", this.taxCard.get_TaxCode());
                            element4.SetAttribute("kpjh", this.taxCard.get_Machine().ToString());
                            string lastRepDate = string.Empty;
                            if (fpzl == FPZL.JDC)
                            {
                                this.loger.Debug("JDC-上次抄税期进入");
                                foreach (InvTypeInfo info4 in this.taxCard.get_StateInfo().InvTypeInfo)
                                {
                                    this.loger.Debug("5 jdc cb invtype:" + info4.InvType);
                                    if (info4.InvType == 12)
                                    {
                                        lastRepDate = info4.LastRepDate;
                                        this.loger.Debug("机动车上次报税日期：" + lastRepDate);
                                        break;
                                    }
                                    if ((info4.InvType == 0x33) && (this.taxCard.get_InvEleKindCode() == 0x33))
                                    {
                                        lastRepDate = this.taxCard.GetCSDate(0x33)[0];
                                        this.loger.Debug("电子发票上次报税日期：" + lastRepDate);
                                    }
                                }
                                this.loger.Debug("JDC-上次抄税期离开");
                            }
                            element4.SetAttribute("bssj", DateTime.Parse(lastRepDate).ToString("yyyy-MM-dd HH:mm"));
                            element.AppendChild(element4);
                            element5 = document.CreateElement("group");
                            element5.SetAttribute("xh", "1");
                            element4.AppendChild(element5);
                            element3 = document.CreateElement("data");
                            element3.SetAttribute("name", "fplx_dm");
                            if (fpzl == FPZL.HY)
                            {
                                element3.SetAttribute("value", "009");
                            }
                            else if (fpzl == FPZL.JDC)
                            {
                                element3.SetAttribute("value", "005");
                            }
                            element5.AppendChild(element3);
                            if ((optype == OPTYPE.HYCB) || (optype == OPTYPE.JDCCB))
                            {
                                this.loger.Debug("6");
                                XmlDocument document3 = new XmlDocument();
                                XmlDeclaration declaration3 = document3.CreateXmlDeclaration("1.0", "GBK", null);
                                document3.PreserveWhitespace = false;
                                document3.AppendChild(declaration3);
                                element6 = document3.CreateElement("hzData");
                                document3.AppendChild(element6);
                                element3 = document3.CreateElement("nsrsbh");
                                element3.InnerText = this.taxCard.get_TaxCode();
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("kpjh");
                                element3.InnerText = this.taxCard.get_Machine().ToString();
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("bssj");
                                element3.InnerText = lastRepDate;
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("sbbh");
                                element3.InnerText = this.taxCard.GetInvControlNum();
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("hxsh");
                                element3.InnerText = this.taxCard.GetHashTaxCode();
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("fplxdm");
                                if (fpzl == FPZL.HY)
                                {
                                    element3.InnerText = "009";
                                }
                                else if (fpzl == FPZL.JDC)
                                {
                                    element3.InnerText = "005";
                                }
                                element6.AppendChild(element3);
                                element3 = document3.CreateElement("ssq");
                                if (fpzl == FPZL.JDC)
                                {
                                    string str6 = "";
                                    this.loger.Debug("JDC-锁死日期进入");
                                    foreach (InvTypeInfo info5 in this.taxCard.get_StateInfo().InvTypeInfo)
                                    {
                                        this.loger.Debug("8 invtype：" + info5.InvType);
                                        if (info5.InvType == 12)
                                        {
                                            str6 = info5.LockedDate;
                                            this.loger.Debug("机动车锁死日期：" + str6);
                                            break;
                                        }
                                        if ((info5.InvType == 0x33) && (this.taxCard.get_InvEleKindCode() == 0x33))
                                        {
                                            str6 = this.taxCard.GetCSDate(0x33)[1];
                                            this.loger.Debug("电子发票锁死日期：" + str6);
                                        }
                                    }
                                    this.loger.Debug("JDC-锁死日期离开");
                                    element3.InnerText = str6.Substring(str6.Length - 2, 2);
                                    this.loger.Debug("10");
                                }
                                element6.AppendChild(element3);
                                byte[] buffer10 = new byte[num3];
                                Array.Copy(fPHZB, 0, buffer10, 0, num3);
                                element3 = document3.CreateElement("hzsj");
                                element3.InnerText = BitConverter.ToString(buffer10).Replace("-", "");
                                element6.AppendChild(element3);
                                element3 = document.CreateElement("data");
                                element3.SetAttribute("name", "fpmx");
                                byte[] buffer11 = ToolUtil.GetBytes(document3.InnerXml);
                                byte[] buffer12 = ToolUtil.GetBytes("lxdyccb123456wjh");
                                byte[] buffer13 = ToolUtil.GetBytes("wjhyccb123456lxd");
                                byte[] buffer14 = AES_Crypt.Encrypt(buffer11, buffer12, buffer13);
                                element3.SetAttribute("value", Convert.ToBase64String(buffer14));
                                element5.AppendChild(element3);
                                this.loger.Debug("离开机动车电子抄报：" + optype);
                                this._dictSenderToServer.Add(OPTYPE.JDCCB, document.InnerXml);
                            }
                        }
                    }
                    else if ((fpzl == FPZL.JSFP) && (optype == OPTYPE.JSPCB))
                    {
                        document = new XmlDocument();
                        document.AppendChild(document.CreateXmlDeclaration("1.0", "gbk", ""));
                        if (this.taxCard.get_QYLX().ISPTFPJSP && (fpzl == FPZL.JSFP))
                        {
                            this.loger.Debug("进入卷式票抄报：" + optype);
                            byte[] buffer15 = null;
                            this.loger.Debug("1");
                            int num4 = 320;
                            this.loger.Debug("2获取抄税密文报和抄税起始终止时间");
                            if ((this._bytesHZSJ != null) && (this._bytesHZSJ.Length > 0))
                            {
                                buffer15 = this._bytesHZSJ;
                            }
                            else
                            {
                                buffer15 = this.taxCard.RemoteRep(fpzl, ref time, ref time2);
                                if (this.taxCard.get_RetCode() != 0)
                                {
                                    this.code = this.taxCard.get_ErrCode();
                                    this.loger.Debug("code:" + this.code);
                                    string str7 = MessageManager.GetMessageInfo(this.code);
                                    this.cbJspMess = str7;
                                    this.loger.Error("底层返回信息：ErrCode：" + this.code + "  描述信息：" + str7);
                                    return "";
                                }
                            }
                            this._bytesHZSJ = buffer15;
                            buffer15 = this.GetFPHZB(this._bytesHZSJ, OPTYPE.JSPCB);
                            element = document.CreateElement("business");
                            element.SetAttribute("id", "WLCB");
                            document.AppendChild(element);
                            element4 = document.CreateElement("body");
                            element4.SetAttribute("count", "1");
                            element4.SetAttribute("skph", this.taxCard.GetInvControlNum());
                            element4.SetAttribute("nsrsbh", this.taxCard.get_TaxCode());
                            element4.SetAttribute("kpjh", this.taxCard.get_Machine().ToString());
                            this.loger.Debug("开启获取卷式发票上次报税日期");
                            string str8 = string.Empty;
                            str8 = this.taxCard.GetCSDate(0x29)[0];
                            this.loger.Debug("卷式发票发票上次报税日期：" + str8);
                            DateTime.Parse(str8);
                            element.AppendChild(element4);
                            element2 = document.CreateElement("input");
                            element4.AppendChild(element2);
                            element5 = document.CreateElement("group");
                            element5.SetAttribute("xh", "1");
                            element2.AppendChild(element5);
                            XmlElement element7 = document.CreateElement("fplxdm");
                            element7.InnerText = "025";
                            element5.AppendChild(element7);
                            XmlElement element8 = document.CreateElement("sq");
                            element8.InnerText = time.ToString("yyyyMMddHHmmss") + time2.ToString("yyyyMMddHHmmss");
                            element5.AppendChild(element8);
                            XmlElement element9 = document.CreateElement("fpmx");
                            element5.AppendChild(element9);
                            XmlElement element10 = document.CreateElement("fpdxx");
                            element5.AppendChild(element10);
                            XmlElement element11 = document.CreateElement("fphz");
                            element5.AppendChild(element11);
                            XmlElement element12 = document.CreateElement("szfphz");
                            element5.AppendChild(element12);
                            XmlElement element13 = document.CreateElement("sfjchz");
                            element13.InnerText = "N";
                            element5.AppendChild(element13);
                            XmlElement element14 = document.CreateElement("qtxx");
                            element5.AppendChild(element14);
                            this.loger.Debug("6 卷票抄报开始组织内层数据");
                            XmlDocument document4 = new XmlDocument();
                            XmlDeclaration declaration4 = document4.CreateXmlDeclaration("1.0", "GBK", null);
                            document4.PreserveWhitespace = false;
                            document4.AppendChild(declaration4);
                            element6 = document4.CreateElement("hzData");
                            document4.AppendChild(element6);
                            element3 = document4.CreateElement("nsrsbh");
                            element3.InnerText = this.taxCard.get_TaxCode();
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("hxsh");
                            element3.InnerText = this.taxCard.GetHashTaxCode();
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("kpjh");
                            element3.InnerText = this.taxCard.get_Machine().ToString();
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("sbbh");
                            element3.InnerText = this.taxCard.GetInvControlNum();
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("fplxdm");
                            element3.InnerText = "025";
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("ssq");
                            string str9 = "";
                            this.loger.Debug("JSP-锁死日期进入");
                            str9 = this.taxCard.GetCSDate(0x29)[1];
                            this.loger.Debug("电子发票锁死日期：" + str9);
                            element3.InnerText = str9.Substring(str9.Length - 2, 2);
                            this.loger.Debug("10");
                            element6.AppendChild(element3);
                            element3 = document4.CreateElement("bssj");
                            element3.InnerText = str8;
                            element6.AppendChild(element3);
                            byte[] buffer16 = new byte[num4];
                            Array.Copy(buffer15, 0, buffer16, 0, num4);
                            element3 = document4.CreateElement("hzsj");
                            element3.InnerText = BitConverter.ToString(buffer16).Replace("-", "");
                            element6.AppendChild(element3);
                            this.loger.Debug("卷式发票抄报：内层数据：" + document4.InnerXml);
                            byte[] buffer17 = ToolUtil.GetBytes(document4.InnerXml);
                            byte[] buffer18 = ToolUtil.GetBytes("lxdyccb123456wjh");
                            byte[] buffer19 = ToolUtil.GetBytes("wjhyccb123456lxd");
                            byte[] buffer20 = AES_Crypt.Encrypt(buffer17, buffer18, buffer19);
                            element11.InnerText = Convert.ToBase64String(buffer20);
                            this.loger.Debug("卷式发票抄报：外层数据：" + document.InnerXml);
                            this.loger.Debug("离开卷式票抄报：" + optype);
                            this._dictSenderToServer.Add(OPTYPE.JSPCB, document.InnerXml);
                        }
                    }
                }
                return document.InnerXml;
            }
            catch (Exception exception)
            {
                this.code = exception.Message;
                this.loger.Debug(exception.ToString());
                return "";
            }
        }

        public void ProcessMsg(int fpzl, string optype)
        {
            this.loger.Debug("操作类型：" + optype + "  开始");
            if ((optype == OPTYPE.JSPQK) && (this.taxCard.get_QYLX().ISJDC || this.taxCard.get_QYLX().ISPTFPDZ))
            {
                this.isQkJsp = this.isQkJdc;
                this.qkJSFPMess = this.qkJdcMess;
                return;
            }
            if (!this.CheckIFCardCanUse())
            {
                if (optype == OPTYPE.ZPCB)
                {
                    this.cbZpMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.HYCB)
                {
                    this.cbHyMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.JDCCB)
                {
                    this.cbJdcMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.JSPCB)
                {
                    this.cbJspMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.ZPQK)
                {
                    this.qkZpMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.HYQK)
                {
                    this.qkHyMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.JDCQK)
                {
                    this.qkJdcMess = "未到开票允许启用时间。";
                }
                if (optype == OPTYPE.JSPQK)
                {
                    this.qkJSFPMess = "未到开票允许启用时间。";
                }
                this.loger.Error("未到开票允许启用时间。");
                return;
            }
            if ((((optype == OPTYPE.JDCQK) || (optype == OPTYPE.JSPQK)) || ((optype == OPTYPE.ZPQK) || (optype == OPTYPE.HYQK))) && !this.CheckQKCondition(fpzl))
            {
                if (optype == OPTYPE.ZPQK)
                {
                    this.qkZpMess = "无上报汇总信息或当前处于非征期，无法进行远程清卡！";
                }
                if (optype == OPTYPE.HYQK)
                {
                    this.qkHyMess = "无上报汇总信息或当前处于非征期，无法进行远程清卡！";
                }
                if (optype == OPTYPE.JDCQK)
                {
                    this.qkJdcMess = "无上报汇总信息或当前处于非征期，无法进行远程清卡！";
                }
                if (optype == OPTYPE.JSPQK)
                {
                    this.qkJSFPMess = "无上报汇总信息或当前处于非征期，无法进行远程清卡！";
                }
                this.loger.Error("无上报汇总信息或当前处于非征期，无法进行远程清卡！：" + optype);
                return;
            }
            string xml = string.Empty;
            try
            {
                XmlElement documentElement;
                XmlNode node3;
                string requestMsg = this.GetRequestMsg(fpzl, optype);
                if (!(optype != OPTYPE.JDCCB) || !(optype != OPTYPE.JSPCB))
                {
                    goto Label_0B8F;
                }
                this.loger.Debug("进入非机动车抄报或者清卡操作 optype：" + optype);
                if (requestMsg == "")
                {
                    string messageInfo = string.Empty;
                    this.loger.Debug("taxCard.RetCode:" + this.taxCard.get_RetCode());
                    if (this.taxCard.get_RetCode() != 0)
                    {
                        this.code = this.taxCard.get_ErrCode();
                        this.loger.Debug("code:" + this.code);
                        messageInfo = MessageManager.GetMessageInfo(this.code);
                        this.loger.Debug("codemess:" + messageInfo);
                    }
                    else
                    {
                        messageInfo = this.code;
                    }
                    if (optype == OPTYPE.ZPCB)
                    {
                        this.isCbZp = false;
                        this.cbZpMess = messageInfo;
                    }
                    else if (optype == OPTYPE.ZPQK)
                    {
                        this.isQkZp = false;
                        this.qkZpMess = messageInfo;
                    }
                    else if (optype == OPTYPE.HYCB)
                    {
                        this.isCbHy = false;
                        this.cbHyMess = messageInfo;
                    }
                    else if (optype == OPTYPE.JDCCB)
                    {
                        this.isCbJdc = false;
                        this.cbJdcMess = messageInfo;
                    }
                    else if (optype == OPTYPE.HYQK)
                    {
                        this.isQkHy = false;
                        this.qkHyMess = messageInfo;
                    }
                    else if (optype == OPTYPE.JDCQK)
                    {
                        this.isQkJdc = false;
                        this.qkJdcMess = messageInfo;
                    }
                    else if (optype == OPTYPE.JSPQK)
                    {
                        this.isQkJsp = false;
                        this.qkJSFPMess = messageInfo;
                    }
                }
                else
                {
                    this.loger.Debug("发送给局端的信息 msg:" + requestMsg);
                    int num = -1;
                    if (optype == OPTYPE.JSPQK)
                    {
                        num = HttpsSender.SendMsg(OPTYPE.JDCQK, requestMsg, ref xml);
                    }
                    else
                    {
                        num = HttpsSender.SendMsg(optype, requestMsg, ref xml);
                    }
                    this.loger.Debug(string.Concat(new object[] { "局端返回的信息 result", xml, " code:", num }));
                    if (num == 0)
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(xml);
                        documentElement = document.DocumentElement;
                        if (fpzl != FPZL.ZP)
                        {
                            goto Label_0698;
                        }
                        XmlNode node = documentElement.SelectSingleNode("OUTPUT");
                        XmlNode node2 = node.SelectSingleNode("CODE");
                        node3 = node.SelectSingleNode("MESS");
                        if (!(node2.InnerText == "0000"))
                        {
                            goto Label_0649;
                        }
                        if (optype == OPTYPE.ZPQK)
                        {
                            byte[] buffer = Convert.FromBase64String(node.SelectSingleNode("DATA").SelectSingleNode("QKXX").InnerText);
                            this.taxCard.RemoteClearCard(buffer, buffer.Length, FPZL.ZP);
                            if (this.taxCard.get_RetCode() == 0)
                            {
                                this.isQkZp = true;
                                this.code = "YC0004";
                                this.qkZpMess = "清卡成功。";
                            }
                            else
                            {
                                this.isQkZp = false;
                                this.code = this.taxCard.get_ErrCode();
                                this.qkZpMess = MessageManager.GetMessageInfo(this.code);
                            }
                        }
                        else
                        {
                            this.code = "YC0003";
                            this.isCbZp = true;
                            this.cbZpMess = node3.InnerText;
                        }
                        goto Label_12EB;
                    }
                    if (optype == OPTYPE.ZPCB)
                    {
                        this.isCbZp = false;
                        this.cbZpMess = xml;
                    }
                    else if (optype == OPTYPE.ZPQK)
                    {
                        this.isQkZp = false;
                        this.qkZpMess = xml;
                    }
                    else if (optype == OPTYPE.HYCB)
                    {
                        this.isCbHy = false;
                        this.cbHyMess = xml;
                    }
                    else if (optype == OPTYPE.JDCCB)
                    {
                        this.isCbJdc = false;
                        this.isCbJSFP = false;
                        this.cbJdcMess = xml;
                        this.cbJspMess = xml;
                    }
                    else if (optype == OPTYPE.HYQK)
                    {
                        this.isQkHy = false;
                        this.qkHyMess = xml;
                    }
                    else if (optype == OPTYPE.JDCQK)
                    {
                        this.isQkJdc = false;
                        this.qkJdcMess = xml;
                    }
                    else if (optype == OPTYPE.JSPQK)
                    {
                        this.isQkJsp = false;
                        this.qkJSFPMess = xml;
                    }
                }
                return;
            Label_0649:
                if (optype == OPTYPE.ZPCB)
                {
                    this.isCbZp = false;
                    this.cbZpMess = node3.InnerText;
                }
                else if (optype == OPTYPE.ZPQK)
                {
                    this.isQkZp = false;
                    this.qkZpMess = node3.InnerText;
                }
                goto Label_12EB;
            Label_0698:
                if (((fpzl == FPZL.HY) || (fpzl == FPZL.JDC)) || (fpzl == FPZL.JSFP))
                {
                    string innerText = "";
                    string str5 = "";
                    string s = "";
                    XmlNode node7 = documentElement.SelectSingleNode("body").SelectSingleNode("group");
                    for (int i = 0; i < node7.ChildNodes.Count; i++)
                    {
                        XmlNode node4 = node7.ChildNodes.Item(i);
                        if (node4.Attributes["name"].InnerText == "returnCode")
                        {
                            innerText = node4.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnCode：" + innerText);
                        }
                        else if (node4.Attributes["name"].InnerText == "returnMessage")
                        {
                            str5 = node4.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnMessage：" + str5);
                        }
                        else if (node4.Attributes["name"].InnerText == "fpjkmw")
                        {
                            s = node4.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnMessage：" + str5);
                        }
                    }
                    if (innerText == "00")
                    {
                        if (optype == OPTYPE.HYQK)
                        {
                            byte[] buffer2 = Convert.FromBase64String(s);
                            this.taxCard.RemoteClearCard(buffer2, buffer2.Length, FPZL.HY);
                            this.loger.Debug("操作类型：" + optype + " RemoteClearCardResult：" + this.taxCard.get_RetCode().ToString());
                            if (this.taxCard.get_RetCode() == 0)
                            {
                                this.isQkHy = true;
                                this.code = "YC0004";
                                this.qkHyMess = "清卡成功。";
                            }
                            else
                            {
                                this.isQkHy = false;
                                this.code = this.taxCard.get_ErrCode();
                                this.qkHyMess = MessageManager.GetMessageInfo(this.code);
                            }
                        }
                        else if ((optype == OPTYPE.JDCQK) || (optype == OPTYPE.JSPQK))
                        {
                            byte[] buffer3 = Convert.FromBase64String(s);
                            this.taxCard.RemoteClearCard(buffer3, buffer3.Length, FPZL.JDC);
                            this.loger.Debug("操作类型：" + optype + " RemoteClearCardResult：" + this.taxCard.get_RetCode().ToString());
                            if (this.taxCard.get_RetCode() == 0)
                            {
                                if (optype == OPTYPE.JDCQK)
                                {
                                    this.isQkJdc = true;
                                    this.code = "YC0004";
                                    this.qkJdcMess = "清卡成功。";
                                }
                                else if (optype == OPTYPE.JSPQK)
                                {
                                    this.isQkJsp = true;
                                    this.code = "YC0004";
                                    this.qkJSFPMess = "清卡成功。";
                                }
                            }
                            else if (optype == OPTYPE.JDCQK)
                            {
                                this.isQkJdc = false;
                                this.code = this.taxCard.get_ErrCode();
                                this.qkJdcMess = MessageManager.GetMessageInfo(this.code);
                            }
                            else if (optype == OPTYPE.JSPQK)
                            {
                                this.isQkJsp = true;
                                this.code = this.taxCard.get_ErrCode();
                                this.qkJSFPMess = MessageManager.GetMessageInfo(this.code);
                            }
                        }
                        else if (optype == OPTYPE.HYCB)
                        {
                            this.code = "YC0003";
                            this.isCbHy = true;
                            this.cbHyMess = str5;
                        }
                        else if (optype == OPTYPE.JDCCB)
                        {
                            this.code = "YC0003";
                            this.isCbJdc = true;
                            this.cbJdcMess = str5;
                        }
                        else if (optype == OPTYPE.JSPCB)
                        {
                            this.code = "YC0003";
                            this.isCbJSFP = true;
                            this.cbJspMess = str5;
                        }
                    }
                    else if (optype == OPTYPE.HYCB)
                    {
                        this.isCbHy = false;
                        this.cbHyMess = str5;
                    }
                    else if (optype == OPTYPE.JDCCB)
                    {
                        this.isCbJdc = false;
                        this.cbJdcMess = str5;
                    }
                    else if (optype == OPTYPE.JSPCB)
                    {
                        this.isCbJSFP = false;
                        this.cbJspMess = str5;
                    }
                    else if (optype == OPTYPE.HYQK)
                    {
                        this.isQkHy = false;
                        this.qkHyMess = str5;
                    }
                    else if (optype == OPTYPE.JDCQK)
                    {
                        this.isQkJdc = false;
                        this.qkJdcMess = str5;
                    }
                    else if (optype == OPTYPE.JSPQK)
                    {
                        this.isQkJsp = false;
                        this.qkJSFPMess = str5;
                    }
                }
                goto Label_12EB;
            Label_0B8F:
                if ((optype == OPTYPE.JDCCB) && this._dictSenderToServer.ContainsKey(OPTYPE.JDCCB))
                {
                    this.loger.Debug("进入机动车抄报");
                    if (this._dictSenderToServer[OPTYPE.JDCCB] == "")
                    {
                        string code = string.Empty;
                        this.loger.Debug("optype:" + optype);
                        if (this.taxCard.get_RetCode() != 0)
                        {
                            this.code = this.taxCard.get_ErrCode();
                            this.loger.Debug("code:" + this.code);
                            code = MessageManager.GetMessageInfo(this.code);
                            this.loger.Debug("codemess:" + code);
                        }
                        else
                        {
                            code = this.code;
                        }
                        this.isCbJdc = false;
                        this.cbJdcMess = code;
                        return;
                    }
                    this.loger.Debug("机动车抄报 发送给局端的msg:" + this._dictSenderToServer[OPTYPE.JDCCB]);
                    int num3 = HttpsSender.SendMsg(optype, this._dictSenderToServer[OPTYPE.JDCCB], ref xml);
                    this.loger.Debug(string.Concat(new object[] { "机动车抄报 局端返回result", xml, " code:", num3 }));
                    if (num3 != 0)
                    {
                        this.isCbJdc = false;
                        this.cbJdcMess = xml;
                        return;
                    }
                    XmlDocument document2 = new XmlDocument();
                    this.loger.Debug("操作类型：" + optype + "   局端返回信息：" + xml);
                    document2.LoadXml(xml);
                    XmlElement element2 = document2.DocumentElement;
                    string str8 = "";
                    string str9 = "";
                    XmlNode node10 = element2.SelectSingleNode("body").SelectSingleNode("group");
                    for (int j = 0; j < node10.ChildNodes.Count; j++)
                    {
                        XmlNode node8 = node10.ChildNodes.Item(j);
                        if (node8.Attributes["name"].InnerText == "returnCode")
                        {
                            str8 = node8.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnCode：" + str8);
                        }
                        else if (node8.Attributes["name"].InnerText == "returnMessage")
                        {
                            str9 = node8.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnMessage：" + str9);
                        }
                        else if (node8.Attributes["name"].InnerText == "fpjkmw")
                        {
                            string text1 = node8.Attributes["value"].InnerText;
                            this.loger.Debug("操作类型：" + optype + " fpjkmw：" + str9);
                        }
                        if (str8 == "00")
                        {
                            this.code = "YC0003";
                            this.isCbJdc = true;
                            this.cbJdcMess = str9;
                        }
                        else
                        {
                            this.isCbJdc = false;
                            this.cbJdcMess = str9;
                        }
                    }
                }
                if ((optype == OPTYPE.JSPCB) && this._dictSenderToServer.ContainsKey(OPTYPE.JSPCB))
                {
                    if (this._dictSenderToServer[OPTYPE.JSPCB] == "")
                    {
                        string str10 = string.Empty;
                        this.loger.Debug("optype:" + optype);
                        if (this.taxCard.get_RetCode() != 0)
                        {
                            this.code = this.taxCard.get_ErrCode();
                            this.loger.Debug("code:" + this.code);
                            str10 = MessageManager.GetMessageInfo(this.code);
                            this.loger.Debug("codemess:" + str10);
                        }
                        else
                        {
                            str10 = this.code;
                        }
                        this.isCbJSFP = false;
                        this.cbJspMess = str10;
                        return;
                    }
                    this.loger.Debug("卷式发票抄报发送给局端数据 msg:" + requestMsg);
                    int num5 = HttpsSender.SendMsg(OPTYPE.JDCCB, requestMsg, ref xml);
                    this.loger.Debug(string.Concat(new object[] { "卷式发票抄报局端返回数据 result", xml, " code:", num5 }));
                    if (num5 != 0)
                    {
                        this.isCbJSFP = false;
                        this.cbJspMess = xml;
                        return;
                    }
                    XmlDocument document3 = new XmlDocument();
                    this.loger.Debug("操作类型：" + optype + "   局端返回信息：" + xml);
                    document3.LoadXml(xml);
                    XmlElement element3 = document3.DocumentElement;
                    string str11 = "";
                    string str12 = "";
                    XmlNode node13 = element3.SelectSingleNode("body").SelectSingleNode("output").SelectSingleNode("group");
                    for (int k = 0; k < node13.ChildNodes.Count; k++)
                    {
                        XmlNode node11 = node13.ChildNodes.Item(k);
                        if (node11.Name == "returncode")
                        {
                            str11 = node11.InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnCode：" + str11);
                        }
                        else if (node11.Name == "returnmsg")
                        {
                            str12 = node11.InnerText;
                            this.loger.Debug("操作类型：" + optype + " returnMessage：" + str12);
                        }
                        else if (node11.Name == "fplxdm")
                        {
                            this.loger.Debug("操作类型：" + optype + " fplxdm：" + node11.InnerText);
                        }
                    }
                    if (str11 == "00")
                    {
                        this.code = "YC0003";
                        this.isCbJSFP = true;
                        this.cbJspMess = str12;
                    }
                    else
                    {
                        this.isCbJSFP = false;
                        this.cbJspMess = str12;
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug(exception.ToString());
                if (optype == OPTYPE.ZPCB)
                {
                    this.isCbZp = false;
                    this.cbZpMess = exception.Message;
                }
                else if (optype == OPTYPE.ZPQK)
                {
                    this.isQkZp = false;
                    this.qkZpMess = exception.Message;
                }
                else if (optype == OPTYPE.HYCB)
                {
                    this.isCbHy = false;
                    this.cbHyMess = exception.Message;
                }
                else if (optype == OPTYPE.JDCCB)
                {
                    this.isCbJdc = false;
                    this.cbJdcMess = exception.Message;
                    this.isCbJSFP = false;
                    this.cbJspMess = exception.Message;
                }
                else if (optype == OPTYPE.HYQK)
                {
                    this.isQkHy = false;
                    this.qkHyMess = exception.Message;
                }
                else if (optype == OPTYPE.JDCQK)
                {
                    this.isQkJdc = false;
                    this.qkJdcMess = exception.Message;
                }
                else if (optype == OPTYPE.JSPQK)
                {
                    this.isQkJsp = false;
                    this.qkJSFPMess = exception.Message;
                }
            }
        Label_12EB:
            this.loger.Debug("操作类型：" + optype + "  结束");
        }

        public string CBHYMESS
        {
            get
            {
                return this.cbHyMess;
            }
        }

        public string CBJDCMESS
        {
            get
            {
                return this.cbJdcMess;
            }
        }

        public string CBJSPMESS
        {
            get
            {
                return this.cbJspMess;
            }
        }

        public string CBZPMESS
        {
            get
            {
                return this.cbZpMess;
            }
        }

        public DateTime dtHYLastCSDate
        {
            get
            {
                return this.GetLastRepDate(FPZL.HY);
            }
        }

        public DateTime dtJDCLastCSDate
        {
            get
            {
                return this.GetLastRepDate(FPZL.JDC);
            }
        }

        public DateTime dtJSPLastCSDate
        {
            get
            {
                return this.GetLastRepDate(FPZL.JSFP);
            }
        }

        public DateTime dtZPLastCSDate
        {
            get
            {
                return this.GetLastRepDate(FPZL.ZP);
            }
        }

        public bool ISCBHy
        {
            get
            {
                return this.isCbHy;
            }
        }

        public bool ISCBJDC
        {
            get
            {
                return this.isCbJdc;
            }
        }

        public bool ISCBJSFP
        {
            get
            {
                return this.isCbJSFP;
            }
        }

        public bool ISCBZP
        {
            get
            {
                return this.isCbZp;
            }
        }

        public bool ISHY
        {
            get
            {
                return this.isHy;
            }
        }

        public bool ISJDC
        {
            get
            {
                return this.isJdc;
            }
        }

        public bool ISJSP
        {
            get
            {
                return this.isJsp;
            }
        }

        public bool ISQKHY
        {
            get
            {
                return this.isQkHy;
            }
        }

        public bool ISQKJDC
        {
            get
            {
                return this.isQkJdc;
            }
        }

        public bool ISQKJSP
        {
            get
            {
                return this.isQkJsp;
            }
        }

        public bool ISQKZP
        {
            get
            {
                return this.isQkZp;
            }
        }

        public bool ISZP
        {
            get
            {
                return this.isZp;
            }
        }

        public string QKHYMESS
        {
            get
            {
                return this.qkHyMess;
            }
        }

        public string QKJDCMESS
        {
            get
            {
                return this.qkJdcMess;
            }
        }

        public string QKJSFPMESS
        {
            get
            {
                return this.qkJSFPMess;
            }
        }

        public string QKZPMESS
        {
            get
            {
                return this.qkZpMess;
            }
        }
    }
}

