namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class DataGridRowCellValidatingEventArgs : CancelEventArgs
    {
        private bool bool_0;
        private DataGridViewRow dataGridViewRow_0;
        private int int_0;
        private object object_0;

        public DataGridRowCellValidatingEventArgs(DataGridViewRow dataGridViewRow_1, bool bool_1, int int_1, object object_1)
        {
            
            this.dataGridViewRow_0 = dataGridViewRow_1;
            this.bool_0 = this.Cancel;
            this.int_0 = int_1;
            this.object_0 = object_1;
        }

        public bool Cancel
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
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

        public object FormattedValue
        {
            get
            {
                return this.object_0;
            }
        }
    }
}

