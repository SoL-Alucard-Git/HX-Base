namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Windows.Forms;

    public class TaxcardDataStatisticEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<TaxcardDataStatisticEntry>();
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();

        protected override void RunCommand()
        {
            DataSumForm form = new DataSumForm(false) {
                StartPosition = FormStartPosition.CenterScreen,
                ShowInTaskbar = false,
                strLabelYearOrStartMonth = "起始月份",
                strLabelEndMonth = "结束月份",
                bLabelTaxPeriod = false,
                bComboxTaxPeriod = false
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
                    new InvStatForm(segment, false, form.m_invType) { Text = form.strDlgTitle, m_strTitle = form.strLabelTitle, ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen }.ShowDialog();
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

