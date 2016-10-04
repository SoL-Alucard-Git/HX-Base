namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpzpz.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FaPiaoFindTiaoJian : BaseForm
    {
        private CustomStyleDataGrid _CustomStyleDataGrid1;
        private ILog _Loger = LogUtil.GetLogger<FaPiaoFindTiaoJian>();
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private AisinoBTN but_zhuhechaxun;
        private AisinoCHK che_gfmc;
        private AisinoCHK che_gfsh;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private AisinoCMB com_fpzl;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        private AisinoTXT text_Fpdm;
        private AisinoTXT text_Fphm_Q;
        private AisinoTXT text_Fphm_Z;
        private AisinoTXT text_Fpje_Q;
        private AisinoTXT text_Fpje_Z;
        private AisinoTXT txt_gfmc;
        private AisinoTXT txt_gfsh;
        private XmlComponentLoader xmlComponentLoader1;

        public FaPiaoFindTiaoJian()
        {
            try
            {
                this.Initialize();
                this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.com_fpzl.Items.Clear();
                for (int i = 0; i < DingYiZhiFuChuan.FPZPZ_Fpzl.Length; i++)
                {
                    this.com_fpzl.Items.Add(DingYiZhiFuChuan.FPZPZ_Fpzl[i]);
                }
                this.com_fpzl.SelectedIndex = 0;
                this.text_Fphm_Z.MaxLength = 8;
                this.text_Fphm_Q.MaxLength = 8;
                this.text_Fpdm.MaxLength = 10;
                this.text_Fpje_Q.MaxLength = 12;
                this.text_Fpje_Z.MaxLength = 12;
                this.txt_gfsh.MaxLength = 0x19;
                this.txt_gfmc.MaxLength = 100;
                this.data_jsrq.MinDate = DingYiZhiFuChuan.MinDate;
                this.data_ksrq.MinDate = DingYiZhiFuChuan.MinDate;
                this.che_jsrq.Checked = true;
                this.che_gfsh.Checked = true;
                this.che_gfmc.Checked = true;
                this.txt_gfsh.Text = string.Empty;
                this.txt_gfmc.Text = string.Empty;
                this.che_ksrq.Checked = true;
                this.text_Fphm_Z.Text = string.Empty;
                this.text_Fphm_Q.Text = string.Empty;
                this.text_Fpdm.Text = string.Empty;
                this.text_Fpje_Z.Text = string.Empty;
                this.text_Fpje_Q.Text = string.Empty;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_Close_Clike(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_Ok_Clike(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.OK;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void but_ZhuHeChaxun_Clike(object sender, EventArgs e)
        {
            try
            {
                this._CustomStyleDataGrid1.Select(this);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
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

        private void Double_KeyPressEventHandler(object sender, KeyPressEventArgs e)
        {
            try
            {
                string text = ((AisinoTXT) sender).Text;
                if (((e.KeyChar != '\b') && !char.IsDigit(e.KeyChar)) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '.')
                {
                    if ((text.IndexOf('.') != -1) || (text.Length == 0))
                    {
                        e.Handled = true;
                    }
                }
                else if ((e.KeyChar == '0') && (text.Length == 0))
                {
                    e.Handled = true;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.che_jsrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_jsrq");
            this.che_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_gfsh");
            this.che_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_gfmc");
            this.txt_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfsh");
            this.txt_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfmc");
            this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
            this.che_ksrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_ksrq");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.but_zhuhechaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_zhuhechaxun");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.text_Fphm_Z = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fphm_Z");
            this.text_Fphm_Q = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fphm_Q");
            this.text_Fpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fpdm");
            this.text_Fpje_Z = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fpje_Z");
            this.text_Fpje_Q = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_Fpje_Q");
            this.but_ok.Click += new EventHandler(this.but_Ok_Clike);
            this.but_zhuhechaxun.Click += new EventHandler(this.but_ZhuHeChaxun_Clike);
            this.but_close.Click += new EventHandler(this.but_Close_Clike);
            this.text_Fphm_Z.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fphm_Q.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fpdm.KeyPress += new KeyPressEventHandler(this.Int_KeyPressEventHandler);
            this.text_Fpje_Q.KeyPress += new KeyPressEventHandler(this.Double_KeyPressEventHandler);
            this.text_Fpje_Z.KeyPress += new KeyPressEventHandler(this.Double_KeyPressEventHandler);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(360, 0x1a2);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.FaPiaoFindTiaoJian\Aisino.Fwkp.Fpzpz.Form.FaPiaoFindTiaoJian.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(360, 0x1a2);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FaPiaoFindTiaoJian";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "未转凭证发票查询";
            base.ResumeLayout(false);
        }

        private void Int_KeyPressEventHandler(object sender, KeyPressEventArgs e)
        {
            try
            {
                string text = ((AisinoTXT) sender).Text;
                if (((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39)) && (((Convert.ToInt32(e.KeyChar) != 0x2e) && (Convert.ToInt32(e.KeyChar) != 8)) && (Convert.ToInt32(e.KeyChar) != 13)))
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '0')
                {
                    if (text.Length == 0)
                    {
                        e.Handled = true;
                    }
                }
                else if (Convert.ToInt32(e.KeyChar) == 0x2e)
                {
                    e.Handled = true;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        public AisinoCHK Che_gfmc
        {
            get
            {
                return this.che_gfmc;
            }
        }

        public AisinoCHK Che_gfsh
        {
            get
            {
                return this.che_gfsh;
            }
        }

        public AisinoCHK Che_jsrq
        {
            get
            {
                return this.che_jsrq;
            }
        }

        public AisinoCHK Che_ksrq
        {
            get
            {
                return this.che_ksrq;
            }
        }

        public AisinoCMB Com_fpzl
        {
            get
            {
                return this.com_fpzl;
            }
        }

        public DateTime Data_jsrqValue
        {
            get
            {
                return this.data_jsrq.Value;
            }
            set
            {
                this.data_jsrq.Value = value;
            }
        }

        public DateTime Data_ksrqValue
        {
            get
            {
                return this.data_ksrq.Value;
            }
            set
            {
                this.data_ksrq.Value = value;
            }
        }

        public CustomStyleDataGrid DataGrid
        {
            set
            {
                this._CustomStyleDataGrid1 = value;
            }
        }

        public AisinoTXT Text_Fpdm
        {
            get
            {
                return this.text_Fpdm;
            }
        }

        public AisinoTXT Text_Fphm_Q
        {
            get
            {
                return this.text_Fphm_Q;
            }
        }

        public AisinoTXT Text_Fphm_Z
        {
            get
            {
                return this.text_Fphm_Z;
            }
        }

        public AisinoTXT Text_Fpje_Q
        {
            get
            {
                return this.text_Fpje_Q;
            }
        }

        public AisinoTXT Text_Fpje_Z
        {
            get
            {
                return this.text_Fpje_Z;
            }
        }

        public AisinoTXT Txt_gfmc
        {
            get
            {
                return this.txt_gfmc;
            }
        }

        public AisinoTXT Txt_gfsh
        {
            get
            {
                return this.txt_gfsh;
            }
        }
    }
}

