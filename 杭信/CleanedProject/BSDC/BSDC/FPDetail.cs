namespace BSDC
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FPDetail
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        [CompilerGenerated]
        private DateTime dateTime_0;
        [CompilerGenerated]
        private DateTime dateTime_1;
        [CompilerGenerated]
        private decimal decimal_0;
        [CompilerGenerated]
        private decimal decimal_1;
        [CompilerGenerated]
        private float float_0;
        [CompilerGenerated]
        private BSDC.FPType fptype_0;
        private ILog ilog_0;
        private readonly List<GoodsInfo> list_0;
        private readonly List<GoodsInfo> list_1;
        [CompilerGenerated]
        private long long_0;
        public string mGFSH;
        private string string_0;
        private string string_1;
        [CompilerGenerated]
        private string string_10;
        [CompilerGenerated]
        private string string_11;
        [CompilerGenerated]
        private string string_12;
        [CompilerGenerated]
        private string string_13;
        [CompilerGenerated]
        private string string_14;
        [CompilerGenerated]
        private string string_15;
        [CompilerGenerated]
        private string string_16;
        [CompilerGenerated]
        private string string_17;
        [CompilerGenerated]
        private string string_18;
        [CompilerGenerated]
        private string string_19;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_20;
        [CompilerGenerated]
        private string string_21;
        [CompilerGenerated]
        private string string_22;
        [CompilerGenerated]
        private string string_23;
        [CompilerGenerated]
        private string string_24;
        [CompilerGenerated]
        private string string_25;
        [CompilerGenerated]
        private string string_26;
        [CompilerGenerated]
        private string string_27;
        [CompilerGenerated]
        private string string_28;
        [CompilerGenerated]
        private string string_29;
        [CompilerGenerated]
        private string string_3;
        [CompilerGenerated]
        private string string_30;
        [CompilerGenerated]
        private string string_31;
        [CompilerGenerated]
        private string string_32;
        [CompilerGenerated]
        private string string_33;
        [CompilerGenerated]
        private string string_34;
        [CompilerGenerated]
        private string string_35;
        [CompilerGenerated]
        private string string_36;
        [CompilerGenerated]
        private string string_37;
        [CompilerGenerated]
        private string string_38;
        [CompilerGenerated]
        private string string_39;
        [CompilerGenerated]
        private string string_4;
        [CompilerGenerated]
        private string string_40;
        [CompilerGenerated]
        private string string_41;
        [CompilerGenerated]
        private string string_42;
        [CompilerGenerated]
        private string string_5;
        [CompilerGenerated]
        private string string_6;
        [CompilerGenerated]
        private string string_7;
        [CompilerGenerated]
        private string string_8;
        [CompilerGenerated]
        private string string_9;

        public FPDetail()
        {
            
            this.ilog_0 = LogUtil.GetLogger<FPDetail>();
            this.string_1 = string.Empty;
            this.list_0 = new List<GoodsInfo>();
            this.list_1 = new List<GoodsInfo>();
        }

        public string BMBBBH
        {
            [CompilerGenerated]
            get
            {
                return this.string_37;
            }
            [CompilerGenerated]
            set
            {
                this.string_37 = value;
            }
        }

        public string BZ
        {
            [CompilerGenerated]
            get
            {
                return this.string_11;
            }
            [CompilerGenerated]
            set
            {
                this.string_11 = value;
            }
        }

        public string CM
        {
            [CompilerGenerated]
            get
            {
                return this.string_12;
            }
            [CompilerGenerated]
            set
            {
                this.string_12 = value;
            }
        }

        public string FHR
        {
            [CompilerGenerated]
            get
            {
                return this.string_27;
            }
            [CompilerGenerated]
            set
            {
                this.string_27 = value;
            }
        }

        public string FPDM
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public long FPHM
        {
            [CompilerGenerated]
            get
            {
                return this.long_0;
            }
            [CompilerGenerated]
            set
            {
                this.long_0 = value;
            }
        }

        public BSDC.FPType FPType
        {
            [CompilerGenerated]
            get
            {
                return this.fptype_0;
            }
            [CompilerGenerated]
            set
            {
                this.fptype_0 = value;
            }
        }

        public string GFDZDH
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public string GFMC
        {
            get
            {
                if (string.IsNullOrEmpty(this.string_0))
                {
                    this.string_0 = " ";
                }
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string GFSH
        {
            get
            {
                if (string.IsNullOrEmpty(this.mGFSH) && ((this.FPType == BSDC.FPType.c) || (this.FPType == BSDC.FPType.s)))
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

        public string GFYHZH
        {
            [CompilerGenerated]
            get
            {
                return this.string_4;
            }
            [CompilerGenerated]
            set
            {
                this.string_4 = value;
            }
        }

        public List<GoodsInfo> GoodsList
        {
            get
            {
                return this.list_0;
            }
        }

        public int GoodsNum
        {
            get
            {
                return this.list_0.Count;
            }
        }

        public decimal HJJE
        {
            [CompilerGenerated]
            get
            {
                return this.decimal_0;
            }
            [CompilerGenerated]
            set
            {
                this.decimal_0 = value;
            }
        }

        public decimal HJSE
        {
            [CompilerGenerated]
            get
            {
                return this.decimal_1;
            }
            [CompilerGenerated]
            set
            {
                this.decimal_1 = value;
            }
        }

        public string HXM
        {
            [CompilerGenerated]
            get
            {
                return this.string_10;
            }
            [CompilerGenerated]
            set
            {
                this.string_10 = value;
            }
        }

        public string HYBM
        {
            [CompilerGenerated]
            get
            {
                return this.string_25;
            }
            [CompilerGenerated]
            set
            {
                this.string_25 = value;
            }
        }

        public string HZTZDH
        {
            [CompilerGenerated]
            get
            {
                return this.string_22;
            }
            [CompilerGenerated]
            set
            {
                this.string_22 = value;
            }
        }

        public string JQBH
        {
            [CompilerGenerated]
            get
            {
                return this.string_20;
            }
            [CompilerGenerated]
            set
            {
                this.string_20 = value;
            }
        }

        public string JYM
        {
            [CompilerGenerated]
            get
            {
                return this.string_32;
            }
            [CompilerGenerated]
            set
            {
                this.string_32 = value;
            }
        }

        public string KHYHMC
        {
            [CompilerGenerated]
            get
            {
                return this.string_16;
            }
            [CompilerGenerated]
            set
            {
                this.string_16 = value;
            }
        }

        public string KHYHZH
        {
            [CompilerGenerated]
            get
            {
                return this.string_31;
            }
            [CompilerGenerated]
            set
            {
                this.string_31 = value;
            }
        }

        public string KPR
        {
            [CompilerGenerated]
            get
            {
                return this.string_9;
            }
            [CompilerGenerated]
            set
            {
                this.string_9 = value;
            }
        }

        public DateTime KPRQ
        {
            [CompilerGenerated]
            get
            {
                return this.dateTime_0;
            }
            [CompilerGenerated]
            set
            {
                this.dateTime_0 = value;
            }
        }

        public string LSLBS
        {
            [CompilerGenerated]
            get
            {
                return this.string_41;
            }
            [CompilerGenerated]
            set
            {
                this.string_41 = value;
            }
        }

        public string LZDMHM
        {
            [CompilerGenerated]
            get
            {
                return this.string_23;
            }
            [CompilerGenerated]
            set
            {
                this.string_23 = value;
            }
        }

        public string MDD
        {
            [CompilerGenerated]
            get
            {
                return this.string_29;
            }
            [CompilerGenerated]
            set
            {
                this.string_29 = value;
            }
        }

        public string MW
        {
            get
            {
                byte[] buffer = new byte[] { 
                    0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                    0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 };
                byte[] buffer2 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                byte[] buffer3 = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                try
                {
                    return ToolUtil.GetString(AES_Crypt.Decrypt(Convert.FromBase64String(this.string_1), buffer, buffer2, buffer3));
                }
                catch
                {
                    this.ilog_0.Debug("本张发票的MW不是base64密文，直接使用MW的明文数据");
                    return this.string_1;
                }
            }
            set
            {
                this.string_1 = value;
            }
        }

        public bool QDBZ
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public List<GoodsInfo> QDList
        {
            get
            {
                return this.list_1;
            }
        }

        public string QYD
        {
            [CompilerGenerated]
            get
            {
                return this.string_17;
            }
            [CompilerGenerated]
            set
            {
                this.string_17 = value;
            }
        }

        public string QYZBM
        {
            [CompilerGenerated]
            get
            {
                return this.string_39;
            }
            [CompilerGenerated]
            set
            {
                this.string_39 = value;
            }
        }

        public string SCCJMC
        {
            [CompilerGenerated]
            get
            {
                return this.string_14;
            }
            [CompilerGenerated]
            set
            {
                this.string_14 = value;
            }
        }

        public string SFYH
        {
            [CompilerGenerated]
            get
            {
                return this.string_40;
            }
            [CompilerGenerated]
            set
            {
                this.string_40 = value;
            }
        }

        public string SIGN
        {
            [CompilerGenerated]
            get
            {
                return this.string_33;
            }
            [CompilerGenerated]
            set
            {
                this.string_33 = value;
            }
        }

        public string SKR
        {
            [CompilerGenerated]
            get
            {
                return this.string_26;
            }
            [CompilerGenerated]
            set
            {
                this.string_26 = value;
            }
        }

        public float SLV
        {
            [CompilerGenerated]
            get
            {
                return this.float_0;
            }
            [CompilerGenerated]
            set
            {
                this.float_0 = value;
            }
        }

        public string SPBM
        {
            [CompilerGenerated]
            get
            {
                return this.string_38;
            }
            [CompilerGenerated]
            set
            {
                this.string_38 = value;
            }
        }

        public string TYDH
        {
            [CompilerGenerated]
            get
            {
                return this.string_13;
            }
            [CompilerGenerated]
            set
            {
                this.string_13 = value;
            }
        }

        public string WSPZHM
        {
            [CompilerGenerated]
            get
            {
                return this.string_34;
            }
            [CompilerGenerated]
            set
            {
                this.string_34 = value;
            }
        }

        public string XFDH
        {
            [CompilerGenerated]
            get
            {
                return this.string_30;
            }
            [CompilerGenerated]
            set
            {
                this.string_30 = value;
            }
        }

        public string XFDZ
        {
            [CompilerGenerated]
            get
            {
                return this.string_15;
            }
            [CompilerGenerated]
            set
            {
                this.string_15 = value;
            }
        }

        public string XFDZDH
        {
            [CompilerGenerated]
            get
            {
                return this.string_7;
            }
            [CompilerGenerated]
            set
            {
                this.string_7 = value;
            }
        }

        public string XFMC
        {
            [CompilerGenerated]
            get
            {
                return this.string_6;
            }
            [CompilerGenerated]
            set
            {
                this.string_6 = value;
            }
        }

        public string XFSH
        {
            [CompilerGenerated]
            get
            {
                return this.string_5;
            }
            [CompilerGenerated]
            set
            {
                this.string_5 = value;
            }
        }

        public string XFYHZH
        {
            [CompilerGenerated]
            get
            {
                return this.string_8;
            }
            [CompilerGenerated]
            set
            {
                this.string_8 = value;
            }
        }

        public string XHD
        {
            [CompilerGenerated]
            get
            {
                return this.string_19;
            }
            [CompilerGenerated]
            set
            {
                this.string_19 = value;
            }
        }

        public string XSBM
        {
            [CompilerGenerated]
            get
            {
                return this.string_28;
            }
            [CompilerGenerated]
            set
            {
                this.string_28 = value;
            }
        }

        public string XSDJBH
        {
            [CompilerGenerated]
            get
            {
                return this.string_35;
            }
            [CompilerGenerated]
            set
            {
                this.string_35 = value;
            }
        }

        public string YSHWXX
        {
            [CompilerGenerated]
            get
            {
                return this.string_24;
            }
            [CompilerGenerated]
            set
            {
                this.string_24 = value;
            }
        }

        public string YYSBZ
        {
            [CompilerGenerated]
            get
            {
                return this.string_36;
            }
            [CompilerGenerated]
            set
            {
                this.string_36 = value;
            }
        }

        public string YYZZH
        {
            [CompilerGenerated]
            get
            {
                return this.string_21;
            }
            [CompilerGenerated]
            set
            {
                this.string_21 = value;
            }
        }

        public bool ZFBZ
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public DateTime ZFRQ
        {
            [CompilerGenerated]
            get
            {
                return this.dateTime_1;
            }
            [CompilerGenerated]
            set
            {
                this.dateTime_1 = value;
            }
        }

        public string ZHD
        {
            [CompilerGenerated]
            get
            {
                return this.string_18;
            }
            [CompilerGenerated]
            set
            {
                this.string_18 = value;
            }
        }

        public string ZZSTSGL
        {
            [CompilerGenerated]
            get
            {
                return this.string_42;
            }
            [CompilerGenerated]
            set
            {
                this.string_42 = value;
            }
        }
    }
}

