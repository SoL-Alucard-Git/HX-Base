namespace Aisino.FTaxBase
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [DefaultMember("Item")]
    public class TaxStateInfo
    {
        public ushort BigAmountInvCount;
        public string CompanyAuth;
        public ushort CompanyType;
        public string DriverVersion;
        public ushort ICBuyInv;
        public ushort ICCardNo;
        public ushort ICRetInv;
        public long InvLimit;
        public List<Aisino.FTaxBase.InvTypeInfo> InvTypeInfo;
        public ushort IsInvEmpty;
        public ushort IsLockReached;
        public ushort IsMainMachine;
        public ushort IsRepReached;
        public ushort IsWithChild;
        public ushort LockedDays;
        public ushort MachineNumber;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public string TaxCode;
        public ushort TBBuyInv;
        public ushort TBCardNo;
        public ushort TBRetInv;
        public ushort TBType;
        public ushort TutorialFlag;
        public ushort ushort_0;
        public ushort ushort_1;
        public ushort ushort_10;
        public ushort ushort_2;
        public ushort ushort_3;
        public ushort ushort_4;
        public ushort ushort_5;
        public ushort ushort_6;
        public ushort ushort_7;
        public ushort ushort_8;
        public ushort ushort_9;

        public TaxStateInfo()
        {
            
            this.CompanyAuth = "0000000000";
            this.DriverVersion = "";
            this.InvTypeInfo = new List<Aisino.FTaxBase.InvTypeInfo>();
        }

        internal Aisino.FTaxBase.InvTypeInfo method_0(InvoiceType invoiceType_0)
        {
            Aisino.FTaxBase.InvTypeInfo info = new Aisino.FTaxBase.InvTypeInfo();
            using (List<Aisino.FTaxBase.InvTypeInfo>.Enumerator enumerator = this.InvTypeInfo.GetEnumerator())
            {
                Aisino.FTaxBase.InvTypeInfo current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    int num2 = (int) invoiceType_0;
                    if (current.InvType.ToString() == num2.ToString())
                    {
                        goto Label_004A;
                    }
                }
                return info;
            Label_004A:
                info = current;
            }
            return info;
        }

        public string Version
        {
            get
            {
                return string.Format("{0}.{1}", this.MajorVersion, this.MinorVersion.ToString("00"));
            }
        }
    }
}

