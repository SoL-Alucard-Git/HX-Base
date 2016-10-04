namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Collections.Generic;

    public class XXFPPaper : XXFPModel
    {
        private bool _mulslv = false;
        private List<XXFP_MXModel> listInvWare = new List<XXFP_MXModel>();

        public List<XXFP_MXModel> ListInvWare
        {
            get
            {
                return this.listInvWare;
            }
            set
            {
                this.listInvWare = value;
            }
        }

        public bool MULSLV
        {
            get
            {
                return this._mulslv;
            }
            set
            {
                this._mulslv = value;
            }
        }
    }
}

