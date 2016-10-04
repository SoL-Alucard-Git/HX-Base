namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class FpxfPrint : IPrint
    {
        public FpxfPrint(string string_0, string[] string_1) : base("fpxf")
        {
            
            base.method_0(new object[] { string_0, string_1, 1 });
        }

        public FpxfPrint(string string_0, DataTable dataTable_0) : base("fpxf")
        {
            
            base.method_0(new object[] { string_0, dataTable_0, 0 });
        }

        protected override DataDict DictCreate(params object[] args)
        {
            try
            {
                if ((args != null) && (args.Length >= 3))
                {
                    switch (int.Parse(args[2].ToString()))
                    {
                        case 0:
                            return this.method_7(args);

                        case 1:
                            return this.method_6(args);
                    }
                    return null;
                }
                base._isPrint = "0003";
                return null;
            }
            catch (Exception exception)
            {
                base._isPrint = "0003";
                base.loger.Error("fpxf打印异常" + exception.Message);
            }
            return null;
        }

        private DataDict method_6(params object[] args)
        {
            string str = args[0].ToString();
            string[] strArray = (string[]) args[1];
            bool.Parse(args[2].ToString());
            List<Dictionary<string, object>> listDict = new List<Dictionary<string, object>>();
            Dictionary<string, object> item = new Dictionary<string, object>();
            item.Add("title", str);
            DataTable table = new DataTable();
            table.Columns.Add("column");
            int num = 0;
            foreach (string str2 in strArray)
            {
                DataRow row = table.NewRow();
                row["column"] = str2;
                table.Rows.Add(row);
                num++;
                if (num >= 40)
                {
                    num = 0;
                    item.Add("dt", table);
                    listDict.Add(item);
                    item = new Dictionary<string, object>();
                    table = new DataTable();
                    table.Columns.Add("column");
                    item.Add("title", str);
                }
            }
            if (table.Rows.Count > 0)
            {
                item.Add("dt", table);
                listDict.Add(item);
            }
            return new DataDict(listDict);
        }

        private DataDict method_7(params object[] args)
        {
            base.ZYFPLX = "";
            string str = args[0].ToString();
            DataTable table = (DataTable) args[1];
            List<Dictionary<string, object>> listDict = new List<Dictionary<string, object>>();
            Dictionary<string, object> item = new Dictionary<string, object>();
            item.Add("title", str);
            DataTable table2 = new DataTable();
            table2.Columns.Add("column");
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                DataRow row2 = table2.NewRow();
                row2["column"] = row[0];
                table2.Rows.Add(row2);
                num++;
                if (num >= 40)
                {
                    num = 0;
                    item.Add("dt", table2);
                    listDict.Add(item);
                    item = new Dictionary<string, object>();
                    table2 = new DataTable();
                    table2.Columns.Add("column");
                    item.Add("title", str);
                }
            }
            if (table2.Rows.Count > 0)
            {
                item.Add("dt", table2);
                listDict.Add(item);
            }
            return new DataDict(listDict);
        }
    }
}

