namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Windows.Forms;

    public class MonthStatEntry
    {
        private static TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();

        public static BaseForm RunCommand(byte[] value)
        {
            string str = "Aisino.Fwkp.Invoice.ActiveX.TaxCardQuery_Month";
            byte[] bytes = ToolUtil.GetBytes(MD5_Crypt.GetHashStr(str));
            byte[] destinationArray = new byte[0x20];
            Array.Copy(bytes, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
            DateTime time = DateTime.Parse(ToolUtil.GetString(AES_Crypt.Decrypt(value, destinationArray, buffer3, null)));
            DateTime now = DateTime.Now;
            if ((time.CompareTo(now) <= 0) && (time.CompareTo(now.AddSeconds(-1.0)) >= 0))
            {
                if (taxcardEntityBLL.IsLocked())
                {
                    MessageManager.ShowMsgBox("INP-253108");
                    return null;
                }
                DataSumForm form = new DataSumForm(true) {
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowInTaskbar = false,
                    strRichTextBoxRemind = "【注意事项】\n\t1、本功能用于查询金税设备黑匣子中纳税统计资料，包括：防伪发票领用存统计及发票销售金额与税额的统计。\n\t2、汇总范围可选择金税设备使用以来的任意年月。",
                    strLabelYearOrStartMonth = "选择年份",
                    strLabelEndMonth = "选择月份"
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    TaxDateSegment segment = new TaxDateSegment();
                    try
                    {
                        segment.m_nYear = form.nTaxYear;
                        segment.m_nStartMonth = form.nStartMonth;
                        segment.m_nEndMonth = form.nEndMonth;
                        segment.m_nTaxPeriod = Convert.ToInt32(form.strTaxPeriod);
                        return new InvStatForm(segment, true) { Text = form.strDlgTitle, m_strTitle = form.strLabelTitle };
                    }
                    catch (Exception exception)
                    {
                        MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
                        return null;
                    }
                }
            }
            return null;
        }
    }
}

