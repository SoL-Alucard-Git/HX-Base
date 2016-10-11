using Aisino.Fwkp.BusinessObject;
using InternetWare.Lodging.Data;

namespace InternetWare.Util.Client
{
    internal class CommonMethods
    {
        internal static FPLX ParseFplx(FaPiaoTypes type)
        {
            switch (type)
            {
                case FaPiaoTypes.Special:
                    return FPLX.ZYFP;
                default:
                case FaPiaoTypes.Common:
                    return FPLX.PTFP;
            }
        }
    }
}
