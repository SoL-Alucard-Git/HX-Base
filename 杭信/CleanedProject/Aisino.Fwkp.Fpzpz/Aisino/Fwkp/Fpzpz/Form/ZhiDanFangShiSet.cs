namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpzpz.Common;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ZhiDanFangShiSet : BaseForm
    {
        private ILog _Loger = LogUtil.GetLogger<ZhiDanFangShiSet>();
        private AisinoCMB comb_Qtkm_Zdfs;
        private AisinoCMB comb_Xtsp_Zds;
        private AisinoCMB comb_Yskm_Zdfs;
        private AisinoCMB comb_Zfsfp_Zds;
        private IContainer components;
        private ToolStripButton tool_CanShu;
        private ToolStripButton tool_Edit;
        private ToolStripButton tool_Quit;
        private XmlComponentLoader xmlComponentLoader1;

        public ZhiDanFangShiSet()
        {
            try
            {
                this.Initialize();
                this.InitializeCtrl();
                this.tool_Edit.Checked = true;
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

        private void combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AisinoCMB ocmb = (AisinoCMB) sender;
                if (ocmb != null)
                {
                    string name = ocmb.Name;
                    if (name.Equals("comb_Yskm_Zdfs"))
                    {
                        string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue);
                        string str3 = this.comb_Yskm_Zdfs.SelectedItem.ToString();
                        if (!str2.Equals(str3))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue, str3);
                        }
                    }
                    else if (name.Equals("comb_Qtkm_Zdfs"))
                    {
                        string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue);
                        string str5 = this.comb_Qtkm_Zdfs.SelectedItem.ToString();
                        if (!str4.Equals(str5))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue, str5);
                        }
                    }
                    else if (name.Equals("comb_Zfsfp_Zds"))
                    {
                        string str6 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                        string str7 = this.comb_Zfsfp_Zds.SelectedItem.ToString();
                        if (!str6.Equals(str7))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, str7);
                        }
                    }
                    else if (name.Equals("comb_Xtsp_Zds"))
                    {
                        string str8 = PropertyUtil.GetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue);
                        string str9 = this.comb_Xtsp_Zds.SelectedItem.ToString();
                        if (!str8.Equals(str9))
                        {
                            PropertyUtil.SetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue, str9);
                        }
                    }
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
            this.comb_Yskm_Zdfs = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Yskm_Zdfs");
            this.comb_Qtkm_Zdfs = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Qtkm_Zdfs");
            this.comb_Zfsfp_Zds = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Zfsfp_Zds");
            this.comb_Xtsp_Zds = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comb_Xtsp_Zds");
            this.tool_Quit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Quit");
            this.tool_Edit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Edit");
            this.tool_CanShu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_CanShu");
            this.comb_Yskm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Qtkm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Zfsfp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comb_Xtsp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tool_Quit.Click += new EventHandler(this.tool_Quit_Click);
            this.tool_Edit.Click += new EventHandler(this.tool_Edit_Click);
            this.tool_CanShu.Click += new EventHandler(this.tool_CanShu_Click);
            this.comb_Yskm_Zdfs.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Qtkm_Zdfs.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Zfsfp_Zds.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            this.comb_Xtsp_Zds.SelectedIndexChanged += new EventHandler(this.combo_Yskm_Qtkm_Zfsfp_Xtsp_SelectedIndexChanged);
            base.FormClosing += new FormClosingEventHandler(this.ZhiDanFangShiSet_FormClosing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a8, 0xf9);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.ZhiDanFangShiSet\Aisino.Fwkp.Fpzpz.Form.ZhiDanFangShiSet.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a8, 0xf9);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ZhiDanFangShiSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "制单方式设置";
            base.ResumeLayout(false);
        }

        private void InitializeCtrl()
        {
            try
            {
                this.comb_Yskm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str in DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItem)
                {
                    this.comb_Yskm_Zdfs.Items.Add(str);
                }
                string str2 = PropertyUtil.GetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue);
                this.comb_Yskm_Zdfs.SelectedItem = str2;
                if (string.IsNullOrEmpty(str2))
                {
                    this.comb_Yskm_Zdfs.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue, this.comb_Yskm_Zdfs.SelectedItem.ToString());
                }
                this.comb_Qtkm_Zdfs.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str3 in DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItem)
                {
                    this.comb_Qtkm_Zdfs.Items.Add(str3);
                }
                string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue);
                this.comb_Qtkm_Zdfs.SelectedItem = str4;
                if (string.IsNullOrEmpty(str4))
                {
                    this.comb_Qtkm_Zdfs.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue, this.comb_Qtkm_Zdfs.SelectedItem.ToString());
                }
                this.comb_Zfsfp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str5 in DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItem)
                {
                    this.comb_Zfsfp_Zds.Items.Add(str5);
                }
                string str6 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                this.comb_Zfsfp_Zds.SelectedItem = str6;
                if (string.IsNullOrEmpty(str6))
                {
                    this.comb_Zfsfp_Zds.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, this.comb_Zfsfp_Zds.SelectedItem.ToString());
                }
                this.comb_Xtsp_Zds.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (string str7 in DingYiZhiFuChuan.Xtsp_ZdsItem)
                {
                    this.comb_Xtsp_Zds.Items.Add(str7);
                }
                string str8 = PropertyUtil.GetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue);
                this.comb_Xtsp_Zds.SelectedItem = str8;
                if (string.IsNullOrEmpty(str8))
                {
                    this.comb_Xtsp_Zds.SelectedIndex = 0;
                    PropertyUtil.SetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue, this.comb_Xtsp_Zds.SelectedItem.ToString());
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

        private void SetAllCtrlEnabled(bool bEnabled)
        {
            try
            {
                this.comb_Yskm_Zdfs.Enabled = bEnabled;
                this.comb_Qtkm_Zdfs.Enabled = bEnabled;
                this.comb_Zfsfp_Zds.Enabled = bEnabled;
                this.comb_Xtsp_Zds.Enabled = bEnabled;
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

        private void tool_CanShu_Click(object sender, EventArgs e)
        {
            try
            {
                new QiYeGuanLiRuanJianMsgSet().ShowDialog();
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

        private void tool_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                this.tool_Edit.Checked = !this.tool_Edit.Checked;
                this.SetAllCtrlEnabled(this.tool_Edit.Checked);
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

        private void tool_Quit_Click(object sender, EventArgs e)
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

        private void ZhiDanFangShiSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            PropertyUtil.Save();
        }
    }
}

