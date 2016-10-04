namespace Aisino.Fwkp.Wbjk.Common
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Data;
    using System.IO;

    public class ExcelHelper : IDisposable
    {
        private bool disposed;
        private string fileName = null;
        private FileStream fs = null;
        private IWorkbook workbook = null;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            this.disposed = false;
        }

        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int num = 0;
            int column = 0;
            int rownum = 0;
            ISheet sheet = null;
            this.fs = new FileStream(this.fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (this.fileName.IndexOf(".xlsx") > 0)
            {
                this.workbook = new XSSFWorkbook();
            }
            else if (this.fileName.IndexOf(".xls") > 0)
            {
                this.workbook = new HSSFWorkbook();
            }
            try
            {
                IRow row;
                if (this.workbook != null)
                {
                    sheet = this.workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }
                if (isColumnWritten)
                {
                    row = sheet.CreateRow(0);
                    column = 0;
                    while (column < data.Columns.Count)
                    {
                        row.CreateCell(column).SetCellValue(data.Columns[column].ColumnName);
                        column++;
                    }
                    rownum = 1;
                }
                else
                {
                    rownum = 0;
                }
                for (num = 0; num < data.Rows.Count; num++)
                {
                    row = sheet.CreateRow(rownum);
                    for (column = 0; column < data.Columns.Count; column++)
                    {
                        row.CreateCell(column).SetCellValue(data.Rows[num][column].ToString());
                    }
                    rownum++;
                }
                this.workbook.Write(this.fs);
                return rownum;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
                return -1;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && (this.fs != null))
                {
                    this.fs.Close();
                }
                this.fs = null;
                this.disposed = true;
            }
        }

        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheetAt = null;
            DataTable table = new DataTable();
            int firstRowNum = 0;
            try
            {
                this.fs = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
                if (this.fileName.IndexOf(".xlsx") > 0)
                {
                    this.workbook = new XSSFWorkbook(this.fs);
                }
                else if (this.fileName.IndexOf(".xls") > 0)
                {
                    this.workbook = new HSSFWorkbook(this.fs);
                }
                if (sheetName != null)
                {
                    sheetAt = this.workbook.GetSheet(sheetName);
                    if (sheetAt == null)
                    {
                        sheetAt = this.workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheetAt = this.workbook.GetSheetAt(0);
                }
                if (sheetAt != null)
                {
                    int num3;
                    IRow row = sheetAt.GetRow(0);
                    int lastCellNum = row.LastCellNum;
                    if (isFirstRowColumn)
                    {
                        for (num3 = row.FirstCellNum; num3 < lastCellNum; num3++)
                        {
                            ICell cell = row.GetCell(num3);
                            if (cell != null)
                            {
                                string stringCellValue = cell.StringCellValue;
                                if (stringCellValue != null)
                                {
                                    DataColumn column = new DataColumn(stringCellValue);
                                    table.Columns.Add(column);
                                }
                            }
                        }
                        firstRowNum = sheetAt.FirstRowNum + 1;
                    }
                    else
                    {
                        firstRowNum = sheetAt.FirstRowNum;
                    }
                    int lastRowNum = sheetAt.LastRowNum;
                    for (num3 = firstRowNum; num3 <= lastRowNum; num3++)
                    {
                        IRow row2 = sheetAt.GetRow(num3);
                        if (row2 != null)
                        {
                            DataRow row3 = table.NewRow();
                            for (int i = row2.FirstCellNum; i < lastCellNum; i++)
                            {
                                if (row2.GetCell(i) != null)
                                {
                                    row3[i] = row2.GetCell(i).ToString();
                                }
                            }
                            table.Rows.Add(row3);
                        }
                    }
                }
                return table;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
                return null;
            }
        }
    }
}

