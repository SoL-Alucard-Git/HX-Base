namespace Aisino.Fwkp.Fptk
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using Aisino.Fwkp.Fptk.Form;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    internal class FpManager : IFpManager
    {
        private string code;
        private string[] codeParams;
        private IBaseDAO dao = BaseDAOFactory.GetBaseDAOSQLite();
        private string errortip = "";
        private static Fpxx fpxx_tmp;
        private ILog log = LogUtil.GetLogger<FpManager>();
        private const string RLY = "(燃料油)";
        private const string RLY_DDZG = "(燃料油DDZG)";
        private const string SNY = "(石脑油)";
        private const string SNY_DDZG = "(石脑油DDZG)";

        public bool CanInvoice(FPLX fplx)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            try
            {
                TaxCard taxCard = TaxCardFactory.CreateTaxCard();
                TaxStateInfo info = taxCard.StateInfo;
                if (((int)fplx == 0) || ((int)fplx == 2))
                {
                    flag = info.IsLockReached == 1;
                    flag2 = info.IsRepReached == 1;
                    flag3 = info.IsInvEmpty == 1;
                }
                else
                {
                    InvTypeInfo info2 = new InvTypeInfo();
                    List<InvTypeInfo> invTypeInfo = info.InvTypeInfo;
                    if ((int)fplx == 11)
                    {
                        foreach (InvTypeInfo info3 in invTypeInfo)
                        {
                            if (info3.InvType == 11)
                            {
                                info2 = info3;
                                break;
                            }
                        }
                    }
                    else if ((int)fplx == 12)
                    {
                        foreach (InvTypeInfo info4 in invTypeInfo)
                        {
                            if (info4.InvType == 12)
                            {
                                info2 = info4;
                                break;
                            }
                        }
                    }
                    else if ((int)fplx == 0x33)
                    {
                        foreach (InvTypeInfo info5 in invTypeInfo)
                        {
                            if (info5.InvType == 0x33)
                            {
                                info2 = info5;
                                break;
                            }
                        }
                    }
                    else if ((int)fplx == 0x29)
                    {
                        foreach (InvTypeInfo info6 in invTypeInfo)
                        {
                            if (info6.InvType == 0x29)
                            {
                                info2 = info6;
                                break;
                            }
                        }
                    }
                    flag = info2.IsLockTime == 1;
                    flag2 = info2.IsRepTime == 1;
                    flag3 = info.IsInvEmpty == 1;
                }
                if (flag)
                {
                    this.code = "INP-242101";
                    return false;
                }
                if (flag2)
                {
                    this.code = "INP-242102";
                    return false;
                }
                if (flag3)
                {
                    this.code = "INP-242103";
                    return false;
                }
                if (((((int)fplx == 0) || ((int)fplx == 2)) || ((int)fplx == 0x33)) && !this.CanXTInv(taxCard, out this.code))
                {
                    return false;
                }
                this.code = "000000";
                return true;
            }
            catch (Exception exception)
            {
                this.log.Error("开票前读取金税卡状态时异常:" + exception.ToString());
                this.code = "9999";
                return false;
            }
        }

        private bool CanXTInv(TaxCard taxCard, out string code)
        {
            code = "";
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if (((objArray != null) && (objArray.Length != 0)) && ((objArray[0] != null) && Convert.ToBoolean(objArray[0])))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.QYLX.ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (((objArray2 == null) || !(objArray2[0] is bool)) || !Convert.ToBoolean(objArray2[0]))
                    {
                        code = "INP-242132";
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                this.log.Error("导入稀土商品异常:" + exception.ToString());
                code = "9999";
                return false;
            }
            return true;
        }

        public bool ChargeAllInfo(Fpxx fpxx, bool iswm, bool isdzfp = false)
        {
            this.errortip = "";
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            list = (fpxx.Mxxx.Count == 0) ? fpxx.Qdxx : fpxx.Mxxx;
            fpxx.sLv = list[0][(SPXX)8];
            double num = 0.0;
            double num2 = 0.0;
            for (int i = 0; i < list.Count; i++)
            {
                int num13;
                if (!iswm || (iswm && (list[i][(SPXX)10] != "4")))
                {
                    if ((fpxx.Zyfplx == (ZYFP_LX)4) && !list[i][0].ToString().EndsWith("(燃料油)"))
                    {
                        list[i][0] = list[i][0].ToString() + "(燃料油)";
                    }
                    if ((fpxx.Zyfplx == (ZYFP_LX)5) && !list[i][0].ToString().EndsWith("(燃料油DDZG)"))
                    {
                        list[i][0] = list[i][0].ToString() + "(燃料油DDZG)";
                    }
                    if ((fpxx.Zyfplx == (ZYFP_LX)2) && !list[i][0].ToString().EndsWith("(石脑油)"))
                    {
                        list[i][0] = list[i][0].ToString() + "(石脑油)";
                    }
                    if ((fpxx.Zyfplx == (ZYFP_LX)3) && !list[i][0].ToString().EndsWith("(石脑油DDZG)"))
                    {
                        list[i][0] = list[i][0].ToString() + "(石脑油DDZG)";
                    }
                }
                if (fpxx.sLv != list[i][(SPXX)8])
                {
                    fpxx.sLv = "";
                }
                num2 = double.Parse(list[i][(SPXX)7]);
                if (fpxx.Zyfplx == (ZYFP_LX)1)
                {
                    num = num2 / 19.0;
                }
                else if (fpxx.Zyfplx == (ZYFP_LX)10)
                {
                    num = (num2 / 1.035) * 0.015;
                }
                else if ((fpxx.Zyfplx == (ZYFP_LX)11) && (list[i][(SPXX)10] != "4"))
                {
                    if ((num2 - double.Parse(list[i][(SPXX)0x18])) < 0.0)
                    {
                        this.errortip = "差额税单据" + fpxx.xsdjbh + "中存在金额小于扣除额的商品明细！";
                        return false;
                    }
                    num = (num2 - double.Parse(list[i][(SPXX)0x18])) * double.Parse(list[i][(SPXX)8]);
                }
                else
                {
                    num = num2 * double.Parse(list[i][(SPXX)8]);
                }
                list[i][(SPXX)9] = num.ToString("F2");
                string s = "0.000%";
                if (list[i][(SPXX)10] != "4")
                {
                    continue;
                }
                list[i][(SPXX)5] = "";
                list[i][(SPXX)6] = "";
                list[i][(SPXX)4] = "";
                list[i][(SPXX)3] = "";
                if (!iswm)
                {
                    goto Label_06CA;
                }
                if (!isdzfp)
                {
                    int num4;
                    if (!int.TryParse(list[i][0], out num4))
                    {
                        this.errortip = "单据" + fpxx.xsdjbh + "中折扣行商品名称不正确！";
                        return false;
                    }
                    if (list[i][0] == "1")
                    {
                        double num5 = (-double.Parse(list[i][(SPXX)7]) / double.Parse(list[i - 1][(SPXX)7])) * 100.0;
                        s = num5.ToString("F3");
                        list[i][0] = "折扣(" + s + "%)";
                        list[i - 1][(SPXX)10] = "3";
                    }
                    else
                    {
                        double num6 = 0.0;
                        for (int j = int.Parse(list[i][0]); j > 0; j--)
                        {
                            num6 += double.Parse(list[i - j][(SPXX)7]);
                            list[i - j][(SPXX)10] = "3";
                        }
                        s = ((-double.Parse(list[i][(SPXX)7]) / num6) * 100.0).ToString("F3");
                        string[] textArray1 = new string[] { "折扣行数", list[i][0], "(", s, "%)" };
                        list[i][0] = string.Concat(textArray1);
                        if (fpxx.Zyfplx == (ZYFP_LX)11)
                        {
                            int num8 = int.Parse(list[i][0]);
                            decimal[] numArray = new decimal[num8];
                            int num9 = (i - num8) + 1;
                            for (int k = 0; k < num8; k++)
                            {
                                decimal num11 = decimal.Parse(list[num9 + k][(SPXX)7]);
                                decimal num12 = decimal.Parse(list[num9 + k][(SPXX)0x18]);
                                numArray[k] = Math.Round((decimal) (num11 * decimal.Parse(s)), 2);
                                if (((num11 - num12) - numArray[k]) < decimal.Zero)
                                {
                                    this.errortip = "差额税单据" + fpxx.xsdjbh + "中存在折扣额超限的商品明细！";
                                    return false;
                                }
                            }
                        }
                    }
                    continue;
                }
                string str2 = list[i][0];
                if (str2.Length < 5)
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行商品名称不正确，应设置为'折扣(X.XXX%)'或'折扣行数Y(X.XXX%)'！";
                    return false;
                }
                if (((str2.Substring(0, 4) == "折扣行数") && int.TryParse(str2.Substring(4, 1), out num13)) && (num13 > 0))
                {
                    goto Label_06C0;
                }
                if ((str2.Substring(0, 2) == "折扣") && ((str2.Substring(2, 1) == "(") || (str2.Substring(2, 1) == "（")))
                {
                    num13 = 1;
                    goto Label_06C0;
                }
                this.errortip = "单据" + fpxx.xsdjbh + "中折扣行商品名称不正确，应设置为'折扣(X.XXX%)'或'折扣行数Y(X.XXX%)'！";
                return false;
            Label_06A4:
                list[i - num13][(SPXX)10] = "3";
                num13--;
            Label_06C0:
                if (num13 > 0)
                {
                    goto Label_06A4;
                }
                continue;
            Label_06CA:
                if (fpxx.Zyfplx == (ZYFP_LX)11)
                {
                    decimal num14 = decimal.Parse(list[i - 1][(SPXX)0x18]);
                    decimal num15 = decimal.Parse(list[i][(SPXX)7]);
                    if (((decimal.Parse(list[i - 1][(SPXX)7]) - num14) + num15) < decimal.Zero)
                    {
                        this.errortip = "差额税单据" + fpxx.xsdjbh + "中存在折扣额超限的商品明细！";
                        return false;
                    }
                }
                string str3 = list[i][0];
                if (list[i - 1][(SPXX)10] == "4")
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中有连续两行以上为折扣行！";
                    return false;
                }
                if (str3 != list[i - 1][0])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行商品名称不正确，应设置为与折扣商品行名称一致！";
                    return false;
                }
                if (list[i][(SPXX)20] != list[i - 1][(SPXX)20])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行分类编码不正确，应设置为与折扣商品行一致！";
                    return false;
                }
                if (list[i][(SPXX)1] != list[i - 1][(SPXX)1])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行自行编码不正确，应设置为与折扣商品行一致！";
                    return false;
                }
                if (list[i][(SPXX)0x15] != list[i - 1][(SPXX)0x15])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行是否享受优惠政策的信息不正确，应设置为与折扣商品行一致！";
                    return false;
                }
                if (list[i][(SPXX)0x16] != list[i - 1][(SPXX)0x16])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行优惠政策信息不正确，应设置为与折扣商品行一致！";
                    return false;
                }
                if (list[i][(SPXX)0x17] != list[i - 1][(SPXX)0x17])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行零税率标志信息不正确，应设置为与折扣商品行一致！";
                    return false;
                }
                if (list[i][(SPXX)8] != list[i - 1][(SPXX)8])
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中折扣行税率信息不正确，应设置为与折扣商品行一致！！";
                    return false;
                }
                if ((double.Parse(list[i][(SPXX)7]) + double.Parse(list[i - 1][(SPXX)7])) < 0.0)
                {
                    this.errortip = "单据" + fpxx.xsdjbh + "中存在折扣额大于被折扣商品金额的折扣行信息！";
                    return false;
                }
                list[i - 1][(SPXX)10] = "3";
            }
            if (((fpxx.Zyfplx == (ZYFP_LX)1) && (fpxx.sLv == "")) || ((fpxx.Zyfplx == (ZYFP_LX)10) && (fpxx.sLv == "")))
            {
                this.errortip = "单据" + fpxx.xsdjbh + "中存在税率不能混开的商品明细！";
                return false;
            }
            if ((fpxx.sLv == "") && (fpxx.Zyfplx == (ZYFP_LX)11))
            {
                this.errortip = "差额税单据" + fpxx.xsdjbh + "中明细税率不统一！";
                return false;
            }
            return true;
        }

        public bool CheckCJH(string cjh)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("XHD", cjh);
                return (BaseDAOFactory.GetBaseDAOSQLite().queryValueSQL<string>("aisino.fwkp.fptk.CheckCJH", dictionary) == null);
            }
            catch (Exception exception)
            {
                this.log.Error("校验车架号时异常：" + exception.ToString());
                this.code = "9999";
                return false;
            }
        }

        public bool CheckRedNum(string redNum, FPLX fplx)
        {
            try
            {
                string str = "s";
                if ((int)fplx == 11)
                {
                    str = "f";
                }
                if (NotesUtil.CheckTzdh(redNum, str))
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("TZD", redNum);
                    if (BaseDAOFactory.GetBaseDAOSQLite().queryValueSQL<string>("aisino.fwkp.fptk.CheckRedNum", dictionary) == null)
                    {
                        this.code = "0000";
                        return true;
                    }
                    this.code = "INP-242106";
                    return false;
                }
                this.code = "INP-242181";
                this.codeParams = new string[] { redNum };
                return false;
            }
            catch (Exception exception)
            {
                this.log.Error("校验通知单号时异常：" + exception.ToString());
                this.code = "9999";
                return false;
            }
        }

        public bool CheckRevBlue(string redNum)
        {
            try
            {
                if (NotesUtil.CheckTzdh(redNum))
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("TZD", redNum);
                    ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.ExistRevBlue", dictionary);
                    if ((list == null) || (list.Count == 0))
                    {
                        this.code = "0000";
                        return true;
                    }
                    this.code = "INP-242126";
                    return false;
                }
                this.code = "INP-242181";
                this.codeParams = new string[] { redNum };
                return false;
            }
            catch (Exception exception)
            {
                this.log.Error("查找反蓝发票异常：" + exception.ToString());
                this.code = "9999";
                return false;
            }
        }

        public string Code()
        {
            return this.code;
        }

        public string[] CodeParams()
        {
            return this.codeParams;
        }

        public bool CopyDjfp(Fpxx djfp, Invoice fpxx, bool RedFlag = false)
        {
            try
            {
                if ((((fpxx.Fplx == (FPLX)2) || (fpxx.Fplx == (FPLX)0x33)) || (fpxx.Fplx == (FPLX)0x29)) && (fpxx.Zyfplx == (ZYFP_LX)9))
                {
                    fpxx.Xfmc=djfp.gfmc;
                    fpxx.Xfsh=djfp.gfsh;
                    fpxx.Xfdzdh=djfp.gfdzdh;
                    fpxx.Xfyhzh=djfp.gfyhzh;
                }
                else
                {
                    fpxx.Gfmc=djfp.gfmc;
                    fpxx.Gfsh=djfp.gfsh;
                    fpxx.Gfdzdh=djfp.gfdzdh;
                    fpxx.Gfyhzh=djfp.gfyhzh;
                }
                fpxx.Bz=djfp.bz;
                fpxx.Skr=djfp.skr;
                fpxx.Fhr=djfp.fhr;
                if (!this.IsSWDK())
                {
                    fpxx.Bmbbbh=djfp.bmbbbh;
                }
                fpxx.Xsdjbh=djfp.xsdjbh;
                if (this.IsSWDK())
                {
                    fpxx.Dk_qysh=djfp.dkqysh;
                    fpxx.Dk_qymc=djfp.dkqymc;
                    fpxx.Xfyhzh=djfp.xfyhzh;
                }
                fpxx.DelSpxxAll();
                bool flag = true;
                bool flag2 = fpxx.Hsjbz;
                ZYFP_LX zyfpLx = djfp.Zyfplx;
                if ((int)zyfpLx == 1)
                {
                    flag = fpxx.SetZyfpLx(zyfpLx);
                }
                else
                {
                    fpxx.Hsjbz=flag2;
                }
                if (djfp.sLv == "0.015")
                {
                    fpxx.SetZyfpLx((ZYFP_LX)10);
                }
                if ((djfp.Mxxx != null) && (djfp.Mxxx.Count > 0))
                {
                    for (int i = 0; i < djfp.Mxxx.Count; i++)
                    {
                        int num2 = -1;
                        if (fpxx.CanAddSpxx(1, false))
                        {
                            decimal num3;
                            Dictionary<SPXX, string> dictionary = djfp.Mxxx[i];
                            if ((dictionary[(SPXX)7] != "") && (dictionary[(SPXX)7] != null))
                            {
                                dictionary[(SPXX)7] = SetDecimals(dictionary[(SPXX)7], 2);
                            }
                            if ((dictionary[(SPXX)9] != "") && (dictionary[(SPXX)9] != null))
                            {
                                dictionary[(SPXX)9] = SetDecimals(dictionary[(SPXX)9], 2);
                            }
                            if ((decimal.TryParse(dictionary[(SPXX)7], out num3) && (num3 < decimal.Zero)) && !RedFlag)
                            {
                                int num4;
                                int num5;
                                if ((int)fpxx.Fplx != 0x33)
                                {
                                    if (!int.TryParse(dictionary[0], out num4) || (num4 <= 0))
                                    {
                                        this.code = "INP-242174";
                                        string[] textArray3 = new string[1];
                                        num5 = i + 1;
                                        textArray3[0] = num5.ToString();
                                        this.codeParams = textArray3;
                                        return false;
                                    }
                                    string str3 = fpxx.GetZkLv(i - 1, num4, dictionary[(SPXX)7]);
                                    if (str3 == null)
                                    {
                                        this.code = fpxx.GetCode();
                                        this.codeParams = fpxx.Params;
                                        return false;
                                    }
                                    num2 = fpxx.AddZkxx(i - 1, num4, str3, dictionary[(SPXX)7]);
                                }
                                else
                                {
                                    string str = dictionary[0];
                                    if (str.Length < 5)
                                    {
                                        this.code = "INP-242202";
                                        string[] textArray1 = new string[1];
                                        num5 = i + 1;
                                        textArray1[0] = "第" + num5.ToString() + "个单据折扣行商品名称不正确，应设置为'折扣(X.XXX%)'或'折扣行数Y(X.XXX%)'！";
                                        this.codeParams = textArray1;
                                        return false;
                                    }
                                    if (((str.Substring(0, 4) != "折扣行数") || !int.TryParse(str.Substring(4, 1), out num4)) || (num4 <= 0))
                                    {
                                        if ((str.Substring(0, 2) != "折扣") || ((str.Substring(2, 1) != "(") && (str.Substring(2, 1) != "（")))
                                        {
                                            this.code = "INP-242202";
                                            string[] textArray2 = new string[1];
                                            num5 = i + 1;
                                            textArray2[0] = "第" + num5.ToString() + "个单据折扣行商品名称不正确，应设置为'折扣(X.XXX%)'或'折扣行数Y(X.XXX%)'！";
                                            this.codeParams = textArray2;
                                            return false;
                                        }
                                        num4 = 1;
                                    }
                                    string str2 = fpxx.GetZkLv(i - 1, num4, dictionary[(SPXX)7]);
                                    if (str2 == null)
                                    {
                                        this.code = fpxx.GetCode();
                                        this.codeParams = fpxx.Params;
                                        return false;
                                    }
                                    if (dictionary[(SPXX)9] == "")
                                    {
                                        num2 = fpxx.AddZkxx(i - 1, num4, str2, dictionary[(SPXX)7]);
                                    }
                                    else
                                    {
                                        num2 = fpxx.AddZkxx(i - 1, num4, str2, dictionary[(SPXX)7], dictionary[(SPXX)9]);
                                    }
                                }
                            }
                            else
                            {
                                bool flag4 = false;
                                if (dictionary[(SPXX)11] == "1")
                                {
                                    flag4 = true;
                                }
                                else
                                {
                                    flag4 = (int)zyfpLx == 1;
                                }
                                Spxx spxx = new Spxx(dictionary[(SPXX)0], "", dictionary[(SPXX)8], dictionary[(SPXX)3], dictionary[(SPXX)4], dictionary[(SPXX)5], flag4, zyfpLx);
                                spxx.SL=dictionary[(SPXX)6];
                                spxx.Je=dictionary[(SPXX)7];
                                if (FLBM_lock.isFlbm())
                                {
                                    spxx.Spbh=dictionary[(SPXX)1];
                                    spxx.Flbm=dictionary[(SPXX)20];
                                    spxx.Xsyh=dictionary[(SPXX)0x15];
                                    spxx.Yhsm=dictionary[(SPXX)0x16];
                                    spxx.Lslvbs=dictionary[(SPXX)0x17];
                                }
                                if (dictionary[(SPXX)9] == "")
                                {
                                    num2 = fpxx.AddSpxx("0001", dictionary[(SPXX)8], fpxx.Zyfplx);
                                    if (num2 >= 0)
                                    {
                                        bool flag5 = fpxx.Hsjbz;
                                        fpxx.Hsjbz=false;
                                        flag = fpxx.SetSpmc(i, dictionary[(SPXX)0].ToString());
                                        flag = fpxx.SetDjHsjbz(i, flag4);
                                        flag = fpxx.SetJe(i, dictionary[(SPXX)7]);
                                        if (!flag)
                                        {
                                            break;
                                        }
                                        string str4 = fpxx.GetSpxx(num2)[(SPXX)9];
                                        spxx.Se=str4;
                                        fpxx.Hsjbz=flag5;
                                        fpxx.DelSpxx(num2);
                                        if (num2 == 0)
                                        {
                                            fpxx.SetZyfpLx(zyfpLx);
                                        }
                                        num2 = fpxx.AddSpxx(spxx);
                                    }
                                }
                                else
                                {
                                    spxx.Se=dictionary[(SPXX)9];
                                    num2 = fpxx.AddSpxx(spxx);
                                }
                                if (num2 < 0)
                                {
                                    this.code = fpxx.GetCode();
                                    this.codeParams = fpxx.Params;
                                    return false;
                                }
                            }
                        }
                        if ((num2 < 0) || !flag)
                        {
                            this.code = fpxx.GetCode();
                            this.codeParams = fpxx.Params;
                            return false;
                        }
                    }
                    if ((djfp.Qdxx != null) && (djfp.Qdxx.Count > 0))
                    {
                        for (int j = 0; j < djfp.Qdxx.Count; j++)
                        {
                            int num7 = -1;
                            if (fpxx.CanAddSpxx(1, false))
                            {
                                decimal num8;
                                Dictionary<SPXX, string> dictionary2 = djfp.Qdxx[j];
                                if (decimal.TryParse(dictionary2[(SPXX)7], out num8) && (num8 < decimal.Zero))
                                {
                                    int num9;
                                    if (!int.TryParse(dictionary2[(SPXX)0], out num9) || (num9 <= 0))
                                    {
                                        this.code = "INP-242174";
                                        this.codeParams = new string[] { (j + 1).ToString() };
                                        return false;
                                    }
                                    string str5 = fpxx.GetZkLv(j - 1, num9, dictionary2[(SPXX)7]);
                                    num7 = fpxx.AddZkxx(j - 1, num9, str5, dictionary2[(SPXX)7]);
                                }
                                else
                                {
                                    bool flag6 = (int)zyfpLx == 1;
                                    Spxx spxx2 = new Spxx(dictionary2[(SPXX)0], "", dictionary2[(SPXX)8], dictionary2[(SPXX)3], dictionary2[(SPXX)4], dictionary2[(SPXX)5], flag6, zyfpLx);
                                    spxx2.SL=dictionary2[(SPXX)6];
                                    spxx2.Je=dictionary2[(SPXX)7];
                                    if (FLBM_lock.isFlbm())
                                    {
                                        spxx2.Spbh=dictionary2[(SPXX)1];
                                        spxx2.Flbm=dictionary2[(SPXX)20];
                                        spxx2.Xsyh=dictionary2[(SPXX)0x15];
                                        spxx2.Yhsm=dictionary2[(SPXX)0x16];
                                        spxx2.Lslvbs=dictionary2[(SPXX)0x17];
                                    }
                                    if (dictionary2[(SPXX)9] == "")
                                    {
                                        num7 = fpxx.AddSpxx("0001", dictionary2[(SPXX)8], fpxx.Zyfplx);
                                        if (num7 >= 0)
                                        {
                                            flag = fpxx.SetSpmc(j, dictionary2[(SPXX)0].ToString());
                                            flag = fpxx.SetDjHsjbz(j, flag6);
                                            flag = fpxx.SetJe(j, dictionary2[(SPXX)7]);
                                            if (!flag)
                                            {
                                                break;
                                            }
                                            string str6 = fpxx.GetSpxx(num7)[(SPXX)9];
                                            spxx2.Se=str6;
                                            fpxx.DelSpxx(num7);
                                            if (num7 == 0)
                                            {
                                                fpxx.SetZyfpLx(zyfpLx);
                                            }
                                            num7 = fpxx.AddSpxx(spxx2);
                                        }
                                    }
                                    else
                                    {
                                        spxx2.Se=dictionary2[(SPXX)9];
                                        num7 = fpxx.AddSpxx(spxx2);
                                    }
                                    if (num7 < 0)
                                    {
                                        this.code = fpxx.GetCode();
                                        this.codeParams = fpxx.Params;
                                        return false;
                                    }
                                }
                            }
                            if ((num7 < 0) || !flag)
                            {
                                this.code = fpxx.GetCode();
                                this.codeParams = fpxx.Params;
                                return false;
                            }
                        }
                    }
                }
                fpxx.Hsjbz=flag2;
                return true;
            }
            catch (Exception exception)
            {
                this.code = "INP-241007";
                this.codeParams = new string[] { "导入单据内容不正确：" + exception.Message };
                this.log.Error("导入单据发票数据异常：" + exception.ToString());
                return false;
            }
        }

        public bool CopyHYRedNotice(Fpxx redNotice, Invoice fpxx)
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().OldTaxCode;
                if (!redNotice.xfsh.Equals(fpxx.Xfsh) && !redNotice.xfsh.Equals(str))
                {
                    this.code = "INP-242110";
                    return false;
                }
                fpxx.RedNum=redNotice.redNum;
                fpxx.IsRed=true;
                fpxx.BlueFpdm=redNotice.blueFpdm;
                fpxx.BlueFphm=redNotice.blueFphm;
                fpxx.Bmbbbh=redNotice.bmbbbh;
                if (!TaxCardFactory.CreateTaxCard().QYLX.ISTLQY)
                {
                    string str2 = "";
                    if (fpxx.Fplx == (FPLX)11)
                    {
                        str2 = "f";
                    }
                    fpxx.Bz=NotesUtil.GetRedZyInvNotes(fpxx.RedNum, str2) + fpxx.Bz;
                }
                fpxx.Gfmc=redNotice.gfmc;
                fpxx.Gfsh=redNotice.gfsh;
                fpxx.Shrmc=redNotice.shrmc;
                fpxx.Shrsh=redNotice.shrnsrsbh;
                fpxx.Fhrmc=redNotice.fhrmc;
                fpxx.Fhrsh=redNotice.fhrnsrsbh;
                fpxx.Qyd_jy_ddd=redNotice.qyd;
                fpxx.Yshwxx=redNotice.yshwxx;
                fpxx.Jqbh=redNotice.jqbh;
                fpxx.Czch=redNotice.czch;
                fpxx.Ccdw=redNotice.ccdw;
                fpxx.SetFpSLv(redNotice.sLv);
                fpxx.DelSpxxAll();
                foreach (Dictionary<SPXX, string> dictionary in redNotice.Mxxx)
                {
                    Spxx spxx = new Spxx(dictionary[(SPXX)0], "", redNotice.sLv);
                    spxx.Je=dictionary[(SPXX)7];
                    spxx.Flbm=dictionary[(SPXX)20];
                    spxx.Spbh=dictionary[(SPXX)1];
                    spxx.Yhsm=dictionary[(SPXX)0x16];
                    spxx.Lslvbs=dictionary[(SPXX)0x17];
                    spxx.Xsyh=dictionary[(SPXX)0x15];
                    if (dictionary[(SPXX)9] == "")
                    {
                        decimal num;
                        decimal num2;
                        decimal num3 = new decimal();
                        if (decimal.TryParse(spxx.Je, out num) && decimal.TryParse(redNotice.sLv, out num2))
                        {
                            num3 = decimal.Round(decimal.Multiply(num, num2), 2, MidpointRounding.AwayFromZero);
                        }
                        spxx.Se=num3.ToString("F2");
                    }
                    else
                    {
                        spxx.Se=dictionary[(SPXX)9];
                    }
                    if (fpxx.AddSpxx(spxx) < 0)
                    {
                        this.code = fpxx.GetCode();
                        this.codeParams = fpxx.Params;
                        return false;
                    }
                }
                fpxx.Hjje=redNotice.je;
                fpxx.Hjse=redNotice.se;
                return true;
            }
            catch (Exception exception)
            {
                this.log.Error("货运红字发票通知单数据异常：" + exception.ToString());
                this.code = "INP-242122";
                return false;
            }
        }

        public bool CopyRedNotice(Fpxx redfp, Invoice fpxx)
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().OldTaxCode;
                this.log.Error("oldsh：" + str + "   newSh:：" + fpxx.Xfsh);
                if (!redfp.xfsh.Equals(fpxx.Xfsh) && !redfp.xfsh.Equals(str))
                {
                    this.code = "INP-242110";
                    return false;
                }
                fpxx.RedNum=redfp.redNum;
                fpxx.BlueFpdm=redfp.blueFpdm;
                fpxx.BlueFphm=redfp.blueFphm;
                fpxx.Bmbbbh=redfp.bmbbbh;
                string str2 = "";
                if (fpxx.Fplx == 0)
                {
                    str2 = "s";
                }
                fpxx.Bz=NotesUtil.GetRedZyInvNotes(fpxx.RedNum, str2) + fpxx.Bz;
                fpxx.Gfmc=redfp.gfmc;
                fpxx.Gfsh=redfp.gfsh;
                fpxx.DelSpxxAll();
                bool flag = fpxx.Hsjbz;
                if (((redfp.Zyfplx == (ZYFP_LX)1) || (redfp.Zyfplx == (ZYFP_LX)10)) || (redfp.Zyfplx == (ZYFP_LX)11))
                {
                    fpxx.SetZyfpLx(redfp.Zyfplx);
                }
                else
                {
                    fpxx.Hsjbz=flag;
                }
                foreach (Dictionary<SPXX, string> dictionary in redfp.Mxxx)
                {
                    bool flag3 = (dictionary[(SPXX)11] == "Y") || (dictionary[(SPXX)11] == "1");
                    Spxx spxx = new Spxx(dictionary[(SPXX)0], "", dictionary[(SPXX)8], dictionary[(SPXX)3], dictionary[(SPXX)4], dictionary[(SPXX)5], flag3, redfp.Zyfplx);
                    spxx.SL=dictionary[(SPXX)6];
                    spxx.Je=dictionary[(SPXX)7];
                    spxx.Se=dictionary[(SPXX)9];
                    spxx.Flbm=dictionary[(SPXX)20];
                    spxx.Spbh=dictionary[(SPXX)1];
                    spxx.Yhsm=dictionary[(SPXX)0x16];
                    spxx.Lslvbs=dictionary[(SPXX)0x17];
                    spxx.Xsyh=dictionary[(SPXX)0x15];
                    if (fpxx.AddSpxx(spxx) < 0)
                    {
                        this.code = fpxx.GetCode();
                        this.codeParams = fpxx.Params;
                        return false;
                    }
                }
                fpxx.Hjje=redfp.je;
                fpxx.Hjse=redfp.se;
                return true;
            }
            catch (Exception exception)
            {
                this.log.Error("红字发票通知单数据异常：" + exception.ToString());
                this.code = "INP-242122";
                return false;
            }
        }

        public bool CopyRevBlueNotice(Fpxx revBlueNotice, Invoice fpxx)
        {
            try
            {
                if (!revBlueNotice.xfsh.Equals(fpxx.Xfsh))
                {
                    this.code = "INP-242123";
                    return false;
                }
                fpxx.RedNum=revBlueNotice.redNum;
                fpxx.Bz=revBlueNotice.bz;
                fpxx.Gfmc=revBlueNotice.gfmc;
                fpxx.Gfsh=revBlueNotice.gfsh;
                fpxx.DelSpxxAll();
                Spxx spxx = new Spxx("详见红字发票", "", revBlueNotice.sLv);
                decimal num = Convert.ToDecimal(revBlueNotice.je);
                decimal num2 = Convert.ToDecimal(revBlueNotice.se);
                spxx.Je=Math.Abs(num).ToString();
                spxx.Se=Math.Abs(num2).ToString();
                if (fpxx.AddSpxx(spxx) < 0)
                {
                    this.code = fpxx.GetCode();
                    this.codeParams = fpxx.Params;
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.log.Error("红字发票反蓝通知单数据异常：" + exception.ToString());
                this.code = "INP-242124";
                return false;
            }
        }

        public string DecodeIDEAFile(string fileName, int DecodeType)
        {
            string str = "";
            string str2 = "";
            byte[] buffer = new byte[8];
            IDEAXT ideaxt = new IDEAXT();
            if (DecodeType == 0)
            {
                str = "8972164305GZGMWT";
            }
            else if (DecodeType == 1)
            {
                str = "8972164305GZGLYH";
            }
            byte[] bytes = ToolUtil.GetBytes(str);
            ideaxt.encrypt_subkey(bytes);
            ideaxt.uncrypt_subkey();
            try
            {
                using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    int length = (int) stream.Length;
                    byte[] buffer3 = new byte[length];
                    byte[] buffer4 = new byte[length];
                    stream.Read(buffer3, 0, length);
                    for (int i = 0; i < (length / 8); i++)
                    {
                        byte[] destinationArray = new byte[8];
                        Array.Copy(buffer3, i * 8, destinationArray, 0, 8);
                        ideaxt.encrypt(destinationArray, buffer);
                        for (int j = 0; j < 8; j++)
                        {
                            buffer4[(8 * i) + j] = buffer[j];
                        }
                    }
                    stream.Close();
                    str2 = ToolUtil.GetString(buffer4);
                }
                int num = str2.LastIndexOf("</info>", StringComparison.CurrentCultureIgnoreCase);
                return str2.Substring(0, num + 7);
            }
            catch
            {
                return "";
            }
        }

        private void FillSpxx(ArrayList list, List<Dictionary<SPXX, string>> SpList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                char[] trimChars = new char[] { ' ' };
                item.Add((SPXX)0, dictionary["SPMC"].ToString().Trim(trimChars));
                char[] chArray2 = new char[] { ' ' };
                item.Add((SPXX)2, dictionary["SPSM"].ToString().Trim(chArray2));
                char[] chArray3 = new char[] { ' ' };
                item.Add((SPXX)8, dictionary["SLV"].ToString().Trim(chArray3));
                char[] chArray4 = new char[] { ' ' };
                item.Add((SPXX)10, dictionary["FPHXZ"].ToString().Trim(chArray4));
                char[] chArray5 = new char[] { ' ' };
                item.Add((SPXX)7, dictionary["JE"].ToString().Trim(chArray5));
                char[] chArray6 = new char[] { ' ' };
                item.Add((SPXX)9, dictionary["SE"].ToString().Trim(chArray6));
                char[] chArray7 = new char[] { ' ' };
                item.Add((SPXX)3, dictionary["GGXH"].ToString().Trim(chArray7));
                char[] chArray8 = new char[] { ' ' };
                item.Add((SPXX)4, dictionary["JLDW"].ToString().Trim(chArray8));
                char[] chArray9 = new char[] { ' ' };
                item.Add((SPXX)6, dictionary["SL"].ToString().Trim(chArray9));
                string str = "";
                if (!(dictionary["DJ"] is DBNull))
                {
                    char[] chArray10 = new char[] { ' ' };
                    str = dictionary["DJ"].ToString().Trim(chArray10);
                }
                item.Add((SPXX)5, str);
                item.Add((SPXX)0x18, "");
                item.Add((SPXX)11, this.ObjectToBool(dictionary["HSJBZ"]) ? "1" : "0");
                char[] chArray11 = new char[] { ' ' };
                item.Add((SPXX)13, dictionary["FPMXXH"].ToString().Trim(chArray11));
                if (item[(SPXX)10] != "5")
                {
                    char[] chArray12 = new char[] { ' ' };
                    item.Add((SPXX)20, dictionary["FLBM"].ToString().ToString().Trim(chArray12));
                    char[] chArray13 = new char[] { ' ' };
                    item.Add((SPXX)1, dictionary["SPBH"].ToString().ToString().Trim(chArray13));
                    char[] chArray14 = new char[] { ' ' };
                    item.Add((SPXX)0x15, dictionary["XSYH"].ToString().ToString().Trim(chArray14));
                    char[] chArray15 = new char[] { ' ' };
                    item.Add((SPXX)0x16, dictionary["YHSM"].ToString().ToString().Trim(chArray15));
                    char[] chArray16 = new char[] { ' ' };
                    item.Add((SPXX)0x17, dictionary["LSLVBS"].ToString().ToString().Trim(chArray16));
                }
                else
                {
                    item.Add((SPXX)20, "");
                    item.Add((SPXX)1, "");
                    item.Add((SPXX)0x15, "0");
                    item.Add((SPXX)0x16, "");
                    item.Add((SPXX)0x17, "");
                }
                SpList.Add(item);
            }
        }

        public List<double> GetAllSlv(string SqSlvs, bool isHYFP = false)
        {
            List<double> source = new List<double>();
            List<string> list2 = new List<string>();
            bool flag = false;
            bool flag2 = false;
            char[] separator = new char[] { ';' };
            string[] strArray = SqSlvs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length >= 1)
            {
                char[] chArray2 = new char[] { ',' };
                string[] strArray2 = strArray[0].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                list2.AddRange(strArray2.ToList<string>());
                flag2 = strArray[0].Contains("0.05") || strArray[0].Contains("0.050");
            }
            if (strArray.Length >= 2)
            {
                char[] chArray3 = new char[] { ',' };
                string[] strArray3 = strArray[1].Split(chArray3, StringSplitOptions.RemoveEmptyEntries);
                list2.AddRange(strArray3.ToList<string>());
                flag = strArray[1].Contains("0.05") || strArray[1].Contains("0.050");
            }
            using (List<string>.Enumerator enumerator = list2.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    double result = 0.0;
                    if (double.TryParse(enumerator.Current, out result) && (result != 0.05))
                    {
                        source.Add(result);
                    }
                }
            }
            if (!isHYFP)
            {
                IEnumerator enumerator2;
                string str = "aisino.fwkp.fptk.SelectYhzcs";
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                enumerator2 = baseDAOSQLite.querySQL(str, dictionary).GetEnumerator();
                {
                    while (enumerator2.MoveNext())
                    {
                        string[] textArray1 = new string[] { "、", ",", "，" };
                        string[] strArray4 = ((Dictionary<string, object>) enumerator2.Current)["SLV"].ToString().Split(textArray1, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < strArray4.Length; i++)
                        {
                            if (strArray4[i] == "1.5%_5%")
                            {
                                strArray4[i] = "1.5%";
                            }
                            double item = double.Parse(strArray4[i].Remove(strArray4[i].Length - 1)) / 100.0;
                            if (item == 0.05)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                source.Add(item);
                            }
                        }
                    }
                }
                str = "aisino.fwkp.fptk.SelectSYSLV";
                enumerator2 = baseDAOSQLite.querySQL(str, dictionary).GetEnumerator();
                {
                    while (enumerator2.MoveNext())
                    {
                        string[] textArray2 = new string[] { "、", ",", "，" };
                        string[] strArray5 = ((Dictionary<string, object>) enumerator2.Current)["SLV"].ToString().Split(textArray2, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < strArray5.Length; j++)
                        {
                            if (strArray5[j] == "1.5%_5%")
                            {
                                strArray5[j] = "1.5%";
                            }
                            double num5 = double.Parse(strArray5[j].Remove(strArray5[j].Length - 1)) / 100.0;
                            if (num5 == 0.05)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                source.Add(num5);
                            }
                        }
                    }
                }
            }
            //source = source.GroupBy<double, double>((serializeClass.staticFunc_11)).Select<IGrouping<double, double>, double>((serializeClass.staticFunc_12)).ToList<double>();
            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (source[i] == source[j])
                    {
                        source.Remove(source[i]);
                        break;
                    }
                }
            }
            if (flag2)
            {
                source.Add(0.05);
            }
            if (flag)
            {
                source.Add(1.0);
            }
            return source;
        }

        public string[] GetCurrent(FPLX fplx)
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                int.Parse(Enum.Format(typeof(FPLX), fplx, "d"));
                InvCodeNum currentInvCode = card.GetCurrentInvCode((InvoiceType)fplx);
                if ((currentInvCode.InvTypeCode == null) || (currentInvCode.InvNum == null))
                {
                    this.code = card.ErrCode;
                    if (this.code.StartsWith("TCD_768") || this.code.StartsWith("TCD_769"))
                    {
                        FormMain.CallUpload();
                    }
                    return null;
                }
                if (currentInvCode.InvTypeCode.Equals("0000000000"))
                {
                    this.code = "INP-242104";
                    return null;
                }
                this.code = "000000";
                if ((int)fplx == 0x29)
                {
                    this.PrintTemplate(currentInvCode.InvTypeCode, currentInvCode.EndNum);
                }
                return new string[] { currentInvCode.InvTypeCode, currentInvCode.InvNum };
            }
            catch (Exception exception)
            {
                this.log.Error("读取当前发票代码号码时异常:" + exception.ToString());
                this.code = "9999";
                return null;
            }
        }

        public string GetErrorTip()
        {
            return this.errortip;
        }

        private Fpxx GetHWYSFpxx(FPLX fpzl, string fpdm, int fphm)
        {
            if (((fpxx_tmp != null) && (fpxx_tmp.fplx == (FPLX)fpzl)) && (fpxx_tmp.fpdm.Equals(fpdm) && (int.Parse(fpxx_tmp.fphm) == fphm)))
            {
                return fpxx_tmp;
            }
            string str = Invoice.FPLX2Str(fpzl);
            Fpxx fpxx = new Fpxx(fpzl, fpdm, fphm.ToString("D8"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", str);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            ArrayList list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectXXFP", dictionary);
            if (list.Count > 0)
            {
                Dictionary<string, object> dictionary2 = list[0] as Dictionary<string, object>;
                string data = ToolUtil.FormatDateTimeEx(dictionary2["KPRQ"]);
                fpxx.kprq = this.ObjectToDateTime(data).Date.ToString("yyyy年MM月dd日");
                fpxx.cyrmc = dictionary2["XFMC"].ToString();
                fpxx.cyrnsrsbh = dictionary2["XFSH"].ToString();
                fpxx.mw = dictionary2["MW"].ToString();
                fpxx.hxm = dictionary2["HXM"].ToString();
                fpxx.spfmc = dictionary2["GFMC"].ToString();
                fpxx.spfnsrsbh = dictionary2["GFSH"].ToString().Trim();
                fpxx.shrmc = dictionary2["GFDZDH"].ToString();
                fpxx.shrnsrsbh = dictionary2["CM"].ToString();
                fpxx.fhrmc = dictionary2["XFDZDH"].ToString();
                fpxx.fhrnsrsbh = dictionary2["TYDH"].ToString();
                fpxx.qyd = dictionary2["XFYHZH"].ToString();
                fpxx.yshwxx = dictionary2["YSHWXX"].ToString();
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectFPMX", dictionary);
                this.FillSpxx(list, fpxx.Mxxx);
                fpxx.Qdxx = null;
                if ((bool) dictionary2["QDBZ"])
                {
                    list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectFPQD", dictionary);
                    fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                    this.FillSpxx(list, fpxx.Qdxx);
                }
                fpxx.je = this.ObjectToDecimal(dictionary2["HJJE"]).ToString("F2");
                fpxx.sLv = dictionary2["SLV"].ToString();
                fpxx.se = this.ObjectToDecimal(dictionary2["HJSE"]).ToString("F2");
                fpxx.kpjh = this.ObjectToInt(dictionary2["KPJH"]);
                fpxx.jqbh = dictionary2["JQBH"].ToString();
                fpxx.czch = dictionary2["QYD"].ToString();
                fpxx.ccdw = dictionary2["YYZZH"].ToString();
                fpxx.zgswjgmc = dictionary2["GFYHZH"].ToString();
                fpxx.zgswjgdm = dictionary2["HYBM"].ToString();
                fpxx.xsdjbh = dictionary2["XSDJBH"].ToString();
                fpxx.jmbbh = dictionary2["JMBBH"].ToString();
                fpxx.bsq = this.ObjectToInt(dictionary2["BSQ"]);
                fpxx.jym = dictionary2["JYM"].ToString();
                fpxx.isRed = this.ObjectToDouble(fpxx.je) < 0.0;
                fpxx.sLv = dictionary2["SLV"].ToString();
                fpxx.se = this.ObjectToDecimal(dictionary2["HJSE"]).ToString("F2");
                fpxx.zyspmc = dictionary2["ZYSPMC"].ToString();
                fpxx.zyspsm = dictionary2["SPSM"].ToString();
                fpxx.bz = dictionary2["BZ"].ToString();
                fpxx.kpr = dictionary2["KPR"].ToString();
                fpxx.fhr = dictionary2["FHR"].ToString();
                fpxx.skr = dictionary2["SKR"].ToString();
                fpxx.yysbz = dictionary2["YYSBZ"].ToString();
                fpxx.ssyf = this.ObjectToInt(dictionary2["SSYF"]);
                object obj2 = dictionary2["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (dictionary2["SYH"] != null)
                    {
                        long.TryParse(dictionary2["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                fpxx.zfbz = this.ObjectToBool(dictionary2["ZFBZ"]);
                fpxx.bsbz = this.ObjectToBool(dictionary2["BSBZ"]);
                fpxx.dybz = this.ObjectToBool(dictionary2["DYBZ"]);
                fpxx.xfbz = this.ObjectToBool(dictionary2["XFBZ"]);
                int num = this.ObjectToInt(dictionary2["BSZT"]);
                fpxx.bszt = num;
                fpxx.sign = dictionary2["SIGN"].ToString();
                this.GetYYSBZ(ref fpxx);
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(dictionary2["ZFRQ"].ToString());
                fpxx.bmbbbh = dictionary2["BMBBBH"].ToString();
                fpxx.redNum = dictionary2["HZTZDH"].ToString();
                this.code = "0000";
            }
            else
            {
                fpxx = null;
                this.code = "INP-242108";
            }
            list = null;
            return fpxx;
        }

        private Fpxx GetJDCFpxx(FPLX fpzl, string fpdm, int fphm)
        {
            if (((fpxx_tmp != null) && (fpxx_tmp.fplx == (FPLX)fpzl)) && (fpxx_tmp.fpdm.Equals(fpdm) && (int.Parse(fpxx_tmp.fphm) == fphm)))
            {
                return fpxx_tmp;
            }
            string str = Invoice.FPLX2Str(fpzl);
            Fpxx fpxx = new Fpxx(fpzl, fpdm, fphm.ToString("D8"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", str);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.SelectXXFP", dictionary);
            if (list.Count > 0)
            {
                Dictionary<string, object> dictionary2 = list[0] as Dictionary<string, object>;
                string data = ToolUtil.FormatDateTimeEx(dictionary2["KPRQ"]);
                fpxx.kprq = this.ObjectToDateTime(data).Date.ToString("yyyy-MM-dd");
                fpxx.kpjh = this.ObjectToInt(dictionary2["KPJH"]);
                fpxx.jqbh = dictionary2["JQBH"].ToString();
                fpxx.mw = dictionary2["MW"].ToString();
                fpxx.gfmc = dictionary2["GFMC"].ToString();
                fpxx.sfzhm = dictionary2["XSBM"].ToString();
                fpxx.gfsh = dictionary2["GFSH"].ToString().Trim();
                fpxx.cllx = dictionary2["GFDZDH"].ToString();
                fpxx.cpxh = dictionary2["XFDZ"].ToString();
                fpxx.cd = dictionary2["KHYHMC"].ToString();
                fpxx.hgzh = dictionary2["CM"].ToString();
                fpxx.jkzmsh = dictionary2["TYDH"].ToString();
                fpxx.sjdh = dictionary2["QYD"].ToString();
                fpxx.fdjhm = dictionary2["ZHD"].ToString();
                fpxx.clsbdh = dictionary2["XHD"].ToString();
                fpxx.sccjmc = dictionary2["SCCJMC"].ToString();
                fpxx.xfmc = dictionary2["XFMC"].ToString();
                fpxx.xfdh = dictionary2["XFDH"].ToString();
                fpxx.xfsh = dictionary2["XFSH"].ToString();
                fpxx.xfzh = dictionary2["KHYHZH"].ToString();
                fpxx.xfdz = dictionary2["XFDZDH"].ToString();
                fpxx.xfyh = dictionary2["XFYHZH"].ToString();
                fpxx.sLv = dictionary2["SLV"].ToString();
                fpxx.se = this.ObjectToDecimal(dictionary2["HJSE"]).ToString("F2");
                fpxx.zgswjgmc = dictionary2["GFYHZH"].ToString();
                fpxx.zgswjgdm = dictionary2["HYBM"].ToString();
                fpxx.je = this.ObjectToDecimal(dictionary2["HJJE"]).ToString("F2");
                fpxx.dw = dictionary2["YYZZH"].ToString();
                fpxx.xcrs = dictionary2["MDD"].ToString();
                fpxx.bz = dictionary2["BZ"].ToString();
                fpxx.kpr = dictionary2["KPR"].ToString();
                fpxx.fhr = dictionary2["FHR"].ToString();
                fpxx.skr = dictionary2["SKR"].ToString();
                fpxx.xsdjbh = dictionary2["XSDJBH"].ToString();
                fpxx.jmbbh = dictionary2["JMBBH"].ToString();
                fpxx.bsq = this.ObjectToInt(dictionary2["BSQ"]);
                fpxx.jym = dictionary2["JYM"].ToString();
                fpxx.zfbz = this.ObjectToBool(dictionary2["ZFBZ"]);
                fpxx.bsbz = this.ObjectToBool(dictionary2["BSBZ"]);
                fpxx.dybz = this.ObjectToBool(dictionary2["DYBZ"]);
                fpxx.xfbz = this.ObjectToBool(dictionary2["XFBZ"]);
                fpxx.redNum = dictionary2["HZTZDH"].ToString();
                fpxx.je = this.ObjectToDecimal(dictionary2["HJJE"]).ToString("F2");
                fpxx.isRed = this.ObjectToDouble(fpxx.je) < 0.0;
                fpxx.zyspmc = dictionary2["ZYSPMC"].ToString();
                fpxx.zyspsm = dictionary2["SPSM_BM"].ToString();
                fpxx.yysbz = dictionary2["YYSBZ"].ToString();
                fpxx.ssyf = this.ObjectToInt(dictionary2["SSYF"]);
                object obj2 = dictionary2["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (dictionary2["SYH"] != null)
                    {
                        long.TryParse(dictionary2["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                fpxx.Qdxx = null;
                fpxx.Mxxx = null;
                int num = this.ObjectToInt(dictionary2["BSZT"]);
                fpxx.bszt = num;
                fpxx.sign = dictionary2["SIGN"].ToString();
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(dictionary2["ZFRQ"].ToString());
                this.GetYYSBZ(ref fpxx);
                fpxx.bmbbbh = dictionary2["BMBBBH"].ToString();
                fpxx.redNum = dictionary2["HZTZDH"].ToString();
                this.code = "0000";
            }
            else
            {
                fpxx = null;
                this.code = "INP-242108";
            }
            list = null;
            dictionary = null;
            return fpxx;
        }

        public string GetJskClock()
        {
            DateTime cardClock;
            try
            {
                TaxCard card1 = TaxCardFactory.CreateTaxCard();
                card1.GetCardClock();
                cardClock = card1.GetCardClock();
                cardClock = cardClock.Date;
                return cardClock.ToString("yyyy年MM月dd日");
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡时钟时异常:" + exception.ToString());
                cardClock = new DateTime();
                return cardClock.Date.ToString("yyyy年MM月dd日");
            }
        }

        public string GetMachineNum()
        {
            try
            {
                string invControlNum = TaxCardFactory.CreateTaxCard().GetInvControlNum();
                if (!string.IsNullOrEmpty(invControlNum))
                {
                    return invControlNum;
                }
                return "";
            }
            catch (Exception exception)
            {
                this.log.Error("读取机器编号时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        private Fpxx GetPTZYFpxx(FPLX fpzl, string fpdm, int fphm)
        {
            if (((fpxx_tmp != null) && (fpxx_tmp.fplx == (FPLX)fpzl)) && (fpxx_tmp.fpdm.Equals(fpdm) && (int.Parse(fpxx_tmp.fphm) == fphm)))
            {
                return fpxx_tmp;
            }
            string str = Invoice.FPLX2Str(fpzl);
            Fpxx fpxx = new Fpxx(fpzl, fpdm, fphm.ToString("D8"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", str);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
            ArrayList list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectXXFP", dictionary);
            if (list.Count > 0)
            {
                Dictionary<string, object> dictionary2 = list[0] as Dictionary<string, object>;
                fpxx.xsdjbh = dictionary2["XSDJBH"].ToString();
                fpxx.kpjh = this.ObjectToInt(dictionary2["KPJH"]);
                fpxx.jqbh = dictionary2["JQBH"].ToString();
                fpxx.jmbbh = dictionary2["JMBBH"].ToString();
                string data = ToolUtil.FormatDateTimeEx(dictionary2["KPRQ"]);
                fpxx.kprq = this.ObjectToDateTime(data).Date.ToString("yyyy年MM月dd日");
                this.log.Debug("before_kprq:" + dictionary2["KPRQ"].ToString() + "after:" + fpxx.kprq);
                fpxx.bsq = this.ObjectToInt(dictionary2["BSQ"]);
                fpxx.jym = dictionary2["JYM"].ToString();
                fpxx.zfbz = this.ObjectToBool(dictionary2["ZFBZ"]);
                fpxx.bsbz = this.ObjectToBool(dictionary2["BSBZ"]);
                fpxx.dybz = this.ObjectToBool(dictionary2["DYBZ"]);
                fpxx.xfbz = this.ObjectToBool(dictionary2["XFBZ"]);
                fpxx.mw = dictionary2["MW"].ToString();
                fpxx.hxm = dictionary2["HXM"].ToString();
                fpxx.hzfw = fpxx.hxm.Length > 0;
                fpxx.redNum = dictionary2["HZTZDH"].ToString();
                fpxx.gfmc = dictionary2["GFMC"].ToString();
                fpxx.gfsh = dictionary2["GFSH"].ToString().Trim();
                fpxx.gfdzdh = dictionary2["GFDZDH"].ToString();
                fpxx.gfyhzh = dictionary2["GFYHZH"].ToString();
                fpxx.xfmc = dictionary2["XFMC"].ToString();
                fpxx.xfsh = dictionary2["XFSH"].ToString();
                fpxx.xfdzdh = dictionary2["XFDZDH"].ToString();
                fpxx.xfyhzh = dictionary2["XFYHZH"].ToString();
                fpxx.je = this.ObjectToDecimal(dictionary2["HJJE"]).ToString("F2");
                fpxx.isRed = this.ObjectToDouble(fpxx.je) < 0.0;
                fpxx.sLv = dictionary2["SLV"].ToString();
                fpxx.se = this.ObjectToDecimal(dictionary2["HJSE"]).ToString("F2");
                fpxx.zyspmc = dictionary2["ZYSPMC"].ToString();
                fpxx.zyspsm = dictionary2["SPSM"].ToString();
                fpxx.bz = dictionary2["BZ"].ToString();
                fpxx.kpr = dictionary2["KPR"].ToString();
                fpxx.fhr = dictionary2["FHR"].ToString();
                fpxx.skr = dictionary2["SKR"].ToString();
                fpxx.yysbz = dictionary2["YYSBZ"].ToString();
                fpxx.ssyf = this.ObjectToInt(dictionary2["SSYF"]);
                fpxx.bmbbbh = dictionary2["BMBBBH"].ToString();
                if (fpxx.sLv == "0.015")
                {
                    fpxx.Zyfplx = (ZYFP_LX)10;
                }
                if ((fpxx.yysbz != null) && (fpxx.yysbz.Length >= 10))
                {
                    if (fpxx.fplx == 0)
                    {
                        if (fpxx.yysbz.Substring(2, 1) == "3")
                        {
                            fpxx.Zyfplx = (ZYFP_LX)2;
                        }
                        else if ((fpxx.yysbz[8] == '0') && (fpxx.sLv == "0.05"))
                        {
                            fpxx.Zyfplx = (ZYFP_LX)1;
                        }
                    }
                    else if ((fpxx.fplx == (FPLX)2) || (fpxx.fplx == (FPLX)0x33))
                    {
                        if (fpxx.yysbz.Substring(5, 1) == "1")
                        {
                            fpxx.Zyfplx = (ZYFP_LX)8;
                        }
                        else if (fpxx.yysbz.Substring(5, 1) == "2")
                        {
                            fpxx.Zyfplx = (ZYFP_LX)9;
                        }
                    }
                    if (fpxx.yysbz[8] == '2')
                    {
                        fpxx.Zyfplx = (ZYFP_LX)11;
                    }
                }
                object obj2 = dictionary2["SBBZ"];
                if (obj2 != null)
                {
                    ulong.TryParse(obj2.ToString(), out fpxx.keyFlagNo);
                    if (dictionary2["SYH"] != null)
                    {
                        long.TryParse(dictionary2["SYH"].ToString(), out fpxx.invQryNo);
                    }
                }
                else
                {
                    fpxx.keyFlagNo = 0L;
                    fpxx.invQryNo = 0L;
                }
                fpxx.Qdxx = null;
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectFPMX", dictionary);
                this.FillSpxx(list, fpxx.Mxxx);
                if (this.ObjectToBool(dictionary2["QDBZ"]))
                {
                    list = baseDAOSQLite.querySQL("aisino.fwkp.fptk.SelectFPQD", dictionary);
                    fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                    this.FillSpxx(list, fpxx.Qdxx);
                }
                int num = this.ObjectToInt(dictionary2["BSZT"]);
                fpxx.bszt = num;
                fpxx.sign = dictionary2["SIGN"].ToString();
                fpxx.zfsj = ToolUtil.FormatDateTimeEx(dictionary2["ZFRQ"].ToString());
                this.GetYYSBZ(ref fpxx);
                fpxx.redNum = dictionary2["HZTZDH"].ToString();
                this.code = "0000";
            }
            else
            {
                fpxx = null;
                this.code = "INP-242108";
            }
            list = null;
            return fpxx;
        }

        public List<string> GetPzbs()
        {
            List<string> list = new List<string>();
            List<PZSQType> pZSQType = TaxCardFactory.CreateTaxCard().SQInfo.PZSQType;
            if (pZSQType != null)
            {
                foreach (PZSQType type in pZSQType)
                {
                    list.Add(type.invType.ToString());
                }
            }
            return list;
        }

        public decimal GetTotalRedJe(string blueFpdm, string blueFphm)
        {
            decimal result = new decimal();
            if (!string.IsNullOrEmpty(blueFpdm) && !string.IsNullOrEmpty(blueFphm))
            {
                string str = blueFpdm + "_" + blueFphm.PadLeft(8, '0');
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("LzfpDmHm", str);
                ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.SelectTotalRedJe", dictionary);
                if (list.Count > 0)
                {
                    decimal.TryParse((list[0] as Dictionary<string, object>)["HZHJJE"].ToString(), out result);
                }
            }
            return result;
        }

        public string GetXfdh()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().Telephone;
                return ((str == null) ? "" : str);
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方电话时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public string GetXfdz()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().Address;
                return ((str == null) ? "" : str);
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方地址时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public string GetXfdzdh()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                return new StringBuilder().Append(card.Address).Append(" ").Append(card.Telephone).ToString();
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方地址电话时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public string GetXfmc()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().Corporation;
                return ((str == null) ? "" : str);
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方名称时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public string GetXfsh()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().TaxCode;
                return ((str == null) ? "" : str);
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方税号时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public string GetXfyhzh()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().BankAccount;
                return ((str == null) ? "" : str);
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡销方银行账号时异常:" + exception.ToString());
                return string.Empty;
            }
        }

        public Fpxx GetXxfp(FPLX fpzl, string fpdm, int fphm)
        {
            Fpxx fpxx;
            try
            {
                if ((int)fpzl <= 11)
                {
                    if ((fpzl == null) || ((int)fpzl == 2))
                    {
                        goto Label_0022;
                    }
                    if ((int)fpzl == 11)
                    {
                        return this.GetHWYSFpxx(fpzl, fpdm, fphm);
                    }
                    goto Label_0046;
                }
                if ((int)fpzl == 12)
                {
                    return this.GetJDCFpxx(fpzl, fpdm, fphm);
                }
                if (((int)fpzl != 0x29) && ((int)fpzl != 0x33))
                {
                    goto Label_0046;
                }
            Label_0022:
                return this.GetPTZYFpxx(fpzl, fpdm, fphm);
            Label_0046:
                fpxx = null;
            }
            catch (Exception exception)
            {
                this.code = "9999";
                this.log.Error("读取销项发票信息时异常:" + exception.ToString());
                fpxx = null;
            }
            return fpxx;
        }

        private void GetYYSBZ(ref Fpxx fpxx)
        {
            if ((fpxx.yysbz != null) && (fpxx.yysbz.Length >= 10))
            {
                switch (fpxx.yysbz[2])
                {
                    case '1':
                        fpxx.Zyfplx = (ZYFP_LX)6;
                        break;

                    case '2':
                        fpxx.Zyfplx = (ZYFP_LX)7;
                        break;

                    case '3':
                        fpxx.Zyfplx = (ZYFP_LX)2;
                        break;
                }
                char ch = fpxx.yysbz[4];
                if (ch == '1')
                {
                    fpxx.isNewJdcfp = false;
                }
                else
                {
                    fpxx.isNewJdcfp = true;
                }
                switch (fpxx.yysbz[5])
                {
                    case '1':
                        fpxx.Zyfplx = (ZYFP_LX)8;
                        break;

                    case '2':
                        fpxx.Zyfplx = (ZYFP_LX)9;
                        break;
                }
                if ((this.ObjectToDouble(fpxx.je) == 0.0) && fpxx.zfbz)
                {
                    fpxx.isBlankWaste = true;
                }
                else
                {
                    fpxx.isBlankWaste = false;
                }
            }
        }

        public string[] GetZgswjg()
        {
            try
            {
                string zGJGDMMC = TaxCardFactory.CreateTaxCard().SQInfo.ZGJGDMMC;
                if (!string.IsNullOrEmpty(zGJGDMMC))
                {
                    char[] separator = new char[] { ',' };
                    string[] strArray = zGJGDMMC.Split(separator);
                    if (strArray.Length == 2)
                    {
                        return strArray;
                    }
                }
                return null;
            }
            catch (Exception exception)
            {
                this.log.Error("读取主管税务机关及代码时异常:" + exception.ToString());
                return null;
            }
        }

        public bool IsSWDK()
        {
            try
            {
                string str = TaxCardFactory.CreateTaxCard().TaxCode;
                ushort companyType = TaxCardFactory.CreateTaxCard().StateInfo.CompanyType;
                if ((!string.IsNullOrEmpty(str) && (str.Length == 15)) && (str.Substring(8, 2) == "DK"))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.log.Error("读取金税卡接口时异常:" + exception.ToString());
            }
            return false;
        }

        private bool ObjectToBool(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            string str = obj.ToString().Trim();
            return (((str == "1") || (str.ToLower() == "true")) || str.Equals("是"));
        }

        private DateTime ObjectToDateTime(object data)
        {
            if (data == null)
            {
                return DateTime.Now;
            }
            DateTime now = DateTime.Now;
            DateTime.TryParse(data.ToString().Trim(), out now);
            return now;
        }

        private decimal ObjectToDecimal(object data)
        {
            if (data == null)
            {
                return decimal.Zero;
            }
            decimal result = new decimal();
            decimal.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        private double ObjectToDouble(object data)
        {
            if (data == null)
            {
                return 0.0;
            }
            double result = 0.0;
            double.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        private int ObjectToInt(object data)
        {
            if (data == null)
            {
                return 0;
            }
            int result = 0;
            int.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        public bool ParseBz(Fpxx _fpxx)
        {
            if (_fpxx.bz.Contains("差额征税"))
            {
                if (_fpxx.sLv == "0.015")
                {
                    return false;
                }
                _fpxx.Zyfplx = (ZYFP_LX)11;
                int index = _fpxx.bz.IndexOf("差额征税");
                int num2 = _fpxx.bz.IndexOf("。", index);
                if (num2 <= index)
                {
                    return false;
                }
                string str = _fpxx.bz.Substring(index + 5, (num2 - index) - 5);
                _fpxx.bz = _fpxx.bz.Remove(index, (num2 - index) + 1);
                new List<Dictionary<SPXX, string>>();
                ((_fpxx.Mxxx.Count == null) ? _fpxx.Qdxx : _fpxx.Mxxx)[0][(SPXX)0x18] = _fpxx.isRed ? ("-" + str) : str;
            }
            return true;
        }

        private Fpxx ParseHYHZTZDxml(XmlDocument xd)
        {
            try
            {
                Fpxx fpxx = new Fpxx((FPLX)11, "", "");
                XmlNode node = xd.SelectSingleNode("/INFO");
                string innerXml = node.SelectSingleNode("XXBLX").InnerXml;
                fpxx.redNum = node.SelectSingleNode("BH").InnerXml;
                fpxx.kprq = node.SelectSingleNode("TKRQ").InnerXml;
                string text2 = node.SelectSingleNode("FPLX_DM").InnerXml;
                string text3 = node.SelectSingleNode(" SFJXSEZC").InnerXml;
                string text4 = node.SelectSingleNode(" SQF").InnerXml;
                fpxx.blueFpdm = node.SelectSingleNode("YFPDM").InnerXml;
                fpxx.blueFphm = node.SelectSingleNode("YFPHM").InnerXml;
                if (node.SelectSingleNode("BMB_BBH") == null)
                {
                    fpxx.bmbbbh = "";
                }
                else
                {
                    fpxx.bmbbbh = node.SelectSingleNode("BMB_BBH").InnerXml;
                }
                fpxx.isRed = true;
                fpxx.gfmc = node.SelectSingleNode("GMFMC").InnerXml;
                fpxx.gfsh = node.SelectSingleNode("GMFDM").InnerXml;
                fpxx.xfsh = node.SelectSingleNode("XSFDM").InnerXml;
                fpxx.xfmc = node.SelectSingleNode("XSFMC").InnerXml;
                fpxx.je = node.SelectSingleNode("HJJE").InnerXml;
                fpxx.sLv = node.SelectSingleNode("SL").InnerXml;
                fpxx.se = node.SelectSingleNode("SE").InnerXml;
                string text5 = node.SelectSingleNode("JSHJ").InnerXml;
                fpxx.zgswjgdm = node.SelectSingleNode("XSFSWJG_DM").InnerXml;
                fpxx.zgswjgmc = node.SelectSingleNode("XSFSWJG_MC").InnerXml;
                string text6 = node.SelectSingleNode("GMFSWJG_DM").InnerXml;
                string text7 = node.SelectSingleNode("GMFSWJG_MC").InnerXml;
                fpxx.shrnsrsbh = node.SelectSingleNode("SHRSBH").InnerXml;
                fpxx.shrmc = node.SelectSingleNode("SHRMC").InnerXml;
                fpxx.fhrnsrsbh = node.SelectSingleNode("FHRSBH").InnerXml;
                fpxx.fhrmc = node.SelectSingleNode("FHRMC").InnerXml;
                fpxx.yshwxx = node.SelectSingleNode("YSHWXX").InnerXml;
                fpxx.jqbh = node.SelectSingleNode("JQBH").InnerXml;
                fpxx.czch = node.SelectSingleNode("CZCH").InnerXml;
                fpxx.ccdw = node.SelectSingleNode("CCDW").InnerXml;
                List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
                foreach (XmlNode node2 in xd.SelectNodes("/INFO/FYXMJJE/ZB"))
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    item.Add(0, node2.SelectSingleNode("FYXM").InnerXml);
                    item.Add((SPXX)7, node2.SelectSingleNode("JE").InnerXml);
                    if (node2.SelectSingleNode("SPBM") == null)
                    {
                        item.Add((SPXX)20, "");
                    }
                    else
                    {
                        string str = node2.SelectSingleNode("SPBM").InnerXml;
                        if ((str.Length < 0x13) && (str != ""))
                        {
                            throw new Exception("商品分类编码错误！");
                        }
                        item.Add((SPXX)20, str);
                    }
                    if (node2.SelectSingleNode("ZXBM") == null)
                    {
                        item.Add((SPXX)1, "");
                    }
                    else
                    {
                        item.Add((SPXX)1, node2.SelectSingleNode("ZXBM").InnerXml);
                    }
                    if (node2.SelectSingleNode("YHZCBS") == null)
                    {
                        item.Add((SPXX)0x15, "0");
                    }
                    else
                    {
                        string str2 = node2.SelectSingleNode("YHZCBS").InnerXml;
                        if (((str2 != "") && (str2 != "0")) && (str2 != "1"))
                        {
                            throw new Exception("是否享受优惠格式错误！");
                        }
                        item.Add((SPXX)0x15, (str2 == "") ? "0" : str2);
                    }
                    if (node2.SelectSingleNode("ZZSTSGL") == null)
                    {
                        item.Add((SPXX)0x16, "");
                    }
                    else
                    {
                        item.Add((SPXX)0x16, node2.SelectSingleNode("ZZSTSGL").InnerXml);
                    }
                    if (node2.SelectSingleNode("LSLBS") == null)
                    {
                        item.Add((SPXX)0x17, "");
                    }
                    else
                    {
                        item.Add((SPXX)0x17, node2.SelectSingleNode("LSLBS").InnerXml);
                    }
                    if (node2.SelectSingleNode("MXSE") != null)
                    {
                        item.Add((SPXX)9, node2.SelectSingleNode("MXSE").InnerXml);
                    }
                    else
                    {
                        item.Add((SPXX)9, "");
                    }
                    list.Add(item);
                }
                fpxx.Mxxx = list;
                return fpxx;
            }
            catch (Exception exception)
            {
                this.log.Error("解析货运红字发票通知单格式异常：" + exception.ToString());
                this.code = "INP-242109";
            }
            return null;
        }

        private Fpxx ParseHZTZDxml(XmlDocument xd)
        {
            try
            {
                Fpxx fpxx = new Fpxx(0, "", "");
                XmlNode node = xd.SelectSingleNode("/Info/RedInvReqBill");
                if (node == null)
                {
                    this.code = "INP-242109";
                    return null;
                }
                fpxx.redNum = node.SelectSingleNode("ReqBillNo").InnerXml;
                fpxx.blueFpdm = node.SelectSingleNode("TypeCode").InnerXml;
                fpxx.blueFphm = node.SelectSingleNode("InvNo").InnerXml;
                fpxx.isRed = true;
                fpxx.bmbbbh = node.SelectSingleNode("SPBMBBH").InnerXml;
                fpxx.gfmc = node.SelectSingleNode("BuyerName").InnerXml;
                fpxx.gfsh = node.SelectSingleNode("BuyTaxCode").InnerXml;
                fpxx.xfsh = node.SelectSingleNode("SellTaxCode").InnerXml;
                fpxx.je = node.SelectSingleNode("Amount").InnerXml;
                fpxx.sLv = node.SelectSingleNode("TaxRate").InnerXml;
                fpxx.se = node.SelectSingleNode("Tax").InnerXml;
                List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
                foreach (XmlNode node2 in xd.SelectNodes("/Info/RedInvReqBill/RedInvReqBillMx/GoodsMx"))
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    item.Add((SPXX)0, node2.SelectSingleNode("GoodsName").InnerXml);
                    item.Add((SPXX)3, node2.SelectSingleNode("GoodsGgxh").InnerXml);
                    item.Add((SPXX)4, node2.SelectSingleNode("GoodsUnit").InnerXml);
                    item.Add((SPXX)20, node2.SelectSingleNode("SPBM").InnerXml);
                    item.Add((SPXX)1, node2.SelectSingleNode("QYSPBM").InnerXml);
                    item.Add((SPXX)0x15, node2.SelectSingleNode("SYYHZCBZ").InnerXml);
                    item.Add((SPXX)0x16, node2.SelectSingleNode("YHZC").InnerXml);
                    item.Add((SPXX)0x17, node2.SelectSingleNode("LSLBZ").InnerXml);
                    string innerXml = node2.SelectSingleNode("GoodsPrice").InnerXml;
                    if ((innerXml == null) || (innerXml.ToUpper() == "NULL"))
                    {
                        innerXml = "";
                    }
                    item.Add((SPXX)5, innerXml);
                    item.Add((SPXX)8, node2.SelectSingleNode("GoodsTaxRate").InnerXml);
                    string str2 = node2.SelectSingleNode("GoodsNum").InnerXml;
                    if ((str2 == null) || (str2.ToUpper() == "NULL"))
                    {
                        str2 = "";
                    }
                    item.Add((SPXX)6, str2);
                    item.Add((SPXX)7, node2.SelectSingleNode("GoodsJE").InnerXml);
                    item.Add((SPXX)9, node2.SelectSingleNode("GoodsSE").InnerXml);
                    item.Add((SPXX)11, node2.SelectSingleNode("HS_BZ").InnerXml);
                    list.Add(item);
                }
                fpxx.Mxxx = list;
                if (node.SelectSingleNode("IsMutiRate").InnerXml == "1")
                {
                    fpxx.sLv = "";
                }
                return fpxx;
            }
            catch (Exception exception)
            {
                this.log.Error("解析红字发票通知单格式异常：" + exception.ToString());
                this.code = "INP-242109";
            }
            return null;
        }

        private Fpxx ParseHZXXBxml(XmlDocument xd)
        {
            Fpxx fpxx = new Fpxx(0, "", "");
            try
            {
                XmlNode node = xd.SelectSingleNode("/FPXT/OUTPUT/DATA/RedInvReqBill");
                if (node == null)
                {
                    this.code = "INP-242109";
                    return null;
                }
                fpxx.redNum = node.SelectSingleNode("ResBillNo").InnerXml;
                string innerXml = node.SelectSingleNode("BillType").InnerXml;
                fpxx.blueFpdm = node.SelectSingleNode("TypeCode").InnerXml;
                fpxx.blueFphm = node.SelectSingleNode("InvNo").InnerXml;
                fpxx.isRed = true;
                string str = node.SelectSingleNode("IsMutiRate").InnerXml;
                if (str == "1")
                {
                    fpxx.sLv = "";
                }
                fpxx.kprq = node.SelectSingleNode("Date").InnerXml;
                fpxx.gfmc = node.SelectSingleNode("BuyerName").InnerXml;
                fpxx.gfsh = node.SelectSingleNode("BuyTaxCode").InnerXml;
                fpxx.xfmc = node.SelectSingleNode("SellerName").InnerXml;
                fpxx.xfsh = node.SelectSingleNode("SellTaxCode").InnerXml;
                fpxx.je = node.SelectSingleNode("Amount").InnerXml;
                fpxx.sLv = node.SelectSingleNode("TaxRate").InnerXml;
                fpxx.se = node.SelectSingleNode("Tax").InnerXml;
                if ((((node.SelectSingleNode("SLBZ") == null) && (str != "1")) && ((fpxx.sLv != "") && (double.Parse(fpxx.sLv) == 0.05))) || ((((node.SelectSingleNode("SLBZ") != null) && (node.SelectSingleNode("SLBZ").InnerXml == "1")) && ((str != "1") && (fpxx.sLv != ""))) && (double.Parse(fpxx.sLv) == 0.05)))
                {
                    fpxx.Zyfplx = (ZYFP_LX)1;
                }
                if ((((node.SelectSingleNode("SLBZ") != null) && (node.SelectSingleNode("SLBZ").InnerXml == "1")) && ((str != "1") && (fpxx.sLv != ""))) && (double.Parse(fpxx.sLv) == 0.015))
                {
                    fpxx.Zyfplx = (ZYFP_LX)10;
                }
                if ((node.SelectSingleNode("SLBZ") != null) && (node.SelectSingleNode("SLBZ").InnerXml == "2"))
                {
                    fpxx.Zyfplx = (ZYFP_LX)11;
                }
                XmlNode node2 = node.SelectSingleNode("SPBMBBH");
                if (node2 == null)
                {
                    fpxx.bmbbbh = "";
                }
                else
                {
                    fpxx.bmbbbh = node2.InnerXml;
                }
                List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
                foreach (XmlNode node3 in node.SelectNodes("RedInvReqBillMx/GoodsMx"))
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    item.Add((SPXX)0, node3.SelectSingleNode("GoodsName").InnerXml);
                    item.Add((SPXX)4, node3.SelectSingleNode("GoodsUnit").InnerXml);
                    item.Add((SPXX)5, node3.SelectSingleNode("GoodsPrice").InnerXml);
                    item.Add((SPXX)8, node3.SelectSingleNode("GoodsTaxRate").InnerXml);
                    item.Add((SPXX)3, node3.SelectSingleNode("GoodsGgxh").InnerXml);
                    item.Add((SPXX)6, node3.SelectSingleNode("GoodsNum").InnerXml);
                    item.Add((SPXX)7, node3.SelectSingleNode("GoodsJE").InnerXml);
                    item.Add((SPXX)9, node3.SelectSingleNode("GoodsSE").InnerXml);
                    item.Add((SPXX)11, node3.SelectSingleNode("HS_BZ").InnerXml);
                    if (node3.SelectSingleNode("SPBM") == null)
                    {
                        item.Add((SPXX)20, "");
                    }
                    else
                    {
                        string str2 = node3.SelectSingleNode("SPBM").InnerXml;
                        if ((str2.Length < 0x13) && (str2 != ""))
                        {
                            throw new Exception("商品分类编码错误！");
                        }
                        item.Add((SPXX)20, str2);
                    }
                    if (node3.SelectSingleNode("QYSPBM") == null)
                    {
                        item.Add((SPXX)1, "");
                    }
                    else
                    {
                        item.Add((SPXX)1, node3.SelectSingleNode("QYSPBM").InnerXml);
                    }
                    if (node3.SelectSingleNode("SYYHZCBZ") == null)
                    {
                        item.Add((SPXX)0x15, "0");
                    }
                    else
                    {
                        string str3 = node3.SelectSingleNode("SYYHZCBZ").InnerXml;
                        if (((str3 != "") && (str3 != "0")) && (str3 != "1"))
                        {
                            throw new Exception("是否享受优惠格式错误！");
                        }
                        item.Add((SPXX)0x15, (str3 == "") ? "0" : str3);
                    }
                    if (node3.SelectSingleNode("YHZC") == null)
                    {
                        item.Add((SPXX)0x16, "");
                    }
                    else
                    {
                        item.Add((SPXX)0x16, node3.SelectSingleNode("YHZC").InnerXml);
                    }
                    if (node3.SelectSingleNode("LSLBZ") == null)
                    {
                        item.Add((SPXX)0x17, "");
                    }
                    else
                    {
                        item.Add((SPXX)0x17, node3.SelectSingleNode("LSLBZ").InnerXml);
                    }
                    list.Add(item);
                }
                fpxx.Mxxx = list;
                return fpxx;
            }
            catch (Exception exception)
            {
                this.log.Error("解析红字发票信息表格式异常：" + exception.ToString());
                this.code = "INP-242109";
            }
            return null;
        }

        public void PrintTemplate(string fpdm, string endnum)
        {
            endnum = JSFPJSelect.endnum.ToString();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("LBDM", fpdm);
            ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.SelectDYMB", dictionary);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
                    if (decimal.Parse(endnum) == this.ObjectToDecimal(dictionary2["JZZH"]))
                    {
                        if (dictionary2["JPGG"].ToString().Length != 0)
                        {
                            InvoiceForm_JS.dy_mb = dictionary2["JPGG"].ToString();
                            return;
                        }
                        this.log.Error("打印模板未设置为空，将按照默认模板打印");
                        return;
                    }
                }
            }
            else
            {
                this.log.Error("打印模板读取失败，将按照默认模板打印");
            }
        }

        public Fpxx ProcessHYHZXXBxml(string fileXML)
        {
            if (string.IsNullOrEmpty(fileXML))
            {
                return null;
            }
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(fileXML);
            return this.ParseHYHZTZDxml(xd);
        }

        public Fpxx ProcessHYRedNotice(string fileName)
        {
            try
            {
                int startIndex = fileName.LastIndexOf('.');
                if ((startIndex > -1) && (startIndex < (fileName.Length - 1)))
                {
                    XmlDocument xd = new XmlDocument();
                    if (fileName.Substring(startIndex).ToUpper() == ".XML")
                    {
                        xd.Load(fileName);
                        return this.ParseHYHZTZDxml(xd);
                    }
                    return null;
                }
            }
            catch (Exception exception)
            {
                this.log.Error("解析红字发票通知单格式异常：" + exception.ToString());
                this.code = "INP-242109";
            }
            return null;
        }

        public Fpxx ProcessHZTZDxml(string fileXML)
        {
            if (string.IsNullOrEmpty(fileXML))
            {
                return null;
            }
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(fileXML);
            if (xd.SelectSingleNode("/FPXT/OUTPUT/DATA/RedInvReqBill") != null)
            {
                return this.ParseHZXXBxml(xd);
            }
            return this.ParseHZTZDxml(xd);
        }

        public Fpxx ProcessRedNotice(string fileName)
        {
            try
            {
                int startIndex = fileName.LastIndexOf('.');
                if ((startIndex > -1) && (startIndex < (fileName.Length - 1)))
                {
                    XmlDocument xd = new XmlDocument();
                    string str = fileName.Substring(startIndex);
                    if (str.ToUpper() == ".XML")
                    {
                        xd.Load(fileName);
                    }
                    else if (str.ToUpper() == ".DAT")
                    {
                        string str2 = this.DecodeIDEAFile(fileName, 1);
                        if (string.IsNullOrEmpty(str2))
                        {
                            return null;
                        }
                        xd.LoadXml(str2);
                    }
                    else
                    {
                        return null;
                    }
                    if (xd.SelectSingleNode("/FPXT/OUTPUT/DATA/RedInvReqBill") != null)
                    {
                        return this.ParseHZXXBxml(xd);
                    }
                    return this.ParseHZTZDxml(xd);
                }
            }
            catch (Exception exception)
            {
                this.log.Error("解析红字发票信息表格式异常：" + exception.ToString());
                this.code = "INP-242109";
            }
            return null;
        }

        public string QueryXzqy(string fpdm)
        {
            string str = "未知";
            try
            {
                if ((fpdm == null) || (fpdm.Length < 4))
                {
                    return str;
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if (fpdm.Length == 10)
                {
                    dictionary.Add("BM", string.Format("{0}{1}", fpdm.Substring(0, 2), "00"));
                }
                else if (fpdm.Length == 12)
                {
                    dictionary.Add("BM", string.Format("{0}{1}", fpdm.Substring(1, 2), "00"));
                }
                else
                {
                    return str;
                }
                string str2 = BaseDAOFactory.GetBaseDAOSQLite().queryValueSQL<string>("aisino.fwkp.fptk.QueryXzqy", dictionary);
                if (str2 == null)
                {
                    return str;
                }
                str = str2.Trim();
                if (str.Contains("黑龙江") || str.Contains("内蒙古"))
                {
                    return str.Substring(0, 3);
                }
                str = str.Substring(0, 2);
            }
            catch (Exception exception)
            {
                this.log.Error("读取行政区域信息时异常：" + exception.ToString());
                this.code = "9999";
            }
            return str;
        }

        public bool SaveXxfp(Fpxx fp)
        {
            bool flag;
            try
            {
                string str;
                List<string> sqls = new List<string>();
                List<Dictionary<string, object>> paramAll = new List<Dictionary<string, object>>();
                this.log.Info("开始构造DB操作对象....");
                FPLX fplx = fp.fplx;
                if ((int)fplx <= 11)
                {
                    if (((int)fplx == 0) || ((int)fplx == 2))
                    {
                        goto Label_004D;
                    }
                    if ((int)fplx == 11)
                    {
                        goto Label_0081;
                    }
                    goto Label_009B;
                }
                if ((int)fplx == 12)
                {
                    goto Label_0067;
                }
                if (((int)fplx != 0x29) && ((int)fplx != 0x33))
                {
                    goto Label_009B;
                }
            Label_004D:
                sqls.Add("aisino.fwkp.fptk.AddXXFP");
                paramAll.Add(this.SetPTZYFpxxParam(fp));
                goto Label_00B3;
            Label_0067:
                sqls.Add("aisino.fwkp.fptk.AddXXFP_HWYS_JDC");
                paramAll.Add(this.SetJDCFpxxParam(fp));
                goto Label_00B3;
            Label_0081:
                sqls.Add("aisino.fwkp.fptk.AddXXFP_HWYS_JDC");
                paramAll.Add(this.SetHWYSFpxxParam(fp));
                goto Label_00B3;
            Label_009B:
                this.log.Info("返回空值");
                return false;
            Label_00B3:
                str = "aisino.fwkp.fptk.FPQD";
                string sqlID = "aisino.fwkp.fptk.FPMX";
                List<Dictionary<SPXX, string>> qdxx = fp.Qdxx;
                List<Dictionary<SPXX, string>> mxxx = fp.Mxxx;
                if ((mxxx != null) && (mxxx.Count > 0))
                {
                    this.SetSpxxParam(sqlID, sqls, paramAll, fp, mxxx);
                }
                if ((qdxx != null) && (qdxx.Count > 0))
                {
                    this.SetSpxxParam(str, sqls, paramAll, fp, qdxx);
                }
                if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0x3e8))
                {
                    UpLoadCheckState.SetFpxfState(true);
                }
                if (this.dao.updateSQLTransaction(sqls.ToArray(), paramAll) > 0)
                {
                    this.log.Info("存库成功....");
                    return true;
                }
                this.log.Info("存库失败....");
                flag = false;
            }
            catch (Exception exception)
            {
                this.log.Error("保存销项发票信息时异常:" + exception.ToString());
                flag = false;
            }
            finally
            {
                if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0x3e8))
                {
                    UpLoadCheckState.SetFpxfState(false);
                }
            }
            return flag;
        }

        internal static string SetDecimals(string a, int decimals)
        {
            return decimal.Round(decimal.Parse(a), decimals, MidpointRounding.AwayFromZero).ToString("f" + decimals);
        }

        public void SetErrorTip(string tmp)
        {
            this.errortip = tmp;
        }

        private Dictionary<string, object> SetHWYSFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", fp.fphm);
            dictionary.Add("SPSM_BM", "");
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("KPRQ", ToolUtil.FormatDateTimeEx(fp.kprq));
            dictionary.Add("XFMC", fp.cyrmc);
            dictionary.Add("XFSH", fp.cyrnsrsbh);
            dictionary.Add("GFMC", fp.spfmc);
            dictionary.Add("GFSH", fp.spfnsrsbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("HXM", "");
            dictionary.Add("GFDZDH", fp.shrmc);
            dictionary.Add("CM", fp.shrnsrsbh);
            dictionary.Add("XFDZDH", fp.fhrmc);
            dictionary.Add("TYDH", fp.fhrnsrsbh);
            dictionary.Add("XFYHZH", fp.qyd);
            dictionary.Add("YSHWXX", fp.yshwxx);
            dictionary.Add("HJJE", fp.je);
            if ((fp.sLv == "") || (fp.sLv == "NULL"))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", fp.sLv);
            }
            dictionary.Add("HJSE", fp.se);
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("JQBH", fp.jqbh);
            dictionary.Add("QYD", fp.czch);
            dictionary.Add("YYZZH", fp.ccdw);
            dictionary.Add("HYBM", fp.zgswjgdm);
            dictionary.Add("GFYHZH", fp.zgswjgmc);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("ZYSPMC", fp.zyspmc);
            dictionary.Add("SPSM", fp.zyspsm);
            dictionary.Add("DYBZ", fp.dybz);
            dictionary.Add("QDBZ", fp.Qdxx != null);
            dictionary.Add("ZFBZ", fp.zfbz);
            dictionary.Add("BSBZ", fp.bsbz);
            dictionary.Add("XFBZ", fp.xfbz);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("YYSBZ", fp.yysbz);
            if (fp.keyFlagNo == 0)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("KHYHMC", "");
            dictionary.Add("KHYHZH", "");
            dictionary.Add("ZHD", "");
            dictionary.Add("XHD", "");
            dictionary.Add("MDD", "");
            dictionary.Add("XFDZ", "");
            dictionary.Add("XFDH", "");
            dictionary.Add("SCCJMC", "");
            dictionary.Add("XSBM", fp.sfzhm);
            dictionary.Add("PZRQ", "1899-12-30 23:59:59");
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if ((fp.isRed && !string.IsNullOrEmpty(fp.blueFpdm)) && !string.IsNullOrEmpty(fp.blueFphm))
            {
                str = fp.blueFpdm + "_" + fp.blueFphm.PadLeft(8, '0');
            }
            dictionary.Add("LZDMHM", str);
            dictionary.Add("DZSYH", (fp.dzsyh == null) ? "" : fp.dzsyh);
            dictionary.Add("KPSXH", (fp.kpsxh == null) ? "" : fp.kpsxh);
            dictionary.Add("BMBBBH", (fp.bmbbbh == null) ? "" : fp.bmbbbh);
            return dictionary;
        }

        private Dictionary<string, object> SetJDCFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", fp.fphm);
            dictionary.Add("KPRQ", ToolUtil.FormatDateTimeEx(fp.kprq));
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("JQBH", fp.jqbh);
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("HXM", "");
            dictionary.Add("GFMC", fp.gfmc);
            dictionary.Add("GFSH", fp.gfsh);
            dictionary.Add("XSBM", fp.sfzhm);
            dictionary.Add("GFDZDH", fp.cllx);
            dictionary.Add("XFDZ", fp.cpxh);
            dictionary.Add("KHYHMC", fp.cd);
            dictionary.Add("CM", fp.hgzh);
            dictionary.Add("TYDH", fp.jkzmsh);
            dictionary.Add("QYD", fp.sjdh);
            dictionary.Add("ZHD", fp.fdjhm);
            dictionary.Add("XHD", fp.clsbdh);
            dictionary.Add("XFMC", fp.xfmc);
            dictionary.Add("XFDH", fp.xfdh);
            dictionary.Add("XFSH", fp.xfsh);
            dictionary.Add("KHYHZH", fp.xfzh);
            dictionary.Add("XFDZDH", fp.xfdz);
            dictionary.Add("XFYHZH", fp.xfyh);
            if ((fp.sLv == "") || (fp.sLv == "NULL"))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", fp.sLv);
            }
            dictionary.Add("HJSE", fp.se);
            dictionary.Add("GFYHZH", fp.zgswjgmc);
            dictionary.Add("HYBM", fp.zgswjgdm);
            dictionary.Add("HJJE", fp.je);
            dictionary.Add("YYZZH", fp.dw);
            dictionary.Add("MDD", fp.xcrs);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("ZYSPMC", fp.zyspmc);
            dictionary.Add("SPSM", "");
            dictionary.Add("SPSM_BM", fp.zyspsm);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("DYBZ", fp.dybz);
            dictionary.Add("QDBZ", fp.Qdxx != null);
            dictionary.Add("ZFBZ", fp.zfbz);
            dictionary.Add("BSBZ", fp.bsbz);
            dictionary.Add("XFBZ", fp.xfbz);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("PZRQ", "1899-12-30 23:59:59");
            dictionary.Add("YYSBZ", fp.yysbz);
            dictionary.Add("YSHWXX", fp.yshwxx);
            dictionary.Add("SCCJMC", fp.sccjmc);
            if (fp.keyFlagNo == 0)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if ((fp.isRed && !string.IsNullOrEmpty(fp.blueFpdm)) && !string.IsNullOrEmpty(fp.blueFphm))
            {
                str = fp.blueFpdm + "_" + fp.blueFphm.PadLeft(8, '0');
            }
            dictionary.Add("LZDMHM", str);
            dictionary.Add("DZSYH", (fp.dzsyh == null) ? "" : fp.dzsyh);
            dictionary.Add("KPSXH", (fp.kpsxh == null) ? "" : fp.kpsxh);
            dictionary.Add("BMBBBH", (fp.bmbbbh == null) ? "" : fp.bmbbbh);
            return dictionary;
        }

        private Dictionary<string, object> SetPTZYFpxxParam(Fpxx fp)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
            dictionary.Add("FPDM", fp.fpdm);
            dictionary.Add("FPHM", fp.fphm);
            dictionary.Add("KPJH", fp.kpjh);
            dictionary.Add("XSDJBH", fp.xsdjbh);
            dictionary.Add("JMBBH", fp.jmbbh);
            if ((fp.jqbh == null) || (fp.jqbh == ""))
            {
                dictionary.Add("JQBH", null);
            }
            else
            {
                dictionary.Add("JQBH", fp.jqbh);
            }
            dictionary.Add("GFMC", fp.gfmc);
            dictionary.Add("XFMC", fp.xfmc);
            dictionary.Add("GFSH", fp.gfsh);
            dictionary.Add("XFSH", fp.xfsh);
            if (fp.fplx == (FPLX)0x29)
            {
                dictionary.Add("GFYHZH", "");
                dictionary.Add("XFYHZH", "");
                dictionary.Add("GFDZDH", "");
                dictionary.Add("XFDZDH", "");
            }
            else
            {
                dictionary.Add("GFYHZH", fp.gfyhzh);
                dictionary.Add("XFYHZH", fp.xfyhzh);
                dictionary.Add("GFDZDH", fp.gfdzdh);
                dictionary.Add("XFDZDH", fp.xfdzdh);
            }
            dictionary.Add("HXM", fp.hxm);
            dictionary.Add("MW", fp.mw);
            dictionary.Add("KPRQ", ToolUtil.FormatDateTimeEx(fp.kprq));
            dictionary.Add("HJJE", fp.je);
            if (((fp.sLv == "") || (fp.sLv == "NULL")) || (fp.sLv == null))
            {
                dictionary.Add("SLV", null);
            }
            else
            {
                dictionary.Add("SLV", fp.sLv);
            }
            dictionary.Add("HJSE", fp.se);
            dictionary.Add("ZYSPMC", fp.zyspmc);
            dictionary.Add("SPSM", fp.zyspsm);
            dictionary.Add("BZ", fp.bz);
            dictionary.Add("KPR", fp.kpr);
            dictionary.Add("SKR", fp.skr);
            dictionary.Add("FHR", fp.fhr);
            dictionary.Add("DYBZ", fp.dybz);
            dictionary.Add("QDBZ", fp.Qdxx != null);
            dictionary.Add("ZFBZ", fp.zfbz);
            dictionary.Add("BSBZ", fp.bsbz);
            dictionary.Add("XFBZ", fp.xfbz);
            dictionary.Add("JYM", fp.jym);
            dictionary.Add("BSQ", fp.bsq);
            dictionary.Add("SSYF", fp.ssyf);
            dictionary.Add("HZTZDH", fp.redNum);
            dictionary.Add("PZRQ", "1899-12-30 23:59:59");
            dictionary.Add("YYSBZ", fp.yysbz);
            if (fp.keyFlagNo == 0)
            {
                dictionary.Add("SBBZ", "");
                dictionary.Add("SYH", "");
            }
            else
            {
                dictionary.Add("SBBZ", fp.keyFlagNo.ToString());
                dictionary.Add("SYH", fp.invQryNo.ToString());
            }
            dictionary.Add("BSZT", fp.bszt);
            dictionary.Add("SIGN", (fp.sign == null) ? "" : fp.sign);
            dictionary.Add("ZFRQ", ToolUtil.FormatDateTimeEx(fp.zfsj));
            dictionary.Add("WSPZHM", "");
            string str = "";
            if ((fp.isRed && !string.IsNullOrEmpty(fp.blueFpdm)) && !string.IsNullOrEmpty(fp.blueFphm))
            {
                str = fp.blueFpdm + "_" + fp.blueFphm.PadLeft(8, '0');
            }
            dictionary.Add("LZDMHM", str);
            dictionary.Add("DZSYH", (fp.dzsyh == null) ? "" : fp.dzsyh);
            dictionary.Add("KPSXH", (fp.kpsxh == null) ? "" : fp.kpsxh);
            dictionary.Add("BMBBBH", (fp.bmbbbh == null) ? "" : fp.bmbbbh);
            return dictionary;
        }

        private void SetSpxxParam(string sqlID, List<string> sqls, List<Dictionary<string, object>> paramAll, Fpxx fp, List<Dictionary<SPXX, string>> sps)
        {
            int num = 1;
            foreach (Dictionary<SPXX, string> dictionary in sps)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("FPZL", Invoice.FPLX2Str(fp.fplx));
                item.Add("FPDM", fp.fpdm);
                item.Add("FPHM", fp.fphm);
                item.Add("FPMXXH", num);
                item.Add("XSDJBH", fp.xsdjbh);
                item.Add("FPHXZ", dictionary[(SPXX)10]);
                item.Add("JE", dictionary[(SPXX)7]);
                if (((dictionary[(SPXX)8] == "") || (dictionary[(SPXX)8] == "NULL")) || (dictionary[(SPXX)8] == null))
                {
                    item.Add("SLV", null);
                }
                else
                {
                    item.Add("SLV", dictionary[(SPXX)8]);
                }
                item.Add("SE", ((dictionary[(SPXX)9] == "NULL") || (dictionary[(SPXX)9] == "")) ? null : dictionary[(SPXX)9]);
                item.Add("SPMC", dictionary[(SPXX)0]);
                item.Add("SPSM", dictionary[(SPXX)2]);
                if (fp.fplx == (FPLX)0x29)
                {
                    item.Add("GGXH", "");
                    item.Add("JLDW", "");
                }
                else
                {
                    item.Add("GGXH", dictionary[(SPXX)3]);
                    item.Add("JLDW", dictionary[(SPXX)4]);
                }
                if ((dictionary[(SPXX)6] == "") || (dictionary[(SPXX)6] == "NULL"))
                {
                    item.Add("SL", null);
                }
                else
                {
                    item.Add("SL", dictionary[(SPXX)6]);
                }
                if ((dictionary[(SPXX)5] == "") || (dictionary[(SPXX)5] == "NULL"))
                {
                    item.Add("DJ", null);
                }
                else
                {
                    item.Add("DJ", dictionary[(SPXX)5]);
                }
                item.Add("HSJBZ", dictionary[(SPXX)11].Equals("1"));
                if (dictionary[(SPXX)10] != "5")
                {
                    item.Add("SPBH", dictionary[(SPXX)1]);
                }
                else
                {
                    item.Add("SPBH", "");
                }
                item.Add("DJMXXH", null);
                if (fp.fplx == (FPLX)0x29)
                {
                    item.Add("FLBM", dictionary[(SPXX)20]);
                    item.Add("XSYH", dictionary[(SPXX)0x15]);
                    item.Add("YHSM", dictionary[(SPXX)0x16]);
                    item.Add("LSLVBS", dictionary[(SPXX)0x17]);
                }
                else if (fp.fplx == (FPLX)11)
                {
                    item.Add("FLBM", dictionary[(SPXX)20]);
                    item.Add("XSYH", dictionary[(SPXX)0x15]);
                    item.Add("YHSM", dictionary[(SPXX)0x16]);
                    item.Add("LSLVBS", dictionary[(SPXX)0x17]);
                }
                else if (dictionary[(SPXX)10] != "5")
                {
                    item.Add("FLBM", dictionary[(SPXX)20]);
                    item.Add("XSYH", dictionary[(SPXX)0x15]);
                    item.Add("YHSM", dictionary[(SPXX)0x16]);
                    item.Add("LSLVBS", dictionary[(SPXX)0x17]);
                }
                else
                {
                    item.Add("FLBM", "");
                    item.Add("XSYH", "0");
                    item.Add("YHSM", "");
                    item.Add("LSLVBS", "");
                }
                sqls.Add(sqlID);
                paramAll.Add(item);
                num++;
            }
        }

        public void WSPZ_dr15slv(Fpxx fpxx, FPLX fplx)
        {
            fpxx.Zyfplx = (ZYFP_LX)10;
            List<Dictionary<SPXX, string>> mxxx = fpxx.Mxxx;
            double num = 0.0;
            double num2 = 0.0;
            if ((int)fplx != 0)
            {
                using (List<Dictionary<SPXX, string>>.Enumerator enumerator = mxxx.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        num2 = double.Parse(enumerator.Current[(SPXX)7]);
                        num += num2;
                    }
                }
                fpxx.je = num.ToString();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class serializeClass
        {
            public static readonly FpManager.serializeClass instance = new FpManager.serializeClass();
            public static Func<double, double> staticFunc_11;
            public static Func<IGrouping<double, double>, double> staticFunc_12;

            internal double lvlistFunc_11(double p)
            {
                return p;
            }

            internal double slvlistFunc_12(IGrouping<double, double> p)
            {
                return p.Key;
            }
        }
    }
}

