namespace Aisino.Fwkp.Fptk
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class XmlProcessor
    {
        private ILog log = LogUtil.GetLogger<XmlProcessor>();

        public XmlDocument CreateDKFPXml(Fpxx fp)
        {
            if (fp == null)
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration newChild = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDoc.AppendChild(newChild);
            try
            {
                if (fp.fplx == 0)
                {
                    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, "INSDATA", "");
                    xmlDoc.AppendChild(node);
                    XmlNode node2 = xmlDoc.CreateElement("KPCG_FPHM");
                    node2.InnerText = fp.fphm;
                    node.AppendChild(node2);
                    XmlNode node3 = xmlDoc.CreateElement("KPCG_FPDM");
                    node3.InnerText = fp.fpdm;
                    node.AppendChild(node3);
                    XmlNode node4 = xmlDoc.CreateElement("WSPZHM");
                    node4.InnerText = fp.xfyhzh;
                    node.AppendChild(node4);
                    XmlNode node5 = xmlDoc.CreateElement("FPZL");
                    node5.InnerText = this.PareFpType(fp.fplx).ToString();
                    node.AppendChild(node5);
                    XmlNode node6 = xmlDoc.CreateElement("ZFZHBZ");
                    node6.InnerText = "Y";
                    node.AppendChild(node6);
                    XmlNode node7 = xmlDoc.CreateElement("KPRQ");
                    node7.InnerText = Convert.ToDateTime(fp.kprq).ToString("yyyy-MM-dd");
                    node.AppendChild(node7);
                    XmlNode node8 = xmlDoc.CreateElement("HJJE");
                    node8.InnerText = fp.je;
                    node.AppendChild(node8);
                    XmlNode node9 = xmlDoc.CreateElement("SLV");
                    node9.InnerText = fp.sLv;
                    node.AppendChild(node9);
                    XmlNode node10 = xmlDoc.CreateElement("HJSE");
                    node10.InnerText = fp.se;
                    node.AppendChild(node10);
                    XmlNode node11 = xmlDoc.CreateElement("GFSH");
                    node11.InnerText = fp.gfsh;
                    node.AppendChild(node11);
                    XmlNode node12 = xmlDoc.CreateElement("GFMC");
                    node12.InnerText = fp.gfmc;
                    node.AppendChild(node12);
                    XmlNode node13 = xmlDoc.CreateElement("GFDZ_DH");
                    node13.InnerText = fp.gfdzdh;
                    node.AppendChild(node13);
                    XmlNode node14 = xmlDoc.CreateElement("GFYHMC_YHZH");
                    node14.InnerText = fp.gfyhzh;
                    node.AppendChild(node14);
                    XmlNode node15 = xmlDoc.CreateElement("XFSH");
                    node15.InnerText = fp.xfsh;
                    node.AppendChild(node15);
                    XmlNode node16 = xmlDoc.CreateElement("XFMC");
                    node16.InnerText = fp.xfmc;
                    node.AppendChild(node16);
                    XmlNode node17 = xmlDoc.CreateElement("XFYHMC_YHZH");
                    node17.InnerText = fp.xfdzdh;
                    node.AppendChild(node17);
                    XmlNode node18 = xmlDoc.CreateElement("BZ");
                    node18.InnerText = ToolUtil.GetString(Convert.FromBase64String(fp.bz));
                    node.AppendChild(node18);
                    XmlNode node19 = xmlDoc.CreateElement("SWJGDM");
                    node19.InnerText = "0";
                    node.AppendChild(node19);
                    XmlNode node20 = xmlDoc.CreateElement("SWJGMC");
                    node20.InnerText = "0";
                    node.AppendChild(node20);
                    XmlNode node21 = xmlDoc.CreateElement("SKR");
                    node21.InnerText = fp.skr;
                    node.AppendChild(node21);
                    XmlNode node22 = xmlDoc.CreateElement("FHR");
                    node22.InnerText = fp.fhr;
                    node.AppendChild(node22);
                    XmlNode node23 = xmlDoc.CreateElement("KPR");
                    node23.InnerText = fp.kpr;
                    node.AppendChild(node23);
                    XmlNode node24 = xmlDoc.CreateElement("TSNSRSBH");
                    node24.InnerText = "0";
                    node.AppendChild(node24);
                    XmlNode node25 = xmlDoc.CreateElement("ZFBZ");
                    node25.InnerText = fp.zfbz ? "Y" : "N";
                    node.AppendChild(node25);
                    XmlNode node26 = xmlDoc.CreateElement("ZBNRS");
                    node.AppendChild(node26);
                    if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0))
                    {
                        foreach (Dictionary<SPXX, string> dictionary in fp.Qdxx)
                        {
                            XmlNode node27 = this.CreateZBNode(fp, xmlDoc, dictionary);
                            node26.AppendChild(node27);
                        }
                    }
                    else if ((fp.Mxxx != null) && (fp.Mxxx.Count > 0))
                    {
                        foreach (Dictionary<SPXX, string> dictionary2 in fp.Mxxx)
                        {
                            XmlNode node28 = this.CreateZBNode(fp, xmlDoc, dictionary2);
                            node26.AppendChild(node28);
                        }
                    }
                    xmlDoc.Save(@"D:\swdkFp.xml");
                    return xmlDoc;
                }
                XmlNode node29 = xmlDoc.CreateNode(XmlNodeType.Element, "INSDATAPP", "");
                xmlDoc.AppendChild(node29);
                XmlNode node30 = xmlDoc.CreateElement("KPCG_FPHM");
                node30.InnerText = fp.fphm;
                node29.AppendChild(node30);
                XmlNode node31 = xmlDoc.CreateElement("KPCG_FPDM");
                node31.InnerText = fp.fpdm;
                node29.AppendChild(node31);
                XmlNode node32 = xmlDoc.CreateElement("WSPZHM");
                node32.InnerText = fp.xfyhzh;
                node29.AppendChild(node32);
                XmlNode node33 = xmlDoc.CreateElement("FPZL");
                node33.InnerText = this.PareFpType(fp.fplx).ToString();
                node29.AppendChild(node33);
                XmlNode node34 = xmlDoc.CreateElement("ZFZHBZ");
                node34.InnerText = "Y";
                node29.AppendChild(node34);
                XmlNode node35 = xmlDoc.CreateElement("KPRQ");
                node35.InnerText = Convert.ToDateTime(fp.kprq).ToString("yyyy-MM-dd");
                node29.AppendChild(node35);
                XmlNode node36 = xmlDoc.CreateElement("HJJE");
                node36.InnerText = fp.je;
                node29.AppendChild(node36);
                XmlNode node37 = xmlDoc.CreateElement("SLV");
                node37.InnerText = fp.sLv;
                node29.AppendChild(node37);
                XmlNode node38 = xmlDoc.CreateElement("HJSE");
                node38.InnerText = fp.se;
                node29.AppendChild(node38);
                XmlNode node39 = xmlDoc.CreateElement("GFSH");
                node39.InnerText = fp.gfsh;
                node29.AppendChild(node39);
                XmlNode node40 = xmlDoc.CreateElement("GFMC");
                node40.InnerText = fp.gfmc;
                node29.AppendChild(node40);
                XmlNode node41 = xmlDoc.CreateElement("GFDZ_DH");
                node41.InnerText = fp.gfdzdh;
                node29.AppendChild(node41);
                XmlNode node42 = xmlDoc.CreateElement("GFYHMC_YHZH");
                node42.InnerText = fp.gfyhzh;
                node29.AppendChild(node42);
                XmlNode node43 = xmlDoc.CreateElement("XFSH");
                node43.InnerText = fp.xfsh;
                node29.AppendChild(node43);
                XmlNode node44 = xmlDoc.CreateElement("XFMC");
                node44.InnerText = fp.xfmc;
                node29.AppendChild(node44);
                XmlNode node45 = xmlDoc.CreateElement("XFDZ_DH");
                node45.InnerText = fp.xfdzdh;
                node29.AppendChild(node45);
                XmlNode node46 = xmlDoc.CreateElement("XFYHMC_YHZH");
                node46.InnerText = fp.xfyhzh;
                node29.AppendChild(node46);
                XmlNode node47 = xmlDoc.CreateElement("BZ");
                node47.InnerText = ToolUtil.GetString(Convert.FromBase64String(fp.bz));
                node29.AppendChild(node47);
                XmlNode node48 = xmlDoc.CreateElement("TSNSRSBH");
                node48.InnerText = "0";
                node29.AppendChild(node48);
                XmlNode node49 = xmlDoc.CreateElement("QYJBR");
                node49.InnerText = "0";
                node29.AppendChild(node49);
                XmlNode node50 = xmlDoc.CreateElement("KPLBDM");
                node50.InnerText = "01";
                node29.AppendChild(node50);
                XmlNode node51 = xmlDoc.CreateElement("SQLY");
                node51.InnerText = "0";
                node29.AppendChild(node51);
                XmlNode node52 = xmlDoc.CreateElement("FPZLDM");
                node52.InnerText = "20020";
                node29.AppendChild(node52);
                XmlNode node53 = xmlDoc.CreateElement("SKR");
                node53.InnerText = fp.skr;
                node29.AppendChild(node53);
                XmlNode node54 = xmlDoc.CreateElement("FHR");
                node54.InnerText = fp.fhr;
                node29.AppendChild(node54);
                XmlNode node55 = xmlDoc.CreateElement("DKR");
                node55.InnerText = fp.kpr;
                node29.AppendChild(node55);
                XmlNode node56 = xmlDoc.CreateElement("MSDK");
                node56.InnerText = "N";
                node29.AppendChild(node56);
                XmlNode node57 = xmlDoc.CreateElement("MSDKX");
                node57.InnerText = "0";
                node29.AppendChild(node57);
                XmlNode node58 = xmlDoc.CreateElement("PZZLDM");
                node58.InnerText = "0";
                node29.AppendChild(node58);
                XmlNode node59 = xmlDoc.CreateElement("ZBNRS");
                node29.AppendChild(node59);
                if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0))
                {
                    foreach (Dictionary<SPXX, string> dictionary3 in fp.Qdxx)
                    {
                        XmlNode node60 = this.CreateZBNode(fp, xmlDoc, dictionary3);
                        node59.AppendChild(node60);
                    }
                }
                else if ((fp.Mxxx != null) && (fp.Mxxx.Count > 0))
                {
                    foreach (Dictionary<SPXX, string> dictionary4 in fp.Mxxx)
                    {
                        XmlNode node61 = this.CreateZBNode(fp, xmlDoc, dictionary4);
                        node59.AppendChild(node61);
                    }
                }
                xmlDoc.Save(@"D:\swdkFp.xml");
            }
            catch (Exception exception)
            {
                this.log.Error("税务代开发票构造XML异常：" + exception.Message);
            }
            return xmlDoc;
        }

        private XmlNode CreateZBNode(Fpxx fp, XmlDocument xmlDoc, Dictionary<SPXX, string> sp)
        {
            XmlNode newChild = xmlDoc.CreateElement("HWMC");
            newChild.InnerText = sp[(SPXX)0];
            XmlElement element1 = xmlDoc.CreateElement("ZBNR");
            element1.AppendChild(newChild);
            XmlNode node2 = xmlDoc.CreateElement("GGXH");
            node2.InnerText = sp[(SPXX)3];
            element1.AppendChild(node2);
            XmlNode node3 = xmlDoc.CreateElement("JLDW");
            node3.InnerText = sp[(SPXX)4];
            element1.AppendChild(node3);
            XmlNode node4 = xmlDoc.CreateElement("SL");
            node4.InnerText = sp[(SPXX)6];
            element1.AppendChild(node4);
            XmlNode node5 = xmlDoc.CreateElement("BHSDJ");
            if (sp[(SPXX)11] == "1")
            {
                node5.InnerText = fp.Get_Print_Dj(sp, 1, null);
            }
            else
            {
                node5.InnerText = sp[(SPXX)5];
            }
            element1.AppendChild(node5);
            XmlNode node6 = xmlDoc.CreateElement("BHSJE");
            node6.InnerText = sp[(SPXX)7];
            element1.AppendChild(node6);
            XmlNode node7 = xmlDoc.CreateElement("SLV");
            node7.InnerText = sp[(SPXX)8];
            element1.AppendChild(node7);
            XmlNode node8 = xmlDoc.CreateElement("SE");
            node8.InnerText = sp[(SPXX)9];
            element1.AppendChild(node8);
            return element1;
        }

        private int PareFpType(FPLX fplx)
        {
            if (fplx != 0)
            {
                if (fplx == FPLX.PTFP)
                {
                    return 2;
                }
                return 0;
            }
            return 1;
        }
    }
}

