namespace ns1
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using log4net;
    using ns7;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows.Forms;

    internal class Class12 : ILogin
    {
        private bool bool_0;
        private static readonly ILog ilog_0;
        private readonly List<string> list_0;
        private readonly List<string> list_1;
        private string string_0;
        private string string_1;

        static Class12()
        {
            
            ilog_0 = LogManager.GetLogger(typeof(Class12).FullName);
        }

        public Class12()
        {
            
            this.list_0 = new List<string>();
            this.list_1 = new List<string>();
        }

        public bool Login(bool bool_1, string string_2)
        {
            //逻辑修改:无Key调试时需要把下面Region里面的TaxCard登录校验屏蔽掉
            //#region
            //bool flag = false;
            //LoginForm form = new LoginForm();
            //if (bool_1)
            //{
            //    flag = false;
            //    return true;
            //}
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    flag = true;
            //    string_2 = form.method_1();
            //}
            //if (!bool_1 && !flag)
            //{
            //    return false;
            //}
            //TaxCardFactory.CardType = CTaxCardType.const_7;
            //TaxCard card = TaxCardFactory.CreateTaxCard();
            //card.CustomMessageBox += new TaxCard.CustomMsgBox(this.method_0);
            //Label_010F:
            //if (!card.TaxCardOpen(form.method_4()) && (card.RetCode != 0))
            //{
            //    if (card.RetCode == -1111)
            //    {
            //        ChangeCertPass pass = new ChangeCertPass();
            //        pass.method_2(form.method_4());
            //        pass.method_3().Enabled = false;
            //        pass.method_4(form.method_1());
            //        if (pass.ShowDialog() != DialogResult.OK)
            //        {
            //            MessageBoxHelper.Show("取消证书默认口令修改，退出系统！", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //            return false;
            //        }
            //        if (form.method_3())
            //        {
            //            PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(form.method_1())).Replace("-", ""), form.method_2() + "-" + pass.method_5());
            //        }
            //        else
            //        {
            //            PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(form.method_1())).Replace("-", ""), string.Empty);
            //        }
            //        MessageBoxHelper.Show("证书口令修改成功，请重新登录系统！", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        card.CloseDevice();
            //        Process.Start(Assembly.GetExecutingAssembly().Location);
            //        return false;
            //    }
            //    if (!card.ErrCode.StartsWith("CA_"))
            //    {
            //        MessageManager.ShowMsgBox(card.ErrCode);
            //        return false;
            //    }
            //    int num4 = Convert.ToInt32(card.ErrCode.Split(new char[] { '_' })[1]);
            //    switch (num4)
            //    {
            //        case 0x3810002a:
            //            MessageBoxHelper.Show("证书口令长度不足8个字符", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //            return false;

            //        case 0x381063c0:
            //            MessageBoxHelper.Show("证书密码被锁死，请解锁后重新登录", "证书登录错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //            return false;

            //        default:
            //            {
            //                if ((num4 >= 0x381063cf) || (num4 <= 0x381063c0))
            //                {
            //                    MessageManager.ShowMsgBox(card.ErrCode);
            //                    return false;
            //                }
            //                int num5 = num4 - 0x381063c0;
            //                CertLogin login = new CertLogin(num5);
            //                if (login.ShowDialog() != DialogResult.OK)
            //                {
            //                    MessageBoxHelper.Show("取消证书登录，退出系统！", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                    return false;
            //                }
            //                card.CloseDevice();
            //                form.method_5(login.method_1());
            //                goto Label_010F;
            //            }
            //    }
            //}
            //MessageHelper.MsgWait("正在更新发票上传标志，不要插拔金税盘或关闭电源，请耐心等待...");
            //ilog_0.Error("开始更新上传标志##########...");
            //List<ClearCardInfo> list = card.UpdateUpLoadFlag();
            //if (list != null)
            //{
            //    ilog_0.Error("UpdateUpLoadFlag返回结果个数#############：" + list.Count);
            //}
            //if ((list != null) && (list.Count > 0))
            //{
            //    foreach (ClearCardInfo info in list)
            //    {
            //        try
            //        {
            //            ilog_0.Error("金税盘RetCode##########:" + info.CSTime.ToString());
            //            if (info.CSTime != "")
            //            {
            //                string str2 = string.Format("【{0}】 抄报清卡", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //                string str = "update XXFP set BSZT = 1, BSBZ = 1, BSRZ = '" + str2 + "' where BSQ = " + info.InvPeriod.ToString() + " and SSYF = " + info.CSTime.ToString();
            //                if (card.SubSoftVersion == "Linux")
            //                {
            //                    str = "update XXFP set BSBZ = 1, BSRZ = '" + str2 + "' where BSQ = " + info.InvPeriod.ToString() + " and SSYF = " + info.CSTime.ToString();
            //                }
            //                switch (info.InvKind)
            //                {
            //                    case 0x29:
            //                    case 0x33:
            //                    case 12:
            //                        str = str + " and (FPZL = 'j' or FPZL = 'p' or FPZL = 'q')";
            //                        if (card.QYLX.ISJDC || card.QYLX.ISPTFPDZ)
            //                        {
            //                            PropertyUtil.SetValue(AttributeName.JDCQKDateName, this.method_2(12, info.CSTime));
            //                        }
            //                        if (card.QYLX.ISPTFPJSP)
            //                        {
            //                            PropertyUtil.SetValue(AttributeName.JSPQKDateName, this.method_2(0x29, info.CSTime));
            //                        }
            //                        break;

            //                    case 0:
            //                    case 2:
            //                        str = str + " and (FPZL = 's' or FPZL = 'c')";
            //                        if (card.QYLX.ISPTFP || card.QYLX.ISZYFP)
            //                        {
            //                            PropertyUtil.SetValue(AttributeName.ZPQKDateName, this.method_2(0, info.CSTime));
            //                        }
            //                        break;

            //                    case 11:
            //                        str = str + " and FPZL = 'f'";
            //                        if (card.QYLX.ISHY)
            //                        {
            //                            PropertyUtil.SetValue(AttributeName.HYQKDateName, this.method_2(11, info.CSTime));
            //                        }
            //                        break;
            //                }
            //                BaseDAOSQLite baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite() as BaseDAOSQLite;
            //                if (baseDAOSQLite != null)
            //                {
            //                    baseDAOSQLite.method_1(str);
            //                }
            //                else
            //                {
            //                    MessageBoxHelper.Show("获取数据库实例失败。");
            //                }
            //                continue;
            //            }
            //            ilog_0.Error("更新发票上传标志接口返回时间错误：" + info.CSTime);
            //        }
            //        catch (Exception exception)
            //        {
            //            MessageBoxHelper.Show(exception.Message);
            //            ilog_0.Error(exception.ToString());
            //        }
            //    }
            //}
            //ilog_0.Error("结束更新上传标志##########...");
            //MessageHelper.MsgWait();
            //string regionCode = card.RegionCode;
            //if (string.Equals(card.SoftVersion, "FWKP_V2.0_Svr_Client"))
            //{
            //    PropertyUtil.SetValue("MAIN_CODE", card.TaxCode);
            //    PropertyUtil.SetValue("MAIN_MACHINE", card.Machine.ToString());
            //    PropertyUtil.SetValue("MAIN_ORGCODE", card.CorpCode.Substring(0, 6));
            //}
            //if (!string.Equals(card.SoftVersion, "FWKP_V2.0_Svr_Client"))
            //{
            //    if ((regionCode != null) && (regionCode.Length > 2))
            //    {
            //        string str4 = regionCode.Substring(0, 2);
            //        if (str4 == "91")
            //        {
            //            str4 = "50";
            //        }
            //        regionCode = str4 + regionCode.Substring(2);
            //    }
            //    if (((card.TaxMode == CTaxCardMode.tcmHave) && (card.TaxCode.Length > 15)) && (RegisterManager.SetupOrgCode != regionCode))
            //    {
            //        ilog_0.Info("注册表中地区编号：" + RegisterManager.SetupOrgCode);
            //        ilog_0.Info("金税设备中地区编号：" + regionCode);
            //        MessageBoxHelper.Show("安装时输入的地区编号和金税设备中不一致，请重新安装！");
            //        return false;
            //    }
            //    if (RegisterManager.SetupTaxCode != card.TaxCode)
            //    {
            //        MessageBoxHelper.Show("安装时输入的税号和金税设备中不一致，请重新安装！");
            //        return false;
            //    }
            //    if (RegisterManager.SetupMachine != card.Machine)
            //    {
            //        MessageBoxHelper.Show("安装时输入的开票机号和金税设备中不一致，请重新安装！");
            //        return false;
            //    }
            //    if (!RegisterManager.PreMakeInvoice())
            //    {
            //        MessageBoxHelper.Show("开票注册文件验证失败！");
            //    }
            //}
            //#endregion
            string_2 = "管理员";
            this.method_1(string_2);
            return true;
        }

        public bool Login(string string_2, string string_3)
        {
            try
            {
                new LoginForm();
                TaxCardFactory.CardType = CTaxCardType.const_7;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                card.CustomMessageBox += new TaxCard.CustomMsgBox(this.method_0);
                if (!card.TaxCardOpen(string_3))
                {
                    return false;
                }
                MessageHelper.MsgWait("正在更新发票上传标志，不要插拔金税盘或关闭电源，请耐心等待...");
                ilog_0.Error("开始更新上传标志=========...");
                List<ClearCardInfo> list = card.UpdateUpLoadFlag();
                if (list != null)
                {
                    ilog_0.Error("UpdateUpLoadFlag返回结果个数=========：" + list.Count);
                }
                if ((list != null) && (list.Count > 0))
                {
                    foreach (ClearCardInfo info in list)
                    {
                        ilog_0.Error("taxCard.RetCode=========" + card.RetCode);
                        try
                        {
                            ilog_0.Error("taxCard.RetCode=========" + info.CSTime.ToString());
                            if (info.CSTime != "")
                            {
                                string str2 = string.Format("【{0}】 抄报清卡", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                string message = "update XXFP set BSZT = 1, BSBZ = 1, BSRZ = '" + str2 + "' where BSQ = " + info.InvPeriod.ToString() + " and SSYF = " + info.CSTime.ToString();
                                if (card.SubSoftVersion == "Linux")
                                {
                                    message = "update XXFP set BSBZ = 1, BSRZ = '" + str2 + "' where BSQ = " + info.InvPeriod.ToString() + " and SSYF = " + info.CSTime.ToString();
                                }
                                switch (info.InvKind)
                                {
                                    case 0x29:
                                    case 0x33:
                                    case 12:
                                        message = message + " and (FPZL = 'j' or FPZL = 'p' or FPZL = 'q')";
                                        if (card.QYLX.ISJDC || card.QYLX.ISPTFPDZ)
                                        {
                                            PropertyUtil.SetValue(AttributeName.JDCQKDateName, this.method_2(12, info.CSTime));
                                        }
                                        if (card.QYLX.ISPTFPJSP)
                                        {
                                            PropertyUtil.SetValue(AttributeName.JSPQKDateName, this.method_2(0x29, info.CSTime));
                                        }
                                        break;

                                    case 0:
                                    case 2:
                                        message = message + " and (FPZL = 's' or FPZL = 'c')";
                                        if (card.QYLX.ISPTFP || card.QYLX.ISZYFP)
                                        {
                                            PropertyUtil.SetValue(AttributeName.ZPQKDateName, this.method_2(0, info.CSTime));
                                        }
                                        break;

                                    case 11:
                                        message = message + " and FPZL = 'f'";
                                        if (card.QYLX.ISHY)
                                        {
                                            PropertyUtil.SetValue(AttributeName.HYQKDateName, this.method_2(11, info.CSTime));
                                        }
                                        break;
                                }
                                BaseDAOSQLite baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite() as BaseDAOSQLite;
                                ilog_0.Error(message);
                                if (baseDAOSQLite != null)
                                {
                                    baseDAOSQLite.method_1(message);
                                }
                                else
                                {
                                    MessageBoxHelper.Show("获取数据库实例失败。");
                                }
                                continue;
                            }
                            ilog_0.Error("更新发票上传标志接口返回时间错误：" + info.CSTime);
                        }
                        catch (Exception exception)
                        {
                            MessageBoxHelper.Show(exception.Message);
                        }
                    }
                }
                ilog_0.Error("结束更新上传标志=========...");
                MessageHelper.MsgWait();
                string regionCode = card.RegionCode;
                if ((regionCode != null) && (regionCode.Length > 2))
                {
                    string str4 = regionCode.Substring(0, 2);
                    if (str4 == "91")
                    {
                        str4 = "50";
                    }
                    regionCode = str4 + regionCode.Substring(2);
                }
                if (((card.TaxMode == CTaxCardMode.tcmHave) && (card.TaxCode.Length > 15)) && (RegisterManager.SetupOrgCode != regionCode))
                {
                    ilog_0.Info("注册表中地区编号：" + RegisterManager.SetupOrgCode);
                    ilog_0.Info("金税设备中地区编号：" + regionCode);
                    MessageBoxHelper.Show("安装时输入的地区编号和金税设备中不一致，请重新安装！");
                    return false;
                }
                if (RegisterManager.SetupTaxCode != card.TaxCode)
                {
                    MessageBoxHelper.Show("安装时输入的税号和金税设备中不一致，请重新安装！");
                    return false;
                }
                if (RegisterManager.SetupMachine != card.Machine)
                {
                    MessageBoxHelper.Show("安装时输入的开票机号和金税设备中不一致，请重新安装！");
                    return false;
                }
                if (!RegisterManager.PreMakeInvoice())
                {
                    MessageBoxHelper.Show("开票注册文件验证失败！");
                }
                this.method_1(string_2);
                return true;
            }
            catch (Exception exception2)
            {
                MessageBoxHelper.Show("单点登录失败失败！" + exception2.Message);
            }
            return false;
        }

        private bool method_0(string string_2)
        {
            return (MessageManager.ShowMsgBox(string_2) == DialogResult.OK);
        }

        private void method_1(string string_2)
        {
            ListDictionary dictionary;
            this.string_1 = string_2;
            Interface0 interface2 = new Class11();
            try
            {
                dictionary = interface2.imethod_2(string_2);
            }
            catch (Exception)
            {
                MessageBoxHelper.Show("数据库异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (dictionary != null)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (entry.Key.ToString() == "DM")
                    {
                        this.string_0 = Convert.ToString(entry.Value);
                    }
                    if (entry.Key.ToString() == "ISADMIN")
                    {
                        this.bool_0 = Convert.ToBoolean(entry.Value);
                    }
                    else if (entry.Key.ToString() == "JSQX")
                    {
                        this.list_1.AddRange(entry.Value as List<string>);
                    }
                    else if (entry.Key.ToString() == "GNQX")
                    {
                        this.list_0.AddRange(entry.Value as List<string>);
                    }
                }
            }
        }

        private string method_2(int int_0, string string_2)
        {
            ilog_0.Debug(string.Concat(new object[] { "大厅清卡回置清卡日期传入参数：qkssyf:", string_2, "  fptype:", int_0 }));
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card == null)
            {
                ilog_0.Debug("GetZQQKDate taxcard为null");
                return "";
            }
            DateTime lastRepDate = new DateTime(0x758, 11, 1);
            string str2 = "";
            try
            {
                string str;
                List<string> list = new List<string>();
                switch (int_0)
                {
                    case 11:
                        list = card.method_41(11);
                        if ((list == null) || (list.Count < 1))
                        {
                            throw new Exception("大厅清卡回置dtHYLastCSDate货运异常");
                        }
                        lastRepDate = DateTime.Parse(list[0]);
                        ilog_0.Debug("大厅清卡回置dtHYLastCSDate:" + lastRepDate.AddMonths(-1).ToString());
                        goto Label_01C9;

                    case 12:
                        if (!card.QYLX.ISJDC)
                        {
                            break;
                        }
                        list = card.method_41(12);
                        goto Label_016F;

                    case 0x29:
                        list = card.method_41(0x29);
                        if ((list == null) || (list.Count < 1))
                        {
                            throw new Exception("大厅清卡回置dtJSPLastCSDate卷式票异常");
                        }
                        lastRepDate = DateTime.Parse(list[0]);
                        ilog_0.Debug("大厅清卡回置dtJSPLastCSDate:" + lastRepDate.AddMonths(-1).ToString());
                        goto Label_01C9;

                    case 0:
                        lastRepDate = card.LastRepDate;
                        goto Label_01C9;

                    default:
                        goto Label_01C9;
                }
                list = card.method_41(0x33);
            Label_016F:
                if ((list != null) && (list.Count >= 1))
                {
                    lastRepDate = DateTime.Parse(list[0]);
                    ilog_0.Debug("大厅清卡回置dtJDCLastCSDate:" + lastRepDate.AddMonths(-1).ToString());
                }
                else
                {
                    throw new Exception("大厅清卡回置dtJDCLastCSDate机动车或电子异常");
                }
            Label_01C9:
                str = lastRepDate.AddMonths(-1).ToString("yyyyMM");
                str2 = (int.Parse(str) > int.Parse(string_2)) ? string_2 : str;
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetZQQKDate异常：" + exception.ToString());
            }
            ilog_0.Debug(string.Concat(new object[] { "大厅清卡回置清卡日期:返回日期", str2, "  fptype:", int_0 }));
            return str2;
        }

        public string Bk1
        {
            get
            {
                return "";
            }
        }

        public string Bk2
        {
            get
            {
                return "";
            }
        }

        public string Bk3
        {
            get
            {
                return "";
            }
        }

        public List<string> Gnqx
        {
            get
            {
                return this.list_0;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.bool_0;
            }
        }

        public List<string> Jsqx
        {
            get
            {
                return this.list_1;
            }
        }

        public string Yhdm
        {
            get
            {
                return this.string_0;
            }
        }

        public string Yhmc
        {
            get
            {
                return this.string_1;
            }
        }
    }
}

