namespace MainExecute
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class MainForm : Form
    {
        private Button aisinoBTN1;
        private ComboBox aisinoCMB1;
        private Label aisinoLBL1;
        private IContainer components;
        internal static Dictionary<string, string[]> corpinfo;
        internal static string flag;
        internal static string[] infos = new string[0];
        private RegistryKey key;

        public MainForm()
        {
            this.InitializeComponent();
            this.aisinoCMB1.Items.AddRange(infos);
            this.key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", true);
            string a = (this.key.GetValue("code") as string) + "." + (this.key.GetValue("machine") as string);
            for (int i = 0; i < infos.Length; i++)
            {
                if (string.Equals(a, infos[i]))
                {
                    this.aisinoCMB1.SelectedIndex = i;
                }
            }
            if (flag.Equals("uninst"))
            {
                this.aisinoBTN1.Text = "卸载开票软件";
                this.Text = "卸载";
            }
        }

        private void aisinoBTN1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.aisinoCMB1.Text))
                {
                    MessageBox.Show("请选择企业信息！", "错误");
                }
                else
                {
                    string[] strArray = corpinfo[this.aisinoCMB1.Text];
                    this.key.SetValue("code", strArray[0]);
                    this.key.SetValue("machine", strArray[1]);
                    this.key.SetValue("orgcode", strArray[2]);
                    this.key.SetValue("Version", strArray[3]);
                    this.key.SetValue("Path", strArray[4]);
                    this.key.SetValue("", strArray[5]);
                    this.key.SetValue("DataBaseVersion", strArray[6]);
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    if (flag.Equals("uninst"))
                    {
                        startInfo.FileName = Path.Combine(strArray[4], "uninst.exe");
                    }
                    else
                    {
                        startInfo.FileName = strArray[5];
                    }
                    MessageBox.Show(startInfo.FileName);
                    startInfo.Arguments = "";
                    startInfo.UseShellExecute = true;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(startInfo);
                    MainE.UnInfo = this.aisinoCMB1.Text;
                    base.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("启动税控发票开票软件异常！", "异常");
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MainForm));
            this.aisinoCMB1 = new ComboBox();
            this.aisinoLBL1 = new Label();
            this.aisinoBTN1 = new Button();
            base.SuspendLayout();
            this.aisinoCMB1.BackColor = SystemColors.Window;
            this.aisinoCMB1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.aisinoCMB1.FlatStyle = FlatStyle.System;
            this.aisinoCMB1.FormattingEnabled = true;
            this.aisinoCMB1.ItemHeight = 12;
            this.aisinoCMB1.Location = new Point(0x75, 0x35);
            this.aisinoCMB1.MaxDropDownItems = 20;
            this.aisinoCMB1.Name = "aisinoCMB1";
            this.aisinoCMB1.Size = new Size(0xe0, 20);
            this.aisinoCMB1.Sorted = true;
            this.aisinoCMB1.TabIndex = 1;
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoLBL1.Location = new Point(0x20, 0x35);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x4f, 20);
            this.aisinoLBL1.TabIndex = 2;
            this.aisinoLBL1.Text = "选择企业：";
            this.aisinoBTN1.Cursor = Cursors.Hand;
            this.aisinoBTN1.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.aisinoBTN1.Location = new Point(0x7b, 0x69);
            this.aisinoBTN1.Name = "aisinoBTN1";
            this.aisinoBTN1.Size = new Size(0x7e, 0x2c);
            this.aisinoBTN1.TabIndex = 5;
            this.aisinoBTN1.Text = "启动开票软件";
            this.aisinoBTN1.UseVisualStyleBackColor = true;
            this.aisinoBTN1.Click += new EventHandler(this.aisinoBTN1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x175, 170);
            base.Controls.Add(this.aisinoBTN1);
            base.Controls.Add(this.aisinoLBL1);
            base.Controls.Add(this.aisinoCMB1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MainForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "运行";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

