namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public sealed class GenerateHYFPEntry : AbstractCommand
    {
        private List<List<string>> QZSQ_Split(string SQ)
        {
            string str = ",";
            string str2 = "-";
            List<List<string>> list = new List<List<string>>();
            List<string> list2 = new List<string>();
            string[] strArray = SQ.Split(new string[] { str }, StringSplitOptions.None);
            foreach (string str3 in strArray)
            {
                list2.Add(str3);
            }
            int count = list2.Count;
            for (int i = 0; i < count; i++)
            {
                string str4 = list2[i];
                List<string> item = new List<string>();
                string[] strArray2 = str4.Split(new string[] { str2 }, StringSplitOptions.None);
                foreach (string str5 in strArray2)
                {
                    item.Add(str5);
                }
                list.Add(item);
            }
            return list;
        }

        protected override void RunCommand()
        {
            SPFLService service = new SPFLService();
            if (CommonTool.isSPBMVersion() && (service.GetMaxBMBBBH() == "0.0"))
            {
                MessageBox.Show("商品和服务税收分类编码表为空，请先更新商品和服务税收分类编码表后，再开具发票！");
            }
            else
            {
                GenerateFP efp = new GenerateFP(InvType.transportation);
                string code = "";
                if (!efp.CanInvoice(11, out code))
                {
                    MessageManager.ShowMsgBox(code);
                }
                else
                {
                    efp.ShowDialog();
                }
            }
        }

        protected override bool SetValid()
        {
            try
            {
                bool iSHY = false;
                iSHY = TaxCardFactory.CreateTaxCard().get_QYLX().ISHY;
                return (((WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT) && iSHY);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

