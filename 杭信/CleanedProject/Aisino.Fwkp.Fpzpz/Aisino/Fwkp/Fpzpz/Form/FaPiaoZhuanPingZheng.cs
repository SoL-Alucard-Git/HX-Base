namespace Aisino.Fwkp.Fpzpz.Form
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Plugin.Core.WebService;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpzpz.BLL;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class FaPiaoZhuanPingZheng : DockForm
    {
        private string _A6PzName = string.Empty;
        private string _A6PzType = string.Empty;
        private int _AvePageNum = 10;
        private BaseDAOSQLite _BaseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
        private DateTime _CardClock;
        private FaPiaoFindTiaoJian _FaPiaoFindTiaoJian = new FaPiaoFindTiaoJian();
        private bool _LinkSuccess;
        private ILog _Loger = LogUtil.GetLogger<FaPiaoZhuanPingZheng>();
        private ProgressHelper _ProgressHelper;
        private List<string> A6pzInfoList = new List<string>();
        private bool bIsWLYW;
        private DataGridViewTextBoxEditingControl CellEdit;
        private DataGridViewTextBoxColumn Column_Fpdm;
        private DataGridViewTextBoxColumn Column_Fphm;
        private DataGridViewTextBoxColumn Column_Fpzl;
        private DataGridViewTextBoxColumn Column_Gfbh;
        private DataGridViewTextBoxColumn Column_Gfmc;
        private DataGridViewTextBoxColumn Column_Gfsh;
        private DataGridViewTextBoxColumn Column_Hjje;
        private DataGridViewTextBoxColumn Column_Hjse;
        private DataGridViewTextBoxColumn Column_Jshj;
        private DataGridViewTextBoxColumn Column_Kprq;
        private DataGridViewTextBoxColumn Column_Xzbz;
        private DataGridViewTextBoxColumn Column_Zfbz;
        private AisinoCMB combo_PingZhengType;
        private IContainer components;
        private string ContrlSubValue = string.Empty;
        private CustomStyleDataGrid customStyleDataGrid1;
        private DateTimePicker dateTime_ZhiDanRiQi;
        private DataTable dtKM = new DataTable();
        private DataTable dtWLYWH = new DataTable();
        private Aisino.Fwkp.Fpzpz.Common.Tool.FP_PZ fp_pz;
        private string GoodsSubValue = string.Empty;
        private int iIndexFpdm = 1;
        private int iIndexFphm = 2;
        private int iIndexFpzl;
        private int iIndexGfbm = 0x1f;
        private int iIndexGfmc = 5;
        private int iIndexGfsh = 6;
        private int iIndexHjje = 0x13;
        private int iIndexHjse = 0x15;
        private int iIndexJshj = 0x34;
        private int iIndexKprq = 0x11;
        private int iIndexXzbz = 0x33;
        private int iIndexZfbz = 0x22;
        private bool isA6CustHs;
        private bool isA6GoodsHs;
        private bool isA6NumHs;
        private IKHBM khbmBll = new KHBM();
        private List<string> listBuyerInfoList = new List<string>();
        private List<string> listGoodsInfoList = new List<string>();
        private List<string> listPZType = new List<string>();
        private int msgPzCount;
        private int msgPzSucCount;
        private int msgSelInvCount;
        private Panel panel1;
        private Panel panneltop;
        private DateTime PZEntryDate = DingYiZhiFuChuan.dataTimeCCRQ;
        private IPZFLB pzflbBll = new PZFLB();
        private ISPBM spbmBll = new SPBM();
        private string sPZWLYWH = string.Empty;
        private ToolStripButton tool_DaYin;
        private ToolStripButton tool_FaPiao;
        private ToolStripButton tool_Find;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_Quit;
        private ToolStripButton tool_QuXiao;
        private ToolStripButton tool_QuXu;
        private ToolStripButton tool_ShunXu;
        private ToolStripButton tool_ZhiDan;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;
        private string XSSRSubJect = string.Empty;
        private string XSTHSubject = string.Empty;
        private IXXFP xxfpBll = new XXFP();
        private IXZQYBM xzqybmBll = new XZQYBM();
        private string YJZZSSubject = string.Empty;
        private string YSSubject = string.Empty;

        public FaPiaoZhuanPingZheng()
        {
            try
            {
                this.Initialize();
                ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
                this.toolStrip1.AutoSize = false;
                this.toolStrip1.Size = new Size(0x2db, 0x21);
                this.panneltop.Controls.Add(this.toolStrip1);
                this.toolStrip1.Dock = DockStyle.Bottom;
                this._CardClock = base.TaxCardInstance.GetCardClock();
                this.InitializeFaPiaoFindTiaoJian();
                this.InsertGridColumn();
                this.FlushGrid(FlushGridType.FirstFlushGrid);
                this.SetZdTime();
                this.xxfpBll.CreateTempTable();
                this._FaPiaoFindTiaoJian.DataGrid = this.customStyleDataGrid1;
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

        private bool AddInfoToCusrKmTBL()
        {
            try
            {
                this.InitCusrKmTBL();
                List<KHBMModal> list = this.khbmBll.SelectKHKMB();
                if (list == null)
                {
                    this._Loger.Error("客户科目表错误：数据为空");
                    return false;
                }
                if (0 >= list.Count)
                {
                    this._Loger.Error("客户科目表错误：数据为空");
                    return false;
                }
                if (this.ContrlSubValue.Equals(DingYiZhiFuChuan.YskmItem[0]))
                {
                    foreach (KHBMModal modal in list)
                    {
                        this.EditCusrTbl(modal);
                    }
                }
                else if (this.ContrlSubValue.Equals(DingYiZhiFuChuan.YskmItem[1]) || this.ContrlSubValue.Equals(DingYiZhiFuChuan.YskmItem[2]))
                {
                    foreach (KHBMModal modal2 in list)
                    {
                        if (string.IsNullOrEmpty(modal2.YSKM))
                        {
                            this.khbmBll.UpdateKHKMB_Yskm(modal2.BM, this.YSSubject);
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool AddInfoToGoodsKmTBL()
        {
            try
            {
                this.InitGoodsKmTBL();
                List<SPBMModal> list = this.spbmBll.SelectSPKMB();
                if (list == null)
                {
                    this._Loger.Error("商品数据错误：商品科目表数据为空");
                    return false;
                }
                if (0 >= list.Count)
                {
                    return false;
                }
                if (this.GoodsSubValue.Equals(DingYiZhiFuChuan.CpkmItem[0]))
                {
                    foreach (SPBMModal modal in list)
                    {
                        this.EditGoodsTbl(modal, 0);
                        this.EditGoodsTbl(modal, 1);
                        this.EditGoodsTbl(modal, 2);
                    }
                }
                else if (this.GoodsSubValue.Equals(DingYiZhiFuChuan.CpkmItem[1]))
                {
                    foreach (SPBMModal modal2 in list)
                    {
                        string xSSRKM = modal2.XSSRKM;
                        string yJZZSKM = modal2.YJZZSKM;
                        string xSTHKM = modal2.XSTHKM;
                        if (string.IsNullOrEmpty(xSSRKM))
                        {
                            this.spbmBll.UpdateSPBM_XSSRKM(modal2.BM, xSSRKM);
                        }
                        if (string.IsNullOrEmpty(yJZZSKM))
                        {
                            this.spbmBll.UpdateSPBM_YJZZSKM(modal2.BM, yJZZSKM);
                        }
                        if (string.IsNullOrEmpty(xSTHKM))
                        {
                            this.spbmBll.UpdateSPBM_XSTHKM(modal2.BM, xSTHKM);
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool AddInfoToPZTempTbl(TPZEntry_InfoModal PZEntryInfo)
        {
            try
            {
                return this.pzflbBll.AddInfoToPZTempTbl(PZEntryInfo);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        private void aisinoGrid_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            try
            {
                DataGridViewRow row = e.get_CurrentRow();
                Convert.ToString(row.Cells["FPDM"].Value);
                Convert.ToString(row.Cells["FPHM"].Value);
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

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            try
            {
                new Dictionary<string, object>().Clear();
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

        private void CellEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.customStyleDataGrid1.CurrentCell.OwningColumn.Name == DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz])
                {
                    if (((Convert.ToInt32(e.KeyChar) < 0x30) || (Convert.ToInt32(e.KeyChar) > 0x39)) && (((Convert.ToInt32(e.KeyChar) != 0x2e) && (Convert.ToInt32(e.KeyChar) != 8)) && (Convert.ToInt32(e.KeyChar) != 13)))
                    {
                        e.Handled = true;
                    }
                    else if (Convert.ToInt32(e.KeyChar) == 0x2e)
                    {
                        e.Handled = true;
                    }
                    if ((char.IsDigit(e.KeyChar) && (e.KeyChar >= 0xff10)) && (e.KeyChar <= 0xff19))
                    {
                        e.KeyChar = (char) (e.KeyChar - 0xfee0);
                        e.Handled = false;
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

        private bool CheckBMProperty()
        {
            bool flag = false;
            IKMProperty property = new KMProperty();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            try
            {
                DingYiZhiFuChuan.GetLinkUserSuit();
                List<string> kMPropertyBM = property.GetKMPropertyBM();
                if ((kMPropertyBM == null) || (kMPropertyBM.Count < 1))
                {
                    return true;
                }
                foreach (string str in kMPropertyBM)
                {
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink))
                    {
                        return false;
                    }
                    string str2 = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid))
                    {
                        return false;
                    }
                    string str3 = DingYiZhiFuChuan.A6SuitGuid;
                    str3 = str3.Substring(0, str3.IndexOf("="));
                    object obj2 = new object();
                    string str4 = "getAcctProperties";
                    string[] strArray = new string[] { str3, str };
                    string str5 = (string) WebServiceFactory.InvokeWebService(str2, str4, strArray);
                    obj2 = null;
                    if ((str5.Length > 0) && !string.IsNullOrEmpty(str5))
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(str5);
                        if (document != null)
                        {
                            if (document.DocumentElement["msg"] != null)
                            {
                                property.Delete(str);
                                return false;
                            }
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("BM", str);
                            XmlElement element = document.DocumentElement["iContact"];
                            if (element != null)
                            {
                                item.Add("iContact", element.InnerText.Trim());
                            }
                            else
                            {
                                item.Add("iContact", string.Empty);
                            }
                            element = document.DocumentElement["uomName"];
                            if (element != null)
                            {
                                item.Add("uomName", element.InnerText.Trim());
                            }
                            else
                            {
                                item.Add("uomName", string.Empty);
                            }
                            element = document.DocumentElement["acctItem"];
                            if ((element != null) && (element["cSupGUID"] != null))
                            {
                                item.Add("cSupGUID", "1");
                            }
                            else
                            {
                                item.Add("cSupGUID", string.Empty);
                            }
                            if ((element != null) && (element["cCustGUID"] != null))
                            {
                                item.Add("cCustGUID", "1");
                            }
                            else
                            {
                                item.Add("cCustGUID", string.Empty);
                            }
                            if ((element != null) && (element["cMateGUID"] != null))
                            {
                                item.Add("cMateGUID", "1");
                            }
                            else
                            {
                                item.Add("cMateGUID", string.Empty);
                            }
                            if ((element != null) && (element["cDeptGUID"] != null))
                            {
                                item.Add("cDeptGUID", "1");
                            }
                            else
                            {
                                item.Add("cDeptGUID", string.Empty);
                            }
                            if ((element != null) && (element["cEmpGUID"] != null))
                            {
                                item.Add("cEmpGUID", "1");
                            }
                            else
                            {
                                item.Add("cEmpGUID", string.Empty);
                            }
                            list.Add(item);
                        }
                    }
                }
                flag = property.ReplaceRecords(list);
                this.dtKM = property.GetKMInfo();
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
                flag = false;
            }
            return flag;
        }

        private bool checkKmCode(string kmcode, string jldw)
        {
            bool flag = false;
            try
            {
                if (string.IsNullOrEmpty(kmcode) || string.IsNullOrEmpty(jldw))
                {
                    return false;
                }
                IKMProperty property = new KMProperty();
                this.isA6NumHs = false;
                this.isA6CustHs = false;
                this.isA6GoodsHs = false;
                XmlDocument kMXmlStr = this.GetKMXmlStr(this.dtKM, kmcode);
                if (kMXmlStr == null)
                {
                    flag = true;
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink))
                    {
                        return false;
                    }
                    string str = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid))
                    {
                        return false;
                    }
                    Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("科目:" + kmcode + " 计量单位：" + jldw + " 开始检验是否数量核算......", this._Loger);
                    string str2 = DingYiZhiFuChuan.A6SuitGuid;
                    str2 = str2.Substring(0, str2.IndexOf("="));
                    object obj2 = new object();
                    string str3 = "getAcctProperties";
                    string[] strArray = new string[] { str2, kmcode };
                    string str4 = (string) WebServiceFactory.InvokeWebService(str, str3, strArray);
                    obj2 = null;
                    if (str4.Length <= 0)
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(str4))
                    {
                        return false;
                    }
                    kMXmlStr = new XmlDocument();
                    kMXmlStr.LoadXml(str4);
                }
                if (!kMXmlStr.DocumentElement.Name.Equals("root"))
                {
                    return false;
                }
                if (kMXmlStr.DocumentElement["msg"] != null)
                {
                    return false;
                }
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("BM", kmcode.Trim());
                XmlElement element2 = kMXmlStr.DocumentElement["iContact"];
                if (element2 != null)
                {
                    if (element2.InnerText.Equals("1"))
                    {
                        this.bIsWLYW = true;
                    }
                    dict.Add("iContact", element2.InnerText.Trim());
                }
                else
                {
                    dict.Add("iContact", string.Empty);
                }
                XmlElement element3 = kMXmlStr.DocumentElement["uomName"];
                if (element3 != null)
                {
                    string innerText = element3.InnerText;
                    if (!string.IsNullOrEmpty(innerText))
                    {
                        this.isA6NumHs = true;
                    }
                    dict.Add("uomName", innerText);
                }
                else
                {
                    dict.Add("uomName", string.Empty);
                }
                element3 = kMXmlStr.DocumentElement["acctItem"];
                if ((element3 != null) && element3.HasChildNodes)
                {
                    if (element3["cCustGUID"] != null)
                    {
                        this.isA6CustHs = true;
                        dict.Add("cCustGUID", element3["cCustGUID"].InnerText);
                    }
                    else
                    {
                        dict.Add("cCustGUID", string.Empty);
                    }
                    if (element3["cMateGUID"] != null)
                    {
                        this.isA6GoodsHs = true;
                        dict.Add("cMateGUID", element3["cMateGUID"].InnerText);
                    }
                    else
                    {
                        dict.Add("cMateGUID", string.Empty);
                    }
                    if (element3["cSupGUID"] != null)
                    {
                        dict.Add("cSupGUID", string.Empty);
                    }
                    else
                    {
                        dict.Add("cSupGUID", string.Empty);
                    }
                    if (element3["cDeptGUID"] != null)
                    {
                        dict.Add("cDeptGUID", string.Empty);
                    }
                    else
                    {
                        dict.Add("cDeptGUID", string.Empty);
                    }
                    if (element3["cEmpGUID"] != null)
                    {
                        dict.Add("cEmpGUID", string.Empty);
                    }
                    else
                    {
                        dict.Add("cEmpGUID", string.Empty);
                    }
                    if (flag)
                    {
                        property.ReplaceRecord(dict);
                    }
                }
                element3 = null;
                kMXmlStr = null;
            }
            catch (BaseException exception)
            {
                Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("Errinfo:" + exception.Message, this._Loger);
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("Errinfo:" + exception2.Message, this._Loger);
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void customStyleDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell currentCell = this.customStyleDataGrid1.CurrentCell;
                if (currentCell != null)
                {
                    DataGridViewColumn owningColumn = currentCell.OwningColumn;
                    if ((owningColumn != null) && ("XZBZ" == owningColumn.DataPropertyName))
                    {
                        string s = currentCell.Value.ToString();
                        if (!s.Length.Equals(0))
                        {
                            int result = 0;
                            int.TryParse(s, out result);
                            if (result.Equals(0))
                            {
                                currentCell.Value = string.Empty;
                            }
                            else
                            {
                                string str2 = result.ToString();
                                if (!s.Equals(str2))
                                {
                                    currentCell.Value = str2;
                                }
                            }
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

        private void customStyleDataGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                this.CellEdit = (DataGridViewTextBoxEditingControl) e.Control;
                this.CellEdit.SelectAll();
                this.CellEdit.KeyPress += new KeyPressEventHandler(this.CellEdit_KeyPress);
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

        private bool EditCusrTbl(KHBMModal khkmbModal)
        {
            try
            {
                if (this.ContrlSubValue.Equals(DingYiZhiFuChuan.YskmItem[2]))
                {
                    return true;
                }
                string sJBM = khkmbModal.SJBM;
                string bM = khkmbModal.BM;
                bool flag = true;
                while (flag && !string.IsNullOrEmpty(sJBM))
                {
                    List<KHBMModal> list = this.khbmBll.SelectKHBM_BM(sJBM);
                    if ((list == null) || (0 >= list.Count))
                    {
                        break;
                    }
                    KHBMModal modal = list[0];
                    string ySKM = modal.YSKM;
                    int length = modal.SJBM.Length;
                    sJBM = modal.SJBM.Trim();
                    if (!string.IsNullOrEmpty(ySKM))
                    {
                        this.khbmBll.UpdateKHKMB_Yskm(bM, ySKM);
                        flag = false;
                    }
                    if (length <= 0)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    this.khbmBll.UpdateKHKMB_Yskm(bM, this.YSSubject);
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool EditGoodsTbl(SPBMModal spkmbModal, int flag)
        {
            try
            {
                string sJBM = spkmbModal.SJBM;
                string bM = spkmbModal.BM;
                bool flag2 = true;
                bool flag3 = true;
                bool flag4 = true;
                if (flag == 0)
                {
                    flag2 = true;
                    flag3 = false;
                    flag4 = false;
                }
                if (flag == 1)
                {
                    flag2 = false;
                    flag3 = true;
                    flag4 = false;
                }
                if (flag == 2)
                {
                    flag2 = false;
                    flag3 = false;
                    flag4 = true;
                }
                while (((flag2 || flag3) || flag4) && !string.IsNullOrEmpty(sJBM))
                {
                    List<SPBMModal> list = this.spbmBll.SelectSPBM_BM(sJBM);
                    if ((list == null) || (0 >= list.Count))
                    {
                        break;
                    }
                    SPBMModal modal = list[0];
                    int length = modal.SJBM.Length;
                    sJBM = modal.SJBM.Trim();
                    if (flag2 && (flag == 0))
                    {
                        string xSSRKM = modal.XSSRKM;
                        if (!string.IsNullOrEmpty(xSSRKM))
                        {
                            this.spbmBll.UpdateSPKMB_XSSRKM(bM, xSSRKM);
                            flag2 = false;
                        }
                        if ((length <= 0) || !flag2)
                        {
                            break;
                        }
                    }
                    if (flag3 && (flag == 1))
                    {
                        string yJZZSKM = modal.YJZZSKM;
                        if (!string.IsNullOrEmpty(yJZZSKM))
                        {
                            this.spbmBll.UpdateSPKMB_YJZZSKM(bM, yJZZSKM);
                            flag3 = false;
                        }
                        if ((length <= 0) || !flag3)
                        {
                            break;
                        }
                    }
                    if (flag4 && (flag == 2))
                    {
                        string xSTHKM = modal.XSTHKM;
                        if (!string.IsNullOrEmpty(xSTHKM))
                        {
                            this.spbmBll.UpdateSPKMB_XSTHKM(bM, xSTHKM);
                            flag4 = false;
                        }
                        if ((length <= 0) || !flag4)
                        {
                            break;
                        }
                    }
                }
                if (flag2 && (flag == 0))
                {
                    this.spbmBll.UpdateSPKMB_XSSRKM(bM, this.XSSRSubJect);
                }
                if (flag3 && (flag == 1))
                {
                    this.spbmBll.UpdateSPKMB_YJZZSKM(bM, this.YJZZSSubject);
                }
                if (flag4 && (flag == 2))
                {
                    this.spbmBll.UpdateSPKMB_XSTHKM(bM, this.XSTHSubject);
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void FaPiaoZhuanPingZheng_Closing(object sender, CancelEventArgs e)
        {
        }

        private void FlushGrid(FlushGridType type)
        {
            try
            {
                List<Fpxx> listModel = this.SelectData();
                this.InsertDataToGrid(listModel);
                int count = this.customStyleDataGrid1.Rows.Count;
                string str = string.Empty;
                switch (type)
                {
                    case FlushGridType.FirstFlushGrid:
                        str = Aisino.Fwkp.Fpzpz.Common.Tool.IsRightA6Info();
                        if (string.IsNullOrEmpty(str))
                        {
                            break;
                        }
                        Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(DingYiZhiFuChuan.strErrLinkFailTip, this._Loger);
                        this._LinkSuccess = false;
                        goto Label_007F;

                    default:
                        goto Label_007F;
                }
                this.SetCombo_PingZhengType();
                this.tool_ZhiDan.Enabled = true;
                this._LinkSuccess = true;
            Label_007F:
                if (0 < this.customStyleDataGrid1.Rows.Count)
                {
                    this.tool_FaPiao.Enabled = true;
                }
                else
                {
                    this.tool_FaPiao.Enabled = false;
                }
                if (string.IsNullOrEmpty(str) && (0 < this.customStyleDataGrid1.Rows.Count))
                {
                    this.tool_ZhiDan.Enabled = true;
                    this.tool_ShunXu.Enabled = true;
                    this.tool_QuXu.Enabled = true;
                    this.tool_QuXiao.Enabled = true;
                }
                else
                {
                    this.tool_ZhiDan.Enabled = false;
                    this.tool_ShunXu.Enabled = false;
                    this.tool_QuXu.Enabled = false;
                    this.tool_QuXiao.Enabled = false;
                }
                this.tool_Find.Enabled = true;
                this.SetGridColumnReadOnly_XZBZ();
                this.customStyleDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        public static string[] GetA6PZType(ILog loger)
        {
            try
            {
                DingYiZhiFuChuan.GetLinkUserSuit();
                string str = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                object obj2 = new object();
                string str2 = "getVoucherType";
                string str3 = DingYiZhiFuChuan.A6SuitGuid;
                if (str3.IndexOf("=") != -1)
                {
                    str3 = str3.Substring(0, str3.IndexOf("="));
                }
                string[] strArray = new string[] { str3 };
                string[] strArray2 = (string[]) WebServiceFactory.InvokeWebService(str, str2, strArray);
                int length = strArray2.Length;
                if (length <= 0)
                {
                    return null;
                }
                string[] strArray3 = new string[length];
                for (int i = 0; i < length; i++)
                {
                    string str4 = strArray2[i];
                    int index = str4.IndexOf('#');
                    int num4 = str4.Length;
                    if (-1 != index)
                    {
                        str4 = str4.Substring(0, index) + "-" + str4.Substring(index + 1, (num4 - index) - 1);
                    }
                    strArray3[i] = str4;
                }
                return strArray3;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return null;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message + MessageManager.GetMessageInfo(DingYiZhiFuChuan.strErrLinkFailTip));
                return null;
            }
        }

        private XmlDocument GetKMXmlStr(DataTable dtKMInfo, string BM)
        {
            if (((dtKMInfo != null) && (dtKMInfo.Rows.Count >= 1)) && !string.IsNullOrEmpty(BM))
            {
                new KMProperty();
                try
                {
                    DataRow[] rowArray = dtKMInfo.Select("BM='" + BM + "'");
                    if ((rowArray == null) || (rowArray.Length < 1))
                    {
                        return null;
                    }
                    XmlDocument document = new XmlDocument();
                    document.AppendChild(document.CreateXmlDeclaration("1.0", "gb2312", ""));
                    XmlNode newChild = document.CreateNode(XmlNodeType.Element, "root", "");
                    document.AppendChild(newChild);
                    if ((rowArray[0]["iContact"] != null) && !string.IsNullOrEmpty(rowArray[0]["iContact"].ToString()))
                    {
                        XmlNode node2 = document.CreateNode(XmlNodeType.Element, "iContact", "");
                        node2.InnerText = rowArray[0]["iContact"].ToString();
                        newChild.AppendChild(node2);
                    }
                    if ((rowArray[0]["uomName"] != null) && !string.IsNullOrEmpty(rowArray[0]["uomName"].ToString()))
                    {
                        XmlNode node3 = document.CreateNode(XmlNodeType.Element, "uomName", "");
                        node3.InnerText = rowArray[0]["uomName"].ToString();
                        newChild.AppendChild(node3);
                    }
                    XmlNode node4 = document.CreateNode(XmlNodeType.Element, "acctItem", "");
                    newChild.AppendChild(node4);
                    if ((rowArray[0]["cSupGUID"] != null) && !string.IsNullOrEmpty(rowArray[0]["cSupGUID"].ToString()))
                    {
                        XmlNode node5 = document.CreateNode(XmlNodeType.Element, "cSupGUID", "");
                        node5.InnerText = rowArray[0]["cSupGUID"].ToString();
                        node4.AppendChild(node5);
                    }
                    if ((rowArray[0]["cCustGUID"] != null) && !string.IsNullOrEmpty(rowArray[0]["cCustGUID"].ToString()))
                    {
                        XmlNode node6 = document.CreateNode(XmlNodeType.Element, "cCustGUID", "");
                        node6.InnerText = rowArray[0]["cCustGUID"].ToString();
                        node4.AppendChild(node6);
                    }
                    if ((rowArray[0]["cMateGUID"] != null) && !string.IsNullOrEmpty(rowArray[0]["cMateGUID"].ToString()))
                    {
                        XmlNode node7 = document.CreateNode(XmlNodeType.Element, "cMateGUID", "");
                        node7.InnerText = rowArray[0]["cMateGUID"].ToString();
                        node4.AppendChild(node7);
                    }
                    if ((rowArray[0]["cDeptGUID"] != null) && !string.IsNullOrEmpty(rowArray[0]["cDeptGUID"].ToString()))
                    {
                        XmlNode node8 = document.CreateNode(XmlNodeType.Element, "cDeptGUID", "");
                        node8.InnerText = rowArray[0]["cDeptGUID"].ToString();
                        node4.AppendChild(node8);
                    }
                    if ((rowArray[0]["cEmpGUID"] != null) && !string.IsNullOrEmpty(rowArray[0]["cEmpGUID"].ToString()))
                    {
                        XmlNode node9 = document.CreateNode(XmlNodeType.Element, "cEmpGUID", "");
                        node9.InnerText = rowArray[0]["cEmpGUID"].ToString();
                        node4.AppendChild(node9);
                    }
                    return document;
                }
                catch (Exception exception)
                {
                    this._Loger.Error(exception.ToString());
                }
            }
            return null;
        }

        private void GetPropertyUtil()
        {
            try
            {
                this.ContrlSubValue = PropertyUtil.GetValue(DingYiZhiFuChuan.YskmItemValue);
                this.YSSubject = PropertyUtil.GetValue(DingYiZhiFuChuan.Ysrkm);
                this.GoodsSubValue = PropertyUtil.GetValue(DingYiZhiFuChuan.CpkmItemValue);
                this.XSTHSubject = PropertyUtil.GetValue(DingYiZhiFuChuan.Xsthkm);
                this.YJZZSSubject = PropertyUtil.GetValue(DingYiZhiFuChuan.Yjzzskm);
                this.XSSRSubJect = PropertyUtil.GetValue(DingYiZhiFuChuan.Xssrkm);
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

        private string GetWLYWH(string fpdm, string fphm, string fpzl)
        {
            try
            {
                DataRow[] rowArray = this.dtWLYWH.Select("FPDM='" + fpdm + "' and FPHM='" + fphm + "' and  fpzl='" + fpzl + "'");
                if ((rowArray == null) || (rowArray.Length < 1))
                {
                    return string.Empty;
                }
                return ((rowArray[0]["WLYWH"] == null) ? string.Empty : rowArray[0]["WLYWH"].ToString());
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            return string.Empty;
        }

        private bool InitCusrKmTBL()
        {
            try
            {
                if (this.listBuyerInfoList == null)
                {
                    this._Loger.Error("购方数据错误：购方数据为空");
                    return false;
                }
                if (0 > this.listBuyerInfoList.Count)
                {
                    this._Loger.Error("购方数据错误：购方数据为空");
                    return false;
                }
                if (this.listBuyerInfoList.Count > 0)
                {
                    for (int i = 0; i < this.listBuyerInfoList.Count; i++)
                    {
                        string str = this.listBuyerInfoList[i].Trim();
                        int index = str.IndexOf(DingYiZhiFuChuan.strFenGeFu);
                        string strMc = str.Substring(0, index).Trim();
                        string strBm = str.Substring(index + 1, (str.Length - index) - 1).Trim();
                        List<KHBMModal> list = this.khbmBll.SelectKHBM_MC_BM(strMc, strBm);
                        if (list == null)
                        {
                            this._Loger.Error("销项发票数据错误购方名称购房编号应和客户编码表里的数据一样：购方名称:" + strMc + "购房编号:" + strBm);
                        }
                        else if (0 >= list.Count)
                        {
                            this._Loger.Error("销项发票数据错误购方名称购房编号应和客户编码表里的数据一样：购方名称:" + strMc + "购房编号:" + strBm);
                        }
                        else
                        {
                            foreach (KHBMModal modal in list)
                            {
                                KHBMModal khbmModal = new KHBMModal {
                                    BM = modal.BM
                                };
                                if (this.ContrlSubValue.Equals(DingYiZhiFuChuan.YskmItem[2]))
                                {
                                    string dQKM = modal.DQKM;
                                    if (string.IsNullOrEmpty(dQKM))
                                    {
                                        khbmModal.YSKM = this.YSSubject.Trim();
                                    }
                                    else
                                    {
                                        khbmModal.YSKM = dQKM.Trim();
                                    }
                                }
                                else
                                {
                                    khbmModal.YSKM = modal.YSKM;
                                }
                                khbmModal.SJBM = modal.SJBM;
                                this.khbmBll.AddInfoToCusrKmTempTbl(khbmModal);
                            }
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool InitGoodsKmTBL()
        {
            try
            {
                if (this.listGoodsInfoList == null)
                {
                    this._Loger.Error("商品数据错误：商品数据为空");
                    return false;
                }
                if (0 >= this.listGoodsInfoList.Count)
                {
                    this._Loger.Error("商品数据错误：商品数据为空");
                    return false;
                }
                int num = 0;
                for (num = 0; num < this.listGoodsInfoList.Count; num++)
                {
                    string str = this.listGoodsInfoList[num];
                    int index = str.IndexOf(DingYiZhiFuChuan.strFenGeFu);
                    string strMc = str.Substring(0, index).Trim();
                    string strBm = str.Substring(index + 1, (str.Length - index) - 1);
                    List<SPBMModal> list = this.spbmBll.SelectSPBM_MC_BM(strMc, strBm);
                    if (list == null)
                    {
                        this._Loger.Error("商品数据错误商品名称商品编号应和商品编码表里的数据一样：商品名称:" + strMc + "商品编号:" + strBm);
                    }
                    else if (0 >= list.Count)
                    {
                        this._Loger.Error("商品数据错误商品名称商品编号应和商品编码表里的数据一样：商品名称:" + strMc + "商品编号:" + strBm);
                    }
                    else
                    {
                        foreach (SPBMModal modal in list)
                        {
                            SPBMModal spbmModal = new SPBMModal {
                                BM = modal.BM,
                                XSSRKM = modal.XSSRKM,
                                YJZZSKM = modal.YJZZSKM,
                                XSTHKM = modal.XSTHKM,
                                SJBM = modal.SJBM
                            };
                            this.spbmBll.AddInfoToGoodsKmTempTbl(spbmModal);
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.panneltop = this.xmlComponentLoader1.GetControlByName<Panel>("panneltop");
            this.panneltop.Dock = DockStyle.Top;
            this.panneltop.Size = new Size(0x2db, 0x23);
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<Panel>("panel1");
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Size = new Size(0x2db, 0x1d);
            this.tool_Quit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Quit");
            this.tool_Find = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Find");
            this.tool_DaYin = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DaYin");
            this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
            this.tool_ShunXu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ShunXu");
            this.tool_QuXu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_QuXu");
            this.tool_QuXiao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_QuXiao");
            this.tool_ZhiDan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ZhiDan");
            this.tool_FaPiao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_FaPiao");
            this.combo_PingZhengType = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_PingZhengType");
            this.dateTime_ZhiDanRiQi = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dateTime_ZhiDanRiQi");
            this.customStyleDataGrid1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.customStyleDataGrid1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.customStyleDataGrid1_EditingControlShowing);
            this.customStyleDataGrid1.CellValueChanged += new DataGridViewCellEventHandler(this.customStyleDataGrid1_CellValueChanged);
            this.tool_Quit.Click += new EventHandler(this.tool_Quit_Click);
            this.tool_Find.Click += new EventHandler(this.tool_Find_Click);
            this.tool_DaYin.Click += new EventHandler(this.tool_DaYin_Click);
            this.tool_GeShi.Click += new EventHandler(this.tool_GeShi_Click);
            this.tool_ShunXu.Click += new EventHandler(this.tool_ShunXu_Click);
            this.tool_QuXu.Click += new EventHandler(this.tool_QuXu_Click);
            this.tool_QuXiao.Click += new EventHandler(this.tool_QuXiao_Click);
            this.tool_ZhiDan.Click += new EventHandler(this.tool_ZhiDan_Click);
            this.tool_FaPiao.Click += new EventHandler(this.tool_FaPiao_Click);
            this.combo_PingZhengType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.combo_PingZhengType.Items.Clear();
            this.tool_ShunXu.Text = "顺序选择";
            this.tool_QuXu.Text = "全部选择";
            this.tool_QuXiao.Text = "取消选择";
            base.Closing += new CancelEventHandler(this.FaPiaoZhuanPingZheng_Closing);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2db, 0x1ba);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.FaPiaoZhuanPingZheng\Aisino.Fwkp.Fpzpz.Form.FaPiaoZhuanPingZheng.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2db, 0x1ba);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FaPiaoZhuanPingZheng";
            base.StartPosition = FormStartPosition.CenterParent;
            base.set_TabText("发票转凭证");
            this.Text = "发票转凭证";
            base.ResumeLayout(false);
        }

        private void InitializeFaPiaoFindTiaoJian()
        {
            try
            {
                this._FaPiaoFindTiaoJian.Data_ksrqValue = new DateTime(this._CardClock.Year, this._CardClock.Month, 1);
                this._FaPiaoFindTiaoJian.Data_jsrqValue = new DateTime(this._CardClock.Year, this._CardClock.Month, DateTime.DaysInMonth(this._CardClock.Year, this._CardClock.Month));
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

        private bool InitMakePZ()
        {
            try
            {
                this.xxfpBll.EmptyTempTable();
                this.AddInfoToCusrKmTBL();
                this.AddInfoToGoodsKmTBL();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void InitWLYWH()
        {
            try
            {
                this.dtWLYWH.Rows.Clear();
                if (!this.dtWLYWH.Columns.Contains("FPDM"))
                {
                    this.dtWLYWH.Columns.Add("FPDM", typeof(string));
                }
                if (!this.dtWLYWH.Columns.Contains("FPHM"))
                {
                    this.dtWLYWH.Columns.Add("FPHM", typeof(string));
                }
                if (!this.dtWLYWH.Columns.Contains("SelectGroupID"))
                {
                    this.dtWLYWH.Columns.Add("SelectGroupID", typeof(int));
                }
                if (!this.dtWLYWH.Columns.Contains("FPZL"))
                {
                    this.dtWLYWH.Columns.Add("FPZL", typeof(string));
                }
                if (!this.dtWLYWH.Columns.Contains("WLYWH"))
                {
                    this.dtWLYWH.Columns.Add("WLYWH", typeof(string));
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
        }

        private void InsertDataToGrid(List<Fpxx> ListModel)
        {
            try
            {
                if (this.customStyleDataGrid1.DataSource != null)
                {
                    ((DataTable) this.customStyleDataGrid1.DataSource).Clear();
                }
                if ((ListModel != null) && (ListModel.Count > 0))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfsh], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexKprq], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjje], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjse], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexJshj], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz], typeof(string));
                    foreach (Fpxx fpxx in ListModel)
                    {
                        DataRow row = table.NewRow();
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]] = string.Empty;
                        string str = string.Empty;
                        if (fpxx.fplx.Equals((FPLX) 2))
                        {
                            str = DingYiZhiFuChuan.strFPZL[1];
                        }
                        else if (fpxx.fplx.Equals((FPLX) 0))
                        {
                            str = DingYiZhiFuChuan.strFPZL[0];
                        }
                        else if (fpxx.fplx.Equals((FPLX) 11))
                        {
                            str = DingYiZhiFuChuan.strFPZL[3];
                        }
                        else if (fpxx.fplx.Equals((FPLX) 12))
                        {
                            str = DingYiZhiFuChuan.strFPZL[2];
                        }
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl]] = str;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm]] = fpxx.fpdm;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm]] = fpxx.fphm;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc]] = fpxx.gfmc;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfsh]] = fpxx.gfsh;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexKprq]] = fpxx.kprq;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjje]] = fpxx.je;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjse]] = fpxx.se;
                        string jshj = Aisino.Fwkp.Fpzpz.Common.Tool.GetJshj(fpxx, this._Loger);
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexJshj]] = jshj;
                        row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm]] = fpxx.gfbh;
                        if (fpxx.zfbz)
                        {
                            row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz]] = "是";
                        }
                        else
                        {
                            row[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz]] = "否";
                        }
                        table.Rows.Add(row);
                    }
                    this.customStyleDataGrid1.DataSource = table;
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

        private void InsertGridColumn()
        {
            try
            {
                int num = 0x4b;
                int num2 = 100;
                this.Column_Xzbz = new DataGridViewTextBoxColumn();
                this.Column_Xzbz.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz];
                this.Column_Xzbz.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexXzbz];
                this.Column_Xzbz.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz];
                this.Column_Xzbz.ReadOnly = true;
                this.Column_Xzbz.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexXzbz];
                this.Column_Xzbz.MaxInputLength = 10;
                this.Column_Xzbz.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.Column_Xzbz.Width = num;
                this.Column_Fpzl = new DataGridViewTextBoxColumn();
                this.Column_Fpzl.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl];
                this.Column_Fpzl.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFpzl];
                this.Column_Fpzl.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl];
                this.Column_Fpzl.ReadOnly = true;
                this.Column_Fpzl.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFpzl];
                this.Column_Fpzl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Fpzl.Width = num;
                this.Column_Fpdm = new DataGridViewTextBoxColumn();
                this.Column_Fpdm.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm];
                this.Column_Fpdm.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFpdm];
                this.Column_Fpdm.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm];
                this.Column_Fpdm.ReadOnly = true;
                this.Column_Fpdm.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFpdm];
                this.Column_Fpdm.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Fpdm.Width = num;
                this.Column_Fphm = new DataGridViewTextBoxColumn();
                this.Column_Fphm.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm];
                this.Column_Fphm.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFphm];
                this.Column_Fphm.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm];
                this.Column_Fphm.ReadOnly = true;
                this.Column_Fphm.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexFphm];
                this.Column_Fphm.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.Column_Fphm.Width = num;
                this.Column_Gfmc = new DataGridViewTextBoxColumn();
                this.Column_Gfmc.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc];
                this.Column_Gfmc.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfmc];
                this.Column_Gfmc.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc];
                this.Column_Gfmc.ReadOnly = true;
                this.Column_Gfmc.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfmc];
                this.Column_Gfmc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Gfmc.Width = num2;
                this.Column_Gfsh = new DataGridViewTextBoxColumn();
                this.Column_Gfsh.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfsh];
                this.Column_Gfsh.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfsh];
                this.Column_Gfsh.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfsh];
                this.Column_Gfsh.ReadOnly = true;
                this.Column_Gfsh.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfsh];
                this.Column_Gfsh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Gfsh.Width = num2;
                this.Column_Kprq = new DataGridViewTextBoxColumn();
                this.Column_Kprq.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexKprq];
                this.Column_Kprq.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexKprq];
                this.Column_Kprq.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexKprq];
                this.Column_Kprq.ReadOnly = true;
                this.Column_Kprq.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexKprq];
                this.Column_Kprq.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Kprq.Width = num;
                this.Column_Hjje = new DataGridViewTextBoxColumn();
                this.Column_Hjje.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjje];
                this.Column_Hjje.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexHjje];
                this.Column_Hjje.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjje];
                this.Column_Hjje.ReadOnly = true;
                this.Column_Hjje.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexHjje];
                this.Column_Hjje.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.Column_Hjje.Width = num;
                this.Column_Hjse = new DataGridViewTextBoxColumn();
                this.Column_Hjse.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjse];
                this.Column_Hjse.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexHjse];
                this.Column_Hjse.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjse];
                this.Column_Hjse.ReadOnly = true;
                this.Column_Hjse.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexHjse];
                this.Column_Hjse.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.Column_Hjse.Width = num;
                this.Column_Jshj = new DataGridViewTextBoxColumn();
                this.Column_Jshj.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexJshj];
                this.Column_Jshj.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexJshj];
                this.Column_Jshj.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexJshj];
                this.Column_Jshj.ReadOnly = true;
                this.Column_Jshj.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexJshj];
                this.Column_Jshj.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.Column_Jshj.Width = num;
                this.Column_Gfbh = new DataGridViewTextBoxColumn();
                this.Column_Gfbh.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm];
                this.Column_Gfbh.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfbm];
                this.Column_Gfbh.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm];
                this.Column_Gfbh.ReadOnly = true;
                this.Column_Gfbh.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexGfbm];
                this.Column_Gfbh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Gfbh.Visible = false;
                this.Column_Gfbh.Width = num;
                this.Column_Zfbz = new DataGridViewTextBoxColumn();
                this.Column_Zfbz.DataPropertyName = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz];
                this.Column_Zfbz.HeaderText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexZfbz];
                this.Column_Zfbz.Name = DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz];
                this.Column_Zfbz.ReadOnly = true;
                this.Column_Zfbz.ToolTipText = DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexZfbz];
                this.Column_Zfbz.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.Column_Zfbz.Visible = false;
                this.Column_Zfbz.Width = num;
                this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.customStyleDataGrid1.MultiSelect = false;
                this.customStyleDataGrid1.ColumnAdd(this.Column_Xzbz);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Fpzl);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Fpdm);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Fphm);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Gfmc);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Gfsh);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Kprq);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Hjje);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Hjse);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Jshj);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Gfbh);
                this.customStyleDataGrid1.ColumnAdd(this.Column_Zfbz);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz], false);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfsh], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexKprq], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjje], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexHjse], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexJshj], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm], true);
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz], true);
                this.customStyleDataGrid1.AllowUserToAddRows = false;
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

        private void InsertGridColumnFenYeGrid()
        {
            try
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "选择标志");
                item.Add("Property", "XZBZ");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票种类");
                item.Add("Property", "FPZL");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票发票代码");
                item.Add("Property", "FPDM");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票号码");
                item.Add("Property", "FPHM");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方名称");
                item.Add("Property", "GFMC");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方税号");
                item.Add("Property", "GFSH");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票日期");
                item.Add("Property", "KPRQ");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计金额");
                item.Add("Property", "HJJE");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计税额");
                item.Add("Property", "HJSE");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "价税合计");
                item.Add("Property", "JSHJ");
                item.Add("Width", "100");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                list.Add(item);
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

        private bool InsertWLYWH(string fpdm, string fphm, string fpzl, int SelectGroupID, string wlywh)
        {
            try
            {
                DataRow[] rowArray = this.dtWLYWH.Select("FPDM='" + fpdm + "' and FPHM='" + fphm + "' and  fpzl='" + fpzl + "'");
                if ((rowArray == null) || (rowArray.Length < 1))
                {
                    int result = 0;
                    int.TryParse(fphm, out result);
                    string str = string.Empty;
                    if (fpzl == "PTFP")
                    {
                        str = "c";
                    }
                    else if (fpzl == "ZYFP")
                    {
                        str = "s";
                    }
                    else
                    {
                        str = fpzl;
                    }
                    DataRow row = this.dtWLYWH.NewRow();
                    row["FPDM"] = fpdm;
                    row["FPHM"] = result;
                    row["FPZL"] = str;
                    row["SelectGroupID"] = SelectGroupID;
                    row["WLYWH"] = wlywh;
                    this.dtWLYWH.Rows.Add(row);
                }
                else
                {
                    rowArray[0]["SelectGroupID"] = SelectGroupID;
                    rowArray[0]["WLYWH"] = wlywh;
                }
                this.dtWLYWH.AcceptChanges();
                return true;
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        private string MakeA6EntryXML(ArrayList arrayList, int EntryNo, int IsNSKSub, int hsBz)
        {
            try
            {
                if (arrayList == null)
                {
                    return string.Empty;
                }
                if (0 >= arrayList.Count)
                {
                    return string.Empty;
                }
                int index = 0;
                string str = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, "借贷方向相同，金额正负相反");
                index = 0;
                while (index < DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItem.Length)
                {
                    if (str.Equals(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItem[index]))
                    {
                        break;
                    }
                    index++;
                }
                if ((index < 0) || (index > 1))
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("A6负数发票处理方式 存储的值有误", this._Loger);
                    return string.Empty;
                }
                bool flag = false;
                string str2 = string.Empty;
                bool flag2 = false;
                for (int i = 0; i < arrayList.Count; i++)
                {
                    Dictionary<string, object> dictionary = arrayList[i] as Dictionary<string, object>;
                    string str3 = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[4]].ToString();
                    string s = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[3]].ToString();
                    double result = 0.0;
                    double.TryParse(s, out result);
                    if (Math.Abs(result) >= 1E-08)
                    {
                        flag = false;
                        if (result < 0.0)
                        {
                            s = result.ToString();
                            if (index.Equals(1))
                            {
                                s = Math.Abs(result).ToString();
                                flag = true;
                            }
                        }
                        if ((s.Length - s.IndexOf(".")) > 6)
                        {
                            object[] objArray = new object[] { s, s.IndexOf(".") + 3 };
                            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray);
                            if ((objArray2 != null) && (objArray2.Length > 0))
                            {
                                s = objArray2[0].ToString();
                            }
                        }
                        int num4 = 0;
                        int.TryParse(dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[5]].ToString(), out num4);
                        string str6 = string.Empty;
                        if ((IsNSKSub.Equals(0) || IsNSKSub.Equals(1)) || hsBz.Equals(4))
                        {
                            str6 = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[2]].ToString();
                        }
                        else
                        {
                            str6 = string.Empty;
                        }
                        string str7 = string.Empty;
                        if ((hsBz > 0) && !hsBz.Equals(4))
                        {
                            str7 = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[6]].ToString();
                        }
                        str2 = ((((str2 + "<entry>" + "<entry_id>") + EntryNo.ToString() + "</entry_id>") + "<account_code>" + str3.Trim()) + "</account_code>" + "<abstract>发票</abstract>") + "<currency/>" + "<exchange_rate2/>";
                        string wlywh = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[1]].ToString();
                        string fpzl = wlywh.Substring(0, 1);
                        string fpdm = wlywh.Substring(wlywh.IndexOf(",") + 1, (wlywh.LastIndexOf(",") - wlywh.IndexOf(",")) - 1);
                        string fphm = wlywh.Substring(wlywh.LastIndexOf(",") + 1, (wlywh.Length - wlywh.LastIndexOf(",")) - 1);
                        wlywh = wlywh.Replace(",", "");
                        wlywh = wlywh.Substring(1, wlywh.Length - 1);
                        this.UpdateWLYWH(fpdm, fphm, fpzl, wlywh);
                        string str12 = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[10]].ToString();
                        if (10 < str12.Length)
                        {
                            str12 = str12.Substring(0, 10);
                        }
                        if (num4.Equals(0))
                        {
                            if (!flag)
                            {
                                str2 = (str2 + "<natural_debit_currency>" + s.Trim()) + "</natural_debit_currency>" + "<natural_credit_currency>0</natural_credit_currency>";
                                flag2 = true;
                            }
                            else
                            {
                                str2 = (str2 + "<natural_credit_currency>" + s.Trim()) + "</natural_credit_currency>" + "<natural_debit_currency>0</natural_debit_currency>";
                                flag2 = false;
                            }
                        }
                        else if (num4.Equals(1) || num4.Equals(2))
                        {
                            if (flag)
                            {
                                str2 = (str2 + "<natural_credit_currency>0</natural_credit_currency>" + "<natural_debit_currency>") + s.Trim() + "</natural_debit_currency>";
                                flag2 = true;
                            }
                            else
                            {
                                str2 = (str2 + "<natural_debit_currency>0</natural_debit_currency>" + "<natural_credit_currency>") + s.Trim() + "</natural_credit_currency>";
                                flag2 = false;
                            }
                        }
                        str2 = ((str2 + "<acctcheck_ywh>" + wlywh) + "</acctcheck_ywh>" + "<acctcheck_ywrq>") + str12 + "</acctcheck_ywrq>";
                        if (this.bIsWLYW)
                        {
                            long num5 = 0L;
                            long num6 = 0L;
                            if (long.TryParse(wlywh, out num6) && (string.IsNullOrEmpty(this.sPZWLYWH) || (long.TryParse(this.sPZWLYWH, out num5) && (num6 > num5))))
                            {
                                this.sPZWLYWH = wlywh;
                            }
                        }
                        string str13 = dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[7]].ToString();
                        double num7 = 0.0;
                        double.TryParse(str13, out num7);
                        if ((hsBz.Equals(2) || hsBz.Equals(3)) || (hsBz.Equals(6) || hsBz.Equals(7)))
                        {
                            string str14 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                            if (flag2)
                            {
                                if (str14.Equals("借贷方向相反，金额正负相同"))
                                {
                                    object obj2 = str2;
                                    str2 = string.Concat(new object[] { obj2, "<debit_quantity>", Math.Abs(num7), "</debit_quantity>" });
                                }
                                else
                                {
                                    str2 = str2 + "<debit_quantity>" + str13 + "</debit_quantity>";
                                }
                            }
                            else if (str14.Equals("借贷方向相反，金额正负相同"))
                            {
                                object obj3 = str2;
                                str2 = string.Concat(new object[] { obj3, "<credit_quantity>", Math.Abs(num7), "</credit_quantity>" });
                            }
                            else
                            {
                                str2 = str2 + "<credit_quantity>" + str13 + "</credit_quantity>";
                            }
                            if (Math.Abs(num7) > 0.0)
                            {
                                str2 = str2 + "<price>" + Math.Abs((double) (result / num7)).ToString() + "</price>";
                            }
                            else
                            {
                                str2 = str2 + "<price>" + dictionary[DingYiZhiFuChuan.PZFLBCulmnDataName[8]].ToString() + "</price>";
                            }
                        }
                        str2 = str2 + "<auxiliary_accounting>";
                        if (num4 == 0)
                        {
                            if (!flag)
                            {
                            }
                        }
                        else if (num4.Equals(1) || num4.Equals(2))
                        {
                            if (flag)
                            {
                                str2 = str2 + "<item name='cCustGUID'></item>";
                            }
                            else
                            {
                                str2 = str2 + "<item name='cCustGUID'></item>";
                            }
                        }
                        str2 = str2 + "<item name='cCustCode'>" + str6.Trim() + "</item>";
                        if ((hsBz.Equals(1) || hsBz.Equals(3)) || (hsBz.Equals(5) || hsBz.Equals(7)))
                        {
                            str2 = str2 + "<item name='cMatCode'>" + str7.Trim() + "</item>";
                        }
                        str2 = str2 + "</auxiliary_accounting>" + "</entry>";
                        EntryNo++;
                    }
                }
                return str2;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        private bool MakeA6PZXML(string PzNo, out bool bHasShown)
        {
            bHasShown = false;
            try
            {
                string[] strArray = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                string[] strArray2 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                string[] strArray3 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                string str = string.Empty;
                int num = 8;
                DingYiZhiFuChuan.GetLinkUserSuit();
                if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink))
                {
                    MessageManager.ShowMsgBox(DingYiZhiFuChuan.ServerEmpty);
                    return false;
                }
                string str2 = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid))
                {
                    MessageManager.ShowMsgBox(DingYiZhiFuChuan.UserEmpty);
                    return false;
                }
                string xml = "<?xml version='1.0' encoding='gb2312' ?>";
                List<TPZEntry_InfoModal> list = this.pzflbBll.SelectPZFLB_ZH();
                xml = xml + "<PzInfo>";
                if (list != null)
                {
                    if (0 >= list.Count)
                    {
                        MessageHelper.MsgWait();
                        return false;
                    }
                    string str4 = PropertyUtil.GetValue(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItemValue);
                    string str5 = PropertyUtil.GetValue(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItemValue);
                    string str6 = PropertyUtil.GetValue(DingYiZhiFuChuan.Xtsp_ZdsItemValue);
                    string str7 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue);
                    if ((string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(str5)) || (string.IsNullOrEmpty(str6) || string.IsNullOrEmpty(str7)))
                    {
                        if (MessageManager.ShowMsgBox("FPZPZ-000022") != DialogResult.OK)
                        {
                            MessageHelper.MsgWait();
                            Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("未设置制单方式，用户已放弃使用默认制单方式", this._Loger);
                            return false;
                        }
                        str4 = "明细到客户";
                        str5 = "明细到客户";
                        str6 = "单价不同不合并";
                        str7 = "借贷方向相同，金额正负相反";
                    }
                    MessageHelper.MsgWait("正在制作凭证信息，请稍等......");
                    int index = 0;
                    index = 0;
                    while (index < DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItem.Length)
                    {
                        if (str4.Equals(DingYiZhiFuChuan.NSKSubValue_Qtkm_ZdfsItem[index]))
                        {
                            break;
                        }
                        index++;
                    }
                    if ((index < 0) || (index > 2))
                    {
                        MessageHelper.MsgWait();
                        Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("非受控科目制单方式-其他科目制单方式 存储的值有误", this._Loger);
                        return false;
                    }
                    int num3 = 0;
                    num3 = 0;
                    while (num3 < DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItem.Length)
                    {
                        if (str5.Equals(DingYiZhiFuChuan.SKSubValue_Yskm_ZdfsItem[num3]))
                        {
                            break;
                        }
                        num3++;
                    }
                    if ((num3 < 0) || (num3 > 1))
                    {
                        MessageHelper.MsgWait();
                        Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("受控科目制单方式：  0明细到客户、1明细到单据 存储的值有误", this._Loger);
                        return false;
                    }
                    int num4 = 0;
                    num4 = 0;
                    while (num4 < DingYiZhiFuChuan.Xtsp_ZdsItem.Length)
                    {
                        if (str6.Equals(DingYiZhiFuChuan.Xtsp_ZdsItem[num4]))
                        {
                            break;
                        }
                        num4++;
                    }
                    if ((num4 < 0) || (num4 > 1))
                    {
                        MessageHelper.MsgWait();
                        Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("A6相同商品制单时 存储的值有误", this._Loger);
                        return false;
                    }
                    string str8 = DingYiZhiFuChuan.A6SuitGuid;
                    string str9 = DingYiZhiFuChuan.A6UserGuid;
                    str8 = str8.Substring(0, str8.IndexOf("="));
                    str9 = str9.Substring(0, str9.IndexOf("="));
                    string str10 = this.dateTime_ZhiDanRiQi.Value.ToString(DingYiZhiFuChuan.strYear_Month_Day_Formart);
                    for (int i = 0; i < list.Count; i++)
                    {
                        ArrayList list2;
                        string[] strArray6;
                        TPZEntry_InfoModal modal = list[i];
                        string strZH = modal.PZEntry_Group.ToString();
                        switch (index)
                        {
                            case 0:
                                str = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                str = ((str + " FROM PZFLB ") + " WHERE ZH=" + strZH) + " And JDBZ<>0 " + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                                strArray[0] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray)[0] = strArray6[0] + " FROM PZFLB ";
                                (strArray6 = strArray)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[0] = strArray6[0] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " And HSBZ=0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " group by ZH,KHBH,KM,JDBZ";
                                strArray[1] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray)[1] = strArray6[1] + " FROM PZFLB ";
                                (strArray6 = strArray)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[1] = strArray6[1] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " And HSBZ=1 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                                if (num4 != 0)
                                {
                                    break;
                                }
                                strArray[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                goto Label_05C4;

                            case 1:
                                str = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                str = ((str + " FROM PZFLB ") + " WHERE ZH=" + strZH) + " And JDBZ<>0 " + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                                strArray[0] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                                (strArray6 = strArray)[0] = strArray6[0] + " FROM PZFLB ";
                                (strArray6 = strArray)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[0] = strArray6[0] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " And HSBZ=0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                                strArray[1] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                                (strArray6 = strArray)[1] = strArray6[1] + " FROM PZFLB ";
                                (strArray6 = strArray)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[1] = strArray6[1] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " And HSBZ=1 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                                if (num4 != 0)
                                {
                                    goto Label_1565;
                                }
                                strArray[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                goto Label_15D3;

                            case 2:
                                str = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                str = ((str + " FROM PZFLB ") + " WHERE ZH=" + strZH) + " And JDBZ<>0 " + " group by ZH,KM,JDBZ,JLDW";
                                strArray[0] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                                (strArray6 = strArray)[0] = strArray6[0] + " FROM PZFLB ";
                                (strArray6 = strArray)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[0] = strArray6[0] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " And HSBZ=0 ";
                                (strArray6 = strArray)[0] = strArray6[0] + " group by ZH,KM,JDBZ";
                                strArray[1] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                                (strArray6 = strArray)[1] = strArray6[1] + " FROM PZFLB ";
                                (strArray6 = strArray)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[1] = strArray6[1] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " And HSBZ=1 ";
                                (strArray6 = strArray)[1] = strArray6[1] + " group by ZH,KM,JDBZ,SPBH";
                                if (num4 != 0)
                                {
                                    goto Label_2574;
                                }
                                strArray[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,KM,JDBZ,SPBH,JLDW,DJ";
                                goto Label_25E2;

                            default:
                                goto Label_33F3;
                        }
                        strArray[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                        (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                        (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                    Label_05C4:
                        if (num4 == 0)
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        strArray[4] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                        (strArray6 = strArray)[4] = strArray6[4] + " FROM PZFLB ";
                        (strArray6 = strArray)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[4] = strArray6[4] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " And HSBZ=4 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " group by ZH,KHBH,KM,JDBZ";
                        strArray[5] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                        (strArray6 = strArray)[5] = strArray6[5] + " FROM PZFLB ";
                        (strArray6 = strArray)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[5] = strArray6[5] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " And HSBZ=5 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                        if (num4 == 0)
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        if (num4 == 0)
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        switch (num3)
                        {
                            case 0:
                                strArray2[0] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,max(KPRQ)KPRQ,KM,JDBZ";
                                (strArray6 = strArray2)[0] = strArray6[0] + " FROM PZFLB ";
                                (strArray6 = strArray2)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[0] = strArray6[0] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[0] = strArray6[0] + " And HSBZ=0 ";
                                (strArray6 = strArray2)[0] = strArray6[0] + " group by ZH,KHBH,KM,JDBZ";
                                strArray2[1] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[1] = strArray6[1] + " FROM PZFLB ";
                                (strArray6 = strArray2)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[1] = strArray6[1] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[1] = strArray6[1] + " And HSBZ=1 ";
                                (strArray6 = strArray2)[1] = strArray6[1] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                                if (num4 == 0)
                                {
                                    strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                    (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                if (num4 == 0)
                                {
                                    strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                    (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                strArray2[4] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[4] = strArray6[4] + " FROM PZFLB ";
                                (strArray6 = strArray2)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[4] = strArray6[4] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[4] = strArray6[4] + " And HSBZ=4 ";
                                (strArray6 = strArray2)[4] = strArray6[4] + " group by ZH,KHBH,KM,JDBZ";
                                strArray2[5] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[5] = strArray6[5] + " FROM PZFLB ";
                                (strArray6 = strArray2)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[5] = strArray6[5] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[5] = strArray6[5] + " And HSBZ=5 ";
                                (strArray6 = strArray2)[5] = strArray6[5] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                                if (num4 == 0)
                                {
                                    strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                    (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                if (num4 == 0)
                                {
                                    strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                    (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                break;

                            case 1:
                                strArray3[0] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,max(KPRQ)KPRQ,KM,JDBZ";
                                (strArray6 = strArray3)[0] = strArray6[0] + " FROM PZFLB ";
                                (strArray6 = strArray3)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[0] = strArray6[0] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[0] = strArray6[0] + " And HSBZ=0 ";
                                (strArray6 = strArray3)[0] = strArray6[0] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                                strArray3[1] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[1] = strArray6[1] + " FROM PZFLB ";
                                (strArray6 = strArray3)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[1] = strArray6[1] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[1] = strArray6[1] + " And HSBZ=1 ";
                                (strArray6 = strArray3)[1] = strArray6[1] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                                if (num4 == 0)
                                {
                                    strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                    (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                if (num4 == 0)
                                {
                                    strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                    (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                strArray3[4] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[4] = strArray6[4] + " FROM PZFLB ";
                                (strArray6 = strArray3)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[4] = strArray6[4] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[4] = strArray6[4] + " And HSBZ=4 ";
                                (strArray6 = strArray3)[4] = strArray6[4] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                                strArray3[5] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[5] = strArray6[5] + " FROM PZFLB ";
                                (strArray6 = strArray3)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[5] = strArray6[5] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[5] = strArray6[5] + " And HSBZ=5 ";
                                (strArray6 = strArray3)[5] = strArray6[5] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                                if (num4 == 0)
                                {
                                    strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                    (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                if (num4 == 0)
                                {
                                    strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                                }
                                else
                                {
                                    strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                    (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                    (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                                }
                                break;
                        }
                        goto Label_33F3;
                    Label_1565:
                        strArray[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                        (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                        (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                    Label_15D3:
                        if (num4 == 0)
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        strArray[4] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                        (strArray6 = strArray)[4] = strArray6[4] + " FROM PZFLB ";
                        (strArray6 = strArray)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[4] = strArray6[4] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " And HSBZ=4 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                        strArray[5] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                        (strArray6 = strArray)[5] = strArray6[5] + " FROM PZFLB ";
                        (strArray6 = strArray)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[5] = strArray6[5] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " And HSBZ=5 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                        if (num4 == 0)
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        if (num4 == 0)
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        if (num3 == 0)
                        {
                            strArray2[0] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " FROM PZFLB ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[0] = strArray6[0] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " And HSBZ=0 ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " group by ZH,KHBH,KM,JDBZ";
                            strArray2[1] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " FROM PZFLB ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[1] = strArray6[1] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " And HSBZ=1 ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            strArray2[4] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " FROM PZFLB ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[4] = strArray6[4] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " And HSBZ=4 ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " group by ZH,KHBH,KM,JDBZ";
                            strArray2[5] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " FROM PZFLB ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[5] = strArray6[5] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " And HSBZ=5 ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                        }
                        if (num3 == 1)
                        {
                            strArray3[0] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,max(KPRQ)KPRQ,KM,JDBZ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " FROM PZFLB ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[0] = strArray6[0] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " And HSBZ=0 ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                            strArray3[1] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " FROM PZFLB ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[1] = strArray6[1] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " And HSBZ=1 ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            strArray3[4] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " FROM PZFLB ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[4] = strArray6[4] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " And HSBZ=4 ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                            strArray3[5] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " FROM PZFLB ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[5] = strArray6[5] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " And HSBZ=5 ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                        }
                        goto Label_33F3;
                    Label_2574:
                        strArray[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                        (strArray6 = strArray)[2] = strArray6[2] + " FROM PZFLB ";
                        (strArray6 = strArray)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[2] = strArray6[2] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " And HSBZ=2 ";
                        (strArray6 = strArray)[2] = strArray6[2] + " group by ZH,KM,JDBZ,SPBH,JLDW";
                    Label_25E2:
                        if (num4 == 0)
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[3] = strArray6[3] + " FROM PZFLB ";
                            (strArray6 = strArray)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[3] = strArray6[3] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " And HSBZ=3 ";
                            (strArray6 = strArray)[3] = strArray6[3] + " group by ZH,KM,JDBZ,SPBH,JLDW";
                        }
                        strArray[4] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                        (strArray6 = strArray)[4] = strArray6[4] + " FROM PZFLB ";
                        (strArray6 = strArray)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[4] = strArray6[4] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " And HSBZ=4 ";
                        (strArray6 = strArray)[4] = strArray6[4] + " group by ZH,KHBH,KM,JDBZ";
                        strArray[5] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                        (strArray6 = strArray)[5] = strArray6[5] + " FROM PZFLB ";
                        (strArray6 = strArray)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                        (strArray6 = strArray)[5] = strArray6[5] + " And JDBZ<>0 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " And HSBZ=5 ";
                        (strArray6 = strArray)[5] = strArray6[5] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                        if (num4 == 0)
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                            (strArray6 = strArray)[6] = strArray6[6] + " FROM PZFLB ";
                            (strArray6 = strArray)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[6] = strArray6[6] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " And HSBZ=6 ";
                            (strArray6 = strArray)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        if (num4 == 0)
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                        }
                        else
                        {
                            strArray[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray)[7] = strArray6[7] + " FROM PZFLB ";
                            (strArray6 = strArray)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray)[7] = strArray6[7] + " And JDBZ<>0 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " And HSBZ=7 ";
                            (strArray6 = strArray)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                        }
                        if (num3 == 0)
                        {
                            strArray2[0] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " FROM PZFLB ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[0] = strArray6[0] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " And HSBZ=0 ";
                            (strArray6 = strArray2)[0] = strArray6[0] + " group by ZH,KHBH,KM,JDBZ";
                            strArray2[1] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " FROM PZFLB ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[1] = strArray6[1] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " And HSBZ=1 ";
                            (strArray6 = strArray2)[1] = strArray6[1] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[2] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray2)[2] = strArray6[2] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[3] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray2)[3] = strArray6[3] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            strArray2[4] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " FROM PZFLB ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[4] = strArray6[4] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " And HSBZ=4 ";
                            (strArray6 = strArray2)[4] = strArray6[4] + " group by ZH,KHBH,KM,JDBZ";
                            strArray2[5] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " FROM PZFLB ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray2)[5] = strArray6[5] + " And JDBZ=0 ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " And HSBZ=5 ";
                            (strArray6 = strArray2)[5] = strArray6[5] + " group by ZH,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[6] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ,JDBZ,JLDW";
                                (strArray6 = strArray2)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray2)[6] = strArray6[6] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray2[7] = "SELECT count(*)cunt,ZH,max(DJXX)DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray2)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray2)[7] = strArray6[7] + " group by ZH,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                        }
                        if (num3 == 1)
                        {
                            strArray3[0] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,max(KPRQ)KPRQ,KM,JDBZ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " FROM PZFLB ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[0] = strArray6[0] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " And HSBZ=0 ";
                            (strArray6 = strArray3)[0] = strArray6[0] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                            strArray3[1] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " FROM PZFLB ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[1] = strArray6[1] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " And HSBZ=1 ";
                            (strArray6 = strArray3)[1] = strArray6[1] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[2] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[2] = strArray6[2] + " FROM PZFLB ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[2] = strArray6[2] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " And HSBZ=2 ";
                                (strArray6 = strArray3)[2] = strArray6[2] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[3] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " FROM PZFLB ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[3] = strArray6[3] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " And HSBZ=3 ";
                                (strArray6 = strArray3)[3] = strArray6[3] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            strArray3[4] = "SELECT count(*)cunt,ZH,DJXX,KHBH,sum(JE) as JE,sum(SL)SL,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " FROM PZFLB ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[4] = strArray6[4] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " And HSBZ=4 ";
                            (strArray6 = strArray3)[4] = strArray6[4] + " group by ZH,DJXX,KHBH,KM,JDBZ";
                            strArray3[5] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,max(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " FROM PZFLB ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " WHERE ZH=" + strZH;
                            (strArray6 = strArray3)[5] = strArray6[5] + " And JDBZ=0 ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " And HSBZ=5 ";
                            (strArray6 = strArray3)[5] = strArray6[5] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH";
                            if (num4 == 0)
                            {
                                strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[6] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ,JLDW";
                                (strArray6 = strArray3)[6] = strArray6[6] + " FROM PZFLB ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[6] = strArray6[6] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " And HSBZ=6 ";
                                (strArray6 = strArray3)[6] = strArray6[6] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                            if (num4 == 0)
                            {
                                strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW,DJ";
                            }
                            else
                            {
                                strArray3[7] = "SELECT count(*)cunt,ZH,DJXX,KHBH,SPBH,sum(JE) as JE,sum(SL)SL,sum(DJ)DJ,KM,max(KPRQ)KPRQ ,JDBZ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " FROM PZFLB ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " WHERE ZH=" + strZH;
                                (strArray6 = strArray3)[7] = strArray6[7] + " And JDBZ=0 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " And HSBZ=7 ";
                                (strArray6 = strArray3)[7] = strArray6[7] + " group by ZH,DJXX,KHBH,KM,JDBZ,SPBH,JLDW";
                            }
                        }
                    Label_33F3:
                        list2 = this._BaseDAOSQLite.querySQL(str);
                        if ((list2 != null) && (0 < list2.Count))
                        {
                            Dictionary<string, object> dictionary = list2[0] as Dictionary<string, object>;
                            string s = dictionary["cunt"].ToString();
                            int result = 0;
                            int.TryParse(s, out result);
                            if (0 < result)
                            {
                                int entryNo = 1;
                                string str13 = string.Empty;
                                List<TPZEntry_InfoModal> list3 = this.pzflbBll.SelectPZFLB_ZH_DJXX(strZH);
                                int count = 0;
                                if ((list3 != null) && (0 < list3.Count))
                                {
                                    count = list3.Count;
                                    TPZEntry_InfoModal modal2 = list3[0];
                                    str13 = modal2.PZEntry_KindcodeNo;
                                    for (int m = 1; m < list3.Count; m++)
                                    {
                                        modal2 = list3[m];
                                        str13 = str13 + "|" + modal2.PZEntry_KindcodeNo;
                                    }
                                }
                                xml = (((((((((((((((xml + "<voucher>") + "<voucher_header>" + "<voucher_type>") + PzNo + "</voucher_type>") + "<tran_type>" + "801") + "</tran_type>" + "<systype>") + "TI" + "</systype>") + "<enter>" + str9) + "</enter>" + "<date>") + str10 + "</date>") + "<cashier/>" + "<attachment_number>") + count.ToString() + "</attachment_number>") + "<remark>" + "防伪开票发票生成凭证") + "</remark>" + "<genTranfVoucher/>") + "<systype/>" + "<InvInfo>") + str13 + "</InvInfo>") + "</voucher_header>" + "<voucher_body>";
                                for (int j = 0; j < num; j++)
                                {
                                    string str14 = strArray2[j];
                                    if (num3 == 0)
                                    {
                                        str14 = strArray2[j];
                                    }
                                    else
                                    {
                                        str14 = strArray3[j];
                                    }
                                    ArrayList arrayList = this._BaseDAOSQLite.querySQL(str14);
                                    if ((arrayList != null) && (0 < arrayList.Count))
                                    {
                                        Dictionary<string, object> dictionary2 = arrayList[0] as Dictionary<string, object>;
                                        string str15 = dictionary2["cunt"].ToString();
                                        int num11 = 0;
                                        int.TryParse(str15, out num11);
                                        if (0 < num11)
                                        {
                                            xml = xml + this.MakeA6EntryXML(arrayList, entryNo, num3, j);
                                            entryNo += arrayList.Count;
                                        }
                                    }
                                }
                                for (int k = 0; k < num; k++)
                                {
                                    string str16 = strArray[k];
                                    ArrayList list5 = this._BaseDAOSQLite.querySQL(str16);
                                    if ((list5 != null) && (0 < list5.Count))
                                    {
                                        Dictionary<string, object> dictionary3 = list5[0] as Dictionary<string, object>;
                                        string str17 = dictionary3["cunt"].ToString();
                                        int num13 = 0;
                                        int.TryParse(str17, out num13);
                                        if (0 < num13)
                                        {
                                            xml = xml + this.MakeA6EntryXML(list5, entryNo, index, k);
                                            entryNo += list5.Count;
                                        }
                                    }
                                }
                                xml = xml + "</voucher_body>" + "</voucher>";
                            }
                        }
                    }
                    xml = xml + "</PzInfo>";
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(xml);
                    object obj2 = new object();
                    string str18 = "saveVoucher";
                    string[] strArray4 = new string[] { str8, str9, xml };
                    string str19 = (string) WebServiceFactory.InvokeWebService(str2, str18, strArray4);
                    if (str19.Length <= 0)
                    {
                        MessageHelper.MsgWait();
                        return Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(DingYiZhiFuChuan.strErrLinkFailTip, this._Loger);
                    }
                    if (string.IsNullOrEmpty(str19))
                    {
                        bHasShown = true;
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000010");
                        return false;
                    }
                    this.A6pzInfoList.Clear();
                    document = new XmlDocument();
                    document.LoadXml(str19);
                    if (!document.DocumentElement.Name.Equals("Doc"))
                    {
                        MessageHelper.MsgWait();
                        return false;
                    }
                    XmlElement element = document.DocumentElement["msg"];
                    if (element != null)
                    {
                        MessageHelper.MsgWait();
                        string innerText = element.InnerText;
                        if (string.IsNullOrEmpty(innerText) || "null".Equals(innerText, StringComparison.CurrentCultureIgnoreCase))
                        {
                            innerText = "总账系统异常，请检查总账系统是否正常或相关功能是否开启！";
                        }
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { innerText });
                        bHasShown = true;
                        return false;
                    }
                    XmlElement element2 = document.DocumentElement["ReturnValue"];
                    if (element2.HasChildNodes)
                    {
                        int num15 = 0;
                        string str21 = string.Empty;
                        string str22 = string.Empty;
                        string str23 = string.Empty;
                        XmlElement element3 = element2["Records"];
                        this.msgPzSucCount = element2.ChildNodes.Count;
                        while (num15 < element2.ChildNodes.Count)
                        {
                            string str30;
                            element3 = (XmlElement) element2.ChildNodes.Item(num15);
                            XmlElement element4 = element3["OutBillInfo"];
                            str21 = element4.InnerText;
                            XmlElement element5 = element3["coutno_id"];
                            str22 = element5.InnerText;
                            this.A6pzInfoList.Add(str22.Trim());
                            string str24 = string.Empty;
                            string fpzl = string.Empty;
                            string fpdm = string.Empty;
                            string fphm = string.Empty;
                            string str28 = string.Empty;
                            str24 = str21;
                            int length = str24.Length;
                            int num17 = str24.IndexOf("|");
                            int num18 = 0;
                            int num19 = 0;
                            string str29 = this.dateTime_ZhiDanRiQi.Value.ToString(DingYiZhiFuChuan.strYear_Month_Day_Formart);
                            while (num17 > 0)
                            {
                                length = str24.Length;
                                str28 = str24.Substring(0, num17);
                                num18 = str28.Length;
                                num19 = str28.IndexOf(",");
                                fpzl = str28.Substring(0, num19);
                                str28 = str28.Substring(num19 + 1, (num18 - num19) - 1);
                                num18 = str28.Length;
                                num19 = str28.IndexOf(",");
                                fpdm = str28.Substring(0, num19);
                                fphm = str28.Substring(num19 + 1, (num18 - num19) - 1);
                                str30 = "Update XXFP set PZYWH='" + str22 + "', PZWLYWH='" + this.GetWLYWH(fpdm, fphm, fpzl) + "', PZZT='0',PZHM='-1'";
                                str23 = (((str30 + ", PZLB='" + this._A6PzName + "' ,PZRQ='" + str29.Trim() + "'") + " where FPZL='" + fpzl + "'") + " and FPDM='" + fpdm + "'") + " and FPHM='" + fphm + "'";
                                this._BaseDAOSQLite.updateSQL(str23);
                                str24 = str24.Substring(num17 + 1, (length - num17) - 1);
                                num17 = str24.IndexOf("|");
                            }
                            str28 = str24;
                            num18 = str28.Length;
                            num19 = str28.IndexOf(",");
                            fpzl = str28.Substring(0, num19);
                            str28 = str28.Substring(num19 + 1, (num18 - num19) - 1);
                            num18 = str28.Length;
                            num19 = str28.IndexOf(",");
                            fpdm = str28.Substring(0, num19);
                            fphm = str28.Substring(num19 + 1, (num18 - num19) - 1);
                            str30 = "Update XXFP set PZYWH='" + str22 + "', PZWLYWH='" + this.GetWLYWH(fpdm, fphm, fpzl) + "'";
                            str23 = (((str30 + ", PZLB='" + this._A6PzName + "' ,PZRQ='" + str29.Trim() + "'") + " where FPZL='" + fpzl + "'") + " and FPDM='" + fpdm + "'") + " and FPHM='" + fphm + "'";
                            this._BaseDAOSQLite.updateSQL(str23);
                            num15++;
                        }
                        MessageHelper.MsgWait();
                        string[] strArray5 = new string[] { this.msgSelInvCount.ToString(), this.msgPzCount.ToString(), this.msgPzSucCount.ToString() };
                        MessageManager.ShowMsgBox("FPZPZ-000017", strArray5);
                    }
                    else
                    {
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000018");
                    }
                }
                else
                {
                    MessageHelper.MsgWait();
                    return false;
                }
            }
            catch (BaseException exception)
            {
                MessageHelper.MsgWait();
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                MessageHelper.MsgWait();
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool MakePz(string PzType, string strA6PzName, List<DataGridViewRow> listRowsZhiDan)
        {
            try
            {
                if (!string.IsNullOrEmpty(PzType) && (listRowsZhiDan != null))
                {
                    int count = this.customStyleDataGrid1.Rows.Count;
                    this.InitWLYWH();
                    if (count <= 0)
                    {
                        return false;
                    }
                    int num2 = listRowsZhiDan.Count;
                    List<Fpxx> list = new List<Fpxx>();
                    if (num2 > 0)
                    {
                        for (int i = 0; i < listRowsZhiDan.Count; i++)
                        {
                            DataGridViewRow row = listRowsZhiDan[i];
                            string str = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[0]].Value.ToString();
                            if (str.Equals(DingYiZhiFuChuan.strFPZL[1]))
                            {
                                str = DingYiZhiFuChuan.strFPZL_s_c[1];
                            }
                            else if (str.Equals(DingYiZhiFuChuan.strFPZL[0]))
                            {
                                str = DingYiZhiFuChuan.strFPZL_s_c[0];
                            }
                            else if (str.Equals(DingYiZhiFuChuan.strFPZL[2]))
                            {
                                str = DingYiZhiFuChuan.strFPZL_s_c[2];
                            }
                            else if (str.Equals(DingYiZhiFuChuan.strFPZL[3]))
                            {
                                str = DingYiZhiFuChuan.strFPZL_s_c[3];
                            }
                            string str2 = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm]].Value.ToString();
                            string s = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm]].Value.ToString();
                            int result = 0;
                            int.TryParse(s, out result);
                            string str4 = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfmc]].Value.ToString();
                            string str5 = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexGfbm]].Value.ToString();
                            string item = str4 + DingYiZhiFuChuan.strFenGeFu + str5;
                            if (!this.listBuyerInfoList.Contains(item))
                            {
                                this.listBuyerInfoList.Add(item);
                            }
                            object[] objArray = new object[] { str, str2, result };
                            object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPXX", objArray);
                            string message = "调用Aisino.Fwkp.QueryFPXX接口异常！";
                            if (objArray2 == null)
                            {
                                MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { message });
                                this._Loger.Error(message);
                                return false;
                            }
                            if (objArray2.Length < 2)
                            {
                                MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { message });
                                this._Loger.Error(message);
                                return false;
                            }
                            if (!((string) objArray2[1]).Equals("0000"))
                            {
                                MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { (string) objArray2[1] });
                                this._Loger.Error(message);
                                return false;
                            }
                            if (objArray2[0] == null)
                            {
                                MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { message });
                                this._Loger.Error(message);
                                return false;
                            }
                            Fpxx fpxx = (Fpxx) objArray2[0];
                            fpxx.bz = row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].Value.ToString();
                            list.Add(fpxx);
                            List<Dictionary<SPXX, string>> mxxx = fpxx.Mxxx;
                            bool flag = false;
                            if ((fpxx.Mxxx != null) && (fpxx.Mxxx.Count > 0))
                            {
                                foreach (Dictionary<SPXX, string> dictionary in fpxx.Mxxx)
                                {
                                    if (dictionary[0].IndexOf("详见销货清单") > 0)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if ((mxxx == null) || flag)
                            {
                                mxxx = fpxx.Qdxx;
                            }
                            if (mxxx != null)
                            {
                                foreach (Dictionary<SPXX, string> dictionary2 in mxxx)
                                {
                                    string str8 = dictionary2[0].ToString();
                                    string str9 = string.Empty;
                                    string str10 = str8 + DingYiZhiFuChuan.strFenGeFu + str9;
                                    if (!this.listGoodsInfoList.Contains(str10))
                                    {
                                        this.listGoodsInfoList.Add(str10);
                                    }
                                }
                            }
                        }
                        this.InitMakePZ();
                        this.msgSelInvCount = 0;
                        this.msgPzCount = 0;
                        this.msgPzSucCount = 0;
                        List<int> list3 = new List<int>();
                        for (int j = 0; j < list.Count; j++)
                        {
                            Fpxx fpxx2 = list[j];
                            double num6 = 0.0;
                            double.TryParse(fpxx2.je, out num6);
                            string bz = fpxx2.bz;
                            int num7 = 0;
                            int.TryParse(bz, out num7);
                            if (!string.IsNullOrEmpty(bz) && (0 < num7))
                            {
                                string invKind = DingYiZhiFuChuan.strFPZL_s_c[0];
                                if (fpxx2.fplx.Equals((FPLX) 2))
                                {
                                    invKind = DingYiZhiFuChuan.strFPZL_s_c[1];
                                }
                                else if (fpxx2.fplx.Equals((FPLX) 0))
                                {
                                    invKind = DingYiZhiFuChuan.strFPZL_s_c[0];
                                }
                                else if (fpxx2.fplx.Equals((FPLX) 12))
                                {
                                    invKind = DingYiZhiFuChuan.strFPZL_s_c[2];
                                }
                                else if (fpxx2.fplx.Equals((FPLX) 11))
                                {
                                    invKind = DingYiZhiFuChuan.strFPZL_s_c[3];
                                }
                                string fpdm = fpxx2.fpdm;
                                string fphm = fpxx2.fphm;
                                int num8 = 0;
                                int.TryParse(fpxx2.fphm, out num8);
                                fphm = num8.ToString();
                                this.MakePZEntryToTbl(invKind, fpdm, fphm, fpxx2);
                                this.msgSelInvCount++;
                                if (!list3.Contains(num7))
                                {
                                    list3.Add(num7);
                                }
                                this.InsertWLYWH(fpxx2.fpdm, fpxx2.fphm, fpxx2.fplx.ToString(), num7, string.Empty);
                            }
                        }
                        this.msgPzCount = list3.Count;
                        if (DingYiZhiFuChuan.isA6PzVersion)
                        {
                            bool bHasShown = false;
                            if (!this.MakeA6PZXML(PzType, out bHasShown))
                            {
                                if (!bHasShown)
                                {
                                    MessageManager.ShowMsgBox("FPZPZ-000021");
                                }
                                return false;
                            }
                        }
                        goto Label_0650;
                    }
                    MessageManager.ShowMsgBox("FPZPZ-000016");
                }
                return false;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        Label_0650:
            return true;
        }

        private bool MakePzEntryFun(Dictionary<SPXX, string> tempSPXXModal, int PZGroup, string CusrID, string InvKind, string InvCode, string InvNo, string strYskm, double Je, double Se, bool IsDiscount)
        {
            try
            {
                if (tempSPXXModal == null)
                {
                    return false;
                }
                TPZEntry_InfoModal pZEntryInfo = new TPZEntry_InfoModal();
                string strSPBH = string.Empty;
                string strMc = tempSPXXModal[0].ToString();
                List<SPBMModal> list = this.spbmBll.SelectSPBM_MC_BM(strMc, string.Empty);
                if ((list != null) && (0 < list.Count))
                {
                    strSPBH = list[0].BM;
                }
                List<SPBMModal> list2 = this.spbmBll.SelectSPKMB_SPBH(strSPBH);
                string xSSRKM = string.Empty;
                string yJZZSKM = string.Empty;
                string xSTHKM = string.Empty;
                double result = 0.0;
                double num2 = 0.0;
                bool flag = false;
                if (tempSPXXModal[11].ToString().Equals(DingYiZhiFuChuan.strRight_Wrong[0]))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
                double num3 = 0.0;
                double.TryParse(tempSPXXModal[8].ToString(), out num3);
                result = 0.0;
                double.TryParse(tempSPXXModal[7].ToString(), out result);
                string str7 = tempSPXXModal[4].ToString();
                if (list2 != null)
                {
                    if (0 < list2.Count)
                    {
                        SPBMModal modal2 = list2[0];
                        xSSRKM = modal2.XSSRKM;
                        yJZZSKM = modal2.YJZZSKM;
                        xSTHKM = modal2.XSTHKM;
                        if (string.IsNullOrEmpty(xSSRKM))
                        {
                            xSSRKM = this.XSSRSubJect.Trim();
                        }
                        if (string.IsNullOrEmpty(yJZZSKM))
                        {
                            yJZZSKM = this.YJZZSSubject.Trim();
                        }
                        if (string.IsNullOrEmpty(xSTHKM))
                        {
                            xSTHKM = this.XSTHSubject.Trim();
                        }
                        if (!IsDiscount)
                        {
                            num2 = 0.0;
                            double.TryParse(tempSPXXModal[9].ToString(), out num2);
                        }
                        else
                        {
                            result = Je;
                            num2 = Se;
                        }
                        pZEntryInfo.PZEntry_Group = PZGroup;
                        pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                        pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                        pZEntryInfo.PZEntry_SkKm = strYskm;
                        decimal num4 = new decimal(result + num2);
                        pZEntryInfo.PZEntry_JE = num4;
                        pZEntryInfo.PZEntry_JDSFlag = 0;
                        pZEntryInfo.PZEntry_GooDsID = strSPBH;
                        pZEntryInfo.PZEntry_SPNum = 0.0;
                        double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                        pZEntryInfo.PZEntry_Jldw = str7;
                        double num5 = 0.0;
                        double.TryParse(tempSPXXModal[5].ToString(), out num5);
                        if (flag)
                        {
                            double num6 = 0.0;
                            double.TryParse(tempSPXXModal[8].ToString(), out num6);
                            pZEntryInfo.PZEntry_SPPrice = num5 / (1.0 + num6);
                        }
                        else
                        {
                            pZEntryInfo.PZEntry_SPPrice = num5;
                        }
                        pZEntryInfo.PZEntry_NumCheck = 0;
                        this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                        if (this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 1;
                        }
                        if (this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 2;
                        }
                        if (this.isA6GoodsHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 3;
                        }
                        if (this.isA6CustHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 4;
                        }
                        if (this.isA6CustHs && this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 5;
                        }
                        if (this.isA6CustHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 6;
                        }
                        if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 7;
                        }
                        pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                        this.AddInfoToPZTempTbl(pZEntryInfo);
                        pZEntryInfo.PZEntry_Group = PZGroup;
                        pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                        pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                        num4 = new decimal(result);
                        pZEntryInfo.PZEntry_JE = num4;
                        string str8 = PropertyUtil.GetValue(DingYiZhiFuChuan.PosiInvOpType_Zfsfp_ZdsItemValue, "借贷方向相同，金额正负相反");
                        if ((result < 0.0) && str8.Equals("借贷方向相反，金额正负相同"))
                        {
                            pZEntryInfo.PZEntry_SkKm = xSTHKM;
                        }
                        else
                        {
                            pZEntryInfo.PZEntry_SkKm = xSSRKM;
                        }
                        pZEntryInfo.PZEntry_JDSFlag = 1;
                        pZEntryInfo.PZEntry_GooDsID = strSPBH;
                        pZEntryInfo.PZEntry_SPNum = 0.0;
                        double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                        pZEntryInfo.PZEntry_Jldw = tempSPXXModal[4].ToString();
                        num5 = 0.0;
                        double.TryParse(tempSPXXModal[5].ToString(), out num5);
                        if (flag)
                        {
                            double num7 = 0.0;
                            double.TryParse(tempSPXXModal[8].ToString(), out num7);
                            pZEntryInfo.PZEntry_SPPrice = num5 / (1.0 + num7);
                        }
                        else
                        {
                            pZEntryInfo.PZEntry_SPPrice = num5;
                        }
                        pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                        pZEntryInfo.PZEntry_NumCheck = 0;
                        this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                        if (this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 1;
                        }
                        if (this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 2;
                        }
                        if (this.isA6GoodsHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 3;
                        }
                        if (this.isA6CustHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 4;
                        }
                        if (this.isA6CustHs && this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 5;
                        }
                        if (this.isA6CustHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 6;
                        }
                        if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 7;
                        }
                        this.AddInfoToPZTempTbl(pZEntryInfo);
                        pZEntryInfo.PZEntry_Group = PZGroup;
                        pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                        pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                        num4 = new decimal(num2);
                        pZEntryInfo.PZEntry_JE = num4;
                        pZEntryInfo.PZEntry_JDSFlag = 2;
                        pZEntryInfo.PZEntry_SkKm = yJZZSKM;
                        pZEntryInfo.PZEntry_GooDsID = strSPBH;
                        pZEntryInfo.PZEntry_SPNum = 0.0;
                        double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                        pZEntryInfo.PZEntry_Jldw = tempSPXXModal[4].ToString();
                        num5 = 0.0;
                        double.TryParse(tempSPXXModal[5].ToString(), out num5);
                        if (flag)
                        {
                            double num8 = 0.0;
                            double.TryParse(tempSPXXModal[8].ToString(), out num8);
                            pZEntryInfo.PZEntry_SPPrice = num5 / (1.0 + num8);
                        }
                        else
                        {
                            pZEntryInfo.PZEntry_SPPrice = num5;
                        }
                        pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                        this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                        pZEntryInfo.PZEntry_NumCheck = 0;
                        if (this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 1;
                        }
                        if (this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 2;
                        }
                        if (this.isA6GoodsHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 3;
                        }
                        if (this.isA6CustHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 4;
                        }
                        if (this.isA6CustHs && this.isA6GoodsHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 5;
                        }
                        if (this.isA6CustHs && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 6;
                        }
                        if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                        {
                            pZEntryInfo.PZEntry_NumCheck = 7;
                        }
                        this.AddInfoToPZTempTbl(pZEntryInfo);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(xSSRKM))
                    {
                        xSSRKM = this.XSSRSubJect.Trim();
                    }
                    if (string.IsNullOrEmpty(yJZZSKM))
                    {
                        yJZZSKM = this.YJZZSSubject.Trim();
                    }
                    if (string.IsNullOrEmpty(xSTHKM))
                    {
                        xSTHKM = this.XSTHSubject.Trim();
                    }
                    if (!IsDiscount)
                    {
                        num2 = 0.0;
                        double.TryParse(tempSPXXModal[9].ToString(), out num2);
                    }
                    else
                    {
                        result = Je;
                        num2 = Se;
                    }
                    pZEntryInfo.PZEntry_Group = PZGroup;
                    pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                    pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                    pZEntryInfo.PZEntry_SkKm = strYskm;
                    decimal num9 = new decimal(result + num2);
                    pZEntryInfo.PZEntry_JE = num9;
                    pZEntryInfo.PZEntry_JDSFlag = 0;
                    pZEntryInfo.PZEntry_GooDsID = strSPBH;
                    pZEntryInfo.PZEntry_SPNum = 0.0;
                    double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                    pZEntryInfo.PZEntry_Jldw = tempSPXXModal[4].ToString();
                    double num10 = 0.0;
                    double.TryParse(tempSPXXModal[5].ToString(), out num10);
                    if (flag)
                    {
                        double num11 = 0.0;
                        double.TryParse(tempSPXXModal[8].ToString(), out num11);
                        pZEntryInfo.PZEntry_SPPrice = num10 / (1.0 + num11);
                    }
                    else
                    {
                        pZEntryInfo.PZEntry_SPPrice = num10;
                    }
                    pZEntryInfo.PZEntry_NumCheck = 0;
                    this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                    if (this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 1;
                    }
                    if (this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 2;
                    }
                    if (this.isA6GoodsHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 3;
                    }
                    if (this.isA6CustHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 4;
                    }
                    if (this.isA6CustHs && this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 5;
                    }
                    if (this.isA6CustHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 6;
                    }
                    if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 7;
                    }
                    pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                    this.AddInfoToPZTempTbl(pZEntryInfo);
                    pZEntryInfo.PZEntry_Group = PZGroup;
                    pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                    pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                    num9 = new decimal(result);
                    pZEntryInfo.PZEntry_JE = num9;
                    if (result < 0.0)
                    {
                        pZEntryInfo.PZEntry_SkKm = xSTHKM;
                    }
                    else
                    {
                        pZEntryInfo.PZEntry_SkKm = xSSRKM;
                    }
                    pZEntryInfo.PZEntry_JDSFlag = 1;
                    pZEntryInfo.PZEntry_GooDsID = strSPBH;
                    pZEntryInfo.PZEntry_SPNum = 0.0;
                    double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                    pZEntryInfo.PZEntry_Jldw = tempSPXXModal[4].ToString();
                    num10 = 0.0;
                    double.TryParse(tempSPXXModal[5].ToString(), out num10);
                    if (flag)
                    {
                        double num12 = 0.0;
                        double.TryParse(tempSPXXModal[8].ToString(), out num12);
                        pZEntryInfo.PZEntry_SPPrice = num10 / (1.0 + num12);
                    }
                    else
                    {
                        pZEntryInfo.PZEntry_SPPrice = num10;
                    }
                    pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                    this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                    pZEntryInfo.PZEntry_NumCheck = 0;
                    if (this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 1;
                    }
                    if (this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 2;
                    }
                    if (this.isA6GoodsHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 3;
                    }
                    if (this.isA6CustHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 4;
                    }
                    if (this.isA6CustHs && this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 5;
                    }
                    if (this.isA6CustHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 6;
                    }
                    if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 7;
                    }
                    this.AddInfoToPZTempTbl(pZEntryInfo);
                    pZEntryInfo.PZEntry_Group = PZGroup;
                    pZEntryInfo.PZEntry_KindcodeNo = InvKind.Trim() + "," + InvCode.Trim() + "," + InvNo.Trim();
                    pZEntryInfo.PZEntry_CusrID = CusrID.Trim();
                    num9 = new decimal(num2);
                    pZEntryInfo.PZEntry_JE = num9;
                    pZEntryInfo.PZEntry_JDSFlag = 2;
                    pZEntryInfo.PZEntry_SkKm = yJZZSKM;
                    pZEntryInfo.PZEntry_GooDsID = strSPBH;
                    pZEntryInfo.PZEntry_SPNum = 0.0;
                    double.TryParse(tempSPXXModal[6].ToString(), out pZEntryInfo.PZEntry_SPNum);
                    pZEntryInfo.PZEntry_Jldw = tempSPXXModal[4].ToString();
                    num10 = 0.0;
                    double.TryParse(tempSPXXModal[5].ToString(), out num10);
                    if (flag)
                    {
                        double num13 = 0.0;
                        double.TryParse(tempSPXXModal[8].ToString(), out num13);
                        pZEntryInfo.PZEntry_SPPrice = num10 / (1.0 + num13);
                    }
                    else
                    {
                        pZEntryInfo.PZEntry_SPPrice = num10;
                    }
                    pZEntryInfo.PZEntry_Date = this.PZEntryDate;
                    this.checkKmCode(pZEntryInfo.PZEntry_SkKm.Trim(), str7.Trim());
                    pZEntryInfo.PZEntry_NumCheck = 0;
                    if (this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 1;
                    }
                    if (this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 2;
                    }
                    if (this.isA6GoodsHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 3;
                    }
                    if (this.isA6CustHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 4;
                    }
                    if (this.isA6CustHs && this.isA6GoodsHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 5;
                    }
                    if (this.isA6CustHs && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 6;
                    }
                    if ((this.isA6CustHs && this.isA6GoodsHs) && this.isA6NumHs)
                    {
                        pZEntryInfo.PZEntry_NumCheck = 7;
                    }
                    this.AddInfoToPZTempTbl(pZEntryInfo);
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
                if (list2 != null)
                {
                    list2.Clear();
                    list2 = null;
                }
                pZEntryInfo = null;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool MakePZEntryToTbl(string InvKind, string InvCode, string InvNo, Fpxx fpxx)
        {
            try
            {
                if ((string.IsNullOrEmpty(InvKind) || string.IsNullOrEmpty(InvCode)) || string.IsNullOrEmpty(InvNo))
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("分录" + InvKind + InvCode + InvNo + " failed:有空数据", this._Loger);
                    return false;
                }
                if (fpxx == null)
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("分录" + InvKind + InvCode + InvNo + " failed:null == fpxx", this._Loger);
                    return false;
                }
                string bM = string.Empty;
                int result = 0;
                int.TryParse(fpxx.bz, out result);
                if (string.IsNullOrEmpty(bM))
                {
                    string gfmc = fpxx.gfmc;
                    List<KHBMModal> list = this.khbmBll.SelectKHBM_MC_BM(gfmc, string.Empty);
                    if (list != null)
                    {
                        if (0 < list.Count)
                        {
                            bM = list[0].BM;
                        }
                        list.Clear();
                    }
                }
                string text1 = "SELECT * FROM KHKMB where KHBH='" + bM.Trim() + "'";
                List<KHBMModal> list2 = this.khbmBll.SelectKHKMB_KHBH(bM);
                string strYskm = string.Empty;
                if (list2 != null)
                {
                    if (0 < list2.Count)
                    {
                        KHBMModal modal = list2[0];
                        strYskm = modal.YSKM;
                    }
                    else
                    {
                        strYskm = this.YSSubject.Trim();
                    }
                }
                else
                {
                    strYskm = this.YSSubject.Trim();
                }
                this.PZEntryDate = DateTime.ParseExact(fpxx.kprq.Replace("年", "-").Replace("月", "-").Replace("日", "").Replace("时", ":").Replace("分", ":").Replace("秒", ""), DingYiZhiFuChuan.strYear_Month_Day_Formart, CultureInfo.CurrentCulture);
                this.MakePzFun(fpxx, bM, InvKind, InvCode, InvNo, strYskm, result);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private bool MakePzFun(Fpxx fpxx, string CusrID, string InvKind, string InvCode, string InvNo, string strYskm, int PZGroup)
        {
            bool flag = true;
            try
            {
                List<Dictionary<SPXX, string>> qdxx = null;
                if (fpxx.Qdxx != null)
                {
                    qdxx = fpxx.Qdxx;
                }
                else
                {
                    qdxx = fpxx.Mxxx;
                }
                if (qdxx == null)
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("分录" + InvKind + InvCode + InvNo + " Fail:" + CusrID + "  发票明细清单都为空", this._Loger);
                    return false;
                }
                if (1 == qdxx.Count)
                {
                    foreach (Dictionary<SPXX, string> dictionary in qdxx)
                    {
                        this.MakePzEntryFun(dictionary, PZGroup, CusrID, InvKind, InvCode, InvNo, strYskm, 0.0, 0.0, false);
                    }
                    return false;
                }
                List<Dictionary<SPXX, string>> list2 = new List<Dictionary<SPXX, string>>();
                foreach (Dictionary<SPXX, string> dictionary2 in qdxx)
                {
                    int result = 0;
                    int.TryParse(dictionary2[10].ToString(), out result);
                    string str2 = dictionary2[0].ToString();
                    if (((result == 0) && (str2 != "原价合计")) && (str2 != "折扣额合计"))
                    {
                        list2.Add(dictionary2);
                    }
                }
                if (0 < list2.Count)
                {
                    flag = true;
                    foreach (Dictionary<SPXX, string> dictionary3 in list2)
                    {
                        this.MakePzEntryFun(dictionary3, PZGroup, CusrID, InvKind, InvCode, InvNo, strYskm, 0.0, 0.0, false);
                    }
                }
                if (list2 != null)
                {
                    list2.Clear();
                    list2 = null;
                }
                List<Dictionary<SPXX, string>> list3 = new List<Dictionary<SPXX, string>>();
                foreach (Dictionary<SPXX, string> dictionary4 in qdxx)
                {
                    int num2 = 0;
                    int.TryParse(dictionary4[10].ToString(), out num2);
                    string str4 = dictionary4[0].ToString();
                    if (((3 == num2) || (4 == num2)) && ((str4 != "原价合计") && (str4 != "折扣额合计")))
                    {
                        list3.Add(dictionary4);
                    }
                }
                if (0 < list3.Count)
                {
                    flag = true;
                    for (int i = 0; i < list3.Count; i++)
                    {
                        Dictionary<SPXX, string> tempSPXXModal = list3[i];
                        int num4 = 0;
                        int.TryParse(tempSPXXModal[10].ToString(), out num4);
                        if (4 == num4)
                        {
                            string str6 = tempSPXXModal[0].ToString();
                            double num5 = 0.0;
                            double.TryParse(tempSPXXModal[8].ToString(), out num5);
                            double num6 = 0.0;
                            double.TryParse(tempSPXXModal[7].ToString(), out num6);
                            double num7 = 0.0;
                            double.TryParse(tempSPXXModal[9].ToString(), out num7);
                            double num8 = num6;
                            double num9 = num7;
                            if (!string.IsNullOrEmpty(str6))
                            {
                                int index = str6.IndexOf("(");
                                int num11 = str6.IndexOf(")");
                                if (index == 2)
                                {
                                    Dictionary<SPXX, string> dictionary6 = list3[i - 1];
                                    double num12 = 0.0;
                                    double.TryParse(dictionary6[7].ToString(), out num12);
                                    num6 = num12 + num6;
                                    double num13 = 0.0;
                                    double.TryParse(dictionary6[9].ToString(), out num13);
                                    num7 = num13 + num7;
                                    tempSPXXModal[0] = dictionary6[0].ToString();
                                    tempSPXXModal[6] = dictionary6[6].ToString();
                                    tempSPXXModal[5] = dictionary6[5].ToString();
                                    tempSPXXModal[4] = dictionary6[4].ToString();
                                    this.MakePzEntryFun(tempSPXXModal, PZGroup, CusrID, InvKind, InvCode, InvNo, strYskm, num6, num7, true);
                                }
                                else if (index > 2)
                                {
                                    int num14 = 0;
                                    int.TryParse(str6.Substring(str6.IndexOf("行数") + 2, (index - str6.IndexOf("行数")) - 2), out num14);
                                    double num15 = 0.0;
                                    double.TryParse(str6.Substring(index + 1, (num11 - index) - 2), out num15);
                                    num15 /= 100.0;
                                    double num16 = 0.0;
                                    double num17 = 0.0;
                                    for (int j = 0; j < num14; j++)
                                    {
                                        Dictionary<SPXX, string> dictionary7 = list3[(i - num14) + j];
                                        double num19 = 0.0;
                                        double.TryParse(dictionary7[7].ToString(), out num19);
                                        double num20 = num19 * num15;
                                        double num21 = 0.0;
                                        double.TryParse(dictionary7[9].ToString(), out num21);
                                        double num22 = num21 * num15;
                                        num6 = num19 * (1.0 - num15);
                                        num7 = num21 * (1.0 - num15);
                                        num16 += num20;
                                        num17 += num22;
                                        if (j == (num14 - 1))
                                        {
                                            num6 += num8 + num16;
                                            num7 += num9 + num17;
                                        }
                                        tempSPXXModal[0] = dictionary7[0].ToString();
                                        tempSPXXModal[6] = dictionary7[6].ToString();
                                        tempSPXXModal[5] = dictionary7[5].ToString();
                                        tempSPXXModal[4] = dictionary7[4].ToString();
                                        this.MakePzEntryFun(tempSPXXModal, PZGroup, CusrID, InvKind, InvCode, InvNo, strYskm, num6, num7, true);
                                        if (dictionary7 != null)
                                        {
                                            dictionary7.Clear();
                                            dictionary7 = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    list3.Clear();
                }
                else
                {
                    flag = false;
                }
                if (list3 != null)
                {
                    list3.Clear();
                    list3 = null;
                }
                if (qdxx != null)
                {
                    qdxx.Clear();
                    qdxx = null;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return flag;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return flag;
            }
            return flag;
        }

        private List<Fpxx> SelectData()
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (this._FaPiaoFindTiaoJian.Che_gfmc.Checked)
                {
                    dict.Add("McMhBz", 1);
                    if (string.IsNullOrEmpty(this._FaPiaoFindTiaoJian.Txt_gfmc.Text.Trim()))
                    {
                        dict.Add("GFMC", "");
                    }
                    else
                    {
                        dict.Add("GFMC", "%" + this._FaPiaoFindTiaoJian.Txt_gfmc.Text.Trim() + "%");
                    }
                }
                else
                {
                    dict.Add("McMhBz", 0);
                    if (string.IsNullOrEmpty(this._FaPiaoFindTiaoJian.Txt_gfmc.Text.Trim()))
                    {
                        dict.Add("GFMC", "");
                    }
                    else
                    {
                        dict.Add("GFMC", this._FaPiaoFindTiaoJian.Txt_gfmc.Text.Trim());
                    }
                }
                if (this._FaPiaoFindTiaoJian.Che_gfsh.Checked)
                {
                    dict.Add("ShMhBz", 1);
                    if (string.IsNullOrEmpty(this._FaPiaoFindTiaoJian.Txt_gfsh.Text.Trim()))
                    {
                        dict.Add("GFSH", "");
                    }
                    else
                    {
                        dict.Add("GFSH", "%" + this._FaPiaoFindTiaoJian.Txt_gfsh.Text.Trim() + "%");
                    }
                }
                else
                {
                    dict.Add("ShMhBz", 0);
                    if (string.IsNullOrEmpty(this._FaPiaoFindTiaoJian.Txt_gfsh.Text.Trim()))
                    {
                        dict.Add("GFSH", "");
                    }
                    else
                    {
                        dict.Add("GFSH", this._FaPiaoFindTiaoJian.Txt_gfsh.Text.Trim());
                    }
                }
                TimeSpan span = new TimeSpan(0x18, 0, 0);
                DateTime time = this._FaPiaoFindTiaoJian.Data_ksrqValue;
                if (!this._FaPiaoFindTiaoJian.Che_ksrq.Checked)
                {
                    time += span;
                }
                dict.Add("KsRq", time.ToString("yyyy-MM-dd 00:00:00"));
                DateTime time2 = this._FaPiaoFindTiaoJian.Data_jsrqValue;
                if (!this._FaPiaoFindTiaoJian.Che_jsrq.Checked)
                {
                    time2 -= span;
                }
                dict.Add("JsRq", time2.ToString("yyyy-MM-dd 23:59:59"));
                int selectedIndex = this._FaPiaoFindTiaoJian.Com_fpzl.SelectedIndex;
                if (selectedIndex.Equals(0))
                {
                    dict.Add("FPZL_QbBz", 1);
                    dict.Add("FPZL", DingYiZhiFuChuan.FPZPZ_FpzlData[selectedIndex]);
                }
                else if ((0 < selectedIndex) && (4 > selectedIndex))
                {
                    dict.Add("FPZL_QbBz", 0);
                    dict.Add("FPZL", DingYiZhiFuChuan.FPZPZ_FpzlData[selectedIndex]);
                }
                string str = this._FaPiaoFindTiaoJian.Text_Fpdm.Text.Trim();
                dict.Add("FPDM", str);
                int result = 0;
                int.TryParse(this._FaPiaoFindTiaoJian.Text_Fphm_Q.Text.Trim(), out result);
                dict.Add("FPHM_Ks", result);
                int num3 = 0;
                int.TryParse(this._FaPiaoFindTiaoJian.Text_Fphm_Z.Text.Trim(), out num3);
                dict.Add("FPHM_Js", num3);
                double num4 = 0.0;
                double.TryParse(this._FaPiaoFindTiaoJian.Text_Fpje_Q.Text.Trim(), out num4);
                dict.Add("JE_Ks", num4);
                double num5 = 0.0;
                double.TryParse(this._FaPiaoFindTiaoJian.Text_Fpje_Z.Text.Trim(), out num5);
                dict.Add("JE_Js", num5);
                return this.xxfpBll.SelectPageXXFP(dict);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private string SetCombo_PingZhengType()
        {
            try
            {
                this.combo_PingZhengType.DropDownStyle = ComboBoxStyle.DropDownList;
                this.combo_PingZhengType.Items.Clear();
                this.listPZType.Clear();
                if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    string[] strArray = GetA6PZType(this._Loger);
                    if ((strArray != null) && (strArray.Length > 0))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            string item = strArray[i];
                            this.listPZType.Add(item);
                            this.combo_PingZhengType.Items.Add(item.Substring(item.IndexOf("-") + 1));
                        }
                        this.combo_PingZhengType.SelectedIndex = 0;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                return DingYiZhiFuChuan.strErrLinkFailTip;
            }
            return string.Empty;
        }

        private void SetGridColumnReadOnly_XZBZ()
        {
            try
            {
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].ReadOnly = false;
                this.customStyleDataGrid1.SetColumnReadOnly(DingYiZhiFuChuan.XXFPCulmnHeaderText[this.iIndexXzbz], false);
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

        private void SetGridColumnReadOnlyFenYe()
        {
        }

        private void SetZdTime()
        {
            try
            {
                this.dateTime_ZhiDanRiQi.Value = this._CardClock;
                this.dateTime_ZhiDanRiQi.MinDate = DingYiZhiFuChuan.MinDate;
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

        public static bool ShowBrowserFrm(List<string> pzInfoList, bool isModify, ILog _Loger)
        {
            try
            {
                if (DingYiZhiFuChuan.isA6PzVersion)
                {
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6ServerLink))
                    {
                        MessageManager.ShowMsgBox(DingYiZhiFuChuan.ServerEmpty);
                        return false;
                    }
                    if (string.IsNullOrEmpty(DingYiZhiFuChuan.A6SuitGuid) || string.IsNullOrEmpty(DingYiZhiFuChuan.A6UserGuid))
                    {
                        MessageManager.ShowMsgBox(DingYiZhiFuChuan.UserEmpty);
                        return false;
                    }
                    string str = DingYiZhiFuChuan.A6SuitGuid;
                    string str2 = DingYiZhiFuChuan.A6UserGuid;
                    str = str.Substring(0, str.IndexOf("="));
                    str2 = str2.Substring(0, str2.IndexOf("="));
                    if (0 < pzInfoList.Count)
                    {
                        string str3 = DingYiZhiFuChuan.A6ServerLink;
                        if (Aisino.Fwkp.Fpzpz.Common.Tool.IsA6Version())
                        {
                            str3 = str3 + DingYiZhiFuChuan.A6LinkZhiFuChuan_PingZheng;
                        }
                        else
                        {
                            str3 = str3 + "/gl/pz/pz.html?";
                        }
                        if (isModify)
                        {
                            string str5 = str3;
                            str3 = str5 + "act=modify&accountID=" + str + "&&userID=" + str2 + "&closeflag=smd&";
                        }
                        else
                        {
                            string str6 = str3;
                            str3 = str6 + "act=browser&accountID=" + str + "&&userID=" + str2 + "&closeflag=smd&";
                        }
                        str3 = str3 + "pzGUID=" + pzInfoList[0];
                        string str4 = string.Empty;
                        for (int i = 0; i < pzInfoList.Count; i++)
                        {
                            if (i == (pzInfoList.Count - 1))
                            {
                                str4 = str4 + pzInfoList[i];
                            }
                            else
                            {
                                str4 = str4 + pzInfoList[i] + ",";
                            }
                        }
                        PingZhengMsg msg = new PingZhengMsg(str3 + "&browseArr=" + str4);
                        msg.ShowDialog();
                        msg.Close();
                        msg.Dispose();
                        msg = null;
                    }
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        private void tool_DaYin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").Print("发票转凭证", this, null, null, true);
                this.Refresh();
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

        private void tool_Edit_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void tool_Edit_Click(object sender, EventArgs e)
        {
        }

        private void tool_FaPiao_Click(object sender, EventArgs e)
        {
            try
            {
                FaPiaoZhuanPingZhengFind.ChaKanMingXi(this.customStyleDataGrid1, DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm], DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm], DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz], DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl], this._Loger);
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

        private void tool_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == this._FaPiaoFindTiaoJian.ShowDialog())
                {
                    MessageHelper.MsgWait("正在查询的发票信息，请稍等......");
                    this.FlushGrid(FlushGridType.FindFlushGrid);
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
            finally
            {
                Thread.Sleep(100);
                MessageHelper.MsgWait();
            }
        }

        private void tool_GeShi_Click(object sender, EventArgs e)
        {
            try
            {
                this._Loger.Info("-----------发票转凭证格式调用开始-------------");
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
                this._Loger.Info("-----------发票转凭证格式调用结束-------------");
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

        private void tool_QuXiao_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.customStyleDataGrid1.Rows != null) && (this.customStyleDataGrid1.Rows.Count > 0))
                {
                    foreach (DataGridViewRow row in (IEnumerable) this.customStyleDataGrid1.Rows)
                    {
                        row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].Value = string.Empty;
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

        private void tool_QuXu_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.customStyleDataGrid1.Rows.Count > 0)
                {
                    int count = this.customStyleDataGrid1.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewRow row = this.customStyleDataGrid1.Rows[i];
                        row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].Value = Convert.ToString(1);
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

        private void tool_ShunXu_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.customStyleDataGrid1.Rows.Count > 0)
                {
                    int count = this.customStyleDataGrid1.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewRow row = this.customStyleDataGrid1.Rows[i];
                        row.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].Value = Convert.ToString((int) (i + 1));
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

        private void tool_ZhiDan_Click(object sender, EventArgs e)
        {
            try
            {
                this.listGoodsInfoList.Clear();
                this.listBuyerInfoList.Clear();
                int count = this.customStyleDataGrid1.Rows.Count;
                if (count > 0)
                {
                    if (this.combo_PingZhengType.SelectedItem == null)
                    {
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { "无法获取凭证类型！" });
                    }
                    else
                    {
                        string str = this.combo_PingZhengType.SelectedItem.ToString();
                        if (string.IsNullOrEmpty(str))
                        {
                            MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { "无法获取凭证类型！" });
                        }
                        else
                        {
                            bool flag = false;
                            foreach (string str2 in this.listPZType)
                            {
                                if (str2.Substring(str2.IndexOf("-") + 1).Equals(str))
                                {
                                    str = str2;
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { "无法获取所设置的凭证类型，请检查凭证类型是否正确！" });
                            }
                            else
                            {
                                int num2 = 0;
                                string str3 = string.Empty;
                                string str4 = string.Empty;
                                List<DataGridViewRow> listRowsZhiDan = new List<DataGridViewRow>();
                                for (int i = 0; i < count; i++)
                                {
                                    DataGridViewRow item = this.customStyleDataGrid1.Rows[i];
                                    string s = item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexXzbz]].Value.ToString();
                                    int result = 0;
                                    int.TryParse(s, out result);
                                    if (result > 0)
                                    {
                                        if (item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexZfbz]].Value.ToString().Equals("是"))
                                        {
                                            str3 = str3 + "FPDM:" + item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm]].Value.ToString() + "  FPHM:" + item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm]].Value.ToString() + "----已作废 \r\n";
                                        }
                                        if (this.xxfpBll.bIfMakedPZ(item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm]].Value.ToString(), item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm]].Value.ToString(), item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpzl]].Value.ToString()))
                                        {
                                            str4 = str4 + "FPDM:" + item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFpdm]].Value.ToString() + "  FPHM:" + item.Cells[DingYiZhiFuChuan.XXFPCulmnDataName[this.iIndexFphm]].Value.ToString() + "----已制凭证 \r\n";
                                        }
                                        listRowsZhiDan.Add(item);
                                        num2++;
                                    }
                                }
                                if (num2 <= 0)
                                {
                                    MessageManager.ShowMsgBox("FPCX-000006");
                                }
                                else
                                {
                                    string str6 = "所选中发票中包含已作废或者已制凭证的发票，请重新选择需要制凭证的发票！\r\n" + str3 + str4;
                                    if (!string.IsNullOrEmpty(str3) || !string.IsNullOrEmpty(str4))
                                    {
                                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str6 });
                                    }
                                    else
                                    {
                                        this.GetPropertyUtil();
                                        string str7 = str.Trim();
                                        int index = str.IndexOf("-");
                                        str = str.Substring(0, index);
                                        this._A6PzType = str;
                                        this._A6PzName = str7.Substring(str7.IndexOf("-") + 1, (str7.Length - index) - 1);
                                        Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("开始制单", this._Loger);
                                        if (!this.CheckBMProperty())
                                        {
                                            MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { "验证科目信息有误，请检查网络是否通畅！" });
                                        }
                                        else
                                        {
                                            if (this.MakePz(this._A6PzType, this._A6PzName, listRowsZhiDan))
                                            {
                                                this.bIsWLYW = false;
                                                this.sPZWLYWH = string.Empty;
                                                MessageHelper.MsgWait();
                                                if (DingYiZhiFuChuan.isA6PzVersion)
                                                {
                                                    ShowBrowserFrm(this.A6pzInfoList, true, this._Loger);
                                                }
                                            }
                                            MessageHelper.MsgWait();
                                            Aisino.Fwkp.Fpzpz.Common.Tool.writeLogfile("制单结束", this._Loger);
                                            this.FlushGrid(FlushGridType.ZhiDanFlushGrid);
                                        }
                                    }
                                }
                            }
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

        private bool UpdateWLYWH(string fpdm, string fphm, string fpzl, string wlywh)
        {
            bool flag2;
            try
            {
                DataRow[] rowArray = this.dtWLYWH.Select("FPDM='" + fpdm + "' and FPHM='" + fphm + "' and  fpzl='" + fpzl + "'");
                if ((rowArray == null) || (rowArray.Length < 1))
                {
                    return false;
                }
                DataRow[] rowArray2 = this.dtWLYWH.Select("SelectGroupID=" + rowArray[0]["SelectGroupID"].ToString());
                if ((rowArray2 == null) || (rowArray2.Length < 1))
                {
                    return false;
                }
                long result = 0L;
                long num2 = 0L;
                long.TryParse(wlywh, out num2);
                long.TryParse(rowArray2[0]["WLYWH"].ToString(), out result);
                if ((num2 != 0L) && ((result <= num2) || (result <= 0L)))
                {
                    for (int i = 0; i < rowArray2.Length; i++)
                    {
                        rowArray2[i]["WLYWH"] = wlywh;
                    }
                    this.dtWLYWH.AcceptChanges();
                }
                return true;
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return flag2;
        }

        private enum FlushGridType
        {
            FirstFlushGrid,
            FindFlushGrid,
            ZhiDanFlushGrid
        }
    }
}

