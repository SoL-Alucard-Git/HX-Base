using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aisino.Fwkp.Print;
using InternetWare.Lodging.Args;
using System.Drawing.Printing;

namespace InternetWare.Util.Client
{
    internal class DaYinClient: BaseClient
    {
        private DaYinArgs _args;
        public DaYinClient(DaYinArgs args)
        {
            _args = args;
        }
        internal override object DoService()
        {
            IPrint print = FPPrint.Create(_args.FPZL,_args.FPDM,_args.FPHM, true);
            print.Print();
            PrinterEventArgs printeventargs = new PrinterEventArgs() { Left = -10, Top = 0, IsQuanDa = true, PageLenght = 0, PrinterName = _args.Printer };
            print.printSetUp_0.printer_0.SaveUserPrinterEdge(printeventargs);

            PrintSetUp.pageSetupDialog.Document.PrinterSettings = new PrinterSettings();
            PrintSetUp.pageSetupDialog.Document.PrinterSettings.PrinterName = _args.Printer;
            print.printSetUp_0.CurrentPrinterName = _args.Printer;
            PrintSetUp.pageSetupDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = print.printSetUp_0.paperSize_0;
            print.method_5(print.printSetUp_0,new PrintSetEventArgs());
            return null;
        }
    }
}
