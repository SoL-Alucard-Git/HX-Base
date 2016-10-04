namespace Aisino.Fwkp.Fpkj.Form.FPXF
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using log4net;
    using BusinessObject;
    public class FpxfMain : DockForm
    {
        private int _AccMonth;
        private bool _bShowDialog;
        private bool _bUSBKeyExist = true;
        private int _iMonth;
        private bool _isSingle;
        private int _iYear;
        private string _xffpdm;
        private string _xffphm;
        public AutoResetEvent AutoEvent = new AutoResetEvent(false);
        private AisinoBTN btStopXiufu;
        private Hashtable FpHashTable;
        public ProgressBar fpxf_progressBar;
        public static bool IsStopXiufu;
        private Label lab_Current;
        private Label label_Tip;
        private Label label_Title;
        private ILog loger = LogUtil.GetLogger<FpxfMain>();
        private int PerformantLevel = 1;
        private int step = 0x3e8;
        private uint TaxCardFPNum;
        public Thread xfThrad;
        private XXFP xxfpBll = new XXFP(false);

        public FpxfMain()
        {
            try
            {
                this.InitializeComponent();
                this.InitializeFPProgressBar();
                this.InitProgressBar();
                this.Text = "发票修复过程";
                this._AccMonth = base.TaxCardInstance.RepDate.Month;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void btStopXiufu_Click(object sender, EventArgs e)
        {
            IsStopXiufu = true;
            this.SetTipThread("正在停止发票修复", "请等待任务完成");
        }

        private bool CanFpxf()
        {
            bool flag = base.TaxCardInstance.StateInfo.IsLockReached == 0;
            if (!flag)
            {
                return false;
            }
            List<InvTypeInfo> invTypeInfo = base.TaxCardInstance.StateInfo.InvTypeInfo;
            if ((invTypeInfo == null) || (invTypeInfo.Count == 0))
            {
                return flag;
            }
            foreach (InvTypeInfo info in invTypeInfo)
            {
                if ((base.TaxCardInstance.QYLX.ISJDC || base.TaxCardInstance.QYLX.ISHY) && (info.IsLockTime != 0))
                {
                    return false;
                }
            }
            return true;
        }

        private void DisposeForm(object obj)
        {
            base.Dispose();
        }

        public void DisposeFormThread(object obj)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    DisposeFormHandle method = new DisposeFormHandle(this.DisposeForm);
                    base.BeginInvoke(method, new object[] { this.step });
                }
                else
                {
                    this.DisposeForm(this.step);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[DisposeFormThread函数]" + exception.Message);
            }
        }

        private void FpxfMain_Load(object sender, EventArgs e)
        {
            FPXFArgs args = new FPXFArgs {
                IsShow = this.XFIsShowDialog,
                XFMonth = this.XFMonth,
                XFYear = this.XFYear,
                XFfpdm = this.XFFpdm,
                XFfphm = this.XFFPHM
            };
            this.StartThread(args);
        }

        private bool GetCardFpDataNewVersion(int StockCount, RepairReport repairReport)
        {
            try
            {
                InvDetail invDetail = null;
                Fpxx fpxx = null;
                int num = 0;
                int num2 = 1;
                int num3 = 1;
                int num4 = 10;
                string data = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.StockStep");
                if (data == "")
                {
                    PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.StockStep", num4.ToString());
                }
                else
                {
                    num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(data);
                }
                if ((num4 <= 0) || base.TaxCardInstance.IsLargeInvDetail)
                {
                    num4 = 1;
                }
                string title = "正在进行发票修复...";
                int num5 = 1;
                int num6 = StockCount / num4;
                if ((StockCount % num4) != 0)
                {
                    num6++;
                }
                int num7 = 0;
                int num8 = 0;
                this.TaxCardFPNum = 0;
                double num9 = 7000.0;
                double num10 = 1.0;
                Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this._iYear.ToString() + this._iMonth.ToString("D2"));
                GC.Collect();
                if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    Dictionary<string, object> condition = new Dictionary<string, object>();
                    string str4 = this._iYear.ToString("D2") + this._iMonth.ToString("D2");
                    condition.Add("SSYF", str4);
                    this.loger.Info("[发票修复]：开始读取DB发票...");
                    this.FpHashTable = this.xxfpBll.SelectFPListFromKprq(condition);
                    this.loger.Info("[发票修复]：结束读取数据库发票...");
                }
                this.ProcessStartThread(6 * this.step);
                do
                {
                    if (num7 >= StockCount)
                    {
                        this.loger.Info("[发票修复]：break111时，起始块：" + num7.ToString() + "终止块数：" + num8.ToString());
                        break;
                    }
                    num8 = (num7 + num4) - 1;
                    if (num8 >= StockCount)
                    {
                        num8 = StockCount - 1;
                    }
                    if (num8 > num7)
                    {
                        this.loger.Info("[发票修复]：break222，起始块：" + num7.ToString() + "终止块数：" + num8.ToString());
                        break;
                    }
                    GC.Collect();
                    this.ResetProcessBarThread(1);
                    this.ProcessStartThread(2 * this.step);
                    title = string.Concat(new object[] { "正在读取", this._iMonth, "月份第", num5.ToString(), "部分发票" });
                    this.SetTipThread(title, "请等待任务完成");
                    this.loger.Info("[发票修复]：开始读取发票块数，起始块：" + num7.ToString() + "终止块数：" + num8.ToString());
                    num3 = base.TaxCardInstance.GetStockInvCount(this._iYear, this._iMonth, num7, num8);
                    this.loger.Info("[发票修复]：完成读取发票块数,读取发票张数：" + num3.ToString());
                    title = string.Concat(new object[] { "正在修复", this._iMonth, "月份第", num5.ToString(), "部分/共", num6.ToString(), "部分" });
                    num5++;
                    this.SetTipThread(title, "请等待任务完成");
                    this.TaxCardFPNum += (uint) num3;
                    num9 /= (double) num3;
                    num7 += num4;
                    if (base.TaxCardInstance.RetCode != 0)
                    {
                        repairReport.SaveToDB();
                        this.ProcessStartThread(8 * this.step);
                        this.loger.Error("[发票修复]：读取发票块数失败，起始块：" + num7.ToString() + "终止块数：" + num8.ToString() + "发票份数：" + num3.ToString());
                        return false;
                    }
                    if ((base.TaxCardInstance.RetCode == 0) && (num3 <= 0))
                    {
                        repairReport.SaveToDB();
                        this.ProcessStartThread(8 * this.step);
                        this.loger.Error("[发票修复]：读取发票块数失败，起始块：" + num7.ToString() + "终止块数：" + num8.ToString() + "发票份数：" + num3.ToString());
                        return true;
                    }
                    for (int i = 0; (i < num3) && !IsStopXiufu; i++)
                    {
                        TextType type;
                        type.visible = true;
                        num = i + 1;
                        type.Text = "第 " + num.ToString() + "张/共" + num3.ToString() + "张";
                        this.SetCurrentFPTextThread(type);
                        num10 = num9 * num2;
                        if (num10 >= 1.0)
                        {
                            num2 = 1;
                            this.ProcessStartThread((int) Math.Round(num10, 0));
                        }
                        else
                        {
                            num2++;
                        }
                        invDetail = null;
                        fpxx = null;
                        invDetail = base.TaxCardInstance.GetInvDetail((long) i);
                        this.loger.Info("[发票修复]：完成读取第" + i.ToString() + "张发票");
                        if (base.TaxCardInstance.RetCode != 0)
                        {
                            MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                            this.loger.Error(string.Concat(new object[] { "[发票修复]：读取金税设备错误，错误号：", base.TaxCardInstance.RetCode, "错误信息：", base.TaxCardInstance.ErrCode }));
                            return false;
                        }
                        if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
                        {
                            fpxx = new Fpxx();
                            fpxx.RepairInv(invDetail, repairReport.currentPeriodCount[invDetail.InvType]);
                        }
                        else
                        {
                            byte[] buffer = ToolUtil.FromBase64String(invDetail.OldInvNo);
                            if (base.TaxCardInstance.SubSoftVersion == "Linux")
                            {
                                fpxx = Fpxx.DeSeriealize_Linux(buffer);
                            }
                            else
                            {
                                fpxx = (Fpxx) SerializeUtil.Deserialize(buffer);
                            }
                            if (fpxx == null)
                            {
                                this.loger.Debug("[发票修复时发票对象为空，发票修复继续。当前修复失败的]发票代码：" + invDetail.TypeCode + "发票号码：" + invDetail.InvNo.ToString());
                                continue;
                            }
                        }
                        fpxx.dybz = true;
                        this.loger.Info("[发票修复]：结束调用对象构造函数");
                        string key = string.Concat(new object[] { Aisino.Fwkp.Fpkj.Common.Tool.PareFpType(fpxx.fplx), fpxx.fpdm, "_", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fpxx.fphm) });
                        if (!repairReport.HashFPFromTaxCard.ContainsKey(key))
                        {
                            repairReport.HashFPFromTaxCard.Add(key, i);
                        }
                        else
                        {
                            this.loger.Error("[发票修复]：发票主键重复: " + key);
                        }
                        if (this.PerformantLevel == 1)
                        {
                            key = fpxx.fpdm + "_" + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fpxx.fphm);
                            if (((this.FpHashTable == null) || (this.FpHashTable.Count == 0)) || (this.FpHashTable[key] == null))
                            {
                                repairReport.modelXXFP = null;
                            }
                            else
                            {
                                repairReport.modelXXFP = this.FpHashTable[key] as Fpxx;
                            }
                            repairReport.RepairInvoice(fpxx, 1);
                        }
                        else
                        {
                            repairReport.RepairInvoice(fpxx, 0);
                        }
                        invDetail = null;
                    }
                }
                while (!IsStopXiufu);
                repairReport.SaveToDB();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
            finally
            {
                this.loger.Info("[发票修复]：完成发票修复过程");
            }
            return (StockCount >= 0);
        }

        private bool GetCardFpDataOldVersion(int Month, long InvCount, RepairReport repairReport)
        {
            try
            {
                InvDetail invDetail = null;
                Fpxx fpxx = null;
                int num = 0;
                int num2 = 1;
                double num3 = 7000.0 / ((double) InvCount);
                double num4 = 1.0;
                Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this._iYear.ToString() + this._iMonth.ToString("D2"));
                GC.Collect();
                base.TaxCardInstance.RepairOpen(Month);
                this.loger.Info("[发票修复]：" + Month.ToString() + "月，【发票张数】：" + InvCount.ToString());
                for (int i = 0; (i < InvCount) && !IsStopXiufu; i++)
                {
                    TextType type;
                    type.visible = true;
                    num = i + 1;
                    type.Text = "第 " + num.ToString() + "张/共" + InvCount.ToString() + "张";
                    this.SetCurrentFPTextThread(type);
                    num4 = num3 * num2;
                    if (num4 >= 1.0)
                    {
                        num2 = 1;
                        this.ProcessStartThread((int) Math.Round(num4, 0));
                    }
                    else
                    {
                        num2++;
                    }
                    invDetail = null;
                    fpxx = null;
                    invDetail = base.TaxCardInstance.GetInvDetail((long) i);
                    this.loger.Info("[发票修复]：完成读取第" + i.ToString() + "张发票");
                    if (base.TaxCardInstance.RetCode != 0)
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                        this.loger.Error(string.Concat(new object[] { "[发票修复]：读取金税设备错误，错误号：", base.TaxCardInstance.RetCode, "错误信息：", base.TaxCardInstance.ErrCode }));
                        return false;
                    }
                    fpxx = new Fpxx();
                    fpxx.RepairInv(invDetail, repairReport.currentPeriodCount[invDetail.InvType]);
                    fpxx.dybz = true;
                    this.loger.Info("[发票修复]：结束调用对象构造函数");
                    string key = string.Concat(new object[] { Aisino.Fwkp.Fpkj.Common.Tool.PareFpType(fpxx.fplx), fpxx.fpdm, "_", Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fpxx.fphm) });
                    if (!repairReport.HashFPFromTaxCard.ContainsKey(key))
                    {
                        repairReport.HashFPFromTaxCard.Add(key, i);
                    }
                    else
                    {
                        this.loger.Error("[发票修复]：发票主键重复: " + key);
                    }
                    if (this.PerformantLevel == 1)
                    {
                        key = fpxx.fpdm + "_" + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fpxx.fphm);
                        if (((this.FpHashTable == null) || (this.FpHashTable.Count == 0)) || (this.FpHashTable[key] == null))
                        {
                            repairReport.modelXXFP = null;
                        }
                        else
                        {
                            repairReport.modelXXFP = this.FpHashTable[key] as Fpxx;
                        }
                        repairReport.RepairInvoice(fpxx, 1);
                    }
                    else
                    {
                        repairReport.RepairInvoice(fpxx, 0);
                    }
                }
                repairReport.SaveToDB();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
            finally
            {
                this.loger.Info("[发票修复]：完成发票对象对比过程");
                base.TaxCardInstance.RepairClose();
            }
            if (Month != base.TaxCardInstance.GetCardClock().Month)
            {
                return (InvCount >= 0L);
            }
            return true;
        }

        private List<int> GetTaxCardMonths(int year)
        {
            List<int> monthStatPeriod = base.TaxCardInstance.GetMonthStatPeriod(year);
            monthStatPeriod.Reverse();
            return monthStatPeriod;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FpxfMain));
            this.btStopXiufu = new AisinoBTN();
            this.label_Tip = new Label();
            this.label_Title = new Label();
            this.fpxf_progressBar = new ProgressBar();
            this.lab_Current = new Label();
            base.SuspendLayout();
            this.btStopXiufu.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btStopXiufu.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btStopXiufu.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btStopXiufu.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btStopXiufu.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btStopXiufu.Location = new Point(0xc3, 0x72);
            this.btStopXiufu.Name = "btStopXiufu";
            this.btStopXiufu.Size = new Size(90, 30);
            this.btStopXiufu.TabIndex = 6;
            this.btStopXiufu.Text = "停止修复";
            this.btStopXiufu.UseVisualStyleBackColor = true;
            this.btStopXiufu.Click += new EventHandler(this.btStopXiufu_Click);
            this.label_Tip.AutoSize = true;
            this.label_Tip.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label_Tip.Location = new Point(0x146, 0x59);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new Size(0x6b, 20);
            this.label_Tip.TabIndex = 5;
            this.label_Tip.Text = "请等待任务完成";
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label_Title.Location = new Point(20, 0x20);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new Size(0x95, 20);
            this.label_Title.TabIndex = 4;
            this.label_Title.Text = "正在进行发票修复准备";
            this.fpxf_progressBar.Location = new Point(14, 0x38);
            this.fpxf_progressBar.Name = "fpxf_progressBar";
            this.fpxf_progressBar.Size = new Size(0x1c9, 0x1d);
            this.fpxf_progressBar.TabIndex = 3;
            this.lab_Current.AutoSize = true;
            this.lab_Current.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lab_Current.Location = new Point(0x146, 0x21);
            this.lab_Current.Name = "lab_Current";
            this.lab_Current.Size = new Size(0x34, 20);
            this.lab_Current.TabIndex = 4;
            this.lab_Current.Text = "第 x 张";
            this.lab_Current.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x1e1, 0xa2);
            base.Controls.Add(this.btStopXiufu);
            base.Controls.Add(this.label_Tip);
            base.Controls.Add(this.lab_Current);
            base.Controls.Add(this.label_Title);
            base.Controls.Add(this.fpxf_progressBar);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FpxfMain";
            base.TabText = "发票修复";
            this.Text = "发票修复";
            base.Load += new EventHandler(this.FpxfMain_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InitializeFPProgressBar()
        {
            int x = 300;
            int y = 100;
            Rectangle virtualScreen = SystemInformation.VirtualScreen;
            x = (virtualScreen.Width - base.Width) / 2;
            if (x <= 0)
            {
                x = 300;
            }
            y = (virtualScreen.Height - base.Height) / 2;
            if (y <= 0)
            {
                y = 100;
            }
            base.Location = new Point(x, y);
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.ControlBox = false;
        }

        private void InitProgressBar()
        {
            this.fpxf_progressBar.Visible = true;
            this.fpxf_progressBar.Minimum = 1;
            this.fpxf_progressBar.Maximum = 0x2710;
            this.fpxf_progressBar.Value = 1;
            this.fpxf_progressBar.Step = 1;
        }

        private void InvokePerformStep(object step)
        {
            try
            {
                int num = int.Parse(step.ToString());
                for (int i = 0; i < num; i++)
                {
                    if ((this.fpxf_progressBar.Value + 1) > this.fpxf_progressBar.Maximum)
                    {
                        this.fpxf_progressBar.Value = this.fpxf_progressBar.Maximum;
                    }
                    else
                    {
                        this.fpxf_progressBar.Value++;
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[InvokePerformStep函数异常]" + exception.Message);
            }
        }

        private void InvokeSetCurrentFPText(object obj)
        {
            TextType type = (TextType) obj;
            if (type.visible != this.lab_Current.Visible)
            {
                this.lab_Current.Visible = type.visible;
            }
            this.lab_Current.Text = type.Text;
        }

        private void PerformStep(object step)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    PerformStepHandle method = new PerformStepHandle(this.InvokePerformStep);
                    base.BeginInvoke(method, new object[] { step });
                }
                else
                {
                    this.InvokePerformStep(step);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[PerformStep]" + exception.Message);
            }
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                this.PerformStep(obj);
            }
            catch (Exception exception)
            {
                this.loger.Error("[ProccessBarShow]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            this.ProccessBarShow(value);
        }

        private void ResetProcessBar(object step)
        {
            this.fpxf_progressBar.Value = 1;
            this.lab_Current.Text = " ";
            this.label_Title.Text = "正在进行发票修复准备";
            this.label_Tip.Text = "请等待任务完成";
            this.btStopXiufu.Text = "停止修复";
            this.fpxf_progressBar.Refresh();
            this.label_Title.Refresh();
            this.label_Tip.Refresh();
            this.lab_Current.Refresh();
            this.btStopXiufu.Refresh();
        }

        private void ResetProcessBarThread(object step)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    ResetProcessBarHandle method = new ResetProcessBarHandle(this.ResetProcessBar);
                    base.BeginInvoke(method, new object[] { step });
                }
                else
                {
                    this.ResetProcessBar(step);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[ResetProcessBarThread函数]" + exception.Message);
            }
        }

        public void RunMain(bool showMsg)
        {
            if (!base.TaxCardInstance.IsLargeStorage)
            {
                this.loger.Info("[发票修复]：启用按张修复流程");
                this.RunOldVersion(showMsg);
            }
            else
            {
                this.loger.Info("[发票修复]：启用按块修复流程");
                this.RunNewVersion(showMsg);
            }
        }

        public void RunNewVersion(bool bTanChuMsg)
        {
            RepairReport repairReport = new RepairReport();
            try
            {
                TextType type;
                UpLoadCheckState.SetFpxfState(true);
                Thread.Sleep(100);
                IsStopXiufu = false;
                int stockCount = 0;
                type.visible = true;
                type.Text = "";
                this.SetCurrentFPTextThread(type);
                this.ResetProcessBarThread(1);
                RepairReport.AllDelete = false;
                RepairReport.AllKeep = false;
                repairReport._iMonth = this._iMonth;
                repairReport._iYear = this._iYear;
                if (this._iMonth == 0)
                {
                    List<int> taxCardMonths = this.GetTaxCardMonths(this._iYear);
                    if ((taxCardMonths == null) || (taxCardMonths.Count <= 0))
                    {
                        this.loger.Error("[发票修复]：GetMonthStatPeriod函数返回的MonthList为空或者月份为0");
                        this.ProcessStartThread(10 * this.step);
                        this.SetVisibleThread(false);
                        MessageManager.ShowMsgBox("FPXF-000004");
                        return;
                    }
                    foreach (int num2 in taxCardMonths)
                    {
                        this.SetVisibleThread(true);
                        this.ResetProcessBarThread(1);
                        this.SetTipThread("正在进行修复准备工作", "请等待任务完成");
                        this.ProcessStartThread(this.step * 6);
                        this._iMonth = num2;
                        repairReport._iMonth = this._iMonth;
                        repairReport._iYear = this._iYear;
                        repairReport._iPeriod = 1;
                        repairReport.InitSet();
                        this.loger.Info("[发票修复]：开始读取设备中" + this._iMonth.ToString() + "月份发票块数");
                        stockCount = base.TaxCardInstance.GetInvStockCount(this._iYear, this._iMonth);
                        this.loger.Info("[发票修复]：完成读取设备,发票块数数目:" + stockCount.ToString());
                        if ((base.TaxCardInstance.RetCode != 0) || (stockCount < 0))
                        {
                            this.loger.Error("[发票修复]：TaxCardInstance.RetCode返回错误或者发票张数小于0, 错误代码：" + base.TaxCardInstance.RetCode.ToString());
                            this.ProcessStartThread(8 * this.step);
                            this.SetVisibleThread(false);
                            return;
                        }
                        if ((base.TaxCardInstance.RetCode == 0) && (stockCount == 0))
                        {
                            if (!this.GetCardFpDataNewVersion(stockCount, repairReport))
                            {
                                this.loger.Error("[发票修复]：读取金税盘数据有误");
                                this.ProcessStartThread(8 * this.step);
                                this.SetVisibleThread(false);
                                MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                                return;
                            }
                            this.ResetProcessBarThread(1);
                            this.SetTipThread("正在校验发票记录", "请等待任务完成");
                            this.ProcessStartThread(this.step * 4);
                            repairReport.DeleteInoviceFromDB(true);
                            this.SetTipThread("正在完成修复", "请等待任务完成");
                            this.ProcessStartThread(this.step * 3);
                            this.SetVisibleThread(false);
                        }
                        else
                        {
                            this.SetTipThread("正在修复" + this._iMonth + "月份数据", "请等待任务完成");
                            this.ProcessStartThread(this.step);
                            if ((stockCount > 0) && !this.GetCardFpDataNewVersion(stockCount, repairReport))
                            {
                                this.loger.Error("[发票修复]：读取金税盘数据有误");
                                this.ProcessStartThread(8 * this.step);
                                this.SetVisibleThread(false);
                                MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                                return;
                            }
                            this.ResetProcessBarThread(1);
                            this.ProcessStartThread(6 * this.step);
                            this.SetTipThread("正在校验发票记录", "请等待任务完成");
                            repairReport.DeleteInoviceFromDB(true);
                            this.SetTipThread("正在完成修复", "请等待任务完成");
                            this.ProcessStartThread(3 * this.step);
                            this.SetVisibleThread(false);
                        }
                    }
                    this.SetVisibleThread(false);
                    bTanChuMsg = true;
                    this.loger.Info("[发票修复]：调用修复函数结束");
                }
                else
                {
                    this.ProcessStartThread(6 * this.step);
                    this.SetTipThread("正在进行修复准备工作", "请等待任务完成");
                    repairReport.InitSet();
                    this.loger.Info("[发票修复]：开始读取设备中" + this._iMonth.ToString() + "月份发票块数");
                    stockCount = base.TaxCardInstance.GetInvStockCount(this._iYear, this._iMonth);
                    this.loger.Info("[发票修复]：完成读取设备,发票块数数目:" + stockCount.ToString());
                    if ((base.TaxCardInstance.RetCode != 0) || (stockCount < 0))
                    {
                        this.loger.Error("TaxCardInstance.RetCode返回错误或者发票张数小于0, 错误代码：" + base.TaxCardInstance.RetCode.ToString());
                        this.ProcessStartThread(8 * this.step);
                        this.SetVisibleThread(false);
                        MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                        return;
                    }
                    if ((base.TaxCardInstance.RetCode == 0) && (stockCount == 0))
                    {
                        this.SetTipThread("正在校验发票记录", "请等待任务完成");
                        this.ProcessStartThread(this.step * 4);
                        repairReport.DeleteInoviceFromDB(true);
                        this.SetTipThread("正在完成修复", "请等待任务完成");
                        this.ProcessStartThread(this.step * 3);
                        this.SetVisibleThread(false);
                        bTanChuMsg = true;
                    }
                    else
                    {
                        this.SetTipThread("正在修复" + this._iMonth + "月份数据", "请等待任务完成");
                        if (!this.GetCardFpDataNewVersion(stockCount, repairReport))
                        {
                            this.loger.Error("[发票修复]：读取金税盘数据有误");
                            this.ProcessStartThread(8 * this.step);
                            this.SetVisibleThread(false);
                            MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                            return;
                        }
                        this.SetTipThread("正在校验发票记录", "请等待任务完成");
                        this.ResetProcessBarThread(1);
                        this.ProcessStartThread(6 * this.step);
                        repairReport.DeleteInoviceFromDB(true);
                        this.SetTipThread("正在完成修复", "请等待任务完成");
                        this.ProcessStartThread(3 * this.step);
                    }
                    this.SetVisibleThread(false);
                }
                if (bTanChuMsg)
                {
                    MessageManager.ShowMsgBox("FPXF-000004");
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[发票修复]：" + exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
            }
            finally
            {
                UpLoadCheckState.SetFpxfState(false);
                if (repairReport != null)
                {
                    repairReport.DisposeForm();
                    repairReport.Dispose();
                    repairReport = null;
                }
                this.AutoEvent.Set();
            }
        }

        public void RunOldVersion(bool bTanChuMsg)
        {
            RepairReport repairReport = new RepairReport();
            try
            {
                TextType type;
                UpLoadCheckState.SetFpxfState(true);
                Thread.Sleep(100);
                IsStopXiufu = false;
                type.visible = true;
                type.Text = "";
                this.SetCurrentFPTextThread(type);
                this.ResetProcessBarThread(1);
                RepairReport.AllDelete = false;
                RepairReport.AllKeep = false;
                repairReport._iMonth = this._iMonth;
                repairReport._iYear = this._iYear;
                if (this._iMonth == 0)
                {
                    long invCount = 0L;
                    List<int> taxCardMonths = this.GetTaxCardMonths(this._iYear);
                    if ((taxCardMonths == null) || (taxCardMonths.Count <= 0))
                    {
                        this.loger.Info("[发票修复]：GetMonthStatPeriod函数返回的MonthList为空或者月份为0");
                        this.ProcessStartThread(10 * this.step);
                        this.SetVisibleThread(false);
                        MessageManager.ShowMsgBox("FPXF-000004");
                        return;
                    }
                    foreach (int num2 in taxCardMonths)
                    {
                        this.SetVisibleThread(true);
                        this.ResetProcessBarThread(1);
                        this.SetTipThread("正在进行修复准备工作", "请等待任务完成");
                        this.ProcessStartThread(this.step * 2);
                        this._iMonth = num2;
                        repairReport._iMonth = this._iMonth;
                        repairReport._iYear = this._iYear;
                        repairReport.InitSet();
                        this.loger.Info("[发票修复]：开始读取设备中" + this._iMonth.ToString() + "月份发票数据");
                        invCount = base.TaxCardInstance.GetInvCount(this._iYear, this._iMonth);
                        this.loger.Info("[发票修复]：完成读取设备中" + this._iMonth.ToString() + "月份发票数据,发票数目:" + invCount.ToString());
                        if ((base.TaxCardInstance.RetCode != 0) || (invCount < 0L))
                        {
                            this.loger.Info("[发票修复]：TaxCardInstance.RetCode返回错误或者发票张数小于0, 错误代码：" + base.TaxCardInstance.RetCode.ToString());
                            this.ProcessStartThread(8 * this.step);
                            this.SetVisibleThread(false);
                            MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                            return;
                        }
                        if ((base.TaxCardInstance.RetCode == 0) && (invCount == 0L))
                        {
                            if (!this.GetCardFpDataOldVersion(this._iMonth, invCount, repairReport))
                            {
                                this.loger.Error("[发票修复]：读取金税盘数据有误");
                                this.ProcessStartThread(8 * this.step);
                                this.SetVisibleThread(false);
                                MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                                return;
                            }
                            this.SetTipThread("正在校验发票记录", "请等待任务完成");
                            this.ProcessStartThread(this.step * 4);
                            repairReport.DeleteInoviceFromDB(true);
                            this.SetTipThread("正在完成修复", "请等待任务完成");
                            this.ProcessStartThread(this.step * 4);
                            this.SetVisibleThread(false);
                        }
                        else
                        {
                            this.SetTipThread("正在修复" + this._iMonth + "月份数据", "请等待任务完成");
                            this.ProcessStartThread(this.step);
                            if ((invCount > 0L) && !this.GetCardFpDataOldVersion(this._iMonth, invCount, repairReport))
                            {
                                this.loger.Error("[发票修复]：读取金税盘数据有误");
                                this.ProcessStartThread(8 * this.step);
                                this.SetVisibleThread(false);
                                MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                                return;
                            }
                            this.SetTipThread("正在校验发票记录", "请等待任务完成");
                            repairReport.DeleteInoviceFromDB(true);
                            this.SetTipThread("正在完成修复", "请等待任务完成");
                            this.ProcessStartThread(this.step);
                        }
                    }
                    this.SetVisibleThread(false);
                    bTanChuMsg = true;
                    this.loger.Info("[发票修复]：调用修复函数结束");
                }
                else
                {
                    this.ProcessStartThread(2 * this.step);
                    repairReport.InitSet();
                    this.SetTipThread("正在进行修复准备工作", "请等待任务完成");
                    this.loger.Info("[发票修复]：开始读取设备中" + this._iYear.ToString() + "年" + this._iMonth.ToString() + "月份发票数据");
                    long num3 = base.TaxCardInstance.GetInvCount(this._iYear, this._iMonth);
                    this.loger.Info("[发票修复]：完成读取设备中" + this._iMonth.ToString() + "月份发票数据,发票张数：" + num3.ToString());
                    if ((base.TaxCardInstance.RetCode != 0) || (num3 < 0L))
                    {
                        this.loger.Info("[发票修复]： 错误代码：" + base.TaxCardInstance.RetCode.ToString());
                        this.ProcessStartThread(8 * this.step);
                        this.SetVisibleThread(false);
                        MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                        return;
                    }
                    if ((base.TaxCardInstance.RetCode == 0) && (num3 == 0L))
                    {
                        this.SetTipThread("正在校验发票记录", "请等待任务完成");
                        this.ProcessStartThread(this.step * 4);
                        repairReport.DeleteInoviceFromDB(true);
                        this.SetTipThread("正在完成修复", "请等待任务完成");
                        this.ProcessStartThread(this.step * 4);
                        this.SetVisibleThread(false);
                        bTanChuMsg = true;
                    }
                    else
                    {
                        this.SetTipThread("正在修复" + this._iMonth + "月份数据", "请等待任务完成");
                        if (!this.GetCardFpDataOldVersion(this._iMonth, num3, repairReport))
                        {
                            this.loger.Error("[发票修复]：读取金税盘数据有误");
                            this.ProcessStartThread(8 * this.step);
                            this.SetVisibleThread(false);
                            MessageManager.ShowMsgBox("FPXF-000005", "提示", new string[] { Convert.ToString(this._iMonth) });
                            return;
                        }
                        this.SetTipThread("正在校验发票记录", "请等待任务完成");
                        repairReport.DeleteInoviceFromDB(true);
                        this.SetTipThread("正在完成修复", "请等待任务完成");
                        this.ProcessStartThread(this.step);
                    }
                    this.SetVisibleThread(false);
                }
                if (bTanChuMsg)
                {
                    MessageManager.ShowMsgBox("FPXF-000004");
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[发票修复]：" + exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
            }
            finally
            {
                UpLoadCheckState.SetFpxfState(false);
                if (repairReport != null)
                {
                    repairReport.DisposeForm();
                    repairReport.Dispose();
                    repairReport = null;
                }
                this.AutoEvent.Set();
            }
        }

        private void SetCurrentFPTextThread(object obj)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    SetTipHandle method = new SetTipHandle(this.InvokeSetCurrentFPText);
                    base.BeginInvoke(method, new object[] { obj });
                }
                else
                {
                    this.InvokeSetCurrentFPText(obj);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[PerformStep]" + exception.Message);
            }
        }

        private void SetTip(object title, object tip)
        {
            try
            {
                if ((tip != null) && (title != null))
                {
                    this.label_Tip.Text = tip.ToString();
                    this.label_Title.Text = title.ToString();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("SetTip函数异常" + exception.Message);
                throw;
            }
        }

        public void SetTipThread(object title, object tip)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    SetLableHandle method = new SetLableHandle(this.SetTip);
                    base.BeginInvoke(method, new object[] { title, tip });
                }
                else
                {
                    this.SetTip(title, tip);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("SetTipThread函数异常" + exception.Message);
                throw;
            }
        }

        private void SetVisible(object visible)
        {
            if (this == null)
            {
                this.loger.Error("[SetVisible函数异常],对象为空");
            }
            else
            {
                bool flag = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToBool(visible.ToString());
                base.Visible = flag;
                this.lab_Current.Visible = flag;
                this.label_Tip.Visible = flag;
                this.label_Title.Visible = flag;
                this.btStopXiufu.Visible = flag;
            }
        }

        private void SetVisibleThread(object visible)
        {
            try
            {
                if (this == null)
                {
                    this.loger.Error("[SetVisibleThread函数异常]：this对象为空");
                }
                else if (base.InvokeRequired)
                {
                    SetVisibleHandle method = new SetVisibleHandle(this.SetVisible);
                    base.BeginInvoke(method, new object[] { visible });
                }
                else
                {
                    this.SetVisible(visible);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[SetVisibleThread函数异常]" + exception.Message);
            }
        }

        public void SingleRunMain(string fpdm, string fphm)
        {
            try
            {
                UpLoadCheckState.SetFpxfState(true);
                this.ResetProcessBarThread(1);
                this.ProcessStartThread(6 * this.step);
                if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
                {
                    this.loger.Debug("当前软件版本：" + base.TaxCardInstance.SoftVersion + "不支持单张修复功能");
                    this.SetVisibleThread(false);
                }
                else
                {
                    InvDetail detail = base.TaxCardInstance.QueryInvInfo(fpdm, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fphm), "0", "0", DateTime.Now);
                    if (base.TaxCardInstance.RetCode != 0)
                    {
                        this.ProcessStartThread(3 * this.step);
                        this.SetVisibleThread(false);
                        MessageManager.ShowMsgBox(base.TaxCardInstance.ErrCode);
                    }
                    else
                    {
                        Fpxx fpxx = null;
                        if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
                        {
                            fpxx = new Fpxx();
                            fpxx.RepairInv(detail, -1);
                        }
                        else
                        {
                            fpxx = Fpxx.DeSeriealize_Linux(ToolUtil.FromBase64String(detail.OldInvNo));
                        }
                        if ((detail.TypeCode == "") && (detail.InvNo == 0))
                        {
                            this.ProcessStartThread(3 * this.step);
                            this.SetVisibleThread(false);
                            MessageManager.ShowMsgBox("FPCX-000031", new string[] { fpdm, ShareMethods.FPHMTo8Wei(fphm) });
                        }
                        else
                        {
                            this.ProcessStartThread(2 * this.step);
                            if (fpxx != null)
                            {
                                XXFP xxfp = new XXFP(false);
                                List<Fpxx> fpList = new List<Fpxx> {
                                    fpxx
                                };
                                xxfp.SaveXxfp(fpList);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug(exception.ToString());
            }
            finally
            {
                this.SetVisibleThread(false);
                MessageManager.ShowMsgBox("FPXF-000004");
                UpLoadCheckState.SetFpxfState(false);
                this.AutoEvent.Set();
            }
        }

        public void StartThread(object args)
        {
            if ((this.xfThrad == null) || !this.xfThrad.IsAlive)
            {
                if (((this.xfThrad == null) || (this.xfThrad.ThreadState == ThreadState.Aborted)) || (this.xfThrad.ThreadState == ThreadState.Stopped))
                {
                    this.xfThrad = new Thread(new ParameterizedThreadStart(this.ThreadContent));
                    this.xfThrad.SetApartmentState(ApartmentState.MTA);
                    this.xfThrad.IsBackground = true;
                    this.xfThrad.Name = "fpxf";
                }
                this.xfThrad.Start(args);
            }
        }

        private void ThreadContent(object obj)
        {
            FPXFArgs args = (FPXFArgs) obj;
            if (this.IsSingle)
            {
                this.SingleRunMain(args.XFfpdm, args.XFfphm);
            }
            else
            {
                this.RunMain(args.IsShow);
            }
        }

        public bool IsSingle
        {
            get
            {
                return this._isSingle;
            }
            set
            {
                this._isSingle = value;
            }
        }

        public bool USBKeyExist
        {
            get
            {
                return this._bUSBKeyExist;
            }
            set
            {
                this._bUSBKeyExist = value;
            }
        }

        public string XFFpdm
        {
            get
            {
                return this._xffpdm;
            }
            set
            {
                this._xffpdm = value;
            }
        }

        public string XFFPHM
        {
            get
            {
                return this._xffphm;
            }
            set
            {
                this._xffphm = value;
            }
        }

        public bool XFIsShowDialog
        {
            get
            {
                return this._bShowDialog;
            }
            set
            {
                this._bShowDialog = value;
            }
        }

        public int XFMonth
        {
            get
            {
                return this._iMonth;
            }
            set
            {
                this._iMonth = value;
            }
        }

        public int XFYear
        {
            get
            {
                return this._iYear;
            }
            set
            {
                this._iYear = value;
            }
        }

        private delegate void DisposeFormHandle(object form);

        [StructLayout(LayoutKind.Sequential)]
        public struct FPXFArgs
        {
            public bool IsShow;
            public int XFMonth;
            public int XFYear;
            public string XFfpdm;
            public string XFfphm;
        }

        private delegate void PerformStepHandle(object step);

        private delegate void ResetProcessBarHandle(object step);

        private delegate void SetLableHandle(object title, object tip);

        private delegate void SetTipHandle(object step);

        private delegate void SetVisibleHandle(object visible);

        [StructLayout(LayoutKind.Sequential)]
        private struct TextType
        {
            public bool visible;
            public string Text;
        }
    }
}

