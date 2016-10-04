namespace Aisino.Fwkp.Fpkj.Form.FPXF
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class RepairReport : BaseForm
    {
        private string _AddColumn = "添加发票";
        private AisinoDataSet _AddDataSet = new AisinoDataSet();
        private DataTable _AddTable = new DataTable();
        private string _DeleteColumn = "删除发票";
        private AisinoDataSet _DeleteDataSet = new AisinoDataSet();
        private DataTable _DeleteTable = new DataTable();
        public int _iMonth;
        public int _iPeriod = 1;
        public int _iYear;
        private string _ModifyColumn = "修改发票";
        private AisinoDataSet _ModifyDataSet = new AisinoDataSet();
        private DataTable _ModifyTable = new DataTable();
        private AisinoDataGrid AisinoDataGrid_Add;
        private AisinoDataGrid AisinoDataGrid_Delete;
        private AisinoDataGrid AisinoDataGrid_Modify;
        public static bool AllDelete;
        public static bool AllKeep;
        private IContainer components;
        public Dictionary<int, int> currentPeriodCount;
        public ArrayList DeleteList;
        private List<Fpxx> fpList = new List<Fpxx>();
        private List<FPZJ> fpzjList = new List<FPZJ>();
        public Hashtable HashFPFromTaxCard = new Hashtable();
        private Hashtable HashFpSaveToDB = new Hashtable();
        private ILog loger = LogUtil.GetLogger<RepairReport>();
        public Fpxx modelXXFP;
        private bool Modified;
        private string Msg0 = string.Empty;
        private string Msg1 = string.Empty;
        private const int PageNum = 1;
        private const int PageSize = 200;
        private const int POSSIBLEBIGNUM = 0x2710;
        private const int POSSIBLECOMPUTENUM = 0x7d0;
        private int SaveToDBFpNum;
        private string ShowMsg = string.Empty;
        private TabControlPwSkin tabControl1;
        private ToolStripDropDownButton tool_DaYin;
        private ToolStripMenuItem tool_DaYin_AddResule;
        private ToolStripMenuItem tool_DaYin_DeleteResule;
        private ToolStripMenuItem tool_DaYin_ModifiedResule;
        private ToolStripButton tool_Exit;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;
        private XXFP XXFPSaveToDB = new XXFP(false);

        public RepairReport()
        {
            try
            {
                this.Initialize();
                DataColumn column = new DataColumn(this._AddColumn, typeof(string));
                this._AddTable.Columns.Add(column);
                DataColumn column2 = new DataColumn(this._DeleteColumn, typeof(string));
                this._DeleteTable.Columns.Add(column2);
                DataColumn column3 = new DataColumn(this._ModifyColumn, typeof(string));
                this._ModifyTable.Columns.Add(column3);
                this.currentPeriodCount = this.GetCurrentPeriodCount();
                base.MaximizeBox = false;
                base.MinimizeBox = false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void AddInvoice(Fpxx _Fpxx)
        {
            try
            {
                if (_Fpxx.retCode != "0000")
                {
                    string str = _Fpxx.fpdm.Trim();
                    string str2 = "发票代码=" + str + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim()) + "修复失败";
                    DataRow row = this._AddTable.NewRow();
                    row[this._AddColumn] = str2;
                    this._AddTable.Rows.Add(row);
                    str2 = str2 + "[错误代码]：" + _Fpxx.retCode;
                    this.loger.Error(str2);
                    return;
                }
                FPLX fplx = _Fpxx.fplx;
                if ((int)fplx <= 12)
                {
                    switch ((int)fplx)
                    {
                        case 0:
                        case 2:
                        case 11:
                        case 12:
                            goto Label_00F9;
                    }
                    return;
                }
                if (((int)fplx != 0x29) && ((int)fplx != 0x33))
                {
                    return;
                }
            Label_00F9:
                _Fpxx.xfbz = true;
                _Fpxx.fpdm.Trim();
                string message = "发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim()) + "【添加】";
                if (((_Fpxx.sign == null) || (_Fpxx.sign.Length <= 20)) || (_Fpxx.sign.Length > 0x800))
                {
                    if (!_Fpxx.zfbz)
                    {
                        message = ("发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim())) + "【签名信息有误，请作废本张发票】";
                        this.loger.Error(message);
                    }
                    if ((_Fpxx.sign != null) && (_Fpxx.sign.Length > 0x800))
                    {
                        _Fpxx.sign = _Fpxx.sign.Substring(0, 0x800);
                    }
                }
                DataRow row2 = this._AddTable.NewRow();
                row2[this._AddColumn] = message;
                this._AddTable.Rows.Add(row2);
                this.SaveFPXX(_Fpxx);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void AisinoDataGrid_Add_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.AisinoDataGrid_Add.DataSource = this.SplitDataTable(this._AddTable, e.PageNO, e.PageSize);
            this.AisinoDataGrid_Add.Refresh();
        }

        private void AisinoDataGrid_Delete_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.AisinoDataGrid_Delete.DataSource = this.SplitDataTable(this._DeleteTable, e.PageNO, e.PageSize);
            this.AisinoDataGrid_Delete.Refresh();
        }

        private void AisinoDataGrid_Modify_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.AisinoDataGrid_Modify.DataSource = this.SplitDataTable(this._ModifyTable, e.PageNO, e.PageSize);
            this.AisinoDataGrid_Modify.Refresh();
        }

        public void DeleteInoviceFromDB(bool IsShow = true)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DisposeForm()
        {
            this.AisinoDataGrid_Add.Dispose();
            this.AisinoDataGrid_Add = null;
            this.XXFPSaveToDB = null;
            this.fpList = null;
            this.fpzjList = null;
            this.loger = null;
            this.DeleteList = null;
        }

        public bool FpxfResultCompute(long StockCount, int iMonth, uint taxCardFpNum = 0)
        {
            try
            {
                return false;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
        }

        private Dictionary<int, int> GetCurrentPeriodCount()
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            try
            {
                List<int> periodCount = new List<int>();
                if (!dictionary.ContainsKey(2))
                {
                    if (!base.TaxCardInstance.QYLX.ISPTFP)
                    {
                        dictionary.Add(2, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(2);
                        dictionary.Add(2, periodCount[1]);
                    }
                }
                if (!dictionary.ContainsKey(0))
                {
                    periodCount.Clear();
                    if (!base.TaxCardInstance.QYLX.ISZYFP)
                    {
                        dictionary.Add(0, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(0);
                        dictionary.Add(0, periodCount[1]);
                    }
                }
                if (!dictionary.ContainsKey(12))
                {
                    periodCount.Clear();
                    if (!base.TaxCardInstance.QYLX.ISJDC)
                    {
                        dictionary.Add(12, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(12);
                        dictionary.Add(12, periodCount[1]);
                    }
                }
                if (!dictionary.ContainsKey(11))
                {
                    periodCount.Clear();
                    if (!base.TaxCardInstance.QYLX.ISHY)
                    {
                        dictionary.Add(11, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(11);
                        dictionary.Add(11, periodCount[1]);
                    }
                }
                if (!dictionary.ContainsKey(0x33))
                {
                    periodCount.Clear();
                    if (!base.TaxCardInstance.QYLX.ISPTFPDZ)
                    {
                        dictionary.Add(0x33, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(0x33);
                        dictionary.Add(0x33, periodCount[1]);
                    }
                }
                if (!dictionary.ContainsKey(0x29))
                {
                    periodCount.Clear();
                    if (!base.TaxCardInstance.QYLX.ISPTFPJSP)
                    {
                        dictionary.Add(0x29, 0);
                    }
                    else
                    {
                        periodCount = base.TaxCardInstance.GetPeriodCount(0x29);
                        dictionary.Add(0x29, periodCount[1]);
                    }
                }
                return dictionary;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return dictionary;
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_Exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Exit");
            this.tool_Exit.Text = "退出";
            this.tool_DaYin = this.xmlComponentLoader1.GetControlByName<ToolStripDropDownButton>("tool_DaYin");
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<TabControlPwSkin>("tabControl1");
            this.AisinoDataGrid_Delete = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid_Delete");
            this.AisinoDataGrid_Add = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid_Add");
            this.AisinoDataGrid_Modify = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid_Modify");
            this.tool_DaYin_DeleteResule = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_DaYin_DeleteResule");
            this.tool_DaYin_AddResule = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_DaYin_AddResule");
            this.tool_DaYin_ModifiedResule = this.xmlComponentLoader1.GetControlByName<ToolStripMenuItem>("tool_DaYin_ModifiedResule");
            this.tool_Exit.Click += new EventHandler(this.tool_Exit_Click);
            this.tool_DaYin_AddResule.Click += new EventHandler(this.tool_DaYin_AddResule_Clik);
            this.AisinoDataGrid_Add.GoToPageEvent += this.AisinoDataGrid_Add_GoToPageEvent;
            this.AisinoDataGrid_Add.ColumeHead = this.InsertColumn(this._AddColumn);
            this.AisinoDataGrid_Add.ReadOnly = true;
            this.AisinoDataGrid_Add.AutoScroll = true;
            this.AisinoDataGrid_Add.HorizontalScroll.Enabled = true;
            this.AisinoDataGrid_Add.AutoSize = false;
            this.AisinoDataGrid_Add.DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.tabControl1.Alignment = TabAlignment.Left;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new Size(30, 90);
            this.tabControl1.SizeMode = TabSizeMode.Fixed;
            this.tool_Exit.Margin = new Padding(20, 1, 0, 2);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(670, 0x16b);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPXF.RepairReport\Aisino.Fwkp.Fpkj.Form.FPXF.RepairReport.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(670, 0x16b);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "RepairReport";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "发票修复报告";
            base.ResumeLayout(false);
        }

        public void InitSet()
        {
            this.fpList.Clear();
            this._AddTable.Clear();
            this._ModifyTable.Clear();
            this._DeleteTable.Clear();
            this.HashFpSaveToDB.Clear();
            this.HashFPFromTaxCard.Clear();
            this.loger.Info(string.Concat(new object[] { "[发票修复]：查询数据库中冗余发票年：", this._iYear, "月：", this._iMonth }));
            this.DeleteList = this.XXFPSaveToDB.SelectAllDate_XXFP(this._iMonth, this._iYear);
            if ((this.DeleteList == null) || (this.DeleteList.Count <= 0))
            {
                this.loger.Error("[发票修复]：数据库中发票张数为空");
            }
            if (this.currentPeriodCount == null)
            {
                this.currentPeriodCount = this.GetCurrentPeriodCount();
            }
        }

        private List<Dictionary<string, string>> InsertColumn(string ColumnName)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("AisinoLBL", ColumnName);
            item.Add("Property", ColumnName);
            item.Add("Width", "100");
            item.Add("Type", "Text");
            item.Add("Align", "MiddleLeft");
            item.Add("HeadAlign", "MiddleCenter");
            list.Add(item);
            return list;
        }

        public static bool IsDaoQi(TaxCard TaxCardInstance)
        {
            try
            {
                TaxStateInfo stateInfo = TaxCardInstance.GetStateInfo(false);
                if (stateInfo.IsLockReached != 0)
                {
                    MessageManager.ShowMsgBox("FPXF-000006");
                    return true;
                }
                if (stateInfo.IsRepReached != 0)
                {
                    MessageManager.ShowMsgBox("FPXF-000007");
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
            return false;
        }

        private void ModifyDisMatchInv(Fpxx _Fpxx)
        {
            try
            {
                if (_Fpxx.retCode != "0000")
                {
                    string message = "发票代码=" + _Fpxx.fpdm.Trim() + "发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim()) + "修复失败";
                    DataRow row = this._ModifyTable.NewRow();
                    row[this._ModifyColumn] = message;
                    this._ModifyTable.Rows.Add(row);
                    message = message + "【错误代码】：" + _Fpxx.retCode;
                    this.loger.Error(message);
                    return;
                }
                if (((_Fpxx.sign == null) || (_Fpxx.sign.Length <= 20)) || (_Fpxx.sign.Length > 0x800))
                {
                    if (!_Fpxx.zfbz)
                    {
                        this.ShowMsg = "发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim());
                        this.ShowMsg = this.ShowMsg + "【签名信息有误，请作废本张发票】";
                        this.loger.Error(this.ShowMsg);
                        this.Modified = true;
                    }
                    if ((_Fpxx.sign != null) && (_Fpxx.sign.Length > 0x800))
                    {
                        _Fpxx.sign = _Fpxx.sign.Substring(0, 0x800);
                    }
                }
                FPLX fplx = _Fpxx.fplx;
                if ((int)fplx <= 12)
                {
                    switch ((int)fplx)
                    {
                        case 0:
                        case 2:
                            goto Label_01B4;

                        case 11:
                            _Fpxx = this.ModifyHWYSFP(_Fpxx);
                            break;

                        case 12:
                            _Fpxx = this.ModifyJDCFP(_Fpxx);
                            break;
                    }
                    goto Label_01D3;
                }
                if (((int)fplx != 0x29) && ((int)fplx != 0x33))
                {
                    goto Label_01D3;
                }
            Label_01B4:
                _Fpxx = this.ModifyZYPTFP(_Fpxx);
            Label_01D3:
                _Fpxx = this.ModifyFPMX(_Fpxx);
                _Fpxx = this.ModifyFPQD(_Fpxx);
                if (this.Modified)
                {
                    _Fpxx.xfbz = true;
                    this.Modified = false;
                    _Fpxx.dybz = true;
                    _Fpxx.xsdjbh = this.modelXXFP.xsdjbh;
                    DataRow row2 = this._ModifyTable.NewRow();
                    row2[this._ModifyColumn] = this.ShowMsg;
                    this._ModifyTable.Rows.Add(row2);
                    this.SaveFPXX(_Fpxx);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[ModifyDisMatchInv函数异常]" + exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private Fpxx ModifyFPMX(Fpxx _Fpxx)
        {
            try
            {
                if ((((_Fpxx != null) && !_Fpxx.isBlankWaste) && !this.Modified) && (12 != (int)_Fpxx.fplx))
                {
                    if ((this.modelXXFP.Mxxx == null) && (_Fpxx.Mxxx != null))
                    {
                        this.Modified = true;
                        return _Fpxx;
                    }
                    if ((this.modelXXFP.Mxxx != null) && (_Fpxx.Mxxx == null))
                    {
                        this.Modified = true;
                        return _Fpxx;
                    }
                    if ((this.modelXXFP.Mxxx == null) || (_Fpxx.Mxxx == null))
                    {
                        return _Fpxx;
                    }
                    if (_Fpxx.Mxxx.Count != this.modelXXFP.Mxxx.Count)
                    {
                        this.Modified = true;
                        return _Fpxx;
                    }
                    for (int i = 0; i < _Fpxx.Mxxx.Count; i++)
                    {
                        Dictionary<SPXX, string> dictionary = _Fpxx.Mxxx[i];
                        Dictionary<SPXX, string> dictionary2 = this.modelXXFP.Mxxx[i];
                        this.Msg0 = "";
                        this.Msg1 = "";
                        this.Msg0 = this.Msg0 + dictionary2[(SPXX)0];
                        this.Msg1 = this.Msg1 + dictionary[(SPXX)0];
                        if ((int)_Fpxx.fplx != 0x29)
                        {
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)3];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)3];
                        }
                        if ((int)_Fpxx.fplx != 0x29)
                        {
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)4];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)4];
                        }
                        double num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)7]);
                        double num3 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)7]);
                        this.Msg0 = this.Msg0 + num2.ToString("f2");
                        this.Msg1 = this.Msg1 + num3.ToString("f2");
                        double num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)8]);
                        double num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)8]);
                        this.Msg0 = this.Msg0 + num4.ToString("f2");
                        this.Msg1 = this.Msg1 + num5.ToString("f2");
                        double num6 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)9]);
                        double num7 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)9]);
                        this.Msg0 = this.Msg0 + num6.ToString("f2");
                        this.Msg1 = this.Msg1 + num7.ToString("f2");
                        this.Msg0 = this.Msg0 + dictionary2[(SPXX)11];
                        this.Msg1 = this.Msg1 + dictionary[(SPXX)11];
                        this.Msg0 = this.Msg0 + dictionary2[(SPXX)10];
                        this.Msg1 = this.Msg1 + dictionary[(SPXX)10];
                        this.Msg0 = this.Msg0 + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dictionary2[(SPXX)13]);
                        this.Msg1 = this.Msg1 + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dictionary[(SPXX)13]);
                        double num8 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)6]);
                        double num9 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)6]);
                        this.Msg0 = this.Msg0 + num8.ToString("f12");
                        this.Msg1 = this.Msg1 + num9.ToString("f12");
                        double num10 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)5]);
                        double num11 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)5]);
                        this.Msg0 = this.Msg0 + num10.ToString("f12");
                        this.Msg1 = this.Msg1 + num11.ToString("f12");
                        if (this.Msg0.GetHashCode() != this.Msg1.GetHashCode())
                        {
                            this.SaveToBase64(this.Msg0);
                            this.SaveToBase64(this.Msg1);
                            this.Modified = true;
                            return _Fpxx;
                        }
                    }
                }
                return _Fpxx;
            }
            catch (Exception exception)
            {
                string str = "发票代码=" + _Fpxx.fpdm.Trim() + "发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim()) + "修复失败！";
                DataRow row = this._ModifyTable.NewRow();
                row[this._ModifyColumn] = str;
                this._ModifyTable.Rows.Add(row);
                this.loger.Error("[ModifyFPMX函数异常]" + exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return _Fpxx;
            }
            return _Fpxx;
        }

        private Fpxx ModifyFPQD(Fpxx _Fpxx)
        {
            try
            {
                if (((_Fpxx != null) && !_Fpxx.isBlankWaste) && (!this.Modified && !base.TaxCardInstance.IsLargeInvDetail))
                {
                    if ((11 == (int)_Fpxx.fplx) || (12 == (int)_Fpxx.fplx))
                    {
                        return _Fpxx;
                    }
                    if ((this.modelXXFP.Qdxx == null) && (_Fpxx.Qdxx != null))
                    {
                        this.Modified = true;
                        return _Fpxx;
                    }
                    if ((this.modelXXFP.Qdxx != null) && (_Fpxx.Qdxx == null))
                    {
                        this.Modified = true;
                        return _Fpxx;
                    }
                    if ((this.modelXXFP.Qdxx != null) && (_Fpxx.Qdxx != null))
                    {
                        if (_Fpxx.Qdxx.Count != this.modelXXFP.Qdxx.Count)
                        {
                            this.Modified = true;
                            return _Fpxx;
                        }
                        for (int i = 0; i < _Fpxx.Qdxx.Count; i++)
                        {
                            Dictionary<SPXX, string> dictionary = _Fpxx.Qdxx[i];
                            Dictionary<SPXX, string> dictionary2 = this.modelXXFP.Qdxx[i];
                            this.Msg0 = "";
                            this.Msg1 = "";
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)0];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)0];
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)3];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)3];
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)4];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)4];
                            double num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)7]);
                            double num3 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)7]);
                            this.Msg0 = this.Msg0 + num2.ToString("f2");
                            this.Msg1 = this.Msg1 + num3.ToString("f2");
                            double num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)8]);
                            double num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)8]);
                            this.Msg0 = this.Msg0 + num4.ToString("f2");
                            this.Msg1 = this.Msg1 + num5.ToString("f2");
                            double num6 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)9]);
                            double num7 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)9]);
                            this.Msg0 = this.Msg0 + num6.ToString("f2");
                            this.Msg1 = this.Msg1 + num7.ToString("f2");
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)11];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)11];
                            this.Msg0 = this.Msg0 + dictionary2[(SPXX)10];
                            this.Msg1 = this.Msg1 + dictionary[(SPXX)10];
                            this.Msg0 = this.Msg0 + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dictionary2[(SPXX)13]);
                            this.Msg1 = this.Msg1 + Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(dictionary[(SPXX)13]);
                            double num8 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)6]);
                            double num9 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)6]);
                            this.Msg0 = this.Msg0 + num8.ToString("f12");
                            this.Msg1 = this.Msg1 + num9.ToString("f12");
                            double num10 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary2[(SPXX)5]);
                            double num11 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(dictionary[(SPXX)5]);
                            this.Msg0 = this.Msg0 + num10.ToString("f12");
                            this.Msg1 = this.Msg1 + num11.ToString("f12");
                            if (this.Msg0.GetHashCode() != this.Msg1.GetHashCode())
                            {
                                this.SaveToBase64(this.Msg0);
                                this.SaveToBase64(this.Msg1);
                                this.Modified = true;
                                return _Fpxx;
                            }
                        }
                    }
                }
                return _Fpxx;
            }
            catch (Exception exception)
            {
                string str = "发票代码=" + _Fpxx.fpdm.Trim() + "发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim()) + " 修复失败";
                DataRow row = this._ModifyTable.NewRow();
                row[this._ModifyColumn] = str;
                this._ModifyTable.Rows.Add(row);
                this.loger.Error("[ModifyFPQD函数异常]" + exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return _Fpxx;
            }
            return _Fpxx;
        }

        private Fpxx ModifyHWYSFP(Fpxx _Fpxx)
        {
            try
            {
                this.Msg0 = string.Empty;
                this.Msg1 = string.Empty;
                this.ShowMsg = string.Empty;
                this.Modified = false;
                this.Msg0 = "发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim());
                this.Msg1 = this.Msg0;
                this.ShowMsg = this.Msg1 + "【修复】";
                Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(_Fpxx.fphm.Trim());
                if (this.modelXXFP == null)
                {
                    MessageManager.ShowMsgBox("FPXF-000008");
                    this.loger.Error(MessageManager.GetMessageInfo("FPXF-000008"));
                    return _Fpxx;
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.kpjh;
                this.Msg1 = this.Msg1 + _Fpxx.kpjh;
                string str = ToolUtil.FormatDateTimeEx(_Fpxx.kprq);
                string str2 = ToolUtil.FormatDateTimeEx(this.modelXXFP.kprq);
                this.Msg0 = this.Msg0 + str2;
                this.Msg1 = this.Msg1 + str;
                this.Msg0 = this.Msg0 + this.modelXXFP.ssyf;
                this.Msg1 = this.Msg1 + _Fpxx.ssyf;
                this.Msg0 = this.Msg0 + this.modelXXFP.bsq;
                this.Msg1 = this.Msg1 + _Fpxx.bsq;
                this.Msg0 = this.Msg0 + this.modelXXFP.mw;
                this.Msg1 = this.Msg1 + _Fpxx.mw;
                this.Msg0 = this.Msg0 + this.modelXXFP.cyrmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.cyrmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.cyrnsrsbh.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.cyrnsrsbh.Trim();
                this.modelXXFP.spfmc.Trim(new char[1]);
                this.Msg0 = this.Msg0 + this.modelXXFP.spfmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.spfmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.spfnsrsbh.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.spfnsrsbh.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.shrmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.shrmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.shrnsrsbh.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.shrnsrsbh.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.fhrmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.fhrmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.fhrnsrsbh.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.fhrnsrsbh.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.qyd.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.qyd.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.yshwxx.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.yshwxx.Trim();
                double num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.je);
                double num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.je);
                this.Msg0 = this.Msg0 + string.Format("{0:F2}", num);
                this.Msg1 = this.Msg1 + string.Format("{0:F2}", num2);
                double num3 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.sLv);
                double num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.sLv);
                this.Msg0 = this.Msg0 + num3.ToString("f2");
                this.Msg1 = this.Msg1 + num4.ToString("f2");
                double num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.se);
                double num6 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.se);
                this.Msg0 = this.Msg0 + string.Format("{0:F2}", num5);
                this.Msg1 = this.Msg1 + string.Format("{0:F2}", num6);
                this.Msg0 = this.Msg0 + this.modelXXFP.jqbh;
                this.Msg1 = this.Msg1 + _Fpxx.jqbh;
                this.Msg0 = this.Msg0 + this.modelXXFP.czch.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.czch.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.ccdw.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.ccdw.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.zgswjgmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.zgswjgmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.zgswjgdm.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.zgswjgdm.Trim();
                this.Msg0 = this.Msg0 + (this.modelXXFP.zfbz ? "是" : "否");
                this.Msg1 = this.Msg1 + (_Fpxx.zfbz ? "是" : "否");
                this.Msg0 = this.Msg0 + this.modelXXFP.yysbz;
                this.Msg1 = this.Msg1 + _Fpxx.yysbz;
                if (this.currentPeriodCount[(int)_Fpxx.fplx] != 0)
                {
                    this.Msg0 = this.Msg0 + (this.modelXXFP.bsbz ? "是" : "否");
                    this.Msg1 = this.Msg1 + (_Fpxx.bsbz ? "是" : "否");
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.bszt;
                this.Msg1 = this.Msg1 + _Fpxx.bszt;
                this.Msg0 = this.Msg0 + this.modelXXFP.sign;
                this.Msg1 = this.Msg1 + _Fpxx.sign;
                this.Msg0 = this.Msg0 + this.modelXXFP.zfsj;
                this.Msg1 = this.Msg1 + _Fpxx.zfsj;
                this.Msg0 = this.Msg0 + this.modelXXFP.zyspmc.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.zyspmc.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.kpr.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.kpr.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.fhr.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.fhr.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.skr.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.skr.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.bz.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.bz.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.jym;
                this.Msg1 = this.Msg1 + _Fpxx.jym;
                this.Msg0 = this.Msg0 + this.modelXXFP.jmbbh;
                this.Msg1 = this.Msg1 + _Fpxx.jmbbh;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFpdm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFpdm;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFphm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFphm;
                this.Msg0 = this.Msg0 + this.modelXXFP.redNum;
                this.Msg1 = this.Msg1 + _Fpxx.redNum;
                if (this.Msg0.GetHashCode() != this.Msg1.GetHashCode())
                {
                    this.SaveToBase64(this.Msg0);
                    this.SaveToBase64(this.Msg1);
                    this.Modified = true;
                }
                return _Fpxx;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
                return _Fpxx;
            }
        }

        private Fpxx ModifyJDCFP(Fpxx _Fpxx)
        {
            try
            {
                this.Msg0 = string.Empty;
                this.Msg1 = string.Empty;
                this.ShowMsg = string.Empty;
                this.Modified = false;
                this.Msg0 = "发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim());
                this.Msg1 = this.Msg0;
                this.ShowMsg = this.Msg1 + "【修复】";
                if (this.modelXXFP == null)
                {
                    MessageManager.ShowMsgBox("FPXF-000008");
                    this.loger.Error(MessageManager.GetMessageInfo("FPXF-000008"));
                    return _Fpxx;
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.kpjh;
                this.Msg1 = this.Msg1 + _Fpxx.kpjh;
                this.Msg0 = this.Msg0 + this.modelXXFP.jqbh;
                this.Msg1 = this.Msg1 + _Fpxx.jqbh;
                this.Msg0 = this.Msg0 + ToolUtil.FormatDateTimeEx(this.modelXXFP.kprq);
                this.Msg1 = this.Msg1 + ToolUtil.FormatDateTimeEx(_Fpxx.kprq);
                this.Msg0 = this.Msg0 + this.modelXXFP.ssyf;
                this.Msg1 = this.Msg1 + _Fpxx.ssyf;
                this.Msg0 = this.Msg0 + this.modelXXFP.bsq;
                this.Msg1 = this.Msg1 + _Fpxx.bsq;
                this.Msg0 = this.Msg0 + this.modelXXFP.mw;
                this.Msg1 = this.Msg1 + _Fpxx.mw;
                this.Msg0 = this.Msg0 + this.modelXXFP.gfmc;
                this.Msg1 = this.Msg1 + _Fpxx.gfmc;
                int index = _Fpxx.gfsh.IndexOf("\0");
                if (index > 0)
                {
                    _Fpxx.gfsh = _Fpxx.gfsh.Substring(0, index);
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.gfsh;
                this.Msg1 = this.Msg1 + _Fpxx.gfsh;
                this.Msg0 = this.Msg0 + this.modelXXFP.sfzhm;
                this.Msg1 = this.Msg1 + _Fpxx.sfzhm;
                this.Msg0 = this.Msg0 + this.modelXXFP.cllx;
                this.Msg1 = this.Msg1 + _Fpxx.cllx;
                this.Msg0 = this.Msg0 + this.modelXXFP.cpxh;
                this.Msg1 = this.Msg1 + _Fpxx.cpxh;
                this.Msg0 = this.Msg0 + this.modelXXFP.cd;
                this.Msg1 = this.Msg1 + _Fpxx.cd;
                this.Msg0 = this.Msg0 + this.modelXXFP.hgzh;
                this.Msg1 = this.Msg1 + _Fpxx.hgzh;
                this.Msg0 = this.Msg0 + this.modelXXFP.jkzmsh;
                this.Msg1 = this.Msg1 + _Fpxx.jkzmsh;
                this.Msg0 = this.Msg0 + this.modelXXFP.sjdh;
                this.Msg1 = this.Msg1 + _Fpxx.sjdh;
                this.Msg0 = this.Msg0 + this.modelXXFP.fdjhm;
                this.Msg1 = this.Msg1 + _Fpxx.fdjhm;
                this.Msg0 = this.Msg0 + this.modelXXFP.clsbdh;
                this.Msg1 = this.Msg1 + _Fpxx.clsbdh;
                this.Msg0 = this.Msg0 + this.modelXXFP.sccjmc;
                this.Msg1 = this.Msg1 + _Fpxx.sccjmc;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfmc;
                this.Msg1 = this.Msg1 + _Fpxx.xfmc;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfdh;
                this.Msg1 = this.Msg1 + _Fpxx.xfdh;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfsh;
                this.Msg1 = this.Msg1 + _Fpxx.xfsh;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfzh;
                this.Msg1 = this.Msg1 + _Fpxx.xfzh;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfdz;
                this.Msg1 = this.Msg1 + _Fpxx.xfdz;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfyh;
                this.Msg1 = this.Msg1 + _Fpxx.xfyh;
                double num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.sLv);
                double num3 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.sLv);
                this.Msg0 = this.Msg0 + num2.ToString("f2");
                this.Msg1 = this.Msg1 + num3.ToString("f2");
                double num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.se);
                double num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.se);
                this.Msg0 = this.Msg0 + string.Format("{0:F2}", num4);
                this.Msg1 = this.Msg1 + string.Format("{0:F2}", num5);
                this.Msg0 = this.Msg0 + this.modelXXFP.zgswjgmc;
                this.Msg1 = this.Msg1 + _Fpxx.zgswjgmc;
                this.Msg0 = this.Msg0 + this.modelXXFP.zgswjgdm;
                this.Msg1 = this.Msg1 + _Fpxx.zgswjgdm;
                double num6 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.je);
                double num7 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.je);
                this.Msg0 = this.Msg0 + string.Format("{0:F2}", num6);
                this.Msg1 = this.Msg1 + string.Format("{0:F2}", num7);
                this.Msg0 = this.Msg0 + this.modelXXFP.dw;
                this.Msg1 = this.Msg1 + _Fpxx.dw;
                this.Msg0 = this.Msg0 + this.modelXXFP.xcrs;
                this.Msg1 = this.Msg1 + _Fpxx.xcrs;
                this.Msg0 = this.Msg0 + (this.modelXXFP.zfbz ? "是" : "否");
                this.Msg1 = this.Msg1 + (_Fpxx.zfbz ? "是" : "否");
                this.Msg0 = this.Msg0 + this.modelXXFP.yysbz;
                this.Msg1 = this.Msg1 + _Fpxx.yysbz;
                if (this.currentPeriodCount[(int)_Fpxx.fplx] != 0)
                {
                    this.Msg0 = this.Msg0 + (this.modelXXFP.bsbz ? "是" : "否");
                    this.Msg1 = this.Msg1 + (_Fpxx.bsbz ? "是" : "否");
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.bszt;
                this.Msg1 = this.Msg1 + _Fpxx.bszt;
                this.Msg0 = this.Msg0 + this.modelXXFP.sign;
                this.Msg1 = this.Msg1 + _Fpxx.sign;
                this.Msg0 = this.Msg0 + this.modelXXFP.zfsj;
                this.Msg1 = this.Msg1 + _Fpxx.zfsj;
                this.Msg0 = this.Msg0 + this.modelXXFP.fhr;
                this.Msg1 = this.Msg1 + _Fpxx.fhr;
                this.Msg0 = this.Msg0 + this.modelXXFP.kpr.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.kpr.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.bz.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.bz.Trim();
                this.Msg0 = this.Msg0 + this.modelXXFP.jym;
                this.Msg1 = this.Msg1 + _Fpxx.jym;
                this.Msg0 = this.Msg0 + this.modelXXFP.jmbbh;
                this.Msg1 = this.Msg1 + _Fpxx.jmbbh;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFpdm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFpdm;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFphm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFphm;
                this.Msg0 = this.Msg0 + this.modelXXFP.redNum;
                this.Msg1 = this.Msg1 + _Fpxx.redNum;
                if (this.Msg0.GetHashCode() != this.Msg1.GetHashCode())
                {
                    this.SaveToBase64(this.Msg0);
                    this.SaveToBase64(this.Msg1);
                    this.Modified = true;
                }
                return _Fpxx;
            }
            catch (Exception exception)
            {
                this.loger.Error("[ModifyJDCFP函数异常]" + exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return _Fpxx;
            }
        }

        private Fpxx ModifyZYPTFP(Fpxx _Fpxx)
        {
            try
            {
                this.Msg0 = string.Empty;
                this.Msg1 = string.Empty;
                this.ShowMsg = string.Empty;
                this.Modified = false;
                this.Msg0 = "发票代码=" + _Fpxx.fpdm.Trim() + " 发票号码=" + ShareMethods.FPHMTo8Wei(_Fpxx.fphm.Trim());
                this.Msg1 = this.Msg0;
                this.ShowMsg = this.Msg1 + "【修复】";
                if (this.modelXXFP == null)
                {
                    MessageManager.ShowMsgBox("FPXF-000008");
                    this.loger.Error(MessageManager.GetMessageInfo("FPXF-000008"));
                    return _Fpxx;
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.kpjh;
                this.Msg1 = this.Msg1 + _Fpxx.kpjh;
                this.Msg0 = this.Msg0 + ToolUtil.FormatDateTimeEx(this.modelXXFP.kprq);
                this.Msg1 = this.Msg1 + ToolUtil.FormatDateTimeEx(_Fpxx.kprq);
                this.Msg0 = this.Msg0 + this.modelXXFP.ssyf;
                this.Msg1 = this.Msg1 + _Fpxx.ssyf;
                this.Msg0 = this.Msg0 + this.modelXXFP.bsq;
                this.Msg1 = this.Msg1 + _Fpxx.bsq;
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.mw;
                    this.Msg1 = this.Msg1 + _Fpxx.mw;
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.hxm;
                    this.Msg1 = this.Msg1 + _Fpxx.hxm;
                }
                double num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.je);
                double num2 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.je);
                this.Msg0 = this.Msg0 + num.ToString("f2");
                this.Msg1 = this.Msg1 + num2.ToString("f2");
                double num3 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.sLv);
                double num4 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.sLv);
                this.Msg0 = this.Msg0 + num3.ToString("f2");
                this.Msg1 = this.Msg1 + num4.ToString("f2");
                double num5 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(this.modelXXFP.se);
                double num6 = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDouble(_Fpxx.se);
                this.Msg0 = this.Msg0 + num5.ToString("f2");
                this.Msg1 = this.Msg1 + num6.ToString("f2");
                this.Msg0 = this.Msg0 + (this.modelXXFP.zfbz ? "是" : "否");
                this.Msg1 = this.Msg1 + (_Fpxx.zfbz ? "是" : "否");
                this.Msg0 = this.Msg0 + this.modelXXFP.yysbz;
                this.Msg1 = this.Msg1 + _Fpxx.yysbz;
                this.Msg0 = this.Msg0 + this.modelXXFP.bszt;
                this.Msg1 = this.Msg1 + _Fpxx.bszt;
                this.Msg0 = this.Msg0 + this.modelXXFP.sign;
                this.Msg1 = this.Msg1 + _Fpxx.sign;
                this.Msg0 = this.Msg0 + this.modelXXFP.zfsj;
                this.Msg1 = this.Msg1 + _Fpxx.zfsj;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfsh;
                this.Msg1 = this.Msg1 + _Fpxx.xfsh;
                this.Msg0 = this.Msg0 + this.modelXXFP.xfmc;
                this.Msg1 = this.Msg1 + _Fpxx.xfmc;
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.xfyhzh;
                    this.Msg1 = this.Msg1 + _Fpxx.xfyhzh;
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.xfdzdh;
                    this.Msg1 = this.Msg1 + _Fpxx.xfdzdh;
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.gfsh;
                this.Msg1 = this.Msg1 + _Fpxx.gfsh;
                this.Msg0 = this.Msg0 + this.modelXXFP.gfmc;
                this.Msg1 = this.Msg1 + _Fpxx.gfmc;
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.gfyhzh;
                    this.Msg1 = this.Msg1 + _Fpxx.gfyhzh;
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.gfdzdh;
                    this.Msg1 = this.Msg1 + _Fpxx.gfdzdh;
                }
                if (this.currentPeriodCount[(int)_Fpxx.fplx] != 0)
                {
                    this.Msg0 = this.Msg0 + (this.modelXXFP.bsbz ? "是" : "否");
                    this.Msg1 = this.Msg1 + (_Fpxx.bsbz ? "是" : "否");
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.zyspmc.Trim();
                    this.Msg1 = this.Msg1 + _Fpxx.zyspmc.Trim();
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.kpr.Trim();
                    this.Msg1 = this.Msg1 + _Fpxx.kpr.Trim();
                }
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.fhr.Trim();
                    this.Msg1 = this.Msg1 + _Fpxx.fhr.Trim();
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.skr.Trim();
                this.Msg1 = this.Msg1 + _Fpxx.skr.Trim();
                if ((int)_Fpxx.fplx != 0x29)
                {
                    this.Msg0 = this.Msg0 + this.modelXXFP.bz.Trim();
                    this.Msg1 = this.Msg1 + _Fpxx.bz.Trim();
                }
                this.Msg0 = this.Msg0 + this.modelXXFP.jym;
                this.Msg1 = this.Msg1 + _Fpxx.jym;
                this.Msg0 = this.Msg0 + this.modelXXFP.jmbbh;
                this.Msg1 = this.Msg1 + _Fpxx.jmbbh;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFpdm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFpdm;
                this.Msg0 = this.Msg0 + this.modelXXFP.blueFphm;
                this.Msg1 = this.Msg1 + _Fpxx.blueFphm;
                this.Msg0 = this.Msg0 + this.modelXXFP.redNum;
                this.Msg1 = this.Msg1 + _Fpxx.redNum;
                this.Msg0 = this.Msg0 + this.modelXXFP.jqbh;
                this.Msg1 = this.Msg1 + _Fpxx.jqbh;
                if (this.Msg0.GetHashCode() != this.Msg1.GetHashCode())
                {
                    this.SaveToBase64(this.Msg0);
                    this.SaveToBase64(this.Msg1);
                    this.Modified = true;
                }
                return _Fpxx;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                MessageManager.ShowMsgBox(exception.Message);
                return _Fpxx;
            }
        }

        public void RepairInvoice(Fpxx fpxx, int PLevel = 1)
        {
            try
            {
                if (PLevel == 0)
                {
                    string fPZL = Aisino.Fwkp.Fpkj.Common.Tool.PareFpType(fpxx.fplx);
                    this.modelXXFP = this.XXFPSaveToDB.GetModel(fPZL, fpxx.fpdm, Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(fpxx.fphm), "");
                }
                if ((int)fpxx.fplx == 0x29)
                {
                    fpxx.mw = "";
                    fpxx.hxm = "";
                    fpxx.gfdzdh = "";
                    fpxx.gfyhzh = "";
                    fpxx.xfdzdh = "";
                    fpxx.xfyhzh = "";
                    fpxx.kpr = "";
                    fpxx.fhr = "";
                }
                if (this.modelXXFP == null)
                {
                    fpxx.OtherFields = new Dictionary<string, object>();
                    fpxx.OtherFields.Add("PZRQ", ToolUtil.FormatDateTimeEx("1899-12-30 23:59:59"));
                    fpxx.OtherFields.Add("PZLB", "");
                    fpxx.OtherFields.Add("PZHM", "-1");
                    fpxx.OtherFields.Add("PZYWH", "");
                    fpxx.OtherFields.Add("PZZT", "-1");
                    fpxx.OtherFields.Add("PZWLYWH", "");
                    fpxx.OtherFields.Add("BSRZ", "");
                    this.AddInvoice(fpxx);
                }
                else
                {
                    fpxx.OtherFields = new Dictionary<string, object>(this.modelXXFP.OtherFields);
                    fpxx.xsdjbh = this.modelXXFP.xsdjbh;
                    this.ModifyDisMatchInv(fpxx);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private bool SaveFPXX(Fpxx _Fpxx)
        {
            int num = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(_Fpxx.fphm.Trim());
            string key = _Fpxx.fpdm + "_" + num;
            if (!this.HashFpSaveToDB.ContainsKey(key))
            {
                this.HashFpSaveToDB.Add(key, num);
                this.fpList.Add(_Fpxx);
                if ((_Fpxx.Mxxx != null) && (_Fpxx.Mxxx.Count > 0))
                {
                    this.SaveToDBFpNum += _Fpxx.Mxxx.Count;
                }
                if ((_Fpxx.Qdxx != null) && (_Fpxx.Qdxx.Count > 0))
                {
                    this.SaveToDBFpNum += _Fpxx.Qdxx.Count;
                }
                this.SaveToDBFpNum++;
            }
            else
            {
                this.loger.Error("[SaveFPXX]:发票主键重复: " + key);
            }
            if ((this.fpList.Count == 0x7d0) || (this.SaveToDBFpNum >= 0x2710))
            {
                this.SaveToDB();
            }
            return true;
        }

        private void SaveToBase64(string msg)
        {
            if ((msg != null) && (msg.Length != 0))
            {
                byte[] bytes = ToolUtil.GetBytes(msg);
                if ((bytes != null) && (bytes.Length > 0))
                {
                    string message = Convert.ToBase64String(bytes);
                    this.loger.Error(message);
                }
            }
        }

        public void SaveToDB()
        {
            try
            {
                if ((this.fpList != null) && (this.fpList.Count > 0))
                {
                    this.XXFPSaveToDB.SaveXxfp(this.fpList);
                    this.SaveToDBFpNum = 0;
                    for (int i = 0; i < this.fpList.Count; i++)
                    {
                        Fpxx local1 = this.fpList[i];
                    }
                    this.fpList.Clear();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        public AisinoDataSet SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 100;
            }
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return null;
            }
            int count = dt.Rows.Count;
            if (PageSize <= 0)
            {
                PageSize = count;
            }
            AisinoDataSet set = new AisinoDataSet();
            int num2 = count / PageSize;
            if ((num2 * PageSize) < count)
            {
                num2++;
            }
            if (count == 0)
            {
                num2 = 0;
            }
            if (PageIndex > num2)
            {
                PageIndex = num2;
            }
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }
            DataTable table = dt.Clone();
            int num3 = (PageIndex - 1) * PageSize;
            int num4 = PageIndex * PageSize;
            if (num3 >= dt.Rows.Count)
            {
                set.Data = table;
                set.AllPageNum = num2;
                set.AllRows = count;
                set.CurrentPage = PageIndex;
                set.PageSize = PageSize;
                return set;
            }
            if (num4 > dt.Rows.Count)
            {
                num4 = dt.Rows.Count;
            }
            for (int i = num3; i <= (num4 - 1); i++)
            {
                DataRow row = table.NewRow();
                DataRow row2 = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    row[column.ColumnName] = row2[column.ColumnName];
                }
                table.Rows.Add(row);
            }
            set.Data = table;
            set.AllPageNum = num2;
            set.AllRows = count;
            set.CurrentPage = PageIndex;
            set.PageSize = PageSize;
            return set;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.tabControl1.GetTabRect(e.Index);
            RectangleF tabRect = this.tabControl1.GetTabRect(e.Index);
            Graphics graphics = e.Graphics;
            StringFormat format = new StringFormat {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Center
            };
            Font font = this.tabControl1.Font;
            SolidBrush brush = new SolidBrush(Color.Black);
            graphics.DrawString(((TabControl) sender).TabPages[e.Index].Text, font, brush, tabRect, format);
        }

        private void tool_DaYin_AddResule_Clik(object sender, EventArgs e)
        {
            try
            {
                if ((this._AddTable == null) || (this._AddTable.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("FPXF-000002");
                }
                else if ((this.AisinoDataGrid_Add == null) || (this.AisinoDataGrid_Add.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有可打印的列表内容");
                }
                else
                {
                    this.AisinoDataGrid_Add.Font = new Font("宋体", 9f);
                    this.AisinoDataGrid_Add.Print("发票修复添加", this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void tool_DaYin_DeleteResule_Clik(object sender, EventArgs e)
        {
            try
            {
                if ((this._DeleteTable == null) || (this._DeleteTable.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("FPXF-000002");
                }
                else if ((this.AisinoDataGrid_Delete == null) || (this.AisinoDataGrid_Delete.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有可打印的列表内容");
                }
                else
                {
                    this.AisinoDataGrid_Delete.Print("发票修复删除", this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void tool_DaYin_ModifiedResule_Clik(object sender, EventArgs e)
        {
            try
            {
                if ((this._ModifyTable == null) || (this._ModifyTable.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("FPXF-000002");
                }
                else if ((this.AisinoDataGrid_Modify == null) || (this.AisinoDataGrid_Modify.Rows.Count <= 0))
                {
                    MessageManager.ShowMsgBox("没有可打印的列表内容");
                }
                else
                {
                    this.AisinoDataGrid_Modify.Print("发票修复修改", this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void tool_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FPZJ
        {
            public string fpdm;
            public string fphm;
            public string fpzl;
        }
    }
}

