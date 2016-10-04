namespace Update.Tool
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;

    public class MailTools
    {
        private MailMessage mailMessage_0;
        private SmtpClient smtpClient_0;
        private string string_0;

        public MailTools(string strDest, string strSrc, string strPassword, string strTitle, string strBody)
        {
           
            this.mailMessage_0 = new MailMessage();
            string[] strArray = strDest.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                this.mailMessage_0.To.Add(strArray[i]);
            }
            this.mailMessage_0.From = new MailAddress(strSrc);
            this.mailMessage_0.Subject = strTitle;
            this.mailMessage_0.Body = strBody;
            this.mailMessage_0.IsBodyHtml = true;
            this.mailMessage_0.BodyEncoding = Encoding.UTF8;
            this.mailMessage_0.Priority = MailPriority.Normal;
            this.string_0 = strPassword;
        }

        public void Attachments(string strPath)
        {
            string[] strArray = strPath.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                Attachment item = new Attachment(strArray[i], "application/octet-stream");
                ContentDisposition contentDisposition = item.ContentDisposition;
                contentDisposition.CreationDate = System.IO.File.GetCreationTime(strArray[i]);
                contentDisposition.ModificationDate = System.IO.File.GetLastWriteTime(strArray[i]);
                contentDisposition.ReadDate = System.IO.File.GetLastAccessTime(strArray[i]);
                this.mailMessage_0.Attachments.Add(item);
            }
        }

        public void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (this.mailMessage_0 != null)
            {
                try
                {
                    this.smtpClient_0 = new SmtpClient();
                    this.smtpClient_0.Credentials = new NetworkCredential(this.mailMessage_0.From.Address, this.string_0);
                    this.smtpClient_0.DeliveryMethod = SmtpDeliveryMethod.Network;
                    this.smtpClient_0.Host = "smtp." + this.mailMessage_0.From.Host;
                    this.smtpClient_0.SendCompleted += new SendCompletedEventHandler(CompletedMethod.Invoke);
                    this.smtpClient_0.SendAsync(this.mailMessage_0, this.mailMessage_0.Body);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public void SendEmail()
        {
            if (this.mailMessage_0 != null)
            {
                try
                {
                    this.smtpClient_0 = new SmtpClient();
                    this.smtpClient_0.Credentials = new NetworkCredential(this.mailMessage_0.From.Address, this.string_0);
                    this.smtpClient_0.DeliveryMethod = SmtpDeliveryMethod.Network;
                    this.smtpClient_0.Host = "smtp." + this.mailMessage_0.From.Host;
                    this.smtpClient_0.Send(this.mailMessage_0);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }
    }
}

