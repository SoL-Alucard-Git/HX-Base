namespace InvAutomation
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    public class SetForm : Form
    {
        private Button btnQuit;
        private Button btnSave;
        private Button btnTestLink;
        private IContainer icontainer_0;
        private Label label1;
        private static SetForm setForm_0;
        private TextBox txtServerIp;

        static SetForm()
        {
            
        }

        private SetForm()
        {
            
            this.InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove("MSURL");
            configuration.AppSettings.Settings.Add("MSURL", this.txtServerIp.Text.Trim());
            configuration.Save();
            Thread.Sleep(0x3e8);
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Application.Exit();
        }

        private void btnTestLink_Click(object sender, EventArgs e)
        {
            if (DlgDown.TestLink(this.txtServerIp.Text.Trim()))
            {
                MessageBox.Show("连接成功！");
            }
            else
            {
                MessageBox.Show("连接失败！");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public static SetForm GetSetInstance()
        {
            if (setForm_0 == null)
            {
                setForm_0 = new SetForm();
            }
            return setForm_0;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SetForm));
            this.label1 = new Label();
            this.txtServerIp = new TextBox();
            this.btnSave = new Button();
            this.btnQuit = new Button();
            this.btnTestLink = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 11f);
            this.label1.ForeColor = Color.Maroon;
            this.label1.Location = new Point(0x15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x8e, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入服务器地址：";
            this.txtServerIp.Location = new Point(0x15, 0x29);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new Size(0x123, 0x15);
            this.txtServerIp.TabIndex = 1;
            this.btnSave.Location = new Point(0x81, 0x56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x4b, 0x17);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnQuit.Location = new Point(0xd8, 0x56);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new Size(0x4b, 0x17);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "取  消";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new EventHandler(this.btnQuit_Click);
            this.btnTestLink.Location = new Point(0x2a, 0x56);
            this.btnTestLink.Name = "btnTestLink";
            this.btnTestLink.Size = new Size(0x4b, 0x17);
            this.btnTestLink.TabIndex = 4;
            this.btnTestLink.Text = "测试连接";
            this.btnTestLink.UseVisualStyleBackColor = true;
            this.btnTestLink.Click += new EventHandler(this.btnTestLink_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x14c, 0x85);
            base.Controls.Add(this.btnTestLink);
            base.Controls.Add(this.btnQuit);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.txtServerIp);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SetForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            base.Load += new EventHandler(this.SetForm_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void SetForm_Load(object sender, EventArgs e)
        {
            this.txtServerIp.Text = ConfigurationManager.AppSettings["MSURL"];
        }
    }
}

