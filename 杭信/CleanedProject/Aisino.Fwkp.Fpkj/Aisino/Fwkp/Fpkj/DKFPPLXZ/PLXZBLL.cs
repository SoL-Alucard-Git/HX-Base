namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml;
    using Framework.Plugin.Core.Util;
    internal class PLXZBLL
    {
        private ILog loger = LogUtil.GetLogger<PLXZ>();
        private TaxCard taxCard = TaxCard.CreateInstance(CTaxCardType.const_7);

        public void AnalysCXResult(string cxResultFromServer)
        {
            try
            {
                this.loger.Debug("进入：AnalysCXResult：" + cxResultFromServer);
                XmlDocument document = new XmlDocument();
                document.LoadXml(cxResultFromServer);
                XmlNodeList elementsByTagName = document.GetElementsByTagName("data");
                if ((elementsByTagName == null) && (elementsByTagName.Count < 1))
                {
                    this.loger.Debug("AnalysCXResult:未解析出数据：" + cxResultFromServer);
                }
                else
                {
                    string innerText = elementsByTagName[0].InnerText;
                    Project project = new Json(innerText).Deserializer();
                    if (project == null)
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("对内容json化失败：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (project.key1 == "00")
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("当期没有待归集数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (project.key1 == "03")
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("系统异常：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (project.key1 == "08")
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("超过最大请求数：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (project.key1 == "09")
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("厂商id非法：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (project.key1 == "01")
                    {
                        if (project.key2 == "")
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("服务器未返回抵扣发票信息：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            string[] strArray = project.key2.Split(new char[] { '=' });
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                if (!ConfigProperty.dictFileInfo.ContainsKey(strArray[i]))
                                {
                                    ConfigProperty.dictFileInfo.Add(strArray[i], "0");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageHelper.MsgWait();
                        MessageBox.Show("未定义错误：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.loger.Error("AnalysCXResult:" + exception.ToString());
            }
        }

        public void AnalysXZResult(string xzResultFromServer, string filePath, string fileName)
        {
            if (xzResultFromServer != "")
            {
                try
                {
                    this.loger.Debug("进入：AnalysXZResult：" + xzResultFromServer);
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(xzResultFromServer);
                    XmlNodeList elementsByTagName = document.GetElementsByTagName("data");
                    if ((elementsByTagName == null) && (elementsByTagName.Count < 1))
                    {
                        this.loger.Debug("AnalysXZResult:未解析出数据：" + xzResultFromServer);
                    }
                    else
                    {
                        string innerText = elementsByTagName[0].InnerText;
                        Project project = new Json(innerText).Deserializer();
                        if (project == null)
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("对内容json化失败：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (project.key1 == "01")
                        {
                            string s = HttpUtility.UrlDecode(project.key2, Encoding.GetEncoding(ConfigProperty.charCode));
                            using (MemoryStream stream = new MemoryStream(Encoding.GetEncoding(ConfigProperty.charCode).GetBytes(s)))
                            {
                                string path = filePath + @"\" + fileName;
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                                FileStream stream2 = new FileStream(path, FileMode.Create, FileAccess.Write);
                                stream.WriteTo(stream2);
                                stream2.Flush();
                                stream2.Dispose();
                            }
                            ConfigProperty.dictFileInfo[fileName] = "1";
                        }
                        else if (project.key1 == "03")
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("系统异常：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (project.key1 == "08")
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("超过最大请求数：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else if (project.key1 == "09")
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("厂商id非法：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            MessageHelper.MsgWait();
                            MessageBox.Show("未定义错误：" + innerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.loger.Error("AnalysXZResult:" + exception.ToString());
                }
            }
        }

        public string GetCXParam()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "");
            document.AppendChild(newChild);
            try
            {
                XmlElement element = document.CreateElement("data");
                element.SetAttribute("version", "1.0");
                element.InnerText = "fpxzcx;" + this.taxCard.TaxCode + ";" + ConfigProperty.csid;
                document.AppendChild(element);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.loger.Error("GetCXParam:" + exception.ToString());
            }
            return document.InnerXml;
        }

        public string GetDownloadParam(string fileName)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "");
            document.AppendChild(newChild);
            try
            {
                XmlElement element = document.CreateElement("data");
                element.SetAttribute("version", "1.0");
                element.InnerText = "fpxz;" + this.taxCard.TaxCode + ";" + fileName + ";" + ConfigProperty.csid;
                document.AppendChild(element);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.loger.Error("GetDownloadParam:" + exception.ToString());
            }
            return document.InnerXml;
        }
    }
}

