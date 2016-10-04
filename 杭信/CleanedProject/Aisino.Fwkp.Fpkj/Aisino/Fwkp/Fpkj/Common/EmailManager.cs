namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Mail;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Form.SendFP;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class EmailManager
    {
        private string _strFilePath = string.Empty;
        private XMLOperation _XmlOperation = new XMLOperation();
        private ILog loger = LogUtil.GetLogger<FaPiaoChaXun>();
        public static string strEmailGeShi = ".zip";
        public static string strRecePersonAdressQueRen = "发送时进行收件人地址确认";
        public static string[] strRefaultJieZhiOut = new string[] { "导出成功", "导出失败" };
        public static string[] strRefaultSendEmail = new string[] { "发送成功", "发送失败" };
        public static string strSendEmailPersonAddress = "发件人邮件地址";
        public static string strTempPathEmail = @"\Bin";
        public static string strTempPathEmailError = @"\EmailFile\ErrInfo";
        public static string strTitleEmailSend = "邮件发送结果";
        public static string strTitleJieZhiOut = "介质导出结果";
        public static string[] strZhiDuanEmailSend = new string[] { "FromEmail", "ToEmail", "ContentEmail", "FileEmail", "RefaultSend", "BeiZhuSend" };
        public static string[] strZhiDuanJieZhiOut = new string[] { "ReceTaxJieZhi", "FilePathJieZhi", "OutFileJieZhi", "RefaultJieZhi", "BeiZhuJieZhi" };

        private string GetInvoiceType(string type)
        {
            switch (type)
            {
                case "普通发票":
                    return "c";

                case "专用发票":
                    return "s";

                case "农产品销售发票":
                    return "c";

                case "收购发票":
                    return "c";

                case "机动车销售统一发票":
                    return "j";

                case "货物运输业增值税专用发票":
                    return "f";
            }
            return "s";
        }

        private void GetQyxx(out string strKhmc, string strKhsh, string strKmdzdh, string strKhyhzh, out string strEmail, out string strKhshOut)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            try
            {
                object[] objArray = new object[] { strKhsh.Trim(), 1 };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMGLGetKHbySH", objArray);
                strKhmc = string.Empty;
                if (objArray2 == null)
                {
                    str = string.Empty;
                    strKhsh = string.Empty;
                    strKmdzdh = string.Empty;
                    strKhyhzh = string.Empty;
                    str2 = string.Empty;
                }
                if (objArray2.Length > 4)
                {
                    str = objArray2[0].ToString();
                    strKhsh = objArray2[1].ToString();
                    strKmdzdh = objArray2[2].ToString();
                    strKhyhzh = objArray2[3].ToString();
                    str2 = objArray2[4].ToString();
                }
                else
                {
                    str = string.Empty;
                    strKhsh = string.Empty;
                    strKmdzdh = string.Empty;
                    strKhyhzh = string.Empty;
                    str2 = string.Empty;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            strKhmc = str;
            strEmail = str2;
            strKhshOut = strKhsh;
        }

        private List<Fpxx> GetXXFPList(Fpxx fpxxTemp)
        {
            try
            {
                if (fpxxTemp != null)
                {
                    return new List<Fpxx> { fpxxTemp };
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return null;
            }
            return null;
        }

        public void SaveXmlToDisk(List<Fpxx> listModel)
        {
            try
            {
                if ((listModel == null) || (listModel.Count <= 0))
                {
                    MessageManager.ShowMsgBox("FPCX-000005");
                }
                else
                {
                    Fpxx fpxx = listModel[0];
                    if (fpxx.zfbz || (fpxx.mw.Length <= 0))
                    {
                        MessageManager.ShowMsgBox("FPCX-000008");
                    }
                    else
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        string str = string.Empty;
                        if (!string.IsNullOrEmpty(str))
                        {
                            if (Directory.Exists(str))
                            {
                                dialog.SelectedPath = str;
                            }
                        }
                        else
                        {
                            dialog.SelectedPath = Application.StartupPath;
                        }
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedPath = dialog.SelectedPath;
                            this.FILE_PATH = selectedPath;
                        }
                        else
                        {
                            return;
                        }
                        this._XmlOperation.FILE_PATH = this.FILE_PATH;
                        string str3 = string.Empty;
                        string str4 = string.Empty;
                        List<Dictionary<string, object>> listEmailInfo = new List<Dictionary<string, object>>();
                        while (listModel.Count > 0)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            List<Fpxx> list2 = new List<Fpxx> {
                                listModel[0]
                            };
                            for (int i = listModel.Count - 1; i > 0; i--)
                            {
                                if (listModel[i].gfsh.Trim() == listModel[0].gfsh.Trim())
                                {
                                    list2.Add(listModel[i]);
                                    listModel.RemoveAt(i);
                                }
                            }
                            listModel.RemoveAt(0);
                            string strKhsh = "";
                            if ((list2[0].fplx == 0) || (list2[0].fplx == FPLX.PTFP))
                            {
                                strKhsh = list2[0].gfsh.Trim();
                            }
                            else if (list2[0].fplx == FPLX.JDCFP)
                            {
                                if (list2[0].yysbz[4] == '2')
                                {
                                    strKhsh = list2[0].gfsh.Trim();
                                }
                                else
                                {
                                    strKhsh = list2[0].sfzhm.Trim();
                                }
                            }
                            else
                            {
                                strKhsh = list2[0].cyrnsrsbh.Trim();
                            }
                            string strKhmc = string.Empty;
                            string strEmail = string.Empty;
                            if (strKhsh == "")
                            {
                                this.GetQyxx(out strKhmc, strKhsh, string.Empty, string.Empty, out strEmail, out strKhsh);
                            }
                            if (string.IsNullOrEmpty(strKhmc.Trim()))
                            {
                                strKhmc = list2[0].gfmc.Trim();
                            }
                            if (list2[0].gfsh.Trim() != strKhsh.Trim())
                            {
                                list2[0].gfsh = strKhsh.Trim();
                            }
                            if (string.IsNullOrEmpty(strKhsh))
                            {
                                return;
                            }
                            int num2 = 0;
                            item.Add(strZhiDuanJieZhiOut[num2++], strKhsh);
                            item.Add(strZhiDuanJieZhiOut[num2++], this.FILE_PATH);
                            string filePathZip = string.Empty;
                            string fileName = string.Empty;
                            string.Format("向购方税号为: {0} 的企业导出发票...", strKhsh);
                            if (this._XmlOperation.OutXmlToDiskCard(list2, out filePathZip))
                            {
                                str3 = str3 + "\n" + filePathZip;
                                fileName = ShareMethods.GetFileName(filePathZip);
                                item.Add(strZhiDuanJieZhiOut[num2++], fileName);
                                item.Add(strZhiDuanJieZhiOut[num2++], strRefaultJieZhiOut[0]);
                                item.Add(strZhiDuanJieZhiOut[num2++], "");
                            }
                            else
                            {
                                str4 = str4 + "\n" + filePathZip;
                                item.Add(strZhiDuanJieZhiOut[num2++], "");
                                item.Add(strZhiDuanJieZhiOut[num2++], strRefaultJieZhiOut[1]);
                                item.Add(strZhiDuanJieZhiOut[num2++], "生成压缩文件失败");
                            }
                            listEmailInfo.Add(item);
                        }
                        EmailOutFilePrompt.OutFileType = 1;
                        EmailOutFilePrompt prompt = new EmailOutFilePrompt(listEmailInfo);
                        prompt.SetFormTitle(strTitleJieZhiOut);
                        prompt.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SendEmail_FPTK(Fpxx fpxxTemp)
        {
        }

        public void SendXmlByEmail(List<Fpxx> listModel)
        {
            try
            {
                if ((listModel == null) || (0 >= listModel.Count))
                {
                    MessageManager.ShowMsgBox("FPCX-000005");
                }
                else
                {
                    Fpxx fpxx = listModel[0];
                    if (fpxx.zfbz || (fpxx.mw.Length <= 0))
                    {
                        MessageManager.ShowMsgBox("FPCX-000007");
                    }
                    else
                    {
                        string str = PropertyUtil.GetValue("SMTP_SERVER").Trim();
                        string s = PropertyUtil.GetValue("SMTP_PORT").Trim();
                        int result = 0;
                        int.TryParse(s.Trim(), out result);
                        if (0 >= result)
                        {
                            result = 110;
                        }
                        string str3 = PropertyUtil.GetValue("SMTP_USER").Trim();
                        string str4 = PropertyUtil.GetValue("SMTP_PASS").Trim();
                        string str5 = string.Empty;
                        this._XmlOperation.FILE_PATH = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf('\\')) + strTempPathEmail;
                        List<Dictionary<string, object>> listEmailInfo = new List<Dictionary<string, object>>();
                        int num2 = 0;
                        while (listModel.Count > 0)
                        {
                            num2++;
                            //这个是Email，我实在懒得想逻辑了
                            List<Fpxx> list2 = new List<Fpxx>();
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            //Dictionary<string, object> item = new Dictionary<string, object> {
                            //    listModel[0]
                            //};
                            for (int i = listModel.Count - 1; i > 0; i--)
                            {
                                if (listModel[i].gfsh.Trim() == listModel[0].gfsh.Trim())
                                {
                                    list2.Add(listModel[i]);
                                    listModel.RemoveAt(i);
                                }
                            }
                            listModel.RemoveAt(0);
                            string strEmail = string.Empty;
                            string strKhsh = list2[0].gfsh.Trim();
                            string strKhmc = list2[0].gfmc.Trim();
                            string fromEmail = PropertyUtil.GetValue("SMTP_USER");
                            int count = list2.Count;
                            string str10 = PropertyUtil.GetValue("SMTP_USER");
                            bool flag = str10 != "0";
                            flag = (str10 != string.Empty) && flag;
                            this.GetQyxx(out strKhmc, strKhsh, string.Empty, string.Empty, out strEmail, out strKhsh);
                            if (string.IsNullOrEmpty(strKhmc.Trim()))
                            {
                                strKhmc = list2[0].gfmc.Trim();
                            }
                            if (list2[0].gfsh.Trim() != strKhsh.Trim())
                            {
                                list2[0].gfsh = strKhsh.Trim();
                            }
                            if (string.IsNullOrEmpty(strKhsh))
                            {
                                return;
                            }
                            string subject = string.Empty;
                            string body = string.Empty;
                            SendFaPiao piao = new SendFaPiao(strEmail.Trim(), strKhsh.Trim(), strKhmc.Trim(), fromEmail.Trim(), count);
                            if (((flag || string.IsNullOrEmpty(fromEmail)) || string.IsNullOrEmpty(strEmail)) && (DialogResult.OK != piao.ShowDialog()))
                            {
                                return;
                            }
                            strEmail = piao.ToEmail;
                            fromEmail = piao.FromEmail;
                            subject = piao.Subject;
                            body = piao.Body;
                            int num5 = 0;
                            item.Add(strZhiDuanEmailSend[num5++], fromEmail);
                            item.Add(strZhiDuanEmailSend[num5++], strEmail);
                            item.Add(strZhiDuanEmailSend[num5++], body);
                            string.Format("向购方邮箱:{0}发送邮件...", strEmail);
                            string filePathZip = string.Empty;
                            if (this._XmlOperation.OutXmlToDiskCard(list2, out filePathZip))
                            {
                                List<string> list3 = new List<string> {
                                    filePathZip
                                };
                                string str14 = filePathZip;
                                int startIndex = str14.LastIndexOf(@"\") + 1;
                                int length = str14.Length - startIndex;
                                str14 = str14.Substring(startIndex, length);
                                item.Add(strZhiDuanEmailSend[num5++], str14);
                                string[] strArray = new string[] { strEmail };
                                if (MailService.SendMail(fromEmail, strKhmc, strArray, subject, body, str3, str4, str, int.Parse(s), null, out str5))
                                {
                                    item.Add(strZhiDuanEmailSend[num5++], strRefaultSendEmail[0]);
                                    item.Add(strZhiDuanEmailSend[num5++], "");
                                }
                                else
                                {
                                    item.Add(strZhiDuanEmailSend[num5++], strRefaultSendEmail[1]);
                                    item.Add(strZhiDuanEmailSend[num5++], MessageManager.GetMessageInfo(str5));
                                }
                            }
                            else
                            {
                                item.Add(strZhiDuanEmailSend[num5++], filePathZip);
                                item.Add(strZhiDuanEmailSend[num5++], strRefaultSendEmail[1]);
                                string messageInfo = MessageManager.GetMessageInfo("生成压缩文件失败");
                                item.Add(strZhiDuanEmailSend[num5++], messageInfo);
                            }
                            listEmailInfo.Add(item);
                        }
                        EmailOutFilePrompt.OutFileType = 0;
                        EmailOutFilePrompt prompt = new EmailOutFilePrompt(listEmailInfo);
                        prompt.SetFormTitle(strTitleEmailSend);
                        prompt.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox("发送邮件失败");
            }
        }

        private string FILE_PATH
        {
            get
            {
                return this._strFilePath.Trim();
            }
            set
            {
                this._strFilePath = value.Trim();
            }
        }
    }
}

