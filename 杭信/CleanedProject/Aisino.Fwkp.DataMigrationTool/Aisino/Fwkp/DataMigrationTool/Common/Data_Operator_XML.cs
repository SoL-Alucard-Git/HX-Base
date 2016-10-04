namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.DataMigrationTool.Form;
    using Aisino.Fwkp.DataMigrationTool.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal class Data_Operator_XML
    {
        private List<List<string>> _listSubTable = new List<List<string>>();
        private List<int> _listTableNum = new List<int>();
        private List<string> _listTableTypeAll = new List<string>();
        private List<XXFP_HXM> _listXXFP_HXM = new List<XXFP_HXM>();
        private ILog _Loger = LogUtil.GetLogger<Data_Operator_XML>();
        private DataMigrationToolForm _Parent;
        private string _strHXMXMLName = (Application.StartupPath + DingYiZhiFuChuan.strHXMXMLName);
        private string _strPathNameConfigureXML = string.Empty;
        private static int FirstSetFlag = 1;
        private static object Lock = new object();
        private static object LockTrans = new object();
        private static Mutex mutexTrans = new Mutex();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private static int threadNum = 0;

        public Data_Operator_XML(DataMigrationToolForm parent)
        {
            try
            {
                this._Parent = parent;
                string startupPath = Application.StartupPath;
                int length = startupPath.LastIndexOf('\\');
                startupPath = startupPath.Substring(0, length);
                this._strPathNameConfigureXML = startupPath + DingYiZhiFuChuan.strPathConfigure;
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void CreateEmptyKzyfXml(string strKzyf, string strPathKzyf)
        {
            try
            {
                if (File.Exists(strPathKzyf))
                {
                    File.Delete(strPathKzyf);
                }
                if (string.IsNullOrEmpty(strKzyf))
                {
                    strKzyf = this.taxCard.GetCardClock().ToString(DingYiZhiFuChuan.strYear_Month);
                }
                else
                {
                    int result = 0;
                    int.TryParse(strKzyf, out result);
                    if (result.Equals(0))
                    {
                        strKzyf = this.taxCard.GetCardClock().ToString(DingYiZhiFuChuan.strYear_Month);
                    }
                }
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlNode node = document.CreateElement("root");
                XmlElement element = document.CreateElement("kzyf");
                element.InnerText = strKzyf;
                node.AppendChild(element);
                document.AppendChild(node);
                document.Save(strPathKzyf);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public byte[] GetGFMC_GFSH_GFYHZH_GFDZDH_MW(string strFpzl, string strFpdm, string strFphm, string strType)
        {
            try
            {
                if (this._listXXFP_HXM == null)
                {
                    return null;
                }
                if (0 >= this._listXXFP_HXM.Count)
                {
                    return null;
                }
                foreach (XXFP_HXM xxfp_hxm in this._listXXFP_HXM)
                {
                    if ((xxfp_hxm.Fphm.Equals(strFphm) && xxfp_hxm.Fpdm.Equals(strFpdm)) && xxfp_hxm.Fpzl.Equals(strFpzl))
                    {
                        int length = 0;
                        string gfmc = string.Empty;
                        if (strType.Equals(DingYiZhiFuChuan.strGfmc))
                        {
                            gfmc = xxfp_hxm.Gfmc;
                        }
                        else if (strType.Equals(DingYiZhiFuChuan.strGfsh))
                        {
                            gfmc = xxfp_hxm.Gfsh;
                        }
                        else if (strType.Equals(DingYiZhiFuChuan.strGfdzdh))
                        {
                            gfmc = xxfp_hxm.Gfdzdh;
                        }
                        else if (strType.Equals(DingYiZhiFuChuan.strGfyhzh))
                        {
                            gfmc = xxfp_hxm.Gfyhzh;
                        }
                        else if (strType.Equals(DingYiZhiFuChuan.strMW))
                        {
                            gfmc = xxfp_hxm.Mw;
                        }
                        string[] strArray = gfmc.Split(new char[] { '+' });
                        length = strArray.Length;
                        if (length > 0)
                        {
                            byte[] buffer = new byte[length];
                            for (int i = 0; i < length; i++)
                            {
                                if (!string.IsNullOrEmpty(strArray[i]))
                                {
                                    buffer[i] = Convert.ToByte(strArray[i]);
                                }
                            }
                            if (0 >= buffer.Length)
                            {
                                return null;
                            }
                            return buffer;
                        }
                        return null;
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
            return null;
        }

        public string GetHXM(string strFpzl, string strFpdm, string strFphm, string strHxm)
        {
            try
            {
                if (string.IsNullOrEmpty(strFphm))
                {
                    return string.Empty;
                }
                if (this._listXXFP_HXM == null)
                {
                    return string.Empty;
                }
                if (0 >= this._listXXFP_HXM.Count)
                {
                    return string.Empty;
                }
                foreach (XXFP_HXM xxfp_hxm in this._listXXFP_HXM)
                {
                    if ((xxfp_hxm.Fphm.Equals(strFphm) && xxfp_hxm.Fpdm.Equals(strFpdm)) && xxfp_hxm.Fpzl.Equals(strFpzl))
                    {
                        string hxm = xxfp_hxm.Hxm;
                        this._listXXFP_HXM.Remove(xxfp_hxm);
                        return hxm;
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
            return string.Empty;
        }

        public void GetHXMList(string strTableName)
        {
            try
            {
                if (strTableName.Equals(DingYiZhiFuChuan.strOldXXFPName))
                {
                    this._Parent.UpdateCurTableStatus("正在构建销项发票数据");
                    XXFP_HXM_TO_XML(DingYiZhiFuChuan.strParadoxPath.ToCharArray());
                    this._listXXFP_HXM.Clear();
                    if (File.Exists(this._strHXMXMLName))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(this._strHXMXMLName);
                        foreach (XmlNode node in document.SelectSingleNode("root").ChildNodes)
                        {
                            if (XmlNodeType.Element == node.NodeType)
                            {
                                XmlElement element = (XmlElement) node;
                                XmlNodeList childNodes = element.ChildNodes;
                                XXFP_HXM item = new XXFP_HXM();
                                foreach (XmlNode node2 in childNodes)
                                {
                                    string innerText = node2.InnerText;
                                    if (node2.Name.Equals("fpzl"))
                                    {
                                        item.Fpzl = innerText;
                                    }
                                    else if (node2.Name.Equals("fphm"))
                                    {
                                        item.Fphm = innerText;
                                    }
                                    else if (node2.Name.Equals("fpdm"))
                                    {
                                        item.Fpdm = innerText;
                                    }
                                    else if (node2.Name.Equals("gfmc"))
                                    {
                                        item.Gfmc = innerText;
                                    }
                                    else if (node2.Name.Equals("gfsh"))
                                    {
                                        item.Gfsh = innerText;
                                    }
                                    else if (node2.Name.Equals("gfyhzh"))
                                    {
                                        item.Gfyhzh = innerText;
                                    }
                                    else if (node2.Name.Equals("gfdzdh"))
                                    {
                                        item.Gfdzdh = innerText;
                                    }
                                    else if (node2.Name.Equals("mw"))
                                    {
                                        item.Mw = innerText;
                                    }
                                    else if (node2.Name.Equals("hxm"))
                                    {
                                        item.Hxm = innerText;
                                    }
                                }
                                this._listXXFP_HXM.Add(item);
                            }
                        }
                        if (File.Exists(this._strHXMXMLName))
                        {
                            File.Delete(this._strHXMXMLName);
                        }
                    }
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public void GetTableTyp_TableNum()
        {
            try
            {
                if (this._listTableTypeAll == null)
                {
                    this._listTableTypeAll = new List<string>();
                }
                this._listTableTypeAll.Clear();
                if (this._listTableNum == null)
                {
                    this._listTableNum = new List<int>();
                }
                this._listTableNum.Clear();
                XmlDocument document = new XmlDocument();
                document.Load(this._strPathNameConfigureXML);
                foreach (XmlNode node2 in document.SelectSingleNode("root").SelectNodes("tableType"))
                {
                    string innerText = node2.Attributes.GetNamedItem("typeName").InnerText;
                    this._listTableTypeAll.Add(innerText);
                    XmlNodeList list2 = node2.SelectNodes("table");
                    this._listTableNum.Add(list2.Count);
                    List<string> item = new List<string>();
                    item.Clear();
                    foreach (XmlNode node3 in list2)
                    {
                        XmlNode node4 = node3.ChildNodes[3];
                        string str2 = node4.Attributes.GetNamedItem("tableNameCB").InnerText;
                        item.Add(str2);
                    }
                    this._listSubTable.Add(item);
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private bool InsertData(List<string> listFieldNameCB, List<string> listFieldNameC, string strTableNameCB, string strTableNameC, string strTablePathCB, bool IsOverlayMark, string strTypeName, List<int> listPrimaryKeyC, bool IsSameClass, List<string> listDefaultValue, bool IsFuGai)
        {
            GC.Collect();
            try
            {
                if (!DataMigrationToolForm._listExistTbNames.Contains(strTableNameCB))
                {
                    return true;
                }
                if (!IsOverlayMark)
                {
                    SQLiteHelper.GetScalar("delete from " + strTableNameC);
                }
                string str2 = string.Empty;
                foreach (int num in listPrimaryKeyC)
                {
                    int num2 = num;
                    num2--;
                    str2 = str2 + listFieldNameC[num2] + ",";
                }
                DataTable dataSet = SQLiteHelper.GetDataSet("select " + str2.Substring(0, str2.Length - 1) + " from " + strTableNameC);
                this.GetHXMList(strTableNameCB);
                string str3 = strTableNameCB;
                string safeSql = "select * from " + str3;
                strTablePathCB = DingYiZhiFuChuan.strParadoxPath + strTablePathCB;
                string connectionString = "Driver={Microsoft Paradox Driver (*.db )};DriverID=538;Fil=Paradox 5.X;DefaultDir=" + strTablePathCB + ";Dbq=" + strTablePathCB + ";CollatingSequence=ASCII;PWD=jIGGAe;";
                string path = strTablePathCB + str3 + ".DB";
                if (!File.Exists(path))
                {
                    this._Loger.Error(path + "不存在。");
                    return true;
                }
                ParadoxHelperNoStatic @static = new ParadoxHelperNoStatic();
                if (@static.ConnValue(connectionString) == null)
                {
                    this._Loger.Error("Paradox数据库连接失败!");
                    return false;
                }
                DataTable table2 = @static.GetDataSet(safeSql);
                @static.CloseConn();
                if (table2 == null)
                {
                    this._Loger.Error("Paradox数据库读取失败!");
                    return false;
                }
                DataColumnCollection cols = table2.Columns;
                DataRowCollection rows = table2.Rows;
                if (rows.Count == 0)
                {
                    this._Parent.UpdateCurTableStatus(strTableNameCB + " 0/0");
                    this._Parent.SetSelectTreeViewTableNode(strTableNameCB, true);
                    return true;
                }
                int num3 = 0;
                mutexTrans.WaitOne();
                using (SQLiteConnection connection = new SQLiteConnection(this._Parent.GetSQLiteConnString()))
                {
                    connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        object obj3;
                        SQLiteCommand command = new SQLiteCommand(connection);
                        command.set_Transaction(transaction);
                        try
                        {
                            foreach (DataRow row in rows)
                            {
                                if (strTableNameCB == "销项发票")
                                {
                                    string str8 = ShareMethod.getString(row[cols["类别代码"]]);
                                    string str9 = ShareMethod.getString(row[cols["发票号码"]]);
                                    if (SQLiteHelper.GetDataSet("select * from " + strTableNameC + " where FPDM='" + str8 + "' and FPHM='" + str9 + "'").Rows.Count != 0)
                                    {
                                        continue;
                                    }
                                }
                                if (((strTableNameCB != "商品编码") || !cols.Contains("商品种类")) || string.IsNullOrEmpty(ShareMethod.getString(row[cols["商品种类"]])))
                                {
                                    num3++;
                                    string str11 = string.Empty;
                                    string str12 = "0";
                                    string cnStr = string.Empty;
                                    List<string> list = new List<string>();
                                    string str14 = string.Empty;
                                    int result = 0;
                                    for (int i = 0; i < listFieldNameCB.Count; i++)
                                    {
                                        string strFieldName = listFieldNameCB[i];
                                        if (("销项发票" == strTableNameCB) && ("报送状态" == strFieldName))
                                        {
                                            list.Add("-1");
                                            continue;
                                        }
                                        if (("销项发票" == strTableNameCB) && ("报送日志" == strFieldName))
                                        {
                                            list.Add("【" + DateTime.Now.ToString() + "】旧版迁移数据，不参与报送！");
                                            continue;
                                        }
                                        string s = ShareMethod.GetValueString(row, cols, strFieldName, listDefaultValue[i]);
                                        if (strTableNameCB == "系统税务信息")
                                        {
                                            switch (strFieldName)
                                            {
                                                case "企业编号":
                                                    s = this.taxCard.get_TaxCode();
                                                    break;

                                                case "企业名称":
                                                    s = this.taxCard.get_Corporation();
                                                    break;

                                                case "注册类型":
                                                    s = this.taxCard.get_RegType();
                                                    break;

                                                case "开票限额":
                                                    if (this.taxCard.get_TaxMode() == 2)
                                                    {
                                                        s = this.taxCard.get_StateInfo().InvLimit.ToString();
                                                    }
                                                    else
                                                    {
                                                        s = "0";
                                                    }
                                                    break;

                                                case "报税日期":
                                                    s = this.taxCard.get_RepDate().ToString("yyyy-MM-dd HH:mm:ss");
                                                    break;

                                                case "报税软盘划分":
                                                    s = "0";
                                                    break;

                                                case "法人代表":
                                                    s = this.taxCard.get_CorpAgent();
                                                    break;

                                                case "简易征税":
                                                    s = this.taxCard.get_EasyLevy().ToString();
                                                    break;

                                                case "会计主管":
                                                    s = "";
                                                    break;
                                            }
                                        }
                                        if ((strTableNameCB == "销项发票明细") && (strFieldName == "含税价标志"))
                                        {
                                            string str17 = ShareMethod.getString(row[cols["商品名称"]]);
                                            if ((str17 == "(详见销货清单)") || str17.Contains("折扣"))
                                            {
                                                s = "0";
                                            }
                                        }
                                        if ((((strTableNameCB == "销售单据") || (strTableNameCB == "销售单据还原")) || ((strTableNameCB == "销售单据明细") || (strTableNameCB == "销售单据明细还原"))) && ((strFieldName == "类别代码") && (s == "0000000000")))
                                        {
                                            s = "";
                                        }
                                        if (strTableNameCB.Equals(DingYiZhiFuChuan.strOldXXFPName) && ((strFieldName.Equals("报税期") || strFieldName.Equals("选择标志")) || (strFieldName.Equals("凭证号码") || strFieldName.Equals("凭证状态"))))
                                        {
                                            int num6 = 0;
                                            int.TryParse(s, out num6);
                                            s = num6.ToString();
                                        }
                                        if (strFieldName.Equals(DingYiZhiFuChuan.strFphm))
                                        {
                                            int.TryParse(s, out result);
                                        }
                                        if (strFieldName.Equals(DingYiZhiFuChuan.strLbdm))
                                        {
                                            str14 = s;
                                        }
                                        if (((strFieldName.Equals(DingYiZhiFuChuan.strGfmc) || strFieldName.Equals(DingYiZhiFuChuan.strGfsh)) || ((strFieldName.Equals(DingYiZhiFuChuan.strGfdzdh) || strFieldName.Equals(DingYiZhiFuChuan.strGfyhzh)) || strFieldName.Equals(DingYiZhiFuChuan.strMW))) && strTableNameCB.Equals(DingYiZhiFuChuan.strOldXXFPName))
                                        {
                                            string strFpzl = list[0];
                                            string strFpdm = list[1];
                                            string strFphm = list[2];
                                            byte[] buffer = this.GetGFMC_GFSH_GFYHZH_GFDZDH_MW(strFpzl, strFpdm, strFphm, strFieldName);
                                            s = string.Empty;
                                            try
                                            {
                                                if (buffer != null)
                                                {
                                                    List<byte[]> list2 = new List<byte[]>();
                                                    List<string> list3 = new List<string> {
                                                        buffer
                                                    };
                                                    list3 = this.taxCard.XXFPDecrypt(str14, result, list2);
                                                    if ((list3 != null) && (0 <= list3.Count))
                                                    {
                                                        s = list3[0].Trim();
                                                    }
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                GC.Collect();
                                                this._Loger.Error(exception.Message);
                                                this._Loger.Error(string.Concat(new object[] { "迁移错误：", strTableNameCB, " ", result, " 已跳过" }));
                                                MessageBoxHelper.Show(string.Concat(new object[] { "解密失败：", strTableNameCB, "\n发票代码：", str14, "\n发票号码：", result, "\n已跳过此条记录！" }), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                                continue;
                                            }
                                        }
                                        if ((strTableNameCB == "销项发票") && ((strFieldName == "备注") || (strFieldName == "运输货物信息")))
                                        {
                                            s = Convert.ToBase64String(ToolUtil.GetBytes(s));
                                        }
                                        try
                                        {
                                            if (strFieldName.Equals(DingYiZhiFuChuan.strSsyf))
                                            {
                                                DateTime time = new DateTime(0x76b, 12, 30, 0x17, 0x3b, 0x3b);
                                                if ("销项发票" == strTableNameCB)
                                                {
                                                    time = DateTime.ParseExact(list[0x11], DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart, CultureInfo.InvariantCulture);
                                                }
                                                if (("红字发票申请单" == strTableNameCB) || ("货运红字发票申请单" == strTableNameCB))
                                                {
                                                    time = DateTime.ParseExact(list[11], DingYiZhiFuChuan.strYear_Month_Day_HHmmss_Formart, CultureInfo.InvariantCulture);
                                                }
                                                s = time.ToString(DingYiZhiFuChuan.strYear_Month);
                                            }
                                        }
                                        catch (Exception exception2)
                                        {
                                            this._Loger.Error(exception2.Message);
                                            strTableNameCB = "000000";
                                        }
                                        if ((((DingYiZhiFuChuan.strTableNameCB_SPBM == strTableNameCB) || (DingYiZhiFuChuan.strTableNameCB_KHBM == strTableNameCB)) || ((DingYiZhiFuChuan.strTableNameCB_SFHRBM == strTableNameCB) || (DingYiZhiFuChuan.strTableNameCB_FYXMBM == strTableNameCB))) || (((DingYiZhiFuChuan.strTableNameCB_GHDWBM == strTableNameCB) || (DingYiZhiFuChuan.strTableNameCB_CLBM == strTableNameCB)) || (DingYiZhiFuChuan.strTableNameCB_XHDWBM == strTableNameCB)))
                                        {
                                            int num7 = 0;
                                            int.TryParse(s, out num7);
                                            if (DingYiZhiFuChuan.strTableNameCB_BM == strFieldName)
                                            {
                                                str11 = s;
                                            }
                                            else if (DingYiZhiFuChuan.strTableNameCB_MC == strFieldName)
                                            {
                                                cnStr = s;
                                            }
                                            else if (DingYiZhiFuChuan.strTableNameCB_SJMC == strFieldName)
                                            {
                                                if (num7 < str11.Length)
                                                {
                                                    s = str11.Substring(0, num7);
                                                }
                                                else
                                                {
                                                    s = str11;
                                                }
                                            }
                                            else if (DingYiZhiFuChuan.strTableNameCB_XJMC == strFieldName)
                                            {
                                                str12 = (num7 == 0) ? "1" : "0";
                                                string[] spellCode = StringUtils.GetSpellCode(cnStr);
                                                s = string.Empty;
                                                foreach (string str21 in spellCode)
                                                {
                                                    s = s + str21;
                                                }
                                                if (10 < s.Length)
                                                {
                                                    s = s.Substring(0, 10);
                                                }
                                            }
                                            else if (DingYiZhiFuChuan.strTableNameCB_WJ == strFieldName)
                                            {
                                                s = str12;
                                            }
                                        }
                                        if ((((DingYiZhiFuChuan.strTableNameCB_SFHRBM != strTableNameCB) && (DingYiZhiFuChuan.strTableNameCB_FYXMBM != strTableNameCB)) && (((DingYiZhiFuChuan.strTableNameCB_GHDWBM != strTableNameCB) && (DingYiZhiFuChuan.strTableNameCB_CLBM != strTableNameCB)) && (DingYiZhiFuChuan.strTableNameCB_XHDWBM != strTableNameCB))) || (DingYiZhiFuChuan.strTableNameCB_XJMC != strFieldName))
                                        {
                                            if ((DingYiZhiFuChuan.strTableNameCB_SPBM == strTableNameCB) && strFieldName.Equals("海洋石油"))
                                            {
                                                double num8 = 0.0;
                                                double.TryParse(list[5], out num8);
                                                if ((0.05 == num8) && ("1" == list[10].ToString()))
                                                {
                                                    s = "1";
                                                }
                                                else
                                                {
                                                    s = "0";
                                                }
                                            }
                                            list.Add(s);
                                        }
                                    }
                                    string str22 = " where 1 = 1 ";
                                    string filterExpression = "1 = 1 ";
                                    foreach (int num9 in listPrimaryKeyC)
                                    {
                                        int num10 = num9;
                                        num10--;
                                        string str33 = str22;
                                        str22 = str33 + " and " + listFieldNameC[num10] + " = '" + list[num10] + "' ";
                                        str33 = filterExpression;
                                        filterExpression = str33 + " and " + listFieldNameC[num10] + " = '" + list[num10] + "' ";
                                    }
                                    string text1 = "select * from " + strTableNameC + str22;
                                    int length = dataSet.Select(filterExpression).Length;
                                    if (((0 < length) && IsFuGai) || !IsOverlayMark)
                                    {
                                        string str24 = "delete from " + strTableNameC + str22;
                                        command.CommandText = str24;
                                        command.ExecuteNonQuery();
                                        length = 0;
                                    }
                                    if (((0 >= length) || !IsOverlayMark) || (IsOverlayMark && IsFuGai))
                                    {
                                        string str25 = "insert into " + strTableNameC + "(";
                                        foreach (string str26 in listFieldNameC)
                                        {
                                            str25 = str25 + str26 + ",";
                                        }
                                        str25 = str25.Substring(0, str25.Length - 1) + ") values(";
                                        foreach (string str27 in listFieldNameC)
                                        {
                                            str25 = str25 + "@" + str27 + ",";
                                        }
                                        str25 = str25.Substring(0, str25.Length - 1) + ")";
                                        SQLiteParameter[] parameterArray = new SQLiteParameter[listFieldNameC.Count];
                                        for (int j = 0; j < list.Count; j++)
                                        {
                                            object obj2 = list[j];
                                            if (strTableNameCB.Equals(DingYiZhiFuChuan.strOldXXFPName) && j.Equals(0x2f))
                                            {
                                                string str28 = list[0];
                                                string str29 = list[1];
                                                string str30 = list[2];
                                                string strHxm = list[0x2f];
                                                obj2 = this.GetHXM(str28, str29, str30, strHxm);
                                            }
                                            parameterArray[j] = new SQLiteParameter(listFieldNameC[j], obj2);
                                        }
                                        command.CommandText = str25;
                                        command.get_Parameters().AddRange(parameterArray);
                                        command.ExecuteNonQuery();
                                        this._Parent.UpdateCurTableStatus(strTableNameCB + " " + num3.ToString() + "/" + rows.Count.ToString());
                                        if ((num3 % 0x3e8) == 0)
                                        {
                                            lock ((obj3 = LockTrans))
                                            {
                                                try
                                                {
                                                    this._Parent.UpdateCurTableStatus(strTableNameCB + " 正在写入数据库");
                                                    transaction.Commit();
                                                    GC.Collect();
                                                }
                                                catch (Exception exception3)
                                                {
                                                    transaction.Rollback();
                                                    GC.Collect();
                                                    this._Loger.Error(exception3.Message);
                                                    ExceptionHandler.HandleError(exception3);
                                                    return false;
                                                }
                                                transaction = connection.BeginTransaction();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception exception4)
                        {
                            GC.Collect();
                            this._Loger.Error(exception4.Message);
                            ExceptionHandler.HandleError(exception4);
                            return false;
                        }
                        finally
                        {
                            lock ((obj3 = LockTrans))
                            {
                                try
                                {
                                    this._Parent.SetSelectTreeViewTableNode(strTableNameCB, true);
                                    this._Parent.UpdateCurTableStatus(strTableNameCB + " 正在写入数据库");
                                    transaction.Commit();
                                    GC.Collect();
                                    string str32 = "数据库复制 " + strTableNameCB + " 到 " + strTableNameC + " " + rows.Count.ToString() + " 条数据。";
                                    this._Loger.Info(str32);
                                }
                                catch (Exception exception5)
                                {
                                    this._Parent.SetSelectTreeViewTableNode(strTableNameCB, false);
                                    this._Parent.UpdateCurTableStatus(strTableNameCB + " 正在回滚数据库");
                                    transaction.Rollback();
                                    GC.Collect();
                                    this._Loger.Error(exception5.Message);
                                    ExceptionHandler.HandleError(exception5);
                                }
                                finally
                                {
                                    mutexTrans.ReleaseMutex();
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseException exception6)
            {
                GC.Collect();
                this._Loger.Error(exception6.Message);
                ExceptionHandler.HandleError(exception6);
                return false;
            }
            catch (Exception exception7)
            {
                GC.Collect();
                this._Loger.Error(exception7.Message);
                if (exception7.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部数据库驱动程序 (12034) 中的意外错误。")
                {
                    MessageBoxHelper.Show("数据表读取错误：" + strTableNameCB + "\n请尝试使用旧版软件修复数据库。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else if (exception7.Message == "ERROR [HY000] [Microsoft][ODBC Paradox 驱动程序] 外部表不是预期的格式。")
                {
                    MessageBoxHelper.Show("数据表读取错误：" + strTableNameCB + "\n请尝试安装BDE数据库驱动程序。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                }
                else
                {
                    ExceptionHandler.HandleError(exception7);
                }
                this._Parent.SetSelectTreeViewTableNode(strTableNameCB, false);
                return false;
            }
            finally
            {
                GC.Collect();
                Interlocked.Increment(ref threadNum);
            }
            GC.Collect();
            return true;
        }

        public bool ReadXml(bool IsFuGai)
        {
            UpLoadCheckState.SetFpxfState(true);
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(this._strPathNameConfigureXML);
                XmlNodeList childNodes = document.SelectSingleNode("root").ChildNodes;
                int num = 0;
                foreach (XmlNode node in childNodes)
                {
                    if (XmlNodeType.Element == node.NodeType)
                    {
                        XmlElement element = (XmlElement) node;
                        XmlNodeList list2 = element.ChildNodes;
                        bool flag = false;
                        string innerText = element.Attributes.GetNamedItem("typeName").InnerText;
                        string str2 = string.Empty;
                        num++;
                        int num2 = 0;
                        if (innerText == "编码类表")
                        {
                            this._Parent.SetSelectTreeViewTopNode(innerText + "(总是清空)");
                        }
                        else
                        {
                            this._Parent.SetSelectTreeViewTopNode(innerText + "(选项有效)");
                        }
                        for (int i = 0; i < list2.Count; i++)
                        {
                            XmlNode node2 = list2[i];
                            if (XmlNodeType.Element == node2.NodeType)
                            {
                                XmlElement element2 = (XmlElement) node2;
                                if ("overlayMark" == element2.Name)
                                {
                                    flag = (element2.InnerText != "0") && !string.IsNullOrEmpty(element2.InnerText);
                                }
                                else if (element2.Name == "table")
                                {
                                    InsData data;
                                    num2++;
                                    string str3 = string.Empty;
                                    XmlNodeList list3 = element2.ChildNodes;
                                    List<string> list4 = new List<string>();
                                    List<string> list5 = new List<string>();
                                    List<string> list6 = new List<string>();
                                    List<int> list7 = new List<int>();
                                    string str4 = string.Empty;
                                    string str5 = string.Empty;
                                    for (int j = 0; j < list3.Count; j++)
                                    {
                                        XmlNode node3 = list3[j];
                                        if (XmlNodeType.Element == node3.NodeType)
                                        {
                                            if ("tablePathCB" == node3.Name)
                                            {
                                                str3 = node3.InnerText;
                                            }
                                            else if ("tableName" == node3.Name)
                                            {
                                                XmlAttributeCollection attributes = node3.Attributes;
                                                str4 = attributes.GetNamedItem("tableNameCB").InnerText;
                                                str5 = attributes.GetNamedItem("tableNameC").InnerText;
                                                XmlNodeList list8 = node3.ChildNodes;
                                                for (int k = 0; k < list8.Count; k++)
                                                {
                                                    XmlNode node4 = list8[k];
                                                    if ("fieldName" == node4.Name)
                                                    {
                                                        XmlAttributeCollection attributes2 = node4.Attributes;
                                                        string item = attributes2.GetNamedItem("fieldNameCB").InnerText;
                                                        if (item != "")
                                                        {
                                                            list4.Add(item);
                                                        }
                                                        string str7 = attributes2.GetNamedItem("fieldNameC").InnerText;
                                                        if (str7 != "")
                                                        {
                                                            list5.Add(str7);
                                                        }
                                                        string str8 = attributes2.GetNamedItem("defaultValue").InnerText;
                                                        list6.Add(str8);
                                                    }
                                                    else if ("PrimaryKey" == node4.Name)
                                                    {
                                                        foreach (XmlNode node5 in node4.ChildNodes)
                                                        {
                                                            list7.Add(Convert.ToInt16(node5.InnerText));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    GC.Collect();
                                    data.listFieldNameCB = list4;
                                    data.listFieldNameC = list5;
                                    data.strTableNameCB = str4;
                                    data.strTableNameC = str5;
                                    data.strTablePathCB = str3;
                                    data.IsOverlayMark = flag;
                                    data.strTypeName = innerText;
                                    data.listPrimaryKeyC = list7;
                                    data.IsSameClass = str2 == innerText;
                                    data.listDefaultValue = list6;
                                    data.IsFuGai = IsFuGai;
                                    data.iIndexTable = num;
                                    data.iPosition = num2;
                                    try
                                    {
                                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunInsertData), data);
                                        Interlocked.Decrement(ref threadNum);
                                    }
                                    catch (NotSupportedException exception)
                                    {
                                        this._Loger.Info("Not support ThreadPool method to migrate datas!");
                                        Thread thread = new Thread(new ParameterizedThreadStart(this.RunInsertData)) {
                                            Name = str4,
                                            IsBackground = false
                                        };
                                        thread.Start(data);
                                        thread.Join();
                                        ExceptionHandler.HandleError(exception);
                                    }
                                    catch (Exception exception2)
                                    {
                                        ExceptionHandler.HandleError(exception2);
                                    }
                                    GC.Collect();
                                    str2 = innerText;
                                }
                            }
                        }
                        while (threadNum != 0)
                        {
                            Thread.Sleep(100);
                        }
                        FirstSetFlag = 1;
                    }
                }
                if (DataMigrationToolForm._listExistTbNames.Contains("客户编码"))
                {
                    this._Parent.UpdateCurTableStatus("正在向客户编码添加地区编码");
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Fpzpz.InsertDQBM_To_KHBM", new object[] { true });
                }
                ServiceFactory.InvokePubService("Aisino.Fwkp.SetCorpInfo", new object[1]);
                this._Parent.UpdateCurTableStatus("正在调整客户编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_KH" });
                this._Parent.UpdateCurTableStatus("正在调整商品编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_SP" });
                this._Parent.UpdateCurTableStatus("正在调整收发货人编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_SFHR" });
                this._Parent.UpdateCurTableStatus("正在调整费用项目编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_FYXM" });
                this._Parent.UpdateCurTableStatus("正在调整购货单位编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_GHDW" });
                this._Parent.UpdateCurTableStatus("正在调整车辆编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_CL" });
                this._Parent.UpdateCurTableStatus("正在调整销货单位编码,请耐心等待");
                ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.AdjustTopBM", new object[] { "BM_XHDW" });
            }
            catch (BaseException exception3)
            {
                this._Loger.Error(exception3.Message);
                ExceptionHandler.HandleError(exception3);
                return false;
            }
            catch (Exception exception4)
            {
                this._Loger.Error(exception4.Message);
                ExceptionHandler.HandleError(exception4);
                return false;
            }
            finally
            {
                UpLoadCheckState.SetDataMigrationState(false);
            }
            return true;
        }

        public bool Run(bool IsFuGai)
        {
            try
            {
                return this.ReadXml(IsFuGai);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        public void RunInsertData(object obj)
        {
            try
            {
                InsData data = (InsData) obj;
                lock (Lock)
                {
                    if (FirstSetFlag == 1)
                    {
                        this._Parent.SetProgressBarPosition(data.iIndexTable, 0);
                        FirstSetFlag = 0;
                    }
                }
                this.InsertData(data.listFieldNameCB, data.listFieldNameC, data.strTableNameCB, data.strTableNameC, data.strTablePathCB, data.IsOverlayMark, data.strTypeName, data.listPrimaryKeyC, data.IsSameClass, data.listDefaultValue, data.IsFuGai);
                lock (Lock)
                {
                    this._Parent.SetProgressBarPosition(data.iIndexTable, 1);
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        [DllImport("XXFP_HXM_TO_XML.dll")]
        public static extern void XXFP_HXM_TO_XML(char[] path);

        public List<List<string>> ListSubTable
        {
            get
            {
                return this._listSubTable;
            }
            set
            {
                this._listSubTable = value;
            }
        }

        public List<int> listTableNum
        {
            get
            {
                return this._listTableNum;
            }
        }

        public List<string> listTableTypeAll
        {
            get
            {
                return this._listTableTypeAll;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct InsData
        {
            public List<string> listFieldNameCB;
            public List<string> listFieldNameC;
            public string strTableNameCB;
            public string strTableNameC;
            public string strTablePathCB;
            public bool IsOverlayMark;
            public string strTypeName;
            public List<int> listPrimaryKeyC;
            public bool IsSameClass;
            public List<string> listDefaultValue;
            public bool IsFuGai;
            public int iIndexTable;
            public int iPosition;
        }
    }
}

