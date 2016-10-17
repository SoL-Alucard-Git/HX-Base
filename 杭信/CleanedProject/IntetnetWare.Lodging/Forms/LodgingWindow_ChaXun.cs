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
        #region Init
        private void Init_ChaXunPage()
        {
            Init_ChaXun_CmbFpType();
            Init_ChaXun_CmbYear();
            Init_ChaXun_CmbMonth();
        }

        private void Init_ChaXun_CmbFpType()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("全部发票", string.Empty);
            dict.Add("专用发票", "s");
            dict.Add("普通发票", "c");
            BindDictionaryToComboBox(dict, ChaXun_CmbFpType);
        }

        private void Init_ChaXun_CmbYear()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            for (int i = 2016; i > 2010; i--)
            {
                dict.Add(i.ToString(), i);
            }
            BindDictionaryToComboBox(dict, ChaXun_CmbYear);
        }

        private void Init_ChaXun_CmbMonth()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("全年", 0);
            for (int i = 1; i <= 12; i++)
            {
                dict.Add(i.ToString(), i);
            }
            BindDictionaryToComboBox(dict, ChaXun_CmbMonth);
        }

        private void BindDictionaryToComboBox(Dictionary<string, object> dict, ComboBox cmbBox)
        {
            string TextPath = "Text";
            string ValuePath = "Value";
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(TextPath));
            dt.Columns.Add(new DataColumn(ValuePath));
            foreach (KeyValuePair<string, object> pair in dict)
            {
                dt.Rows.Add(pair.Key, pair.Value);
            }
            cmbBox.DataSource = dt;
            cmbBox.DisplayMember = TextPath;
            cmbBox.ValueMember = ValuePath;
            cmbBox.SelectedIndex = 0;
        }
        #endregion

        private void ChaXun_BtnGo_Click(object sender, EventArgs e)
        {
            ChaXunResult res = JsonConvert.DeserializeObject<ChaXunResult>(Base64Encode(DataService.DoService(GetChaXunArgs())));
            ChaXun_DataGrid.DataSource = res.DataTable;
        }

        private ChaXunArgs GetChaXunArgs()
        {
            return new ChaXunArgs()
            {
                Year = int.Parse(ChaXun_CmbYear.SelectedValue.ToString()),
                Month = ChaXun_CmbMonth.Text == "全年"
                ? 0
                : int.Parse(ChaXun_CmbMonth.SelectedValue.ToString()),
                MathStr = ChaXun_txtMathStr.Text,
                FPZL = ChaXun_CmbFpType.SelectedValue.ToString(),
                WeiBaoSongChecked = ChaXun_checkWeiBaoSong.Checked,
                YanQianShiBaiChecked = ChaXun_checkYanQianShiBai.Checked
            };
        }

        private void ChaXun_btnPrint_Click(object sender, EventArgs e)
        {
            if (ChaXun_DataGrid.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择并且只能选择一行数据进行打印");
            }
            else
            {
                DataGridViewRow row = ChaXun_DataGrid.SelectedRows[0];
                DataRow dataRow = (row.DataBoundItem as DataRowView).Row;
                DaYin_tbFpdm.Text = dataRow["fpdm"].ToString();
                DaYin_tbFphm.Text = dataRow["fphm"].ToString();
                tabControl.SelectedTab = DaYinPage;
            }
        }
    }
}
