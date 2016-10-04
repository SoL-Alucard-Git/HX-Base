namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Windows.Forms;

    public class TaxcardDataMonthlyStatisticEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<TaxcardDataMonthlyStatisticEntry>();
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();

        protected override void RunCommand()
        {
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
                    new InvStatForm(segment, true, form.m_invType) { Text = form.strDlgTitle, m_strTitle = form.strLabelTitle, ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen }.ShowDialog();
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("INP-253107", new string[] { exception.Message });
                }
            }
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

