namespace Aisino.Fwkp.Wbjk.Model
{
    using System;

    public class ImportErrorDetail
    {
        private bool accept;
        private string djbh;
        private string error;
        private int rowId;

        public ImportErrorDetail(string ErrowInfo, string DJBH, int LineNum, bool Accept)
        {
            this.error = ErrowInfo;
            this.djbh = DJBH;
            this.rowId = LineNum;
            this.accept = Accept;
        }

        public bool Accept
        {
            get
            {
                return this.accept;
            }
        }

        public string DJBH
        {
            get
            {
                return this.djbh;
            }
        }

        public string ErrowInfo
        {
            get
            {
                return this.error;
            }
        }

        public int RowIndex
        {
            get
            {
                return this.rowId;
            }
        }
    }
}

