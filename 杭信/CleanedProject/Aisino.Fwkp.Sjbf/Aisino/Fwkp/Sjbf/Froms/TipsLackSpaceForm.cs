namespace Aisino.Fwkp.Sjbf.Froms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class TipsLackSpaceForm : BaseForm
    {
        private ILog _Loger = LogUtil.GetLogger<TipsLackSpaceForm>();
        private AisinoBTN btn_JiXu;
        private AisinoBTN btn_Quit;
        private IContainer components;
        private AisinoLBL label1;
        public string strDestPathCopy = string.Empty;
        public string strSrcPathCopy = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;

        public TipsLackSpaceForm()
        {
            try
            {
                this.Initialize();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_JiXu_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.OK;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void btn_Quit_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Cancel;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.btn_JiXu = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_JiXu");
            this.btn_Quit = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Quit");
            this.btn_JiXu.Click += new EventHandler(this.btn_JiXu_Click);
            this.btn_Quit.Click += new EventHandler(this.btn_Quit_Click);
            this.btn_Quit.Text = "取消";
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(400, 0xc3);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Sjbf.Froms.TipsLackSpaceForm\Aisino.Fwkp.Sjbf.Froms.TipsLackSpaceForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 0xc3);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TipsLackSpaceForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "提示";
            base.ResumeLayout(false);
        }

        public bool Run(TipsType tipsType)
        {
            bool flag;
            try
            {
                long length;
                int num2;
                string str;
                long availableFreeSpace;
                long totalSize;
                switch (tipsType)
                {
                    case TipsType.TipsThatSamePath:
                    {
                        string startupPath = Application.StartupPath;
                        string str7 = this.strDestPathCopy.Substring(0, 1);
                        string str8 = string.Empty;
                        if (!str7.Equals("C") && !str7.Equals("c"))
                        {
                            goto Label_0295;
                        }
                        str8 = "数据备份路径:" + this.strDestPathCopy + "\n";
                        this.label1.Text = string.Empty;
                        this.label1.Text = this.label1.Text + str8;
                        this.label1.Text = this.label1.Text + "请不要把数据备份路径设置在系统盘符(C:)，以防数据丢失，\n是否继续?";
                        return true;
                    }
                    case TipsType.TipsLackSpace:
                    {
                        FileInfo info = new FileInfo(this.strSrcPathCopy);
                        length = info.Length;
                        num2 = 0x400;
                        str = this.strDestPathCopy.Substring(0, 2);
                        DriveInfo info2 = new DriveInfo(str);
                        availableFreeSpace = info2.AvailableFreeSpace;
                        totalSize = info2.TotalSize;
                        if (length >= (availableFreeSpace / 5L))
                        {
                            break;
                        }
                        base.DialogResult = DialogResult.OK;
                        return false;
                    }
                    default:
                        throw new Exception("TipsLackSpaceForm.Run传参错误");
                }
                long num5 = availableFreeSpace / ((long) num2);
                totalSize /= (long) num2;
                long num6 = num5;
                string str2 = "KB";
                if (num5 >= num2)
                {
                    long num7 = num5 / ((long) num2);
                    totalSize /= (long) num2;
                    num6 = num7;
                    str2 = "MB";
                    if (num7 >= num2)
                    {
                        long num8 = num7 / ((long) num2);
                        totalSize /= (long) num2;
                        num6 = num8;
                        str2 = "GB";
                        if (num8 >= num2)
                        {
                            num6 = num8 / ((long) num2);
                            totalSize /= (long) num2;
                            str2 = "TB";
                        }
                    }
                }
                string str3 = "数据库备份路径：" + this.strDestPathCopy + "\n";
                string str4 = "数据库备份路径所在盘符：" + str + " 总大小：" + totalSize.ToString() + str2 + " 可用空间：" + num6.ToString() + str2 + "\n";
                string str5 = "数据库文件大小：" + ((length / ((long) (num2 * num2)))).ToString() + "MB\n\n  ";
                string str6 = "数据库文件大小已超过:" + str + "可用空间20%，是否继续备份数据?";
                this.label1.Text = string.Empty;
                this.label1.Text = this.label1.Text + str3;
                this.label1.Text = this.label1.Text + str4;
                this.label1.Text = this.label1.Text + str5;
                this.label1.Text = this.label1.Text + str6;
                return true;
            Label_0295:
                flag = false;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                flag = false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                flag = false;
            }
            return flag;
        }

        public enum TipsType
        {
            TipsThatSamePath,
            TipsLackSpace
        }
    }
}

