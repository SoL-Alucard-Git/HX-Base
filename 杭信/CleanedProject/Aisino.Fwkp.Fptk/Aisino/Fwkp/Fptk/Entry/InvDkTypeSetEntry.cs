namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Form;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class InvDkTypeSetEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<InvDkTypeSetEntry>();

        protected override void RunCommand()
        {
            InvDkTypeSetForm instance = InvDkTypeSetForm.GetInstance();
            instance.StartPosition = FormStartPosition.CenterScreen;
            instance.ShowInTaskbar = true;
            instance.ShowDialog();
        }

        protected override bool SetValid()
        {
            bool flag = RegisterManager.CheckRegFile("DKST");
            FpManager manager = new FpManager();
            return ((((int)TaxCardFactory.CreateTaxCard().TaxMode == 2) && manager.IsSWDK()) & flag);
        }
    }
}

