namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Fwkp.Wbjk;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;

    public class ExcelRead
    {
        public const string EXCELCONNECTION = "Provider=Microsoft.ACE.OleDb.12.0;Extended Properties='Excel 12.0;HDR=NO;IMEX=1';";

        public static DataTable ExcelToDataTable(int HeadLine, string strExcelFileName, string SheetName)
        {
            DataTable table = ExcelToDataTable(strExcelFileName, SheetName, HeadLine);
            if (table.Rows.Count <= 0)
            {
                throw new CustomException("表头行数过大，请重新设置Excel文件或减少表头行数");
            }
            return table;
        }

        public static DataTable ExcelToDataTable(string excelFileFullPath, string sheetName, int startRow)
        {
            DataTable table2;
            try
            {
                table2 = ExcelToDataTableNPOI(excelFileFullPath, sheetName, startRow);
            }
            catch (AccessViolationException exception)
            {
                throw new CustomException(exception.Message);
            }
            catch (Exception exception2)
            {
                if (exception2.Message.Contains("该表为空，不能导入"))
                {
                    throw new CustomException("该表表头为空，不能导入");
                }
                if (exception2.Message.ToString().Contains("无法找到列"))
                {
                    throw new CustomException("该表表头以下缺少单据列，请完善该表");
                }
                if (exception2.Message.Contains("Invalid header signature"))
                {
                    throw new CustomException("该文件不符合Excel标准格式，请尝试使用Excel软件另存该文件解决此问题");
                }
                if (exception2.Message.Contains("非Excel文件"))
                {
                    throw new CustomException("非Excel文件");
                }
                HandleException.HandleError(exception2);
                table2 = null;
            }
            return table2;
        }

        public static DataTable ExcelToDataTableNPOI(string excelFileFullPath, string sheetName, int startRowNum)
        {
            int num;
            DataTable table = new DataTable();
            IWorkbook workbook = null;
            using (FileStream stream = new FileStream(excelFileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (excelFileFullPath.ToLower().IndexOf(".xlsx") <= 0)
                {
                    if (excelFileFullPath.ToLower().IndexOf(".xls") <= 0)
                    {
                        throw new CustomException("非Excel文件");
                    }
                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    workbook = new XSSFWorkbook(stream);
                }
            }
            ISheet sheetAt = workbook.GetSheetAt(workbook.ActiveSheetIndex);
            for (num = sheetAt.LastRowNum - 1; num >= sheetAt.FirstRowNum; num--)
            {
                if (null == sheetAt.GetRow(num))
                {
                    sheetAt.ShiftRows(num + 1, sheetAt.LastRowNum, -1);
                }
            }
            if (startRowNum > sheetAt.LastRowNum)
            {
                throw new CustomException("该表为空，不能导入");
            }
            int lastCellNum = 0;
            for (num = startRowNum; num <= sheetAt.LastRowNum; num++)
            {
                IRow row = sheetAt.GetRow(num);
                if ((row != null) && (lastCellNum < row.LastCellNum))
                {
                    lastCellNum = row.LastCellNum;
                }
            }
            for (num = 0; num < lastCellNum; num++)
            {
                DataColumn column = new DataColumn(num.ToString(), Type.GetType("System.String"));
                table.Columns.Add(column);
            }
            for (num = startRowNum; num <= sheetAt.LastRowNum; num++)
            {
                IRow row2 = sheetAt.GetRow(num);
                if ((row2 != null) && (-1 != row2.FirstCellNum))
                {
                    DataRow row3 = table.NewRow();
                    for (int i = row2.FirstCellNum; i < lastCellNum; i++)
                    {
                        ICell cell = row2.GetCell(i);
                        if (cell == null)
                        {
                            continue;
                        }
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                            {
                                if (!DateUtil.IsCellDateFormatted(cell))
                                {
                                    break;
                                }
                                row3[i] = cell.DateCellValue.ToShortDateString();
                                continue;
                            }
                            case CellType.Formula:
                            {
                                row3[i] = cell.NumericCellValue.ToString();
                                continue;
                            }
                            case CellType.Boolean:
                            {
                                row3[i] = cell.BooleanCellValue.ToString();
                                continue;
                            }
                            default:
                                goto Label_029C;
                        }
                        row3[i] = cell.NumericCellValue.ToString();
                        continue;
                    Label_029C:
                        row3[i] = cell.StringCellValue.ToString();
                    }
                    table.Rows.Add(row3);
                }
            }
            return table;
        }

        public static DataTable GetExcelTableNames(string path)
        {
            DataTable table = new DataTable();
            if (!File.Exists(path))
            {
                return table;
            }
            path = "Provider=Microsoft.ACE.OleDb.12.0;Extended Properties='Excel 12.0;HDR=NO;IMEX=1';Data Source=" + path;
            using (OleDbConnection connection = new OleDbConnection(path))
            {
                connection.Open();
                return connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            }
        }
    }
}

