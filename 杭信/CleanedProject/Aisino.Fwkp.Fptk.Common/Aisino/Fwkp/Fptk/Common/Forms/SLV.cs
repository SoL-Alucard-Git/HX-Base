using Aisino.Fwkp.BusinessObject;
using System;

namespace Aisino.Fwkp.Fptk.Common.Forms
{
    public class SLV
    {
        private FPLX fplx_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private ZYFP_LX zyfp_LX_0;

        public SLV(FPLX fplx_1, ZYFP_LX zyfp_LX_1, string string_3, string string_4, string string_5)
        {
            
            this.fplx_0 = fplx_1;
            this.zyfp_LX_0 = zyfp_LX_1;
            this.string_0 = string_3;
            this.string_1 = string_4;
            this.string_2 = string_5;
        }

        public override bool Equals(object value)
        {
            SLV slv = value as SLV;
            if (slv == null)
            {
                return false;
            }
            return this.string_0.Equals(slv.string_0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.string_1;
        }

        public string ComboxValue
        {
            get
            {
                return this.string_1;
            }
        }

        public string DataValue
        {
            get
            {
                return this.string_0;
            }
        }

        public FPLX fplx
        {
            get
            {
                return this.fplx_0;
            }
        }

        public string ShowValue
        {
            get
            {
                return this.string_2;
            }
        }

        public ZYFP_LX Zyfplx
        {
            get
            {
                return this.zyfp_LX_0;
            }
        }
    }
}

