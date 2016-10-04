namespace Aisino.Framework.Plugin.Core.ExcelGrid
{
    using System;

    public class ExportEndedEventArgs : EventArgs
    {
        private bool bool_0;
        private Exception exception_0;

        public ExportEndedEventArgs(bool bool_1, Exception exception_1)
        {
            
            this.bool_0 = bool_1;
            if (exception_1 != null)
            {
                this.exception_0 = exception_1;
            }
        }

        public Exception Errors
        {
            get
            {
                return this.exception_0;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return this.bool_0;
            }
        }
    }
}

