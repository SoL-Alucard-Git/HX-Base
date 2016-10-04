namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using ns6;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class MessageManager
    {
        private MessageBoxButtons messageBoxButtons_0;
        private MessageBoxDefaultButton messageBoxDefaultButton_0;
        private MessageBoxIcon messageBoxIcon_0;
        public const string MSG_SEARCH_PATH = "root/Message/";
        private static string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private static string string_8;

        static MessageManager()
        {
            
            string_0 = @"..\Resources\AppMessage\Aisino.Message.xml";
        }

        internal MessageManager(string string_9)
        {
            
            this.method_0(string_9);
        }

        public static string GetMessageInfo(string string_9)
        {
            string str2;
            try
            {
                string text1 = "root/Message/" + string_9;
                Hashtable hashtable = new Hashtable();
                hashtable = smethod_2(string_9);
                if ((hashtable != null) && hashtable.ContainsKey("value"))
                {
                    return (hashtable["value"] as string);
                }
                str2 = string_9;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string GetMessageInfo(string string_9, object[] object_0)
        {
            string str2;
            try
            {
                Hashtable hashtable = new Hashtable();
                string format = "";
                format = smethod_2(string_9)["value"] as string;
                if (format != null)
                {
                    if (object_0 == null)
                    {
                        return format;
                    }
                    return string.Format(format, object_0);
                }
                str2 = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string GetMessageSolution(string string_9, object[] object_0)
        {
            string str2;
            try
            {
                Hashtable hashtable = new Hashtable();
                string format = "";
                format = smethod_2(string_9)["solution"] as string;
                if (format != null)
                {
                    format = format.Replace("证书接口返回错误信息：", "");
                    if (object_0 == null)
                    {
                        return format;
                    }
                    return string.Format(format, object_0);
                }
                str2 = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string GetMessageTitle(string string_9)
        {
            string str2;
            try
            {
                Hashtable hashtable = new Hashtable();
                string str = "";
                str = smethod_2(string_9)["title"] as string;
                str2 = str;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string GetMessageType(string string_9)
        {
            string str3;
            try
            {
                string str = "root/Message/" + string_9;
                smethod_0();
                Hashtable hashtable = new Hashtable();
                string str2 = "";
                str2 = XmlManager.GetAttributes(string_8, str, "id", string_9)["type"] as string;
                str3 = str2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str3;
        }

        private void method_0(string string_9)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable = smethod_2(string_9);
                this.string_1 = hashtable["id"] as string;
                this.string_3 = hashtable["type"] as string;
                this.string_2 = hashtable["value"] as string;
                if (!string.IsNullOrEmpty(this.string_2))
                {
                    this.string_2 = this.string_2.Replace(@"\n\r", Environment.NewLine);
                    this.string_2 = this.string_2.Replace(@"\r\n", Environment.NewLine);
                    this.string_2 = this.string_2.Replace(@"\n", Environment.NewLine);
                }
                this.string_4 = hashtable["title"] as string;
                this.string_5 = hashtable["operate"] as string;
                this.string_6 = hashtable["solution"] as string;
                if (!string.IsNullOrEmpty(this.string_6))
                {
                    this.string_6 = this.string_6.Replace(@"\n\r", Environment.NewLine);
                    this.string_6 = this.string_6.Replace(@"\r\n", Environment.NewLine);
                    this.string_6 = this.string_6.Replace(@"\n", Environment.NewLine);
                }
                this.string_7 = hashtable["funcode"] as string;
                this.method_1(hashtable);
            }
            catch (Exception)
            {
            }
        }

        private void method_1(Hashtable hashtable_0)
        {
            try
            {
                string str3;
                switch (hashtable_0["MsgBoxButtons"].ToString())
                {
                    case "0":
                        this.messageBoxButtons_0 = MessageBoxButtons.AbortRetryIgnore;
                        break;

                    case "1":
                        this.messageBoxButtons_0 = MessageBoxButtons.OK;
                        break;

                    case "2":
                        this.messageBoxButtons_0 = MessageBoxButtons.OKCancel;
                        break;

                    case "3":
                        this.messageBoxButtons_0 = MessageBoxButtons.RetryCancel;
                        break;

                    case "4":
                        this.messageBoxButtons_0 = MessageBoxButtons.YesNo;
                        break;

                    case "5":
                        this.messageBoxButtons_0 = MessageBoxButtons.YesNoCancel;
                        break;

                    default:
                        this.messageBoxButtons_0 = MessageBoxButtons.OK;
                        break;
                }
                switch (hashtable_0["MsgBoxIcon"].ToString())
                {
                    case "0":
                        this.messageBoxIcon_0 = MessageBoxIcon.Asterisk;
                        break;

                    case "1":
                        this.messageBoxIcon_0 = MessageBoxIcon.Hand;
                        break;

                    case "2":
                        this.messageBoxIcon_0 = MessageBoxIcon.Exclamation;
                        break;

                    case "3":
                        this.messageBoxIcon_0 = MessageBoxIcon.Hand;
                        break;

                    case "4":
                        this.messageBoxIcon_0 = MessageBoxIcon.Asterisk;
                        break;

                    case "5":
                        this.messageBoxIcon_0 = MessageBoxIcon.None;
                        break;

                    case "6":
                        this.messageBoxIcon_0 = MessageBoxIcon.Question;
                        break;

                    case "7":
                        this.messageBoxIcon_0 = MessageBoxIcon.Hand;
                        break;

                    case "8":
                        this.messageBoxIcon_0 = MessageBoxIcon.Exclamation;
                        break;

                    default:
                        switch (hashtable_0["type"].ToString())
                        {
                            case "CE":
                            case "E":
                                this.messageBoxIcon_0 = MessageBoxIcon.Hand;
                                goto Label_035A;

                            case "CI":
                            case "I":
                                this.messageBoxIcon_0 = MessageBoxIcon.Asterisk;
                                goto Label_035A;

                            case "CW":
                            case "W":
                                this.messageBoxIcon_0 = MessageBoxIcon.Exclamation;
                                goto Label_035A;

                            case "CC":
                            case "C":
                                this.messageBoxIcon_0 = MessageBoxIcon.Asterisk;
                                goto Label_035A;

                            case "CQ":
                            case "Q":
                                this.messageBoxIcon_0 = MessageBoxIcon.Question;
                                goto Label_035A;
                        }
                        this.messageBoxIcon_0 = MessageBoxIcon.None;
                        break;
                }
            Label_035A:
                if ((str3 = hashtable_0["MsgBoxDefaultButton"].ToString()) != null)
                {
                    if (str3 == "0")
                    {
                        this.messageBoxDefaultButton_0 = MessageBoxDefaultButton.Button1;
                    }
                    else if (!(str3 == "1"))
                    {
                        if (!(str3 == "2"))
                        {
                            goto Label_03B8;
                        }
                        this.messageBoxDefaultButton_0 = MessageBoxDefaultButton.Button3;
                    }
                    else
                    {
                        this.messageBoxDefaultButton_0 = MessageBoxDefaultButton.Button2;
                    }
                    return;
                }
            Label_03B8:
                this.messageBoxDefaultButton_0 = MessageBoxDefaultButton.Button1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DialogResult ShowMsgBox(string string_9)
        {
            //return MessageBox.Show(string_9, "发生异常", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            DialogResult result2;
            try
            {
                string messageBody = "";
                MessageManager manager = new MessageManager(string_9);
                DialogResult oK = DialogResult.OK;
                if (manager.MessageBody != null)
                {
                    messageBody = manager.MessageBody;
                }
                if (string.Empty.Equals(messageBody))
                {
                    messageBody = string_9;
                }
                if (((!(manager.string_3 == "CC") && !(manager.string_3 == "CI")) && (!(manager.string_3 == "CE") && !(manager.string_3 == "CQ"))) && !(manager.string_3 == "CW"))
                {
                    oK = new SysMessageBox(manager.string_4, messageBody, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                else
                {
                    oK = new CusMessageBox(manager.string_4, DateTime.Now.ToString(), manager.string_5, manager.string_1, messageBody, manager.string_6, manager.string_7, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                result2 = oK;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result2;
        }

        public static DialogResult ShowMsgBox(string string_9, params string[] para)
        {
            return ShowMsgBox(string_9, null, para);
        }

        public static DialogResult ShowMsgBox(string string_9, string string_10)
        {
            DialogResult result2;
            try
            {
                string messageBody = "";
                MessageManager manager = new MessageManager(string_9);
                DialogResult oK = DialogResult.OK;
                if (manager.MessageBody != null)
                {
                    messageBody = manager.MessageBody;
                }
                if (string.Empty.Equals(messageBody))
                {
                    messageBody = string_9;
                }
                if (((!(manager.string_3 == "CC") && !(manager.string_3 == "CI")) && (!(manager.string_3 == "CE") && !(manager.string_3 == "CQ"))) && !(manager.string_3 == "CW"))
                {
                    oK = new SysMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, messageBody, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                else
                {
                    oK = new CusMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, DateTime.Now.ToString(), manager.string_5, manager.string_1, messageBody, manager.string_6, manager.string_7, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                result2 = oK;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result2;
        }

        public static DialogResult ShowMsgBox(string string_9, string string_10, params string[] para)
        {
            DialogResult result2;
            try
            {
                string messageBody = "";
                MessageManager manager = new MessageManager(string_9);
                DialogResult oK = DialogResult.OK;
                if (manager.MessageBody != null)
                {
                    messageBody = manager.MessageBody;
                    if ((para != null) && (para.Length > 0))
                    {
                        for (int i = 0; i < para.Length; i++)
                        {
                            messageBody = messageBody.Replace("{" + i.ToString() + "}", para[i]);
                        }
                    }
                }
                if (string.Empty.Equals(messageBody))
                {
                    messageBody = string_9;
                }
                if (((!(manager.string_3 == "CC") && !(manager.string_3 == "CI")) && (!(manager.string_3 == "CE") && !(manager.string_3 == "CQ"))) && !(manager.string_3 == "CW"))
                {
                    oK = new SysMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, messageBody, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                else
                {
                    oK = new CusMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, DateTime.Now.ToString(), manager.string_5, manager.string_1, messageBody, manager.string_6, manager.string_7, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                result2 = oK;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result2;
        }

        public static DialogResult ShowMsgBox(string string_9, List<KeyValuePair<string, string>> attrsCollection, string[] string_10, string[] string_11)
        {
            DialogResult result2;
            try
            {
                string messageBody = "";
                string solution = "";
                MessageManager manager = new MessageManager(string_9);
                DialogResult oK = DialogResult.OK;
                if ((attrsCollection != null) && (attrsCollection.Count > 0))
                {
                    foreach (KeyValuePair<string, string> pair in attrsCollection)
                    {
                        if (pair.Key.Equals("type"))
                        {
                            if ((pair.Value != null) && (pair.Value != ""))
                            {
                                manager.string_3 = pair.Value;
                            }
                        }
                        else if (pair.Key.Equals("value"))
                        {
                            if ((pair.Value != null) && (pair.Value != ""))
                            {
                                manager.string_2 = pair.Value;
                            }
                        }
                        else if (pair.Key.Equals("title"))
                        {
                            if ((pair.Value != null) && (pair.Value != ""))
                            {
                                manager.string_4 = pair.Value;
                            }
                        }
                        else if (pair.Key.Equals("operate"))
                        {
                            if ((pair.Value != null) && (pair.Value != ""))
                            {
                                manager.string_5 = pair.Value;
                            }
                        }
                        else if ((pair.Key.Equals("solution") && (pair.Value != null)) && (pair.Value != ""))
                        {
                            manager.string_6 = pair.Value;
                        }
                    }
                }
                if (manager.MessageBody != null)
                {
                    messageBody = manager.MessageBody;
                    if ((string_10 != null) && (string_10.Length > 0))
                    {
                        for (int i = 0; i < string_10.Length; i++)
                        {
                            messageBody = messageBody.Replace("{" + i.ToString() + "}", string_10[i]);
                        }
                    }
                }
                if (manager.Solution != null)
                {
                    solution = manager.Solution;
                    if ((string_11 != null) && (string_11.Length > 0))
                    {
                        for (int j = 0; j < string_11.Length; j++)
                        {
                            solution = solution.Replace("{" + j.ToString() + "}", string_11[j]);
                        }
                    }
                }
                if (string.Empty.Equals(messageBody))
                {
                    messageBody = string_9;
                }
                if (((!(manager.string_3 == "CC") && !(manager.string_3 == "CI")) && (!(manager.string_3 == "CE") && !(manager.string_3 == "CQ"))) && !(manager.string_3 == "CW"))
                {
                    oK = new SysMessageBox(manager.string_4, messageBody, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                else
                {
                    oK = new CusMessageBox(manager.string_4, DateTime.Now.ToString(), manager.string_5, manager.string_1, messageBody, solution, manager.string_7, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                result2 = oK;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result2;
        }

        public static DialogResult ShowMsgBox(string string_9, string string_10, string string_11, params string[] msgParams)
        {
            DialogResult result2;
            try
            {
                string messageBody = "";
                MessageManager manager = new MessageManager(string_9);
                DialogResult oK = DialogResult.OK;
                if (manager.MessageBody != null)
                {
                    messageBody = manager.MessageBody;
                    if ((msgParams != null) && (msgParams.Length > 0))
                    {
                        for (int i = 0; i < msgParams.Length; i++)
                        {
                            messageBody = messageBody.Replace("{" + i.ToString() + "}", msgParams[i]);
                        }
                    }
                }
                if (string.Empty.Equals(messageBody))
                {
                    messageBody = string_9;
                }
                if (((!(manager.string_3 == "CC") && !(manager.string_3 == "CI")) && (!(manager.string_3 == "CE") && !(manager.string_3 == "CQ"))) && !(manager.string_3 == "CW"))
                {
                    oK = new SysMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, messageBody, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                else
                {
                    oK = new CusMessageBox(((string_10 == null) || (string_10 == "")) ? manager.string_4 : string_10, DateTime.Now.ToString(), ((string_11 == null) || (string_11 == "")) ? manager.string_5 : string_11, manager.string_1, messageBody, manager.string_6, manager.string_7, manager.messageBoxButtons_0, manager.messageBoxIcon_0, manager.messageBoxDefaultButton_0) { TopMost = true }.ShowDialog();
                }
                result2 = oK;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result2;
        }

        private static void smethod_0()
        {
            try
            {
                if (string_8 == null)
                {
                    string_8 = smethod_1(string_0);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static string smethod_1(string string_9)
        {
            string str = string.Empty;
            try
            {
                string pattern = @"^[a-zA-Z]:\\([\w][\s])*";
                string str3 = @"(\.\.\\)*";
                if (Regex.IsMatch(string_9, pattern))
                {
                    return string_9;
                }
                if (Regex.IsMatch(string_9, str3))
                {
                    bool flag = true;
                    int num2 = 0;
                    string str5 = string_9;
                    while (flag)
                    {
                        if (str5.IndexOf(@"..\", 0) > -1)
                        {
                            num2++;
                            str5 = str5.Substring(3, str5.Length - 3);
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    string executablePath = Application.ExecutablePath;
                    if (!executablePath.EndsWith(@"\"))
                    {
                        executablePath = executablePath.Substring(0, executablePath.LastIndexOf(@"\"));
                    }
                    string str4 = string.Empty;
                    string[] strArray = executablePath.Split(new string[] { @"\" }, StringSplitOptions.None);
                    for (int i = 0; i < (strArray.Length - num2); i++)
                    {
                        str4 = str4 + strArray[i] + @"\";
                    }
                    return (str4 + str5);
                }
                str = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + string_9;
            }
            catch
            {
            }
            return str;
        }

        private static Hashtable smethod_2(string string_9)
        {
            Hashtable hashtable = new Hashtable();
            string str = string.Empty;
            string str2 = string.Empty;
            try
            {
                if (string_9.ToUpper().IndexOf("TCD") > -1)
                {
                    string[] strArray = string_9.Split(new char[] { '_' });
                    str = strArray[1];
                    str2 = strArray[2];
                }
                else
                {
                    str = string_9;
                }
                string str6 = smethod_3();
                if (string_9.ToUpper().IndexOf("CA_") > -1)
                {
                    str = string_9.Split(new char[] { '_' })[1];
                    str2 = "";
                    string str3 = "select * from ERRMSG2 where id='" + str + "'";
                    DataTable table = null;
                    if (string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Server"))
                    {
                        table = Class113.smethod_0(str3);
                    }
                    else
                    {
                        table = Class123.smethod_0(str3);
                    }
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        hashtable.Add("id", str);
                        hashtable.Add("type", "CE");
                        hashtable.Add("value", "证书接口调用失败！");
                        hashtable.Add("title", "证书相关");
                        hashtable.Add("operate", "证书接口调用");
                        if (table.Rows[0]["solution"] != null)
                        {
                            string str8 = table.Rows[0]["solution"].ToString().Replace("\n", Environment.NewLine);
                            hashtable.Add("solution", string.Format("证书接口返回错误信息：{0}", str8));
                        }
                        else
                        {
                            hashtable.Add("solution", string.Format("证书接口返回错误信息：{0}", str));
                        }
                        hashtable.Add("version", "0");
                        hashtable.Add("MsgBoxButtons", "1");
                        hashtable.Add("MsgBoxIcon", "1");
                        hashtable.Add("MsgBoxDefaultButton", "0");
                        hashtable.Add("funcode", str2);
                        return hashtable;
                    }
                    hashtable.Add("id", str);
                    hashtable.Add("type", "CE");
                    hashtable.Add("value", "证书接口调用失败！");
                    hashtable.Add("title", "证书相关");
                    hashtable.Add("operate", "证书接口调用");
                    hashtable.Add("solution", string.Format("证书接口返回错误信息：{0}", str.ToString()));
                    hashtable.Add("version", "0");
                    hashtable.Add("MsgBoxButtons", "1");
                    hashtable.Add("MsgBoxIcon", "1");
                    hashtable.Add("MsgBoxDefaultButton", "0");
                    hashtable.Add("funcode", str2);
                    return hashtable;
                }
                string str5 = "select * from ERRMSG where id='" + str + "' and version <= " + str6 + " order by version desc";
                DataTable table2 = null;
                if (string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Server"))
                {
                    table2 = Class113.smethod_0(str5);
                }
                else
                {
                    table2 = Class123.smethod_0(str5);
                }
                if ((table2 == null) || (table2.Rows.Count <= 0))
                {
                    return hashtable;
                }
                hashtable.Add("id", table2.Rows[0]["id"]);
                hashtable.Add("type", table2.Rows[0]["type"]);
                if (table2.Rows[0]["value"] != null)
                {
                    hashtable.Add("value", table2.Rows[0]["value"]);
                }
                else
                {
                    hashtable.Add("value", "");
                }
                if (table2.Rows[0]["title"] != null)
                {
                    hashtable.Add("title", table2.Rows[0]["title"]);
                }
                else
                {
                    hashtable.Add("title", "");
                }
                if (table2.Rows[0]["operate"] != null)
                {
                    string str7 = table2.Rows[0]["operate"].ToString().Replace("\n", Environment.NewLine);
                    hashtable.Add("operate", str7);
                }
                else
                {
                    hashtable.Add("operate", "");
                }
                if (table2.Rows[0]["solution"] != null)
                {
                    string str4 = table2.Rows[0]["solution"].ToString().Replace("\n", Environment.NewLine);
                    hashtable.Add("solution", str4);
                }
                else
                {
                    hashtable.Add("solution", "");
                }
                hashtable.Add("version", table2.Rows[0]["version"]);
                hashtable.Add("MsgBoxButtons", table2.Rows[0]["buttons"]);
                hashtable.Add("MsgBoxIcon", table2.Rows[0]["icon"]);
                hashtable.Add("MsgBoxDefaultButton", table2.Rows[0]["defaultbutton"]);
                hashtable.Add("funcode", str2);
            }
            catch (Exception)
            {
                hashtable = null;
            }
            return hashtable;
        }

        private static string smethod_3()
        {
            try
            {
                return Convert.ToInt32(TaxCardFactory.CreateTaxCard().ECardType).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public string MessageBody
        {
            get
            {
                return this.string_2;
            }
        }

        public string MessageID
        {
            get
            {
                return this.string_1;
            }
        }

        public string MessageType
        {
            get
            {
                return this.string_3;
            }
        }

        public string Operate
        {
            get
            {
                return this.string_5;
            }
        }

        public string Solution
        {
            get
            {
                return this.string_6;
            }
        }
    }
}

