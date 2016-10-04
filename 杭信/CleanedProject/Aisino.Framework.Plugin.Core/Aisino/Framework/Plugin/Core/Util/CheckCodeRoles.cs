namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core;
    using System;

    public class CheckCodeRoles
    {
        public CheckCodeRoles()
        {
            
        }

        public static bool IsSNY(string string_0)
        {
            return string_0.Equals(CodeRoles.CORP_PERMIT_SNYQYDM);
        }

        public static bool IsXT(string string_0)
        {
            if (((!CodeRoles.CORP_RATE_XTKCPSCQY.Equals(string_0) && !CodeRoles.CORP_RATE_XTKCPSCQYYCX.Equals(string_0)) && (!CodeRoles.CORP_RATE_XTSMQY.Equals(string_0) && !CodeRoles.CORP_RATE_XTSMQYYCX.Equals(string_0))) && (!CodeRoles.CORP_RATE_XTYLFLQY.Equals(string_0) && !CodeRoles.CORP_RATE_XTYLFLQYYCX.Equals(string_0)))
            {
                return false;
            }
            return true;
        }
    }
}

