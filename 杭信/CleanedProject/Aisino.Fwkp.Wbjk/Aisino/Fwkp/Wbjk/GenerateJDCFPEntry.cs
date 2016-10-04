namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;
    using System.Collections.Generic;

    public sealed class GenerateJDCFPEntry : AbstractCommand
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
            GenerateFP efp = new GenerateFP(InvType.vehiclesales, 4);
            if (efp.FPleftnum == 0)
            {
                MessageManager.ShowMsgBox("INP-242103");
            }
            else
            {
                efp.ShowDialog();
            }
        }

        protected override bool SetValid()
        {
            try
            {
                bool iSJDC = false;
                iSJDC = TaxCardFactory.CreateTaxCard().get_QYLX().ISJDC;
                bool flag2 = ((WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT) && iSJDC;
                return false;
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

