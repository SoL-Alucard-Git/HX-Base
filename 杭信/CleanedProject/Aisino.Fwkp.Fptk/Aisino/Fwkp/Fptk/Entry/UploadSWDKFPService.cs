namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;
    using System.Xml;

    internal sealed class UploadSWDKFPService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if ((param != null) && (param.Length >= 1))
            {
                Fpxx fp = param[0] as Fpxx;
                if (fp != null)
                {
                    XmlDocument document = new XmlProcessor().CreateDKFPXml(fp);
                    return new object[] { document };
                }
            }
            return null;
        }
    }
}

