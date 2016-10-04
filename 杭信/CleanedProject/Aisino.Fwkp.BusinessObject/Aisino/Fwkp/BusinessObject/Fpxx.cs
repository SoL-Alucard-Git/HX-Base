namespace Aisino.Fwkp.BusinessObject
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.CommonLibrary;
    using log4net;
    using ns3;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Framework.Plugin.Core.Util;
    [Serializable]
    public class Fpxx
    {
        public string blueFpdm;
        public string blueFphm;
        public string bmbbbh;
        internal bool bool_0;
        public bool bsbz;
        public int bsq;
        public int bszt;
        public string bz;
        public string ccdw;
        public string cd;
        public string cllx;
        public string clsbdh;
        public string cpxh;
        public Dictionary<string, object> CustomFields;
        public string cyrmc;
        public string cyrnsrsbh;
        public string czch;
        public object[] data;
        public string dkqymc;
        public string dkqysh;
        public string dw;
        public string dy_mb;
        public bool dybz;
        public string dzsyh;
        public string fdjhm;
        public string fhr;
        public string fhrmc;
        public string fhrnsrsbh;
        public string fpdm;
        public string fphm;
        public FPLX fplx;
        public string gfbh;
        public string gfdzdh;
        public string gfmc;
        public string gfsh;
        public string gfyhzh;
        public string hgzh;
        public string hxm;
        public bool hzfw;
        private static ILog ilog_0;
        public long invQryNo;
        public bool isBlankWaste;
        public bool isNewJdcfp;
        public bool isRed;
        public string je;
        public string jkzmsh;
        public string jmbbh;
        public string jqbh;
        private JskFpmxVersion jskFpmxVersion_0;
        public string jym;
        public ulong keyFlagNo;
        public int kpjh;
        public string kpr;
        public string kprq;
        public string kpsxh;
        public string mw;
        public List<Dictionary<SPXX, string>> Mxxx;
        public Dictionary<string, object> OtherFields;
        public List<Dictionary<SPXX, string>> Qdxx;
        public string qyd;
        public byte[] rdmByte;
        public string redNum;
        public string retCode;
        public string sbbz;
        public string sccjmc;
        public string se;
        public string sfzhm;
        public string shrmc;
        public string shrnsrsbh;
        public string sign;
        public string sjdh;
        public string skr;
        public string sLv;
        public static string SpecialChar;
        public string spfmc;
        public string spfnsrsbh;
        public int ssyf;
        private string string_0;
        private string[] string_1;
        public string wspzhm;
        public string xcrs;
        public bool xfbz;
        public string xfdh;
        public string xfdz;
        public string xfdzdh;
        public string xfmc;
        public string xfsh;
        public string xfyh;
        public string xfyhzh;
        public string xfzh;
        public string xsdjbh;
        public string yshwxx;
        public string yysbz;
        public bool zfbz;
        public string zfsj;
        public string zgswjgdm;
        public string zgswjgmc;
        public ZYFP_LX Zyfplx;
        public string zyspmc;
        public string zyspsm;

        static Fpxx()
        {
            
            SpecialChar = "€\x001f";
            ilog_0 = LogUtil.GetLogger<Fpxx>();
        }

        public Fpxx()
        {
            
            this.retCode = "0000";
            this.kpjh = string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Client") ? int.Parse(string.IsNullOrEmpty(TaxCardFactory.CreateTaxCard().Reserve) ? TaxCardFactory.CreateTaxCard().Machine.ToString() : TaxCardFactory.CreateTaxCard().Reserve) : TaxCardFactory.CreateTaxCard().Machine;
            this.bmbbbh = string.Empty;
            this.fpdm = string.Empty;
            this.fphm = string.Empty;
            this.hxm = string.Empty;
            this.mw = string.Empty;
            this.jym = string.Empty;
            this.kprq = string.Empty;
            this.zyspmc = string.Empty;
            this.zyspsm = string.Empty;
            this.sbbz = string.Empty;
            this.gfsh = string.Empty;
            this.gfmc = string.Empty;
            this.gfbh = string.Empty;
            this.gfdzdh = string.Empty;
            this.gfyhzh = string.Empty;
            this.xfsh = string.Empty;
            this.xfmc = string.Empty;
            this.xfdzdh = string.Empty;
            this.xfyhzh = string.Empty;
            this.bz = string.Empty;
            this.sLv = string.Empty;
            this.je = string.Empty;
            this.se = string.Empty;
            this.kpr = string.Empty;
            this.skr = string.Empty;
            this.fhr = string.Empty;
            this.xsdjbh = string.Empty;
            this.redNum = string.Empty;
            this.blueFpdm = string.Empty;
            this.blueFphm = string.Empty;
            this.zfsj = string.Empty;
            this.jmbbh = string.Empty;
            this.yysbz = string.Empty;
            this.sign = string.Empty;
            this.jqbh = string.Empty;
            this.czch = string.Empty;
            this.ccdw = string.Empty;
            this.cyrnsrsbh = string.Empty;
            this.cyrmc = string.Empty;
            this.spfnsrsbh = string.Empty;
            this.spfmc = string.Empty;
            this.shrnsrsbh = string.Empty;
            this.shrmc = string.Empty;
            this.fhrnsrsbh = string.Empty;
            this.fhrmc = string.Empty;
            this.qyd = string.Empty;
            this.yshwxx = string.Empty;
            this.zgswjgdm = string.Empty;
            this.zgswjgmc = string.Empty;
            this.sfzhm = string.Empty;
            this.xfdz = string.Empty;
            this.xfdh = string.Empty;
            this.xfyh = string.Empty;
            this.xfzh = string.Empty;
            this.cpxh = string.Empty;
            this.cllx = string.Empty;
            this.hgzh = string.Empty;
            this.jkzmsh = string.Empty;
            this.cd = string.Empty;
            this.sjdh = string.Empty;
            this.fdjhm = string.Empty;
            this.clsbdh = string.Empty;
            this.dw = string.Empty;
            this.xcrs = string.Empty;
            this.sccjmc = string.Empty;
            this.isNewJdcfp = true;
            this.dkqysh = string.Empty;
            this.dkqymc = string.Empty;
            this.wspzhm = string.Empty;
            this.dy_mb = string.Empty;
            this.string_1 = new string[] { "0.17", "0.13", "0.06", "0.04", "0.05" };
            this.jskFpmxVersion_0 = JskFpmxVersion.V2;
        }

        public Fpxx(FPLX fplx_0, string string_2, string string_3)
        {
            
            this.retCode = "0000";
            this.kpjh = string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Client") ? int.Parse(string.IsNullOrEmpty(TaxCardFactory.CreateTaxCard().Reserve) ? TaxCardFactory.CreateTaxCard().Machine.ToString() : TaxCardFactory.CreateTaxCard().Reserve) : TaxCardFactory.CreateTaxCard().Machine;
            this.bmbbbh = string.Empty;
            this.fpdm = string.Empty;
            this.fphm = string.Empty;
            this.hxm = string.Empty;
            this.mw = string.Empty;
            this.jym = string.Empty;
            this.kprq = string.Empty;
            this.zyspmc = string.Empty;
            this.zyspsm = string.Empty;
            this.sbbz = string.Empty;
            this.gfsh = string.Empty;
            this.gfmc = string.Empty;
            this.gfbh = string.Empty;
            this.gfdzdh = string.Empty;
            this.gfyhzh = string.Empty;
            this.xfsh = string.Empty;
            this.xfmc = string.Empty;
            this.xfdzdh = string.Empty;
            this.xfyhzh = string.Empty;
            this.bz = string.Empty;
            this.sLv = string.Empty;
            this.je = string.Empty;
            this.se = string.Empty;
            this.kpr = string.Empty;
            this.skr = string.Empty;
            this.fhr = string.Empty;
            this.xsdjbh = string.Empty;
            this.redNum = string.Empty;
            this.blueFpdm = string.Empty;
            this.blueFphm = string.Empty;
            this.zfsj = string.Empty;
            this.jmbbh = string.Empty;
            this.yysbz = string.Empty;
            this.sign = string.Empty;
            this.jqbh = string.Empty;
            this.czch = string.Empty;
            this.ccdw = string.Empty;
            this.cyrnsrsbh = string.Empty;
            this.cyrmc = string.Empty;
            this.spfnsrsbh = string.Empty;
            this.spfmc = string.Empty;
            this.shrnsrsbh = string.Empty;
            this.shrmc = string.Empty;
            this.fhrnsrsbh = string.Empty;
            this.fhrmc = string.Empty;
            this.qyd = string.Empty;
            this.yshwxx = string.Empty;
            this.zgswjgdm = string.Empty;
            this.zgswjgmc = string.Empty;
            this.sfzhm = string.Empty;
            this.xfdz = string.Empty;
            this.xfdh = string.Empty;
            this.xfyh = string.Empty;
            this.xfzh = string.Empty;
            this.cpxh = string.Empty;
            this.cllx = string.Empty;
            this.hgzh = string.Empty;
            this.jkzmsh = string.Empty;
            this.cd = string.Empty;
            this.sjdh = string.Empty;
            this.fdjhm = string.Empty;
            this.clsbdh = string.Empty;
            this.dw = string.Empty;
            this.xcrs = string.Empty;
            this.sccjmc = string.Empty;
            this.isNewJdcfp = true;
            this.dkqysh = string.Empty;
            this.dkqymc = string.Empty;
            this.wspzhm = string.Empty;
            this.dy_mb = string.Empty;
            this.string_1 = new string[] { "0.17", "0.13", "0.06", "0.04", "0.05" };
            this.jskFpmxVersion_0 = JskFpmxVersion.V2;
            this.fplx = fplx_0;
            this.fpdm = string_2;
            this.fphm = string_3;
        }

        public Fpxx Clone()
        {
            Fpxx fpxx = new Fpxx {
                bmbbbh = this.bmbbbh,
                sbbz = this.sbbz,
                blueFpdm = this.blueFpdm,
                blueFphm = this.blueFphm,
                bsbz = this.bsbz,
                bsq = this.bsq,
                bszt = this.bszt,
                bz = this.bz,
                ccdw = this.ccdw,
                cd = this.cd,
                cllx = this.cllx,
                clsbdh = this.clsbdh,
                cpxh = this.cpxh,
                CustomFields = this.CustomFields,
                cyrmc = this.cyrmc,
                cyrnsrsbh = this.cyrnsrsbh,
                czch = this.czch,
                data = this.data,
                dkqymc = this.dkqymc,
                dkqysh = this.dkqysh,
                dw = this.dw,
                dybz = this.dybz,
                dzsyh = this.dzsyh,
                fdjhm = this.fdjhm,
                fhr = this.fhr,
                fhrmc = this.fhrmc,
                fhrnsrsbh = this.fhrnsrsbh,
                fpdm = this.fpdm,
                fphm = this.fphm,
                fplx = this.fplx,
                gfbh = this.gfbh,
                gfdzdh = this.gfdzdh,
                gfmc = this.gfmc,
                gfsh = this.gfsh,
                gfyhzh = this.gfyhzh,
                hgzh = this.hgzh,
                hxm = this.hxm,
                hzfw = this.hzfw,
                invQryNo = this.invQryNo,
                isBlankWaste = this.isBlankWaste,
                isNewJdcfp = this.isNewJdcfp,
                isRed = this.isRed,
                bool_0 = this.bool_0,
                je = this.je,
                jkzmsh = this.jkzmsh,
                jmbbh = this.jmbbh,
                jqbh = this.jqbh,
                jskFpmxVersion_0 = this.jskFpmxVersion_0,
                jym = this.jym,
                keyFlagNo = this.keyFlagNo,
                kpjh = this.kpjh,
                kpr = this.kpr,
                kprq = this.kprq,
                kpsxh = this.kpsxh,
                mw = this.mw,
                dy_mb = this.dy_mb
            };
            if (this.Mxxx != null)
            {
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                foreach (Dictionary<SPXX, string> dictionary3 in this.Mxxx)
                {
                    fpxx.Mxxx.Add(dictionary3);
                }
            }
            else
            {
                fpxx.Mxxx = null;
            }
            fpxx.OtherFields = this.OtherFields;
            if (this.Qdxx != null)
            {
                fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                foreach (Dictionary<SPXX, string> dictionary in this.Qdxx)
                {
                    fpxx.Qdxx.Add(dictionary);
                }
            }
            else
            {
                fpxx.Qdxx = null;
            }
            fpxx.qyd = this.qyd;
            fpxx.rdmByte = this.rdmByte;
            fpxx.redNum = this.redNum;
            fpxx.retCode = this.retCode;
            fpxx.sccjmc = this.sccjmc;
            fpxx.se = this.se;
            fpxx.sfzhm = this.sfzhm;
            fpxx.shrmc = this.shrmc;
            fpxx.shrnsrsbh = this.shrnsrsbh;
            fpxx.sign = this.sign;
            fpxx.sjdh = this.sjdh;
            fpxx.skr = this.skr;
            fpxx.sLv = this.sLv;
            fpxx.spfmc = this.spfmc;
            fpxx.spfnsrsbh = this.spfnsrsbh;
            fpxx.ssyf = this.ssyf;
            if (this.string_1 != null)
            {
                fpxx.string_1 = new string[this.string_1.Length];
                Array.Copy(this.string_1, fpxx.string_1, this.string_1.Length);
            }
            fpxx.wspzhm = this.xcrs;
            fpxx.xcrs = this.xcrs;
            fpxx.xfbz = this.xfbz;
            fpxx.xfdh = this.xfdh;
            fpxx.xfdz = this.xfdz;
            fpxx.xfdzdh = this.xfdzdh;
            fpxx.xfmc = this.xfmc;
            fpxx.xfsh = this.xfsh;
            fpxx.xfyh = this.xfyh;
            fpxx.xfyhzh = this.xfyhzh;
            fpxx.xfzh = this.xfzh;
            fpxx.xsdjbh = this.xsdjbh;
            fpxx.yshwxx = this.yshwxx;
            fpxx.yysbz = this.yysbz;
            fpxx.zfbz = this.zfbz;
            fpxx.zfsj = this.zfsj;
            fpxx.zgswjgdm = this.zgswjgdm;
            fpxx.zgswjgmc = this.zgswjgmc;
            fpxx.Zyfplx = this.Zyfplx;
            fpxx.zyspmc = this.zyspmc;
            fpxx.zyspsm = this.zyspsm;
            return fpxx;
        }

        public static bool Copy(Fpxx fpxx_0, Fpxx fpxx_1)
        {
            bool flag;
            if ((fpxx_0 == null) || (fpxx_1 == null))
            {
                return false;
            }
            try
            {
                fpxx_1.blueFpdm = fpxx_0.blueFpdm;
                fpxx_1.blueFphm = fpxx_0.blueFphm;
                fpxx_1.bsbz = fpxx_0.bsbz;
                fpxx_1.bsq = fpxx_0.bsq;
                fpxx_1.bszt = fpxx_0.bszt;
                fpxx_1.bz = fpxx_0.bz;
                fpxx_1.ccdw = fpxx_0.ccdw;
                fpxx_1.cd = fpxx_0.cd;
                fpxx_1.cllx = fpxx_0.cllx;
                fpxx_1.clsbdh = fpxx_0.clsbdh;
                fpxx_1.cpxh = fpxx_0.cpxh;
                fpxx_1.CustomFields = fpxx_0.CustomFields;
                fpxx_1.cyrmc = fpxx_0.cyrmc;
                fpxx_1.cyrnsrsbh = fpxx_0.cyrnsrsbh;
                fpxx_1.czch = fpxx_0.czch;
                fpxx_1.data = fpxx_0.data;
                fpxx_1.dkqymc = fpxx_0.dkqymc;
                fpxx_1.dkqysh = fpxx_0.dkqysh;
                fpxx_1.dw = fpxx_0.dw;
                fpxx_1.dybz = fpxx_0.dybz;
                fpxx_1.dzsyh = fpxx_0.dzsyh;
                fpxx_1.fdjhm = fpxx_0.fdjhm;
                fpxx_1.fhr = fpxx_0.fhr;
                fpxx_1.fhrmc = fpxx_0.fhrmc;
                fpxx_1.fhrnsrsbh = fpxx_0.fhrnsrsbh;
                fpxx_1.fpdm = fpxx_0.fpdm;
                fpxx_1.fphm = fpxx_0.fphm;
                fpxx_1.fplx = fpxx_0.fplx;
                fpxx_1.gfbh = fpxx_0.gfbh;
                fpxx_1.gfdzdh = fpxx_0.gfdzdh;
                fpxx_1.gfmc = fpxx_0.gfmc;
                fpxx_1.gfsh = fpxx_0.gfsh;
                fpxx_1.gfyhzh = fpxx_0.gfyhzh;
                fpxx_1.hgzh = fpxx_0.hgzh;
                fpxx_1.hxm = fpxx_0.hxm;
                fpxx_1.hzfw = fpxx_0.hzfw;
                fpxx_1.invQryNo = fpxx_0.invQryNo;
                fpxx_1.isBlankWaste = fpxx_0.isBlankWaste;
                fpxx_1.isNewJdcfp = fpxx_0.isNewJdcfp;
                fpxx_1.isRed = fpxx_0.isRed;
                fpxx_1.bool_0 = fpxx_0.bool_0;
                fpxx_1.je = fpxx_0.je;
                fpxx_1.jkzmsh = fpxx_0.jkzmsh;
                fpxx_1.jmbbh = fpxx_0.jmbbh;
                fpxx_1.jqbh = fpxx_0.jqbh;
                fpxx_1.jskFpmxVersion_0 = fpxx_0.jskFpmxVersion_0;
                fpxx_1.jym = fpxx_0.jym;
                fpxx_1.keyFlagNo = fpxx_0.keyFlagNo;
                fpxx_1.kpjh = fpxx_0.kpjh;
                fpxx_1.kpr = fpxx_0.kpr;
                fpxx_1.kprq = fpxx_0.kprq;
                fpxx_1.kpsxh = fpxx_0.kpsxh;
                fpxx_1.mw = fpxx_0.mw;
                fpxx_1.dy_mb = fpxx_0.dy_mb;
                if (fpxx_0.Mxxx != null)
                {
                    fpxx_1.Mxxx = new List<Dictionary<SPXX, string>>();
                    foreach (Dictionary<SPXX, string> dictionary in fpxx_0.Mxxx)
                    {
                        fpxx_1.Mxxx.Add(dictionary);
                    }
                }
                else
                {
                    fpxx_1.Mxxx = null;
                }
                fpxx_1.OtherFields = fpxx_0.OtherFields;
                if (fpxx_0.Qdxx != null)
                {
                    fpxx_1.Qdxx = new List<Dictionary<SPXX, string>>();
                    foreach (Dictionary<SPXX, string> dictionary3 in fpxx_0.Qdxx)
                    {
                        fpxx_1.Qdxx.Add(dictionary3);
                    }
                }
                else
                {
                    fpxx_1.Qdxx = null;
                }
                fpxx_1.qyd = fpxx_0.qyd;
                fpxx_1.rdmByte = fpxx_0.rdmByte;
                fpxx_1.redNum = fpxx_0.redNum;
                fpxx_1.retCode = fpxx_0.retCode;
                fpxx_1.sccjmc = fpxx_0.sccjmc;
                fpxx_1.se = fpxx_0.se;
                fpxx_1.sfzhm = fpxx_0.sfzhm;
                fpxx_1.shrmc = fpxx_0.shrmc;
                fpxx_1.shrnsrsbh = fpxx_0.shrnsrsbh;
                fpxx_1.sign = fpxx_0.sign;
                fpxx_1.sjdh = fpxx_0.sjdh;
                fpxx_1.skr = fpxx_0.skr;
                fpxx_1.sLv = fpxx_0.sLv;
                fpxx_1.spfmc = fpxx_0.spfmc;
                fpxx_1.spfnsrsbh = fpxx_0.spfnsrsbh;
                fpxx_1.ssyf = fpxx_0.ssyf;
                if (fpxx_0.string_1 != null)
                {
                    fpxx_1.string_1 = new string[fpxx_0.string_1.Length];
                    Array.Copy(fpxx_0.string_1, fpxx_1.string_1, fpxx_0.string_1.Length);
                }
                else
                {
                    fpxx_1.string_1 = null;
                }
                fpxx_1.wspzhm = fpxx_0.xcrs;
                fpxx_1.xcrs = fpxx_0.xcrs;
                fpxx_1.xfbz = fpxx_0.xfbz;
                fpxx_1.xfdh = fpxx_0.xfdh;
                fpxx_1.xfdz = fpxx_0.xfdz;
                fpxx_1.xfdzdh = fpxx_0.xfdzdh;
                fpxx_1.xfmc = fpxx_0.xfmc;
                fpxx_1.xfsh = fpxx_0.xfsh;
                fpxx_1.xfyh = fpxx_0.xfyh;
                fpxx_1.xfyhzh = fpxx_0.xfyhzh;
                fpxx_1.xfzh = fpxx_0.xfzh;
                fpxx_1.xsdjbh = fpxx_0.xsdjbh;
                fpxx_1.yshwxx = fpxx_0.yshwxx;
                fpxx_1.yysbz = fpxx_0.yysbz;
                fpxx_1.zfbz = fpxx_0.zfbz;
                fpxx_1.zfsj = fpxx_0.zfsj;
                fpxx_1.zgswjgdm = fpxx_0.zgswjgdm;
                fpxx_1.zgswjgmc = fpxx_0.zgswjgmc;
                fpxx_1.Zyfplx = fpxx_0.Zyfplx;
                fpxx_1.zyspmc = fpxx_0.zyspmc;
                fpxx_1.zyspsm = fpxx_0.zyspsm;
                return true;
            }
            catch (Exception exception)
            {
                ilog_0.Error("Fpxx.Copy 出现异常：" + exception.Message);
                flag = false;
            }
            return flag;
        }

        public static Fpxx DeSeriealize(byte[] byte_0)
        {
            byte[] buffer = new byte[] { 
                0x33, 0x95, 0x9d, 0x54, 30, 0x57, 0x9c, 0xb9, 0x2a, 0x8b, 0xb5, 0x6a, 0x1c, 0x9d, 0xff, 0xb2, 
                0x2d, 0xbb, 0xae, 0xcd, 0x5c, 240, 0x5f, 0x2f, 0x4d, 0x9e, 0x8b, 170, 0xcb, 0x9e, 0xbf, 13
             };
            byte[] buffer2 = new byte[] { 0xa1, 0xac, 0xbb, 0xba, 0xfb, 9, 200, 0xb9, 12, 0x9f, 0x56, 0xa9, 0xbd, 0xca, 0x6b, 0xf3 };
            AES_Crypt.Decrypt(byte_0, buffer, buffer2, null);
            XmlDocument document = new XmlDocument();
            document.LoadXml(ToolUtil.GetString(byte_0));
            Fpxx fpxx = new Fpxx();
            XmlNode node = document.SelectSingleNode("//FP");
            fpxx.blueFpdm = node.SelectSingleNode("blueFpdm").InnerText;
            fpxx.blueFphm = node.SelectSingleNode("blueFphm").InnerText;
            fpxx.bsbz = node.SelectSingleNode("bsbz").InnerText.Equals("1");
            fpxx.bsq = int.Parse(node.SelectSingleNode("bsq").InnerText);
            fpxx.bszt = int.Parse(node.SelectSingleNode("bszt").InnerText);
            fpxx.bz = node.SelectSingleNode("bz").InnerText;
            fpxx.ccdw = node.SelectSingleNode("ccdw").InnerText;
            fpxx.cd = node.SelectSingleNode("cd").InnerText;
            fpxx.cllx = node.SelectSingleNode("cllx").InnerText;
            fpxx.clsbdh = node.SelectSingleNode("clsbdh").InnerText;
            fpxx.cpxh = node.SelectSingleNode("cpxh").InnerText;
            fpxx.cyrmc = node.SelectSingleNode("cyrmc").InnerText;
            fpxx.cyrnsrsbh = node.SelectSingleNode("cyrnsrsbh").InnerText;
            fpxx.czch = node.SelectSingleNode("czch").InnerText;
            fpxx.dkqymc = node.SelectSingleNode("dkqymc").InnerText;
            fpxx.dkqysh = node.SelectSingleNode("dkqysh").InnerText;
            fpxx.dw = node.SelectSingleNode("dw").InnerText;
            fpxx.dybz = node.SelectSingleNode("dybz").InnerText.Equals("1");
            fpxx.dzsyh = node.SelectSingleNode("dzsyh").InnerText;
            fpxx.fdjhm = node.SelectSingleNode("fdjhm").InnerText;
            fpxx.fhr = node.SelectSingleNode("fhr").InnerText;
            fpxx.fhrmc = node.SelectSingleNode("fhrmc").InnerText;
            fpxx.fhrnsrsbh = node.SelectSingleNode("fhrnsrsbh").InnerText;
            fpxx.fpdm = node.SelectSingleNode("fpdm").InnerText;
            fpxx.fphm = node.SelectSingleNode("fphm").InnerText;
            fpxx.fplx = Invoice.ParseFPLX(node.SelectSingleNode("fplx").InnerText);
            fpxx.gfbh = node.SelectSingleNode("gfbh").InnerText;
            fpxx.gfdzdh = node.SelectSingleNode("gfdzdh").InnerText;
            fpxx.gfmc = node.SelectSingleNode("gfmc").InnerText;
            fpxx.gfsh = node.SelectSingleNode("gfsh").InnerText;
            fpxx.gfyhzh = node.SelectSingleNode("gfyhzh").InnerText;
            fpxx.hgzh = node.SelectSingleNode("hgzh").InnerText;
            fpxx.hxm = node.SelectSingleNode("hxm").InnerText;
            fpxx.hzfw = node.SelectSingleNode("hzfw").InnerText.Equals("1");
            fpxx.invQryNo = long.Parse(node.SelectSingleNode("invQryNo").InnerText);
            fpxx.isBlankWaste = node.SelectSingleNode("isBlankWaste").InnerText.Equals("1");
            fpxx.isNewJdcfp = node.SelectSingleNode("isNewJdcfp").InnerText.Equals("1");
            fpxx.isRed = node.SelectSingleNode("isRed").InnerText.Equals("1");
            fpxx.bool_0 = node.SelectSingleNode("isSqd").InnerText.Equals("1");
            fpxx.je = node.SelectSingleNode("je").InnerText;
            fpxx.jkzmsh = node.SelectSingleNode("jkzmsh").InnerText;
            fpxx.jmbbh = node.SelectSingleNode("jmbbh").InnerText;
            fpxx.jqbh = node.SelectSingleNode("jqbh").InnerText;
            fpxx.jskFpmxVersion_0 = (JskFpmxVersion) int.Parse(node.SelectSingleNode("jsk_fpmx_version").InnerText);
            fpxx.jym = node.SelectSingleNode("jym").InnerText;
            fpxx.keyFlagNo = ulong.Parse(node.SelectSingleNode("keyFlagNo").InnerText);
            fpxx.kpjh = int.Parse(node.SelectSingleNode("kpjh").InnerText);
            fpxx.kpr = node.SelectSingleNode("kpr").InnerText;
            fpxx.kprq = node.SelectSingleNode("kprq").InnerText;
            fpxx.kpsxh = node.SelectSingleNode("kpsxh").InnerText;
            fpxx.mw = node.SelectSingleNode("mw").InnerText;
            fpxx.string_0 = node.SelectSingleNode("mxhjje").InnerText;
            fpxx.qyd = node.SelectSingleNode("qyd").InnerText;
            string innerText = node.SelectSingleNode("rdmByte").InnerText;
            fpxx.rdmByte = (innerText.Length == 0) ? null : Convert.FromBase64String(innerText);
            fpxx.redNum = node.SelectSingleNode("redNum").InnerText;
            fpxx.retCode = node.SelectSingleNode("retCode").InnerText;
            fpxx.sccjmc = node.SelectSingleNode("sccjmc").InnerText;
            fpxx.se = node.SelectSingleNode("se").InnerText;
            fpxx.sfzhm = node.SelectSingleNode("sfzhm").InnerText;
            fpxx.shrmc = node.SelectSingleNode("shrmc").InnerText;
            fpxx.shrnsrsbh = node.SelectSingleNode("shrnsrsbh").InnerText;
            fpxx.sign = node.SelectSingleNode("sign").InnerText;
            fpxx.sjdh = node.SelectSingleNode("sjdh").InnerText;
            fpxx.skr = node.SelectSingleNode("skr").InnerText;
            fpxx.sLv = node.SelectSingleNode("sLv").InnerText;
            fpxx.spfmc = node.SelectSingleNode("spfmc").InnerText;
            fpxx.spfnsrsbh = node.SelectSingleNode("spfnsrsbh").InnerText;
            fpxx.ssyf = int.Parse(node.SelectSingleNode("ssyf").InnerText);
            fpxx.dy_mb = node.SelectSingleNode("dymb").InnerText;
            XmlNodeList list2 = node.SelectNodes("TAX_CLASS/value");
            if (list2 != null)
            {
                List<string> list3 = new List<string>();
                for (int i = 0; i < list2.Count; i++)
                {
                    list3.Add(list2[i].InnerText);
                }
                fpxx.string_1 = list3.ToArray();
            }
            fpxx.wspzhm = node.SelectSingleNode("wspzhm").InnerText;
            fpxx.xcrs = node.SelectSingleNode("xcrs").InnerText;
            fpxx.xfbz = node.SelectSingleNode("xfbz").InnerText.Equals("1");
            fpxx.xfdh = node.SelectSingleNode("xfdh").InnerText;
            fpxx.xfdz = node.SelectSingleNode("xfdz").InnerText;
            fpxx.xfdzdh = node.SelectSingleNode("xfdzdh").InnerText;
            fpxx.xfmc = node.SelectSingleNode("xfmc").InnerText;
            fpxx.xfsh = node.SelectSingleNode("xfsh").InnerText;
            fpxx.xfyh = node.SelectSingleNode("xfyh").InnerText;
            fpxx.xfyhzh = node.SelectSingleNode("xfyhzh").InnerText;
            fpxx.xfzh = node.SelectSingleNode("xfzh").InnerText;
            fpxx.xsdjbh = node.SelectSingleNode("xsdjbh").InnerText;
            fpxx.yshwxx = node.SelectSingleNode("yshwxx").InnerText;
            fpxx.yysbz = node.SelectSingleNode("yysbz").InnerText;
            fpxx.zfbz = node.SelectSingleNode("zfbz").InnerText.Equals("1");
            fpxx.zfsj = node.SelectSingleNode("zfsj").InnerText;
            fpxx.zgswjgdm = node.SelectSingleNode("zgswjgdm").InnerText;
            fpxx.zgswjgmc = node.SelectSingleNode("zgswjgmc").InnerText;
            fpxx.Zyfplx = (ZYFP_LX) int.Parse(node.SelectSingleNode("zyfpLx").InnerText);
            fpxx.zyspmc = node.SelectSingleNode("zyspmc").InnerText;
            fpxx.zyspsm = node.SelectSingleNode("zyspsm").InnerText;
            list2 = node.SelectNodes("Mxxx/spxx");
            if (list2 != null)
            {
                List<Dictionary<SPXX, string>> list5 = new List<Dictionary<SPXX, string>>();
                for (int j = 0; j < list2.Count; j++)
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    XmlNodeList list4 = list2[j].SelectNodes("sp");
                    for (int k = 0; k < list4.Count; k++)
                    {
                        item.Add((SPXX) int.Parse(list4[k].Attributes["key"].Value), list4[k].Attributes["value"].Value);
                    }
                    list5.Add(item);
                }
                fpxx.Mxxx = list5;
            }
            list2 = node.SelectNodes("Qdxx/spxx");
            if ((list2 != null) && (list2.Count > 0))
            {
                List<Dictionary<SPXX, string>> list6 = new List<Dictionary<SPXX, string>>();
                for (int m = 0; m < list2.Count; m++)
                {
                    Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                    XmlNodeList list = list2[m].SelectNodes("sp");
                    for (int n = 0; n < list.Count; n++)
                    {
                        dictionary.Add((SPXX) int.Parse(list[n].Attributes["key"].Value), list[n].Attributes["value"].Value);
                    }
                    list6.Add(dictionary);
                }
                fpxx.Qdxx = list6;
            }
            XmlNode node2 = document.SelectSingleNode("Data");
            if (node2 != null)
            {
                object[] objArray = new object[3];
                objArray[0] = Convert.FromBase64String(node2.SelectSingleNode("Data1").InnerText);
                string[] strArray = node2.SelectSingleNode("Data2").InnerText.Split(new char[] { ';' });
                for (int num6 = 0; num6 < strArray.Length; num6++)
                {
                    strArray[num6] = (strArray[num6].Length == 0) ? null : ToolUtil.GetString(Convert.FromBase64String(strArray[num6]));
                }
                objArray[1] = strArray;
                objArray[2] = Convert.FromBase64String(node2.SelectSingleNode("Data3").InnerText);
                fpxx.data = objArray;
            }
            return fpxx;
        }

        public static Fpxx DeSeriealize_Linux(byte[] byte_0)
        {
            string name = "GB18030";
            string xml = Encoding.GetEncoding(name).GetString(byte_0);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            Fpxx fpxx = new Fpxx();
            XmlNode node = document.SelectSingleNode("//FP");
            fpxx.blueFpdm = node.SelectSingleNode("blueFpdm").InnerText;
            fpxx.blueFphm = node.SelectSingleNode("blueFphm").InnerText;
            fpxx.bsbz = node.SelectSingleNode("bsbz").InnerText.Equals("1");
            fpxx.bsq = int.Parse(node.SelectSingleNode("bsq").InnerText);
            fpxx.bszt = int.Parse(node.SelectSingleNode("bszt").InnerText);
            fpxx.bz = node.SelectSingleNode("bz").InnerText;
            fpxx.ccdw = node.SelectSingleNode("ccdw").InnerText;
            fpxx.cd = node.SelectSingleNode("cd").InnerText;
            fpxx.cllx = node.SelectSingleNode("cllx").InnerText;
            fpxx.clsbdh = node.SelectSingleNode("clsbdh").InnerText;
            fpxx.cpxh = node.SelectSingleNode("cpxh").InnerText;
            fpxx.cyrmc = node.SelectSingleNode("cyrmc").InnerText;
            fpxx.cyrnsrsbh = node.SelectSingleNode("cyrnsrsbh").InnerText;
            fpxx.czch = node.SelectSingleNode("czch").InnerText;
            fpxx.dkqymc = node.SelectSingleNode("dkqymc").InnerText;
            fpxx.dkqysh = node.SelectSingleNode("dkqysh").InnerText;
            fpxx.dw = node.SelectSingleNode("dw").InnerText;
            fpxx.dybz = node.SelectSingleNode("dybz").InnerText.Equals("1");
            fpxx.dzsyh = node.SelectSingleNode("dzsyh").InnerText;
            fpxx.fdjhm = node.SelectSingleNode("fdjhm").InnerText;
            fpxx.fhr = node.SelectSingleNode("fhr").InnerText;
            fpxx.fhrmc = node.SelectSingleNode("fhrmc").InnerText;
            fpxx.fhrnsrsbh = node.SelectSingleNode("fhrnsrsbh").InnerText;
            fpxx.fpdm = node.SelectSingleNode("fpdm").InnerText;
            fpxx.fphm = node.SelectSingleNode("fphm").InnerText;
            fpxx.fplx = Invoice.ParseFPLX(node.SelectSingleNode("fplx").InnerText);
            fpxx.gfbh = node.SelectSingleNode("gfbh").InnerText;
            fpxx.gfdzdh = node.SelectSingleNode("gfdzdh").InnerText;
            fpxx.gfmc = node.SelectSingleNode("gfmc").InnerText;
            fpxx.gfsh = node.SelectSingleNode("gfsh").InnerText;
            fpxx.gfyhzh = node.SelectSingleNode("gfyhzh").InnerText;
            fpxx.hgzh = node.SelectSingleNode("hgzh").InnerText;
            fpxx.hxm = node.SelectSingleNode("hxm").InnerText;
            fpxx.hzfw = node.SelectSingleNode("hzfw").InnerText.Equals("1");
            fpxx.invQryNo = long.Parse(node.SelectSingleNode("invQryNo").InnerText);
            fpxx.isBlankWaste = node.SelectSingleNode("isBlankWaste").InnerText.Equals("1");
            fpxx.isNewJdcfp = node.SelectSingleNode("isNewJdcfp").InnerText.Equals("1");
            fpxx.isRed = node.SelectSingleNode("isRed").InnerText.Equals("1");
            fpxx.bool_0 = node.SelectSingleNode("isSqd").InnerText.Equals("1");
            fpxx.je = node.SelectSingleNode("je").InnerText;
            fpxx.jkzmsh = node.SelectSingleNode("jkzmsh").InnerText;
            fpxx.jmbbh = node.SelectSingleNode("jmbbh").InnerText;
            fpxx.jqbh = node.SelectSingleNode("jqbh").InnerText;
            fpxx.jskFpmxVersion_0 = (JskFpmxVersion) int.Parse(node.SelectSingleNode("jsk_fpmx_version").InnerText);
            fpxx.jym = node.SelectSingleNode("jym").InnerText;
            fpxx.keyFlagNo = ulong.Parse(node.SelectSingleNode("keyFlagNo").InnerText);
            fpxx.kpjh = int.Parse(node.SelectSingleNode("kpjh").InnerText);
            fpxx.kpr = node.SelectSingleNode("kpr").InnerText;
            fpxx.kprq = node.SelectSingleNode("kprq").InnerText;
            fpxx.kpsxh = node.SelectSingleNode("kpsxh").InnerText;
            fpxx.mw = node.SelectSingleNode("mw").InnerText;
            fpxx.string_0 = node.SelectSingleNode("mxhjje").InnerText;
            fpxx.qyd = node.SelectSingleNode("qyd").InnerText;
            string innerText = node.SelectSingleNode("rdmByte").InnerText;
            fpxx.rdmByte = (innerText.Length == 0) ? null : Convert.FromBase64String(innerText);
            fpxx.redNum = node.SelectSingleNode("redNum").InnerText;
            fpxx.retCode = node.SelectSingleNode("retCode").InnerText;
            fpxx.sccjmc = node.SelectSingleNode("sccjmc").InnerText;
            fpxx.se = node.SelectSingleNode("se").InnerText;
            fpxx.sfzhm = node.SelectSingleNode("sfzhm").InnerText;
            fpxx.shrmc = node.SelectSingleNode("shrmc").InnerText;
            fpxx.shrnsrsbh = node.SelectSingleNode("shrnsrsbh").InnerText;
            fpxx.sign = node.SelectSingleNode("sign").InnerText;
            fpxx.sjdh = node.SelectSingleNode("sjdh").InnerText;
            fpxx.skr = node.SelectSingleNode("skr").InnerText;
            fpxx.sLv = node.SelectSingleNode("sLv").InnerText;
            fpxx.spfmc = node.SelectSingleNode("spfmc").InnerText;
            fpxx.spfnsrsbh = node.SelectSingleNode("spfnsrsbh").InnerText;
            fpxx.ssyf = int.Parse(node.SelectSingleNode("ssyf").InnerText);
            fpxx.dy_mb = node.SelectSingleNode("dymb").InnerText;
            XmlNodeList list = node.SelectNodes("TAX_CLASS/value");
            if (list != null)
            {
                List<string> list2 = new List<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    list2.Add(list[i].InnerText);
                }
                fpxx.string_1 = list2.ToArray();
            }
            fpxx.wspzhm = node.SelectSingleNode("wspzhm").InnerText;
            fpxx.xcrs = node.SelectSingleNode("xcrs").InnerText;
            fpxx.xfbz = node.SelectSingleNode("xfbz").InnerText.Equals("1");
            fpxx.xfdh = node.SelectSingleNode("xfdh").InnerText;
            fpxx.xfdz = node.SelectSingleNode("xfdz").InnerText;
            fpxx.xfdzdh = node.SelectSingleNode("xfdzdh").InnerText;
            fpxx.xfmc = node.SelectSingleNode("xfmc").InnerText;
            fpxx.xfsh = node.SelectSingleNode("xfsh").InnerText;
            fpxx.xfyh = node.SelectSingleNode("xfyh").InnerText;
            fpxx.xfyhzh = node.SelectSingleNode("xfyhzh").InnerText;
            fpxx.xfzh = node.SelectSingleNode("xfzh").InnerText;
            fpxx.xsdjbh = node.SelectSingleNode("xsdjbh").InnerText;
            fpxx.yshwxx = node.SelectSingleNode("yshwxx").InnerText;
            fpxx.yysbz = node.SelectSingleNode("yysbz").InnerText;
            fpxx.zfbz = node.SelectSingleNode("zfbz").InnerText.Equals("1");
            fpxx.zfsj = node.SelectSingleNode("zfsj").InnerText;
            fpxx.zgswjgdm = node.SelectSingleNode("zgswjgdm").InnerText;
            fpxx.zgswjgmc = node.SelectSingleNode("zgswjgmc").InnerText;
            fpxx.Zyfplx = (ZYFP_LX) int.Parse(node.SelectSingleNode("zyfpLx").InnerText);
            fpxx.zyspmc = node.SelectSingleNode("zyspmc").InnerText;
            fpxx.zyspsm = node.SelectSingleNode("zyspsm").InnerText;
            fpxx.bmbbbh = node.SelectSingleNode("bmbbbh").InnerText;
            list = node.SelectNodes("Mxxx/spxx");
            if (list != null)
            {
                List<Dictionary<SPXX, string>> list6 = new List<Dictionary<SPXX, string>>();
                for (int j = 0; j < list.Count; j++)
                {
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    XmlNodeList list3 = list[j].SelectNodes("sp");
                    for (int k = 0; k < list3.Count; k++)
                    {
                        SPXX key = (SPXX) Enum.Parse(typeof(SPXX), list3[k].Attributes["key"].Value);
                        string str4 = list3[k].Attributes["value"].Value;
                        item.Add(key, str4);
                    }
                    list6.Add(item);
                }
                fpxx.Mxxx = list6;
            }
            list = node.SelectNodes("Qdxx/spxx");
            if ((list != null) && (list.Count > 0))
            {
                List<Dictionary<SPXX, string>> list4 = new List<Dictionary<SPXX, string>>();
                for (int m = 0; m < list.Count; m++)
                {
                    Dictionary<SPXX, string> dictionary2 = new Dictionary<SPXX, string>();
                    XmlNodeList list5 = list[m].SelectNodes("sp");
                    for (int n = 0; n < list5.Count; n++)
                    {
                        SPXX spxx2 = (SPXX) Enum.Parse(typeof(SPXX), list5[n].Attributes["key"].Value);
                        string str5 = list5[n].Attributes["value"].Value;
                        dictionary2.Add(spxx2, str5);
                    }
                    list4.Add(dictionary2);
                }
                fpxx.Qdxx = list4;
            }
            fpxx.retCode = "0000";
            return fpxx;
        }

        public string Get_Print_Dj(Dictionary<SPXX, string> spxx, int int_0, byte[] byte_0 = null)
        {
            if (spxx == null)
            {
                if (byte_0 != null)
                {
                    if (((this.xfsh != null) && (this.xfsh.Length == 15)) && this.xfsh.Substring(8, 2).ToUpper().Equals("DK"))
                    {
                        string bz = this.bz;
                        try
                        {
                            bz = ToolUtil.GetString(Convert.FromBase64String(this.bz));
                        }
                        catch (Exception)
                        {
                        }
                        NotesUtil.GetDKQYFromInvNotes(bz, out this.dkqysh, out this.dkqymc);
                        this.dkqymc = Class34.smethod_23(this.dkqymc, Struct40.int_1, false, true);
                    }
                    try
                    {
                        if (TaxCardFactory.CreateTaxCard().SubSoftVersion != "Linux")
                        {
                            byte[] buffer = new byte[] { 
                                0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                                0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                             };
                            byte[] buffer2 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                            byte[] buffer3 = AES_Crypt.Decrypt(Convert.FromBase64String(this.mw), buffer, buffer2, byte_0);
                            this.mw = ToolUtil.GetString(buffer3);
                        }
                        else
                        {
                            byte[] buffer4 = new byte[] { 0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd };
                            byte[] buffer5 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                            byte[] buffer7 = AES_Crypt.Decrypt(Convert.FromBase64String(this.mw), buffer4, buffer5, byte_0);
                            this.mw = ToolUtil.GetString(buffer7);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    if (int_0 == 0)
                    {
                        Encoding encoding = Encoding.GetEncoding("GB18030");
                        this.bz = encoding.GetString(Convert.FromBase64String(this.bz));
                        this.yshwxx = encoding.GetString(Convert.FromBase64String(this.yshwxx));
                        TaxCardFactory.CreateTaxCard();
                        if (!this.isBlankWaste && new StringBuilder().Append('0', 15).ToString().Equals(this.gfsh))
                        {
                            this.gfsh = string.Empty;
                        }
                        if ((this.fplx == FPLX.PTFP) && (this.Zyfplx == ZYFP_LX.NCP_SG))
                        {
                            string gfsh = this.gfsh;
                            this.gfsh = this.xfsh;
                            this.xfsh = gfsh;
                            gfsh = this.gfmc;
                            this.gfmc = this.xfmc;
                            this.xfmc = gfsh;
                            gfsh = this.gfyhzh;
                            this.gfyhzh = this.xfyhzh;
                            this.xfyhzh = gfsh;
                            gfsh = this.gfdzdh;
                            this.gfdzdh = this.xfdzdh;
                            this.xfdzdh = gfsh;
                        }
                    }
                }
                return "";
            }
            if (spxx[SPXX.DJ].Length == 0)
            {
                return string.Empty;
            }
            if (spxx[SPXX.SLV].Length != 0)
            {
                if ((this.fplx == FPLX.ZYFP) && (this.Zyfplx == ZYFP_LX.HYSY))
                {
                    if (string.Equals(spxx[SPXX.HSJBZ], "1"))
                    {
                        return Class34.smethod_24(Class34.smethod_17(spxx[SPXX.DJ], Struct40.string_0), Struct40.int_47, false);
                    }
                    return Class34.smethod_11(spxx[SPXX.DJ], Class34.smethod_17(spxx[SPXX.SLV], "1"), Struct40.int_46);
                }
                if (((this.fplx == FPLX.ZYFP) || (this.fplx == FPLX.PTFP)) && ((this.Zyfplx == ZYFP_LX.JZ_50_15) || (this.Zyfplx == ZYFP_LX.CEZS)))
                {
                    return Class34.smethod_24(Class34.smethod_17(spxx[SPXX.DJ], Struct40.string_0), Struct40.int_47, false);
                }
                if (string.Equals(spxx[SPXX.HSJBZ], "1"))
                {
                    if (this.fplx == FPLX.JSFP)
                    {
                        if ((int_0 != 0) && (int_0 != 3))
                        {
                            return Class34.smethod_14(spxx[SPXX.DJ], Class34.smethod_17(spxx[SPXX.SLV], "1"), Struct40.int_46);
                        }
                        return Class34.smethod_24(Class34.smethod_17(spxx[SPXX.DJ], Struct40.string_0), Struct40.int_47, false);
                    }
                    return Class34.smethod_14(spxx[SPXX.DJ], Class34.smethod_17(spxx[SPXX.SLV], "1"), Struct40.int_46);
                }
                if (this.fplx == FPLX.JSFP)
                {
                    return Class34.smethod_11(spxx[SPXX.DJ], Class34.smethod_17(spxx[SPXX.SLV], "1"), Struct40.int_46);
                }
            }
            return Class34.smethod_24(Class34.smethod_17(spxx[SPXX.DJ], Struct40.string_0), Struct40.int_47, false);
        }

        private string method_0(InvDetail invDetail_0, int int_0)
        {
            string str = "0000";
            this.string_0 = string.Empty;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            try
            {
                List<InvTypeInfo>.Enumerator enumerator;
                DateTime time4;
                DateTime time6;
                int length;
                DateTime time8;
                DateTime time12;
                this.fplx = (FPLX) invDetail_0.InvType;
                if (string.Equals(card.SoftVersion, "FWKP_V2.0_Svr_Server"))
                {
                    this.kpjh = invDetail_0.ClientNum;
                }
                str = InvoiceHandler.CheckFpdm(invDetail_0.TypeCode);
                if (!str.Equals("0000"))
                {
                    ilog_0.Error("[发票修复] 从底层修复上来的发票代码不正确，此发票无法修复");
                    return str;
                }
                if ((((this.fplx == FPLX.ZYFP) || (this.fplx == FPLX.PTFP)) || (this.fplx == FPLX.DZFP)) || (this.fplx == FPLX.JSFP))
                {
                    goto Label_08E4;
                }
                if (this.fplx != FPLX.HYFP)
                {
                    goto Label_0504;
                }
                this.fpdm = invDetail_0.TypeCode;
                this.fphm = invDetail_0.InvNo.ToString("D8");
                this.kprq = ToolUtil.FormatDateTime(invDetail_0.Date);
                this.ssyf = int.Parse(invDetail_0.Date.ToString("yyyyMM"));
                this.gfsh = invDetail_0.BuyTaxCode;
                this.xfsh = invDetail_0.SaleTaxCode;
                this.je = invDetail_0.Amount.ToString();
                this.isRed = invDetail_0.Amount < 0.0;
                this.se = invDetail_0.Tax.ToString();
                if (Math.Abs(decimal.Parse(this.je)) > 0M)
                {
                    this.sLv = Class34.smethod_15(this.se, this.je, 2);
                }
                else
                {
                    this.sLv = "0.00";
                }
                this.dzsyh = invDetail_0.Index;
                this.kpsxh = invDetail_0.InvSqeNo;
                this.mw = invDetail_0.MW;
                this.jym = invDetail_0.JYM;
                this.xfmc = card.Corporation;
                this.zfbz = invDetail_0.CancelFlag;
                this.zfsj = string.Empty;
                if (this.zfbz)
                {
                    this.zfsj = ToolUtil.FormatDateTime(invDetail_0.WasteTime);
                }
                this.bszt = invDetail_0.IsUpload ? 1 : 0;
                this.jmbbh = card.CipherVersion;
                this.bsq = invDetail_0.InvRepPeriod;
                if (!card.QYLX.ISHY)
                {
                    this.bsbz = true;
                    goto Label_0310;
                }
                DateTime time7 = new DateTime();
                using (List<InvTypeInfo>.Enumerator enumerator2 = card.StateInfo.InvTypeInfo.GetEnumerator())
                {
                    InvTypeInfo current;
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.InvType == 11)
                        {
                            goto Label_0282;
                        }
                    }
                    goto Label_02A0;
                Label_0282:
                    time7 = Convert.ToDateTime(current.LastRepDate);
                }
            Label_02A0:
                time8 = new DateTime(time7.Year, time7.Month, 1);
                DateTime time9 = new DateTime(invDetail_0.Date.Year, invDetail_0.Date.Month, 1);
                int num11 = DateTime.Compare(time9, time8);
                if (num11 == 0)
                {
                    int num12 = int_0;
                    this.bsbz = num12 != this.bsq;
                }
                else if (num11 < 0)
                {
                    this.bsbz = true;
                }
                else
                {
                    this.bsbz = false;
                }
            Label_0310:
                this.isBlankWaste = ((Math.Abs(invDetail_0.Amount) < 1E-08) && (Math.Abs(invDetail_0.Tax) < 1E-08)) && invDetail_0.CancelFlag;
                DateTime time10 = new DateTime(0x7d0, 1, 1);
                if (DateTime.Compare(invDetail_0.Date, time10) < 0)
                {
                    ilog_0.Error("[发票修复] 修复时取到的开票日期不正确，开票日期=" + invDetail_0.Date.ToString());
                    return "A667";
                }
                if ((invDetail_0.SignBuffer != null) && (invDetail_0.SignBuffer.Length != 0))
                {
                    if (invDetail_0.SignBuffer.Length > 0x800)
                    {
                        str = "A671";
                        length = invDetail_0.SignBuffer.Length;
                        ilog_0.Error("签名数据长度=" + length.ToString() + "，超长");
                        return str;
                    }
                    this.sign = Convert.ToBase64String(invDetail_0.SignBuffer);
                }
                else
                {
                    this.sign = invDetail_0.Index;
                }
                string[] strArray3 = null;
                string str5 = null;
                strArray3 = Regex.Split(ToolUtil.GetString(invDetail_0.OldTypeCode), "\n");
                bool flag2 = true;
                if (strArray3.Length < 30)
                {
                    ilog_0.Error("发票修复：货运发票无明细");
                    flag2 = false;
                    this.zyspmc = "修复";
                    this.kpr = "修复";
                }
                int num13 = strArray3[0x1d].LastIndexOf(" ");
                str5 = strArray3[0x1d].Substring(0, num13 + 1);
                if (((ToolUtil.GetByteCount(str5) % 0x10a) == 0) && (strArray3[5].IndexOf("B") >= 0))
                {
                    this.jskFpmxVersion_0 = JskFpmxVersion.V3;
                }
                else
                {
                    this.jskFpmxVersion_0 = JskFpmxVersion.V2;
                }
                if (this.isBlankWaste)
                {
                    this.method_8(invDetail_0, card);
                }
                if (flag2)
                {
                    this.method_4(strArray3, str5);
                    if (this.isRed && card.QYLX.ISTLQY)
                    {
                        this.redNum = string.Empty;
                    }
                }
                goto Label_136C;
            Label_0504:
                if (this.fplx != FPLX.JDCFP)
                {
                    goto Label_136C;
                }
                this.fpdm = invDetail_0.TypeCode;
                this.fphm = invDetail_0.InvNo.ToString("D8");
                this.kprq = ToolUtil.FormatDateTime(invDetail_0.Date);
                this.ssyf = int.Parse(invDetail_0.Date.ToString("yyyyMM"));
                this.gfsh = invDetail_0.BuyTaxCode;
                this.xfsh = invDetail_0.SaleTaxCode;
                this.je = invDetail_0.Amount.ToString();
                this.isRed = invDetail_0.Amount < 0.0;
                this.se = invDetail_0.Tax.ToString();
                if (Math.Abs(decimal.Parse(this.je)) > 0M)
                {
                    this.sLv = Class34.smethod_15(this.se, this.je, 2);
                }
                else
                {
                    this.sLv = "0.00";
                }
                this.dzsyh = invDetail_0.Index;
                this.kpsxh = invDetail_0.InvSqeNo;
                this.mw = invDetail_0.MW;
                this.jym = invDetail_0.JYM;
                this.xfmc = card.Corporation;
                this.zfbz = invDetail_0.CancelFlag;
                this.zfsj = string.Empty;
                if (this.zfbz)
                {
                    this.zfsj = ToolUtil.FormatDateTime(invDetail_0.WasteTime);
                }
                this.bszt = invDetail_0.IsUpload ? 1 : 0;
                this.jmbbh = card.CipherVersion;
                this.bsq = invDetail_0.InvRepPeriod;
                if (!card.QYLX.ISJDC)
                {
                    this.bsbz = true;
                    goto Label_0771;
                }
                DateTime time11 = new DateTime();
                using (enumerator = card.StateInfo.InvTypeInfo.GetEnumerator())
                {
                    InvTypeInfo info3;
                    while (enumerator.MoveNext())
                    {
                        info3 = enumerator.Current;
                        if (info3.InvType == 12)
                        {
                            goto Label_06E3;
                        }
                    }
                    goto Label_0701;
                Label_06E3:
                    time11 = Convert.ToDateTime(info3.LastRepDate);
                }
            Label_0701:
                time12 = new DateTime(time11.Year, time11.Month, 1);
                DateTime time13 = new DateTime(invDetail_0.Date.Year, invDetail_0.Date.Month, 1);
                int num15 = DateTime.Compare(time13, time12);
                if (num15 == 0)
                {
                    int num16 = int_0;
                    this.bsbz = num16 != this.bsq;
                }
                else if (num15 < 0)
                {
                    this.bsbz = true;
                }
                else
                {
                    this.bsbz = false;
                }
            Label_0771:
                this.isBlankWaste = ((Math.Abs(invDetail_0.Amount) < 1E-08) && (Math.Abs(invDetail_0.Tax) < 1E-08)) && invDetail_0.CancelFlag;
                DateTime time14 = new DateTime(0x7d0, 1, 1);
                if (DateTime.Compare(invDetail_0.Date, time14) < 0)
                {
                    ilog_0.Error("[发票修复] 修复时取到的开票日期不正确，开票日期=" + invDetail_0.Date.ToString());
                    return "A667";
                }
                if ((invDetail_0.SignBuffer != null) && (invDetail_0.SignBuffer.Length != 0))
                {
                    if (invDetail_0.SignBuffer.Length > 0x800)
                    {
                        str = "A671";
                        length = invDetail_0.SignBuffer.Length;
                        ilog_0.Error("签名数据长度=" + length.ToString() + "，超长");
                        return str;
                    }
                    this.sign = Convert.ToBase64String(invDetail_0.SignBuffer);
                }
                else
                {
                    this.sign = invDetail_0.Index;
                }
                string[] strArray4 = null;
                strArray4 = Regex.Split(ToolUtil.GetString(invDetail_0.OldTypeCode), "\n");
                bool flag3 = true;
                if (strArray4.Length < 0x24)
                {
                    ilog_0.Error("发票修复：机动车发票无明细");
                    flag3 = false;
                    this.zyspmc = "修复";
                    this.kpr = "修复";
                }
                if (this.isBlankWaste)
                {
                    this.method_8(invDetail_0, card);
                }
                if (flag3)
                {
                    this.method_5(strArray4);
                }
                goto Label_136C;
            Label_08E4:
                this.fpdm = invDetail_0.TypeCode;
                this.fphm = invDetail_0.InvNo.ToString("D8");
                this.kprq = ToolUtil.FormatDateTime(invDetail_0.Date);
                this.ssyf = int.Parse(invDetail_0.Date.ToString("yyyyMM"));
                this.gfsh = invDetail_0.BuyTaxCode;
                this.xfsh = invDetail_0.SaleTaxCode;
                this.je = invDetail_0.Amount.ToString();
                this.isRed = invDetail_0.Amount < 0.0;
                this.se = invDetail_0.Tax.ToString();
                this.dzsyh = invDetail_0.Index;
                this.kpsxh = invDetail_0.InvSqeNo;
                this.mw = invDetail_0.MW;
                if (this.fplx == FPLX.JSFP)
                {
                    this.mw = string.Empty;
                }
                this.jym = invDetail_0.JYM;
                this.xfmc = card.Corporation;
                this.xfdzdh = card.Address + " " + card.Telephone;
                if (card.BankAccount != null)
                {
                    string[] strArray = Regex.Split(card.BankAccount, Environment.NewLine);
                    this.xfyhzh = strArray[0];
                }
                this.zfbz = invDetail_0.CancelFlag;
                this.zfsj = string.Empty;
                if (this.zfbz)
                {
                    this.zfsj = ToolUtil.FormatDateTime(invDetail_0.WasteTime);
                }
                this.bszt = invDetail_0.IsUpload ? 1 : 0;
                this.jmbbh = card.CipherVersion;
                this.bsq = invDetail_0.InvRepPeriod;
                if (this.fplx != FPLX.DZFP)
                {
                    if ((((this.fplx == FPLX.ZYFP) && !card.QYLX.ISZYFP) || ((this.fplx == FPLX.PTFP) && !card.QYLX.ISPTFP)) || ((this.fplx == FPLX.JSFP) && !card.QYLX.ISPTFPJSP))
                    {
                        this.bsbz = true;
                    }
                    else if (card.ECardType == ECardType.ectDefault)
                    {
                        if (invDetail_0.Date < card.LastRepDate)
                        {
                            this.bsbz = true;
                        }
                    }
                    else
                    {
                        DateTime time = new DateTime(card.LastRepDateYear, card.LastRepDateMonth, 1);
                        DateTime time2 = new DateTime(invDetail_0.Date.Year, invDetail_0.Date.Month, 1);
                        int num = DateTime.Compare(time2, time);
                        if (num == 0)
                        {
                            this.bsbz = int_0 != this.bsq;
                        }
                        else if (num < 0)
                        {
                            this.bsbz = true;
                        }
                        else
                        {
                            this.bsbz = false;
                        }
                    }
                    goto Label_0C46;
                }
                if (!card.QYLX.ISPTFPDZ)
                {
                    this.bsbz = true;
                    goto Label_0C46;
                }
                DateTime time3 = new DateTime();
                using (enumerator = card.StateInfo.InvTypeInfo.GetEnumerator())
                {
                    InvTypeInfo info;
                    while (enumerator.MoveNext())
                    {
                        info = enumerator.Current;
                        if (info.InvType == 0x33)
                        {
                            goto Label_0BC1;
                        }
                    }
                    goto Label_0BDF;
                Label_0BC1:
                    time3 = Convert.ToDateTime(info.LastRepDate);
                }
            Label_0BDF:
                time4 = new DateTime(time3.Year, time3.Month, 1);
                DateTime time5 = new DateTime(invDetail_0.Date.Year, invDetail_0.Date.Month, 1);
                int num2 = DateTime.Compare(time5, time4);
                if (num2 == 0)
                {
                    int num3 = int_0;
                    this.bsbz = num3 != this.bsq;
                }
                else if (num2 < 0)
                {
                    this.bsbz = true;
                }
            Label_0C46:
                time6 = new DateTime(0x7d0, 1, 1);
                if (DateTime.Compare(invDetail_0.Date, time6) < 0)
                {
                    ilog_0.Error("[发票修复] 修复时取到的开票日期不正确，开票日期=" + invDetail_0.Date.ToString());
                    return "A667";
                }
                if ((invDetail_0.SignBuffer != null) && (invDetail_0.SignBuffer.Length != 0))
                {
                    if (invDetail_0.SignBuffer.Length > 0x800)
                    {
                        str = "A671";
                        ilog_0.Error("签名数据长度=" + invDetail_0.SignBuffer.Length.ToString() + "，超长");
                        return str;
                    }
                    this.sign = Convert.ToBase64String(invDetail_0.SignBuffer);
                }
                else
                {
                    this.sign = string.Empty;
                    ilog_0.Debug("[发票修复] 从底层修复上来的签名数据为空");
                }
                if (((invDetail_0.OldInvNo != null) && (card.ECardType == ECardType.ectDefault)) && (invDetail_0.OldTypeCode == null))
                {
                    if (invDetail_0.OldInvNo.Length == 0x10)
                    {
                        if (NotesUtil.CheckTzdh(invDetail_0.OldInvNo))
                        {
                            this.isRed = true;
                            this.redNum = invDetail_0.OldInvNo;
                            this.bz = NotesUtil.GetRedInvNotes(invDetail_0.OldInvNo);
                        }
                    }
                    else if (invDetail_0.OldInvNo.Length == 0x12)
                    {
                        this.isRed = true;
                        this.blueFpdm = invDetail_0.OldInvNo.Substring(0, 10);
                        this.blueFphm = invDetail_0.OldInvNo.Substring(10, 8);
                        this.bz = NotesUtil.GetRedInvNotes(invDetail_0.OldInvNo.Substring(0, 10), invDetail_0.OldInvNo.Substring(10, 8));
                    }
                }
                this.isBlankWaste = ((Math.Abs(invDetail_0.Amount) < 1E-08) && (Math.Abs(invDetail_0.Tax) < 1E-08)) && invDetail_0.CancelFlag;
                byte[] destinationArray = null;
                string[] strArray2 = null;
                string str3 = null;
                string str4 = null;
                if ((invDetail_0.OldTypeCode != null) && (invDetail_0.OldTypeCode.Length > 0x80))
                {
                    if (invDetail_0.OldTypeCode.Length >= 0x80)
                    {
                        byte[] buffer2 = new byte["BarcodeKey".Length];
                        Array.Copy(invDetail_0.OldTypeCode, 0, buffer2, 0, "BarcodeKey".Length);
                        if (ToolUtil.GetString(buffer2) == "BarcodeKey")
                        {
                            this.hzfw = true;
                            destinationArray = new byte[0x20];
                            Array.Copy(invDetail_0.OldTypeCode, 0, destinationArray, 0, 0x20);
                            byte[] buffer3 = new byte[invDetail_0.OldTypeCode.Length - 0x20];
                            Array.Copy(invDetail_0.OldTypeCode, 0x20, buffer3, 0, invDetail_0.OldTypeCode.Length - 0x20);
                            strArray2 = Regex.Split(ToolUtil.GetString(buffer3), "\n");
                            ilog_0.Debug("fpxx.lenth=" + strArray2.Length.ToString());
                        }
                        else
                        {
                            this.hzfw = false;
                            strArray2 = Regex.Split(ToolUtil.GetString(invDetail_0.OldTypeCode), "\n");
                        }
                        if (strArray2.Length < 0x16)
                        {
                            ilog_0.Error("金税卡返回的发票明细行数有误");
                            return "A672";
                        }
                        if (strArray2[0x15].Trim() == "N")
                        {
                            int num7 = strArray2[0x16].LastIndexOf(" ");
                            str3 = strArray2[0x16].Substring(0, num7 + 1);
                        }
                        else if (strArray2[0x15].Trim() == "Y")
                        {
                            str3 = strArray2[0x16];
                            if (strArray2.Length >= 0x18)
                            {
                                int num8 = strArray2[0x17].LastIndexOf(" ");
                                str4 = strArray2[0x17].Substring(0, num8 + 1);
                            }
                        }
                        if (string.IsNullOrEmpty(str3) && !this.isBlankWaste)
                        {
                            str = "A672";
                            ilog_0.Error("发票修复时，返回的发票明细长度不正确，长度=0");
                            return str;
                        }
                        int byteCount = ToolUtil.GetByteCount(str3);
                        if (((byteCount % 0x178) != 0) || (strArray2[1].IndexOf("B") < 0))
                        {
                            if ((byteCount % 0xfc) != 0)
                            {
                                if ((byteCount % 250) != 0)
                                {
                                    ilog_0.Error("金税卡返回的发票明细行数有误2");
                                    str = "A672";
                                    ilog_0.Error("发票修复时，返回的发票明细长度不正确，长度=" + byteCount.ToString());
                                    return str;
                                }
                                this.jskFpmxVersion_0 = JskFpmxVersion.V1;
                            }
                            else
                            {
                                this.jskFpmxVersion_0 = JskFpmxVersion.V2;
                            }
                        }
                        else
                        {
                            this.jskFpmxVersion_0 = JskFpmxVersion.V3;
                        }
                    }
                    else
                    {
                        str3 = null;
                        this.jskFpmxVersion_0 = JskFpmxVersion.V0;
                    }
                }
                else
                {
                    ilog_0.Debug("OldTypeCode.Length=" + invDetail_0.OldTypeCode.Length.ToString());
                    str3 = null;
                    this.jskFpmxVersion_0 = JskFpmxVersion.V0;
                    this.zyspmc = "修复";
                    this.kpr = "修复";
                }
                if ((card.ECardType == ECardType.ectDefault) && this.hzfw)
                {
                    this.keyFlagNo = invDetail_0.KeyFlagNo;
                    this.invQryNo = invDetail_0.InvQryNo;
                }
                this.sLv = this.method_7(invDetail_0, str3);
                if (((this.sLv != "") && (this.fplx == FPLX.ZYFP)) && ((Math.Abs((double) (double.Parse(this.sLv) - 0.05)) < 0.001) && (strArray2[1].IndexOf("H") < 0)))
                {
                    this.Zyfplx = ZYFP_LX.HYSY;
                }
                if (((this.fplx == FPLX.ZYFP) || (this.fplx == FPLX.PTFP)) && (strArray2[1].IndexOf("J") >= 0))
                {
                    this.Zyfplx = ZYFP_LX.JZ_50_15;
                }
                if (this.jskFpmxVersion_0 != JskFpmxVersion.V0)
                {
                    this.method_3(strArray2, str3, str4);
                }
                if (this.isBlankWaste)
                {
                    this.method_8(invDetail_0, card);
                }
                this.hxm = "";
                if (this.hzfw && !this.isBlankWaste)
                {
                    if (card.ECardType == ECardType.ectDefault)
                    {
                        this.hxm = "";
                    }
                    else
                    {
                        if (this.jskFpmxVersion_0 == JskFpmxVersion.V0)
                        {
                            str = "A672";
                            ilog_0.Error("金税盘返回的发票明细长度不正确");
                            return str;
                        }
                        bool flag = strArray2[1].IndexOf('V') > -1;
                        ushort num10 = 0;
                        if ((destinationArray != null) && (destinationArray.Length == 0x20))
                        {
                            num10 = (ushort) (destinationArray[0x1a] << 8);
                            num10 = (ushort) (num10 + destinationArray[0x1b]);
                        }
                        byte[] inArray = new InvoiceHandler().method_19(this, num10, flag);
                        if (inArray != null)
                        {
                            this.hxm = Convert.ToBase64String(inArray);
                        }
                    }
                }
                if (this.fplx == FPLX.JSFP)
                {
                    this.hxm = "";
                    this.mw = "";
                }
                if ((strArray2 != null) && (strArray2.Length > 2))
                {
                    if ((strArray2[1].IndexOf("C") >= 0) && ((this.fplx == FPLX.ZYFP) || (this.fplx == FPLX.PTFP)))
                    {
                        this.yysbz = this.yysbz.Substring(0, 8) + "2" + this.yysbz.Substring(9, 1);
                        this.Zyfplx = ZYFP_LX.CEZS;
                    }
                    else if (((strArray2[1].IndexOf("H") >= 0) && (this.fplx == FPLX.ZYFP)) && (!string.IsNullOrEmpty(this.sLv) && Class34.smethod_10("0.05", this.sLv)))
                    {
                        this.yysbz = this.yysbz.Substring(0, 8) + "1" + this.yysbz.Substring(9, 1);
                    }
                }
            Label_136C:
                if (!string.IsNullOrEmpty(this.string_0))
                {
                    double amount = invDetail_0.Amount;
                    if ((this.fplx == FPLX.HYFP) && (invDetail_0.Amount < 0.0))
                    {
                        amount = -1.0 * amount;
                    }
                    if (!Class34.smethod_8(amount.ToString(), this.string_0, 2, "0.01"))
                    {
                        str = "A673";
                        ilog_0.Debug("[发票修复] 底层返回的合计金额=" + amount.ToString() + "，明细累加的金额=" + this.string_0);
                        ilog_0.Error("[发票修复] 发票修复时，底层返回的合计金额和明细累加的金额不一致，此发票不修复");
                        return str;
                    }
                }
                else if (!string.IsNullOrEmpty(this.sLv) && !Class34.smethod_8(Class34.smethod_12(invDetail_0.Amount.ToString(), this.sLv, 2), invDetail_0.Tax.ToString(), 2, Struct40.string_4))
                {
                    str = "A674";
                    ilog_0.Debug("[发票修复] 底层返回的合计金额=" + invDetail_0.Amount.ToString() + "，底层返回的合计税额=" + invDetail_0.Tax.ToString() + "，税率=" + this.sLv);
                    ilog_0.Error("[发票修复] 发票修复时，底层返回的合计金额和合计税额校验失败，此发票不修复");
                    return str;
                }
                if ((((this.fplx == FPLX.ZYFP) || (this.fplx == FPLX.PTFP)) || (this.fplx == FPLX.HYFP)) && (Math.Abs(invDetail_0.Amount) > 100000000.0))
                {
                    str = "A675";
                    ilog_0.Debug("[发票修复] 底层返回的合计金额=" + invDetail_0.Amount.ToString() + "，底层返回的合计税额=" + invDetail_0.Tax.ToString());
                    ilog_0.Error("[发票修复] 发票修复时，底层返回的合计金额超过最大限额，此发票不修复");
                    return str;
                }
                if ((this.fplx == FPLX.DZFP) && (Math.Abs(invDetail_0.Amount) > 1000000000.0))
                {
                    str = "A675";
                    ilog_0.Debug("[发票修复] 底层返回的合计金额=" + invDetail_0.Amount.ToString() + "，底层返回的合计税额=" + invDetail_0.Tax.ToString());
                    ilog_0.Error("[发票修复] 发票修复时，底层返回的合计金额超过最大限额，此发票不修复");
                    return str;
                }
                this.xfbz = true;
            }
            catch (Exception exception)
            {
                str = "A679";
                ilog_0.Error(exception.Message);
            }
            try
            {
                byte[] buffer5 = new byte[] { 
                    0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                    0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 };
                byte[] buffer6 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                byte[] buffer7 = AES_Crypt.Encrypt(ToolUtil.GetBytes(this.mw), buffer5, buffer6);
                this.mw = Convert.ToBase64String(buffer7);
            }
            catch (Exception)
            {
            }
            return str;
        }

        internal string method_1()
        {
            if (this.isRed)
            {
                return (Struct40.string_9 + "。");
            }
            string str = "";
            if (this.Zyfplx == ZYFP_LX.CEZS)
            {
                for (int i = 0; i < this.Mxxx.Count; i++)
                {
                    if (((FPHXZ) Enum.Parse(typeof(FPHXZ), this.Mxxx[i][SPXX.FPHXZ])) != FPHXZ.ZKXX)
                    {
                        str = str + Class34.smethod_19(this.Mxxx[i][SPXX.KCE], Struct40.int_50) + "；";
                    }
                }
                if (this.Mxxx.Count > 0)
                {
                    str = Struct40.string_9 + "：" + str.Substring(0, str.Length - 1) + "。";
                }
            }
            return str;
        }

        private string method_2(string string_2)
        {
            string str = string.Empty;
            try
            {
                int index = string_2.IndexOf("B");
                if (index < 0)
                {
                    return str;
                }
                StringBuilder builder = new StringBuilder();
                foreach (char ch in string_2.Substring(index + 1))
                {
                    if (!char.IsDigit(ch) && (ch != '.'))
                    {
                        break;
                    }
                    builder.Append(ch);
                }
                str = builder.ToString();
            }
            catch (Exception exception)
            {
                ilog_0.ErrorFormat("获取编码表版本号出现异常:{0}，data={1}", exception.Message, string_2);
            }
            return str;
        }

        private void method_3(string[] string_2, string string_3, string string_4)
        {
            if ((string_2 == null) || (string_2.Length < 0x16))
            {
                ilog_0.Error("金税卡返回的发票明细行数有误3");
                throw new BusinessObjectException(null, "Aisino.Fwkp.BusinessObject.Fpxx.ProccessFpmxqd()", "金税卡返回的发票明细行数有误", null);
            }
            this.yysbz = this.method_6(string_2);
            if (this.yysbz[2] == '1')
            {
                this.Zyfplx = ZYFP_LX.XT_YCL;
            }
            else if (this.yysbz[2] == '2')
            {
                this.Zyfplx = ZYFP_LX.XT_CCP;
            }
            if (this.yysbz[5] == '1')
            {
                this.Zyfplx = ZYFP_LX.NCP_XS;
            }
            else if (this.yysbz[5] == '2')
            {
                this.Zyfplx = ZYFP_LX.NCP_SG;
            }
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V3)
            {
                this.bmbbbh = this.method_2(string_2[1]);
            }
            string str11 = string_2[3];
            if (str11.Length > 8)
            {
                string str12 = str11.Substring(8, str11.Length - 8).Replace('.', ':');
                this.kprq = str11.Substring(0, 4) + "-" + str11.Substring(4, 2) + "-" + str11.Substring(6, 2) + str12;
            }
            else if (str11.Length == 8)
            {
                this.kprq = str11.Substring(0, 4) + "-" + str11.Substring(4, 2) + "-" + str11.Substring(6, 2) + " 00:00:00";
            }
            this.gfmc = string_2[4];
            this.gfsh = string_2[5];
            this.gfdzdh = string_2[6];
            this.gfyhzh = string_2[7];
            if ((this.fplx != FPLX.DZFP) && (this.fplx != FPLX.JSFP))
            {
                this.jmbbh = string_2[8];
            }
            else
            {
                this.jqbh = string_2[8];
                ilog_0.Debug("[发票修复]电子发票 jqbh=" + this.jqbh);
            }
            this.zyspmc = string_2[9];
            if ((this.fplx == FPLX.ZYFP) && this.isRed)
            {
                string input = string_2[10];
                string[] strArray = Regex.Split(input, "@");
                if (strArray.Length >= 3)
                {
                    this.blueFpdm = strArray[1];
                    this.blueFphm = strArray[2];
                }
            }
            else if (this.fplx == FPLX.JSFP)
            {
                string str21 = string_2[10];
                string[] strArray3 = Regex.Split(str21, "@");
                if (this.isRed && (strArray3.Length == 4))
                {
                    this.blueFpdm = strArray3[1];
                    this.blueFphm = strArray3[2];
                    ilog_0.Debug("[FPXF] 卷票打印模板=" + strArray3[3]);
                    this.yysbz = this.yysbz.Substring(0, 6) + strArray3[3] + this.yysbz.Substring(8, 2);
                }
                else if (!this.isRed && (strArray3.Length == 2))
                {
                    ilog_0.Debug("[FPXF] 卷票打印模板=" + strArray3[1]);
                    this.yysbz = this.yysbz.Substring(0, 6) + strArray3[1] + this.yysbz.Substring(8, 2);
                }
            }
            this.xfmc = string_2[11];
            this.xfsh = string_2[12];
            this.xfdzdh = string_2[13];
            this.xfyhzh = string_2[14];
            this.kpr = string_2[15];
            this.fhr = string_2[0x10];
            this.skr = string_2[0x11];
            string str19 = (string_2[0x12] == null) ? string.Empty : string_2[0x12].Replace("￠\x00a7", "\r\n");
            this.bz = str19;
            if (this.isRed && (this.bz != null))
            {
                int num5 = 0;
                FPLX fplx = this.fplx;
                switch (fplx)
                {
                    case FPLX.ZYFP:
                        num5 = 1;
                        break;

                    case ((FPLX) 1):
                        break;

                    case FPLX.PTFP:
                        num5 = 0;
                        break;

                    case FPLX.JSFP:
                        num5 = 0;
                        break;

                    default:
                        if (fplx == FPLX.DZFP)
                        {
                            num5 = 4;
                        }
                        break;
                }
                string str10 = NotesUtil.GetInfo(ToolUtil.GetString(Convert.FromBase64String(this.bz)), num5, "");
                if (str10.Length > 0)
                {
                    if (this.fplx == FPLX.ZYFP)
                    {
                        if (str10.Length == 0x10)
                        {
                            this.redNum = str10;
                        }
                    }
                    else if (((this.fplx == FPLX.PTFP) || (this.fplx == FPLX.DZFP)) || (this.fplx == FPLX.JSFP))
                    {
                        if (str10.Length == 0x12)
                        {
                            this.blueFpdm = str10.Substring(0, 10);
                            this.blueFphm = str10.Substring(10, 8);
                        }
                        else if (str10.Length == 20)
                        {
                            this.blueFpdm = str10.Substring(0, 12);
                            this.blueFphm = str10.Substring(12, 8);
                        }
                    }
                }
            }
            this.string_0 = "0.00";
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V1)
            {
                byte[] bytes = ToolUtil.GetBytes(string_3);
                int num3 = bytes.Length / 250;
                this.Mxxx = new List<Dictionary<SPXX, string>>();
                byte[] destinationArray = new byte[250];
                for (int i = 0; i < num3; i++)
                {
                    Array.Copy(bytes, 250 * i, destinationArray, 0, 250);
                    Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                    item[SPXX.SPMC] = ToolUtil.GetString(destinationArray, 0, 80).Trim();
                    item[SPXX.GGXH] = ToolUtil.GetString(destinationArray, 80, 40).Trim();
                    item[SPXX.JLDW] = ToolUtil.GetString(destinationArray, 120, 0x20).Trim();
                    string str5 = ToolUtil.GetString(destinationArray, 0x98, 0x12).Trim();
                    item[SPXX.SL] = (str5 == "0") ? string.Empty : str5;
                    string str9 = ToolUtil.GetString(destinationArray, 170, 0x12).Trim();
                    item[SPXX.DJ] = (str9 == "0") ? string.Empty : str9;
                    item[SPXX.JE] = ToolUtil.GetString(destinationArray, 0xbc, 0x12).Trim();
                    item[SPXX.SLV] = ToolUtil.GetString(destinationArray, 0xce, 6).Trim();
                    item[SPXX.SE] = ToolUtil.GetString(destinationArray, 0xd4, 0x12).Trim();
                    item[SPXX.XH] = ToolUtil.GetString(destinationArray, 230, 8).Trim();
                    item[SPXX.FPHXZ] = ToolUtil.GetString(destinationArray, 0xee, 6).Trim();
                    item[SPXX.HSJBZ] = ToolUtil.GetString(destinationArray, 0xf4, 6).Trim();
                    item[SPXX.SPSM] = string.Empty;
                    item[SPXX.FLBM] = string.Empty;
                    item[SPXX.SPBH] = string.Empty;
                    item[SPXX.XSYH] = "0";
                    item[SPXX.YHSM] = string.Empty;
                    item[SPXX.LSLVBS] = string.Empty;
                    this.Mxxx.Add(item);
                    this.string_0 = Class34.smethod_17(this.string_0, item[SPXX.JE]);
                }
                if (string_4 != null)
                {
                    byte[] sourceArray = ToolUtil.GetBytes(string_4);
                    int num10 = sourceArray.Length / 250;
                    this.Qdxx = new List<Dictionary<SPXX, string>>();
                    byte[] buffer9 = new byte[250];
                    for (int j = 0; j < num10; j++)
                    {
                        Array.Copy(sourceArray, 250 * j, buffer9, 0, 250);
                        Dictionary<SPXX, string> dictionary5 = new Dictionary<SPXX, string>();
                        dictionary5[SPXX.SPMC] = ToolUtil.GetString(buffer9, 0, 80).Trim();
                        dictionary5[SPXX.GGXH] = ToolUtil.GetString(buffer9, 80, 40).Trim();
                        dictionary5[SPXX.JLDW] = ToolUtil.GetString(buffer9, 120, 0x20).Trim();
                        string str7 = ToolUtil.GetString(buffer9, 0x98, 0x12).Trim();
                        dictionary5[SPXX.SL] = (str7 == "0") ? string.Empty : str7;
                        string str17 = ToolUtil.GetString(buffer9, 170, 0x12).Trim();
                        dictionary5[SPXX.DJ] = (str17 == "0") ? string.Empty : str17;
                        dictionary5[SPXX.JE] = ToolUtil.GetString(buffer9, 0xbc, 0x12).Trim();
                        dictionary5[SPXX.SLV] = ToolUtil.GetString(buffer9, 0xce, 6).Trim();
                        dictionary5[SPXX.SE] = ToolUtil.GetString(buffer9, 0xd4, 0x12).Trim();
                        dictionary5[SPXX.XH] = ToolUtil.GetString(buffer9, 230, 8).Trim();
                        dictionary5[SPXX.FPHXZ] = ToolUtil.GetString(buffer9, 0xee, 6).Trim();
                        dictionary5[SPXX.HSJBZ] = ToolUtil.GetString(buffer9, 0xf4, 6).Trim();
                        dictionary5[SPXX.SPSM] = string.Empty;
                        dictionary5[SPXX.FLBM] = string.Empty;
                        dictionary5[SPXX.SPBH] = string.Empty;
                        dictionary5[SPXX.XSYH] = "0";
                        dictionary5[SPXX.YHSM] = string.Empty;
                        dictionary5[SPXX.LSLVBS] = string.Empty;
                        this.Qdxx.Add(dictionary5);
                    }
                }
            }
            else if (this.jskFpmxVersion_0 == JskFpmxVersion.V2)
            {
                byte[] buffer12 = ToolUtil.GetBytes(string_3);
                int num11 = buffer12.Length / 0xfc;
                this.Mxxx = new List<Dictionary<SPXX, string>>();
                byte[] buffer7 = new byte[0xfc];
                string str15 = "";
                bool flag = false;
                for (int k = 0; k < num11; k++)
                {
                    Array.Copy(buffer12, 0xfc * k, buffer7, 0, 0xfc);
                    Dictionary<SPXX, string> dictionary4 = new Dictionary<SPXX, string>();
                    dictionary4[SPXX.SPMC] = ToolUtil.GetString(buffer7, 0, 0x5c).Trim();
                    dictionary4[SPXX.GGXH] = ToolUtil.GetString(buffer7, 0x5c, 40).Trim();
                    dictionary4[SPXX.JLDW] = ToolUtil.GetString(buffer7, 0x84, 0x16).Trim();
                    string str13 = ToolUtil.GetString(buffer7, 0x9a, 0x15).Trim();
                    dictionary4[SPXX.SL] = (str13 == "0") ? string.Empty : str13;
                    string str14 = ToolUtil.GetString(buffer7, 0xaf, 0x15).Trim();
                    dictionary4[SPXX.DJ] = (str14 == "0") ? string.Empty : str14;
                    dictionary4[SPXX.JE] = ToolUtil.GetString(buffer7, 0xc4, 0x12).Trim();
                    dictionary4[SPXX.SLV] = ToolUtil.GetString(buffer7, 0xd6, 6).Trim();
                    if (k == 0)
                    {
                        str15 = dictionary4[SPXX.SLV];
                    }
                    if (str15 != dictionary4[SPXX.SLV])
                    {
                        flag = true;
                    }
                    dictionary4[SPXX.SE] = ToolUtil.GetString(buffer7, 220, 0x12).Trim();
                    dictionary4[SPXX.XH] = ToolUtil.GetString(buffer7, 0xee, 8).Trim();
                    dictionary4[SPXX.FPHXZ] = ToolUtil.GetString(buffer7, 0xf6, 3).Trim();
                    dictionary4[SPXX.HSJBZ] = ToolUtil.GetString(buffer7, 0xf9, 3).Trim();
                    dictionary4[SPXX.SPSM] = string.Empty;
                    dictionary4[SPXX.FLBM] = string.Empty;
                    dictionary4[SPXX.SPBH] = string.Empty;
                    dictionary4[SPXX.XSYH] = "0";
                    dictionary4[SPXX.YHSM] = string.Empty;
                    dictionary4[SPXX.LSLVBS] = string.Empty;
                    this.Mxxx.Add(dictionary4);
                    this.string_0 = Class34.smethod_17(this.string_0, dictionary4[SPXX.JE]);
                }
                if (flag)
                {
                    this.sLv = "";
                }
                else
                {
                    this.sLv = str15;
                }
                if (string_4 != null)
                {
                    byte[] buffer = ToolUtil.GetBytes(string_4);
                    int num12 = buffer.Length / 0xfc;
                    this.Qdxx = new List<Dictionary<SPXX, string>>();
                    byte[] buffer2 = new byte[0xfc];
                    for (int m = 0; m < num12; m++)
                    {
                        Array.Copy(buffer, 0xfc * m, buffer2, 0, 0xfc);
                        Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                        dictionary[SPXX.SPMC] = ToolUtil.GetString(buffer2, 0, 0x5c).Trim();
                        dictionary[SPXX.GGXH] = ToolUtil.GetString(buffer2, 0x5c, 40).Trim();
                        dictionary[SPXX.JLDW] = ToolUtil.GetString(buffer2, 0x84, 0x16).Trim();
                        string str = ToolUtil.GetString(buffer2, 0x9a, 0x15).Trim();
                        dictionary[SPXX.SL] = (str == "0") ? string.Empty : str;
                        string str6 = ToolUtil.GetString(buffer2, 0xaf, 0x15).Trim();
                        dictionary[SPXX.DJ] = (str6 == "0") ? string.Empty : str6;
                        dictionary[SPXX.JE] = ToolUtil.GetString(buffer2, 0xc4, 0x12).Trim();
                        dictionary[SPXX.SLV] = ToolUtil.GetString(buffer2, 0xd6, 6).Trim();
                        dictionary[SPXX.SE] = ToolUtil.GetString(buffer2, 220, 0x12).Trim();
                        dictionary[SPXX.XH] = ToolUtil.GetString(buffer2, 0xee, 8).Trim();
                        dictionary[SPXX.FPHXZ] = ToolUtil.GetString(buffer2, 0xf6, 3).Trim();
                        dictionary[SPXX.HSJBZ] = ToolUtil.GetString(buffer2, 0xf9, 3).Trim();
                        dictionary[SPXX.SPSM] = string.Empty;
                        dictionary[SPXX.FLBM] = string.Empty;
                        dictionary[SPXX.SPBH] = string.Empty;
                        dictionary[SPXX.XSYH] = "0";
                        dictionary[SPXX.YHSM] = string.Empty;
                        dictionary[SPXX.LSLVBS] = string.Empty;
                        this.Qdxx.Add(dictionary);
                    }
                }
            }
            else if (this.jskFpmxVersion_0 == JskFpmxVersion.V3)
            {
                byte[] buffer3 = ToolUtil.GetBytes(string_3);
                int num13 = buffer3.Length / 0x178;
                this.Mxxx = new List<Dictionary<SPXX, string>>();
                byte[] buffer4 = new byte[0x178];
                string str16 = "";
                bool flag2 = false;
                for (int n = 0; n < num13; n++)
                {
                    Array.Copy(buffer3, 0x178 * n, buffer4, 0, 0x178);
                    Dictionary<SPXX, string> dictionary2 = new Dictionary<SPXX, string>();
                    dictionary2[SPXX.SPMC] = ToolUtil.GetString(buffer4, 0, 100).Trim();
                    dictionary2[SPXX.GGXH] = ToolUtil.GetString(buffer4, 100, 40).Trim();
                    dictionary2[SPXX.JLDW] = ToolUtil.GetString(buffer4, 140, 0x16).Trim();
                    string str2 = ToolUtil.GetString(buffer4, 0xa2, 0x15).Trim();
                    dictionary2[SPXX.SL] = (str2 == "0") ? string.Empty : str2;
                    string str4 = ToolUtil.GetString(buffer4, 0xb7, 0x15).Trim();
                    dictionary2[SPXX.DJ] = (str4 == "0") ? string.Empty : str4;
                    dictionary2[SPXX.JE] = ToolUtil.GetString(buffer4, 0xcc, 0x12).Trim();
                    dictionary2[SPXX.SLV] = ToolUtil.GetString(buffer4, 0xde, 6).Trim();
                    if (n == 0)
                    {
                        str16 = dictionary2[SPXX.SLV];
                    }
                    if (str16 != dictionary2[SPXX.SLV])
                    {
                        flag2 = true;
                    }
                    dictionary2[SPXX.SE] = ToolUtil.GetString(buffer4, 0xe4, 0x12).Trim();
                    dictionary2[SPXX.XH] = ToolUtil.GetString(buffer4, 0xf6, 8).Trim();
                    dictionary2[SPXX.FPHXZ] = ToolUtil.GetString(buffer4, 0xfe, 3).Trim();
                    dictionary2[SPXX.HSJBZ] = ToolUtil.GetString(buffer4, 0x101, 3).Trim();
                    dictionary2[SPXX.SPSM] = string.Empty;
                    dictionary2[SPXX.FLBM] = ToolUtil.GetString(buffer4, 260, 30).Trim();
                    dictionary2[SPXX.SPBH] = ToolUtil.GetString(buffer4, 290, 30).Trim();
                    dictionary2[SPXX.XSYH] = ToolUtil.GetString(buffer4, 320, 3).Trim();
                    dictionary2[SPXX.YHSM] = ToolUtil.GetString(buffer4, 0x143, 50).Trim();
                    dictionary2[SPXX.LSLVBS] = ToolUtil.GetString(buffer4, 0x175, 3).Trim();
                    this.Mxxx.Add(dictionary2);
                    this.string_0 = Class34.smethod_17(this.string_0, dictionary2[SPXX.JE]);
                }
                if (flag2)
                {
                    this.sLv = "";
                }
                else
                {
                    this.sLv = str16;
                }
                if (string_4 != null)
                {
                    byte[] buffer10 = ToolUtil.GetBytes(string_4);
                    int num9 = buffer10.Length / 0x178;
                    this.Qdxx = new List<Dictionary<SPXX, string>>();
                    byte[] buffer11 = new byte[0x178];
                    for (int num8 = 0; num8 < num9; num8++)
                    {
                        Array.Copy(buffer10, 0x178 * num8, buffer11, 0, 0x178);
                        Dictionary<SPXX, string> dictionary6 = new Dictionary<SPXX, string>();
                        dictionary6[SPXX.SPMC] = ToolUtil.GetString(buffer11, 0, 100).Trim();
                        dictionary6[SPXX.GGXH] = ToolUtil.GetString(buffer11, 100, 40).Trim();
                        dictionary6[SPXX.JLDW] = ToolUtil.GetString(buffer11, 140, 0x16).Trim();
                        string str8 = ToolUtil.GetString(buffer11, 0xa2, 0x15).Trim();
                        dictionary6[SPXX.SL] = (str8 == "0") ? string.Empty : str8;
                        string str18 = ToolUtil.GetString(buffer11, 0xb7, 0x15).Trim();
                        dictionary6[SPXX.DJ] = (str18 == "0") ? string.Empty : str18;
                        dictionary6[SPXX.JE] = ToolUtil.GetString(buffer11, 0xcc, 0x12).Trim();
                        dictionary6[SPXX.SLV] = ToolUtil.GetString(buffer11, 0xde, 6).Trim();
                        dictionary6[SPXX.SE] = ToolUtil.GetString(buffer11, 0xe4, 0x12).Trim();
                        dictionary6[SPXX.XH] = ToolUtil.GetString(buffer11, 0xf6, 8).Trim();
                        dictionary6[SPXX.FPHXZ] = ToolUtil.GetString(buffer11, 0xfe, 3).Trim();
                        dictionary6[SPXX.HSJBZ] = ToolUtil.GetString(buffer11, 0x101, 3).Trim();
                        dictionary6[SPXX.SPSM] = string.Empty;
                        dictionary6[SPXX.FLBM] = ToolUtil.GetString(buffer11, 260, 30).Trim();
                        dictionary6[SPXX.SPBH] = ToolUtil.GetString(buffer11, 290, 30).Trim();
                        dictionary6[SPXX.XSYH] = ToolUtil.GetString(buffer11, 320, 3).Trim();
                        dictionary6[SPXX.YHSM] = ToolUtil.GetString(buffer11, 0x143, 50).Trim();
                        dictionary6[SPXX.LSLVBS] = ToolUtil.GetString(buffer11, 0x175, 3).Trim();
                        this.Qdxx.Add(dictionary6);
                    }
                }
            }
        }

        private void method_4(string[] string_2, string string_3)
        {
            if ((string_2 == null) || (string_2.Length < 0x1d))
            {
                throw new BusinessObjectException(null, "Aisino.Fwkp.BusinessObject.Fpxx.ProccessFpmx_hyfp()", "金税卡返回的发票明细行数有误", null);
            }
            this.yysbz = "0000000000";
            this.spfnsrsbh = string_2[1];
            this.gfsh = this.spfnsrsbh;
            this.cyrnsrsbh = string_2[2];
            this.zgswjgdm = string_2[3];
            this.zgswjgmc = string_2[4];
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V3)
            {
                this.bmbbbh = this.method_2(string_2[5]);
            }
            this.jqbh = string_2[7];
            string str2 = string_2[8];
            if (str2.Length > 8)
            {
                string str3 = str2.Substring(8, str2.Length - 8);
                this.kprq = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + str3;
            }
            else if (str2.Length == 8)
            {
                this.kprq = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + " 00:00:00";
            }
            this.cyrmc = string_2[9];
            this.spfmc = string_2[10];
            this.shrmc = string_2[11];
            this.shrnsrsbh = string_2[12];
            this.fhrmc = string_2[13];
            this.fhrnsrsbh = string_2[14];
            this.qyd = string_2[15];
            this.je = string_2[0x10];
            this.sLv = string_2[0x11];
            this.se = string_2[0x12];
            this.czch = string_2[0x13];
            this.ccdw = string_2[20];
            this.skr = string_2[0x15];
            this.fhr = string_2[0x16];
            this.kpr = string_2[0x17];
            this.bz = string_2[0x18];
            if (this.isRed && (this.bz != null))
            {
                string str4 = NotesUtil.GetInfo(ToolUtil.GetString(Convert.FromBase64String(this.bz)), 2, "");
                if (str4.Length == 0x10)
                {
                    this.redNum = str4;
                }
            }
            this.yshwxx = string_2[0x19];
            this.jmbbh = string_2[0x1a];
            string[] strArray = null;
            string input = string_2[0x1b];
            string[] strArray2 = Regex.Split(input, "@");
            if (strArray2.Length >= 3)
            {
                this.blueFpdm = strArray2[1];
                this.blueFphm = strArray2[2];
            }
            if (strArray2.Length == 2)
            {
                string str6 = strArray2[1];
                strArray = Regex.Split(str6, "#");
            }
            if (strArray2.Length >= 4)
            {
                string str7 = strArray2[3];
                strArray = Regex.Split(str7, "#");
            }
            this.zyspmc = string_2[0x1c];
            if (!this.isBlankWaste)
            {
                if (this.jskFpmxVersion_0 == JskFpmxVersion.V3)
                {
                    byte[] bytes = ToolUtil.GetBytes(string_3);
                    int num3 = bytes.Length / 0x10a;
                    this.Mxxx = new List<Dictionary<SPXX, string>>();
                    byte[] destinationArray = new byte[0x10a];
                    bool flag2 = (strArray != null) && (strArray.Length == num3);
                    this.string_0 = "0.00";
                    for (int i = 0; i < num3; i++)
                    {
                        Array.Copy(bytes, 0x10a * i, destinationArray, 0, 0x10a);
                        Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                        item[SPXX.SPMC] = ToolUtil.GetString(destinationArray, 0, 0x70).Trim();
                        item[SPXX.XH] = ToolUtil.GetString(destinationArray, 0x70, 8).Trim();
                        item[SPXX.JE] = ToolUtil.GetString(destinationArray, 120, 30).Trim();
                        item[SPXX.GGXH] = string.Empty;
                        item[SPXX.JLDW] = string.Empty;
                        item[SPXX.SPSM] = string.Empty;
                        item[SPXX.SL] = string.Empty;
                        item[SPXX.DJ] = string.Empty;
                        item[SPXX.SLV] = this.sLv;
                        item[SPXX.FPHXZ] = "0";
                        if (flag2)
                        {
                            item[SPXX.SE] = strArray[i];
                        }
                        else
                        {
                            item[SPXX.SE] = Class34.smethod_12(item[SPXX.JE], item[SPXX.SLV], Struct40.int_50);
                        }
                        item[SPXX.HSJBZ] = "0";
                        item[SPXX.FLBM] = ToolUtil.GetString(destinationArray, 150, 30).Trim();
                        item[SPXX.SPBH] = ToolUtil.GetString(destinationArray, 180, 30).Trim();
                        item[SPXX.XSYH] = ToolUtil.GetString(destinationArray, 210, 3).Trim();
                        item[SPXX.YHSM] = ToolUtil.GetString(destinationArray, 0xd5, 50).Trim();
                        item[SPXX.LSLVBS] = ToolUtil.GetString(destinationArray, 0x107, 3).Trim();
                        this.Mxxx.Add(item);
                        this.string_0 = Class34.smethod_17(this.string_0, item[SPXX.JE]);
                    }
                }
                else
                {
                    byte[] sourceArray = ToolUtil.GetBytes(string_3);
                    int num = sourceArray.Length / 150;
                    this.Mxxx = new List<Dictionary<SPXX, string>>();
                    byte[] buffer2 = new byte[150];
                    bool flag = (strArray != null) && (strArray.Length == num);
                    this.string_0 = "0.00";
                    for (int j = 0; j < num; j++)
                    {
                        Array.Copy(sourceArray, 150 * j, buffer2, 0, 150);
                        Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                        dictionary[SPXX.SPMC] = ToolUtil.GetString(buffer2, 0, 0x70).Trim();
                        dictionary[SPXX.XH] = ToolUtil.GetString(buffer2, 0x70, 8).Trim();
                        dictionary[SPXX.JE] = ToolUtil.GetString(buffer2, 120, 30).Trim();
                        dictionary[SPXX.GGXH] = string.Empty;
                        dictionary[SPXX.JLDW] = string.Empty;
                        dictionary[SPXX.SPSM] = string.Empty;
                        dictionary[SPXX.SL] = string.Empty;
                        dictionary[SPXX.DJ] = string.Empty;
                        dictionary[SPXX.SLV] = this.sLv;
                        dictionary[SPXX.FPHXZ] = "0";
                        if (flag)
                        {
                            dictionary[SPXX.SE] = strArray[j];
                        }
                        else
                        {
                            dictionary[SPXX.SE] = Class34.smethod_12(dictionary[SPXX.JE], dictionary[SPXX.SLV], Struct40.int_50);
                        }
                        dictionary[SPXX.HSJBZ] = "0";
                        dictionary[SPXX.FLBM] = string.Empty;
                        dictionary[SPXX.SPBH] = string.Empty;
                        dictionary[SPXX.XSYH] = "0";
                        dictionary[SPXX.YHSM] = string.Empty;
                        dictionary[SPXX.LSLVBS] = string.Empty;
                        this.Mxxx.Add(dictionary);
                        this.string_0 = Class34.smethod_17(this.string_0, dictionary[SPXX.JE]);
                    }
                }
            }
        }

        private void method_5(string[] string_2)
        {
            bool flag;
            if ((string_2 == null) || (string_2.Length < 0x23))
            {
                throw new BusinessObjectException(null, "Aisino.Fwkp.BusinessObject.Fpxx.ProccessFpmx_jdc()", "金税卡返回的发票明细行数有误", null);
            }
            this.gfsh = string_2[1];
            this.xfsh = string_2[2];
            this.clsbdh = string_2[3];
            this.zgswjgdm = string_2[4];
            this.zgswjgmc = string_2[5];
            if (flag = string_2[6].IndexOf("B") >= 0)
            {
                this.bmbbbh = this.method_2(string_2[6]);
            }
            else
            {
                this.bmbbbh = string.Empty;
            }
            this.jqbh = string_2[8];
            string str3 = string_2[9];
            if (str3.Length > 8)
            {
                string str4 = str3.Substring(8, str3.Length - 8);
                this.kprq = str3.Substring(0, 4) + "-" + str3.Substring(4, 2) + "-" + str3.Substring(6, 2) + str4;
            }
            else if (str3.Length == 8)
            {
                this.kprq = str3.Substring(0, 4) + "-" + str3.Substring(4, 2) + "-" + str3.Substring(6, 2) + " 00:00:00";
            }
            this.gfmc = string_2[10];
            this.cllx = string_2[11];
            this.cpxh = string_2[12];
            this.cd = string_2[13];
            this.hgzh = string_2[14];
            this.jkzmsh = string_2[15];
            this.sjdh = string_2[0x10];
            this.fdjhm = string_2[0x11];
            this.xfmc = string_2[0x12];
            this.xfdz = string_2[0x13];
            this.xfdh = string_2[20];
            this.xfyh = string_2[0x15];
            this.xfzh = string_2[0x16];
            this.je = string_2[0x17];
            this.sLv = string_2[0x18];
            this.se = string_2[0x19];
            this.dw = string_2[0x1a];
            this.xcrs = string_2[0x1b];
            this.kpr = string_2[0x1c];
            this.sccjmc = string_2[0x1d];
            this.jmbbh = string_2[30];
            this.bsq = Convert.ToInt32(string_2[0x1f]);
            this.bz = string_2[0x20];
            if (this.isRed && (this.bz != null))
            {
                string str2 = NotesUtil.GetInfo(ToolUtil.GetString(Convert.FromBase64String(this.bz)), 3, "");
                if (str2.Length == 20)
                {
                    this.blueFpdm = str2.Substring(0, 12);
                    this.blueFphm = str2.Substring(12, 8);
                }
            }
            this.sfzhm = string_2[0x21];
            this.isNewJdcfp = string_2[0x22] == "2";
            this.yysbz = this.isNewJdcfp ? "0000200000" : "0000100000";
            if (flag)
            {
                this.zyspmc = string_2[0x23];
                this.zyspsm = string_2[0x24] + "#%" + string_2[0x26];
                this.skr = string_2[0x25] + "#%" + string_2[0x27];
            }
        }

        private string method_6(string[] string_2)
        {
            if ((string_2 == null) || (string_2.Length < 0x17))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("0");
            builder.Append(this.hzfw ? "1" : "0");
            if (string_2[1].IndexOf("V2") >= 0)
            {
                builder.Append("1");
            }
            else if (string_2[1].IndexOf("V3") >= 0)
            {
                builder.Append("2");
            }
            else if (string_2[1].IndexOf("V4") >= 0)
            {
                builder.Append("3");
            }
            else
            {
                builder.Append("0");
            }
            builder.Append("0");
            builder.Append("0");
            string str = (string_2[1].IndexOf("V5") >= 0) ? "1" : ((string_2[1].IndexOf("V6") >= 0) ? "2" : "0");
            builder.Append(str);
            builder.Append('0', 4);
            return builder.ToString();
        }

        private string method_7(InvDetail invDetail_0, string string_2)
        {
            if (invDetail_0.TaxClass < this.string_1.Length)
            {
                return this.string_1[invDetail_0.TaxClass];
            }
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V0)
            {
                if (Math.Abs(invDetail_0.Amount) < 0.001)
                {
                    return "0";
                }
                return Math.Round((double) (invDetail_0.Tax / invDetail_0.Amount), 3).ToString();
            }
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V1)
            {
                byte[] buffer = new byte[6];
                Array.Copy(ToolUtil.GetBytes(string_2), 0xce, buffer, 0, 6);
                return ToolUtil.GetString(buffer).Trim();
            }
            if (this.jskFpmxVersion_0 == JskFpmxVersion.V2)
            {
                byte[] buffer2 = new byte[6];
                Array.Copy(ToolUtil.GetBytes(string_2), 0xd6, buffer2, 0, 6);
                return ToolUtil.GetString(buffer2).Trim();
            }
            if (this.jskFpmxVersion_0 != JskFpmxVersion.V3)
            {
                throw new BusinessObjectException(null, "Aisino.Fwkp.BusinessObject.Fpxx.GetTaxRate()", "金税卡返回的发票明细长度有误", null);
            }
            byte[] destinationArray = new byte[6];
            Array.Copy(ToolUtil.GetBytes(string_2), 0xde, destinationArray, 0, 6);
            return ToolUtil.GetString(destinationArray).Trim();
        }

        private void method_8(InvDetail invDetail_0, TaxCard taxCard_0)
        {
            if (this.isBlankWaste)
            {
                this.sLv = "0.17";
            }
        }

        public string RepairInv(InvDetail invDetail_0, int int_0)
        {
            string str = "0000";
            try
            {
                if (int_0 < 0)
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    List<int> list = null;
                    card.GetPeriodCount(0, out list, 0);
                    int_0 = list[1];
                }
                str = this.method_0(invDetail_0, int_0);
                if (str != "0000")
                {
                    ilog_0.ErrorFormat("发票修复不成功，错误号={0}", str);
                    if (str != "A643")
                    {
                        object[] objArray2 = new object[] { Invoice.FPLX2Str(this.fplx), this.fpdm, this.fphm };
                        if (!(ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray2)[0] is Fpxx))
                        {
                            this.zyspmc = "修复";
                            this.kpr = "修复";
                            this.dybz = true;
                            this.xfbz = true;
                            str = "0000";
                            this.retCode = str;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                str = "A679";
                ilog_0.Error(exception.Message);
            }
            this.retCode = str;
            return str;
        }

        public byte[] Seriealize()
        {
            XmlDeclaration declaration;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            XmlDocument document = new XmlDocument();
            if (card.SubSoftVersion != "Linux")
            {
                declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            }
            else
            {
                declaration = document.CreateXmlDeclaration("1.0", "GBK", null);
            }
            document.AppendChild(declaration);
            XmlElement newChild = document.CreateElement("FP");
            document.AppendChild(newChild);
            XmlElement element85 = document.CreateElement("blueFpdm");
            element85.InnerText = this.blueFpdm;
            newChild.AppendChild(element85);
            XmlElement element86 = document.CreateElement("blueFphm");
            element86.InnerText = this.blueFphm;
            newChild.AppendChild(element86);
            XmlElement element68 = document.CreateElement("bsbz");
            element68.InnerText = this.bsbz ? "1" : "0";
            newChild.AppendChild(element68);
            XmlElement element69 = document.CreateElement("bsq");
            element69.InnerText = this.bsq.ToString();
            newChild.AppendChild(element69);
            XmlElement element70 = document.CreateElement("bszt");
            element70.InnerText = this.bszt.ToString();
            newChild.AppendChild(element70);
            XmlElement element71 = document.CreateElement("bz");
            if (card.SubSoftVersion == "Linux")
            {
                string str7 = ToolUtil.GetString(Convert.FromBase64String(this.bz));
                foreach (char ch3 in SpecialChar)
                {
                    str7 = str7.Replace(ch3, '?');
                }
                str7 = Convert.ToBase64String(ToolUtil.GetBytes(str7));
                element71.InnerText = str7;
            }
            else
            {
                element71.InnerText = this.bz;
            }
            newChild.AppendChild(element71);
            XmlElement element73 = document.CreateElement("ccdw");
            element73.InnerText = this.ccdw;
            newChild.AppendChild(element73);
            XmlElement element74 = document.CreateElement("cd");
            element74.InnerText = this.cd;
            newChild.AppendChild(element74);
            XmlElement element75 = document.CreateElement("cllx");
            element75.InnerText = this.cllx;
            newChild.AppendChild(element75);
            XmlElement element76 = document.CreateElement("clsbdh");
            element76.InnerText = this.clsbdh;
            newChild.AppendChild(element76);
            XmlElement element77 = document.CreateElement("cpxh");
            element77.InnerText = this.cpxh;
            newChild.AppendChild(element77);
            XmlElement element78 = document.CreateElement("cyrmc");
            element78.InnerText = this.cyrmc;
            newChild.AppendChild(element78);
            XmlElement element79 = document.CreateElement("cyrnsrsbh");
            element79.InnerText = this.cyrnsrsbh;
            newChild.AppendChild(element79);
            XmlElement element80 = document.CreateElement("czch");
            element80.InnerText = this.czch;
            newChild.AppendChild(element80);
            XmlElement element81 = document.CreateElement("dkqymc");
            element81.InnerText = this.dkqymc;
            newChild.AppendChild(element81);
            XmlElement element82 = document.CreateElement("dkqysh");
            element82.InnerText = this.dkqysh;
            newChild.AppendChild(element82);
            XmlElement element83 = document.CreateElement("dw");
            element83.InnerText = this.dw;
            newChild.AppendChild(element83);
            XmlElement element84 = document.CreateElement("dybz");
            element84.InnerText = this.dybz ? "1" : "0";
            newChild.AppendChild(element84);
            XmlElement element87 = document.CreateElement("dymb");
            element87.InnerText = this.dy_mb;
            newChild.AppendChild(element87);
            XmlElement element88 = document.CreateElement("dzsyh");
            element88.InnerText = this.dzsyh;
            newChild.AppendChild(element88);
            XmlElement element89 = document.CreateElement("fdjhm");
            element89.InnerText = this.fdjhm;
            newChild.AppendChild(element89);
            XmlElement element90 = document.CreateElement("fhr");
            element90.InnerText = this.fhr;
            newChild.AppendChild(element90);
            XmlElement element91 = document.CreateElement("fhrmc");
            element91.InnerText = this.fhrmc;
            newChild.AppendChild(element91);
            XmlElement element92 = document.CreateElement("fhrnsrsbh");
            element92.InnerText = this.fhrnsrsbh;
            newChild.AppendChild(element92);
            XmlElement element93 = document.CreateElement("fpdm");
            element93.InnerText = this.fpdm;
            newChild.AppendChild(element93);
            XmlElement element94 = document.CreateElement("fphm");
            element94.InnerText = this.fphm;
            newChild.AppendChild(element94);
            XmlElement element95 = document.CreateElement("fplx");
            element95.InnerText = Invoice.FPLX2Str(this.fplx);
            newChild.AppendChild(element95);
            XmlElement element96 = document.CreateElement("gfbh");
            element96.InnerText = this.gfbh;
            newChild.AppendChild(element96);
            XmlElement element97 = document.CreateElement("gfdzdh");
            element97.InnerText = this.gfdzdh;
            newChild.AppendChild(element97);
            XmlElement element98 = document.CreateElement("gfmc");
            element98.InnerText = this.gfmc;
            newChild.AppendChild(element98);
            XmlElement element99 = document.CreateElement("gfsh");
            element99.InnerText = this.gfsh;
            newChild.AppendChild(element99);
            XmlElement element100 = document.CreateElement("gfyhzh");
            element100.InnerText = this.gfyhzh;
            newChild.AppendChild(element100);
            XmlElement element101 = document.CreateElement("hgzh");
            element101.InnerText = this.hgzh;
            newChild.AppendChild(element101);
            XmlElement element102 = document.CreateElement("hxm");
            element102.InnerText = this.hxm;
            newChild.AppendChild(element102);
            XmlElement element62 = document.CreateElement("hzfw");
            element62.InnerText = this.hzfw ? "1" : "0";
            newChild.AppendChild(element62);
            XmlElement element63 = document.CreateElement("invQryNo");
            element63.InnerText = this.invQryNo.ToString();
            newChild.AppendChild(element63);
            XmlElement element23 = document.CreateElement("isBlankWaste");
            element23.InnerText = this.isBlankWaste ? "1" : "0";
            newChild.AppendChild(element23);
            XmlElement element21 = document.CreateElement("isNewJdcfp");
            element21.InnerText = this.isNewJdcfp ? "1" : "0";
            newChild.AppendChild(element21);
            XmlElement element22 = document.CreateElement("isRed");
            element22.InnerText = this.isRed ? "1" : "0";
            newChild.AppendChild(element22);
            XmlElement element24 = document.CreateElement("isSqd");
            element24.InnerText = this.bool_0 ? "1" : "0";
            newChild.AppendChild(element24);
            XmlElement element25 = document.CreateElement("je");
            element25.InnerText = this.je;
            newChild.AppendChild(element25);
            XmlElement element26 = document.CreateElement("jkzmsh");
            element26.InnerText = this.jkzmsh;
            newChild.AppendChild(element26);
            XmlElement element27 = document.CreateElement("jmbbh");
            element27.InnerText = this.jmbbh;
            newChild.AppendChild(element27);
            XmlElement element28 = document.CreateElement("jqbh");
            element28.InnerText = this.jqbh;
            newChild.AppendChild(element28);
            XmlElement element29 = document.CreateElement("jsk_fpmx_version");
            element29.InnerText = ((int) this.jskFpmxVersion_0).ToString();
            newChild.AppendChild(element29);
            XmlElement element30 = document.CreateElement("jym");
            element30.InnerText = this.jym;
            newChild.AppendChild(element30);
            XmlElement element31 = document.CreateElement("keyFlagNo");
            element31.InnerText = this.keyFlagNo.ToString();
            newChild.AppendChild(element31);
            XmlElement element32 = document.CreateElement("kpjh");
            element32.InnerText = this.kpjh.ToString();
            newChild.AppendChild(element32);
            XmlElement element33 = document.CreateElement("kpr");
            element33.InnerText = this.kpr;
            newChild.AppendChild(element33);
            XmlElement element34 = document.CreateElement("kprq");
            element34.InnerText = this.kprq;
            newChild.AppendChild(element34);
            XmlElement element35 = document.CreateElement("kpsxh");
            element35.InnerText = this.kpsxh;
            newChild.AppendChild(element35);
            XmlElement element36 = document.CreateElement("mw");
            element36.InnerText = this.mw;
            newChild.AppendChild(element36);
            XmlElement element37 = document.CreateElement("mxhjje");
            element37.InnerText = this.string_0;
            newChild.AppendChild(element37);
            XmlElement element38 = document.CreateElement("qyd");
            element38.InnerText = this.qyd;
            newChild.AppendChild(element38);
            XmlElement element2 = document.CreateElement("rdmByte");
            element2.InnerText = (this.rdmByte == null) ? "" : Convert.ToBase64String(this.rdmByte);
            newChild.AppendChild(element2);
            XmlElement element3 = document.CreateElement("redNum");
            element3.InnerText = this.redNum;
            newChild.AppendChild(element3);
            XmlElement element4 = document.CreateElement("retCode");
            element4.InnerText = this.retCode;
            newChild.AppendChild(element4);
            XmlElement element5 = document.CreateElement("sccjmc");
            element5.InnerText = this.sccjmc;
            newChild.AppendChild(element5);
            XmlElement element6 = document.CreateElement("se");
            element6.InnerText = this.se;
            newChild.AppendChild(element6);
            XmlElement element7 = document.CreateElement("sfzhm");
            element7.InnerText = this.sfzhm;
            newChild.AppendChild(element7);
            XmlElement element8 = document.CreateElement("shrmc");
            element8.InnerText = this.shrmc;
            newChild.AppendChild(element8);
            XmlElement element9 = document.CreateElement("shrnsrsbh");
            element9.InnerText = this.shrnsrsbh;
            newChild.AppendChild(element9);
            XmlElement element10 = document.CreateElement("sign");
            element10.InnerText = this.sign;
            newChild.AppendChild(element10);
            XmlElement element11 = document.CreateElement("sjdh");
            element11.InnerText = this.sjdh;
            newChild.AppendChild(element11);
            XmlElement element12 = document.CreateElement("skr");
            element12.InnerText = this.skr;
            newChild.AppendChild(element12);
            XmlElement element13 = document.CreateElement("sLv");
            element13.InnerText = this.sLv;
            newChild.AppendChild(element13);
            XmlElement element14 = document.CreateElement("spfmc");
            element14.InnerText = this.spfmc;
            newChild.AppendChild(element14);
            XmlElement element15 = document.CreateElement("spfnsrsbh");
            element15.InnerText = this.spfnsrsbh;
            newChild.AppendChild(element15);
            XmlElement element16 = document.CreateElement("ssyf");
            element16.InnerText = this.ssyf.ToString();
            newChild.AppendChild(element16);
            XmlElement element17 = document.CreateElement("TAX_CLASS");
            newChild.AppendChild(element17);
            if (this.string_1 != null)
            {
                for (int i = 0; i < this.string_1.Length; i++)
                {
                    XmlElement element103 = document.CreateElement("value");
                    element103.InnerText = this.string_1[i];
                    element17.AppendChild(element103);
                }
            }
            XmlElement element66 = document.CreateElement("wspzhm");
            element66.InnerText = this.wspzhm;
            newChild.AppendChild(element66);
            XmlElement element67 = document.CreateElement("xcrs");
            element67.InnerText = this.xcrs;
            newChild.AppendChild(element67);
            XmlElement element44 = document.CreateElement("xfbz");
            element44.InnerText = this.xfbz ? "1" : "0";
            newChild.AppendChild(element44);
            XmlElement element45 = document.CreateElement("xfdh");
            element45.InnerText = this.xfdh;
            newChild.AppendChild(element45);
            XmlElement element46 = document.CreateElement("xfdz");
            element46.InnerText = this.xfdz;
            newChild.AppendChild(element46);
            XmlElement element47 = document.CreateElement("xfdzdh");
            element47.InnerText = this.xfdzdh;
            newChild.AppendChild(element47);
            XmlElement element48 = document.CreateElement("xfmc");
            element48.InnerText = this.xfmc;
            newChild.AppendChild(element48);
            XmlElement element49 = document.CreateElement("xfsh");
            element49.InnerText = this.xfsh;
            newChild.AppendChild(element49);
            XmlElement element50 = document.CreateElement("xfyh");
            element50.InnerText = this.xfyh;
            newChild.AppendChild(element50);
            XmlElement element51 = document.CreateElement("xfyhzh");
            element51.InnerText = this.xfyhzh;
            newChild.AppendChild(element51);
            XmlElement element52 = document.CreateElement("xfzh");
            element52.InnerText = this.xfzh;
            newChild.AppendChild(element52);
            XmlElement element53 = document.CreateElement("xsdjbh");
            element53.InnerText = this.xsdjbh;
            newChild.AppendChild(element53);
            XmlElement element18 = document.CreateElement("yshwxx");
            if (card.SubSoftVersion == "Linux")
            {
                string str3 = ToolUtil.GetString(Convert.FromBase64String(this.yshwxx));
                foreach (char ch2 in SpecialChar)
                {
                    str3 = str3.Replace(ch2, '?');
                }
                str3 = Convert.ToBase64String(ToolUtil.GetBytes(str3));
                element18.InnerText = str3;
            }
            else
            {
                element18.InnerText = this.yshwxx;
            }
            newChild.AppendChild(element18);
            XmlElement element19 = document.CreateElement("yysbz");
            element19.InnerText = this.yysbz;
            newChild.AppendChild(element19);
            XmlElement element20 = document.CreateElement("zfbz");
            element20.InnerText = this.zfbz ? "1" : "0";
            newChild.AppendChild(element20);
            XmlElement element54 = document.CreateElement("zfsj");
            element54.InnerText = this.zfsj;
            newChild.AppendChild(element54);
            XmlElement element55 = document.CreateElement("zgswjgdm");
            element55.InnerText = this.zgswjgdm;
            newChild.AppendChild(element55);
            XmlElement element56 = document.CreateElement("zgswjgmc");
            element56.InnerText = this.zgswjgmc;
            newChild.AppendChild(element56);
            XmlElement element57 = document.CreateElement("zyfpLx");
            element57.InnerText = ((int) this.Zyfplx).ToString();
            newChild.AppendChild(element57);
            XmlElement element58 = document.CreateElement("zyspmc");
            element58.InnerText = this.zyspmc;
            newChild.AppendChild(element58);
            XmlElement element59 = document.CreateElement("zyspsm");
            element59.InnerText = this.zyspsm;
            newChild.AppendChild(element59);
            XmlElement element60 = document.CreateElement("bmbbbh");
            element60.InnerText = this.bmbbbh;
            newChild.AppendChild(element60);
            XmlElement element61 = document.CreateElement("Mxxx");
            newChild.AppendChild(element61);
            if (this.Mxxx != null)
            {
                foreach (Dictionary<SPXX, string> dictionary2 in this.Mxxx)
                {
                    XmlElement element104 = document.CreateElement("spxx");
                    foreach (KeyValuePair<SPXX, string> pair2 in dictionary2)
                    {
                        XmlElement element105 = document.CreateElement("sp");
                        if (card.SubSoftVersion != "Linux")
                        {
                            element105.SetAttribute("key", ((int) pair2.Key).ToString());
                        }
                        else
                        {
                            element105.SetAttribute("key", pair2.Key.ToString());
                        }
                        string str8 = pair2.Value;
                        if (((card.SubSoftVersion == "Linux") && (((SPXX) pair2.Key) == SPXX.SE)) && string.Equals(pair2.Value, "-0.00"))
                        {
                            str8 = "0.00";
                        }
                        element105.SetAttribute("value", str8);
                        element104.AppendChild(element105);
                    }
                    if (card.SubSoftVersion == "Linux")
                    {
                        string str9 = this.Get_Print_Dj(dictionary2, 1, null);
                        XmlElement element106 = document.CreateElement("sp");
                        element106.SetAttribute("key", "BHSDJ");
                        element106.SetAttribute("value", string.IsNullOrEmpty(str9) ? "" : Class34.smethod_19(str9, 8));
                        element104.AppendChild(element106);
                    }
                    element61.AppendChild(element104);
                }
            }
            XmlElement element42 = document.CreateElement("Qdxx");
            newChild.AppendChild(element42);
            if (this.Qdxx != null)
            {
                foreach (Dictionary<SPXX, string> dictionary in this.Qdxx)
                {
                    XmlElement element39 = document.CreateElement("spxx");
                    foreach (KeyValuePair<SPXX, string> pair in dictionary)
                    {
                        XmlElement element40 = document.CreateElement("sp");
                        if (card.SubSoftVersion != "Linux")
                        {
                            element40.SetAttribute("key", ((int) pair.Key).ToString());
                        }
                        else
                        {
                            element40.SetAttribute("key", pair.Key.ToString());
                        }
                        string str5 = pair.Value;
                        if (((card.SubSoftVersion == "Linux") && (((SPXX) pair.Key) == SPXX.SE)) && string.Equals(pair.Value, "-0.00"))
                        {
                            str5 = "0.00";
                        }
                        element40.SetAttribute("value", str5);
                        element39.AppendChild(element40);
                    }
                    if (card.SubSoftVersion == "Linux")
                    {
                        string str6 = this.Get_Print_Dj(dictionary, 1, null);
                        XmlElement element41 = document.CreateElement("sp");
                        element41.SetAttribute("key", "BHSDJ");
                        element41.SetAttribute("value", string.IsNullOrEmpty(str6) ? "" : Class34.smethod_19(str6, 8));
                        element39.AppendChild(element41);
                    }
                    element42.AppendChild(element39);
                }
            }
            if (card.SubSoftVersion != "Linux")
            {
                XmlElement element43 = document.CreateElement("Data");
                newChild.AppendChild(element43);
                if (this.data != null)
                {
                    XmlElement element64 = document.CreateElement("Data1");
                    element64.InnerText = Convert.ToBase64String((byte[]) this.data[0]);
                    element43.AppendChild(element64);
                    XmlElement element65 = document.CreateElement("Data2");
                    string[] strArray = (string[]) this.data[1];
                    string str4 = "";
                    ilog_0.Debug(0xe9);
                    if (strArray != null)
                    {
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            str4 = str4 + ((strArray[j] == null) ? "" : Convert.ToBase64String(ToolUtil.GetBytes(strArray[j]))) + ";";
                        }
                        str4 = str4.Substring(0, str4.Length - 1);
                    }
                    element65.InnerText = str4;
                    element43.AppendChild(element65);
                    XmlElement element72 = document.CreateElement("Data3");
                    element72.InnerText = Convert.ToBase64String((byte[]) this.data[2]);
                    element43.AppendChild(element72);
                }
                byte[] buffer2 = new byte[] { 
                    0x33, 0x95, 0x9d, 0x54, 30, 0x57, 0x9c, 0xb9, 0x2a, 0x8b, 0xb5, 0x6a, 0x1c, 0x9d, 0xff, 0xb2, 
                    0x2d, 0xbb, 0xae, 0xcd, 0x5c, 240, 0x5f, 0x2f, 0x4d, 0x9e, 0x8b, 170, 0xcb, 0x9e, 0xbf, 13
                 };
                byte[] buffer3 = new byte[] { 0xa1, 0xac, 0xbb, 0xba, 0xfb, 9, 200, 0xb9, 12, 0x9f, 0x56, 0xa9, 0xbd, 0xca, 0x6b, 0xf3 };
                return AES_Crypt.Encrypt(ToolUtil.GetBytes(document.InnerXml.ToString()), buffer2, buffer3);
            }
            string str2 = document.InnerXml.ToString();
            foreach (char ch in SpecialChar)
            {
                str2 = str2.Replace(ch, '?');
            }
            return ToolUtil.GetBytes(str2);
        }
    }
}

