namespace Aisino.Fwkp.Wbjk.Model
{
    using System;

    public class MXModelLogic : XSDJ_MXModel
    {
        private double _dj;
        private double _djShow;
        private double _je;
        private double _jeShow;
        private double _sl;
        public bool djIsHysy = false;

        public double DJ
        {
            get
            {
                if (base.DJHXZ == 4)
                {
                    this.SL = 0.0;
                    this.DJ = 0.0;
                }
                else if (!this.IsHYSY && base.HSJBZ)
                {
                    if (!(this.SL == 0.0))
                    {
                        this.DJ = this.JE / this.SL;
                    }
                    else
                    {
                        this.DJ = this.JE;
                    }
                }
                return this._dj;
            }
            set
            {
                this._dj = value;
            }
        }

        public double DjShow
        {
            get
            {
                if (!this.IsHYSY && !(this.SL == 0.0))
                {
                    this._djShow = this.DJ + (base.SE / this.SL);
                }
                return this._djShow;
            }
            set
            {
                this._djShow = value;
            }
        }

        public bool IsHYSY
        {
            get
            {
                return (this.djIsHysy && (base.SLV == 0.05));
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

        public double JeShow
        {
            get
            {
                if (!this.IsHYSY)
                {
                    this._jeShow = this.JE + base.SE;
                }
                return this._jeShow;
            }
            set
            {
                this._jeShow = value;
            }
        }

        public double SL
        {
            get
            {
                if (base.DJHXZ == 4)
                {
                    this.SL = 0.0;
                    this.DJ = 0.0;
                }
                return this._sl;
            }
            set
            {
                this._sl = value;
            }
        }
    }
}

