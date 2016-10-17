using InternetWare.Lodging.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

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
            DaYinArgs args = new DaYinArgs() { FPZL = "c", FPHM = Convert.ToInt32(DaYin_tbFphm.Text), FPDM = DaYin_tbFpdm.Text, Printer = DaYin_CmbPrinter.SelectedValue.ToString() };
            DaYinResult rb = JsonConvert.DeserializeObject<DaYinResult>(Base64Encode(DataService.DoService(args)));
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(rb.ImgBase64Str));
            Image bm = Image.FromStream(ms);
            //bm.Save($"C:/因特睿/因特睿解决方案/img{DateTime.Now.Ticks}.bmp",bm.RawFormat);
            DaYin_picBox.Image = bm;
        }
    }
}
