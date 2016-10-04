namespace Aisino.Fwkp.Wbjk.Model
{
    using System;
    using System.Collections.Generic;

    public class XSDJMXModel : XSDJModel
    {
        public List<XSDJ_MXModel> ListXSDJ_MX = new List<XSDJ_MXModel>();
        public string ZBKInfo = "";

        public void SetHead(XSDJModel XSDJ)
        {
            base.BH = XSDJ.BH;
            base.GFMC = XSDJ.GFMC;
            base.GFSH = XSDJ.GFSH;
            base.GFDZDH = XSDJ.GFDZDH;
            base.GFYHZH = XSDJ.GFYHZH;
            base.XSBM = XSDJ.XSBM;
            base.YDXS = XSDJ.YDXS;
            base.JEHJ = XSDJ.JEHJ;
            base.DJRQ = XSDJ.DJRQ;
            base.DJYF = XSDJ.DJYF;
            base.DJZT = XSDJ.DJZT;
            base.KPZT = XSDJ.KPZT;
            base.BZ = XSDJ.BZ;
            base.FHR = XSDJ.FHR;
            base.SKR = XSDJ.SKR;
            base.QDHSPMC = XSDJ.QDHSPMC;
            base.XFYHZH = XSDJ.XFYHZH;
            base.XFDZDH = XSDJ.XFDZDH;
            base.CFHB = XSDJ.CFHB;
            base.DJZL = XSDJ.DJZL;
            base.SFZJY = XSDJ.SFZJY;
            base.HYSY = XSDJ.HYSY;
        }
    }
}

