namespace Aisino.Fwkp.BusinessObject
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.CommonLibrary;
    using log4net;
    using ns3;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Invoice
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_10;
        private bool bool_11;
        private bool bool_12;
        private bool bool_2;
        private static bool bool_3;
        private bool bool_4;
        private bool bool_5;
        private bool bool_6;
        private bool bool_7;
        private bool bool_8;
        private bool bool_9;
        private static byte[] byte_0;
        private byte[] byte_1;
        private static byte[] byte_2;
        private static byte[] byte_3;
        private Dictionary<string, object> dictionary_0;
        private Dictionary<string, object> dictionary_1;
        private FPLX fplx_0;
        private ILog ilog_0;
        private int int_0;
        internal static int[] int_1;
        private static int int_2;
        private List<Spxx> list_0;
        private object[] object_0;
        private OpType opType_0;
        public string[] Params;
        private string string_0;
        private string string_1;
        private string string_10;
        private string string_11;
        private string string_12;
        private string string_13;
        private string string_14;
        private string string_15;
        private string string_16;
        private string string_17;
        private string string_18;
        private string string_19;
        private string string_2;
        private string string_20;
        private string string_21;
        private string string_22;
        private string string_23;
        private string string_24;
        private string string_25;
        private string string_26;
        private string string_27;
        private string string_28;
        private string string_29;
        private string string_3;
        private string string_30;
        private string string_31;
        private string string_32;
        private string string_33;
        private string string_34;
        private string string_35;
        private string string_36;
        private string string_37;
        private string string_38;
        private string string_39;
        private string string_4;
        private string string_40;
        private string string_41;
        private string string_42;
        private string string_43;
        private string string_44;
        private string string_45;
        private string string_46;
        private string string_47;
        private string string_48;
        private string string_49;
        private string string_5;
        private string string_50;
        private string string_51;
        private string string_52;
        private string string_53;
        private string string_54;
        private string string_55;
        private string string_56;
        private string string_57;
        private string string_58;
        private string string_59;
        private string string_6;
        private string string_60;
        private string string_7;
        private string string_8;
        private string string_9;
        private ZYFP_LX zyfp_LX_0;

        public static  event CheckSLvDelegate CheckSLvEvent;

        static Invoice()
        {
            
            bool_3 = false;
            byte_0 = new byte[0x30];
            int_1 = null;
            byte_2 = null;
            byte_3 = null;
            int_2 = 0;
        }

        public Invoice(bool bool_13, Fpxx fpxx_0, byte[] byte_4, string string_61 = null)
        {
            
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.string_3 = string.Empty;
            this.string_4 = string.Empty;
            this.string_5 = string.Empty;
            this.string_6 = string.Empty;
            this.string_7 = string.Empty;
            this.string_8 = string.Empty;
            this.string_9 = string.Empty;
            this.bool_0 = true;
            this.string_10 = string.Empty;
            this.string_11 = string.Empty;
            this.string_12 = string.Empty;
            this.string_13 = string.Empty;
            this.string_14 = string.Empty;
            this.string_15 = string.Empty;
            this.string_16 = string.Empty;
            this.string_17 = string.Empty;
            this.string_18 = string.Empty;
            this.string_19 = string.Empty;
            this.string_20 = string.Empty;
            this.string_21 = string.Empty;
            this.string_22 = string.Empty;
            this.string_23 = string.Empty;
            this.string_24 = string.Empty;
            this.string_25 = string.Empty;
            this.string_26 = string.Empty;
            this.string_27 = string.Empty;
            this.string_28 = string.Empty;
            this.string_29 = string.Empty;
            this.string_30 = string.Empty;
            this.string_31 = string.Empty;
            this.string_32 = string.Empty;
            this.string_33 = string.Empty;
            this.string_34 = string.Empty;
            this.string_35 = string.Empty;
            this.string_36 = string.Empty;
            this.string_37 = string.Empty;
            this.string_38 = string.Empty;
            this.string_39 = string.Empty;
            this.string_40 = string.Empty;
            this.string_41 = string.Empty;
            this.string_42 = string.Empty;
            this.string_43 = string.Empty;
            this.string_44 = string.Empty;
            this.string_45 = string.Empty;
            this.string_46 = string.Empty;
            this.string_47 = string.Empty;
            this.string_48 = string.Empty;
            this.string_49 = string.Empty;
            this.string_50 = string.Empty;
            this.string_51 = string.Empty;
            this.string_52 = Struct40.string_0;
            this.list_0 = new List<Spxx>(0);
            this.ilog_0 = LogUtil.GetLogger<Invoice>();
            this.bool_12 = TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM";
            this.string_56 = ";0.17;0.13;0.11;0.06;0.05;0.04;0.03;" + ((TaxCardFactory.CreateTaxCard().TaxClock.Subtract(new DateTime(0x7e0, 5, 1)).TotalDays >= 0.0) ? "0.015;" : "") + "0.00;0;";
            byte[] destinationArray = new byte[0x20];
            Array.Copy(byte_0, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(byte_0, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Decrypt(byte_4, destinationArray, buffer2, null);
            this.opType_0 = this.method_19(buffer3);
            this.string_57 = fpxx_0.bmbbbh;
            this.string_1 = fpxx_0.fpdm;
            this.string_2 = fpxx_0.fphm;
            this.string_49 = fpxx_0.mw;
            this.string_50 = fpxx_0.jym;
            this.string_3 = fpxx_0.kprq;
            this.bool_6 = fpxx_0.isRed;
            this.bool_1 = fpxx_0.Qdxx != null;
            this.bool_2 = bool_13;
            this.fplx_0 = fpxx_0.fplx;
            this.bool_0 = fpxx_0.isNewJdcfp;
            this.Bz = ToolUtil.GetString(Convert.FromBase64String(fpxx_0.bz));
            if (!fpxx_0.isBlankWaste && new StringBuilder().Append('0', 15).ToString().Equals(fpxx_0.gfsh))
            {
                fpxx_0.gfsh = string.Empty;
            }
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (((fpxx_0.fplx == FPLX.PTFP) && (fpxx_0.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx_0.xfsh.Equals(card.TaxCode) || fpxx_0.xfsh.Equals(card.OldTaxCode)))
            {
                string gfsh = fpxx_0.gfsh;
                fpxx_0.gfsh = fpxx_0.xfsh;
                fpxx_0.xfsh = gfsh;
                gfsh = fpxx_0.gfmc;
                fpxx_0.gfmc = fpxx_0.xfmc;
                fpxx_0.xfmc = gfsh;
                gfsh = fpxx_0.gfyhzh;
                fpxx_0.gfyhzh = fpxx_0.xfyhzh;
                fpxx_0.xfyhzh = gfsh;
                gfsh = fpxx_0.gfdzdh;
                fpxx_0.gfdzdh = fpxx_0.xfdzdh;
                fpxx_0.xfdzdh = gfsh;
            }
            if ((card.TaxCode.Length == 15) && card.TaxCode.Substring(8, 2).ToUpper().Equals("DK"))
            {
                if (!NotesUtil.GetDKQYFromInvNotes(this.string_41, out this.string_9, out this.string_8).Equals("0000"))
                {
                    this.string_8 = string.Empty;
                    this.string_9 = string.Empty;
                }
                this.string_8 = Class34.smethod_23(this.string_8, Struct40.int_1, false, true);
            }
            if ((fpxx_0.fplx == FPLX.ZYFP) && (fpxx_0.Zyfplx == ZYFP_LX.SNY))
            {
                bool flag = false;
                foreach (Dictionary<SPXX, string> dictionary2 in (fpxx_0.Qdxx == null) ? fpxx_0.Mxxx : fpxx_0.Qdxx)
                {
                    using (Dictionary<ZYFP_LX, string>.KeyCollection.Enumerator enumerator2 = Struct40.dictionary_0.Keys.GetEnumerator())
                    {
                        ZYFP_LX current;
                        while (enumerator2.MoveNext())
                        {
                            current = enumerator2.Current;
                            if (dictionary2[SPXX.SPMC].EndsWith(Struct40.dictionary_0[current]) && ((dictionary2[SPXX.FPHXZ] == Convert.ToString(0)) || (dictionary2[SPXX.FPHXZ] == Convert.ToString(3))))
                            {
                                goto Label_05DE;
                            }
                        }
                        goto Label_05F9;
                    Label_05DE:
                        fpxx_0.Zyfplx = current;
                        flag = true;
                    }
                Label_05F9:
                    if (flag)
                    {
                        break;
                    }
                }
            }
            if ((fpxx_0.fplx != FPLX.JDCFP) || fpxx_0.isNewJdcfp)
            {
                this.Gfsh = fpxx_0.gfsh;
            }
            this.Gfmc = fpxx_0.gfmc;
            this.string_37 = fpxx_0.gfdzdh;
            this.string_38 = fpxx_0.gfyhzh;
            this.string_51 = fpxx_0.sLv;
            this.string_4 = fpxx_0.xfsh;
            this.string_5 = fpxx_0.xfmc;
            this.string_39 = fpxx_0.xfdzdh;
            this.string_40 = fpxx_0.xfyhzh;
            this.string_42 = fpxx_0.kpr;
            this.string_43 = fpxx_0.skr;
            this.string_44 = fpxx_0.fhr;
            this.SetZyfpLx(fpxx_0.Zyfplx);
            this.string_45 = fpxx_0.redNum;
            this.string_46 = fpxx_0.blueFpdm;
            this.string_47 = fpxx_0.blueFphm;
            this.object_0 = fpxx_0.data;
            this.Hjje = fpxx_0.je;
            this.Hjse = fpxx_0.se;
            if ((((this.fplx_0 != FPLX.ZYFP) && (this.fplx_0 != FPLX.PTFP)) && (this.fplx_0 != FPLX.DZFP)) && (this.fplx_0 != FPLX.JSFP))
            {
                if (fpxx_0.fplx == FPLX.HYFP)
                {
                    this.method_6(card);
                    this.bool_1 = false;
                    this.string_4 = fpxx_0.cyrnsrsbh;
                    this.string_5 = fpxx_0.cyrmc;
                    this.string_7 = fpxx_0.spfnsrsbh;
                    this.Gfmc = fpxx_0.spfmc;
                    this.string_29 = fpxx_0.shrmc;
                    this.string_30 = fpxx_0.shrnsrsbh;
                    this.string_31 = fpxx_0.fhrmc;
                    this.string_32 = fpxx_0.fhrnsrsbh;
                    this.string_33 = fpxx_0.qyd;
                    this.string_34 = ToolUtil.GetString(Convert.FromBase64String(fpxx_0.yshwxx));
                    this.string_26 = fpxx_0.jqbh;
                    this.string_35 = fpxx_0.czch;
                    this.string_36 = fpxx_0.ccdw;
                    this.string_27 = fpxx_0.zgswjgdm;
                    this.string_28 = fpxx_0.zgswjgmc;
                }
                else if (fpxx_0.fplx == FPLX.JDCFP)
                {
                    this.method_7(card);
                    this.bool_1 = false;
                    this.string_26 = fpxx_0.jqbh;
                    this.string_10 = fpxx_0.sfzhm;
                    this.string_11 = fpxx_0.cllx;
                    this.string_12 = fpxx_0.cpxh;
                    this.string_13 = fpxx_0.cd;
                    this.string_14 = fpxx_0.hgzh;
                    this.string_15 = fpxx_0.jkzmsh;
                    this.string_16 = fpxx_0.sjdh;
                    this.string_17 = fpxx_0.fdjhm;
                    this.string_18 = fpxx_0.clsbdh;
                    this.string_19 = fpxx_0.sccjmc;
                    this.string_20 = fpxx_0.xfdh;
                    this.string_21 = fpxx_0.xfzh;
                    this.string_22 = fpxx_0.xfdz;
                    this.string_23 = fpxx_0.xfyh;
                    this.string_51 = fpxx_0.sLv;
                    this.string_27 = fpxx_0.zgswjgdm;
                    this.string_28 = fpxx_0.zgswjgmc;
                    this.string_24 = fpxx_0.dw;
                    this.string_25 = fpxx_0.xcrs;
                    this.string_58 = fpxx_0.zyspmc;
                    this.string_59 = fpxx_0.zyspsm;
                    this.string_60 = fpxx_0.sbbz;
                }
            }
            else if (this.fplx_0 == FPLX.ZYFP)
            {
                this.method_3(card);
            }
            else if (this.fplx_0 == FPLX.PTFP)
            {
                this.method_4(card);
            }
            else if (this.fplx_0 == FPLX.DZFP)
            {
                this.method_5(card);
                this.string_26 = fpxx_0.jqbh;
            }
            else if (this.fplx_0 == FPLX.JSFP)
            {
                this.method_8(card, string_61);
                this.string_26 = fpxx_0.jqbh;
                this.string_55 = string_61;
            }
            this.dictionary_0 = fpxx_0.CustomFields;
            this.dictionary_1 = fpxx_0.OtherFields;
            List<Dictionary<SPXX, string>> list = (fpxx_0.Qdxx == null) ? fpxx_0.Mxxx : fpxx_0.Qdxx;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Dictionary<SPXX, string> dictionary = list[i];
                    Spxx item = new Spxx(dictionary[SPXX.SPMC], dictionary[SPXX.SPSM], dictionary[SPXX.SLV]);
                    item.method_13(fpxx_0.Zyfplx);
                    item.Dj = (dictionary[SPXX.DJ].Length == 0) ? "" : Class34.smethod_24(dictionary[SPXX.DJ], Struct40.int_47, false);
                    item.method_6((FPHXZ) Enum.Parse(typeof(FPHXZ), dictionary[SPXX.FPHXZ]));
                    item.Ggxh = dictionary[SPXX.GGXH];
                    item.method_2(dictionary[SPXX.HSJBZ].Equals("1"));
                    item.method_8(this.bool_6);
                    item.Je = Class34.smethod_19(dictionary[SPXX.JE], Struct40.int_50);
                    item.JLdw = dictionary[SPXX.JLDW];
                    if (this.bool_12)
                    {
                        item.Flbm = dictionary[SPXX.FLBM];
                        item.Spbh = dictionary[SPXX.SPBH];
                        item.Xsyh = dictionary[SPXX.XSYH];
                        item.Yhsm = dictionary[SPXX.YHSM];
                        item.Lslvbs = dictionary[SPXX.LSLVBS];
                    }
                    item.Se = Class34.smethod_19(dictionary[SPXX.SE], Struct40.int_50);
                    item.SL = (dictionary[SPXX.SL].Length == 0) ? "" : Class34.smethod_24(dictionary[SPXX.SL], Struct40.int_49, false);
                    item.method_8(this.bool_6);
                    if (this.fplx_0 == FPLX.HYFP)
                    {
                        item.SLv = fpxx_0.sLv;
                    }
                    if ((fpxx_0.Zyfplx == ZYFP_LX.CEZS) && (item.method_5() != FPHXZ.ZKXX))
                    {
                        if (string.IsNullOrEmpty(dictionary[SPXX.KCE]))
                        {
                            string str2 = Class34.smethod_0(this.string_41);
                            if ((i <= 0) && !string.IsNullOrEmpty(str2))
                            {
                                item.Kce = str2;
                            }
                            else if (Class34.smethod_10(dictionary[SPXX.SLV], Struct40.string_0))
                            {
                                item.Kce = "0";
                            }
                            else
                            {
                                item.Kce = Class34.smethod_18(Class34.smethod_17(dictionary[SPXX.JE], dictionary[SPXX.SE]), Class34.smethod_11(Class34.smethod_16(Class34.smethod_17("1", dictionary[SPXX.SLV]), dictionary[SPXX.SLV]), dictionary[SPXX.SE], Struct40.int_50));
                            }
                        }
                        else
                        {
                            item.Kce = dictionary[SPXX.KCE];
                        }
                    }
                    else
                    {
                        item.Kce = "0";
                    }
                    this.list_0.Add(item);
                }
            }
        }

        public Invoice(bool bool_13, bool bool_14, bool bool_15, FPLX fplx_1, byte[] byte_4, string string_61 = null)
        {
            
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.string_3 = string.Empty;
            this.string_4 = string.Empty;
            this.string_5 = string.Empty;
            this.string_6 = string.Empty;
            this.string_7 = string.Empty;
            this.string_8 = string.Empty;
            this.string_9 = string.Empty;
            this.bool_0 = true;
            this.string_10 = string.Empty;
            this.string_11 = string.Empty;
            this.string_12 = string.Empty;
            this.string_13 = string.Empty;
            this.string_14 = string.Empty;
            this.string_15 = string.Empty;
            this.string_16 = string.Empty;
            this.string_17 = string.Empty;
            this.string_18 = string.Empty;
            this.string_19 = string.Empty;
            this.string_20 = string.Empty;
            this.string_21 = string.Empty;
            this.string_22 = string.Empty;
            this.string_23 = string.Empty;
            this.string_24 = string.Empty;
            this.string_25 = string.Empty;
            this.string_26 = string.Empty;
            this.string_27 = string.Empty;
            this.string_28 = string.Empty;
            this.string_29 = string.Empty;
            this.string_30 = string.Empty;
            this.string_31 = string.Empty;
            this.string_32 = string.Empty;
            this.string_33 = string.Empty;
            this.string_34 = string.Empty;
            this.string_35 = string.Empty;
            this.string_36 = string.Empty;
            this.string_37 = string.Empty;
            this.string_38 = string.Empty;
            this.string_39 = string.Empty;
            this.string_40 = string.Empty;
            this.string_41 = string.Empty;
            this.string_42 = string.Empty;
            this.string_43 = string.Empty;
            this.string_44 = string.Empty;
            this.string_45 = string.Empty;
            this.string_46 = string.Empty;
            this.string_47 = string.Empty;
            this.string_48 = string.Empty;
            this.string_49 = string.Empty;
            this.string_50 = string.Empty;
            this.string_51 = string.Empty;
            this.string_52 = Struct40.string_0;
            this.list_0 = new List<Spxx>(0);
            this.ilog_0 = LogUtil.GetLogger<Invoice>();
            this.bool_12 = TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM";
            this.string_56 = ";0.17;0.13;0.11;0.06;0.05;0.04;0.03;" + ((TaxCardFactory.CreateTaxCard().TaxClock.Subtract(new DateTime(0x7e0, 5, 1)).TotalDays >= 0.0) ? "0.015;" : "") + "0.00;0;";
            byte[] destinationArray = new byte[0x20];
            Array.Copy(byte_0, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(byte_0, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Decrypt(byte_4, destinationArray, buffer2, null);
            this.opType_0 = this.method_19(buffer3);
            if ((((fplx_1 != FPLX.ZYFP) && (fplx_1 != FPLX.PTFP)) && (fplx_1 != FPLX.DZFP)) && (fplx_1 != FPLX.JSFP))
            {
                if (fplx_1 == FPLX.HYFP)
                {
                    this.bool_6 = bool_13;
                    this.bool_1 = false;
                    this.bool_2 = bool_15;
                    this.fplx_0 = FPLX.HYFP;
                    TaxCard card3 = TaxCardFactory.CreateTaxCard();
                    this.method_6(card3);
                }
                else if (fplx_1 == FPLX.JDCFP)
                {
                    this.bool_6 = bool_13;
                    this.bool_1 = false;
                    this.bool_2 = false;
                    this.fplx_0 = FPLX.JDCFP;
                    this.string_53 = Struct40.string_0;
                    this.string_54 = Struct40.string_0;
                    TaxCard card2 = TaxCardFactory.CreateTaxCard();
                    this.method_7(card2);
                }
            }
            else
            {
                this.bool_6 = bool_13;
                if ((fplx_1 != FPLX.DZFP) && (fplx_1 != FPLX.JSFP))
                {
                    this.bool_1 = bool_14;
                }
                else
                {
                    this.bool_1 = false;
                }
                this.bool_2 = bool_15;
                this.fplx_0 = fplx_1;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (fplx_1 == FPLX.ZYFP)
                {
                    this.method_3(card);
                }
                else if (fplx_1 == FPLX.PTFP)
                {
                    this.method_4(card);
                }
                else if (fplx_1 == FPLX.DZFP)
                {
                    this.method_5(card);
                }
                else if (fplx_1 == FPLX.JSFP)
                {
                    this.method_8(card, string_61);
                    this.string_55 = string_61;
                }
                if (this.bool_11 && (fplx_1 == FPLX.ZYFP))
                {
                    this.bool_1 = false;
                }
            }
        }

        public Invoice(FPLX fplx_1, string string_61, string string_62, string string_63, string string_64, string string_65, byte[] byte_4, string string_66 = null)
        {
            
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.string_3 = string.Empty;
            this.string_4 = string.Empty;
            this.string_5 = string.Empty;
            this.string_6 = string.Empty;
            this.string_7 = string.Empty;
            this.string_8 = string.Empty;
            this.string_9 = string.Empty;
            this.bool_0 = true;
            this.string_10 = string.Empty;
            this.string_11 = string.Empty;
            this.string_12 = string.Empty;
            this.string_13 = string.Empty;
            this.string_14 = string.Empty;
            this.string_15 = string.Empty;
            this.string_16 = string.Empty;
            this.string_17 = string.Empty;
            this.string_18 = string.Empty;
            this.string_19 = string.Empty;
            this.string_20 = string.Empty;
            this.string_21 = string.Empty;
            this.string_22 = string.Empty;
            this.string_23 = string.Empty;
            this.string_24 = string.Empty;
            this.string_25 = string.Empty;
            this.string_26 = string.Empty;
            this.string_27 = string.Empty;
            this.string_28 = string.Empty;
            this.string_29 = string.Empty;
            this.string_30 = string.Empty;
            this.string_31 = string.Empty;
            this.string_32 = string.Empty;
            this.string_33 = string.Empty;
            this.string_34 = string.Empty;
            this.string_35 = string.Empty;
            this.string_36 = string.Empty;
            this.string_37 = string.Empty;
            this.string_38 = string.Empty;
            this.string_39 = string.Empty;
            this.string_40 = string.Empty;
            this.string_41 = string.Empty;
            this.string_42 = string.Empty;
            this.string_43 = string.Empty;
            this.string_44 = string.Empty;
            this.string_45 = string.Empty;
            this.string_46 = string.Empty;
            this.string_47 = string.Empty;
            this.string_48 = string.Empty;
            this.string_49 = string.Empty;
            this.string_50 = string.Empty;
            this.string_51 = string.Empty;
            this.string_52 = Struct40.string_0;
            this.list_0 = new List<Spxx>(0);
            this.ilog_0 = LogUtil.GetLogger<Invoice>();
            this.bool_12 = TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM";
            this.string_56 = ";0.17;0.13;0.11;0.06;0.05;0.04;0.03;" + ((TaxCardFactory.CreateTaxCard().TaxClock.Subtract(new DateTime(0x7e0, 5, 1)).TotalDays >= 0.0) ? "0.015;" : "") + "0.00;0;";
            this.fplx_0 = fplx_1;
            byte[] destinationArray = new byte[0x20];
            Array.Copy(byte_0, 0, destinationArray, 0, 0x20);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(byte_0, 0x20, buffer2, 0, 0x10);
            byte[] buffer3 = AES_Crypt.Decrypt(byte_4, destinationArray, buffer2, null);
            this.opType_0 = this.method_19(buffer3);
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (fplx_1 == FPLX.ZYFP)
            {
                this.method_3(card);
            }
            else if (fplx_1 == FPLX.PTFP)
            {
                this.method_4(card);
            }
            else if (fplx_1 == FPLX.DZFP)
            {
                this.method_5(card);
                string str3 = string.Empty;
                card.GetInvControlNum(out str3, 0);
                this.string_26 = str3;
            }
            else if (fplx_1 == FPLX.HYFP)
            {
                this.method_6(card);
                string str = string.Empty;
                card.GetInvControlNum(out str, 0);
                this.string_26 = str;
                string[] strArray = card.SQInfo.ZGJGDMMC.Split(new char[] { ',' });
                this.string_27 = strArray[0];
                this.string_28 = strArray[1];
            }
            else if (fplx_1 == FPLX.JDCFP)
            {
                this.method_7(card);
                this.string_18 = "0";
                string str4 = string.Empty;
                card.GetInvControlNum(out str4, 0);
                this.string_26 = str4;
                string[] strArray2 = card.SQInfo.ZGJGDMMC.Split(new char[] { ',' });
                this.string_27 = strArray2[0];
                this.string_28 = strArray2[1];
            }
            else if (fplx_1 == FPLX.JSFP)
            {
                this.method_8(card, string_66);
                string str2 = string.Empty;
                card.GetInvControlNum(out str2, 0);
                this.string_26 = str2;
                this.string_55 = string_66;
            }
            this.string_51 = "0.17";
            this.zyfp_LX_0 = ZYFP_LX.ZYFP;
            this.string_1 = string_61;
            this.string_2 = string_62;
            this.string_7 = "000000000000000";
            this.Gfmc = "0";
            this.string_42 = (string_63 == null) ? "" : string_63.Split(Environment.NewLine.ToCharArray())[0];
            this.string_5 = card.Corporation;
            this.string_4 = card.TaxCode;
            this.string_40 = "";
            this.string_39 = "";
            this.bool_10 = true;
        }

        public int AddSpxx(Spxx spxx_0)
        {
            return this.InsertSpxx(-1, spxx_0);
        }

        public int AddSpxx(string string_61, string string_62, ZYFP_LX zyfp_LX_1)
        {
            return this.InsertSpxx(-1, "", string_61, string_62, zyfp_LX_1);
        }

        public int AddSpxx(string string_61, string string_62, string string_63, ZYFP_LX zyfp_LX_1)
        {
            return this.InsertSpxx(-1, string_61, string_62, string_63, zyfp_LX_1);
        }

        public int AddSpxx(string string_61, string string_62, string string_63, string string_64, string string_65, string string_66, bool bool_13, ZYFP_LX zyfp_LX_1)
        {
            return this.InsertSpxx(-1, string_61, string_62, string_63, string_64, string_65, string_66, bool_13, zyfp_LX_1);
        }

        public int AddZkxx(int int_3, int int_4, string string_61, string string_62)
        {
            if (this.method_9())
            {
                if (this.fplx_0 == FPLX.HYFP)
                {
                    this.Code = "A071";
                    return -1;
                }
                if (this.bool_6)
                {
                    this.Code = "A011";
                    return -1;
                }
                if ((int_4 < 1) || (int_4 > (int_3 + 1)))
                {
                    this.Code = "A006";
                    return -1;
                }
                Spxx spxx = this.list_0[int_3];
                if (spxx.method_5() == FPHXZ.ZKXX)
                {
                    this.Code = "A012";
                    return -1;
                }
                if (spxx.method_5() == FPHXZ.SPXX_ZK)
                {
                    this.Code = "A013";
                    return -1;
                }
                if (this.bool_12 && (int_4 > 1))
                {
                    this.Code = "A215";
                    return -1;
                }
                if (!this.CanAddSpxx(1, true))
                {
                    return -1;
                }
                for (int i = int_4; i > 0; i--)
                {
                    spxx = this.list_0[(int_3 - i) + 1];
                    if ((spxx.method_5() == FPHXZ.ZKXX) || (spxx.method_5() == FPHXZ.SPXX_ZK))
                    {
                        this.Code = "A014";
                        return -1;
                    }
                    if (!string.Equals(this.list_0[int_3].SLv, spxx.SLv))
                    {
                        this.Code = "A031";
                        return -1;
                    }
                }
                string_62 = Class34.smethod_19(string_62, Struct40.int_50);
                if (Class34.smethod_10(string_62, Struct40.string_0))
                {
                    this.Code = "A095";
                    return -1;
                }
                string_61 = Class34.smethod_19(string_61, Struct40.int_54);
                string str = Struct40.string_0;
                string str4 = Struct40.string_0;
                string str5 = Struct40.string_0;
                for (int j = 0; j < int_4; j++)
                {
                    if (this.bool_2 && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                    {
                        str = Class34.smethod_17(this.list_0[int_3 - j].method_11(), str);
                    }
                    else
                    {
                        str = Class34.smethod_17(this.list_0[int_3 - j].Je, str);
                    }
                    str4 = Class34.smethod_17(this.list_0[int_3 - j].Se, str4);
                    if (!string.IsNullOrEmpty(this.list_0[j].Kce))
                    {
                        str5 = Class34.smethod_17(this.list_0[j].Kce, str5);
                    }
                }
                if ((this.zyfp_LX_0 == ZYFP_LX.CEZS) && Class34.smethod_9(Class34.smethod_21(string_62), Class34.smethod_18(str, str5), false))
                {
                    this.Code = "A219";
                    return -1;
                }
                if (Class34.smethod_9(Class34.smethod_21(string_62), str, false))
                {
                    this.Code = "A008";
                    return -1;
                }
                if (!Class34.smethod_7(str, string_61, Class34.smethod_21(string_62), Struct40.int_50, Struct40.string_0) && !Class34.smethod_22(Class34.smethod_21(string_62), str, string_61, Struct40.int_54, Struct40.string_0))
                {
                    string_61 = Class34.smethod_15(Class34.smethod_21(string_62), str, Struct40.int_54);
                }
                string spmc = this.list_0[int_3].Spmc;
                if (!this.bool_12)
                {
                    spmc = this.method_16(string_61, int_4);
                }
                Spxx spxx2 = new Spxx(spmc, "", spxx.SLv) {
                    Kce = "0"
                };
                spxx2.method_6(FPHXZ.ZKXX);
                spxx2.method_2(this.bool_2);
                spxx2.method_13(this.zyfp_LX_0);
                if (this.bool_2 && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                {
                    if (Class34.smethod_10(Class34.smethod_21(string_62), str))
                    {
                        string str3 = "0";
                        for (int k = 0; k < int_4; k++)
                        {
                            str3 = Class34.smethod_17(this.list_0[int_3 - k].Je, str3);
                        }
                        spxx2.Je = Class34.smethod_21(str3);
                    }
                    else if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                    {
                        spxx2.Je = Class34.smethod_15(string_62, Class34.smethod_17(spxx.SLv, "1.00"), Struct40.int_50);
                    }
                    else
                    {
                        spxx2.Je = Class34.smethod_2(this.zyfp_LX_0, string_62, Struct40.int_50, null, spxx2);
                    }
                }
                else
                {
                    spxx2.Je = string_62;
                }
                if (!this.bool_2 && Class34.smethod_10(Class34.smethod_21(string_62), str))
                {
                    spxx2.Se = Class34.smethod_21(str4);
                }
                else
                {
                    if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                    {
                        spxx2.Se = Class34.smethod_15(Class34.smethod_13(spxx2.Je, spxx.SLv), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                    }
                    else if (!this.bool_2)
                    {
                        if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.Zyfplx != ZYFP_LX.CEZS))
                        {
                            spxx2.Se = Class34.smethod_12(spxx2.Je, spxx.SLv, Struct40.int_50);
                        }
                        else
                        {
                            spxx2.Se = Class34.smethod_1(this.Zyfplx, spxx2.Je, Struct40.int_50, spxx2);
                        }
                    }
                    else
                    {
                        spxx2.Se = Class34.smethod_18(string_62, spxx2.Je);
                    }
                    if (Class34.smethod_9(Class34.smethod_21(spxx2.Se), str4, false))
                    {
                        spxx2.Se = Class34.smethod_21(str4);
                    }
                }
                if (((spxx2.Je.Length <= Struct40.int_51) && (spxx2.Se.Length <= Struct40.int_53)) && ((Struct40.int_52 <= 0) || (spxx2.method_11().Length <= Struct40.int_52)))
                {
                    this.list_0.Insert(int_3 + 1, spxx2);
                    for (int m = 0; m < int_4; m++)
                    {
                        this.list_0[int_3 - m].method_6(FPHXZ.SPXX_ZK);
                    }
                    this.string_53 = null;
                    this.string_54 = null;
                    this.Code = "0000";
                    return (int_3 + 1);
                }
                this.Code = "A123";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
            }
            return -1;
        }

        public int AddZkxx(int int_3, int int_4, string string_61, string string_62, string string_63)
        {
            int num = this.AddZkxx(int_3, int_4, string_61, string_62);
            if (num >= 0)
            {
                this.list_0[num].Se = string_63;
                if (this.bool_2 && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                {
                    this.list_0[num].Je = Class34.smethod_18(string_62, string_63);
                }
            }
            return num;
        }

        public bool CanAddSpxx(int int_3 = 1, bool bool_13 = false)
        {
            return this.method_18(0, int_3, bool_13);
        }

        public bool CanSaveSpxx()
        {
            return this.method_18(1, 1, false);
        }

        public bool CheckFpData()
        {
            try
            {
                Fpxx fpData = this.GetFpData();
                if (fpData == null)
                {
                    return false;
                }
                return this.CheckFpData(fpData);
            }
            catch (Exception exception)
            {
                this.ilog_0.ErrorFormat("发票数据校验异常1：{0}", exception.ToString());
                this.Code = "A999";
                return false;
            }
        }

        public bool CheckFpData(Fpxx fpxx_0)
        {
            if (fpxx_0 == null)
            {
                return false;
            }
            if (this.bool_10)
            {
                this.Code = "0000";
                return true;
            }
            if (((this.opType_0 == OpType.KT) || (this.opType_0 == OpType.KC)) || (this.bool_11 || (bool_3 && this.bool_5)))
            {
                string str = "Aisino.Fwkp.Invoice" + this.string_1 + this.string_2;
                byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                byte[] destinationArray = new byte[0x20];
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String(fpxx_0.gfmc), destinationArray, buffer3, null);
                if (buffer4 == null)
                {
                    this.opType_0 = OpType.ER;
                }
                else
                {
                    fpxx_0.gfmc = Encoding.Unicode.GetString(buffer4);
                }
            }
            fpxx_0.bz = Convert.ToBase64String(ToolUtil.GetBytes(fpxx_0.bz));
            fpxx_0.yshwxx = Convert.ToBase64String(ToolUtil.GetBytes(fpxx_0.yshwxx));
            InvoiceHandler handler = new InvoiceHandler();
            if (!handler.CheckHzfwFpxx(fpxx_0))
            {
                this.Code = handler.GetCode();
                return false;
            }
            this.Code = "0000";
            return true;
        }

        public bool CheckFpData_WBJK_ChaiFen()
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if (!this.CheckSpxx(i))
                {
                    return false;
                }
            }
            if (!this.method_11())
            {
                return false;
            }
            bool flag = true;
            if (this.bool_11)
            {
                this.int_0 = 1;
                flag = this.CheckFpData();
                this.int_0 = 0;
            }
            return flag;
        }

        public bool CheckInvoice()
        {
            if ((this.fplx_0 == FPLX.JDCFP) && (this.list_0.Count == 0))
            {
                if (!this.method_13(this.string_51, -1))
                {
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    Spxx spxx = this.list_0[i];
                    if (((spxx.method_5() != FPHXZ.XHQDZK) && (spxx.method_5() != FPHXZ.XJXHQD)) && !this.method_13(this.list_0[i].SLv, i))
                    {
                        return false;
                    }
                }
            }
            if (!this.method_18(1, 1, false))
            {
                return false;
            }
            if ((!this.bool_10 && (this.fplx_0 == FPLX.ZYFP)) && (this.string_7.Length == 0))
            {
                this.Code = "A024";
                return false;
            }
            if (((this.bool_6 && !string.IsNullOrEmpty(this.string_1)) && (!string.IsNullOrEmpty(this.string_2) && string.Equals(this.string_1, this.BlueFpdm))) && string.Equals(this.string_2.PadLeft(8, '0'), this.string_47.PadLeft(8, '0')))
            {
                this.Code = "A074";
                return false;
            }
            if (((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP)) || ((this.fplx_0 == FPLX.DZFP) || (this.fplx_0 == FPLX.JSFP)))
            {
                if (this.Gfmc.Length == 0)
                {
                    this.Code = "A025";
                    return false;
                }
                if ((this.zyfp_LX_0 != ZYFP_LX.NCP_SG) && (this.string_4.Length == 0))
                {
                    this.Code = "A026";
                    return false;
                }
                if (this.string_5.Length == 0)
                {
                    this.Code = "A027";
                    return false;
                }
                if (!this.bool_8 && ((this.zyfp_LX_0 == ZYFP_LX.XT_CCP) || (this.zyfp_LX_0 == ZYFP_LX.XT_YCL)))
                {
                    this.Code = "A048";
                    return false;
                }
            }
            if ((this.fplx_0 == FPLX.JDCFP) && !this.bool_10)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append((this.Gfmc.Length == 0) ? "购货单位，" : "").Append((this.string_10.Length == 0) ? "身份证号码/组织机构代码，" : "").Append((this.string_11.Length == 0) ? "车辆类型，" : "").Append((this.string_12.Length == 0) ? "厂牌型号，" : "").Append((this.string_13.Length == 0) ? "产地，" : "").Append((this.string_18.Length == 0) ? "车辆识别代号/车架号码，" : "");
                if (builder.Length > 0)
                {
                    this.Params = new string[] { builder.ToString().Substring(0, builder.Length - 1) };
                    this.Code = "A039";
                    return false;
                }
            }
            if ((this.fplx_0 == FPLX.HYFP) && !this.bool_10)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append((this.Gfmc.Length == 0) ? "实际受票方，" : "").Append((this.string_29.Length == 0) ? "收货人，" : "").Append((this.string_31.Length == 0) ? "发货人，" : "").Append((this.string_7.Length == 0) ? "实际受票方税号，" : "").Append((this.string_30.Length == 0) ? "收货人税号，" : "").Append((this.string_32.Length == 0) ? "发货人税号，" : "");
                if (builder2.Length > 0)
                {
                    this.Params = new string[] { builder2.ToString().Substring(0, builder2.Length - 1) };
                    this.Code = "A039";
                    return false;
                }
            }
            return true;
        }

        public bool CheckSpxx(int int_3)
        {
            if (this.list_0.Count == 0)
            {
                this.Code = "0000";
                this.Params = null;
                return true;
            }
            if (!this.list_0[int_3].method_4(this.bool_2))
            {
                this.Code = this.list_0[int_3].method_0();
                this.Params = new string[this.list_0[int_3].string_0.Length + 1];
                this.Params[0] = Convert.ToString((int) (int_3 + 1));
                Array.ConstrainedCopy(this.list_0[int_3].string_0, 0, this.Params, 1, this.list_0[int_3].string_0.Length);
                return false;
            }
            this.Code = "0000";
            this.Params = null;
            return true;
        }

        public void CopyFpxx(Fpxx fpxx_0)
        {
            fpxx_0.sLv = ((fpxx_0.sLv == null) || (fpxx_0.sLv.Length <= 0)) ? "" : Class34.smethod_20(fpxx_0.sLv, Struct40.int_55);
            this.zyfp_LX_0 = fpxx_0.Zyfplx;
            this.bool_1 = fpxx_0.Qdxx != null;
            this.string_57 = fpxx_0.bmbbbh;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (((this.zyfp_LX_0 == ZYFP_LX.NCP_SG) && (fpxx_0.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx_0.xfsh.Equals(card.TaxCode) || fpxx_0.xfsh.Equals(card.OldTaxCode)))
            {
                this.Xfsh = fpxx_0.gfsh;
                this.Xfmc = fpxx_0.gfmc;
                this.Xfdzdh = fpxx_0.gfdzdh;
                this.Xfyhzh = fpxx_0.gfyhzh;
                this.string_38 = fpxx_0.xfyhzh;
            }
            else
            {
                if ((fpxx_0.fplx != FPLX.JDCFP) || fpxx_0.isNewJdcfp)
                {
                    this.Gfsh = fpxx_0.gfsh;
                }
                this.Gfmc = fpxx_0.gfmc;
                this.Gfdzdh = fpxx_0.gfdzdh;
                this.Gfyhzh = fpxx_0.gfyhzh;
                this.string_40 = fpxx_0.xfyhzh;
            }
            if ((fpxx_0.fplx != FPLX.JDCFP) && (fpxx_0.fplx != FPLX.HYFP))
            {
                this.string_51 = fpxx_0.sLv;
            }
            else if (!this.SetFpSLv(fpxx_0.sLv))
            {
                return;
            }
            this.Skr = fpxx_0.skr;
            this.Fhr = fpxx_0.fhr;
            this.Bz = ToolUtil.GetString(Convert.FromBase64String(fpxx_0.bz));
            this.Hjje = fpxx_0.je;
            this.Hjse = fpxx_0.se;
            if (fpxx_0.fplx == FPLX.HYFP)
            {
                this.string_4 = fpxx_0.cyrnsrsbh;
                this.string_5 = fpxx_0.cyrmc;
                this.string_7 = fpxx_0.spfnsrsbh;
                this.Gfmc = fpxx_0.spfmc;
                this.string_29 = fpxx_0.shrmc;
                this.string_30 = fpxx_0.shrnsrsbh;
                this.string_31 = fpxx_0.fhrmc;
                this.string_32 = fpxx_0.fhrnsrsbh;
                this.string_33 = fpxx_0.qyd;
                this.string_34 = ToolUtil.GetString(Convert.FromBase64String(fpxx_0.yshwxx));
                this.string_26 = fpxx_0.jqbh;
                this.string_35 = fpxx_0.czch;
                this.string_36 = fpxx_0.ccdw;
                this.string_27 = fpxx_0.zgswjgdm;
                this.string_28 = fpxx_0.zgswjgmc;
            }
            else if (fpxx_0.fplx == FPLX.JDCFP)
            {
                this.string_26 = fpxx_0.jqbh;
                this.string_10 = fpxx_0.sfzhm;
                this.string_11 = fpxx_0.cllx;
                this.string_12 = fpxx_0.cpxh;
                this.string_13 = fpxx_0.cd;
                this.string_14 = fpxx_0.hgzh;
                this.string_15 = fpxx_0.jkzmsh;
                this.string_16 = fpxx_0.sjdh;
                this.string_17 = fpxx_0.fdjhm;
                this.string_18 = fpxx_0.clsbdh;
                this.string_19 = fpxx_0.sccjmc;
                this.string_20 = fpxx_0.xfdh;
                this.string_21 = fpxx_0.xfzh;
                this.string_22 = fpxx_0.xfdz;
                this.string_23 = fpxx_0.xfyh;
                this.string_51 = fpxx_0.sLv;
                this.string_27 = fpxx_0.zgswjgdm;
                this.string_28 = fpxx_0.zgswjgmc;
                this.string_24 = fpxx_0.dw;
                this.string_25 = fpxx_0.xcrs;
                this.string_58 = fpxx_0.zyspmc;
                this.string_59 = fpxx_0.zyspsm;
                this.string_60 = fpxx_0.sbbz;
            }
            else if ((fpxx_0.fplx == FPLX.DZFP) || (fpxx_0.fplx == FPLX.JSFP))
            {
                this.string_26 = fpxx_0.jqbh;
            }
            this.list_0.Clear();
            List<Dictionary<SPXX, string>> list = (fpxx_0.Qdxx == null) ? fpxx_0.Mxxx : fpxx_0.Qdxx;
            if (list != null)
            {
                int num = 0;
                foreach (Dictionary<SPXX, string> dictionary in list)
                {
                    FPHXZ fphxz = (FPHXZ) Enum.Parse(typeof(FPHXZ), dictionary[SPXX.FPHXZ]);
                    switch (fphxz)
                    {
                        case FPHXZ.XHQDZK:
                        case FPHXZ.XJXHQD:
                            break;

                        default:
                        {
                            Spxx item = new Spxx(dictionary[SPXX.SPMC], dictionary[SPXX.SPSM], dictionary[SPXX.SLV]);
                            item.method_6(fphxz);
                            item.method_13(this.zyfp_LX_0);
                            item.Dj = dictionary[SPXX.DJ];
                            item.Ggxh = dictionary[SPXX.GGXH];
                            item.method_2(dictionary[SPXX.HSJBZ].Equals("1"));
                            item.method_8(this.bool_6);
                            item.Je = decimal.Round(decimal.Parse(dictionary[SPXX.JE]), 2, MidpointRounding.AwayFromZero).ToString("0.00");
                            item.JLdw = dictionary[SPXX.JLDW];
                            item.Se = decimal.Round(decimal.Parse(dictionary[SPXX.SE]), 2, MidpointRounding.AwayFromZero).ToString("0.00");
                            item.SL = dictionary[SPXX.SL];
                            item.Kce = dictionary[SPXX.KCE];
                            if (this.bool_12)
                            {
                                item.Flbm = dictionary[SPXX.FLBM];
                                item.Spbh = dictionary[SPXX.SPBH];
                                item.Xsyh = dictionary[SPXX.XSYH];
                                item.Yhsm = dictionary[SPXX.YHSM];
                                item.Lslvbs = dictionary[SPXX.LSLVBS];
                            }
                            if ((fpxx_0.Zyfplx == ZYFP_LX.CEZS) && (item.method_5() != FPHXZ.ZKXX))
                            {
                                if (string.IsNullOrEmpty(dictionary[SPXX.KCE]))
                                {
                                    string str = Class34.smethod_0(this.string_41);
                                    if ((num <= 0) && !string.IsNullOrEmpty(str))
                                    {
                                        item.Kce = str;
                                    }
                                    else if (Class34.smethod_10(dictionary[SPXX.SLV], Struct40.string_0))
                                    {
                                        item.Kce = "0";
                                    }
                                    else
                                    {
                                        item.Kce = Class34.smethod_18(Class34.smethod_17(dictionary[SPXX.JE], dictionary[SPXX.SE]), Class34.smethod_11(Class34.smethod_16(Class34.smethod_17("1", dictionary[SPXX.SLV]), dictionary[SPXX.SLV]), dictionary[SPXX.SE], Struct40.int_50));
                                    }
                                }
                                else
                                {
                                    item.Kce = dictionary[SPXX.KCE];
                                }
                            }
                            else
                            {
                                item.Kce = "0";
                            }
                            if (!this.method_13(item.SLv, -1))
                            {
                                return;
                            }
                            this.list_0.Add(item);
                            num++;
                            break;
                        }
                    }
                }
            }
            string taxCode = card.TaxCode;
            if ((taxCode.Length == 15) && taxCode.Substring(8, 2).ToUpper().Equals("DK"))
            {
                this.string_40 = string.Empty;
                this.string_39 = string.Empty;
                this.string_41 = string.Empty;
                this.string_9 = string.Empty;
                this.string_8 = string.Empty;
            }
            this.Code = "0000";
        }

        public bool DelSpxx(int int_3)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() != FPHXZ.SPXX_ZK)
            {
                if (spxx.method_5() == FPHXZ.ZKXX)
                {
                    for (int i = 1; (int_3 - i) >= 0; i++)
                    {
                        spxx = this.list_0[int_3 - i];
                        if (spxx.method_5() != FPHXZ.SPXX_ZK)
                        {
                            break;
                        }
                        spxx.method_6(FPHXZ.SPXX);
                    }
                }
            }
            else
            {
                int num2 = int_3;
                while (num2 < this.list_0.Count)
                {
                    if (this.list_0[num2].method_5() == FPHXZ.ZKXX)
                    {
                        break;
                    }
                    num2++;
                }
                if (num2 >= this.list_0.Count)
                {
                    this.Code = "A003";
                    return false;
                }
                string[] zkBySpmc = this.GetZkBySpmc(this.list_0[num2].Spmc, num2);
                string str = zkBySpmc[0];
                int num3 = int.Parse(zkBySpmc[1]);
                for (int j = int_3; j >= 0; j--)
                {
                    if (this.list_0[j].method_5() != FPHXZ.SPXX_ZK)
                    {
                        break;
                    }
                    this.list_0[j].method_6(FPHXZ.SPXX);
                }
                for (int k = int_3; k < num2; k++)
                {
                    this.list_0[k].method_6(FPHXZ.SPXX);
                }
                this.list_0.RemoveAt(num2);
                this.list_0.RemoveAt(int_3);
                if ((num3 - 1) > 0)
                {
                    string str2 = Class34.smethod_15(str, "100.00", Struct40.int_54);
                    string str3 = this.GetZke(num2 - 2, num3 - 1, str2);
                    this.AddZkxx(num2 - 2, num3 - 1, str2, str3);
                }
                this.Code = "0000";
                this.string_53 = null;
                this.string_54 = null;
                return true;
            }
            this.list_0.RemoveAt(int_3);
            if (this.list_0.Count == 0)
            {
                this.string_51 = string.Empty;
                if (!this.bool_6)
                {
                    if ((this.fplx_0 == FPLX.ZYFP) && ((this.zyfp_LX_0 == ZYFP_LX.HYSY) || (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)))
                    {
                        this.zyfp_LX_0 = ZYFP_LX.ZYFP;
                    }
                    if ((this.fplx_0 == FPLX.PTFP) && (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15))
                    {
                        this.zyfp_LX_0 = ZYFP_LX.ZYFP;
                    }
                }
            }
            else
            {
                int num5 = 1;
                while (num5 < this.list_0.Count)
                {
                    if (!string.Equals(this.list_0[0].SLv, this.list_0[num5].SLv))
                    {
                        this.string_51 = string.Empty;
                        break;
                    }
                    num5++;
                }
                if (num5 == this.list_0.Count)
                {
                    this.string_51 = this.list_0[0].SLv;
                }
            }
            this.string_53 = null;
            this.string_54 = null;
            return true;
        }

        public bool DelSpxxAll()
        {
            if (this.list_0.Count > 0)
            {
                this.list_0.Clear();
                this.string_51 = "";
                if ((((this.fplx_0 == FPLX.ZYFP) && (this.zyfp_LX_0 != ZYFP_LX.SNY)) && ((this.zyfp_LX_0 != ZYFP_LX.SNY_DDZG) && (this.zyfp_LX_0 != ZYFP_LX.RLY))) && (this.zyfp_LX_0 != ZYFP_LX.RLY_DDZG))
                {
                    this.zyfp_LX_0 = ZYFP_LX.ZYFP;
                }
                this.string_53 = null;
                this.string_54 = null;
            }
            return true;
        }

        public static string FPLX2Str(FPLX fplx_1)
        {
            switch (fplx_1)
            {
                case FPLX.ZYFP:
                    return "s";

                case FPLX.PTFP:
                    return "c";

                case FPLX.HYFP:
                    return "f";

                case FPLX.JDCFP:
                    return "j";

                case FPLX.JSFP:
                    return "q";

                case FPLX.DZFP:
                    return "p";
            }
            return "s";
        }

        public string GetCode()
        {
            return this.string_0;
        }

        public Fpxx GetFpData()
        {
            string str = "0";
            try
            {
                int num2;
                int num4;
                if ((this.opType_0 == OpType.KT) || (this.opType_0 == OpType.KC))
                {
                    str = "1";
                    byte[] sourceArray = (byte[]) this.object_0[0];
                    string[] strArray = (string[]) this.object_0[1];
                    if (((this.byte_1 != null) && (this.byte_1.Length > 0)) && ((int_2 < 1) || (sourceArray.Length == 0x11)))
                    {
                        str = "2";
                        byte[] destinationArray = new byte[0x10];
                        Array.Copy(sourceArray, 0, destinationArray, 0, 0x10);
                        byte[] buffer = RegisterManager.CheckRegFile((this.opType_0 == OpType.KT) ? "JI" : "JP", destinationArray, null);
                        if (buffer == null)
                        {
                            this.Code = RegisterManager.errCode;
                            return null;
                        }
                        str = "3";
                        int_1 = new int[buffer.Length];
                        byte_2 = this.byte_1;
                        byte_3 = sourceArray;
                        if (buffer == null)
                        {
                            this.Code = RegisterManager.errCode;
                            return null;
                        }
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            if ((this.byte_1[i] ^ buffer[i]) >= 0x10)
                            {
                                this.ilog_0.ErrorFormat("I:{0}", BitConverter.ToString(destinationArray));
                                this.ilog_0.ErrorFormat("R:{0}", BitConverter.ToString(this.byte_1));
                                this.ilog_0.ErrorFormat("O:{0}", BitConverter.ToString(buffer));
                            }
                            int_1[this.byte_1[i] ^ buffer[i]] = i;
                        }
                        str = "4";
                        if (sourceArray.Length == 0x11)
                        {
                            int.TryParse(strArray[0x10], out int_2);
                        }
                    }
                    else if (int_1 == null)
                    {
                        for (int j = 0; j < 0x10; j++)
                        {
                            int_1[j] = new Random().Next(0x10);
                        }
                        this.ilog_0.Warn("索引异常");
                    }
                    this.string_2 = strArray[int_1[0]];
                    strArray[int_1[0]] = this.string_2;
                    this.Xsdjbh = strArray[int_1[1]];
                    strArray[int_1[1]] = this.string_48;
                    this.Gfmc = strArray[int_1[2]];
                    strArray[int_1[2]] = this.Gfmc;
                    this.Xfdzdh = strArray[int_1[3]];
                    strArray[int_1[3]] = this.string_39;
                    this.Xfyhzh = strArray[int_1[4]];
                    strArray[int_1[4]] = this.string_40;
                    this.string_1 = strArray[int_1[5]];
                    strArray[int_1[5]] = this.string_1;
                    this.Bz = strArray[int_1[6]];
                    strArray[int_1[6]] = this.string_41;
                    this.Gfdzdh = strArray[int_1[7]];
                    strArray[int_1[7]] = this.string_37;
                    this.Gfyhzh = strArray[int_1[8]];
                    strArray[int_1[8]] = this.string_38;
                    this.Fhr = strArray[int_1[9]];
                    strArray[int_1[9]] = this.string_44;
                    if (strArray[int_1[10]] != null)
                    {
                        this.string_54 = strArray[int_1[10]];
                        strArray[int_1[10]] = this.string_54;
                    }
                    this.Skr = strArray[int_1[11]];
                    strArray[int_1[11]] = this.string_43;
                    this.Gfsh = strArray[int_1[12]];
                    strArray[int_1[12]] = this.Gfsh;
                    this.Kpr = strArray[int_1[13]];
                    strArray[int_1[13]] = this.string_42;
                    if (strArray[int_1[14]] != null)
                    {
                        this.string_51 = strArray[int_1[14]];
                        strArray[int_1[14]] = this.string_51;
                    }
                    if (strArray[int_1[15]] != null)
                    {
                        this.string_53 = strArray[int_1[15]];
                        strArray[int_1[15]] = this.string_53;
                    }
                    this.object_0 = new object[] { sourceArray, strArray };
                    int_2--;
                    str = "5";
                }
                if (this.int_0 == 0)
                {
                    InvoiceHandler handler = new InvoiceHandler();
                    if (this.bool_10)
                    {
                        if (!Class34.smethod_10(this.GetHjJeNotHs(), Struct40.string_0))
                        {
                            this.Code = "A089";
                            return null;
                        }
                    }
                    else
                    {
                        str = "6";
                        this.method_0();
                        str = "7";
                        //逻辑修改：红字开票时去掉校验
                        if (!InternetWare.Config.Constants.IsTest)
                        {
                            if (!this.CheckInvoice())
                            {
                                return null;
                            }
                        }
                        str = "8";
                        string str5 = "0000";
                        if (((this.fplx_0 != FPLX.JDCFP) || this.bool_0) && (this.string_7.Length > 0))
                        {
                            str5 = handler.CheckTaxCode(this.string_7, this.fplx_0);
                        }
                        if (!str5.Equals("0000"))
                        {
                            this.Code = str5;
                            this.Params = new string[] { (this.fplx_0 == FPLX.HYFP) ? "实际受票方纳税人识别号" : "购买方纳税人识别号" };
                            return null;
                        }
                        if ((this.zyfp_LX_0 == ZYFP_LX.NCP_SG) && (this.string_4.Length > 0))
                        {
                            str5 = handler.CheckTaxCode(this.string_4, this.fplx_0);
                            if (!str5.Equals("0000"))
                            {
                                this.Code = str5;
                                this.Params = new string[] { "销方纳税人识别号（收购发票）" };
                                return null;
                            }
                        }
                        if (this.string_10.Length > 0)
                        {
                            str5 = this.bool_0 ? handler.CheckZzjgdm_New(this.string_10) : handler.CheckZzjgdm(this.string_10);
                        }
                        if (!str5.Equals("0000"))
                        {
                            this.Code = str5;
                            this.Params = new string[] { "身份证号码/组织机构代码" };
                            return null;
                        }
                        if (this.string_30.Length > 0)
                        {
                            str5 = handler.CheckTaxCode(this.string_30, this.fplx_0);
                        }
                        if (!str5.Equals("0000"))
                        {
                            this.Code = str5;
                            this.Params = new string[] { "收货人纳税人识别号" };
                            return null;
                        }
                        if (this.string_32.Length > 0)
                        {
                            str5 = handler.CheckTaxCode(this.string_32, this.fplx_0);
                        }
                        if (!str5.Equals("0000"))
                        {
                            this.Code = str5;
                            this.Params = new string[] { "发货人纳税人识别号" };
                            return null;
                        }
                    }
                    str = "9";
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    string taxCode = card.TaxCode;
                    if ((this.bool_6 && !bool_3) && !this.bool_4)
                    {
                        if (string.IsNullOrEmpty(this.string_45) && ((this.fplx_0 == FPLX.ZYFP) || ((this.fplx_0 == FPLX.HYFP) && !card.QYLX.ISTLQY)))
                        {
                            this.Code = "A085";
                            return null;
                        }
                        if ((string.IsNullOrEmpty(this.string_46) || string.IsNullOrEmpty(this.string_47)) && ((((this.fplx_0 == FPLX.PTFP) || (this.fplx_0 == FPLX.JDCFP)) || (this.fplx_0 == FPLX.DZFP)) || (this.fplx_0 == FPLX.JSFP)))
                        {
                            this.Code = "A087";
                            return null;
                        }
                        int num = 0;
                        switch (this.fplx_0)
                        {
                            case FPLX.ZYFP:
                                num = 1;
                                break;

                            case FPLX.PTFP:
                                num = 0;
                                break;

                            case FPLX.HYFP:
                                num = 2;
                                break;

                            case FPLX.JDCFP:
                                num = 3;
                                break;

                            case FPLX.JSFP:
                                num = 0;
                                break;

                            case FPLX.DZFP:
                                num = 4;
                                break;
                        }
                        str = "10";
                        string str4 = FPLX2Str(this.fplx_0);
                        if (!string.IsNullOrEmpty(this.string_45) && !NotesUtil.CheckTzdh(this.string_45, str4))
                        {
                            this.Code = "A075";
                            return null;
                        }
                        string str8 = this.string_46 + ((this.string_47 == null) ? "" : this.string_47.PadLeft(8, '0'));
                        if (((str8.Length > 0) && !Class34.smethod_6(str8)) && ((str8.Length != 0x12) && (str8.Length != 20)))
                        {
                            this.Code = "A076";
                            return null;
                        }
                        str = "11";
                        string a = NotesUtil.GetInfo(this.string_41, num, "");
                        if (a != string.Empty)
                        {
                            if ((((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.HYFP)) && !string.Equals(a, this.string_45)) && ((this.fplx_0 != FPLX.HYFP) || !card.QYLX.ISTLQY))
                            {
                                this.Code = "A090";
                                this.Params = new string[] { this.string_45 };
                                return null;
                            }
                            if ((((this.fplx_0 == FPLX.PTFP) || (this.fplx_0 == FPLX.JDCFP)) || ((this.fplx_0 == FPLX.DZFP) || (this.fplx_0 == FPLX.JSFP))) && !string.Equals(a, this.string_46 + this.string_47.PadLeft(8, '0')))
                            {
                                this.Code = "A091";
                                this.Params = new string[] { this.string_46, this.string_47.PadLeft(8, '0') };
                                return null;
                            }
                        }
                        else
                        {
                            if ((this.fplx_0 != FPLX.ZYFP) && (this.fplx_0 != FPLX.HYFP))
                            {
                                this.Code = "A097";
                                this.Params = new string[] { this.string_46, this.string_47.PadLeft(8, '0') };
                                return null;
                            }
                            if ((this.fplx_0 != FPLX.HYFP) || !card.QYLX.ISTLQY)
                            {
                                this.Code = "A096";
                                this.Params = new string[] { this.string_45 };
                                return null;
                            }
                        }
                    }
                    str = "12";
                    if (((!this.bool_10 && (taxCode.Length == 15)) && (taxCode.Substring(8, 2).ToUpper().Equals("DK") && !this.bool_4)) && !bool_3)
                    {
                        string str6;
                        string str10;
                        if ((this.fplx_0 == FPLX.ZYFP) && string.IsNullOrEmpty(this.string_40))
                        {
                            this.Code = "A079";
                            return null;
                        }
                        if (string.IsNullOrEmpty(this.string_39))
                        {
                            this.Code = "A080";
                            return null;
                        }
                        if (string.IsNullOrEmpty(this.string_8))
                        {
                            this.Code = "A081";
                            return null;
                        }
                        if (string.IsNullOrEmpty(this.string_9))
                        {
                            this.Code = "A088";
                            return null;
                        }
                        string str9 = handler.CheckDkqysh(this.string_9);
                        if (!str9.Equals("0000"))
                        {
                            this.Code = str9;
                            return null;
                        }
                        if (!NotesUtil.GetDKQYFromInvNotes(this.string_41, out str6, out str10).Equals("0000"))
                        {
                            this.Code = "A084";
                            return null;
                        }
                        if (!string.Equals(str6, this.string_9))
                        {
                            this.Code = "A082";
                            this.Params = new string[] { this.string_9 };
                            return null;
                        }
                        if (!string.Equals(Class34.smethod_23(str10, Struct40.int_1, false, true), this.string_8))
                        {
                            this.Code = "A083";
                            this.Params = new string[] { this.string_8 };
                            return null;
                        }
                    }
                    str = "13";
                }
                Fpxx fpxx2 = new Fpxx {
                    bmbbbh = this.string_57,
                    fpdm = this.string_1,
                    fphm = this.string_2,
                    mw = this.string_49,
                    jym = this.string_50,
                    xsdjbh = this.string_48,
                    gfmc = this.string_6
                };
                if (this.fplx_0 != FPLX.JSFP)
                {
                    fpxx2.gfdzdh = this.string_37;
                    fpxx2.gfyhzh = this.string_38;
                    fpxx2.xfdzdh = this.string_39;
                    fpxx2.xfyhzh = this.string_40;
                    fpxx2.kpr = this.string_42;
                    fpxx2.fhr = this.string_44;
                }
                fpxx2.bz = this.Bz;
                if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                {
                    string str2 = this.method_1();
                    if (fpxx2.bz.IndexOf(str2) < 0)
                    {
                        fpxx2.bz = str2 + fpxx2.bz;
                    }
                }
                fpxx2.isNewJdcfp = this.bool_0;
                fpxx2.gfsh = this.string_7;
                fpxx2.je = this.GetHjJeNotHs();
                fpxx2.se = this.GetHjSe();
                fpxx2.sLv = this.string_51;
                fpxx2.isBlankWaste = this.bool_10;
                fpxx2.xfsh = this.string_4;
                fpxx2.xfmc = this.string_5;
                fpxx2.fplx = this.fplx_0;
                fpxx2.hzfw = this.bool_11;
                fpxx2.skr = this.string_43;
                fpxx2.Zyfplx = this.zyfp_LX_0;
                fpxx2.dy_mb = this.string_55;
                fpxx2.Mxxx = this.method_14(false);
                str = "14";
                if (this.bool_6)
                {
                    fpxx2.Qdxx = null;
                }
                else
                {
                    fpxx2.Qdxx = this.method_15(false);
                }
                if ((this.zyfp_LX_0 == ZYFP_LX.JZ_50_15) || (this.zyfp_LX_0 == ZYFP_LX.CEZS))
                {
                    if (fpxx2.Qdxx == null)
                    {
                        goto Label_0F4C;
                    }
                    num2 = 0;
                    while (num2 < fpxx2.Qdxx.Count)
                    {
                        Dictionary<SPXX, string> spxx = fpxx2.Qdxx[num2];
                        if ((!string.IsNullOrEmpty(spxx[SPXX.KCE]) && !Class34.smethod_10(spxx[SPXX.KCE], Struct40.string_0)) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                        {
                            goto Label_0F1D;
                        }
                        if (spxx[SPXX.HSJBZ].Equals("1"))
                        {
                            spxx[SPXX.HSJBZ] = "0";
                            if (spxx[SPXX.DJ].Length > 0)
                            {
                                if (((this.zyfp_LX_0 == ZYFP_LX.CEZS) && !string.IsNullOrEmpty(spxx[SPXX.JE])) && !string.IsNullOrEmpty(spxx[SPXX.SL]))
                                {
                                    spxx[SPXX.DJ] = Class34.smethod_14(spxx[SPXX.JE], spxx[SPXX.SL], Struct40.int_46);
                                }
                                else
                                {
                                    spxx[SPXX.DJ] = Class34.smethod_3(this.zyfp_LX_0, spxx[SPXX.DJ], Struct40.int_46, spxx, null);
                                }
                                spxx[SPXX.DJ] = Class34.smethod_24(spxx[SPXX.DJ], Struct40.int_47, false);
                            }
                        }
                        num2++;
                    }
                }
                goto Label_10B1;
            Label_0F1D:
                this.Code = "A226";
                this.Params = new string[] { Convert.ToString((int) (num2 + 1)) };
                return null;
            Label_0F4C:
                if (fpxx2.Mxxx != null)
                {
                    num4 = 0;
                    while (num4 < fpxx2.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary2 = fpxx2.Mxxx[num4];
                        if ((!string.IsNullOrEmpty(dictionary2[SPXX.KCE]) && !Class34.smethod_10(dictionary2[SPXX.KCE], Struct40.string_0)) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                        {
                            goto Label_1082;
                        }
                        if (dictionary2[SPXX.HSJBZ].Equals("1"))
                        {
                            dictionary2[SPXX.HSJBZ] = "0";
                            if (dictionary2[SPXX.DJ].Length > 0)
                            {
                                if (((this.zyfp_LX_0 == ZYFP_LX.CEZS) && !string.IsNullOrEmpty(dictionary2[SPXX.JE])) && !string.IsNullOrEmpty(dictionary2[SPXX.SL]))
                                {
                                    dictionary2[SPXX.DJ] = Class34.smethod_14(dictionary2[SPXX.JE], dictionary2[SPXX.SL], Struct40.int_46);
                                }
                                else
                                {
                                    dictionary2[SPXX.DJ] = Class34.smethod_3(this.zyfp_LX_0, dictionary2[SPXX.DJ], Struct40.int_46, dictionary2, null);
                                }
                                dictionary2[SPXX.DJ] = Class34.smethod_24(dictionary2[SPXX.DJ], Struct40.int_47, false);
                            }
                        }
                        num4++;
                    }
                }
                goto Label_10B1;
            Label_1082:
                this.Code = "A226";
                this.Params = new string[] { Convert.ToString((int) (num4 + 1)) };
                return null;
            Label_10B1:
                if ((this.bool_1 && (fpxx2.Mxxx != null)) && !this.method_10(fpxx2.Mxxx))
                {
                    return null;
                }
                if ((fpxx2.Qdxx != null) && (fpxx2.Qdxx.Count > 0))
                {
                    fpxx2.zyspmc = fpxx2.Qdxx[0][SPXX.SPMC].Trim();
                    fpxx2.zyspsm = fpxx2.Qdxx[0][SPXX.SPSM].Trim();
                }
                else if ((fpxx2.Mxxx != null) && (fpxx2.Mxxx.Count > 0))
                {
                    fpxx2.zyspmc = fpxx2.Mxxx[0][SPXX.SPMC].Trim();
                    fpxx2.zyspsm = fpxx2.Mxxx[0][SPXX.SPSM].Trim();
                }
                if ((this.fplx_0 == FPLX.JDCFP) && this.bool_12)
                {
                    fpxx2.zyspmc = this.string_58;
                    fpxx2.zyspsm = this.string_59;
                }
                str = "15";
                fpxx2.isRed = this.bool_6;
                fpxx2.redNum = this.string_45;
                fpxx2.blueFpdm = this.string_46;
                fpxx2.blueFphm = this.string_47;
                fpxx2.data = this.object_0;
                if (fpxx2.fplx == FPLX.HYFP)
                {
                    fpxx2.cyrnsrsbh = this.string_4;
                    fpxx2.cyrmc = this.string_5;
                    fpxx2.spfnsrsbh = this.string_7;
                    fpxx2.shrmc = this.string_29;
                    fpxx2.shrnsrsbh = this.string_30;
                    fpxx2.fhrmc = this.string_31;
                    fpxx2.fhrnsrsbh = this.string_32;
                    fpxx2.qyd = this.string_33;
                    fpxx2.yshwxx = this.string_34;
                    fpxx2.jqbh = this.string_26;
                    fpxx2.czch = this.string_35;
                    fpxx2.ccdw = this.string_36;
                    fpxx2.zgswjgdm = this.string_27;
                    fpxx2.zgswjgmc = this.string_28;
                }
                else if (fpxx2.fplx == FPLX.JDCFP)
                {
                    fpxx2.jqbh = this.string_26;
                    fpxx2.sfzhm = this.string_10;
                    fpxx2.cllx = this.string_11;
                    fpxx2.cpxh = this.string_12;
                    fpxx2.cd = this.string_13;
                    fpxx2.hgzh = this.string_14;
                    fpxx2.jkzmsh = this.string_15;
                    fpxx2.sjdh = this.string_16;
                    fpxx2.fdjhm = this.string_17;
                    fpxx2.clsbdh = this.string_18;
                    fpxx2.sccjmc = this.string_19;
                    fpxx2.je = this.string_53;
                    fpxx2.se = this.string_54;
                    fpxx2.xfdh = this.string_20;
                    fpxx2.xfzh = this.string_21;
                    fpxx2.xfdz = this.string_22;
                    fpxx2.xfyh = this.string_23;
                    fpxx2.sLv = this.string_51;
                    fpxx2.zgswjgdm = this.string_27;
                    fpxx2.zgswjgmc = this.string_28;
                    fpxx2.dw = this.string_24;
                    fpxx2.xcrs = this.string_25;
                    fpxx2.zyspmc = this.string_58;
                    fpxx2.zyspsm = this.string_59;
                    fpxx2.sbbz = this.string_60;
                }
                else if ((fpxx2.fplx == FPLX.DZFP) || (fpxx2.fplx == FPLX.JSFP))
                {
                    fpxx2.jqbh = this.string_26;
                }
                fpxx2.CustomFields = this.dictionary_0;
                fpxx2.OtherFields = this.dictionary_1;
                fpxx2.bool_0 = this.IsGfSqdFp || this.IsXfSqdFp;
                this.Code = "0000";
                return fpxx2;
            }
            catch (Exception exception)
            {
                this.ilog_0.ErrorFormat("获取发票信息异常({0}):{1}", str, exception.ToString());
                this.Code = "A999";
                this.Params = new string[] { str };
                return null;
            }
        }

        public string GetHjJe()
        {
            if (this.string_53 != null)
            {
                if (!this.bool_2 || ((this.zyfp_LX_0 == ZYFP_LX.HYSY) && (this.fplx_0 == FPLX.ZYFP)))
                {
                    return this.string_53;
                }
                return Class34.smethod_17(this.string_53, this.GetHjSe());
            }
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() != FPHXZ.XHQDZK) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, (!this.bool_2 || (this.zyfp_LX_0 == ZYFP_LX.HYSY)) ? spxx.Je : spxx.method_11());
                }
            }
            if ((this.bool_6 && (this.fplx_0 == FPLX.HYFP)) && Class34.smethod_9(str, Struct40.string_0, false))
            {
                str = Class34.smethod_21(str);
            }
            return str;
        }

        public string GetHjJeHs()
        {
            if (this.string_53 != null)
            {
                return Class34.smethod_17(this.string_53, this.GetHjSe());
            }
            string str = Struct40.string_0;
            if (this.fplx_0 == FPLX.HYFP)
            {
                str = Class34.smethod_17(this.GetHjJeNotHs(), this.GetHjSe());
            }
            else
            {
                foreach (Spxx spxx in this.list_0)
                {
                    if ((spxx.method_5() != FPHXZ.XHQDZK) && (spxx.Je.Length > 0))
                    {
                        str = Class34.smethod_17(str, spxx.method_11());
                    }
                }
            }
            if ((this.bool_6 && (this.fplx_0 == FPLX.HYFP)) && Class34.smethod_9(str, Struct40.string_0, false))
            {
                str = Class34.smethod_21(str);
            }
            return str;
        }

        public string GetHjJeNotHs()
        {
            if (this.string_53 != null)
            {
                return this.string_53;
            }
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() != FPHXZ.XHQDZK) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Je);
                }
            }
            if ((this.bool_6 && (this.fplx_0 == FPLX.HYFP)) && Class34.smethod_9(str, Struct40.string_0, false))
            {
                str = Class34.smethod_21(str);
            }
            return str;
        }

        public string GetHjSe()
        {
            if (this.string_54 != null)
            {
                return this.string_54;
            }
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() != FPHXZ.XHQDZK) && (spxx.Se.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Se);
                }
            }
            if ((this.bool_6 && (this.fplx_0 == FPLX.HYFP)) && Class34.smethod_9(str, Struct40.string_0, false))
            {
                str = Class34.smethod_21(str);
            }
            return str;
        }

        public List<Dictionary<SPXX, string>> GetMxxxs()
        {
            return this.method_14(true);
        }

        public List<Dictionary<SPXX, string>> GetQdxxs()
        {
            return this.method_15(true);
        }

        public Invoice GetRedInvoice(bool bool_13)
        {
            this.Code = "0000";
            if (this.bool_6)
            {
                this.Code = "A010";
                return null;
            }
            byte[] typeByte = TypeByte;
            byte[] destinationArray = new byte[0x20];
            Array.Copy(typeByte, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(typeByte, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes("KP" + DateTime.Now.ToString("F")), destinationArray, buffer3);
            Invoice invoice = new Invoice(true, false, bool_13, this.fplx_0, buffer4, this.string_55) {
                byte_1 = this.byte_1,
                opType_0 = this.opType_0,
                string_57 = this.string_57,
                bool_11 = this.bool_11,
                string_6 = this.string_6,
                string_7 = this.string_7,
                string_38 = this.string_38,
                string_37 = this.string_37,
                string_4 = this.string_4,
                string_5 = this.string_5,
                string_39 = this.string_39,
                string_40 = this.string_40,
                string_41 = this.string_41,
                string_51 = this.string_51
            };
            invoice.SetZyfpLx(this.zyfp_LX_0);
            if (invoice.fplx_0 == FPLX.HYFP)
            {
                invoice.string_29 = this.string_29;
                invoice.string_30 = this.string_30;
                invoice.string_31 = this.string_31;
                invoice.string_32 = this.string_32;
                invoice.string_33 = this.string_33;
                invoice.string_34 = this.string_34;
                invoice.string_26 = this.string_26;
                invoice.string_35 = this.string_35;
                invoice.string_36 = this.string_36;
                invoice.string_27 = this.string_27;
                invoice.string_28 = this.string_28;
            }
            else if (invoice.fplx_0 == FPLX.JDCFP)
            {
                invoice.string_26 = this.string_26;
                invoice.string_10 = this.string_10;
                invoice.string_11 = this.string_11;
                invoice.string_12 = this.string_12;
                invoice.string_13 = this.string_13;
                invoice.string_14 = this.string_14;
                invoice.string_15 = this.string_15;
                invoice.string_16 = this.string_16;
                invoice.string_17 = this.string_17;
                invoice.string_18 = this.string_18;
                invoice.string_19 = this.string_19;
                invoice.string_53 = Class34.smethod_21(this.string_53);
                invoice.string_54 = Class34.smethod_21(this.string_54);
                invoice.string_20 = this.string_20;
                invoice.string_21 = this.string_21;
                invoice.string_22 = this.string_22;
                invoice.string_23 = this.string_23;
                invoice.string_51 = this.string_51;
                invoice.string_27 = this.string_27;
                invoice.string_28 = this.string_28;
                invoice.string_24 = this.string_24;
                invoice.string_25 = this.string_25;
                invoice.bool_0 = this.bool_0;
                if (!this.bool_0)
                {
                    invoice.string_7 = invoice.string_10;
                }
                invoice.string_58 = this.string_58;
                invoice.string_59 = this.string_59;
                invoice.string_60 = this.string_60;
            }
            else if ((invoice.fplx_0 == FPLX.DZFP) || (invoice.fplx_0 == FPLX.JSFP))
            {
                invoice.string_26 = this.string_26;
            }
            if (this.bool_1)
            {
                Spxx spxx2 = new Spxx(Struct40.string_6, "", this.string_51);
                spxx2.method_8(true);
                spxx2.method_6(FPHXZ.XJDYZSFPQD);
                spxx2.method_13(this.zyfp_LX_0);
                spxx2.Je = Class34.smethod_21(this.GetHjJeNotHs());
                spxx2.Se = Class34.smethod_21(this.GetHjSe());
                if (invoice.InsertSpxx(-1, spxx2) < 0)
                {
                    invoice.string_51 = string.Empty;
                    this.Code = invoice.GetCode();
                    this.Params = invoice.Params;
                    this.ilog_0.Error("添加红字发票商品信息时失败" + invoice.GetCode());
                }
                invoice.string_53 = Class34.smethod_21(this.string_53);
                invoice.string_54 = Class34.smethod_21(this.string_54);
                return invoice;
            }
            string str10 = "";
            int num3 = 0;
            int num6 = 0;
            string je = "0";
            string se = "0";
            string str6 = "0";
            string str7 = "0";
            for (int i = this.list_0.Count - 1; i >= 0; i--)
            {
                Spxx spxx = new Spxx(this.list_0[i].Spmc, this.list_0[i].Spsm, this.list_0[i].SLv);
                spxx.method_8(true);
                spxx.method_6(this.list_0[i].method_5());
                spxx.Ggxh = this.list_0[i].Ggxh;
                spxx.method_2(this.list_0[i].method_1());
                spxx.JLdw = this.list_0[i].JLdw;
                spxx.method_13(this.list_0[i].method_12());
                spxx.Flbm = this.list_0[i].Flbm;
                spxx.Spbh = this.list_0[i].Spbh;
                spxx.Xsyh = this.list_0[i].Xsyh;
                spxx.Yhsm = this.list_0[i].Yhsm;
                spxx.Lslvbs = this.list_0[i].Lslvbs;
                spxx.Kce = this.list_0[i].Kce;
                if (spxx.method_5() == FPHXZ.SPXX)
                {
                    spxx.Dj = this.list_0[i].Dj;
                    if (this.fplx_0 == FPLX.HYFP)
                    {
                        spxx.SL = (this.list_0[i].SL.Length == 0) ? "" : this.list_0[i].SL;
                        spxx.Je = this.list_0[i].Je;
                        spxx.Se = this.list_0[i].Se;
                    }
                    else
                    {
                        spxx.SL = (this.list_0[i].SL.Length == 0) ? "" : Class34.smethod_21(this.list_0[i].SL);
                        spxx.Je = Class34.smethod_21(this.list_0[i].Je);
                        spxx.Se = Class34.smethod_21(this.list_0[i].Se);
                    }
                    if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                    {
                        if (string.IsNullOrEmpty(this.list_0[i].Kce))
                        {
                            string str = Class34.smethod_0(this.string_41);
                            if ((i <= 0) && !string.IsNullOrEmpty(str))
                            {
                                spxx.Kce = Class34.smethod_21(str);
                            }
                            else if (Class34.smethod_10(this.list_0[i].SLv, Struct40.string_0))
                            {
                                spxx.Kce = "0";
                            }
                            else
                            {
                                spxx.Kce = Class34.smethod_21(Class34.smethod_18(Class34.smethod_17(this.list_0[i].Je, this.list_0[i].Se), Class34.smethod_11(Class34.smethod_16(Class34.smethod_17("1", this.list_0[i].SLv), this.list_0[i].SLv), this.list_0[i].Se, Struct40.int_50)));
                            }
                        }
                        else
                        {
                            spxx.Kce = Class34.smethod_21(this.list_0[i].Kce);
                        }
                    }
                    else
                    {
                        spxx.Kce = "0";
                    }
                    if (invoice.InsertSpxx(0, spxx) < 0)
                    {
                        this.Code = invoice.GetCode();
                        this.Params = invoice.Params;
                        this.ilog_0.Error("添加红字发票商品信息时失败" + invoice.GetCode());
                    }
                }
                else if (spxx.method_5() == FPHXZ.ZKXX)
                {
                    string spmc = spxx.Spmc;
                    num3 = int.Parse(this.GetZkBySpmc(spmc, i)[1]);
                    je = this.list_0[i].Je;
                    se = this.list_0[i].Se;
                    string str5 = "0";
                    for (int j = 1; j <= num3; j++)
                    {
                        if ((i - j) >= 0)
                        {
                            str5 = Class34.smethod_17(str5, this.list_0[i - j].Je);
                        }
                        else
                        {
                            this.ilog_0.ErrorFormat("被折扣商品行越界：{0},{1},{2}", i, j, this.list_0.Count);
                        }
                    }
                    str10 = Class34.smethod_16(Class34.smethod_21(this.list_0[i].Je), str5);
                    str6 = "0";
                    str7 = "0";
                    num6 = 0;
                }
                else if (spxx.method_5() == FPHXZ.SPXX_ZK)
                {
                    num6++;
                    spxx.SL = this.list_0[i].SL;
                    if (num6 < num3)
                    {
                        string str9;
                        string str11 = Class34.smethod_12(this.list_0[i].Je, str10, Struct40.int_50);
                        str6 = Class34.smethod_17(str6, str11);
                        spxx.Je = Class34.smethod_18(this.list_0[i].Je, str11);
                        if (spxx.method_12() == ZYFP_LX.HYSY)
                        {
                            str9 = Class34.smethod_15(Class34.smethod_13(spxx.Je, spxx.SLv), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                        }
                        else if ((spxx.method_12() != ZYFP_LX.JZ_50_15) && (spxx.method_12() != ZYFP_LX.CEZS))
                        {
                            str9 = Class34.smethod_12(spxx.Je, spxx.SLv, Struct40.int_50);
                        }
                        else
                        {
                            str9 = Class34.smethod_1(spxx.method_12(), spxx.Je, Struct40.int_50, spxx);
                        }
                        str7 = Class34.smethod_17(str7, Class34.smethod_18(this.list_0[i].Se, str9));
                        spxx.Se = str9;
                    }
                    else
                    {
                        spxx.Je = Class34.smethod_17(Class34.smethod_17(je, str6), this.list_0[i].Je);
                        spxx.Se = Class34.smethod_17(Class34.smethod_17(se, str7), this.list_0[i].Se);
                    }
                    if (spxx.SL.Length > 0)
                    {
                        if (spxx.method_1())
                        {
                            spxx.Dj = Class34.smethod_14(Class34.smethod_17(spxx.Je, spxx.Se), spxx.SL, Struct40.int_46);
                        }
                        else
                        {
                            spxx.Dj = Class34.smethod_14(spxx.Je, spxx.SL, Struct40.int_46);
                        }
                    }
                    else
                    {
                        spxx.Dj = string.Empty;
                    }
                    spxx.SL = (spxx.SL.Length == 0) ? string.Empty : Class34.smethod_21(spxx.SL);
                    spxx.Je = Class34.smethod_21(spxx.Je);
                    spxx.Se = Class34.smethod_21(spxx.Se);
                    spxx.method_6(FPHXZ.SPXX);
                    if (!Class34.smethod_9(spxx.Je, Struct40.string_0, true))
                    {
                        if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                        {
                            if (string.IsNullOrEmpty(this.list_0[i].Kce))
                            {
                                string str8 = Class34.smethod_0(this.string_41);
                                if ((i <= 0) && !string.IsNullOrEmpty(str8))
                                {
                                    spxx.Kce = Class34.smethod_21(str8);
                                }
                                else
                                {
                                    spxx.Kce = Class34.smethod_21(Class34.smethod_18(Class34.smethod_17(this.list_0[i].Je, this.list_0[i].Se), Class34.smethod_11(Class34.smethod_16(Class34.smethod_17("1", this.list_0[i].SLv), this.list_0[i].SLv), this.list_0[i].Se, Struct40.int_50)));
                                }
                            }
                            else
                            {
                                spxx.Kce = Class34.smethod_21(this.list_0[i].Kce);
                            }
                        }
                        else
                        {
                            spxx.Kce = "0";
                        }
                        if (invoice.InsertSpxx(0, spxx) < 0)
                        {
                            this.Code = invoice.GetCode();
                            this.Params = invoice.Params;
                            this.ilog_0.Error("添加红字发票商品信息时失败" + invoice.GetCode());
                        }
                    }
                }
            }
            int num4 = 1;
            while (num4 < invoice.list_0.Count)
            {
                if (!string.Equals(invoice.list_0[0].SLv, invoice.list_0[num4].SLv))
                {
                    invoice.string_51 = string.Empty;
                    break;
                }
                num4++;
            }
            if (num4 == invoice.list_0.Count)
            {
                invoice.string_51 = invoice.list_0[0].SLv;
            }
            else if (invoice.fplx_0 != FPLX.JDCFP)
            {
                invoice.string_51 = string.Empty;
            }
            invoice.string_53 = Class34.smethod_21(this.string_53);
            invoice.string_54 = Class34.smethod_21(this.string_54);
            return invoice;
        }

        public string GetSpHjJe()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if (((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, (!this.bool_2 || (this.zyfp_LX_0 == ZYFP_LX.HYSY)) ? spxx.Je : spxx.method_11());
                }
            }
            return str;
        }

        public string GetSpHjJeNotHs()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if (((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Je);
                }
            }
            return str;
        }

        public string GetSpHjSe()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if (((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)) && (spxx.Se.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Se);
                }
            }
            return str;
        }

        public Dictionary<SPXX, string> GetSpxx(int int_3)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return null;
            }
            Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
            Spxx spxx = this.list_0[int_3];
            dictionary.Add(SPXX.SPMC, spxx.Spmc);
            dictionary.Add(SPXX.SPBH, spxx.Spbh);
            dictionary.Add(SPXX.XTHASH, spxx.XTHash);
            dictionary.Add(SPXX.SPSM, spxx.Spsm);
            dictionary.Add(SPXX.GGXH, spxx.Ggxh);
            dictionary.Add(SPXX.JLDW, spxx.JLdw);
            dictionary.Add(SPXX.SL, spxx.SL);
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                dictionary.Add(SPXX.DJ, spxx.method_10());
                dictionary.Add(SPXX.JE, spxx.Je);
            }
            else
            {
                dictionary.Add(SPXX.JE, this.bool_2 ? spxx.method_11() : spxx.Je);
                dictionary.Add(SPXX.DJ, this.bool_2 ? spxx.method_10() : spxx.method_9());
            }
            dictionary.Add(SPXX.SLV, spxx.SLv);
            dictionary.Add(SPXX.SE, spxx.Se);
            dictionary.Add(SPXX.FPHXZ, spxx.method_5().ToString("D"));
            dictionary.Add(SPXX.FLBM, spxx.Flbm);
            dictionary.Add(SPXX.XSYH, spxx.Xsyh);
            dictionary.Add(SPXX.YHSM, spxx.Yhsm);
            dictionary.Add(SPXX.LSLVBS, spxx.Lslvbs);
            dictionary.Add(SPXX.KCE, spxx.Kce);
            return dictionary;
        }

        public List<Dictionary<SPXX, string>> GetSpxxs()
        {
            return this.method_12(true);
        }

        public string GetSpxxString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Spxx spxx in this.list_0)
            {
                builder.Append(spxx.ToString());
            }
            return builder.ToString();
        }

        public string GetSpZkJe()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() == FPHXZ.ZKXX) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, (!this.bool_2 || (this.zyfp_LX_0 == ZYFP_LX.HYSY)) ? spxx.Je : spxx.method_11());
                }
            }
            return str;
        }

        public string GetSpZkJeNotHs()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() == FPHXZ.ZKXX) && (spxx.Je.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Je);
                }
            }
            return str;
        }

        public string GetSpZkSe()
        {
            string str = Struct40.string_0;
            foreach (Spxx spxx in this.list_0)
            {
                if ((spxx.method_5() == FPHXZ.ZKXX) && (spxx.Se.Length > 0))
                {
                    str = Class34.smethod_17(str, spxx.Se);
                }
            }
            return str;
        }

        public string GetSqSLv()
        {
            if (InternetWare.Config.Constants.IsTest)
                return "0,0.02,0.03,0.05,0.06,0.11,0.13,0.015,0.17;0,0.02,0.03,0.05,0.06,0.11,0.13,0.015,0.17";
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            switch (this.fplx_0)
            {
                case FPLX.ZYFP:
                    list = Struct40.list_0;
                    list2 = Struct40.list_1;
                    break;

                case FPLX.PTFP:
                    list = Struct40.list_5;
                    list2 = Struct40.list_2;
                    break;

                case FPLX.HYFP:
                    list = Struct40.list_3;
                    break;

                case FPLX.JDCFP:
                    list = Struct40.list_4;
                    break;

                case FPLX.JSFP:
                    list = Struct40.list_6;
                    break;

                case FPLX.DZFP:
                    list = Struct40.list_7;
                    break;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(",");
                }
                builder.Append(list[i]);
            }
            if ((list.Count > 0) && (list2.Count > 0))
            {
                builder.Append(";");
            }
            for (int j = 0; j < list2.Count; j++)
            {
                if (j > 0)
                {
                    builder.Append(",");
                }
                builder.Append(list2[j]);
            }
            return builder.ToString();
        }

        public string[] GetZkBySpmc(string string_61)
        {
            try
            {
                int index = string_61.IndexOf("(");
                int num2 = string_61.IndexOf("%");
                int num3 = string_61.IndexOf("(");
                int num4 = string_61.IndexOf("折扣行数");
                string str = string_61.Substring(index + 1, (num2 - num3) - 1);
                string str2 = (num4 == 0) ? string_61.Substring(4, index - 4) : "1";
                return new string[] { str, str2 };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string[] GetZkBySpmc(string string_61, int int_3)
        {
            try
            {
                if (string.IsNullOrEmpty(this.string_57))
                {
                    int index = string_61.IndexOf("(");
                    int num2 = string_61.IndexOf("%");
                    int num3 = string_61.IndexOf("(");
                    int num4 = string_61.IndexOf("折扣行数");
                    string str = string_61.Substring(index + 1, (num2 - num3) - 1);
                    string str2 = (num4 == 0) ? string_61.Substring(4, index - 4) : "1";
                    return new string[] { str, str2 };
                }
                return new string[] { this.list_0[int_3].SLv, "1" };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetZke(int int_3, int int_4, string string_61)
        {
            string_61 = Class34.smethod_19(string_61, Struct40.int_54);
            if (Class34.smethod_9(string_61, "1.00000", false))
            {
                this.Code = "A007";
                return null;
            }
            string zkSpJe = this.GetZkSpJe(int_3, int_4);
            if (zkSpJe == null)
            {
                return null;
            }
            this.Code = "0000";
            return Class34.smethod_21(Class34.smethod_12(Class34.smethod_19(string_61, Struct40.int_54), zkSpJe, Struct40.int_50));
        }

        public string GetZkLv(int int_3, int int_4, string string_61)
        {
            string zkSpJe = this.GetZkSpJe(int_3, int_4);
            if (zkSpJe == null)
            {
                return null;
            }
            string_61 = Class34.smethod_21(Class34.smethod_19(string_61, Struct40.int_50));
            if (Class34.smethod_9(string_61, zkSpJe, false))
            {
                this.Code = "A008";
                return null;
            }
            this.Code = "0000";
            return Class34.smethod_15(Class34.smethod_19(string_61, Struct40.int_50), zkSpJe, Struct40.int_54);
        }

        public string GetZkSpJe(int int_3, int int_4)
        {
            if (this.bool_6)
            {
                this.Code = "A011";
                return null;
            }
            if (((int_4 < 1) || (int_4 > (int_3 + 1))) || (this.list_0.Count == 0))
            {
                this.Code = "A006";
                return null;
            }
            string str = Struct40.string_0;
            for (int i = 0; i < int_4; i++)
            {
                int num2 = int_3 - i;
                if (string.IsNullOrEmpty(this.list_0[num2].Je))
                {
                    this.Code = "A217";
                    return null;
                }
                if ((num2 >= 0) && (num2 < this.list_0.Count))
                {
                    if (this.bool_2 && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                    {
                        str = Class34.smethod_17(this.list_0[num2].method_11(), str);
                    }
                    else
                    {
                        str = Class34.smethod_17(this.list_0[num2].Je, str);
                    }
                }
            }
            if (Class34.smethod_10(str, Struct40.string_0))
            {
                this.Code = "A006";
                return null;
            }
            return str;
        }

        public int InsertSpxx(int int_3, Spxx spxx_0)
        {
            if (this.method_9())
            {
                decimal num2;
                if (spxx_0.method_12() != this.zyfp_LX_0)
                {
                    this.Code = "A021";
                    return -1;
                }
                if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.Code = "A036";
                    return -1;
                }
                if (!string.IsNullOrEmpty(spxx_0.Je) && Class34.smethod_10(spxx_0.Je, Struct40.string_0))
                {
                    this.Code = "A121";
                    this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                    return -1;
                }
                if ((spxx_0.SLv.Length > 0) && !decimal.TryParse(spxx_0.SLv, out num2))
                {
                    this.Code = "A116";
                    this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                    return -1;
                }
                if (spxx_0.SLv.Length > 0)
                {
                    spxx_0.SLv = Class34.smethod_20(spxx_0.SLv, Struct40.int_55);
                }
                if (decimal.TryParse(spxx_0.Dj, out num2) && decimal.Equals(num2, 0M))
                {
                    spxx_0.Dj = string.Empty;
                }
                if (decimal.TryParse(spxx_0.SL, out num2) && decimal.Equals(num2, 0M))
                {
                    spxx_0.SL = string.Empty;
                }
                if (string.IsNullOrEmpty(spxx_0.SLv) && !string.IsNullOrEmpty(spxx_0.Dj))
                {
                    this.Code = "A133";
                    this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                    return -1;
                }
                if (string.IsNullOrEmpty(spxx_0.SLv) && !string.IsNullOrEmpty(spxx_0.SL))
                {
                    this.Code = "A134";
                    this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                    return -1;
                }
                spxx_0.method_8(this.bool_6);
                if ((spxx_0.Je != null) && (spxx_0.Je.Length > 0))
                {
                    if (this.bool_6)
                    {
                        if ((this.fplx_0 != FPLX.HYFP) && Class34.smethod_9(spxx_0.Je, Struct40.string_0, false))
                        {
                            this.Code = "A104";
                            this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                            return -1;
                        }
                        if ((this.fplx_0 == FPLX.HYFP) && !Class34.smethod_9(spxx_0.Je, Struct40.string_0, true))
                        {
                            this.Code = "A124";
                            this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                            return -1;
                        }
                    }
                    else if (((spxx_0.method_5() != FPHXZ.ZKXX) && (spxx_0.method_5() != FPHXZ.XHQDZK)) && !Class34.smethod_9(spxx_0.Je, Struct40.string_0, true))
                    {
                        this.Code = "A105";
                        this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                        return -1;
                    }
                }
                if (!string.IsNullOrEmpty(spxx_0.SL))
                {
                    string s = Class34.smethod_24(spxx_0.SL, Struct40.int_49, false);
                    if (s == null)
                    {
                        this.Code = "A118";
                        this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                        return -1;
                    }
                    spxx_0.SL = decimal.Round(decimal.Parse(s), Struct40.int_48, MidpointRounding.AwayFromZero).ToString();
                }
                if (!string.IsNullOrEmpty(spxx_0.Dj))
                {
                    string str3 = Class34.smethod_24(spxx_0.Dj, Struct40.int_47, false);
                    if (str3 == null)
                    {
                        this.Code = "A114";
                        this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                        return -1;
                    }
                    spxx_0.Dj = decimal.Round(decimal.Parse(str3), Struct40.int_46, MidpointRounding.AwayFromZero).ToString();
                }
                if (!string.IsNullOrEmpty(spxx_0.Je))
                {
                    spxx_0.Je = Class34.smethod_19(spxx_0.Je, Struct40.int_50);
                    if ((spxx_0.Je.Length > Struct40.int_51) || ((Struct40.int_52 > 0) && (spxx_0.method_11().Length > Struct40.int_52)))
                    {
                        this.Code = "A123";
                        this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                        return -1;
                    }
                }
                if (!string.IsNullOrEmpty(spxx_0.Se))
                {
                    spxx_0.Se = Class34.smethod_19(spxx_0.Se, Struct40.int_50);
                    if (spxx_0.Se.Length > Struct40.int_53)
                    {
                        this.Code = "A117";
                        this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                        return -1;
                    }
                }
                if ((this.zyfp_LX_0 == ZYFP_LX.CEZS) && string.IsNullOrEmpty(spxx_0.Kce))
                {
                    if (spxx_0.method_5() != FPHXZ.ZKXX)
                    {
                        if ((string.IsNullOrEmpty(spxx_0.Kce) && !string.IsNullOrEmpty(spxx_0.Je)) && (!string.IsNullOrEmpty(spxx_0.Se) && !string.IsNullOrEmpty(spxx_0.SLv)))
                        {
                            if (Class34.smethod_10(spxx_0.SLv, Struct40.string_0))
                            {
                                spxx_0.Kce = "0";
                            }
                            else
                            {
                                spxx_0.Kce = Class34.smethod_18(Class34.smethod_17(spxx_0.Je, spxx_0.Se), Class34.smethod_11(Class34.smethod_16(Class34.smethod_17("1", spxx_0.SLv), spxx_0.SLv), spxx_0.Se, Struct40.int_50));
                            }
                        }
                    }
                    else
                    {
                        spxx_0.Kce = "0";
                    }
                }
                if (!this.CanAddSpxx(1, false))
                {
                    return -1;
                }
                if (this.list_0.Count == 0)
                {
                    this.string_51 = spxx_0.SLv;
                    if ((this.bool_6 && ((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP))) && string.Equals(spxx_0.SLv, string.Empty))
                    {
                        spxx_0.method_6(FPHXZ.XJDYZSFPQD);
                        spxx_0.Dj = string.Empty;
                        spxx_0.SL = string.Empty;
                        spxx_0.Spmc = Struct40.string_6;
                    }
                    if (!this.method_13(spxx_0.SLv, -1))
                    {
                        return -1;
                    }
                }
                else if (!this.string_51.Equals(spxx_0.SLv))
                {
                    if ((!this.bool_7 || (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)) || (this.zyfp_LX_0 == ZYFP_LX.CEZS))
                    {
                        this.Code = "A005";
                        return -1;
                    }
                    this.string_51 = string.Empty;
                }
                if (spxx_0.method_5() != FPHXZ.XJDYZSFPQD)
                {
                    spxx_0.method_6(FPHXZ.SPXX);
                }
                if (spxx_0.method_3(this.bool_2))
                {
                    if (((spxx_0.method_5() == FPHXZ.SPXX) || (spxx_0.method_5() == FPHXZ.SPXX_ZK)) && (((this.zyfp_LX_0 == ZYFP_LX.SNY) || (this.zyfp_LX_0 == ZYFP_LX.SNY_DDZG)) || ((this.zyfp_LX_0 == ZYFP_LX.RLY) || (this.zyfp_LX_0 == ZYFP_LX.RLY_DDZG))))
                    {
                        spxx_0.JLdw = Struct40.string_13;
                        string b = spxx_0.Spmc.Replace(Struct40.dictionary_0[this.zyfp_LX_0], "") + Struct40.dictionary_0[this.zyfp_LX_0];
                        spxx_0.Spmc = b;
                        if (!string.Equals(spxx_0.Spmc, b))
                        {
                            this.Code = "A125";
                            this.Params = new string[] { Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1)) };
                            return -1;
                        }
                    }
                    if ((int_3 >= 0) && (int_3 < this.list_0.Count))
                    {
                        Spxx spxx = this.list_0[int_3];
                        if ((int_3 > 0) && (((spxx.method_5() == FPHXZ.SPXX_ZK) && (this.list_0[int_3 - 1].method_5() == FPHXZ.SPXX_ZK)) || ((spxx.method_5() == FPHXZ.ZKXX) && (this.list_0[int_3 - 1].method_5() == FPHXZ.SPXX_ZK))))
                        {
                            if (spxx.method_5() == FPHXZ.ZKXX)
                            {
                                this.Code = "A023";
                            }
                            else
                            {
                                this.Code = "A086";
                            }
                            return -1;
                        }
                        this.list_0.Insert(int_3, spxx_0);
                        this.Code = "0000";
                        this.string_53 = null;
                        this.string_54 = null;
                        return int_3;
                    }
                    this.list_0.Add(spxx_0);
                    this.Code = "0000";
                    this.string_53 = null;
                    this.string_54 = null;
                    return (this.list_0.Count - 1);
                }
                this.Code = spxx_0.method_0();
                this.Params = new string[spxx_0.string_0.Length + 1];
                this.Params[0] = Convert.ToString((int) (((int_3 < 0) ? this.list_0.Count : int_3) + 1));
                Array.ConstrainedCopy(spxx_0.string_0, 0, this.Params, 1, spxx_0.string_0.Length);
            }
            return -1;
        }

        public int InsertSpxx(int int_3, string string_61, string string_62, ZYFP_LX zyfp_LX_1)
        {
            Spxx spxx = new Spxx(" ", string_61, string_62, "", "", "", this.bool_2, zyfp_LX_1);
            return this.InsertSpxx(int_3, spxx);
        }

        public int InsertSpxx(int int_3, string string_61, string string_62, string string_63, ZYFP_LX zyfp_LX_1)
        {
            Spxx spxx = new Spxx(string_61, string_62, string_63, "", "", "", this.bool_2, zyfp_LX_1);
            return this.InsertSpxx(int_3, spxx);
        }

        public int InsertSpxx(int int_3, string string_61, string string_62, string string_63, string string_64, string string_65, string string_66, bool bool_13, ZYFP_LX zyfp_LX_1)
        {
            Spxx spxx = new Spxx(string_61, string_62, string_63, string_64, string_65, string_66, bool_13, zyfp_LX_1);
            return this.InsertSpxx(int_3, spxx);
        }

        public bool MakeCardInvoice(Fpxx fpxx_0, bool bool_13 = false)
        {
            if (((!bool_3 && !this.bool_4) && !fpxx_0.bool_0) && (this.opType_0 != OpType.DJ))
            {
                if (this.opType_0 == OpType.KP)
                {
                    DateTime now = DateTime.Now;
                    string str5 = "Aisino.Fwkp.Invoice" + this.string_1 + this.string_2;
                    byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str5));
                    byte[] destinationArray = new byte[0x20];
                    Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                    byte[] buffer7 = new byte[0x10];
                    Array.Copy(bytes, 0x20, buffer7, 0, 0x10);
                    byte[] buffer = AES_Crypt.Decrypt(Convert.FromBase64String(fpxx_0.gfmc), destinationArray, buffer7, null);
                    fpxx_0.gfmc = Encoding.Unicode.GetString(buffer);
                    int index = fpxx_0.gfmc.IndexOf(';');
                    if (index < 0)
                    {
                        this.opType_0 = OpType.ER;
                    }
                    else
                    {
                        buffer = AES_Crypt.Decrypt(Convert.FromBase64String(fpxx_0.gfmc.Substring(0, index)), destinationArray, buffer7, null);
                        if (buffer == null)
                        {
                            this.opType_0 = OpType.ER;
                        }
                        else
                        {
                            DateTime time = DateTime.ParseExact(Encoding.Unicode.GetString(buffer), "F", CultureInfo.CurrentCulture);
                            if ((time.CompareTo(now) <= 0) && (time.CompareTo(now.AddSeconds(-5.0)) >= 0))
                            {
                                fpxx_0.gfmc = fpxx_0.gfmc.Substring(index + 1);
                                if (fpxx_0.fplx == FPLX.HYFP)
                                {
                                    fpxx_0.spfmc = fpxx_0.gfmc;
                                }
                            }
                            else
                            {
                                this.opType_0 = OpType.ER;
                            }
                        }
                    }
                    fpxx_0.data = new object[] { MD5_Crypt.GetHash(fpxx_0.fpdm + "00" + fpxx_0.fphm), new string[0x10], new byte[0x10] };
                }
                else if ((this.opType_0 == OpType.KT) || (this.opType_0 == OpType.KC))
                {
                    string str6 = ((this.opType_0 == OpType.KT) ? "Aisino.Fwkp.Invoice" : "sAxItaO.aIsinO.ZZxF") + this.string_1 + this.string_2;
                    byte[] sourceArray = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str6));
                    byte[] buffer9 = new byte[0x20];
                    Array.Copy(sourceArray, 0, buffer9, 0, 0x20);
                    byte[] buffer10 = new byte[0x10];
                    Array.Copy(sourceArray, 0x20, buffer10, 0, 0x10);
                    byte[] buffer2 = AES_Crypt.Decrypt(Convert.FromBase64String(fpxx_0.gfmc), buffer9, buffer10, null);
                    if (buffer2 == null)
                    {
                        this.opType_0 = OpType.ER;
                    }
                    else
                    {
                        fpxx_0.gfmc = Encoding.Unicode.GetString(buffer2);
                        if (this.opType_0 == OpType.KC)
                        {
                            int length = fpxx_0.gfmc.IndexOf(';');
                            if (length < 0)
                            {
                                this.opType_0 = OpType.ER;
                            }
                            else
                            {
                                string s = fpxx_0.gfmc.Substring(0, length);
                                byte[] buffer3 = new byte[] { 
                                    250, 0xbd, 0xff, 0x67, 0x99, 0xab, 0x11, 0xee, 0xdd, 0xbb, 0x99, 0xee, 0xdd, 0xff, 0x98, 0x67, 
                                    0x29, 0x19, 0x4d, 0x7a, 0x9f, 13, 0x6b, 0xbb, 0x7a, 0x5c, 15, 0x5d, 0x4d, 0x5d, 0x2a, 0x66
                                 };
                                byte[] buffer4 = new byte[] { 0x7f, 90, 11, 0x47, 0x7d, 0x4e, 0x6f, 90, 0x86, 0x94, 0x20, 0x10, 0xff, 0x74, 0xff, 0xaf };
                                buffer2 = AES_Crypt.Decrypt(Convert.FromBase64String(s), buffer3, buffer4, null);
                                if (buffer2 == null)
                                {
                                    this.opType_0 = OpType.ER;
                                }
                                else
                                {
                                    string str3 = Encoding.Unicode.GetString(buffer2);
                                    int num2 = str3.IndexOf(":");
                                    if (num2 < 0)
                                    {
                                        this.opType_0 = OpType.ER;
                                    }
                                    else if (!string.Equals(str3.Substring(0, num2), this.string_1 + this.string_2.PadLeft(15, '0')))
                                    {
                                        this.opType_0 = OpType.ER;
                                    }
                                    else
                                    {
                                        double totalSeconds = DateTime.Now.Subtract(DateTime.Parse("2004-04-10 10:32:54")).TotalSeconds;
                                        double result = 0.0;
                                        double.TryParse(str3.Substring(num2 + 1), out result);
                                        if (Math.Abs((double) (totalSeconds - result)) > 5.0)
                                        {
                                            this.opType_0 = OpType.ER;
                                        }
                                        else
                                        {
                                            fpxx_0.gfmc = fpxx_0.gfmc.Substring(length + 1);
                                        }
                                    }
                                }
                            }
                        }
                        if (fpxx_0.fplx == FPLX.HYFP)
                        {
                            fpxx_0.spfmc = fpxx_0.gfmc;
                        }
                    }
                    fpxx_0.xsdjbh = "";
                    fpxx_0.xfdzdh = "";
                    fpxx_0.xfyhzh = "";
                    fpxx_0.bz = "";
                    fpxx_0.gfdzdh = "";
                    fpxx_0.gfyhzh = "";
                    fpxx_0.fhr = "";
                    fpxx_0.skr = "";
                    fpxx_0.kpr = "";
                    fpxx_0.data = new object[] { byte_3, this.object_0[1], MD5_Crypt.GetHash(fpxx_0.fpdm + ((this.opType_0 == OpType.KT) ? "01" : "02") + fpxx_0.fphm) };
                    fpxx_0.rdmByte = null;
                }
                if (this.opType_0 == OpType.ER)
                {
                    fpxx_0.sLv = "";
                    fpxx_0.gfdzdh = "";
                    fpxx_0.gfmc = "";
                    fpxx_0.gfdzdh = "";
                    fpxx_0.gfyhzh = "";
                    fpxx_0.xfdzdh = "";
                    fpxx_0.xfyhzh = "";
                    fpxx_0.kpr = "";
                    fpxx_0.skr = "";
                    fpxx_0.fhr = "";
                    fpxx_0.xsdjbh = "";
                    fpxx_0.bz = "";
                    fpxx_0.Mxxx = null;
                    fpxx_0.Qdxx = null;
                    this.ilog_0.Error("开票类型非法：" + this.opType_0.ToString());
                }
                TaxCard card = TaxCardFactory.CreateTaxCard();
                InvoiceHandler handler = new InvoiceHandler();
                if ((((this.opType_0 == OpType.KP) && (fpxx_0.fplx == FPLX.PTFP)) && (fpxx_0.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx_0.gfsh.Equals(card.TaxCode) || fpxx_0.gfsh.Equals(card.OldTaxCode)))
                {
                    string gfsh = fpxx_0.gfsh;
                    fpxx_0.gfsh = fpxx_0.xfsh;
                    fpxx_0.xfsh = gfsh;
                    gfsh = fpxx_0.gfmc;
                    fpxx_0.gfmc = fpxx_0.xfmc;
                    fpxx_0.xfmc = gfsh;
                    gfsh = fpxx_0.gfyhzh;
                    fpxx_0.gfyhzh = fpxx_0.xfyhzh;
                    fpxx_0.xfyhzh = gfsh;
                    gfsh = fpxx_0.gfdzdh;
                    fpxx_0.gfdzdh = fpxx_0.xfdzdh;
                    fpxx_0.xfdzdh = gfsh;
                }
                if ((fpxx_0.fplx == FPLX.JDCFP) && !fpxx_0.isNewJdcfp)
                {
                    fpxx_0.gfsh = fpxx_0.sfzhm;
                }
                fpxx_0.bz = Convert.ToBase64String(ToolUtil.GetBytes(fpxx_0.bz));
                fpxx_0.yshwxx = Convert.ToBase64String(ToolUtil.GetBytes(fpxx_0.yshwxx));
                object[] objArray = ServiceFactory.InvokePubService("saxitao.aisino.china.world", new object[] { fpxx_0, bool_13 });
                if ((objArray != null) && !objArray[0].Equals("0000"))
                {
                    this.Code = (string) objArray[0];
                    return false;
                }
                string str = "";
                if (!bool_13)
                {
                    if (!this.bool_10 && !handler.CheckHzfwFpxx(fpxx_0))
                    {
                        this.Code = handler.GetCode();
                        return false;
                    }
                    if ((this.opType_0 == OpType.KT) || (this.opType_0 == OpType.KC))
                    {
                        fpxx_0.gfmc = "";
                        fpxx_0.gfsh = "";
                    }
                    str = handler.method_16(fpxx_0);
                    if (str.Equals("A666"))
                    {
                        string str2 = handler.method_17(fpxx_0, false);
                        if (str2.Equals("0000"))
                        {
                            str = "A200";
                            this.ilog_0.WarnFormat("发票开具核对失败后，作废成功：{0}，{1}", fpxx_0.fpdm, fpxx_0.fphm);
                        }
                        else
                        {
                            str = "A201";
                            this.ilog_0.ErrorFormat("发票开具核对失败后，作废时异常：{0},{1},{2}", str2, fpxx_0.fpdm, fpxx_0.fphm);
                        }
                    }
                }
                else
                {
                    str = handler.method_17(fpxx_0, true);
                }
                fpxx_0.retCode = str;
                if ((!str.Equals("0000") && !str.Equals("A200")) && !str.Equals("A201"))
                {
                    this.Code = str;
                    return false;
                }
                if (str.Equals("0000"))
                {
                    fpxx_0.zfbz = bool_13 || fpxx_0.isBlankWaste;
                }
                if (fpxx_0.fplx == FPLX.JDCFP)
                {
                    Class35 class2 = new Class35();
                    if (fpxx_0.isNewJdcfp)
                    {
                        class2.method_0(fpxx_0);
                    }
                    else
                    {
                        class2.method_1(fpxx_0);
                    }
                }
                ServiceFactory.InvokePubService("saxitao.aisino.china", new object[] { fpxx_0, bool_13 });
                this.Code = str;
                return true;
            }
            this.opType_0 = OpType.ER;
            return false;
        }

        private void method_0()
        {
            this.string_48 = (this.string_48 == null) ? "" : this.string_48.Trim();
            this.string_41 = (this.string_41 == null) ? "" : this.string_41.Trim();
            this.string_37 = (this.string_37 == null) ? "" : this.string_37.Trim();
            this.string_6 = (this.string_6 == null) ? "" : this.string_6.Trim();
            this.string_7 = (this.string_7 == null) ? "" : this.string_7.Trim();
            this.string_38 = (this.string_38 == null) ? "" : this.string_38.Trim();
            this.string_4 = (this.string_4 == null) ? "" : this.string_4.Trim();
            this.string_5 = (this.string_5 == null) ? "" : this.string_5.Trim();
            this.string_39 = (this.string_39 == null) ? "" : this.string_39.Trim();
            this.string_40 = (this.string_40 == null) ? "" : this.string_40.Trim();
            this.string_42 = (this.string_42 == null) ? "" : this.string_42.Trim();
            this.string_43 = (this.string_43 == null) ? "" : this.string_43.Trim();
            this.string_44 = (this.string_44 == null) ? "" : this.string_44.Trim();
            if (this.fplx_0 == FPLX.HYFP)
            {
                this.string_29 = (this.string_29 == null) ? "" : this.string_29.Trim();
                this.string_30 = (this.string_30 == null) ? "" : this.string_30.Trim();
                this.string_31 = (this.string_31 == null) ? "" : this.string_31.Trim();
                this.string_32 = (this.string_32 == null) ? "" : this.string_32.Trim();
                this.string_33 = (this.string_33 == null) ? "" : this.string_33.Trim();
                this.string_34 = (this.string_34 == null) ? "" : this.string_34.Trim();
                this.string_26 = (this.string_26 == null) ? "" : this.string_26.Trim();
                this.string_35 = (this.string_35 == null) ? "" : this.string_35.Trim();
                this.string_36 = (this.string_36 == null) ? "" : this.string_36.Trim();
                this.string_27 = (this.string_27 == null) ? "" : this.string_27.Trim();
                this.string_28 = (this.string_28 == null) ? "" : this.string_28.Trim();
            }
            else if (this.fplx_0 == FPLX.JDCFP)
            {
                this.string_26 = (this.string_26 == null) ? "" : this.string_26.Trim();
                this.string_10 = (this.string_10 == null) ? "" : this.string_10.Trim();
                this.string_11 = (this.string_11 == null) ? "" : this.string_11.Trim();
                this.string_12 = (this.string_12 == null) ? "" : this.string_12.Trim();
                this.string_13 = (this.string_13 == null) ? "" : this.string_13.Trim();
                this.string_14 = (this.string_14 == null) ? "" : this.string_14.Trim();
                this.string_15 = (this.string_15 == null) ? "" : this.string_15.Trim();
                this.string_16 = (this.string_16 == null) ? "" : this.string_16.Trim();
                this.string_17 = (this.string_17 == null) ? "" : this.string_17.Trim();
                this.string_18 = (this.string_18 == null) ? "" : this.string_18.Trim();
                this.string_19 = (this.string_19 == null) ? "" : this.string_19.Trim();
                this.string_20 = (this.string_20 == null) ? "" : this.string_20.Trim();
                this.string_21 = (this.string_21 == null) ? "" : this.string_21.Trim();
                this.string_22 = (this.string_22 == null) ? "" : this.string_22.Trim();
                this.string_23 = (this.string_23 == null) ? "" : this.string_23.Trim();
                this.string_27 = (this.string_27 == null) ? "" : this.string_27.Trim();
                this.string_28 = (this.string_28 == null) ? "" : this.string_28.Trim();
                this.string_24 = (this.string_24 == null) ? "" : this.string_24.Trim();
                this.string_25 = (this.string_25 == null) ? "" : this.string_25.Trim();
            }
            for (int i = 0; i < this.list_0.Count; i++)
            {
                this.list_0[i].Spmc = (this.list_0[i].Spmc == null) ? "" : this.list_0[i].Spmc.Trim();
                this.list_0[i].Ggxh = (this.list_0[i].Ggxh == null) ? "" : this.list_0[i].Ggxh.Trim();
                this.list_0[i].JLdw = (this.list_0[i].JLdw == null) ? "" : this.list_0[i].JLdw.Trim();
                this.list_0[i].Spbh = (this.list_0[i].Spbh == null) ? "" : this.list_0[i].Spbh.Trim();
                this.list_0[i].XTHash = (this.list_0[i].XTHash == null) ? "" : this.list_0[i].XTHash.Trim();
                this.list_0[i].Spsm = (this.list_0[i].Spsm == null) ? "" : this.list_0[i].Spsm.Trim();
            }
        }

        private string method_1()
        {
            if (this.bool_6)
            {
                return (Struct40.string_9 + "。");
            }
            string str = "";
            if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    if (this.list_0[i].method_5() != FPHXZ.ZKXX)
                    {
                        str = str + Class34.smethod_19(this.list_0[i].Kce, Struct40.int_50) + "；";
                    }
                }
                if (this.list_0.Count > 0)
                {
                    str = Struct40.string_9 + "：" + str.Substring(0, str.Length - 1) + "。";
                }
            }
            return str;
        }

        private bool method_10(List<Dictionary<SPXX, string>> mxxx)
        {
            for (int i = 0; i < mxxx.Count; i++)
            {
                Dictionary<SPXX, string> dictionary = mxxx[i];
                string str = dictionary[SPXX.JE];
                string str2 = dictionary[SPXX.SE];
                string str3 = dictionary[SPXX.SLV];
                if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                {
                    str = Class34.smethod_17(str, str2);
                }
                else if (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)
                {
                    str = Class34.smethod_15(Class34.smethod_17(str, str2), "1.05", Struct40.int_50);
                }
                else if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                {
                    str = Class34.smethod_18(str, dictionary[SPXX.KCE]);
                }
                string str4 = Struct40.string_4;
                if ((Class34.smethod_9(str4, Struct40.string_0, true) && (str3.Length > 0)) && !Class34.smethod_7(str, str3, str2, Struct40.int_50, str4))
                {
                    this.Code = "A126";
                    this.Params = new string[] { Convert.ToString((int) (i + 1)), str4 };
                    return false;
                }
                if (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)
                {
                    str = Class34.smethod_17(dictionary[SPXX.JE], str2);
                    str2 = Class34.smethod_12(str2, "1.05", Struct40.int_50);
                }
            }
            return true;
        }

        private bool method_11()
        {
            string hjJeNotHs = "";
            if (!bool_3 && (((this.fplx_0 == FPLX.ZYFP) && (this.zyfp_LX_0 != ZYFP_LX.HYSY)) || (((this.fplx_0 == FPLX.PTFP) || (this.fplx_0 == FPLX.DZFP)) || (this.fplx_0 == FPLX.HYFP))))
            {
                hjJeNotHs = this.GetHjJeNotHs();
                //逻辑修改：红字去掉校验
                if (!InternetWare.Config.Constants.IsTest)
                {
                    if (Class34.smethod_9(hjJeNotHs.StartsWith("-") ? hjJeNotHs.Substring(1) : hjJeNotHs, this.string_52.ToString(), false))
                    {
                        this.Code = "A028";
                        this.Params = new string[] { this.string_52, hjJeNotHs };
                        return false;
                    }
                }
            }
            string hjJeHs = "";
            if (!bool_3 && (((this.fplx_0 == FPLX.ZYFP) && (this.zyfp_LX_0 == ZYFP_LX.HYSY)) || ((this.fplx_0 == FPLX.JDCFP) || (this.fplx_0 == FPLX.JSFP))))
            {
                hjJeHs = this.GetHjJeHs();
                if (Class34.smethod_9(hjJeHs.StartsWith("-") ? hjJeHs.Substring(1) : hjJeHs, this.string_52.ToString(), false))
                {
                    this.Code = "A028";
                    this.Params = new string[] { this.string_52, hjJeHs };
                    return false;
                }
            }
            string str2 = "0";
            if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
            {
                str2 = string.IsNullOrEmpty(hjJeHs) ? this.GetHjJeHs() : hjJeHs;
            }
            else if (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                str2 = Class34.smethod_15(string.IsNullOrEmpty(hjJeHs) ? this.GetHjJeHs() : hjJeHs, "1.05", Struct40.int_50);
            }
            else
            {
                str2 = string.IsNullOrEmpty(hjJeNotHs) ? this.GetHjJeNotHs() : hjJeNotHs;
            }
            string hjSe = this.GetHjSe();
            if (Class34.smethod_9(Struct40.string_4, Struct40.string_0, true))
            {
                string str4 = Struct40.string_0;
                if ((this.string_51.Length > 0) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                {
                    str4 = Class34.smethod_12(str2, this.string_51, Struct40.int_50);
                }
                else
                {
                    for (int i = 0; i < this.list_0.Count; i++)
                    {
                        if (this.list_0[i].SLv.Length > 0)
                        {
                            if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                            {
                                str4 = Class34.smethod_17(str4, Class34.smethod_1(this.zyfp_LX_0, this.list_0[i].Je, Struct40.int_46, this.list_0[i]));
                            }
                            else
                            {
                                str4 = Class34.smethod_17(str4, Class34.smethod_13(this.list_0[i].Je, this.list_0[i].SLv));
                            }
                        }
                        else if (this.bool_6)
                        {
                            str4 = "";
                            break;
                        }
                    }
                }
                if (str4.Length == 0)
                {
                    if (!Class34.smethod_9(Class34.smethod_17(Class34.smethod_12(Math.Abs(decimal.Parse(str2)).ToString(), Struct40.string_14, Struct40.int_50), Struct40.string_4), Math.Abs(decimal.Parse(hjSe)).ToString(), true))
                    {
                        this.Code = "A015";
                        this.Params = new string[] { Struct40.string_4 };
                        return false;
                    }
                }
                else if (!Class34.smethod_8(str4, hjSe, Struct40.int_50, Struct40.string_4))
                {
                    this.Code = "A015";
                    this.Params = new string[] { Struct40.string_4 };
                    return false;
                }
            }
            if (!this.bool_10 && Class34.smethod_10(str2, Struct40.string_0))
            {
                this.Code = "A016";
                return false;
            }
            if ((Class34.smethod_9(str2, Struct40.string_0, false) && Class34.smethod_9(Struct40.string_0, hjSe, false)) || (Class34.smethod_9(Struct40.string_0, str2, false) && Class34.smethod_9(hjSe, Struct40.string_0, false)))
            {
                this.Code = "A073";
                return false;
            }
            if ((this.string_53 != null) && (this.list_0.Count > 0))
            {
                string str6 = this.string_53;
                this.string_53 = null;
                string str7 = this.GetHjJeNotHs();
                this.string_53 = str6;
                if (!Class34.smethod_8(str6, str7, Struct40.int_50, Struct40.string_1))
                {
                    this.Code = "A077";
                    this.Params = new string[] { Struct40.string_1 };
                    return false;
                }
            }
            this.Code = "0000";
            return true;
        }

        private List<Dictionary<SPXX, string>> method_12(bool bool_13)
        {
            string str = "0.00";
            string str2 = "0.00";
            string str3 = "0.00";
            string str4 = "0.00";
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            bool flag = false;
            foreach (Spxx spxx in this.list_0)
            {
                if (this.bool_1)
                {
                    if (spxx.method_5() == FPHXZ.ZKXX)
                    {
                        str3 = Class34.smethod_17(str3, spxx.Je);
                        str4 = Class34.smethod_17(str4, spxx.Se);
                    }
                    else if (spxx.Je.Length > 0)
                    {
                        str = Class34.smethod_17(str, spxx.Je);
                        str2 = Class34.smethod_17(str2, spxx.Se);
                    }
                }
                if (spxx.method_5() == FPHXZ.XHQDZK)
                {
                    flag = true;
                }
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item.Add(SPXX.SPMC, spxx.Spmc);
                item.Add(SPXX.SPBH, spxx.Spbh);
                item.Add(SPXX.XTHASH, spxx.XTHash);
                item.Add(SPXX.SPSM, spxx.Spsm);
                item.Add(SPXX.KCE, spxx.Kce);
                if (this.fplx_0 == FPLX.JSFP)
                {
                    item.Add(SPXX.GGXH, "");
                    item.Add(SPXX.JLDW, "");
                }
                else
                {
                    item.Add(SPXX.GGXH, spxx.Ggxh);
                    item.Add(SPXX.JLDW, spxx.JLdw);
                }
                item.Add(SPXX.SL, spxx.SL);
                if (bool_13)
                {
                    if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                    {
                        item.Add(SPXX.DJ, spxx.method_10());
                        item.Add(SPXX.JE, spxx.Je);
                    }
                    else
                    {
                        item.Add(SPXX.JE, this.bool_2 ? spxx.method_11() : spxx.Je);
                        if (((this.zyfp_LX_0 == ZYFP_LX.CEZS) && !string.IsNullOrEmpty(item[SPXX.JE])) && !string.IsNullOrEmpty(item[SPXX.SL]))
                        {
                            item.Add(SPXX.DJ, Class34.smethod_14(item[SPXX.JE], item[SPXX.SL], Struct40.int_46));
                        }
                        else
                        {
                            item.Add(SPXX.DJ, this.bool_2 ? spxx.method_10() : spxx.method_9());
                        }
                    }
                }
                else
                {
                    item.Add(SPXX.DJ, spxx.Dj);
                    item.Add(SPXX.JE, spxx.Je);
                }
                item.Add(SPXX.SLV, spxx.SLv);
                item.Add(SPXX.SE, spxx.Se);
                item.Add(SPXX.FPHXZ, spxx.method_5().ToString("D"));
                item.Add(SPXX.HSJBZ, spxx.method_1() ? "1" : "0");
                item.Add(SPXX.FLBM, spxx.Flbm);
                item.Add(SPXX.XSYH, spxx.Xsyh);
                item.Add(SPXX.YHSM, spxx.Yhsm);
                item.Add(SPXX.LSLVBS, spxx.Lslvbs);
                list.Add(item);
            }
            if ((!bool_13 && !flag) && !Class34.smethod_10(str3, Struct40.string_0))
            {
                Dictionary<SPXX, string> dictionary2 = new Dictionary<SPXX, string>();
                dictionary2.Add(SPXX.FPHXZ, "5");
                dictionary2.Add(SPXX.JE, str);
                dictionary2.Add(SPXX.SLV, this.string_51);
                dictionary2.Add(SPXX.SE, str2);
                dictionary2.Add(SPXX.SPMC, Struct40.string_7);
                dictionary2.Add(SPXX.SPBH, "");
                dictionary2.Add(SPXX.SPSM, "0101");
                dictionary2.Add(SPXX.HSJBZ, "0");
                dictionary2.Add(SPXX.DJ, "");
                dictionary2.Add(SPXX.GGXH, "");
                dictionary2.Add(SPXX.JLDW, "");
                dictionary2.Add(SPXX.SL, "");
                dictionary2.Add(SPXX.KCE, "");
                list.Add(dictionary2);
                Dictionary<SPXX, string> dictionary3 = new Dictionary<SPXX, string>();
                dictionary3.Add(SPXX.FPHXZ, "5");
                dictionary3.Add(SPXX.JE, str3);
                dictionary3.Add(SPXX.SLV, this.string_51);
                dictionary3.Add(SPXX.SE, str4);
                dictionary3.Add(SPXX.SPMC, Struct40.string_8);
                dictionary3.Add(SPXX.SPBH, "");
                dictionary3.Add(SPXX.SPSM, "0101");
                dictionary3.Add(SPXX.HSJBZ, "0");
                dictionary3.Add(SPXX.DJ, "");
                dictionary3.Add(SPXX.GGXH, "");
                dictionary3.Add(SPXX.JLDW, "");
                dictionary3.Add(SPXX.SL, "");
                dictionary3.Add(SPXX.KCE, "");
                list.Add(dictionary3);
            }
            return list;
        }

        private bool method_13(string string_61, int int_3)
        {
            int num;
            if (this.opType_0 == OpType.DJ)
            {
                if (CheckSLvEvent != null)
                {
                    return CheckSLvEvent(string_61, int_3);
                }
                return true;
            }
            if (string_61.Length == 0)
            {
                if ((((this.zyfp_LX_0 != ZYFP_LX.HYSY) && (this.zyfp_LX_0 != ZYFP_LX.JZ_50_15)) && ((this.zyfp_LX_0 != ZYFP_LX.CEZS) && (this.fplx_0 != FPLX.DZFP))) && (this.fplx_0 != FPLX.JSFP))
                {
                    if (!this.bool_6)
                    {
                        this.Code = (int_3 < 0) ? "A067" : "A102";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if (int_3 >= 0)
                    {
                        if (this.list_0.Count > 1)
                        {
                            this.Code = (int_3 < 0) ? "A067" : "A102";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if ((int_3 == -1) && (this.list_0.Count > 0))
                    {
                        this.Code = (int_3 < 0) ? "A067" : "A102";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    this.string_51 = string.Empty;
                    this.Code = "0000";
                    return true;
                }
                this.Code = (int_3 < 0) ? "A067" : "A102";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            string_61 = Class34.smethod_20(string_61, Struct40.int_55);
            if (this.bool_12)
            {
                if (this.string_56.EndsWith(";0.00;0;"))
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "slv.txt");
                    if (File.Exists(path))
                    {
                        this.string_56 = this.string_56 + ";" + File.ReadAllText(path);
                    }
                    else
                    {
                        this.string_56 = this.string_56 + ";";
                    }
                }
                if (!this.string_56.Contains(";" + string_61 + ";"))
                {
                    this.Code = (int_3 < 0) ? "A216" : "A128";
                    this.Params = (int_3 < 0) ? new string[] { string_61 } : new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((this.zyfp_LX_0 == ZYFP_LX.HYSY) && !Class34.smethod_10(string_61, "0.05"))
                {
                    this.Code = (int_3 < 0) ? "A017" : "A128";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((this.zyfp_LX_0 == ZYFP_LX.JZ_50_15) && !Class34.smethod_10(string_61, "0.015"))
                {
                    this.Code = (int_3 < 0) ? "A216" : "A128";
                    this.Params = new string[] { (int_3 < 0) ? string_61 : Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((!bool_3 && (this.fplx_0 == FPLX.HYFP)) && !Struct40.list_3.Contains(string_61))
                {
                    this.Code = (int_3 < 0) ? "A032" : "A128";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((this.fplx_0 == FPLX.ZYFP) && Class34.smethod_10(string_61, Struct40.string_0))
                {
                    this.Code = (int_3 < 0) ? "A017" : "A128";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                this.Code = "0000";
                return true;
            }
            if (!bool_3)
            {
                if (this.fplx_0 == FPLX.ZYFP)
                {
                    switch (this.zyfp_LX_0)
                    {
                        case ZYFP_LX.HYSY:
                            if (!Class34.smethod_10(string_61, "0.05") || !Struct40.list_1.Contains(string_61))
                            {
                                this.Code = (int_3 < 0) ? "A017" : "A128";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                            goto Label_0714;

                        case ZYFP_LX.JZ_50_15:
                            if (!Class34.smethod_10(string_61, "0.015") || !Struct40.list_1.Contains(string_61))
                            {
                                this.Code = (int_3 < 0) ? "A017" : "A128";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                            goto Label_0714;
                    }
                    if (!Struct40.list_0.Contains(string_61))
                    {
                        this.Code = (int_3 < 0) ? "A017" : "A128";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    this.Code = "0000";
                    return true;
                }
                if (this.fplx_0 != FPLX.PTFP)
                {
                    if (this.fplx_0 != FPLX.DZFP)
                    {
                        if (this.fplx_0 != FPLX.JSFP)
                        {
                            if (this.fplx_0 != FPLX.HYFP)
                            {
                                if ((this.fplx_0 == FPLX.JDCFP) && !Struct40.list_4.Contains(string_61))
                                {
                                    this.Code = (int_3 < 0) ? "A038" : "A128";
                                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                    return false;
                                }
                            }
                            else if (!Struct40.list_3.Contains(string_61))
                            {
                                this.Code = (int_3 < 0) ? "A032" : "A128";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                        }
                        else if (!Struct40.list_6.Contains(string_61))
                        {
                            this.Code = (int_3 < 0) ? "A207" : "A128";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if (!Struct40.list_7.Contains(string_61))
                    {
                        this.Code = (int_3 < 0) ? "A098" : "A128";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                }
                else if (this.zyfp_LX_0 != ZYFP_LX.NCP_SG)
                {
                    if (this.zyfp_LX_0 != ZYFP_LX.JZ_50_15)
                    {
                        if (!Struct40.list_5.Contains(string_61))
                        {
                            this.Code = (int_3 < 0) ? "A018" : "A128";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if (!Class34.smethod_10(string_61, "0.015") || !Struct40.list_2.Contains(string_61))
                    {
                        this.Code = (int_3 < 0) ? "A018" : "A128";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                }
                else if ((string_61.Length == 0) || !Class34.smethod_10(string_61, Struct40.string_0))
                {
                    this.Code = (int_3 < 0) ? "A057" : "A131";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
            }
            else if (Class34.smethod_9(string_61, Struct40.string_14, false) || Class34.smethod_9(Struct40.string_15, string_61, false))
            {
                this.Code = (int_3 < 0) ? "A068" : "A130";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        Label_0714:
            num = 0;
            while (num < this.list_0.Count)
            {
                if (((!this.bool_7 || (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)) || (this.zyfp_LX_0 == ZYFP_LX.CEZS)) && (!string_61.Equals(this.list_0[num].SLv) && (num != int_3)))
                {
                    this.Code = (int_3 < 0) ? "A005" : "A129";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                num++;
            }
            this.Code = "0000";
            return true;
        }

        private List<Dictionary<SPXX, string>> method_14(bool bool_13)
        {
            if (!bool_13 && !this.CanSaveSpxx())
            {
                return null;
            }
            if (!this.bool_1)
            {
                return this.method_12(bool_13);
            }
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
            if (this.bool_6)
            {
                item.Add(SPXX.SPMC, Struct40.string_6);
                item.Add(SPXX.FPHXZ, FPHXZ.XJDYZSFPQD.ToString("D"));
                item.Add(SPXX.JE, bool_13 ? this.GetHjJe() : this.GetHjJeNotHs());
                item.Add(SPXX.SE, this.GetHjSe());
            }
            else
            {
                item.Add(SPXX.SPMC, Struct40.string_5);
                item.Add(SPXX.FPHXZ, FPHXZ.XJXHQD.ToString("D"));
                item.Add(SPXX.JE, bool_13 ? this.GetSpHjJe() : this.GetSpHjJeNotHs());
                item.Add(SPXX.SE, this.GetSpHjSe());
            }
            item.Add(SPXX.SPBH, "");
            item.Add(SPXX.SPSM, "");
            item.Add(SPXX.GGXH, "");
            item.Add(SPXX.JLDW, "");
            item.Add(SPXX.SL, "");
            item.Add(SPXX.DJ, "");
            item.Add(SPXX.SLV, this.string_51);
            item.Add(SPXX.HSJBZ, this.bool_2 ? "1" : "0");
            item.Add(SPXX.FLBM, "");
            item.Add(SPXX.XSYH, "0");
            item.Add(SPXX.YHSM, "");
            item.Add(SPXX.LSLVBS, "");
            item.Add(SPXX.KCE, "");
            list.Add(item);
            if (!this.bool_6)
            {
                if (!string.IsNullOrEmpty(this.string_57))
                {
                    string str = list[0][SPXX.JE];
                    list[0].Remove(SPXX.JE);
                    list[0].Add(SPXX.JE, Class34.smethod_17(str, bool_13 ? this.GetSpZkJe() : this.GetSpZkJeNotHs()));
                    string str2 = list[0][SPXX.SE];
                    list[0].Remove(SPXX.SE);
                    list[0].Add(SPXX.SE, Class34.smethod_17(str2, this.GetSpZkSe()));
                    return list;
                }
                string str3 = this.method_17();
                if (str3.Length > 0)
                {
                    Dictionary<SPXX, string> dictionary2 = new Dictionary<SPXX, string>();
                    dictionary2.Add(SPXX.SPMC, str3);
                    dictionary2.Add(SPXX.SPBH, "");
                    dictionary2.Add(SPXX.SPSM, "");
                    dictionary2.Add(SPXX.GGXH, "");
                    dictionary2.Add(SPXX.JLDW, "");
                    dictionary2.Add(SPXX.SL, "");
                    dictionary2.Add(SPXX.DJ, "");
                    dictionary2.Add(SPXX.SLV, this.string_51);
                    dictionary2.Add(SPXX.JE, bool_13 ? this.GetSpZkJe() : this.GetSpZkJeNotHs());
                    dictionary2.Add(SPXX.SE, this.GetSpZkSe());
                    dictionary2.Add(SPXX.FPHXZ, FPHXZ.XHQDZK.ToString("D"));
                    dictionary2.Add(SPXX.HSJBZ, "0");
                    dictionary2.Add(SPXX.FLBM, "");
                    dictionary2.Add(SPXX.XSYH, "0");
                    dictionary2.Add(SPXX.YHSM, "");
                    dictionary2.Add(SPXX.LSLVBS, "");
                    dictionary2.Add(SPXX.KCE, "");
                    list.Add(dictionary2);
                }
            }
            return list;
        }

        private List<Dictionary<SPXX, string>> method_15(bool bool_13)
        {
            if (this.CanSaveSpxx() && this.bool_1)
            {
                return this.method_12(bool_13);
            }
            return null;
        }

        private string method_16(string string_61, int int_3)
        {
            if (int_3 > 1)
            {
                return new StringBuilder("折扣行数").Append(int_3).Append("(").Append(Class34.smethod_12(string_61, "100", Struct40.int_54 - 2)).Append("%)").ToString();
            }
            return new StringBuilder("折扣(").Append(Class34.smethod_12(string_61, "100", Struct40.int_54 - 2)).Append("%)").ToString();
        }

        private string method_17()
        {
            string str = "";
            foreach (Spxx spxx in this.list_0)
            {
                if (spxx.method_5() == FPHXZ.ZKXX)
                {
                    string str2 = spxx.Spmc.Substring(spxx.Spmc.LastIndexOf("("));
                    if (str.Length == 0)
                    {
                        str = str2;
                    }
                    else if (!str.Equals(str2))
                    {
                        return "折扣";
                    }
                }
            }
            if (str.Length == 0)
            {
                return str;
            }
            return new StringBuilder().Append("折扣").Append(str).ToString();
        }

        private bool method_18(int int_3, int int_4 = 1, bool bool_13 = false)
        {
            if ((int_3 == 0) && (this.list_0.Count == 0))
            {
                this.Code = "0000";
                return true;
            }
            if (this.fplx_0 == FPLX.HYFP)
            {
                int num6 = 0;
                while (num6 < this.list_0.Count)
                {
                    Spxx spxx3 = this.list_0[num6];
                    if (!string.Equals(Class34.smethod_23(spxx3.Spmc, Struct40.int_42, false, true), spxx3.Spmc))
                    {
                        break;
                    }
                    num6++;
                }
                if ((this.list_0.Count >= (Struct40.int_43 + int_3)) && (num6 < this.list_0.Count))
                {
                    this.Params = new string[] { Convert.ToString((int) (num6 + 1)) };
                    this.Code = "A063";
                    return false;
                }
            }
            if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                int num5 = 0;
                for (int j = 0; j < this.list_0.Count; j++)
                {
                    if (string.IsNullOrEmpty(this.list_0[j].Kce))
                    {
                        this.Params = new string[] { Convert.ToString((int) (j + 1)) };
                        this.Code = "A223";
                        return false;
                    }
                    if ((this.list_0[j].method_5() == FPHXZ.SPXX) || (this.list_0[j].method_5() == FPHXZ.SPXX_ZK))
                    {
                        num5++;
                    }
                    if (num5 > ((bool_13 ? 1 : 0) + int_3))
                    {
                        this.Code = "A225";
                        return false;
                    }
                }
                string str3 = this.method_1();
                if (this.string_41.IndexOf(str3) < 0)
                {
                    string str4 = str3 + this.string_41;
                    if (!Class34.smethod_23(str4, Struct40.int_37, false, false).Equals(str4))
                    {
                        this.Code = "A224";
                        return false;
                    }
                }
            }
            if (int_3 == 0)
            {
                if (((this.fplx_0 == FPLX.JSFP) && (((this.list_0.Count + int_4) - 1) >= Struct40.int_6)) && (this.opType_0 != OpType.DJ))
                {
                    this.Params = new string[] { Struct40.int_6.ToString() };
                    this.Code = "A208";
                    return false;
                }
                if (((this.fplx_0 == FPLX.DZFP) && (((this.list_0.Count + int_4) - 1) >= Struct40.int_5)) && (this.opType_0 != OpType.DJ))
                {
                    this.Params = new string[] { Struct40.int_5.ToString() };
                    this.Code = "A100";
                    return false;
                }
                if (((this.fplx_0 == FPLX.HYFP) && (((this.list_0.Count + int_4) - 1) >= Struct40.int_11)) && (this.opType_0 != OpType.DJ))
                {
                    this.Code = "A040";
                    return false;
                }
                if ((((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP)) && !this.bool_1) && (((this.list_0.Count + int_4) - 1) >= (this.bool_11 ? 7 : 8)))
                {
                    this.Code = "A001";
                    return false;
                }
                if (object.Equals(this.list_0[0].method_5(), FPHXZ.XJDYZSFPQD))
                {
                    this.Code = "A030";
                    return false;
                }
            }
            if (int_3 != 1)
            {
                this.Code = "0000";
                return true;
            }
            bool flag = false;
            if ((this.fplx_0 == FPLX.ZYFP) && (((this.zyfp_LX_0 == ZYFP_LX.SNY) || (this.zyfp_LX_0 == ZYFP_LX.SNY_DDZG)) || ((this.zyfp_LX_0 == ZYFP_LX.RLY) || (this.zyfp_LX_0 == ZYFP_LX.RLY_DDZG))))
            {
                flag = true;
            }
            for (int i = 0; i < this.list_0.Count; i++)
            {
                Spxx spxx = this.list_0[i];
                if (!spxx.method_4(this.bool_2))
                {
                    this.Code = spxx.method_0();
                    this.Params = new string[spxx.string_0.Length + 1];
                    this.Params[0] = Convert.ToString((int) (i + 1));
                    Array.ConstrainedCopy(spxx.string_0, 0, this.Params, 1, spxx.string_0.Length);
                    return false;
                }
                if (!flag)
                {
                    continue;
                }
                string spmc = spxx.Spmc;
                using (Dictionary<ZYFP_LX, string>.KeyCollection.Enumerator enumerator = Struct40.dictionary_0.Keys.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ZYFP_LX current = enumerator.Current;
                        if ((current != this.zyfp_LX_0) && (spmc.IndexOf(Struct40.dictionary_0[current]) >= 0))
                        {
                            goto Label_03CC;
                        }
                    }
                    goto Label_0409;
                Label_03CC:;
                    this.Params = new string[] { Convert.ToString((int) (i + 1)) };
                    this.Code = "A045";
                    return false;
                }
            Label_0409:
                if (!string.Equals(spxx.JLdw, Struct40.string_13) && ((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)))
                {
                    this.Params = new string[] { Convert.ToString((int) (i + 1)) };
                    this.Code = "A046";
                    return false;
                }
                if (((spxx.SL.Length == 0) || Class34.smethod_10(spxx.SL, Struct40.string_0)) && ((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)))
                {
                    this.Params = new string[] { Convert.ToString((int) (i + 1)) };
                    this.Code = "A047";
                    return false;
                }
            }
            if (((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP)) && (!this.bool_6 && !this.bool_1))
            {
                int num4 = 0;
                while (num4 < this.list_0.Count)
                {
                    if (this.list_0[num4].Spmc.IndexOf(Struct40.string_10) >= 0)
                    {
                        break;
                    }
                    num4++;
                }
                if (num4 < this.list_0.Count)
                {
                    if ((this.opType_0 == OpType.KP) && (!this.bool_11 || (this.fplx_0 != FPLX.ZYFP)))
                    {
                        this.Params = new string[] { Convert.ToString((int) (num4 + 1)) };
                        this.Code = "A059";
                        return false;
                    }
                    this.Params = new string[] { Convert.ToString((int) (num4 + 1)) };
                    this.Code = "A058";
                    return false;
                }
            }
            if ((((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP)) && !this.bool_1) && (this.list_0.Count > (this.bool_11 ? 7 : 8)))
            {
                this.Code = "A094";
                return false;
            }
            if ((this.bool_8 && (((this.fplx_0 == FPLX.ZYFP) || (this.fplx_0 == FPLX.PTFP)) || ((this.fplx_0 == FPLX.DZFP) || (this.fplx_0 == FPLX.JSFP)))) && !this.IsGfSqdFp)
            {
                string a = "0";
                for (int k = 0; k < this.list_0.Count; k++)
                {
                    Spxx spxx2 = this.list_0[k];
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSPByName", new object[] { spxx2.Spmc });
                    if (objArray2 == null)
                    {
                        this.Code = "A072";
                        return false;
                    }
                    if (object.Equals(objArray2[0], "3"))
                    {
                        this.Code = "A093";
                        return false;
                    }
                    if (!object.Equals(objArray2[0], "0"))
                    {
                        if (this.fplx_0 != FPLX.ZYFP)
                        {
                            this.Code = "A051";
                            return false;
                        }
                        if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                        {
                            this.Code = "A078";
                            return false;
                        }
                        if ((this.zyfp_LX_0 == ZYFP_LX.JZ_50_15) || (this.zyfp_LX_0 == ZYFP_LX.CEZS))
                        {
                            this.Code = "A212";
                            return false;
                        }
                    }
                    if (k == 0)
                    {
                        a = (string) objArray2[0];
                    }
                    else
                    {
                        if (spxx2.method_5() == FPHXZ.ZKXX)
                        {
                            continue;
                        }
                        if (!string.Equals(a, (string) objArray2[0]))
                        {
                            if (!string.Equals(a, "0") && !string.Equals((string) objArray2[0], "0"))
                            {
                                this.Code = "A049";
                            }
                            else
                            {
                                this.Code = "A092";
                            }
                            this.Params = new string[] { Convert.ToString((int) (k + 1)) };
                            return false;
                        }
                    }
                    if (!string.Equals((string) objArray2[0], "0"))
                    {
                        if (this.bool_6 && ((spxx2.SL.Length == 0) ^ (spxx2.JLdw.Length == 0)))
                        {
                            this.Params = new string[] { Convert.ToString((int) (k + 1)) };
                            this.Code = "A069";
                            return false;
                        }
                        if (!this.bool_6 || (spxx2.SL.Length > 0))
                        {
                            if ((spxx2.SL.Length == 0) || Class34.smethod_10(spxx2.SL, Struct40.string_0))
                            {
                                this.Params = new string[] { Convert.ToString((int) (k + 1)) };
                                this.Code = "A052";
                                return false;
                            }
                            if (!string.Equals(spxx2.JLdw, Struct40.string_11) && !string.Equals(spxx2.JLdw, Struct40.string_12))
                            {
                                this.Params = new string[] { Convert.ToString((int) (k + 1)) };
                                this.Code = "A053";
                                return false;
                            }
                        }
                    }
                }
                if (string.Equals(a, "0") && ((this.zyfp_LX_0 == ZYFP_LX.XT_YCL) || (this.zyfp_LX_0 == ZYFP_LX.XT_CCP)))
                {
                    this.zyfp_LX_0 = ZYFP_LX.ZYFP;
                }
                else if (string.Equals(a, "1"))
                {
                    this.zyfp_LX_0 = ZYFP_LX.XT_YCL;
                }
                else if (string.Equals(a, "2"))
                {
                    this.zyfp_LX_0 = ZYFP_LX.XT_CCP;
                }
            }
            if (this.method_11())
            {
                this.Code = "0000";
                return true;
            }
            return false;
        }

        private OpType method_19(byte[] byte_4)
        {
            OpType eR = OpType.ER;
            try
            {
                byte_0.Initialize();
                string str = Encoding.Unicode.GetString(byte_4);
                DateTime time = DateTime.ParseExact(str.Substring(2, str.Length - 2), "F", CultureInfo.CurrentCulture);
                DateTime now = DateTime.Now;
                if ((time.CompareTo(now) <= 0) && (time.CompareTo(now.AddSeconds(-5.0)) >= 0))
                {
                    eR = (OpType) Enum.Parse(typeof(OpType), str.Substring(0, 2), false);
                }
            }
            catch (Exception exception)
            {
                this.ilog_0.ErrorFormat("验证开具类型异常：{0}", exception.ToString());
            }
            return eR;
        }

        private void method_2(TaxCard taxCard_0)
        {
        }

        private void method_3(TaxCard taxCard_0)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISZYFP)
                {
                    Struct40.list_0.Clear();
                    Struct40.list_1.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A041";
                    return;
                }
                this.bool_11 = taxCard_0.StateInfo.CompanyType != 0;
                this.bool_7 = true;
                if (this.bool_11)
                {
                    Struct40.int_49 = 0x12;
                }
                else
                {
                    Struct40.int_49 = 0x15;
                }
                PZSQType type = taxCard_0.SQInfo[InvoiceType.special];
                if (type != null)
                {
                    Struct40.list_0.Clear();
                    List<TaxRateType> taxRate = type.TaxRate;
                    for (int i = 0; i < taxRate.Count; i++)
                    {
                        string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_0.Add(item);
                    }
                    if (this.bool_6)
                    {
                        Struct40.list_0.Add("");
                    }
                    Struct40.list_1.Clear();
                    taxRate = type.TaxRate2;
                    for (int j = 0; j < taxRate.Count; j++)
                    {
                        string str = decimal.Round(decimal.Parse(taxRate[j].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_1.Add(str);
                    }
                    this.string_52 = type.InvAmountLimit.ToString("F02");
                }
                this.bool_9 = taxCard_0.QYLX.ISSNY;
                this.bool_8 = taxCard_0.QYLX.ISXT;
            }
            bool_3 = false;
            Struct40.int_2 = 100;
            Struct40.int_41 = 0x5c;
            Struct40.int_37 = 230;
            Struct40.int_39 = 8;
            Struct40.int_40 = 8;
            Struct40.int_1 = 100;
            Struct40.int_46 = 15;
            Struct40.int_47 = 0x15;
            Struct40.int_48 = 15;
            Struct40.int_50 = 2;
            Struct40.int_52 = -1;
        }

        private void method_4(TaxCard taxCard_0)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISPTFP)
                {
                    Struct40.list_5.Clear();
                    Struct40.list_2.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A062";
                    return;
                }
                this.bool_11 = taxCard_0.StateInfo.CompanyType != 0;
                this.bool_7 = true;
                if (this.bool_11)
                {
                    Struct40.int_49 = 0x12;
                }
                else
                {
                    Struct40.int_49 = 0x15;
                }
                PZSQType type = taxCard_0.SQInfo[InvoiceType.common];
                if (type != null)
                {
                    Struct40.list_5.Clear();
                    List<TaxRateType> taxRate = type.TaxRate;
                    for (int i = 0; i < taxRate.Count; i++)
                    {
                        string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_5.Add(item);
                    }
                    if (this.bool_6)
                    {
                        Struct40.list_5.Add("");
                    }
                    Struct40.list_2.Clear();
                    taxRate = type.TaxRate2;
                    for (int j = 0; j < taxRate.Count; j++)
                    {
                        string str2 = decimal.Round(decimal.Parse(taxRate[j].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_2.Add(str2);
                    }
                    this.string_52 = type.InvAmountLimit.ToString("F02");
                }
                this.bool_9 = taxCard_0.QYLX.ISSNY;
                this.bool_8 = taxCard_0.QYLX.ISXT;
            }
            bool_3 = false;
            Struct40.int_2 = 100;
            Struct40.int_41 = 0x5c;
            Struct40.int_37 = 230;
            Struct40.int_39 = 8;
            Struct40.int_40 = 8;
            Struct40.int_1 = 100;
            Struct40.int_46 = 15;
            Struct40.int_47 = 0x15;
            Struct40.int_48 = 15;
            Struct40.int_50 = 2;
            Struct40.int_52 = -1;
        }

        private void method_5(TaxCard taxCard_0)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISPTFPDZ)
                {
                    Struct40.list_7.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A099";
                    return;
                }
                this.bool_11 = false;
                this.bool_7 = true;
                if (this.bool_11)
                {
                    Struct40.int_49 = 0x12;
                }
                else
                {
                    Struct40.int_49 = 0x15;
                }
                PZSQType type = taxCard_0.SQInfo[InvoiceType.Electronic];
                if (type != null)
                {
                    Struct40.list_7.Clear();
                    List<TaxRateType> taxRate = type.TaxRate;
                    for (int i = 0; i < taxRate.Count; i++)
                    {
                        string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_7.Add(item);
                    }
                    if (this.bool_6)
                    {
                        Struct40.list_7.Add("");
                    }
                    this.string_52 = type.InvAmountLimit.ToString("F02");
                }
                this.bool_9 = taxCard_0.QYLX.ISSNY;
                this.bool_8 = taxCard_0.QYLX.ISXT;
            }
            bool_3 = false;
            Struct40.int_2 = 100;
            Struct40.int_41 = 0x5c;
            Struct40.int_37 = 230;
            Struct40.int_39 = 8;
            Struct40.int_40 = 8;
            Struct40.int_1 = 100;
            Struct40.int_46 = 15;
            Struct40.int_47 = 0x15;
            Struct40.int_48 = 15;
            Struct40.int_50 = 2;
            Struct40.int_52 = -1;
        }

        private void method_6(TaxCard taxCard_0)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISHY)
                {
                    Struct40.list_3.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A042";
                    return;
                }
                this.bool_11 = false;
                Struct40.list_3.Clear();
                PZSQType type = taxCard_0.SQInfo[InvoiceType.transportation];
                this.string_52 = type.InvAmountLimit.ToString("F02");
                List<TaxRateType> taxRate = type.TaxRate;
                for (int i = 0; i < taxRate.Count; i++)
                {
                    string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                    Struct40.list_3.Add(item);
                }
            }
            bool_3 = false;
            Struct40.int_2 = 80;
            Struct40.int_41 = 20;
            Struct40.int_37 = 200;
            Struct40.int_39 = 0x10;
            Struct40.int_40 = 0x10;
            Struct40.int_36 = 100;
            Struct40.int_1 = 80;
            Struct40.int_46 = 15;
            Struct40.int_47 = 0x15;
            Struct40.int_48 = 15;
            Struct40.int_49 = 0x15;
            Struct40.int_50 = 2;
            Struct40.int_52 = -1;
        }

        private void method_7(TaxCard taxCard_0)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISJDC)
                {
                    Struct40.list_4.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A043";
                    return;
                }
                this.bool_11 = false;
                Struct40.list_4.Clear();
                PZSQType type = taxCard_0.SQInfo[InvoiceType.vehiclesales];
                this.string_52 = type.InvAmountLimit.ToString("F02");
                List<TaxRateType> taxRate = type.TaxRate;
                for (int i = 0; i < taxRate.Count; i++)
                {
                    string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                    Struct40.list_4.Add(item);
                }
            }
            bool_3 = false;
            Struct40.int_36 = 100;
            Struct40.int_1 = 100;
            Struct40.int_41 = 0x5c;
            Struct40.int_46 = 15;
            Struct40.int_47 = 0x15;
            Struct40.int_48 = 15;
            Struct40.int_49 = 0x15;
            Struct40.int_50 = 2;
            Struct40.int_52 = -1;
        }

        private void method_8(TaxCard taxCard_0, string string_61)
        {
            this.method_2(taxCard_0);
            if (!bool_3)
            {
                if (!taxCard_0.QYLX.ISPTFPJSP)
                {
                    Struct40.list_6.Clear();
                    this.opType_0 = OpType.ER;
                    this.Code = "A202";
                    return;
                }
                this.bool_11 = false;
                this.bool_7 = true;
                PZSQType type = taxCard_0.SQInfo[InvoiceType.volticket];
                if (type != null)
                {
                    Struct40.list_6.Clear();
                    List<TaxRateType> taxRate = type.TaxRate;
                    for (int i = 0; i < taxRate.Count; i++)
                    {
                        string item = decimal.Round(decimal.Parse(taxRate[i].Rate.ToString()), Struct40.int_55, MidpointRounding.AwayFromZero).ToString();
                        Struct40.list_6.Add(item);
                    }
                    this.string_52 = type.InvAmountLimit.ToString("F02");
                }
                this.bool_9 = false;
                this.bool_8 = false;
            }
            bool_3 = false;
            Struct40.int_2 = 80;
            string_61 = (string_61 == null) ? "" : string_61;
            if (string_61.IndexOf("76mmX") >= 0)
            {
                Struct40.int_41 = 40;
                Struct40.int_46 = 8;
                Struct40.int_47 = 10;
                Struct40.int_48 = 5;
                Struct40.int_49 = 7;
                Struct40.int_50 = 2;
                Struct40.int_52 = 10;
            }
            else if (string_61.IndexOf("57mmX") >= 0)
            {
                Struct40.int_41 = 40;
                Struct40.int_46 = 6;
                Struct40.int_47 = 8;
                Struct40.int_48 = 4;
                Struct40.int_49 = 6;
                Struct40.int_50 = 2;
                Struct40.int_52 = 8;
            }
            else
            {
                Struct40.int_41 = 0;
                Struct40.int_46 = 0;
                Struct40.int_47 = 0;
                Struct40.int_48 = 0;
                Struct40.int_49 = 0;
                Struct40.int_50 = 0;
                Struct40.int_52 = 0;
                this.ilog_0.ErrorFormat("未指定卷式发票的打印模板:{0}", string_61);
            }
            if (string_61.IndexOf("X177mm") > 0)
            {
                Struct40.int_6 = 6;
            }
            else if (string_61.IndexOf("X127mm") > 0)
            {
                Struct40.int_6 = 6;
            }
            else
            {
                Struct40.int_6 = 6;
            }
            Struct40.int_37 = 230;
            Struct40.int_39 = 8;
            Struct40.int_40 = 8;
            Struct40.int_1 = 100;
        }

        private bool method_9()
        {
            if (TaxCardFactory.CreateTaxCard().IsLargeInvDetail)
            {
                int num2 = Struct40.int_9;
                if (this.bool_12)
                {
                    num2 = Struct40.int_10;
                }
                if (this.list_0.Count >= num2)
                {
                    this.Params = new string[] { num2.ToString() };
                    this.Code = "A209";
                    return false;
                }
            }
            else
            {
                int num = Struct40.int_7;
                if (this.bool_12)
                {
                    num = Struct40.int_8;
                }
                if (this.list_0.Count >= num)
                {
                    this.Params = new string[] { num.ToString() };
                    this.Code = "A209";
                    return false;
                }
            }
            return true;
        }

        public void SetData(object[] object_1)
        {
            this.object_0 = object_1;
        }

        public bool SetDj(int int_3, string string_61)
        {
            string dj = "";
            string se = "";
            string je = "";
            bool flag = false;
            Spxx spxx = null;
            try
            {
                if (int_3 > (this.list_0.Count - 1))
                {
                    this.Code = "A002";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                spxx = this.list_0[int_3];
                if ((this.Hsjbz != spxx.method_1()) || !string_61.Equals(spxx.Dj))
                {
                    decimal num2;
                    if (string.IsNullOrEmpty(spxx.SLv) && !string.IsNullOrEmpty(string_61))
                    {
                        this.Code = "A133";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((string_61.Length > 0) && !decimal.TryParse(string_61, out num2))
                    {
                        this.Code = "A114";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((string_61.Length > 0) && !Class34.smethod_9(string_61, Struct40.string_0, true))
                    {
                        this.Code = "A112";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if (spxx.method_5() == FPHXZ.SPXX_ZK)
                    {
                        this.Code = "A004";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if (spxx.method_5() == FPHXZ.ZKXX)
                    {
                        this.Code = "A064";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((string_61.Length > 0) && Class34.smethod_10(string_61, Struct40.string_0))
                    {
                        string_61 = "";
                    }
                    if (string_61.Length == 0)
                    {
                        spxx.Dj = "";
                        spxx.Je = "";
                        spxx.Se = "";
                        this.string_53 = null;
                        this.string_54 = null;
                        return true;
                    }
                    dj = spxx.Dj;
                    se = spxx.Se;
                    je = spxx.Je;
                    flag = spxx.method_1();
                    string s = Class34.smethod_24(Class34.smethod_17(string_61, Struct40.string_0), Struct40.int_47, false);
                    if (s == null)
                    {
                        this.Code = "A114";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    spxx.Dj = decimal.Round(decimal.Parse(s), Struct40.int_46, MidpointRounding.AwayFromZero).ToString();
                    if (spxx.method_12() != ZYFP_LX.HYSY)
                    {
                        spxx.method_2(this.bool_2);
                    }
                    else
                    {
                        spxx.method_2(true);
                    }
                    if (spxx.SL.Length > 0)
                    {
                        if (spxx.method_12() == ZYFP_LX.HYSY)
                        {
                            spxx.Je = Class34.smethod_12(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                            spxx.Se = Class34.smethod_18(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), spxx.Je);
                        }
                        else
                        {
                            if (!spxx.method_1())
                            {
                                spxx.Je = Class34.smethod_12(spxx.SL, spxx.Dj, Struct40.int_50);
                            }
                            else if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                            {
                                spxx.Je = Class34.smethod_15(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Class34.smethod_17("1.00", spxx.SLv), Struct40.int_50);
                            }
                            else
                            {
                                spxx.Je = Class34.smethod_2(this.zyfp_LX_0, Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Struct40.int_50, null, spxx);
                            }
                            if (spxx.method_1())
                            {
                                spxx.Se = Class34.smethod_18(Class34.smethod_12(spxx.SL, spxx.Dj, Struct40.int_50), spxx.Je);
                            }
                            else if (spxx.method_5() != FPHXZ.XJDYZSFPQD)
                            {
                                if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                                {
                                    spxx.Se = Class34.smethod_12(spxx.Je, spxx.SLv, Struct40.int_50);
                                }
                                else
                                {
                                    spxx.Se = Class34.smethod_1(this.zyfp_LX_0, spxx.Je, Struct40.int_50, spxx);
                                }
                            }
                        }
                        if (((spxx.Je.Length > Struct40.int_51) || (spxx.Se.Length > Struct40.int_53)) || ((Struct40.int_52 > 0) && (spxx.method_11().Length > Struct40.int_52)))
                        {
                            if (spxx != null)
                            {
                                spxx.Dj = dj;
                                spxx.Se = se;
                                spxx.Je = je;
                                spxx.method_2(flag);
                            }
                            this.Code = "A123";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        if (((this.zyfp_LX_0 == ZYFP_LX.CEZS) && (spxx.Kce.Length > 0)) && ((spxx.method_11().Length > 0) && Class34.smethod_9(Class34.smethod_16(spxx.Kce, spxx.method_11()), "1.00", false)))
                        {
                            if (spxx != null)
                            {
                                spxx.Dj = dj;
                                spxx.Se = se;
                                spxx.Je = je;
                                spxx.method_2(flag);
                            }
                            this.Code = "A222";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        if (Class34.smethod_10(spxx.Je, Struct40.string_0))
                        {
                            if (spxx != null)
                            {
                                spxx.Dj = dj;
                                spxx.Se = se;
                                spxx.Je = je;
                                spxx.method_2(flag);
                            }
                            this.Code = "A121";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if (spxx.Je.Length > 0)
                    {
                        string str4 = Class34.smethod_24(Class34.smethod_14(spxx.method_1() ? Class34.smethod_17(spxx.Je, spxx.Se) : spxx.Je, spxx.Dj, Struct40.int_48), Struct40.int_49, false);
                        if (str4 == null)
                        {
                            if (spxx != null)
                            {
                                spxx.Dj = dj;
                                spxx.Se = se;
                                spxx.Je = je;
                                spxx.method_2(flag);
                            }
                            this.Code = "A118";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        spxx.SL = str4;
                    }
                    this.Code = "0000";
                    this.string_53 = null;
                    this.string_54 = null;
                }
                return true;
            }
            catch (Exception)
            {
                if (spxx != null)
                {
                    spxx.Dj = dj;
                    spxx.Se = se;
                    spxx.Je = je;
                    spxx.method_2(flag);
                }
                this.Code = "A114";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        }

        public bool SetDjHsjbz(int int_3, bool bool_13)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            spxx.method_2(bool_13);
            return true;
        }

        public bool SetFlbm(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            spxx.Flbm = string_61;
            return true;
        }

        public bool SetFpSLv(string string_61)
        {
            decimal num;
            if (!decimal.TryParse(string_61, out num))
            {
                this.Code = "A065";
                return false;
            }
            string_61 = Class34.smethod_20(string_61, Struct40.int_55);
            if (this.fplx_0 == FPLX.JDCFP)
            {
                if ((!bool_3 && !Struct40.list_4.Contains(string_61)) && !this.bool_12)
                {
                    this.Code = "A038";
                    return false;
                }
                this.string_51 = string_61;
                string str = Class34.smethod_17(this.string_53, this.string_54);
                this.string_53 = Class34.smethod_15(str, Class34.smethod_17(this.string_51, "1.00"), Struct40.int_50);
                this.string_54 = Class34.smethod_18(str, this.string_53);
                this.Code = "0000";
                return true;
            }
            if (this.fplx_0 == FPLX.HYFP)
            {
                if ((!bool_3 && !Struct40.list_3.Contains(string_61)) && !this.bool_12)
                {
                    this.Code = "A032";
                    return false;
                }
                this.bool_7 = true;
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    this.SetSLv(i, string_61);
                }
                this.string_51 = string_61;
                this.bool_7 = false;
                this.Code = "0000";
                this.string_53 = null;
                this.string_54 = null;
                return true;
            }
            this.Code = "A037";
            return false;
        }

        public bool SetGgxh(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            if (spxx.method_5() == FPHXZ.ZKXX)
            {
                this.Code = "A064";
                return false;
            }
            spxx.Ggxh = string_61;
            return true;
        }

        public bool SetJe(int int_3, string string_61)
        {
            string je = "";
            string se = "";
            string dj = "";
            string sL = "";
            bool flag = false;
            Spxx spxx = null;
            try
            {
                decimal num;
                if (int_3 > (this.list_0.Count - 1))
                {
                    this.Code = "A002";
                    return false;
                }
                spxx = this.list_0[int_3];
                if (!this.bool_2 && string.Equals(spxx.Je, string_61))
                {
                    return true;
                }
                if ((string_61.Length > 0) && !decimal.TryParse(string_61, out num))
                {
                    this.Code = "A115";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((!this.bool_2 && decimal.TryParse(string_61, out num)) && (decimal.TryParse(spxx.Je, out num) && Class34.smethod_10(string_61, spxx.Je)))
                {
                    return true;
                }
                if ((string_61.Length > 0) && Class34.smethod_10(Class34.smethod_19(string_61, Struct40.int_50), Struct40.string_0))
                {
                    this.Code = "A121";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if ((((this.zyfp_LX_0 == ZYFP_LX.CEZS) && (this.list_0[int_3].Kce.Length > 0)) && ((this.list_0[int_3].method_11().Length > 0) && (string_61.Length > 0))) && Class34.smethod_9(Class34.smethod_16(this.list_0[int_3].Kce, this.list_0[int_3].method_11()), "1.00", false))
                {
                    this.Code = "A222";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if (this.bool_6)
                {
                    if (((this.fplx_0 != FPLX.HYFP) && (string_61.Length > 0)) && Class34.smethod_9(string_61, Struct40.string_0, false))
                    {
                        this.Code = "A104";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if (((this.fplx_0 == FPLX.HYFP) && (string_61.Length > 0)) && !Class34.smethod_9(string_61, Struct40.string_0, true))
                    {
                        this.Code = "A124";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                }
                else if (((spxx.method_5() != FPHXZ.ZKXX) && (spxx.method_5() != FPHXZ.XHQDZK)) && ((string_61.Length > 0) && !Class34.smethod_9(string_61, Struct40.string_0, true)))
                {
                    this.Code = "A105";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if (((string_61.Length == 0) && (spxx.SL.Length > 0)) && (spxx.Dj.Length > 0))
                {
                    this.Code = "A110";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if (spxx.method_5() == FPHXZ.SPXX_ZK)
                {
                    this.Code = "A004";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                if (spxx.method_5() == FPHXZ.ZKXX)
                {
                    this.Code = "A064";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                je = spxx.Je;
                se = spxx.Se;
                dj = spxx.Dj;
                flag = spxx.method_1();
                sL = spxx.SL;
                if (string_61.Length == 0)
                {
                    spxx.Je = "";
                    spxx.Se = "";
                    this.Code = "0000";
                    this.string_53 = null;
                    this.string_54 = null;
                    return true;
                }
                string_61 = Class34.smethod_19(string_61, Struct40.int_50);
                if (this.bool_2 && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                {
                    if (spxx.SLv.Length > 0)
                    {
                        if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                        {
                            spxx.Je = Class34.smethod_15(string_61, Class34.smethod_17(spxx.SLv, "1.00"), Struct40.int_50);
                        }
                        else
                        {
                            spxx.Je = Class34.smethod_2(this.zyfp_LX_0, string_61, Struct40.int_50, null, spxx);
                        }
                    }
                    else
                    {
                        spxx.Je = Class34.smethod_19(Class34.smethod_18(string_61, (spxx.Se.Length > 0) ? spxx.Se : "0"), Struct40.int_50);
                    }
                }
                else
                {
                    spxx.Je = Class34.smethod_19(string_61, Struct40.int_50);
                }
                if (spxx.method_12() == ZYFP_LX.HYSY)
                {
                    spxx.Se = Class34.smethod_15(Class34.smethod_13(spxx.Je, spxx.SLv), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                }
                else if (this.bool_2)
                {
                    spxx.Se = Class34.smethod_18(string_61, spxx.Je);
                }
                else if ((spxx.method_12() != ZYFP_LX.JZ_50_15) && (spxx.method_12() != ZYFP_LX.CEZS))
                {
                    if (spxx.SLv.Length > 0)
                    {
                        spxx.Se = Class34.smethod_12(spxx.Je, spxx.SLv, Struct40.int_50);
                    }
                }
                else
                {
                    spxx.Se = Class34.smethod_1(spxx.method_12(), spxx.Je, Struct40.int_50, spxx);
                }
                if (((spxx.Je.Length <= Struct40.int_51) && (spxx.Se.Length <= Struct40.int_53)) && ((Struct40.int_52 <= 0) || (spxx.method_11().Length <= Struct40.int_52)))
                {
                    if (this.bool_6)
                    {
                        if (spxx.Dj.Length > 0)
                        {
                            if (spxx.method_12() == ZYFP_LX.HYSY)
                            {
                                spxx.SL = Class34.smethod_14(spxx.method_11(), spxx.method_10(), Struct40.int_48);
                            }
                            else
                            {
                                spxx.SL = Class34.smethod_24(Class34.smethod_14(spxx.method_1() ? Class34.smethod_17(spxx.Je, spxx.Se) : spxx.Je, spxx.Dj, Struct40.int_48), Struct40.int_49, false);
                            }
                            string str7 = Class34.smethod_24(spxx.SL, Struct40.int_49, false);
                            if (str7 == null)
                            {
                                if (spxx != null)
                                {
                                    spxx.Je = je;
                                    spxx.Se = se;
                                    spxx.Dj = dj;
                                    spxx.method_2(flag);
                                    spxx.SL = sL;
                                }
                                this.Code = "A118";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                            spxx.SL = str7;
                        }
                        else if (spxx.SL.Length > 0)
                        {
                            if (spxx.method_12() == ZYFP_LX.HYSY)
                            {
                                spxx.Dj = Class34.smethod_14(spxx.method_11(), spxx.SL, Struct40.int_46);
                            }
                            else
                            {
                                spxx.method_2(this.bool_2);
                                spxx.Dj = Class34.smethod_14(string_61, spxx.SL, Struct40.int_46);
                            }
                            string str8 = Class34.smethod_24(spxx.Dj, Struct40.int_47, false);
                            if (str8 == null)
                            {
                                if (spxx != null)
                                {
                                    spxx.Je = je;
                                    spxx.Se = se;
                                    spxx.Dj = dj;
                                    spxx.method_2(flag);
                                    spxx.SL = sL;
                                }
                                this.Code = "A114";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                            spxx.Dj = str8;
                        }
                    }
                    else if (spxx.SL.Length > 0)
                    {
                        if (spxx.method_12() == ZYFP_LX.HYSY)
                        {
                            spxx.Dj = Class34.smethod_14(spxx.method_11(), spxx.SL, Struct40.int_46);
                        }
                        else
                        {
                            spxx.method_2(this.bool_2);
                            spxx.Dj = Class34.smethod_14(string_61, spxx.SL, Struct40.int_46);
                        }
                        string str5 = Class34.smethod_24(spxx.Dj, Struct40.int_47, false);
                        if (str5 == null)
                        {
                            if (spxx != null)
                            {
                                spxx.Je = je;
                                spxx.Se = se;
                                spxx.Dj = dj;
                                spxx.method_2(flag);
                                spxx.SL = sL;
                            }
                            this.Code = "A114";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        spxx.Dj = str5;
                    }
                    else if (spxx.Dj.Length > 0)
                    {
                        if (spxx.method_12() == ZYFP_LX.HYSY)
                        {
                            spxx.SL = Class34.smethod_14(spxx.method_11(), spxx.Dj, Struct40.int_48);
                        }
                        else
                        {
                            spxx.SL = Class34.smethod_24(Class34.smethod_14(spxx.method_1() ? Class34.smethod_17(spxx.Je, spxx.Se) : spxx.Je, spxx.Dj, Struct40.int_48), Struct40.int_49, false);
                        }
                        string str6 = Class34.smethod_24(spxx.SL, Struct40.int_49, false);
                        if (str6 == null)
                        {
                            if (spxx != null)
                            {
                                spxx.Je = je;
                                spxx.Se = se;
                                spxx.Dj = dj;
                                spxx.method_2(flag);
                                spxx.SL = sL;
                            }
                            this.Code = "A118";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        spxx.SL = str6;
                    }
                    this.Code = "0000";
                    this.string_53 = null;
                    this.string_54 = null;
                    return true;
                }
                if (spxx != null)
                {
                    spxx.Je = je;
                    spxx.Se = se;
                    spxx.Dj = dj;
                    spxx.method_2(flag);
                    spxx.SL = sL;
                }
                this.Code = "A123";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            catch (Exception)
            {
                if (spxx != null)
                {
                    spxx.Je = je;
                    spxx.Se = se;
                    spxx.Dj = dj;
                    spxx.method_2(flag);
                    spxx.SL = sL;
                }
                this.Code = "A115";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        }

        public bool SetJLdw(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            if (spxx.method_5() == FPHXZ.ZKXX)
            {
                this.Code = "A064";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            if (!string_61.Equals(Struct40.string_13) && (((this.zyfp_LX_0 == ZYFP_LX.SNY) || (this.zyfp_LX_0 == ZYFP_LX.SNY_DDZG)) || ((this.zyfp_LX_0 == ZYFP_LX.RLY) || (this.zyfp_LX_0 == ZYFP_LX.RLY_DDZG))))
            {
                this.Code = "A019";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            if ((!string_61.Equals(Struct40.string_11) && !string_61.Equals(Struct40.string_12)) && ((this.zyfp_LX_0 == ZYFP_LX.XT_CCP) || (this.zyfp_LX_0 == ZYFP_LX.XT_YCL)))
            {
                this.Code = "A053";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            this.Code = "0000";
            spxx.JLdw = string_61;
            return true;
        }

        public bool SetJshj(string string_61)
        {
            if (this.fplx_0 == FPLX.JDCFP)
            {
                decimal num;
                if (this.string_51.Length == 0)
                {
                    this.Code = "A033";
                    return false;
                }
                if (string_61.Length == 0)
                {
                    this.string_53 = Struct40.string_0;
                    this.string_54 = Struct40.string_0;
                    this.Code = "0000";
                    return true;
                }
                if (!decimal.TryParse(string_61, out num))
                {
                    this.Code = "A066";
                    return false;
                }
                if ((this.bool_6 && Class34.smethod_9(string_61, Struct40.string_0, false)) && (this.opType_0 != OpType.DJ))
                {
                    this.Code = "A054";
                    return false;
                }
                if ((!this.bool_6 && Class34.smethod_9(Struct40.string_0, string_61, false)) && (this.opType_0 != OpType.DJ))
                {
                    this.Code = "A055";
                    return false;
                }
                if ((this.fplx_0 == FPLX.JDCFP) && Class34.smethod_9(string_61.StartsWith("-") ? string_61.Substring(1) : string_61, this.string_52.ToString(), false))
                {
                    this.Code = "A028";
                    this.Params = new string[] { this.string_52, Class34.smethod_19(string_61, Struct40.int_50) };
                    return false;
                }
                string_61 = Class34.smethod_19(string_61, Struct40.int_50);
                this.string_53 = Class34.smethod_15(string_61, Class34.smethod_17(this.string_51, "1.00"), Struct40.int_50);
                this.string_54 = Class34.smethod_18(string_61, this.string_53);
                this.Code = "0000";
                return true;
            }
            this.Code = "A035";
            return false;
        }

        public bool SetKce(int int_3, string string_61)
        {
            decimal num;
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            if (!decimal.TryParse(string_61, out num))
            {
                this.Code = "A220";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            if (((this.list_0[int_3].Je.Length > 0) && (this.list_0[int_3].Je.Length > 0)) && Class34.smethod_9(Class34.smethod_16(string_61, Class34.smethod_17(this.list_0[int_3].Je, this.list_0[int_3].Se)), "1.00", false))
            {
                this.Code = "A221";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            this.Code = "0000";
            spxx.Kce = Class34.smethod_20(string_61, Struct40.int_50);
            if (this.bool_6 && Class34.smethod_9(spxx.Kce, Struct40.string_0, false))
            {
                spxx.Kce = Class34.smethod_21(spxx.Kce);
            }
            return true;
        }

        public bool SetLslvbs(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            spxx.Lslvbs = string_61;
            return true;
        }

        public bool SetQdbz(bool bool_13)
        {
            if (bool_13)
            {
                if ((this.fplx_0 == FPLX.ZYFP) && this.bool_11)
                {
                    this.Code = "A009";
                    return false;
                }
                if (this.fplx_0 == FPLX.DZFP)
                {
                    this.Code = "A203";
                    return false;
                }
                if (this.fplx_0 == FPLX.JSFP)
                {
                    this.Code = "A204";
                    return false;
                }
                if (this.fplx_0 == FPLX.HYFP)
                {
                    this.Code = "A205";
                    return false;
                }
                if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.Code = "A206";
                    return false;
                }
            }
            this.bool_1 = bool_13;
            return true;
        }

        public bool SetSe(int int_3, string string_61)
        {
            try
            {
                if ((int_3 >= 0) && (int_3 <= (this.list_0.Count - 1)))
                {
                    decimal num;
                    Spxx spxx = this.list_0[int_3];
                    if (string.Equals(spxx.Se, string_61))
                    {
                        return true;
                    }
                    if ((string_61.Length > 0) && !decimal.TryParse(string_61, out num))
                    {
                        this.Code = "A117";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((decimal.TryParse(string_61, out num) && decimal.TryParse(spxx.Se, out num)) && Class34.smethod_10(spxx.Se, string_61))
                    {
                        return true;
                    }
                    if ((!this.bool_6 && (spxx.Se.Length > 0)) && !Class34.smethod_10(string_61, spxx.Se))
                    {
                        this.Code = "A022";
                        return false;
                    }
                    if (this.bool_6 && Class34.smethod_9(string_61, Struct40.string_0, false))
                    {
                        this.Code = "A122";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    spxx.Se = Class34.smethod_19(string_61, Struct40.int_50);
                    if (((spxx.Je.Length <= Struct40.int_51) && (spxx.Se.Length <= Struct40.int_53)) && ((Struct40.int_52 <= 0) || (spxx.method_11().Length <= Struct40.int_52)))
                    {
                        this.Code = "0000";
                        this.string_53 = null;
                        this.string_54 = null;
                        return true;
                    }
                    this.Code = "A123";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
                this.Code = "A002";
                return false;
            }
            catch (Exception)
            {
                this.Code = "A117";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        }

        public bool SetSL(int int_3, string string_61)
        {
            string sL = "";
            string se = "";
            string je = "";
            Spxx spxx = null;
            try
            {
                if (int_3 > (this.list_0.Count - 1))
                {
                    this.Code = "A002";
                    return false;
                }
                spxx = this.list_0[int_3];
                if (!string_61.Equals(spxx.SL))
                {
                    decimal num2;
                    if (string.IsNullOrEmpty(spxx.SLv) && !string.IsNullOrEmpty(string_61))
                    {
                        this.Code = "A134";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((string_61.Length > 0) && !decimal.TryParse(string_61, out num2))
                    {
                        this.Code = "A118";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if ((string_61.Length > 0) && Class34.smethod_10(string_61, Struct40.string_0))
                    {
                        string_61 = "";
                    }
                    if (spxx.method_5() == FPHXZ.SPXX_ZK)
                    {
                        this.Code = "A004";
                        return false;
                    }
                    if (spxx.method_5() == FPHXZ.ZKXX)
                    {
                        this.Code = "A064";
                        return false;
                    }
                    if (string_61.Length == 0)
                    {
                        spxx.SL = "";
                        spxx.Je = "";
                        spxx.Se = "";
                        this.string_53 = null;
                        this.string_54 = null;
                        return true;
                    }
                    if (this.bool_6)
                    {
                        if (Class34.smethod_9(string_61, Struct40.string_0, false))
                        {
                            this.Code = "A113";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if (Class34.smethod_9(Struct40.string_0, string_61, false))
                    {
                        this.Code = "A119";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    sL = spxx.SL;
                    se = spxx.Se;
                    je = spxx.Je;
                    string s = Class34.smethod_24(string_61, Struct40.int_49, false);
                    if (s == null)
                    {
                        this.Code = "A118";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    spxx.SL = decimal.Round(decimal.Parse(s), Struct40.int_48, MidpointRounding.AwayFromZero).ToString();
                    if (spxx.Dj.Length > 0)
                    {
                        if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                        {
                            spxx.Je = Class34.smethod_12(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                            spxx.Se = Class34.smethod_18(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), spxx.Je);
                        }
                        else
                        {
                            if (!spxx.method_1())
                            {
                                spxx.Je = Class34.smethod_12(spxx.SL, spxx.Dj, Struct40.int_50);
                            }
                            else if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                            {
                                spxx.Je = Class34.smethod_15(Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Class34.smethod_17("1.00", spxx.SLv), Struct40.int_50);
                            }
                            else
                            {
                                spxx.Je = Class34.smethod_2(this.zyfp_LX_0, Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Struct40.int_50, null, spxx);
                            }
                            if (spxx.method_1())
                            {
                                spxx.Se = Class34.smethod_18(Class34.smethod_12(spxx.SL, spxx.Dj, Struct40.int_50), spxx.Je);
                            }
                            else if (spxx.SLv.Length > 0)
                            {
                                if ((this.zyfp_LX_0 != ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                                {
                                    spxx.Se = Class34.smethod_12(spxx.Je, spxx.SLv, Struct40.int_50);
                                }
                                else
                                {
                                    spxx.Se = Class34.smethod_1(this.zyfp_LX_0, spxx.Je, Struct40.int_50, spxx);
                                }
                            }
                        }
                        if (((spxx.Je.Length > Struct40.int_51) || (spxx.Se.Length > Struct40.int_53)) || ((Struct40.int_52 > 0) && (spxx.method_11().Length > Struct40.int_52)))
                        {
                            if (spxx != null)
                            {
                                spxx.SL = sL;
                                spxx.Se = se;
                                spxx.Je = je;
                            }
                            this.Code = "A123";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        if (((this.zyfp_LX_0 == ZYFP_LX.CEZS) && (spxx.Kce.Length > 0)) && ((spxx.method_11().Length > 0) && Class34.smethod_9(Class34.smethod_16(spxx.Kce, spxx.method_11()), "1.00", false)))
                        {
                            if (spxx != null)
                            {
                                spxx.SL = sL;
                                spxx.Se = se;
                                spxx.Je = je;
                            }
                            this.Code = "A222";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        if (Class34.smethod_10(spxx.Je, Struct40.string_0))
                        {
                            if (spxx != null)
                            {
                                spxx.SL = sL;
                                spxx.Se = se;
                                spxx.Je = je;
                            }
                            this.Code = "A121";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                    }
                    else if (spxx.Je.Length > 0)
                    {
                        bool flag2;
                        string str5 = Class34.smethod_24(Class34.smethod_14((flag2 = (this.zyfp_LX_0 == ZYFP_LX.HYSY) || this.bool_2) ? spxx.method_11() : spxx.Je, spxx.SL, Struct40.int_46), Struct40.int_47, false);
                        if (str5 == null)
                        {
                            if (spxx != null)
                            {
                                spxx.SL = sL;
                                spxx.Se = se;
                                spxx.Je = je;
                            }
                            this.Code = "A114";
                            this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                            return false;
                        }
                        spxx.Dj = str5;
                        spxx.method_2(flag2);
                    }
                    this.Code = "0000";
                    this.string_53 = null;
                    this.string_54 = null;
                }
                return true;
            }
            catch (Exception)
            {
                if (spxx != null)
                {
                    spxx.SL = sL;
                    spxx.Se = se;
                    spxx.Je = je;
                }
                this.Code = "A118";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        }

        public bool SetSLv(int int_3, string string_61)
        {
            string sLv = "";
            string str2 = "";
            FPHXZ sPXX = FPHXZ.SPXX;
            Spxx spxx = null;
            try
            {
                if ((int_3 >= 0) && (int_3 <= (this.list_0.Count - 1)))
                {
                    decimal num;
                    if ((string_61.Length > 0) && !decimal.TryParse(string_61, out num))
                    {
                        this.Code = "A116";
                        this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                        return false;
                    }
                    if (string_61.Length > 0)
                    {
                        string_61 = Class34.smethod_20(string_61, Struct40.int_55);
                    }
                    spxx = this.list_0[int_3];
                    if ((!decimal.TryParse(string_61, out num) || !decimal.TryParse(spxx.SLv, out num)) || (!Class34.smethod_10(string_61, spxx.SLv) || Class34.smethod_10(string_61, "0.05")))
                    {
                        if (spxx.method_5() == FPHXZ.SPXX_ZK)
                        {
                            this.Code = "A004";
                            return false;
                        }
                        if (spxx.method_5() == FPHXZ.ZKXX)
                        {
                            this.Code = "A064";
                            return false;
                        }
                        if (!this.method_13(string_61, int_3))
                        {
                            return false;
                        }
                        str2 = this.string_51;
                        sPXX = spxx.method_5();
                        if (string_61.Length == 0)
                        {
                            spxx.method_6(FPHXZ.XJDYZSFPQD);
                            spxx.Dj = string.Empty;
                            spxx.SL = string.Empty;
                            spxx.Spmc = Struct40.string_6;
                        }
                        else
                        {
                            spxx.method_6(FPHXZ.SPXX);
                        }
                        if (this.list_0.Count == 1)
                        {
                            this.string_51 = string_61;
                        }
                        else if (!this.string_51.Equals(string_61))
                        {
                            bool flag2 = true;
                            for (int i = 0; i < this.list_0.Count; i++)
                            {
                                if ((i != int_3) && !this.list_0[i].SLv.Equals(string_61))
                                {
                                    flag2 = false;
                                }
                            }
                            if (!flag2 && ((!this.bool_7 || (this.zyfp_LX_0 == ZYFP_LX.JZ_50_15)) || (this.zyfp_LX_0 == ZYFP_LX.CEZS)))
                            {
                                this.Code = "A005";
                                return false;
                            }
                            this.string_51 = flag2 ? string_61 : string.Empty;
                        }
                        sLv = spxx.SLv;
                        spxx.SLv = string_61;
                        if (spxx.method_1())
                        {
                            if ((spxx.Dj.Length > 0) && (spxx.SL.Length > 0))
                            {
                                if (spxx.method_12() == ZYFP_LX.HYSY)
                                {
                                    spxx.Je = Class34.smethod_12(Class34.smethod_13(spxx.Dj, spxx.SL), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                                }
                                else if (this.zyfp_LX_0 == ZYFP_LX.CEZS)
                                {
                                    spxx.Je = Class34.smethod_2(this.zyfp_LX_0, Class34.smethod_12(spxx.Dj, spxx.SL, Struct40.int_50), Struct40.int_50, null, spxx);
                                }
                                else
                                {
                                    spxx.Je = Class34.smethod_12(spxx.method_9(), spxx.SL, Struct40.int_50);
                                }
                            }
                            if ((spxx.Je.Length > Struct40.int_51) || ((Struct40.int_52 > 0) && (spxx.method_11().Length > Struct40.int_52)))
                            {
                                if (spxx != null)
                                {
                                    spxx.SLv = sLv;
                                    this.string_51 = str2;
                                    spxx.method_6(sPXX);
                                }
                                this.Code = "A123";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                        }
                        else
                        {
                            if (spxx.method_12() == ZYFP_LX.HYSY)
                            {
                                spxx.method_2(true);
                            }
                            if ((spxx.Dj.Length > 0) && (spxx.method_12() == ZYFP_LX.HYSY))
                            {
                                spxx.Dj = Class34.smethod_14(spxx.Dj, Class34.smethod_18("1.00", spxx.SLv), Struct40.int_46);
                                string str3 = Class34.smethod_24(spxx.Dj, Struct40.int_47, false);
                                if (str3 == null)
                                {
                                    if (spxx != null)
                                    {
                                        spxx.SLv = sLv;
                                        this.string_51 = str2;
                                        spxx.method_6(sPXX);
                                    }
                                    this.Code = "A114";
                                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                    return false;
                                }
                                spxx.Dj = str3;
                            }
                        }
                        if (spxx.Je.Length > 0)
                        {
                            if ((spxx.method_1() && (spxx.Dj.Length > 0)) && (spxx.SL.Length > 0))
                            {
                                spxx.Se = Class34.smethod_18(Class34.smethod_12(spxx.SL, spxx.Dj, Struct40.int_50), spxx.Je);
                            }
                            else if (spxx.method_12() == ZYFP_LX.HYSY)
                            {
                                spxx.Se = Class34.smethod_15(Class34.smethod_13(spxx.SLv, spxx.Je), Class34.smethod_18("1.00", spxx.SLv), Struct40.int_50);
                            }
                            else if (spxx.SLv.Length > 0)
                            {
                                if ((spxx.method_12() != ZYFP_LX.JZ_50_15) && (spxx.method_12() != ZYFP_LX.CEZS))
                                {
                                    spxx.Se = Class34.smethod_12(spxx.Je, spxx.SLv, 2);
                                }
                                else
                                {
                                    spxx.Se = Class34.smethod_1(spxx.method_12(), spxx.Je, Struct40.int_50, spxx);
                                }
                            }
                            if (((spxx.Se.Length > Struct40.int_53) || (spxx.Je.Length > Struct40.int_51)) || ((Struct40.int_52 > 0) && (spxx.method_11().Length > Struct40.int_52)))
                            {
                                if (spxx != null)
                                {
                                    spxx.SLv = sLv;
                                    this.string_51 = str2;
                                    spxx.method_6(sPXX);
                                }
                                this.Code = "A123";
                                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                                return false;
                            }
                        }
                        this.Code = "0000";
                        this.string_53 = null;
                        this.string_54 = null;
                    }
                    return true;
                }
                this.Code = "A002";
                return false;
            }
            catch (Exception)
            {
                if (spxx != null)
                {
                    spxx.SLv = sLv;
                    this.string_51 = str2;
                    spxx.method_6(sPXX);
                }
                this.Code = "A116";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
        }

        public bool SetSpbh(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            spxx.Spbh = string_61;
            return true;
        }

        public bool SetSpbm(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            this.Code = "0000";
            spxx.Spbh = string_61;
            return true;
        }

        public bool SetSpmc(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            if (!this.bool_12 && (spxx.method_5() == FPHXZ.ZKXX))
            {
                this.Code = "A064";
                return false;
            }
            string spmc = spxx.Spmc;
            if (string.Equals(spmc, string_61))
            {
                this.Code = "0000";
                return true;
            }
            spxx.Spmc = string_61;
            if (((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK)) && (((this.zyfp_LX_0 == ZYFP_LX.SNY) || (this.zyfp_LX_0 == ZYFP_LX.SNY_DDZG)) || ((this.zyfp_LX_0 == ZYFP_LX.RLY) || (this.zyfp_LX_0 == ZYFP_LX.RLY_DDZG))))
            {
                string b = spxx.Spmc.Replace(Struct40.dictionary_0[this.zyfp_LX_0], "") + Struct40.dictionary_0[this.zyfp_LX_0];
                spxx.Spmc = b;
                if (!string.Equals(spxx.Spmc, b))
                {
                    spxx.Spmc = spmc;
                    this.Code = "A125";
                    this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                    return false;
                }
            }
            this.Code = "0000";
            return true;
        }

        public bool SetSpsm(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            if (spxx.method_5() == FPHXZ.SPXX_ZK)
            {
                this.Code = "A004";
                return false;
            }
            this.Code = "0000";
            spxx.Spsm = string_61;
            return true;
        }

        public bool SetXsyh(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            spxx.Xsyh = string_61;
            return true;
        }

        public bool SetXtHash(int int_3, string string_61)
        {
            if ((int_3 >= 0) && (int_3 <= (this.list_0.Count - 1)))
            {
                Spxx spxx = this.list_0[int_3];
                if (spxx.method_5() == FPHXZ.SPXX_ZK)
                {
                    this.Code = "A004";
                    return false;
                }
                this.Code = "0000";
                spxx.XTHash = string_61;
                return true;
            }
            this.Code = "A002";
            return false;
        }

        public bool SetYhsm(int int_3, string string_61)
        {
            if (int_3 > (this.list_0.Count - 1))
            {
                this.Code = "A002";
                this.Params = new string[] { Convert.ToString((int) (int_3 + 1)) };
                return false;
            }
            Spxx spxx = this.list_0[int_3];
            spxx.Yhsm = string_61;
            return true;
        }

        public bool SetZyfpLx(ZYFP_LX zyfp_LX_1)
        {
            bool flag2;
            if (((zyfp_LX_1 == ZYFP_LX.NCP_SG) || (zyfp_LX_1 == ZYFP_LX.NCP_XS)) && (this.fplx_0 != FPLX.PTFP))
            {
                this.Code = "A056";
                return false;
            }
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((zyfp_LX_1 == ZYFP_LX.NCP_SG) && !card.QYLX.ISNCPSG)
            {
                this.Code = "A060";
                return false;
            }
            if ((zyfp_LX_1 == ZYFP_LX.NCP_XS) && !card.QYLX.ISNCPXS)
            {
                this.Code = "A061";
                return false;
            }
            if ((zyfp_LX_1 == ZYFP_LX.CEZS) && !card.GetExtandParams("CEBTVisble").Equals("1"))
            {
                this.Code = "A227";
                return false;
            }
            if (this.list_0.Count <= 1)
            {
                if (((zyfp_LX_1 == ZYFP_LX.HYSY) && (this.zyfp_LX_0 != ZYFP_LX.ZYFP)) && (this.zyfp_LX_0 != ZYFP_LX.HYSY))
                {
                    this.Code = "A070";
                    return false;
                }
                if (((zyfp_LX_1 == ZYFP_LX.JZ_50_15) && (this.zyfp_LX_0 != ZYFP_LX.ZYFP)) && (this.zyfp_LX_0 != ZYFP_LX.JZ_50_15))
                {
                    this.Code = "A210";
                    return false;
                }
                if (((zyfp_LX_1 == ZYFP_LX.CEZS) && (this.zyfp_LX_0 != ZYFP_LX.ZYFP)) && (this.zyfp_LX_0 != ZYFP_LX.CEZS))
                {
                    this.Code = "A218";
                    return false;
                }
            }
            else
            {
                using (List<Spxx>.Enumerator enumerator2 = this.list_0.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        Spxx current = enumerator2.Current;
                        if (((zyfp_LX_1 == ZYFP_LX.HYSY) && (current.method_12() != ZYFP_LX.HYSY)) || ((zyfp_LX_1 != ZYFP_LX.HYSY) && (current.method_12() == ZYFP_LX.HYSY)))
                        {
                            goto Label_0192;
                        }
                        if (((zyfp_LX_1 == ZYFP_LX.JZ_50_15) && (current.method_12() != ZYFP_LX.JZ_50_15)) || ((zyfp_LX_1 != ZYFP_LX.JZ_50_15) && (current.method_12() == ZYFP_LX.JZ_50_15)))
                        {
                            goto Label_01A5;
                        }
                        if (((zyfp_LX_1 == ZYFP_LX.CEZS) && (current.method_12() != ZYFP_LX.CEZS)) || ((zyfp_LX_1 != ZYFP_LX.CEZS) && (current.method_12() == ZYFP_LX.CEZS)))
                        {
                            goto Label_01B8;
                        }
                    }
                    goto Label_01D9;
                Label_0192:
                    this.Code = "A070";
                    return false;
                Label_01A5:
                    this.Code = "A210";
                    return false;
                Label_01B8:
                    this.Code = "A218";
                    return false;
                }
            }
        Label_01D9:
            flag2 = false;
            if (((zyfp_LX_1 == ZYFP_LX.SNY) || (zyfp_LX_1 == ZYFP_LX.SNY_DDZG)) || ((zyfp_LX_1 == ZYFP_LX.RLY) || (zyfp_LX_1 == ZYFP_LX.RLY_DDZG)))
            {
                flag2 = true;
            }
            if ((this.fplx_0 != FPLX.ZYFP) && flag2)
            {
                this.Code = "A044";
                return false;
            }
            bool flag = false;
            if (((this.zyfp_LX_0 == ZYFP_LX.SNY) || (this.zyfp_LX_0 == ZYFP_LX.SNY_DDZG)) || ((this.zyfp_LX_0 == ZYFP_LX.RLY) || (this.zyfp_LX_0 == ZYFP_LX.RLY_DDZG)))
            {
                flag = true;
            }
            if (flag2)
            {
                Spxx[] array = new Spxx[this.list_0.Count];
                this.list_0.CopyTo(array);
                for (int i = 0; i < array.Length; i++)
                {
                    Spxx spxx = array[i];
                    if ((spxx.method_5() == FPHXZ.SPXX) || (spxx.method_5() == FPHXZ.SPXX_ZK))
                    {
                        if (flag)
                        {
                            spxx.Spmc = spxx.Spmc.Replace(Struct40.dictionary_0[this.zyfp_LX_0], "");
                        }
                        spxx.Spmc = spxx.Spmc.Replace(Struct40.dictionary_0[zyfp_LX_1], "");
                        string b = spxx.Spmc + Struct40.dictionary_0[zyfp_LX_1];
                        spxx.Spmc = b;
                        if (!string.Equals(spxx.Spmc, b))
                        {
                            this.Code = "A125";
                            this.Params = new string[] { Convert.ToString((int) (i + 1)) };
                            return false;
                        }
                    }
                }
            }
            foreach (Spxx spxx2 in this.list_0)
            {
                spxx2.method_13(zyfp_LX_1);
                if (flag2 && ((spxx2.method_5() == FPHXZ.SPXX) || (spxx2.method_5() == FPHXZ.SPXX_ZK)))
                {
                    if (flag)
                    {
                        spxx2.Spmc = spxx2.Spmc.Replace(Struct40.dictionary_0[this.zyfp_LX_0], "");
                    }
                    spxx2.Spmc = spxx2.Spmc.Replace(Struct40.dictionary_0[zyfp_LX_1], "");
                    spxx2.Spmc = spxx2.Spmc + Struct40.dictionary_0[zyfp_LX_1];
                    spxx2.JLdw = Struct40.string_13;
                }
            }
            this.zyfp_LX_0 = zyfp_LX_1;
            if (zyfp_LX_1 == ZYFP_LX.HYSY)
            {
                this.bool_2 = true;
            }
            this.Code = "0000";
            return true;
        }

        public static FPLX ParseFPLX(string string_61)
        {
            switch (string_61)
            {
                case "s":
                    return FPLX.ZYFP;

                case "c":
                    return FPLX.PTFP;

                case "f":
                    return FPLX.HYFP;

                case "j":
                    return FPLX.JDCFP;

                case "p":
                    return FPLX.DZFP;

                case "q":
                    return FPLX.JSFP;
            }
            return FPLX.ZYFP;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (PropertyInfo info in base.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string name = info.Name;
                object obj2 = info.GetValue(this, null);
                if ((info.PropertyType.IsValueType || info.PropertyType.Name.StartsWith("String")) || (info.PropertyType.Name.StartsWith("FPLX") || info.PropertyType.Name.StartsWith("ZY_FPLX")))
                {
                    builder.Append(string.Format("({0}:{1})", name, obj2));
                }
            }
            builder.Append(string.Format("({0}:{1})", "Type", this.opType_0));
            builder.Append(string.Format("({0}:{1})", "Code", this.string_0));
            for (int i = 0; i < this.list_0.Count; i++)
            {
                builder.Append("\n").Append(i);
                builder.Append(this.list_0[i].ToString());
            }
            return builder.ToString();
        }

        public string BlueFpdm
        {
            get
            {
                return this.string_46;
            }
            set
            {
                this.string_46 = value;
            }
        }

        public string BlueFphm
        {
            get
            {
                return this.string_47;
            }
            set
            {
                this.string_47 = value;
            }
        }

        public string Bmbbbh
        {
            get
            {
                return this.string_57;
            }
            set
            {
                this.string_57 = value;
            }
        }

        public bool IsGfSqdFp
        {
            get
            {
                return bool_3;
            }
            set
            {
                bool_3 = value;
                if (bool_3)
                {
                    this.bool_4 = false;
                }
                if (bool_3)
                {
                    this.bool_7 = true;
                    this.bool_9 = true;
                    this.bool_8 = true;
                }
                else
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    this.bool_7 = true;
                    this.bool_11 = card.StateInfo.CompanyType != 0;
                    this.bool_9 = card.QYLX.ISSNY;
                    this.bool_8 = card.QYLX.ISXT;
                }
            }
        }

        public bool IsXfSqdFp
        {
            get
            {
                return this.bool_4;
            }
            set
            {
                this.bool_4 = value;
                if (this.bool_4)
                {
                    bool_3 = false;
                }
            }
        }

        public string Bz
        {
            get
            {
                return this.string_41;
            }
            set
            {
                this.string_41 = Class34.smethod_23(value, Struct40.int_37, false, false);
            }
        }

        public string Ccdw
        {
            get
            {
                return this.string_36;
            }
            set
            {
                this.string_36 = Class34.smethod_23(value, Struct40.int_18, false, true);
            }
        }

        public string Cd
        {
            get
            {
                return this.string_13;
            }
            set
            {
                this.string_13 = Class34.smethod_23(value, Struct40.int_22, false, true);
            }
        }

        public string Cllx
        {
            get
            {
                return this.string_11;
            }
            set
            {
                this.string_11 = Class34.smethod_23(value, Struct40.int_20, false, true);
            }
        }

        public string Clsbdh_cjhm
        {
            get
            {
                return this.string_18;
            }
            set
            {
                value = (value == null) ? "" : value;
                if ((value.Length == 0) || new Regex("^[-* A-Z0-9]*$").IsMatch(value.ToUpper()))
                {
                    this.string_18 = Class34.smethod_23(value, Struct40.int_27, false, true).ToUpper();
                }
            }
        }

        private string Code
        {
            set
            {
                this.string_0 = value;
            }
        }

        public string Cpxh
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = Class34.smethod_23(value, Struct40.int_21, false, true);
            }
        }

        public Dictionary<string, object> CustomFields
        {
            get
            {
                return this.dictionary_0;
            }
        }

        public string Czch
        {
            get
            {
                return this.string_35;
            }
            set
            {
                this.string_35 = Class34.smethod_23(value, Struct40.int_17, false, true);
            }
        }

        public string Dh
        {
            get
            {
                return this.string_20;
            }
            set
            {
                this.string_20 = Class34.smethod_23(value, Struct40.int_29, false, true);
            }
        }

        public string Dk_qymc
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = (value == null) ? "" : value.Trim();
            }
        }

        public string Dk_qysh
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = (value == null) ? "" : value.ToUpper().Trim();
            }
        }

        public string Dw
        {
            get
            {
                return this.string_24;
            }
            set
            {
                this.string_24 = Class34.smethod_23(value, Struct40.int_33, false, true);
            }
        }

        public string Dz
        {
            get
            {
                return this.string_22;
            }
            set
            {
                this.string_22 = Class34.smethod_23(value, Struct40.int_30, false, true);
            }
        }

        public string Fdjhm
        {
            get
            {
                return this.string_17;
            }
            set
            {
                this.string_17 = Class34.smethod_23(value, Struct40.int_26, false, true);
            }
        }

        public string Fhr
        {
            get
            {
                return this.string_44;
            }
            set
            {
                this.string_44 = Class34.smethod_23(value, Struct40.int_40, false, true);
            }
        }

        public string Fhrmc
        {
            get
            {
                return this.string_31;
            }
            set
            {
                this.string_31 = Class34.smethod_23(value, Struct40.int_14, false, true);
            }
        }

        public string Fhrsh
        {
            get
            {
                return this.string_32;
            }
            set
            {
                if ((((value.Length == 15) || (value.Length == 0x11)) || ((value.Length == 0x12) || (value.Length == 20))) && new StringBuilder().Append('0', value.Length).ToString().Equals(value))
                {
                    this.string_32 = string.Empty;
                }
                else
                {
                    value = (value == null) ? "" : value;
                    if ((value.Length == 0) || Class34.smethod_5(value.ToUpper()))
                    {
                        this.string_32 = Class34.smethod_23(value.ToUpper(), Struct40.int_0, false, true);
                    }
                }
            }
        }

        public string Fpdm
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string Fphm
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

        public FPLX Fplx
        {
            get
            {
                return this.fplx_0;
            }
        }

        public string Gfdzdh
        {
            get
            {
                return this.string_37;
            }
            set
            {
                this.string_37 = Class34.smethod_23(value, Struct40.int_3, false, true);
            }
        }

        public string Gfmc
        {
            get
            {
                if (this.string_6 == string.Empty)
                {
                    return "";
                }
                string str2 = "Aisino.Fwkp.Invoice" + this.string_1 + this.string_2;
                byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str2));
                byte[] destinationArray = new byte[0x20];
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer4 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer4, 0, 0x10);
                byte[] buffer = AES_Crypt.Decrypt(Convert.FromBase64String(this.string_6), destinationArray, buffer4, null);
                if (buffer != null)
                {
                    string s = Encoding.Unicode.GetString(buffer);
                    if (Encoding.Unicode.GetByteCount(s) == buffer.Length)
                    {
                        return s;
                    }
                }
                return this.string_6;
            }
            set
            {
                this.string_6 = Class34.smethod_23(value, Struct40.int_2, false, true);
                string str = "Aisino.Fwkp.Invoice" + this.string_1 + this.string_2;
                byte[] bytes = Encoding.Unicode.GetBytes(MD5_Crypt.GetHashStr(str));
                byte[] destinationArray = new byte[0x20];
                Array.Copy(bytes, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(bytes, 0x20, buffer3, 0, 0x10);
                byte[] inArray = AES_Crypt.Encrypt(Encoding.Unicode.GetBytes(this.string_6), destinationArray, buffer3);
                this.string_6 = Convert.ToBase64String(inArray);
            }
        }

        public string Gfsh
        {
            get
            {
                return this.string_7;
            }
            set
            {
                if ((((value.Length == 15) || (value.Length == 0x11)) || ((value.Length == 0x12) || (value.Length == 20))) && new StringBuilder().Append('0', value.Length).ToString().Equals(value))
                {
                    this.string_7 = string.Empty;
                }
                else
                {
                    value = (value == null) ? "" : value;
                    if ((value.Length == 0) || Class34.smethod_5(value.ToUpper()))
                    {
                        this.string_7 = Class34.smethod_23(value.ToUpper(), Struct40.int_0, false, true);
                    }
                }
            }
        }

        public string Gfyhzh
        {
            get
            {
                return this.string_38;
            }
            set
            {
                this.string_38 = Class34.smethod_23(value, Struct40.int_4, false, true);
            }
        }

        public string Hgzh
        {
            get
            {
                return this.string_14;
            }
            set
            {
                this.string_14 = Class34.smethod_23(value, Struct40.int_23, false, true);
            }
        }

        public string Hjje
        {
            get
            {
                return this.string_53;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.string_53 = Class34.smethod_19(value, Struct40.int_50);
                }
            }
        }

        public string Hjse
        {
            get
            {
                return this.string_54;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.string_54 = Class34.smethod_19(value, Struct40.int_50);
                }
            }
        }

        public bool Hsjbz
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                if (this.zyfp_LX_0 == ZYFP_LX.HYSY)
                {
                    this.Code = "A020";
                }
                else if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.Code = "A034";
                }
                else
                {
                    this.bool_2 = value;
                    this.Code = "0000";
                }
            }
        }

        public bool Hzfw
        {
            get
            {
                return this.bool_11;
            }
        }

        public static bool IsGfSqdFp_Static
        {
            set
            {
                bool_3 = value;
            }
        }

        public bool IsRed
        {
            get
            {
                return this.bool_6;
            }
            set
            {
                this.bool_6 = value;
            }
        }

        public bool IsXfHzfw
        {
            get
            {
                return this.bool_5;
            }
            set
            {
                if (bool_3)
                {
                    this.bool_5 = value;
                    this.bool_11 = value;
                    if (this.bool_11 && (this.fplx_0 == FPLX.ZYFP))
                    {
                        this.bool_1 = false;
                    }
                }
            }
        }

        public bool Jdc_ver_new
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
                if (this.bool_0)
                {
                    Struct40.int_2 = 0x48;
                    Struct40.int_33 = 8;
                    Struct40.int_19 = 0x16;
                    Struct40.int_21 = 60;
                }
                else
                {
                    Struct40.int_2 = 0x6c;
                    Struct40.int_33 = 12;
                    Struct40.int_19 = 20;
                    Struct40.int_21 = 0x2c;
                }
            }
        }

        public string Jkzmsh
        {
            get
            {
                return this.string_15;
            }
            set
            {
                this.string_15 = Class34.smethod_23(value, Struct40.int_24, false, true);
            }
        }

        public string Jqbh
        {
            get
            {
                return this.string_26;
            }
            set
            {
                this.string_26 = Class34.smethod_23(value, Struct40.int_12, false, true);
            }
        }

        public string Khyh
        {
            get
            {
                return this.string_23;
            }
            set
            {
                this.string_23 = Class34.smethod_23(value, Struct40.int_31, false, true);
            }
        }

        public string Kpr
        {
            get
            {
                return this.string_42;
            }
            set
            {
                this.string_42 = Class34.smethod_23(value, Struct40.int_38, false, true);
            }
        }

        public string Kprq
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public Dictionary<string, object> OtherFields
        {
            get
            {
                return this.dictionary_1;
            }
        }

        public bool Qdbz
        {
            get
            {
                return this.bool_1;
            }
        }

        public string Qyd_jy_ddd
        {
            get
            {
                return this.string_33;
            }
            set
            {
                this.string_33 = Class34.smethod_23(value, Struct40.int_15, false, true);
            }
        }

        public byte[] RdmByte
        {
            get
            {
                this.byte_1 = new byte[20];
                int num = 10;
                while (this.byte_1 == null)
                {
                    if (--num <= 0)
                    {
                        break;
                    }
                    new Random().NextBytes(this.byte_1);
                }
                return this.byte_1;
            }
        }

        public string RedNum
        {
            get
            {
                return this.string_45;
            }
            set
            {
                this.string_45 = value;
            }
        }

        public string Sbbz
        {
            get
            {
                return this.string_60;
            }
            set
            {
                if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.string_60 = value;
                }
            }
        }

        public string Sccjmc
        {
            get
            {
                return this.string_19;
            }
            set
            {
                this.string_19 = Class34.smethod_23(value, Struct40.int_28, false, true);
            }
        }

        public string Sfzh_zzjgdm
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = Class34.smethod_23(value, Struct40.int_19, false, true);
            }
        }

        public string Shrmc
        {
            get
            {
                return this.string_29;
            }
            set
            {
                this.string_29 = Class34.smethod_23(value, Struct40.int_13, false, true);
            }
        }

        public string Shrsh
        {
            get
            {
                return this.string_30;
            }
            set
            {
                if ((((value.Length == 15) || (value.Length == 0x11)) || ((value.Length == 0x12) || (value.Length == 20))) && new StringBuilder().Append('0', value.Length).ToString().Equals(value))
                {
                    this.string_30 = string.Empty;
                }
                else
                {
                    value = (value == null) ? "" : value;
                    if ((value.Length == 0) || Class34.smethod_5(value.ToUpper()))
                    {
                        this.string_30 = Class34.smethod_23(value.ToUpper(), Struct40.int_0, false, true);
                    }
                }
            }
        }

        public string Sjdh
        {
            get
            {
                return this.string_16;
            }
            set
            {
                this.string_16 = Class34.smethod_23(value, Struct40.int_25, false, true);
            }
        }

        public string Skr
        {
            get
            {
                return this.string_43;
            }
            set
            {
                this.string_43 = Class34.smethod_23(value, Struct40.int_39, false, true);
            }
        }

        public string SLv
        {
            get
            {
                return this.string_51;
            }
        }

        public bool SupportMulti
        {
            get
            {
                return this.bool_7;
            }
        }

        public static byte[] TypeByte
        {
            get
            {
                new Random().NextBytes(byte_0);
                int num = 10;
                while (byte_0 == null)
                {
                    if (--num <= 0)
                    {
                        break;
                    }
                    new Random().NextBytes(byte_0);
                }
                return byte_0;
            }
        }

        public string Xcrs
        {
            get
            {
                return this.string_25;
            }
            set
            {
                this.string_25 = Class34.smethod_23(value, Struct40.int_34, false, true);
            }
        }

        public string Xfdzdh
        {
            get
            {
                return this.string_39;
            }
            set
            {
                this.string_39 = Class34.smethod_23(value, Struct40.int_35, false, true);
            }
        }

        public string Xfmc
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = Class34.smethod_23(value, Struct40.int_1, false, true);
            }
        }

        public string Xfsh
        {
            get
            {
                return this.string_4;
            }
            set
            {
                if (((this.zyfp_LX_0 == ZYFP_LX.NCP_SG) && (((value.Length == 15) || (value.Length == 0x11)) || ((value.Length == 0x12) || (value.Length == 20)))) && new StringBuilder().Append('0', value.Length).ToString().Equals(value))
                {
                    this.string_4 = string.Empty;
                }
                else
                {
                    value = (value == null) ? "" : value;
                    if ((value.Length == 0) || Class34.smethod_5(value.ToUpper()))
                    {
                        this.string_4 = Class34.smethod_23(value.ToUpper(), Struct40.int_0, false, true);
                    }
                }
            }
        }

        public string Xfyhzh
        {
            get
            {
                return this.string_40;
            }
            set
            {
                this.string_40 = Class34.smethod_23(value, Struct40.int_36, false, true);
            }
        }

        public string Xsdjbh
        {
            get
            {
                return this.string_48;
            }
            set
            {
                this.string_48 = value;
            }
        }

        public string Yshwxx
        {
            get
            {
                return this.string_34;
            }
            set
            {
                this.string_34 = Class34.smethod_23(value, Struct40.int_16, false, false);
            }
        }

        public string Zgswjg_dm
        {
            get
            {
                return this.string_27;
            }
            set
            {
                this.string_27 = value;
            }
        }

        public string Zgswjg_mc
        {
            get
            {
                return this.string_28;
            }
            set
            {
                this.string_28 = value;
            }
        }

        public string Zh
        {
            get
            {
                return this.string_21;
            }
            set
            {
                this.string_21 = Class34.smethod_23(value, Struct40.int_32, false, true);
            }
        }

        public ZYFP_LX Zyfplx
        {
            get
            {
                return this.zyfp_LX_0;
            }
        }

        public string Zyspmc
        {
            get
            {
                return this.string_58;
            }
            set
            {
                if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.string_58 = value;
                }
            }
        }

        public string Zyspsm
        {
            get
            {
                return this.string_59;
            }
            set
            {
                if (this.fplx_0 == FPLX.JDCFP)
                {
                    this.string_59 = value;
                }
            }
        }

        public delegate bool CheckSLvDelegate(string sLv, int flag);
    }
}

