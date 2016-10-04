namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;
    using System.Windows.Forms;

    public sealed class GenerateNCPSGPTFPEntry : AbstractCommand
    {
        private bool CanXTInv(TaxCard taxCard)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if ((((objArray != null) && (objArray.Length > 0)) && (objArray[0] != null)) && Convert.ToBoolean(objArray[0]))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.get_QYLX().ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (!(((objArray2 != null) && (objArray2[0] is bool)) && Convert.ToBoolean(objArray2[0])))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected override void RunCommand()
        {
            SPFLService service;
            TaxCard taxCard = TaxCardFactory.CreateTaxCard();
            InvSplitPara para = new InvSplitPara();
            para.GetInvSplitPara(InvType.Common);
            bool flag = taxCard.get_StateInfo().CompanyType != 0;
            if (para.ShowSetForm && flag)
            {
                service = new SPFLService();
                if (CommonTool.isSPBMVersion() && (service.GetMaxBMBBBH() == "0.0"))
                {
                    MessageBox.Show("商品和服务税收分类编码表为空，请先更新商品和服务税收分类编码表后，再开具发票！");
                }
                else
                {
                    GenerateInvSetForm form2 = new GenerateInvSetForm(InvType.Common) {
                        NCPBZ = 2
                    };
                    form2.ShowDialog();
                }
            }
            else
            {
                service = new SPFLService();
                if (CommonTool.isSPBMVersion() && (service.GetMaxBMBBBH() == "0.0"))
                {
                    MessageBox.Show("商品和服务税收分类编码表为空，请先更新商品和服务税收分类编码表后，再开具发票！");
                }
                else
                {
                    GenerateFP efp = new GenerateFP(InvType.Common, 2);
                    string code = "";
                    if (!efp.CanInvoice(2, out code))
                    {
                        MessageManager.ShowMsgBox(code);
                    }
                    else if (!this.CanXTInv(taxCard))
                    {
                        MessageManager.ShowMsgBox("INP-242132");
                    }
                    else
                    {
                        JSFPJSelect select = new JSFPJSelect(2);
                        if (select.ShowDialog() == DialogResult.OK)
                        {
                            if (efp.GetCurrent(0x29) != null)
                            {
                            }
                            efp.ShowDialog();
                        }
                    }
                }
            }
        }

        protected override bool SetValid()
        {
            try
            {
                bool flag3;
                bool flag4;
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (card.get_StateInfo().CompanyType == 0)
                {
                    flag3 = (WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT;
                    flag4 = card.get_QYLX().ISPTFP && card.get_QYLX().ISNCPSG;
                    return (flag3 && flag4);
                }
                flag3 = (WbjkEntry.RegFlag_JT || WbjkEntry.RegFlag_ST) || WbjkEntry.RegFlag_KT;
                flag4 = card.get_QYLX().ISPTFP && card.get_QYLX().ISNCPSG;
                return (flag3 && flag4);
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

