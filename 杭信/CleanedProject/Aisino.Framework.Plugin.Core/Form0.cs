using Aisino.Framework.Plugin.Core;
using Aisino.Framework.Plugin.Core.Controls;
using Aisino.Framework.Plugin.Core.MessageDlg.Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

internal class Form0 : DockForm
{
    private string string_0;

    public Form0()
    {
        
        this.string_0 = string.Empty;
    }

    public void method_3(string string_1)
    {
        this.string_0 = string_1.Trim();
    }

    public string method_4()
    {
        return this.string_0.Trim();
    }

    public string method_5(Fwsk_ErrInfo fwsk_ErrInfo_0)
    {
        try
        {
            XmlNode node16;
            XmlDocument document = new XmlDocument();
            XmlNode newChild = document.CreateElement("Fwsk_ErrInfo");
            XmlNode node2 = document.CreateElement("OpSysInfo");
            XmlNode node3 = document.CreateElement("OpSystem");
            node3.InnerText = fwsk_ErrInfo_0.OpSystem.Trim();
            node2.AppendChild(node3);
            XmlNode node4 = document.CreateElement("CpuInfo");
            node4.InnerText = fwsk_ErrInfo_0.Cpu.Trim();
            node2.AppendChild(node4);
            XmlNode node5 = document.CreateElement("TotalMem");
            node5.InnerText = fwsk_ErrInfo_0.TotalMem.Trim();
            node2.AppendChild(node5);
            XmlNode node6 = document.CreateElement("VaildMem");
            node6.InnerText = fwsk_ErrInfo_0.ValidMem.Trim();
            node2.AppendChild(node6);
            newChild.AppendChild(node2);
            XmlNode node7 = document.CreateElement("SoftInfo");
            XmlNode node8 = document.CreateElement("VerName");
            node8.InnerText = fwsk_ErrInfo_0.VerName.Trim();
            node7.AppendChild(node8);
            XmlNode node9 = document.CreateElement("RegVer");
            node9.InnerText = fwsk_ErrInfo_0.RegVer.Trim();
            node7.AppendChild(node9);
            XmlNode node10 = document.CreateElement("RegZone");
            node10.InnerText = fwsk_ErrInfo_0.RegZone.Trim();
            node7.AppendChild(node10);
            XmlNode node11 = document.CreateElement("RegSerial");
            node11.InnerText = fwsk_ErrInfo_0.RegSerial.Trim();
            node7.AppendChild(node11);
            XmlNode node12 = document.CreateElement("RegCode");
            node12.InnerText = fwsk_ErrInfo_0.RegCode.Trim();
            node7.AppendChild(node12);
            XmlNode node13 = document.CreateElement("TaxCode");
            node13.InnerText = fwsk_ErrInfo_0.TaxCode.Trim();
            node7.AppendChild(node13);
            XmlNode node14 = document.CreateElement("CorpName");
            node14.InnerText = fwsk_ErrInfo_0.CorpName.Trim();
            node7.AppendChild(node14);
            XmlNode node17 = document.CreateElement("CorpAddr");
            node17.InnerText = fwsk_ErrInfo_0.CorpAddress.Trim();
            node7.AppendChild(node17);
            XmlNode node18 = document.CreateElement("CorpTele");
            node18.InnerText = fwsk_ErrInfo_0.CorpTelephone.Trim();
            node7.AppendChild(node18);
            XmlNode node15 = document.CreateElement("MachineInfo");
            if (base.TaxCardInstance.Machine == 0)
            {
                node15.InnerText = fwsk_ErrInfo_0.MachineInfo = "主机";
                node16 = document.CreateElement("MachCount");
                node16.InnerText = fwsk_ErrInfo_0.MachCount.Trim();
            }
            else
            {
                node15.InnerText = fwsk_ErrInfo_0.MachineInfo = "分机";
                node16 = document.CreateElement("MachNo");
                node16.InnerText = fwsk_ErrInfo_0.MachCount.Trim();
            }
            node7.AppendChild(node15);
            node7.AppendChild(node16);
            newChild.AppendChild(node7);
            XmlNode node19 = document.CreateElement("ErrInfo");
            XmlNode node20 = document.CreateElement("OpItem");
            node20.InnerText = fwsk_ErrInfo_0.OpItem.Trim();
            node19.AppendChild(node20);
            XmlNode node21 = document.CreateElement("ErrCode");
            node21.InnerText = fwsk_ErrInfo_0.ErrCode.Trim();
            node19.AppendChild(node21);
            int result = 0;
            if (int.TryParse(fwsk_ErrInfo_0.FuncCode, out result) && (result != 0))
            {
                XmlNode node22 = document.CreateElement("FuncCode");
                node22.InnerText = fwsk_ErrInfo_0.FuncCode.Trim();
                node19.AppendChild(node22);
            }
            XmlNode node23 = document.CreateElement("ErrDesride");
            node23.InnerText = fwsk_ErrInfo_0.ErrDesride.Trim();
            node19.AppendChild(node23);
            XmlNode node24 = document.CreateElement("Reason");
            node24.InnerText = fwsk_ErrInfo_0.Reason.Trim();
            node19.AppendChild(node24);
            newChild.AppendChild(node19);
            XmlNode node25 = document.CreateElement("OtherInfo");
            XmlNode node26 = document.CreateElement("ErrDetailed");
            XmlNode node27 = document.CreateElement("Step");
            node27.InnerText = fwsk_ErrInfo_0.Step.Trim();
            node26.AppendChild(node27);
            XmlNode node28 = document.CreateElement("Resolvent");
            node28.InnerText = fwsk_ErrInfo_0.Resolvent.Trim();
            node26.AppendChild(node28);
            node25.AppendChild(node26);
            XmlNode node29 = document.CreateElement("AddAdvise");
            node29.InnerText = fwsk_ErrInfo_0.AddAdvise.Trim();
            node25.AppendChild(node29);
            XmlNode node30 = document.CreateElement("Contactme");
            XmlNode node31 = document.CreateElement("Corp");
            node31.InnerText = fwsk_ErrInfo_0.Corp.Trim();
            node30.AppendChild(node31);
            XmlNode node32 = document.CreateElement("name");
            node32.InnerText = fwsk_ErrInfo_0.Name.Trim();
            node30.AppendChild(node32);
            XmlNode node33 = document.CreateElement("post");
            node33.InnerText = fwsk_ErrInfo_0.Post.Trim();
            node30.AppendChild(node33);
            XmlNode node34 = document.CreateElement("Adress");
            node34.InnerText = fwsk_ErrInfo_0.Adress.Trim();
            node30.AppendChild(node34);
            XmlNode node35 = document.CreateElement("email");
            node35.InnerText = fwsk_ErrInfo_0.Email.Trim();
            node30.AppendChild(node35);
            XmlNode node36 = document.CreateElement("telephone");
            node36.InnerText = fwsk_ErrInfo_0.Telephone.Trim();
            node30.AppendChild(node36);
            XmlNode node37 = document.CreateElement("tax");
            node37.InnerText = fwsk_ErrInfo_0.Tax.Trim();
            node30.AppendChild(node37);
            node25.AppendChild(node30);
            newChild.AppendChild(node25);
            XmlNode node38 = document.CreateElement("CNNSR");
            XmlNode node39 = document.CreateElement("AuthorityCode");
            string s = base.TaxCardInstance.TaxCode + "cnnsr";
            string str2 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(s))).Replace("-", "");
            node39.InnerText = str2;
            node38.AppendChild(node39);
            XmlNode node40 = document.CreateElement("SupportMail");
            node40.InnerText = fwsk_ErrInfo_0.SuprtMail.Trim();
            node38.AppendChild(node40);
            newChild.AppendChild(node38);
            document.AppendChild(newChild);
            return document.InnerXml;
        }
        catch (BaseException exception)
        {
            ExceptionHandler.HandleError(exception);
        }
        catch (Exception exception2)
        {
            ExceptionHandler.HandleError(exception2);
        }
        return string.Empty;
    }
}

