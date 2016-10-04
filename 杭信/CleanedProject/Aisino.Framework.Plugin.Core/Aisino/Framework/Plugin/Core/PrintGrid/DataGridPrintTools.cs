namespace Aisino.Framework.Plugin.Core.PrintGrid
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataGridPrintTools
    {
        public DataGridPrintTools()
        {
            
        }

        public static bool Print(DataGridView dataGridView_0, object object_0, string string_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_0)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            PrintOptionsForm form = new PrintOptionsForm(dataGridView_0, object_0, string_0, header, footer);
            if ((string_0 != null) && (string_0.IndexOf("发票修复") != -1))
            {
                form.SetBodyFont(new Font("宋体", 9f));
            }
            flag = form.Print(bool_0);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            return flag;
        }

        public static bool Print(bool bool_0, DataGridView dataGridView_0, object object_0, string string_0, List<PrinterItems> header, List<PrinterItems> footer, bool bool_1)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            PrintOptionsForm form = new PrintOptionsForm(bool_0, dataGridView_0, object_0, string_0, header, footer);
            if ((string_0 != null) && (string_0.IndexOf("发票修复") != -1))
            {
                form.SetBodyFont(new Font("宋体", 9f));
            }
            flag = form.Print(bool_1);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            return flag;
        }

        internal static bool smethod_0(DataGridView dataGridView_0, string string_0, object object_0, string string_1, List<PrinterItems> header, List<PrinterItems> footer, bool bool_0)
        {
            bool flag = false;
            bool allowUserToAddRows = dataGridView_0.AllowUserToAddRows;
            dataGridView_0.AllowUserToAddRows = false;
            PrintOptionsForm form = new PrintOptionsForm(dataGridView_0, string_0, object_0, string_1, header, footer);
            if (string_1.Contains("编码"))
            {
                form.SetChkCounterEnabled(false);
                form.SetChkCounterPubEnabled(false);
            }
            if ((string_1 != null) && (string_1.IndexOf("发票修复") != -1))
            {
                form.SetBodyFont(new Font("宋体", 9f));
            }
            flag = form.Print(bool_0);
            dataGridView_0.AllowUserToAddRows = allowUserToAddRows;
            return flag;
        }
    }
}

