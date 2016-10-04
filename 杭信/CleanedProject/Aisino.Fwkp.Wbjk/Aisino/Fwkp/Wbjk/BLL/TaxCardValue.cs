namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;

    public class TaxCardValue
    {
        protected int Card_Type;
        protected double DataPrecision;
        protected double InvLimit;
        private static double InvLimitCommon = -1.0;
        private static double InvLimitSpecial = -1.0;
        private static double InvLimitTransportation = -1.0;
        private static double InvLimitVehiclesales = -1.0;
        protected List<double> slvList;
        public static double SoftAAmountPre = 0.625;
        protected double SoftAmountLimit;
        public static double SoftATaxPre = 1.27;
        public static double SoftTaxLimit;
        protected double SoftTaxPre;
        protected double SoftTaxRate;
        public static TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        public const double ValueSmallest = 1E-15;

        public TaxCardValue()
        {
            int num;
            this.slvList = new List<double>();
            this.DataPrecision = 1E-08;
            this.Card_Type = 0;
            this.SoftTaxPre = 0.06;
            this.SoftAmountLimit = 0.0;
            this.SoftTaxRate = 0.0;
            for (num = 0; num < taxCard.get_TaxRateAuthorize().TaxRateNoTax.Count; num++)
            {
                this.slvList.Add(taxCard.get_TaxRateAuthorize().TaxRateNoTax[num]);
            }
            for (num = 0; num < taxCard.get_TaxRateAuthorize().TaxRateTax.Count; num++)
            {
                this.slvList.Add(taxCard.get_TaxRateAuthorize().TaxRateTax[num]);
            }
            if (taxCard.get_ECardType() != 0)
            {
                SoftTaxLimit = 42949672.95;
                this.SoftAmountLimit = 10995116277.75;
                this.SoftTaxPre = 0.06;
                this.SoftTaxRate = 25.5;
                SoftATaxPre = 1.27;
                this.Card_Type = 1;
            }
            else
            {
                SoftTaxLimit = 17000000.0;
                this.SoftAmountLimit = 100000000.0;
                this.SoftTaxPre = 0.06;
                this.SoftTaxRate = 32.0;
                SoftAAmountPre = 0.625;
                this.Card_Type = 0;
            }
        }

        public static double GetInvLimit(InvType FPType)
        {
            if (InvLimitCommon == -1.0)
            {
                InvoiceType type = (InvoiceType) FPType;
                int count = taxCard.get_SQInfo().PZSQType.Count;
                for (int i = 0; i < count; i++)
                {
                    if (taxCard.get_SQInfo().PZSQType[i].invType == 2)
                    {
                        InvLimitCommon = taxCard.get_SQInfo().PZSQType[i].InvAmountLimit;
                    }
                    if (taxCard.get_SQInfo().PZSQType[i].invType == 0)
                    {
                        InvLimitSpecial = taxCard.get_SQInfo().PZSQType[i].InvAmountLimit;
                    }
                    if (taxCard.get_SQInfo().PZSQType[i].invType == 11)
                    {
                        InvLimitTransportation = taxCard.get_SQInfo().PZSQType[i].InvAmountLimit;
                    }
                    if (taxCard.get_SQInfo().PZSQType[i].invType == 12)
                    {
                        InvLimitVehiclesales = taxCard.get_SQInfo().PZSQType[i].InvAmountLimit;
                    }
                }
                if (FPType == InvType.Common)
                {
                    return InvLimitCommon;
                }
                if (FPType == InvType.Special)
                {
                    return InvLimitSpecial;
                }
                if (FPType == InvType.transportation)
                {
                    return InvLimitTransportation;
                }
                if (FPType == InvType.vehiclesales)
                {
                    return InvLimitVehiclesales;
                }
                return 0.0;
            }
            if (FPType == InvType.Common)
            {
                return InvLimitCommon;
            }
            if (FPType == InvType.Special)
            {
                return InvLimitSpecial;
            }
            if (FPType == InvType.transportation)
            {
                return InvLimitTransportation;
            }
            if (FPType == InvType.vehiclesales)
            {
                return InvLimitVehiclesales;
            }
            return 0.0;
        }

        public static double GetInvLimit(string DJType)
        {
            return GetInvLimit(CommonTool.GetInvType(DJType));
        }

        public static double GetRound(double value)
        {
            if (value == 0.0)
            {
                return value;
            }
            value = (Math.Abs(value) < 1E-15) ? 1E-15 : value;
            return SaleBillCtrl.GetRound(value, 15);
        }

        public bool IsXTCorpAgent
        {
            get
            {
                return ((taxCard.get_CorpAgent().Substring(0, 10) != "02C2511011") && (taxCard.get_CorpAgent().Substring(9, 1) == "1"));
            }
        }
    }
}

