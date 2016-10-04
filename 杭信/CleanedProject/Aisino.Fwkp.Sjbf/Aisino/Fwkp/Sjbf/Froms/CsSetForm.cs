namespace Aisino.Fwkp.Sjbf.Froms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Sjbf.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    public class CsSetForm : BaseForm
    {
        private static ILog _Loger = LogUtil.GetLogger<CsSetForm>();
        private AisinoBTN btn_Cancle;
        private AisinoBTN btn_FindPath;
        private AisinoBTN btn_OK;
        private AisinoCHK chk_BeginningMonthCopy;
        private AisinoCHK chk_EndRunCopy;
        private AisinoCHK chk_XtcshBf;
        private AisinoCMB combo_Day_Month;
        private IContainer components;
        private static DateTime dateTimeNow;
        private AisinoLBL label2;
        private static string strDataCopyItem = string.Empty;
        private static string strSrcPathCopy = string.Empty;
        private AisinoTXT textBox_PathName;
        private AisinoTXT textBox_Sjjg;
        private XmlComponentLoader xmlComponentLoader1;

        public CsSetForm()
        {
            try
            {
                this.Initialize();
                dateTimeNow = DateTime.Now;
                GetPath_FWSK();
                this.combo_Day_Month.Items.Add(ZhiFuChuan.strIntervalTimeTypeValue[0]);
                this.combo_Day_Month.Items.Add(ZhiFuChuan.strIntervalTimeTypeValue[1]);
                this.combo_Day_Month.DropDownStyle = ComboBoxStyle.DropDownList;
                this.GetValue();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static bool AutomaticCopy()
        {
            try
            {
                SetDefaultIntervalTime();
                string s = PropertyUtil.GetValue(ZhiFuChuan.strPreIntervalTimeCopy);
                string str2 = PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeNum);
                string str3 = PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeType);
                if ((!str2.Equals("0") && !string.IsNullOrEmpty(str2)) && !string.IsNullOrEmpty(str3))
                {
                    DateTime time = DateTime.ParseExact(s, ZhiFuChuan.strIntervalTimeFormat, CultureInfo.CurrentCulture);
                    int result = 0;
                    int.TryParse(str2, out result);
                    if (str3.Equals(ZhiFuChuan.strIntervalTimeTypeValue[1]))
                    {
                        time = time.AddMonths(result + 1);
                    }
                    else
                    {
                        time = time.AddDays((double) (result + 1));
                    }
                    if (dateTimeNow >= time)
                    {
                        s = dateTimeNow.ToString(ZhiFuChuan.strIntervalTimeFormat);
                        PropertyUtil.SetValue(ZhiFuChuan.strPreIntervalTimeCopy, s);
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        public static void BeginRunCopy(bool bDirectCopy, bool bTipSuccess)
        {
            try
            {
                dateTimeNow = DateTime.Now;
                GetValueRunCopy();
                GetPath_FWSK();
                string str = "数据备份时间:" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat1);
                strDataCopyItem = "数据备份满足条件:";
                string str2 = "操作员:" + UserInfo.Yhmc;
                string str3 = "数据库备份名称:" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseName;
                if (!bDirectCopy)
                {
                    if (!AutomaticCopy())
                    {
                        if (!TiaoJianCopy(true))
                        {
                            return;
                        }
                    }
                    else
                    {
                        strDataCopyItem = strDataCopyItem + "间隔一定时间备份";
                    }
                }
                else
                {
                    strDataCopyItem = strDataCopyItem + "强制数据备份";
                }
                string str4 = dateTimeNow.ToString(ZhiFuChuan.strIntervalTimeFormat);
                PropertyUtil.SetValue(ZhiFuChuan.strPreIntervalTimeCopy, str4);
                string strDefaultPathCopy = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(strDefaultPathCopy) && !Directory.Exists(strDefaultPathCopy))
                {
                    Directory.CreateDirectory(strDefaultPathCopy);
                }
                if (string.IsNullOrEmpty(strDefaultPathCopy) || !Directory.Exists(strDefaultPathCopy))
                {
                    strDefaultPathCopy = ZhiFuChuan.strDefaultPathCopy;
                    PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, strDefaultPathCopy);
                }
                string path = strDefaultPathCopy + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseZipName;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str7 = path;
                StreamWriter writer = File.AppendText(str7 + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-备份说明.txt");
                writer.WriteLine(str);
                writer.WriteLine(strDataCopyItem);
                writer.WriteLine(str2);
                writer.WriteLine(str3);
                writer.WriteLine("软件版本号:" + PropertyUtil.GetValue("MAIN_VER"));
                writer.WriteLine("纳税人识别号:" + TaxCardFactory.CreateTaxCard().TaxCode);
                writer.WriteLine("开票机号:" + TaxCardFactory.CreateTaxCard().Machine.ToString());
                writer.Flush();
                writer.Close();
                Copying(bTipSuccess);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_FindPath_Click(object sender, EventArgs e)
        {
            try
            {
                string str = this.textBox_PathName.Text.Trim();
                FolderBrowserDialog dialog = new FolderBrowserDialog {
                    ShowNewFolderButton = true
                };
                string selectedPath = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    if (Directory.Exists(selectedPath))
                    {
                        dialog.SelectedPath = selectedPath;
                    }
                }
                else
                {
                    dialog.SelectedPath = Application.StartupPath;
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = dialog.SelectedPath;
                    DriveInfo info = new DriveInfo(selectedPath.Substring(0, 1));
                    if (DriveType.Fixed != info.DriveType)
                    {
                        MessageBoxHelper.Show("请选择一个固定磁盘！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (ToolUtil.GetBytes(selectedPath).Length > 200)
                    {
                        MessageBoxHelper.Show("完全路径长度不能超过189字符!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.textBox_PathName.Text = str;
                    }
                    else
                    {
                        this.textBox_PathName.Text = selectedPath;
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimeNow = DateTime.Now;
                if (this.TipsThatSamePath() && this.TipsLackSpace())
                {
                    this.SetValue();
                    base.Close();
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void combo_Day_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.combo_Day_Month.SelectedIndex.Equals(0))
                {
                    this.textBox_Sjjg.MaxLength = 3;
                }
                else if (this.combo_Day_Month.SelectedIndex.Equals(1))
                {
                    this.textBox_Sjjg.MaxLength = 2;
                }
                else
                {
                    this.textBox_Sjjg.MaxLength = 4;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static void Copying(bool bTipSuccess)
        {
            try
            {
                string strDefaultPathCopy = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(strDefaultPathCopy) && !Directory.Exists(strDefaultPathCopy))
                {
                    Directory.CreateDirectory(strDefaultPathCopy);
                }
                if (string.IsNullOrEmpty(strDefaultPathCopy) || !Directory.Exists(strDefaultPathCopy))
                {
                    strDefaultPathCopy = ZhiFuChuan.strDefaultPathCopy;
                    PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, strDefaultPathCopy);
                }
                string path = strDefaultPathCopy + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseZipName;
                strDefaultPathCopy = path + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseName;
                File.Copy(strSrcPathCopy, strDefaultPathCopy, true);
                if (ZipUtil.Zip(path, path + ".zip", null))
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    AES_Crypt_File.EncryptFile(path + ".zip");
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    MessageManager.ShowMsgBox("INP-442215");
                    return;
                }
                if (bTipSuccess)
                {
                    MessageManager.ShowMsgBox("SJBF-000001", new string[] { path + ".zip" });
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static void EndRunCopy(bool bDirectCopy, bool bTipSuccess)
        {
            try
            {
                dateTimeNow = DateTime.Now;
                GetValueRunCopy();
                GetPath_FWSK();
                string str = "数据备份时间:" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat1);
                strDataCopyItem = "数据备份满足条件:";
                string str2 = "操作员:" + UserInfo.Yhmc;
                string str3 = "数据库备份名称:" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseName;
                if (!bDirectCopy)
                {
                    if (!AutomaticCopy())
                    {
                        if (!TiaoJianCopy(false))
                        {
                            return;
                        }
                    }
                    else
                    {
                        strDataCopyItem = strDataCopyItem + "间隔一定时间备份";
                    }
                }
                else
                {
                    strDataCopyItem = strDataCopyItem + "系统初始化";
                }
                string str4 = dateTimeNow.ToString(ZhiFuChuan.strIntervalTimeFormat);
                PropertyUtil.SetValue(ZhiFuChuan.strPreIntervalTimeCopy, str4);
                string strDefaultPathCopy = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(strDefaultPathCopy) && !Directory.Exists(strDefaultPathCopy))
                {
                    Directory.CreateDirectory(strDefaultPathCopy);
                }
                if (string.IsNullOrEmpty(strDefaultPathCopy) || !Directory.Exists(strDefaultPathCopy))
                {
                    strDefaultPathCopy = ZhiFuChuan.strDefaultPathCopy;
                    PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, strDefaultPathCopy);
                }
                string path = strDefaultPathCopy + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-" + ZhiFuChuan.strDateBaseZipName;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str7 = path;
                StreamWriter writer = File.AppendText(str7 + @"\" + dateTimeNow.ToString(ZhiFuChuan.strDateTimeFormat) + "-" + PropertyUtil.GetValue("MAIN_VER") + "-备份说明.txt");
                writer.WriteLine(str);
                writer.WriteLine(strDataCopyItem);
                writer.WriteLine(str2);
                writer.WriteLine(str3);
                writer.WriteLine("软件版本号:" + PropertyUtil.GetValue("MAIN_VER"));
                writer.WriteLine("纳税人识别号:" + TaxCardFactory.CreateTaxCard().TaxCode);
                writer.WriteLine("开票机号:" + TaxCardFactory.CreateTaxCard().Machine.ToString());
                writer.Flush();
                writer.Close();
                Copying(bTipSuccess);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public static string GetPath_FWSK()
        {
            try
            {
                strSrcPathCopy = Application.StartupPath;
                int length = strSrcPathCopy.LastIndexOf('\\');
                strSrcPathCopy = strSrcPathCopy.Substring(0, length);
                strSrcPathCopy = strSrcPathCopy + @"\Bin";
                strSrcPathCopy = strSrcPathCopy + @"\" + ZhiFuChuan.strDateBaseName;
                return strSrcPathCopy;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        private void GetValue()
        {
            try
            {
                dateTimeNow = DateTime.Now;
                SetDefaultPathCopy();
                string str = PropertyUtil.GetValue(ZhiFuChuan.strBeginningMonthCopy);
                if (string.IsNullOrEmpty(str))
                {
                    PropertyUtil.SetValue(ZhiFuChuan.strBeginningMonthCopy, ZhiFuChuan.strRight);
                    this.chk_BeginningMonthCopy.Checked = true;
                }
                else if (!str.Equals(ZhiFuChuan.strWrong))
                {
                    this.chk_BeginningMonthCopy.Checked = true;
                }
                else
                {
                    this.chk_BeginningMonthCopy.Checked = false;
                }
                string str2 = PropertyUtil.GetValue(ZhiFuChuan.strEndRunCopy);
                if (string.IsNullOrEmpty(str2))
                {
                    PropertyUtil.SetValue(ZhiFuChuan.strEndRunCopy, ZhiFuChuan.strWrong);
                    this.chk_EndRunCopy.Checked = false;
                }
                else if (!str2.Equals(ZhiFuChuan.strWrong))
                {
                    this.chk_EndRunCopy.Checked = true;
                }
                else
                {
                    this.chk_EndRunCopy.Checked = false;
                }
                SetDefaultIntervalTime();
                string s = PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeNum);
                int result = 0;
                int.TryParse(s, out result);
                s = result.ToString();
                this.textBox_Sjjg.Text = s;
                string str4 = PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeType);
                int index = 0;
                if (string.IsNullOrEmpty(str4))
                {
                    string str5 = ZhiFuChuan.strIntervalTimeTypeValue[0];
                    PropertyUtil.SetValue(ZhiFuChuan.strIntervalTimeType, str5);
                    index = 0;
                }
                else
                {
                    index = this.combo_Day_Month.Items.IndexOf(str4);
                }
                this.combo_Day_Month.SelectedIndex = index;
                string strDefaultPathCopy = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(strDefaultPathCopy) && !Directory.Exists(strDefaultPathCopy))
                {
                    Directory.CreateDirectory(strDefaultPathCopy);
                }
                if (string.IsNullOrEmpty(strDefaultPathCopy) || !Directory.Exists(strDefaultPathCopy))
                {
                    strDefaultPathCopy = ZhiFuChuan.strDefaultPathCopy;
                    PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, strDefaultPathCopy);
                }
                this.textBox_PathName.Text = strDefaultPathCopy;
                this.textBox_PathName.ReadOnly = true;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static void GetValueRunCopy()
        {
            try
            {
                SetDefaultPathCopy();
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(ZhiFuChuan.strBeginningMonthCopy)))
                {
                    PropertyUtil.SetValue(ZhiFuChuan.strBeginningMonthCopy, ZhiFuChuan.strRight);
                }
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(ZhiFuChuan.strEndRunCopy)))
                {
                    PropertyUtil.SetValue(ZhiFuChuan.strEndRunCopy, ZhiFuChuan.strWrong);
                }
                SetDefaultIntervalTime();
                string s = PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeNum);
                int result = 0;
                int.TryParse(s, out result);
                s = result.ToString();
                if (string.IsNullOrEmpty(PropertyUtil.GetValue(ZhiFuChuan.strIntervalTimeType)))
                {
                    string str5 = ZhiFuChuan.strIntervalTimeTypeValue[0];
                    PropertyUtil.SetValue(ZhiFuChuan.strIntervalTimeType, str5);
                }
                string strDefaultPathCopy = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(strDefaultPathCopy) && !Directory.Exists(strDefaultPathCopy))
                {
                    Directory.CreateDirectory(strDefaultPathCopy);
                }
                if (string.IsNullOrEmpty(strDefaultPathCopy) || !Directory.Exists(strDefaultPathCopy))
                {
                    strDefaultPathCopy = ZhiFuChuan.strDefaultPathCopy;
                    PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, strDefaultPathCopy);
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.textBox_PathName = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_PathName");
            this.chk_EndRunCopy = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chk_EndRunCopy");
            this.chk_BeginningMonthCopy = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chk_BeginningMonthCopy");
            this.chk_XtcshBf = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chk_XtcshBf");
            this.chk_XtcshBf.Enabled = false;
            this.textBox_Sjjg = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBox_Sjjg");
            this.combo_Day_Month = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_Day_Month");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.btn_FindPath = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_FindPath");
            this.btn_OK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_OK");
            this.btn_Cancle = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Cancle");
            this.btn_FindPath.Click += new EventHandler(this.btn_FindPath_Click);
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancle.Click += new EventHandler(this.btn_Cancle_Click);
            this.chk_EndRunCopy.CheckedChanged += new EventHandler(this.Chk_CheckedChanged);
            this.chk_BeginningMonthCopy.CheckedChanged += new EventHandler(this.Chk_CheckedChanged);
            this.textBox_Sjjg.KeyPress += new KeyPressEventHandler(this.textBox_Sjjg_KeyPress);
            this.textBox_Sjjg.TextChanged += new EventHandler(this.textBox_Sjjg_TextChanged);
            this.combo_Day_Month.SelectedIndexChanged += new EventHandler(this.combo_Day_Month_SelectedIndexChanged);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x204, 0x12a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Sjbf.Froms.CsSetForm\Aisino.Fwkp.Sjbf.Froms.CsSetForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x204, 0x12a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CsSetForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "数据备份选项设置";
            base.ResumeLayout(false);
        }

        private static void SetDefaultIntervalTime()
        {
            try
            {
                string str = PropertyUtil.GetValue(ZhiFuChuan.strPreIntervalTimeCopy);
                if (string.IsNullOrEmpty(str))
                {
                    str = dateTimeNow.ToString(ZhiFuChuan.strIntervalTimeFormat);
                    PropertyUtil.SetValue(ZhiFuChuan.strPreIntervalTimeCopy, str);
                }
                else
                {
                    try
                    {
                        DateTime.ParseExact(str, ZhiFuChuan.strIntervalTimeFormat, CultureInfo.CurrentCulture);
                    }
                    catch (Exception)
                    {
                        str = dateTimeNow.ToString(ZhiFuChuan.strIntervalTimeFormat);
                        PropertyUtil.SetValue(ZhiFuChuan.strPreIntervalTimeCopy, str);
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static void SetDefaultPathCopy()
        {
            try
            {
                string str = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                bool flag = false;
                bool flag2 = false;
                if (str.Length > 0)
                {
                    flag2 = new DriveInfo(str.Substring(0, 1)).DriveType == DriveType.Fixed;
                }
                if ((string.IsNullOrEmpty(str) || !char.IsLetter(str[0])) || (!flag2 || !Directory.Exists(str)))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                if (flag)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    for (int i = drives.Length - 1; i >= 0; i--)
                    {
                        if (drives[i].DriveType == DriveType.Fixed)
                        {
                            ZhiFuChuan.strDefaultPathCopy = Path.Combine(drives[i].Name, string.Concat(new object[] { @"开票软件数据备份\", TaxCardFactory.CreateTaxCard().TaxCode, "-", TaxCardFactory.CreateTaxCard().Machine }));
                            PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, ZhiFuChuan.strDefaultPathCopy);
                            PropertyUtil.Save();
                            break;
                        }
                    }
                }
                string str2 = PropertyUtil.GetValue(ZhiFuChuan.strPathCopy);
                if (!string.IsNullOrEmpty(str2) && !Directory.Exists(str2))
                {
                    Directory.CreateDirectory(str2);
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void SetValue()
        {
            try
            {
                SetDefaultPathCopy();
                string strRight = string.Empty;
                if (this.chk_BeginningMonthCopy.Checked)
                {
                    strRight = ZhiFuChuan.strRight;
                }
                else
                {
                    strRight = ZhiFuChuan.strWrong;
                }
                PropertyUtil.SetValue(ZhiFuChuan.strBeginningMonthCopy, strRight);
                string strWrong = string.Empty;
                if (this.chk_EndRunCopy.Checked)
                {
                    strWrong = ZhiFuChuan.strRight;
                }
                else
                {
                    strWrong = ZhiFuChuan.strWrong;
                }
                PropertyUtil.SetValue(ZhiFuChuan.strEndRunCopy, strWrong);
                string str3 = this.textBox_Sjjg.Text.Trim();
                int result = 0;
                int.TryParse(str3.Trim(), out result);
                if (result < 0)
                {
                    result = 0;
                }
                str3 = result.ToString();
                PropertyUtil.SetValue(ZhiFuChuan.strIntervalTimeNum, str3);
                int selectedIndex = this.combo_Day_Month.SelectedIndex;
                PropertyUtil.SetValue(ZhiFuChuan.strIntervalTimeType, ZhiFuChuan.strIntervalTimeTypeValue[selectedIndex]);
                string text = string.Empty;
                text = this.textBox_PathName.Text;
                if (string.IsNullOrEmpty(text) || !Directory.Exists(text))
                {
                    text = ZhiFuChuan.strDefaultPathCopy;
                }
                PropertyUtil.SetValue(ZhiFuChuan.strPathCopy, text);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void textBox_Sjjg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == ' ')
                {
                    e.KeyChar = '\0';
                }
                if (((e.KeyChar != '-') || (((AisinoTXT) sender).Text.Length != 0)) && (e.KeyChar > ' '))
                {
                    try
                    {
                        int.Parse(((AisinoTXT) sender).Text + e.KeyChar.ToString()).ToString();
                    }
                    catch
                    {
                        e.KeyChar = '\0';
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void textBox_Sjjg_TextChanged(object sender, EventArgs e)
        {
        }

        private static bool TiaoJianCopy(bool bBeginOrEnd)
        {
            try
            {
                if (bBeginOrEnd)
                {
                    DateTime dateTimeNow = CsSetForm.dateTimeNow;
                    int year = dateTimeNow.Year;
                    int month = dateTimeNow.Month;
                    int day = dateTimeNow.Day;
                    if (!PropertyUtil.GetValue(ZhiFuChuan.strBeginningMonthCopy).Equals(ZhiFuChuan.strRight))
                    {
                        return false;
                    }
                    DateTime time2 = DateTime.ParseExact(PropertyUtil.GetValue(ZhiFuChuan.strPreIntervalTimeCopy), ZhiFuChuan.strIntervalTimeFormat, CultureInfo.CurrentCulture);
                    int num = time2.Year;
                    int num2 = time2.Month;
                    int num5 = time2.Day;
                    bool flag = false;
                    if (CsSetForm.dateTimeNow.Year > num)
                    {
                        flag = true;
                    }
                    else if (CsSetForm.dateTimeNow.Month > num2)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        strDataCopyItem = strDataCopyItem + "每月月初备份数据";
                    }
                    return flag;
                }
                if (!PropertyUtil.GetValue(ZhiFuChuan.strEndRunCopy).Equals(ZhiFuChuan.strWrong))
                {
                    strDataCopyItem = strDataCopyItem + "程序每次运行结束备份数据";
                    return true;
                }
                return false;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private bool TipsLackSpace()
        {
            try
            {
                TipsLackSpaceForm form = new TipsLackSpaceForm {
                    strSrcPathCopy = strSrcPathCopy,
                    strDestPathCopy = this.textBox_PathName.Text.Trim()
                };
                form.Refresh();
                if (!form.Run(TipsLackSpaceForm.TipsType.TipsLackSpace))
                {
                    form.Refresh();
                    return true;
                }
                if (DialogResult.OK == form.ShowDialog())
                {
                    form.Refresh();
                    return true;
                }
                form.Refresh();
                return false;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private bool TipsThatSamePath()
        {
            try
            {
                TipsLackSpaceForm form = new TipsLackSpaceForm {
                    strSrcPathCopy = strSrcPathCopy,
                    strDestPathCopy = this.textBox_PathName.Text.Trim()
                };
                form.Refresh();
                if (!form.Run(TipsLackSpaceForm.TipsType.TipsThatSamePath))
                {
                    form.Refresh();
                    return true;
                }
                if (DialogResult.OK == form.ShowDialog())
                {
                    form.Refresh();
                    return true;
                }
                form.Refresh();
                return false;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }
    }
}

