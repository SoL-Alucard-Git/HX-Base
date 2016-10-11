using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace InternetWare.Form
{
    public partial class LodgingWindow
    {
        #region Init

        private void InitWeiKaiPage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("<空>", null);
            dict.Add("专用发票", nameof(FaPiaoTypes.Special));
            dict.Add("普通发票", nameof(FaPiaoTypes.Common));
            BindDictionaryToComboBox(dict, WeiKai_cmbFpType);
        }

        #endregion


        private void WeiKai_cmbFpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(WeiKai_cmbFpType.SelectedValue == null)
            {
                WeiKai_txt发票代码.Text = string.Empty;
                WeiKai_txt发票份数.Text = string.Empty;
                WeiKai_txt发票起始号码.Text = string.Empty;
                WeiKai_txt要作废发票份数.Text = string.Empty;
            }
            else
            {
                WeiKaiChaXunResult res = (DataService.DoService(new WeiKaiChaXunArgs() { FpType = (FaPiaoTypes)Enum.Parse(typeof(FaPiaoTypes), WeiKai_cmbFpType.SelectedValue.ToString()) })) as WeiKaiChaXunResult;
                WeiKai_txt发票代码.Text = res.Fpdm;
                WeiKai_txt发票份数.Text = res.FpHasNum.ToString();
                WeiKai_txt发票起始号码.Text = res.InvNum;
            }
        }

        private void WeiKai_btn作废未开_Click(object sender, EventArgs e)
        {
            WeiKaiArgs args = new WeiKaiArgs() { Count = int.Parse(WeiKai_txt要作废发票份数.Text), FpType = (FaPiaoTypes)Enum.Parse(typeof(FaPiaoTypes), WeiKai_cmbFpType.SelectedValue.ToString()) };
            CountableResult res = DataService.DoService(args) as CountableResult;
            MessageBox.Show($"作废{res.Total}条，成功{res.Succeed}条，失败{res.Failed}条。错误信息:{res.ErrorInfo.ErrorDescription}");
        }
    }
}
