namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Goods
    {
        private double _dj = 0.0;
        private int _djhxz;
        private string _flbm = string.Empty;
        private string _flmc = string.Empty;
        private string _fpdm = string.Empty;
        private int _fphm;
        private string _fpzl = string.Empty;
        private string _ggxh = string.Empty;
        private bool _hsjbz;
        private double _je = 0.0;
        private string _jldw = string.Empty;
        private double _kce = 0.0;
        private string _lslvbs = "";
        private int _scfpxh;
        private double _se = 0.0;
        private double _sl = 0.0;
        private double _slv;
        private string _spbm = string.Empty;
        private string _spmc = string.Empty;
        private string _spsm = "";
        private int _xh;
        private string _xsdjbh = string.Empty;
        private bool _xsyh;
        private string _xsyhsm = string.Empty;
        public string Reserve = "";
        public string strDj = "";
        public string strEEEDj = "";
        public string strJe = "";
        public string strSe = "";
        public string strSl = "";

        public static Goods Clone(Goods mx)
        {
            return new Goods { 
                DJ = mx.DJ, DJHXZ = mx.DJHXZ, GGXH = mx.GGXH, HSJBZ = mx.HSJBZ, JE = mx.JE, JLDW = mx.JLDW, SE = SaleBillCtrl.GetRound(mx.SE, 2), SL = mx.SL, SLV = mx.SLV, SPMC = mx.SPMC, SPSM = mx.SPSM, XH = mx.XH, XSDJBH = mx.XSDJBH, Reserve = mx.Reserve, FLBM = mx.FLBM, XSYH = mx.XSYH, 
                FLMC = mx.FLMC, SPBM = mx.SPBM, XSYHSM = mx.XSYHSM, LSLVBS = mx.LSLVBS
             };
        }

        public double getDj(bool Flag_ContainTax)
        {
            double num;
            if (Flag_ContainTax)
            {
                if (this.HSJBZ)
                {
                    return this.DJ;
                }
                num = Finacial.Mul(this.DJ, 1.0 + this.SLV, 15);
                if (Math.Abs((double) (this.SLV - 0.015)) < 1E-05)
                {
                    num = (this.DJ * 70.0) / 69.0;
                }
                if (!(this.KCE == 0.0))
                {
                    num = Finacial.Div(this.JE + this.SE, this.SL, 15);
                }
                return num;
            }
            if (this.HSJBZ)
            {
                num = Finacial.Div(this.DJ, 1.0 + this.SLV, 15);
                if (Math.Abs((double) (this.SLV - 0.015)) < 1E-05)
                {
                    num = Finacial.Div(this.DJ * 69.0, 70.0, 15);
                }
                if (!(this.KCE == 0.0))
                {
                    num = Finacial.Div(this.JE, this.SL, 15);
                }
                return num;
            }
            return this.DJ;
        }

        public double getDjNoTax(bool hysy)
        {
            if (!hysy && this.HSJBZ)
            {
                return Finacial.Div(this.DJ, 1.0 + this.SLV, 15);
            }
            return this.DJ;
        }

        private double popopo(string strvalue, int digits = 0)
        {
            double result = 0.0;
            if (double.TryParse(strvalue, out result))
            {
                if (digits == 0)
                {
                    return result;
                }
                return (result = Finacial.GetRound(result, digits));
            }
            return 0.0;
        }

        public void setDj(double dDj, bool hsjbz)
        {
            if (hsjbz)
            {
                double num = dDj;
                double num2 = num * this.SL;
                this.HSJBZ = true;
                this.DJ = num;
                this.JE = Finacial.Div(num2, 1.0 + this.SLV, 2);
                this.SE = Finacial.GetRound((double) (num2 - this.JE), 2);
            }
            else
            {
                this.HSJBZ = false;
                this.DJ = dDj;
                this.JE = Finacial.GetRound((double) (dDj * this.SL), 2);
                this.SE = Finacial.GetRound((double) (this.JE * this.SLV), 2);
            }
        }

        public void setSl(double dSl, bool hsjbz)
        {
            this.SL = dSl;
            this.JE = Finacial.GetRound((double) (this.getDjNoTax(false) * this.SL), 2);
            this.SE = Finacial.GetRound((double) (this.JE * this.SLV), 2);
        }

        public bool DanJiaError { get; set; }

        public double DJ
        {
            get
            {
                return this._dj;
            }
            set
            {
                this._dj = value;
            }
        }

        public int DJHXZ
        {
            get
            {
                return this._djhxz;
            }
            set
            {
                this._djhxz = value;
            }
        }

        public double DJnoTax
        {
            get
            {
                if (this.HSJBZ)
                {
                    return (this.DJ / (1.0 + this.SLV));
                }
                return this.DJ;
            }
            set
            {
                if (this.HSJBZ)
                {
                    this.DJ = value * (1.0 + this.SLV);
                }
                else
                {
                    this.DJ = value;
                }
            }
        }

        public string FLBM
        {
            get
            {
                return this._flbm;
            }
            set
            {
                this._flbm = value;
            }
        }

        public string FLMC
        {
            get
            {
                return this._flmc;
            }
            set
            {
                this._flmc = value;
            }
        }

        public string FPDM
        {
            get
            {
                return this._fpdm;
            }
            set
            {
                this._fpdm = value;
            }
        }

        public int FPHM
        {
            get
            {
                return this._fphm;
            }
            set
            {
                this._fphm = value;
            }
        }

        public string FPZL
        {
            get
            {
                return this._fpzl;
            }
            set
            {
                this._fpzl = value;
            }
        }

        public string GGXH
        {
            get
            {
                return this._ggxh;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this._ggxh = GetSafeData.GetSafeString(value, 40);
            }
        }

        public bool HSJBZ
        {
            get
            {
                return this._hsjbz;
            }
            set
            {
                this._hsjbz = value;
            }
        }

        public double JE
        {
            get
            {
                return this._je;
            }
            set
            {
                this._je = value;
            }
        }

        public string JLDW
        {
            get
            {
                return this._jldw;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this._jldw = GetSafeData.GetSafeString(value, 0x16);
            }
        }

        public double KCE
        {
            get
            {
                return this._kce;
            }
            set
            {
                this._kce = value;
            }
        }

        public string LSLVBS
        {
            get
            {
                return this._lslvbs;
            }
            set
            {
                this._lslvbs = value;
            }
        }

        public int SCFPXH
        {
            get
            {
                return this._scfpxh;
            }
            set
            {
                this._scfpxh = value;
            }
        }

        public double SE
        {
            get
            {
                return this._se;
            }
            set
            {
                this._se = value;
            }
        }

        public double SL
        {
            get
            {
                return this._sl;
            }
            set
            {
                this._sl = TaxCardValue.GetRound(value);
            }
        }

        public double SLV
        {
            get
            {
                return this._slv;
            }
            set
            {
                this._slv = value;
            }
        }

        public string SPBM
        {
            get
            {
                return this._spbm;
            }
            set
            {
                this._spbm = value;
            }
        }

        public string SPMC
        {
            get
            {
                return this._spmc;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this._spmc = GetSafeData.GetSafeString(value, 0x5c);
            }
        }

        public string SPSM
        {
            get
            {
                return this._spsm;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                this._spsm = GetSafeData.GetSafeString(value, 4);
            }
        }

        public int XH
        {
            get
            {
                return this._xh;
            }
            set
            {
                this._xh = value;
            }
        }

        public string XSDJBH
        {
            get
            {
                return this._xsdjbh;
            }
            set
            {
                this._xsdjbh = GetSafeData.GetSafeString(value, 50);
            }
        }

        public bool XSYH
        {
            get
            {
                return this._xsyh;
            }
            set
            {
                this._xsyh = value;
            }
        }

        public string XSYHSM
        {
            get
            {
                return this._xsyhsm;
            }
            set
            {
                this._xsyhsm = value;
            }
        }

        public double ZKJE { get; set; }

        public double ZKL { get; set; }

        public double ZKSE { get; set; }
    }
}

