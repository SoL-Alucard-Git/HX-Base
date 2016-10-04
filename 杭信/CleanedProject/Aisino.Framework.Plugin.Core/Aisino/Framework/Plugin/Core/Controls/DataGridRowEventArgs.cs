namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Windows.Forms;

    public class DataGridRowEventArgs : EventArgs
    {
        private DataGridViewRow dataGridViewRow_0;
        private int int_0;

        public DataGridRowEventArgs(DataGridViewRow dataGridViewRow_1, int int_1)
        {
            
            this.dataGridViewRow_0 = dataGridViewRow_1;
            this.int_0 = int_1;
        }

        public int CurrentColumnIndex
        {
            get
            {
                return this.int_0;
            }
        }

        public DataGridViewRow CurrentRow
        {
            get
            {
                return this.dataGridViewRow_0;
            }
        }
    }
}

