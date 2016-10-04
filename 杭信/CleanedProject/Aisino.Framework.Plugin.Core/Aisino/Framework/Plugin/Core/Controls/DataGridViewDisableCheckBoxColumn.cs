namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Windows.Forms;

    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public DataGridViewDisableCheckBoxColumn()
        {
            
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
        }
    }
}

