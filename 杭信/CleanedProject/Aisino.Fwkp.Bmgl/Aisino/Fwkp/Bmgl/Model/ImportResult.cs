namespace Aisino.Fwkp.Bmgl.Model
{
    using System;
    using System.Data;

    public class ImportResult
    {
        private int _correct;
        private int _duplicated;
        private int _failed;
        private string _importTable;
        private int _invalid;
        private DataTable dtResult;

        public int Correct
        {
            get
            {
                return this._correct;
            }
            set
            {
                this._correct = value;
            }
        }

        public DataTable DtResult
        {
            get
            {
                if (this.dtResult == null)
                {
                    this.dtResult = new DataTable();
                    this.dtResult.Columns.Add("Code");
                    this.dtResult.Columns.Add("Name");
                    this.dtResult.Columns.Add("Result");
                    this.dtResult.Columns.Add("Reason");
                }
                return this.dtResult;
            }
            set
            {
                this.dtResult = value;
            }
        }

        public int Duplicated
        {
            get
            {
                return this._duplicated;
            }
            set
            {
                this._duplicated = value;
            }
        }

        public int Failed
        {
            get
            {
                return this._failed;
            }
            set
            {
                this._failed = value;
            }
        }

        public string ImportTable
        {
            get
            {
                return this._importTable;
            }
            set
            {
                this._importTable = value;
            }
        }

        public int Invalid
        {
            get
            {
                return this._invalid;
            }
            set
            {
                this._invalid = value;
            }
        }

        public int Total
        {
            get
            {
                if (this.dtResult != null)
                {
                    return this.dtResult.Rows.Count;
                }
                return 0;
            }
        }
    }
}

