namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class IPrint
    {
        protected object[] _args;
        protected bool _IsHZFW;
        protected string _isPrint;
        protected bool _isZYPT;
        public AisinoPrint aisinoPrint;
        private bool bool_0;
        protected Fpxx dyfp;
        protected bool IsFirstCreate;
        protected int IsHindErrorBox;
        protected bool IsShowTaoDaGruoupButton;
        public static bool IsZjFlag;
        protected ILog loger;
        public PrintSetUp printSetUp_0;
        private TaxCard taxCard_0;
        protected string ZYFPLX;

        static IPrint()
        {

        }

        protected IPrint()
        {

            this.taxCard_0 = TaxCard.CreateInstance(CTaxCardType.tctAddedTax);
            this.ZYFPLX = "";
            this.loger = LogUtil.GetLogger<IPrint>();
            this._isPrint = "0002";
            this.aisinoPrint = new AisinoPrint();
        }

        protected IPrint(string string_0)
        {

            this.taxCard_0 = TaxCard.CreateInstance(CTaxCardType.tctAddedTax);
            this.ZYFPLX = "";
            this.loger = LogUtil.GetLogger<IPrint>();
            this._isPrint = "0002";
            this.aisinoPrint = new AisinoPrint();
            this.Id = string_0;
        }

        protected virtual Canvas CanvasCreate(string string_0)
        {
            if (File.Exists(string_0))
            {
                Canvas canvas = new Canvas(string_0);
                if (canvas == null)
                {
                    this._isPrint = "0001";
                }
                this._isPrint = "0002";
                return canvas;
            }
            this._isPrint = "0001";
            return null;
        }

        protected abstract DataDict DictCreate(params object[] args);
        internal void method_0(params object[] args)
        {
            this._args = args;
            this.loger.Debug("[发票打印]开始Load参数.....");
            PrintSetUp.pageSetupDialog = this.aisinoPrint.pageSetupDialog;
            if (PrintSetUp.pageSetupDialog == null)
            {
                this.loger.Debug("[发票打印]PrintSetUp.pageSetupDialog is null");
            }
            if (this.printSetUp_0 == null)
            {
                this.printSetUp_0 = new PrintSetUp(this._args);
            }
        }

        private void method_1()
        {
            if (this._isPrint == "0002")
            {
                try
                {
                    if ((this._args != null) && (this._args.Length > 3))
                    {
                        string str = this._args[1].ToString() + " " + Aisino.Fwkp.Print.Common.smethod_1(this._args[2].ToString());
                        this.aisinoPrint.printDocument.DocumentName = str;
                    }
                    this.aisinoPrint.Preview(this.ZYFPLX, this._isZYPT);
                    this._isPrint = "0002";
                }
                catch (Exception exception)
                {
                    this._isPrint = "0004";
                    this.loger.Error("[OnPreview函数异常]" + exception.ToString());
                }
            }
        }

        private void method_2()
        {
            if (this._isPrint == "0002")
            {
                try
                {
                    string str;
                    if ((this._args != null) && (this._args.Length > 3))
                    {
                        string str2 = this._args[1].ToString() + " " + Aisino.Fwkp.Print.Common.smethod_1(this._args[2].ToString());
                        this.aisinoPrint.printDocument.DocumentName = str2;
                    }
                    this.aisinoPrint.Print(this._isZYPT);
                    if ((((this._args != null) && (this._args.Length >= 4)) && ((this._args[3].ToString() == "_FP") && ((str = this._args[0].ToString()) != null))) && (((str == "s") || (str == "c")) || (((str == "f") || (str == "j")) || (str == "q"))))
                    {
                        ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPDYShareMethod", this._args);
                        if (this.taxCard_0.SubSoftVersion == "Linux")
                        {
                            FPLX fplx = Aisino.Fwkp.Print.Common.DBFpzlToCardType(this._args[0].ToString());
                            this.taxCard_0.UpdateDybzToDB((int)fplx, this._args[1].ToString(), this._args[2].ToString());
                        }
                    }
                    this._isPrint = "0000";
                }
                catch (Exception exception)
                {
                    this._isPrint = "0004";
                    this.loger.Error("[OnPrint函数异常]" + exception.ToString());
                }
            }
        }
        #region 预览方法
        public byte[] PreviewMethod()
        {
            byte[] imgbyte = null;
            PrintSetEventArgs e = new PrintSetEventArgs
            {
                IsTaoDa = false,
                Offset = (PointF)new Point(0, 0)
            };
            this.printSetUp_0.TopMost = false;
            try
            {
                if (this._isPrint == "0000")
                {
                    this._isPrint = "0002";
                }
                this.printSetUp_0.TopMost = false;
                this.IsTaoDa = e.IsTaoDa;
                string[] strArray2 = Aisino.Fwkp.Print.Common.CheckInstalledFont();
                if ((strArray2 != null) && (strArray2[0].Length > 0))
                {
                    if (this.IsHindErrorBox == 0)
                    {
                        MessageManager.ShowMsgBox("FPDY-000003", "错误", new string[] { strArray2[0] });
                    }
                    string message = string.Format("当前操作系统缺少{0}等字体，请及时安装所需字体！", strArray2[0]);
                    this.loger.Error(message);
                    this.loger.Error(strArray2[1]);
                }
                else
                {
                    this.aisinoPrint.Data = this.DictCreate(this._args);
                    if (this.aisinoPrint.Data == null)
                    {
                        if (this.IsHindErrorBox == 0)
                        {
                            MessageManager.ShowMsgBox("FPDY-000004");
                        }
                        this.loger.Error("发票打印的对象为空");
                        this._isPrint = "0004";
                    }
                    else
                    {
                        this.aisinoPrint.Canvas.startPoint = new PointF(this.MillimeterToPx(e.Offset.X), this.MillimeterToPx(e.Offset.Y));
                        if ((this._IsHZFW && (this.printSetUp_0 != null)) && !this.printSetUp_0.IsSupportHZFW)
                        {
                            if (this.IsHindErrorBox == 0)
                            {
                                MessageManager.ShowMsgBox("FPDY-000001", "提示", new string[] { this.printSetUp_0.CurrentPrinterName });
                            }
                            this.loger.Error("当前打印机型号不支持汉字防伪发票打印");
                            this._isPrint = "0004";
                        }
                        else
                        {
                            if (this._isPrint == "0002")
                            {
                                try
                                {
                                    if ((this._args != null) && (this._args.Length > 3))
                                    {
                                        string str = this._args[1].ToString() + " " + Aisino.Fwkp.Print.Common.smethod_1(this._args[2].ToString());
                                        this.aisinoPrint.printDocument.DocumentName = str;
                                    }
                                    imgbyte= this.aisinoPrint.PreviewImgMethod(this.ZYFPLX, this._isZYPT);
                                    this._isPrint = "0002";
                                }
                                catch (Exception exception)
                                {
                                    this._isPrint = "0004";
                                    this.loger.Error("[OnPreview函数异常]" + exception.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
                this.loger.Error("[psu_OnPreview]:" + exception.ToString());
                this._isPrint = "0004";
                if (this.IsHindErrorBox == 0)
                {
                    MessageManager.ShowMsgBox("FPDY-000002");
                }
            }
            return imgbyte;
        }
        #endregion

        private void method_3(object sender, PrintSetEventArgs e)
        {
            try
            {
                if (this._isPrint == "0000")
                {
                    this._isPrint = "0002";
                }
                this.printSetUp_0.TopMost = false;
                this.IsTaoDa = e.IsTaoDa;
                string[] strArray2 = Aisino.Fwkp.Print.Common.CheckInstalledFont();
                if ((strArray2 != null) && (strArray2[0].Length > 0))
                {
                    if (this.IsHindErrorBox == 0)
                    {
                        MessageManager.ShowMsgBox("FPDY-000003", "错误", new string[] { strArray2[0] });
                    }
                    string message = string.Format("当前操作系统缺少{0}等字体，请及时安装所需字体！", strArray2[0]);
                    this.loger.Error(message);
                    this.loger.Error(strArray2[1]);
                }
                else
                {
                    this.aisinoPrint.Data = this.DictCreate(this._args);
                    if (this.aisinoPrint.Data == null)
                    {
                        if (this.IsHindErrorBox == 0)
                        {
                            MessageManager.ShowMsgBox("FPDY-000004");
                        }
                        this.loger.Error("发票打印的对象为空");
                        this._isPrint = "0004";
                    }
                    else
                    {
                        this.aisinoPrint.Canvas.startPoint = new PointF(this.MillimeterToPx(e.Offset.X), this.MillimeterToPx(e.Offset.Y));
                        if ((this._IsHZFW && (this.printSetUp_0 != null)) && !this.printSetUp_0.IsSupportHZFW)
                        {
                            if (this.IsHindErrorBox == 0)
                            {
                                MessageManager.ShowMsgBox("FPDY-000001", "提示", new string[] { this.printSetUp_0.CurrentPrinterName });
                            }
                            this.loger.Error("当前打印机型号不支持汉字防伪发票打印");
                            this._isPrint = "0004";
                        }
                        else
                        {
                            this.method_1();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[psu_OnPreview]:" + exception.ToString());
                this._isPrint = "0004";
                if (this.IsHindErrorBox == 0)
                {
                    MessageManager.ShowMsgBox("FPDY-000002");
                }
            }
        }

        private void method_4(object sender, PrintSetEventArgs e)
        {
            this._isPrint = "0005";
        }

        public void method_5(object sender, PrintSetEventArgs e)
        {
            try
            {
                if (this._isPrint == "0000")
                {
                    this._isPrint = "0002";
                }
                string[] strArray = Aisino.Fwkp.Print.Common.CheckInstalledFont();
                if ((strArray != null) && (strArray[0].Length > 0))
                {
                    if (this.IsHindErrorBox == 0)
                    {
                        MessageManager.ShowMsgBox("FPDY-000003", "错误", new string[] { strArray[0] });
                    }
                    string message = string.Format("当前操作系统缺少{0}等字体，请及时安装所需字体！", strArray[0]);
                    this.loger.Error(message);
                    this.loger.Error(strArray[1]);
                }
                else
                {
                    Printer printer = new Printer(this._args);
                    printer.GetPrinterArgs(IsZjFlag);
                    this.IsTaoDa = printer.RealPrinterArgs.IsQuanDa;
                    this.aisinoPrint.Data = this.DictCreate(this._args);
                    if (this.aisinoPrint.Data == null)
                    {
                        if (this.IsHindErrorBox == 0)
                        {
                            MessageManager.ShowMsgBox("FPDY-000004");
                        }
                        this.loger.Error("发票打印的对象为空");
                        this._isPrint = "0004";
                    }
                    else
                    {
                        this.loger.Error(string.Concat(new object[] { "[连续打印]Left:", printer.RealPrinterArgs.Left, "[Top]:", printer.RealPrinterArgs.Top }));
                        this.aisinoPrint.Canvas.startPoint = new PointF(this.MillimeterToPx(printer.RealPrinterArgs.Left), this.MillimeterToPx(printer.RealPrinterArgs.Top));
                        if ((this._IsHZFW && (this.printSetUp_0 != null)) && !this.printSetUp_0.IsSupportHZFW)
                        {
                            if (this.IsHindErrorBox == 0)
                            {
                                MessageManager.ShowMsgBox("FPDY-000001", "提示", new string[] { this.printSetUp_0.CurrentPrinterName });
                            }
                            this.loger.Error("当前打印机型号不支持汉字防伪发票打印");
                            this._isPrint = "0004";
                        }
                        else
                        {
                            this.method_2();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[psu_OnPrint]: " + exception.ToString());
                this._isPrint = "0004";
                if (this.IsHindErrorBox == 0)
                {
                    MessageManager.ShowMsgBox("FPDY-000002");
                }
            }
        }
        #region 打印方法
        public byte[] DaYinMethod(object sender, PrintSetEventArgs e, bool isyulan)
        {
            byte[] imgbyte = null;
            try
            {
                if (this._isPrint == "0000")
                {
                    this._isPrint = "0002";
                }
                string[] strArray = Aisino.Fwkp.Print.Common.CheckInstalledFont();
                if ((strArray != null) && (strArray[0].Length > 0))
                {
                    if (this.IsHindErrorBox == 0)
                    {
                        MessageManager.ShowMsgBox("FPDY-000003", "错误", new string[] { strArray[0] });
                    }
                    string message = string.Format("当前操作系统缺少{0}等字体，请及时安装所需字体！", strArray[0]);
                    this.loger.Error(message);
                    this.loger.Error(strArray[1]);
                }
                else
                {
                    Printer printer = new Printer(this._args);
                    printer.GetPrinterArgs(IsZjFlag);
                    this.IsTaoDa = printer.RealPrinterArgs.IsQuanDa;
                    this.aisinoPrint.Data = this.DictCreate(this._args);
                    if (this.aisinoPrint.Data == null)
                    {
                        if (this.IsHindErrorBox == 0)
                        {
                            MessageManager.ShowMsgBox("FPDY-000004");
                        }
                        this.loger.Error("发票打印的对象为空");
                        this._isPrint = "0004";
                    }
                    else
                    {
                        this.loger.Error(string.Concat(new object[] { "[连续打印]Left:", printer.RealPrinterArgs.Left, "[Top]:", printer.RealPrinterArgs.Top }));
                        this.aisinoPrint.Canvas.startPoint = new PointF(this.MillimeterToPx(printer.RealPrinterArgs.Left), this.MillimeterToPx(printer.RealPrinterArgs.Top));
                        if ((this._IsHZFW && (this.printSetUp_0 != null)) && !this.printSetUp_0.IsSupportHZFW)
                        {
                            if (this.IsHindErrorBox == 0)
                            {
                                MessageManager.ShowMsgBox("FPDY-000001", "提示", new string[] { this.printSetUp_0.CurrentPrinterName });
                            }
                            this.loger.Error("当前打印机型号不支持汉字防伪发票打印");
                            this._isPrint = "0004";
                        }
                        else
                        {
                            if (this._isPrint == "0002")
                            {
                                try
                                {
                                    string str;
                                    if ((this._args != null) && (this._args.Length > 3))
                                    {
                                        string str2 = this._args[1].ToString() + " " + Aisino.Fwkp.Print.Common.smethod_1(this._args[2].ToString());
                                        this.aisinoPrint.printDocument.DocumentName = str2;
                                    }
                                    imgbyte = this.aisinoPrint.PrintMethod(isyulan, this.ZYFPLX, this._isZYPT);
                                    if ((((this._args != null) && (this._args.Length >= 4)) && ((this._args[3].ToString() == "_FP") && ((str = this._args[0].ToString()) != null))) && (((str == "s") || (str == "c")) || (((str == "f") || (str == "j")) || (str == "q"))))
                                    {
                                        ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPDYShareMethod", this._args);
                                        if (this.taxCard_0.SubSoftVersion == "Linux")
                                        {
                                            FPLX fplx = Aisino.Fwkp.Print.Common.DBFpzlToCardType(this._args[0].ToString());
                                            this.taxCard_0.UpdateDybzToDB((int)fplx, this._args[1].ToString(), this._args[2].ToString());
                                        }
                                    }
                                    this._isPrint = "0000";
                                }
                                catch (Exception exception)
                                {
                                    this._isPrint = "0004";
                                    this.loger.Error("[OnPrint函数异常]" + exception.ToString());
                                }
                            }
                        }
                    }
                }
                return imgbyte;
            }
            catch (Exception exception)
            {
                this.loger.Error("[psu_OnPrint]: " + exception.ToString());
                this._isPrint = "0004";
                if (this.IsHindErrorBox == 0)
                {
                    MessageManager.ShowMsgBox("FPDY-000002");
                }
                return null;
            }
        }
        #endregion
        public float MillimeterToPx(float float_0)
        {
            return (float)PrinterUnitConvert.Convert((double)(float_0 * 10f), PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
        }

        public void Preview()
        {
            PrintSetEventArgs e = new PrintSetEventArgs
            {
                IsTaoDa = false,
                Offset = (PointF)new Point(0, 0)
            };
            this.printSetUp_0.TopMost = false;
            this.method_3(null, e);
        }

        public void Print()
        {
            this.IsHindErrorBox = 0;
            this.printSetUp_0.IsGroupTDbutton = this.IsShowTaoDaGruoupButton;
            this.printSetUp_0.OnPreview += new PrintSetUp.PrintSet(this.method_3);
            this.printSetUp_0.OnPrint += new PrintSetUp.PrintSet(this.method_5);
            this.printSetUp_0.OnClose += new PrintSetUp.PrintSet(this.method_4);
			//逻辑修改 本地运行需要屏蔽此弹窗
            this.printSetUp_0.ShowDialog();
        }

        public void Print(bool bool_1 = true)
        {
            this.IsHindErrorBox = 0;
            if (bool_1)
            {
                this.Print();
            }
            else
            {
                PrintSetEventArgs e = new PrintSetEventArgs
                {
                    IsTaoDa = false,
                    Offset = (PointF)new Point(0, 0)
                };
                this.method_5(null, e);
            }
        }

        public void Print(bool bool_1 = true, int int_0 = 0)
        {
            this.IsHindErrorBox = int_0;
            if (bool_1)
            {
                this.Print();
            }
            else
            {
                PrintSetEventArgs e = new PrintSetEventArgs
                {
                    IsTaoDa = false,
                    Offset = (PointF)new Point(0, 0)
                };
                this.method_5(null, e);
            }
        }

        protected string Id
        {
            set
            {
                try
                {
                    Aisino.Fwkp.Print.ReadXml xml = Aisino.Fwkp.Print.ReadXml.Get();
                    if (xml.ContainsKey(value))
                    {
                        Aisino.Fwkp.Print.PrintFileModel model = xml[value];
                        if (model != null)
                        {
                            this.aisinoPrint.Canvas = this.CanvasCreate(model.CanvasPath);
                            this._isPrint = "0002";
                        }
                        else
                        {
                            this._isPrint = "0001";
                        }
                    }
                    else
                    {
                        this._isPrint = "0001";
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message);
                }
            }
        }

        public string IsPrint
        {
            get
            {
                return this._isPrint;
            }
        }

        protected bool IsTaoDa
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

