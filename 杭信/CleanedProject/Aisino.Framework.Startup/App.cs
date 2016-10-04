using Aisino.Framework.MainForm;
using Aisino.Framework.MainForm.UpDown;
using Aisino.Framework.Plugin.Core;
using Aisino.Framework.Plugin.Core.Crypto;
using Aisino.Framework.Plugin.Core.Http;
using Aisino.Framework.Plugin.Core.MessageDlg;
using Aisino.Framework.Plugin.Core.Plugin;
using Aisino.Framework.Plugin.Core.Util;
using Aisino.Framework.Startup.Login;
using Aisino.FTaxBase;
using ICSharpCode.SharpZipLib.Zip;
using log4net;
using log4net.Config;
using Microsoft.Win32;
using ns1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

internal static class App
{
    private static readonly ILog ilog_0 = LogManager.GetLogger(typeof(App).FullName);

    [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
    private static extern int GetAreaCode(byte[] byte_0, byte[] byte_1);
    [STAThread]
    private static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
       
        try
        {
            Mutex mutex;
            string name = "AisinoInvoice";
            try
            {
                mutex = new Mutex(false, name);
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.ToString());
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\dddl.txt", "7", Encoding.Unicode);
                smethod_7();
                return;
            }
            using (mutex)
            {
                if (!mutex.WaitOne(10, false))
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\dddl.txt", "7", Encoding.Unicode);
                    smethod_7();
                }
                else
                {
                    CoreStartup.LoadProperty();
                    string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    directoryName = directoryName.Substring(0, directoryName.LastIndexOf(@"\"));
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    if ((card.RetCode != 0) && (card.RetCode != -1))
                    {
                        ilog_0.Error("金税盘初始化错误,返回值为空");
                        MessageManager.ShowMsgBox(card.ErrCode);
                    }
                    else
                    {
                        RegistryKey key = null;
                        if (card.SoftVersion == "FWKP_V2.0_Svr_Client")
                        {
                            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwqkp.exe");
                        }
                        else
                        {
                            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe");
                        }
                        PropertyUtil.SetValue("MAIN_PATH", key.GetValue("Path").ToString());
                        PropertyUtil.SetValue("MAIN_CODE", key.GetValue("code", "").ToString());
                        PropertyUtil.SetValue("MAIN_MACHINE", key.GetValue("machine", "0").ToString());
                        PropertyUtil.SetValue("MAIN_ORGCODE", key.GetValue("orgcode", "").ToString());
                        PropertyUtil.SetValue("MAIN_VER", key.GetValue("Version", "0000").ToString());
                        PropertyUtil.SetValue("MAIN_DBVER", key.GetValue("DataBaseVersion", "0000").ToString());
                        PropertyUtil.SetValue("MAIN_UI", "CLIENT");
                        if (card.SoftVersion != "FWKP_V2.0_Svr_Client")
                        {
                            smethod_5();
                        }
                        smethod_0();
                        if (!smethod_3())
                        {
                            MessageBoxHelper.Show("数据库更新失败", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (!smethod_4())
                        {
                            MessageBoxHelper.Show("数据库更新失败", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            string str5 = Path.Combine(directoryName, @"Log\Log.log");
                            string str6 = Path.Combine(directoryName, @"Config\Common\log.cfg");
                            XmlManager.SetNodeAttribute(str6, "log4net/appender/file", "value", str5);
                            XmlConfigurator.Configure(new FileInfo(str6));
                            ilog_0.Info("系统正在启动...");
                            if (card.SoftVersion != "FWKP_V2.0_Svr_Client")
                            {
                                Process[] processesByName = Process.GetProcessesByName("usautoreg");
                                if ((processesByName == null) || (processesByName.Length <= 0))
                                {
                                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"AisinoCSP\usautoreg.exe");
                                    if (File.Exists(path))
                                    {
                                        Process[] processArray2 = Process.GetProcessesByName("usautoreg");
                                        if ((processArray2 != null) && (processArray2.Length > 0))
                                        {
                                            foreach (Process process in processArray2)
                                            {
                                                process.Kill();
                                            }
                                        }
                                        File.Delete(path);
                                    }
                                    string str8 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"AisinoCSP\wtautoreg.exe");
                                    if (File.Exists(str8))
                                    {
                                        Process process2 = new Process();
                                        ProcessStartInfo info = new ProcessStartInfo(str8);
                                        process2.StartInfo = info;
                                        process2.StartInfo.UseShellExecute = false;
                                        process2.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                        process2.Start();
                                    }
                                }
                            }
                            FontSetupUtil.SetUpFonts();
                            bool flag = false;
                            string str9 = string.Empty;
                            ILogin login = new Class12();
                            if (args != null)
                            {
                                int length = args.Length;
                                if (length.Equals(4) && args[0].Equals("单点登录"))
                                {
                                    str9 = args[1];
                                    string hashStr = MD5_Crypt.GetHashStr(args[2]);
                                    string str11 = args[3];
                                    string contents = "0";
                                    Interface0 interface2 = new Class11();
                                    try
                                    {
                                        flag = interface2.imethod_1(str9, hashStr);
                                    }
                                    catch (Exception exception2)
                                    {
                                        MessageBoxHelper.Show("数据库连接异常", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        ilog_0.Error(exception2.ToString());
                                        flag = false;
                                    }
                                    if (!flag)
                                    {
                                        contents = "5";
                                    }
                                    else if (!login.Login(str9, str11))
                                    {
                                        contents = "6";
                                    }
                                    try
                                    {
                                        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\dddl.txt", contents, Encoding.Unicode);
                                        if (!"0".Equals(contents))
                                        {
                                            return;
                                        }
                                    }
                                    catch
                                    {
                                        return;
                                    }
                                }
                            }
                            if ((login != null) && login.Login(flag, str9))
                            {
                                if (card.SoftVersion.Equals("FWKP_V2.0_Svr_Client") && card.SubSoftVersion.Equals("Linux"))
                                {
                                    int num4;
                                    string message = card.String_0;
                                    int num3 = message.LastIndexOf('/');
                                    if (num3 > 0)
                                    {
                                        message = message.Substring(0, num3);
                                    }
                                    string[] strArray = message.Split(new char[] { ':' });
                                    if (strArray.Length > 2)
                                    {
                                        message = strArray[0] + ":" + strArray[1];
                                    }
                                    string str15 = "8080";
                                    string str16 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "consolePort.txt");
                                    if (File.Exists(str16))
                                    {
                                        str15 = File.ReadAllText(str16);
                                    }
                                    else
                                    {
                                        ilog_0.WarnFormat("配置文件不存在（{0}），使用默认端口8080", str16);
                                    }
                                    string str17 = message;
                                    message = string.Concat(new object[] { str17, ":", str15, "/", card.TaxCode, "_", card.Machine, "/clientUpdateServiceServlet" });
                                    ilog_0.Debug(message);
                                    WebClient client = new WebClient();
                                    byte[] buffer = client.Post_Byte(message, ToolUtil.GetBytes("kpjh=" + card.Reserve), out num4);
                                    if (num4 != 0)
                                    {
                                        ilog_0.WarnFormat("获取版本信息失败：{0},{1}", num4, ToolUtil.GetString(buffer));
                                    }
                                    else
                                    {
                                        string str18 = "";
                                        string str19 = "";
                                        string str20 = "";
                                        using (XmlTextReader reader = new XmlTextReader(new MemoryStream(buffer)))
                                        {
                                            while (reader.Read())
                                            {
                                                if (reader.IsStartElement())
                                                {
                                                    if (string.Equals("version", reader.LocalName))
                                                    {
                                                        str18 = reader.ReadString();
                                                    }
                                                    if (string.Equals("url", reader.LocalName))
                                                    {
                                                        str19 = reader.ReadString();
                                                    }
                                                    if (string.Equals("wasteInfo", reader.LocalName))
                                                    {
                                                        str20 = reader.ReadString();
                                                    }
                                                }
                                            }
                                        }
                                        if (str18.CompareTo(PropertyUtil.GetValue("MAIN_VER", "")) > 0)
                                        {
                                            str19 = string.Concat(new object[] { str17, ":", str15, "/", card.TaxCode, "_", card.Machine, str19 });
                                            byte[] buffer2 = new byte[1];
                                            buffer = client.Post_Byte(str19, buffer2, out num4);
                                            if (num4 != 0)
                                            {
                                                ilog_0.WarnFormat("下载版本失败：{0},{1}", num4, ToolUtil.GetString(buffer));
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    string str21 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "kps_client_update.zip");
                                                    string str22 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "kps_client_update_files");
                                                    File.WriteAllBytes(str21, buffer);
                                                    ZipUtil.UnZip(str21, str22, null);
                                                    str16 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "kps_client_update.bat");
                                                    StreamWriter writer = new StreamWriter(str16, false, Encoding.Default);
                                                    writer.WriteLine("echo off");
                                                    writer.WriteLine("ping /n 2 127.1>nul");
                                                    writer.WriteLine(string.Format("xcopy /e /y /q \"{0}\" \"{1}\" >> clientUpdate.log", str22, PropertyUtil.GetValue("MAIN_PATH")));
                                                    writer.WriteLine(string.Format("if \"%errorlevel%\"==\"0\" (reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\fwqkp.exe\" /v \"Version\" /t reg_sz /d \"{0}\" /f)", str18));
                                                    writer.WriteLine("if \"%errorlevel%\"==\"0\" (echo 升级成功 >> clientUpdate.log)");
                                                    writer.WriteLine(string.Format("del \"{0}\" /q", str21));
                                                    writer.WriteLine(string.Format("rmdir /s \"{0}\" /q", str22));
                                                    writer.WriteLine(string.Format("start \"\" \"{0}\\Bin\\Aisino.Framework.Startup.exe\"", PropertyUtil.GetValue("MAIN_PATH")));
                                                    writer.WriteLine(string.Format("del \"{0}\" /q", str16));
                                                    writer.Close();
                                                    MessageBoxHelper.Show("成功下载最新版本【" + str18 + "】升级包，确认后完成更新并自动重新启动！", "升级提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                                                    ProcessStartInfo startInfo = new ProcessStartInfo(str16) {
                                                        WindowStyle = ProcessWindowStyle.Hidden
                                                    };
                                                    Process.Start(startInfo);
                                                    Environment.Exit(0);
                                                }
                                                catch (Exception exception3)
                                                {
                                                    ilog_0.ErrorFormat("保存升级包异常：{0}", exception3.ToString());
                                                }
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(str20) && !string.Equals(str20, PropertyUtil.GetValue("SVR_WASTE_INFO")))
                                        {
                                            str20 = str20.Replace(PropertyUtil.GetValue("SVR_WASTE_INFO"), "");
                                            PropertyUtil.SetValue("SVR_WASTE_INFO", str20);
                                            MessageBoxHelper.Show("检查到服务器端的已作废发票，需执行发票修复操作后启动自动报送，否则可能会影响开票功能！" + Environment.NewLine + str20.Replace(".", Environment.NewLine));
                                        }
                                    }
                                }
                                PropertyUtil.SetValue("Login_UserID", login.Yhdm);
                                PropertyUtil.SetValue("Login_UserName", login.Yhmc);
                                PropertyUtil.SetValue("Login_IsAdmin", login.IsAdmin.ToString());
                                UserInfo.setValue(login);
                                ToolUtil.GetLoginUserInfoEvent += new ToolUtil.GetLoginUserInfoDelegate(App.smethod_6);
                                FormSplashHelper.MsgWait("登录成功，开始加载资源...");
                                CoreStartup.LoadPlugins(Assembly.GetExecutingAssembly());
                                ilog_0.Info("启动程序主界面...");
                                List<double> taxRateNoTax = card.TaxRateAuthorize.TaxRateNoTax;
                                string str23 = string.Empty;
                                if (taxRateNoTax != null)
                                {
                                    for (int i = 0; i < taxRateNoTax.Count; i++)
                                    {
                                        if (string.Empty.Equals(str23))
                                        {
                                            double num6 = taxRateNoTax[i];
                                            str23 = num6.ToString();
                                        }
                                        else
                                        {
                                            str23 = str23 + "," + taxRateNoTax[i].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    str23 = "0.17,0.13,0.06,0.05.0.04";
                                }
                                PropertyUtil.SetValue("MAIN_TAXTRATE", str23);
                                string str24 = PropertyUtil.GetValue("MAIN_FORMSTYLE", "OLD");
                                FormMain mainForm = null;
                                if (str24.ToUpper() == "OLD")
                                {
                                    mainForm = new MDIMainForm();
                                    PropertyUtil.SetValue("MAIN_FORMSTYLE", "OLD");
                                }
                                else
                                {
                                    mainForm = new NormalMainForm();
                                    PropertyUtil.SetValue("MAIN_FORMSTYLE", "NEW");
                                }
                                if (!card.QYLX.ISTDQY)
                                {
                                    string str25 = "正在同步企业参数和商品编码信息，不要插拔金税盘或关闭电源，请耐心等待...";
                                    if (!(PropertyUtil.GetValue("MAIN_BMFLAG", "") == "FLBM"))
                                    {
                                        str25 = "正在同步企业参数，不要插拔金税盘或关闭电源，请耐心等待...";
                                    }
                                    MessageHelper.MsgWait(str25);
                                    new fpUpDownInit().init();
                                    Thread.Sleep(100);
                                    MessageHelper.MsgWait();
                                }
                                Application.Run(mainForm);
                            }
                        }
                    }
                }
            }
        }
        catch (ReStartupException exception4)
        {
            MessageHelper.MsgWait();
            MessageManager.ShowMsgBox("FRM-000010", new string[] { exception4.Message });
        }
        catch (Exception exception5)
        {
            MessageHelper.MsgWait();
            ilog_0.Error("程序运行异常：" + exception5.ToString());
            MessageManager.ShowMsgBox("FRM-000013", new string[] { exception5.ToString() });
        }
        finally
        {
            ilog_0.Info("系统退出");
            PropertyUtil.Save();
        }
    }

    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr intptr_0, int int_0);
    private static void smethod_0()
    {
        string str = PropertyUtil.GetValue("恢复数据库路径");
        try
        {
            if ((!string.IsNullOrEmpty(str) && File.Exists(str)) && (str.EndsWith("FWSK.zip") && !smethod_2(str)))
            {
                ilog_0.Error("恢复数据库失败！");
            }
        }
        catch (Exception exception)
        {
            ilog_0.Error(exception.Message);
        }
        finally
        {
            PropertyUtil.SetValue("恢复数据库路径", "");
        }
    }

    private static string smethod_1(string string_0)
    {
        string str2;
        string path = Path.GetTempPath() + Path.GetFileName(string_0);
        try
        {
            FileStream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = new byte[] { 
                0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             };
            byte[] buffer3 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
            DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time);
            double totalSeconds = span.TotalSeconds;
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.GetEncoding("GBK").GetBytes(totalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            byte[] buffer5 = AES_Crypt.Decrypt(buffer, buffer2, buffer3, buffer4);
            FileStream stream2 = new FileStream(path, FileMode.Create, FileAccess.Write) {
                Position = 0L
            };
            stream2.Write(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return path;
        }
        catch
        {
            str2 = "";
        }
        return str2;
    }

    private static bool smethod_2(string string_0)
    {
        string tempPath = Path.GetTempPath();
        string str2 = string.Empty;
        string startupPath = string.Empty;
        try
        {
            string_0 = smethod_1(string_0);
            new FastZip().ExtractZip(string_0, tempPath, ".dll");
            if (File.Exists(string_0))
            {
                File.Delete(string_0);
            }
            str2 = tempPath + Path.GetFileName(string_0).Replace("FWSK.zip", "cc3268.dll");
            startupPath = Application.StartupPath;
            int length = startupPath.LastIndexOf('\\');
            startupPath = startupPath.Substring(0, length) + @"\Bin" + @"\cc3268.dll";
            if (!string.IsNullOrEmpty(str2) && File.Exists(str2))
            {
                if (!string.IsNullOrEmpty(startupPath) && File.Exists(startupPath))
                {
                    File.Delete(startupPath);
                }
                File.Copy(str2, startupPath, true);
                File.Delete(str2);
                return true;
            }
        }
        catch (Exception exception)
        {
            ilog_0.Error(exception.Message);
            if (File.Exists(string_0))
            {
                File.Delete(string_0);
            }
            if (File.Exists(str2))
            {
                File.Delete(str2);
            }
            return false;
        }
        return false;
    }

    private static bool smethod_3()
    {
        bool flag = false;
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        Dictionary<string, string> item = new Dictionary<string, string>();
        item.Add("CHECK", "select count(name) from sqlite_master where name='BM_XHDW' and type='table'");
        item.Add("UPDATE", "CREATE TABLE 'BM_XHDW'([BM] varchar(16) NOT NULL,[MC] varchar(120) NOT NULL,[JM] varchar(6),[SJBM] varchar(16),[SH] varchar(25),[DZDH] varchar(100),[YHZH] varchar(100),[YZBM] varchar(25),[WJ] int NOT NULL,[JWBZ] bit,[SFZJY] bit, Primary Key(BM))");
        list.Add(item);
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        dictionary2.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary2.Add("UPDATE", "Create  TABLE MAIN.[Temp_763287791]([FPZL] char(1) NOT NULL,[FPDM] varchar(10) NOT NULL,[FPHM] int NOT NULL,[FPMXXH] float NOT NULL,[XSDJBH] varchar(20),[FPHXZ] int NOT NULL,[JE] decimal NOT NULL,[SLV] float,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[SL] VARCHAR(21),[DJ] VARCHAR(21),[HSJBZ] bit NOT NULL,[SPBH] varchar(16),[DJMXXH] int, Primary Key(FPZL,FPDM,FPHM,FPMXXH), FOREIGN KEY ([FPZL],[FPDM],[FPHM])  REFERENCES [XXFP] ([FPZL],[FPDM],[FPHM]) On Delete CASCADE On Update CASCADE );Insert Into MAIN.[Temp_763287791] ([FPZL],[FPDM],[FPHM],[FPMXXH],[XSDJBH],[FPHXZ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[SL],[DJ],[HSJBZ],[SPBH],[DJMXXH]) Select [FPZL],[FPDM],[FPHM],[FPMXXH],[XSDJBH],[FPHXZ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[SL],[DJ],[HSJBZ],[SPBH],[DJMXXH] From MAIN.[XXFP_MX];Drop Table MAIN.[XXFP_MX];Alter Table MAIN.[Temp_763287791] Rename To [XXFP_MX];CREATE INDEX [IDX_XXFPMX_FPMXXH] ON [XXFP_MX] ([FPMXXH] ASC);CREATE INDEX [IDX_XXFPMX_XSDJBH] ON [XXFP_MX] ([XSDJBH] ASC);");
        list.Add(dictionary2);
        Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
        dictionary3.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary3.Add("UPDATE", "Create  TABLE MAIN.[Temp_721535929]([FPZL] char(1) NOT NULL,[FPDM] varchar(10) NOT NULL,[FPHM] int NOT NULL,[FPMXXH] float NOT NULL,[XSDJBH] varchar(20),[FPHXZ] int NOT NULL,[JE] decimal NOT NULL,[SLV] float,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[SL] VARCHAR(21),[DJ] VARCHAR(21),[HSJBZ] bit NOT NULL,[SPBH] varchar(16),[DJMXXH] int,[MXXH] INT,[MXYH] INT, Primary Key(FPZL,FPDM,FPHM,FPMXXH), FOREIGN KEY ([FPZL],[FPDM],[FPHM])  REFERENCES [XXFP] ([FPZL],[FPDM],[FPHM]) On Delete CASCADE On Update CASCADE );Insert Into MAIN.[Temp_721535929] ([FPZL],[FPDM],[FPHM],[FPMXXH],[XSDJBH],[FPHXZ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[SL],[DJ],[HSJBZ],[SPBH],[DJMXXH],[MXXH],[MXYH]) Select [FPZL],[FPDM],[FPHM],[FPMXXH],[XSDJBH],[FPHXZ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[SL],[DJ],[HSJBZ],[SPBH],[DJMXXH],[MXXH],[MXYH] From MAIN.[XXFP_XHQD];Drop Table MAIN.[XXFP_XHQD];Alter Table MAIN.[Temp_721535929] Rename To [XXFP_XHQD];");
        list.Add(dictionary3);
        Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
        dictionary4.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary4.Add("UPDATE", "Create  TABLE MAIN.[Temp_866339921]([XSDJBH] varchar(20) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6) NOT NULL,[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] VARCHAR(10),[FPHM] int,[SCFPXH] int, Primary Key(XSDJBH,XH));Insert Into MAIN.[Temp_866339921] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH]) Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH] From MAIN.[XSDJ_MX];Drop Table MAIN.[XSDJ_MX];Alter Table MAIN.[Temp_866339921] Rename To [XSDJ_MX];");
        list.Add(dictionary4);
        Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
        dictionary5.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary5.Add("UPDATE", "Create  TABLE MAIN.[Temp_293215244]([XSDJBH] varchar(20) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] VARCHAR(10),[FPHM] int,[SCFPXH] int, Primary Key(XSDJBH,XH));Insert Into MAIN.[Temp_293215244] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH])  Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH] From MAIN.[XSDJ_MXYL];Drop Table MAIN.[XSDJ_MXYL];Alter Table MAIN.[Temp_293215244] Rename To [XSDJ_MXYL];");
        list.Add(dictionary5);
        Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
        dictionary6.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary6.Add("UPDATE", "Create  TABLE MAIN.[Temp_7690825]([XSDJBH] varchar(20) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] varchar(10),[FPHM] int,[SCFPXH] int, Primary Key(XSDJBH,XH));Insert Into MAIN.[Temp_7690825] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH]) Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH] From MAIN.[XSDJ_MX_HY];Drop Table MAIN.[XSDJ_MX_HY];Alter Table MAIN.[Temp_7690825] Rename To [XSDJ_MX_HY];");
        list.Add(dictionary6);
        Dictionary<string, string> dictionary7 = new Dictionary<string, string>();
        dictionary7.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary7.Add("UPDATE", "Create  TABLE MAIN.[Temp_502461671]([SQDH] varchar(30) NOT NULL,[FPZL] Char(1),[FPDM] varchar(15),[FPHM] varchar(8),[KPJH] int NOT NULL,[GFMC] varchar(100) NOT NULL,[GFSH] varchar(25) NOT NULL,[GFBH] varchar(16),[XFMC] varchar(100) NOT NULL,[XFSH] varchar(25) NOT NULL,[SPSM] varchar(6),[TKRQ] datetime NOT NULL,[SSYF] int NOT NULL,[HJJE] decimal NOT NULL,[SL] float NOT NULL,[HJSE] decimal NOT NULL,[JBR] varchar(10),[DYBZ] bit,[ZFBZ] bit,[SQXZ] varchar(25),[SQLY] varchar(25),[DCBZ] bit,[SQRDH] varchar(25),[YYSBZ] varchar(10),[BBBZ] VARCHAR(10),[REQNSRSBH] VARCHAR(25),[JSPH] VARCHAR(20),[XXBBH] VARCHAR(20),[XXBZT] VARCHAR(7),[XXBMS] VARCHAR(100),[FRDB] VARCHAR(10),[FHRMC] VARCHAR(100),[FHRSH] VARCHAR(25),[SHRMC] VARCHAR(100),[SHRSH] VARCHAR(25),[CZCH] VARCHAR(40),[CCDW] VARCHAR(20),[JQBH] VARCHAR(20),[YSHWXX] varchar(200),[YQBZ] VARCHAR(5),[ZGSWJGMC] VARCHAR(50),[ZGSWJGDM] VARCHAR(15), Primary Key(SQDH));Insert Into MAIN.[Temp_502461671] ([SQDH],[FPZL],[FPDM],[FPHM],[KPJH],[GFMC],[GFSH],[GFBH],[XFMC],[XFSH],[SPSM],[TKRQ],[SSYF],[HJJE],[SL],[HJSE],[JBR],[DYBZ],[ZFBZ],[SQXZ],[SQLY],[DCBZ],[SQRDH],[YYSBZ],[BBBZ],[REQNSRSBH],[JSPH],[XXBBH],[XXBZT],[XXBMS],[FRDB],[FHRMC],[FHRSH],[SHRMC],[SHRSH],[CZCH],[CCDW],[JQBH],[YSHWXX],[YQBZ],[ZGSWJGMC],[ZGSWJGDM]) Select [SQDH],[FPZL],[FPDM],[FPHM],[KPJH],[GFMC],[GFSH],[GFBH],[XFMC],[XFSH],[SPSM],[TKRQ],[SSYF],[HJJE],[SL],[HJSE],[JBR],[DYBZ],[ZFBZ],[SQXZ],[SQLY],[DCBZ],[SQRDH],[YYSBZ],[BBBZ],[REQNSRSBH],[JSPH],[XXBBH],[XXBZT],[XXBMS],[FRDB],[FHRMC],[FHRSH],[SHRMC],[SHRSH],[CZCH],[CCDW],[JQBH],[YSHWXX],[YQBZ],[ZGSWJGMC],[ZGSWJGDM] From MAIN.[HZFPHY_SQD];Drop Table MAIN.[HZFPHY_SQD];Alter Table MAIN.[Temp_502461671] Rename To [HZFPHY_SQD];");
        list.Add(dictionary7);
        Dictionary<string, string> dictionary8 = new Dictionary<string, string>();
        dictionary8.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%PZWLYWH%'");
        dictionary8.Add("UPDATE", "ALTER TABLE XXFP ADD COLUMN [PZWLYWH] varchar(100)");
        list.Add(dictionary8);
        Dictionary<string, string> dictionary9 = new Dictionary<string, string>();
        dictionary9.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%BSRZ%'");
        dictionary9.Add("UPDATE", "ALTER TABLE XXFP ADD COLUMN [BSRZ] VARCHAR(200)");
        list.Add(dictionary9);
        Dictionary<string, string> dictionary10 = new Dictionary<string, string>();
        dictionary10.Add("CHECK", "select count(name) from sqlite_master where name='FPSLH' and type='table' and sql like '%zfbz%'");
        dictionary10.Add("UPDATE", "ALTER TABLE FPSLH ADD COLUMN [zfbz] varchar(2)");
        list.Add(dictionary10);
        Dictionary<string, string> dictionary11 = new Dictionary<string, string>();
        dictionary11.Add("CHECK", "select count(name) from sqlite_master where name='BM_SFHR' and type='table' and sql like '%KJM%'");
        dictionary11.Add("UPDATE", "ALTER TABLE BM_SFHR ADD COLUMN [KJM] varchar(16)");
        list.Add(dictionary11);
        Dictionary<string, string> dictionary12 = new Dictionary<string, string>();
        dictionary12.Add("CHECK", "select count(name) from sqlite_master where name='BM_FYXM' and type='table' and sql like '%KJM%'");
        dictionary12.Add("UPDATE", "ALTER TABLE BM_FYXM ADD COLUMN [KJM] varchar(16)");
        list.Add(dictionary12);
        Dictionary<string, string> dictionary13 = new Dictionary<string, string>();
        dictionary13.Add("CHECK", "select count(name) from sqlite_master where name='BM_GHDW' and type='table' and sql like '%KJM%'");
        dictionary13.Add("UPDATE", "ALTER TABLE BM_GHDW ADD COLUMN [KJM] varchar(16)");
        list.Add(dictionary13);
        Dictionary<string, string> dictionary14 = new Dictionary<string, string>();
        dictionary14.Add("CHECK", "select count(name) from sqlite_master where name='BM_CL' and type='table' and sql like '%KJM%'");
        dictionary14.Add("UPDATE", "ALTER TABLE BM_CL ADD COLUMN [KJM] varchar(16)");
        list.Add(dictionary14);
        Dictionary<string, string> dictionary15 = new Dictionary<string, string>();
        dictionary15.Add("CHECK", "select count(name) from sqlite_master where name='BM_XHDW' and type='table' and sql like '%KJM%'");
        dictionary15.Add("UPDATE", "ALTER TABLE BM_XHDW ADD COLUMN [KJM] varchar(16)");
        list.Add(dictionary15);
        Dictionary<string, string> dictionary16 = new Dictionary<string, string>();
        dictionary16.Add("CHECK", "");
        dictionary16.Add("UPDATE", "UPDATE XTBBXX SET MC = '机动车申报接口'\tWHERE [BBZL] = 'F' and [DM] = 'Z'");
        list.Add(dictionary16);
        Dictionary<string, string> dictionary17 = new Dictionary<string, string>();
        dictionary17.Add("CHECK", "");
        dictionary17.Add("UPDATE", "UPDATE XTBBXX SET MC = '机动车辅助开票'\tWHERE [BBZL] = 'F' and [DM] = 'M'");
        list.Add(dictionary17);
        Dictionary<string, string> dictionary18 = new Dictionary<string, string>();
        dictionary18.Add("CHECK", "");
        dictionary18.Add("UPDATE", "REPLACE Into XTBBXX ([BBZL],[DM],[MC],[BBBS],[SM]) values('F','B','车购税数据加密','QC','车购税数据加密接口')");
        list.Add(dictionary18);
        Dictionary<string, string> dictionary19 = new Dictionary<string, string>();
        dictionary19.Add("CHECK", "select count(*) from GG_LSXX");
        dictionary19.Add("UPDATE", "Drop Table [GG_LSXX];Create  TABLE [GG_LSXX]([xh] VARCHAR(50) NOT NULL,[bt] TEXT NOT NULL ON CONFLICT Fail,[nr] TEXT NOT NULL ON CONFLICT Fail,[jssj] DATETIME NOT NULL ON CONFLICT Fail,[lx] VARCHAR(50) NOT NULL ON CONFLICT Fail, Primary Key(xh))");
        list.Add(dictionary19);
        Dictionary<string, string> dictionary20 = new Dictionary<string, string>();
        dictionary20.Add("CHECK", "select count(name) from sqlite_master where name='BM_Property' and type='table'");
        dictionary20.Add("UPDATE", "Create  TABLE [BM_Property]([BM] NVARCHAR2(20) NOT NULL ON CONFLICT Replace,[iContact] NVARCHAR(1),[uomName] NVARCHAR(10),[cSupGUID] NVARCHAR(20),[cCustGUID] NVARCHAR(20),[cMateGUID] NVARCHAR(20),[cDeptGUID] NVARCHAR(20),[cEmpGUID] NVARCHAR(20), Primary Key(BM) ON CONFLICT Replace)");
        list.Add(dictionary20);
        Dictionary<string, string> dictionary21 = new Dictionary<string, string>();
        dictionary21.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%DZSYH%'");
        dictionary21.Add("UPDATE", "ALTER TABLE XXFP ADD COLUMN [DZSYH] VARCHAR(20)");
        list.Add(dictionary21);
        Dictionary<string, string> dictionary22 = new Dictionary<string, string>();
        dictionary22.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%KPSXH%'");
        dictionary22.Add("UPDATE", "ALTER TABLE XXFP ADD COLUMN [KPSXH] VARCHAR(20)");
        list.Add(dictionary22);
        Dictionary<string, string> dictionary23 = new Dictionary<string, string>();
        dictionary23.Add("CHECK", "");
        dictionary23.Add("UPDATE", "update ERRMSG2 set solution='业务受理转发连接异常，在业务受理转发连接到业务处理服务器时，发生了其他异常，具体异常信息参看详细日志' where id='-960016055'");
        list.Add(dictionary23);
        Dictionary<string, string> dictionary24 = new Dictionary<string, string>();
        dictionary24.Add("CHECK", "");
        dictionary24.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.XtszSet.PrintSet',datetime('now','localtime'),'admin')");
        list.Add(dictionary24);
        Dictionary<string, string> dictionary25 = new Dictionary<string, string>();
        dictionary25.Add("CHECK", "");
        dictionary25.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.ZYFPPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary25);
        Dictionary<string, string> dictionary26 = new Dictionary<string, string>();
        dictionary26.Add("CHECK", "");
        dictionary26.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.PTFPPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary26);
        Dictionary<string, string> dictionary27 = new Dictionary<string, string>();
        dictionary27.Add("CHECK", "");
        dictionary27.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.HYFPPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary27);
        Dictionary<string, string> dictionary28 = new Dictionary<string, string>();
        dictionary28.Add("CHECK", "");
        dictionary28.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.JDCOldPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary28);
        Dictionary<string, string> dictionary29 = new Dictionary<string, string>();
        dictionary29.Add("CHECK", "");
        dictionary29.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.QDPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary29);
        Dictionary<string, string> dictionary30 = new Dictionary<string, string>();
        dictionary30.Add("CHECK", "");
        dictionary30.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.ZYXXBPrintSetEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary30);
        Dictionary<string, string> dictionary31 = new Dictionary<string, string>();
        dictionary31.Add("CHECK", "");
        dictionary31.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Xtsz.HYXXBPrintEntry',datetime('now','localtime'),'admin')");
        list.Add(dictionary31);
        Dictionary<string, string> dictionary32 = new Dictionary<string, string>();
        dictionary32.Add("CHECK", "");
        dictionary32.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wlgp',datetime('now','localtime'),'admin')");
        list.Add(dictionary32);
        Dictionary<string, string> dictionary33 = new Dictionary<string, string>();
        dictionary33.Add("CHECK", "");
        dictionary33.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Bsgl.Cbsgl.QDFpsjdc',datetime('now','localtime'),'admin')");
        list.Add(dictionary33);
        Dictionary<string, string> dictionary34 = new Dictionary<string, string>();
        dictionary34.Add("CHECK", "");
        dictionary34.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Bsgl.Cbsgl.JDCFpsjdc',datetime('now','localtime'),'admin')");
        list.Add(dictionary34);
        Dictionary<string, string> dictionary35 = new Dictionary<string, string>();
        dictionary35.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='ZXGP_GPXX' and type='table'");
        dictionary35.Add("UPDATE", "drop table ZXGP_GPXX");
        list.Add(dictionary35);
        Dictionary<string, string> dictionary36 = new Dictionary<string, string>();
        dictionary36.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='ZXGP_HZSQD' and type='table'");
        dictionary36.Add("UPDATE", "drop table ZXGP_HZSQD");
        list.Add(dictionary36);
        Dictionary<string, string> dictionary37 = new Dictionary<string, string>();
        dictionary37.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='ZXGP_PYXX' and type='table'");
        dictionary37.Add("UPDATE", "drop table ZXGP_PYXX");
        list.Add(dictionary37);
        Dictionary<string, string> dictionary38 = new Dictionary<string, string>();
        dictionary38.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='ZXGP_WLTP' and type='table'");
        dictionary38.Add("UPDATE", "drop table ZXGP_WLTP");
        list.Add(dictionary38);
        Dictionary<string, string> dictionary39 = new Dictionary<string, string>();
        dictionary39.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='YCBS_LSXX' and type='table'");
        dictionary39.Add("UPDATE", "drop table YCBS_LSXX");
        list.Add(dictionary39);
        Dictionary<string, string> dictionary40 = new Dictionary<string, string>();
        dictionary40.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='BBSY' and type='table'");
        dictionary40.Add("UPDATE", "drop table BBSY");
        list.Add(dictionary40);
        Dictionary<string, string> dictionary41 = new Dictionary<string, string>();
        dictionary41.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='BMBM' and type='table'");
        dictionary41.Add("UPDATE", "drop table BMBM");
        list.Add(dictionary41);
        Dictionary<string, string> dictionary42 = new Dictionary<string, string>();
        dictionary42.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='BM_FPLB' and type='table'");
        dictionary42.Add("UPDATE", "drop table BM_FPLB");
        list.Add(dictionary42);
        Dictionary<string, string> dictionary43 = new Dictionary<string, string>();
        dictionary43.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='CZY' and type='table'");
        dictionary43.Add("UPDATE", "drop table CZY");
        list.Add(dictionary43);
        Dictionary<string, string> dictionary44 = new Dictionary<string, string>();
        dictionary44.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPJ' and type='table'");
        dictionary44.Add("UPDATE", "drop table FPJ");
        list.Add(dictionary44);
        Dictionary<string, string> dictionary45 = new Dictionary<string, string>();
        dictionary45.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPJMX' and type='table'");
        dictionary45.Add("UPDATE", "drop table FPJMX");
        list.Add(dictionary45);
        Dictionary<string, string> dictionary46 = new Dictionary<string, string>();
        dictionary46.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPLBBM' and type='table'");
        dictionary46.Add("UPDATE", "drop table FPLBBM");
        list.Add(dictionary46);
        Dictionary<string, string> dictionary47 = new Dictionary<string, string>();
        dictionary47.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPLYCYBB' and type='table'");
        dictionary47.Add("UPDATE", "drop table FPLYCYBB");
        list.Add(dictionary47);
        Dictionary<string, string> dictionary48 = new Dictionary<string, string>();
        dictionary48.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPLYCYBBFB' and type='table'");
        dictionary48.Add("UPDATE", "drop table FPLYCYBBFB");
        list.Add(dictionary48);
        Dictionary<string, string> dictionary49 = new Dictionary<string, string>();
        dictionary49.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPZLBM' and type='table'");
        dictionary49.Add("UPDATE", "drop table FPZLBM");
        list.Add(dictionary49);
        Dictionary<string, string> dictionary50 = new Dictionary<string, string>();
        dictionary50.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FPZLDM' and type='table'");
        dictionary50.Add("UPDATE", "drop table FPZLDM");
        list.Add(dictionary50);
        Dictionary<string, string> dictionary51 = new Dictionary<string, string>();
        dictionary51.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='FYXMBM' and type='table'");
        dictionary51.Add("UPDATE", "drop table FYXMBM");
        list.Add(dictionary51);
        Dictionary<string, string> dictionary52 = new Dictionary<string, string>();
        dictionary52.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='JXFP' and type='table'");
        dictionary52.Add("UPDATE", "drop table JXFP");
        list.Add(dictionary52);
        Dictionary<string, string> dictionary53 = new Dictionary<string, string>();
        dictionary53.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='KPJXX' and type='table'");
        dictionary53.Add("UPDATE", "drop table KPJXX");
        list.Add(dictionary53);
        Dictionary<string, string> dictionary54 = new Dictionary<string, string>();
        dictionary54.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='XTZTXX' and type='table'");
        dictionary54.Add("UPDATE", "drop table XTZTXX");
        list.Add(dictionary54);
        Dictionary<string, string> dictionary55 = new Dictionary<string, string>();
        dictionary55.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='XTZTXX' and type='table'");
        dictionary55.Add("UPDATE", "drop table XTZTXX");
        list.Add(dictionary55);
        Dictionary<string, string> dictionary56 = new Dictionary<string, string>();
        dictionary56.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='XT_CSXX' and type='table'");
        dictionary56.Add("UPDATE", "drop table XT_CSXX");
        list.Add(dictionary56);
        Dictionary<string, string> dictionary57 = new Dictionary<string, string>();
        dictionary57.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='XT_RZXX' and type='table'");
        dictionary57.Add("UPDATE", "drop table XT_RZXX");
        list.Add(dictionary57);
        Dictionary<string, string> dictionary58 = new Dictionary<string, string>();
        dictionary58.Add("CHECK", "select case count(name) when 1 then 0 else 1 end from sqlite_master where name='XXFP_TE' and type='table'");
        dictionary58.Add("UPDATE", "drop table XXFP_TE");
        list.Add(dictionary58);
        Dictionary<string, string> dictionary59 = new Dictionary<string, string>();
        dictionary59.Add("CHECK", "");
        dictionary59.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fpgl.Fptk.DzFptk',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary59);
        Dictionary<string, string> dictionary60 = new Dictionary<string, string>();
        dictionary60.Add("CHECK", "");
        dictionary60.Add("UPDATE", "REPLACE into XTBBXX(BBZL,DM,MC,BBBS,SM) values('F','F', '进项下载','JX','进项发票下载接口')");
        list.Add(dictionary60);
        Dictionary<string, string> dictionary61 = new Dictionary<string, string>();
        dictionary61.Add("CHECK", "select count(name) from sqlite_master where name='LYGL_PSXX' and type='table'");
        dictionary61.Add("UPDATE", "CREATE TABLE [LYGL_PSXX] ([SFMR] BOOL NOT NULL,[SJR] VARCHAR NOT NULL,[YZBM] VARCHAR,[DZ] VARCHAR NOT NULL,[GDDH] VARCHAR,[YDDH] VARCHAR NOT NULL,[BZ] VARCHAR,CONSTRAINT [sqlite_autoindex_LYGL_PSXX_1] PRIMARY KEY ([SJR], [YZBM], [DZ], [GDDH], [YDDH]));");
        list.Add(dictionary61);
        Dictionary<string, string> dictionary62 = new Dictionary<string, string>();
        dictionary62.Add("CHECK", "select count(name) from sqlite_master where name='LYGL_JPXX' and type='table'");
        dictionary62.Add("UPDATE", "CREATE TABLE [LYGL_JPXX] ([LBDM] VARCHAR NOT NULL, [JQSH] VARCHAR(8) NOT NULL,[JZZH] VARCHAR(8) NOT NULL,[LGZS] INT NOT NULL,[QSHM] VARCHAR(8) NOT NULL,[SYZS] INT NOT NULL,[KPXE] DECIMAL NOT NULL,[LGRQ] DATETIME NOT NULL,[JPGG] VARCHAR NOT NULL,CONSTRAINT [sqlite_autoindex_LYGL_JPXX_1] PRIMARY KEY ([LBDM], [JQSH], [JZZH]));");
        list.Add(dictionary62);
        Dictionary<string, string> dictionary63 = new Dictionary<string, string>();
        dictionary63.Add("CHECK", "");
        dictionary63.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary63);
        Dictionary<string, string> dictionary64 = new Dictionary<string, string>();
        dictionary64.Add("CHECK", "");
        dictionary64.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Wssl',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary64);
        Dictionary<string, string> dictionary65 = new Dictionary<string, string>();
        dictionary65.Add("CHECK", "");
        dictionary65.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wsslqq',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary65);
        Dictionary<string, string> dictionary66 = new Dictionary<string, string>();
        dictionary66.Add("CHECK", "");
        dictionary66.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wsslzt',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary66);
        Dictionary<string, string> dictionary67 = new Dictionary<string, string>();
        dictionary67.Add("CHECK", "");
        dictionary67.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Wslp',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary67);
        Dictionary<string, string> dictionary68 = new Dictionary<string, string>();
        dictionary68.Add("CHECK", "");
        dictionary68.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wlgp',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary68);
        Dictionary<string, string> dictionary69 = new Dictionary<string, string>();
        dictionary69.Add("CHECK", "");
        dictionary69.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.WlgpCx',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary69);
        Dictionary<string, string> dictionary70 = new Dictionary<string, string>();
        dictionary70.Add("CHECK", "");
        dictionary70.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fpgl.Fptk.JsFptk',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary70);
        Dictionary<string, string> dictionary71 = new Dictionary<string, string>();
        dictionary71.Add("CHECK", "");
        dictionary71.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fpgl.Fpkj.Wkfpzf.Q',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary71);
        Dictionary<string, string> dictionary72 = new Dictionary<string, string>();
        dictionary72.Add("CHECK", "");
        dictionary72.Add("UPDATE", "REPLACE into QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.SetFormat',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary72);
        Dictionary<string, string> dictionary73 = new Dictionary<string, string>();
        dictionary73.Add("CHECK", "");
        dictionary73.Add("UPDATE", "SELECT * FROM SYS_CONFIG");
        list.Add(dictionary73);
        Dictionary<string, string> dictionary74 = new Dictionary<string, string>();
        dictionary74.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%BMBBBH%'");
        dictionary74.Add("UPDATE", "ALTER TABLE XXFP ADD COLUMN BMBBBH VARCHAR(20)");
        list.Add(dictionary74);
        Dictionary<string, string> dictionary75 = new Dictionary<string, string>();
        dictionary75.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_MX' and type='table' and sql like '%XSYH%'");
        dictionary75.Add("UPDATE", "ALTER TABLE XXFP_MX ADD COLUMN [XSYH] int");
        list.Add(dictionary75);
        Dictionary<string, string> dictionary76 = new Dictionary<string, string>();
        dictionary76.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_MX' and type='table' and sql like '%FLBM%'");
        dictionary76.Add("UPDATE", "ALTER TABLE XXFP_MX ADD COLUMN [FLBM] VARCHAR(19)");
        list.Add(dictionary76);
        Dictionary<string, string> dictionary77 = new Dictionary<string, string>();
        dictionary77.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_MX' and type='table' and sql like '%YHSM%'");
        dictionary77.Add("UPDATE", "ALTER TABLE XXFP_MX ADD COLUMN [YHSM] VARCHAR(500)");
        list.Add(dictionary77);
        Dictionary<string, string> dictionary78 = new Dictionary<string, string>();
        dictionary78.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_MX' and type='table' and sql like '%LSLVBS%'");
        dictionary78.Add("UPDATE", "ALTER TABLE XXFP_MX ADD COLUMN [LSLVBS] VARCHAR(1)");
        list.Add(dictionary78);
        Dictionary<string, string> dictionary79 = new Dictionary<string, string>();
        dictionary79.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_XHQD' and type='table' and sql like '%XSYH%'");
        dictionary79.Add("UPDATE", "ALTER TABLE XXFP_XHQD ADD COLUMN [XSYH] int");
        list.Add(dictionary79);
        Dictionary<string, string> dictionary80 = new Dictionary<string, string>();
        dictionary80.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_XHQD' and type='table' and sql like '%FLBM%'");
        dictionary80.Add("UPDATE", "ALTER TABLE XXFP_XHQD ADD COLUMN [FLBM] VARCHAR(19)");
        list.Add(dictionary80);
        Dictionary<string, string> dictionary81 = new Dictionary<string, string>();
        dictionary81.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_XHQD' and type='table' and sql like '%YHSM%'");
        dictionary81.Add("UPDATE", "ALTER TABLE XXFP_XHQD ADD COLUMN [YHSM] VARCHAR(500)");
        list.Add(dictionary81);
        Dictionary<string, string> dictionary82 = new Dictionary<string, string>();
        dictionary82.Add("CHECK", "select count(name) from sqlite_master where name='XXFP_XHQD' and type='table' and sql like '%LSLVBS%'");
        dictionary82.Add("UPDATE", "ALTER TABLE XXFP_XHQD ADD COLUMN [LSLVBS] VARCHAR(1)");
        list.Add(dictionary82);
        Dictionary<string, string> dictionary83 = new Dictionary<string, string>();
        dictionary83.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%FLBM%'");
        dictionary83.Add("UPDATE", "ALTER TABLE XSDJ_MX ADD COLUMN FLBM VARCHAR(19)");
        list.Add(dictionary83);
        Dictionary<string, string> dictionary84 = new Dictionary<string, string>();
        dictionary84.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%XSYH%'");
        dictionary84.Add("UPDATE", "ALTER TABLE XSDJ_MX ADD COLUMN XSYH BIT");
        list.Add(dictionary84);
        Dictionary<string, string> dictionary85 = new Dictionary<string, string>();
        dictionary85.Add("CHECK", "select count(name) from sqlite_master where name='BM_SPFL' and type='table'");
        dictionary85.Add("UPDATE", "Create  TABLE BM_SPFL([BM] varchar(19) UNIQUE NOT NULL,[HBBM] varchar(19) NOT NULL,[MC] varchar(200) NOT NULL,[SM] varchar(4000),[SLV] varchar(200),[ZZSTSGL] varchar(500),[ZZSZCYJ] varchar(1000),[ZZSTSNRDM] varchar(100),[XFSGL] varchar(500),[XFSZCYJ] varchar(1000),[XFSTSNRDM] varchar(100),[GJZ] varchar(2000),[HZX] varchar(1),[TJJBM] varchar(500),[HGJCKSPPM] TEXT,[BMB_BBH] varchar(20) NOT NULL,[BBH] varchar(20) NOT NULL,[KYZT] varchar(1),[QYSJ] varchar(8),[GDQJZSJ] varchar(8),[SJBM] varchar(19),[GXSJ] DATETIME,[WJ] INT, Primary Key(BM))");
        list.Add(dictionary85);
        Dictionary<string, string> dictionary86 = new Dictionary<string, string>();
        dictionary86.Add("CHECK", "select count(name) from sqlite_master where name='BM_YHZC' and type='table'");
        dictionary86.Add("UPDATE", "Create  TABLE BM_YHZC([YHZCMC] varchar(500) NOT NULL,[SLV] varchar(500), Primary Key(YHZCMC))");
        list.Add(dictionary86);
        Dictionary<string, string> dictionary87 = new Dictionary<string, string>();
        dictionary87.Add("CHECK", "select count(name) from sqlite_master where name='BM_SP' and type='table' and sql like '%SPFL%'");
        dictionary87.Add("UPDATE", "alter table BM_SP add column SPFL varchar(19)");
        list.Add(dictionary87);
        Dictionary<string, string> dictionary88 = new Dictionary<string, string>();
        dictionary88.Add("CHECK", "select count(name) from sqlite_master where name='BM_SP' and type='table' and sql like '%YHZC%'");
        dictionary88.Add("UPDATE", "alter table BM_SP add column YHZC varchar(4)");
        list.Add(dictionary88);
        Dictionary<string, string> dictionary89 = new Dictionary<string, string>();
        dictionary89.Add("CHECK", "select count(name) from sqlite_master where name='BM_CL' and type='table' and sql like '%SPFL%'");
        dictionary89.Add("UPDATE", "alter table BM_CL add column SPFL varchar(19)");
        list.Add(dictionary89);
        Dictionary<string, string> dictionary90 = new Dictionary<string, string>();
        dictionary90.Add("CHECK", "select count(name) from sqlite_master where name='BM_CL' and type='table' and sql like '%YHZC%'");
        dictionary90.Add("UPDATE", "alter table BM_CL add column YHZC varchar(4)");
        list.Add(dictionary90);
        Dictionary<string, string> dictionary91 = new Dictionary<string, string>();
        dictionary91.Add("CHECK", "select count(name) from sqlite_master where name='BM_FYXM' and type='table' and sql like '%SPFL%'");
        dictionary91.Add("UPDATE", "alter table BM_FYXM add column SPFL varchar(19)");
        list.Add(dictionary91);
        Dictionary<string, string> dictionary92 = new Dictionary<string, string>();
        dictionary92.Add("CHECK", "select count(name) from sqlite_master where name='BM_FYXM' and type='table' and sql like '%YHZC%'");
        dictionary92.Add("UPDATE", "alter table BM_FYXM add column YHZC varchar(4)");
        list.Add(dictionary92);
        Dictionary<string, string> dictionary93 = new Dictionary<string, string>();
        dictionary93.Add("CHECK", "select count(name) from sqlite_master where name='LYGL_PSXX' and type='table'");
        dictionary93.Add("UPDATE", "CREATE TEMPORARY TABLE [LYGL_PSXX_TMP]([SFMR] BOOL NOT NULL,[SJR] VARCHAR NOT NULL,[YZBM] VARCHAR,[DZ] VARCHAR NOT NULL, [GDDH] VARCHAR, [YDDH] VARCHAR NOT NULL, [BZ] VARCHAR, CONSTRAINT [] PRIMARY KEY ([SJR], [YZBM], [DZ], [GDDH], [YDDH], [BZ]));INSERT INTO LYGL_PSXX_TMP SELECT * FROM LYGL_PSXX;DROP TABLE LYGL_PSXX;CREATE TABLE [LYGL_PSXX] ([SFMR] BOOL NOT NULL, [SJR] VARCHAR NOT NULL, [YZBM] VARCHAR, [DZ] VARCHAR NOT NULL, [GDDH] VARCHAR, [YDDH] VARCHAR NOT NULL, [BZ] VARCHAR, CONSTRAINT [] PRIMARY KEY ([SJR], [YZBM], [DZ], [GDDH], [YDDH], [BZ]));INSERT INTO LYGL_PSXX SELECT * FROM LYGL_PSXX_TMP;DROP TABLE LYGL_PSXX_TMP;");
        list.Add(dictionary93);
        Dictionary<string, string> dictionary94 = new Dictionary<string, string>();
        dictionary94.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD' and type='table' and sql like '%FLBMBBBH%'");
        dictionary94.Add("UPDATE", "alter table HZFP_SQD add column FLBMBBBH varchar(20)");
        list.Add(dictionary94);
        Dictionary<string, string> dictionary95 = new Dictionary<string, string>();
        dictionary95.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD' and type='table' and sql like '%FLBMBBBH%'");
        dictionary95.Add("UPDATE", "alter table HZFPHY_SQD add column FLBMBBBH varchar(20)");
        list.Add(dictionary95);
        Dictionary<string, string> dictionary96 = new Dictionary<string, string>();
        dictionary96.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD_MX' and type='table' and sql like '%FLBM%'");
        dictionary96.Add("UPDATE", "alter table HZFP_SQD_MX add column FLBM varchar(20)");
        list.Add(dictionary96);
        Dictionary<string, string> dictionary97 = new Dictionary<string, string>();
        dictionary97.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD_MX' and type='table' and sql like '%QYSPBM%'");
        dictionary97.Add("UPDATE", "alter table HZFP_SQD_MX add column QYSPBM varchar(20)");
        list.Add(dictionary97);
        Dictionary<string, string> dictionary98 = new Dictionary<string, string>();
        dictionary98.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD_MX' and type='table' and sql like '%SFXSYHZC%'");
        dictionary98.Add("UPDATE", "alter table HZFP_SQD_MX add column SFXSYHZC varchar(2)");
        list.Add(dictionary98);
        Dictionary<string, string> dictionary99 = new Dictionary<string, string>();
        dictionary99.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD_MX' and type='table' and sql like '%YHZCMC%'");
        dictionary99.Add("UPDATE", "alter table HZFP_SQD_MX add column YHZCMC varchar(50)");
        list.Add(dictionary99);
        Dictionary<string, string> dictionary100 = new Dictionary<string, string>();
        dictionary100.Add("CHECK", "select count(name) from sqlite_master where name='HZFP_SQD_MX' and type='table' and sql like '%LSLBS%'");
        dictionary100.Add("UPDATE", "alter table HZFP_SQD_MX add column LSLBS varchar(2)");
        list.Add(dictionary100);
        Dictionary<string, string> dictionary101 = new Dictionary<string, string>();
        dictionary101.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD_MX' and type='table' and sql like '%FLBM%'");
        dictionary101.Add("UPDATE", "alter table HZFPHY_SQD_MX add column FLBM varchar(20)");
        list.Add(dictionary101);
        Dictionary<string, string> dictionary102 = new Dictionary<string, string>();
        dictionary102.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD_MX' and type='table' and sql like '%QYSPBM%'");
        dictionary102.Add("UPDATE", "alter table HZFPHY_SQD_MX add column QYSPBM varchar(20)");
        list.Add(dictionary102);
        Dictionary<string, string> dictionary103 = new Dictionary<string, string>();
        dictionary103.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD_MX' and type='table' and sql like '%SFXSYHZC%'");
        dictionary103.Add("UPDATE", "alter table HZFPHY_SQD_MX add column SFXSYHZC varchar(2)");
        list.Add(dictionary103);
        Dictionary<string, string> dictionary104 = new Dictionary<string, string>();
        dictionary104.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD_MX' and type='table' and sql like '%YHZCMC%'");
        dictionary104.Add("UPDATE", "alter table HZFPHY_SQD_MX add column YHZCMC varchar(50)");
        list.Add(dictionary104);
        Dictionary<string, string> dictionary105 = new Dictionary<string, string>();
        dictionary105.Add("CHECK", "select count(name) from sqlite_master where name='HZFPHY_SQD_MX' and type='table' and sql like '%LSLBS%'");
        dictionary105.Add("UPDATE", "alter table HZFPHY_SQD_MX add column LSLBS varchar(2)");
        list.Add(dictionary105);
        Dictionary<string, string> dictionary106 = new Dictionary<string, string>();
        dictionary106.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%FLBM%'");
        dictionary106.Add("UPDATE", "alter table XSDJ_MXYL add column FLBM varchar(19)");
        list.Add(dictionary106);
        Dictionary<string, string> dictionary107 = new Dictionary<string, string>();
        dictionary107.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%XSYH%'");
        dictionary107.Add("UPDATE", "alter table XSDJ_MXYL add column XSYH bit");
        list.Add(dictionary107);
        Dictionary<string, string> dictionary108 = new Dictionary<string, string>();
        dictionary108.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%FLBM%'");
        dictionary108.Add("UPDATE", "alter table XSDJ_MX_HY add column FLBM varchar(19)");
        list.Add(dictionary108);
        Dictionary<string, string> dictionary109 = new Dictionary<string, string>();
        dictionary109.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%XSYH%'");
        dictionary109.Add("UPDATE", "alter table XSDJ_MX_HY add column XSYH bit");
        list.Add(dictionary109);
        Dictionary<string, string> dictionary110 = new Dictionary<string, string>();
        dictionary110.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%FLMC%'");
        dictionary110.Add("UPDATE", "alter table XSDJ_MX add column FLMC varchar(200)");
        list.Add(dictionary110);
        Dictionary<string, string> dictionary111 = new Dictionary<string, string>();
        dictionary111.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%XSYHSM%'");
        dictionary111.Add("UPDATE", "alter table XSDJ_MX add column XSYHSM varchar(500)");
        list.Add(dictionary111);
        Dictionary<string, string> dictionary112 = new Dictionary<string, string>();
        dictionary112.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%SPBM%'");
        dictionary112.Add("UPDATE", "alter table XSDJ_MX add column SPBM varchar(16)");
        list.Add(dictionary112);
        Dictionary<string, string> dictionary113 = new Dictionary<string, string>();
        dictionary113.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%FLMC%'");
        dictionary113.Add("UPDATE", "alter table XSDJ_MXYL add column FLMC varchar(200)");
        list.Add(dictionary113);
        Dictionary<string, string> dictionary114 = new Dictionary<string, string>();
        dictionary114.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%XSYHSM%'");
        dictionary114.Add("UPDATE", "alter table XSDJ_MXYL add column XSYHSM varchar(400)");
        list.Add(dictionary114);
        Dictionary<string, string> dictionary115 = new Dictionary<string, string>();
        dictionary115.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%SPBM%'");
        dictionary115.Add("UPDATE", "alter table XSDJ_MXYL add column SPBM varchar(16)");
        list.Add(dictionary115);
        Dictionary<string, string> dictionary116 = new Dictionary<string, string>();
        dictionary116.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%FLMC%'");
        dictionary116.Add("UPDATE", "alter table XSDJ_MX_HY add column FLMC varchar(200)");
        list.Add(dictionary116);
        Dictionary<string, string> dictionary117 = new Dictionary<string, string>();
        dictionary117.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%XSYHSM%'");
        dictionary117.Add("UPDATE", "alter table XSDJ_MX_HY add column XSYHSM varchar(400)");
        list.Add(dictionary117);
        Dictionary<string, string> dictionary118 = new Dictionary<string, string>();
        dictionary118.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%SPBM%'");
        dictionary118.Add("UPDATE", "alter table XSDJ_MX_HY add column SPBM varchar(16)");
        list.Add(dictionary118);
        Dictionary<string, string> dictionary119 = new Dictionary<string, string>();
        dictionary119.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%FLBM%'");
        dictionary119.Add("UPDATE", "alter table XSDJ add column FLBM varchar(19)");
        list.Add(dictionary119);
        Dictionary<string, string> dictionary120 = new Dictionary<string, string>();
        dictionary120.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%XSYH%'");
        dictionary120.Add("UPDATE", "alter table XSDJ add column XSYH bit");
        list.Add(dictionary120);
        Dictionary<string, string> dictionary121 = new Dictionary<string, string>();
        dictionary121.Add("CHECK", "select count(name) from sqlite_master where name='XXFP' and type='table' and sql like '%SPSM_BM%'");
        dictionary121.Add("UPDATE", "alter table XXFP add column SPSM_BM varchar(100)");
        list.Add(dictionary121);
        Dictionary<string, string> dictionary122 = new Dictionary<string, string>();
        dictionary122.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%FLMC%'");
        dictionary122.Add("UPDATE", "alter table XSDJ add column FLMC varchar(200)");
        list.Add(dictionary122);
        Dictionary<string, string> dictionary123 = new Dictionary<string, string>();
        dictionary123.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%XSYHSM%'");
        dictionary123.Add("UPDATE", "alter table XSDJ add column XSYHSM varchar(500)");
        list.Add(dictionary123);
        Dictionary<string, string> dictionary124 = new Dictionary<string, string>();
        dictionary124.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%LSLVBS%'");
        dictionary124.Add("UPDATE", "alter table XSDJ add column LSLVBS char(1)");
        list.Add(dictionary124);
        Dictionary<string, string> dictionary125 = new Dictionary<string, string>();
        dictionary125.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%CLBM%'");
        dictionary125.Add("UPDATE", "alter table XSDJ add column CLBM varchar(16)");
        list.Add(dictionary125);
        Dictionary<string, string> dictionary126 = new Dictionary<string, string>();
        dictionary126.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%LSLVBS%'");
        dictionary126.Add("UPDATE", "alter table XSDJ_MX add column LSLVBS char(1)");
        list.Add(dictionary126);
        Dictionary<string, string> dictionary127 = new Dictionary<string, string>();
        dictionary127.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%LSLVBS%'");
        dictionary127.Add("UPDATE", "alter table XSDJ_MXYL add column LSLVBS char(1)");
        list.Add(dictionary127);
        Dictionary<string, string> dictionary128 = new Dictionary<string, string>();
        dictionary128.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%LSLVBS%'");
        dictionary128.Add("UPDATE", "alter table XSDJ_MX_HY add column LSLVBS char(1)");
        list.Add(dictionary128);
        Dictionary<string, string> dictionary129 = new Dictionary<string, string>();
        dictionary129.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%[BH] varchar(50)%'");
        dictionary129.Add("UPDATE", "Create  TABLE MAIN.[Temp_824673053]([BH] varchar(50) NOT NULL,[GFMC] varchar(100),[GFSH] varchar(25),[GFDZDH] varchar(100),[GFYHZH] varchar(100),[XSBM] varchar(20),[YDXS] bit,[JEHJ] decimal NOT NULL,[DJRQ] datetime NOT NULL,[DJYF] int NOT NULL,[DJZT] char(1) NOT NULL,[KPZT] char(1) NOT NULL,[BZ] varchar(250),[FHR] varchar(16),[SKR] varchar(16),[QDHSPMC] varchar(100),[XFYHZH] varchar(100),[XFDZDH] varchar(100),[CFHB] bit,[DJZL] char(1),[SFZJY] bit,[HYSY] bit,[CM] varchar(100),[DLGRQ] datetime,[KHYHMC] varchar(100),[KHYHZH] varchar(100),[TYDH] varchar(50),[QYD] varchar(100),[ZHD] varchar(100),[XHD] varchar(100),[MDD] varchar(100),[XFDZ] varchar(80),[XFDH] varchar(40),[YSHWXX] varchar(200),[SCCJMC] varchar(80),[SLV] float,[DW] varchar(15),[FPZL] CHAR(1),[FPDM] VARCHAR(20),[FPHM] int,[FLBM] varchar(19),[XSYH] bit,[FLMC] varchar(200),[XSYHSM] varchar(500),[LSLVBS] char(1),[CLBM] varchar(16), Primary Key(BH)   );Insert Into MAIN.[Temp_824673053] ([BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW],[FPZL],[FPDM],[FPHM],[FLBM],[XSYH],[FLMC],[XSYHSM],[LSLVBS],[CLBM]) Select [BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW],[FPZL],[FPDM],[FPHM],[FLBM],[XSYH],[FLMC],[XSYHSM],[LSLVBS],[CLBM] From MAIN.[XSDJ];Drop Table MAIN.[XSDJ];Alter Table MAIN.[Temp_824673053] Rename To [XSDJ];");
        list.Add(dictionary129);
        Dictionary<string, string> dictionary130 = new Dictionary<string, string>();
        dictionary130.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX' and type='table' and sql like '%[XSDJBH] varchar(50)%'");
        dictionary130.Add("UPDATE", "Create  TABLE MAIN.[Temp_158237412]([XSDJBH] varchar(50) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6) NOT NULL,[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] VARCHAR(10),[FPHM] int,[SCFPXH] int,[FLBM] varchar(19),[XSYH] bit,[FLMC] varchar(200),[XSYHSM] varchar(500),[SPBM] varchar(16),[LSLVBS] char(1), Primary Key(XSDJBH,XH)   );Insert Into MAIN.[Temp_158237412] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS]) Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS] From MAIN.[XSDJ_MX];Drop Table MAIN.[XSDJ_MX];Alter Table MAIN.[Temp_158237412] Rename To [XSDJ_MX];");
        list.Add(dictionary130);
        Dictionary<string, string> dictionary131 = new Dictionary<string, string>();
        dictionary131.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_HY' and type='table' and sql like '%[BH] varchar(50)%'");
        dictionary131.Add("UPDATE", "Create  TABLE MAIN.[Temp_406267010]([BH] varchar(50) NOT NULL,[GFMC] varchar(100),[GFSH] varchar(25),[GFDZDH] varchar(100),[GFYHZH] varchar(100),[XSBM] varchar(40),[YDXS] bit,[JEHJ] decimal NOT NULL,[DJRQ] datetime NOT NULL,[DJYF] int NOT NULL,[DJZT] char(1) NOT NULL,[KPZT] char(1) NOT NULL,[BZ] varchar(240),[FHR] varchar(8),[SKR] varchar(8),[QDHSPMC] varchar(100),[XFYHZH] varchar(100),[XFDZDH] varchar(100),[CFHB] bit,[DJZL] char(1),[SFZJY] bit,[HYSY] bit,[CM] varchar(100),[DLGRQ] datetime,[KHYHMC] varchar(100),[KHYHZH] varchar(100),[TYDH] varchar(50),[QYD] varchar(100),[ZHD] varchar(100),[XHD] varchar(100),[MDD] varchar(100),[XFDZ] varchar(100),[XFDH] varchar(100),[YSHWXX] varchar(200),[SCCJMC] varchar(100),[SLV] float,[DW] varchar(20),[YSBH] varchar(20) NOT NULL,[FPZL] CHAR(1),[FPDM] VARCHAR(20),[FPHM] INT, Primary Key(BH) );Insert Into MAIN.[Temp_406267010] ([BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW],[YSBH],[FPZL],[FPDM],[FPHM]) Select [BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW],[YSBH],[FPZL],[FPDM],[FPHM] From MAIN.[XSDJ_HY];Drop Table MAIN.[XSDJ_HY];Alter Table MAIN.[Temp_406267010] Rename To [XSDJ_HY];");
        list.Add(dictionary131);
        Dictionary<string, string> dictionary132 = new Dictionary<string, string>();
        dictionary132.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MX_HY' and type='table' and sql like '%[XSDJBH] varchar(50)%'");
        dictionary132.Add("UPDATE", "Create  TABLE MAIN.[Temp_766175860]([XSDJBH] varchar(50) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] varchar(10),[FPHM] int,[SCFPXH] int,[FLBM] varchar(19),[XSYH] bit,[FLMC] varchar(100),[XSYHSM] varchar(400),[SPBM] varchar(16),[LSLVBS] char(1), Primary Key(XSDJBH,XH) );Insert Into MAIN.[Temp_766175860] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS]) Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS] From MAIN.[XSDJ_MX_HY];Drop Table MAIN.[XSDJ_MX_HY];Alter Table MAIN.[Temp_766175860] Rename To [XSDJ_MX_HY];");
        list.Add(dictionary132);
        Dictionary<string, string> dictionary133 = new Dictionary<string, string>();
        dictionary133.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_YL' and type='table' and sql like '%[BH] varchar(50)%'");
        dictionary133.Add("UPDATE", "Create  TABLE MAIN.[Temp_167570075]([BH] varchar(50) NOT NULL,[GFMC] varchar(100),[GFSH] varchar(25),[GFDZDH] varchar(100),[GFYHZH] varchar(100),[XSBM] varchar(20),[YDXS] bit,[JEHJ] decimal NOT NULL,[DJRQ] datetime NOT NULL,[DJYF] int NOT NULL,[DJZT] char(1) NOT NULL,[KPZT] char(1) NOT NULL,[BZ] varchar(250),[FHR] varchar(10),[SKR] varchar(10),[QDHSPMC] varchar(100),[XFYHZH] varchar(100),[XFDZDH] varchar(100),[CFHB] bit,[DJZL] char(1),[SFZJY] bit,[HYSY] bit,[CM] varchar(100),[DLGRQ] datetime,[KHYHMC] varchar(100),[KHYHZH] varchar(100),[TYDH] varchar(50),[QYD] varchar(100),[ZHD] varchar(100),[XHD] varchar(100),[MDD] varchar(100),[XFDZ] varchar(80),[XFDH] varchar(40),[YSHWXX] varchar(200),[SCCJMC] varchar(80),[SLV] float,[DW] varchar(15), Primary Key(BH) );Insert Into MAIN.[Temp_167570075] ([BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW]) Select [BH],[GFMC],[GFSH],[GFDZDH],[GFYHZH],[XSBM],[YDXS],[JEHJ],[DJRQ],[DJYF],[DJZT],[KPZT],[BZ],[FHR],[SKR],[QDHSPMC],[XFYHZH],[XFDZDH],[CFHB],[DJZL],[SFZJY],[HYSY],[CM],[DLGRQ],[KHYHMC],[KHYHZH],[TYDH],[QYD],[ZHD],[XHD],[MDD],[XFDZ],[XFDH],[YSHWXX],[SCCJMC],[SLV],[DW] From MAIN.[XSDJ_YL];Drop Table MAIN.[XSDJ_YL];Alter Table MAIN.[Temp_167570075] Rename To [XSDJ_YL];");
        list.Add(dictionary133);
        Dictionary<string, string> dictionary134 = new Dictionary<string, string>();
        dictionary134.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_MXYL' and type='table' and sql like '%[XSDJBH] varchar(50)%'");
        dictionary134.Add("UPDATE", "Create  TABLE MAIN.[Temp_193138452]([XSDJBH] varchar(50) NOT NULL,[XH] int NOT NULL,[SL] decimal,[DJ] decimal,[JE] decimal NOT NULL,[SLV] float NOT NULL,[SE] decimal NOT NULL,[SPMC] varchar(100) NOT NULL,[SPSM] varchar(6),[GGXH] varchar(40),[JLDW] varchar(32),[HSJBZ] bit NOT NULL,[DJHXZ] int,[FPZL] char(1),[FPDM] VARCHAR(10),[FPHM] int,[SCFPXH] int,[FLBM] varchar(19),[XSYH] bit,[FLMC] varchar(100),[XSYHSM] varchar(400),[SPBM] varchar(16),[LSLVBS] char(1), Primary Key(XSDJBH,XH) );Insert Into MAIN.[Temp_193138452] ([XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS]) Select [XSDJBH],[XH],[SL],[DJ],[JE],[SLV],[SE],[SPMC],[SPSM],[GGXH],[JLDW],[HSJBZ],[DJHXZ],[FPZL],[FPDM],[FPHM],[SCFPXH],[FLBM],[XSYH],[FLMC],[XSYHSM],[SPBM],[LSLVBS] From MAIN.[XSDJ_MXYL];Drop Table MAIN.[XSDJ_MXYL];Alter Table MAIN.[Temp_193138452] Rename To [XSDJ_MXYL];");
        list.Add(dictionary134);
        Dictionary<string, string> dictionary135 = new Dictionary<string, string>();
        dictionary135.Add("CHECK", "");
        dictionary135.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','_BMSPFLManager',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary135);
        Dictionary<string, string> dictionary136 = new Dictionary<string, string>();
        dictionary136.Add("CHECK", "select count(name) from sqlite_master where name='LYGL_PSXX' and type='table' and sql like '%SFTB%'");
        dictionary136.Add("UPDATE", "alter table LYGL_PSXX add SFTB BOOL");
        list.Add(dictionary136);
        Dictionary<string, string> dictionary137 = new Dictionary<string, string>();
        dictionary137.Add("CHECK", "select count(name) from sqlite_master where name='BM_SP' and type='table' and sql like '%SPFLMC%'");
        dictionary137.Add("UPDATE", "alter table BM_SP add column SPFLMC  varchar(200)");
        list.Add(dictionary137);
        Dictionary<string, string> dictionary138 = new Dictionary<string, string>();
        dictionary138.Add("CHECK", "select count(name) from sqlite_master where name='BM_SP' and type='table' and sql like '%YHZCMC%'");
        dictionary138.Add("UPDATE", "alter table BM_SP add column YHZCMC varchar(200)");
        list.Add(dictionary138);
        Dictionary<string, string> dictionary139 = new Dictionary<string, string>();
        dictionary139.Add("CHECK", "select count(name) from sqlite_master where name='BM_SP' and type='table' and sql like '%LSLVBS%'");
        dictionary139.Add("UPDATE", "alter table BM_SP add column LSLVBS varchar(1)");
        list.Add(dictionary139);
        Dictionary<string, string> dictionary140 = new Dictionary<string, string>();
        dictionary140.Add("CHECK", "select count(name) from sqlite_master where name='BM_CL' and type='table' and sql like '%SPFLMC%'");
        dictionary140.Add("UPDATE", "alter table BM_CL add column SPFLMC  varchar(200)");
        list.Add(dictionary140);
        Dictionary<string, string> dictionary141 = new Dictionary<string, string>();
        dictionary141.Add("CHECK", "select count(name) from sqlite_master where name='BM_CL' and type='table' and sql like '%YHZCMC%'");
        dictionary141.Add("UPDATE", "alter table BM_CL add column YHZCMC varchar(200)");
        list.Add(dictionary141);
        Dictionary<string, string> dictionary142 = new Dictionary<string, string>();
        dictionary142.Add("CHECK", "select count(name) from sqlite_master where name='BM_FYXM' and type='table' and sql like '%SPFLMC%'");
        dictionary142.Add("UPDATE", "alter table BM_FYXM add column SPFLMC  varchar(200)");
        list.Add(dictionary142);
        Dictionary<string, string> dictionary143 = new Dictionary<string, string>();
        dictionary143.Add("CHECK", "select count(name) from sqlite_master where name='BM_FYXM' and type='table' and sql like '%YHZCMC%'");
        dictionary143.Add("UPDATE", "alter table BM_FYXM add column YHZCMC varchar(200)");
        list.Add(dictionary143);
        Dictionary<string, string> dictionary144 = new Dictionary<string, string>();
        dictionary144.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ' and type='table' and sql like '%JZ_50_15%'");
        dictionary144.Add("UPDATE", "alter table XSDJ add column JZ_50_15 bit");
        list.Add(dictionary144);
        Dictionary<string, string> dictionary145 = new Dictionary<string, string>();
        dictionary145.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_YL' and type='table' and sql like '%JZ_50_15%'");
        dictionary145.Add("UPDATE", "alter table XSDJ_YL add column JZ_50_15 bit");
        list.Add(dictionary145);
        Dictionary<string, string> dictionary146 = new Dictionary<string, string>();
        dictionary146.Add("CHECK", "select count(name) from sqlite_master where name='XSDJ_HY' and type='table' and sql like '%JZ_50_15%'");
        dictionary146.Add("UPDATE", "alter table XSDJ_HY add column JZ_50_15 bit");
        list.Add(dictionary146);
        Dictionary<string, string> dictionary147 = new Dictionary<string, string>();
        dictionary147.Add("CHECK", "");
        dictionary147.Add("UPDATE", "REPLACE INTO XTBBXX(BBZL,DM,MC,BBBS,SM) VALUES('F','I','单据管理','JI','以文本方式提供的与企业管理信息系统的接口，可以实现发票的批量生成和批量打印。')");
        list.Add(dictionary147);
        Dictionary<string, string> dictionary148 = new Dictionary<string, string>();
        dictionary148.Add("CHECK", "");
        dictionary148.Add("UPDATE", "REPLACE INTO XTBBXX(BBZL,DM,MC,BBBS,SM) VALUES('F','P','集成开票','JP','将防伪税控开票功能嵌入企业销售软件内，组件调用软件基于ActiveX规范，提供标准开票界面和后台命令两种接口方式，实现企业日常开具、作废和打印税控发票的功能，为需要实时打印发票或有专门开票要求的企业提供了适宜的解决方案。')");
        list.Add(dictionary148);
        Dictionary<string, string> dictionary149 = new Dictionary<string, string>();
        dictionary149.Add("CHECK", "SELECT COUNT(*) FROM SYS_CONFIG WHERE NAME='BMB_BBH'");
        dictionary149.Add("UPDATE", "REPLACE INTO SYS_CONFIG(NAME,VALUE) VALUES('BMB_BBH','1.0')");
        list.Add(dictionary149);
        Dictionary<string, string> dictionary150 = new Dictionary<string, string>();
        dictionary150.Add("CHECK", "");
        dictionary150.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fpgl.Fpkj.DKFPPLXZ',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary150);
        Dictionary<string, string> dictionary151 = new Dictionary<string, string>();
        dictionary151.Add("CHECK", "");
        dictionary151.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','_XSDJQL',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary151);
        Dictionary<string, string> dictionary152 = new Dictionary<string, string>();
        dictionary152.Add("CHECK", "");
        dictionary152.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','_BMXHDWManager',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary152);
        Dictionary<string, string> dictionary153 = new Dictionary<string, string>();
        dictionary153.Add("CHECK", "");
        dictionary153.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wsslqr',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary153);
        Dictionary<string, string> dictionary154 = new Dictionary<string, string>();
        dictionary154.Add("CHECK", "");
        dictionary154.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Wsslcx',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary154);
        Dictionary<string, string> dictionary155 = new Dictionary<string, string>();
        dictionary155.Add("CHECK", "");
        dictionary155.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Wsfp',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary155);
        Dictionary<string, string> dictionary156 = new Dictionary<string, string>();
        dictionary156.Add("CHECK", "");
        dictionary156.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Zjwsfp',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary156);
        Dictionary<string, string> dictionary157 = new Dictionary<string, string>();
        dictionary157.Add("CHECK", "");
        dictionary157.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Lyztcx',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary157);
        Dictionary<string, string> dictionary158 = new Dictionary<string, string>();
        dictionary158.Add("CHECK", "");
        dictionary158.Add("UPDATE", "REPLACE INTO QX_JSXX_GNXX(JSXX_DM,GNXX_DM,CJSJ,CJYH) values('-227964606','Menu.Fplygl.Lygl.Drxgfp.Xzzjfpfp',datetime('now', 'localtime'),'admin')");
        list.Add(dictionary158);
        Dictionary<string, string> dictionary159 = new Dictionary<string, string>();
        dictionary159.Add("CHECK", "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TBL_NAME='BM_SPFL' AND [SQL] LIKE '%ISHIDE%' AND TYPE='table'");
        dictionary159.Add("UPDATE", "ALTER TABLE BM_SPFL ADD COLUMN ISHIDE INT NOT NULL DEFAULT 0");
        list.Add(dictionary159);
        Dictionary<string, string> dictionary160 = new Dictionary<string, string>();
        dictionary160.Add("CHECK", "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TBL_NAME='XSDJ_MX' AND [SQL] LIKE '%KCE%' AND TYPE='table'");
        dictionary160.Add("UPDATE", "ALTER TABLE XSDJ_MX ADD COLUMN KCE DECIMAL DEFAULT 0");
        list.Add(dictionary160);
        Dictionary<string, string> dictionary161 = new Dictionary<string, string>();
        dictionary161.Add("CHECK", "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TBL_NAME='XSDJ_MXYL' AND [SQL] LIKE '%KCE%' AND TYPE='table'");
        dictionary161.Add("UPDATE", "ALTER TABLE XSDJ_MXYL ADD COLUMN KCE DECIMAL DEFAULT 0");
        list.Add(dictionary161);
        Dictionary<string, string> dictionary162 = new Dictionary<string, string>();
        dictionary162.Add("CHECK", "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TBL_NAME='XSDJ_MX_HY' AND [SQL] LIKE '%KCE%' AND TYPE='table'");
        dictionary162.Add("UPDATE", "ALTER TABLE XSDJ_MX_HY ADD COLUMN KCE DECIMAL DEFAULT 0");
        list.Add(dictionary162);
        List<Dictionary<string, string>> list2 = list;
        SQLiteConnection connection = null;
        SQLiteTransaction transaction = null;
        try
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            directoryName = directoryName.Substring(0, directoryName.LastIndexOf(@"\"));
            string str2 = Path.Combine(directoryName, @"Bin\");
            try
            {
                if (!File.Exists(Path.Combine(directoryName, @"Bin\cc3268.dll")) && File.Exists(Path.Combine(directoryName, @"Config\DB\FWSK.db")))
                {
                    File.Move(Path.Combine(directoryName, @"Config\DB\FWSK.db"), Path.Combine(directoryName, @"Bin\cc3268.dll"));
                }
            }
            catch (Exception exception)
            {
                ilog_0.ErrorFormat("处理数据库文件异常：{0}", exception.ToString());
                return false;
            }
            connection = new SQLiteConnection {
                ConnectionString = "Data Source=" + Path.Combine(str2, "cc3268.dll") + ";"
            };
            try
            {
                connection.SetPassword("LoveR1314");
                connection.Open();
            }
            catch
            {
                connection.Close();
                connection.SetPassword("LoveR1314");
                connection.Open();
            }
            int result = -1;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            SQLiteCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = "select count(name) as cnt from sqlite_master where type='table' and name='SYS_CONFIG'";
            IDataReader reader = command.ExecuteReader();
            if (reader.Read() && (reader.GetInt32(0) == 1))
            {
                reader.Close();
                command.CommandText = "select VALUE from SYS_CONFIG where NAME='DB_VER'";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int.TryParse(reader.GetString(0), out result);
                }
                reader.Close();
                if ((TaxCardFactory.CreateTaxCard().SoftVersion == "FWKP_V2.0_Svr_Client") && (result <= 0x48))
                {
                    result = -1;
                }
            }
            else
            {
                reader.Close();
                command.CommandText = "Create TABLE SYS_CONFIG([NAME] varchar(50) PRIMARY KEY,[VALUE] varchar(50))";
                command.ExecuteNonQuery();
            }
            if (result < (list2.Count - 1))
            {
                MessageHelper.MsgWait("正在更新数据库，请耐心等待...");
            }
            for (int i = result + 1; i < list2.Count; i++)
            {
                Dictionary<string, string> dictionary163 = list2[i];
                string str3 = dictionary163["CHECK"];
                bool flag3 = false;
                if (!string.IsNullOrEmpty(str3))
                {
                    command.CommandText = str3;
                    reader = command.ExecuteReader();
                    if (!reader.Read() || (reader.GetInt32(0) == 0))
                    {
                        flag3 = true;
                    }
                    reader.Close();
                }
                if (string.IsNullOrEmpty(str3) || flag3)
                {
                    command.CommandText = dictionary163["UPDATE"];
                    command.ExecuteNonQuery();
                }
            }
            command.CommandText = "replace into SYS_CONFIG (NAME,VALUE) values('DB_VER','" + (list2.Count - 1) + "')";
            command.ExecuteNonQuery();
            transaction.Commit();
            flag = true;
        }
        catch (Exception exception2)
        {
            ilog_0.ErrorFormat("更新数据库异常：{0}", exception2.ToString());
            transaction.Rollback();
            flag = false;
        }
        finally
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        MessageHelper.MsgWait();
        return flag;
    }

    private static bool smethod_4()
    {
        bool flag = true;
        string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string str2 = Path.Combine(directoryName.Substring(0, directoryName.LastIndexOf(@"\")), @"Bin\");
        if (File.Exists(Path.Combine(str2, "message.dll")))
        {
            MessageHelper.MsgWait("正在更新数据库，请耐心等待...");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + Path.Combine(str2, "cc3268.dll") + ";"))
            {
                try
                {
                    connection.Open();
                    connection.ChangePassword("LoveR1314");
                }
                catch
                {
                    connection.Close();
                    connection.SetPassword("LoveR1314");
                    connection.Open();
                    connection.ChangePassword("LoveR1314");
                }
                string commandText = string.Format("attach database '{0}' AS message key '{1}';", Path.Combine(str2, "message.dll"), "LoveR1314");
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    MessageHelper.MsgWait();
                    return false;
                }
                SQLiteTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    commandText = "delete from [ERRMSG];insert into [ERRMSG] select * from message.[ERRMSG];delete from [ERRMSG2];insert into [ERRMSG2] select * from message.[ERRMSG2];";
                    using (SQLiteCommand command2 = new SQLiteCommand(commandText, connection))
                    {
                        command2.Transaction = transaction;
                        command2.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    flag = false;
                }
                finally
                {
                    if ((connection != null) && (connection.State != ConnectionState.Closed))
                    {
                        connection.Close();
                    }
                }
            }
            if (flag && File.Exists(Path.Combine(str2, "message.dll")))
            {
                File.Delete(Path.Combine(str2, "message.dll"));
            }
            MessageHelper.MsgWait();
        }
        return flag;
    }

    private static void smethod_5()
    {
        string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string str2 = Path.Combine(directoryName.Substring(0, directoryName.LastIndexOf(@"\")), @"Bin\plugin\");
        if (File.Exists(Path.Combine(str2, "Aisino.Fwkp.FpUpDownLoad.plugin")))
        {
            File.Delete(Path.Combine(str2, "Aisino.Fwkp.FpUpDownLoad.plugin"));
        }
        if (File.Exists(Path.Combine(str2, "Aisino.Fwkp.FpUpDownLoad.sql.xml")))
        {
            File.Delete(Path.Combine(str2, "Aisino.Fwkp.FpUpDownLoad.sql.xml"));
        }
        string str3 = PropertyUtil.GetValue("MAIN_ORGCODE", "");
        switch (str3)
        {
            case null:
            case "":
            {
                byte[] buffer = new byte[30];
                switch (GetAreaCode(buffer, Encoding.GetEncoding("GBK").GetBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JSDiskDLL.dll"))))
                {
                    case 0:
                        str3 = Encoding.GetEncoding("GBK").GetString(buffer).Trim(new char[1]);
                        if (TaxCardFactory.CreateTaxCard().SoftVersion == "FWKP_V2.0_Svr_Client")
                        {
                            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwqkp.exe", "orgcode", str3);
                        }
                        else
                        {
                            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", "orgcode", str3);
                        }
                        PropertyUtil.SetValue("MAIN_ORGCODE", str3);
                        return;

                    case 1:
                        MessageBoxHelper.Show("未检测到金税盘，请检查金税盘连接是否正常！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        Environment.Exit(0);
                        return;
                }
                MessageBoxHelper.Show("地区编号错误,请重新运行程序！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(0);
                break;
            }
        }
    }

    private static object[] smethod_6()
    {
        return new object[] { UserInfo.IsAdmin, UserInfo.Yhmc, UserInfo.Jsqx, UserInfo.Gnqx };
    }

    private static void smethod_7()
    {
        new CusMessageBox("信息", DateTime.Now.ToString(), "主程序运行", "FRM-000012", "程序已经在运行中。", "关闭本窗口后，切换到运行中的程序。", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) { TopMost = true }.ShowDialog();
    }
}

