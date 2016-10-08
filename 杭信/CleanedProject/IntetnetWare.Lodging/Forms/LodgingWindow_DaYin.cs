using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace InternetWare.Form
{
    public partial class LodgingWindow
    {
        #region Init
        private void Init_DaYinPage()
        {
            Init_DaYin_CmbPrinter();
        }

        private void Init_DaYin_CmbPrinter()
        {
            Dictionary<string, object> printers = new Dictionary<string, object>();
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                printers.Add(str, str);
            }
            BindDictionaryToComboBox(printers, DaYin_CmbPrinter);
        }


        #endregion
        private void DaYin_btnDoPrint_Click(object sender, EventArgs e)
        {
            DaYinArgs args = new DaYinArgs() { FPZL = "s", FPHM =Convert.ToInt32(DaYin_tbFphm.Text), FPDM = DaYin_tbFpdm.Text, Printer = DaYin_CmbPrinter.SelectedValue.ToString() };
            DataService.DoService(args);
        }
    }
}
