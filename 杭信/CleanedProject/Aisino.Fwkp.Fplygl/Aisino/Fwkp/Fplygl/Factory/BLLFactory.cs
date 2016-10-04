namespace Aisino.Fwkp.Fplygl.Factory
{
    using System;

    public class BLLFactory
    {
        public static T CreateInstant<T>(string TypeName)
        {
            T local = default(T);
            try
            {
                return (T) Activator.CreateInstance(Type.GetType("Aisino.Fwkp.Fplygl.BLL." + TypeName));
            }
            catch
            {
                return local;
            }
        }
    }
}

