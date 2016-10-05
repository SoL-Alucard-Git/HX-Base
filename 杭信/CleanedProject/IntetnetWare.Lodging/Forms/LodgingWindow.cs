using System;

namespace InternetWare.Form
{
    public partial class LodgingWindow : System.Windows.Forms.Form
    {
        public LodgingWindow()
        {
            InitializeComponent();
        }

        private void LodgingWindow_Load(object sender, EventArgs e)
        {
            Init_ChaXunPage();
            Init_DaYinPage();
        }
    }
}
