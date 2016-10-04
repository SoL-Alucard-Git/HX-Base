namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Collections.Generic;

    public class ErrorResolver
    {
        private int abandonCount = 0;
        public int AcceptCount = 0;
        private List<ImportErrorDetail> ErrorsList = new List<ImportErrorDetail>();
        public int ImportTotal = 0;
        public static SaleBill PtSaleBill;
        public int SaveCount = 0;

        public void AddError(string ErrowInfo, string DJBH, int LineNum, bool Accept)
        {
            if ((PtSaleBill != null) && (((DJBH == PtSaleBill.BH) && !Accept) && (PtSaleBill.ReserveA != "Abandon")))
            {
                PtSaleBill.ReserveA = "Abandon";
            }
            ImportErrorDetail item = new ImportErrorDetail(ErrowInfo, DJBH, LineNum, Accept);
            this.ErrorsList.Add(item);
        }

        public List<ImportErrorDetail> GetErrorsList()
        {
            return this.ErrorsList;
        }

        public int AbandonCount
        {
            get
            {
                return this.abandonCount;
            }
            set
            {
                this.abandonCount = value;
            }
        }
    }
}

