namespace Aisino.Framework.Plugin.Core.StatisticsGrid
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class GridStatisticsUtil
    {
        public GridStatisticsUtil()
        {
            
        }

        public static void GridStatistics(DataGridView dataGridView_0, object object_0)
        {
            string str = object_0.GetType().FullName + "_" + dataGridView_0.Name + ".sta";
            string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AisinoInvoice"), "GridStatistics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str3 = Path.Combine(path, str);
            new StatisticsOptions(dataGridView_0, str3).ShowDialog();
        }
    }
}

