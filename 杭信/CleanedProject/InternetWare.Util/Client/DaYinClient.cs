using Aisino.Fwkp.Print;
using System.Drawing.Printing;
using InternetWare.Lodging.Data;
using System;

namespace InternetWare.Util.Client
{
    internal class DaYinClient : BaseClient
    {
        private DaYinArgs _args;
        public DaYinClient(DaYinArgs args)
        {
            _args = args;
        }
        internal override BaseResult DoService()
        {
            IPrint print = FPPrint.Create(_args.FPZL, _args.FPDM, _args.FPHM, true);
            print.Print();
            PrinterEventArgs printeventargs = new PrinterEventArgs() { Left = -10, Top = 0, IsQuanDa = true, PageLenght = 0, PrinterName = _args.Printer };
            print.printSetUp_0.printer_0.SaveUserPrinterEdge(printeventargs);

            PrintSetUp.pageSetupDialog.Document.PrinterSettings = new PrinterSettings();
            PrintSetUp.pageSetupDialog.Document.PrinterSettings.PrinterName = _args.Printer;
            print.printSetUp_0.CurrentPrinterName = _args.Printer;
            PrintSetUp.pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = print.printSetUp_0.paperSize_0;
            //byte[] imgbyte = print.DaYinMethod(print.printSetUp_0, new PrintSetEventArgs(), false);
            ;
            byte[] imgbyte1 = print.PreviewMethod();
            return new DaYinResult(_args,Convert.ToBase64String(imgbyte1));
        }
    }
}
