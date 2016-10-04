namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using log4net;
    using NPOI.HSSF.UserModel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class PLXZ
    {
        private ILog loger = LogUtil.GetLogger<PLXZ>();

        public void BeginDownload()
        {
            try
            {
                FormSaveFile file = new FormSaveFile();
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string downloadSavePath = file.DownloadSavePath;
                    if ((downloadSavePath == null) || (downloadSavePath == ""))
                    {
                        MessageBox.Show("所选择的文件保存路径为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        if (ConfigProperty.dictFileInfo == null)
                        {
                            ConfigProperty.dictFileInfo = new Dictionary<string, string>();
                        }
                        this.CXFPInfo();
                        this.DownloadFPFile(downloadSavePath);
                        this.SaveDownloadResult(downloadSavePath);
                        if (ConfigProperty.dictFileInfo.Count > 0)
                        {
                            MessageBox.Show("抵扣发票已下载完成，详情查看《" + (downloadSavePath + @"\抵扣发票下载结果" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls") + "》。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            ConfigProperty.dictFileInfo.Clear();
                            ConfigProperty.dictFileInfo = null;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.loger.Error("抵扣发票批量查询下载异常：" + exception.ToString());
            }
        }

        private void CXFPInfo()
        {
            MessageHelper.MsgWait("正在查询服务器上抵扣发票信息，请耐心等待...");
            try
            {
                PLXZBLL plxzbll = new PLXZBLL();
                string cXParam = plxzbll.GetCXParam();
                int num = -1;
                string text = "";
                this.loger.Debug("查询发送受理开始");
                num = HttpsSender.SendMsg(ConfigProperty.opType, cXParam, out text);
                this.loger.Debug("查询发送受理结束");
                if (num != 0)
                {
                    MessageHelper.MsgWait();
                    MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (text == "")
                {
                    MessageHelper.MsgWait();
                    MessageBox.Show("未查询到抵扣发票信息，服务器返回为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    ConfigProperty.dictFileInfo.Clear();
                    plxzbll.AnalysCXResult(text);
                }
            }
            catch (Exception exception)
            {
                MessageHelper.MsgWait();
                MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.loger.Error("CXFPInfo:" + exception.ToString());
            }
            MessageHelper.MsgWait();
        }

        private void DownloadFPFile(string filePath)
        {
            if ((ConfigProperty.dictFileInfo.Count < 1) || !ConfigProperty.dictFileInfo.ContainsValue("0"))
            {
                MessageHelper.MsgWait();
            }
            else
            {
                MessageHelper.MsgWait("正在下载服务器上抵扣发票信息，请耐心等待...");
                PLXZBLL plxzbll = new PLXZBLL();
                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> pair in ConfigProperty.dictFileInfo)
                    {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                    foreach (KeyValuePair<string, string> pair2 in dictionary)
                    {
                        if (pair2.Value == "0")
                        {
                            string downloadParam = plxzbll.GetDownloadParam(pair2.Key);
                            string text = "";
                            this.loger.Debug("下载发送受理开始");
                            int num = HttpsSender.SendMsg("0035", downloadParam, out text);
                            this.loger.Debug("下载发送受理结束");
                            if (num != 0)
                            {
                                MessageHelper.MsgWait();
                                MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else if (text == "")
                            {
                                MessageHelper.MsgWait();
                                MessageBox.Show("未下载到抵扣发票文件，服务器返回为空：" + pair2.Key, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                plxzbll.AnalysXZResult(text, filePath, pair2.Key);
                            }
                            MessageHelper.MsgWait();
                        }
                    }
                    dictionary.Clear();
                    dictionary = null;
                }
                catch (Exception exception)
                {
                    MessageHelper.MsgWait();
                    MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.loger.Error("CXFPInfo:" + exception.ToString());
                }
                MessageHelper.MsgWait();
            }
        }

        private void SaveDownloadResult(string filePath)
        {
            if (ConfigProperty.dictFileInfo.Count >= 1)
            {
                MessageHelper.MsgWait("正在保存抵扣发票下载信息，请耐心等待...");
                try
                {
                    string path = filePath + @"\抵扣发票下载结果" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    HSSFSheet sheet = (HSSFSheet) workbook.CreateSheet("下载结果");
                    HSSFRow row = (HSSFRow) sheet.CreateRow(0);
                    HSSFCell cell = (HSSFCell) row.CreateCell(0);
                    cell.SetCellValue("序号");
                    HSSFCell cell2 = (HSSFCell) row.CreateCell(1);
                    cell2.SetCellValue("文件名");
                    HSSFCell cell3 = (HSSFCell) row.CreateCell(2);
                    cell3.SetCellValue("下载情况");
                    int rownum = 1;
                    foreach (KeyValuePair<string, string> pair in ConfigProperty.dictFileInfo)
                    {
                        row = (HSSFRow) sheet.CreateRow(rownum);
                        ((HSSFCell) row.CreateCell(0)).SetCellValue((double) rownum);
                        ((HSSFCell) row.CreateCell(1)).SetCellValue(pair.Key);
                        cell3 = (HSSFCell) row.CreateCell(2);
                        if (pair.Value == "1")
                        {
                            cell3.SetCellValue("已下载");
                        }
                        else
                        {
                            cell3.SetCellValue("未下载");
                        }
                        rownum++;
                    }
                    using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(stream);
                    }
                }
                catch (Exception exception)
                {
                    MessageHelper.MsgWait();
                    MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.loger.Error("抵扣发票批量下载：保存下载结果异常：" + exception.ToString());
                }
                MessageHelper.MsgWait();
            }
        }
    }
}

