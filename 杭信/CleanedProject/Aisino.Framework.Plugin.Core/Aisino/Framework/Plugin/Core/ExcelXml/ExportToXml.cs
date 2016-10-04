namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelGrid;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class ExportToXml
    {
        private DataGridView dataGridView_0;

        public event EventHandler<ExportEndedEventArgs> ExportEndedEvent;

        public event EventHandler ExportProgressingEvent;

        public event EventHandler ExportStartingEvent;

        public ExportToXml(DataGridView dataGridView_1)
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
                    catch (Exception exception2)
                    {
                        this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception(string.Format("Cannot delete the file\r\n{0}", exception2.Message))));
                        return;
                    }
                }
                this.ExportStartingEvent(columnCount + rowCount, new EventArgs());
                ExcelXmlWorkbook workbook = new ExcelXmlWorkbook {
                    Properties = { Author = "航天信息股份有限公司" }
                };
                Worksheet worksheet = workbook[0];
                if (string.Empty.Equals(string_1))
                {
                    worksheet.Name = "防伪开票系统导出报表";
                }
                else
                {
                    worksheet.Name = string_1;
                }
                try
                {
                    worksheet.FreezeTopRows = 1;
                    int num3 = 1;
                    for (int i = 0; i <= (columnCount - 1); i++)
                    {
                        if (this.dataGridView_0.Columns[i].Visible)
                        {
                            worksheet[num3 - 1, 0].Value = this.dataGridView_0.Columns[i].HeaderText.Trim();
                            num3++;
                        }
                        this.ExportProgressingEvent(i + 1, new EventArgs());
                    }
                    new Range(worksheet[0, 0], worksheet[columnCount - 1, 0]).Font.Bold = true;
                    int num5 = 0;
                Label_01B7:
                    if (num5 <= (rowCount - 1))
                    {
                        num3 = 1;
                        int num6 = 0;
                        while (true)
                        {
                            if (num6 < columnCount)
                            {
                                if (this.dataGridView_0.Columns[num6].Visible)
                                {
                                    try
                                    {
                                        worksheet[num3 - 1, num5 + 1].Value = this.dataGridView_0.Rows[num5].Cells[num6].Value.ToString().Trim();
                                        num3++;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            else
                            {
                                this.ExportProgressingEvent((columnCount + num5) + 1, new EventArgs());
                                num5++;
                                goto Label_01B7;
                            }
                            num6++;
                        }
                    }
                    workbook.Export(string_0);
                }
                catch (Exception exception)
                {
                    this.ExportEndedEvent(new object(), new ExportEndedEventArgs(false, new Exception(string.Format("Export file failed.\r\n{0}", exception.Message))));
                    return;
                }
                finally
                {
                    GC.Collect();
                }
                this.ExportEndedEvent(new object(), new ExportEndedEventArgs(true, null));
            }
        }
    }
}

