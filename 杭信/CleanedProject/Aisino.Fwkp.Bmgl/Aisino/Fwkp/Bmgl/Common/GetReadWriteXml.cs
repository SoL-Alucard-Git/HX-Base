namespace Aisino.Fwkp.Bmgl.Common
{
    using Aisino.Fwkp.Bmgl.Model;
    using System;

    internal class GetReadWriteXml
    {
        private string exportCarPath = string.Empty;
        private string exportCustomerPath = string.Empty;
        private string exportDistrictPath = string.Empty;
        private string exportExpensePath = string.Empty;
        private string exportGoodsPath = string.Empty;
        private string exportGoodsTaxPath = string.Empty;
        private string exportInvoiceTypePath = string.Empty;
        private string exportPurchasePath = string.Empty;
        private string exportRecSenPath = string.Empty;
        private string exportXHDWPath = string.Empty;
        private string importCarPath = string.Empty;
        private string importCustomerPath = string.Empty;
        private string importDistrictPath = string.Empty;
        private string importExpensePath = string.Empty;
        private string importGoodsPath = string.Empty;
        private string importGoodsTaxPath = string.Empty;
        private string importInvoiceTypePath = string.Empty;
        private string importPurchasePath = string.Empty;
        private string importRecSenPath = string.Empty;
        private string importXHDWPath = string.Empty;

        public string GetFileFullName(BMType type)
        {
            switch (type)
            {
                case BMType.BM_KH:
                    return this.ExportCustomerPath;

                case BMType.BM_SP:
                    return this.ExportGoodsPath;

                case BMType.BM_FYXM:
                    return this.ExportExpensePath;

                case BMType.InvoiceType:
                    return this.ExportInvoiceTypePath;

                case BMType.GoodsTax:
                    return this.ExportGoodsTaxPath;

                case BMType.District:
                    return this.ExportDistrictPath;

                case BMType.BM_SFHR:
                    return this.ExportRecSenPath;

                case BMType.BM_GHDW:
                    return this.ExportPurchasePath;

                case BMType.BM_CL:
                    return this.ExportCarPath;

                case BMType.BM_XHDW:
                    return this.ExportXHDWPath;
            }
            return "";
        }

        public string ExportCarPath
        {
            get
            {
                this.exportCarPath = ReadWriteXml.Read("ExportCar");
                if (this.exportCarPath.Length == 0)
                {
                    this.exportCarPath = @"C:\车辆编码.txt";
                }
                return this.exportCarPath;
            }
            set
            {
                ReadWriteXml.Write("ExportCar", value);
                this.exportCarPath = value;
            }
        }

        public string ExportCustomerPath
        {
            get
            {
                this.exportCustomerPath = ReadWriteXml.Read("ExportCustomer");
                if (this.exportCustomerPath.Length == 0)
                {
                    this.exportCustomerPath = @"C:\客户编码.txt";
                }
                return this.exportCustomerPath;
            }
            set
            {
                ReadWriteXml.Write("ExportCustomer", value);
                this.exportCustomerPath = value;
            }
        }

        public string ExportDistrictPath
        {
            get
            {
                this.exportDistrictPath = ReadWriteXml.Read("ExportDistrict");
                if (this.exportDistrictPath.Length == 0)
                {
                    this.exportDistrictPath = @"C:\行政区域编码.txt";
                }
                return this.exportDistrictPath;
            }
            set
            {
                ReadWriteXml.Write("ExportDistrict", value);
                this.exportDistrictPath = value;
            }
        }

        public string ExportExpensePath
        {
            get
            {
                this.exportExpensePath = ReadWriteXml.Read("ExportExpense");
                if (this.exportExpensePath.Length == 0)
                {
                    this.exportExpensePath = @"C:\费用项目编码.txt";
                }
                return this.exportExpensePath;
            }
            set
            {
                ReadWriteXml.Write("ExportExpense", value);
                this.exportExpensePath = value;
            }
        }

        public string ExportGoodsPath
        {
            get
            {
                this.exportGoodsPath = ReadWriteXml.Read("ExportGoods").Trim();
                if (this.exportGoodsPath.Length == 0)
                {
                    this.exportGoodsPath = @"C:\商品编码.txt";
                }
                return this.exportGoodsPath;
            }
            set
            {
                ReadWriteXml.Write("ExportGoods", value);
                this.exportGoodsPath = value;
            }
        }

        public string ExportGoodsTaxPath
        {
            get
            {
                this.exportGoodsTaxPath = ReadWriteXml.Read("ExportGoodsTax");
                if (this.exportGoodsTaxPath.Length == 0)
                {
                    this.exportGoodsTaxPath = @"C:\商品税目编码.txt";
                }
                return this.exportGoodsTaxPath;
            }
            set
            {
                ReadWriteXml.Write("ExportGoodsTax", value);
                this.exportGoodsTaxPath = value;
            }
        }

        public string ExportInvoiceTypePath
        {
            get
            {
                this.exportInvoiceTypePath = ReadWriteXml.Read("ExportInvoiceType");
                if (this.exportInvoiceTypePath.Length == 0)
                {
                    this.exportInvoiceTypePath = @"C:\发票类别编码.txt";
                }
                return this.exportInvoiceTypePath;
            }
            set
            {
                ReadWriteXml.Write("ExportInvoiceType", value);
                this.exportInvoiceTypePath = value;
            }
        }

        public string ExportPurchasePath
        {
            get
            {
                this.exportPurchasePath = ReadWriteXml.Read("ExportPurchase");
                if (this.exportPurchasePath.Length == 0)
                {
                    this.exportPurchasePath = @"C:\购货单位编码.txt";
                }
                return this.exportPurchasePath;
            }
            set
            {
                ReadWriteXml.Write("ExportPurchase", value);
                this.exportPurchasePath = value;
            }
        }

        public string ExportRecSenPath
        {
            get
            {
                this.exportRecSenPath = ReadWriteXml.Read("ExportRecSen");
                if (this.exportRecSenPath.Length == 0)
                {
                    this.exportRecSenPath = @"C:\收发货人编码.txt";
                }
                return this.exportRecSenPath;
            }
            set
            {
                ReadWriteXml.Write("ExportRecSen", value);
                this.exportRecSenPath = value;
            }
        }

        public string ExportXHDWPath
        {
            get
            {
                this.exportXHDWPath = ReadWriteXml.Read("ExportXHDW");
                if (this.exportXHDWPath.Length == 0)
                {
                    this.exportXHDWPath = @"C:\销货单位编码.txt";
                }
                return this.exportXHDWPath;
            }
            set
            {
                ReadWriteXml.Write("ExportXHDW", value);
                this.exportXHDWPath = value;
            }
        }

        public string ImportCarPath
        {
            get
            {
                this.importCarPath = ReadWriteXml.Read("ImportCar");
                if (this.importCarPath.Length == 0)
                {
                    this.importCarPath = @"C:\车辆编码.txt";
                }
                return this.importCarPath;
            }
            set
            {
                ReadWriteXml.Write("ImportCar", value);
                this.importCarPath = value;
            }
        }

        public string ImportCustomerPath
        {
            get
            {
                this.importCustomerPath = ReadWriteXml.Read("ImportCustomer");
                if (this.importCustomerPath.Length == 0)
                {
                    this.importCustomerPath = @"C:\客户编码.txt";
                }
                return this.importCustomerPath;
            }
            set
            {
                ReadWriteXml.Write("ImportCustomer", value);
                this.importCustomerPath = value;
            }
        }

        public string ImportDistrictPath
        {
            get
            {
                this.importDistrictPath = ReadWriteXml.Read("ImportDistrict");
                if (this.importDistrictPath.Length == 0)
                {
                    this.importDistrictPath = @"C:\行政区域编码.txt";
                }
                return this.importDistrictPath;
            }
            set
            {
                ReadWriteXml.Write("ImportDistrict", value);
                this.importDistrictPath = value;
            }
        }

        public string ImportExpensePath
        {
            get
            {
                this.importExpensePath = ReadWriteXml.Read("ImportExpense");
                if (this.importExpensePath.Length == 0)
                {
                    this.importExpensePath = @"C:\费用项目编码.txt";
                }
                return this.importExpensePath;
            }
            set
            {
                ReadWriteXml.Write("ImportExpense", value);
                this.importExpensePath = value;
            }
        }

        public string ImportGoodsPath
        {
            get
            {
                this.importGoodsPath = ReadWriteXml.Read("ImportGoods");
                if (this.importGoodsPath.Length == 0)
                {
                    this.importGoodsPath = @"C:\商品编码.txt";
                }
                return this.importGoodsPath;
            }
            set
            {
                ReadWriteXml.Write("ImportGoods", value);
                this.importGoodsPath = value;
            }
        }

        public string ImportGoodsTaxPath
        {
            get
            {
                this.importGoodsTaxPath = ReadWriteXml.Read("ImportGoodsTax");
                if (this.importGoodsTaxPath.Length == 0)
                {
                    this.importGoodsTaxPath = @"C:\商品税目编码.txt";
                }
                return this.importGoodsTaxPath;
            }
            set
            {
                ReadWriteXml.Write("ImportGoodsTax", value);
                this.importGoodsTaxPath = value;
            }
        }

        public string ImportInvoiceTypePath
        {
            get
            {
                this.importInvoiceTypePath = ReadWriteXml.Read("ImportInvoiceType");
                if (this.importInvoiceTypePath.Length == 0)
                {
                    this.importInvoiceTypePath = @"C:\发票类别编码.txt";
                }
                return this.importInvoiceTypePath;
            }
            set
            {
                ReadWriteXml.Write("ImportInvoiceType", value);
                this.importInvoiceTypePath = value;
            }
        }

        public string ImportPurchasePath
        {
            get
            {
                this.importPurchasePath = ReadWriteXml.Read("ImportPurchase");
                if (this.importPurchasePath.Length == 0)
                {
                    this.importPurchasePath = @"C:\购货单位编码.txt";
                }
                return this.importPurchasePath;
            }
            set
            {
                ReadWriteXml.Write("ImportPurchase", value);
                this.importPurchasePath = value;
            }
        }

        public string ImportRecSenPath
        {
            get
            {
                this.importRecSenPath = ReadWriteXml.Read("ImportRecSen");
                if (this.importRecSenPath.Length == 0)
                {
                    this.importRecSenPath = @"C:\收发货人编码.txt";
                }
                return this.importRecSenPath;
            }
            set
            {
                ReadWriteXml.Write("ImportRecSen", value);
                this.importRecSenPath = value;
            }
        }

        public string ImportXHDWPath
        {
            get
            {
                this.importXHDWPath = ReadWriteXml.Read("ImportXHDW");
                if (this.importXHDWPath.Length == 0)
                {
                    this.importXHDWPath = @"C:\销货单位编码.txt";
                }
                return this.importXHDWPath;
            }
            set
            {
                ReadWriteXml.Write("ImportXHDW", value);
                this.importXHDWPath = value;
            }
        }
    }
}

