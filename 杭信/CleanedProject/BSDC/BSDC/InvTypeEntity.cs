namespace BSDC
{
    using System;

    public class InvTypeEntity
    {
        public INV_TYPE m_invType;
        public string m_strInvName;

        public InvTypeEntity()
        {
            
            this.m_invType = INV_TYPE.INV_SPECIAL;
            this.m_strInvName = "增值税专用发票";
        }

        public static string GetInvName(INV_TYPE inv_TYPE_0)
        {
            switch (inv_TYPE_0)
            {
                case INV_TYPE.INV_SPECIAL:
                    return "专用发票";

                case INV_TYPE.INV_COMMON:
                    return "普通发票";

                case INV_TYPE.INV_TRANSPORTATION:
                    return "货物运输业增值税专用发票";

                case INV_TYPE.INV_VEHICLESALES:
                    return "机动车销售统一发票";
            }
            return "废旧物资发票";
        }

        public string SetInvName(INV_TYPE inv_TYPE_0, string string_0)
        {
            this.m_strInvName = string_0;
            return this.m_strInvName;
        }
    }
}

