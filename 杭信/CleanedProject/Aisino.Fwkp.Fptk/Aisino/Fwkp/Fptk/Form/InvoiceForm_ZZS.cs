namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Http;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.CommonLibrary;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Fptk.Properties;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class InvoiceForm_ZZS : InvoiceForm
    {
        private string[] all_slv_list;
        private string blueJe;
        private string CAFileName;
        private string CAPassWord;
        private AisinoMultiCombox com_fhr;
        private AisinoMultiCombox com_gfdzdh;
        private AisinoMultiCombox com_gfmc;
        private AisinoMultiCombox com_gfsbh;
        private AisinoMultiCombox com_gfzh;
        private AisinoMultiCombox com_skr;
        private AisinoMultiCombox com_xfdzdh;
        private AisinoMultiCombox com_xfmc;
        private AisinoMultiCombox com_xfsbh;
        private AisinoMultiCombox com_xfzh;
        private AisinoCMB com_yplx;
        private string djfile;
        private string fileTaxCode;
        private IFpManager fpm;
        private bool have5Slv;
        private bool haveHYSY;
        private bool initSuccess;
        private bool isCopy;
        private bool isdrdjdk;
        private bool isFPCopy;
        private bool IsHYXXB;
        private bool isHzwm;
        private bool isNcpsgfp;
        private bool isSnyZyfp;
        private bool isYD;
        private AisinoLBL lab_fpdm;
        private AisinoLBL lab_fphm;
        private AisinoLBL lab_hj_je;
        private AisinoLBL lab_hj_jshj;
        private AisinoLBL lab_hj_jshj_dx;
        private AisinoLBL lab_hj_se;
        private AisinoLBL lab_kp;
        private AisinoLBL lab_kprq;
        private AisinoLBL lab_title;
        private AisinoLBL lab_yplx;
        private AisinoLBL lblDq;
        private AisinoLBL lblJYM;
        private AisinoLBL lblNCP;
        private ILog log;
        private AisinoPNL mainPanel;
        private FPLX mFplx;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private object prevCmbSlv;
        private QingDanTianKai qd;
        private const string RLY = "(燃料油)";
        private const string RLY_DDZG = "(燃料油DDZG)";
        private const string SNY = "(石脑油)";
        private const string SNY_DDZG = "(石脑油DDZG)";
        private ToolTip tip;
        private ToolStripMenuItem tool_autoImport;
        private ToolStripMenuItem tool_autokh;
        private ToolStripButton tool_close;
        private ToolStripMenuItem tool_DaoRuHZTZD;
        private ToolStripButton tool_dkdjdr;
        private ToolStripButton tool_dkdr;
        private ToolStripMenuItem tool_drgp;
        private ToolStripButton tool_fanlan;
        private ToolStripButton tool_fushu;
        private ToolStripDropDownButton tool_fushu1;
        private ToolStripButton tool_fuzhi;
        private ToolStripDropDownButton tool_import;
        private ToolStripMenuItem tool_imputSet;
        private ToolStripDropDownButton tool_kehu;
        private ToolStripMenuItem tool_manualImport;
        private ToolStripMenuItem tool_manukh;
        private ToolStripButton tool_print;
        private ToolStripMenuItem tool_zjkj;
        private ToolStripButton tool_zuofei;
        private ToolStrip toolStrip3;
        private AisinoTXT txt_bz;
        private const string XT = "稀土";
        private CheckBox YD_checkBox;
        private ZYFP_LX Zyfplx;

        internal InvoiceForm_ZZS(FPLX fplx, string fpdm, string fphm)
        {
            this.tip = new ToolTip();
            this.blueJe = string.Empty;
            this.log = LogUtil.GetLogger<InvoiceForm_ZZS>();
            this.djfile = "";
            this.initSuccess = true;
            this.fileTaxCode = "";
            this.CAFileName = "";
            this.CAPassWord = "";
            this.all_slv_list = new string[] { string.Empty };
            this.InvoiceForm_Init(fplx, fpdm, fphm);
        }

        internal InvoiceForm_ZZS(FPLX fplx, ZYFP_LX zyfplx, string fpdm, string fphm)
        {
            this.tip = new ToolTip();
            this.blueJe = string.Empty;
            this.log = LogUtil.GetLogger<InvoiceForm_ZZS>();
            this.djfile = "";
            this.initSuccess = true;
            this.fileTaxCode = "";
            this.CAFileName = "";
            this.CAPassWord = "";
            this.all_slv_list = new string[] { string.Empty };
            if ((int)fplx == 0)
            {
                if (((int)zyfplx == 0) && !base.TaxCardInstance.QYLX.ISZYFP)
                {
                    string[] textArray2 = new string[] { " 无增值税专用发票授权。" };
                    MessageManager.ShowMsgBox("INP-242115", textArray2);
                    return;
                }
                if (((int)zyfplx == 2) && !base.TaxCardInstance.QYLX.ISSNY)
                {
                    string[] textArray3 = new string[] { " 无石脑油、燃料油增值税专用发票授权。" };
                    MessageManager.ShowMsgBox("INP-242115", textArray3);
                    return;
                }
            }
            else if ((int)fplx == 2)
            {
                if (((int)zyfplx == 0) && !base.TaxCardInstance.QYLX.ISPTFP)
                {
                    string[] textArray4 = new string[] { " 无增值税普通发票授权。" };
                    MessageManager.ShowMsgBox("INP-242114", textArray4);
                    return;
                }
                if (((int)zyfplx == 8) && !base.TaxCardInstance.QYLX.ISNCPXS)
                {
                    string[] textArray5 = new string[] { " 无农产品销售发票授权。" };
                    MessageManager.ShowMsgBox("INP-242158", textArray5);
                    return;
                }
                if (((int)zyfplx == 9) && !base.TaxCardInstance.QYLX.ISNCPSG)
                {
                    string[] textArray6 = new string[] { " 无收购发票授权。" };
                    MessageManager.ShowMsgBox("INP-242159", textArray6);
                    return;
                }
            }
            if (((int)fplx == 0) && ((int)zyfplx == 2))
            {
                this.isSnyZyfp = true;
                this.Zyfplx = 0;
            }
            else if (((int)fplx == 2) && ((int)zyfplx == 9))
            {
                this.isNcpsgfp = true;
                this.Zyfplx = zyfplx;
            }
            else
            {
                this.Zyfplx = zyfplx;
            }
            this.InvoiceForm_Init(fplx, fpdm, fphm);
            this.isCopy = false;
        }

        public InvoiceForm_ZZS(FPLX fplx, string fpdm, string fphm, int flag, byte[] value)
        {
            this.tip = new ToolTip();
            this.blueJe = string.Empty;
            this.log = LogUtil.GetLogger<InvoiceForm_ZZS>();
            this.djfile = "";
            this.initSuccess = true;
            this.fileTaxCode = "";
            this.CAFileName = "";
            this.CAPassWord = "";
            this.all_slv_list = new string[] { string.Empty };
            if (flag == 1)
            {
                string str = "Aisino.Fwkp.Invoice.ActiveX.Invoice_ZZS" + fpdm + fphm;
                byte[] destinationArray = new byte[0x20];
                byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer2 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
                byte[] buffer3 = AES_Crypt.Decrypt(value, destinationArray, buffer2, null);
                DateTime time = DateTime.Parse(Encoding.Unicode.GetString(buffer3));
                DateTime now = DateTime.Now;
                if ((time.CompareTo(now) <= 0) && (time.CompareTo(now.AddSeconds(-1.0)) >= 0))
                {
                    this.InvoiceForm_Init(fplx, fpdm, fphm);
                }
            }
        }

        protected override bool _AddSpxx(int index)
        {
            if (this.isSnyZyfp)
            {
                CustomStyleDataGrid grid = null;
                if (base._DataGridView_qd == null)
                {
                    grid = base._DataGridView;
                }
                else
                {
                    grid = base._DataGridView_qd;
                }
                int num = 0;
                if (index < 0)
                {
                    num = grid.Rows.Count - 1;
                }
                else
                {
                    num = index;
                }
                base._fpxx.SetJLdw(num, "吨");
                this._ShowDataGrid(grid, base._fpxx.GetSpxx(num), num);
            }
            return base._AddSpxx(index);
        }

        protected override bool _CheckDelSPRow(CustomStyleDataGrid dataGrid)
        {
            if ((dataGrid != null) && (dataGrid.CurrentRow.Index > -1))
            {
                List<Dictionary<SPXX, string>> spxxs = null;
                if (((base._DataGridView_qd == null) && !base._fpxx.Qdbz) || (base._DataGridView_qd != null))
                {
                    spxxs = base._fpxx.GetSpxxs();
                }
                else
                {
                    spxxs = base._fpxx.GetMxxxs();
                }
                if ((spxxs == null) || (spxxs.Count == 0))
                {
                    return true;
                }
                Dictionary<SPXX, string> dictionary = spxxs[dataGrid.CurrentRow.Index];
                if (dictionary != null)
                {
                    string str = dictionary[(SPXX)10];
                    if (str == ((FPHXZ)3).ToString("D"))
                    {
                        return (MessageManager.ShowMsgBox("INP-242151") == DialogResult.Yes);
                    }
                    if (str == ((FPHXZ)4).ToString("D"))
                    {
                        return (MessageManager.ShowMsgBox("INP-242152") == DialogResult.Yes);
                    }
                    if (str == ((FPHXZ)1).ToString("D"))
                    {
                        return (MessageManager.ShowMsgBox("INP-242153") == DialogResult.Yes);
                    }
                    if (str == ((FPHXZ)5).ToString("D"))
                    {
                        MessageManager.ShowMsgBox("INP-242154");
                        return false;
                    }
                }
            }
            return base._CheckDelSPRow(dataGrid);
        }

        protected override void _comboBox_SLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SLV selectedItem = base.comboBox_SLV.SelectedItem as SLV;
            if (selectedItem == null)
            {
                this.log.Error("税率列表类型异常！");
            }
            else
            {
                if (base._ChaeButton.Checked)
                {
                    if (this.prevCmbSlv == null)
                    {
                        this.prevCmbSlv = (SLV)base.comboBox_SLV.Items[1];
                    }
                    string str = ((SLV)this.prevCmbSlv).ToString();
                    if (((selectedItem.ToString() == "减按1.5%计算") || (selectedItem.ToString() == "中外合作油气田")) || (selectedItem.ToString() == ""))
                    {
                        string[] textArray1 = new string[] { "差额税不支持特殊税率商品填开！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray1);
                        this.SetPrevSelSlv();
                        return;
                    }
                    if ((selectedItem.ToString() != str.ToString()) && (base._fpxx.GetSpxxs().Count > 1))
                    {
                        string[] textArray2 = new string[] { "差额税不支持多税率商品填开！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray2);
                        this.SetPrevSelSlv();
                        return;
                    }
                }
                if ((this.isSnyZyfp && (selectedItem.ToString() == "减按1.5%计算")) || (this.isSnyZyfp && (selectedItem.ToString() == "中外合作油气田")))
                {
                    string[] textArray3 = new string[] { "当前发票类型不能填开特殊税率！" };
                    MessageManager.ShowMsgBox("INP-242185", textArray3);
                    this.SetPrevSelSlv();
                }
                else
                {
                    if ((base._fpxx.IsRed && (base._fpxx.Fplx == 0)) && (this.blueJe.Trim() != ""))
                    {
                        bool flag = false;
                        if ((this.prevCmbSlv != null) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)1))
                        {
                            flag = true;
                        }
                        if ((flag && (selectedItem.Zyfplx != (ZYFP_LX)1)) || (!flag && (selectedItem.Zyfplx == (ZYFP_LX)1)))
                        {
                            MessageManager.ShowMsgBox("INP-242173");
                            this.SetPrevSelSlv();
                            return;
                        }
                    }
                    if (base._fpxx.IsRed && (this.blueJe.Trim() != ""))
                    {
                        bool flag2 = false;
                        if ((this.prevCmbSlv != null) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)10))
                        {
                            flag2 = true;
                        }
                        if ((flag2 && (selectedItem.Zyfplx != (ZYFP_LX)10)) || (!flag2 && (selectedItem.Zyfplx == (ZYFP_LX)10)))
                        {
                            string[] textArray4 = new string[] { "红字发票中不能同时开具1.5%税率商品和非1.5%税率商品明细" };
                            MessageManager.ShowMsgBox("INP-242185", textArray4);
                            this.SetPrevSelSlv();
                            return;
                        }
                    }
                    if (((selectedItem.fplx == 0) && (selectedItem.Zyfplx == (ZYFP_LX)1)) || ((base._fpxx.Zyfplx == (ZYFP_LX)1) && (selectedItem.Zyfplx == 0)))
                    {
                        if (base._fpxx.GetSpxxs().Count == 1)
                        {
                            base._fpxx.SetZyfpLx(0);
                        }
                        if (!base._fpxx.SetZyfpLx(selectedItem.Zyfplx))
                        {
                            MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                            this.SetPrevSelSlv();
                            return;
                        }
                    }
                    if ((selectedItem.Zyfplx == (ZYFP_LX)10) || (base._fpxx.Zyfplx == (ZYFP_LX)10))
                    {
                        if (base._fpxx.GetSpxxs().Count == 1)
                        {
                            base._fpxx.SetZyfpLx(0);
                        }
                        if (!base._fpxx.SetZyfpLx(selectedItem.Zyfplx))
                        {
                            MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                            this.SetPrevSelSlv();
                            return;
                        }
                    }
                    base._comboBox_SLV_SelectedIndexChanged(sender, e);
                    if (base._fpxx.GetCode() != "0000")
                    {
                        this.SetPrevSelSlv();
                    }
                    else
                    {
                        CustomStyleDataGrid parent = (CustomStyleDataGrid)((AisinoCMB)sender).Parent;
                        if (((base._fpxx.Fplx == 0) && (selectedItem != null)) && ((selectedItem.fplx == 0) && (selectedItem.Zyfplx == (ZYFP_LX)1)))
                        {
                            this.SetHysyHsjxx(parent, true);
                        }
                        else
                        {
                            this.SetHysyHsjxx(parent, false);
                        }
                        if (base._fpxx.IsRed)
                        {
                            base._SetDataGridReadOnlyColumns(base._DataGridView);
                        }
                        this.prevCmbSlv = base.comboBox_SLV.SelectedItem;
                    }
                }
            }
        }

        protected override void _dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CustomStyleDataGrid grid = (CustomStyleDataGrid)sender;
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;
            object obj2 = grid.Rows[rowIndex].Cells[columnIndex].Value;
            string s = (obj2 == null) ? "" : obj2.ToString();
            bool flag = false;
            switch (columnIndex)
            {
                case 0:
                    if ((s.Trim() == "") || (s.Trim() == "0"))
                    {
                        s = "";
                    }
                    flag = base._fpxx.SetSpmc(rowIndex, s);
                    if (this.isSnyZyfp)
                    {
                        base._fpxx.SetJLdw(rowIndex, "吨");
                    }
                    goto Label_03C1;

                case 1:
                    flag = base._fpxx.SetGgxh(rowIndex, s);
                    goto Label_03C1;

                case 2:
                    flag = base._fpxx.SetJLdw(rowIndex, s);
                    goto Label_03C1;

                case 3:
                    {
                        double num3 = 0;
                        double num4 = 0;
                        if (base._fpxx.Zyfplx != (ZYFP_LX)11)
                        {
                            break;
                        }
                        Dictionary<SPXX, string> spxx = base._fpxx.GetSpxx(rowIndex);
                        if ((!double.TryParse(s, out num3) || !double.TryParse(spxx[(SPXX)5], out num4)) || (Math.Abs((double)(num3 * num4)) >= Math.Abs(double.Parse(spxx[(SPXX)0x18]))))
                        {
                            break;
                        }
                        string[] textArray1 = new string[] { "金额的绝对值小于扣除额！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray1);
                        grid.Rows[rowIndex].Cells[columnIndex].Value = "";
                        grid.Rows[rowIndex].Cells[columnIndex + 2].Value = "";
                        base._fpxx.SetJe(rowIndex, "");
                        return;
                    }
                case 4:
                    {
                        double num5 = 0;
                        double num6 = 0;
                        if (base._fpxx.Zyfplx != (ZYFP_LX)11)
                        {
                            goto Label_02B2;
                        }
                        Dictionary<SPXX, string> dictionary2 = base._fpxx.GetSpxx(rowIndex);
                        if ((!double.TryParse(s, out num6) || !double.TryParse(dictionary2[(SPXX)6], out num5)) || (Math.Abs((double)(num5 * num6)) >= Math.Abs(double.Parse(dictionary2[(SPXX)0x18]))))
                        {
                            goto Label_02B2;
                        }
                        string[] textArray2 = new string[] { "金额的绝对值小于扣除额！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray2);
                        grid.Rows[rowIndex].Cells[columnIndex].Value = "";
                        grid.Rows[rowIndex].Cells[columnIndex + 1].Value = "";
                        base._fpxx.SetJe(rowIndex, "");
                        return;
                    }
                case 5:
                    {
                        double num7;
                        if (base._fpxx.Zyfplx != (ZYFP_LX)11)
                        {
                            goto Label_0346;
                        }
                        Dictionary<SPXX, string> dictionary3 = base._fpxx.GetSpxx(rowIndex);
                        if (!double.TryParse(s, out num7) || (Math.Abs(num7) >= Math.Abs(double.Parse(dictionary3[(SPXX)0x18]))))
                        {
                            goto Label_0346;
                        }
                        string[] textArray3 = new string[] { "金额的绝对值小于扣除额！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray3);
                        grid.Rows[rowIndex].Cells[columnIndex].Value = "";
                        return;
                    }
                case 6:
                    if (base._fpxx.Fplx == 0)
                    {
                        ZYFP_LX zyfp_lx = base._fpxx.Zyfplx;
                        if ((int)zyfp_lx == 1)
                        {
                            flag = base._fpxx.SetZyfpLx(zyfp_lx);
                            if (!flag)
                            {
                                goto Label_03C1;
                            }
                        }
                    }
                    flag = base._fpxx.SetSLv(rowIndex, s);
                    goto Label_03C1;

                case 7:
                    if (s == "")
                    {
                        s = "0";
                    }
                    flag = base._fpxx.SetSe(rowIndex, s);
                    goto Label_03C1;

                default:
                    goto Label_03C1;
            }
            flag = base._fpxx.SetSL(rowIndex, s);
            goto Label_03C1;
        Label_02B2:
            flag = base._fpxx.SetDj(rowIndex, s);
            goto Label_03C1;
        Label_0346:
            flag = base._fpxx.SetJe(rowIndex, s);
        Label_03C1:
            if (!flag)
            {
                MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
            }
            base.cellEndEdit = flag;
            base._dataGridView_CellEndEdit(sender, e);
        }

        protected override void _dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            base.comboBox_SLV.SelectedIndexChanged -= new EventHandler(this._comboBox_SLV_SelectedIndexChanged);
            CustomStyleDataGrid grid = (CustomStyleDataGrid)sender;
            if (grid.CurrentCell != null)
            {
                int rowIndex = grid.CurrentCell.RowIndex;
                base.comboBox_SLV.Visible = true;
                Dictionary<SPXX, string> spxx = base._fpxx.GetSpxx(rowIndex);
                base.comboBox_SLV.Items.Clear();
                base.comboBox_SLV.Items.AddRange(base.GetSlvList(base._fpxx.Fplx));
                if ((rowIndex >= 0) && (grid.Rows[rowIndex].Cells["SLV"].Value != null))
                {
                    base.comboBox_SLV.Text = grid.Rows[rowIndex].Cells["SLV"].Value.ToString();
                }
                if ((grid.CurrentCell.OwningColumn.Name.Equals("SLV") && (rowIndex > -1)) && (spxx != null))
                {
                    this.SLVLIST_CHANGE(rowIndex);
                }
                if (base._fpxx.Fplx == 0)
                {
                    int num2 = 0;
                    foreach (object obj2 in base.comboBox_SLV.Items)
                    {
                        if (((obj2.ToString() == "免税") || (obj2.ToString() == "不征税")) || (obj2.ToString() == "0%"))
                        {
                            num2++;
                        }
                    }
                    int index = 0;
                    foreach (object obj3 in base.comboBox_SLV.Items)
                    {
                        if (((obj3.ToString() == "免税") || (obj3.ToString() == "不征税")) || (obj3.ToString() == "0%"))
                        {
                            break;
                        }
                        index++;
                    }
                    for (int i = 0; i < num2; i++)
                    {
                        base.comboBox_SLV.Items.RemoveAt(index);
                    }
                }
            }
            base._dataGridView_CurrentCellChanged(sender, e);
            base.comboBox_SLV.SelectedIndexChanged += new EventHandler(this._comboBox_SLV_SelectedIndexChanged);
        }

        protected override void _dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CustomStyleDataGrid grid = sender as CustomStyleDataGrid;
            base._dataGridView_RowsRemoved(sender, e);
            bool flag = false;
            if (((base._fpxx.IsRed && (base._fpxx.Fplx == 0)) && ((this.blueJe.Trim() != "") && (this.prevCmbSlv != null))) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)1))
            {
                flag = true;
            }
            if ((!flag && (grid != null)) && ((grid.Rows.Count == 0) && (base._fpxx.GetMxxxs().Count == 0)))
            {
                this.SetHysyHsjxx(grid, false);
            }
        }

        protected override void _FormMain_UpdateUserNameEvent(string yhmc)
        {
            this.lab_kp.Text = yhmc;
            if (base._fpxx != null)
            {
                base._fpxx.Kpr = yhmc;
            }
        }

        protected override Dictionary<FPLX, List<SLV>> _GetSLvList()
        {
            Dictionary<FPLX, List<SLV>> dictionary = base._GetSLvList();
            string sqSLv = "";
            if (base._fpxx != null)
            {
                sqSLv = base._fpxx.GetSqSLv();
                bool flag = false;
                bool flag2 = false;
                char[] separator = new char[] { ';' };
                string[] strArray = sqSLv.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>();
                List<double> source = new List<double>();
                if (strArray.Length >= 1)
                {
                    char[] chArray2 = new char[] { ',' };
                    string[] strArray2 = strArray[0].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                    list.AddRange(strArray2.ToList<string>());
                    flag2 = strArray[0].Contains("0.05") || strArray[0].Contains("0.050");
                }
                if (strArray.Length >= 2)
                {
                    char[] chArray3 = new char[] { ',' };
                    string[] strArray3 = strArray[1].Split(chArray3, StringSplitOptions.RemoveEmptyEntries);
                    list.AddRange(strArray3.ToList<string>());
                    flag = strArray[1].Contains("0.05") || strArray[1].Contains("0.050");
                }
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        double result = 0.0;
                        if (double.TryParse(enumerator.Current, out result))
                        {
                            source.Add(result);
                        }
                    }
                }
                List<SLV> list3 = dictionary[(FPLX)0];
                List<SLV> list4 = dictionary[(FPLX)2];
                list3.Clear();
                list4.Clear();
                //source = source.GroupBy<double, double>((serializeClass.staticFunc_3)).Select<IGrouping<double, double>, double>((serializeClass.staticFunc_4)).ToList<double>();
                for (int i = source.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (source[i] == source[j])
                        {
                            source.Remove(source[i]);
                            break;
                        }
                    }
                }
                source.Sort();
                source.Reverse();
                if (string.IsNullOrEmpty(sqSLv))
                {
                    return dictionary;
                }
                if (base._fpxx.Fplx == 0)
                {
                    foreach (double num2 in source)
                    {
                        if (num2 == 0.05)
                        {
                            if (!this.isSnyZyfp & flag)
                            {
                                list3.Add(new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", ""));
                            }
                            if (flag2)
                            {
                                list3.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                            }
                        }
                        else if (num2 == 0.0)
                        {
                            list3.Add(new SLV(0, 0, "0.00", "0%", "0%"));
                        }
                        else
                        {
                            double num3 = num2 * 100.0;
                            string str2 = num3.ToString() + "%";
                            list3.Add(new SLV(0, 0, num2.ToString(), str2, str2));
                        }
                    }
                    return dictionary;
                }
                if ((int)base._fpxx.Fplx != 2)
                {
                    return dictionary;
                }
                if (this.isNcpsgfp)
                {
                    list4.Add(new SLV((FPLX)2, (ZYFP_LX)9, "0.00", "0%", "0%"));
                    return dictionary;
                }
                foreach (double num4 in source)
                {
                    if ((num4 == 0.05) & flag2)
                    {
                        list3.Add(new SLV((FPLX)0, 0, "0.05", "5%", "5%"));
                    }
                    else if (num4 == 0.0)
                    {
                        list4.Add(new SLV((FPLX)2, 0, "0.00", "0%", "0%"));
                    }
                    else
                    {
                        string str3 = ((num4 * 100.0)).ToString() + "%";
                        list4.Add(new SLV((FPLX)2, 0, num4.ToString(), str3, str3));
                    }
                }
            }
            return dictionary;
        }

        protected override DataTable _GfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,SH,DZDH,YHZH" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        protected override void _GfxxSelect(string value, int type)
        {
            object[] objArray1 = new object[] { value, type, "MC,SH,DZDH,YHZH" };
            object[] khxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKH", objArray1);
            if (khxx != null)
            {
                this.GfxxSetValue(khxx);
            }
        }

        protected override void _GfxxSetValue(Dictionary<string, string> khxx)
        {
            object[] objArray1 = new object[] { khxx["MC"], khxx["SH"], khxx["DZDH"], khxx["YHZH"] };
            this.GfxxSetValue(objArray1);
        }

        protected override void _InitQingdanForm()
        {
            this.qd = new QingDanTianKai(base._onlyShow, base._fpxx.Fpdm, base._fpxx.Fphm, false);
            this.qd.Fplx = base._fpxx.Fplx;
            if (base._fpxx.Zyfplx == (ZYFP_LX)1)
            {
                this.SetHysyHsjxx(this.qd.dataGridView1, true);
            }
            else
            {
                this.SetHysyHsjxx(this.qd.dataGridView1, false);
            }
            base._SetQingdanFormProp(this.qd, this.qd.dataGridView1, this.qd.tool_jg, this.qd.tool_zhekou, this.qd.tool_add, this.qd.tool_remove);
        }

        protected override void _InvoiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            base._InvoiceForm_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter)
            {
                if (this.com_gfsbh.Focused || this.com_gfmc.Focused)
                {
                    if (this.com_gfsbh.Focused)
                    {
                        if (this.com_gfsbh.Text == "")
                        {
                            this._GfxxSelect(this.com_gfsbh.Text, 1);
                        }
                    }
                    else if (this.com_gfmc.Focused && (this.com_gfmc.Text == ""))
                    {
                        this._GfxxSelect(this.com_gfmc.Text, 1);
                    }
                }
                if ((this.fpm.IsSWDK() && this.com_xfdzdh.Focused) && (this.com_xfdzdh.Text == ""))
                {
                    this._XfxxSelect(this.com_xfdzdh.Text, 1);
                }
            }
        }

        protected override void _InvoiceForm_Resize(object sender, EventArgs e)
        {
            if (this.panel1 != null)
            {
                this.panel1.Location = new Point((base.Width - this.panel1.Width) / 2, this.panel1.Location.Y);
            }
        }

        protected override string _SetDefaultSpsLv()
        {
            if ((base._fpxx.IsRed && (this.prevCmbSlv != null)) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)1))
            {
                return ((SLV)this.prevCmbSlv).DataValue;
            }
            return base._SetDefaultSpsLv();
        }

        protected override void _SetHsjxx(CustomStyleDataGrid dataGridView1, bool hsjbz)
        {
            base._SetHsjxx(dataGridView1, hsjbz);
            if (hsjbz)
            {
                base._HsjbzButton.Image = Resources.hanshuijiage_03;
                if (base._DataGridView_qd != null)
                {
                    this.qd.tool_jg.Image = Resources.hanshuijiage_03;
                }
            }
            else
            {
                base._HsjbzButton.Image = Resources.jiage_03;
                if (base._DataGridView_qd != null)
                {
                    this.qd.tool_jg.Image = Resources.jiage_03;
                }
            }
        }

        protected override void _SetHzxx()
        {
            this.lab_hj_je.Text = "￥" + base._fpxx.GetHjJe();
            this.lab_hj_se.Text = "￥" + base._fpxx.GetHjSe();
            this.lab_hj_jshj.Text = "￥" + base._fpxx.GetHjJeHs();
            this.lab_hj_jshj_dx.Text = ToolUtil.RMBToDaXie(decimal.Parse(base._fpxx.GetHjJeHs()));
            this.lab_hj_je.Invalidate();
        }

        protected override string _SetReadOnlyColumns()
        {
            string str = base._SetReadOnlyColumns();
            if (!base._fpxx.IsRed)
            {
                str = str + ",SE";
            }
            if (this.isSnyZyfp)
            {
                str = str + ",JLDW";
            }
            return str;
        }

        protected override void _ShowQingdanForm()
        {
            if (this.qd != null)
            {
                this.qd.ShowDialog();
                this.qd = null;
            }
        }

        public override void _spmcBt_leave(object sender, EventArgs e)
        {
            if (!this.isWM() && (base._spmcBt.Parent != null))
            {
                CustomStyleDataGrid parent = (CustomStyleDataGrid)base._spmcBt.Parent;
                int rowIndex = parent.CurrentCell.RowIndex;
                Dictionary<SPXX, string> spxx = base._fpxx.GetSpxx(rowIndex);
                string str = "";
                if ((spxx != null) && (spxx[0] != null))
                {
                    str = spxx[0];
                    if (this.com_yplx.SelectedItem != null)
                    {
                        string oldValue = this.com_yplx.SelectedItem.ToString();
                        str = str.Replace(oldValue, "");
                    }
                }
                if (base._spmcBt.Text != "")
                {
                    string str3 = this._GetSpmcText();
                    CustomStyleDataGrid grid2 = (base._DataGridView_qd == null) ? base._DataGridView : base._DataGridView_qd;
                    DataTable table = this._SpmcOnAutoCompleteDataSource(grid2, str3);
                    if ((table == null) || (table.Rows.Count == 0))
                    {
                        if ((spxx != null) && ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == "")))
                        {
                            if ((((spxx[0] == null) || (spxx[0] == "")) || (base._fpxx.IsRed && (spxx[0] == "详见对应正数发票及清单"))) || (str == ""))
                            {
                                base._fpxx.SetXsyh(rowIndex, "0");
                            }
                            else
                            {
                                string text = base._spmcBt.Text;
                                object[] objArray = new object[] { text, "", "", false };
                                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                                if (objArray2 == null)
                                {
                                    base._spmcBt.Text = "";
                                }
                                this.SetSpxx(parent, objArray2);
                            }
                        }
                    }
                    else if ((spxx[(SPXX)20] == null) || (spxx[(SPXX)20] == ""))
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((table.Rows[i]["SPFL"].ToString() != "") && (str3 == table.Rows[i]["MC"].ToString()))
                            {
                                object[] objArray7 = new object[] { table.Rows[i]["BM"], table.Rows[i]["MC"], table.Rows[i]["JM"], table.Rows[i]["SLV"], table.Rows[i]["SPSM"], table.Rows[i]["GGXH"], table.Rows[i]["JLDW"], table.Rows[i]["DJ"], table.Rows[i]["HSJBZ"], table.Rows[i]["XTHASH"], table.Rows[i]["HYSY"], table.Rows[i]["SPFL"], table.Rows[i]["YHZC"], table.Rows[i]["SPFL_ZZSTSGL"], table.Rows[i]["YHZC_SLV"], table.Rows[i]["YHZCMC"] };
                                this.SetSpxx(parent, objArray7);
                                return;
                            }
                        }
                        if (str3 == table.Rows[0]["MC"].ToString())
                        {
                            object[] objArray3 = new object[] { table.Rows[0]["MC"], "", table.Rows[0]["BM"], true };
                            object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray3);
                            if (objArray4 == null)
                            {
                                base._spmcBt.Text = "";
                            }
                            this.SetSpxx(parent, objArray4);
                        }
                        else
                        {
                            object[] objArray5 = new object[] { str3, "", "", false };
                            object[] objArray6 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray5);
                            if (objArray6 == null)
                            {
                                base._spmcBt.Text = "";
                            }
                            this.SetSpxx(parent, objArray6);
                        }
                    }
                }
            }
        }

        public override void _spmcBt_OnSelectValue(object sender, EventArgs e)
        {
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            base._spmcBt.Visible = false;
            try
            {
                Dictionary<string, string> dictionary = base._spmcBt.SelectDict;
                CustomStyleDataGrid parent = (base._DataGridView_qd == null) ? base._DataGridView : base._DataGridView_qd;
                if ((dictionary["SPFL"].ToString() == "") && !this.isWM())
                {
                    object[] objArray = new object[] { dictionary["MC"], "", dictionary["BM"], true };
                    object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
                    if (spxx == null)
                    {
                        base._spmcBt.Text = "";
                    }
                    this.SetSpxx(parent, spxx);
                }
                else
                {
                    this._SpmcOnSelectValue(parent, dictionary);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Visible = true;
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        protected override bool _SpmcChangeError()
        {
            return this.isFPCopy;
        }

        protected override DataTable _SpmcOnAutoCompleteDataSource(CustomStyleDataGrid dataGrid, string str)
        {
            double num;
            if ((base._fpxx.Zyfplx == (ZYFP_LX)1) && ((base._fpxx.GetSpxxs().Count > 1) || base._fpxx.IsRed))
            {
                num = 0.05;
            }
            else if (((base._fpxx.GetSpxxs().Count == 1) && (dataGrid.Columns["SLV"].Tag == null)) || base._fpxx.SupportMulti)
            {
                num = -1.0;
            }
            else
            {
                num = double.Parse(base._fpxx.SLv);
            }
            string str2 = "";
            if (this.isSnyZyfp)
            {
                str2 = "SNY";
            }
            else if (base._fpxx.Zyfplx == (ZYFP_LX)1)
            {
                str2 = "HYSY";
            }
            else if (base._fpxx.Zyfplx == (ZYFP_LX)10)
            {
                str2 = "OPF";
            }
            else if ((base._fpxx.Zyfplx == (ZYFP_LX)7) || (base._fpxx.Zyfplx == (ZYFP_LX)6))
            {
                str2 = "XT";
            }
            object[] objArray1 = new object[] { str, 20, "", num, str2, "1" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSPMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                DataTable table = objArray[0] as DataTable;
                if (table.Rows.Count > 0)
                {
                    return table;
                }
            }
            return null;
        }

        protected override void _SpmcOnSelectValue(CustomStyleDataGrid dataGrid, Dictionary<string, string> value)
        {
            object[] spxx = new object[] { value["BM"], value["MC"], value["JM"], value["SLV"], value["SPSM"], value["GGXH"], value["JLDW"], value["DJ"], value["HSJBZ"], value["XTHASH"], value["HYSY"], value["SPFL"], value["YHZC"], value["SPFL_ZZSTSGL"], value["YHZC_SLV"], value["YHZCMC"] };
            this.SetSpxx(dataGrid, spxx);
        }

        protected override void _SpmcSelect(CustomStyleDataGrid parent, int type, int showDisableSLv)
        {
            double num;
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            string text = base._spmcBt.Text;
            if ((base._fpxx.Zyfplx == (ZYFP_LX)1) && ((base._fpxx.GetSpxxs().Count > 1) || base._fpxx.IsRed))
            {
                num = 0.05;
            }
            else if (((base._fpxx.GetSpxxs().Count == 1) && (parent.Columns["SLV"].Tag == null)) || base._fpxx.SupportMulti)
            {
                num = -1.0;
            }
            else
            {
                num = double.Parse(base._fpxx.SLv);
            }
            string str2 = "";
            if (this.isSnyZyfp)
            {
                str2 = "SNY";
            }
            else if (base._fpxx.Zyfplx == (ZYFP_LX)1)
            {
                str2 = "HYSY";
            }
            else if (base._fpxx.Zyfplx == (ZYFP_LX)10)
            {
                str2 = "OPF";
            }
            else if ((base._fpxx.Zyfplx == (ZYFP_LX)7) || (base._fpxx.Zyfplx == (ZYFP_LX)6))
            {
                str2 = "XT";
            }
            object[] objArray = new object[] { text, num, type, showDisableSLv, "", str2 };
            object[] spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetSP", objArray);
            if ((!this.isWM() && (spxx != null)) && ((spxx.Length >= 11) && (spxx[11].ToString() == "")))
            {
                objArray = new object[] { spxx[1].ToString(), "", spxx[0].ToString(), true };
                spxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddSP", objArray);
            }
            this.SetSpxx(parent, spxx);
            base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
        }

        protected override DataTable _XfxxOnAutoCompleteDataSource(string str)
        {
            object[] objArray1 = new object[] { str, 20, "MC,SH,DZDH,YHZH" };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetXHDWMore", objArray1);
            if ((objArray != null) && (objArray.Length != 0))
            {
                return (objArray[0] as DataTable);
            }
            return null;
        }

        protected override void _XfxxSelect(string value, int type)
        {
            object[] objArray1 = new object[] { value, type, "MC,SH,DZDH,YHZH" };
            object[] xfxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetXHDW", objArray1);
            if (xfxx != null)
            {
                this.XfxxSetValue(xfxx);
            }
        }

        protected override void _XfxxSetValue(Dictionary<string, string> khxx)
        {
            object[] xfxx = new object[] { khxx["MC"], khxx["SH"], khxx["DZDH"], khxx["YHZH"] };
            this.XfxxSetValue(xfxx);
        }

        private void AddEmptySlv()
        {
            int count = base.comboBox_SLV.Items.Count;
            SLV slv = new SLV(base._fpxx.Fplx, 0, "", "", "");
            if (!base.comboBox_SLV.Items.Contains(slv))
            {
                base.comboBox_SLV.Items.Insert(count, slv);
            }
        }

        public void AutoImportZpfp(AutoImport impForm, Djfp djfp)
        {
            if ((djfp != null) && (djfp.Fpxx != null))
            {
                if ((base._fpxx.Fpdm != "0000000000") && (base._fpxx.Fphm != "00000000"))
                {
                    AutoImport.success = true;
                    Fpxx fpData = base._fpxx.GetFpData();
                    if (fpData == null)
                    {
                        string errorMessage = FPDJHelper.GetErrorMessage(base._fpxx.GetCode(), base._fpxx.Params);
                        object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorMessage };
                        AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
                    }
                    else
                    {
                        string str = "Aisino.Fwkp.Invoice" + base._fpxx.Fpdm + base._fpxx.Fphm;
                        byte[] destinationArray = new byte[0x20];
                        byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                        Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                        byte[] buffer2 = new byte[0x10];
                        Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
                        byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer2);
                        fpData.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + ";" + base._fpxx.Gfmc), destinationArray, buffer2));
                        if (base._fpxx.MakeCardInvoice(fpData, false))
                        {
                            Thread.Sleep(100);
                            if (this.fpm.SaveXxfp(fpData))
                            {
                                Thread.Sleep(100);
                                if (this.djfile != "")
                                {
                                    new FPDJHelper().InsertYkdj(this.djfile, fpData.xsdjbh);
                                }
                                object[] objArray1 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 1, this.CreateTitleText(base._fpxx.Fplx), base._fpxx.Fpdm, base._fpxx.Fphm };
                                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},对应发票信息:{3},{4},{5}", objArray1);
                            }
                            else
                            {
                                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:0,开具失败原因:发票开具成功，数据写库失败！", base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh);
                            }
                            this.GetNextFp();
                            impForm.CurFpdm = base._fpxx.Fpdm;
                            impForm.CurFphm = base._fpxx.Fphm;
                        }
                        else
                        {
                            string str2 = FPDJHelper.GetErrorMessage(base._fpxx.GetCode(), base._fpxx.Params);
                            object[] objArray2 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, str2 };
                            AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray2);
                            if (base._fpxx.GetCode().StartsWith("TCD_7") || (base._fpxx.GetCode() == "A654"))
                            {
                                AutoImport.ErrorExist = true;
                            }
                        }
                    }
                }
                else
                {
                    AutoImport.success = false;
                    string str4 = FPDJHelper.GetErrorMessage(this.fpm.Code(), base._fpxx.Params);
                    object[] objArray4 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, str4 };
                    AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray4);
                }
            }
        }

        private bool CheckBlueFp(FPLX fplx, string fpdm, string strFphm)
        {
            int num;
            if (int.TryParse(strFphm, out num))
            {
                Fpxx fpxx = this.fpm.GetXxfp(fplx, fpdm, num);
                if (fpxx != null)
                {
                    if ((fpxx.gfmc == "见红字发票购方名称") || (fpxx.gfsh == "000000123456789"))
                    {
                        MessageManager.ShowMsgBox("INP-242131");
                        return false;
                    }
                    if (decimal.Parse(fpxx.je).CompareTo(decimal.Parse("0.00")) < 0)
                    {
                        string[] textArray1 = new string[] { "红字增值税专用发票填开" };
                        MessageManager.ShowMsgBox("INP-242142", textArray1);
                        return false;
                    }
                    if (fpxx.zfbz)
                    {
                        string[] textArray2 = new string[] { "红字增值税专用发票填开" };
                        MessageManager.ShowMsgBox("INP-242143", textArray2);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool checkDkdjdrFpxx(Fpxx fp)
        {
            if (fp == null)
            {
                MessageManager.ShowMsgBox("SWDK-0050");
            }
            return true;
        }

        private bool CheckInvBlue(Fpxx fpxx)
        {
            string redNum = fpxx.redNum.Trim();
            if (redNum.Substring(0, 6) == "000000")
            {
                string[] textArray1 = new string[] { redNum };
                MessageManager.ShowMsgBox("INP-242125", textArray1);
                return false;
            }
            if (!this.fpm.CheckRevBlue(redNum))
            {
                string[] textArray2 = new string[] { redNum };
                MessageManager.ShowMsgBox("INP-242126", textArray2);
                return false;
            }
            return true;
        }

        private bool CheckPTHZFP()
        {
            decimal num;
            decimal num3;
            decimal.TryParse(base._fpxx.GetHjJeNotHs(), out num);
            decimal num2 = Math.Abs(num);
            if ((this.blueJe != "") && decimal.TryParse(this.blueJe, out num3))
            {
                decimal totalRedJe = this.fpm.GetTotalRedJe(base._fpxx.BlueFpdm, base._fpxx.BlueFphm);
                decimal num5 = decimal.Add(num3, totalRedJe);
                if ((num5 <= decimal.Zero) || (num2.CompareTo(Math.Abs(num5)) > 0))
                {
                    string[] textArray1 = new string[] { decimal.Negate(num5).ToString("F2"), num.ToString() };
                    MessageManager.ShowMsgBox("INP-242118", textArray1);
                    return false;
                }
            }
            else if (!base.TaxCardInstance.QYLX.ISTDQY && !this.isdrdjdk)
            {
                int num7;
                string xml = "";
                int.TryParse(base._fpxx.BlueFphm, out num7);
                string str2 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT><INPUT>").Append("<NSRSBH>").Append(base._fpxx.Xfsh).Append("</NSRSBH>").Append("<KPJH>").Append(base.TaxCardInstance.Machine).Append("</KPJH>").Append("<SBBH>").Append(this.fpm.GetMachineNum()).Append("</SBBH>").Append("<LZFPDM>").Append(base._fpxx.BlueFpdm).Append("</LZFPDM>").Append("<LZFPHM>").Append(string.Format("{0:00000000}", num7)).Append("</LZFPHM>").Append("<FPZL>").Append((base._fpxx.Fplx == (FPLX)2) ? "c" : "p").Append("</FPZL>").Append("</INPUT></FPXT>").ToString();
                if (HttpsSender.SendMsg("0007", str2, out xml) != 0)
                {
                    MessageManager.ShowMsgBox("INP-242160");
                    return false;
                }
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(xml);
                XmlNode node1 = document1.SelectSingleNode("/FPXT/OUTPUT");
                string innerText = node1.SelectSingleNode("HZJE").InnerText;
                if (!node1.SelectSingleNode("CODE").InnerText.Trim().Equals("0000"))
                {
                    MessageManager.ShowMsgBox("INP-242160");
                    return false;
                }
                if (!innerText.Trim().Equals("-0"))
                {
                    decimal num8;
                    decimal.TryParse(innerText, out num8);
                    if ((num8 <= decimal.Zero) || (num2.CompareTo(Math.Abs(num8)) > 0))
                    {
                        string[] textArray2 = new string[] { decimal.Negate(num8).ToString("F2"), num.ToString() };
                        MessageManager.ShowMsgBox("INP-242118", textArray2);
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckRedNote(string redNum, FPLX fplx, bool activedStatus)
        {
            if (redNum == null)
            {
                return false;
            }
            try
            {
                if (!this.fpm.CheckRedNum(redNum, fplx))
                {
                    string[] textArray1 = new string[] { redNum };
                    MessageManager.ShowMsgBox(this.fpm.Code(), textArray1);
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.log.Error("校验通知单号时异常：" + exception.ToString());
                string[] textArray2 = new string[] { redNum };
                MessageManager.ShowMsgBox("9999", textArray2);
                return false;
            }
            return true;
        }

        private bool checkSwdkdjFileName(string fileName)
        {
            bool flag = false;
            string str = "";
            if (Path.GetExtension(fileName) != ".dat")
            {
                return flag;
            }
            string directoryName = Path.GetDirectoryName(fileName);
            fileName = Path.GetFileName(fileName);
            int index = fileName.IndexOf("_");
            if (index <= -1)
            {
                return flag;
            }
            if (fileName.Substring(0, index).ToUpper() != "DKSQ")
            {
                return flag;
            }
            fileName = fileName.Substring(index + 1, (fileName.Length - index) - 1);
            index = fileName.IndexOf("_");
            if (index <= 0)
            {
                return flag;
            }
            string taxCode = fileName.Substring(0, index);
            if ((taxCode.Length > 20) || (taxCode.Length < 15))
            {
                return flag;
            }
            str = taxCode;
            string path = directoryName + @"\DK60ST" + taxCode + "000.RFHX";
            if (!File.Exists(path))
            {
                return flag;
            }
            string str5 = this.getRegFileTaxCode(path);
            if (str5.Length < 15)
            {
                return flag;
            }
            string newCode = new string('0', 15);
            this.CompressTaxCode(out newCode, taxCode, taxCode.Length);
            if (newCode.Substring(0, 15) != str5.Substring(0, 15))
            {
                return flag;
            }
            this.fileTaxCode = str;
            return true;
        }

        private void ClearDkXfmc()
        {
            if (this.fpm.IsSWDK())
            {
                base._fpxx.Xfdzdh = "";
            }
        }

        private void com_fhr_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("INV-FHR-IDX", this.com_fhr.Text);
        }

        private void com_fhr_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_fhr.Text.Trim();
            base._fpxx.Fhr = str;
            if (base._fpxx.Fhr != str)
            {
                this.com_fhr.Text = base._fpxx.Fhr;
                this.com_fhr.SelectionStart = this.com_fhr.Text.Length;
            }
        }

        private void com_gfdzdh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfdzdh.Text.Trim();
            base._fpxx.Gfdzdh = str;
            if (base._fpxx.Gfdzdh != str)
            {
                this.com_gfdzdh.Text = base._fpxx.Gfdzdh;
                this.com_gfdzdh.SelectionStart = this.com_gfdzdh.Text.Length;
            }
        }

        private void com_gfmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfmc.Text.Trim();
            base._fpxx.Gfmc = str;
            if (base._fpxx.Gfmc != str)
            {
                this.com_gfmc.Text = base._fpxx.Gfmc;
                this.com_gfmc.SelectionStart = this.com_gfmc.Text.Length;
            }
        }

        private void com_gfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfsbh.Text.Trim();
            base._fpxx.Gfsh = str;
            if (base._fpxx.Gfsh != str)
            {
                this.com_gfsbh.Text = base._fpxx.Gfsh;
                this.com_gfsbh.SelectionStart = this.com_gfsbh.Text.Length;
            }
        }

        private void com_gfzh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isNcpsgfp)
            {
                PropertyUtil.SetValue("INV-XFYHZH", this.com_gfzh.Text);
            }
        }

        private void com_gfzh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_gfzh.Text.Trim();
            base._fpxx.Gfyhzh = str;
            if (base._fpxx.Gfyhzh != str)
            {
                this.com_gfzh.Text = base._fpxx.Gfyhzh;
                this.com_gfzh.SelectionStart = this.com_gfzh.Text.Length;
            }
        }

        private void com_skr_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("INV-SKR-IDX", this.com_skr.Text);
        }

        private void com_skr_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_skr.Text.Trim();
            base._fpxx.Skr = str;
            if (base._fpxx.Skr != str)
            {
                this.com_skr.Text = base._fpxx.Skr;
                this.com_skr.SelectionStart = this.com_skr.Text.Length;
            }
        }

        private void com_xfdzdh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfdzdh.Text.Trim();
            base._fpxx.Xfdzdh = str;
            if (base._fpxx.Xfdzdh != str)
            {
                this.com_xfdzdh.Text = base._fpxx.Xfdzdh;
                this.com_xfdzdh.SelectionStart = this.com_xfdzdh.Text.Length;
            }
        }

        private void com_xfmc_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfmc.Text.Trim();
            base._fpxx.Xfmc = str;
            if (base._fpxx.Xfmc != str)
            {
                this.com_xfmc.Text = base._fpxx.Xfmc;
                this.com_xfmc.SelectionStart = this.com_xfmc.Text.Length;
            }
        }

        private void com_xfsbh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfsbh.Text.Trim();
            base._fpxx.Xfsh = str;
            if (base._fpxx.Xfsh != str)
            {
                this.com_xfsbh.Text = base._fpxx.Xfsh;
                this.com_xfsbh.SelectionStart = this.com_xfsbh.Text.Length;
            }
        }

        private void com_xfzh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.isNcpsgfp)
            {
                PropertyUtil.SetValue("INV-XFYHZH", this.com_xfzh.Text);
            }
        }

        private void com_xfzh_TextChanged(object sender, EventArgs e)
        {
            string str = this.com_xfzh.Text.Trim();
            base._fpxx.Xfyhzh = str;
            if (base._fpxx.Xfyhzh != str)
            {
                this.com_xfzh.Text = base._fpxx.Xfyhzh;
                this.com_xfzh.SelectionStart = this.com_xfzh.Text.Length;
            }
        }

        private void com_yplx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SetFpSnyLx())
            {
                base._ShowDataGridMxxx(base._DataGridView);
            }
        }

        private static int CompareSlvDesc(string x, string y)
        {
            double num;
            double num2;
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            double.TryParse(x, out num);
            double.TryParse(y, out num2);
            return num2.CompareTo(num);
        }

        private string CompressTaxCode(out string NewCode, string TaxCode, int Bytes)
        {
            NewCode = "";
            if (TaxCode.Length < 15)
            {
                int count = 15 - TaxCode.Length;
                string str = new string('0', count);
                NewCode = TaxCode + str;
            }
            else if (TaxCode.Length == 15)
            {
                NewCode = TaxCode;
            }
            else if (TaxCode.Length > 15)
            {
                NewCode = TaxCode.Substring(0, 6) + TaxCode.Substring(TaxCode.Length - 9, 9);
            }
            if (NewCode.Substring(0, 2) == "91")
            {
                NewCode = "50" + NewCode.Substring(2, NewCode.Length - 2);
            }
            return NewCode;
        }

        private string ConvertXfdzdh(string xfdzdhs)
        {
            string[] separator = new string[] { Environment.NewLine };
            string[] strArray = xfdzdhs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", strArray);
        }

        private string CreateTitleText(FPLX fplx)
        {
            string str = "";
            if ((int)fplx == 0)
            {
                return "增值税专用发票";
            }
            if ((int)fplx != 2)
            {
                return str;
            }
            if (this.Zyfplx == (ZYFP_LX)9)
            {
                return "收购发票";
            }
            if (this.Zyfplx == (ZYFP_LX)8)
            {
                return "农产品销售发票";
            }
            return "增值税普通发票";
        }

        private void DelTextChangedEvent()
        {
            this.com_gfmc.OnTextChanged = (EventHandler)Delegate.Remove(this.com_gfmc.OnTextChanged, new EventHandler(this.com_gfmc_TextChanged));
            this.com_gfsbh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_gfsbh.OnTextChanged, new EventHandler(this.com_gfsbh_TextChanged));
            this.com_gfdzdh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_gfdzdh.OnTextChanged, new EventHandler(this.com_gfdzdh_TextChanged));
            this.com_gfzh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_gfzh.OnTextChanged, new EventHandler(this.com_gfzh_TextChanged));
            this.com_xfmc.OnTextChanged = (EventHandler)Delegate.Remove(this.com_xfmc.OnTextChanged, new EventHandler(this.com_xfmc_TextChanged));
            this.com_xfsbh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_xfsbh.OnTextChanged, new EventHandler(this.com_xfsbh_TextChanged));
            this.com_xfdzdh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_xfdzdh.OnTextChanged, new EventHandler(this.com_xfdzdh_TextChanged));
            this.com_xfzh.OnTextChanged = (EventHandler)Delegate.Remove(this.com_xfzh.OnTextChanged, new EventHandler(this.com_xfzh_TextChanged));
            this.com_skr.OnTextChanged = (EventHandler)Delegate.Remove(this.com_skr.OnTextChanged, new EventHandler(this.com_skr_TextChanged));
            this.com_fhr.OnTextChanged = (EventHandler)Delegate.Remove(this.com_fhr.OnTextChanged, new EventHandler(this.com_fhr_TextChanged));
            this.txt_bz.TextChanged -= new EventHandler(this.txt_bz_TextChanged);
        }

        public bool DKSlv_isvalid()
        {
            List<Dictionary<SPXX, string>> spxxs = base._fpxx.GetSpxxs();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (Dictionary<SPXX, string> dictionary2 in spxxs)
            {
                if (!dictionary.ContainsKey(dictionary2[(SPXX)8]))
                {
                    dictionary.Add(dictionary2[(SPXX)8], 1);
                }
            }
            if ((dictionary.Count == 1) && (spxxs[0][(SPXX)8] == "0.015"))
            {
                base._fpxx.SetZyfpLx((ZYFP_LX)10);
                return true;
            }
            if ((dictionary.Count >= 2) && dictionary.Keys.Contains<string>("0.015"))
            {
                return false;
            }
            return true;
        }

        internal bool FillDjxx(Djfp djfp)
        {
            bool flag = true;
            this.djfile = djfp.File;
            Fpxx fpxx = djfp.Fpxx;
            base._fpxx.DelSpxxAll();
            this.SetDrfpQdbz(fpxx);
            if (this.isNcpsgfp)
            {
                fpxx.xfmc = fpxx.gfmc;
                fpxx.xfsh = fpxx.gfsh;
                fpxx.xfdzdh = fpxx.gfdzdh;
                fpxx.xfyhzh = fpxx.gfyhzh;
                fpxx.Zyfplx = (ZYFP_LX)9;
                fpxx.gfmc = base._fpxx.Gfmc;
                fpxx.gfsh = base._fpxx.Gfsh;
                fpxx.gfdzdh = base._fpxx.Gfdzdh;
                fpxx.gfyhzh = base._fpxx.Gfyhzh;
                List<Dictionary<SPXX, string>> mxxx = fpxx.Mxxx;
                for (int i = 0; i < mxxx.Count; i++)
                {
                    if (double.Parse(mxxx[i][(SPXX)8]) != 0.0)
                    {
                        return false;
                    }
                    if (!this.isWM())
                    {
                        mxxx[i][(SPXX)0x15] = "0";
                        mxxx[i][(SPXX)0x16] = "";
                        mxxx[i][(SPXX)0x17] = "3";
                    }
                }
            }
            else
            {
                fpxx.xfmc = base._fpxx.Xfmc;
                fpxx.xfsh = base._fpxx.Xfsh;
                fpxx.xfdzdh = base._fpxx.Xfdzdh;
                fpxx.xfyhzh = base._fpxx.Xfyhzh;
            }
            fpxx.fpdm = base._fpxx.Fpdm;
            fpxx.fphm = base._fpxx.Fphm;
            fpxx.kprq = base._fpxx.Kprq;
            string bz = fpxx.bz;
            fpxx.bz = Convert.ToBase64String(ToolUtil.GetBytes(fpxx.bz));
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static = false;
            if (((base._fpxx.Zyfplx == (ZYFP_LX)4) || (base._fpxx.Zyfplx == (ZYFP_LX)5)) || ((base._fpxx.Zyfplx == (ZYFP_LX)2) || (base._fpxx.Zyfplx == (ZYFP_LX)3)))
            {
                fpxx.Zyfplx = base._fpxx.Zyfplx;
                List<Dictionary<SPXX, string>> list2 = fpxx.Mxxx;
                for (int j = 0; j < list2.Count; j++)
                {
                    list2[j][(SPXX)4] = "吨";
                }
            }
            if (!this.fpm.ChargeAllInfo(fpxx, this.isWM(), false))
            {
                djfp.Fpxx = null;
                string errorTip = this.fpm.GetErrorTip();
                object[] args = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorTip };
                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", args);
                return false;
            }
            bool flag2 = base.TaxCardInstance.StateInfo.CompanyType > 0;
            int num = flag2 ? 7 : 8;
            if (((fpxx.Mxxx.Count > num) && flag2) && (base._fpxx.Fplx == 0))
            {
                djfp.Fpxx = null;
                string str3 = "超过明细最大行数限制！";
                this.fpm.SetErrorTip("超过明细最大行数限制！");
                object[] objArray2 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, str3 };
                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray2);
                return false;
            }
            base._fpxx = new Invoice(false, fpxx, buffer3, null);
            if (fpxx.Mxxx.Count > num)
            {
                base._fpxx.SetQdbz(true);
            }
            base._fpxx.Xsdjbh = fpxx.xsdjbh;
            base._fpxx.Bz = bz;
            flag = base._fpxx.GetCode() == "0000";
            if (!flag)
            {
                djfp.Fpxx = null;
                string errorMessage = FPDJHelper.GetErrorMessage(this.fpm.Code(), this.fpm.CodeParams());
                object[] objArray3 = new object[] { base.TaxCardInstance.TaxClock.ToString("yyyy-MM-dd HH:mm:ss"), djfp.Djh, 0, errorMessage };
                AutoImport.KpResult = string.Format("[{0}] 单据号:{1},开具结果:{2},开具失败原因:{3}", objArray3);
            }
            return flag;
        }

        private string GetDisCountName(string Str, string Name, string Rate, int DisRow)
        {
            Str = Str.Replace("$N", Name);
            Str = Str.Replace("$R", Rate);
            if (DisRow == 1)
            {
                Str = Str.Replace("$C", "");
                return Str;
            }
            Str = Str.Replace("$C", "行数" + DisRow.ToString());
            return Str;
        }

        private string GetDisCountRow(string Name, string Rate, int DisRow)
        {
            string str = "折扣$C($R%)";
            string str2 = "";
            for (int i = 0; i <= 1; i++)
            {
                str2 = this.GetDisCountName(str, Name, Rate, DisRow);
                if (str2.Length != 0)
                {
                    return str2;
                }
                MessageManager.ShowMsgBox("SWDK-0065");
                str = "折扣$C($R%)";
            }
            return str2;
        }

        private bool getDkdjdrFpxx(Dictionary<string, string> slInvHead, List<Dictionary<string, string>> slInvBody, ref Fpxx fp)
        {
            if ((slInvHead.Count > 0) && (slInvBody.Count > 0))
            {
                double num3;
                fp.gfmc = slInvHead["gfmc"];
                fp.gfsh = slInvHead["gfsh"];
                fp.gfdzdh = slInvHead["gfdzdh"];
                fp.gfyhzh = slInvHead["gfyhzh"];
                fp.bz = slInvHead["bz"];
                fp.fhr = slInvHead["fhr"];
                fp.skr = slInvHead["skr"];
                fp.xsdjbh = slInvHead["djh"];
                fp.zyspmc = slInvHead["qdhspmc"];
                fp.xfyh = slInvHead["wspzh"];
                fp.xfdzdh = slInvHead["dkqydzdh"];
                fp.dkqymc = slInvHead["xfmc"];
                fp.dkqysh = slInvHead["xfsh"];
                decimal num = new decimal();
                decimal num2 = new decimal();
                bool flag = false;
                if (slInvHead["qdhspmc"].Length > 0)
                {
                    flag = true;
                    fp.Qdxx = new List<Dictionary<SPXX, string>>();
                }
                fp.Mxxx = new List<Dictionary<SPXX, string>>();
                for (int i = 0; i < slInvBody.Count; i++)
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    item.Add((SPXX)13, (i + 1).ToString());
                    item.Add((SPXX)0, slInvBody[i]["hwmc"]);
                    item.Add((SPXX)7, slInvBody[i]["bhsje"]);
                    item[(SPXX)8] = slInvBody[i]["sl"];
                    item[(SPXX)9] = slInvBody[i]["se"];
                    item[(SPXX)3] = slInvBody[i]["gg"];
                    item[(SPXX)4] = slInvBody[i]["jldw"];
                    item[(SPXX)2] = string.Empty;
                    item[(SPXX)6] = slInvBody[i]["count"];
                    item[(SPXX)5] = slInvBody[i]["dj"];
                    item[(SPXX)10] = slInvBody[i]["fphxz"];
                    item[(SPXX)11] = slInvBody[i]["jgfs"];
                    item[(SPXX)2] = slInvBody[i]["spsm"];
                    item[(SPXX)20] = "";
                    item[(SPXX)1] = "";
                    item[(SPXX)0x15] = "";
                    item[(SPXX)0x16] = "";
                    item[(SPXX)0x17] = "";
                    if (flag)
                    {
                        fp.Qdxx.Add(item);
                    }
                    else
                    {
                        fp.Mxxx.Add(item);
                    }
                    num += decimal.Parse(slInvBody[i]["bhsje"]);
                    num2 += decimal.Parse(slInvBody[i]["se"]);
                    fp.sLv = slInvBody[i]["sl"];
                }
                if (double.TryParse(fp.sLv, out num3) && (num3 == 0.015))
                {
                    fp.Zyfplx = (ZYFP_LX)10;
                }
                fp.je = num.ToString();
                fp.se = num2.ToString();
                if (num > decimal.Zero)
                {
                    fp.isRed = false;
                }
                else
                {
                    fp.isRed = true;
                }
            }
            return true;
        }

        private bool getInvBody(string currRow, ref List<Dictionary<string, string>> slInvBody)
        {
            bool flag = true;
            decimal result = new decimal();
            decimal num2 = new decimal();
            decimal num3 = new decimal();
            decimal num4 = new decimal();
            decimal num5 = new decimal();
            decimal num6 = new decimal();
            decimal num7 = new decimal();
            decimal num8 = new decimal();
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string s = "";
            string str7 = "0101";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "0";
            try
            {
                decimal num14;
                int index = currRow.IndexOf("~~");
                if (index > -1)
                {
                    str = currRow.Substring(0, index);
                    if (str == string.Empty)
                    {
                        return false;
                    }
                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                    index = currRow.IndexOf("~~");
                    if (index > -1)
                    {
                        str2 = currRow.Substring(0, index);
                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                        index = currRow.IndexOf("~~");
                        if (index > -1)
                        {
                            str3 = currRow.Substring(0, index);
                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                            index = currRow.IndexOf("~~");
                            if (index > -1)
                            {
                                str4 = currRow.Substring(0, index);
                                if (str4.Contains("E"))
                                {
                                    str4 = Convert.ToDecimal(Convert.ToDouble(str4)).ToString();
                                }
                                if ((str4.Trim().Length > 0) && !decimal.TryParse(str4, out result))
                                {
                                    return false;
                                }
                                decimal.TryParse(str4, out result);
                                currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                index = currRow.IndexOf("~~");
                                if (index > -1)
                                {
                                    str5 = currRow.Substring(0, index);
                                    if (str5.Contains("E"))
                                    {
                                        str5 = Convert.ToDecimal(Convert.ToDouble(str5)).ToString();
                                    }
                                    if (!decimal.TryParse(str5, out num2))
                                    {
                                        return false;
                                    }
                                    decimal.TryParse(str5, out num2);
                                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                    index = currRow.IndexOf("~~");
                                    if (index > -1)
                                    {
                                        s = currRow.Substring(0, index);
                                        if ((s.Length == 0) || (double.Parse(s) <= -1E-05))
                                        {
                                            return false;
                                        }
                                        decimal.TryParse(s, out num3);
                                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                        index = currRow.IndexOf("~~");
                                        if (index > -1)
                                        {
                                            str7 = currRow.Substring(0, index);
                                            if (str7.Length == 0)
                                            {
                                                str7 = "0101";
                                            }
                                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                            index = currRow.IndexOf("~~");
                                            if (index > -1)
                                            {
                                                str8 = currRow.Substring(0, index);
                                                if (str8.Length > 0)
                                                {
                                                    str8 = currRow.Substring(0, index);
                                                    if (str8.Contains("E"))
                                                    {
                                                        str8 = Convert.ToDecimal(Convert.ToDouble(str8)).ToString();
                                                    }
                                                    if (Math.Abs(decimal.Parse(str8)) > Math.Abs(num2))
                                                    {
                                                        return false;
                                                    }
                                                    decimal.TryParse(str8, out num4);
                                                }
                                                currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                index = currRow.IndexOf("~~");
                                                if (index > -1)
                                                {
                                                    str9 = currRow.Substring(0, index);
                                                    if (str9.Contains("E"))
                                                    {
                                                        str9 = Convert.ToDecimal(Convert.ToDouble(str9)).ToString();
                                                    }
                                                    if (str9.Length == 0)
                                                    {
                                                        num14 = num2 * num3;
                                                        str9 = num14.ToString();
                                                    }
                                                    else
                                                    {
                                                        num5 = decimal.Parse(str9);
                                                        num14 = num5 / num3;
                                                        if (((decimal.Round(Math.Abs((decimal)(decimal.Parse(num14.ToString()) - num2)), 3, MidpointRounding.AwayFromZero) - decimal.Parse("1.27")) > decimal.Parse("0.000001")) && (base.TaxCardInstance.ECardType == null))
                                                        {
                                                            return false;
                                                        }
                                                        if (((decimal.Round(Math.Abs((decimal)(decimal.Round(decimal.Multiply(num2, num3), 2, MidpointRounding.AwayFromZero) - num5)), 2, MidpointRounding.AwayFromZero) - decimal.Parse("1.27")) > decimal.Parse("0.000001")) && ((int)base.TaxCardInstance.ECardType > 1))
                                                        {
                                                            return false;
                                                        }
                                                    }
                                                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                    index = currRow.IndexOf("~~");
                                                    if (index > -1)
                                                    {
                                                        str10 = currRow.Substring(0, index);
                                                        if (str10.Contains("E"))
                                                        {
                                                            str10 = Convert.ToDecimal(Convert.ToDouble(str10)).ToString();
                                                        }
                                                        if (str10.Length > 0)
                                                        {
                                                            num6 = decimal.Parse(str10);
                                                            if (decimal.Round(decimal.Multiply(num4, num3) - num6, 2, MidpointRounding.AwayFromZero) > decimal.Parse("1.27"))
                                                            {
                                                                return false;
                                                            }
                                                        }
                                                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                        index = currRow.IndexOf("~~");
                                                        if (index > -1)
                                                        {
                                                            str11 = currRow.Substring(0, index);
                                                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                            index = currRow.IndexOf("~~");
                                                            if (index > -1)
                                                            {
                                                                str12 = currRow.Substring(0, index);
                                                                if (str12.Contains("E"))
                                                                {
                                                                    str12 = Convert.ToDecimal(Convert.ToDouble(str12)).ToString();
                                                                }
                                                                if (str12.Length > 0)
                                                                {
                                                                    decimal.TryParse(str12, out num8);
                                                                }
                                                                if (str12 != "")
                                                                {
                                                                    str13 = currRow.Replace(str12, "");
                                                                }
                                                                str13 = str13.Replace("~~", "").Replace("\0", "");
                                                                if (str13.Length == 0)
                                                                {
                                                                    str13 = "0";
                                                                }
                                                                else if ((str13 != "0") && (str13 != "1"))
                                                                {
                                                                    str13 = "0";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            str11 = currRow;
                                                            decimal.TryParse(str11, out num7);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        str10 = currRow;
                                                        decimal.TryParse(str10, out num6);
                                                    }
                                                }
                                                else
                                                {
                                                    str7 = currRow;
                                                    if (str7.Length == 0)
                                                    {
                                                        str7 = "0101";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                s = currRow;
                                                if (s.Length == 0)
                                                {
                                                    return false;
                                                }
                                                if (decimal.Parse(s) < decimal.Parse("- 0.00001"))
                                                {
                                                    return false;
                                                }
                                                decimal.TryParse(s, out num3);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    if (str9.Length == 0)
                    {
                        str9 = decimal.Round(num2 * num3, 2, MidpointRounding.AwayFromZero).ToString();
                        num5 = decimal.Round(num2 * num3, 2, MidpointRounding.AwayFromZero);
                    }
                    if ((str4.Length > 0) && (str12.Length == 0))
                    {
                        result = decimal.Parse(str4);
                        num14 = num2 / result;
                        str12 = decimal.Round(decimal.Parse(num14.ToString()), 2, MidpointRounding.AwayFromZero).ToString();
                        num8 = decimal.Parse(str12);
                    }
                    if ((str12.Length > 0) && (str4.Length == 0))
                    {
                        num8 = decimal.Parse(str12);
                        num14 = num2 / num8;
                        str4 = decimal.Round(decimal.Parse(num14.ToString()), 2, MidpointRounding.AwayFromZero).ToString();
                        result = decimal.Parse(str4);
                    }
                    if (str8.Length > 0)
                    {
                        if (str10.Length == 0)
                        {
                            num14 = num4 / num2;
                            str11 = decimal.Round(decimal.Parse(num14.ToString()), 3, MidpointRounding.AwayFromZero).ToString();
                            num7 = decimal.Parse(str11);
                            str10 = decimal.Round(decimal.Multiply(num5, num7), 2, MidpointRounding.AwayFromZero).ToString();
                        }
                        if (str11.Length == 0)
                        {
                            num14 = num4 / num2;
                            str11 = decimal.Round(decimal.Parse(num14.ToString()), 2, MidpointRounding.AwayFromZero).ToString();
                            num7 = decimal.Parse(str11);
                        }
                    }
                    string str14 = "0";
                    if (str8.Length > 0)
                    {
                        str14 = "3";
                    }
                    Dictionary<string, string> item = new Dictionary<string, string>();
                    item.Add("hwmc", str);
                    item.Add("jldw", str2);
                    item.Add("gg", str3);
                    item.Add("count", str4);
                    item.Add("bhsje", str5);
                    item.Add("sl", s);
                    item.Add("spsm", str7);
                    item.Add("zkje", str8);
                    item.Add("se", str9);
                    item.Add("zkse", str10);
                    item.Add("zkl", str11);
                    item.Add("dj", str12);
                    item.Add("jgfs", str13);
                    item.Add("fphxz", str14);
                    slInvBody.Add(item);
                }
                return flag;
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
                return flag;
            }
            return flag;
        }

        private bool getInvHead(string currRow, ref Dictionary<string, string> slInvHead)
        {
            bool flag = false;
            string str = "";
            string s = "0";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "";
            string str15 = "";
            string str14 = "";
            try
            {
                int index = currRow.IndexOf("~~");
                if (index > -1)
                {
                    str = currRow.Substring(0, index);
                    if ((str.Length <= 0) || (str.Length > 20))
                    {
                        return false;
                    }
                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                    index = currRow.IndexOf("~~");
                    if (index > -1)
                    {
                        s = currRow.Substring(0, index);
                        int result = 0;
                        if (!int.TryParse(s, out result))
                        {
                            return false;
                        }
                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                        index = currRow.IndexOf("~~");
                        if (index > -1)
                        {
                            str3 = currRow.Substring(0, index);
                            if (str3 == string.Empty)
                            {
                                return false;
                            }
                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                            index = currRow.IndexOf("~~");
                            if (index > -1)
                            {
                                str4 = currRow.Substring(0, index);
                                if ((str4.Length < 15) || (str4.Length > 20))
                                {
                                    return false;
                                }
                                currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                index = currRow.IndexOf("~~");
                                if (index > -1)
                                {
                                    str5 = currRow.Substring(0, index);
                                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                    index = currRow.IndexOf("~~");
                                    if (index > -1)
                                    {
                                        str6 = currRow.Substring(0, index);
                                        flag = true;
                                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                        index = currRow.IndexOf("~~");
                                        if (index > -1)
                                        {
                                            str7 = currRow.Substring(0, index);
                                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                            index = currRow.IndexOf("~~");
                                            if (index > -1)
                                            {
                                                str8 = currRow.Substring(0, index);
                                                currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                index = currRow.IndexOf("~~");
                                                if (index > -1)
                                                {
                                                    str9 = currRow.Substring(0, index);
                                                    currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                    index = currRow.IndexOf("~~");
                                                    if (index > -1)
                                                    {
                                                        str10 = currRow.Substring(0, index);
                                                        currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                        index = currRow.IndexOf("~~");
                                                        if (index > -1)
                                                        {
                                                            str11 = currRow.Substring(0, index);
                                                            currRow = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                            index = currRow.IndexOf("~~");
                                                            if (index > -1)
                                                            {
                                                                str12 = currRow.Substring(0, index);
                                                                str13 = currRow.Substring(index + 2, (currRow.Length - index) - 2);
                                                            }
                                                            else
                                                            {
                                                                str12 = currRow;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            str11 = currRow;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        str10 = currRow;
                                                    }
                                                }
                                                else
                                                {
                                                    str9 = currRow;
                                                }
                                            }
                                            else
                                            {
                                                str8 = currRow;
                                            }
                                        }
                                        else
                                        {
                                            str7 = currRow;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (str7 != string.Empty)
                {
                    int num3 = str7.IndexOf("代开企业税号：");
                    int num4 = str7.IndexOf("代开企业名称：");
                    int num5 = str7.IndexOf("|");
                    if (((num3 > -1) && (num4 > -1)) && (num4 > num3))
                    {
                        str14 = str7.Substring(num3 + 7, (num4 - num3) - 8);
                        if (this.fileTaxCode.Substring(0, 15) != str14.Substring(0, 15))
                        {
                            return false;
                        }
                        str15 = str7.Substring(num4 + 7, (num5 - num4) - 7);
                    }
                    if (num3 > -1)
                    {
                        str7 = str7.Replace("代开企业税号：", "代开企业税号:");
                    }
                    if (num4 > -1)
                    {
                        str7 = str7.Replace("代开企业名称：", "代开企业名称:");
                    }
                    if (num5 > -1)
                    {
                        str7 = str7.Replace("|", Environment.NewLine);
                    }
                    if (0 != 0)
                    {
                        int num6 = str7.IndexOf("开具红字增值税专用发票通知单号");
                        if (num6 > -1)
                        {
                            str7.Substring(num6 + 15, 0x10);
                            int num7 = str7.IndexOf("对应正数发票代码:");
                            int num8 = str7.IndexOf("号码:");
                            if (num7 > -1)
                            {
                                str7.Substring((num7 + 8) + 1, 10);
                            }
                            if (num8 > -1)
                            {
                                str7.Substring((num8 + 2) + 1, str7.Length - ((num8 + 2) + 1));
                            }
                            str7 = str7.Replace("开具红字增值税专用发票通知单号", "开具红字增值税专用发票信息表编号");
                        }
                    }
                    else
                    {
                        int num9 = str7.IndexOf("开具红字增值税专用发票通知单号");
                        if (num9 > -1)
                        {
                            str7.Substring(num9 + 15, 0x10);
                            int num10 = str7.IndexOf("对应正数发票代码:");
                            int num11 = str7.IndexOf("号码:");
                            if (num10 > -1)
                            {
                                str7.Substring((num10 + 8) + 1, 10);
                            }
                            if (num11 > -1)
                            {
                                str7.Substring((num11 + 2) + 1, str7.Length - ((num11 + 2) + 1));
                            }
                            str7 = str7.Replace("开具红字增值税专用发票通知单号", "开具红字增值税专用发票信息表编号");
                        }
                    }
                }
                if (flag)
                {
                    slInvHead.Add("djh", str);
                    slInvHead.Add("sphs", s);
                    slInvHead.Add("gfmc", str3);
                    slInvHead.Add("gfsh", str4);
                    slInvHead.Add("gfdzdh", str5);
                    slInvHead.Add("gfyhzh", str6);
                    slInvHead.Add("bz", str7);
                    slInvHead.Add("fhr", str8);
                    slInvHead.Add("skr", str9);
                    slInvHead.Add("qdhspmc", str10);
                    slInvHead.Add("djrq", str11);
                    slInvHead.Add("wspzh", str12);
                    slInvHead.Add("dkqydzdh", str13);
                    slInvHead.Add("xfmc", str15);
                    slInvHead.Add("xfsh", str14);
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
                return flag;
            }
            return flag;
        }

        private void GetNextFp()
        {
            if (this.fpm.CanInvoice(base._fpxx.Fplx))
            {
                FPLX fplx = base._fpxx.Fplx;
                string[] current = this.fpm.GetCurrent(fplx);
                if (current != null)
                {
                    string str = base._fpxx.Kprq;
                    string xfsh = this.fpm.GetXfsh();
                    string xfmc = this.fpm.GetXfmc();
                    string xfdzdh = this.fpm.GetXfdzdh();
                    string xfyhzh = this.fpm.GetXfyhzh();
                    string str6 = PropertyUtil.GetValue("INV-HSJBZ", "0");
                    byte[] destinationArray = new byte[0x20];
                    byte[] sourceArray = Invoice.TypeByte;
                    Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                    byte[] buffer2 = new byte[0x10];
                    Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                    byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                    Invoice.IsGfSqdFp_Static = false;
                    base._fpxx = new Invoice(false, false, str6.Equals("1"), base._fpxx.Fplx, buffer3, null);
                    base._fpxx.Fpdm = current[0];
                    base._fpxx.Fphm = current[1];
                    if (this.isNcpsgfp)
                    {
                        base._fpxx.Gfsh = xfsh;
                        base._fpxx.Gfmc = xfmc;
                        base._fpxx.Gfdzdh = xfdzdh;
                        base._fpxx.Gfyhzh = xfyhzh;
                    }
                    else
                    {
                        base._fpxx.Xfsh = xfsh;
                        base._fpxx.Xfmc = xfmc;
                        base._fpxx.Xfdzdh = xfdzdh;
                        base._fpxx.Xfyhzh = xfyhzh;
                    }
                    base._fpxx.Kprq = str;
                    base._fpxx.Kpr = UserInfo.Yhmc;
                    base._fpxx.Skr = this.com_skr.Text;
                    base._fpxx.Fhr = this.com_fhr.Text;
                    base._fpxx.SetZyfpLx(this.Zyfplx);
                    base._fpxx.AddSpxx(this._SetDefaultSpsm(), this._SetDefaultSpsLv(), this.Zyfplx);
                }
                else
                {
                    base._fpxx.Fpdm = "0000000000";
                    base._fpxx.Fphm = "00000000";
                }
            }
        }

        private string getRegFileTaxCode(string regFileName)
        {
            string str = "";
            try
            {
                qwe qwe = new qwe();
                Xihaa.abc(regFileName, "", 0, DateTime.Now.ToString("yyyyMMdd"), ref qwe);
                string str2 = new string(qwe.SoftwareID).Substring(0, 6);
                if ((str2.Substring(0, 2) != "DK") || (str2.Substring(4, 2) != "ST"))
                {
                    return str;
                }
                str = new string(qwe.TaxCode).Substring(0, 15);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
            }
            return str;
        }

        private void GfxxSetValue(object[] khxx)
        {
            if (khxx.Length == 4)
            {
                khxx[0].ToString();
                khxx[1].ToString();
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray = khxx[2].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("DZDH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    object[] values = new object[] { strArray[i] };
                    table.Rows.Add(values);
                }
                string[] textArray2 = new string[] { Environment.NewLine };
                string[] strArray2 = khxx[3].ToString().Split(textArray2, StringSplitOptions.RemoveEmptyEntries);
                DataTable table2 = new DataTable();
                DataColumn column2 = new DataColumn("YHZH");
                table2.Columns.Add(column2);
                for (int j = 0; j < strArray2.Length; j++)
                {
                    object[] objArray2 = new object[] { strArray2[j] };
                    table2.Rows.Add(objArray2);
                }
                if (this.isNcpsgfp)
                {
                    this.com_xfmc.Text = khxx[0].ToString();
                    this.com_xfsbh.Text = khxx[1].ToString();
                    this.com_xfdzdh.Text = "";
                    this.com_xfdzdh.DataSource = table;
                    this.com_xfdzdh.Text = (strArray.Length != 0) ? strArray[0] : "";
                    this.com_xfzh.Text = "";
                    this.com_xfzh.DataSource = table2;
                    this.com_xfzh.Text = (strArray2.Length != 0) ? strArray2[0] : "";
                }
                else
                {
                    this.com_gfmc.Text = khxx[0].ToString();
                    this.com_gfsbh.Text = khxx[1].ToString();
                    this.com_gfdzdh.Text = "";
                    this.com_gfdzdh.DataSource = table;
                    this.com_gfdzdh.Text = (strArray.Length != 0) ? strArray[0] : "";
                    this.com_gfzh.Text = "";
                    this.com_gfzh.DataSource = table2;
                    this.com_gfzh.Text = (strArray2.Length != 0) ? strArray2[0] : "";
                }
            }
        }

        private void ImportFpData(Fpxx fp)
        {
            this.blueJe = string.Empty;
            bool flag = true;
            if (fp == null)
            {
                MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                flag = false;
            }
            else if (!this.CheckRedNote(fp.redNum, fp.fplx, false))
            {
                flag = false;
            }
            else if (!this.CheckBlueFp(fp.fplx, fp.blueFpdm, fp.blueFphm))
            {
                flag = false;
            }
            if (!flag)
            {
                this.tool_fushu.Checked = false;
                this.isHzwm = false;
                if (base._fpxx.IsRed)
                {
                    this.RefreshData(false);
                }
            }
            else
            {
                this.RefreshData(true);
                base._fpxx.Hsjbz = false;
                this._SetHsjxx(base._DataGridView, false);
                if (this.fpm.IsSWDK())
                {
                    fp.bz = NotesUtil.GetRedZyInvNotes(fp.redNum, "s") + fp.bz;
                }
                if (this.fpm.CopyRedNotice(fp, base._fpxx))
                {
                    this.ResetQyxx();
                    this.ClearDkXfmc();
                    base.reset_fpxx(base._fpxx.Zyfplx == (ZYFP_LX)1);
                    this.ShowInvMainInfo();
                    if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                    {
                        this.SetHysyHsjxx(base._DataGridView, true);
                    }
                    else
                    {
                        this.SetHysyHsjxx(base._DataGridView, false);
                    }
                    if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                    {
                        base._ChaeButton.Checked = true;
                        base._QingdanButton.Enabled = false;
                    }
                    base._ChaeButton.Enabled = false;
                    string[] strArray = this.MatchKhxx(fp.gfsh, false);
                    if (strArray.Length > 2)
                    {
                        this.ParseGfDzdh(strArray[1]);
                        this.ParseGfyhzh(strArray[2]);
                    }
                    base._ShowDataGridMxxx(base._DataGridView);
                    this.com_gfmc.Edit = 0;
                    this.com_gfsbh.Edit = 0;
                    base._DataGridView.ReadOnly = true;
                    base._AddRowButton.Enabled = false;
                    base._DelRowButton.Enabled = false;
                    base._spmcBt.Visible = false;
                }
                else
                {
                    MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                    this.RefreshData(false);
                }
            }
        }

        private void Initialize()
        {
            base.XmlComponentFile = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPtiankai_new\Aisino.Fwkp.Fpkj.Form.FPtiankai_new.xml";
            this.fpm = new FpManager();
            string str = "Aisino.Fwkp.Fptk.Form" + base.XmlComponentFile;
            byte[] destinationArray = new byte[0x20];
            byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
            Array.Copy(bytes, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer2);
            base.Initialize(buffer3);
            this.panel1 = base._xmlComponentLoader.GetControlByName<AisinoPNL>("panel1");
            this.panel2 = base._xmlComponentLoader.GetControlByName<AisinoPNL>("panel2");
            if (this.mFplx == 0)
            {
                this.panel1.BackgroundImage = Resources.ZY;
            }
            else
            {
                this.panel1.BackgroundImage = Resources.PT;
            }
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 650);
            this.lblDq = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lbl_Dq");
            this.tool_close = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_close");
            this.tool_zuofei = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_zuofei");
            this.tool_zuofei.CheckOnClick = false;
            this.tool_kehu = base._xmlComponentLoader.GetControlByName<ToolStripDropDownButton>("tool_kehu");
            this.tool_autokh = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_autokh");
            this.tool_manukh = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_manukh");
            this.tool_fanlan = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_fanlan");
            this.tool_zjkj = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_zjkj");
            this.tool_DaoRuHZTZD = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_DaoRuHZTZD");
            this.tool_drgp = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_drgp");
            this.tool_fushu = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_fushu");
            this.tool_fushu1 = base._xmlComponentLoader.GetControlByName<ToolStripDropDownButton>("tool_fushu1");
            this.tool_fushu1.Visible = false;
            this.tool_import = base._xmlComponentLoader.GetControlByName<ToolStripDropDownButton>("tool_import");
            this.tool_imputSet = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_imputSet");
            this.tool_manualImport = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_manualImport");
            this.tool_autoImport = base._xmlComponentLoader.GetControlByName<ToolStripMenuItem>("tool_autoImport");
            this.tool_dkdr = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_dkdr");
            this.tool_dkdjdr = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_dkdjdr");
            this.tool_print = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_print");
            this.tool_fuzhi = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_fuzhi");
            base._ChaeButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_chae");
            this.lab_title = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_title");
            this.lab_fpdm = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_kprq = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_kprq");
            this.lab_fphm = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_fphm");
            this.lab_yplx = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_yplx");
            this.lab_yplx.Visible = false;
            this.com_yplx = base._xmlComponentLoader.GetControlByName<AisinoCMB>("com_yplx");
            this.com_yplx.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] items = new object[] { "(石脑油)", "(石脑油DDZG)", "(燃料油)", "(燃料油DDZG)" };
            this.com_yplx.Items.AddRange(items);
            this.com_yplx.Visible = false;
            this.com_gfsbh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_gfsbh");
            this.com_gfmc = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_gfmc");
            this.com_gfdzdh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_gfdzdh");
            this.com_gfzh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_gfzh");
            this.lab_hj_se = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_hj_se");
            this.lab_hj_je = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_hj_je");
            this.lab_hj_jshj = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_hj_jshj");
            this.lab_hj_jshj_dx = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_hj_jshj_dx");
            this.txt_bz = base._xmlComponentLoader.GetControlByName<AisinoTXT>("txt_bz");
            this.txt_bz.AcceptsTab = false;
            this.txt_bz.AcceptsReturn = true;
            this.txt_bz.ScrollBars = ScrollBars.Vertical;
            this.com_xfsbh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("lab_xfsbh");
            this.com_xfmc = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("lab_xfmc");
            this.com_xfzh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_xfzh");
            this.com_xfdzdh = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("lab_xfdzdh");
            this.lab_kp = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lab_kp");
            this.com_fhr = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_fhr");
            this.com_fhr.IsSelectAll = true;
            this.com_fhr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.com_fhr.Width));
            this.com_fhr.DrawHead = false;
            this.com_skr = base._xmlComponentLoader.GetControlByName<AisinoMultiCombox>("com_skr");
            this.com_skr.IsSelectAll = true;
            this.com_skr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.com_skr.Width));
            this.com_skr.DrawHead = false;
            base._QingdanButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_qingdan");
            base._AddRowButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_addrow");
            base._DelRowButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_delrow");
            base._ZhekouButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_zhekou");
            base._DataGridView = base._xmlComponentLoader.GetControlByName<CustomStyleDataGrid>("DataGrid1");
            base._StatisticButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("tool_tongji");
            base._HsjbzButton = base._xmlComponentLoader.GetControlByName<ToolStripButton>("bt_jg");
            this.mainPanel = base._xmlComponentLoader.GetControlByName<AisinoPNL>("panel_main");
            this.toolStrip3 = base._xmlComponentLoader.GetControlByName<ToolStrip>("toolStrip3");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip3);
            this.YD_checkBox = base._xmlComponentLoader.GetControlByName<CheckBox>("YD_checkBox");
            this.lblJYM = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lblJYM");
            this.lblJYM.Visible = false;
            this.lblNCP = base._xmlComponentLoader.GetControlByName<AisinoLBL>("lblNCP");
            this.lblNCP.Visible = false;
            this.tool_close.Margin = new Padding(20, 1, 0, 2);
            this.SetTitleFont();
            this.SetDzdhInfo();
            this.SetYhzhInfo();
            this.SetGridStyle();
            if (this.isNcpsgfp)
            {
                this.com_gfmc.Edit = 0;
                this.com_gfsbh.Edit = 0;
            }
            else
            {
                this.com_xfmc.Edit = 0;
                this.com_xfsbh.Edit = 0;
            }
            base._DataGridView.MultiSelect = false;
            this.tool_print.Click += new EventHandler(this.tool_print_Click);
            this.tool_print.MouseDown += new MouseEventHandler(this.tool_print_MouseDown);
            this.tool_fuzhi.Click += new EventHandler(this.tool_fuzhi_Click);
            this.tool_zjkj.Click += new EventHandler(this.tool_zjkj_Click);
            this.tool_DaoRuHZTZD.Click += new EventHandler(this.tool_DaoRuHZTZD_Click);
            this.tool_drgp.Click += new EventHandler(this.tool_drgp_Click);
            this.tool_fushu.Click += new EventHandler(this.tool_fushu_Click);
            this.tool_autokh.CheckOnClick = true;
            this.tool_autokh.Click += new EventHandler(this.tool_autokh_Click);
            this.tool_manukh.Click += new EventHandler(this.tool_GfxxSave_Click);
            this.tool_fanlan.Click += new EventHandler(this.tool_fanlan_Click);
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.tool_imputSet.Click += new EventHandler(this.tool_imputSet_Click);
            this.tool_manualImport.Click += new EventHandler(this.tool_manualImport_Click);
            this.tool_autoImport.Click += new EventHandler(this.tool_autoImport_Click);
            this.tool_dkdr.Click += new EventHandler(this.tool_dkdr_Click);
            this.tool_dkdjdr.Click += new EventHandler(this.tool_dkdjdr_Click);
            this.YD_checkBox.CheckedChanged += new EventHandler(this.YD_checkBox_StateChange);
            this.tool_dkdjdr.Visible = true;
            if (!base._onlyShow)
            {
                this.SetAutoKHChecked();
                if (this.isNcpsgfp)
                {
                    this._SetGfxxControl(this.com_xfsbh, "SH");
                    this._SetGfxxControl(this.com_xfmc, "MC");
                }
                else
                {
                    this._SetGfxxControl(this.com_gfsbh, "SH");
                    this._SetGfxxControl(this.com_gfmc, "MC");
                }
                if (this.fpm.IsSWDK())
                {
                    this.YD_checkBox.Visible = false;
                    this._SetXfxxControl(this.com_xfdzdh, "DZDH");
                }
                else
                {
                    this.YD_checkBox.Visible = false;
                    this.com_xfzh.OnSelectValue += com_xfzh_SelectedIndexChanged;
                    this.com_gfzh.OnSelectValue += com_gfzh_SelectedIndexChanged;
                }
                this.com_skr.OnSelectValue += com_skr_SelectedIndexChanged;
                this.com_fhr.OnSelectValue += com_fhr_SelectedIndexChanged;
                this.com_yplx.SelectedIndexChanged += new EventHandler(this.com_yplx_SelectedIndexChanged);
            }
            this.tool_close.ToolTipText = "退出";
            this.tool_fushu.ToolTipText = "开具红字发票";
            this.tool_fuzhi.ToolTipText = "复制发票";
            this.tool_kehu.ToolTipText = this.isNcpsgfp ? "保存销方信息" : "保存购方信息";
            this.tool_fanlan.ToolTipText = "红字发票反开蓝字发票";
            this.tool_print.ToolTipText = base._onlyShow ? "发票打印" : "开具发票并打印";
            base._DataGridView.ImeMode = ImeMode.NoControl;
            base.MaximizeBox = true;
            base.MinimizeBox = true;
            base.FormBorderStyle = FormBorderStyle.Sizable;
            bool flag = RegisterManager.CheckRegFile("DKST");
            this.tool_dkdjdr.Visible = this.fpm.IsSWDK() & flag;
            this.tool_dkdr.Visible = this.fpm.IsSWDK();
            if ((this.isNcpsgfp || this.isSnyZyfp) || !FLBM_lock.isCes())
            {
                base._ChaeButton.Visible = false;
            }
        }

        private void InitNextFP(string[] dmhm)
        {
            this.isHzwm = false;
            this.IsHYXXB = false;
            base._fpxx.Fpdm = dmhm[0];
            base._fpxx.Fphm = dmhm[1];
            this.RefreshData(false);
            this._SetHsjxx(base._DataGridView, base._fpxx.Hsjbz);
            base._DataGridView.Columns["SLV"].Tag = null;
            if (base._DataGridView.RowCount > 0)
            {
                base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, 0];
                base._DataGridView.CurrentCell = base._DataGridView[0, 0];
            }
            bool flag = base.TaxCardInstance.StateInfo.CompanyType > 0;
            if ((base._fpxx.Fplx == (FPLX)0) & flag)
            {
                base._QingdanButton.Visible = false;
            }
            else
            {
                base._QingdanButton.Visible = true;
            }
            if (base.comboBox_SLV.Items.Count > 0)
            {
                base.comboBox_SLV.SelectedIndex = 0;
            }
        }

        private void InvoiceForm_Init(FPLX fplx, string fpdm, string fphm)
        {
            string str = PropertyUtil.GetValue("INV-HSJBZ", "0");
            this.mFplx = fplx;
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static = false;
            base._fpxx = new Invoice(false, false, str.Equals("1"), fplx, buffer3, null);
            base._fpxx.Bmbbbh = base.getbmbbbh();
            if (((int)fplx == 2) && ((this.Zyfplx == (ZYFP_LX)9) || (this.Zyfplx == (ZYFP_LX)8)))
            {
                base._fpxx.SetZyfpLx(this.Zyfplx);
            }
            if (string.IsNullOrEmpty(base._fpxx.GetSqSLv()))
            {
                this.initSuccess = false;
                string str8 = "增值税专用发票";
                if ((int)fplx == 2)
                {
                    str8 = "增值税普通发票";
                }
                string[] textArray1 = new string[] { str8 };
                MessageManager.ShowMsgBox("INP-242129", textArray1);
            }
            else
            {
                this.Initialize();
                int num = 0;
                string str2 = this._SetDefaultSpsLv();
                if (this.isSnyZyfp)
                {
                    Spxx spxx = new Spxx("", this._SetDefaultSpsm(), str2, "", "吨", "", str.Equals("1"), this.Zyfplx);
                    num = base._fpxx.AddSpxx(spxx);
                }
                else
                {
                    if ((int)fplx == 0)
                    {
                        ZYFP_LX zyfp_lx = base._fpxx.Zyfplx;
                        if ((int)zyfp_lx == 1)
                        {
                            base._fpxx.SetZyfpLx(zyfp_lx);
                            this.Zyfplx = zyfp_lx;
                        }
                    }
                    num = base._fpxx.AddSpxx(this._SetDefaultSpsm(), str2, this.Zyfplx);
                }
                if (num < 0)
                {
                    this.initSuccess = false;
                    MessageManager.ShowMsgBox(base._fpxx.GetCode());
                }
                else
                {
                    this.SetTkFormTitle(fpdm, fplx);
                    base.Name = this.Text;
                    this.tool_zuofei.Visible = false;
                    this.tool_fanlan.Visible = false;
                    if (base.comboBox_SLV.Items.Count > 0)
                    {
                        this.prevCmbSlv = base.comboBox_SLV.SelectedItem;
                    }
                    if (this.isSnyZyfp)
                    {
                        this.tool_fuzhi.Visible = false;
                        base._QingdanButton.Visible = false;
                        this.lab_yplx.Visible = true;
                        this.com_yplx.Visible = true;
                    }
                    else
                    {
                        this.tool_fuzhi.Visible = true;
                        bool flag = base.TaxCardInstance.StateInfo.CompanyType > 0;
                        if (((int)fplx == 0) & flag)
                        {
                            base._QingdanButton.Visible = false;
                        }
                        else
                        {
                            base._QingdanButton.Visible = true;
                        }
                        this.lab_yplx.Visible = false;
                        this.com_yplx.Visible = false;
                    }
                    if (base.TaxCardInstance.QYLX.ISTDQY)
                    {
                        this.tool_drgp.Visible = false;
                    }
                    else
                    {
                        this.tool_drgp.Visible = true;
                    }
                    if (this.fpm.IsSWDK())
                    {
                        this.tool_import.Visible = false;
                    }
                    else
                    {
                        this.tool_dkdr.Visible = false;
                    }
                    this.lblNCP.Visible = false;
                    if ((int)fplx == 2)
                    {
                        if (this.Zyfplx == (ZYFP_LX)9)
                        {
                            this.lblNCP.Visible = true;
                            this.lblNCP.Text = "收购";
                        }
                        else if (this.Zyfplx == (ZYFP_LX)8)
                        {
                            this.lblNCP.Visible = true;
                            this.lblNCP.Text = "农产品销售";
                        }
                    }
                    this.SetSkrAndFhr();
                    this.RegTextChangedEvent();
                    string xfsh = this.fpm.GetXfsh();
                    string xfmc = this.fpm.GetXfmc();
                    string xfyhzh = this.fpm.GetXfyhzh();
                    string xfdzdh = this.fpm.GetXfdzdh();
                    string jskClock = this.fpm.GetJskClock();
                    base._fpxx.Fpdm = fpdm;
                    base._fpxx.Fphm = fphm;
                    base._fpxx.Kprq = jskClock;
                    base._fpxx.Kpr = UserInfo.Yhmc;
                    base._fpxx.Skr = this.com_skr.Text;
                    base._fpxx.Fhr = this.com_fhr.Text;
                    if (this.isNcpsgfp)
                    {
                        base._fpxx.Gfsh = xfsh;
                        base._fpxx.Gfmc = xfmc;
                        base._fpxx.Gfdzdh = xfdzdh;
                    }
                    else
                    {
                        base._fpxx.Xfsh = xfsh;
                        base._fpxx.Xfmc = xfmc;
                        base._fpxx.Xfdzdh = xfdzdh;
                    }
                    this.ShowInvMainInfo();
                    if (this.fpm.IsSWDK())
                    {
                        this.com_xfdzdh.Text = "";
                        this.com_xfzh.Text = "";
                    }
                    else
                    {
                        this.ParseXfyhzh(xfyhzh);
                        this.ParseXfDzdh(xfdzdh);
                    }
                    this._SetHsjxx(base._DataGridView, base._fpxx.Hsjbz);
                    if (base._DataGridView.Rows.Count > 0)
                    {
                        base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, 0];
                    }
                }
            }
        }

        private bool IsCAExist(string CAFileName)
        {
            bool flag = true;
            string path = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\" + CAFileName;
            return (((PropertyUtil.GetValue("SWDK_CA_FILENAME", "").Trim().Length != 0) && File.Exists(path)) && flag);
        }

        public bool isWM()
        {
            bool flag = !FLBM_lock.isFlbm();
            return ((this.IsHYXXB || this.isHzwm) | flag);
        }

        private string[] MatchKhxx(string sh, bool isXhdw)
        {
            List<string> list = new List<string>();
            object[] objArray = null;
            if (isXhdw)
            {
                object[] objArray1 = new object[] { sh, 1, "MC,SH,DZDH,YHZH" };
                objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetXHDWMore", objArray1);
            }
            else
            {
                object[] objArray2 = new object[] { sh, 1, "MC,SH,DZDH,YHZH" };
                objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", objArray2);
            }
            if ((objArray != null) && (objArray.Length != 0))
            {
                DataTable table = objArray[0] as DataTable;
                if ((table != null) && (table.Rows.Count > 0))
                {
                    list.Add(table.Rows[0]["MC"].ToString());
                    list.Add(table.Rows[0]["DZDH"].ToString());
                    list.Add(table.Rows[0]["YHZH"].ToString());
                }
            }
            return list.ToArray();
        }

        private string ParseBz(string bz)
        {
            if (!string.IsNullOrEmpty(bz))
            {
                int num = 0;
                if (base._fpxx.Fplx == 0)
                {
                    num = 1;
                }
                string str = NotesUtil.GetInfo(bz, num, "");
                if (str != string.Empty)
                {
                    return str;
                }
            }
            return "";
        }

        private void ParseGfDzdh(string gfdzdh)
        {
            if (gfdzdh != null)
            {
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray = gfdzdh.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("DZDH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    object[] values = new object[] { strArray[i] };
                    table.Rows.Add(values);
                }
                this.com_gfdzdh.DataSource = table;
                if (table.Rows.Count > 0)
                {
                    this.com_gfdzdh.SelectedIndex = 0;
                }
                base._fpxx.Gfdzdh = this.com_gfdzdh.Text;
            }
        }

        private void ParseGfyhzh(string gfyhzh)
        {
            if (gfyhzh != null)
            {
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray = gfyhzh.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YHZH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    object[] values = new object[] { strArray[i] };
                    table.Rows.Add(values);
                }
                this.com_gfzh.DataSource = table;
                if (table.Rows.Count > 0)
                {
                    this.com_gfzh.SelectedIndex = 0;
                }
                base._fpxx.Gfyhzh = this.com_gfzh.Text;
            }
        }

        private bool parseInvBody(Dictionary<string, string> slInvItem, ref Dictionary<string, string> slValues)
        {
            slValues.Clear();
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "0101";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "0";
            try
            {
                str = slInvItem["hwmc"];
                str2 = slInvItem["jldw"];
                str3 = slInvItem["gg"];
                str4 = slInvItem["count"];
                str5 = slInvItem["bhsje"];
                str6 = slInvItem["sl"];
                str7 = slInvItem["spsm"];
                if (str7.Length == 0)
                {
                    str7 = "0101";
                }
                str8 = slInvItem["zkje"];
                str9 = slInvItem["se"];
                str10 = slInvItem["zkse"];
                str11 = slInvItem["zkl"];
                str12 = slInvItem["dj"];
                str13 = slInvItem["jgfs"];
                string str14 = slInvItem["fphxz"];
                slValues.Add("hwmc", str);
                slValues.Add("jldw", str2);
                slValues.Add("gg", str3);
                slValues.Add("count", str4);
                slValues.Add("bhsje", str5);
                slValues.Add("sl", str6);
                slValues.Add("spsm", str7);
                slValues.Add("zkje", str8);
                slValues.Add("se", str9);
                slValues.Add("zkse", str10);
                slValues.Add("zkl", str11);
                slValues.Add("dj", str12);
                slValues.Add("jgfs", str13);
                slValues.Add("fphxz", str14);
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
                return false;
            }
            return true;
        }

        private bool parseProxyInvFile(string fileName, ref Dictionary<string, string> slInvHead, ref List<Dictionary<string, string>> slInvBody)
        {
            bool flag = false;
            double[] numArray1 = new double[] { 0.17, 0.13, 0.06, 0.04, 0.05 };
            List<string> list = new List<string>();
            FileStream stream = File.Open(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(stream, ToolUtil.GetEncoding());
            string item = "";
            while ((item = reader.ReadLine()) != null)
            {
                list.Add(item);
            }
            stream.Close();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            if (list.Count < 3)
            {
                return false;
            }
            string currRow = "";
            bool flag2 = false;
            bool flag3 = false;
            for (int i = 0; i < list.Count; i++)
            {
                currRow = list[i].ToString().Trim();
                if ((currRow != string.Empty) && (currRow.Substring(0, 2) != "//"))
                {
                    if (!flag2)
                    {
                        if (currRow.Substring(0, 8) != "SJJK0101")
                        {
                            return flag;
                        }
                        flag2 = true;
                    }
                    else if (!flag3)
                    {
                        if (!this.getInvHead(currRow, ref slInvHead))
                        {
                            return flag;
                        }
                        flag3 = true;
                    }
                    else if (!this.getInvBody(currRow, ref slInvBody))
                    {
                        return flag;
                    }
                }
            }
            if (((slInvHead.Count > 5) && (slInvBody.Count == int.Parse(slInvHead["sphs"]))) && this.rewriteInvBody(slInvBody))
            {
                flag = true;
            }
            return true;
        }

        private void ParseXfDzdh(string qyDzdh)
        {
            if (qyDzdh != null)
            {
                string str = PropertyUtil.GetValue("INV-XFDZDH", "");
                int num = -1;
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray = qyDzdh.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("DZDH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    object[] values = new object[] { strArray[i] };
                    table.Rows.Add(values);
                    if (strArray[i].Equals(str))
                    {
                        num = i;
                    }
                }
                if (this.isNcpsgfp)
                {
                    this.com_gfdzdh.DataSource = table;
                    if (num >= 0)
                    {
                        this.com_gfdzdh.SelectedIndex = num;
                    }
                    else if (table.Rows.Count > 0)
                    {
                        this.com_gfdzdh.SelectedIndex = 0;
                    }
                    base._fpxx.Gfdzdh = this.com_gfdzdh.Text;
                }
                else
                {
                    this.com_xfdzdh.DataSource = table;
                    if (num >= 0)
                    {
                        this.com_xfdzdh.SelectedIndex = num;
                    }
                    else if (table.Rows.Count > 0)
                    {
                        this.com_xfdzdh.SelectedIndex = 0;
                    }
                    base._fpxx.Xfdzdh = this.com_xfdzdh.Text;
                }
            }
        }

        private void ParseXfyhzh(string qyyhzh)
        {
            if (qyyhzh != null)
            {
                string str = PropertyUtil.GetValue("INV-XFYHZH", "");
                int num = -1;
                string[] separator = new string[] { Environment.NewLine };
                string[] strArray = qyyhzh.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YHZH");
                table.Columns.Add(column);
                for (int i = 0; i < strArray.Length; i++)
                {
                    object[] values = new object[] { strArray[i] };
                    table.Rows.Add(values);
                    if (strArray[i].Equals(str))
                    {
                        num = i;
                    }
                }
                if (this.isNcpsgfp)
                {
                    this.com_gfzh.DataSource = table;
                    if (num >= 0)
                    {
                        this.com_gfzh.SelectedIndex = num;
                    }
                    else if (table.Rows.Count > 0)
                    {
                        this.com_gfzh.SelectedIndex = 0;
                    }
                    base._fpxx.Gfyhzh = this.com_gfzh.Text;
                }
                else
                {
                    this.com_xfzh.DataSource = table;
                    if (num >= 0)
                    {
                        this.com_xfzh.SelectedIndex = num;
                    }
                    else if (table.Rows.Count > 0)
                    {
                        this.com_xfzh.SelectedIndex = 0;
                    }
                    base._fpxx.Xfyhzh = this.com_xfzh.Text;
                }
            }
        }

        private void PrintFp(Fpxx fp)
        {
            try
            {
                FPPrint print1 = new FPPrint(Invoice.FPLX2Str(base._fpxx.Fplx), fp.fpdm, int.Parse(fp.fphm));
                print1.Print(true);
                string str = print1.IsPrint;
                if ((str != "0000") && (str != "0005"))
                {
                    MessageManager.ShowMsgBox("INP-242116");
                }
            }
            catch (Exception exception)
            {
                this.log.Error("发票打印异常：" + exception.Message);
                MessageManager.ShowMsgBox("INP-242116");
            }
        }

        private Fpxx ProcessFanlan(string fileName)
        {
            try
            {
                string str = this.fpm.DecodeIDEAFile(fileName, 0);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                Fpxx fpxx = new Fpxx(0, "", "");
                XmlDocument document1 = new XmlDocument();
                document1.LoadXml(str);
                XmlNode node = document1.SelectSingleNode("/info/head/version");
                if (node != null)
                {
                    string text1 = node.InnerText;
                }
                XmlNode node2 = document1.SelectSingleNode("/info/data");
                fpxx.fpdm = node2.SelectSingleNode("fpdm").InnerText;
                fpxx.fphm = node2.SelectSingleNode("fphm").InnerText;
                fpxx.gfsh = node2.SelectSingleNode("gfnsrsbh").InnerText;
                fpxx.gfmc = node2.SelectSingleNode("gfnsrmc").InnerText;
                fpxx.xfsh = node2.SelectSingleNode("xfnsrsbh").InnerText;
                fpxx.xfmc = node2.SelectSingleNode("xfnsrmc").InnerText;
                fpxx.je = node2.SelectSingleNode("je").InnerText;
                fpxx.sLv = node2.SelectSingleNode("sl").InnerText;
                fpxx.se = node2.SelectSingleNode("se").InnerText;
                fpxx.kprq = node2.SelectSingleNode("kprq").InnerText;
                string innerText = node2.SelectSingleNode("kpjh").InnerText;
                if (!string.IsNullOrEmpty(innerText))
                {
                    fpxx.kpjh = Convert.ToInt32(innerText);
                }
                fpxx.redNum = node2.SelectSingleNode("tzdbh").InnerText;
                string text2 = node2.SelectSingleNode("szlb").InnerText;
                return fpxx;
            }
            catch (Exception exception)
            {
                this.log.Error("解析反蓝字发票通知单格式异常：" + exception.ToString());
                return null;
            }
        }

        private bool proxyInvCADL(string sourFile, string DestFile, out string Msgs)
        {
            bool flag = true;
            Msgs = "";
            if (((sourFile == string.Empty) || (sourFile.Length == 0)) || ((DestFile == string.Empty) || (DestFile.Trim().Length == 0)))
            {
                Msgs = "传入文件路径为空！";
                return false;
            }
            this.log.Info("DestFile文件位置: " + DestFile);
            if (File.Exists(DestFile))
            {
                this.log.Info("存在DestFile文件路径: " + DestFile);
                File.Delete(DestFile);
            }
            if (!File.Exists(sourFile))
            {
                Msgs = "源文件不存在！";
                return false;
            }
            try
            {
                char[] err = new char[0x400];
                int errLen = 0x400;
                if (!this.IsCAExist(this.CAFileName))
                {
                    MessageManager.ShowMsgBox("SWDK-0067");
                    return false;
                }
                int num = DigitalEnvelop.DigEnvInit(false, true, true);
                if (num != 0)
                {
                    errLen = 0x400;
                    DigitalEnvelop.GetErrInfo(err, ref errLen);
                    Msgs = new string(err);
                    if (num == 1)
                    {
                        Msgs = Msgs + "证书初始化失败";
                    }
                    return false;
                }
                this.log.Info("SetCaCertAndCrlByPfx CAFileName, CAPassWord" + this.CAFileName + "," + this.CAPassWord);
                this.log.Info("GetCurrentDirectory ：" + Directory.GetCurrentDirectory());
                num = DigitalEnvelop.SetCaCertAndCrlByPfx(this.CAFileName, this.CAPassWord, "");
                if (num != 0)
                {
                    errLen = 0x400;
                    DigitalEnvelop.GetErrInfo(err, ref errLen);
                    Msgs = new string(err);
                    switch (num)
                    {
                        case 1:
                            Msgs = Msgs + "由证书文件设置受信证书和撤销列表异常";
                            break;

                        case 2:
                            Msgs = Msgs + "证书口令错误";
                            break;
                    }
                    return false;
                }
                num = DigitalEnvelop.SetAccessByPfx(this.CAFileName, this.CAPassWord);
                if (num != 0)
                {
                    errLen = 0x400;
                    DigitalEnvelop.GetErrInfo(err, ref errLen);
                    Msgs = new string(err);
                    switch (num)
                    {
                        case 1:
                            Msgs = Msgs + "由证书文件设置访问控制所用证书异常";
                            break;

                        case 2:
                            Msgs = Msgs + "证书口令错误";
                            break;
                    }
                    return false;
                }
                num = DigitalEnvelop.SetPrivateKeyAndCertByPfx(this.CAFileName, this.CAPassWord);
                DigitalEnvelop.GetErrInfo(err, ref errLen);
                if (num != 0)
                {
                    errLen = 0x400;
                    DigitalEnvelop.GetErrInfo(err, ref errLen);
                    Msgs = new string(err);
                    switch (num)
                    {
                        case 1:
                            Msgs = Msgs + "由证书文件设置所需证书和密钥异常";
                            break;

                        case 2:
                            Msgs = Msgs + "证书口令错误";
                            break;
                    }
                    return false;
                }
                FileStream stream = File.Open(sourFile, FileMode.Open);
                StreamReader reader1 = new StreamReader(stream);
                reader1.BaseStream.Seek(0L, SeekOrigin.Begin);
                stream.Close();
                byte[] bytes = ToolUtil.GetBytes(reader1.ReadToEnd());
                int length = bytes.Length;
                byte[] cert = null;
                byte[] dataOut = null;
                byte[] context = null;
                int certLen = -1;
                int dataOutLen = -1;
                int contextLen = -1;
                num = DigitalEnvelop.Unpack(bytes, length, cert, ref certLen, dataOut, ref dataOutLen);
                if (num == 0)
                {
                    cert = new byte[certLen + 1];
                    dataOut = new byte[dataOutLen + 1];
                    cert[certLen] = Convert.ToByte('\0');
                    dataOut[dataOutLen] = Convert.ToByte('\0');
                    DigitalEnvelop.Unpack(bytes, length, cert, ref certLen, dataOut, ref dataOutLen);
                    string text1 = ("Client certificate:\n" + ToolUtil.GetString(cert) + "\n") + "Client data:\n" + ToolUtil.GetString(dataOut);
                    num = DigitalEnvelop.GetExt1(ToolUtil.GetString(cert), "TAXID", context, ref contextLen);
                    if (num == 0)
                    {
                        context = new byte[contextLen + 1];
                        context[contextLen] = Convert.ToByte('\0');
                        DigitalEnvelop.GetExt1(ToolUtil.GetString(cert), "TAXID", context, ref contextLen);
                        string str2 = ToolUtil.GetString(context);
                        if (this.fileTaxCode.Trim() != str2.Substring(6, contextLen - 6).Trim())
                        {
                            Msgs = "单据中的税号为：" + this.fileTaxCode;
                            Msgs = Msgs + "\n证书中的税号为：" + str2.Substring(6, contextLen - 6).Trim();
                            Msgs = Msgs + "\n税号不一致,不能开具!";
                            return false;
                        }
                        FileStream stream1 = new FileStream(DestFile, FileMode.OpenOrCreate);
                        stream1.Write(dataOut, 0, dataOut.Length);
                        stream1.Flush();
                        stream1.Close();
                    }
                    else
                    {
                        errLen = 0x400;
                        DigitalEnvelop.GetErrInfo(err, ref errLen);
                        Msgs = new string(err);
                        if (num == 1)
                        {
                            Msgs = Msgs + "数据解包函数异常";
                        }
                        if (num > 1)
                        {
                            Msgs = Msgs + "证书信任相关错误：" + num.ToString();
                            if (num == 10)
                            {
                                Msgs = Msgs + "\r\n证书已经过期！";
                            }
                        }
                        if (num == -1)
                        {
                            Msgs = Msgs + "密钥错误";
                        }
                        else if (num == -2)
                        {
                            Msgs = Msgs + "签名错误";
                        }
                        else if (num == -3)
                        {
                            Msgs = Msgs + "无权限";
                        }
                        else if (num == -4)
                        {
                            Msgs = Msgs + "公钥解密错误";
                        }
                        else if (num == 10)
                        {
                            Msgs = Msgs + "证书已经过期";
                        }
                        return false;
                    }
                    DigitalEnvelop.DigEnvClose();
                    Msgs = "Success";
                    return flag;
                }
                if (num == 10)
                {
                    Msgs = Msgs + "解密失败，证书已经过期 ";
                }
                else
                {
                    Msgs = Msgs + "解密失败，错误代码 " + num.ToString();
                }
                return false;
            }
            catch (Exception exception)
            {
                Msgs = "Failed:" + exception.Message;
                this.log.Error(exception);
                return false;
            }
        }

        private void proxyInvDl()
        {
            this.CAFileName = PropertyUtil.GetValue("SWDK_CA_FILENAME");
            this.CAPassWord = PropertyUtil.GetValue("SWDK_CA_PWD");
            if ((this.CAFileName.Length == 0) || (this.CAPassWord.Length == 0))
            {
                MessageManager.ShowMsgBox("SWDK-0063");
            }
            else if (!this.IsCAExist(this.CAFileName))
            {
                MessageManager.ShowMsgBox("SWDK-0063");
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "税务代开单据(*.dat)|*.dat"
                };
                string destFile = PropertyUtil.GetValue("MAIN_PATH") + @"\bin\temp.dat";
                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = dialog.FileName;
                        if (!this.checkSwdkdjFileName(fileName))
                        {
                            MessageManager.ShowMsgBox("SWDK-0051");
                        }
                        else
                        {
                            string msgs = "";
                            string currentDirectory = Directory.GetCurrentDirectory();
                            Directory.SetCurrentDirectory(PropertyUtil.GetValue("MAIN_PATH") + @"\bin\");
                            if (!this.proxyInvCADL(fileName, destFile, out msgs))
                            {
                                string[] textArray1 = new string[] { msgs };
                                MessageManager.ShowMsgBox("SWDK-0052", textArray1);
                                Directory.SetCurrentDirectory(currentDirectory);
                            }
                            else
                            {
                                Dictionary<string, string> slInvHead = new Dictionary<string, string>();
                                List<Dictionary<string, string>> slInvBody = new List<Dictionary<string, string>>();
                                if (!this.parseProxyInvFile(destFile, ref slInvHead, ref slInvBody))
                                {
                                    MessageManager.ShowMsgBox("SWDK-0053");
                                    Directory.SetCurrentDirectory(currentDirectory);
                                }
                                else
                                {
                                    Directory.SetCurrentDirectory(currentDirectory);
                                    Fpxx fp = new Fpxx();
                                    if (!this.getDkdjdrFpxx(slInvHead, slInvBody, ref fp))
                                    {
                                        MessageManager.ShowMsgBox("SWDK-0053");
                                    }
                                    else if (!this.checkDkdjdrFpxx(fp))
                                    {
                                        MessageManager.ShowMsgBox("SWDK-0053");
                                    }
                                    else
                                    {
                                        this.Set_zkhMC(fp);
                                        this.RefreshData(fp.isRed);
                                        fp.fplx = base._fpxx.Fplx;
                                        base._fpxx.DelSpxxAll();
                                        this.SetDjDrfpQdbz(fp);
                                        bool flag = false;
                                        if (fp.isRed)
                                        {
                                            string str5 = this.ParseBz(fp.bz);
                                            if (!string.IsNullOrEmpty(str5))
                                            {
                                                if (base._fpxx.Fplx == 0)
                                                {
                                                    fp.redNum = str5;
                                                }
                                                else if (base._fpxx.Fplx == (FPLX)2)
                                                {
                                                    if (str5.Length == 20)
                                                    {
                                                        fp.blueFpdm = str5.Substring(0, 12);
                                                        fp.blueFphm = str5.Substring(12);
                                                    }
                                                    else if (str5.Length == 0x12)
                                                    {
                                                        fp.blueFpdm = str5.Substring(0, 10);
                                                        fp.blueFphm = str5.Substring(10);
                                                    }
                                                }
                                            }
                                            fp.xfsh = base._fpxx.Xfsh;
                                            flag = this.fpm.CopyRedNotice(fp, base._fpxx);
                                            if (this.fpm.IsSWDK())
                                            {
                                                base._fpxx.Dk_qysh = fp.dkqysh;
                                                base._fpxx.Dk_qymc = fp.dkqymc;
                                                base._fpxx.Xfyhzh = fp.xfyhzh;
                                                base._fpxx.Bz = fp.bz;
                                            }
                                            if ((fp.isRed && (base._fpxx.Fplx == (FPLX)2)) && ((fp.blueFpdm.Length == 0) || (fp.blueFphm.Length == 0)))
                                            {
                                                HZFPTK_PP_SWDK hzfptk_pp_swdk = new HZFPTK_PP_SWDK(base._fpxx.Fplx)
                                                {
                                                    mZyfplx = base._fpxx.Zyfplx,
                                                    blueFpdm = fp.blueFpdm,
                                                    blueFphm = fp.blueFphm
                                                };
                                                if (hzfptk_pp_swdk.ShowDialog() != DialogResult.OK)
                                                {
                                                    return;
                                                }
                                                string str6 = "";
                                                string str7 = "";
                                                int index = fp.bz.IndexOf("开具红字增值税专用发票信息表编号");
                                                if (fp.bz.Length >= (index + 0x20))
                                                {
                                                    str6 = fp.bz.Substring(0, index + 0x20);
                                                    str7 = fp.bz.Substring(index + 0x20, fp.bz.Length - (index + 0x20));
                                                    fp.bz = str6 + NotesUtil.GetRedInvNotes(hzfptk_pp_swdk.blueFpdm, hzfptk_pp_swdk.blueFphm) + str7;
                                                }
                                                else
                                                {
                                                    fp.bz = NotesUtil.GetRedInvNotes(hzfptk_pp_swdk.blueFpdm, hzfptk_pp_swdk.blueFphm);
                                                }
                                                fp.blueFpdm = hzfptk_pp_swdk.blueFpdm;
                                                fp.blueFphm = hzfptk_pp_swdk.blueFphm;
                                                base._fpxx.Bz = fp.bz;
                                                if (base._fpxx.Bz.Length > 230)
                                                {
                                                    base._fpxx.Bz = base._fpxx.Bz.Substring(0, 230);
                                                }
                                                fp.xfsh = base._fpxx.Xfsh;
                                                flag = this.fpm.CopyRedNotice(fp, base._fpxx);
                                                base._fpxx.Gfdzdh = fp.gfdzdh;
                                                base._fpxx.Gfyhzh = fp.gfyhzh;
                                                base._fpxx.Dk_qysh = fp.dkqysh;
                                                base._fpxx.Dk_qymc = fp.dkqymc;
                                                base._fpxx.Xfyhzh = fp.xfyhzh;
                                                this.ShowInvMainInfo();
                                            }
                                        }
                                        else
                                        {
                                            fp.bz = Convert.ToBase64String(ToolUtil.GetBytes(fp.bz));
                                            byte[] destinationArray = new byte[0x20];
                                            byte[] sourceArray = Invoice.TypeByte;
                                            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                                            byte[] buffer2 = new byte[0x10];
                                            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                                            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                                            Invoice.IsGfSqdFp_Static = false;
                                            fp.fpdm = base._fpxx.Fpdm;
                                            fp.fphm = base._fpxx.Fphm;
                                            fp.kprq = base._fpxx.Kprq;
                                            base._fpxx = new Invoice(false, fp, buffer3, null);
                                            base._fpxx.Dk_qysh = fp.dkqysh;
                                            base._fpxx.Dk_qymc = fp.dkqymc;
                                            base._fpxx.Xfyhzh = fp.xfyhzh;
                                            flag = base._fpxx.GetCode() == "0000";
                                        }
                                        if (flag)
                                        {
                                            base._fpxx.Bz = base._fpxx.Bz.ToUpper();
                                            if (base._fpxx.GetSpxxs().Count > 8)
                                            {
                                                base._fpxx.SetQdbz(true);
                                            }
                                            if (!this.DKSlv_isvalid())
                                            {
                                                MessageBox.Show("导入单据税率不合法！原因可能是包含1.5%税率的多税率发票单据！");
                                                this.RefreshData(false);
                                            }
                                            this.isdrdjdk = true;
                                            this.ResetQyxx();
                                            base._fpxx.Xfyhzh = fp.wspzhm;
                                            string[] strArray = this.MatchKhxx(fp.dkqysh, true);
                                            if (strArray.Length > 2)
                                            {
                                                this.ParseXfDzdh(strArray[1]);
                                            }
                                            base._DataGridView.Rows.Clear();
                                            this.ShowInvMainInfo();
                                            this.txt_bz.Text = this.SetDkBz(fp.dkqymc, fp.dkqysh);
                                            base._ShowDataGridMxxx(base._DataGridView);
                                            if (base._DataGridView.RowCount > 0)
                                            {
                                                base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, base._DataGridView.RowCount - 1];
                                            }
                                            if (base._spmcBt != null)
                                            {
                                                base._spmcBt.Visible = false;
                                            }
                                            if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                                            {
                                                this.SetHysyHsjxx(base._DataGridView, true);
                                            }
                                            else
                                            {
                                                this.SetHysyHsjxx(base._DataGridView, false);
                                            }
                                            if (base._fpxx.Qdbz)
                                            {
                                                base._AddRowButton.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                                            this.RefreshData(false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.log.Error(exception);
                }
            }
        }

        protected override void QingdanFormClosing(object sender, FormClosingEventArgs e)
        {
            QingDanTianKai kai = (QingDanTianKai)sender;
            base.QdToMx = kai.IsToMX;
            if (base._fpxx.CheckFpData())
            {
                base.QingdanFormClosing(sender, e);
            }
            else
            {
                if ((base._fpxx.GetCode() == "A126") || (base._fpxx.GetCode() == "A127"))
                {
                    MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                }
                base.QingdanFormClosing(sender, e);
            }
        }

        private void RefreshData(bool isRed)
        {
            this.prevCmbSlv = null;
            string str = base._fpxx.Kprq;
            string str2 = base._fpxx.Fpdm;
            string str3 = base._fpxx.Fphm;
            string str4 = PropertyUtil.GetValue("INV-HSJBZ", "0");
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static = false;
            base._fpxx = new Invoice(isRed, false, str4.Equals("1"), base._fpxx.Fplx, buffer3, null);
            base._fpxx.Bmbbbh = base.getbmbbbh();
            InvoiceForm.qingdanflag = base._fpxx.Qdbz;
            base._fpxx.SetZyfpLx(this.Zyfplx);
            base._fpxx.AddSpxx(this._SetDefaultSpsm(), this._SetDefaultSpsLv(), this.Zyfplx);
            base._DataGridView.Rows.Clear();
            this._ShowDataGrid(base._DataGridView, base._fpxx.GetSpxx(0), 0);
            base._fpxx.Fpdm = str2;
            base._fpxx.Fphm = str3;
            base._fpxx.Kprq = str;
            base._fpxx.Kpr = UserInfo.Yhmc;
            base._fpxx.Skr = this.com_skr.Text;
            base._fpxx.Fhr = this.com_fhr.Text;
            this.SetQyxx();
            base._ChaeButton.Enabled = true;
            base._ChaeButton.Checked = false;
            base._QingdanButton.Enabled = true;
            if (this.isSnyZyfp)
            {
                this.SetFpSnyLx();
            }
            base._fpxx.Gfmc = this.com_gfmc.Text;
            base._fpxx.Gfsh = this.com_gfsbh.Text;
            base._fpxx.Gfdzdh = this.com_gfdzdh.Text;
            base._fpxx.Gfyhzh = this.com_gfzh.Text;
            if (this.isNcpsgfp)
            {
                this.tool_kehu.ToolTipText = "保存销方信息";
                base._fpxx.Xfmc = this.com_xfmc.Text;
                base._fpxx.Xfsh = this.com_xfsbh.Text;
                base._fpxx.Xfdzdh = this.com_xfdzdh.Text;
                base._fpxx.Xfyhzh = this.com_xfzh.Text;
            }
            this.isCopy = false;
            this.blueJe = string.Empty;
            this.isdrdjdk = false;
            this.ShowInvMainInfo();
            if (!this.fpm.IsSWDK())
            {
                this.ParseXfyhzh(this.fpm.GetXfyhzh());
                this.ParseXfDzdh(this.fpm.GetXfdzdh());
            }
            base._DataGridView.ReadOnly = false;
            base._SetDataGridReadOnlyColumns(base._DataGridView);
            this._SetHzxx();
            base._AddRowButton.Enabled = true;
            base._DelRowButton.Enabled = true;
            this.tool_fushu.Checked = isRed;
            if (isRed)
            {
                base._QingdanButton.Visible = false;
                base._ZhekouButton.Visible = false;
                this.tool_fuzhi.Enabled = false;
                if (!this.isNcpsgfp && !this.isSnyZyfp)
                {
                    this.AddEmptySlv();
                }
            }
            else
            {
                if ((base.TaxCardInstance.StateInfo.CompanyType > 0) && (base._fpxx.Fplx == 0))
                {
                    base._QingdanButton.Visible = false;
                }
                else
                {
                    base._QingdanButton.Visible = true;
                }
                base._ZhekouButton.Visible = true;
                this.tool_fuzhi.Enabled = true;
                if (!this.isNcpsgfp && !this.isSnyZyfp)
                {
                    this.RemoveEmptySlv();
                }
            }
            base._HsjbzButton.Enabled = true;
            base._HsjbzButton.Checked = str4.Equals("1");
            if (this.isNcpsgfp)
            {
                this.com_xfmc.Edit = EditStyle.TextBox;
                this.com_xfsbh.Edit = EditStyle.TextBox;
                this.com_xfdzdh.Edit = EditStyle.TextBox;
                this.com_xfzh.Edit = EditStyle.TextBox;
            }
            else
            {
                this.com_gfmc.Edit = EditStyle.TextBox;
                this.com_gfsbh.Edit = EditStyle.TextBox;
                this.com_gfdzdh.Edit = EditStyle.TextBox;
                this.com_gfzh.Edit = EditStyle.TextBox;
            }
        }

        private void RegTextChangedEvent()
        {
            this.com_gfmc.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfmc.OnTextChanged, new EventHandler(this.com_gfmc_TextChanged));
            this.com_gfsbh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfsbh.OnTextChanged, new EventHandler(this.com_gfsbh_TextChanged));
            this.com_gfdzdh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfdzdh.OnTextChanged, new EventHandler(this.com_gfdzdh_TextChanged));
            this.com_gfzh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_gfzh.OnTextChanged, new EventHandler(this.com_gfzh_TextChanged));
            this.com_xfmc.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfmc.OnTextChanged, new EventHandler(this.com_xfmc_TextChanged));
            this.com_xfsbh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfsbh.OnTextChanged, new EventHandler(this.com_xfsbh_TextChanged));
            this.com_xfdzdh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfdzdh.OnTextChanged, new EventHandler(this.com_xfdzdh_TextChanged));
            this.com_xfzh.OnTextChanged = (EventHandler)Delegate.Combine(this.com_xfzh.OnTextChanged, new EventHandler(this.com_xfzh_TextChanged));
            this.com_skr.OnTextChanged = (EventHandler)Delegate.Combine(this.com_skr.OnTextChanged, new EventHandler(this.com_skr_TextChanged));
            this.com_fhr.OnTextChanged = (EventHandler)Delegate.Combine(this.com_fhr.OnTextChanged, new EventHandler(this.com_fhr_TextChanged));
            this.txt_bz.TextChanged += new EventHandler(this.txt_bz_TextChanged);
        }

        private void RemoveEmptySlv()
        {
            if (base.comboBox_SLV.Items.Count > 0)
            {
                int index = base.comboBox_SLV.Items.Count - 1;
                SLV slv = base.comboBox_SLV.Items[index] as SLV;
                if ((slv != null) && (slv.DataValue == ""))
                {
                    base.comboBox_SLV.Items.RemoveAt(index);
                }
            }
        }

        private void ResetQyxx()
        {
            if (base._fpxx != null)
            {
                if (this.Zyfplx == (ZYFP_LX)9)
                {
                    base._fpxx.Gfmc = this.fpm.GetXfmc();
                    base._fpxx.Gfsh = this.fpm.GetXfsh();
                }
                else
                {
                    base._fpxx.Xfmc = this.fpm.GetXfmc();
                    base._fpxx.Xfsh = this.fpm.GetXfsh();
                }
            }
        }

        private bool rewriteInvBody(List<Dictionary<string, string>> slInvBody)
        {
            string s = "";
            string str2 = "";
            string str3 = "";
            decimal num = new decimal();
            decimal num2 = new decimal();
            int disRow = 0;
            int num6 = 0;
            Dictionary<string, string> slValues = new Dictionary<string, string>();
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            for (int i = 0; i < slInvBody.Count; i++)
            {
                list.Add(DigitalEnvelop.Clone<Dictionary<string, string>>(slInvBody[i]));
            }
            try
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (this.parseInvBody(list[j], ref slValues))
                    {
                        str3 = slValues["zkl"];
                        s = slValues["zkje"];
                        str2 = slValues["zkse"];
                        if (((str3.Length == 0) || (s.Length == 0)) || (str2.Length == 0))
                        {
                            continue;
                        }
                        num = decimal.Parse(s);
                        num2 = decimal.Parse(str2);
                        int num9 = j + 1;
                        while (num9 < list.Count)
                        {
                            if (this.parseInvBody(list[num9], ref dictionary2))
                            {
                                if (dictionary2["zkl"] != str3)
                                {
                                    break;
                                }
                                decimal num3 = decimal.Parse(dictionary2["zkje"]);
                                decimal num4 = decimal.Parse(dictionary2["zkse"]);
                                num += num3;
                                num2 += num4;
                            }
                            num9++;
                        }
                        disRow = num9 - j;
                        decimal num10 = decimal.Parse(str3) * 100M;
                        this.GetDisCountRow("", num10.ToString(), disRow);
                        Dictionary<string, string> item = new Dictionary<string, string>();
                        item.Add("hwmc", disRow.ToString());
                        item.Add("jldw", "");
                        item.Add("gg", "");
                        item.Add("count", "");
                        num10 = num * decimal.MinusOne;
                        item.Add("bhsje", num10.ToString());
                        item.Add("sl", slValues["sl"]);
                        item.Add("spsm", slValues["spsm"]);
                        item.Add("zkje", "");
                        item.Add("se", (num2 * decimal.MinusOne).ToString());
                        item.Add("zkse", "");
                        item.Add("zkl", "");
                        item.Add("dj", "");
                        item.Add("jgfs", "");
                        item.Add("fphxz", "4");
                        slInvBody.Insert(num9 + num6, item);
                        j = num9 - 1;
                        num6++;
                        continue;
                    }
                    MessageManager.ShowMsgBox("SWDK-0066");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.log.Error(exception);
                return false;
            }
            return true;
        }

        private void SaveKH(bool autoSave)
        {
            object[] objArray = new object[4];
            if (this.isNcpsgfp)
            {
                objArray[0] = this.com_xfmc.Text;
                objArray[1] = this.com_xfsbh.Text;
                objArray[2] = this.com_xfdzdh.Text;
                objArray[3] = this.com_xfzh.Text;
            }
            else
            {
                objArray[0] = this.com_gfmc.Text;
                objArray[1] = this.com_gfsbh.Text;
                objArray[2] = this.com_gfdzdh.Text;
                objArray[3] = this.com_gfzh.Text;
            }
            if ((base._fpxx.Fplx == 0) && (this.com_gfsbh.Text.Trim() == ""))
            {
                MessageManager.ShowMsgBox("A024");
            }
            else if (autoSave)
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddKHAuto", objArray);
            }
            else
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLAddKH", objArray);
            }
        }

        private void Set_zkhMC(Fpxx fp)
        {
            double num;
            if (double.TryParse(fp.sLv, out num) && (num == 0.015))
            {
                fp.Zyfplx = (ZYFP_LX)10;
            }
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            list = (fp.Mxxx.Count == 0) ? fp.Qdxx : fp.Mxxx;
            string str = "0.000%";
            fp.sLv = list[0][(SPXX)8];
            for (int i = 0; i < list.Count; i++)
            {
                if (fp.sLv != list[i][(SPXX)8])
                {
                    fp.sLv = "";
                }
                if (list[i][(SPXX)10] == "4")
                {
                    list[i][(SPXX)5] = "";
                    list[i][(SPXX)6] = "";
                    list[i][(SPXX)4] = "";
                    list[i][(SPXX)3] = "";
                    if (list[i][0] == "1")
                    {
                        double num3 = (-double.Parse(list[i][(SPXX)7]) / double.Parse(list[i - 1][(SPXX)7])) * 100.0;
                        str = num3.ToString("F3");
                        list[i][0] = "折扣(" + str + "%)";
                        list[i - 1][(SPXX)10] = "3";
                    }
                    else
                    {
                        double num4 = 0.0;
                        for (int j = int.Parse(list[i][0]); j > 0; j--)
                        {
                            num4 += double.Parse(list[i - j][(SPXX)7]);
                            list[i - j][(SPXX)10] = "3";
                        }
                        str = ((-double.Parse(list[i][(SPXX)7]) / num4) * 100.0).ToString("F3");
                        string[] textArray1 = new string[] { "折扣行数", list[i][0], "(", str, "%)" };
                        list[i][0] = string.Concat(textArray1);
                    }
                }
            }
        }

        private void SetAutoKHChecked()
        {
            string str = PropertyUtil.GetValue("INV-AUTOSAVEKH", "0");
            this.tool_autokh.Checked = str != "0";
        }

        private void SetDjDrfpQdbz(Fpxx fpxx)
        {
            if ((fpxx.Qdxx != null) && (fpxx.Qdxx.Count > 0))
            {
                base._fpxx.SetQdbz(true);
            }
        }

        private string SetDkBz(string mc, string sh)
        {
            string str3;
            string str4;
            string text = this.txt_bz.Text;
            string dKInvNotes = NotesUtil.GetDKInvNotes(sh, mc);
            if (string.IsNullOrEmpty(dKInvNotes))
            {
                return text;
            }
            if (text.Trim() == "")
            {
                return dKInvNotes;
            }
            if (text.StartsWith(dKInvNotes))
            {
                return text;
            }
            if (NotesUtil.GetDKQYFromInvNotes(text, out str3, out str4).Equals("0000"))
            {
                string oldValue = NotesUtil.GetDKInvNotes(str3, str4);
                return text.Replace(oldValue, dKInvNotes);
            }
            return (dKInvNotes + "\r\n" + text);
        }

        private void SetDkXfmc(string xfdzdh)
        {
            if (this.fpm.IsSWDK())
            {
                base._fpxx.Xfdzdh = xfdzdh;
            }
        }

        private void SetDrfpQdbz(Fpxx fpxx)
        {
            if (base.TaxCardInstance.StateInfo.CompanyType > 0)
            {
                if ((base._fpxx.Fplx == (FPLX)2) && (fpxx.Mxxx.Count > 7))
                {
                    base._fpxx.SetQdbz(true);
                }
            }
            else if (fpxx.Mxxx.Count > 8)
            {
                base._fpxx.SetQdbz(true);
            }
        }

        private void SetDzdhInfo()
        {
            if (!base._onlyShow)
            {
                this.com_gfdzdh.IsSelectAll = true;
                this.com_gfdzdh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "DZDH", this.com_gfdzdh.Width));
                this.com_gfdzdh.DrawHead = false;
                if (!this.fpm.IsSWDK())
                {
                    this.com_xfdzdh.IsSelectAll = true;
                    this.com_xfdzdh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "DZDH", this.com_gfdzdh.Width));
                    this.com_xfdzdh.DrawHead = false;
                }
                if (this.isNcpsgfp)
                {
                    this.com_gfdzdh.ReadOnly = true;
                }
                else
                {
                    this.com_xfdzdh.ReadOnly = true;
                }
            }
        }

        private bool SetFpSnyLx()
        {
            if (this.com_yplx.SelectedItem != null)
            {
                ZYFP_LX zyfp_lx = (ZYFP_LX)2;
                string str = this.com_yplx.SelectedItem.ToString();
                List<Dictionary<SPXX, string>> spxxs = base._fpxx.GetSpxxs();
                for (int i = 0; i < spxxs.Count; i++)
                {
                    if ((spxxs[i][(SPXX)8].ToString() == "0.05") && (base._fpxx.Zyfplx == (ZYFP_LX)1))
                    {
                        this.com_yplx.SelectedIndexChanged -= new EventHandler(this.com_yplx_SelectedIndexChanged);
                        this.com_yplx.SelectedItem = null;
                        MessageManager.ShowMsgBox("INP-242175");
                        this.com_yplx.SelectedIndexChanged += new EventHandler(this.com_yplx_SelectedIndexChanged);
                        return false;
                    }
                    if (spxxs[i][(SPXX)8].ToString() == "0.015")
                    {
                        this.com_yplx.SelectedIndexChanged -= new EventHandler(this.com_yplx_SelectedIndexChanged);
                        this.com_yplx.SelectedItem = null;
                        string[] textArray1 = new string[] { "所选油品类型不能开具1.5%税率的明细行！" };
                        MessageManager.ShowMsgBox("INP-242185", textArray1);
                        this.com_yplx.SelectedIndexChanged += new EventHandler(this.com_yplx_SelectedIndexChanged);
                        return false;
                    }
                }
                switch (str)
                {
                    case "(石脑油)":
                        zyfp_lx = (ZYFP_LX)2;
                        break;

                    case "(石脑油DDZG)":
                        zyfp_lx = (ZYFP_LX)3;
                        break;

                    case "(燃料油)":
                        zyfp_lx = (ZYFP_LX)4;
                        break;

                    case "(燃料油DDZG)":
                        zyfp_lx = (ZYFP_LX)5;
                        break;
                }
                if (base._fpxx.SetZyfpLx(zyfp_lx))
                {
                    this.Zyfplx = zyfp_lx;
                    return true;
                }
                MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
            }
            return false;
        }

        private void SetGridStyle()
        {
            base._DataGridView.GridStyle = CustomStyle.invWare;
            base._DataGridView.BorderStyle = BorderStyle.FixedSingle;
            if ((int)this.mFplx == 2)
            {
                base._DataGridView.GridColor = Color.DodgerBlue;
            }
            else
            {
                base._DataGridView.GridColor = Color.Black;
            }
        }

        private void SetHysyHsjxx(CustomStyleDataGrid dataGridView1, bool start)
        {
            if (start)
            {
                dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                dataGridView1.Columns["JE"].HeaderText = "金额(不含税)";
                base._HsjbzButton.Click -= new EventHandler(this._hsjbzButton_Click);
                base._HsjbzButton.Checked = true;
                base._HsjbzButton.Enabled = false;
                base._HsjbzButton.Click += new EventHandler(this._hsjbzButton_Click);
                if (base._fpxx.Qdbz && (this.qd != null))
                {
                    base._DataGridView.Columns["DJ"].HeaderText = "单价(含税)";
                    base._DataGridView.Columns["JE"].HeaderText = "金额(不含税)";
                    this.qd.tool_jg.Click -= new EventHandler(this._hsjbzButton_Click);
                    this.qd.tool_jg.Checked = true;
                    this.qd.tool_jg.Enabled = false;
                    this.qd.tool_jg.Click += new EventHandler(this._hsjbzButton_Click);
                }
            }
            else
            {
                if (base._fpxx.Hsjbz)
                {
                    dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                    dataGridView1.Columns["JE"].HeaderText = "金额(含税)";
                }
                else
                {
                    dataGridView1.Columns["DJ"].HeaderText = "单价(不含税)";
                    dataGridView1.Columns["JE"].HeaderText = "金额(不含税)";
                }
                base._HsjbzButton.Enabled = true;
                base._HsjbzButton.Checked = base._fpxx.Hsjbz;
                if (base._fpxx.Qdbz && (this.qd != null))
                {
                    if (base._fpxx.Hsjbz)
                    {
                        base._DataGridView.Columns["DJ"].HeaderText = "单价(含税)";
                        base._DataGridView.Columns["JE"].HeaderText = "金额(含税)";
                    }
                    else
                    {
                        base._DataGridView.Columns["DJ"].HeaderText = "单价(不含税)";
                        base._DataGridView.Columns["JE"].HeaderText = "金额(不含税)";
                    }
                    this.qd.tool_jg.Enabled = true;
                    this.qd.tool_jg.Checked = base._fpxx.Hsjbz;
                }
            }
        }

        private void SetPrevSelSlv()
        {
            if (this.prevCmbSlv != null)
            {
                base.comboBox_SLV.SelectedIndexChanged -= new EventHandler(this._comboBox_SLV_SelectedIndexChanged);
                if (this.prevCmbSlv.ToString() == "中外合作油气田")
                {
                    base.comboBox_SLV.SelectedIndex = base.comboBox_SLV.Items.Count - 1;
                }
                else
                {
                    base.comboBox_SLV.SelectedItem = this.prevCmbSlv;
                }
                base.comboBox_SLV.SelectedIndexChanged += new EventHandler(this._comboBox_SLV_SelectedIndexChanged);
            }
        }

        private void SetQyxx()
        {
            string xfsh = this.fpm.GetXfsh();
            string xfmc = this.fpm.GetXfmc();
            string xfdzdh = this.fpm.GetXfdzdh();
            if (this.isNcpsgfp)
            {
                base._fpxx.Gfsh = xfsh;
                base._fpxx.Gfmc = xfmc;
                base._fpxx.Gfdzdh = xfdzdh;
            }
            else
            {
                base._fpxx.Xfsh = xfsh;
                base._fpxx.Xfmc = xfmc;
                if (!this.fpm.IsSWDK())
                {
                    base._fpxx.Xfdzdh = xfdzdh;
                }
            }
        }

        private void SetSkrAndFhr()
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Xtgl.UserRoleService", null);
            if (objArray != null)
            {
                this.DelTextChangedEvent();
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("YH");
                table.Columns.Add(column);
                foreach (string str2 in objArray[0] as List<string>)
                {
                    object[] values = new object[] { str2 };
                    table.Rows.Add(values);
                }
                string str = PropertyUtil.GetValue("INV-SKR-IDX", "");
                this.com_skr.DataSource = table;
                this.com_skr.Text = str;
                str = PropertyUtil.GetValue("INV-FHR-IDX", "");
                this.com_fhr.DataSource = table;
                this.com_fhr.Text = str;
                this.RegTextChangedEvent();
            }
        }

        private void SetSpxx(CustomStyleDataGrid parent, object[] spxx)
        {
            decimal num2;
            if ((this.isWM() && (spxx != null)) && (spxx.Length > 15))
            {
                spxx[11] = "";
                spxx[12] = "";
                spxx[14] = "";
                spxx[15] = "";
            }
            int rowIndex = parent.CurrentCell.RowIndex;
            parent.Rows[rowIndex].Cells["SL"].Value = "";
            base._fpxx.SetSL(rowIndex, "");
            if ((spxx == null) || (spxx.Length <= 15))
            {
                goto Label_0C47;
            }
            if (((!this.isWM() && (spxx.Length > 15)) && ((spxx[12].ToString().Trim() == "是") && (spxx[15].ToString().Trim() == ""))) && !base.Update_SP(spxx))
            {
                MessageBox.Show("更新商品库失败!");
                return;
            }
            if (base._ChaeButton.Checked)
            {
                if (base.isXT(spxx[1].ToString()))
                {
                    base._spmcBt.Text = "";
                    string[] textArray1 = new string[] { "当前发票类型不能填开稀土商品！" };
                    MessageManager.ShowMsgBox("INP-242185", textArray1);
                    return;
                }
                string str6 = ((double.Parse(spxx[3].ToString()) * 100.0)).ToString() + "%";
                if ((spxx[10].ToString().Trim() == "True") || (spxx[3].ToString().Trim() == "0.015"))
                {
                    base._spmcBt.Text = "";
                    string[] textArray2 = new string[] { "当前发票类型不能填开特殊税率的商品！" };
                    MessageManager.ShowMsgBox("INP-242185", textArray2);
                    return;
                }
                if (((this.prevCmbSlv != null) && (((SLV)this.prevCmbSlv).ToString().ToString() != str6)) && (base._fpxx.GetSpxxs().Count > 1))
                {
                    base._spmcBt.Text = "";
                    string[] textArray3 = new string[] { "差额税不支持多税率商品填开！" };
                    MessageManager.ShowMsgBox("INP-242185", textArray3);
                    return;
                }
            }
            if ((base._fpxx.Fplx != 0) && (spxx[10].ToString().Trim() == "True"))
            {
                base._spmcBt.Text = "";
                MessageManager.ShowMsgBox("INP-242172");
                return;
            }
            if ((this.isSnyZyfp && (spxx[10].ToString().Trim() == "True")) || (this.isSnyZyfp && (spxx[3].ToString().Trim() == "0.015")))
            {
                base._spmcBt.Text = "";
                string[] textArray4 = new string[] { "当前发票类型不能填开特殊税率的商品！" };
                MessageManager.ShowMsgBox("INP-242185", textArray4);
                return;
            }
            if ((base._fpxx.IsRed && (base._fpxx.Fplx == 0)) && (this.blueJe.Trim() != ""))
            {
                bool flag3 = false;
                if ((this.prevCmbSlv != null) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)1))
                {
                    flag3 = true;
                }
                if ((flag3 && (spxx[10].ToString().Trim() != "True")) || (!flag3 && (spxx[10].ToString().Trim() == "True")))
                {
                    base._spmcBt.Text = "";
                    MessageManager.ShowMsgBox("INP-242173");
                    this.SetPrevSelSlv();
                    return;
                }
            }
            if (base._fpxx.IsRed && (this.blueJe.Trim() != ""))
            {
                bool flag4 = false;
                if ((this.prevCmbSlv != null) && (((SLV)this.prevCmbSlv).Zyfplx == (ZYFP_LX)10))
                {
                    flag4 = true;
                }
                if ((flag4 && (spxx[3].ToString().Trim() != "0.015")) || (!flag4 && (spxx[3].ToString().Trim() == "0.015")))
                {
                    base._spmcBt.Text = "";
                    string[] textArray5 = new string[] { "红字发票中不能同时开具1.5%税率商品和非1.5%税率商品明细" };
                    MessageManager.ShowMsgBox("INP-242185", textArray5);
                    this.SetPrevSelSlv();
                    return;
                }
            }
            if (((base._fpxx.Zyfplx == (ZYFP_LX)1) && (spxx[10].ToString().Trim() != "True")) || ((base._fpxx.Zyfplx == (ZYFP_LX)10) && (spxx[3].ToString().Trim() != "0.015")))
            {
                base._spmcBt.Text = "";
                string[] textArray6 = new string[] { "商品", "\r\n原因：当前发票或所选商品不支持多税率填开。" };
                MessageManager.ShowMsgBox("INP-242207", textArray6);
                return;
            }
            if (base._fpxx.GetSpxxs().Count >= 2)
            {
                Dictionary<SPXX, string> dictionary = base._fpxx.GetSpxx(0);
                if (((spxx[3].ToString().Trim() == "0.015") && (dictionary[(SPXX)8].ToString() != "0.015")) || ((spxx[10].ToString().Trim() == "True") && (base._fpxx.Zyfplx != (ZYFP_LX)1)))
                {
                    base._spmcBt.Text = "";
                    string[] textArray7 = new string[] { "商品", "\r\n原因：当前发票或所选商品不支持多税率填开。" };
                    MessageManager.ShowMsgBox("INP-242207", textArray7);
                    return;
                }
            }
            if (!this.isWM())
            {
                if ((base._fpxx.Fplx == 0) && (spxx[11].ToString().Trim() == "303"))
                {
                    base._spmcBt.Text = "";
                    string[] textArray8 = new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" };
                    MessageManager.ShowMsgBox("INP-242207", textArray8);
                    return;
                }
                bool flag5 = base.isXT(spxx[1].ToString());
                object[] objArray1 = new object[] { spxx[11].ToString(), true, flag5 };
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", objArray1);
                if ((objArray != null) && !bool.Parse(objArray[0].ToString()))
                {
                    base._spmcBt.Text = "";
                    string[] textArray9 = new string[] { "商品", "\r\n可能原因：\r\n1、当前企业没有所选税收分类编码授权。\r\n2、当前版本所选税收分类编码可用状态为不可用。" };
                    MessageManager.ShowMsgBox("INP-242207", textArray9);
                    return;
                }
            }
            spxx[10].ToString().Trim();
            decimal.TryParse(spxx[3].ToString().Trim(), out num2);
            string str = spxx[11].ToString().Trim();
            if (!this.isWM() && (str.Length < 0x13))
            {
                str = str.PadRight(0x13, '0');
            }
            string str2 = spxx[12].ToString().Trim();
            string str3 = spxx[15].ToString().Trim();
            spxx[14].ToString().Trim();
            base._fpxx.SetLslvbs(rowIndex, "");
            base._fpxx.SetSpbh(rowIndex, spxx[0].ToString().Trim());
            base._fpxx.SetFlbm(rowIndex, str);
            base._fpxx.SetYhsm(rowIndex, str3);
            base.comboBox_SLV.Items.Clear();
            str2 = (str2 == "是") ? "1" : "0";
            base._fpxx.SetXsyh(rowIndex, str2);
            string str4 = spxx[1].ToString().Trim();
            if ((str4.Trim() == "") || (str4.Trim() == "0"))
            {
                str4 = "";
            }
            base._fpxx.SetSpmc(rowIndex, str4);
            this.SLVLIST_CHANGE(rowIndex);
            bool flag = false;
            int num3 = 0;
            if (base.comboBox_SLV.Items != null)
            {
                if (((base._fpxx.Fplx == 0) && (spxx[10].ToString().Trim() == "True")) && this.haveHYSY)
                {
                    MessageBox.Show(base._fpxx.Fplx.ToString());
                    MessageBox.Show(spxx[10].ToString());
                    num3 = base.comboBox_SLV.Items.Count - 1;
                    flag = true;
                }
                else
                {
                    switch (str3)
                    {
                        case "免税":
                        case "不征税":
                            {
                                IEnumerator enumerator = base.comboBox_SLV.Items.GetEnumerator();
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        object current = enumerator.Current;
                                        if (base.comboBox_SLV.Items[num3].ToString() == str3)
                                        {
                                            flag = true;
                                            break;
                                        }
                                        num3++;
                                    }
                                    goto Label_0905;
                                }
                            }
                    }
                    foreach (object obj2 in base.comboBox_SLV.Items)
                    {
                        if (obj2.ToString() != "")
                        {
                            decimal num5;
                            decimal.TryParse(base.GetSL(obj2.ToString()), out num5);
                            if (decimal.Compare(num5, num2) == 0)
                            {
                                flag = true;
                                break;
                            }
                        }
                        num3++;
                    }
                }
            }
        Label_0905:
            if (!flag)
            {
                base._spmcBt.Text = "";
                base._fpxx.SetFlbm(rowIndex, "");
                MessageManager.ShowMsgBox("INP-242164");
                return;
            }
            base.comboBox_SLV.SelectedIndex = num3;
            if (base.comboBox_SLV.SelectedItem.ToString() == "减按1.5%计算")
            {
                base._fpxx.SetZyfpLx((ZYFP_LX)10);
            }
            if (num2 == decimal.Zero)
            {
                if ((str2 == "1") && !this.isNcpsgfp)
                {
                    if (str3.Contains("免税"))
                    {
                        base._fpxx.SetLslvbs(rowIndex, "1");
                    }
                    else if (str3.Contains("出口零税"))
                    {
                        base._fpxx.SetLslvbs(rowIndex, "0");
                    }
                    else if (str3.Contains("不征税"))
                    {
                        base._fpxx.SetLslvbs(rowIndex, "2");
                    }
                }
                else
                {
                    base._fpxx.SetLslvbs(rowIndex, "3");
                }
            }
            else
            {
                base._fpxx.SetLslvbs(rowIndex, "");
            }
            if (base._fpxx.Fplx == 0)
            {
                ZYFP_LX zyfp_lx = base._fpxx.Zyfplx;
                if (spxx[10].ToString().Trim() == "True")
                {
                    zyfp_lx = (ZYFP_LX)1;
                }
                if (this.isSnyZyfp && ((int)zyfp_lx == 1))
                {
                    MessageManager.ShowMsgBox("INP-242175");
                    return;
                }
                if ((base._fpxx.Zyfplx != (ZYFP_LX)zyfp_lx) && !base._fpxx.SetZyfpLx(zyfp_lx))
                {
                    MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                    return;
                }
            }
            if (!base._fpxx.SetSLv(rowIndex, spxx[3].ToString().Trim()))
            {
                MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                return;
            }
            base._fpxx.SetSpsm(rowIndex, spxx[4].ToString().Trim());
            base._fpxx.SetGgxh(rowIndex, spxx[5].ToString().Trim());
            string str5 = spxx[6].ToString().Trim();
            if (this.isSnyZyfp)
            {
                str5 = "吨";
            }
            base._fpxx.SetJLdw(rowIndex, str5);
            bool flag2 = base._fpxx.Hsjbz;
            base._fpxx.Hsjbz = spxx[8].ToString().Trim().Equals("True");
            if (base._fpxx.Zyfplx == (ZYFP_LX)11)
            {
                base._fpxx.SetDj(rowIndex, "");
            }
            else
            {
                base._fpxx.SetDj(rowIndex, spxx[7].ToString().Trim());
            }
            base._fpxx.Hsjbz = flag2;
            if (!base._fpxx.SetDjHsjbz(rowIndex, spxx[8].ToString().Trim().Equals("True")))
            {
                MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                return;
            }
            this._ShowDataGrid(parent, base._fpxx.GetSpxx(rowIndex), rowIndex);
            if (!this.isSnyZyfp)
            {
                if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                {
                    this.SetHysyHsjxx(parent, true);
                }
                else
                {
                    this.SetHysyHsjxx(parent, false);
                }
            }
            base._spmcBt.Text = str4;
        Label_0C47:
            parent.Focus();
        }

        private void SetTitleFont()
        {
            string name = "楷体";
            foreach (FontFamily family in FontFamily.Families)
            {
                if (family.Name.StartsWith(name))
                {
                    name = family.Name;
                    break;
                }
            }
            this.lab_title.Font = new Font(name, 18.75f, FontStyle.Bold);
            this.lblDq.Font = new Font(name, 10f);
        }

        private void SetTkFormTitle(string fpdm, FPLX fplx)
        {
            string str = this.fpm.QueryXzqy(fpdm);
            string str2 = this.CreateTitleText(fplx);
            if (((int)fplx == 0) && this.isSnyZyfp)
            {
                this.Text = string.Format("开具石脑油、燃料油{0}", str2);
            }
            else
            {
                this.Text = string.Format("开具{0}", str2);
            }
            this.lab_title.Text = str;
            if (this.mFplx == 0)
            {
                if (this.lab_title.Text.Length == 3)
                {
                    this.lab_title.Location = new Point(this.lblDq.Location.X - 130, this.lab_title.Location.Y);
                }
                else
                {
                    this.lab_title.Location = new Point(this.lblDq.Location.X - 0x7e, this.lab_title.Location.Y);
                }
                this.lab_title.ForeColor = Color.Black;
            }
            else
            {
                if (this.lab_title.Text.Length == 3)
                {
                    this.lab_title.Location = new Point(this.lblDq.Location.X - 110, this.lab_title.Location.Y);
                }
                else
                {
                    this.lab_title.Location = new Point(this.lblDq.Location.X - 0x6a, this.lab_title.Location.Y);
                }
                this.lab_title.ForeColor = Color.FromArgb(0x1c, 0x60, 0xcd);
            }
            if (str.Length == 2)
            {
                str = str.Substring(0, 1) + "  " + str.Substring(1);
            }
            this.lblDq.Text = str;
        }

        private void SetYhzhInfo()
        {
            if (!base._onlyShow)
            {
                this.com_gfzh.IsSelectAll = true;
                this.com_gfzh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YHZH", this.com_gfzh.Width));
                this.com_gfzh.DrawHead = false;
                if (!this.fpm.IsSWDK())
                {
                    this.com_xfzh.IsSelectAll = true;
                    this.com_xfzh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YHZH", this.com_xfzh.Width));
                    this.com_xfzh.DrawHead = false;
                }
                if (this.isNcpsgfp)
                {
                    this.com_gfzh.ReadOnly = true;
                }
                else
                {
                    this.com_xfzh.ReadOnly = true;
                }
            }
        }

        private void ShowCopy(Fpxx fp)
        {
            base._fpxx.Bmbbbh = base.getbmbbbh();
            if (base._fpxx.GetCode() != "0000")
            {
                MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                this.RefreshData(false);
            }
            else
            {
                this.ResetQyxx();
                if (((base._fpxx.Fplx == (FPLX)2) && (this.Zyfplx == 0)) && (fp.Zyfplx == (ZYFP_LX)8))
                {
                    base._fpxx.SetZyfpLx(this.Zyfplx);
                }
                if (((base._fpxx.Fplx == 0) && (this.Zyfplx == 0)) && (((fp.Zyfplx == (ZYFP_LX)2) || (fp.Zyfplx == (ZYFP_LX)3)) || ((fp.Zyfplx == (ZYFP_LX)4) || (fp.Zyfplx == (ZYFP_LX)5))))
                {
                    base._fpxx.SetZyfpLx(this.Zyfplx);
                }
                if ((base._fpxx.Zyfplx == (ZYFP_LX)11) && !this.fpm.IsSWDK())
                {
                    int index = base._fpxx.Bz.IndexOf("差额征税");
                    int num2 = base._fpxx.Bz.IndexOf("。", index);
                    base._fpxx.Bz = base._fpxx.Bz.Remove(index, (num2 - index) + 1);
                }
                this.ShowCopyMainInfo();
                base._DataGridView.Rows.Clear();
                base._ShowDataGridMxxx(base._DataGridView);
                if (base._spmcBt != null)
                {
                    base._spmcBt.Visible = false;
                }
                if (base._fpxx.GetSpxx(0)[(SPXX)8].ToString() == "0.015")
                {
                    base._fpxx.SetZyfpLx((ZYFP_LX)10);
                }
                if (base._DataGridView.RowCount > 0)
                {
                    base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, base._DataGridView.RowCount - 1];
                }
                if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                {
                    this.SetHysyHsjxx(base._DataGridView, true);
                }
                else
                {
                    this.SetHysyHsjxx(base._DataGridView, false);
                }
                if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0))
                {
                    base._AddRowButton.Enabled = false;
                }
                this.isCopy = true;
                if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                {
                    base._ChaeButton.Checked = true;
                    base._QingdanButton.Enabled = false;
                }
            }
        }

        private void ShowCopyMainInfo()
        {
            this.DelTextChangedEvent();
            if (this.isNcpsgfp)
            {
                this.com_xfmc.Text = base._fpxx.Xfmc;
                this.com_xfsbh.Text = base._fpxx.Xfsh;
                this.com_xfzh.Text = base._fpxx.Xfyhzh;
                this.com_xfdzdh.Text = base._fpxx.Xfdzdh;
                this.com_gfzh.Text = base._fpxx.Gfyhzh;
            }
            else
            {
                this.com_gfmc.Text = base._fpxx.Gfmc;
                this.com_gfsbh.Text = base._fpxx.Gfsh;
                this.com_gfzh.Text = base._fpxx.Gfyhzh;
                this.com_gfdzdh.Text = base._fpxx.Gfdzdh;
                this.com_xfzh.Text = base._fpxx.Xfyhzh;
                if (this.fpm.IsSWDK())
                {
                    this.com_xfdzdh.Text = base._fpxx.Xfdzdh;
                }
            }
            this.com_skr.Text = base._fpxx.Skr;
            this.com_fhr.Text = base._fpxx.Fhr;
            this.txt_bz.Text = base._fpxx.Bz;
            this.RegTextChangedEvent();
        }

        internal void ShowImprotFp(Djfp djfp)
        {
            base._spmcBt.Visible = false;
            try
            {
                if ((djfp != null) && (djfp.Fpxx != null))
                {
                    base._DataGridView.Rows.Clear();
                    this.ShowInvMainInfo();
                    if (this.isNcpsgfp)
                    {
                        this.ParseXfyhzh(base._fpxx.Gfyhzh);
                    }
                    else
                    {
                        this.ParseXfyhzh(base._fpxx.Xfyhzh);
                    }
                    base._ShowDataGridMxxx(base._DataGridView);
                }
                else
                {
                    this.RefreshData(false);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Visible = true;
            }
        }

        private void ShowInvMainInfo()
        {
            this.DelTextChangedEvent();
            this.lab_fpdm.Text = base._fpxx.Fpdm;
            this.lab_fphm.Text = base._fpxx.Fphm;
            this.lab_kprq.Text = base._fpxx.Kprq;
            this.com_gfsbh.Text = base._fpxx.Gfsh;
            this.com_gfmc.Text = base._fpxx.Gfmc;
            this.com_gfdzdh.Text = base._fpxx.Gfdzdh;
            this.com_gfzh.Text = base._fpxx.Gfyhzh;
            this.com_xfmc.Text = base._fpxx.Xfmc;
            this.com_xfsbh.Text = base._fpxx.Xfsh;
            this.com_xfdzdh.Text = base._fpxx.Xfdzdh;
            this.com_xfzh.Text = base._fpxx.Xfyhzh;
            this.com_fhr.Text = base._fpxx.Fhr;
            this.com_skr.Text = base._fpxx.Skr;
            this.lab_kp.Text = base._fpxx.Kpr;
            this.txt_bz.Text = base._fpxx.Bz;
            this.RegTextChangedEvent();
        }

        private void SLVLIST_CHANGE(int rowidx)
        {
            Dictionary<SPXX, string> dictionary = base._fpxx.GetSpxxs()[rowidx];
            if ((dictionary[0].ToString() == "详见对应正数发票及清单") && base._fpxx.IsRed)
            {
                base._fpxx.SetYhsm(rowidx, "");
                base._fpxx.SetGgxh(rowidx, "");
                base._fpxx.SetFlbm(rowidx, "");
                base._fpxx.SetLslvbs(rowidx, "");
                base._fpxx.SetXsyh(rowidx, "0");
            }
            dictionary = base._fpxx.GetSpxxs()[rowidx];
            string str = dictionary[(SPXX)0x16];
            string local1 = dictionary[(SPXX)8];
            string sqSLv = "";
            sqSLv = base._fpxx.GetSqSLv();
            this.haveHYSY = false;
            this.have5Slv = false;
            char[] separator = new char[] { ';' };
            string[] strArray = sqSLv.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            bool flag = false;
            if (sqSLv.Contains("0.000") || sqSLv.Contains("0"))
            {
                flag = true;
            }
            string[] sYSlv = base.GetSYSlv(dictionary[(SPXX)20], false);
            List<string> list = new List<string>();
            if (strArray.Length >= 1)
            {
                char[] chArray2 = new char[] { ',' };
                string[] strArray3 = strArray[0].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(strArray3.ToList<string>());
                this.have5Slv = strArray[0].Contains("0.05") || strArray[0].Contains("0.050");
            }
            if (strArray.Length >= 2)
            {
                char[] chArray3 = new char[] { ',' };
                string[] strArray4 = strArray[1].Split(chArray3, StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(strArray4.ToList<string>());
                this.haveHYSY = strArray[1].Contains("0.05") || strArray[1].Contains("0.050");
            }
            if (base.yhzc2slv(str).Contains("0.05") || sYSlv.Contains<string>("0.05"))
            {
                this.have5Slv = true;
            }
            list.AddRange(base.yhzc2slv(str));
            list.AddRange(sYSlv.ToList<string>());
            List<double> source = new List<double>();
            using (List<string>.Enumerator enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    double result = 0.0;
                    if (double.TryParse(enumerator.Current, out result))
                    {
                        source.Add(result);
                    }
                }
            }
            //source = source.GroupBy<double, double>((serializeClass.staticFunc_1 ?? (serializeClass.staticFunc_1 = new Func<double, double>(serializeClass.instance.slvlistFunc_1)))).Select<IGrouping<double, double>, double>((serializeClass.staticFunc_2 ?? (serializeClass.staticFunc_2 = new Func<IGrouping<double, double>, double>(serializeClass.instance.slvlistFunc_2)))).ToList<double>();
            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (source[i] == source[j])
                    {
                        source.Remove(source[i]);
                        break;
                    }
                }
            }
            base.comboBox_SLV.Items.Clear();
            Dictionary<FPLX, List<SLV>> dictionary2 = base._GetSLvList();
            if (base._fpxx != null)
            {
                List<SLV> list3 = dictionary2[(FPLX)0];
                List<SLV> list4 = dictionary2[(FPLX)2];
                list3.Clear();
                list4.Clear();
                source.Sort();
                source.Reverse();
                if (base._fpxx.Fplx == 0)
                {
                    if (base._fpxx.IsRed)
                    {
                        list3.Add(new SLV(0, 0, "", "", ""));
                    }
                    foreach (double num2 in source)
                    {
                        decimal d = Math.Round(decimal.Parse(num2.ToString()), 3);
                        if (d == 0.0M)
                            continue;
                        else if (d == 0.015M)
                            list3.Add(new SLV((FPLX)0, (ZYFP_LX)10, num2.ToString(), "减按1.5%计算", "减按1.5%计算"));
                        else if (d == 0.05M)
                        {
                            if (this.have5Slv)
                            {
                                list3.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                            }
                        }
                        else
                        {
                            decimal num3 = d * 100.0M;
                            string str3 = num3.ToString() + "%";
                            list3.Add(new SLV(0, 0, d.ToString(), str3, str3));
                        }
                        //switch (num2)
                        //{
                        //    case 0.05:
                        //        if (this.have5Slv)
                        //        {
                        //            list3.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                        //        }
                        //        break;

                        //    case 0.0:
                        //        break;

                        //    case 0.015:
                        //        list3.Add(new SLV((FPLX)0, (ZYFP_LX)10, num2.ToString(), "减按1.5%计算", "减按1.5%计算"));
                        //        break;

                        //    default:
                        //    {
                        //        double num3 = num2 * 100.0;
                        //        string str3 = num3.ToString() + "%";
                        //        list3.Add(new SLV(0, 0, num2.ToString(), str3, str3));
                        //        break;
                        //    }
                        //}
                    }
                    if (this.haveHYSY)
                    {
                        list3.Add(new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", "中外合作油气田"));
                    }
                }
                else if (base._fpxx.Fplx == (FPLX)2)
                {
                    if (base._fpxx.IsRed && !this.isNcpsgfp)
                    {
                        list4.Add(new SLV((FPLX)2, 0, "", "", ""));
                    }
                    if (this.isNcpsgfp)
                    {
                        if (FLBM_lock.isFlbm())
                        {
                            list4.Add(new SLV((FPLX)2, (ZYFP_LX)9, "0.00", "0%", "0%"));
                        }
                        else
                        {
                            list4.Add(new SLV((FPLX)2, (ZYFP_LX)9, "0.00", "免税", "免税"));
                        }
                    }
                    else
                    {
                        foreach (double num4 in source)
                        {
                            decimal d = Math.Round(decimal.Parse(num4.ToString()), 3);
                            if (d == 0.0M)
                            {
                                if (!FLBM_lock.isFlbm())
                                {
                                    list4.Add(new SLV((FPLX)2, 0, "0.00", "免税", "免税"));
                                }
                                else
                                {
                                    if (str.Contains("出口零税") | flag)
                                    {
                                        list4.Add(new SLV((FPLX)2, 0, "0.00", "0%", "0%"));
                                    }
                                    if (str.Contains("免税"))
                                    {
                                        list4.Add(new SLV((FPLX)2, 0, "0.00", "免税", "免税"));
                                    }
                                    if (str.Contains("不征税"))
                                    {
                                        list4.Add(new SLV((FPLX)2, 0, "0.00", "不征税", "不征税"));
                                    }
                                }
                            }
                            else if (d == 0.05M)
                            {
                                if (this.have5Slv)
                                {
                                    list4.Add(new SLV((FPLX)2, 0, "0.05", "5%", "5%"));
                                }
                            }
                            else
                            {
                                if (d == 0.015M)
                                {
                                    list4.Add(new SLV((FPLX)2, (ZYFP_LX)10, num4.ToString(), "减按1.5%计算", "减按1.5%计算"));
                                }
                                else
                                {
                                    string str4 = ((num4 * 100.0)).ToString() + "%";
                                    list4.Add(new SLV((FPLX)2, 0, num4.ToString(), str4, str4));
                                }
                            }
                            //switch (num4)
                            //{
                            //    case 0.05:
                            //        if (this.have5Slv)
                            //        {
                            //            list4.Add(new SLV((FPLX)2, 0, "0.05", "5%", "5%"));
                            //        }
                            //        break;

                            //    case 0.0:
                            //        if (!FLBM_lock.isFlbm())
                            //        {
                            //            list4.Add(new SLV((FPLX)2, 0, "0.00", "免税", "免税"));
                            //        }
                            //        else
                            //        {
                            //            if (str.Contains("出口零税") | flag)
                            //            {
                            //                list4.Add(new SLV((FPLX)2, 0, "0.00", "0%", "0%"));
                            //            }
                            //            if (str.Contains("免税"))
                            //            {
                            //                list4.Add(new SLV((FPLX)2, 0, "0.00", "免税", "免税"));
                            //            }
                            //            if (str.Contains("不征税"))
                            //            {
                            //                list4.Add(new SLV((FPLX)2, 0, "0.00", "不征税", "不征税"));
                            //            }
                            //        }
                            //        break;

                            //    default:
                            //        if (num4 == 0.015)
                            //        {
                            //            list4.Add(new SLV((FPLX)2, (ZYFP_LX)10, num4.ToString(), "减按1.5%计算", "减按1.5%计算"));
                            //        }
                            //        else
                            //        {
                            //            string str4 = ((num4 * 100.0)).ToString() + "%";
                            //            list4.Add(new SLV((FPLX)2, 0, num4.ToString(), str4, str4));
                            //        }
                            //        break;
                            //}
                        }
                    }
                }
            }
            base.comboBox_SLV.Items.AddRange(dictionary2[base._fpxx.Fplx].ToArray());
        }

        private void tool_autoImport_Click(object sender, EventArgs e)
        {
            this.IsHYXXB = false;
            this.isHzwm = false;
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                if (this.isSnyZyfp && (this.com_yplx.SelectedIndex == -1))
                {
                    MessageManager.ShowMsgBox("INP-242144");
                }
                else
                {
                    AutoImport import = new AutoImport(base._fpxx.Fplx, base._fpxx.GetSqSLv(), base._fpxx.Zyfplx);
                    if (!AutoImport.PathIsNull)
                    {
                        string str = base._fpxx.Fpdm;
                        string str2 = base._fpxx.Fphm;
                        import.CurFpdm = str;
                        import.CurFphm = str2;
                        import.FPTKForm = this;
                        if ((import.ShowDialog() == DialogResult.Cancel) && ((str != import.CurFpdm) || (str2 != import.CurFphm)))
                        {
                            if ((import.CurFpdm == "0000000000") || (import.CurFphm == "00000000"))
                            {
                                string text1 = this.fpm.Code();
                                MessageManager.ShowMsgBox(text1);
                                if (text1 != "000000")
                                {
                                    base.FormClosing -= new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                                    base.Close();
                                }
                                base.Close();
                            }
                            else
                            {
                                this.GetNextFp();
                                this.ShowInvMainInfo();
                                this.ParseXfyhzh(base._fpxx.Xfyhzh);
                                base._DataGridView.Rows.Clear();
                                this._SetHsjxx(base._DataGridView, base._fpxx.Hsjbz);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void tool_autokh_Click(object sender, EventArgs e)
        {
            PropertyUtil.SetValue("INV-AUTOSAVEKH", this.tool_autokh.Checked ? "1" : "0");
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void tool_DaoRuHZTZD_Click(object sender, EventArgs e)
        {
            this.IsHYXXB = false;
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "红字发票信息表|*.xml;*.dat"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Fpxx fp = this.fpm.ProcessRedNotice(dialog.FileName);
                if ((fp != null) && (fp.bmbbbh == ""))
                {
                    this.isHzwm = true;
                }
                this.IsHYXXB = true;
                this.ImportFpData(fp);
            }
            else
            {
                this.tool_fushu.Checked = false;
                this.isHzwm = false;
                if (base._fpxx.IsRed)
                {
                    this.RefreshData(false);
                }
            }
            if (base._DataGridView.RowCount > 0)
            {
                base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, 0];
            }
        }

        private void tool_dkdjdr_Click(object sender, EventArgs e)
        {
            if (!RegisterManager.CheckRegFile("DKST"))
            {
                MessageManager.ShowMsgBox("SWDK-0068");
            }
            else
            {
                this.proxyInvDl();
            }
        }

        private void tool_dkdr_Click(object sender, EventArgs e)
        {
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                WSPZCXForm form = new WSPZCXForm(this.mFplx)
                {
                    IsRed = base._fpxx.IsRed
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string text = this.txt_bz.Text;
                    string blueJe = this.blueJe;
                    this.RefreshData(form.IsRed);
                    SwdkFpxx swdkFpxxRet = form.swdkFpxxRet;
                    if ((swdkFpxxRet != null) && (swdkFpxxRet.fpxx != null))
                    {
                        if ((int)this.mFplx == 2)
                        {
                            this._SetHsjxx(base._DataGridView, true);
                        }
                        Fpxx fpxx = swdkFpxxRet.fpxx;
                        this.Set_zkhMC(fpxx);
                        fpxx.fplx = base._fpxx.Fplx;
                        base._fpxx.DelSpxxAll();
                        this.SetDrfpQdbz(fpxx);
                        if (!this.fpm.ParseBz(fpxx))
                        {
                            MessageBox.Show("备注信息与发票类型不统一！");
                            this.RefreshData(false);
                        }
                        else
                        {
                            bool flag = false;
                            if (fpxx.isRed)
                            {
                                string str3 = this.ParseBz(text);
                                if (!string.IsNullOrEmpty(str3))
                                {
                                    if (base._fpxx.Fplx != 0)
                                    {
                                        if (base._fpxx.Fplx == (FPLX)2)
                                        {
                                            if (str3.Length != 20)
                                            {
                                                if (str3.Length != 0x12)
                                                {
                                                    MessageManager.ShowMsgBox("SWDK-0018");
                                                    return;
                                                }
                                                fpxx.blueFpdm = str3.Substring(0, 10);
                                                fpxx.blueFphm = str3.Substring(10);
                                            }
                                            else
                                            {
                                                fpxx.blueFpdm = str3.Substring(0, 12);
                                                fpxx.blueFphm = str3.Substring(12);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fpxx.redNum = str3;
                                    }
                                }
                                else
                                {
                                    if (base._fpxx.Fplx == 0)
                                    {
                                        MessageManager.ShowMsgBox("SWDK-0017");
                                        return;
                                    }
                                    if (base._fpxx.Fplx == (FPLX)2)
                                    {
                                        MessageManager.ShowMsgBox("SWDK-0018");
                                        return;
                                    }
                                }
                                fpxx.xfsh = base._fpxx.Xfsh;
                                flag = this.fpm.CopyRedNotice(fpxx, base._fpxx);
                                base._fpxx.Gfdzdh = fpxx.gfdzdh;
                                base._fpxx.Gfyhzh = fpxx.gfyhzh;
                                base._fpxx.Dk_qysh = fpxx.dkqysh;
                                base._fpxx.Dk_qymc = fpxx.dkqymc;
                                base._fpxx.Xfyhzh = fpxx.xfyhzh;
                                base._fpxx.Bz = text;
                                this.blueJe = blueJe;
                            }
                            else
                            {
                                fpxx.bz = Convert.ToBase64String(ToolUtil.GetBytes(fpxx.bz));
                                byte[] destinationArray = new byte[0x20];
                                byte[] sourceArray = Invoice.TypeByte;
                                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                                byte[] buffer2 = new byte[0x10];
                                Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                                byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                                Invoice.IsGfSqdFp_Static = false;
                                fpxx.fpdm = base._fpxx.Fpdm;
                                fpxx.fphm = base._fpxx.Fphm;
                                fpxx.kprq = base._fpxx.Kprq;
                                base._fpxx = new Invoice(false, fpxx, buffer3, null);
                                base._fpxx.Dk_qysh = fpxx.dkqysh;
                                base._fpxx.Dk_qymc = fpxx.dkqymc;
                                base._fpxx.Xfyhzh = fpxx.xfyhzh;
                                flag = base._fpxx.GetCode() == "0000";
                            }
                            if (flag)
                            {
                                base._fpxx.Kpr = UserInfo.Yhmc;
                                base._fpxx.Bz = base._fpxx.Bz.ToUpper();
                                if (base._fpxx.GetSpxxs().Count > 8)
                                {
                                    base._fpxx.SetQdbz(true);
                                }
                                if (!this.DKSlv_isvalid())
                                {
                                    MessageBox.Show("导入单据税率不合法！原因可能是包含1.5%税率的多税率发票单据！");
                                    this.RefreshData(false);
                                }
                                this.ResetQyxx();
                                base._fpxx.Xfyhzh = fpxx.wspzhm;
                                string[] strArray = this.MatchKhxx(fpxx.dkqysh, true);
                                if (strArray.Length > 2)
                                {
                                    string str4 = this.ConvertXfdzdh(strArray[1]);
                                    base._fpxx.Xfdzdh = str4;
                                }
                                base._DataGridView.Rows.Clear();
                                this.ShowInvMainInfo();
                                this.txt_bz.Text = this.SetDkBz(fpxx.dkqymc, fpxx.dkqysh);
                                base._ShowDataGridMxxx(base._DataGridView);
                                if (base._DataGridView.RowCount > 0)
                                {
                                    base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, base._DataGridView.RowCount - 1];
                                }
                                if (base._spmcBt != null)
                                {
                                    base._spmcBt.Visible = false;
                                }
                                if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                                {
                                    base._QingdanButton.Enabled = false;
                                    base._ChaeButton.Checked = true;
                                }
                                if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                                {
                                    this.SetHysyHsjxx(base._DataGridView, true);
                                }
                                else
                                {
                                    this.SetHysyHsjxx(base._DataGridView, false);
                                }
                                if (base._fpxx.Qdbz)
                                {
                                    base._AddRowButton.Enabled = false;
                                }
                            }
                            else
                            {
                                MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                                this.RefreshData(false);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void tool_drgp_Click(object sender, EventArgs e)
        {
            this.IsHYXXB = false;
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Hzfp.HZFPGetXXBInfo", null);
            if ((objArray != null) && (objArray.Length != 0))
            {
                string fileXML = objArray[0].ToString();
                if (fileXML.Length > 0)
                {
                    Fpxx fp = this.fpm.ProcessHZTZDxml(fileXML);
                    if ((fp != null) && (fp.bmbbbh == ""))
                    {
                        this.isHzwm = true;
                    }
                    this.IsHYXXB = true;
                    this.ImportFpData(fp);
                }
            }
            else
            {
                this.tool_fushu.Checked = false;
                this.isHzwm = false;
                if (base._fpxx.IsRed)
                {
                    this.RefreshData(false);
                }
            }
        }

        private void tool_fanlan_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "红字发票反开蓝字发票(*.dat)|*.dat"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Fpxx fpxx = this.ProcessFanlan(dialog.FileName);
                if (fpxx == null)
                {
                    MessageManager.ShowMsgBox("INP-242127");
                    return;
                }
                if (this.CheckInvBlue(fpxx))
                {
                    fpxx.gfsh = "000000123456789";
                    fpxx.gfmc = "见红字发票购方名称";
                    fpxx.bz = string.Format("开具红字增值税专用发票通知单号{0}\r\n对应红字发票代码：{1} 号码：{2}", fpxx.redNum, fpxx.fpdm, fpxx.fphm);
                    this.RefreshData(false);
                    base._fpxx.Hsjbz = false;
                    this._SetHsjxx(base._DataGridView, false);
                    if (this.fpm.CopyRevBlueNotice(fpxx, base._fpxx))
                    {
                        this.ShowInvMainInfo();
                        base._ShowDataGridMxxx(base._DataGridView);
                        this.com_gfmc.Edit = 0;
                        this.com_gfsbh.Edit = 0;
                        this.com_gfdzdh.Edit = 0;
                        this.com_gfzh.Edit = 0;
                        base._DataGridView.ReadOnly = true;
                        base._AddRowButton.Enabled = false;
                        base._spmcBt.Visible = false;
                    }
                    else
                    {
                        MessageManager.ShowMsgBox(this.fpm.Code());
                    }
                }
            }
            this.tool_fanlan.Checked = false;
        }

        private void tool_fushu_Click(object sender, EventArgs e)
        {
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                if (base._fpxx.Fplx == 0)
                {
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tool_zjkj, this.tool_DaoRuHZTZD, this.tool_drgp };
                    strip.Items.AddRange(toolStripItems);
                    strip.Show(this, new Point(this.tool_fushu.Bounds.X, this.tool_fushu.Bounds.Bottom));
                }
                else
                {
                    if (!this.tool_fushu.Checked)
                    {
                        this.tool_fushu.Checked = true;
                        HZFPTK_PP hzfptk_pp = new HZFPTK_PP(base._fpxx.Fplx)
                        {
                            mZyfplx = base._fpxx.Zyfplx
                        };
                        if (hzfptk_pp.ShowDialog() == DialogResult.OK)
                        {
                            this.RefreshData(true);
                            Fpxx blueFpxx = hzfptk_pp.blueFpxx;
                            this.isHzwm = false;
                            if (blueFpxx != null)
                            {
                                double num;
                                if ((blueFpxx.bmbbbh == null) || (blueFpxx.bmbbbh == ""))
                                {
                                    base._fpxx.Bmbbbh = "";
                                    this.isHzwm = true;
                                }
                                else
                                {
                                    base._fpxx.Bmbbbh = blueFpxx.bmbbbh;
                                }
                                this.blueJe = blueFpxx.je;
                                byte[] destinationArray = new byte[0x20];
                                byte[] sourceArray = Invoice.TypeByte;
                                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                                byte[] buffer2 = new byte[0x10];
                                Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                                byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                                Invoice.IsGfSqdFp_Static = false;
                                Invoice invoice = new Invoice(base._fpxx.Hsjbz, blueFpxx, buffer3, null);
                                string str = base._fpxx.Fpdm;
                                string str2 = base._fpxx.Fphm;
                                string str3 = base._fpxx.Kprq;
                                Invoice redInvoice = invoice.GetRedInvoice(base._fpxx.Hsjbz);
                                if ((redInvoice == null) || (invoice.GetCode() != "0000"))
                                {
                                    MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                                }
                                base._fpxx = redInvoice;
                                base._fpxx.Fpdm = str;
                                base._fpxx.Fphm = str2;
                                base._fpxx.Kprq = str3;
                                if ((base._fpxx.Fplx == (FPLX)2) && (base._fpxx.Zyfplx != (ZYFP_LX)11))
                                {
                                    base._fpxx.SetZyfpLx(this.Zyfplx);
                                }
                                if (double.TryParse(base._fpxx.SLv, out num) && (num == 0.015))
                                {
                                    base._fpxx.SetZyfpLx((ZYFP_LX)10);
                                }
                                base._fpxx.Gfmc = invoice.Gfmc;
                                this.ResetQyxx();
                                this.ClearDkXfmc();
                                base._fpxx.Kpr = UserInfo.Yhmc;
                                base._fpxx.Fhr = blueFpxx.fhr;
                                base._fpxx.Skr = blueFpxx.skr;
                                if (base._fpxx.GetSpxxs().Count == 0)
                                {
                                    base._DataGridView.Rows.Clear();
                                }
                                base.reset_fpxx(base._fpxx.Zyfplx == (ZYFP_LX)1);
                                base._ShowDataGridMxxx(base._DataGridView);
                                base._spmcBt.Visible = false;
                                if (base._DataGridView.Rows.Count > 0)
                                {
                                    base._DataGridView.Rows[0].Cells[base._DataGridView.ColumnCount - 1].Selected = true;
                                }
                                if (this.isNcpsgfp)
                                {
                                    this.com_xfmc.Edit = 0;
                                    this.com_xfsbh.Edit = 0;
                                    this.com_xfdzdh.Edit = 0;
                                    this.com_xfzh.Edit = 0;
                                }
                                else
                                {
                                    this.com_gfmc.Edit = 0;
                                    this.com_gfsbh.Edit = 0;
                                    this.com_gfdzdh.Edit = 0;
                                    this.com_gfzh.Edit = 0;
                                }
                                if (base._fpxx.Zyfplx == (ZYFP_LX)10)
                                {
                                    this.prevCmbSlv = base._GetSLv(base._fpxx.Fplx, "0.015", 0);
                                }
                                base._ChaeButton.Enabled = false;
                            }
                            else
                            {
                                base._DataGridView.Columns["SLV"].Tag = null;
                                this.blueJe = string.Empty;
                            }
                            base._fpxx.Bz = NotesUtil.GetRedInvNotes(hzfptk_pp.blueFpdm, hzfptk_pp.blueFphm);
                            base._fpxx.BlueFpdm = hzfptk_pp.blueFpdm;
                            base._fpxx.BlueFphm = hzfptk_pp.blueFphm;
                            this.ShowInvMainInfo();
                            if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                            {
                                base._ChaeButton.Checked = true;
                            }
                        }
                        else
                        {
                            this.RefreshData(false);
                            base._DataGridView.Columns["SLV"].Tag = null;
                        }
                    }
                    else
                    {
                        this.isHzwm = false;
                        this.RefreshData(false);
                        base._DataGridView.Columns["SLV"].Tag = null;
                    }
                    if (base._DataGridView.RowCount > 0)
                    {
                        base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, 0];
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void tool_fuzhi_Click(object sender, EventArgs e)
        {
            this.isHzwm = false;
            this.IsHYXXB = false;
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            base._spmcBt.OnTextChanged = (EventHandler)Delegate.Remove(base._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
            try
            {
                object[] objArray1 = new object[] { Invoice.FPLX2Str(base._fpxx.Fplx), base._fpxx.Zyfplx };
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoChaXun_FPFZ", objArray1);
                this.isFPCopy = true;
                if ((objArray != null) && (objArray.Length == 3))
                {
                    if (objArray[0].Equals(""))
                    {
                        return;
                    }
                    Fpxx fp = this.fpm.GetXxfp(base._fpxx.Fplx, objArray[1] as string, int.Parse(objArray[2] as string));
                    if (fp != null)
                    {
                        if (base._ChaeButton.Checked && (fp.yysbz[8] != '2'))
                        {
                            string[] textArray1 = new string[] { "\r\n原因：差额税状态下不能复制非差额发票！" };
                            MessageManager.ShowMsgBox("INP-242178", textArray1);
                            return;
                        }
                        base._fpxx.DelSpxxAll();
                        base._fpxx.CopyFpxx(fp);
                        if ((base._fpxx.Zyfplx == (ZYFP_LX)1) || (((base._fpxx.SLv == "0.05") && (base._fpxx.Fplx == 0)) && (fp.yysbz[8] == '0')))
                        {
                            base._fpxx.SetZyfpLx((ZYFP_LX)1);
                        }
                        if (this.isWM())
                        {
                            this.ShowCopy(fp);
                        }
                        else if ((fp.bmbbbh == null) || (fp.bmbbbh == ""))
                        {
                            List<Dictionary<SPXX, string>> spxxs = base._fpxx.GetSpxxs();
                            for (int i = 0; i < spxxs.Count; i++)
                            {
                                if (spxxs[i][(SPXX)10] == "4")
                                {
                                    string[] textArray2 = new string[] { "\r\n原因：被复制发票为旧版本含有折扣行发票！" };
                                    MessageManager.ShowMsgBox("INP-242178", textArray2);
                                    this.isFPCopy = false;
                                    this.RefreshData(false);
                                    base._fpxx.SetQdbz(false);
                                    return;
                                }
                            }
                            if (new Add_SPFLBM(base._fpxx, base._fpxx.Zyfplx == (ZYFP_LX)1).ShowDialog() != DialogResult.OK)
                            {
                                this.isFPCopy = false;
                                this.RefreshData(false);
                                base._fpxx.SetQdbz(false);
                                return;
                            }
                            this.ShowCopy(fp);
                            this.isFPCopy = false;
                        }
                        else
                        {
                            base.reset_fpxx(base._fpxx.Zyfplx == (ZYFP_LX)1);
                            this.ShowCopy(fp);
                        }
                    }
                }
                this.isFPCopy = false;
            }
            catch (Exception exception)
            {
                this.log.Error(exception.Message, exception);
                this.isFPCopy = false;
            }
            finally
            {
                base._spmcBt.OnTextChanged = (EventHandler)Delegate.Combine(base._spmcBt.OnTextChanged, new EventHandler(this._spmcBt_TextChanged));
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void tool_GfxxSave_Click(object sender, EventArgs e)
        {
            this.SaveKH(false);
        }

        private void tool_imputSet_Click(object sender, EventArgs e)
        {
            new ImportSet(0).ShowDialog();
        }

        private void tool_manualImport_Click(object sender, EventArgs e)
        {
            this.IsHYXXB = false;
            this.isHzwm = false;
            base._spmcBt.Leave -= new EventHandler(this._spmcBt_leave);
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "单据(*.xml)|*.xml"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string errorTip = "";
                    List<Djfp> collection = new FPDJHelper().ParseDjFileManual(base._fpxx.Fplx, dialog.FileName, out errorTip, base._fpxx.GetSqSLv(), base._fpxx.Zyfplx);
                    if (!string.IsNullOrEmpty(errorTip))
                    {
                        string[] textArray1 = new string[] { errorTip };
                        MessageManager.ShowMsgBox("INP-241007", textArray1);
                    }
                    else if (collection.Count > 0)
                    {
                        ManualImport import = new ManualImport(0, (this.Zyfplx == (ZYFP_LX)9) ? 1 : 0);
                        import.DjfpList.AddRange(collection);
                        import.AllDjfpList.AddRange(collection);
                        if (import.ShowDialog() == DialogResult.OK)
                        {
                            this.RefreshData(false);
                            Djfp djfp = import.Djfp;
                            if ((djfp != null) && (djfp.Fpxx != null))
                            {
                                if (this.FillDjxx(djfp))
                                {
                                    base._DataGridView.Rows.Clear();
                                    this.ShowInvMainInfo();
                                    base._ShowDataGridMxxx(base._DataGridView);
                                    if (base._DataGridView.RowCount > 0)
                                    {
                                        base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, base._DataGridView.RowCount - 1];
                                    }
                                    if (base._spmcBt != null)
                                    {
                                        base._spmcBt.Visible = false;
                                    }
                                    if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                                    {
                                        base._ChaeButton.Checked = true;
                                        base._QingdanButton.Enabled = false;
                                    }
                                    if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                                    {
                                        this.SetHysyHsjxx(base._DataGridView, true);
                                    }
                                    else
                                    {
                                        this.SetHysyHsjxx(base._DataGridView, false);
                                    }
                                    if (base._fpxx.Qdbz)
                                    {
                                        base._AddRowButton.Enabled = false;
                                    }
                                }
                                else
                                {
                                    this.RefreshData(false);
                                    if (this.fpm.GetErrorTip() != "")
                                    {
                                        string[] textArray2 = new string[] { this.fpm.GetErrorTip() };
                                        MessageManager.ShowMsgBox("INP-242179", textArray2);
                                    }
                                    else
                                    {
                                        MessageManager.ShowMsgBox(this.fpm.Code(), this.fpm.CodeParams());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] textArray3 = new string[] { "XML文件中单据信息不正确！" };
                        MessageManager.ShowMsgBox("INP-241007", textArray3);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                base._spmcBt.Leave += new EventHandler(this._spmcBt_leave);
            }
        }

        private void tool_print_Click(object sender, EventArgs e)
        {
            if (base._fpxx.GetSpxxs().Count == 0)
            {
                MessageManager.ShowMsgBox("INP-242120");
            }
            else
            {
                if (!base._onlyShow)
                {
                    if (this.isSnyZyfp && (this.com_yplx.SelectedIndex == -1))
                    {
                        MessageManager.ShowMsgBox("INP-242144");
                        return;
                    }
                    if (base._fpxx.IsRed)
                    {
                        if ((base._fpxx.Fplx == (FPLX)2) && !this.CheckPTHZFP())
                        {
                            return;
                        }
                        if (((base._fpxx.Fplx == 0) && !string.IsNullOrEmpty(this.blueJe)) && (Math.Abs(decimal.Parse(base._fpxx.GetHjJeNotHs())).CompareTo(decimal.Parse(this.blueJe)) > 0))
                        {
                            string[] textArray1 = new string[] { decimal.Negate(decimal.Parse(this.blueJe)).ToString("F2"), base._fpxx.GetHjJeNotHs() };
                            MessageManager.ShowMsgBox("INP-242118", textArray1);
                            return;
                        }
                    }
                }
                if (!FLBM_lock.isFlbm() || this.isHzwm)
                {
                    base._fpxx.Bmbbbh = "";
                    int count = base._fpxx.GetSpxxs().Count;
                    for (int i = 0; i < count; i++)
                    {
                        base._fpxx.SetLslvbs(i, "");
                        base._fpxx.SetYhsm(i, "");
                        base._fpxx.SetXsyh(i, "0");
                        base._fpxx.SetFlbm(i, "");
                        base._fpxx.SetSpbh(i, "");
                    }
                }
                if (!this.isWM())
                {
                    List<Dictionary<SPXX, string>> spxxs = base._fpxx.GetSpxxs();
                    for (int j = 0; j < spxxs.Count; j++)
                    {
                        if ((((spxxs[j][(SPXX)10] == "0") || (spxxs[j][(SPXX)10] == "3")) || (spxxs[j][(SPXX)10] == "4")) && ((!(spxxs[j][0].ToString() == "详见对应正数发票及清单") && !spxxs[j][0].Contains("详见")) && (spxxs[j][(SPXX)20] == "")))
                        {
                            string[] textArray2 = new string[] { (j + 1).ToString() };
                            MessageManager.ShowMsgBox("INP-242186", textArray2);
                            return;
                        }
                    }
                }
                if (!this.isWM())
                {
                    List<Dictionary<SPXX, string>> list2 = base._fpxx.GetSpxxs();
                    for (int k = 0; k < list2.Count; k++)
                    {
                        if (this.isNcpsgfp)
                        {
                            base._fpxx.SetLslvbs(k, "3");
                            if (((list2[k][(SPXX)0x16] == "免税") || (list2[k][(SPXX)0x16] == "不征税")) || (list2[k][(SPXX)0x16] == "出口零税"))
                            {
                                base._fpxx.SetYhsm(k, "");
                                base._fpxx.SetXsyh(k, "0");
                            }
                        }
                        if (list2[k][(SPXX)0x15] == "0")
                        {
                            base._fpxx.SetYhsm(k, "");
                        }
                        if (list2[k][(SPXX)0x16] == "")
                        {
                            base._fpxx.SetXsyh(k, "0");
                        }
                    }
                }
                Fpxx fpData = base._fpxx.GetFpData();
                if (fpData == null)
                {
                    MessageManager.ShowMsgBox(base._fpxx.GetCode(), base._fpxx.Params);
                    if (base._fpxx.GetCode() == "A122")
                    {
                        int num7 = int.Parse(base._fpxx.Params[0]);
                        if (num7 > 0)
                        {
                            this._ShowDataGrid(base._DataGridView, base._fpxx.GetSpxx(num7 - 1), num7 - 1);
                        }
                    }
                }
                else
                {
                    if (this.isNcpsgfp)
                    {
                        if ((((base._fpxx.Xfsh.Length != 0) && (base._fpxx.Xfsh.Length != 15)) && ((base._fpxx.Xfsh.Length != 0x11) && (base._fpxx.Xfsh.Length != 0x12))) && (base._fpxx.Xfsh.Length != 20))
                        {
                            string[] textArray3 = new string[] { "销售方纳税人识别号" };
                            if (MessageManager.ShowMsgBox("INP-242203", textArray3) != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                    else if ((((int)base._fpxx.Fplx != 2) || (base._fpxx.Gfsh.Length != 0)) && (((base._fpxx.Gfsh.Length != 15) && (base._fpxx.Gfsh.Length != 0x11)) && ((base._fpxx.Gfsh.Length != 0x12) && (base._fpxx.Gfsh.Length != 20))))
                    {
                        string[] textArray4 = new string[] { "购买方纳税人识别号" };
                        if (MessageManager.ShowMsgBox("INP-242203", textArray4) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    if ((!base._onlyShow && base._fpxx.IsRed) && (((int)fpData.fplx == 0) && !this.fpm.CheckRedNum(fpData.redNum, fpData.fplx)))
                    {
                        string[] textArray5 = new string[] { fpData.redNum };
                        MessageManager.ShowMsgBox(this.fpm.Code(), textArray5);
                    }
                    else
                    {
                        string str = "Aisino.Fwkp.Invoice" + base._fpxx.Fpdm + base._fpxx.Fphm;
                        byte[] destinationArray = new byte[0x20];
                        byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                        Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                        byte[] buffer2 = new byte[0x10];
                        Array.Copy(bytes, 0x20, buffer2, 0, 0x10);
                        byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(DateTime.Now.ToString("F")), destinationArray, buffer2);
                        fpData.gfmc = Convert.ToBase64String(AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(Convert.ToBase64String(inArray) + ";" + base._fpxx.Gfmc), destinationArray, buffer2));
                        if (base._onlyShow || base._fpxx.MakeCardInvoice(fpData, false))
                        {
                            this.log.Error("[发票填开错误号]" + fpData.retCode);
                            if (base._onlyShow || this.fpm.SaveXxfp(fpData))
                            {
                                this.PrintFp(fpData);
                                PropertyUtil.SetValue("INV-FHR-IDX", this.com_fhr.Text);
                                PropertyUtil.SetValue("INV-SKR-IDX", this.com_skr.Text);
                                if (!base._onlyShow && (this.djfile != ""))
                                {
                                    new FPDJHelper().InsertYkdj(this.djfile, fpData.xsdjbh);
                                }
                                if (!base._onlyShow && this.fpm.IsSWDK())
                                {
                                    this.UploadSwdkfp(fpData);
                                }
                                if (!base._onlyShow && this.tool_autokh.Checked)
                                {
                                    this.SaveKH(true);
                                }
                            }
                            else
                            {
                                MessageManager.ShowMsgBox("INP-242111");
                            }
                            if (!base._onlyShow)
                            {
                                if (!this.fpm.CanInvoice(base._fpxx.Fplx))
                                {
                                    MessageManager.ShowMsgBox(this.fpm.Code());
                                    base.FormClosing -= new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                                    base.Close();
                                }
                                else
                                {
                                    FPLX fplx = base._fpxx.Fplx;
                                    string[] current = this.fpm.GetCurrent(fplx);
                                    if ((current != null) && (current.Length == 2))
                                    {
                                        if (new StartConfirmForm(base._fpxx.Fplx, current).ShowDialog() == DialogResult.OK)
                                        {
                                            this.InitNextFP(current);
                                        }
                                        else
                                        {
                                            base.FormClosing -= new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                                            base.Close();
                                        }
                                    }
                                    else
                                    {
                                        string text1 = this.fpm.Code();
                                        MessageManager.ShowMsgBox(text1);
                                        if (text1 != "000000")
                                        {
                                            base.FormClosing -= new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                                            base.Close();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageManager.ShowMsgBox(base._fpxx.GetCode());
                            if (base._fpxx.GetCode().StartsWith("TCD_768") || base._fpxx.GetCode().StartsWith("TCD_769"))
                            {
                                FormMain.CallUpload();
                                base.FormClosing -= new FormClosingEventHandler(this._InvoiceForm_FormClosing);
                                base.Close();
                            }
                        }
                    }
                }
            }
        }

        private void tool_print_MouseDown(object sender, MouseEventArgs e)
        {
            base._CommitEditGrid();
            this.lab_title.Focus();
        }

        private void tool_zjkj_Click(object sender, EventArgs e)
        {
            this.tool_fushu.Checked = true;
            HZFPTK hzfptk = new HZFPTK(0)
            {
                mIsSny = this.isSnyZyfp
            };
            this.isHzwm = false;
            if (hzfptk.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData(true);
                Fpxx blueFpxx = hzfptk.blueFpxx;
                if (blueFpxx != null)
                {
                    this.blueJe = blueFpxx.je;
                    byte[] destinationArray = new byte[0x20];
                    byte[] sourceArray = Invoice.TypeByte;
                    Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                    byte[] buffer2 = new byte[0x10];
                    Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
                    byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
                    Invoice.IsGfSqdFp_Static = false;
                    Invoice invoice = new Invoice(base._fpxx.Hsjbz, blueFpxx, buffer3, null);
                    string str = base._fpxx.Fpdm;
                    string str2 = base._fpxx.Fphm;
                    string str3 = base._fpxx.Kprq;
                    Invoice redInvoice = invoice.GetRedInvoice(base._fpxx.Hsjbz);
                    if ((redInvoice == null) || (invoice.GetCode() != "0000"))
                    {
                        MessageManager.ShowMsgBox(invoice.GetCode(), invoice.Params);
                    }
                    base._fpxx = redInvoice;
                    if ((blueFpxx.bmbbbh == null) || (blueFpxx.bmbbbh == ""))
                    {
                        base._fpxx.Bmbbbh = "";
                        this.isHzwm = true;
                    }
                    else
                    {
                        base._fpxx.Bmbbbh = blueFpxx.bmbbbh;
                    }
                    base._fpxx.Fpdm = str;
                    base._fpxx.Fphm = str2;
                    base._fpxx.Kprq = str3;
                    base._fpxx.Kpr = UserInfo.Yhmc;
                    base._fpxx.Fhr = blueFpxx.fhr;
                    base._fpxx.Skr = blueFpxx.skr;
                    base._fpxx.Gfmc = invoice.Gfmc;
                    this.ResetQyxx();
                    this.ClearDkXfmc();
                    if (base._fpxx.GetSpxxs().Count == 0)
                    {
                        base._DataGridView.Rows.Clear();
                    }
                    base.reset_fpxx(base._fpxx.Zyfplx == (ZYFP_LX)1);
                    base._ShowDataGridMxxx(base._DataGridView);
                    base._spmcBt.Visible = false;
                    if (base._DataGridView.Rows.Count > 0)
                    {
                        base._DataGridView.Rows[0].Cells[base._DataGridView.ColumnCount - 1].Selected = true;
                    }
                    this.com_gfmc.Edit = 0;
                    this.com_gfsbh.Edit = 0;
                    this.com_gfdzdh.Edit = 0;
                    this.com_gfzh.Edit = 0;
                    if (base._fpxx.Zyfplx == (ZYFP_LX)1)
                    {
                        this.SetHysyHsjxx(base._DataGridView, true);
                        string str4 = "中外合作油气田";
                        this.prevCmbSlv = base._GetSLv(0, str4, 0);
                        base._DataGridView.Columns["SLV"].Tag = "1";
                    }
                    else
                    {
                        this.SetHysyHsjxx(base._DataGridView, false);
                    }
                    if (base._fpxx.Zyfplx == (ZYFP_LX)10)
                    {
                        string str5 = "0.015";
                        this.prevCmbSlv = base._GetSLv(0, str5, 0);
                    }
                    if (base._fpxx.Zyfplx == (ZYFP_LX)11)
                    {
                        base._ChaeButton.Checked = true;
                    }
                    base._ChaeButton.Enabled = false;
                    if (((blueFpxx.Zyfplx == (ZYFP_LX)2) || (blueFpxx.Zyfplx == (ZYFP_LX)3)) || ((blueFpxx.Zyfplx == (ZYFP_LX)4) || (blueFpxx.Zyfplx == (ZYFP_LX)5)))
                    {
                        string zyspmc = blueFpxx.zyspmc;
                        if (zyspmc.EndsWith("(石脑油)"))
                        {
                            this.com_yplx.SelectedIndex = 0;
                        }
                        else if (zyspmc.EndsWith("(石脑油DDZG)"))
                        {
                            this.com_yplx.SelectedIndex = 1;
                        }
                        else if (zyspmc.EndsWith("(燃料油)"))
                        {
                            this.com_yplx.SelectedIndex = 2;
                        }
                        else if (zyspmc.EndsWith("(燃料油DDZG)"))
                        {
                            this.com_yplx.SelectedIndex = 3;
                        }
                    }
                }
                else
                {
                    this.blueJe = string.Empty;
                    base._DataGridView.Columns["SLV"].Tag = null;
                }
                base._fpxx.Bz = NotesUtil.GetRedInvNotes(hzfptk.redNum);
                base._fpxx.RedNum = hzfptk.redNum;
                base._fpxx.BlueFpdm = hzfptk.blueFpdm;
                base._fpxx.BlueFphm = hzfptk.blueFphm;
                this.ShowInvMainInfo();
            }
            else
            {
                this.RefreshData(false);
                base._DataGridView.Columns["SLV"].Tag = null;
            }
            if (base._DataGridView.RowCount > 0)
            {
                base._DataGridView.CurrentCell = base._DataGridView[base._DataGridView.ColumnCount - 1, 0];
            }
        }

        private void txt_bz_TextChanged(object sender, EventArgs e)
        {
            string str = this.txt_bz.Text.Trim();
            base._fpxx.Bz = str;
            if (base._fpxx.Bz != str)
            {
                this.txt_bz.Text = base._fpxx.Bz;
                this.txt_bz.SelectionStart = this.txt_bz.Text.Length;
            }
        }

        private void UploadSwdkfp(Fpxx fp)
        {
            string str = PropertyUtil.GetValue("SWDK_SERVER");
            if (str.Trim() != "")
            {
                try
                {
                    WebClient client = new WebClient();
                    byte[] bytes = ToolUtil.GetBytes(new XmlProcessor().CreateDKFPXml(fp).OuterXml);
                    if ((bytes != null) && (bytes.Length != 0))
                    {
                        int num;
                        int num2;
                        string str2 = Convert.ToBase64String(bytes);
                        string s = client.Post(str, str2, out num);
                        if (num != 0)
                        {
                            MessageManager.ShowMsgBox(s);
                        }
                        if ((s != null) && (s.Length > 0))
                        {
                            s = ToolUtil.GetString(Convert.FromBase64String(s));
                        }
                        if (((num == 0) && int.TryParse(s, out num2)) && (num2 >= 1))
                        {
                            string[] textArray1 = new string[] { "成功" };
                            MessageManager.ShowMsgBox("INP-242183", textArray1);
                        }
                        else
                        {
                            string[] textArray2 = new string[] { "失败" };
                            MessageManager.ShowMsgBox("INP-242183", textArray2);
                        }
                    }
                }
                catch (Exception exception)
                {
                    string[] textArray3 = new string[] { "异常" };
                    MessageManager.ShowMsgBox("INP-242183", textArray3);
                    this.log.Error(exception);
                }
            }
        }

        private void XfxxSetValue(object[] xfxx)
        {
            this.DelTextChangedEvent();
            if ((xfxx != null) && (xfxx.Length > 2))
            {
                string mc = xfxx[0].ToString();
                string sh = xfxx[1].ToString();
                string str3 = this.ConvertXfdzdh(xfxx[2].ToString());
                this.com_xfdzdh.Text = str3;
                this.txt_bz.Text = this.SetDkBz(mc, sh);
                base._fpxx.Xfdzdh = this.com_xfdzdh.Text;
                base._fpxx.Dk_qymc = mc;
                base._fpxx.Dk_qysh = sh;
                base._fpxx.Bz = this.txt_bz.Text;
            }
            this.RegTextChangedEvent();
        }

        private void YD_checkBox_StateChange(object sender, EventArgs e)
        {
            this.isYD = this.YD_checkBox.CheckState == CheckState.Checked;
        }

        public bool InitSuccess
        {
            get
            {
                return this.initSuccess;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class serializeClass
        {
            public static readonly InvoiceForm_ZZS.serializeClass instance = new InvoiceForm_ZZS.serializeClass();
            public static Func<double, double> staticFunc_1 = new Func<double, double>(slvlistFunc_1);
            public static Func<IGrouping<double, double>, double> staticFunc_2 = new Func<IGrouping<double, double>, double>(slvlistFunc_2);
            public static Func<double, double> staticFunc_3 = new Func<double, double>(slvlistFunc_3);
            public static Func<IGrouping<double, double>, double> staticFunc_4 = new Func<IGrouping<double, double>, double>(slvlistFunc_4);

            internal static double slvlistFunc_3(double p)
            {
                return p;
            }

            internal static double slvlistFunc_4(IGrouping<double, double> p)
            {
                return p.Key;
            }

            internal static double slvlistFunc_1(double p)
            {
                return p;
            }

            internal static double slvlistFunc_2(IGrouping<double, double> p)
            {
                return p.Key;
            }
        }
    }
}

