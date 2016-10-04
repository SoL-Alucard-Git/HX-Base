namespace BSDC
{
    using System;
    using System.Collections.Generic;

    public class PrintEntity
    {
        public InvTypeEntity m_InvTypeEntity;
        public List<ItemEntity> m_ItemEntity;

        public PrintEntity()
        {
            
            this.m_InvTypeEntity = new InvTypeEntity();
            this.m_ItemEntity = new List<ItemEntity>();
        }
    }
}

