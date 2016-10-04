using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        SQLiteConnection cnn;

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainWindow_Loaded;
            this.Unloaded -= MainWindow_Unloaded;
            cnn.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //SQLite 文件的完全路径。  
            string dataBasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\DB\cc3268.dll";
            //SQLite 文件的密码。  
            string password = "LoveR1314;";
            //SQLite 数据库的连接字符串  
            string sqliteConnnectionString = null;
            if (password != null)   //数据数据库有密码的话，需要在连接字符串后面加上密码信息。  
                sqliteConnnectionString = string.Format("Data Source={0};password={1}", dataBasePath, password);
            else
                sqliteConnnectionString = string.Format("Data Source={0}", dataBasePath);

            cnn = new SQLiteConnection(sqliteConnnectionString);
            cnn.Open();

            DataTable dt = new DataTable();
            SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter("select * from SQLITE_MASTER", cnn);
            sqliteAdapter.Fill(dt);
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["type"].ToString() == "table")
                {
                    Console.WriteLine(row["tbl_name"]);
                    list.Add(row["tbl_name"].ToString());
                }
            }
            lb.ItemsSource = list;
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter($"select * from {e.AddedItems[0]}", cnn);
            sqliteAdapter.Fill(dt);
            dataGrid.ItemsSource = dt.AsDataView();
        }
    }
}
