namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ClearDJ : Form
    {
        private AisinoBTN btnOK;
        private AisinoBTN button2;
        private CheckBox checkBoxCommSpec;
        private CheckBox checkBoxTrans;
        private CheckBox checkBoxVech;
        private IContainer components = null;
        private DateTimePicker dateTimePickerBegin;
        private DateTimePicker dateTimePickerEnd;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        public ClearDJ()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.checkBoxCommSpec.Checked || this.checkBoxTrans.Checked) || this.checkBoxVech.Checked)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("BeginTime", this.dateTimePickerBegin.Text);
                    dictionary.Add("EndTime", this.dateTimePickerEnd.Text);
                    dictionary.Add("CommSpec", this.checkBoxCommSpec.Checked);
                    dictionary.Add("Trans", this.checkBoxTrans.Checked);
                    dictionary.Add("Vech", this.checkBoxVech.Checked);
                    BaseDAOFactory.GetBaseDAOSQLite().updateSQL("aisino.Fwkp.Wbjk.XSDJClearByDate", dictionary);
                    MessageManager.ShowMsgBox("单据清理成功！", "提示");
                }
                base.Close();
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.checkBoxCommSpec.Checked || this.checkBoxTrans.Checked) || this.checkBoxVech.Checked)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
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
            this.dateTimePickerBegin = new DateTimePicker();
            this.dateTimePickerEnd = new DateTimePicker();
            this.label1 = new Label();
            this.label2 = new Label();
            this.button2 = new AisinoBTN();
            this.btnOK = new AisinoBTN();
            this.checkBoxCommSpec = new CheckBox();
            this.checkBoxTrans = new CheckBox();
            this.label3 = new Label();
            this.checkBoxVech = new CheckBox();
            this.label4 = new Label();
            base.SuspendLayout();
            this.dateTimePickerBegin.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerBegin.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerBegin.Location = new Point(0x62, 0x40);
            this.dateTimePickerBegin.Name = "dateTimePickerBegin";
            this.dateTimePickerBegin.Size = new Size(0xac, 0x15);
            this.dateTimePickerBegin.TabIndex = 0;
            this.dateTimePickerBegin.Value = new DateTime(0x7df, 5, 0x19, 8, 0x22, 0x2a, 0);
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new Point(0x62, 0x5f);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new Size(0xac, 0x15);
            this.dateTimePickerEnd.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1b, 0x44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始日期：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x1b, 0x63);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束日期：";
            this.button2.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.button2.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.button2.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.button2.Font = new Font("宋体", 9f);
            this.button2.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.button2.Location = new Point(0xc3, 0x85);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.btnOK.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnOK.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnOK.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnOK.Enabled = false;
            this.btnOK.Font = new Font("宋体", 9f);
            this.btnOK.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnOK.Location = new Point(0x72, 0x85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.checkBoxCommSpec.AutoSize = true;
            this.checkBoxCommSpec.Location = new Point(0x62, 0x1d);
            this.checkBoxCommSpec.Name = "checkBoxCommSpec";
            this.checkBoxCommSpec.Size = new Size(60, 0x10);
            this.checkBoxCommSpec.TabIndex = 10;
            this.checkBoxCommSpec.Text = "专普票";
            this.checkBoxCommSpec.UseVisualStyleBackColor = true;
            this.checkBoxCommSpec.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
            this.checkBoxTrans.AutoSize = true;
            this.checkBoxTrans.Location = new Point(0xa4, 0x1d);
            this.checkBoxTrans.Name = "checkBoxTrans";
            this.checkBoxTrans.Size = new Size(0x30, 0x10);
            this.checkBoxTrans.TabIndex = 10;
            this.checkBoxTrans.Text = "货运";
            this.checkBoxTrans.UseVisualStyleBackColor = true;
            this.checkBoxTrans.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1b, 0x1f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "票种：";
            this.checkBoxVech.AutoSize = true;
            this.checkBoxVech.Location = new Point(0xda, 0x1d);
            this.checkBoxVech.Name = "checkBoxVech";
            this.checkBoxVech.Size = new Size(60, 0x10);
            this.checkBoxVech.TabIndex = 10;
            this.checkBoxVech.Text = "机动车";
            this.checkBoxVech.UseVisualStyleBackColor = true;
            this.checkBoxVech.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1c, 0x2b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xf5, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "........................................";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(300, 0xaf);
            base.Controls.Add(this.checkBoxVech);
            base.Controls.Add(this.checkBoxTrans);
            base.Controls.Add(this.checkBoxCommSpec);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dateTimePickerEnd);
            base.Controls.Add(this.dateTimePickerBegin);
            base.Controls.Add(this.label4);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ClearDJ";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "单据清理";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

