namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class InvDkTypeSetForm : BaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOk;
        private IContainer components;
        private AisinoBTN fileCA;
        private AisinoLBL lblCA;
        private ILog loger = LogUtil.GetLogger<InvDkTypeSetForm>();
        private static InvDkTypeSetForm m_Instance;
        private string pfxFileName = "";
        private AisinoTXT txtPassword1;
        private AisinoTXT txtPassword2;
        private XmlComponentLoader xmlComponentLoader1;

        public InvDkTypeSetForm()
        {
            this.InitializeComponent();
            this.btnOk = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOk");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.fileCA = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("fileCA");
            this.lblCA = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblCA");
            this.txtPassword1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPassword1");
            this.txtPassword2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtPassword2");
            this.txtPassword1.PasswordChar = '*';
            this.txtPassword2.PasswordChar = '*';
            this.fileCA.Text = "证书读入";
            this.fileCA.Size = new Size(80, 30);
            this.lblCA.Text = "";
            this.lblCA.ForeColor = Color.Blue;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.fileCA.Click += new EventHandler(this.btnCAOk_Click);
            base.Size = new Size(0x1ac, 0x184);
            string str = PropertyUtil.GetValue("SWDK_CA_FILENAME");
            string path = AppDomain.CurrentDomain.BaseDirectory + "server.pfx";
            if ((str.Trim().Length > 0) && File.Exists(path))
            {
                this.lblCA.Text = str;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCAOk_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "证书文件(*.pfx)|*.pfx",
                Title = "请选择证书文件",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                this.pfxFileName = dialog.FileName.Trim();
                string path = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\server.pfx";
                if (dialog.FileName.Trim().Length > 0)
                {
                    this.lblCA.Text = "server.pfx";
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (!Directory.Exists(PropertyUtil.GetValue("MAIN_PATH") + @"\bin\"))
                {
                    MessageManager.ShowMsgBox("SWDK-0058");
                }
                else if (!File.Exists(this.pfxFileName))
                {
                    MessageManager.ShowMsgBox("SWDK-0059");
                }
                else
                {
                    File.Copy(this.pfxFileName, path);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((this.txtPassword1.Text.Trim().Length == 0) || (this.txtPassword2.Text.Trim().Length == 0))
            {
                MessageManager.ShowMsgBox("SWDK-0062");
            }
            else if (this.txtPassword1.Text != this.txtPassword2.Text)
            {
                MessageManager.ShowMsgBox("SWDK-0055");
            }
            else if (this.pfxFileName.Length == 0)
            {
                MessageManager.ShowMsgBox("SWDK-0056");
            }
            else
            {
                try
                {
                    if (!this.checkCaPassword(this.txtPassword1.Text.Trim(), this.pfxFileName))
                    {
                        MessageManager.ShowMsgBox("SWDK-0060");
                    }
                    else
                    {
                        PropertyUtil.SetValue("SWDK_CA_PWD", this.txtPassword1.Text.Trim());
                        PropertyUtil.SetValue("SWDK_CA_FILENAME", "server.pfx");
                        PropertyUtil.Save();
                        base.DialogResult = DialogResult.OK;
                        MessageManager.ShowMsgBox("SWDK-0064");
                        base.Close();
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error("复制证书文件异常：" + exception.ToString());
                    string[] textArray1 = new string[] { exception.ToString() };
                    MessageManager.ShowMsgBox("SWDK-0057", textArray1);
                }
            }
        }

        private bool checkCaPassword(string password, string CA)
        {
            bool flag = true;
            if ((CA.Length == 0) || (password.Length == 0))
            {
                return false;
            }
            try
            {
                string path = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\CAConsole.exe";
                string str2 = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\server.pfx";
                string currentDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(PropertyUtil.GetValue("MAIN_PATH") + @"\bin\");
                this.loger.Info("GetCurrentDirectory ：" + currentDirectory);
                this.loger.Info("m_strPfxPath ：" + str2);
                if (File.Exists("caResult.dat"))
                {
                    File.Delete("caResult.dat");
                }
                if (File.Exists(path))
                {
                    Process process = new Process {
                        StartInfo = { FileName = path, Arguments = password, UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden }
                    };
                    process.Start();
                    process.WaitForExit();
                    Thread.Sleep(0x3e8);
                    if (process.HasExited)
                    {
                        if (File.Exists("caResult.dat"))
                        {
                            StreamReader reader = new StreamReader(File.Open("caResult.dat", FileMode.Open), ToolUtil.GetEncoding());
                            string str4 = "";
                            while ((str4 = reader.ReadLine()) != null)
                            {
                                this.loger.Info(" caResult.dat Line ：" + str4);
                                if (str4.IndexOf("return=0") == -1)
                                {
                                    flag = false;
                                }
                            }
                        }
                        else
                        {
                            string[] textArray1 = new string[] { "caResult.dat文件未找到" };
                            MessageManager.ShowMsgBox("SWDK-0061", textArray1);
                            flag = false;
                        }
                    }
                    else if (!process.HasExited)
                    {
                        process.Kill();
                        flag = false;
                    }
                }
                Directory.SetCurrentDirectory(currentDirectory);
            }
            catch (Exception exception)
            {
                this.loger.Error("校验证书密码发生异常：" + exception.ToString());
                string[] textArray2 = new string[] { exception.ToString() };
                MessageManager.ShowMsgBox("SWDK-0061", textArray2);
                return false;
            }
            DigitalEnvelop.DigEnvClose();
            return flag;
        }

        private bool checkCaPassword1(string password, string CA)
        {
            bool flag = true;
            if ((CA.Length == 0) || (password.Length == 0))
            {
                return false;
            }
            try
            {
                if (DigitalEnvelop.DigEnvInit(false, true, true) == 0)
                {
                    try
                    {
                        string str = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\server.pfx";
                        string currentDirectory = Directory.GetCurrentDirectory();
                        Directory.SetCurrentDirectory(PropertyUtil.GetValue("MAIN_PATH") + @"\bin\");
                        int num = DigitalEnvelop.SetCaCertAndCrlByPfx(CA, password, "");
                        this.loger.Info("GetCurrentDirectory ：" + currentDirectory);
                        this.loger.Info("m_strPfxPath ：" + str);
                        this.loger.Info(" DigitalEnvelop.SetCaCertAndCrlByPfx ret ：" + num.ToString());
                        Directory.SetCurrentDirectory(currentDirectory);
                        if ((num != 0) && (num == 2))
                        {
                            flag = false;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.loger.Error("校验证书密码发生异常：" + exception.ToString());
                        string[] textArray1 = new string[] { exception.ToString() };
                        MessageManager.ShowMsgBox("SWDK-0061", textArray1);
                        return false;
                    }
                }
                DigitalEnvelop.DigEnvClose();
            }
            catch (Exception exception2)
            {
                this.loger.Error("校验证书密码发生异常：" + exception2.ToString());
                string[] textArray2 = new string[] { exception2.ToString() };
                MessageManager.ShowMsgBox("SWDK-0061", textArray2);
                return false;
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal static InvDkTypeSetForm GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new InvDkTypeSetForm();
            }
            return m_Instance;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InvDkTypeSetForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x162, 0x110);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.SWDK.InvDkTypeSetForm\Aisino.Fwkp.Fpkj.Form.SWDK.InvDkTypeSetForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x162, 0x110);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "InvDkTypeSetForm";
            this.Text = "代开信息设置";
            base.ResumeLayout(false);
        }
    }
}

