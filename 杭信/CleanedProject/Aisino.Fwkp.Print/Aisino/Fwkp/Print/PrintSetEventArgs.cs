namespace Aisino.Fwkp.Print
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PrintSetEventArgs : EventArgs
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private PointF pointF_0;

        public PrintSetEventArgs()
        {
            
        }

        public bool IsTaoDa
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public PointF Offset
        {
            [CompilerGenerated]
            get
            {
                return this.pointF_0;
            }
            [CompilerGenerated]
            set
            {
                this.pointF_0 = value;
            }
        }
    }
}

