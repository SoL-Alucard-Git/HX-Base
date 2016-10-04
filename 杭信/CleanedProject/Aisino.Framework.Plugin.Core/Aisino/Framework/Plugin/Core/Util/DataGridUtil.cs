namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core.ExcelGrid;
    using Aisino.Framework.Plugin.Core.ExcelXml;
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class DataGridUtil
    {
        public event EventHandler<ExportEndedEventArgs> ExportEndedEvent;

        public event EventHandler ExportProgressingEvent;

        public event EventHandler ExportStartingEvent;

        public DataGridUtil()
        {
            
        }

        public void Export(DataGridView dataGridView_0)
        {
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            SaveFileDialog dialog = new SaveFileDialog();
            string fileName = string.Empty;
            dialog.RestoreDirectory = true;
            dialog.AddExtension = true;
            dialog.Title = "保存导出数据......";
            dialog.Filter = "Excel XML数据(*.xml)|*.xml|Excel 97-2003 工作簿(*.xls)|*.xls|Excel 工作簿(*.xlsx)|*.xlsx|CSV文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                if (dialog.FilterIndex == 1)
                {
                    this.method_2(dataGridView_0, fileName, null);
                }
                else if (dialog.FilterIndex == 2)
                {
                    this.method_1(dataGridView_0, fileName, null);
                }
                else if (dialog.FilterIndex == 3)
                {
                    this.method_1(dataGridView_0, fileName, null);
                }
                else if (dialog.FilterIndex == 4)
                {
                    this.method_0(dataGridView_0, fileName);
                }
            }
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
        }

        private void method_0(DataGridView dataGridView_0, string string_0)
        {
            FileStream stream = new FileStream(string_0, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
            ExportDataGridViewToExcel excel = new ExportDataGridViewToExcel(dataGridView_0);
            excel.ExportStartingEvent += new EventHandler(this.method_4);
            excel.ExportProgressingEvent += new EventHandler(this.method_3);
            excel.ExportEndedEvent += new EventHandler<ExportEndedEventArgs>(this.method_5);
            excel.ConvertToCSV(stream);
        }

        private void method_1(DataGridView dataGridView_0, string string_0, string string_1)
        {
            ExportDataGridViewToExcel excel = new ExportDataGridViewToExcel(dataGridView_0);
            excel.ExportStartingEvent += new EventHandler(this.method_4);
            excel.ExportProgressingEvent += new EventHandler(this.method_3);
            excel.ExportEndedEvent += new EventHandler<ExportEndedEventArgs>(this.method_5);
            excel.ConvertToExcel(string_0, string_1);
        }

        private void method_2(DataGridView dataGridView_0, string string_0, string string_1)
        {
            ExportToXml xml = new ExportToXml(dataGridView_0);
            xml.ExportStartingEvent += new EventHandler(this.method_4);
            xml.ExportProgressingEvent += new EventHandler(this.method_3);
            xml.ExportEndedEvent += new EventHandler<ExportEndedEventArgs>(this.method_5);
            xml.ConvertToExcel(string_0, string_1);
        }

        private void method_3(object sender, EventArgs e)
        {
            if (this.ExportProgressingEvent != null)
            {
                this.ExportProgressingEvent(sender, e);
            }
        }

        private void method_4(object sender, EventArgs e)
        {
            if (this.ExportStartingEvent != null)
            {
                this.ExportStartingEvent(sender, e);
            }
        }

        private void method_5(object sender, ExportEndedEventArgs e)
        {
            if (this.ExportEndedEvent != null)
            {
                this.ExportEndedEvent(sender, e);
            }
        }
    }
}

