namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FPDetail
    {
        private string mGFMC;
        public string mGFSH;
        private readonly List<GoodsInfo> mGoodsList = new List<GoodsInfo>();
        private readonly List<GoodsInfo> mQDList = new List<GoodsInfo>();
        private string mw_tmp = string.Empty;

        public string BMBBBH { get; set; }

        public string BZ { get; set; }

        public string CM { get; set; }

        public string FHR { get; set; }

        public string FPDM { get; set; }

        public long FPHM { get; set; }

        public Aisino.Fwkp.Bsgl.FPType FPType { get; set; }

        public string GFDZDH { get; set; }

        public string GFMC
        {
            get
            {
                if (string.IsNullOrEmpty(this.mGFMC))
                {
                    this.mGFMC = " ";
                }
                return this.mGFMC;
            }
            set
            {
                this.mGFMC = value;
            }
        }

        public string GFSH
        {
            get
            {
                if (string.IsNullOrEmpty(this.mGFSH) && ((this.FPType == Aisino.Fwkp.Bsgl.FPType.c) || (this.FPType == Aisino.Fwkp.Bsgl.FPType.s)))
                {
                    this.mGFSH = "000000000000000";
                }
                if (this.YYSBZ.Substring(4, 1).Equals("1"))
                {
                    this.mGFSH = "";
                }
                return this.mGFSH;
            }
            set
            {
                this.mGFSH = value;
            }
        }

        public string GFYHZH { get; set; }

        public List<GoodsInfo> GoodsList
        {
            get
            {
                return this.mGoodsList;
            }
        }

        public int GoodsNum
        {
            get
            {
                return this.mGoodsList.Count;
            }
        }

        public decimal HJJE { get; set; }

        public decimal HJSE { get; set; }

        public string HXM { get; set; }

        public string HYBM { get; set; }

        public string HZTZDH { get; set; }

        public string JQBH { get; set; }

        public string JYM { get; set; }

        public string KHYHMC { get; set; }

        public string KHYHZH { get; set; }

        public string KPR { get; set; }

        public DateTime KPRQ { get; set; }

        public string LSLBS { get; set; }

        public string LZDMHM { get; set; }

        public string MDD { get; set; }

        public string MW
        {
            get
            {
                Fpxx fpxx = new Fpxx {
                    fpdm = this.FPDM,
                    fphm = this.FPHM.ToString("D8"),
                    mw = this.mw_tmp
                };
                DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                fpxx.Get_Print_Dj(null, 2, buffer);
                return fpxx.mw;
            }
            set
            {
                this.mw_tmp = value;
            }
        }

        public bool QDBZ { get; set; }

        public List<GoodsInfo> QDList
        {
            get
            {
                return this.mQDList;
            }
        }

        public string QYD { get; set; }

        public string QYZBM { get; set; }

        public string SCCJMC { get; set; }

        public string SFYH { get; set; }

        public string SIGN { get; set; }

        public string SKR { get; set; }

        public float SLV { get; set; }

        public string SPBM { get; set; }

        public string TYDH { get; set; }

        public string WSPZHM { get; set; }

        public string XFDH { get; set; }

        public string XFDZ { get; set; }

        public string XFDZDH { get; set; }

        public string XFMC { get; set; }

        public string XFSH { get; set; }

        public string XFYHZH { get; set; }

        public string XHD { get; set; }

        public string XSBM { get; set; }

        public string XSDJBH { get; set; }

        public string YSHWXX { get; set; }

        public string YYSBZ { get; set; }

        public string YYZZH { get; set; }

        public bool ZFBZ { get; set; }

        public DateTime ZFRQ { get; set; }

        public string ZHD { get; set; }

        public string ZZSTSGL { get; set; }
    }
}

