namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Util;
    using System;

    public class StubFpDetailInterface : AbstractCommand
    {
        private bool CheckData()
        {
            if (!RegisterManager.CheckRegFile("JS"))
            {
                MessageManager.ShowMsgBox("INP-111001", "提示", new string[] { "注册文件校验失败！" });
                return false;
            }
            return true;
        }

        protected override void RunCommand()
        {
            if (this.CheckData())
            {
                new StubFPOutput { ShowInTaskbar = false }.ShowDialog();
            }
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

