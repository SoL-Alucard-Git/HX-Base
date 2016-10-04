namespace Aisino.Fwkp.BusinessObject
{
    using System;
    using System.Text;
    using ns3;
    public enum SPXX
    {
        SPMC,
        SPBH,
        SPSM,
        GGXH,
        JLDW,
        DJ,
        SL,
        JE,
        SLV,
        SE,
        FPHXZ,
        HSJBZ,
        BZ,
        XH,
        XTHASH,
        BK2,
        BK3,
        BK4,
        BK5,
        BHSDJ,
        FLBM,
        XSYH,
        YHSM,
        LSLVBS,
        KCE
    }

    public class Spxx
    {
        // Fields
        private bool bool_0;
        private bool bool_1;
        private FPHXZ fphxz_0;
        internal string[] string_0;
        private string string_1;
        private string string_10;
        private string string_11;
        private string string_12;
        private string string_13;
        private string string_14;
        private string string_15;
        private string string_16;
        private string string_17;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private string string_8;
        private string string_9;
        private ZYFP_LX zyfp_LX_0;

        // Methods
        public Spxx(string string_18, string string_19, string string_20) : this(string_18, string_19, string_20, "", "", "", false, ZYFP_LX.ZYFP)
        {
        }

        public Spxx(string string_18, string string_19, string string_20, string string_21, string string_22, string string_23, bool bool_2, ZYFP_LX zyfp_LX_1)
        {
            this.string_0 = new string[0];
            this.string_2 = "";
            this.string_3 = "";
            this.string_4 = "";
            this.string_5 = "";
            this.string_6 = "";
            this.string_7 = "";
            this.string_8 = "";
            this.string_9 = "";
            this.string_10 = "";
            this.string_11 = "";
            this.string_12 = "";
            this.string_13 = "";
            this.string_14 = "";
            this.string_15 = "0";
            this.string_16 = "";
            this.string_17 = "";
            this.Spmc = string_18;
            this.Spsm = string_19;
            this.string_11 = ((string_20 == null) || (string_20.Length <= 0)) ? "" : Class34.smethod_20(string_20, Struct40.int_55);
            this.zyfp_LX_0 = zyfp_LX_1;
            this.Ggxh = string_21;
            this.JLdw = string_22;
            this.bool_0 = bool_2;
            this.string_9 = string_23;
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                this.bool_0 = true;
                if ((!bool_2 && (string_23.Length > 0)) && (string_20.Length > 0))
                {
                    this.string_9 = Class34.smethod_12(string_23, Class34.smethod_17("1.00", string_20), Struct40.int_46);
                    if (Class34.smethod_24(this.string_9, Struct40.int_47, false) == null)
                    {
                        this.string_9 = string.Empty;
                    }
                }
            }
        }

        internal string method_0()
        {
            return this.string_1;
        }

        internal bool method_1()
        {
            return this.bool_0;
        }

        internal string method_10()
        {
            if (this.bool_0)
            {
                return this.string_9;
            }
            if (this.string_9.Length <= 0)
            {
                return "";
            }
            if ((this.zyfp_LX_0 != ZYFP_LX.CEZS) && (this.zyfp_LX_0 != ZYFP_LX.JZ_50_15))
            {
                return Class34.smethod_11(this.string_9, Class34.smethod_17(this.string_11, "1.00"), Struct40.int_46);
            }
            return Class34.smethod_20(Class34.smethod_4(this.zyfp_LX_0, this.string_9, Struct40.int_46, null, this), Struct40.int_46);
        }

        internal string method_11()
        {
            if ((this.string_10.Length + this.string_12.Length) == 0)
            {
                return string.Empty;
            }
            if ((this.string_10.Length > 0) && (this.string_12.Length > 0))
            {
                return Class34.smethod_17(this.string_10, this.string_12);
            }
            return this.string_10;
        }

        internal ZYFP_LX method_12()
        {
            return this.zyfp_LX_0;
        }

        internal void method_13(ZYFP_LX zyfp_LX_1)
        {
            this.zyfp_LX_0 = zyfp_LX_1;
        }

        private bool method_14()
        {
            decimal num;
            if (!decimal.TryParse(this.string_10, out num))
            {
                this.string_1 = "A115";
                return false;
            }
            if ((this.string_11.Length > 0) && !decimal.TryParse(this.string_11, out num))
            {
                this.string_1 = "A116";
                return false;
            }
            if (!decimal.TryParse(this.string_12, out num))
            {
                this.string_1 = "A117";
                return false;
            }
            string str2 = this.string_10;
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                str2 = Class34.smethod_17(this.string_10, this.string_12);
            }
            else if (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                str2 = Class34.smethod_15(Class34.smethod_17(this.string_10, this.string_12), "1.05", Struct40.int_50);
            }
            else if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                str2 = Class34.smethod_18(this.string_10, this.string_2);
            }
            string str = Struct40.string_3;
            if (this.bool_1)
            {
                str = Struct40.string_4;
            }
            if ((Class34.smethod_9(str, Struct40.string_0, true) && (this.string_11.Length > 0)) && !Class34.smethod_7(str2, this.string_11, this.string_12, Struct40.int_50, str))
            {
                this.string_1 = "A108";
                this.string_0 = new string[] { str };
                return false;
            }
            if (((this.string_11.Length > 0) && (this.string_12.Length > 0)) && (Class34.smethod_10(this.string_11, Struct40.string_0) && !Class34.smethod_10(this.string_12, Struct40.string_0)))
            {
                this.string_1 = "A132";
                return false;
            }
            this.string_1 = "0000";
            return true;
        }

        private bool method_15()
        {
            decimal num;
            if (!decimal.TryParse(this.string_8, out num))
            {
                this.string_1 = "A118";
                return false;
            }
            if (!decimal.TryParse(this.string_9, out num))
            {
                this.string_1 = "A114";
                return false;
            }
            if (!decimal.TryParse(this.string_10, out num))
            {
                this.string_1 = "A115";
                return false;
            }
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                if (!Class34.smethod_7(this.method_10(), this.string_8, Class34.smethod_17(this.string_10, this.string_12), 2, Struct40.string_2))
                {
                    this.string_1 = "A106";
                    return false;
                }
            }
            else if (!Class34.smethod_7(this.bool_0 ? this.method_10() : this.method_9(), this.string_8, this.bool_0 ? this.method_11() : this.string_10, 2, Struct40.string_2))
            {
                this.string_1 = "A106";
                return false;
            }
            return true;
        }

        internal void method_2(bool bool_2)
        {
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                this.string_1 = "A020";
                this.bool_0 = true;
            }
            else
            {
                this.string_1 = "0000";
                this.bool_0 = bool_2;
            }
        }

        internal bool method_3(bool bool_2)
        {
            if (!this.bool_1 && (this.string_11.Length == 0))
            {
                this.string_1 = "A102";
                return false;
            }
            if ((this.string_9.Length > 0) && !Class34.smethod_9(this.string_9, Struct40.string_0, true))
            {
                this.string_1 = "A112";
                return false;
            }
            if (this.string_10.Length > 0)
            {
                if (this.string_12.Length == 0)
                {
                    this.string_1 = "A107";
                    return false;
                }
                if (!this.method_14())
                {
                    return false;
                }
                if ((!this.bool_1 && (this.fphxz_0 != FPHXZ.ZKXX)) && ((this.fphxz_0 != FPHXZ.XHQDZK) && !Class34.smethod_9(this.string_10, Struct40.string_0, true)))
                {
                    this.string_1 = "A105";
                    return false;
                }
                if ((this.string_8.Length > 0) && !this.method_15())
                {
                    return false;
                }
            }
            else if (this.string_12.Length > 0)
            {
                this.string_1 = "A109";
                return false;
            }
            return true;
        }

        internal bool method_4(bool bool_2)
        {
            if ((this.fphxz_0 != FPHXZ.XHQDZK) && (this.fphxz_0 != FPHXZ.XJXHQD))
            {
                if (((this.fphxz_0 != FPHXZ.SPXX) && (this.fphxz_0 != FPHXZ.SPXX_ZK)) || (((this.zyfp_LX_0 != ZYFP_LX.SNY) && (this.zyfp_LX_0 != ZYFP_LX.SNY_DDZG)) && ((this.zyfp_LX_0 != ZYFP_LX.RLY) && (this.zyfp_LX_0 != ZYFP_LX.RLY_DDZG))))
                {
                    if (this.string_3.Length == 0)
                    {
                        this.string_1 = "A101";
                        return false;
                    }
                }
                else if (this.string_3.Replace(Struct40.dictionary_0[this.zyfp_LX_0], "").Length == 0)
                {
                    this.string_1 = "A101";
                    return false;
                }
                if ((this.fphxz_0 != FPHXZ.XJDYZSFPQD) && (this.string_11.Length == 0))
                {
                    this.string_1 = "A102";
                    return false;
                }
                if ((this.string_8.Length == 0) ^ (this.string_9.Length == 0))
                {
                    this.string_1 = "A103";
                    return false;
                }
                if (this.string_10.Length == 0)
                {
                    this.string_1 = "A110";
                    return false;
                }
                if (this.string_12.Length == 0)
                {
                    this.string_1 = "A111";
                    return false;
                }
                if ((!this.bool_1 && (this.fphxz_0 != FPHXZ.ZKXX)) && ((this.fphxz_0 != FPHXZ.XHQDZK) && !Class34.smethod_9(this.string_10, Struct40.string_0, true)))
                {
                    this.string_1 = "A105";
                    return false;
                }
                if (this.string_8.Length > 0)
                {
                    if (Class34.smethod_24(this.string_8, Struct40.int_49, false) == null)
                    {
                        this.string_1 = "A118";
                        return false;
                    }
                    if (Class34.smethod_24(this.string_9, Struct40.int_47, false) == null)
                    {
                        this.string_1 = "A114";
                        return false;
                    }
                    if (!Class34.smethod_9(this.string_9, Struct40.string_0, true))
                    {
                        this.string_1 = "A112";
                        return false;
                    }
                    if (!this.method_15())
                    {
                        return false;
                    }
                }
                return this.method_14();
            }
            this.string_1 = "0000";
            return true;
        }

        internal FPHXZ method_5()
        {
            return this.fphxz_0;
        }

        internal void method_6(FPHXZ fphxz_1)
        {
            this.fphxz_0 = fphxz_1;
        }

        internal bool method_7()
        {
            return this.bool_1;
        }

        internal void method_8(bool bool_2)
        {
            this.bool_1 = bool_2;
        }

        internal string method_9()
        {
            if (!this.bool_0)
            {
                return this.string_9;
            }
            if (this.string_9.Length <= 0)
            {
                return "";
            }
            if ((this.zyfp_LX_0 != ZYFP_LX.CEZS) && (this.zyfp_LX_0 != ZYFP_LX.JZ_50_15))
            {
                return Class34.smethod_14(this.string_9, Class34.smethod_17(this.string_11, "1.00"), Struct40.int_46);
            }
            return Class34.smethod_3(this.zyfp_LX_0, this.string_9, Struct40.int_46, null, this);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("【商品名称:").Append(this.string_3).Append(",商品税目:").Append(this.string_13).Append(",规格型号:").Append(this.string_7).Append(",计量单位:").Append(this.string_6).Append(",数量:").Append(this.string_8).Append(",单价:").Append(this.string_9).Append(",金额:").Append(this.string_10).Append(",税率:").Append(this.string_11).Append(",税额:").Append(this.string_12).Append(",含税价标志:").Append(this.bool_0).Append(",发票行性质:").Append(this.fphxz_0.ToString("D")).Append("】");
            return builder.ToString();
        }

        // Properties
        public string Dj
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = ((value == null) || (value == "0")) ? "" : value;
            }
        }

        public string Flbm
        {
            get
            {
                return this.string_14;
            }
            set
            {
                this.string_14 = value;
            }
        }

        public string Ggxh
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = (value == null) ? "" : Class34.smethod_23(value, Struct40.int_45, false, true);
            }
        }

        public string Je
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = (value == null) ? "" : value;
            }
        }

        public string JLdw
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = (value == null) ? "" : Class34.smethod_23(value, Struct40.int_44, false, true);
            }
        }

        public string Kce
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string Lslvbs
        {
            get
            {
                return this.string_17;
            }
            set
            {
                this.string_17 = value;
            }
        }

        public string Se
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = (value == null) ? "" : value;
            }
        }

        public string SL
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = (value == null) ? "" : value;
            }
        }

        public string SLv
        {
            get
            {
                return this.string_11;
            }
            set
            {
                this.string_11 = (value == null) ? "" : value;
            }
        }

        public string Spbh
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string Spmc
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = (value == null) ? "" : Class34.smethod_23(value, Struct40.int_41, false, true);
            }
        }

        public string Spsm
        {
            get
            {
                return this.string_13;
            }
            set
            {
                this.string_13 = (value == null) ? "" : value;
            }
        }

        public string Xsyh
        {
            get
            {
                return this.string_15;
            }
            set
            {
                this.string_15 = value;
            }
        }

        public string XTHash
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string Yhsm
        {
            get
            {
                return this.string_16;
            }
            set
            {
                this.string_16 = value;
            }
        }
    }


}

