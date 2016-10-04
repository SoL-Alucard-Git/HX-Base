namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;

    public class GoToPageEventArgs : EventArgs
    {
        private bool bool_0;
        private int int_0;
        private int int_1;

        public GoToPageEventArgs(int int_2, int int_3, bool bool_1)
        {
            
            this.int_0 = int_2;
            this.int_1 = int_3;
            this.bool_0 = bool_1;
        }

        public int PageNO
        {
            get
            {
                return this.int_0;
            }
        }

        public int PageSize
        {
            get
            {
                return this.int_1;
            }
        }

        public bool ShowAllRows
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}

