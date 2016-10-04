namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class ApplyAddressList : DockForm
    {
        private Button btn_Indent;
        private AisinoBTN btn_InsertUpdate;
        private AisinoBTN btn_Reset;
        private IContainer components;
        private CustomStyleDataGrid csdgAddress;
        private AddressInfo existedAddr = new AddressInfo();
        private ILog loger = LogUtil.GetLogger<ApplyVolume>();
        public static AddressInfo outAddr = new AddressInfo();
        private AisinoPNL pnl_Edit;
        private readonly ILYGL_PSXX psxxDal = BLLFactory.CreateInstant<ILYGL_PSXX>("LYGL_PSXX");
        private AisinoRTX rtxt_Memo;
        private ToolStripButton tool_Choose;
        private ToolStripButton tool_Close;
        private ToolStripButton tool_Default;
        private ToolStripButton tool_Delete;
        private ToolStripButton tool_Insert;
        private ToolStripButton tool_Update;
        private ToolStrip toolStrip;
        private AisinoTXT txt_Address;
        private AisinoTXT txt_Cellphone;
        private AisinoTXT txt_Landline;
        private AisinoTXT txt_Postcode;
        private AisinoTXT txt_Receiver;
        protected XmlComponentLoader xmlComponentLoader1;

        public ApplyAddressList()
        {
            this.Initialize();
            this.btn_InsertUpdate.Text = "新增";
            this.btn_Indent.Text = "▼";
            this.pnl_Edit.Visible = false;
            this.BindData();
        }

        private void BindData()
        {
            List<AddressInfo> addrInfoList = new List<AddressInfo>();
            List<bool> isDefaultList = new List<bool>();
            this.psxxDal.SelectAddrInfos(out addrInfoList, out isDefaultList);
            DataTable table = new DataTable();
            table.Columns.Add("IsDefault", typeof(string));
            table.Columns.Add("SJR", typeof(string));
            table.Columns.Add("DZ", typeof(string));
            table.Columns.Add("YDDH", typeof(string));
            table.Columns.Add("GDDH", typeof(string));
            table.Columns.Add("YB", typeof(string));
            table.Columns.Add("BZ", typeof(string));
            if ((addrInfoList != null) && (addrInfoList.Count > 0))
            {
                for (int i = 0; i < addrInfoList.Count; i++)
                {
                    DataRow row = table.NewRow();
                    row["IsDefault"] = isDefaultList[i] ? "是" : string.Empty;
                    row["SJR"] = addrInfoList[i].receiverName;
                    row["DZ"] = addrInfoList[i].address;
                    row["YDDH"] = addrInfoList[i].cellphone;
                    row["GDDH"] = addrInfoList[i].landline;
                    row["YB"] = addrInfoList[i].postcode;
                    row["BZ"] = addrInfoList[i].memo;
                    table.Rows.Add(row);
                }
            }
            this.csdgAddress.DataSource = table;
        }

        private void btn_Indent_Click(object sender, EventArgs e)
        {
            if (this.btn_Indent.Text.Equals("▼"))
            {
                this.btn_Indent.Text = "▲";
                this.pnl_Edit.Visible = true;
            }
            else if (this.btn_Indent.Text.Equals("▲"))
            {
                this.btn_Indent.Text = "▼";
                this.pnl_Edit.Visible = false;
            }
        }

        private void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            if (this.CheckText())
            {
                if (this.btn_InsertUpdate.Text.Equals("新增"))
                {
                    AddressInfo addrInfo = this.CreateAddrInfoFromText();
                    if (this.psxxDal.CheckExist(addrInfo))
                    {
                        MessageManager.ShowMsgBox("INP-4412A1");
                        return;
                    }
                    this.psxxDal.InsertAddrInfo(addrInfo, this.csdgAddress.Rows.Count <= 0);
                }
                else if (this.btn_InsertUpdate.Text.Equals("修改"))
                {
                    AddressInfo info2 = this.CreateAddrInfoFromText();
                    if (this.psxxDal.CheckExist(info2))
                    {
                        MessageManager.ShowMsgBox("INP-4412A1");
                        return;
                    }
                    this.psxxDal.DeleteAddrInfo(this.existedAddr);
                    this.psxxDal.InsertAddrInfo(info2, false);
                }
                this.txt_Receiver.Text = string.Empty;
                this.txt_Address.Text = string.Empty;
                this.txt_Cellphone.Text = string.Empty;
                this.txt_Landline.Text = string.Empty;
                this.txt_Postcode.Text = string.Empty;
                this.rtxt_Memo.Text = string.Empty;
                this.BindData();
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (this.btn_InsertUpdate.Text.Equals("新增"))
            {
                this.txt_Receiver.Text = string.Empty;
                this.txt_Address.Text = string.Empty;
                this.txt_Cellphone.Text = string.Empty;
                this.txt_Landline.Text = string.Empty;
                this.txt_Postcode.Text = string.Empty;
                this.rtxt_Memo.Text = string.Empty;
            }
            else if (this.btn_InsertUpdate.Text.Equals("修改"))
            {
                this.txt_Receiver.Text = this.existedAddr.receiverName;
                this.txt_Address.Text = this.existedAddr.address;
                this.txt_Cellphone.Text = this.existedAddr.cellphone;
                this.txt_Landline.Text = this.existedAddr.landline;
                this.txt_Postcode.Text = this.existedAddr.postcode;
                this.rtxt_Memo.Text = this.existedAddr.memo;
            }
        }

        private bool CheckText()
        {
            if (this.txt_Receiver.Text.Equals(string.Empty))
            {
                MessageManager.ShowMsgBox("INP-441295", new string[] { "收件人姓名", string.Empty });
                return false;
            }
            if (this.txt_Cellphone.Text.Equals(string.Empty))
            {
                MessageManager.ShowMsgBox("INP-441295", new string[] { "移动电话", string.Empty });
                return false;
            }
            if (this.txt_Address.Text.Equals(string.Empty))
            {
                MessageManager.ShowMsgBox("INP-441295", new string[] { "配送地址", string.Empty });
                return false;
            }
            return true;
        }

        private AddressInfo CreateAddrInfoFromGrid()
        {
            DataGridViewRow row = this.csdgAddress.SelectedRows[0];
            return new AddressInfo { receiverName = row.Cells["SJR"].Value.ToString(), address = row.Cells["DZ"].Value.ToString(), cellphone = row.Cells["YDDH"].Value.ToString(), landline = row.Cells["GDDH"].Value.ToString(), postcode = row.Cells["YB"].Value.ToString(), memo = row.Cells["BZ"].Value.ToString() };
        }

        private AddressInfo CreateAddrInfoFromText()
        {
            return new AddressInfo { receiverName = this.txt_Receiver.Text, address = this.txt_Address.Text, cellphone = this.txt_Cellphone.Text, landline = this.txt_Landline.Text, postcode = this.txt_Postcode.Text, memo = this.rtxt_Memo.Text };
        }

        private void csdgAddress_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            outAddr = this.CreateAddrInfoFromGrid();
            base.DialogResult = DialogResult.Yes;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void gridSetting()
        {
            this.csdgAddress.AllowUserToDeleteRows = false;
            this.csdgAddress.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.csdgAddress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.csdgAddress.ReadOnly = true;
            this.csdgAddress.Columns["IsDefault"].FillWeight = 10f;
            this.csdgAddress.Columns["SJR"].FillWeight = 10f;
            this.csdgAddress.Columns["YDDH"].FillWeight = 13f;
            this.csdgAddress.Columns["GDDH"].FillWeight = 13f;
            this.csdgAddress.Columns["YB"].FillWeight = 10f;
            this.csdgAddress.Columns["DZ"].FillWeight = 24f;
            this.csdgAddress.Columns["BZ"].FillWeight = 20f;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip");
            this.tool_Close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Close");
            this.tool_Choose = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Choose");
            this.tool_Default = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Default");
            this.tool_Insert = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Insert");
            this.tool_Update = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Update");
            this.tool_Delete = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Delete");
            this.pnl_Edit = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnl_Edit");
            this.txt_Receiver = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_Receiver");
            this.txt_Cellphone = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_Cellphone");
            this.txt_Landline = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_Landline");
            this.txt_Postcode = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_Postcode");
            this.txt_Address = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_Address");
            this.rtxt_Memo = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtxt_Memo");
            this.btn_InsertUpdate = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_InsertUpdate");
            this.btn_Reset = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_Reset");
            this.btn_Indent = this.xmlComponentLoader1.GetControlByName<Button>("btn_Indent");
            this.csdgAddress = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("csdgList");
            this.tool_Close.Click += new EventHandler(this.tool_Close_Click);
            this.tool_Choose.Click += new EventHandler(this.tool_Choose_Click);
            this.tool_Default.Click += new EventHandler(this.tool_Default_Click);
            this.tool_Insert.Click += new EventHandler(this.tool_Insert_Click);
            this.tool_Update.Click += new EventHandler(this.tool_Update_Click);
            this.tool_Delete.Click += new EventHandler(this.tool_Delete_Click);
            this.btn_Indent.Click += new EventHandler(this.btn_Indent_Click);
            this.btn_InsertUpdate.Click += new EventHandler(this.btn_InsertUpdate_Click);
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.txt_Receiver.TextChanged += new EventHandler(this.txt_Receiver_TextChanged);
            this.txt_Cellphone.TextChanged += new EventHandler(this.txt_Cellphone_TextChanged);
            this.txt_Landline.TextChanged += new EventHandler(this.txt_Landline_TextChanged);
            this.txt_Postcode.TextChanged += new EventHandler(this.txt_Postcode_TextChanged);
            this.txt_Address.TextChanged += new EventHandler(this.txt_Address_TextChanged);
            this.rtxt_Memo.TextChanged += new EventHandler(this.rtxt_Memo_TextChanged);
            this.txt_Receiver.KeyPress += new KeyPressEventHandler(this.txt_Receiver_KeyPress);
            this.txt_Cellphone.KeyPress += new KeyPressEventHandler(this.txt_Cellphone_KeyPress);
            this.txt_Landline.KeyPress += new KeyPressEventHandler(this.txt_Landline_KeyPress);
            this.txt_Postcode.KeyPress += new KeyPressEventHandler(this.txt_Postcode_KeyPress);
            this.txt_Address.KeyPress += new KeyPressEventHandler(this.txt_Address_KeyPress);
            this.rtxt_Memo.KeyPress += new KeyPressEventHandler(this.rtxt_Memo_KeyPress);
            this.csdgAddress.MouseDoubleClick += new MouseEventHandler(this.csdgAddress_MouseDoubleClick);
            this.tool_Close.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.gridSetting();
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ApplyAddressList));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2f1, 0x1f7);
            this.xmlComponentLoader1.TabIndex = 5;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyAddressList\Aisino.Fwkp.Fplygl.Forms.WebWindows.ApplyAddressList.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2f1, 0x1f7);
            base.Controls.Add(this.xmlComponentLoader1);
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Name = "AddressList";
            base.set_TabText("AddressList");
            this.Text = "配送信息";
            base.ResumeLayout(false);
        }

        private void rtxt_Memo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.rtxt_Memo.Text).Length >= 200) && (Encoding.Default.GetBytes(this.rtxt_Memo.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void rtxt_Memo_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = this.rtxt_Memo.SelectionStart;
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.rtxt_Memo.Text);
            if (bytes.Length <= 200)
            {
                length = bytes.Length;
            }
            else
            {
                int num3 = 0;
                int num4 = 0;
                while (num4 < 200)
                {
                    if ((this.rtxt_Memo.Text[num3] >= '一') && (this.rtxt_Memo.Text[num3] <= 0x9fbb))
                    {
                        if (num4 == 0xc7)
                        {
                            break;
                        }
                        num4 += 2;
                    }
                    else
                    {
                        num4++;
                    }
                    num3++;
                }
                length = num4;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.rtxt_Memo.Text = Encoding.Default.GetString(buffer2);
            this.rtxt_Memo.Select(selectionStart, 0);
        }

        private void SetComponentsAttribute(string btnText)
        {
            this.btn_InsertUpdate.Text = btnText;
            this.btn_Indent.Text = "▲";
            this.pnl_Edit.Visible = true;
        }

        private void tool_Choose_Click(object sender, EventArgs e)
        {
            if (this.UserHasChoosen())
            {
                this.csdgAddress_MouseDoubleClick(null, null);
            }
        }

        private void tool_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tool_Default_Click(object sender, EventArgs e)
        {
            if (this.UserHasChoosen())
            {
                AddressInfo oldAddr = this.psxxDal.SelectDefaultAddr();
                AddressInfo addrInfo = this.CreateAddrInfoFromGrid();
                this.psxxDal.UpdateAddrDefault(oldAddr, addrInfo);
                this.BindData();
            }
        }

        private void tool_Delete_Click(object sender, EventArgs e)
        {
            if (this.UserHasChoosen())
            {
                AddressInfo addrInfo = this.CreateAddrInfoFromGrid();
                this.psxxDal.DeleteAddrInfo(addrInfo);
                this.BindData();
            }
        }

        private void tool_Insert_Click(object sender, EventArgs e)
        {
            this.SetComponentsAttribute("新增");
            if (this.btn_Indent.Text.Equals("▼"))
            {
                this.btn_Indent.Text = "▲";
                this.pnl_Edit.Visible = true;
            }
        }

        private void tool_Update_Click(object sender, EventArgs e)
        {
            if (this.UserHasChoosen())
            {
                if (this.btn_Indent.Text.Equals("▼"))
                {
                    this.btn_Indent.Text = "▲";
                    this.pnl_Edit.Visible = true;
                }
                this.SetComponentsAttribute("修改");
                this.existedAddr = this.CreateAddrInfoFromGrid();
                this.txt_Receiver.Text = this.existedAddr.receiverName;
                this.txt_Address.Text = this.existedAddr.address;
                this.txt_Cellphone.Text = this.existedAddr.cellphone;
                this.txt_Landline.Text = this.existedAddr.landline;
                this.txt_Postcode.Text = this.existedAddr.postcode;
                this.rtxt_Memo.Text = this.existedAddr.memo;
            }
        }

        private void txt_Address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Address.Text).Length >= 200) && (Encoding.Default.GetBytes(this.txt_Address.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Address_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Address.Text);
            if (bytes.Length <= 200)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 200)
                {
                    if ((this.txt_Address.Text[num2] >= '一') && (this.txt_Address.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0xc7)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Address.Text = Encoding.Default.GetString(buffer2);
        }

        private void txt_Cellphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Cellphone.Text).Length >= 20) && (Encoding.Default.GetBytes(this.txt_Cellphone.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Cellphone_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Cellphone.Text);
            if (bytes.Length <= 20)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 20)
                {
                    if ((this.txt_Cellphone.Text[num2] >= '一') && (this.txt_Cellphone.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0x13)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Cellphone.Text = Encoding.Default.GetString(buffer2);
        }

        private void txt_Landline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Landline.Text).Length >= 20) && (Encoding.Default.GetBytes(this.txt_Landline.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Landline_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Landline.Text);
            if (bytes.Length <= 20)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 20)
                {
                    if ((this.txt_Landline.Text[num2] >= '一') && (this.txt_Landline.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0x13)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Landline.Text = Encoding.Default.GetString(buffer2);
        }

        private void txt_Postcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Postcode.Text).Length >= 10) && (Encoding.Default.GetBytes(this.txt_Postcode.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Postcode_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Postcode.Text);
            if (bytes.Length <= 10)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 10)
                {
                    if ((this.txt_Postcode.Text[num2] >= '一') && (this.txt_Postcode.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 9)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Postcode.Text = Encoding.Default.GetString(buffer2);
        }

        private void txt_Receiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("\b"))
            {
                e.Handled = false;
            }
            else if ((Encoding.Default.GetBytes(this.txt_Receiver.Text).Length >= 50) && (Encoding.Default.GetBytes(this.txt_Receiver.SelectedText).Length <= 0))
            {
                e.Handled = true;
            }
        }

        private void txt_Receiver_TextChanged(object sender, EventArgs e)
        {
            int length = 0;
            byte[] bytes = Encoding.Default.GetBytes(this.txt_Receiver.Text);
            if (bytes.Length <= 50)
            {
                length = bytes.Length;
            }
            else
            {
                int num2 = 0;
                int num3 = 0;
                while (num3 < 50)
                {
                    if ((this.txt_Receiver.Text[num2] >= '一') && (this.txt_Receiver.Text[num2] <= 0x9fbb))
                    {
                        if (num3 == 0x31)
                        {
                            break;
                        }
                        num3 += 2;
                    }
                    else
                    {
                        num3++;
                    }
                    num2++;
                }
                length = num3;
            }
            byte[] buffer2 = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer2[i] = bytes[i];
            }
            this.txt_Receiver.Text = Encoding.Default.GetString(buffer2);
        }

        private bool UserHasChoosen()
        {
            if (this.csdgAddress.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-441298");
                return false;
            }
            return true;
        }
    }
}

