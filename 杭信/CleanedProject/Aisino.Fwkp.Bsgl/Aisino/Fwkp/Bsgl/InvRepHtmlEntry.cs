namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Windows.Forms;

    public class InvRepHtmlEntry
    {
        public static DockForm RunCommand(byte[] value)
        {
            string str = "Aisino.Fwkp.Invoice.ActiveX.TaxCardQuery_Status";
            byte[] bytes = ToolUtil.GetBytes(MD5_Crypt.GetHashStr(str));
            byte[] destinationArray = new byte[0x20];
            Array.Copy(bytes, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
            DateTime time = DateTime.Parse(ToolUtil.GetString(AES_Crypt.Decrypt(value, destinationArray, buffer3, null)));
            DateTime now = DateTime.Now;
            if ((time.CompareTo(now) > 0) || (time.CompareTo(now.AddSeconds(-1.0)) < 0))
            {
                return null;
            }
            WaitForm form = new WaitForm {
                StartPosition = FormStartPosition.CenterScreen,
                ShowInTaskbar = false
            };
            form.Show();
            bool flag = form.DataCheck();
            form.Close();
            if (!flag)
            {
                return null;
            }
            if (form.bIsNeedRepair)
            {
                MessageManager.ShowMsgBox("INP-253207", new string[] { form.strRet });
            }
            return new InvoiceReportForm();
        }
    }
}

