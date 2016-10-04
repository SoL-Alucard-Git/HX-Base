namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Controls.AisinoControls;
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    [Description("打印控件")]
    public class AisinoPrint : Component
    {
        private bool bool_0;
        private bool bool_1;
        private Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas canvas_0;
        private DataDict dataDict_0;
        private int int_0;
        public PageSetupDialog pageSetupDialog;
        public PrintDocument printDocument;
        private PrintPreviewDialogEx printPreviewDialogEx_0;
        private string string_0;
        private string string_1;
        private ToolStripControlHost toolStripControlHost_0;

        public event PrintEvnet printEventBegin;

        public event PrintEvnet printEventEnd;

        public AisinoPrint()
        {
            
            this.string_1 = string.Empty;
            if (this.printDocument == null)
            {
                this.printDocument = new PrintDocument();
                this.printDocument.PrintController = new StandardPrintController();
            }
            if (this.pageSetupDialog == null)
            {
                this.pageSetupDialog = new PageSetupDialog();
            }
            if (this.printPreviewDialogEx_0 == null)
            {
                this.printPreviewDialogEx_0 = new PrintPreviewDialogEx();
                this.printPreviewDialogEx_0.SaveStripBT.Click += new EventHandler(this.method_1);
                ToolStrip strip = null;
                foreach (Control control in this.printPreviewDialogEx_0.Controls)
                {
                    if (control.GetType() == typeof(ToolStrip))
                    {
                        strip = control as ToolStrip;
                    }
                }
                if ((strip != null) && (strip.Items.Count > 0))
                {
                    foreach (ToolStripItem item in strip.Items)
                    {
                        if (item is ToolStripControlHost)
                        {
                            this.toolStripControlHost_0 = (ToolStripControlHost) item;
                        }
                    }
                }
            }
            this.printDocument.BeginPrint += new PrintEventHandler(this.printDocument_BeginPrint);
            this.printDocument.EndPrint += new PrintEventHandler(this.printDocument_EndPrint);
            this.printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
            this.pageSetupDialog.Document = this.printDocument;
        }

        public AisinoPrint(DataDict dataDict_1, Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas canvas_1) : this()
        {
            
            this.Data = dataDict_1;
            this.Canvas = canvas_1;
        }

        public AisinoPrint(Dictionary<string, object> dict, bool bool_2, Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas canvas_1) : this()
        {
            
            this.Canvas = canvas_1;
            this.Data = new DataDict(dict);
        }

        public AisinoPrint(Dictionary<string, object> dict, bool bool_2, string string_2) : this()
        {
            
            this.Canvas = new Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas(string_2);
            this.Data = new DataDict(dict);
        }

        private AisinoPrint(Dictionary<string, object> dict, bool bool_2, XmlDocument xmlDocument_0) : this()
        {
            
            this.Canvas = new Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas(xmlDocument_0, "Canvas");
            this.Data = new DataDict(dict);
        }

        public AisinoPrint(List<Dictionary<string, object>> listBind, bool bool_2, string string_2) : this()
        {
            
            new Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas(string_2);
            this.Data = new DataDict(listBind);
        }

        private void method_0(Graphics graphics_0, int int_1)
        {
            try
            {
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_0.SmoothingMode = SmoothingMode.HighQuality;
                graphics_0.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics_0.PixelOffsetMode = PixelOffsetMode.HighQuality;
                if (this.bool_1)
                {
                    Bitmap image = null;
                    PointF empty = (PointF) Point.Empty;
                    string str = this.string_1.ToUpper();
                    if (str != null)
                    {
                        if (str == "C")
                        {
                            image = Class131.smethod_39();
                            empty = new PointF(this.canvas_0.startPoint.X + 5f, this.canvas_0.startPoint.Y - 20f);
                        }
                        else if (str == "S")
                        {
                            image = Class131.smethod_47();
                            empty = new PointF(this.canvas_0.startPoint.X + 8f, this.canvas_0.startPoint.Y - 18f);
                        }
                        else if (str == "JO")
                        {
                            image = Class131.smethod_12();
                            empty = new PointF(this.canvas_0.startPoint.X, this.canvas_0.startPoint.Y - 18f);
                        }
                        else if (!(str == "JN"))
                        {
                            if (str == "F")
                            {
                                image = Class131.smethod_11();
                                empty = new PointF(this.canvas_0.startPoint.X - 25f, this.canvas_0.startPoint.Y - 13f);
                            }
                        }
                        else
                        {
                            image = Class131.smethod_13();
                            empty = new PointF(this.canvas_0.startPoint.X - 15f, this.canvas_0.startPoint.Y - 18f);
                        }
                    }
                    if (image != null)
                    {
                        if (this.string_1.ToUpper() == "F")
                        {
                            empty.X += 3f;
                        }
                        graphics_0.DrawImage(image, empty);
                    }
                }
                if (int_1 < this.dataDict_0.Count)
                {
                    this.canvas_0.Print(graphics_0, this.dataDict_0.Data(int_1), this.bool_0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void method_1(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    Filter = "EMF文件(*.EMF)|*.EMF|JPG文件(*.JPG)|*.JPG"
                };
                string str = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.string_0 = dialog.FileName;
                    if (this.toolStripControlHost_0 != null)
                    {
                        int num2 = this.method_2(this.toolStripControlHost_0.Text);
                        Image image = null;
                        if (this.dataDict_0.DataList().Count > 0)
                        {
                            Graphics graphics;
                            if (!this.dataDict_0.Data(0).ContainsKey("allpage"))
                            {
                                if (!this.dataDict_0.Data(0).ContainsKey("hpxxbbh"))
                                {
                                    image = new Bitmap(950, 700);
                                    graphics = Graphics.FromImage(image);
                                    graphics.FillRectangle(Brushes.White, 0, 0, 950, 700);
                                }
                                else
                                {
                                    image = new Bitmap(950, 0x44c);
                                    graphics = Graphics.FromImage(image);
                                    graphics.FillRectangle(Brushes.White, 0, 0, 950, 0x44c);
                                }
                                if ((num2 <= this.dataDict_0.Count) && (num2 >= 1))
                                {
                                    this.method_0(graphics, num2 - 1);
                                }
                                image.Save(this.string_0);
                            }
                            else
                            {
                                image = new Bitmap(950, 0x44c);
                                graphics = Graphics.FromImage(image);
                                int startIndex = dialog.FileName.LastIndexOf(@"\");
                                str = dialog.FileName.Substring(startIndex);
                                this.string_0 = dialog.FileName.Substring(0, startIndex);
                                int index = str.IndexOf('.');
                                string filename = "";
                                for (int i = 1; i <= this.dataDict_0.Count; i++)
                                {
                                    graphics.FillRectangle(Brushes.White, 0, 0, 950, 0x44c);
                                    this.method_0(graphics, i - 1);
                                    if (this.dataDict_0.Count > 1)
                                    {
                                        filename = this.string_0 + str.Insert(index, "_" + i.ToString());
                                    }
                                    else
                                    {
                                        filename = dialog.FileName;
                                    }
                                    image.Save(filename);
                                }
                            }
                            image.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int method_2(object object_0)
        {
            if (object_0 == null)
            {
                return 0;
            }
            int result = 0;
            int.TryParse(object_0.ToString(), out result);
            return result;
        }

        public void Preview(string string_2, bool bool_2 = false)
        {
            try
            {
                this.bool_0 = bool_2;
                this.bool_1 = true;
                this.string_1 = string_2;
                this.printPreviewDialogEx_0.Document = this.printDocument;
                this.printPreviewDialogEx_0.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Print(bool bool_2 = false)
        {
            this.bool_0 = bool_2;
            this.bool_1 = false;
            this.printDocument.Print();
        }

        public void Print(out PreviewPageInfo[] previewPageInfo_0)
        {
            PreviewPrintController controller = new PreviewPrintController {
                UseAntiAlias = true
            };
            this.printDocument.PrintController = controller;
            this.printDocument.Print();
            previewPageInfo_0 = controller.GetPreviewPageInfo();
        }

        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if ((this.int_0 == 0) && (this.printEventBegin != null))
                {
                    this.printEventBegin(e);
                }
                this.method_0(e.Graphics, this.int_0);
                this.int_0++;
                if (this.int_0 < this.dataDict_0.Count)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    this.int_0 = 0;
                    if (this.printEventEnd != null)
                    {
                        this.printEventEnd(e);
                    }
                }
                this.bool_1 = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Aisino.Framework.Plugin.Core.Controls.PrintControl.Canvas Canvas
        {
            get
            {
                return this.canvas_0;
            }
            set
            {
                this.canvas_0 = value;
                if (this.printDocument != null)
                {
                    PaperSize size3 = new PaperSize("Custom", this.canvas_0.PageSize.Width, this.canvas_0.PageSize.Height);
                    this.printDocument.DefaultPageSettings.PaperSize = size3;
                }
            }
        }

        public DataDict Data
        {
            get
            {
                return this.dataDict_0;
            }
            set
            {
                this.dataDict_0 = value;
            }
        }

        public PrinterSettings.PrinterResolutionCollection Items
        {
            get
            {
                return this.pageSetupDialog.PrinterSettings.PrinterResolutions;
            }
        }

        public PrinterResolution Resolution
        {
            get
            {
                return this.pageSetupDialog.PageSettings.PrinterResolution;
            }
            set
            {
                this.pageSetupDialog.PageSettings.PrinterResolution = value;
            }
        }

        public delegate void PrintEvnet(PrintPageEventArgs e);
    }
}

