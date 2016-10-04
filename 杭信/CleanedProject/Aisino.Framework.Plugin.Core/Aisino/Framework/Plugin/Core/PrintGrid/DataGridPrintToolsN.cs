namespace Aisino.Framework.Plugin.Core.PrintGrid
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class DataGridPrintToolsN
    {
        public DataGridPrintToolsN()
        {
            
        }

        public static bool Print(DataGridView dataGridView_0, object object_0, string string_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_0)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            flag = new PrintOptionsFormN(dataGridView_0, object_0, string_0, header, footer).Print(bool_0);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            return flag;
        }

        public static bool PrintSerial(DataGridView dataGridView_0, object object_0, string string_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_0, bool bool_1, string string_1)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            PrintOptionsFormN mn = new PrintOptionsFormN(dataGridView_0, object_0, string_0, header, footer) {
                showTextInserialPrint = string_1,
                isSerialPrint = bool_1
            };
            flag = mn.Print(bool_0);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            if (mn.isGiveUpByUser)
            {
                throw new Exception("用户放弃连续打印");
            }
            return flag;
        }

        internal static bool smethod_0(DataGridView dataGridView_0, string string_0, object object_0, string string_1, List<PrinterItems> header, List<PrinterItems> footer, bool bool_0)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            flag = new PrintOptionsFormN(dataGridView_0, string_0, object_0, string_1, header, footer).Print(bool_0);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            return flag;
        }
    }
}

