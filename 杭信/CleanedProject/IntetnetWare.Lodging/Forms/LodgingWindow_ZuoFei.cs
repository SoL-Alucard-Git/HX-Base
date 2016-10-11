using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace InternetWare.Form
{
    public partial class LodgingWindow
    {

        private void ZuoFei_btnSearch_Click(object sender, EventArgs e)
        {
            ZuoFei_gridView.DataSource = (DataService.DoService(GetZuoFeiChaXunArgs()) as ResultBase).Data as DataTable;
        }

        private ZuoFeiChaXunArgs GetZuoFeiChaXunArgs()
        {
            ZuoFeiChaXunArgs args = new ZuoFeiChaXunArgs();
            args.MathStr = ZuoFei_txtMatch.Text;
            args.YanQianShiBaiChecked = ZuoFei_checkYanQianShiBai.Checked;
            return args;
        }

        private void ZuoFei_btnDoService_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in ZuoFei_gridView.SelectedRows)
            {
                list.Add(row);
            }
            CountableResult res = DataService.DoService(new ZuoFeiArgs() { list = list }) as CountableResult;
            MessageBox.Show($"作废{res.Total}条，成功{res.Succeed}条，失败{res.Failed}条。错误信息:{res.ErrorInfo.ErrorDescription}");
        }
    }
}
