namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using Microsoft.Office.Interop.Excel;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    public class FPExport : DockForm
    {
        private bool _isSPBMVersion = true;
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private int cancelX;
        private int cancelY;
        private int changeHeight;
        private AisinoCHK chkEnd;
        private AisinoCHK chkStart;
        private AisinoCMB cmbMonth;
        private AisinoCMB cmbPiaoZhong;
        private IContainer components;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private DateTime endDate = new DateTime(1, 1, 1);
        private int FormHeight;
        private long freeSpece;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBoxGFXX;
        private AisinoGRP groupBoxPiaoZhong;
        public bool isJDCOnly;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL lableGFMC;
        private AisinoLBL lableGFSH;
        private AisinoLBL lablePiaoZhong;
        private int lastSelectedMonth = -1;
        private ILog loger = LogUtil.GetLogger<FPExport>();
        private CommFun m_commFun = new CommFun();
        private List<InvTypeEntity> m_InvTypeEntityList = new List<InvTypeEntity>();
        private long needSpace;
        private bool nonSetChanged = true;
        private long offSetSpace = 0x100000L;
        private int okX;
        private int okY;
        private string preGfmcText = string.Empty;
        private string preGfshText = string.Empty;
        private FPProgressBar progressBar;
        public static bool QDFPFlag;
        private DateTime startDate = new DateTime(1, 1, 1);
        private AisinoTXT tbGFMC;
        private AisinoTXT tbGFSH;
        private XmlComponentLoader xmlComponentLoader1;

        public FPExport()
        {
            this.Initialize();
            base.Load += new EventHandler(this.FPExport_Load);
        }

        private void AutoWidth(int maxColumn, ref HSSFSheet sheet)
        {
            for (int i = 1; i < maxColumn; i++)
            {
                int num2 = sheet.GetColumnWidth(i) / 0x100;
                for (int j = 0; j < sheet.LastRowNum; j++)
                {
                    IRow row;
                    if (sheet.GetRow(j) == null)
                    {
                        row = sheet.CreateRow(j);
                    }
                    else
                    {
                        row = sheet.GetRow(j);
                    }
                    if (row.GetCell(i) != null)
                    {
                        int length = ToolUtil.GetBytes(row.GetCell(i).ToString()).Length;
                        if (num2 < length)
                        {
                            num2 = length;
                        }
                    }
                }
                sheet.SetColumnWidth(i, ((num2 + ((int) 0.72)) * 0x100) + 0x3e8);
            }
            sheet.SetColumnWidth(0, 0xdac);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog;
            string outPath;
            if (((this.cmbPiaoZhong.Items.Count < 1) || (this.cmbPiaoZhong.SelectedItem == null)) || string.IsNullOrEmpty(this.cmbPiaoZhong.SelectedItem.ToString()))
            {
                MessageManager.ShowMsgBox("INP-251516");
                return;
            }
            base.Visible = false;
            List<FPDetail> fPList = this.GetFPList();
            if (fPList != null)
            {
                if (fPList.Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-251501");
                    return;
                }
                string str = base.TaxCardInstance.get_TaxClock().ToString("yyyyMMdd");
                dialog = new SaveFileDialog();
                if (this.isJDCOnly)
                {
                    dialog.Filter = "Xml 文件 (*.XML)|*.xml";
                }
                else
                {
                    dialog.Filter = "Excel 文件 (*.XLS)|*.xls|Xml 文件 (*.XML)|*.xml";
                }
                if (this.cmbPiaoZhong.SelectedItem.ToString() == "增值税专用、普通发票")
                {
                    if (QDFPFlag)
                    {
                        dialog.FileName = "增值税专普清单发票数据导出" + str;
                    }
                    else
                    {
                        dialog.FileName = "增值税专普发票数据导出" + str;
                    }
                }
                else if (this.cmbPiaoZhong.SelectedItem.ToString() == "货物运输业增值税专用发票")
                {
                    dialog.FileName = "货物运输业增值税专用发票数据导出" + str;
                }
                else if (this.cmbPiaoZhong.SelectedItem.ToString() == "机动车销售统一发票")
                {
                    dialog.FileName = "机动车销售统一发票数据导出" + str;
                }
                outPath = string.Empty;
                if (!this.isJDCOnly)
                {
                    goto Label_0368;
                }
                string str3 = string.Empty;
                FPExportJDCPath path = new FPExportJDCPath {
                    StartPosition = FormStartPosition.CenterScreen
                };
                DialogResult result = path.ShowDialog();
                if (DialogResult.OK == result)
                {
                    outPath = path.OutPath;
                    string str4 = "taxML_JDCXSFP_V1.1";
                    string str5 = base.TaxCardInstance.get_TaxCode();
                    if ((base.TaxCardInstance.get_TaxCode().Length == 15) && "DK".Equals(base.TaxCardInstance.get_TaxCode().Substring(8, 2)))
                    {
                        str5 = "000000000000000";
                    }
                    string hYBM = "";
                    if (fPList.Count > 0)
                    {
                        FPDetail detail = fPList[0];
                        hYBM = detail.HYBM;
                    }
                    string str7 = base.TaxCardInstance.get_TaxClock().ToString("yyyyMMdd");
                    string str8 = "";
                    string str9 = "";
                    if (fPList.Count > 1)
                    {
                        str8 = "000000000000";
                        str9 = "00000000";
                    }
                    else if (fPList.Count == 1)
                    {
                        str8 = string.Format("{0:000000000000}", fPList[0].FPDM);
                        str9 = string.Format("{0:00000000}", fPList[0].FPHM);
                    }
                    int num = 0;
                    string par = str5 + "_" + hYBM + "_" + str7;
                    num = this.checkJDCXMLPath(outPath, par);
                    if (num >= 100)
                    {
                        base.Close();
                        return;
                    }
                    str3 = str4 + "_" + str5 + "_" + hYBM + "_" + str7 + "_" + str8 + "_" + str9 + "_" + num.ToString("D2") + ".xml";
                    dialog.FileName = str3;
                    goto Label_0368;
                }
                base.Close();
            }
            return;
        Label_0368:
            if (this.isJDCOnly)
            {
                if (this.progressBar == null)
                {
                    this.progressBar = new FPProgressBar();
                }
                string title = "车购税申报数据导出";
                this.progressBar.fpxf_progressBar.Value = 1;
                this.progressBar.SetTip(title, "请等待任务完成", title);
                this.progressBar.Show();
                this.progressBar.Refresh();
                this.ProcessStartThread(0xfa0);
                this.progressBar.Refresh();
                string file = Path.Combine(outPath, dialog.FileName);
                if (this.ExportToXmlJDC1_2(fPList, file))
                {
                    this.ProcessStartThread(0xfa0);
                    this.progressBar.Refresh();
                    MessageManager.ShowMsgBox("INP-251511");
                }
                else
                {
                    if (this.needSpace > (this.freeSpece - this.offSetSpace))
                    {
                        File.Delete(file);
                        MessageManager.ShowMsgBox("INP-251513");
                        if (this.progressBar != null)
                        {
                            this.progressBar.Close();
                            this.progressBar = null;
                        }
                        base.Close();
                        return;
                    }
                    this.ProcessStartThread(0xfa0);
                    this.progressBar.Refresh();
                    MessageManager.ShowMsgBox("INP-251512");
                }
                if (this.progressBar != null)
                {
                    this.progressBar.Close();
                    this.progressBar = null;
                }
                base.Close();
            }
            else
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DriveInfo info = new DriveInfo(dialog.FileName.Substring(0, 3));
                    this.freeSpece = info.AvailableFreeSpace;
                    this.needSpace = 0L;
                    string str14 = "发票数据导出";
                    if (QDFPFlag)
                    {
                        str14 = "清单发票数据导出";
                    }
                    if (this.progressBar == null)
                    {
                        this.progressBar = new FPProgressBar();
                    }
                    this.progressBar.fpxf_progressBar.Value = 1;
                    this.progressBar.SetTip(str14, "请等待任务完成", str14);
                    this.progressBar.Show();
                    this.progressBar.Refresh();
                    this.ProcessStartThread(0xfa0);
                    this.progressBar.Refresh();
                    string fileName = dialog.FileName;
                    if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
                    {
                        this.ProcessStartThread(0x7d0);
                        this.progressBar.Refresh();
                        if (dialog.FilterIndex == 2)
                        {
                            if (this.ExportToXmlHWYS(fPList, fileName))
                            {
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251511");
                            }
                            else
                            {
                                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                                {
                                    File.Delete(fileName);
                                    MessageManager.ShowMsgBox("INP-251513");
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar = null;
                                    }
                                    base.Close();
                                    return;
                                }
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251512");
                            }
                        }
                        if (dialog.FilterIndex == 1)
                        {
                            if (this.ExportToExcelHWYS_(fPList, fileName))
                            {
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251511");
                            }
                            else
                            {
                                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                                {
                                    File.Delete(fileName);
                                    MessageManager.ShowMsgBox("INP-251513");
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar = null;
                                    }
                                    base.Close();
                                    return;
                                }
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251512");
                            }
                        }
                        if (this.progressBar != null)
                        {
                            this.progressBar.Close();
                            this.progressBar = null;
                        }
                        base.Close();
                        return;
                    }
                    if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
                    {
                        this.ProcessStartThread(0x7d0);
                        this.progressBar.Refresh();
                        if (dialog.FilterIndex == 2)
                        {
                            if (this.ExportToXmlJDC1_1(fPList, fileName))
                            {
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251511");
                            }
                            else
                            {
                                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                                {
                                    File.Delete(fileName);
                                    MessageManager.ShowMsgBox("INP-251513");
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar = null;
                                    }
                                    base.Close();
                                    return;
                                }
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251512");
                            }
                        }
                        if (dialog.FilterIndex == 1)
                        {
                            if (this.ExportToExcelJDC_(fPList, fileName))
                            {
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251511");
                            }
                            else
                            {
                                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                                {
                                    File.Delete(fileName);
                                    MessageManager.ShowMsgBox("INP-251513");
                                    if (this.progressBar != null)
                                    {
                                        this.progressBar.Close();
                                        this.progressBar = null;
                                    }
                                    base.Close();
                                    return;
                                }
                                this.ProcessStartThread(0xfa0);
                                this.progressBar.Refresh();
                                MessageManager.ShowMsgBox("INP-251512");
                            }
                        }
                        if (this.progressBar != null)
                        {
                            this.progressBar.Close();
                            this.progressBar = null;
                        }
                        base.Close();
                        return;
                    }
                    if (dialog.FilterIndex == 1)
                    {
                        this.ProcessStartThread(0x7d0);
                        this.progressBar.Refresh();
                        if (this.ExportToExcel_(fPList, fileName))
                        {
                            this.ProcessStartThread(0xfa0);
                            this.progressBar.Refresh();
                            MessageManager.ShowMsgBox("INP-251511");
                        }
                        else
                        {
                            if (this.needSpace > (this.freeSpece - this.offSetSpace))
                            {
                                File.Delete(fileName);
                                MessageManager.ShowMsgBox("INP-251513");
                                if (this.progressBar != null)
                                {
                                    this.progressBar.Close();
                                    this.progressBar = null;
                                }
                                base.Close();
                                return;
                            }
                            this.ProcessStartThread(0xfa0);
                            this.progressBar.Refresh();
                            MessageManager.ShowMsgBox("INP-251512");
                        }
                    }
                    if (dialog.FilterIndex == 2)
                    {
                        this.ProcessStartThread(0x7d0);
                        this.progressBar.Refresh();
                        if (this.ExportToXml(fPList, fileName))
                        {
                            this.ProcessStartThread(0xfa0);
                            this.progressBar.Refresh();
                            MessageManager.ShowMsgBox("INP-251511");
                        }
                        else
                        {
                            if (this.needSpace > (this.freeSpece - this.offSetSpace))
                            {
                                File.Delete(fileName);
                                MessageManager.ShowMsgBox("INP-251513");
                                if (this.progressBar != null)
                                {
                                    this.progressBar.Close();
                                    this.progressBar = null;
                                }
                                base.Close();
                                return;
                            }
                            this.ProcessStartThread(0xfa0);
                            this.progressBar.Refresh();
                            MessageManager.ShowMsgBox("INP-251512");
                        }
                    }
                    if (this.progressBar != null)
                    {
                        this.progressBar.Close();
                        this.progressBar = null;
                    }
                    this.Refresh();
                }
                base.Close();
            }
        }

        private int checkJDCXMLPath(string outPath, string par)
        {
            int num = 1;
            foreach (string str in Directory.GetFiles(outPath, "*.xml", SearchOption.TopDirectoryOnly))
            {
                if (str.Contains(par))
                {
                    num++;
                }
            }
            if (num >= 100)
            {
                MessageManager.ShowMsgBox("本日机动车销售统一发票导出次数过多！");
            }
            return num;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.nonSetChanged)
            {
                this.FillTimer_();
            }
        }

        private void cmbPiaoZhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            int height = base.Height;
            if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专用、普通发票"))
            {
                this.tbGFMC.Enabled = true;
                this.tbGFSH.Enabled = true;
                if (base.Height != this.FormHeight)
                {
                    base.Height = this.FormHeight;
                    this.groupBoxGFXX.Visible = true;
                    this.btnOK.Location = new Point(this.okX, this.okY);
                    this.btnCancel.Location = new Point(this.cancelX, this.cancelY);
                }
            }
            if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票") || this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
            {
                this.tbGFMC.Clear();
                this.tbGFSH.Clear();
                this.tbGFMC.Enabled = false;
                this.tbGFSH.Enabled = false;
                base.Height = this.FormHeight - this.changeHeight;
                this.groupBoxGFXX.Visible = false;
                this.btnOK.Location = new Point(this.okX, this.okY - this.changeHeight);
                this.btnCancel.Location = new Point(this.cancelX, this.cancelY - this.changeHeight);
            }
        }

        public void ConvertToExcel(string sFilePath, string sheetName, DataTable dataTable)
        {
            int count = dataTable.Rows.Count;
            int num2 = dataTable.Columns.Count;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            try
            {
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet(sheetName);
                HSSFFont font = (HSSFFont) workbook.CreateFont();
                font.FontName = "黑体";
                font.FontHeightInPoints = 0x16;
                for (int i = 0; i <= (num2 - 1); i++)
                {
                    row.CreateCell(i).SetCellValue(dataTable.Columns[i].ColumnName.Trim());
                }
                for (int j = 0; j <= (count - 1); j++)
                {
                    row = sheet.CreateRow(j + 1);
                    for (int k = 0; k < num2; k++)
                    {
                        try
                        {
                            row.CreateCell(k).SetCellValue(dataTable.Rows[j][k].ToString().Trim());
                        }
                        catch
                        {
                        }
                    }
                }
                FileStream stream = new FileStream(sFilePath, FileMode.Create);
                workbook.Write(stream);
                stream.Close();
            }
            catch (Exception)
            {
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

        private bool ExportToExcel(List<FPDetail> fpList, string file)
        {
            int rowNo = 1;
            Font textFont = new Font("宋体", 16f, FontStyle.Bold);
            Font contentFont = new Font("宋体", 9f);
            Font font3 = new Font("宋体", 12f);
            string text = base.TaxCardInstance.get_TaxCode() + "发票数据";
            string str2 = string.Format("资料区间：{0} ~ {1}", this.dtpStart.Value.ToString("yyyy-MM-dd"), this.dtpEnd.Value.ToString("yyyy-MM-dd"));
            string[] colNames = new string[] { "发票代码", "发票号码", "购方企业名称", "购方税号", "银行账号", "地址电话", "开票日期", "单据号", "商品名称", "规格", "单位", "数量", "单价", "金额", "税率", "税额" };
            string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
            string subTotalText = "小计";
            List<FPDetail> sfpList = new List<FPDetail>();
            List<FPDetail> list2 = new List<FPDetail>();
            foreach (FPDetail detail in fpList)
            {
                if (detail.FPType == FPType.c)
                {
                    sfpList.Add(detail);
                }
                else if (detail.FPType == FPType.s)
                {
                    list2.Add(detail);
                }
            }
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                object obj2 = Missing.Value;
                ApplicationClass class2 = new ApplicationClass();
                Workbook workbook = class2.get_Workbooks().Add(obj2);
                class2.set_DisplayAlerts(false);
                class2.set_Visible(false);
                Worksheet sheet = workbook.get_Worksheets().Add(obj2, obj2, 1, obj2) as Worksheet;
                SetMergeCellValue(sheet, text, 2, colNames.Length, textFont, true, -4108);
                SetMergeCellValue(sheet, str2, 4, 4, font3, false, -4131);
                rowNo = 5;
                if (list2.Count > 0)
                {
                    SetMergeCellValue(sheet, "发票类别：专用发票", rowNo, colNames.Length, textFont, true, -4131);
                    rowNo++;
                    rowNo = this.SetCellContent(sheet, list2, rowNo, font3, contentFont, colNames, totalText, subTotalText) + 1;
                }
                if (sfpList.Count > 0)
                {
                    SetMergeCellValue(sheet, "发票类别：普通发票", rowNo, colNames.Length, textFont, true, -4131);
                    rowNo++;
                    rowNo = this.SetCellContent(sheet, sfpList, rowNo, font3, contentFont, colNames, totalText, subTotalText) + 1;
                }
                workbook.SaveAs(file, (XlFileFormat) (-4143), obj2, obj2, obj2, obj2, 1, obj2, obj2, obj2, obj2, obj2);
                workbook.Close(false, obj2, obj2);
                class2.Quit();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return false;
        }

        private bool ExportToExcel_(List<FPDetail> fpList, string file)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = null;
                sheet = (HSSFSheet) workbook.CreateSheet("sheet1");
                sheet.AutoSizeColumn(1, true);
                HSSFCellStyle style = (HSSFCellStyle) workbook.CreateCellStyle();
                style.FillPattern = FillPattern.Squares;
                HSSFFont font = (HSSFFont) workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 0x10;
                font.Boldweight = 700;
                HSSFFont font2 = (HSSFFont) workbook.CreateFont();
                font2.FontName = "宋体";
                font2.FontHeightInPoints = 9;
                HSSFFont font3 = (HSSFFont) workbook.CreateFont();
                font3.FontName = "宋体";
                font3.FontHeightInPoints = 12;
                HSSFCellStyle style2 = (HSSFCellStyle) workbook.CreateCellStyle();
                style2.SetFont(font);
                style2.Alignment = HorizontalAlignment.Center;
                style2.VerticalAlignment = VerticalAlignment.Center;
                style2.IsLocked = true;
                HSSFCellStyle style3 = (HSSFCellStyle) workbook.CreateCellStyle();
                style3.SetFont(font);
                style3.Alignment = HorizontalAlignment.Left;
                style3.VerticalAlignment = VerticalAlignment.Center;
                style3.IsLocked = true;
                HSSFCellStyle blackStyle = (HSSFCellStyle) workbook.CreateCellStyle();
                blackStyle.FillForegroundColor = 0x16;
                blackStyle.Alignment = HorizontalAlignment.Center;
                blackStyle.FillPattern = FillPattern.SolidForeground;
                blackStyle.SetFont(font2);
                HSSFCellStyle contentstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                contentstyle.SetFont(font2);
                HSSFCellStyle otherstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                otherstyle.SetFont(font3);
                string str = base.TaxCardInstance.get_Corporation() + "发票数据";
                string str2 = string.Format("资料区间：{0} ~ {1}", this.startDate.ToString("yyyy-MM-dd"), this.endDate.ToString("yyyy-MM-dd"));
                string[] colNames = new string[] { 
                    "发票代码", "发票号码", "购方企业名称", "购方税号", "银行账号", "地址电话", "开票日期", "商品编码版本号", "单据号", "商品名称", "规格", "单位", "数量", "单价", "金额", "税率", 
                    "税额", "税收分类编码"
                 };
                if (!this._isSPBMVersion)
                {
                    colNames = new string[] { "发票代码", "发票号码", "购方企业名称", "购方税号", "银行账号", "地址电话", "开票日期", "单据号", "商品名称", "规格", "单位", "数量", "单价", "金额", "税率", "税额" };
                }
                string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
                string subTotalText = "小计";
                List<FPDetail> sfpList = new List<FPDetail>();
                List<FPDetail> list2 = new List<FPDetail>();
                foreach (FPDetail detail in fpList)
                {
                    if (detail.FPType == FPType.c)
                    {
                        sfpList.Add(detail);
                    }
                    else if (detail.FPType == FPType.s)
                    {
                        list2.Add(detail);
                    }
                }
                HSSFRow row = (HSSFRow) sheet.CreateRow(1);
                HSSFCell cell = (HSSFCell) row.CreateCell(0);
                cell.SetCellValue(new HSSFRichTextString(str));
                row.Height = 500;
                cell.CellStyle = style2;
                CellRangeAddress region = new CellRangeAddress(1, 1, 0, colNames.Length - 1);
                sheet.AddMergedRegion(region);
                row = (HSSFRow) sheet.CreateRow(3);
                cell = (HSSFCell) row.CreateCell(0);
                cell.SetCellValue(new HSSFRichTextString(str2));
                cell.CellStyle = otherstyle;
                region = new CellRangeAddress(3, 3, 0, 3);
                sheet.AddMergedRegion(region);
                int firstRow = 4;
                if (list2.Count > 0)
                {
                    row = (HSSFRow) sheet.CreateRow(4);
                    cell = (HSSFCell) row.CreateCell(0);
                    cell.SetCellValue(new HSSFRichTextString("发票类别：专用发票"));
                    row.Height = 500;
                    cell.CellStyle = style3;
                    region = new CellRangeAddress(firstRow, firstRow, 0, colNames.Length - 1);
                    sheet.AddMergedRegion(region);
                    firstRow++;
                    firstRow = this.SetCellContent_(sheet, list2, firstRow, colNames, totalText, subTotalText, contentstyle, otherstyle, blackStyle) + 1;
                }
                if (sfpList.Count > 0)
                {
                    row = (HSSFRow) sheet.CreateRow(firstRow);
                    cell = (HSSFCell) row.CreateCell(0);
                    cell.SetCellValue(new HSSFRichTextString("发票类别：普通发票"));
                    row.Height = 500;
                    cell.CellStyle = style3;
                    region = new CellRangeAddress(firstRow, firstRow, 0, colNames.Length - 1);
                    sheet.AddMergedRegion(region);
                    firstRow++;
                    firstRow = this.SetCellContent_(sheet, sfpList, firstRow, colNames, totalText, subTotalText, contentstyle, otherstyle, blackStyle) + 1;
                }
                this.AutoWidth(0x10, ref sheet);
                FileStream stream = new FileStream(file, FileMode.Create);
                this.needSpace = workbook.GetBytes().LongLength;
                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                {
                    stream.Close();
                    return false;
                }
                workbook.Write(stream);
                stream.Close();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception);
            }
            return false;
        }

        private bool ExportToExcelHWYS(List<FPDetail> fpList, string file)
        {
            int rowNo = 1;
            Font textFont = new Font("宋体", 16f, FontStyle.Bold);
            Font contentFont = new Font("宋体", 9f);
            Font font3 = new Font("宋体", 12f);
            string text = base.TaxCardInstance.get_TaxCode() + "发票数据";
            string str2 = string.Format("资料区间：{0} ~ {1}", this.dtpStart.Value.ToString("yyyy-MM-dd"), this.dtpEnd.Value.ToString("yyyy-MM-dd"));
            string[] colNames = new string[] { "发票代码", "发票号码", "开票日期", "收货人名称", "收货人识别号", "发货人名称", "发货人识别号", "受票方名称", "受票方识别号", "合计金额", "税率", "合计税额", "价税合计", "序号", "费用项目", "金额" };
            string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
            string subTotalText = "小计";
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                object obj2 = Missing.Value;
                ApplicationClass class2 = new ApplicationClass();
                Workbook workbook = class2.get_Workbooks().Add(obj2);
                class2.set_DisplayAlerts(false);
                class2.set_Visible(false);
                Worksheet sheet = workbook.get_Worksheets().Add(obj2, obj2, 1, obj2) as Worksheet;
                SetMergeCellValue(sheet, text, 2, colNames.Length, textFont, true, -4108);
                SetMergeCellValue(sheet, str2, 4, 4, font3, false, -4131);
                rowNo = 5;
                if (fpList.Count > 0)
                {
                    SetMergeCellValue(sheet, "发票类别：货物运输业增值税专用发票", rowNo, colNames.Length, textFont, true, -4131);
                    rowNo++;
                    rowNo = this.SetCellContentHWYS(sheet, fpList, rowNo, font3, contentFont, colNames, totalText, subTotalText) + 1;
                }
                workbook.SaveAs(file, (XlFileFormat) (-4143), obj2, obj2, obj2, obj2, 1, obj2, obj2, obj2, obj2, obj2);
                workbook.Close(false, obj2, obj2);
                class2.Quit();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return false;
        }

        private bool ExportToExcelHWYS_(List<FPDetail> fpList, string file)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = null;
                sheet = (HSSFSheet) workbook.CreateSheet("sheet1");
                sheet.AutoSizeColumn(1, true);
                HSSFCellStyle style = (HSSFCellStyle) workbook.CreateCellStyle();
                style.FillPattern = FillPattern.Squares;
                HSSFFont font = (HSSFFont) workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 0x10;
                font.Boldweight = 700;
                HSSFFont font2 = (HSSFFont) workbook.CreateFont();
                font2.FontName = "宋体";
                font2.FontHeightInPoints = 9;
                HSSFFont font3 = (HSSFFont) workbook.CreateFont();
                font3.FontName = "宋体";
                font3.FontHeightInPoints = 12;
                HSSFCellStyle style2 = (HSSFCellStyle) workbook.CreateCellStyle();
                style2.SetFont(font);
                style2.Alignment = HorizontalAlignment.Center;
                style2.VerticalAlignment = VerticalAlignment.Center;
                style2.IsLocked = true;
                HSSFCellStyle style3 = (HSSFCellStyle) workbook.CreateCellStyle();
                style3.SetFont(font);
                style3.Alignment = HorizontalAlignment.Left;
                style3.VerticalAlignment = VerticalAlignment.Center;
                style3.IsLocked = true;
                HSSFCellStyle blackStyle = (HSSFCellStyle) workbook.CreateCellStyle();
                blackStyle.FillForegroundColor = 0x16;
                blackStyle.Alignment = HorizontalAlignment.Center;
                blackStyle.FillPattern = FillPattern.SolidForeground;
                blackStyle.SetFont(font2);
                HSSFCellStyle contentstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                contentstyle.SetFont(font2);
                HSSFCellStyle otherstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                otherstyle.SetFont(font3);
                string str = "货物运输业增值税专用发票";
                string.Format("资料区间：{0} ~ {1}", this.startDate.ToString("yyyy-MM-dd"), this.endDate.ToString("yyyy-MM-dd"));
                string[] colNames = new string[] { 
                    "发票代码", "发票号码", "开票日期", "收货人名称", "收货人识别号", "发货人名称", "发货人识别号", "受票方名称", "受票方识别号", "合计金额", "税率", "合计税额", "价税合计", "商品版本号", "序号", "费用项目", 
                    "金额", "税收编码"
                 };
                if (!this._isSPBMVersion)
                {
                    colNames = new string[] { "发票代码", "发票号码", "开票日期", "收货人名称", "收货人识别号", "发货人名称", "发货人识别号", "受票方名称", "受票方识别号", "合计金额", "税率", "合计税额", "价税合计", "序号", "费用项目", "金额" };
                }
                string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
                string subTotalText = "小计";
                HSSFRow row = (HSSFRow) sheet.CreateRow(0);
                HSSFCell cell = (HSSFCell) row.CreateCell(0);
                cell.SetCellValue(new HSSFRichTextString(str));
                row.Height = 500;
                cell.CellStyle = style2;
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, colNames.Length - 1);
                sheet.AddMergedRegion(region);
                int curRowNo = 1;
                if (fpList.Count > 0)
                {
                    curRowNo = this.SetCellContentHWYS_(sheet, fpList, curRowNo, colNames, totalText, subTotalText, contentstyle, otherstyle, blackStyle);
                }
                this.AutoWidth(colNames.Length, ref sheet);
                FileStream stream = new FileStream(file, FileMode.Create);
                this.needSpace = workbook.GetBytes().LongLength;
                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                {
                    stream.Close();
                    return false;
                }
                workbook.Write(stream);
                stream.Close();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception);
            }
            return false;
        }

        private bool ExportToExcelJDC(List<FPDetail> fpList, string file)
        {
            int rowNo = 1;
            Font textFont = new Font("宋体", 16f, FontStyle.Bold);
            Font contentFont = new Font("宋体", 9f);
            Font font3 = new Font("宋体", 12f);
            string text = base.TaxCardInstance.get_Corporation() + "发票数据";
            string str2 = string.Format("资料区间：{0} ~ {1}", this.dtpStart.Value.ToString("yyyy-MM-dd"), this.dtpEnd.Value.ToString("yyyy-MM-dd"));
            string[] colNames = new string[] { 
                "发票代码", "发票号码", "购货单位", "购方税号", "生产企业名称", "车辆类型", "厂牌型号", "产地", "合格证号", "进口证明书号", "商检单号", "发动机号码", "车架号码", "开票日期", "金额", "税率", 
                "税额"
             };
            string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
            string subTotalText = "小计";
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                object obj2 = Missing.Value;
                ApplicationClass class2 = new ApplicationClass();
                Workbook workbook = class2.get_Workbooks().Add(obj2);
                class2.set_DisplayAlerts(false);
                class2.set_Visible(false);
                Worksheet sheet = workbook.get_Worksheets().Add(obj2, obj2, 1, obj2) as Worksheet;
                SetMergeCellValue(sheet, text, 2, colNames.Length, textFont, true, -4108);
                SetMergeCellValue(sheet, str2, 4, 4, font3, false, -4131);
                rowNo = 5;
                if (fpList.Count > 0)
                {
                    SetMergeCellValue(sheet, "发票类别：机动车销售统一发票", rowNo, colNames.Length, textFont, true, -4131);
                    rowNo++;
                    rowNo = this.SetCellContentJDC(sheet, fpList, rowNo, font3, contentFont, colNames, totalText, subTotalText) + 1;
                }
                workbook.SaveAs(file, (XlFileFormat) (-4143), obj2, obj2, obj2, obj2, 1, obj2, obj2, obj2, obj2, obj2);
                workbook.Close(false, obj2, obj2);
                class2.Quit();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return false;
        }

        private bool ExportToExcelJDC_(List<FPDetail> fpList, string file)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = null;
                sheet = (HSSFSheet) workbook.CreateSheet("sheet1");
                sheet.AutoSizeColumn(1, true);
                HSSFCellStyle style = (HSSFCellStyle) workbook.CreateCellStyle();
                style.FillPattern = FillPattern.Squares;
                HSSFFont font = (HSSFFont) workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 0x10;
                font.Boldweight = 700;
                HSSFFont font2 = (HSSFFont) workbook.CreateFont();
                font2.FontName = "宋体";
                font2.FontHeightInPoints = 9;
                HSSFFont font3 = (HSSFFont) workbook.CreateFont();
                font3.FontName = "宋体";
                font3.FontHeightInPoints = 12;
                HSSFCellStyle style2 = (HSSFCellStyle) workbook.CreateCellStyle();
                style2.SetFont(font);
                style2.Alignment = HorizontalAlignment.Center;
                style2.VerticalAlignment = VerticalAlignment.Center;
                style2.IsLocked = true;
                HSSFCellStyle style3 = (HSSFCellStyle) workbook.CreateCellStyle();
                style3.SetFont(font);
                style3.Alignment = HorizontalAlignment.Left;
                style3.VerticalAlignment = VerticalAlignment.Center;
                style3.IsLocked = true;
                HSSFCellStyle blackStyle = (HSSFCellStyle) workbook.CreateCellStyle();
                blackStyle.FillForegroundColor = 0x16;
                blackStyle.Alignment = HorizontalAlignment.Center;
                blackStyle.FillPattern = FillPattern.SolidForeground;
                blackStyle.SetFont(font2);
                HSSFCellStyle contentstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                contentstyle.SetFont(font2);
                HSSFCellStyle otherstyle = (HSSFCellStyle) workbook.CreateCellStyle();
                otherstyle.SetFont(font3);
                string str = "机动车销售统一发票";
                string[] colNames = new string[] { 
                    "发票代码", "发票号码", "开票日期", "购方单位名称", "身份证号码", "购方单位识别号", "车辆类型", "厂牌型号", "产地", "合格证书", "进口证明书号", "商检单号", "发动机号码", "车辆识别代号", "价税合计", "电话", 
                    "账号", "地址", "开户银行", "增值税税率", "增值税税额", "不含税价", "吨位", "限乘人数", "商品版本号", "税收编码"
                 };
                if (!this._isSPBMVersion)
                {
                    colNames = new string[] { 
                        "发票代码", "发票号码", "开票日期", "购方单位名称", "身份证号码", "购方单位识别号", "车辆类型", "厂牌型号", "产地", "合格证书", "进口证明书号", "商检单号", "发动机号码", "车辆识别代号", "价税合计", "电话", 
                        "账号", "地址", "开户银行", "增值税税率", "增值税税额", "不含税价", "吨位", "限乘人数"
                     };
                }
                string totalText = "份数：{0}  金额：{1}元  税额：{2}元";
                string subTotalText = "小计";
                HSSFRow row = (HSSFRow) sheet.CreateRow(0);
                HSSFCell cell = (HSSFCell) row.CreateCell(0);
                cell.SetCellValue(new HSSFRichTextString(str));
                row.Height = 500;
                cell.CellStyle = style2;
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, colNames.Length - 1);
                sheet.AddMergedRegion(region);
                int curRowNo = 1;
                if (fpList.Count > 0)
                {
                    curRowNo = this.SetCellContentJDC_(sheet, fpList, curRowNo, colNames, totalText, subTotalText, contentstyle, otherstyle, blackStyle);
                }
                this.AutoWidth(0x19, ref sheet);
                FileStream stream = new FileStream(file, FileMode.Create);
                this.needSpace = workbook.GetBytes().LongLength;
                if (this.needSpace > (this.freeSpece - this.offSetSpace))
                {
                    stream.Close();
                    return false;
                }
                workbook.Write(stream);
                stream.Close();
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception);
            }
            return false;
        }

        private bool ExportToXml(List<FPDetail> fpList, string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element3 = document.CreateElement("Kp");
                XmlElement element = document.CreateElement("Version");
                element.InnerText = this._isSPBMVersion ? "2.0" : "1.0";
                element3.AppendChild(element);
                XmlElement element4 = document.CreateElement("Fpxx");
                XmlElement element5 = document.CreateElement("Zsl");
                element5.InnerText = fpList.Count.ToString();
                element4.AppendChild(element5);
                element5 = document.CreateElement("Fpsj");
                element4.AppendChild(element5);
                for (int i = 0; i < fpList.Count; i++)
                {
                    FPDetail detail = fpList[i];
                    XmlElement element6 = document.CreateElement("Fp");
                    element = document.CreateElement("Djh");
                    element.InnerText = detail.XSDJBH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Fpzl");
                    string str = "";
                    if (detail.FPType == FPType.s)
                    {
                        str = "专用发票";
                    }
                    if (detail.FPType == FPType.c)
                    {
                        str = "普通发票";
                    }
                    element.InnerText = str;
                    element6.AppendChild(element);
                    element = document.CreateElement("Lbdm");
                    element.InnerText = detail.FPDM;
                    element6.AppendChild(element);
                    element = document.CreateElement("Fphm");
                    element.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element6.AppendChild(element);
                    element = document.CreateElement("Kprq");
                    element.InnerText = detail.KPRQ.ToString("yyyyMMdd");
                    element6.AppendChild(element);
                    element = document.CreateElement("Gfmc");
                    element.InnerText = detail.GFMC;
                    element6.AppendChild(element);
                    element = document.CreateElement("Gfsh");
                    element.InnerText = detail.GFSH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Gfyhzh");
                    element.InnerText = detail.GFYHZH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Gfdzdh");
                    element.InnerText = detail.GFDZDH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Xfmc");
                    element.InnerText = detail.XFMC;
                    element6.AppendChild(element);
                    element = document.CreateElement("Xfsh");
                    element.InnerText = detail.XFSH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Xfyhzh");
                    element.InnerText = detail.XFYHZH;
                    element6.AppendChild(element);
                    element = document.CreateElement("Xfdzdh");
                    element.InnerText = detail.XFDZDH;
                    element6.AppendChild(element);
                    decimal hJJE = detail.HJJE;
                    element = document.CreateElement("Hjje");
                    element.InnerText = hJJE.ToString("F2");
                    element6.AppendChild(element);
                    decimal hJSE = detail.HJSE;
                    element = document.CreateElement("Hjse");
                    element.InnerText = hJSE.ToString("F2");
                    element6.AppendChild(element);
                    element = document.CreateElement("Bz");
                    string str2 = ToolUtil.GetString(Convert.FromBase64String(detail.BZ));
                    element.InnerText = str2;
                    element6.AppendChild(element);
                    element = document.CreateElement("Kpr");
                    element.InnerText = detail.KPR;
                    element6.AppendChild(element);
                    element = document.CreateElement("Fhr");
                    element.InnerText = detail.FHR;
                    element6.AppendChild(element);
                    element = document.CreateElement("Skr");
                    element.InnerText = detail.SKR;
                    element6.AppendChild(element);
                    if (this._isSPBMVersion)
                    {
                        element = document.CreateElement("Spbmbbh");
                        element.InnerText = detail.BMBBBH;
                        element6.AppendChild(element);
                    }
                    element = document.CreateElement("Hsbz");
                    if ((((detail.FPType == FPType.s) && (detail.SLV == 0.05f)) && (detail.YYSBZ.Substring(8, 1) == "0")) || (detail.SLV == 0.015f))
                    {
                        element.InnerText = "1";
                    }
                    else if (detail.YYSBZ.Substring(8, 1) == "2")
                    {
                        element.InnerText = "2";
                    }
                    else
                    {
                        element.InnerText = "0";
                    }
                    element6.AppendChild(element);
                    element = document.CreateElement("Spxx");
                    XmlElement element7 = document.CreateElement("Sph");
                    if (((detail.GoodsList != null) && (detail.GoodsList.Count > 0)) || ((detail.QDList != null) && (detail.QDList.Count > 0)))
                    {
                        List<GoodsInfo> goodsList = detail.GoodsList;
                        if (QDFPFlag)
                        {
                            goodsList = detail.QDList;
                        }
                        int num4 = 1;
                        for (int j = 0; j < goodsList.Count; j++)
                        {
                            if (!QDFPFlag || (((goodsList[j].Name != "原价合计") && (goodsList[j].Name != "折扣额合计")) && (goodsList[j].Name != "小计")))
                            {
                                XmlElement element2 = document.CreateElement("Xh");
                                element2.InnerText = num4.ToString();
                                num4++;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Spmc");
                                element2.InnerText = goodsList[j].Name;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Ggxh");
                                element2.InnerText = goodsList[j].SpecMark;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Jldw");
                                element2.InnerText = goodsList[j].Unit;
                                element7.AppendChild(element2);
                                if (this._isSPBMVersion)
                                {
                                    element2 = document.CreateElement("Spbm");
                                    element2.InnerText = goodsList[j].SPBM;
                                    element7.AppendChild(element2);
                                    element2 = document.CreateElement("Qyspbm");
                                    element2.InnerText = goodsList[j].QYZBM;
                                    element7.AppendChild(element2);
                                    element2 = document.CreateElement("Syyhzcbz");
                                    element2.InnerText = goodsList[j].SFYH;
                                    element7.AppendChild(element2);
                                    element2 = document.CreateElement("Lslbz");
                                    element2.InnerText = goodsList[j].LSLBS;
                                    element7.AppendChild(element2);
                                    element2 = document.CreateElement("Yhzcsm");
                                    element2.InnerText = goodsList[j].ZZSTSGL;
                                    element7.AppendChild(element2);
                                }
                                element2 = document.CreateElement("Dj");
                                element2.InnerText = goodsList[j].Price;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Sl");
                                element2.InnerText = goodsList[j].Num;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Je");
                                element2.InnerText = goodsList[j].Amount.ToString("F2");
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Slv");
                                string str3 = "";
                                if (goodsList[j].SLV == -1f)
                                {
                                    str3 = "";
                                }
                                else
                                {
                                    str3 = goodsList[j].SLV.ToString();
                                }
                                element2.InnerText = str3;
                                element7.AppendChild(element2);
                                element2 = document.CreateElement("Se");
                                element2.InnerText = goodsList[j].Tax.ToString("F2");
                                element7.AppendChild(element2);
                            }
                        }
                    }
                    element.AppendChild(element7);
                    element6.AppendChild(element);
                    element5.AppendChild(element6);
                }
                element4.AppendChild(element5);
                element3.AppendChild(element4);
                document.AppendChild(element3);
                document.Save(file);
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票数据导出:专普票:" + exception.ToString());
            }
            return false;
        }

        private bool ExportToXmlHWYS(List<FPDetail> fpList, string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                for (int i = 0; i < fpList.Count; i++)
                {
                    XmlElement element3;
                    FPDetail detail = fpList[i];
                    XmlElement element2 = document.CreateElement("body");
                    if (this._isSPBMVersion)
                    {
                        element3 = document.CreateElement("bmb_bbh");
                        element3.InnerText = detail.BMBBBH;
                        element2.AppendChild(element3);
                    }
                    element3 = document.CreateElement("fpdm");
                    element3.InnerText = detail.FPDM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("shrmc");
                    element3.InnerText = detail.GFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("shrsbh");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fhrmc");
                    element3.InnerText = detail.XFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fhrsbh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("spfmc");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("spfsbh");
                    element3.InnerText = detail.GFSH;
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    element3 = document.CreateElement("hjje");
                    element3.InnerText = hJJE.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("slv");
                    element3.InnerText = (detail.SLV * 100f).ToString();
                    element2.AppendChild(element3);
                    decimal hJSE = detail.HJSE;
                    element3 = document.CreateElement("se");
                    element3.InnerText = hJSE.ToString("F2");
                    element2.AppendChild(element3);
                    decimal num4 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = num4.ToString("F2");
                    element2.AppendChild(element3);
                    if (detail.GoodsNum > 0)
                    {
                        element3 = document.CreateElement("group");
                        int num5 = 1;
                        for (int j = 0; j < detail.GoodsList.Count; j++)
                        {
                            XmlElement element4;
                            if (this._isSPBMVersion)
                            {
                                element4 = document.CreateElement("spbm");
                                element4.InnerText = detail.GoodsList[j].SPBM;
                                element3.AppendChild(element4);
                                element4 = document.CreateElement("zxbm");
                                element4.InnerText = detail.GoodsList[j].QYZBM;
                                element3.AppendChild(element4);
                                element4 = document.CreateElement("yhzcbs");
                                element4.InnerText = detail.GoodsList[j].SFYH;
                                element3.AppendChild(element4);
                                element4 = document.CreateElement("lslbs");
                                element4.InnerText = detail.GoodsList[j].LSLBS;
                                element3.AppendChild(element4);
                                element4 = document.CreateElement("zzstsgl");
                                element4.InnerText = detail.GoodsList[j].ZZSTSGL;
                                element3.AppendChild(element4);
                            }
                            element4 = document.CreateElement("xh");
                            element4.InnerText = num5.ToString();
                            num5++;
                            element3.AppendChild(element4);
                            element4 = document.CreateElement("xmmc");
                            element4.InnerText = detail.GoodsList[j].Name;
                            element3.AppendChild(element4);
                            element4 = document.CreateElement("je");
                            element4.InnerText = detail.GoodsList[j].Amount.ToString("F2");
                            element3.AppendChild(element4);
                        }
                        element2.AppendChild(element3);
                    }
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                document.Save(file);
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票数据导出:货运:" + exception.ToString());
            }
            return false;
        }

        private bool ExportToXmlJDC(List<FPDetail> fpList, string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                for (int i = 0; i < fpList.Count; i++)
                {
                    FPDetail detail = fpList[i];
                    XmlElement element2 = document.CreateElement("body");
                    XmlElement element3 = document.CreateElement("fpdm");
                    element3.InnerText = detail.FPDM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyyMMdd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ghdw");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("gfsbh");
                    string xSBM = "";
                    if (string.IsNullOrEmpty(detail.GFSH))
                    {
                        xSBM = detail.XSBM;
                    }
                    else
                    {
                        xSBM = detail.GFSH;
                    }
                    element3.InnerText = xSBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("scqymc");
                    element3.InnerText = detail.SCCJMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cllx");
                    element3.InnerText = detail.GFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cpxh");
                    element3.InnerText = detail.XFDZ;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cd");
                    element3.InnerText = detail.KHYHMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("hgzh");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jkzmsh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sjdh");
                    element3.InnerText = detail.QYD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fdjhm");
                    element3.InnerText = detail.ZHD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cjhm");
                    element3.InnerText = detail.XHD;
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    element3 = document.CreateElement("je");
                    element3.InnerText = hJJE.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sl");
                    element3.InnerText = detail.SLV.ToString();
                    element2.AppendChild(element3);
                    decimal hJSE = detail.HJSE;
                    element3 = document.CreateElement("se");
                    element3.InnerText = hJSE.ToString("F2");
                    element2.AppendChild(element3);
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                document.Save(file);
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票数据导出:机动车:" + exception.ToString());
            }
            return false;
        }

        private bool ExportToXmlJDC1_1(List<FPDetail> fpList, string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "gbk", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                for (int i = 0; i < fpList.Count; i++)
                {
                    XmlElement element3;
                    FPDetail detail = fpList[i];
                    XmlElement element2 = document.CreateElement("body");
                    if (this._isSPBMVersion)
                    {
                        element3 = document.CreateElement("bmb_bbh");
                        element3.InnerText = detail.BMBBBH;
                        element2.AppendChild(element3);
                    }
                    element3 = document.CreateElement("fpdm");
                    element3.InnerText = string.Format("{0:000000000000}", detail.FPDM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("gfdwmc");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sfzhm");
                    element3.InnerText = detail.XSBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("gfdwsbh");
                    string gFSH = "";
                    if (string.IsNullOrEmpty(detail.GFSH))
                    {
                        gFSH = "";
                    }
                    else
                    {
                        gFSH = detail.GFSH;
                    }
                    element3.InnerText = gFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cllx");
                    element3.InnerText = detail.GFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cpxh");
                    element3.InnerText = detail.XFDZ;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("cd");
                    element3.InnerText = detail.KHYHMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("hgzh");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jkzmsh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sjdh");
                    element3.InnerText = detail.QYD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fdjhm");
                    element3.InnerText = detail.ZHD;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("clsbdh");
                    element3.InnerText = detail.XHD;
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    decimal hJSE = detail.HJSE;
                    decimal num4 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = (detail.HJJE + detail.HJSE).ToString("0.00");
                    element2.AppendChild(element3);
                    element3.InnerText = num4.ToString("F2");
                    element3 = document.CreateElement("dh");
                    element3.InnerText = detail.XFDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zh");
                    element3.InnerText = detail.KHYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("dz");
                    element3.InnerText = detail.XFDZDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("khyh");
                    element3.InnerText = detail.XFYHZH;
                    element2.AppendChild(element3);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zzssl");
                    element3.InnerText = detail.SLV.ToString();
                    element2.AppendChild(element3);
                    decimal num5 = detail.HJSE;
                    element3 = document.CreateElement("zzsse");
                    element3.InnerText = num5.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("bhsj");
                    element3.InnerText = hJJE.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("dw");
                    element3.InnerText = detail.YYZZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("xcrs");
                    element3.InnerText = detail.MDD;
                    element2.AppendChild(element3);
                    if (this._isSPBMVersion)
                    {
                        element3 = document.CreateElement("spbm");
                        element3.InnerText = detail.SPBM;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zxbm");
                        element3.InnerText = detail.QYZBM;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("yhzcbs");
                        element3.InnerText = detail.SFYH;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("lslbs");
                        element3.InnerText = detail.LSLBS;
                        element2.AppendChild(element3);
                        element3 = document.CreateElement("zzstsgl");
                        element3.InnerText = detail.ZZSTSGL;
                        element2.AppendChild(element3);
                    }
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                document.Save(file);
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票数据导出:机动车:" + exception.ToString());
            }
            return false;
        }

        private bool ExportToXmlJDC1_2(List<FPDetail> fpList, string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("taxML");
                element.SetAttribute("xmlns", "http://www.chinatax.gov.cn/dataspec/");
                element.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                element.SetAttribute("cnName", "批量信息－机动车销售发票明细信息报文");
                element.SetAttribute("name", "plxxJdcxsfpMxxx");
                element.SetAttribute("version", "SW5001-2014");
                for (int i = 0; i < fpList.Count; i++)
                {
                    FPDetail detail = fpList[i];
                    XmlElement element2 = document.CreateElement("jdcxsfpMxxx");
                    XmlElement element3 = document.CreateElement("fpdm");
                    element3.InnerText = string.Format("{0:000000000000}", detail.FPDM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fphm");
                    element3.InnerText = string.Format("{0:00000000}", detail.FPHM);
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kprq");
                    element3.InnerText = detail.KPRQ.ToString("yyyy-MM-dd");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ghdw");
                    element3.InnerText = detail.GFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ghdwdm");
                    string gFSH = "";
                    if (string.IsNullOrEmpty(detail.GFSH))
                    {
                        gFSH = "";
                    }
                    else
                    {
                        gFSH = detail.GFSH;
                    }
                    element3.InnerText = gFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("sfzhm");
                    element3.InnerText = detail.XSBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("hgzs");
                    element3.InnerText = detail.CM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("jkzmsh");
                    element3.InnerText = detail.TYDH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("clsbdh");
                    element3.InnerText = detail.XHD;
                    element2.AppendChild(element3);
                    decimal hJJE = detail.HJJE;
                    decimal hJSE = detail.HJSE;
                    decimal num4 = hJJE + hJSE;
                    element3 = document.CreateElement("jshj");
                    element3.InnerText = num4.ToString("F2");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("xhdwmc");
                    element3.InnerText = detail.XFMC;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("nsrsbh");
                    element3.InnerText = detail.XFSH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zgswjg_mc");
                    element3.InnerText = detail.GFYHZH;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("zgswjg_dm");
                    element3.InnerText = detail.HYBM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("wspzh");
                    element3.InnerText = detail.WSPZHM;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("kplx");
                    int num5 = 1;
                    if ((detail.XFSH.Length == 15) && detail.XFSH.Substring(8, 2).Equals("DK"))
                    {
                        num5 = 2;
                    }
                    else
                    {
                        num5 = 1;
                    }
                    element3.InnerText = num5.ToString();
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("fpbz");
                    bool zFBZ = detail.ZFBZ;
                    int num6 = 0;
                    if (zFBZ)
                    {
                        num6 = 1;
                    }
                    else if (hJJE > 0M)
                    {
                        num6 = 0;
                    }
                    else
                    {
                        num6 = 2;
                    }
                    element3.InnerText = num6.ToString();
                    element2.AppendChild(element3);
                    element.AppendChild(element2);
                }
                document.AppendChild(element);
                document.Save(file);
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票数据导出:机动车:" + exception.ToString());
            }
            return false;
        }

        private void FillCmbMonth()
        {
            List<IdTextPair> list = new List<IdTextPair>();
            DateTime time = base.TaxCardInstance.get_TaxClock();
            int num = 0;
            DateTime initDate = this.GetInitDate();
            if (initDate.Year == (time.Year - 1))
            {
                num = (time.Month - initDate.Month) + 12;
            }
            else if (initDate.Year == time.Year)
            {
                num = time.Month - initDate.Month;
            }
            else
            {
                num = (time.Month - initDate.Month) + ((time.Year - initDate.Year) * 12);
            }
            if (num < 0)
            {
                num = 0;
            }
            for (int i = num; i >= 0; i--)
            {
                DateTime time3 = time.AddMonths(-i);
                int month = time3.Month;
                string text = "";
                if (time3.Year < time.Year)
                {
                    month = -month;
                    text = time3.Year.ToString() + "年";
                }
                text = text + time3.ToString("MM月");
                list.Add(new IdTextPair(month, text));
            }
            this.nonSetChanged = true;
            this.cmbMonth.DataSource = list;
            this.cmbMonth.DisplayMember = "Text";
            this.cmbMonth.ValueMember = "Id";
            this.nonSetChanged = false;
            this.cmbMonth.SelectedValue = time.Month;
        }

        private void FillCmbMonth_()
        {
            List<IdTextPair> list = new List<IdTextPair>();
            base.TaxCardInstance.get_TaxClock();
            DateTime initDate = this.GetInitDate();
            try
            {
                for (DateTime time2 = new DateTime(base.TaxCardInstance.get_SysYear(), base.TaxCardInstance.get_SysMonth(), 1); DateTime.Compare(initDate, time2) <= 0; time2 = time2.AddMonths(-1))
                {
                    int id = int.Parse(time2.ToString("yyyyMM"));
                    string text = time2.ToString("yyyy年MM月");
                    list.Add(new IdTextPair(id, text));
                }
                this.cmbMonth.DataSource = list;
                this.cmbMonth.DisplayMember = "Text";
                this.cmbMonth.ValueMember = "Id";
                this.nonSetChanged = false;
                this.cmbMonth.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                this.loger.Debug(exception.ToString());
            }
        }

        private void FillCmbPiaoZhong()
        {
            this.cmbPiaoZhong.Items.Clear();
            if (QDFPFlag)
            {
                if (!this.cmbPiaoZhong.Items.Contains("增值税专用、普通发票"))
                {
                    this.cmbPiaoZhong.Items.Add("增值税专用、普通发票");
                }
            }
            else
            {
                this.m_InvTypeEntityList = this.m_commFun.GetInvTypeCollect();
                foreach (InvTypeEntity entity in this.m_InvTypeEntityList)
                {
                    if ((entity.m_invType == INV_TYPE.INV_SPECIAL) && !this.cmbPiaoZhong.Items.Contains("增值税专用、普通发票"))
                    {
                        this.cmbPiaoZhong.Items.Add("增值税专用、普通发票");
                    }
                    if ((entity.m_invType == INV_TYPE.INV_COMMON) && !this.cmbPiaoZhong.Items.Contains("增值税专用、普通发票"))
                    {
                        this.cmbPiaoZhong.Items.Add("增值税专用、普通发票");
                    }
                    if (!QDFPFlag)
                    {
                        if (entity.m_invType == INV_TYPE.INV_TRANSPORTATION)
                        {
                            this.cmbPiaoZhong.Items.Add("货物运输业增值税专用发票");
                        }
                        if (entity.m_invType == INV_TYPE.INV_VEHICLESALES)
                        {
                            this.cmbPiaoZhong.Items.Add("机动车销售统一发票");
                        }
                    }
                }
            }
            if (this.cmbPiaoZhong.Items.Count > 0)
            {
                this.cmbPiaoZhong.SelectedIndex = 0;
            }
        }

        private void FillTimer()
        {
            int selectedValue = 0;
            if (this.cmbMonth.SelectedValue == null)
            {
                selectedValue = base.TaxCardInstance.get_SysMonth();
            }
            else
            {
                selectedValue = (int) this.cmbMonth.SelectedValue;
            }
            int year = base.TaxCardInstance.get_SysYear();
            int month = selectedValue;
            if (selectedValue < 0)
            {
                year--;
                month = -month;
            }
            DateTime time = new DateTime(year, month, 1);
            DateTime time2 = time.AddMonths(1).AddDays(-1.0);
            if (this.lastSelectedMonth <= selectedValue)
            {
                this.dtpStart.MaxDate = time2;
                this.dtpStart.Value = time;
                this.dtpStart.MinDate = time;
                this.dtpEnd.MaxDate = time2;
                this.dtpEnd.Value = time2;
                this.dtpEnd.MinDate = time;
            }
            else
            {
                this.dtpStart.MinDate = time;
                this.dtpStart.Value = time;
                this.dtpStart.MaxDate = time2;
                this.dtpEnd.MinDate = time;
                this.dtpEnd.Value = time2;
                this.dtpEnd.MaxDate = time2;
            }
        }

        private void FillTimer_()
        {
            int selectedValue = 0;
            int month = 0;
            int year = 0;
            if (this.cmbMonth.SelectedValue == null)
            {
                year = base.TaxCardInstance.get_SysYear();
                month = base.TaxCardInstance.get_SysMonth();
            }
            else
            {
                selectedValue = (int) this.cmbMonth.SelectedValue;
                month = int.Parse(selectedValue.ToString().Substring(4, 2));
                year = int.Parse(selectedValue.ToString().Substring(0, 4));
            }
            DateTime time = new DateTime(year, month, 1);
            DateTime time2 = time.AddMonths(1).AddDays(-1.0);
            if (this.lastSelectedMonth <= selectedValue)
            {
                this.dtpStart.MaxDate = time2;
                this.dtpStart.Value = time;
                this.dtpStart.MinDate = time;
                this.dtpEnd.MaxDate = time2;
                this.dtpEnd.Value = time2;
                this.dtpEnd.MinDate = time;
            }
            else
            {
                this.dtpStart.MinDate = time;
                this.dtpStart.Value = time;
                this.dtpStart.MaxDate = time2;
                this.dtpEnd.MinDate = time;
                this.dtpEnd.Value = time2;
                this.dtpEnd.MaxDate = time2;
            }
            this.lastSelectedMonth = selectedValue;
        }

        private void FPExport_Load(object sender, EventArgs e)
        {
            if (QDFPFlag)
            {
                this.Text = "清单发票数据导出";
            }
            else
            {
                this.Text = "发票数据导出";
            }
            this.changeHeight = this.groupBoxGFXX.Height;
            this.FormHeight = base.Height;
            this.okX = this.btnOK.Location.X;
            this.okY = this.btnOK.Location.Y;
            this.cancelX = this.btnCancel.Location.X;
            this.cancelY = this.btnCancel.Location.Y;
            this.FillCmbMonth_();
            this.FillCmbPiaoZhong();
            this.FillTimer_();
            this.tbGFMC.MaxLength = 100;
            this.tbGFSH.MaxLength = 20;
            this.tbGFSH.CharacterCasing = CharacterCasing.Upper;
            if (this.isJDCOnly)
            {
                this.Text = "车购税申报导出";
            }
            this.SetFPLZ();
            TaxCard card = TaxCardFactory.CreateTaxCard();
            this._isSPBMVersion = card.GetExtandParams("FLBMFlag") == "FLBM";
        }

        private List<FPDetail> GetFPList()
        {
            if (this.dtpStart.Value > this.dtpEnd.Value)
            {
                MessageManager.ShowMsgBox("INP-251514");
                return null;
            }
            if ((this.dtpStart.Value == this.dtpEnd.Value) && (!this.chkStart.Checked || !this.chkEnd.Checked))
            {
                MessageManager.ShowMsgBox("INP-251515");
                return null;
            }
            if (this.dtpStart.Value <= this.dtpEnd.Value)
            {
                this.startDate = this.dtpStart.Value;
                this.endDate = this.dtpEnd.Value;
            }
            else
            {
                this.startDate = this.dtpEnd.Value;
                this.endDate = this.dtpStart.Value;
            }
            if (!this.chkStart.Checked)
            {
                this.startDate = this.startDate.AddDays(1.0);
            }
            if (!this.chkEnd.Checked)
            {
                this.endDate = this.endDate.AddDays(-1.0);
            }
            if (this.startDate > this.endDate)
            {
                MessageManager.ShowMsgBox("INP-251515");
                return null;
            }
            FPDetailDAL ldal = new FPDetailDAL();
            List<FPDetail> list = new List<FPDetail>();
            if (this.cmbPiaoZhong.SelectedItem.Equals("增值税专用、普通发票"))
            {
                List<FPDetail> list2 = ldal.GetFPDetailListByFPZL(this.startDate, this.endDate, "s", QDFPFlag);
                List<FPDetail> list3 = ldal.GetFPDetailListByFPZL(this.startDate, this.endDate, "c", QDFPFlag);
                foreach (FPDetail detail in list2)
                {
                    list.Add(detail);
                }
                foreach (FPDetail detail2 in list3)
                {
                    list.Add(detail2);
                }
                if (this.tbGFMC.TextLength != 0)
                {
                    for (int j = list.Count - 1; j >= 0; j--)
                    {
                        if (!list[j].GFMC.Contains(this.tbGFMC.Text))
                        {
                            list.RemoveAt(j);
                        }
                    }
                }
                if (this.tbGFSH.TextLength != 0)
                {
                    for (int k = list.Count - 1; k >= 0; k--)
                    {
                        if (!list[k].GFSH.Contains(this.tbGFSH.Text))
                        {
                            list.RemoveAt(k);
                        }
                    }
                }
            }
            else if (this.cmbPiaoZhong.SelectedItem.Equals("货物运输业增值税专用发票"))
            {
                list = ldal.GetFPDetailListByFPZL(this.startDate, this.endDate, "f", false);
            }
            else if (this.cmbPiaoZhong.SelectedItem.Equals("机动车销售统一发票"))
            {
                list = ldal.GetFPDetailListByFPZL(this.startDate, this.endDate, "j", false);
            }
            else
            {
                return null;
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].ZFBZ)
                {
                    list.RemoveAt(i);
                }
            }
            foreach (FPDetail detail3 in list)
            {
                if (detail3 != null)
                {
                    if (detail3.FPType == FPType.s)
                    {
                        detail3.GoodsList.AddRange(ldal.GetGoodsListFPExport(detail3.FPType.ToString(), detail3.FPDM, detail3.FPHM));
                    }
                    else
                    {
                        detail3.GoodsList.AddRange(ldal.GetGoodsList(detail3.FPType.ToString(), detail3.FPDM, detail3.FPHM));
                    }
                    if (detail3.QDBZ && ((detail3.FPType == FPType.s) || (detail3.FPType == FPType.c)))
                    {
                        detail3.QDList.AddRange(ldal.GetGoodsQDList(detail3.FPType.ToString(), detail3.FPDM, detail3.FPHM));
                    }
                }
            }
            return list;
        }

        private DateTime GetInitDate()
        {
            DateTime time = base.TaxCardInstance.get_TaxClock().AddMonths(-12);
            try
            {
                string str = base.TaxCardInstance.get_CardEffectDate();
                time = Convert.ToDateTime(string.Format("{0}-{1}-01", str.Substring(0, 4), str.Substring(4, 2)));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
            }
            return time;
        }

        private List<InvTypeEntity> GetInvTypeCollect()
        {
            List<InvTypeEntity> list = new List<InvTypeEntity>();
            InvTypeEntity item = new InvTypeEntity();
            bool iSZYFP = base.TaxCardInstance.get_QYLX().ISZYFP;
            bool iSPTFP = base.TaxCardInstance.get_QYLX().ISPTFP;
            if (iSZYFP)
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
            }
            if (iSPTFP)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            bool iSHY = base.TaxCardInstance.get_QYLX().ISHY;
            if (iSHY)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_TRANSPORTATION,
                    m_strInvName = "货物运输业增值税专用发票"
                };
                list.Add(item);
            }
            bool iSJDC = base.TaxCardInstance.get_QYLX().ISJDC;
            if (iSJDC)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_VEHICLESALES,
                    m_strInvName = "机动车销售统一发票"
                };
                list.Add(item);
            }
            if ((!iSZYFP && !iSPTFP) && (!iSHY && !iSJDC))
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            return list;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.cmbMonth = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbMonth");
            this.groupBox2 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox2");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.dtpStart = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpStart");
            this.dtpEnd = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpEnd");
            this.chkStart = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkStart");
            this.chkEnd = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("chkEnd");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.groupBoxPiaoZhong = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBoxPiaoZhong");
            this.groupBoxGFXX = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBoxGFXX");
            this.cmbPiaoZhong = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmbPiaoZhong");
            this.lablePiaoZhong = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lablePiaoZhong");
            this.lableGFMC = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableGFMC");
            this.lableGFSH = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lableGFSH");
            this.tbGFSH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("tbGFSH");
            this.tbGFMC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("tbGFMC");
            this.tbGFSH.TextChanged += new EventHandler(this.tbGFSH_TextChanged);
            this.tbGFMC.TextChanged += new EventHandler(this.tbGFMC_TextChanged);
            this.cmbMonth.DropDownHeight = 80;
            this.cmbMonth.SelectedIndexChanged += new EventHandler(this.cmbMonth_SelectedIndexChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.cmbPiaoZhong.SelectedIndexChanged += new EventHandler(this.cmbPiaoZhong_SelectedIndexChanged);
            this.cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPiaoZhong.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPExport));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x174, 0x16c);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.FPExport\Aisino.Fwkp.Bsgl.FPExport.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x174, 0x16c);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "FPExport";
            this.Text = "发票数据导出";
            base.ResumeLayout(false);
        }

        private void InvokePerformStep(object step)
        {
            int num = int.Parse(step.ToString());
            for (int i = 0; i < num; i++)
            {
                if ((this.progressBar.fpxf_progressBar.Value + 1) > this.progressBar.fpxf_progressBar.Maximum)
                {
                    this.progressBar.fpxf_progressBar.Value = this.progressBar.fpxf_progressBar.Maximum;
                }
                else
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.progressBar.fpxf_progressBar.Refresh();
            }
        }

        private bool IsValidDriver(string path)
        {
            try
            {
                DriveInfo info = new DriveInfo(path.Substring(0, 3));
                if (info.AvailableFreeSpace < 0x989680L)
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception);
                return false;
            }
            return true;
        }

        private void PerformStep(object step)
        {
            this.InvokePerformStep(step);
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                this.PerformStep(obj);
            }
            catch (Exception exception)
            {
                this.loger.Info("[ProccessBarShow]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            this.ProccessBarShow(value);
        }

        private int SetCellContent(Worksheet sheet, List<FPDetail> sfpList, int curRowNo, Font otherFont, Font contentFont, string[] colNames, string totalText, string subTotalText)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            this.SetHeaderCellValue(sheet, colNames, curRowNo, contentFont, -4108);
            curRowNo++;
            int num3 = curRowNo;
            foreach (FPDetail detail in sfpList)
            {
                if (detail != null)
                {
                    sheet.get_Cells().set__Default(curRowNo, 1, "'" + detail.FPDM);
                    sheet.get_Cells().set__Default(curRowNo, 2, "'" + detail.FPHM);
                    sheet.get_Cells().set__Default(curRowNo, 3, "'" + detail.GFMC);
                    sheet.get_Cells().set__Default(curRowNo, 4, "'" + detail.GFSH);
                    sheet.get_Cells().set__Default(curRowNo, 5, "'" + detail.GFYHZH);
                    sheet.get_Cells().set__Default(curRowNo, 6, "'" + detail.GFDZDH);
                    sheet.get_Cells().set__Default(curRowNo, 7, detail.KPRQ.ToString("yyyy-MM-dd"));
                    sheet.get_Cells().set__Default(curRowNo, 8, detail.XSDJBH);
                    if (detail.GoodsList.Count > 0)
                    {
                        decimal num4 = 0M;
                        decimal num5 = 0M;
                        foreach (GoodsInfo info in detail.GoodsList)
                        {
                            if (info != null)
                            {
                                sheet.get_Cells().set__Default(curRowNo, 9, "'" + info.Name);
                                sheet.get_Cells().set__Default(curRowNo, 10, "'" + info.SpecMark);
                                sheet.get_Cells().set__Default(curRowNo, 11, "'" + info.Unit);
                                sheet.get_Cells().set__Default(curRowNo, 12, "'" + info.Num);
                                sheet.get_Cells().set__Default(curRowNo, 13, "'" + info.Price);
                                sheet.get_Cells().set__Default(curRowNo, 14, "'" + info.Amount.ToString("F2"));
                                sheet.get_Cells().set__Default(curRowNo, 15, "'" + info.SLV.ToString("P0"));
                                if (info.SLV == -1f)
                                {
                                    sheet.get_Cells().set__Default(curRowNo, 15, "");
                                }
                                sheet.get_Cells().set__Default(curRowNo, 0x10, "'" + info.Tax.ToString("F2"));
                                num4 += info.Amount;
                                num5 += info.Tax;
                                curRowNo++;
                            }
                        }
                        if (detail.GoodsList.Count > 1)
                        {
                            sheet.get_Cells().set__Default(curRowNo, 9, "'" + subTotalText);
                            sheet.get_Cells().set__Default(curRowNo, 14, "'" + num4.ToString("F2"));
                            sheet.get_Cells().set__Default(curRowNo, 0x10, "'" + num5.ToString("F2"));
                        }
                        else
                        {
                            curRowNo--;
                        }
                    }
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                    curRowNo++;
                }
            }
            sheet.get_Range(sheet.get_Cells().get__Default(num3, 15), sheet.get_Cells().get__Default(curRowNo - 1, 15)).set_HorizontalAlignment((Constants) (-4152));
            Range range = sheet.get_Range(sheet.get_Cells().get__Default(num3, 1), sheet.get_Cells().get__Default(curRowNo - 1, colNames.Length));
            range.get_EntireColumn().AutoFit();
            SetRangeFormat(range, contentFont, -4131);
            curRowNo++;
            SetMergeCellValue(sheet, string.Format(totalText, sfpList.Count, num.ToString("F2"), num2.ToString("F2")), curRowNo, colNames.Length, otherFont, false, -4131);
            curRowNo++;
            return curRowNo;
        }

        private int SetCellContent_(HSSFSheet sheet, List<FPDetail> sfpList, int curRowNo, string[] colNames, string totalText, string subTotalText, HSSFCellStyle contentstyle, HSSFCellStyle otherstyle, HSSFCellStyle blackStyle)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            HSSFRow row = (HSSFRow) sheet.CreateRow(curRowNo);
            HSSFCell cell = null;
            for (int i = 0; i < colNames.Length; i++)
            {
                cell = (HSSFCell) row.CreateCell(i);
                cell.CellStyle = blackStyle;
                cell.SetCellValue(new HSSFRichTextString(colNames[i]));
            }
            curRowNo++;
            foreach (FPDetail detail in sfpList)
            {
                row = (HSSFRow) sheet.CreateRow(curRowNo);
                if (detail != null)
                {
                    int column = 0;
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.FPDM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(string.Format("{0:00000000}", detail.FPHM)));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFMC));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFSH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFYHZH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFDZDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.KPRQ.ToString("yyyy-MM-dd")));
                    if (this._isSPBMVersion)
                    {
                        cell = (HSSFCell) row.CreateCell(column);
                        column++;
                        cell.CellStyle = contentstyle;
                        cell.SetCellValue(new HSSFRichTextString(detail.BMBBBH.ToString()));
                    }
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XSDJBH));
                    if (((detail.GoodsList != null) && (detail.GoodsList.Count > 0)) || ((detail.QDList != null) && (detail.QDList.Count > 0)))
                    {
                        List<GoodsInfo> goodsList = detail.GoodsList;
                        if (QDFPFlag)
                        {
                            goodsList = detail.QDList;
                        }
                        decimal num5 = 0M;
                        decimal num6 = 0M;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        foreach (GoodsInfo info in goodsList)
                        {
                            int num10 = column;
                            if ((info != null) && (!QDFPFlag || ((info.Name != "原价合计") && (info.Name != "折扣额合计"))))
                            {
                                cell = (HSSFCell) row.CreateCell(num10);
                                num7 = num10;
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Name));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.SpecMark));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Unit));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Num));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Price));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num8 = num10;
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Amount.ToString("F2")));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                string str = "";
                                if (info.SLV == -1f)
                                {
                                    str = "";
                                }
                                else if (info.SLV == 0.015f)
                                {
                                    str = "1.5%";
                                }
                                else
                                {
                                    str = info.SLV.ToString("P0");
                                }
                                cell.SetCellValue(new HSSFRichTextString(str));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num9 = num10;
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Tax.ToString("F2")));
                                num5 += info.Amount;
                                num6 += info.Tax;
                                if (this._isSPBMVersion)
                                {
                                    cell = (HSSFCell) row.CreateCell(num10);
                                    num10++;
                                    cell.CellStyle = contentstyle;
                                    cell.SetCellValue(new HSSFRichTextString(info.SPBM));
                                }
                                curRowNo++;
                                row = (HSSFRow) sheet.CreateRow(curRowNo);
                            }
                        }
                        if (detail.GoodsList.Count > 1)
                        {
                            cell = (HSSFCell) row.CreateCell(num7);
                            cell.CellStyle = contentstyle;
                            cell.SetCellValue(new HSSFRichTextString(subTotalText));
                            cell = (HSSFCell) row.CreateCell(num8);
                            cell.CellStyle = contentstyle;
                            cell.SetCellValue(new HSSFRichTextString(num5.ToString("F2")));
                            cell = (HSSFCell) row.CreateCell(num9);
                            cell.CellStyle = contentstyle;
                            cell.SetCellValue(new HSSFRichTextString(num6.ToString("F2")));
                        }
                        else
                        {
                            curRowNo--;
                        }
                    }
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                    curRowNo++;
                }
            }
            curRowNo++;
            row = (HSSFRow) sheet.CreateRow(curRowNo);
            HSSFCell cell2 = (HSSFCell) row.CreateCell(0);
            cell2 = (HSSFCell) row.CreateCell(0);
            cell2.CellStyle = otherstyle;
            cell2.SetCellValue(new HSSFRichTextString(string.Format(totalText, sfpList.Count, num.ToString("F2"), num2.ToString("F2"))));
            row.Height = 500;
            CellRangeAddress region = new CellRangeAddress(curRowNo, curRowNo, 0, 15);
            sheet.AddMergedRegion(region);
            curRowNo++;
            return curRowNo;
        }

        private int SetCellContentHWYS(Worksheet sheet, List<FPDetail> sfpList, int curRowNo, Font otherFont, Font contentFont, string[] colNames, string totalText, string subTotalText)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            this.SetHeaderCellValue(sheet, colNames, curRowNo, contentFont, -4108);
            curRowNo++;
            int num3 = curRowNo;
            foreach (FPDetail detail in sfpList)
            {
                if (detail != null)
                {
                    sheet.get_Cells().set__Default(curRowNo, 1, "'" + detail.FPDM);
                    sheet.get_Cells().set__Default(curRowNo, 2, "'" + detail.FPHM);
                    sheet.get_Cells().set__Default(curRowNo, 3, detail.KPRQ.ToString("yyyy-MM-dd"));
                    sheet.get_Cells().set__Default(curRowNo, 4, "'" + detail.GFDZDH);
                    sheet.get_Cells().set__Default(curRowNo, 5, "'" + detail.CM);
                    sheet.get_Cells().set__Default(curRowNo, 6, "'" + detail.XFDZDH);
                    sheet.get_Cells().set__Default(curRowNo, 7, "'" + detail.TYDH);
                    sheet.get_Cells().set__Default(curRowNo, 8, "'" + detail.GFMC);
                    sheet.get_Cells().set__Default(curRowNo, 9, "'" + detail.GFSH);
                    sheet.get_Cells().set__Default(curRowNo, 10, "'" + detail.HJJE.ToString("F2"));
                    sheet.get_Cells().set__Default(curRowNo, 11, "'" + detail.SLV.ToString("0.00"));
                    sheet.get_Cells().set__Default(curRowNo, 12, "'" + detail.HJSE.ToString("F2"));
                    decimal num10 = detail.HJJE + detail.HJSE;
                    sheet.get_Cells().set__Default(curRowNo, 13, "'" + num10.ToString("F2"));
                    if (detail.GoodsList.Count > 0)
                    {
                        decimal num4 = 0M;
                        decimal num5 = 0M;
                        int num6 = 1;
                        foreach (GoodsInfo info in detail.GoodsList)
                        {
                            if (info != null)
                            {
                                sheet.get_Cells().set__Default(curRowNo, 14, "'" + num6.ToString());
                                num6++;
                                sheet.get_Cells().set__Default(curRowNo, 15, "'" + info.Name);
                                sheet.get_Cells().set__Default(curRowNo, 0x10, "'" + info.Amount.ToString("F2"));
                                num4 += info.Amount;
                                num5 += info.Tax;
                                curRowNo++;
                            }
                        }
                        if (detail.GoodsList.Count > 1)
                        {
                            sheet.get_Cells().set__Default(curRowNo, 15, "'" + subTotalText);
                            sheet.get_Cells().set__Default(curRowNo, 0x10, "'" + num4.ToString("F2"));
                        }
                        else
                        {
                            curRowNo--;
                        }
                    }
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                    curRowNo++;
                }
            }
            sheet.get_Range(sheet.get_Cells().get__Default(num3, 14), sheet.get_Cells().get__Default(curRowNo - 1, 14)).set_HorizontalAlignment((Constants) (-4152));
            Range range = sheet.get_Range(sheet.get_Cells().get__Default(num3, 1), sheet.get_Cells().get__Default(curRowNo - 1, colNames.Length));
            range.get_EntireColumn().AutoFit();
            SetRangeFormat(range, contentFont, -4131);
            curRowNo++;
            SetMergeCellValue(sheet, string.Format(totalText, sfpList.Count, num.ToString("F2"), num2.ToString("F2")), curRowNo, colNames.Length, otherFont, false, -4131);
            curRowNo++;
            return curRowNo;
        }

        private int SetCellContentHWYS_(HSSFSheet sheet, List<FPDetail> sfpList, int curRowNo, string[] colNames, string totalText, string subTotalText, HSSFCellStyle contentstyle, HSSFCellStyle otherstyle, HSSFCellStyle blackStyle)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            HSSFRow row = (HSSFRow) sheet.CreateRow(curRowNo);
            HSSFCell cell = null;
            for (int i = 0; i < colNames.Length; i++)
            {
                cell = (HSSFCell) row.CreateCell(i);
                cell.CellStyle = blackStyle;
                cell.SetCellValue(new HSSFRichTextString(colNames[i]));
            }
            curRowNo++;
            foreach (FPDetail detail in sfpList)
            {
                row = (HSSFRow) sheet.CreateRow(curRowNo);
                if (detail != null)
                {
                    int column = 0;
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.FPDM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(string.Format("{0:00000000}", detail.FPHM)));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.KPRQ.ToString("yyyy-MM-dd")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFDZDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.CM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XFDZDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.TYDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFMC));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFSH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.HJJE.ToString("F2")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    float num12 = detail.SLV * 100f;
                    cell.SetCellValue(new HSSFRichTextString(num12.ToString()));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.HJSE.ToString("F2")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    decimal num14 = detail.HJJE + detail.HJSE;
                    cell.SetCellValue(new HSSFRichTextString(num14.ToString("F2")));
                    if (this._isSPBMVersion)
                    {
                        cell = (HSSFCell) row.CreateCell(column);
                        column++;
                        cell.CellStyle = contentstyle;
                        cell.SetCellValue(new HSSFRichTextString(detail.BMBBBH));
                    }
                    int num5 = 0;
                    int num6 = 0;
                    if (detail.GoodsList.Count > 0)
                    {
                        decimal num7 = 0M;
                        decimal num8 = 0M;
                        int num9 = 1;
                        int num10 = column;
                        foreach (GoodsInfo info in detail.GoodsList)
                        {
                            if (info != null)
                            {
                                num10 = column;
                                cell = (HSSFCell) row.CreateCell(num10);
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(num9.ToString()));
                                num9++;
                                cell = (HSSFCell) row.CreateCell(num10);
                                num5 = num10;
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Name));
                                cell = (HSSFCell) row.CreateCell(num10);
                                num6 = num10;
                                num10++;
                                cell.CellStyle = contentstyle;
                                cell.SetCellValue(new HSSFRichTextString(info.Amount.ToString("F2")));
                                if (this._isSPBMVersion)
                                {
                                    cell = (HSSFCell) row.CreateCell(num10);
                                    num10++;
                                    cell.CellStyle = contentstyle;
                                    cell.SetCellValue(new HSSFRichTextString(detail.SPBM.ToString()));
                                }
                                num7 += info.Amount;
                                num8 += info.Tax;
                                curRowNo++;
                                row = (HSSFRow) sheet.CreateRow(curRowNo);
                            }
                        }
                        if (detail.GoodsList.Count > 1)
                        {
                            cell = (HSSFCell) row.CreateCell(num5);
                            num10++;
                            cell.CellStyle = contentstyle;
                            cell.SetCellValue(new HSSFRichTextString(subTotalText));
                            cell = (HSSFCell) row.CreateCell(num6);
                            num10++;
                            cell.CellStyle = contentstyle;
                            cell.SetCellValue(new HSSFRichTextString(num7.ToString("F2")));
                        }
                        else
                        {
                            curRowNo--;
                        }
                    }
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                    curRowNo++;
                }
            }
            return curRowNo;
        }

        private int SetCellContentJDC(Worksheet sheet, List<FPDetail> sfpList, int curRowNo, Font otherFont, Font contentFont, string[] colNames, string totalText, string subTotalText)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            this.SetHeaderCellValue(sheet, colNames, curRowNo, contentFont, -4108);
            curRowNo++;
            int num3 = curRowNo;
            foreach (FPDetail detail in sfpList)
            {
                if (detail != null)
                {
                    sheet.get_Cells().set__Default(curRowNo, 1, "'" + detail.FPDM);
                    sheet.get_Cells().set__Default(curRowNo, 2, "'" + detail.FPHM);
                    sheet.get_Cells().set__Default(curRowNo, 3, "'" + detail.GFMC);
                    string xSBM = "";
                    if (string.IsNullOrEmpty(detail.GFSH))
                    {
                        xSBM = detail.XSBM;
                    }
                    else
                    {
                        xSBM = detail.GFSH;
                    }
                    sheet.get_Cells().set__Default(curRowNo, 4, "'" + xSBM);
                    sheet.get_Cells().set__Default(curRowNo, 5, "'" + detail.SCCJMC);
                    sheet.get_Cells().set__Default(curRowNo, 6, "'" + detail.GFDZDH);
                    sheet.get_Cells().set__Default(curRowNo, 7, "'" + detail.XFDZ);
                    sheet.get_Cells().set__Default(curRowNo, 8, "'" + detail.KHYHMC);
                    sheet.get_Cells().set__Default(curRowNo, 9, "'" + detail.CM);
                    sheet.get_Cells().set__Default(curRowNo, 10, "'" + detail.TYDH);
                    sheet.get_Cells().set__Default(curRowNo, 11, "'" + detail.QYD);
                    sheet.get_Cells().set__Default(curRowNo, 12, "'" + detail.ZHD);
                    sheet.get_Cells().set__Default(curRowNo, 13, "'" + detail.XHD);
                    sheet.get_Cells().set__Default(curRowNo, 14, detail.KPRQ.ToString("yyyy-MM-dd"));
                    sheet.get_Cells().set__Default(curRowNo, 15, "'" + detail.HJJE.ToString("F2"));
                    sheet.get_Cells().set__Default(curRowNo, 0x10, "'" + detail.SLV.ToString("0.00"));
                    sheet.get_Cells().set__Default(curRowNo, 0x11, "'" + detail.HJSE.ToString("F2"));
                    curRowNo++;
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                }
            }
            sheet.get_Range(sheet.get_Cells().get__Default(num3, 14), sheet.get_Cells().get__Default(curRowNo - 1, 14)).set_HorizontalAlignment((Constants) (-4152));
            Range range = sheet.get_Range(sheet.get_Cells().get__Default(num3, 1), sheet.get_Cells().get__Default(curRowNo - 1, colNames.Length));
            range.get_EntireColumn().AutoFit();
            SetRangeFormat(range, contentFont, -4131);
            curRowNo++;
            SetMergeCellValue(sheet, string.Format(totalText, sfpList.Count, num.ToString("F2"), num2.ToString("F2")), curRowNo, colNames.Length, otherFont, false, -4131);
            curRowNo++;
            return curRowNo;
        }

        private int SetCellContentJDC_(HSSFSheet sheet, List<FPDetail> sfpList, int curRowNo, string[] colNames, string totalText, string subTotalText, HSSFCellStyle contentstyle, HSSFCellStyle otherstyle, HSSFCellStyle blackStyle)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            HSSFRow row = (HSSFRow) sheet.CreateRow(curRowNo);
            HSSFCell cell = null;
            for (int i = 0; i < colNames.Length; i++)
            {
                cell = (HSSFCell) row.CreateCell(i);
                cell.CellStyle = blackStyle;
                cell.SetCellValue(new HSSFRichTextString(colNames[i]));
            }
            curRowNo++;
            foreach (FPDetail detail in sfpList)
            {
                row = (HSSFRow) sheet.CreateRow(curRowNo);
                if (detail != null)
                {
                    int column = 0;
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.FPDM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(string.Format("{0:00000000}", detail.FPHM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.KPRQ.ToString("yyyy-MM-dd")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFMC));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XSBM));
                    string gFSH = "";
                    if (string.IsNullOrEmpty(detail.GFSH))
                    {
                        gFSH = "";
                    }
                    else
                    {
                        gFSH = detail.GFSH;
                    }
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(gFSH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.GFDZDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XFDZ));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.KHYHMC));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.CM));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.TYDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.QYD));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.ZHD));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XHD));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    decimal num5 = detail.HJJE + detail.HJSE;
                    cell.SetCellValue(new HSSFRichTextString(num5.ToString("0.00")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XFDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.KHYHZH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XFDZDH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.XFYHZH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    float num6 = detail.SLV * 100f;
                    cell.SetCellValue(new HSSFRichTextString(num6.ToString()));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.HJSE.ToString("F2")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.HJJE.ToString("F2")));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.YYZZH));
                    cell = (HSSFCell) row.CreateCell(column);
                    column++;
                    cell.CellStyle = contentstyle;
                    cell.SetCellValue(new HSSFRichTextString(detail.MDD));
                    if (this._isSPBMVersion)
                    {
                        cell = (HSSFCell) row.CreateCell(column);
                        column++;
                        cell.CellStyle = contentstyle;
                        cell.SetCellValue(new HSSFRichTextString(detail.BMBBBH));
                        cell = (HSSFCell) row.CreateCell(column);
                        column++;
                        cell.CellStyle = contentstyle;
                        cell.SetCellValue(new HSSFRichTextString(detail.SPBM));
                    }
                    num += detail.HJJE;
                    num2 += detail.HJSE;
                    curRowNo++;
                }
            }
            return curRowNo;
        }

        private void SetFPLZ()
        {
            try
            {
                if (this.isJDCOnly)
                {
                    for (int i = 0; i < this.cmbPiaoZhong.Items.Count; i++)
                    {
                        if (this.cmbPiaoZhong.Items[i] == "机动车销售统一发票")
                        {
                            this.cmbPiaoZhong.SelectedIndex = i;
                            this.cmbPiaoZhong.Enabled = false;
                            return;
                        }
                    }
                    this.cmbPiaoZhong.Items.Add("机动车销售统一发票");
                    this.cmbPiaoZhong.SelectedIndex = this.cmbPiaoZhong.Items.Count - 1;
                    this.cmbPiaoZhong.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
            }
        }

        private void SetHeaderCellValue(Worksheet sheet, string[] colNames, int curRowNo, Font contentFont, Constants constants)
        {
            int num = 1;
            foreach (string str in colNames)
            {
                sheet.get_Cells().set__Default(curRowNo, num, str);
                num++;
            }
            Range range = sheet.get_Range(sheet.get_Cells().get__Default(curRowNo, 1), sheet.get_Cells().get__Default(curRowNo, num - 1));
            range.get_EntireColumn().AutoFit();
            SetRangeFormat(range, contentFont, -4108);
            range.get_Borders().set_LineStyle(1);
            range.get_Interior().set_Color(ColorTranslator.ToOle(Color.Silver));
        }

        private static void SetMergeCellValue(Worksheet sheet, string text, int rowNo, int colCount, Font textFont, bool bold, Constants horizontalAlignment)
        {
            Range range = sheet.get_Range(sheet.get_Cells().get__Default(rowNo, 1), sheet.get_Cells().get__Default(rowNo, colCount));
            range.Merge(Missing.Value);
            range.set_Value2(text);
            SetRangeFormat(range, textFont, horizontalAlignment);
            range.get_Font().set_Bold(bold);
        }

        private static void SetRangeFormat(Range range, Font textFont, Constants horizontalAlignment)
        {
            range.get_Font().set_Size(textFont.Size);
            range.get_Font().set_Name(textFont.Name);
            range.set_HorizontalAlignment(horizontalAlignment);
        }

        private void tbGFMC_TextChanged(object sender, EventArgs e)
        {
            string text = this.tbGFMC.Text;
            int byteCount = ToolUtil.GetByteCount(text);
            int selectionStart = this.tbGFMC.SelectionStart;
            if (byteCount > 100)
            {
                this.tbGFMC.Text = this.preGfmcText;
                this.tbGFMC.SelectionStart = selectionStart;
            }
            else
            {
                this.preGfmcText = text;
            }
        }

        private void tbGFSH_TextChanged(object sender, EventArgs e)
        {
            string text = this.tbGFSH.Text;
            if (!Regex.IsMatch(text, "^[A-Z0-9]+$") && !string.IsNullOrEmpty(text))
            {
                this.tbGFSH.Text = this.preGfshText;
            }
            else
            {
                this.preGfshText = text;
            }
        }

        private delegate void PerformStepHandle(object step);
    }
}

