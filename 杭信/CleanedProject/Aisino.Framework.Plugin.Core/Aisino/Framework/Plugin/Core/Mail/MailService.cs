namespace Aisino.Framework.Plugin.Core.Mail
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MailService
    {
        private static byte[] byte_0;
        private static byte[] byte_1;
        private byte[] byte_2;
        private int int_0;
        private NetworkStream networkStream_0;
        private StreamReader streamReader_0;
        private string string_0;
        private string string_1;
        private static string string_2;
        private TcpClient tcpClient_0;

        static MailService()
        {
            
            byte_0 = new byte[] { 
                250, 20, 0x5d, 0x3a, 0x1c, 0x23, 150, 0x12, 0xff, 0xab, 0xef, 0x2d, 0x1a, 140, 0xdd, 0xab, 
                0xcd, 120, 0x98, 0x34, 0x68, 0x38, 0x69, 0x30, 0xf1, 210, 0xa3, 0xc4, 70, 0x24, 0x3e, 0x99
             };
            byte_1 = new byte[] { 0xff, 0xdf, 0xfe, 0xad, 0xca, 0x3a, 0x4f, 0x1d, 0x2e, 0x5f, 0x65, 0x57, 0x62, 0xad, 0xff, 0xf8 };
            string_2 = "【AISINO】";
        }

        public MailService(string string_3, int int_1)
        {
            
            this.string_0 = string_3;
            this.int_0 = int_1;
        }

        public void Close()
        {
            this.string_1 = "QUIT\r\n";
            this.method_0(this.string_1);
            this.streamReader_0.ReadLine();
            this.networkStream_0.Close();
            this.streamReader_0.Close();
            this.tcpClient_0.Close();
            this.networkStream_0 = null;
            this.streamReader_0 = null;
            this.tcpClient_0 = null;
        }

        public bool Connect(string string_3, string string_4, out string string_5)
        {
            try
            {
                bool flag2;
                this.tcpClient_0 = new TcpClient(this.string_0, this.int_0);
                this.networkStream_0 = this.tcpClient_0.GetStream();
                this.streamReader_0 = new StreamReader(this.tcpClient_0.GetStream(), ToolUtil.GetEncoding());
                string_5 = this.streamReader_0.ReadLine();
                if (string_3 == null)
                {
                    return string_5.StartsWith("+OK");
                }
                this.string_1 = "USER " + ((string_3 == null) ? "" : string_3) + "\r\n";
                this.method_0(this.string_1);
                string_5 = this.streamReader_0.ReadLine();
                this.string_1 = "PASS " + string_4 + "\r\n";
                this.method_0(this.string_1);
                string_5 = this.streamReader_0.ReadLine();
                if (!(flag2 = string_5.StartsWith("+OK")))
                {
                    this.networkStream_0.Close();
                    this.streamReader_0.Close();
                    this.networkStream_0 = null;
                    this.streamReader_0 = null;
                }
                return flag2;
            }
            catch (Exception exception)
            {
                string_5 = exception.Message;
                return false;
            }
        }

        private bool method_0(string string_3)
        {
            try
            {
                this.byte_2 = Encoding.ASCII.GetBytes(string_3.ToCharArray());
                this.networkStream_0.Write(this.byte_2, 0, this.byte_2.Length);
                return true;
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show("发送邮件请求异常：" + exception.Message, "错误");
                this.networkStream_0.Close();
                this.streamReader_0.Close();
                this.tcpClient_0.Close();
                return false;
            }
        }

        public List<MailData> ReceiveMail(bool bool_0, out string string_3)
        {
            try
            {
                string str = string.Empty;
                if (this.networkStream_0 == null)
                {
                    string_3 = "没有连接到POP3服务器";
                    return null;
                }
                List<MailData> list = new List<MailData>();
                this.string_1 = "LIST " + "\r\n";
                this.method_0(this.string_1);
                string[] strArray = this.streamReader_0.ReadLine().Split(new char[] { ' ' });
                int result = 0;
                int.TryParse(strArray[1], out result);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                do
                {
                    strArray = this.streamReader_0.ReadLine().Split(new char[] { ' ' });
                    dictionary.Add(strArray[0], strArray[1]);
                }
                while (str != ".");
                int num2 = 1;
            Label_00B7:
                if (num2 > result)
                {
                    goto Label_038C;
                }
                this.string_1 = "RETR " + num2 + "\r\n";
                this.method_0(this.string_1);
                str = this.streamReader_0.ReadLine();
                if (str[0] == '-')
                {
                    goto Label_037B;
                }
                int num3 = 0;
                MailData item = new MailData {
                    int_0 = num2
                };
                while (str != ".")
                {
                    if (str.StartsWith("Message-ID:"))
                    {
                        item.string_5 = str.Substring(11);
                        num3 = 0;
                        goto Label_0309;
                    }
                    if (str.StartsWith("From:"))
                    {
                        item.string_0 = str.Substring(5);
                        num3 = 1;
                        goto Label_0309;
                    }
                    if (str.StartsWith("To:"))
                    {
                        item.string_1 = str.Substring(3);
                        num3 = 0;
                        goto Label_0309;
                    }
                    if (str.StartsWith("Date:"))
                    {
                        item.string_4 = str.Substring(5);
                        num3 = 0;
                        goto Label_0309;
                    }
                    if (!str.StartsWith("Subject:"))
                    {
                        goto Label_0276;
                    }
                    try
                    {
                        item.string_3 = str.Substring(str.IndexOf("=?utf-8?B?") + 10);
                        item.string_3 = item.string_3.Substring(0, item.string_3.Length - 2);
                        item.string_3 = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(item.string_3));
                        if (item.string_3.StartsWith(string_2))
                        {
                            goto Label_026E;
                        }
                        item = null;
                        while (this.streamReader_0.ReadLine() != ".")
                        {
                        }
                    }
                    catch (Exception)
                    {
                        item = null;
                        while (this.streamReader_0.ReadLine() != ".")
                        {
                        }
                    }
                    break;
                Label_026E:
                    num3 = 0;
                    goto Label_0309;
                Label_0276:
                    if (str.Equals(""))
                    {
                        switch (num3)
                        {
                            case 0:
                                num3 = 2;
                                break;

                            case 2:
                                num3 = 0;
                                break;
                        }
                    }
                    else
                    {
                        switch (num3)
                        {
                            case 1:
                                item.string_0 = item.string_0 + str;
                                break;

                            case 2:
                                try
                                {
                                    item.string_6 = item.string_6 + (str.EndsWith("=") ? str.Substring(0, str.Length - 1) : str);
                                }
                                catch (Exception)
                                {
                                    item = null;
                                    while (this.streamReader_0.ReadLine() != ".")
                                    {
                                    }
                                    goto Label_0386;
                                }
                                break;
                        }
                    }
                Label_0309:
                    str = this.streamReader_0.ReadLine();
                }
                goto Label_0386;
            Label_0327:
                try
                {
                    item.string_6 = ToolUtil.GetString(Convert.FromBase64String(item.string_6.Replace("=3D", "=")));
                    int.TryParse(dictionary[item.int_0.ToString()], out item.int_1);
                    list.Add(item);
                }
                catch (Exception)
                {
                }
            Label_037B:
                num2++;
                goto Label_00B7;
            Label_0386:
                if (item == null)
                {
                    goto Label_037B;
                }
                goto Label_0327;
            Label_038C:
                if (bool_0)
                {
                    foreach (MailData data2 in list)
                    {
                        this.string_1 = "DELE " + data2.int_0 + "\r\n";
                        this.method_0(this.string_1);
                    }
                }
                string_3 = "+OK";
                return list;
            }
            catch (Exception exception)
            {
                string_3 = exception.ToString();
                return null;
            }
        }

        public static bool SendMail(string string_3, string string_4, string[] string_5, string string_6, string string_7, string string_8, string string_9, string string_10, int int_1, string[] string_11, out string string_12)
        {
            try
            {
                MailMessage message = new MailMessage {
                    From = new MailAddress(string_3, string_4)
                };
                foreach (string str2 in string_5)
                {
                    message.To.Add(str2);
                }
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.Subject = string_2 + string_6;
                message.BodyEncoding = ToolUtil.GetEncoding();
                message.Priority = MailPriority.High;
                message.Body = Convert.ToBase64String(AES_Crypt.Encrypt(ToolUtil.GetBytes(string_7), byte_0, byte_1));
                message.IsBodyHtml = false;
                if ((string_11 != null) && (string_11.Length > 0))
                {
                    foreach (string str in string_11)
                    {
                        message.Attachments.Add(new Attachment(str));
                    }
                }
                SmtpClient client = new SmtpClient(string_10, int_1) {
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                if (string_8 != null)
                {
                    client.Credentials = new NetworkCredential(string_8, string_9);
                }
                client.Timeout = 0x2710;
                client.Send(message);
                client.Dispose();
                string_12 = "OK";
                return true;
            }
            catch (Exception exception)
            {
                string_12 = exception.Message;
                return false;
            }
        }
    }
}

