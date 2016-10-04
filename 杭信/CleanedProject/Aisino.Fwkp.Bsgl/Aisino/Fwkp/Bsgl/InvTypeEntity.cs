namespace Aisino.Fwkp.Bsgl
{
    using System;

    public class InvTypeEntity
    {
        public INV_TYPE m_invType = INV_TYPE.INV_SPECIAL;
        public string m_strInvName = "增值税专用发票";

        public static string GetInvName(INV_TYPE invType)
        {
            switch (invType)
            {
                case INV_TYPE.INV_SPECIAL:
                    return "专用发票";

                case INV_TYPE.INV_COMMON:
                    return "普通发票";

                case INV_TYPE.INV_TRANSPORTATION:
                    return "货物运输业增值税专用发票";

                case INV_TYPE.INV_VEHICLESALES:
                    return "机动车销售统一发票";

                case INV_TYPE.INV_PTDZ:
                    return "电子增值税普通发票";

                case INV_TYPE.INV_JSFP:
                    return "增值税普通发票(卷票)";
            }
            return "废旧物资发票";
        }

        public string SetInvName(INV_TYPE _InvType, string strNewName)
        {
            this.m_strInvName = strNewName;
            return this.m_strInvName;
        }
    }
}

