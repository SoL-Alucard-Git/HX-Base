using InternetWare.Lodging.Data;
using Newtonsoft.Json;
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
            ZuoFei_gridView.DataSource = JsonConvert.DeserializeObject<ZFCXResult>(Base64Encode(DataService.DoService(GetZuoFeiChaXunArgs()))).DataTable as DataTable;
        }

        private ZuoFeiChaXunArgs GetZuoFeiChaXunArgs()
        {
            return new ZuoFeiChaXunArgs()
            {
                MathStr = ZuoFei_txtMatch.Text,
                YanQianShiBaiChecked = ZuoFei_checkYanQianShiBai.Checked
            };
        }

        private void ZuoFei_btnDoService_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in ZuoFei_gridView.SelectedRows)
            {
                list.Add(row);
            }
            string rtnStr = Base64Encode(DataService.DoService(new ZuoFeiArgs() { DataRowList = list }));
            CountableResult res = JsonConvert.DeserializeObject<CountableResult>(rtnStr);
            MessageBox.Show($"作废{res.Total}条，成功{res.Succeed}条，失败{res.Failed}条。错误信息:{res.ErrorInfo.ErrorDescription}");
        }
    }
}
