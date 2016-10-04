namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Fptk.Properties;
    using Aisino.Fwkp.Print;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class InvoiceShowForm : DockForm
    {
        private CustomStyleDataGrid _DataGridView;
        protected Invoice _fpxx;
        private ToolStripButton addRowButton;
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
        private IContainer components;
        internal List<string[]> data;
        private CustomStyleDataGrid dataGridView_qd;
        private ToolStripButton delRowButton;
        private IFpManager fpm;
        private Fpxx fpxx;
        private ToolStripButton hsjbzButton;
        private ToolStripButton hsjbzButton_qd;
        internal int index;
        private bool isNcpsgfp;
        private bool isSnyZyfp;
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
        private ILog log = LogUtil.GetLogger<InvoiceShowForm>();
        private AisinoPNL mainPanel;
        private FPLX mFplx;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC picZuofei;
        private QingDanTianKai qd;
        private ToolStripButton qingdanButton;
        private const string RLY = "(燃料油)";
        private const string RLY_DDZG = "(燃料油DDZG)";
        private List<SLV> sLvList;
        private const string SNY = "(石脑油)";
        private const string SNY_DDZG = "(石脑油DDZG)";
        private ToolStripButton statisticButton;
        private ToolTip tip = new ToolTip();
        private ToolStripMenuItem tool_autoImport;
        private ToolStripMenuItem tool_autokh;
        private ToolStripButton tool_chae;
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
        private XmlComponentLoader xmlComponentLoader1;
        private const string XT = "稀土";
        private CheckBox YD_checkBox;
        private ToolStripButton zhekouButton;

        internal InvoiceShowForm(bool flag, int index, List<string[]> data)
        {
            this.data = data;
            this.index = index;
            string[] strArray = data[index];
            this.Initialize();
            base.KeyPreview = true;
            base.KeyDown += new KeyEventHandler(this.InvoiceShowForm_KeyDown);
            base.Resize += new EventHandler(this.InvoiceShowForm_Resize);
            this.tool_DaoRuHZTZD.Visible = false;
            this.tool_fushu.Visible = false;
            this.tool_fuzhi.Visible = false;
            this.tool_kehu.Visible = false;
            this.tool_import.Visible = false;
            this.tool_dkdr.Visible = false;
            this.tool_dkdjdr.Visible = false;
            this.tool_chae.Visible = false;
            this.com_fhr.Edit=0;
            this.com_skr.Edit=0;
            this.YD_checkBox.Visible = false;
            this.txt_bz.ReadOnly = true;
            this.tool_print.Enabled = true;
            this.tool_zuofei.Visible = flag;
            this.tool_fanlan.Visible = false;
            this.SetDataGridPropEven(this._DataGridView);
            this.SetTitleFont();
            this.fpm = new FpManager();
            this.fpxx = this.fpm.GetXxfp(Invoice.ParseFPLX(strArray[0]), strArray[1], int.Parse(strArray[2]));
            this.ShowInfo(this.fpxx, strArray[3]);
        }

        private SLV _GetSLv(FPLX fplx, string value, int valueType)
        {
            double num;
            if ((value == "免税") || (value == "不征税"))
            {
                value = "0%";
            }
            if (value == "中外合作油气田")
            {
                return new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", "");
            }
            if (((value == "0.05") || (value == "0.050")) && (((int)fplx == 0) && (this._fpxx.Zyfplx == (ZYFP_LX)1)))
            {
                return new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", "");
            }
            if (value == "0.015")
            {
                return new SLV(fplx, (ZYFP_LX)10, "0.015", "1.5%", "");
            }
            if (valueType != 0)
            {
                return null;
            }
            if (double.TryParse(value, out num))
            {
                string str = (value.Length == 0) ? "" : (((num * 100.0)).ToString() + "%");
                return new SLV(fplx, 0, (value.Length == 0) ? "" : num.ToString(), str, str);
            }
            return new SLV(fplx, 0, value, value, value);
        }

        private List<SLV> _GetSLvList()
        {
            List<SLV> list = new List<SLV>();
            if (this._fpxx != null)
            {
                bool flag = false;
                bool flag2 = false;
                char[] separator = new char[] { ';' };
                string[] strArray = this._fpxx.GetSqSLv().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                List<string> list2 = new List<string>();
                List<double> source = new List<double>();
                if (strArray.Length >= 1)
                {
                    char[] chArray2 = new char[] { ',' };
                    string[] strArray2 = strArray[0].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                    list2.AddRange(strArray2.ToList<string>());
                    flag2 = strArray[0].Contains("0.05") || strArray[0].Contains("0.050");
                }
                if (strArray.Length >= 2)
                {
                    char[] chArray3 = new char[] { ',' };
                    string[] strArray3 = strArray[1].Split(chArray3, StringSplitOptions.RemoveEmptyEntries);
                    list2.AddRange(strArray3.ToList<string>());
                    flag = strArray[1].Contains("0.05") || strArray[1].Contains("0.050");
                }
                using (List<string>.Enumerator enumerator = list2.GetEnumerator())
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
                source = source.GroupBy<double, double>((serializeClass.staticFunc_5 )).Select<IGrouping<double, double>, double>((serializeClass.staticFunc_6)).ToList<double>();
                source.Sort();
                source.Reverse();
                if (this._fpxx.Fplx == 0)
                {
                    foreach (double num2 in source)
                    {
                        decimal d = Math.Round(decimal.Parse(num2.ToString()),3);
                        if (d == 0.0M)
                            list.Add(new SLV(0, 0, "0.00", "0%", "0%"));
                        else if (d == 0.015M)
                            list.Add(new SLV(0, 0, "0.015", "1.5%", ""));
                        else if (d == 0.05M)
                        {
                            if (!this.isSnyZyfp & flag)
                            {
                                list.Add(new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", ""));
                            }
                            if (flag2)
                            {
                                list.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                            }
                        }
                        else
                        {
                            decimal num3 = d * 100.0M;
                            string str = num3.ToString() + "%";
                            list.Add(new SLV(0, 0, num2.ToString(), str, str));
                        }
                        //switch (num2)
                        //{
                        //    case 0.0:
                        //        list.Add(new SLV(0, 0, "0.00", "0%", "0%"));
                        //        break;

                        //    case 0.015:
                        //        list.Add(new SLV(0, 0, "0.015", "1.5%", ""));
                        //        break;

                        //    case 0.05:
                        //        if (!this.isSnyZyfp & flag)
                        //        {
                        //            list.Add(new SLV(0, (ZYFP_LX)1, "0.05", "中外合作油气田", ""));
                        //        }
                        //        if (flag2)
                        //        {
                        //            list.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                        //        }
                        //        break;

                        //    default:
                        //    {
                        //        double num3 = num2 * 100.0;
                        //        string str = num3.ToString() + "%";
                        //        list.Add(new SLV(0, 0, num2.ToString(), str, str));
                        //        break;
                        //    }
                        //}
                    }
                    return list;
                }
                if ((int)this._fpxx.Fplx != 2)
                {
                    return list;
                }
                if (this.isNcpsgfp)
                {
                    list.Add(new SLV((FPLX)2, (ZYFP_LX)9, "0.00", "0%", "0%"));
                    return list;
                }
                foreach (double num4 in source)
                {
                    if ((num4 == 0.05) & flag2)
                    {
                        list.Add(new SLV(0, 0, "0.05", "5%", "5%"));
                    }
                    else if (num4 == 0.0)
                    {
                        list.Add(new SLV((FPLX)2, 0, "0.00", "0%", "0%"));
                    }
                    else if (num4 == 0.015)
                    {
                        list.Add(new SLV((FPLX)0, 0, "0.015", "1.5%", ""));
                    }
                    else
                    {
                        string str2 = ((num4 * 100.0)).ToString() + "%";
                        list.Add(new SLV((FPLX)2, 0, num4.ToString(), str2, str2));
                    }
                }
            }
            return list;
        }

        private void _InitQingdanForm()
        {
            this.qd = new QingDanTianKai(true, this._fpxx.Fpdm, this._fpxx.Fphm, false);
            this.qd.Fplx = this._fpxx.Fplx;
            if (this._fpxx.Zyfplx == (ZYFP_LX)1)
            {
                this.SetHysyHsjxx(this.qd.dataGridView1, true);
            }
            else
            {
                this.SetHysyHsjxx(this.qd.dataGridView1, false);
            }
            this.SetQingdanFormProp(this.qd, this.qd.dataGridView1, this.qd.tool_jg, this.qd.tool_zhekou, this.qd.tool_add, this.qd.tool_remove);
        }

        private void _SetHsjxx(CustomStyleDataGrid dataGridView1, bool hsjbz)
        {
            string str = hsjbz ? "(含税)" : "(不含税)";
            if (dataGridView1.Columns["DJ"] != null)
            {
                char[] separator = new char[] { '(' };
                dataGridView1.Columns["DJ"].HeaderText = dataGridView1.Columns["DJ"].HeaderText.Split(separator)[0] + str;
            }
            if (dataGridView1.Columns["JE"] != null)
            {
                char[] chArray2 = new char[] { '(' };
                dataGridView1.Columns["JE"].HeaderText = dataGridView1.Columns["JE"].HeaderText.Split(chArray2)[0] + str;
            }
            this._fpxx.Hsjbz=hsjbz;
            if (this._fpxx.Qdbz)
            {
                if (this.dataGridView_qd != null)
                {
                    this._ShowDataGrid(dataGridView1);
                    if (this._DataGridView.Columns["DJ"] != null)
                    {
                        char[] chArray3 = new char[] { '(' };
                        this._DataGridView.Columns["DJ"].HeaderText = this._DataGridView.Columns["DJ"].HeaderText.Split(chArray3)[0] + str;
                    }
                    if (this._DataGridView.Columns["JE"] != null)
                    {
                        char[] chArray4 = new char[] { '(' };
                        this._DataGridView.Columns["JE"].HeaderText = this._DataGridView.Columns["JE"].HeaderText.Split(chArray4)[0] + str;
                    }
                }
                this._ShowDataGridMxxx(this._DataGridView);
            }
            else
            {
                this._ShowDataGrid(dataGridView1);
            }
            this.hsjbzButton.Checked = hsjbz;
            if (hsjbz)
            {
                this.hsjbzButton.Image = Resources.hanshuijiage_03;
                if (this.dataGridView_qd != null)
                {
                    this.qd.tool_jg.Image = Resources.hanshuijiage_03;
                }
            }
            else
            {
                this.hsjbzButton.Image = Resources.jiage_03;
                if (this.dataGridView_qd != null)
                {
                    this.qd.tool_jg.Image = Resources.jiage_03;
                }
            }
        }

        private void _SetHzxx()
        {
            this.lab_hj_je.Text = "￥" + this._fpxx.GetHjJe();
            this.lab_hj_se.Text = "￥" + this._fpxx.GetHjSe();
            this.lab_hj_jshj.Text = "￥" + this._fpxx.GetHjJeHs();
            this.lab_hj_jshj_dx.Text = ToolUtil.RMBToDaXie(decimal.Parse(this._fpxx.GetHjJeHs()));
        }

        private void _ShowDataGrid(CustomStyleDataGrid dataGridView1)
        {
            int count = this._fpxx.GetSpxxs().Count;
            for (int i = 0; i < count; i++)
            {
                this._ShowDataGrid(dataGridView1, this._fpxx.GetSpxx(i), i);
            }
        }

        private void _ShowDataGrid(CustomStyleDataGrid parent, Dictionary<SPXX, string> spxx, int index)
        {
            while ((parent.Rows.Count - 1) < index)
            {
                parent.Rows.Add();
            }
            FPLX fplx = this._fpxx.Fplx;
            string str = this._GetSLv(fplx, spxx[(SPXX)8], 0).ShowValue;
            if (((str != "") && (str == "0%")) && FLBM_lock.isFlbm())
            {
                if ((spxx[(SPXX)0x17] == "") || (spxx[(SPXX)0x17] == "3"))
                {
                    str = "0%";
                }
                if (spxx[(SPXX)0x17] == "1")
                {
                    str = "免税";
                }
                if (spxx[(SPXX)0x17] == "2")
                {
                    str = "不征税";
                }
            }
            else if (((str != "") && (str == "0%")) && !FLBM_lock.isFlbm())
            {
                str = "免税";
            }
            string str2 = ((spxx[(SPXX)9].Length > 0) && (Math.Abs(double.Parse(spxx[(SPXX)9])) < 0.009)) ? "" : spxx[(SPXX)9];
            DataGridViewRow row = parent.Rows[index];
            for (int i = 0; i < row.Cells.Count; i++)
            {
                string name = parent.Columns[i].Name;
                try
                {
                    if (name.Equals("SLV"))
                    {
                        row.Cells["SLV"].Value = str;
                    }
                    else if (name.Equals("SE"))
                    {
                        row.Cells["SE"].Value = str2;
                    }
                    else
                    {
                        row.Cells[name].Value = spxx[(SPXX) Enum.Parse(typeof(SPXX), name)];
                    }
                }
                catch (ArgumentException exception)
                {
                    this.log.Error("设置数据表格内容异常", exception);
                }
            }
            this._SetHzxx();
        }

        private void _ShowDataGridMxxx(CustomStyleDataGrid dataGridView1)
        {
            List<Dictionary<SPXX, string>> mxxxs = this._fpxx.GetMxxxs();
            if (mxxxs != null)
            {
                int count = mxxxs.Count;
                for (int i = 0; i < count; i++)
                {
                    this._ShowDataGrid(dataGridView1, mxxxs[i], i);
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

        private void hsjbzButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_qd == null)
            {
                this._SetHsjxx(this._DataGridView, this.hsjbzButton.Checked);
            }
            else
            {
                this._SetHsjxx(this.dataGridView_qd, !this._fpxx.Hsjbz);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.tool_close = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_close");
            this.tool_zuofei = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zuofei");
            this.tool_zuofei.CheckOnClick = false;
            this.tool_kehu = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_kehu");
            this.tool_autokh = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_autokh");
            this.tool_manukh = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_manukh");
            this.tool_fanlan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fanlan");
            this.tool_zjkj = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_zjkj");
            this.tool_DaoRuHZTZD = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_DaoRuHZTZD");
            this.tool_drgp = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_drgp");
            this.tool_fushu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fushu");
            this.tool_fushu1 = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_fushu1");
            this.tool_fushu1.Visible = false;
            this.tool_import = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_import");
            this.tool_imputSet = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_imputSet");
            this.tool_manualImport = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_manualImport");
            this.tool_autoImport = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_autoImport");
            this.tool_print = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_print");
            this.tool_fuzhi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_fuzhi");
            this.tool_dkdr = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_dkdr");
            this.tool_dkdjdr = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_dkdjdr");
            this.tool_chae = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chae");
            this.lab_title = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_title");
            this.lab_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fpdm");
            this.lab_kprq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_kprq");
            this.lab_fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_fphm");
            this.lab_yplx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_yplx");
            this.lab_yplx.Visible = false;
            this.lblDq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lbl_Dq");
            this.com_yplx = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_yplx");
            this.com_gfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfsbh");
            this.com_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfmc");
            this.com_gfdzdh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfdzdh");
            this.com_gfzh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_gfzh");
            this.lab_hj_se = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_hj_se");
            this.lab_hj_je = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_hj_je");
            this.lab_hj_jshj = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_hj_jshj");
            this.lab_hj_jshj_dx = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_hj_jshj_dx");
            this.txt_bz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_bz");
            this.com_xfsbh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("lab_xfsbh");
            this.com_xfmc = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("lab_xfmc");
            this.com_xfzh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_xfzh");
            this.com_xfdzdh = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("lab_xfdzdh");
            this.lab_kp = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lab_kp");
            this.com_fhr = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_fhr");
            this.com_fhr.IsSelectAll=true;
            this.com_fhr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.com_fhr.Width));
            this.com_fhr.DrawHead=false;
            this.com_skr = this.xmlComponentLoader1.GetControlByName<AisinoMultiCombox>("com_skr");
            this.com_skr.IsSelectAll=true;
            this.com_skr.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YH", this.com_skr.Width));
            this.com_skr.DrawHead=false;
            this.qingdanButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_qingdan");
            this.addRowButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_addrow");
            this.delRowButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_delrow");
            this.zhekouButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_zhekou");
            this._DataGridView = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("DataGrid1");
            this.statisticButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.hsjbzButton = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("bt_jg");
            this.mainPanel = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel_main");
            this.YD_checkBox = this.xmlComponentLoader1.GetControlByName<CheckBox>("YD_checkBox");
            this.lblJYM = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJYM");
            this.lblJYM.Visible = false;
            this.lblNCP = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblNCP");
            this.lblNCP.Visible = false;
            this.statisticButton.Visible = false;
            this.hsjbzButton.CheckOnClick = true;
            this.com_gfdzdh.IsSelectAll=true;
            this.com_gfdzdh.Columns.Add(new AisinoMultiCombox.AisinoComboxColumn("", "YHZH", this.com_gfdzdh.Width));
            this.com_gfdzdh.DrawHead=false;
            this.com_yplx.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] items = new object[] { "(石脑油)", "(石脑油DDZG)", "(燃料油)", "(燃料油DDZG)" };
            this.com_yplx.Items.AddRange(items);
            this.com_yplx.Visible = false;
            this.txt_bz.AcceptsTab = false;
            this.txt_bz.AcceptsReturn = true;
            this.txt_bz.ScrollBars = ScrollBars.Vertical;
            this.com_gfmc.Edit=0;
            this.com_gfsbh.Edit=0;
            this.com_gfdzdh.Edit=0;
            this.com_gfzh.Edit=0;
            this.com_xfmc.Edit=0;
            this.com_xfsbh.Edit=0;
            this.com_xfdzdh.Edit=0;
            this.com_xfzh.Edit=0;
            this.tool_print.Click += new EventHandler(this.tool_print_Click);
            this.tool_zuofei.Click += new EventHandler(this.tool_zuofei_Click);
            this.hsjbzButton.Click += new EventHandler(this.hsjbzButton_Click);
            this.qingdanButton.Click += new EventHandler(this.qingdanButton_Click);
            this.tool_close.Click += new EventHandler(this.tool_close_Click);
            this.qingdanButton.ToolTipText = "销货清单";
            this.statisticButton.Click += new EventHandler(this.statistic_Click);
            this.tool_print.ToolTipText = "发票打印";
            this.toolStrip3 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip3");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip3);
            this._DataGridView.RowHeadersVisible = false;
            this._DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this._DataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this._DataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.picZuofei = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("picZuofei");
            this.picZuofei.BackColor = Color.Transparent;
            this.picZuofei.SizeMode = PictureBoxSizeMode.Zoom;
            this.picZuofei.Visible = false;
            this.panel1.BackgroundImage = Resources.ZY;
            this.panel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new Size(0x3c4, 650);
            this.tool_close.Margin = new Padding(20, 1, 0, 2);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(InvoiceShowForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x3c4, 0x2be);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPtiankai_new\Aisino.Fwkp.Fpkj.Form.FPtiankai_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3c4, 0x2be);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            this.MinimumSize = new Size(0x323, 0x209);
            base.Name = "InvoiceShowForm";
            base.TabText="增值税专用发票填开";
            this.Text = "增值税专用发票填开";
            base.ResumeLayout(false);
        }

        private void InvoiceShowForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            bool flag = false;
            switch (keyCode)
            {
                case Keys.Next:
                    if (this.index < (this.data.Count - 1))
                    {
                        this.index++;
                        this.tip.Hide(this.lab_title);
                    }
                    else
                    {
                        this.tip.Show("    已经到达本页最后一张    ", this.lab_title, new Point(0, 0x37), 0x7d0);
                        return;
                    }
                    flag = true;
                    this.tip.Show("    正在查询后一张发票...    ", this.lab_title, new Point(0, 0x37));
                    break;

                case Keys.PageUp:
                    if (this.index > 0)
                    {
                        this.index--;
                        this.tip.Hide(this.lab_title);
                    }
                    else
                    {
                        this.tip.Show("    已经到达本页第一张    ", this.lab_title, new Point(0, 0x37), 0x7d0);
                        return;
                    }
                    flag = true;
                    this.tip.Show("    正在查询前一张发票...    ", this.lab_title, new Point(0, 0x37));
                    break;
            }
            if (flag)
            {
                string[] strArray = this.data[this.index];
                FPLX fpzl = Invoice.ParseFPLX(strArray[0]);
                if ((fpzl == null) || ((int)fpzl == 2))
                {
                    this.fpxx = this.fpm.GetXxfp(fpzl, strArray[1], int.Parse(strArray[2]));
                    this._DataGridView.Rows.Clear();
                    this.ShowInfo(this.fpxx, strArray[3]);
                    this.tip.Hide(this.lab_title);
                }
                else
                {
                    base.DialogResult = DialogResult.Ignore;
                    base.Close();
                }
            }
        }

        private void InvoiceShowForm_Resize(object sender, EventArgs e)
        {
            this._DataGridView.Invalidate();
            if (this.panel1 != null)
            {
                this.panel1.Location = new Point((base.Width - this.panel1.Width) / 2, this.panel1.Location.Y);
            }
        }

        private void qingdanButton_Click(object sender, EventArgs e)
        {
            if (this._fpxx.Qdbz)
            {
                this._InitQingdanForm();
                this._ShowDataGridMxxx(this._DataGridView);
                this._DataGridView.ReadOnly = true;
                this.addRowButton.Enabled = false;
                this.zhekouButton.Enabled = false;
                this.delRowButton.Enabled = false;
                this._ShowDataGrid(this.dataGridView_qd);
                if (this.qd != null)
                {
                    this.qd.ShowDialog();
                    this.qd = null;
                }
                this.dataGridView_qd = null;
            }
        }

        private void SetBackgroudImage()
        {
            if (this.mFplx == 0)
            {
                this.panel1.BackgroundImage = Resources.ZY;
            }
            else
            {
                this.panel1.BackgroundImage = Resources.PT;
            }
        }

        private void SetCxFormTitle(string fpdm, FPLX fplx, ZYFP_LX subFplx)
        {
            string str = this.fpm.QueryXzqy(fpdm);
            if ((int)fplx == 0)
            {
                if (this.isSnyZyfp)
                {
                    this.Text = "石脑油、燃料油增值税专用发票查询";
                }
                else
                {
                    this.Text = "增值税专用发票查询";
                }
            }
            else if ((int)fplx == 2)
            {
                if ((int)subFplx == 9)
                {
                    this.Text = "收购发票查询";
                }
                else if ((int)subFplx == 8)
                {
                    this.Text = "农产品销售发票查询";
                }
                else
                {
                    this.Text = "增值税普通发票查询";
                }
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

        private void SetDataGridPropEven(CustomStyleDataGrid obj)
        {
            obj.RowHeadersWidth = 0x19;
            for (int i = 0; i < obj.Columns.Count; i++)
            {
                obj.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            obj.AllowUserToAddRows = false;
            obj.ReadOnly = true;
            obj.GridStyle = CustomStyle.invWare;
            obj.BorderStyle = BorderStyle.FixedSingle;
            if ((int)this.mFplx == 2)
            {
                this._DataGridView.GridColor = Color.DodgerBlue;
            }
            else
            {
                this._DataGridView.GridColor = Color.Black;
            }
        }

        private void SetHysyHsjxx(CustomStyleDataGrid dataGridView1, bool start)
        {
            if (start)
            {
                dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                dataGridView1.Columns["JE"].HeaderText = "金额(不含税)";
                this.hsjbzButton.Click -= new EventHandler(this.hsjbzButton_Click);
                this.hsjbzButton.Checked = true;
                this.hsjbzButton.Enabled = false;
                this.hsjbzButton.Click += new EventHandler(this.hsjbzButton_Click);
                if (this._fpxx.Qdbz && (this.qd != null))
                {
                    this._DataGridView.Columns["DJ"].HeaderText = "单价(含税)";
                    this._DataGridView.Columns["JE"].HeaderText = "金额(不含税)";
                    this.qd.tool_jg.Click -= new EventHandler(this.hsjbzButton_Click);
                    this.qd.tool_jg.Checked = true;
                    this.qd.tool_jg.Enabled = false;
                    this.qd.tool_jg.Click += new EventHandler(this.hsjbzButton_Click);
                }
            }
            else
            {
                if (this._fpxx.Hsjbz)
                {
                    dataGridView1.Columns["DJ"].HeaderText = "单价(含税)";
                    dataGridView1.Columns["JE"].HeaderText = "金额(含税)";
                }
                else
                {
                    dataGridView1.Columns["DJ"].HeaderText = "单价(不含税)";
                    dataGridView1.Columns["JE"].HeaderText = "金额(不含税)";
                }
                this.hsjbzButton.Enabled = true;
                this.hsjbzButton.Checked = this._fpxx.Hsjbz;
                if (this._fpxx.Qdbz && (this.qd != null))
                {
                    if (this._fpxx.Hsjbz)
                    {
                        this._DataGridView.Columns["DJ"].HeaderText = "单价(含税)";
                        this._DataGridView.Columns["JE"].HeaderText = "金额(含税)";
                    }
                    else
                    {
                        this._DataGridView.Columns["DJ"].HeaderText = "单价(不含税)";
                        this._DataGridView.Columns["JE"].HeaderText = "金额(不含税)";
                    }
                    this.qd.tool_jg.Enabled = true;
                    this.qd.tool_jg.Checked = this._fpxx.Hsjbz;
                }
            }
        }

        private void SetQingdanFormProp(Form qingdanForm, CustomStyleDataGrid dataGrid, ToolStripButton hsjbzButton, ToolStripButton zhekouButton, ToolStripButton addRowButton, ToolStripButton delRowButton)
        {
            this.hsjbzButton_qd = hsjbzButton;
            this.hsjbzButton_qd.Checked = this._fpxx.Hsjbz;
            this.hsjbzButton_qd.Click += new EventHandler(this.hsjbzButton_Click);
            this.dataGridView_qd = dataGrid;
            this.SetDataGridPropEven(this.dataGridView_qd);
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

        private void ShowInfo(Fpxx fp, string zfbz)
        {
            this.lab_yplx.Visible = false;
            this.com_yplx.Visible = false;
            this.lblNCP.Visible = false;
            this.isSnyZyfp = false;
            this.mFplx = fp.fplx;
            if (((fp.fplx == 0) && !string.IsNullOrEmpty(fp.yysbz)) && (fp.yysbz.Substring(2, 1) == "3"))
            {
                fp.Zyfplx = (ZYFP_LX)2;
                this.isSnyZyfp = true;
            }
            if (fp.fplx == (FPLX)2)
            {
                if (!string.IsNullOrEmpty(fp.yysbz) && (fp.yysbz.Substring(5, 1) == "1"))
                {
                    fp.Zyfplx = (ZYFP_LX)8;
                }
                else if (!string.IsNullOrEmpty(fp.yysbz) && (fp.yysbz.Substring(5, 1) == "2"))
                {
                    fp.Zyfplx = (ZYFP_LX)9;
                    this.isNcpsgfp = true;
                }
            }
            this.SetBackgroudImage();
            this.SetCxFormTitle(fp.fpdm, fp.fplx, fp.Zyfplx);
            string str = PropertyUtil.GetValue("INV-HSJBZ", "0");
            byte[] destinationArray = new byte[0x20];
            byte[] sourceArray = Invoice.TypeByte;
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer2);
            Invoice.IsGfSqdFp_Static=false;
            this._fpxx = new Invoice(str.Equals("1"), fp, buffer3, null);
            this.sLvList = this._GetSLvList();
            this.lab_fpdm.Text = this._fpxx.Fpdm;
            this.lab_kprq.Text = this._fpxx.Kprq;
            this.lab_fphm.Text = this._fpxx.Fphm;
            this.com_gfsbh.Text = this._fpxx.Gfsh;
            this.com_gfmc.Text = this._fpxx.Gfmc;
            this.com_gfdzdh.Text = this._fpxx.Gfdzdh;
            this.com_gfzh.Text = this._fpxx.Gfyhzh;
            this.com_xfsbh.Text = this._fpxx.Xfsh;
            this.com_xfmc.Text = this._fpxx.Xfmc;
            this.com_xfdzdh.Text = this._fpxx.Xfdzdh;
            this.com_xfzh.Text = this._fpxx.Xfyhzh;
            this.com_skr.Text = this._fpxx.Skr;
            this.com_fhr.Text = this._fpxx.Fhr;
            this.lab_kp.Text = this._fpxx.Kpr;
            this.txt_bz.Text = this._fpxx.Bz;
            bool flag = zfbz.Equals("1");
            this.tool_zuofei.Enabled = !flag;
            this.picZuofei.Visible = flag;
            this._SetHzxx();
            if (!this._fpxx.Qdbz)
            {
                this.qingdanButton.Enabled = false;
            }
            else
            {
                this.qingdanButton.Enabled = true;
            }
            this._SetHsjxx(this._DataGridView, this._fpxx.Hsjbz);
            if (this._fpxx.Zyfplx == (ZYFP_LX)1)
            {
                this.SetHysyHsjxx(this._DataGridView, true);
            }
            else
            {
                this.SetHysyHsjxx(this._DataGridView, false);
            }
            if (this.isSnyZyfp)
            {
                this.lab_yplx.Visible = true;
                this.com_yplx.Visible = true;
                this.com_yplx.Enabled = false;
                string str2 = "";
                List<Dictionary<SPXX, string>> spxxs = this._fpxx.GetSpxxs();
                if (((spxxs != null) && (spxxs.Count > 0)) && (spxxs[0] != null))
                {
                    string str3 = spxxs[0][0];
                    if (str3.EndsWith("(石脑油)"))
                    {
                        str2 = "(石脑油)";
                    }
                    else if (str3.EndsWith("(石脑油DDZG)"))
                    {
                        str2 = "(石脑油DDZG)";
                    }
                    else if (str3.EndsWith("(燃料油)"))
                    {
                        str2 = "(燃料油)";
                    }
                    else if (str3.EndsWith("(燃料油DDZG)"))
                    {
                        str2 = "(燃料油DDZG)";
                    }
                }
                this.com_yplx.Text = str2;
            }
            if (fp.fplx == (FPLX)2)
            {
                this.lblJYM.Visible = true;
                this.lblJYM.Text = "校验码：" + fp.jym;
                if (fp.Zyfplx == (ZYFP_LX)9)
                {
                    this.lblNCP.Visible = true;
                    this.lblNCP.Text = "收购";
                }
                else if (fp.Zyfplx == (ZYFP_LX)8)
                {
                    this.lblNCP.Visible = true;
                    this.lblNCP.Text = "农产品销售";
                }
                else
                {
                    this.lblNCP.Visible = false;
                }
            }
            else
            {
                this.lblJYM.Visible = false;
            }
            this.zhekouButton.Visible = false;
            this.addRowButton.Visible = false;
            this.delRowButton.Visible = false;
        }

        private void statistic_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_qd == null)
            {
                this._DataGridView.Statistics(this);
            }
            else
            {
                this.dataGridView_qd.Statistics(this);
            }
        }

        private void tool_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void tool_print_Click(object sender, EventArgs e)
        {
            if (this._fpxx.GetSpxxs().Count == 0)
            {
                MessageManager.ShowMsgBox("INP-242120");
            }
            else
            {
                try
                {
                    FPPrint print1 = new FPPrint(Invoice.FPLX2Str(this._fpxx.Fplx), this.fpxx.fpdm, int.Parse(this.fpxx.fphm));
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
        }

        private void tool_zuofei_Click(object sender, EventArgs e)
        {
            object[] objArray1 = new object[] { Invoice.FPLX2Str(this.fpxx.fplx), this.fpxx.fpdm, this.fpxx.fphm, 0 };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPYiKaiZuoFeiWenBenJieKouShareMethods", objArray1);
            if (((objArray != null) && (objArray.Length != 0)) && (objArray[0] != null))
            {
                if (Convert.ToBoolean(objArray[0]))
                {
                    MessageManager.ShowMsgBox("INP-442312");
                    this.tool_zuofei.Enabled = false;
                    this.data[this.index][3] = "1";
                }
                else
                {
                    this.tool_zuofei.Enabled = true;
                    this.data[this.index][3] = "0";
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class serializeClass
        {
            public static readonly InvoiceShowForm.serializeClass instance = new InvoiceShowForm.serializeClass();
            public static Func<double, double> staticFunc_5;
            public static Func<IGrouping<double, double>, double> staticFunc_6;

            internal double slvlistFunc_5(double p)
            {
                return p;
            }

            internal double slvlistFunc_6(IGrouping<double, double> p)
            {
                return p.Key;
            }
        }
    }
}

