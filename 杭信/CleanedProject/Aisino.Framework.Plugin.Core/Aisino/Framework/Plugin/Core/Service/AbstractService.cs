namespace Aisino.Framework.Plugin.Core.Service
{
    using System;

    public class AbstractService : Interface4
    {
        public AbstractService()
        {
            
        }

        protected virtual object[] doService(object[] object_0)
        {
            return null;
        }

        public object[] imethod_0(object[] object_0)
        {
            return this.doService(object_0);
        }
    }
}

