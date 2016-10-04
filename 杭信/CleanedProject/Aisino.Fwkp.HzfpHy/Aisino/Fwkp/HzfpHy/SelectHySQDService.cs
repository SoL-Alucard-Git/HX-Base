namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.HzfpHy.Form;
    using System;

    public sealed class SelectHySQDService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            string result = string.Empty;
            HySqdSelect select = new HySqdSelect();
            select.ShowDialog();
            result = select.Result;
            return new object[] { result };
        }
    }
}

