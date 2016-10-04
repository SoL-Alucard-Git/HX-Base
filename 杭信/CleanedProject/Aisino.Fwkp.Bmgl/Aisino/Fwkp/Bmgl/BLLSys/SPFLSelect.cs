namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.Forms;
    using System;
    using System.Windows.Forms;

    internal sealed class SPFLSelect : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            bool isxtsp = false;
            string keyWord = string.Empty;
            if (param.Length >= 2)
            {
                keyWord = (string) param[0];
                isxtsp = (bool) param[1];
            }
            BMSPFLSelect select = new BMSPFLSelect(keyWord, isxtsp, this);
            if (DialogResult.OK == select.ShowDialog())
            {
                return new object[] { select.SelectBM };
            }
            return null;
        }
    }
}

