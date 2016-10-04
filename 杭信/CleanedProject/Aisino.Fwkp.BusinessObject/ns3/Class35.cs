namespace ns3
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    internal class Class35
    {
        public Class35()
        {
            
        }

        [DllImport("DecodeXMLFIle.dll", CallingConvention=CallingConvention.StdCall)]
        public static extern int EncodeFile(byte[] byte_0, byte[] byte_1, byte[] byte_2, byte[] byte_3);
        internal void method_0(Fpxx fpxx_0)
        {
            if (RegisterManager.CheckRegFile("JCST") || RegisterManager.CheckRegFile("JCJS"))
            {
                string str15 = PropertyUtil.GetValue("MAIN_PATH");
                string xfsh = fpxx_0.xfsh;
                string fpdm = fpxx_0.fpdm;
                string fphm = fpxx_0.fphm;
                string str19 = xfsh + "_" + fpdm + "_" + fphm + ".xml";
                string path = str15 + @"\OutPutFile\" + str19;
                string filename = str15 + @"\OutPutFile\JdcBZGSFile\" + str19;
                path.Remove(path.LastIndexOf(@"\"));
                string str = filename.Remove(filename.LastIndexOf(@"\"));
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
                double num = Convert.ToDouble(fpxx_0.je);
                double num2 = Convert.ToDouble(fpxx_0.se);
                string str7 = (num + num2).ToString("F2");
                string kprq = fpxx_0.kprq;
                string str3 = "";
                string str4 = "";
                if ((kprq != null) && (kprq.Length >= 0x13))
                {
                    str3 = kprq.Substring(0, 10);
                    str4 = kprq.Substring(11, 8);
                }
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(newChild);
                XmlElement element70 = document.CreateElement("business");
                document.AppendChild(element70);
                XmlElement element = document.CreateElement("body");
                element70.AppendChild(element);
                XmlElement element71 = document.CreateElement("fplx");
                element71.InnerText = "2";
                element.AppendChild(element71);
                XmlElement element72 = document.CreateElement("fpdm");
                element72.InnerText = fpxx_0.fpdm;
                element.AppendChild(element72);
                XmlElement element73 = document.CreateElement("fphm");
                element73.InnerText = fpxx_0.fphm;
                element.AppendChild(element73);
                XmlElement element2 = document.CreateElement("zfbz");
                element2.InnerText = fpxx_0.zfbz ? "1" : "0";
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("kprq");
                element3.InnerText = str3;
                element.AppendChild(element3);
                XmlElement element4 = document.CreateElement("ghdw");
                element4.InnerText = fpxx_0.gfmc;
                element.AppendChild(element4);
                XmlElement element5 = document.CreateElement("gfsbh");
                element5.InnerText = fpxx_0.gfsh;
                element.AppendChild(element5);
                XmlElement element6 = document.CreateElement("sfzhm");
                element6.InnerText = fpxx_0.sfzhm;
                element.AppendChild(element6);
                XmlElement element7 = document.CreateElement("scqymc");
                element7.InnerText = fpxx_0.sccjmc;
                element.AppendChild(element7);
                XmlElement element8 = document.CreateElement("cllx");
                element8.InnerText = fpxx_0.cllx;
                element.AppendChild(element8);
                XmlElement element9 = document.CreateElement("cpxh");
                element9.InnerText = fpxx_0.cpxh;
                element.AppendChild(element9);
                XmlElement element10 = document.CreateElement("cd");
                element10.InnerText = fpxx_0.cd;
                element.AppendChild(element10);
                XmlElement element11 = document.CreateElement("hgzh");
                element11.InnerText = fpxx_0.hgzh;
                element.AppendChild(element11);
                XmlElement element12 = document.CreateElement("jkzmsh");
                element12.InnerText = fpxx_0.jkzmsh;
                element.AppendChild(element12);
                XmlElement element13 = document.CreateElement("sjdh");
                element13.InnerText = fpxx_0.sjdh;
                element.AppendChild(element13);
                XmlElement element14 = document.CreateElement("fdjhm");
                element14.InnerText = fpxx_0.fdjhm;
                element.AppendChild(element14);
                XmlElement element15 = document.CreateElement("cjhm");
                element15.InnerText = fpxx_0.clsbdh;
                element.AppendChild(element15);
                XmlElement element16 = document.CreateElement("je");
                element16.InnerText = fpxx_0.je;
                element.AppendChild(element16);
                XmlElement element17 = document.CreateElement("sl");
                element17.InnerText = fpxx_0.sLv;
                element.AppendChild(element17);
                XmlElement element18 = document.CreateElement("se");
                element18.InnerText = fpxx_0.se;
                element.AppendChild(element18);
                XmlElement element19 = document.CreateElement("jshj");
                element19.InnerText = str7;
                element.AppendChild(element19);
                XmlElement element20 = document.CreateElement("xfmc");
                element20.InnerText = fpxx_0.xfmc;
                element.AppendChild(element20);
                XmlElement element21 = document.CreateElement("xfsh");
                element21.InnerText = fpxx_0.xfsh;
                element.AppendChild(element21);
                XmlElement element22 = document.CreateElement("xfdz");
                element22.InnerText = fpxx_0.xfdz;
                element.AppendChild(element22);
                XmlElement element23 = document.CreateElement("xfdh");
                element23.InnerText = fpxx_0.xfdh;
                element.AppendChild(element23);
                XmlElement element24 = document.CreateElement("jxsswjgdm");
                element24.InnerText = fpxx_0.zgswjgdm;
                element.AppendChild(element24);
                XmlElement element25 = document.CreateElement("dw");
                if ((fpxx_0.dw != null) && (fpxx_0.dw.Length > 0))
                {
                    element25.InnerText = fpxx_0.dw;
                }
                element.AppendChild(element25);
                if (RegisterManager.CheckRegFile("JCJS"))
                {
                    string str5 = "";
                    byte[] bytes = ToolUtil.GetBytes(document.InnerXml.ToString());
                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        str5 = Convert.ToBase64String(bytes);
                    }
                    string[] contents = new string[] { str5 };
                    File.WriteAllLines(path, contents, Encoding.Unicode);
                }
                string zfsj = fpxx_0.zfsj;
                string str9 = "";
                string str10 = "";
                if ((zfsj != null) && (zfsj.Length >= 0x13))
                {
                    str9 = zfsj.Substring(0, 10);
                    str10 = zfsj.Substring(11, 8);
                }
                string str8 = "0";
                if (fpxx_0.zfbz)
                {
                    str8 = "1";
                }
                else if (num < 0.0)
                {
                    str8 = "2";
                }
                XmlDocument document2 = new XmlDocument();
                XmlDeclaration declaration = document2.CreateXmlDeclaration("1.0", "GBK", null);
                document2.AppendChild(declaration);
                XmlElement element26 = document2.CreateElement("taxML");
                document2.AppendChild(element26);
                XmlElement element27 = document2.CreateElement("jdcxsfpMxxx");
                element26.AppendChild(element27);
                XmlElement element28 = document2.CreateElement("fpdm");
                element28.InnerText = fpxx_0.fpdm;
                element27.AppendChild(element28);
                XmlElement element29 = document2.CreateElement("fphm");
                element29.InnerText = fpxx_0.fphm;
                element27.AppendChild(element29);
                XmlElement element30 = document2.CreateElement("jqbh");
                element30.InnerText = fpxx_0.jqbh;
                element27.AppendChild(element30);
                XmlElement element31 = document2.CreateElement("kprq");
                element31.InnerText = str3;
                element27.AppendChild(element31);
                XmlElement element32 = document2.CreateElement("kpsj");
                element32.InnerText = str4;
                element27.AppendChild(element32);
                XmlElement element33 = document2.CreateElement("skm");
                element33.InnerText = "";
                element27.AppendChild(element33);
                XmlElement element34 = document2.CreateElement("ghdw");
                element34.InnerText = fpxx_0.gfmc;
                element27.AppendChild(element34);
                XmlElement element35 = document2.CreateElement("ghdwdm");
                element35.InnerText = fpxx_0.gfsh;
                element27.AppendChild(element35);
                XmlElement element36 = document2.CreateElement("sfzhm");
                element36.InnerText = fpxx_0.sfzhm;
                element27.AppendChild(element36);
                XmlElement element37 = document2.CreateElement("scqymc");
                element37.InnerText = fpxx_0.sccjmc;
                element27.AppendChild(element37);
                XmlElement element38 = document2.CreateElement("cllx");
                element38.InnerText = fpxx_0.cllx;
                element27.AppendChild(element38);
                XmlElement element39 = document2.CreateElement("cpxh");
                element39.InnerText = fpxx_0.cpxh;
                element27.AppendChild(element39);
                XmlElement element40 = document2.CreateElement("cd");
                element40.InnerText = fpxx_0.cd;
                element27.AppendChild(element40);
                XmlElement element41 = document2.CreateElement("hgzs");
                element41.InnerText = fpxx_0.hgzh;
                element27.AppendChild(element41);
                XmlElement element42 = document2.CreateElement("jkzmsh");
                element42.InnerText = fpxx_0.jkzmsh;
                element27.AppendChild(element42);
                XmlElement element43 = document2.CreateElement("sjdh");
                element43.InnerText = fpxx_0.sjdh;
                element27.AppendChild(element43);
                XmlElement element44 = document2.CreateElement("fdjhm");
                element44.InnerText = fpxx_0.fdjhm;
                element27.AppendChild(element44);
                XmlElement element45 = document2.CreateElement("clsbdh");
                element45.InnerText = fpxx_0.clsbdh;
                element27.AppendChild(element45);
                XmlElement element46 = document2.CreateElement("jshj");
                element46.InnerText = str7;
                element27.AppendChild(element46);
                XmlElement element47 = document2.CreateElement("xhdwmc");
                element47.InnerText = fpxx_0.xfmc;
                element27.AppendChild(element47);
                XmlElement element48 = document2.CreateElement("nsrsbh");
                element48.InnerText = fpxx_0.xfsh;
                element27.AppendChild(element48);
                XmlElement element49 = document2.CreateElement("dh");
                element49.InnerText = fpxx_0.xfdh;
                element27.AppendChild(element49);
                XmlElement element50 = document2.CreateElement("zh");
                element50.InnerText = fpxx_0.xfzh;
                element27.AppendChild(element50);
                XmlElement element51 = document2.CreateElement("dz");
                element51.InnerText = fpxx_0.xfdz;
                element27.AppendChild(element51);
                XmlElement element52 = document2.CreateElement("khyh");
                element52.InnerText = fpxx_0.xfyh;
                element27.AppendChild(element52);
                XmlElement element53 = document2.CreateElement("zzssl");
                element53.InnerText = fpxx_0.sLv;
                element27.AppendChild(element53);
                XmlElement element54 = document2.CreateElement("zzsse");
                element54.InnerText = fpxx_0.se;
                element27.AppendChild(element54);
                XmlElement element55 = document2.CreateElement("dkdw_mc");
                element55.InnerText = "";
                element27.AppendChild(element55);
                XmlElement element56 = document2.CreateElement("dkdw_dm");
                element56.InnerText = "";
                element27.AppendChild(element56);
                XmlElement element57 = document2.CreateElement("zgswjg_mc");
                element57.InnerText = fpxx_0.zgswjgmc;
                element27.AppendChild(element57);
                XmlElement element58 = document2.CreateElement("zgswjg_dm");
                element58.InnerText = fpxx_0.zgswjgdm;
                element27.AppendChild(element58);
                XmlElement element59 = document2.CreateElement("bhsj");
                element59.InnerText = fpxx_0.je;
                element27.AppendChild(element59);
                XmlElement element60 = document2.CreateElement("dw");
                element60.InnerText = fpxx_0.dw;
                element27.AppendChild(element60);
                XmlElement element61 = document2.CreateElement("xcrs");
                element61.InnerText = fpxx_0.xcrs;
                element27.AppendChild(element61);
                XmlElement element62 = document2.CreateElement("bz");
                element62.InnerText = "一车一票";
                element27.AppendChild(element62);
                XmlElement element63 = document2.CreateElement("kpr");
                element63.InnerText = fpxx_0.kpr;
                element27.AppendChild(element63);
                XmlElement element64 = document2.CreateElement("kplx");
                element64.InnerText = "1";
                element27.AppendChild(element64);
                XmlElement element65 = document2.CreateElement("dkbz");
                element65.InnerText = "0";
                element27.AppendChild(element65);
                XmlElement element66 = document2.CreateElement("fpbz");
                element66.InnerText = str8;
                element27.AppendChild(element66);
                XmlElement element67 = document2.CreateElement("zfrq");
                element67.InnerText = str9;
                element27.AppendChild(element67);
                XmlElement element68 = document2.CreateElement("zfsj");
                element68.InnerText = str10;
                element27.AppendChild(element68);
                XmlElement element69 = document2.CreateElement("zfr");
                element69.InnerText = fpxx_0.zfbz ? UserInfo.Yhmc : "";
                element27.AppendChild(element69);
                XmlElement element74 = document2.CreateElement("tpdm");
                element74.InnerText = fpxx_0.blueFpdm;
                element27.AppendChild(element74);
                XmlElement element75 = document2.CreateElement("tphm");
                element75.InnerText = fpxx_0.blueFphm;
                element27.AppendChild(element75);
                XmlElement element76 = document2.CreateElement("lgrq");
                element76.InnerText = "";
                element27.AppendChild(element76);
                if (RegisterManager.CheckRegFile("JCST"))
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    byte[] buffer2 = ToolUtil.GetBytes(fpxx_0.xfsh + card.Machine.ToString());
                    document2.Save(filename);
                    byte[] buffer3 = ToolUtil.GetBytes(filename);
                    byte[] buffer4 = new byte[0x400];
                    byte[] buffer5 = new byte[100];
                    EncodeFile(buffer3, buffer2, buffer4, buffer5);
                    File.Delete(filename);
                }
            }
        }

        internal void method_1(Fpxx fpxx_0)
        {
            if (RegisterManager.CheckRegFile("JCST") || RegisterManager.CheckRegFile("JCJS"))
            {
                string str12 = PropertyUtil.GetValue("MAIN_PATH");
                string xfsh = fpxx_0.xfsh;
                string fpdm = fpxx_0.fpdm;
                string fphm = fpxx_0.fphm;
                string str16 = xfsh + "_" + fpdm + "_" + fphm + ".xml";
                string path = str12 + @"\OutPutFile\" + str16;
                string filename = str12 + @"\OutPutFile\JdcBZGSFile\" + str16;
                path.Remove(path.LastIndexOf(@"\"));
                string str10 = filename.Remove(filename.LastIndexOf(@"\"));
                if (!Directory.Exists(str10))
                {
                    Directory.CreateDirectory(str10);
                }
                double num = Convert.ToDouble(fpxx_0.je);
                double num2 = Convert.ToDouble(fpxx_0.se);
                string str3 = (num + num2).ToString("F2");
                string kprq = fpxx_0.kprq;
                string str4 = "";
                string str5 = "";
                if ((kprq != null) && (kprq.Length >= 0x13))
                {
                    str4 = kprq.Substring(0, 10);
                    str5 = kprq.Substring(11, 8);
                }
                XmlDocument document2 = new XmlDocument();
                XmlDeclaration newChild = document2.CreateXmlDeclaration("1.0", "GBK", null);
                document2.AppendChild(newChild);
                XmlElement element50 = document2.CreateElement("business");
                document2.AppendChild(element50);
                XmlElement element2 = document2.CreateElement("body");
                element50.AppendChild(element2);
                XmlElement element51 = document2.CreateElement("fplx");
                element51.InnerText = "1";
                element2.AppendChild(element51);
                XmlElement element52 = document2.CreateElement("fpdm");
                element52.InnerText = fpxx_0.fpdm;
                element2.AppendChild(element52);
                XmlElement element53 = document2.CreateElement("fphm");
                element53.InnerText = fpxx_0.fphm;
                element2.AppendChild(element53);
                XmlElement element54 = document2.CreateElement("zfbz");
                element54.InnerText = fpxx_0.zfbz ? "1" : "0";
                element2.AppendChild(element54);
                XmlElement element55 = document2.CreateElement("kprq");
                element55.InnerText = str4;
                element2.AppendChild(element55);
                XmlElement element56 = document2.CreateElement("ghdw");
                element56.InnerText = fpxx_0.gfmc;
                element2.AppendChild(element56);
                XmlElement element57 = document2.CreateElement("gfsbh");
                element57.InnerText = "";
                element2.AppendChild(element57);
                XmlElement element58 = document2.CreateElement("sfzhm");
                element58.InnerText = fpxx_0.sfzhm;
                element2.AppendChild(element58);
                XmlElement element59 = document2.CreateElement("scqymc");
                element59.InnerText = fpxx_0.sccjmc;
                element2.AppendChild(element59);
                XmlElement element60 = document2.CreateElement("cllx");
                element60.InnerText = fpxx_0.cllx;
                element2.AppendChild(element60);
                XmlElement element61 = document2.CreateElement("cpxh");
                element61.InnerText = fpxx_0.cpxh;
                element2.AppendChild(element61);
                XmlElement element62 = document2.CreateElement("cd");
                element62.InnerText = fpxx_0.cd;
                element2.AppendChild(element62);
                XmlElement element63 = document2.CreateElement("hgzh");
                element63.InnerText = fpxx_0.hgzh;
                element2.AppendChild(element63);
                XmlElement element64 = document2.CreateElement("jkzmsh");
                element64.InnerText = fpxx_0.jkzmsh;
                element2.AppendChild(element64);
                XmlElement element65 = document2.CreateElement("sjdh");
                element65.InnerText = fpxx_0.sjdh;
                element2.AppendChild(element65);
                XmlElement element66 = document2.CreateElement("fdjhm");
                element66.InnerText = fpxx_0.fdjhm;
                element2.AppendChild(element66);
                XmlElement element67 = document2.CreateElement("cjhm");
                element67.InnerText = fpxx_0.clsbdh;
                element2.AppendChild(element67);
                XmlElement element68 = document2.CreateElement("je");
                element68.InnerText = fpxx_0.je;
                element2.AppendChild(element68);
                XmlElement element69 = document2.CreateElement("sl");
                element69.InnerText = fpxx_0.sLv;
                element2.AppendChild(element69);
                XmlElement element70 = document2.CreateElement("se");
                element70.InnerText = fpxx_0.se;
                element2.AppendChild(element70);
                XmlElement element71 = document2.CreateElement("jshj");
                element71.InnerText = str3;
                element2.AppendChild(element71);
                XmlElement element72 = document2.CreateElement("xfmc");
                element72.InnerText = fpxx_0.xfmc;
                element2.AppendChild(element72);
                XmlElement element73 = document2.CreateElement("xfsh");
                element73.InnerText = fpxx_0.xfsh;
                element2.AppendChild(element73);
                XmlElement element74 = document2.CreateElement("xfdz");
                element74.InnerText = fpxx_0.xfdz;
                element2.AppendChild(element74);
                XmlElement element75 = document2.CreateElement("xfdh");
                element75.InnerText = fpxx_0.xfdh;
                element2.AppendChild(element75);
                XmlElement element76 = document2.CreateElement("jxsswjgdm");
                element76.InnerText = fpxx_0.zgswjgdm;
                element2.AppendChild(element76);
                XmlElement element = document2.CreateElement("dw");
                if ((fpxx_0.dw != null) && (fpxx_0.dw.Length > 0))
                {
                    element.InnerText = fpxx_0.dw;
                }
                element2.AppendChild(element);
                if (RegisterManager.CheckRegFile("JCJS"))
                {
                    string str9 = "";
                    byte[] bytes = ToolUtil.GetBytes(document2.InnerXml.ToString());
                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        str9 = Convert.ToBase64String(bytes);
                    }
                    string[] contents = new string[] { str9 };
                    File.WriteAllLines(path, contents, Encoding.Unicode);
                }
                string zfsj = fpxx_0.zfsj;
                string str17 = "";
                string str18 = "";
                if ((zfsj != null) && (zfsj.Length >= 0x13))
                {
                    str17 = zfsj.Substring(0, 10);
                    str18 = zfsj.Substring(11, 8);
                }
                string str8 = "0";
                if (fpxx_0.zfbz)
                {
                    str8 = "1";
                }
                else if (num < 0.0)
                {
                    str8 = "2";
                }
                XmlDocument document = new XmlDocument();
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(declaration);
                XmlElement element8 = document.CreateElement("taxML");
                document.AppendChild(element8);
                XmlElement element3 = document.CreateElement("jdcxsfpMxxx");
                element8.AppendChild(element3);
                XmlElement element9 = document.CreateElement("fpdm");
                element9.InnerText = fpxx_0.fpdm;
                element3.AppendChild(element9);
                XmlElement element10 = document.CreateElement("fphm");
                element10.InnerText = fpxx_0.fphm;
                element3.AppendChild(element10);
                XmlElement element11 = document.CreateElement("jqbh");
                element11.InnerText = fpxx_0.jqbh;
                element3.AppendChild(element11);
                XmlElement element12 = document.CreateElement("kprq");
                element12.InnerText = str4;
                element3.AppendChild(element12);
                XmlElement element13 = document.CreateElement("kpsj");
                element13.InnerText = str5;
                element3.AppendChild(element13);
                XmlElement element14 = document.CreateElement("skm");
                element14.InnerText = "";
                element3.AppendChild(element14);
                XmlElement element15 = document.CreateElement("ghdw");
                element15.InnerText = fpxx_0.gfmc;
                element3.AppendChild(element15);
                XmlElement element16 = document.CreateElement("ghdwdm");
                element16.InnerText = "";
                element3.AppendChild(element16);
                XmlElement element17 = document.CreateElement("sfzhm");
                element17.InnerText = fpxx_0.sfzhm;
                element3.AppendChild(element17);
                XmlElement element18 = document.CreateElement("scqymc");
                element18.InnerText = fpxx_0.sccjmc;
                element3.AppendChild(element18);
                XmlElement element19 = document.CreateElement("cllx");
                element19.InnerText = fpxx_0.cllx;
                element3.AppendChild(element19);
                XmlElement element20 = document.CreateElement("cpxh");
                element20.InnerText = fpxx_0.cpxh;
                element3.AppendChild(element20);
                XmlElement element21 = document.CreateElement("cd");
                element21.InnerText = fpxx_0.cd;
                element3.AppendChild(element21);
                XmlElement element22 = document.CreateElement("hgzs");
                element22.InnerText = fpxx_0.hgzh;
                element3.AppendChild(element22);
                XmlElement element23 = document.CreateElement("jkzmsh");
                element23.InnerText = fpxx_0.jkzmsh;
                element3.AppendChild(element23);
                XmlElement element24 = document.CreateElement("sjdh");
                element24.InnerText = fpxx_0.sjdh;
                element3.AppendChild(element24);
                XmlElement element25 = document.CreateElement("fdjhm");
                element25.InnerText = fpxx_0.fdjhm;
                element3.AppendChild(element25);
                XmlElement element26 = document.CreateElement("clsbdh");
                element26.InnerText = fpxx_0.clsbdh;
                element3.AppendChild(element26);
                XmlElement element27 = document.CreateElement("jshj");
                element27.InnerText = str3;
                element3.AppendChild(element27);
                XmlElement element28 = document.CreateElement("xhdwmc");
                element28.InnerText = fpxx_0.xfmc;
                element3.AppendChild(element28);
                XmlElement element29 = document.CreateElement("nsrsbh");
                element29.InnerText = fpxx_0.xfsh;
                element3.AppendChild(element29);
                XmlElement element30 = document.CreateElement("dh");
                element30.InnerText = fpxx_0.xfdh;
                element3.AppendChild(element30);
                XmlElement element31 = document.CreateElement("zh");
                element31.InnerText = fpxx_0.xfzh;
                element3.AppendChild(element31);
                XmlElement element32 = document.CreateElement("dz");
                element32.InnerText = fpxx_0.xfdz;
                element3.AppendChild(element32);
                XmlElement element33 = document.CreateElement("khyh");
                element33.InnerText = fpxx_0.xfyh;
                element3.AppendChild(element33);
                XmlElement element34 = document.CreateElement("zzssl");
                element34.InnerText = fpxx_0.sLv;
                element3.AppendChild(element34);
                XmlElement element35 = document.CreateElement("zzsse");
                element35.InnerText = fpxx_0.se;
                element3.AppendChild(element35);
                XmlElement element36 = document.CreateElement("dkdw_mc");
                element36.InnerText = "";
                element3.AppendChild(element36);
                XmlElement element37 = document.CreateElement("dkdw_dm");
                element37.InnerText = "";
                element3.AppendChild(element37);
                XmlElement element38 = document.CreateElement("zgswjg_mc");
                element38.InnerText = fpxx_0.zgswjgmc;
                element3.AppendChild(element38);
                XmlElement element39 = document.CreateElement("zgswjg_dm");
                element39.InnerText = fpxx_0.zgswjgdm;
                element3.AppendChild(element39);
                XmlElement element40 = document.CreateElement("bhsj");
                element40.InnerText = fpxx_0.je;
                element3.AppendChild(element40);
                XmlElement element41 = document.CreateElement("dw");
                element41.InnerText = fpxx_0.dw;
                element3.AppendChild(element41);
                XmlElement element42 = document.CreateElement("xcrs");
                element42.InnerText = fpxx_0.xcrs;
                element3.AppendChild(element42);
                XmlElement element43 = document.CreateElement("bz");
                element43.InnerText = "一车一票";
                element3.AppendChild(element43);
                XmlElement element44 = document.CreateElement("kpr");
                element44.InnerText = fpxx_0.kpr;
                element3.AppendChild(element44);
                XmlElement element45 = document.CreateElement("kplx");
                element45.InnerText = "1";
                element3.AppendChild(element45);
                XmlElement element46 = document.CreateElement("dkbz");
                element46.InnerText = "0";
                element3.AppendChild(element46);
                XmlElement element47 = document.CreateElement("fpbz");
                element47.InnerText = str8;
                element3.AppendChild(element47);
                XmlElement element48 = document.CreateElement("zfrq");
                element48.InnerText = str17;
                element3.AppendChild(element48);
                XmlElement element49 = document.CreateElement("zfsj");
                element49.InnerText = str18;
                element3.AppendChild(element49);
                XmlElement element4 = document.CreateElement("zfr");
                element4.InnerText = fpxx_0.zfbz ? UserInfo.Yhmc : "";
                element3.AppendChild(element4);
                XmlElement element5 = document.CreateElement("tpdm");
                element5.InnerText = fpxx_0.blueFpdm;
                element3.AppendChild(element5);
                XmlElement element6 = document.CreateElement("tphm");
                element6.InnerText = fpxx_0.blueFphm;
                element3.AppendChild(element6);
                XmlElement element7 = document.CreateElement("lgrq");
                element7.InnerText = "";
                element3.AppendChild(element7);
                if (RegisterManager.CheckRegFile("JCST"))
                {
                    TaxCard card = TaxCardFactory.CreateTaxCard();
                    byte[] buffer2 = ToolUtil.GetBytes(fpxx_0.xfsh + card.Machine.ToString());
                    document.Save(filename);
                    byte[] buffer3 = ToolUtil.GetBytes(filename);
                    byte[] buffer4 = new byte[0x400];
                    byte[] buffer5 = new byte[100];
                    EncodeFile(buffer3, buffer2, buffer4, buffer5);
                    File.Delete(filename);
                }
            }
        }
    }
}

