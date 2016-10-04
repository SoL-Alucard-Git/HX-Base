namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class BMCL_Edit : BaseForm
    {
        private string _bm;
        private string _sjbm;
        private AisinoLBL aisinoLBL1;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL3;
        private AisinoLBL aisinoLBL4;
        private AisinoLBL aisinoLBL5;
        private BLL.BMCLManager clManager;
        private BMCLModel clModel;
        private AisinoCMB comboBoxYHZC;
        private AisinoCMB comboBoxYHZCMC;
        private AisinoMultiCombox comBoxSPFL;
        private IContainer components;
        private BMCL father;
        private bool isUpdate;
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label13;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label8;
        private ILog log;
        public string retCode;
        private AisinoTXT SPFLMC;
        private bool SucDialog;
        private string SuggestBM;
        private string SuggestMC;
        private AisinoTXT textBoxBM;
        private AisinoMultiCombox textBoxCD;
        private AisinoTXT textBoxCPXH;
        private AisinoTXT textBoxJM;
        private AisinoMultiCombox textBoxSCCJMC;
        private TextBoxTreeView textBoxSJBM;
        private AisinoMultiCombox textBoxWaitMC;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnCancel;
        private ToolStripButton toolStripBtnContinue;
        private ToolStripButton toolStripBtnSave;
        private List<string> YHZCMC;
        private string yuanBM;

        public BMCL_Edit(string SJBM, BMCL Father)
        {
            this.clManager = new BLL.BMCLManager();
            this.clModel = new BMCLModel();
            this.log = LogUtil.GetLogger<BMCL_Edit>();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.retCode = string.Empty;
            this.SuggestMC = string.Empty;
            this.SucDialog = true;
            this.YHZCMC = new List<string>();
            this.Initialize();
            this._sjbm = SJBM;
            this.father = Father;
            this.Text = "车辆编码添加";
        }

        public BMCL_Edit(string BM, bool Isupdate)
        {
            this.clManager = new BLL.BMCLManager();
            this.clModel = new BMCLModel();
            this.log = LogUtil.GetLogger<BMCL_Edit>();
            this._bm = "";
            this._sjbm = "";
            this.SuggestBM = "";
            this.retCode = string.Empty;
            this.SuggestMC = string.Empty;
            this.SucDialog = true;
            this.YHZCMC = new List<string>();
            this.Initialize();
            this._bm = BM;
            this.yuanBM = BM;
            this.isUpdate = Isupdate;
            if (this.isUpdate)
            {
                this.Text = "车辆编码编辑";
            }
            else
            {
                this.Text = "车辆编码添加";
            }
        }

        public BMCL_Edit(string BM, string MC, bool Isupdate, bool sucdialog) : this(BM, Isupdate)
        {
            this.SuggestMC = MC;
            this.SucDialog = sucdialog;
        }

        private void AdjustComboBoxYHZC()
        {
            if (Flbm.IsYM())
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if (((objArray == null) || ((objArray[0] as DataTable).Rows.Count <= 0)) || ((objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString() == ""))
                {
                    this.comboBoxYHZC.SelectedIndex = 1;
                    this.comboBoxYHZC.Enabled = false;
                }
                else
                {
                    this.comboBoxYHZC.Enabled = true;
                    if (!Flbm.IsDK())
                    {
                        string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                        if ((strArray.Length == 1) && strArray[0].Contains("1.5%"))
                        {
                            this.comboBoxYHZC.SelectedIndex = 1;
                            this.comboBoxYHZC.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                this.comboBoxYHZC.Text = "否";
                this.comboBoxYHZCMC.Items.Add("");
                this.comboBoxYHZCMC.Text = "";
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void AdjustComboBoxYHZCMC()
        {
            if (this.comboBoxYHZC.Enabled && (this.comboBoxYHZC.Text.Trim() == "是"))
            {
                this.comboBoxYHZCMC.Enabled = true;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if ((objArray != null) && (objArray.Length > 0))
                {
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray.Length > 0)
                    {
                        string text = this.comboBoxYHZCMC.Text;
                        this.comboBoxYHZCMC.Items.Clear();
                        foreach (string str2 in strArray)
                        {
                            if (Flbm.IsDK() || !str2.Contains("1.5%"))
                            {
                                this.comboBoxYHZCMC.Items.Add(str2);
                                if (text == str2)
                                {
                                    this.comboBoxYHZCMC.Text = text;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void BMCL_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                this.aisinoLBL1.Visible = Flbm.IsYM();
                this.SPFLMC.Visible = Flbm.IsYM();
                this.aisinoLBL4.Visible = Flbm.IsYM();
                this.comboBoxYHZCMC.Visible = Flbm.IsYM();
                this.aisinoLBL5.Visible = Flbm.IsYM();
                this.aisinoLBL2.Visible = Flbm.IsYM();
                this.comboBoxYHZC.Visible = Flbm.IsYM();
                this.comBoxSPFL.Visible = Flbm.IsYM();
                if (this.isUpdate)
                {
                    this.LoadData(this._bm);
                    this._sjbm = this.clModel.SJBM;
                    this.textBoxSJBM.SelectBM = this.clModel.SJBM;
                    this.toolStripBtnContinue.Visible = false;
                }
                else
                {
                    this.textBoxSJBM.SelectBM = this._sjbm;
                    this.SuggestBM = this.clManager.TuiJianBM(this._sjbm);
                    if (this.SuggestBM == "NoNode")
                    {
                        MessageManager.ShowMsgBox("INP-235121");
                        base.Close();
                        return;
                    }
                    if (this.SuggestBM.Length > 0x10)
                    {
                        MessageManager.ShowMsgBox("INP-235111");
                        base.Close();
                        return;
                    }
                    this.textBoxBM.Text = this.SuggestBM;
                    if (this.SuggestMC.Contains("#%"))
                    {
                        string[] strArray = this.SuggestMC.Split(new string[] { "#%" }, StringSplitOptions.None);
                        if (strArray.Length != 4)
                        {
                            return;
                        }
                        this.textBoxWaitMC.Text = strArray[0].Trim();
                        this.textBoxCPXH.Text = strArray[1].Trim();
                        this.textBoxCD.Text = strArray[2].Trim();
                        this.textBoxSCCJMC.Text = strArray[3].Trim();
                    }
                    else
                    {
                        this.textBoxWaitMC.Text = this.SuggestMC;
                    }
                }
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                this.textBoxSJBM.TreeLoad();
                if (Flbm.IsYM())
                {
                    this.AdjustComboBoxYHZC();
                    this.AdjustComboBoxYHZCMC();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void btnCD_Click(object sender, EventArgs e)
        {
            BMCDSelect select = new BMCDSelect(this.textBoxCD.Text);
            if (select.ShowDialog() == DialogResult.OK)
            {
                this.textBoxCD.Text = select.SelectedBM;
            }
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            BMCLZLSelect select = new BMCLZLSelect(this.textBoxWaitMC.Text);
            if (select.ShowDialog() == DialogResult.OK)
            {
                this.textBoxWaitMC.Text = select.SelectedBM;
            }
        }

        private void btnSCCJMC_Click(object sender, EventArgs e)
        {
            BMSCCJSelect select = new BMSCCJSelect(this.textBoxSCCJMC.Text);
            if (select.ShowDialog() == DialogResult.OK)
            {
                this.textBoxSCCJMC.Text = select.SelectedBM;
            }
        }

        private void comboBoxYHZC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("是" == this.comboBoxYHZC.SelectedItem.ToString())
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if ((objArray != null) && (objArray.Length > 0))
                {
                    string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(new char[] { '，', '、', '；', ',', ';' });
                    if (strArray.Length > 0)
                    {
                        this.comboBoxYHZCMC.Items.Clear();
                        foreach (string str in strArray)
                        {
                            if (Flbm.IsDK() || !str.Contains("1.5%"))
                            {
                                this.comboBoxYHZCMC.Items.Add(str);
                            }
                        }
                    }
                }
                this.comboBoxYHZCMC.Enabled = true;
            }
            else
            {
                this.comboBoxYHZC.Text = "否";
                this.comboBoxYHZCMC.Items.Add("");
                this.comboBoxYHZCMC.Text = "";
                this.comboBoxYHZCMC.Enabled = false;
            }
        }

        private void ComBoxSPFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                this.AdjustComboBoxYHZC();
                this.AdjustComboBoxYHZCMC();
            }
        }

        private void ComBoxSPFL_OnAutoComplate(object sender, EventArgs e)
        {
            string str = "";
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if ((combox != null) && combox.Name.Equals("comBoxSPFL"))
            {
                str = this.comBoxSPFL.Text.Trim();
            }
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGetSPFLMore", new object[] { str, 10, false, "BM,MC,SLV" });
            if ((objArray != null) && (objArray.Length > 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((combox != null) && (table != null))
                {
                    combox.DataSource = table;
                }
            }
            this.AdjustComboBoxYHZC();
            this.AdjustComboBoxYHZCMC();
        }

        private void comBoxSPFL_OnButtonClick(object sender, EventArgs e)
        {
            BMSPFLSelect select = new BMSPFLSelect(this.comBoxSPFL.Text.Trim(), false, this);
            if (select.ShowDialog() == DialogResult.OK)
            {
                this.comBoxSPFL.Text = select.SelectBM;
                this.SPFLMC.Text = select.SelectBMMC;
            }
        }

        private void ComBoxSPFL_TextChanged(object sender, EventArgs e)
        {
            if (this.comBoxSPFL.Text.Trim() != "")
            {
                int selectionStart = this.comBoxSPFL.SelectionStart;
                this.comBoxSPFL.Text = StringUtils.GetSubString(this.comBoxSPFL.Text, 0x13).Trim();
                this.comBoxSPFL.SelectionStart = selectionStart;
                this.comboBoxYHZC.SelectedIndex = 1;
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", new object[] { this.comBoxSPFL.Text.Trim() });
                if (((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0)) && ((objArray[0] as DataTable).Rows[0]["SLV"].ToString() != ""))
                {
                    DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                    if (!manager.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), false, false))
                    {
                        MessageBox.Show("查无此分类编码或此分类编码不可用，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.comBoxSPFL.Focus();
                        this.comboBoxYHZC.SelectedIndex = 1;
                        this.comboBoxYHZC.Enabled = false;
                        return;
                    }
                }
                this.AdjustComboBoxYHZC();
                this.AdjustComboBoxYHZCMC();
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
            this.textBoxWaitMC.buttonStyle = ButtonStyle.Button;
            this.textBoxWaitMC.Edit = EditStyle.TextBox;
            this.textBoxWaitMC.OnButtonClick += new EventHandler(this.btnMC_Click);
            this.textBoxWaitMC.OnTextChanged = (EventHandler) Delegate.Combine(this.textBoxWaitMC.OnTextChanged, new EventHandler(this.textBoxWaitMC_TextChanged));
            this.textBoxCD.buttonStyle = ButtonStyle.Button;
            this.textBoxCD.Edit = EditStyle.TextBox;
            this.textBoxCD.OnButtonClick += new EventHandler(this.btnCD_Click);
            this.textBoxCD.OnTextChanged = (EventHandler) Delegate.Combine(this.textBoxCD.OnTextChanged, new EventHandler(this.textBoxCD_TextChanged));
            this.textBoxSCCJMC.buttonStyle = ButtonStyle.Button;
            this.textBoxSCCJMC.Edit = EditStyle.TextBox;
            this.textBoxSCCJMC.OnButtonClick += new EventHandler(this.btnSCCJMC_Click);
            this.textBoxSCCJMC.OnTextChanged = (EventHandler) Delegate.Combine(this.textBoxSCCJMC.OnTextChanged, new EventHandler(this.textBoxSCCJMC_TextChanged));
            this.textBoxBM.KeyPress += new KeyPressEventHandler(this.textBoxBM_KeyPress);
            this.textBoxBM.TextChanged += new EventHandler(this.textBoxBM_TextChanged);
            this.textBoxBM.Validating += new CancelEventHandler(this.textBoxBM_Validating);
            this.textBoxJM.KeyPress += new KeyPressEventHandler(this.textBoxJM_KeyPress);
            this.textBoxJM.TextChanged += new EventHandler(this.textBoxJM_TextChanged);
            this.textBoxCPXH.TextChanged += new EventHandler(this.textBoxCPXH_TextChanged);
            this.toolStripBtnSave.Click += new EventHandler(this.toolStripBtnSave_Click);
            this.toolStripBtnContinue.Click += new EventHandler(this.toolStripBtnContinue_Click);
            this.toolStripBtnCancel.Click += new EventHandler(this.toolStripBtnCancel_Click);
            this.textBoxSJBM.RootNodeString = "车辆编码";
            this.textBoxSJBM.Text = "编码";
            this.textBoxSJBM.selectChanged += new TextBoxTreeView.SelectChanged(this.textBoxSJBM_selectChanged);
            this.textBoxSJBM.getListNodes += new TextBoxTreeView.GetListNodes(this.treeviewneed_getListNodes);
            this.comBoxSPFL.KeyPress += new KeyPressEventHandler(this.ComBoxSPFL_KeyPress);
            this.comBoxSPFL.OnTextChanged = (EventHandler) Delegate.Combine(this.comBoxSPFL.OnTextChanged, new EventHandler(this.ComBoxSPFL_TextChanged));
            this.comBoxSPFL.OnAutoComplate += new EventHandler(this.ComBoxSPFL_OnAutoComplate);
            this.comBoxSPFL.OnSelectValue += new EventHandler(this.textBoxSPFL_OnSelectValue);
            this.comBoxSPFL.OnButtonClick += new EventHandler(this.comBoxSPFL_OnButtonClick);
            this.comBoxSPFL.AutoComplate = AutoComplateStyle.HeadWork;
            this.comBoxSPFL.AutoIndex = 1;
            this.comBoxSPFL.Edit = EditStyle.TextBox;
            this.comBoxSPFL.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("编码", "BM", 80));
            this.comBoxSPFL.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("分类名称", "MC", 160));
            this.toolStripBtnContinue.Visible = false;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BMCL_Edit));
            this.textBoxSJBM = new TextBoxTreeView();
            this.label1 = new AisinoLBL();
            this.toolStripBtnContinue = new ToolStripButton();
            this.toolStripBtnCancel = new ToolStripButton();
            this.toolStrip1 = new ToolStrip();
            this.toolStripBtnSave = new ToolStripButton();
            this.label2 = new AisinoLBL();
            this.label4 = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.textBoxWaitMC = new AisinoMultiCombox();
            this.label3 = new AisinoLBL();
            this.textBoxCPXH = new AisinoTXT();
            this.textBoxCD = new AisinoMultiCombox();
            this.label13 = new AisinoLBL();
            this.textBoxSCCJMC = new AisinoMultiCombox();
            this.label10 = new AisinoLBL();
            this.textBoxJM = new AisinoTXT();
            this.textBoxBM = new AisinoTXT();
            this.comBoxSPFL = new AisinoMultiCombox();
            this.aisinoLBL2 = new AisinoLBL();
            this.comboBoxYHZC = new AisinoCMB();
            this.aisinoLBL1 = new AisinoLBL();
            this.aisinoLBL3 = new AisinoLBL();
            this.SPFLMC = new AisinoTXT();
            this.aisinoLBL4 = new AisinoLBL();
            this.comboBoxYHZCMC = new AisinoCMB();
            this.aisinoLBL5 = new AisinoLBL();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.textBoxSJBM.BackColor = SystemColors.Window;
            this.textBoxSJBM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxSJBM.Cursor = Cursors.Arrow;
            this.textBoxSJBM.Location = new Point(0x66, 0x31);
            this.textBoxSJBM.Name = "textBoxSJBM";
            this.textBoxSJBM.ReadOnly = true;
            this.textBoxSJBM.RootNodeString = "根节点";
            this.textBoxSJBM.SelectBM = null;
            this.textBoxSJBM.Size = new Size(0xaf, 0x15);
            this.textBoxSJBM.TabIndex = 0x1f;
            this.textBoxSJBM.Text = "下拉树";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x37, 0x53);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "*编码";
            this.toolStripBtnContinue.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnContinue.Name = "toolStripBtnContinue";
            this.toolStripBtnContinue.Size = new Size(60, 0x18);
            this.toolStripBtnContinue.Text = "保存继续";
            this.toolStripBtnCancel.Image = Resources.取消;
            this.toolStripBtnCancel.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnCancel.Name = "toolStripBtnCancel";
            this.toolStripBtnCancel.Size = new Size(0x34, 0x18);
            this.toolStripBtnCancel.Text = "取消";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripBtnSave, this.toolStripBtnCancel, this.toolStripBtnContinue });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x247, 0x1b);
            this.toolStrip1.TabIndex = 0x26;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripBtnSave.Image = Resources.保存;
            this.toolStripBtnSave.ImageTransparentColor = Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new Size(0x34, 0x18);
            this.toolStripBtnSave.Text = "保存";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x132, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 0x2a;
            this.label2.Text = "*车辆类型";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x120, 0x90);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4d, 12);
            this.label4.TabIndex = 0x2c;
            this.label4.Text = "生产企业名称";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x25, 0x35);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x27;
            this.label8.Text = "上级编码";
            this.textBoxWaitMC.AutoComplate = AutoComplateStyle.None;
            this.textBoxWaitMC.AutoIndex = 1;
            this.textBoxWaitMC.BackColor = SystemColors.Window;
            this.textBoxWaitMC.BorderColor = SystemColors.WindowFrame;
            this.textBoxWaitMC.BorderStyle = AisinoComboxBorderStyle.System;
            this.textBoxWaitMC.ButtonAutoHide = false;
            this.textBoxWaitMC.buttonStyle = ButtonStyle.Button;
            this.textBoxWaitMC.DataSource = null;
            this.textBoxWaitMC.DrawHead = true;
            this.textBoxWaitMC.Edit = EditStyle.TextBox;
            this.textBoxWaitMC.IsSelectAll = false;
            this.textBoxWaitMC.Location = new Point(0x179, 0x31);
            this.textBoxWaitMC.MaxIndex = 8;
            this.textBoxWaitMC.MaxLength = 0x7fff;
            this.textBoxWaitMC.Name = "textBoxWaitMC";
            this.textBoxWaitMC.ReadOnly = false;
            this.textBoxWaitMC.SelectedIndex = -1;
            this.textBoxWaitMC.SelectionStart = 0;
            this.textBoxWaitMC.ShowText = "";
            this.textBoxWaitMC.Size = new Size(0xaf, 0x15);
            this.textBoxWaitMC.TabIndex = 0x21;
            this.textBoxWaitMC.UnderLineColor = Color.Transparent;
            this.textBoxWaitMC.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(330, 0x71);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x2b;
            this.label3.Text = "产地";
            this.textBoxCPXH.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxCPXH.Location = new Point(0x179, 0x4f);
            this.textBoxCPXH.Name = "textBoxCPXH";
            this.textBoxCPXH.Size = new Size(0xaf, 0x15);
            this.textBoxCPXH.TabIndex = 0x23;
            this.textBoxCD.AutoComplate = AutoComplateStyle.None;
            this.textBoxCD.AutoIndex = 1;
            this.textBoxCD.BorderColor = SystemColors.WindowFrame;
            this.textBoxCD.BorderStyle = AisinoComboxBorderStyle.System;
            this.textBoxCD.ButtonAutoHide = false;
            this.textBoxCD.buttonStyle = ButtonStyle.Button;
            this.textBoxCD.DataSource = null;
            this.textBoxCD.DrawHead = true;
            this.textBoxCD.Edit = EditStyle.TextBox;
            this.textBoxCD.IsSelectAll = false;
            this.textBoxCD.Location = new Point(0x179, 0x6d);
            this.textBoxCD.MaxIndex = 8;
            this.textBoxCD.MaxLength = 0x7fff;
            this.textBoxCD.Name = "textBoxCD";
            this.textBoxCD.ReadOnly = false;
            this.textBoxCD.SelectedIndex = -1;
            this.textBoxCD.SelectionStart = 0;
            this.textBoxCD.ShowText = "";
            this.textBoxCD.Size = new Size(0xaf, 0x15);
            this.textBoxCD.TabIndex = 0x24;
            this.textBoxCD.UnderLineColor = Color.Transparent;
            this.textBoxCD.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(310, 0x53);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x29;
            this.label13.Text = "厂牌型号";
            this.textBoxSCCJMC.AutoComplate = AutoComplateStyle.None;
            this.textBoxSCCJMC.AutoIndex = 1;
            this.textBoxSCCJMC.BorderColor = SystemColors.WindowFrame;
            this.textBoxSCCJMC.BorderStyle = AisinoComboxBorderStyle.System;
            this.textBoxSCCJMC.ButtonAutoHide = false;
            this.textBoxSCCJMC.buttonStyle = ButtonStyle.Button;
            this.textBoxSCCJMC.DataSource = null;
            this.textBoxSCCJMC.DrawHead = true;
            this.textBoxSCCJMC.Edit = EditStyle.TextBox;
            this.textBoxSCCJMC.IsSelectAll = false;
            this.textBoxSCCJMC.Location = new Point(0x179, 140);
            this.textBoxSCCJMC.MaxIndex = 8;
            this.textBoxSCCJMC.MaxLength = 0x7fff;
            this.textBoxSCCJMC.Name = "textBoxSCCJMC";
            this.textBoxSCCJMC.ReadOnly = false;
            this.textBoxSCCJMC.SelectedIndex = -1;
            this.textBoxSCCJMC.SelectionStart = 0;
            this.textBoxSCCJMC.ShowText = "";
            this.textBoxSCCJMC.Size = new Size(0xaf, 0x15);
            this.textBoxSCCJMC.TabIndex = 0x25;
            this.textBoxSCCJMC.UnderLineColor = Color.Transparent;
            this.textBoxSCCJMC.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x3d, 0x71);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x2d;
            this.label10.Text = "简码";
            this.textBoxJM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxJM.Location = new Point(0x66, 0x6d);
            this.textBoxJM.Name = "textBoxJM";
            this.textBoxJM.Size = new Size(0xaf, 0x15);
            this.textBoxJM.TabIndex = 0x22;
            this.textBoxBM.BackColor = SystemColors.Window;
            this.textBoxBM.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxBM.Location = new Point(0x66, 0x4f);
            this.textBoxBM.Name = "textBoxBM";
            this.textBoxBM.Size = new Size(0xaf, 0x15);
            this.textBoxBM.TabIndex = 0x20;
            this.comBoxSPFL.AutoComplate = AutoComplateStyle.None;
            this.comBoxSPFL.AutoIndex = 1;
            this.comBoxSPFL.BorderColor = SystemColors.WindowFrame;
            this.comBoxSPFL.BorderStyle = AisinoComboxBorderStyle.System;
            this.comBoxSPFL.ButtonAutoHide = false;
            this.comBoxSPFL.buttonStyle = ButtonStyle.Button;
            this.comBoxSPFL.DataSource = null;
            this.comBoxSPFL.DrawHead = false;
            this.comBoxSPFL.Edit = EditStyle.TextBox;
            this.comBoxSPFL.ForeColor = Color.Maroon;
            this.comBoxSPFL.IsSelectAll = true;
            this.comBoxSPFL.Location = new Point(0x66, 140);
            this.comBoxSPFL.MaxIndex = 8;
            this.comBoxSPFL.MaxLength = 0x7fff;
            this.comBoxSPFL.Name = "comBoxSPFL";
            this.comBoxSPFL.ReadOnly = false;
            this.comBoxSPFL.SelectedIndex = -1;
            this.comBoxSPFL.SelectionStart = 0;
            this.comBoxSPFL.ShowText = "";
            this.comBoxSPFL.Size = new Size(0xaf, 0x15);
            this.comBoxSPFL.TabIndex = 0xc2;
            this.comBoxSPFL.UnderLineColor = Color.White;
            this.comBoxSPFL.UnderLineStyle = AisinoComboxBorderStyle.None;
            this.aisinoLBL2.AutoSize = true;
            this.aisinoLBL2.Location = new Point(0x11f, 0xb2);
            this.aisinoLBL2.Name = "aisinoLBL2";
            this.aisinoLBL2.Size = new Size(0x4d, 12);
            this.aisinoLBL2.TabIndex = 0xc0;
            this.aisinoLBL2.Text = "享受优惠政策";
            this.comboBoxYHZC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYHZC.FormattingEnabled = true;
            this.comboBoxYHZC.Items.AddRange(new object[] { "是", "否" });
            this.comboBoxYHZC.Location = new Point(0x179, 0xae);
            this.comboBoxYHZC.Name = "comboBoxYHZC";
            this.comboBoxYHZC.Size = new Size(0x57, 20);
            this.comboBoxYHZC.TabIndex = 0xbf;
            this.comboBoxYHZC.SelectedIndexChanged += new EventHandler(this.comboBoxYHZC_SelectedIndexChanged);
            this.aisinoLBL1.AutoSize = true;
            this.aisinoLBL1.Location = new Point(13, 0x90);
            this.aisinoLBL1.Name = "aisinoLBL1";
            this.aisinoLBL1.Size = new Size(0x53, 12);
            this.aisinoLBL1.TabIndex = 0xc1;
            this.aisinoLBL1.Text = "*税收分类编码";
            this.aisinoLBL3.AutoSize = true;
            this.aisinoLBL3.Location = new Point(0x10, 0xb2);
            this.aisinoLBL3.Name = "aisinoLBL3";
            this.aisinoLBL3.Size = new Size(0, 12);
            this.aisinoLBL3.TabIndex = 0xc3;
            this.SPFLMC.BorderStyle = BorderStyle.FixedSingle;
            this.SPFLMC.Enabled = false;
            this.SPFLMC.Location = new Point(0x68, 0xad);
            this.SPFLMC.Name = "SPFLMC";
            this.SPFLMC.Size = new Size(0xaf, 0x15);
            this.SPFLMC.TabIndex = 0xc4;
            this.aisinoLBL4.AutoSize = true;
            this.aisinoLBL4.Location = new Point(0x11, 0xd3);
            this.aisinoLBL4.Name = "aisinoLBL4";
            this.aisinoLBL4.Size = new Size(0x4d, 12);
            this.aisinoLBL4.TabIndex = 0xc6;
            this.aisinoLBL4.Text = "优惠政策类型";
            this.comboBoxYHZCMC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxYHZCMC.FormattingEnabled = true;
            this.comboBoxYHZCMC.Items.AddRange(new object[] { "是", "否" });
            this.comboBoxYHZCMC.Location = new Point(0x6b, 0xcf);
            this.comboBoxYHZCMC.Name = "comboBoxYHZCMC";
            this.comboBoxYHZCMC.Size = new Size(170, 20);
            this.comboBoxYHZCMC.TabIndex = 0xc5;
            this.aisinoLBL5.AutoSize = true;
            this.aisinoLBL5.Location = new Point(0x15, 0xb2);
            this.aisinoLBL5.Name = "aisinoLBL5";
            this.aisinoLBL5.Size = new Size(0x4d, 12);
            this.aisinoLBL5.TabIndex = 0xc7;
            this.aisinoLBL5.Text = "税收分类名称";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x247, 240);
            base.Controls.Add(this.aisinoLBL5);
            base.Controls.Add(this.aisinoLBL4);
            base.Controls.Add(this.comboBoxYHZCMC);
            base.Controls.Add(this.SPFLMC);
            base.Controls.Add(this.aisinoLBL3);
            base.Controls.Add(this.comBoxSPFL);
            base.Controls.Add(this.aisinoLBL2);
            base.Controls.Add(this.comboBoxYHZC);
            base.Controls.Add(this.aisinoLBL1);
            base.Controls.Add(this.textBoxSJBM);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.toolStrip1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.textBoxWaitMC);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBoxCPXH);
            base.Controls.Add(this.textBoxCD);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.textBoxSCCJMC);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.textBoxJM);
            base.Controls.Add(this.textBoxBM);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "BMCL_Edit";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "BMCL_Edit";
            base.Load += new EventHandler(this.BMCL_Edit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InModel()
        {
            this.clModel.BM = this.textBoxBM.Text.Trim();
            this.clModel.CPXH = this.textBoxCPXH.Text.Trim();
            this.clModel.MC = this.textBoxWaitMC.Text.Trim();
            this.clModel.CD = this.textBoxCD.Text.Trim();
            this.clModel.SJBM = this.textBoxSJBM.Text.Trim();
            this.clModel.JM = this.textBoxJM.Text.Trim();
            this.clModel.SCCJMC = this.textBoxSCCJMC.Text.Trim();
            this.clModel.KJM = CommonFunc.GenerateKJM(this.textBoxWaitMC.Text.Trim());
            this.clModel.WJ = 1;
            if (Flbm.IsYM())
            {
                this.clModel.SPFL = this.comBoxSPFL.Text.Trim();
                this.clModel.SPFLMC = this.SPFLMC.Text.Trim();
                this.clModel.YHZC = (this.comboBoxYHZC.SelectedIndex == 0) ? "是" : "否";
                this.clModel.YHZCMC = this.comboBoxYHZCMC.Text.Trim();
            }
        }

        private void LoadData(string BM)
        {
            this.clModel = (BMCLModel) this.clManager.GetModel(BM);
            this.textBoxBM.Text = this.clModel.BM;
            this.textBoxCPXH.Text = this.clModel.CPXH;
            this.textBoxWaitMC.Text = this.clModel.MC;
            this.textBoxCD.Text = this.clModel.CD;
            this.textBoxSJBM.Text = this.clModel.SJBM;
            this.textBoxJM.Text = this.clModel.JM;
            this.textBoxSCCJMC.Text = this.clModel.SCCJMC;
            if (Flbm.IsYM())
            {
                this.comBoxSPFL.Text = this.clModel.SPFL;
                this.SPFLMC.Text = this.clModel.SPFLMC;
                this.comboBoxYHZC.SelectedIndex = ("是" == this.clModel.YHZC) ? 0 : 1;
                if (this.comboBoxYHZCMC.Enabled)
                {
                    try
                    {
                        this.comboBoxYHZCMC.Text = this.clModel.YHZCMC.ToString();
                    }
                    catch
                    {
                        if (!this.comboBoxYHZCMC.Items.Contains(""))
                        {
                            this.comboBoxYHZCMC.Items.Add("");
                            this.comboBoxYHZCMC.Text = "";
                        }
                    }
                }
            }
        }

        private bool SimpleValidated()
        {
            bool flag = true;
            if (this.textBoxWaitMC.Text.Trim().Length == 0)
            {
                this.textBoxWaitMC.Focus();
                flag = false;
            }
            if (this.textBoxJM.Text.Trim().Trim().Length != 0)
            {
                string input = this.textBoxJM.Text.Trim();
                if ((input[0] < 'A') || (input[0] > 'Z'))
                {
                    this.textBoxJM.Focus();
                    flag = false;
                    MessageManager.ShowMsgBox("INP-235501");
                    return flag;
                }
                string pattern = "^[A-Z0-9]+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(input))
                {
                    this.textBoxJM.Focus();
                    flag = false;
                    MessageManager.ShowMsgBox("INP-235502");
                    return flag;
                }
            }
            if (Flbm.IsYM())
            {
                DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                if (!manager.CanUseThisSPFLBM(this.comBoxSPFL.Text.Trim(), false, false))
                {
                    MessageBox.Show("查无此分类编码或此分类编码不可用，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comBoxSPFL.Focus();
                    this.SPFLMC.Text = "";
                    return false;
                }
                this.SPFLMC.Text = manager.GetSPFLMCBYBM(this.comBoxSPFL.Text.Trim());
                if (("是" == this.comboBoxYHZC.SelectedItem.ToString()) && (this.comboBoxYHZCMC.Text == ""))
                {
                    MessageBox.Show("优惠类型不能为空，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.comboBoxYHZCMC.Focus();
                    return false;
                }
            }
            else
            {
                flag = true;
            }
            if (!flag)
            {
                MessageManager.ShowMsgBox("INP-235309");
            }
            return flag;
        }

        private void textBoxBM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (char.IsUpper(e.KeyChar))
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if ((!char.IsDigit(e.KeyChar) && !char.IsLower(e.KeyChar)) || (this.textBoxBM.Text.Trim().Length >= 0x10))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBoxBM_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBM.Text.Trim() != "")
            {
                int selectionStart = this.textBoxBM.SelectionStart;
                this.textBoxBM.Text = StringUtils.GetSubString(this.textBoxBM.Text, 0x10).Trim();
                this.textBoxBM.SelectionStart = selectionStart;
            }
        }

        private bool textBoxBM_Validating()
        {
            bool flag = true;
            string input = this.textBoxBM.Text.Trim();
            string pattern = "^[a-z0-9]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(input))
            {
                MessageManager.ShowMsgBox("INP-235506");
                return false;
            }
            if (!this.isUpdate)
            {
                if (!this.textBoxBM.Text.StartsWith(this._sjbm))
                {
                    this.textBoxBM.Text = this.SuggestBM;
                    this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                    MessageManager.ShowMsgBox("INP-235306");
                    return false;
                }
                if (!("NoXJBM" != this.clManager.ChildDetermine(this.textBoxBM.Text.Trim(), this._sjbm)) || (this.textBoxBM.Text.Length == this.clManager.GetSuggestBMLen(this._sjbm)))
                {
                    return flag;
                }
                this.textBoxBM.Text = this.SuggestBM;
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235305");
                return false;
            }
            if (!this.textBoxBM.Text.StartsWith(this._sjbm))
            {
                if (this.textBoxSJBM.Text != this.clModel.SJBM)
                {
                    this.textBoxBM.Text = this.SuggestBM;
                }
                else
                {
                    this.textBoxBM.Text = this._bm;
                }
                this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
                MessageManager.ShowMsgBox("INP-235306");
                return false;
            }
            if (!("OnlyBMAndIsSelf" != this.clManager.ChildDetermine(this.yuanBM, this._sjbm)) || (this.textBoxBM.Text.Length == this.clManager.GetSuggestBMLen(this._sjbm)))
            {
                return flag;
            }
            if (this.textBoxSJBM.Text != this.clModel.SJBM)
            {
                this.textBoxBM.Text = this.SuggestBM;
            }
            else
            {
                this.textBoxBM.Text = this._bm;
            }
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
            MessageManager.ShowMsgBox("INP-235305");
            return false;
        }

        private void textBoxBM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.textBoxBM_Validating())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void textBoxCD_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxCD.SelectionStart;
            this.textBoxCD.Text = StringUtils.GetSubString(this.textBoxCD.Text, 0x20);
            this.textBoxCD.SelectionStart = selectionStart;
        }

        private void textBoxCPXH_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxCPXH.SelectionStart;
            this.textBoxCPXH.Text = StringUtils.GetSubString(this.textBoxCPXH.Text, 60);
            this.textBoxCPXH.SelectionStart = selectionStart;
        }

        private void textBoxJM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (char.IsLower(e.KeyChar))
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                if (!char.IsDigit(e.KeyChar) && !char.IsUpper(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBoxJM_TextChanged(object sender, EventArgs e)
        {
            this.textBoxJM.Text = this.textBoxJM.Text.Trim().ToUpper();
            if (this.textBoxJM.Text.Trim() != "")
            {
                string str = this.textBoxJM.Text.Trim();
                if (!char.IsLetter(str[0]))
                {
                    MessageBoxHelper.Show("简码必须以大写字母开头!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBoxJM.Text = "";
                    this.textBoxJM.Focus();
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!char.IsDigit(str[i]) && !char.IsUpper(str[i]))
                        {
                            MessageBoxHelper.Show("简码只能用大写字母和数字!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.textBoxJM.Text = "";
                            this.textBoxJM.Focus();
                            return;
                        }
                    }
                    int selectionStart = this.textBoxJM.SelectionStart;
                    this.textBoxJM.Text = StringUtils.GetSubString(this.textBoxJM.Text, 6).Trim();
                    this.textBoxJM.SelectionStart = selectionStart;
                }
            }
        }

        private void textBoxSCCJMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxSCCJMC.SelectionStart;
            this.textBoxSCCJMC.Text = StringUtils.GetSubString(this.textBoxSCCJMC.Text, 80);
            this.textBoxSCCJMC.SelectionStart = selectionStart;
        }

        private void textBoxSJBM_selectChanged(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._sjbm = e.Node.Name;
            if (this.isUpdate && (this._sjbm == this.clModel.SJBM))
            {
                this.textBoxBM.Text = this._bm;
            }
            else
            {
                this.textBoxBM.Text = this.clManager.TuiJianBM(e.Node.Name);
            }
            this.SuggestBM = this.clManager.TuiJianBM(e.Node.Name);
            this.textBoxBM.Select(this.textBoxBM.Text.Length, 0);
        }

        private void textBoxSPFL_OnSelectValue(object sender, EventArgs e)
        {
            AisinoMultiCombox combox = sender as AisinoMultiCombox;
            if (combox != null)
            {
                Dictionary<string, string> selectDict = combox.SelectDict;
                this.comBoxSPFL.Text = selectDict["BM"].ToString();
                this.SPFLMC.Text = selectDict["MC"].ToString();
                this.ComBoxSPFL_OnAutoComplate(this.comBoxSPFL, e);
            }
        }

        private void textBoxWaitMC_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.textBoxWaitMC.SelectionStart;
            this.textBoxWaitMC.Text = StringUtils.GetSubString(this.textBoxWaitMC.Text, 40);
            this.textBoxWaitMC.SelectionStart = selectionStart;
        }

        private void textBoxWaitMC_TextChangedWaitGetText(object sender, GetTextEventArgs e)
        {
            int length = StringUtils.GetSpellCode(this.textBoxWaitMC.Text.Trim()).Length;
        }

        private void toolStripBtnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SimpleValidated())
                {
                    this.InModel();
                    string str = this.clManager.AddCustomer(this.clModel);
                    if (str == "0")
                    {
                        this.father.RefreshGrid();
                        this.textBoxBM.Text = this.clManager.TuiJianBM(this.textBoxSJBM.Text);
                        this.textBoxCPXH.Text = "";
                        this.textBoxWaitMC.Text = "";
                        this.textBoxCD.Text = "";
                        this.textBoxCD.Focus();
                    }
                    else
                    {
                        this.log.Info("车辆增加失败" + str);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBoxBM_Validating() && this.SimpleValidated())
                {
                    if (!this.isUpdate)
                    {
                        this.InModel();
                        string str = this.clManager.AddCustomer(this.clModel);
                        if (str == "0")
                        {
                            this.retCode = this.clModel.BM;
                            if (this.SucDialog)
                            {
                                MessageManager.ShowMsgBox("INP-235201");
                            }
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            this.log.Info("车辆增加失败" + str);
                        }
                    }
                    else
                    {
                        this.InModel();
                        string str2 = this.clManager.ModifyCustomer(this.clModel, this.yuanBM);
                        if (str2 == "0")
                        {
                            this.retCode = this.clModel.BM;
                            if (this.SucDialog)
                            {
                                MessageManager.ShowMsgBox("INP-235303");
                            }
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                        }
                        else if (str2 == "e1")
                        {
                            MessageManager.ShowMsgBox("INP-235108");
                        }
                        else
                        {
                            this.log.Info("车辆增加失败" + str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private List<TreeNodeTemp> treeviewneed_getListNodes(string ParentBM)
        {
            return this.clManager.listNodes(ParentBM);
        }
    }
}

