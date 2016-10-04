namespace BSDC
{
    using System;

    public class ItemEntity
    {
        public bool m_bStatus;
        public ITEM_ACTION m_ItemType;
        public string m_strItemName;

        public ItemEntity()
        {
            
            this.m_ItemType = ITEM_ACTION.ITEM_TOTAL;
            this.m_strItemName = "增值税发票汇总表";
            this.m_bStatus = false;
        }
    }
}

