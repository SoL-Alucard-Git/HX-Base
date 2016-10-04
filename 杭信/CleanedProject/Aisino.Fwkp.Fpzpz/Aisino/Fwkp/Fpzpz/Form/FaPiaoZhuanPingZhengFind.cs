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
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IBLL;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class FaPiaoZhuanPingZhengFind : DockForm
    {
        private int _AvePageNum = 10;
        private BaseDAOSQLite _BaseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog _Loger = LogUtil.GetLogger<FaPiaoZhuanPingZhengFind>();
        private PingZhengFindTiaoJian _PingZhengFindTiaoJian;
        private ProgressHelper _ProgressHelper;
        private bool bErr;
        private DataGridViewTextBoxColumn Column_Fphm;
        private DataGridViewTextBoxColumn Column_FpLbdm;
        private DataGridViewTextBoxColumn Column_Fpzl;
        private DataGridViewTextBoxColumn Column_Gfmc;
        private DataGridViewTextBoxColumn Column_Gfsh;
        private DataGridViewTextBoxColumn Column_Kprq;
        private DataGridViewTextBoxColumn Column_Pzhm;
        private DataGridViewTextBoxColumn Column_Pzlb;
        private DataGridViewTextBoxColumn Column_Pzrq;
        private DataGridViewTextBoxColumn Column_PZWLYWH;
        private DataGridViewTextBoxColumn Column_Pzywh;
        private DataGridViewTextBoxColumn Column_Pzzt;
        private DataGridViewTextBoxColumn Column_ZFBZ;
        private IContainer components;
        private CustomStyleDataGrid customStyleDataGrid1;
        private Aisino.Fwkp.Fpzpz.Common.Tool.FP_PZ fp_pz = Aisino.Fwkp.Fpzpz.Common.Tool.FP_PZ.PZFind;
        private int iIndexFPDM = 7;
        private int iIndexFPHM = 8;
        private int iIndexFPZL = 6;
        private int iIndexGFMC = 9;
        private int iIndexGFSH = 10;
        private int iIndexKPRQ = 5;
        private int iIndexPZHM = 2;
        private int iIndexPZLB = 1;
        private int iIndexPZRQ;
        private int iIndexPZYWH = 3;
        private int iIndexPZZT = 4;
        private int iIndexWLYWH = 12;
        private int iIndexZFBZ = 11;
        private Panel panel1;
        private ToolStripButton tool_ChaKan;
        private ToolStripButton tool_ChongXiao;
        private ToolStripButton tool_DaYin;
        private ToolStripButton tool_Delete;
        private ToolStripButton tool_FaPiao;
        private ToolStripButton tool_Find;
        private ToolStripButton tool_GeShi;
        private ToolStripButton tool_Quit;
        private ToolStripButton tool_Refresh;
        private ToolStripButton tool_XiuGai;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;
        private IXXFP xxfpBll = new XXFP();

        public FaPiaoZhuanPingZhengFind()
        {
            try
            {
                this.Initialize();
                ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
                this.toolStrip1.AutoSize = false;
                this.toolStrip1.Size = new Size(0x2d3, 30);
                this.panel1.Controls.Add(this.toolStrip1);
                this.toolStrip1.Dock = DockStyle.Bottom;
                this.SetCustomStyleDataGrid();
                if (!string.IsNullOrEmpty(Aisino.Fwkp.Fpzpz.Common.Tool.IsRightA6Info()))
                {
                    Aisino.Fwkp.Fpzpz.Common.Tool.PzInterFaceLinkInfo(DingYiZhiFuChuan.strErrLinkFailTip, this._Loger);
                    this.bErr = true;
                }
                else
                {
                    this.bErr = false;
                }
                this._PingZhengFindTiaoJian = new PingZhengFindTiaoJian(!this.bErr);
                this.FlushGrid(this.bErr, false);
                this._PingZhengFindTiaoJian.DataGrid = this.customStyleDataGrid1;
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

        private bool A6PZInfoCancel(string PzNo, string Pzlx, string pzNum)
        {
            try
            {
                if (string.IsNullOrEmpty(PzNo))
                {
                    return false;
                }
                string[] strArray = new string[] { pzNum, Pzlx };
                int result = -2;
                int.TryParse(pzNum, out result);
                if (result < 1)
                {
                    if (DialogResult.OK != MessageManager.ShowMsgBox("FPZPZ-000024", new string[] { "确定要冲销选中的凭证码？" }))
                    {
                        return false;
                    }
                }
                else if (DialogResult.OK != MessageManager.ShowMsgBox("FPZPZ-000013", strArray))
                {
                    return false;
                }
                string str = "SELECT * From  XXFP Where PZYWH='" + PzNo.Trim() + "'";
                ArrayList list = this._BaseDAOSQLite.querySQL(str);
                if ((list != null) && (0 < list.Count))
                {
                    Dictionary<string, object> dictionary = list[0] as Dictionary<string, object>;
                    string s = dictionary[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]].ToString();
                    int num2 = 0;
                    int.TryParse(s, out num2);
                    if (num2 != 2)
                    {
                        MessageManager.ShowMsgBox("FPZPZ-000014");
                        return false;
                    }
                }
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
                MessageHelper.MsgWait("正在冲销凭证，请稍等......");
                string str3 = DingYiZhiFuChuan.A6SuitGuid;
                string str4 = DingYiZhiFuChuan.A6UserGuid;
                str3 = str3.Substring(0, str3.IndexOf("="));
                str4 = str4.Substring(0, str4.IndexOf("="));
                string str5 = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                object obj2 = new object();
                string str6 = "redVoucher";
                string[] strArray2 = new string[] { str3, str4, PzNo };
                string str7 = (string) WebServiceFactory.InvokeWebService(str5, str6, strArray2);
                if (string.IsNullOrEmpty(str7))
                {
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000010");
                    return false;
                }
                string str8 = str7.Trim();
                string str9 = str8.Substring(0, 3);
                string str10 = string.Empty;
                int index = str8.IndexOf("_");
                if ((str9.Trim().ToUpper() == "ERR") && (index > 0))
                {
                    str10 = str8.Substring(3, 3);
                    if (str10.Trim() == "000")
                    {
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str8.Substring(7, str8.Length - 7) });
                        return false;
                    }
                    if ((str10.Trim() == "001") || (str10.Trim() == "002"))
                    {
                        string str11 = "Update XXFP set PZYWH='',PZWLYWH='',PZRQ='1899-12-30 23:59:59',PZZT='-1'";
                        str11 = (str11 + ", PZLB='', PZHM=-1") + " where PZYWH='" + PzNo.Trim() + "'";
                        this._BaseDAOSQLite.updateSQL(str11);
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str8.Substring(7, str8.Length - 7) });
                        return true;
                    }
                }
                if (PzNo.Trim() == str7.Trim())
                {
                    string str12 = "Update XXFP set PZYWH='',PZWLYWH='',PZRQ='1899-12-30 23:59:59',PZZT='-1'";
                    str12 = (str12 + ", PZLB='', PZHM=-1") + " where PZYWH='" + PzNo.Trim() + "'";
                    this._BaseDAOSQLite.updateSQL(str12);
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000015");
                }
                else
                {
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str7 });
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
                MessageHelper.MsgWait();
            }
            return true;
        }

        private bool A6PZInfoDel(string PzNo, string Pzlx, string pzNum)
        {
            try
            {
                if (string.IsNullOrEmpty(PzNo))
                {
                    return false;
                }
                string[] strArray = new string[] { pzNum, Pzlx };
                int result = -2;
                int.TryParse(pzNum, out result);
                if (result < 1)
                {
                    if (DialogResult.OK != MessageManager.ShowMsgBox("FPZPZ-000023", new string[] { "确定要删除选中的凭证吗？" }))
                    {
                        return false;
                    }
                }
                else if (DialogResult.OK != MessageManager.ShowMsgBox("FPZPZ-000011", strArray))
                {
                    return false;
                }
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
                MessageHelper.MsgWait("正在删除凭证，请稍等......");
                string str = DingYiZhiFuChuan.A6SuitGuid;
                string str2 = DingYiZhiFuChuan.A6UserGuid;
                str = str.Substring(0, str.IndexOf("="));
                str2 = str2.Substring(0, str2.IndexOf("="));
                string str3 = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                object obj2 = new object();
                string str4 = "deleteVoucher";
                string[] strArray2 = new string[] { str, str2, PzNo };
                string str5 = (string) WebServiceFactory.InvokeWebService(str3, str4, strArray2);
                if (string.IsNullOrEmpty(str5))
                {
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000019");
                    return false;
                }
                string str6 = str5.Trim();
                string str7 = str6.Substring(0, 3);
                string str8 = string.Empty;
                int index = str6.IndexOf("_");
                if ((str7.Trim().ToUpper() == "ERR") && (index > 0))
                {
                    str8 = str6.Substring(3, 3);
                    if (str8.Trim() == "000")
                    {
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str6.Substring(7, str6.Length - 7) });
                        return false;
                    }
                    if ((str8.Trim() == "001") || (str8.Trim() == "002"))
                    {
                        string str9 = "Update XXFP set PZYWH='',PZRQ='1899-12-30 23:59:59',PZZT='-1',PZWLYWH=''";
                        str9 = (str9 + ", PZLB='', PZHM=-1") + " where PZYWH='" + PzNo.Trim() + "'";
                        this._BaseDAOSQLite.updateSQL(str9);
                        MessageHelper.MsgWait();
                        MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str6.Substring(7, str6.Length - 7) });
                        return true;
                    }
                }
                if (PzNo.Trim() == str5.Trim())
                {
                    string str10 = "Update XXFP set PZYWH='',PZRQ='1899-12-30 23:59:59',PZZT='-1',PZWLYWH=''";
                    str10 = (str10 + ", PZLB='', PZHM=-1") + " where PZYWH='" + PzNo.Trim() + "'";
                    this._BaseDAOSQLite.updateSQL(str10);
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000012");
                }
                else
                {
                    MessageHelper.MsgWait();
                    MessageManager.ShowMsgBox("FPZPZ-000020", new string[] { str5 });
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
                MessageHelper.MsgWait();
            }
            return true;
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
        }

        public static bool ChaKanMingXi(CustomStyleDataGrid dataGrid, string fpdmZhiDuan, string fphmZhiDuan, string zfbzZhiDuan, string fpzlZhiDuan, ILog _Loger)
        {
            try
            {
                if (dataGrid == null)
                {
                    return false;
                }
                DataGridViewRowCollection rows = dataGrid.Rows;
                if (rows == null)
                {
                    return false;
                }
                if (rows.Count <= 0)
                {
                    return false;
                }
                if (dataGrid.CurrentCell == null)
                {
                    return false;
                }
                List<string[]> list = new List<string[]>();
                string str = Convert.ToString(dataGrid.CurrentCell.RowIndex);
                int num = 0;
                List<int> list2 = new List<int>();
                list2.Clear();
                foreach (DataGridViewRow row in (IEnumerable) rows)
                {
                    string str2 = Convert.ToString(row.Cells[fpdmZhiDuan].Value);
                    string s = Convert.ToString(row.Cells[fphmZhiDuan].Value);
                    string str4 = Convert.ToString(row.Cells[zfbzZhiDuan].Value);
                    int rowIndex = row.Cells[0].RowIndex;
                    int result = 0;
                    int.TryParse(s, out result);
                    s = Convert.ToString(result);
                    string str5 = (Convert.ToString(row.Cells[fpzlZhiDuan].Value) == DingYiZhiFuChuan.strFPZL[0]) ? "s" : "c";
                    str4 = (str4.Trim() == "是") ? "1" : "0";
                    string[] item = new string[] { str5, str2.ToString(), s.ToString(), str4, Convert.ToString(rowIndex) };
                    if (rowIndex == dataGrid.CurrentCell.RowIndex)
                    {
                        str = Convert.ToString((int) (dataGrid.CurrentCell.RowIndex - num));
                    }
                    if (!IsEmptyFPCurrentData(row.Cells[0]))
                    {
                        list2.Add(list.Count);
                        list.Add(item);
                    }
                    else
                    {
                        num++;
                        list2.Add(-1000);
                    }
                }
                if (list.Count <= 0)
                {
                    return false;
                }
                string str6 = "0";
                object[] objArray = new object[] { str6, str, list };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray);
                if (objArray2 == null)
                {
                    MessageManager.ShowMsgBox("INP-442211");
                    return false;
                }
                if (objArray2.Length < 2)
                {
                    MessageManager.ShowMsgBox("INP-442211");
                    return false;
                }
                int num4 = (int) objArray2[0];
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i] == num4)
                    {
                        num4 = i;
                        break;
                    }
                }
                dataGrid.Rows[num4].Selected = true;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }

        private void customStyleDataGrid1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.customStyleDataGrid1.Rows.Count > 0)
            {
                DataGridViewRow row = this.customStyleDataGrid1.Rows[e.RowIndex];
                if (row != null)
                {
                    string str = row.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]].Value.ToString();
                    if ((str.Equals("0") || str.Equals("3")) || (str.Equals("未审核") || str.Equals("暂存")))
                    {
                        this.tool_Delete.Enabled = !this.bErr;
                        this.tool_XiuGai.Enabled = !this.bErr;
                        this.tool_ChongXiao.Enabled = false;
                    }
                    else if (str.Equals("1") || str.Equals("已审核"))
                    {
                        this.tool_Delete.Enabled = false;
                        this.tool_XiuGai.Enabled = false;
                        this.tool_ChongXiao.Enabled = false;
                    }
                    else if (str.Equals("2") || str.Equals("已记账"))
                    {
                        this.tool_Delete.Enabled = false;
                        this.tool_XiuGai.Enabled = false;
                        this.tool_ChongXiao.Enabled = !this.bErr;
                    }
                    else
                    {
                        this.tool_Delete.Enabled = !this.bErr;
                        this.tool_XiuGai.Enabled = !this.bErr;
                        this.tool_ChongXiao.Enabled = !this.bErr;
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

        private void FlushGrid(bool bErr, bool bShowProgress)
        {
            try
            {
                List<Fpxx> listFpxx = this.SelectData();
                if (!bErr)
                {
                    this.PZInfoQry(listFpxx);
                    listFpxx = this.SelectData();
                }
                this.InsertDataToGrid(listFpxx);
                bool flag = this.customStyleDataGrid1.Rows.Count > 0;
                this.tool_Find.Enabled = true;
                if (flag)
                {
                    this.tool_FaPiao.Enabled = true;
                }
                else
                {
                    this.tool_FaPiao.Enabled = false;
                }
                this.tool_Delete.Enabled = !bErr && flag;
                this.tool_XiuGai.Enabled = !bErr && flag;
                this.tool_ChongXiao.Enabled = !bErr && flag;
                this.tool_ChaKan.Enabled = !bErr && flag;
                this.customStyleDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.customStyleDataGrid1.Focus();
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

        private bool GetA6PZList(List<string> PzList)
        {
            try
            {
                string str = string.Empty;
                if (0 >= PzList.Count)
                {
                    return false;
                }
                for (int i = 0; i < PzList.Count; i++)
                {
                    if (i == (PzList.Count - 1))
                    {
                        str = str + PzList[i];
                    }
                    else
                    {
                        str = str + PzList[i] + "#";
                    }
                }
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
                string str2 = DingYiZhiFuChuan.A6SuitGuid;
                str2 = str2.Substring(0, str2.IndexOf("="));
                string str3 = DingYiZhiFuChuan.A6ServerLink + "/pzWebService.ws";
                object obj2 = new object();
                string str4 = "getVoucherState";
                string[] strArray = new string[] { str2, str };
                string str5 = (string) WebServiceFactory.InvokeWebService(str3, str4, strArray);
                if (string.IsNullOrEmpty(str5))
                {
                    MessageManager.ShowMsgBox("FPZPZ-000010");
                    return false;
                }
                XmlDocument document = new XmlDocument();
                document.LoadXml(str5);
                if (!document.DocumentElement.Name.Equals("root"))
                {
                    return false;
                }
                XmlElement element = document.DocumentElement["state"];
                if (element == null)
                {
                    return false;
                }
                if (element.HasChildNodes)
                {
                    int index = 0;
                    XmlElement element2 = element["vou"];
                    while (index < element.ChildNodes.Count)
                    {
                        element2 = (XmlElement) element.ChildNodes.Item(index);
                        string innerText = element2.InnerText;
                        string attribute = element2.GetAttribute("guid");
                        string str8 = element2.GetAttribute("voutype");
                        string str9 = element2.GetAttribute("code");
                        string str10 = element2.GetAttribute("date");
                        string str11 = ("Update XXFP set PZZT=" + innerText.Trim()) + " ,PZLB='" + str8.Trim() + "'";
                        if (str9.Trim().Length > 0)
                        {
                            str11 = str11 + " ,PZHM=" + str9.Trim();
                        }
                        if (str10.Trim().Length > 0)
                        {
                            try
                            {
                                str11 = str11 + " ,PZRQ='" + str10.Trim() + "'";
                            }
                            catch (Exception)
                            {
                            }
                        }
                        str11 = str11 + " where PZYWH='" + attribute.Trim() + "'";
                        this._BaseDAOSQLite.updateSQL(str11);
                        index++;
                    }
                }
                else
                {
                    return false;
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
            return true;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.panel1 = this.xmlComponentLoader1.GetControlByName<Panel>("panel1");
            this.panel1.AutoSize = false;
            this.panel1.Size = new Size(0x2d3, 0x23);
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_Quit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Quit");
            this.tool_Find = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Find");
            this.tool_DaYin = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_DaYin");
            this.tool_GeShi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_GeShi");
            this.tool_Delete = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Delete");
            this.tool_XiuGai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_XiuGai");
            this.tool_ChaKan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ChaKan");
            this.tool_ChongXiao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ChongXiao");
            this.tool_Refresh = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Refresh");
            this.tool_FaPiao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_FaPiao");
            this.customStyleDataGrid1 = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1");
            this.customStyleDataGrid1.RowEnter += new DataGridViewCellEventHandler(this.customStyleDataGrid1_RowEnter);
            this.tool_Quit.Click += new EventHandler(this.tool_Quit_Click);
            this.tool_Find.Click += new EventHandler(this.tool_Find_Click);
            this.tool_DaYin.Click += new EventHandler(this.tool_DaYin_Click);
            this.tool_GeShi.Click += new EventHandler(this.tool_GeShi_Click);
            this.tool_Delete.Click += new EventHandler(this.tool_Delete_Click);
            this.tool_XiuGai.Click += new EventHandler(this.tool_XiuGai_Click);
            this.tool_ChaKan.Click += new EventHandler(this.tool_ChaKan_Click);
            this.tool_ChongXiao.Click += new EventHandler(this.tool_ChongXiao_Click);
            this.tool_Refresh.Click += new EventHandler(this.tool_Refresh_Click);
            this.tool_FaPiao.Click += new EventHandler(this.tool_FaPiao_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x299, 0x1ec);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fpzpz.Form.FaPiaoZhuanPingZhengFind\Aisino.Fwkp.Fpzpz.Form.FaPiaoZhuanPingZhengFind.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x299, 0x1ec);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FaPiaoZhuanPingZhengFind";
            base.StartPosition = FormStartPosition.CenterParent;
            base.set_TabText("发票转凭证查询");
            this.Text = "发票转凭证查询";
            base.ResumeLayout(false);
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
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH], typeof(string));
                    table.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ], typeof(string));
                    foreach (Fpxx fpxx in ListModel)
                    {
                        DataRow row = table.NewRow();
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ]];
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]];
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]];
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]];
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]];
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ]] = fpxx.kprq;
                        string str = string.Empty;
                        if (fpxx.fplx.Equals((FPLX) 2))
                        {
                            str = DingYiZhiFuChuan.strFPZL[1];
                        }
                        else if (fpxx.fplx.Equals((FPLX) 0))
                        {
                            str = DingYiZhiFuChuan.strFPZL[0];
                        }
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL]] = str;
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM]] = fpxx.fpdm;
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM]] = fpxx.fphm;
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC]] = fpxx.gfmc;
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH]] = fpxx.gfsh;
                        row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH]] = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH]];
                        if (fpxx.zfbz)
                        {
                            row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ]] = "是";
                        }
                        else
                        {
                            row[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ]] = "否";
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

        private void InsertGridColumn_FenYe()
        {
            try
            {
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

        public static bool IsEmptyFPCurrentData(DataGridViewCell CurrentCell)
        {
            try
            {
                if (CurrentCell == null)
                {
                    return true;
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        private bool PZInfoQry(List<Fpxx> listFpxx)
        {
            try
            {
                if (listFpxx == null)
                {
                    return false;
                }
                if (0 >= listFpxx.Count)
                {
                    return false;
                }
                List<string> pzList = new List<string>();
                for (int i = 0; i < listFpxx.Count; i++)
                {
                    Fpxx fpxx = listFpxx[i];
                    string str2 = fpxx.CustomFields[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].ToString();
                    bool flag = false;
                    if (!string.IsNullOrEmpty(str2))
                    {
                        for (int j = 0; j < pzList.Count; j++)
                        {
                            if (str2.Trim() == pzList[j].Trim())
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            pzList.Add(str2.Trim());
                        }
                    }
                }
                if (DingYiZhiFuChuan.isA6PzVersion && !this.GetA6PZList(pzList))
                {
                    return false;
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
            return true;
        }

        private List<Fpxx> SelectData()
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (this._PingZhengFindTiaoJian.Che_gfmc.Checked)
                {
                    if (string.IsNullOrEmpty(this._PingZhengFindTiaoJian.Txt_gfmc.Text.Trim()))
                    {
                        dict.Add("McMhBz", 1);
                        dict.Add("GFMC", "");
                    }
                    else
                    {
                        dict.Add("McMhBz", 1);
                        dict.Add("GFMC", "%" + this._PingZhengFindTiaoJian.Txt_gfmc.Text.Trim() + "%");
                    }
                }
                else
                {
                    dict.Add("McMhBz", 0);
                    dict.Add("GFMC", this._PingZhengFindTiaoJian.Txt_gfmc.Text.Trim());
                }
                if (this._PingZhengFindTiaoJian.Che_gfsh.Checked)
                {
                    if (string.IsNullOrEmpty(this._PingZhengFindTiaoJian.Txt_gfsh.Text.Trim()))
                    {
                        dict.Add("ShMhBz", 1);
                        dict.Add("GFSH", "");
                    }
                    else
                    {
                        dict.Add("ShMhBz", 1);
                        dict.Add("GFSH", "%" + this._PingZhengFindTiaoJian.Txt_gfsh.Text.Trim() + "%");
                    }
                }
                else
                {
                    dict.Add("ShMhBz", 0);
                    dict.Add("GFSH", this._PingZhengFindTiaoJian.Txt_gfsh.Text.Trim());
                }
                if (this._PingZhengFindTiaoJian.Radio_Kprq.Checked)
                {
                    dict.Add("KprqKprqBz", 1);
                }
                else
                {
                    dict.Add("KprqKprqBz", 0);
                }
                TimeSpan span = new TimeSpan(0x18, 0, 0);
                DateTime time = this._PingZhengFindTiaoJian.Data_ksrq;
                if (!this._PingZhengFindTiaoJian.Che_ksrq.Checked)
                {
                    time += span;
                }
                dict.Add("KsRq", time.ToString("yyyy-MM-dd 00:00:00"));
                DateTime time2 = this._PingZhengFindTiaoJian.Data_jsrq;
                if (!this._PingZhengFindTiaoJian.Che_jsrq.Checked)
                {
                    time2 -= span;
                }
                dict.Add("JsRq", time2.ToString("yyyy-MM-dd 23:59:59"));
                int selectedIndex = this._PingZhengFindTiaoJian.Com_fpzl.SelectedIndex;
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
                string str = this._PingZhengFindTiaoJian.Text_Fpdm.Text.Trim();
                dict.Add("FPDM", str);
                int result = 0;
                int.TryParse(this._PingZhengFindTiaoJian.Text_Fphm_Q.Text.Trim(), out result);
                dict.Add("FPHM_Ks", result);
                int num3 = 0;
                int.TryParse(this._PingZhengFindTiaoJian.Text_Fphm_Z.Text.Trim(), out num3);
                dict.Add("FPHM_Js", num3);
                if (this._PingZhengFindTiaoJian.Radio_Wzf.Checked)
                {
                    dict.Add("WzfBz", 0);
                }
                else
                {
                    dict.Add("WzfBz", 1);
                }
                int num4 = 0;
                int.TryParse(this._PingZhengFindTiaoJian.Text_Pzhm_Q.Text.Trim(), out num4);
                dict.Add("PZHM_Ks", num4);
                int num5 = 0;
                int.TryParse(this._PingZhengFindTiaoJian.Text_Pzhm_Z.Text.Trim(), out num5);
                dict.Add("PZHM_Js", num5);
                if (string.IsNullOrEmpty(this._PingZhengFindTiaoJian.Text_Wlywh.Text))
                {
                    dict.Add("PZWLYWH_Bz", "1");
                    dict.Add("PZWLYWH", "");
                }
                else if (this._PingZhengFindTiaoJian.Che_wlywh.Checked)
                {
                    dict.Add("PZWLYWH_Bz", "1");
                    dict.Add("PZWLYWH", "%" + this._PingZhengFindTiaoJian.Text_Wlywh.Text + "%");
                }
                else
                {
                    dict.Add("PZWLYWH_Bz", "0");
                    dict.Add("PZWLYWH", this._PingZhengFindTiaoJian.Text_Wlywh.Text);
                }
                if ((this._PingZhengFindTiaoJian.Combo_PzType.SelectedIndex == 0) || (-1 == this._PingZhengFindTiaoJian.Combo_PzType.SelectedIndex))
                {
                    dict.Add("PZLB", string.Empty);
                }
                else
                {
                    string selectedItem = (string) this._PingZhengFindTiaoJian.Combo_PzType.SelectedItem;
                    int index = selectedItem.IndexOf("-");
                    selectedItem = selectedItem.Substring(index + 1, (selectedItem.Length - index) - 1);
                    dict.Add("PZLB", selectedItem);
                }
                return this.xxfpBll.SelectPagePZXXFP(dict);
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
            return null;
        }

        private void SetCustomStyleDataGrid()
        {
            try
            {
                this.customStyleDataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.customStyleDataGrid1.MultiSelect = false;
                this.customStyleDataGrid1.AllowUserToAddRows = false;
                this.customStyleDataGrid1.Columns.Clear();
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM], "凭证号码");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ], "凭证日期");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZRQ];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB], "凭证类型");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH], "凭证往来业务号");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexWLYWH];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH], "凭证业务号");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].Visible = false;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT], "凭证状态");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZZT];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM], "发票代码");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM], "发票号码");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL], "发票种类");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ], "开票日期");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexKPRQ];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC], "购方名称");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFMC];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH], "购方税号");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexGFSH];
                this.customStyleDataGrid1.Columns.Add(DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ], "作废标志");
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ]].ReadOnly = true;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ]].Visible = false;
                this.customStyleDataGrid1.Columns[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ]].DataPropertyName = DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ];
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

        private void tool_ChaKan_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.customStyleDataGrid1.Rows.Count > 0) && (this.customStyleDataGrid1.SelectedRows.Count > 0))
                {
                    DataGridViewRow currentRow = this.customStyleDataGrid1.CurrentRow;
                    if (currentRow != null)
                    {
                        string str = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].Value.ToString();
                        List<string> pzInfoList = new List<string> {
                            str
                        };
                        if (DingYiZhiFuChuan.isA6PzVersion)
                        {
                            FaPiaoZhuanPingZheng.ShowBrowserFrm(pzInfoList, false, this._Loger);
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

        private void tool_ChongXiao_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.customStyleDataGrid1.Rows.Count > 0) && (this.customStyleDataGrid1.SelectedRows.Count > 0))
                {
                    DataGridViewRow currentRow = this.customStyleDataGrid1.CurrentRow;
                    if (currentRow != null)
                    {
                        string pzNo = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].Value.ToString();
                        string pzlx = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]].Value.ToString();
                        int result = 0;
                        int.TryParse(currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]].Value.ToString(), out result);
                        string pzNum = string.Format("{0:0000}", result);
                        if (DingYiZhiFuChuan.isA6PzVersion && this.A6PZInfoCancel(pzNo, pzlx, pzNum))
                        {
                            this.FlushGrid(this.bErr, true);
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

        private void tool_DaYin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").Print("发票转凭证查询", this, null, null, true);
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

        private void tool_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.customStyleDataGrid1.Rows.Count > 0) && (this.customStyleDataGrid1.SelectedRows.Count > 0))
                {
                    DataGridViewRow currentRow = this.customStyleDataGrid1.CurrentRow;
                    if (currentRow != null)
                    {
                        string pzNo = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].Value.ToString();
                        string pzlx = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZLB]].Value.ToString();
                        int result = 0;
                        int.TryParse(currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZHM]].Value.ToString(), out result);
                        string pzNum = string.Format("{0:0000}", result);
                        if (DingYiZhiFuChuan.isA6PzVersion && this.A6PZInfoDel(pzNo, pzlx, pzNum))
                        {
                            this.FlushGrid(this.bErr, true);
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
            finally
            {
                MessageHelper.MsgWait();
            }
        }

        private void tool_FaPiao_Click(object sender, EventArgs e)
        {
            try
            {
                ChaKanMingXi(this.customStyleDataGrid1, DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPDM], DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPHM], DingYiZhiFuChuan.PZCulmnDataName[this.iIndexZFBZ], DingYiZhiFuChuan.PZCulmnDataName[this.iIndexFPZL], this._Loger);
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
                if (DialogResult.OK == this._PingZhengFindTiaoJian.ShowDialog())
                {
                    MessageHelper.MsgWait("正在查询凭证信息，请稍等......");
                    this.FlushGrid(this.bErr, true);
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
                MessageHelper.MsgWait();
            }
        }

        private void tool_GeShi_Click(object sender, EventArgs e)
        {
            try
            {
                this._Loger.Info("-----------发票转凭证查找格式调用开始-------------");
                this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("customStyleDataGrid1").SetColumnStyles(this.xmlComponentLoader1.get_XMLPath(), this);
                this._Loger.Info("-----------发票转凭证查找格式调用结束-------------");
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

        private void tool_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Aisino.Fwkp.Fpzpz.Common.Tool.IsRightA6Info()))
                {
                    this.bErr = true;
                }
                else
                {
                    this.bErr = false;
                }
                this.FlushGrid(this.bErr, true);
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

        private void tool_XiuGai_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.customStyleDataGrid1.Rows.Count > 0) && (this.customStyleDataGrid1.SelectedRows.Count > 0))
                {
                    DataGridViewRow currentRow = this.customStyleDataGrid1.CurrentRow;
                    if (currentRow != null)
                    {
                        string str = currentRow.Cells[DingYiZhiFuChuan.PZCulmnDataName[this.iIndexPZYWH]].Value.ToString();
                        List<string> pzInfoList = new List<string> {
                            str
                        };
                        if (DingYiZhiFuChuan.isA6PzVersion && FaPiaoZhuanPingZheng.ShowBrowserFrm(pzInfoList, true, this._Loger))
                        {
                            this.FlushGrid(this.bErr, true);
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
    }
}

