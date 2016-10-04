namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Collections.Generic;

    public class DJZFImportResult
    {
        private Dictionary<string, string> result = new Dictionary<string, string>();
        private int success;
        private int undo;

        public Dictionary<string, string> Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        public int Success
        {
            get
            {
                return this.success;
            }
            set
            {
                this.success = value;
            }
        }

        public int Undo
        {
            get
            {
                return this.undo;
            }
            set
            {
                this.undo = value;
            }
        }
    }
}

