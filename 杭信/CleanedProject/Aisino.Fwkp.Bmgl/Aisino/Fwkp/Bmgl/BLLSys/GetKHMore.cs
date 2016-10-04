namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class GetKHMore : AbstractService
    {
        private BMKHManager khmanager = new BMKHManager();

        protected override object[] doService(object[] param)
        {
            if (!CheckPermission.Check("KH"))
            {
                return null;
            }
            string keyWord = (string) param[0];
            int topNo = (int) param[1];
            DataTable table = this.khmanager.AppendByKey(keyWord, topNo);
            table.Constraints.Clear();
            if (param.Length == 3)
            {
                string[] collection = ((string) param[2]).Split(new char[] { ',' });
                List<string> list = new List<string>();
                list.AddRange(collection);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (!list.Contains(table.Columns[i].ColumnName))
                    {
                        table.Columns.RemoveAt(i);
                        i--;
                    }
                }
            }
            return new object[] { table };
        }
    }
}

