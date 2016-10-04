namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.Form.DKFP;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    internal class XxfpOutUtils
    {
        private int MAX_FPXX = 0x1388;

        public XmlDocument genCRCXml(string fileName, string crcValue)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("CRC");
            System.Xml.XmlNode node2 = document.CreateElement("FILE");
            System.Xml.XmlAttribute attribute = document.CreateAttribute("NAME");
            System.Xml.XmlAttribute attribute2 = document.CreateAttribute("CRCVALUE");
            document.AppendChild(node);
            node.AppendChild(node2);
            node2.Attributes.Append(attribute);
            node2.Attributes.Append(attribute2);
            attribute.InnerText = fileName;
            attribute2.InnerText = crcValue;
            return document;
        }

        private XmlDocument genPackageFpxxXml(List<Fpxx> xxfps, SelectSSQ selectSsq, TaxCard TaxCardInstance, int start, int end)
        {
            XmlDocument document = new XmlDocument();
            document.CreateXmlDeclaration("1.0", "GBK", "yes");
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.AppendChild(newChild);
            System.Xml.XmlNode node = document.CreateElement("taxML");
            System.Xml.XmlAttribute attribute = document.CreateAttribute("cnName");
            attribute.InnerText = "增值税发票开具明细";
            node.Attributes.Append(attribute);
            System.Xml.XmlAttribute attribute2 = document.CreateAttribute("xmlns");
            attribute2.InnerText = "http://www.chinatax.gov.cn/dataspec/";
            node.Attributes.Append(attribute2);
            System.Xml.XmlAttribute attribute3 = document.CreateAttribute("name");
            attribute3.InnerText = "slSbbtjZzsfpkjmxRequest";
            node.Attributes.Append(attribute3);
            System.Xml.XmlAttribute attribute4 = document.CreateAttribute("version");
            attribute4.InnerText = "SW5001-2006";
            node.Attributes.Append(attribute4);
            System.Xml.XmlAttribute attribute5 = document.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
            attribute5.InnerText = "slSbbtjZzsfpkjmxRequest";
            node.Attributes.Append(attribute5);
            document.AppendChild(node);
            System.Xml.XmlNode node2 = document.CreateElement("sbbZzsfpkjmx");
            node.AppendChild(node2);
            System.Xml.XmlNode node3 = document.CreateElement("head");
            node2.AppendChild(node3);
            System.Xml.XmlNode node4 = document.CreateElement("publicHead");
            node3.AppendChild(node4);
            System.Xml.XmlNode node5 = document.CreateElement("nsrsbh");
            System.Xml.XmlNode node6 = document.CreateElement("nsrmc");
            node5.InnerText = TaxCardInstance.TaxCode;
            node6.InnerText = TaxCardInstance.Corporation;
            System.Xml.XmlNode node7 = document.CreateElement("tbrq");
            node7.InnerText = selectSsq.tbrq;
            System.Xml.XmlNode node8 = document.CreateElement("sssq");
            node4.AppendChild(node5);
            node4.AppendChild(node6);
            node4.AppendChild(node7);
            node4.AppendChild(node8);
            System.Xml.XmlNode node9 = document.CreateElement("rqQ");
            System.Xml.XmlNode node10 = document.CreateElement("rqZ");
            node9.InnerText = selectSsq.ssqQ.Replace("-", "");
            node10.InnerText = selectSsq.ssqZ.Replace("-", "");
            node8.AppendChild(node9);
            node8.AppendChild(node10);
            System.Xml.XmlNode node11 = document.CreateElement("body");
            node2.AppendChild(node11);
            System.Xml.XmlNode node13 = document.CreateElement("zyfpkjhjxx");
            System.Xml.XmlNode node12 = document.CreateElement("zyfpkjmx");
            node11.AppendChild(node12);
            System.Xml.XmlNode node14 = document.CreateElement("zyfpkjhjs");
            System.Xml.XmlNode node15 = document.CreateElement("zzszyfphjJe");
            System.Xml.XmlNode node16 = document.CreateElement("zzszyfphjSe");
            node13.AppendChild(node14);
            node13.AppendChild(node15);
            node13.AppendChild(node16);
            System.Xml.XmlNode node17 = document.CreateElement("ptfpkjmx");
            System.Xml.XmlNode node18 = document.CreateElement("ptfpkjhjxx");
            System.Xml.XmlNode node19 = document.CreateElement("ptfpkjhjs");
            System.Xml.XmlNode node20 = document.CreateElement("ptfpkjhjJe");
            System.Xml.XmlNode node21 = document.CreateElement("ptfpkjhjSe");
            node18.AppendChild(node19);
            node18.AppendChild(node20);
            node18.AppendChild(node21);
            node11.AppendChild(node12);
            node11.AppendChild(node13);
            node11.AppendChild(node17);
            node11.AppendChild(node18);
            int num = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            int num4 = 0;
            decimal num5 = 0M;
            decimal num6 = 0M;
            for (int i = start; i <= end; i++)
            {
                Fpxx fpxx = xxfps[i];
                System.Xml.XmlNode node22 = document.CreateElement("mxxx");
                System.Xml.XmlNode node23 = document.CreateElement("xh");
                System.Xml.XmlNode node24 = document.CreateElement("fpdm");
                System.Xml.XmlNode node25 = document.CreateElement("fphm");
                System.Xml.XmlNode node26 = document.CreateElement("kprq");
                System.Xml.XmlNode node27 = document.CreateElement("gmfnsrsbh");
                System.Xml.XmlNode node28 = document.CreateElement("je");
                System.Xml.XmlNode node29 = document.CreateElement("se");
                System.Xml.XmlNode node30 = document.CreateElement("zfbz");
                node24.InnerText = fpxx.fpdm;
                node25.InnerText = ShareMethods.FPHMTo8Wei(fpxx.fphm.ToString());
                node27.InnerText = fpxx.gfsh;
                node28.InnerText = fpxx.je.ToString();
                node29.InnerText = fpxx.se.ToString();
                if (fpxx.zfbz)
                {
                    node30.InnerText = "Y";
                }
                else
                {
                    node30.InnerText = "N";
                }
                node27.InnerText = fpxx.gfsh;
                node26.InnerText = fpxx.kprq.Replace("-", "");
                node22.AppendChild(node23);
                node22.AppendChild(node24);
                node22.AppendChild(node25);
                node22.AppendChild(node26);
                node22.AppendChild(node27);
                node22.AppendChild(node28);
                node22.AppendChild(node29);
                node22.AppendChild(node30);
                if (fpxx.fplx == 0)
                {
                    num++;
                    node23.InnerText = num.ToString();
                    num2 += decimal.Parse(fpxx.je);
                    num3 += decimal.Parse(fpxx.se);
                    node12.AppendChild(node22);
                }
                else if (fpxx.fplx == (FPLX)2)
                {
                    num4++;
                    node23.InnerText = num4.ToString();
                    num5 += decimal.Parse(fpxx.je);
                    num6 += decimal.Parse(fpxx.se);
                    node17.AppendChild(node22);
                }
                node22.AppendChild(node23);
            }
            node14.InnerText = num.ToString();
            node15.InnerText = num2.ToString();
            node16.InnerText = num3.ToString();
            node19.InnerText = num4.ToString();
            node20.InnerText = num5.ToString();
            node21.InnerText = num6.ToString();
            return document;
        }

        public bool genXxfpInfo(List<Fpxx> xxfps, SelectSSQ selectSsq, TaxCard TaxCardInstance, string filePath)
        {
            XmlDocument document = new XmlDocument();
            XmlDocument document2 = new XmlDocument();
            string str = selectSsq.ssqQ.Replace("-", "");
            string str2 = selectSsq.ssqZ.Replace("-", "");
            int num = xxfps.Count / this.MAX_FPXX;
            int num2 = 0;
            if ((xxfps.Count % this.MAX_FPXX) != 0)
            {
                num++;
            }
            string fileName = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            int start = 0;
            int end = 0;
            string crcValue = "";
            for (int i = 1; i <= num; i++)
            {
                num2 = i;
                str4 = string.Concat(new object[] { "taxML_ZZSFPKJMX_", num, "_", num2, "_", str, "_", str2, "_", TaxCardInstance.TaxCode });
                str6 = string.Concat(new object[] { @"\taxML_ZZSFPKJMX_", num, "_", num2, "_", str, "_", str2, "_", TaxCardInstance.TaxCode });
                fileName = str4 + "_V10.xml";
                str5 = str4 + "_CRC.xml";
                if (!Directory.Exists(filePath + str6))
                {
                    Directory.CreateDirectory(filePath + str6);
                }
                start = (i - 1) * this.MAX_FPXX;
                end = (i * this.MAX_FPXX) - 1;
                if ((xxfps.Count - (this.MAX_FPXX * i)) < 0)
                {
                    end = xxfps.Count - 1;
                }
                try
                {
                    this.genPackageFpxxXml(xxfps, selectSsq, TaxCardInstance, start, end).Save(filePath + str6 + @"\" + fileName);
                    crcValue = Convert.ToString((long) Crc32.GetFileCRC32(filePath + str6 + @"\" + fileName), 0x10).ToUpper();
                    this.genCRCXml(fileName, crcValue).Save(filePath + str6 + @"\" + str5);
                    ZipUtil.Zip(filePath + str6, filePath + str6 + ".zip", "");
                    if (Directory.Exists(filePath + str6))
                    {
                        Directory.Delete(filePath + str6, true);
                    }
                }
                catch (Exception)
                {
                    if (Directory.Exists(filePath + str6))
                    {
                        Directory.Delete(filePath + str6, true);
                    }
                    MessageManager.ShowMsgBox("DKFPXZ-0014", "错误");
                    return false;
                }
            }
            return true;
        }
    }
}

