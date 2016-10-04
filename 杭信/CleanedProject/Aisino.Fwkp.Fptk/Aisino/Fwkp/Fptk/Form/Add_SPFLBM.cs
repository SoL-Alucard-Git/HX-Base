namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Framework.Dao;
    public class Add_SPFLBM : DockForm
    {
        protected AisinoMultiCombox _spmcBt;
        protected AisinoButton button1;
        protected AisinoButton button2;
        private IContainer components;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn FLBM;
        private Invoice fpxx;
        public bool isTsfp;
        private Label label1;
        private DataGridViewTextBoxColumn SLV;
        private DataGridViewTextBoxColumn SPMC;
        private Dictionary<string, SPNode> spxxmap;
        private DataGridViewTextBoxColumn XSYH;
        private ComboBox XSYH_com;
        private DataGridViewTextBoxColumn YHZC;
        private ComboBox YHZC_com;

        public Add_SPFLBM(Invoice fp, bool isTsfp)
        {
            this.isTsfp = isTsfp;
            this.fpxx = fp;
            this.spxxmap = new Dictionary<string, SPNode>();
            this.getmap();
            this.InitializeComponent();
            this.dataGridView1.Controls.Add(this._spmcBt);
            this.dataGridView1.Controls.Add(this.YHZC_com);
            this.dataGridView1.Controls.Add(this.XSYH_com);
            this.XSYH_com.Items.Add("是");
            this.XSYH_com.Items.Add("否");
            this._spmcBt.buttonStyle=0;
            this._spmcBt.MaxLength=0x13;
            this._spmcBt.OnButtonClick += new EventHandler(this._spmcBt_Click);
            this._spmcBt.Leave += new EventHandler(this._spmcBt_Leave);
            this.YHZC_com.SelectedIndexChanged += new EventHandler(this.YHZC_Select);
            this.XSYH_com.SelectedIndexChanged += new EventHandler(this.XSYH_Select);
            this._spmcBt.KeyPress += new KeyPressEventHandler(this._spmcBt_keypress);
            this._spmcBt.PreviewKeyDown += new PreviewKeyDownEventHandler(this._spmcBt_PreviewKeyDown);
        }

        private void _spmcBt_Click(object sender, EventArgs e)
        {
            this.select_FLBM();
        }

        public void _spmcBt_keypress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void _spmcBt_Leave(object sender, EventArgs e)
        {
            if (this._spmcBt.Text != "")
            {
                string text = this._spmcBt.Text;
                object[] objArray1 = new object[] { text };
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", objArray1);
                int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
                string slv = this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value.ToString();
                string mc = this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value.ToString();
                if (this.FlbmCanUse(text, mc))
                {
                    string str4 = mc + "#%" + slv;
                    string sqSLv = this.fpxx.GetSqSLv();
                    if ((objArray == null) || ((objArray[0] as DataTable).Rows.Count == 0))
                    {
                        string[] textArray1 = new string[] { "输入的分类编码不可用！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray1);
                        this._spmcBt.Text = "";
                        this.spxxmap[str4].xsyh = false;
                        this.spxxmap[str4].xsyh_canuse = false;
                        this.spxxmap[str4].flbm = "";
                        this.spxxmap[str4].lslvbs = "";
                        this.spxxmap[str4].yhzc = "";
                        this.dataGridView1.Rows[rowIndex].Cells["XSYH"].Value = "否";
                        this.dataGridView1.Rows[rowIndex].Cells["YHZC"].Value = "";
                        this.dataGridView1.Rows[rowIndex].Cells["FLBM"].Value = "";
                    }
                    else if (this.spxxmap[str4].flbm != text)
                    {
                        if (slv == "0.00")
                        {
                            this.spxxmap[str4].lslvbs = "3";
                        }
                        if ((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0))
                        {
                            this.spxxmap[str4].flbm = text;
                            this.dataGridView1.Rows[rowIndex].Cells["FLBM"].Value = text;
                            string[] separator = new string[] { "、", ",", "，" };
                            string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            this.spxxmap[str4].xsyh = false;
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                if (this.yhzc_contain_slv(strArray[i], slv, false, this.isTsfp))
                                {
                                    this.YHZC_com.Items.Add(strArray[i]);
                                }
                            }
                        }
                        this.spxxmap[str4].xsyh_canuse = false;
                        if ((this.YHZC_com.Items.Count > 0) && sqSLv.Contains(slv))
                        {
                            this.spxxmap[str4].xsyh_canuse = true;
                        }
                    }
                }
            }
        }

        private void _spmcBt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.select_FLBM();
            }
        }

        private void Add_SPFLBM_Load(object sender, EventArgs e)
        {
            int num = 0;
            this.dataGridView1.ReadOnly = true;
            this.YHZC_com.DropDownStyle = ComboBoxStyle.DropDownList;
            this.XSYH_com.DropDownStyle = ComboBoxStyle.DropDownList;
            using (Dictionary<string, SPNode>.KeyCollection.Enumerator enumerator = this.spxxmap.Keys.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    num = this.dataGridView1.Rows.Add();
                    string[] separator = new string[] { "#%" };
                    string[] strArray = enumerator.Current.Split(separator, StringSplitOptions.None);
                    this.dataGridView1.Rows[num].Cells["SPMC"].Value = strArray[0];
                    this.dataGridView1.Rows[num].Cells["SLV"].Value = strArray[1];
                    this.dataGridView1.Rows[num].Cells["XSYH"].Value = "否";
                }
            }
            if (this.spxxmap.Keys.Count <= 10)
            {
                this.dataGridView1.Height = 0x12b;
            }
            base.Height = this.dataGridView1.Height + 0x7d;
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string str in this.spxxmap.Keys)
            {
                if (this.spxxmap[str].flbm == "")
                {
                    string[] textArray1 = new string[] { "仍有未赋码的明细信息！" };
                    MessageManager.ShowMsgBox("INP-242185", textArray1);
                    return;
                }
            }
            foreach (string str2 in this.spxxmap.Keys)
            {
                foreach (int num in this.spxxmap[str2].mat_list)
                {
                    this.fpxx.SetFlbm(num, this.spxxmap[str2].flbm.PadRight(0x13, '0'));
                    this.fpxx.SetYhsm(num, this.spxxmap[str2].yhzc);
                    if (this.spxxmap[str2].yhzc == "")
                    {
                        this.fpxx.SetXsyh(num, "0");
                    }
                    else
                    {
                        this.fpxx.SetXsyh(num, "1");
                    }
                    this.fpxx.SetLslvbs(num, this.spxxmap[str2].lslvbs);
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell != null)
            {
                AisinoMultiCombox combox = this.dataGridView1.Controls["_spmcBt"] as AisinoMultiCombox;
                ComboBox box = this.dataGridView1.Controls["YHZC_com"] as ComboBox;
                ComboBox box2 = this.dataGridView1.Controls["XSYH_com"] as ComboBox;
                int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
                string str = "";
                string slv = (this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value.ToString();
                str = ((this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value.ToString()) + "#%" + slv;
                if (rowIndex >= 0)
                {
                    DataGridViewColumn owningColumn = this.dataGridView1.CurrentCell.OwningColumn;
                    if (owningColumn.Name.Equals("FLBM"))
                    {
                        int index = owningColumn.Index;
                        int num3 = this.dataGridView1.CurrentCell.RowIndex;
                        Rectangle rectangle = this.dataGridView1.GetCellDisplayRectangle(index, num3, false);
                        combox.Left = rectangle.Left;
                        combox.Top = rectangle.Top;
                        combox.Width = rectangle.Width;
                        combox.Height = rectangle.Height;
                        combox.Visible = true;
                        if (this.spxxmap[str].flbm != "")
                        {
                            this._spmcBt.Text = this.spxxmap[str].flbm;
                        }
                        else
                        {
                            this._spmcBt.Text = "";
                        }
                    }
                    else
                    {
                        combox.Visible = false;
                    }
                    if (owningColumn.Name.Equals("XSYH"))
                    {
                        int columnIndex = owningColumn.Index;
                        int num5 = this.dataGridView1.CurrentCell.RowIndex;
                        Rectangle rectangle2 = this.dataGridView1.GetCellDisplayRectangle(columnIndex, num5, false);
                        box2.Left = rectangle2.Left;
                        box2.Top = rectangle2.Top;
                        box2.Width = rectangle2.Width;
                        box2.Height = rectangle2.Height;
                        box2.Visible = true;
                        box2.Text = this.dataGridView1.CurrentCell.Value.ToString();
                        if (this.spxxmap[str].xsyh)
                        {
                            box2.SelectedIndex = 0;
                        }
                        else
                        {
                            box2.SelectedIndex = 1;
                        }
                        if (this.spxxmap[str].xsyh_canuse)
                        {
                            box2.Enabled = true;
                        }
                        else
                        {
                            box2.Enabled = false;
                        }
                    }
                    else
                    {
                        box2.Visible = false;
                    }
                    if (owningColumn.Name.Equals("YHZC"))
                    {
                        int num6 = owningColumn.Index;
                        int num7 = this.dataGridView1.CurrentCell.RowIndex;
                        Rectangle rectangle3 = this.dataGridView1.GetCellDisplayRectangle(num6, num7, false);
                        box.Left = rectangle3.Left;
                        box.Top = rectangle3.Top;
                        box.Width = rectangle3.Width;
                        box.Height = rectangle3.Height;
                        box.Visible = true;
                        if (this.spxxmap[str].xsyh)
                        {
                            box.Items.Clear();
                            object[] objArray1 = new object[] { this.spxxmap[str].flbm };
                            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", objArray1);
                            if ((objArray != null) && ((objArray[0] as DataTable).Rows.Count > 0))
                            {
                                string[] separator = new string[] { "、", ",", "，" };
                                string[] strArray = (objArray[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < strArray.Length; i++)
                                {
                                    if (this.yhzc_contain_slv(strArray[i], slv, false, this.isTsfp))
                                    {
                                        this.YHZC_com.Items.Add(strArray[i]);
                                    }
                                }
                            }
                            box.Text = this.spxxmap[str].yhzc;
                        }
                        else
                        {
                            box.Text = "";
                            box.Items.Clear();
                        }
                    }
                    else
                    {
                        box.Visible = false;
                    }
                }
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

        private bool FlbmCanUse(string flbm, string mc)
        {
            bool flag = this.isXT(mc);
            object[] objArray1 = new object[] { flbm, true, flag };
            if (!bool.Parse(ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", objArray1)[0].ToString()))
            {
                this._spmcBt.Text = "";
                string[] textArray1 = new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" };
                MessageManager.ShowMsgBox("INP-242207", textArray1);
                return false;
            }
            return true;
        }

        private bool getmap()
        {
            try
            {
                List<Dictionary<SPXX, string>> spxxs = this.fpxx.GetSpxxs();
                for (int i = 0; i < spxxs.Count; i++)
                {
                    string key = spxxs[i][(SPXX)0].ToString() + "#%" + spxxs[i][(SPXX)8].ToString();
                    if (!this.spxxmap.ContainsKey(key))
                    {
                        SPNode node = new SPNode {
                            mat_list = { i }
                        };
                        this.spxxmap.Add(key, node);
                    }
                    else
                    {
                        this.spxxmap[key].mat_list.Add(i);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new DataGridView();
            this.SPMC = new DataGridViewTextBoxColumn();
            this.FLBM = new DataGridViewTextBoxColumn();
            this.XSYH = new DataGridViewTextBoxColumn();
            this.YHZC = new DataGridViewTextBoxColumn();
            this.SLV = new DataGridViewTextBoxColumn();
            this._spmcBt = new AisinoMultiCombox();
            this.XSYH_com = new ComboBox();
            this.YHZC_com = new ComboBox();
            this.label1 = new Label();
            this.button1 = new AisinoButton();
            this.button2 = new AisinoButton();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.SPMC, this.FLBM, this.XSYH, this.YHZC, this.SLV };
            this.dataGridView1.Columns.AddRange(dataGridViewColumns);
            this.dataGridView1.Location = new Point(13, 0x4a);
            this.dataGridView1.Margin = new Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x292, 0x170);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CurrentCellChanged += new EventHandler(this.dataGridView1_CurrentCellChanged);
            this.SPMC.HeaderText = "名称";
            this.SPMC.Name = "SPMC";
            this.SPMC.Width = 130;
            this.FLBM.HeaderText = "税收分类编码";
            this.FLBM.Name = "FLBM";
            this.FLBM.Width = 170;
            this.XSYH.HeaderText = "是否享受优惠政策";
            this.XSYH.Name = "XSYH";
            this.XSYH.Width = 130;
            this.YHZC.HeaderText = "优惠政策";
            this.YHZC.Name = "YHZC";
            this.YHZC.Width = 130;
            this.SLV.HeaderText = "税率";
            this.SLV.Name = "SLV";
            this.SLV.Width = 0x37;
            this._spmcBt.AutoComplate = 0;
            this._spmcBt.AutoIndex=1;
            this._spmcBt.BorderColor=Color.Transparent;
            this._spmcBt.BorderStyle = AisinoComboxBorderStyle.System;
            this._spmcBt.ButtonAutoHide=true;
            this._spmcBt.buttonStyle = ButtonStyle.Select;
            this._spmcBt.DataSource=null;
            this._spmcBt.DrawHead=true;
            this._spmcBt.Edit=EditStyle.TextBox;
            this._spmcBt.IsSelectAll=false;
            this._spmcBt.Location = new Point(0, 0);
            this._spmcBt.MaxIndex=8;
            this._spmcBt.MaxLength=0x7fff;
            this._spmcBt.Name = "_spmcBt";
            this._spmcBt.ReadOnly=false;
            this._spmcBt.SelectedIndex=-1;
            this._spmcBt.SelectionStart=0;
            this._spmcBt.ShowText="";
            this._spmcBt.Size = new Size(0x7a, 0x15);
            this._spmcBt.TabIndex = 0;
            this._spmcBt.UnderLineColor=Color.Transparent;
            this._spmcBt.UnderLineStyle=0;
            this.XSYH_com.Location = new Point(0, 0);
            this.XSYH_com.Name = "XSYH_com";
            this.XSYH_com.Size = new Size(0x79, 20);
            this.XSYH_com.TabIndex = 0;
            this.YHZC_com.Location = new Point(0, 0);
            this.YHZC_com.Name = "YHZC_com";
            this.YHZC_com.Size = new Size(0x79, 20);
            this.YHZC_com.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(13, 0x18);
            this.label1.MaximumSize = new Size(550, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20c, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在复制含有无税收分类编码信息的发票！以下明细没有添加税收分类编码，请先设置分类编码。";
            this.button1.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.button1.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.button1.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.button1.Font = new Font("宋体", 9f);
            this.button1.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.button1.Imagesize=0x19;
            this.button1.Location = new Point(0x254, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.BackColorActive=Color.FromArgb(0x19, 0x76, 210);
            this.button2.ColorDefaultA=Color.FromArgb(0, 0xac, 0xfb);
            this.button2.ColorDefaultB=Color.FromArgb(0, 0x91, 0xe0);
            this.button2.Font = new Font("宋体", 9f);
            this.button2.FontColor=Color.FromArgb(0xff, 0xff, 0xff);
            this.button2.Imagesize=0x19;
            this.button2.Location = new Point(0x254, 0x2c);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2ac, 0x1c1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dataGridView1);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Add_SPFLBM";
            this.Text = "批量添加税收分类编码";
            base.Load += new EventHandler(this.Add_SPFLBM_Load);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool isXT(string mc)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MC", mc);
            return (BaseDAOFactory.GetBaseDAOSQLite().querySQL("aisino.fwkp.fptk.selectXThash", dictionary).Count > 0);
        }

        public void select_FLBM()
        {
            int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            string slv = this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value.ToString();
            string mc = this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value.ToString();
            this.YHZC_com.Items.Clear();
            this.XSYH_com.Enabled = false;
            object[] objArray1 = new object[] { "", this.isXT(mc) };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.SPFLSelect", objArray1);
            if ((objArray != null) && (objArray[0].ToString().Trim() != ""))
            {
                string str3 = objArray[0].ToString().Trim();
                this.dataGridView1.Rows[rowIndex].Cells["FLBM"].Value = str3;
                string str4 = mc + "#%" + slv;
                string sqSLv = this.fpxx.GetSqSLv();
                this._spmcBt.Text = str3;
                this.spxxmap[str4].flbm = str3;
                object[] objArray3 = new object[] { str3 };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", objArray3);
                if (slv == "0.00")
                {
                    this.spxxmap[str4].lslvbs = "3";
                }
                if ((objArray2 != null) && ((objArray2[0] as DataTable).Rows.Count > 0))
                {
                    string[] separator = new string[] { "、", ",", "，" };
                    string[] strArray = (objArray2[0] as DataTable).Rows[0]["ZZSTSGL"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    this.spxxmap[str4].xsyh = false;
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (this.yhzc_contain_slv(strArray[i], slv, false, this.isTsfp))
                        {
                            this.YHZC_com.Items.Add(strArray[i]);
                        }
                    }
                }
                this.spxxmap[str4].xsyh_canuse = false;
                if ((this.YHZC_com.Items.Count > 0) && sqSLv.Contains(slv))
                {
                    this.spxxmap[str4].xsyh_canuse = true;
                }
                this.XSYH_com.SelectedIndex = 1;
            }
        }

        private void showdatagrid(int rowidx)
        {
            string str = (this.dataGridView1.Rows[rowidx].Cells["SLV"].Value == null) ? "" : this.dataGridView1.Rows[rowidx].Cells["SLV"].Value.ToString();
            string str2 = ((this.dataGridView1.Rows[rowidx].Cells["SPMC"].Value == null) ? "" : this.dataGridView1.Rows[rowidx].Cells["SPMC"].Value.ToString()) + "#%" + str;
            this.dataGridView1.Rows[rowidx].Cells["FLBM"].Value = this.spxxmap[str2].flbm;
            if (this.spxxmap[str2].xsyh)
            {
                this.dataGridView1.Rows[rowidx].Cells["XSYH"].Value = "是";
                this.dataGridView1.Rows[rowidx].Cells["YHZC"].Value = this.spxxmap[str2].yhzc;
            }
            else
            {
                this.dataGridView1.Rows[rowidx].Cells["XSYH"].Value = "否";
                this.dataGridView1.Rows[rowidx].Cells["YHZC"].Value = "";
            }
        }

        private void XSYH_Select(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            string str = "";
            string str2 = (this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value.ToString();
            str = ((this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value.ToString()) + "#%" + str2;
            if (this.XSYH_com.SelectedItem.ToString() == "是")
            {
                this.spxxmap[str].xsyh = true;
            }
            else
            {
                this.spxxmap[str].xsyh = false;
                this.spxxmap[str].yhzc = "";
            }
            this.showdatagrid(rowIndex);
        }

        public bool yhzc_contain_slv(string yhzc, string slv, bool flag, bool isTsfp)
        {
            string str = "aisino.fwkp.fptk.SelectYhzcs";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ArrayList list = BaseDAOFactory.GetBaseDAOSQLite().querySQL(str, dictionary);
            if (isTsfp)
            {
                foreach (Dictionary<string, object> dictionary2 in list)
                {
                    if ((dictionary2["YHZCMC"].ToString() == yhzc) && (dictionary2["SLV"].ToString() == ""))
                    {
                        return true;
                    }
                }
                return false;
            }
            if ((slv == "免税") || (slv == "不征税"))
            {
                slv = "0%";
            }
            else if (!flag)
            {
                slv = ((double.Parse(slv) * 100.0)).ToString() + "%";
            }
            foreach (Dictionary<string, object> dictionary3 in list)
            {
                string[] separator = new string[] { "、", ",", "，" };
                string[] source = dictionary3["SLV"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == "1.5%_5%")
                    {
                        source[i] = "1.5%";
                    }
                }
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && (dictionary3["SLV"].ToString() == ""))
                {
                    return true;
                }
                if ((dictionary3["YHZCMC"].ToString() == yhzc) && source.Contains<string>(slv))
                {
                    return true;
                }
            }
            return false;
        }

        private void YHZC_Select(object sender, EventArgs e)
        {
            int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
            string str = "";
            string str2 = (this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SLV"].Value.ToString();
            str = ((this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value == null) ? "" : this.dataGridView1.Rows[rowIndex].Cells["SPMC"].Value.ToString()) + "#%" + str2;
            this.spxxmap[str].yhzc = this.YHZC_com.SelectedItem.ToString();
            this.dataGridView1.Rows[rowIndex].Cells["YHZC"].Value = this.YHZC_com.SelectedItem.ToString();
            if (this.spxxmap[str].yhzc == "出口零税")
            {
                this.spxxmap[str].lslvbs = "0";
            }
            else if (this.spxxmap[str].yhzc == "免税")
            {
                this.spxxmap[str].lslvbs = "1";
            }
            else if (this.spxxmap[str].yhzc == "不征税")
            {
                this.spxxmap[str].lslvbs = "2";
            }
        }
    }
}

