namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class CustomListView : ListView
    {
        private ListViewItem listViewItem_0;
        private ListViewItem listViewItem_1;

        public CustomListView()
        {
            
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            base.UpdateStyles();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.listViewItem_1 = base.GetItemAt(e.X, e.Y);
            if ((this.listViewItem_1 != null) && !this.listViewItem_1.Equals(this.listViewItem_0))
            {
                if (this.listViewItem_0 != null)
                {
                    this.listViewItem_0.BackColor = Color.White;
                }
                this.listViewItem_1.BackColor = Color.LightSkyBlue;
                this.listViewItem_0 = this.listViewItem_1;
            }
            base.OnMouseMove(e);
        }
    }
}

