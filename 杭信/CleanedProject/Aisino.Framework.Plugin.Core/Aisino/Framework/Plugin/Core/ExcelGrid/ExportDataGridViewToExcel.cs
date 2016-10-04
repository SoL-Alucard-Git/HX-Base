namespace Aisino.Framework.Plugin.Core.ExcelGrid
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class ExportDataGridViewToExcel
    {
        private DataGridView dataGridView_0;

        public event EventHandler<ExportEndedEventArgs> ExportEndedEvent;

        public event EventHandler ExportProgressingEvent;

        public event EventHandler ExportStartingEvent;

        public ExportDataGridViewToExcel(DataGridView dataGridView_1)
        {
            
            if (dataGridView_1 == null)
            {
                throw new NullReferenceException("the paramter is null.");
            }
            this.dataGridView_0 = dataGridView_1;
        }

        public void ConvertToCSV(object object_0)
        {
            int rowCount = this.dataGridView_0.RowCount;
            int columnCount = this.dataGridView_0.ColumnCount;
            if ((rowCount <= 0) || (columnCount <= 0))
            {
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception("No data for exporting.")));
            }
            else
            {
                this.ExportStartingEvent(columnCount + rowCount, new EventArgs());
                Stream stream = null;
                StreamWriter writer = null;
                string str = "";
                try
                {
                    stream = (Stream) object_0;
                    writer = new StreamWriter(stream, Encoding.UTF8);
                    for (int i = 0; i < columnCount; i++)
                    {
                        if (i > 0)
                        {
                            str = str + ",";
                        }
                        str = str + this.dataGridView_0.Columns[i].HeaderText;
                        this.ExportProgressingEvent(i + 1, new EventArgs());
                    }
                    writer.WriteLine(str);
                    for (int j = 0; j < rowCount; j++)
                    {
                        string str2 = "";
                        for (int k = 0; k < columnCount; k++)
                        {
                            if (k > 0)
                            {
                                str2 = str2 + ",";
                            }
                            if (this.dataGridView_0.Rows[j].Cells[k].Value == null)
                            {
                                str2 = str2 ?? "";
                            }
                            else
                            {
                                str2 = str2 + this.dataGridView_0.Rows[j].Cells[k].Value.ToString().Trim();
                            }
                        }
                        writer.WriteLine(str2);
                        this.ExportProgressingEvent((columnCount + j) + 1, new EventArgs());
                    }
                }
                catch (Exception exception)
                {
                    this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception(string.Format("Exporte file failed.\r\n", exception.Message))));
                    return;
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(true, null));
            }
        }

        public void ConvertToExcel(string string_0, string string_1)
        {
            int rowCount = this.dataGridView_0.RowCount;
            int columnCount = this.dataGridView_0.ColumnCount;
            if ((rowCount <= 0) || (columnCount <= 0))
            {
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception("No data for exporting.")));
            }
            else if (rowCount > 0x10000)
            {
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception("The rows count should be less than or equal to 65536.")));
            }
            else if (columnCount > 0xff)
            {
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception("The columns count should be less than or equal to 255.")));
            }
            else
            {
                if (File.Exists(string_0))
                {
                    try
                    {
                        File.Delete(string_0);
                    }
                    catch (Exception exception)
                    {
                        this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception(string.Format("Cannot delete the file\r\n{0}", exception.Message))));
                        return;
                    }
                }
                this.ExportStartingEvent(columnCount + rowCount, new EventArgs());
                IWorkbook workbook = null;
                ISheet sheet = null;
                IRow row = null;
                try
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet(string_1);
                    row = sheet.CreateRow(0);
                    for (int i = 0; i <= (columnCount - 1); i++)
                    {
                        if (this.dataGridView_0.Columns[i].Visible)
                        {
                            row.CreateCell(i).SetCellValue(this.dataGridView_0.Columns[i].HeaderText.Trim());
                        }
                        this.ExportProgressingEvent(i + 1, new EventArgs());
                    }
                    int num4 = 0;
                Label_0164:
                    if (num4 <= (rowCount - 1))
                    {
                        row = sheet.CreateRow(num4 + 1);
                        int column = 0;
                        while (true)
                        {
                            if (column < columnCount)
                            {
                                if (this.dataGridView_0.Columns[column].Visible)
                                {
                                    try
                                    {
                                        row.CreateCell(column).SetCellValue(this.dataGridView_0.Rows[num4].Cells[column].Value.ToString().Trim());
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            else
                            {
                                this.ExportProgressingEvent((columnCount + num4) + 1, new EventArgs());
                                num4++;
                                goto Label_0164;
                            }
                            column++;
                        }
                    }
                    FileStream stream = new FileStream(string_0, FileMode.Create);
                    workbook.Write(stream);
                    stream.Close();
                }
                catch (Exception exception2)
                {
                    this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception(string.Format("Export file failed.\r\n{0}", exception2.Message))));
                    return;
                }
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(true, null));
            }
        }
    }
}

