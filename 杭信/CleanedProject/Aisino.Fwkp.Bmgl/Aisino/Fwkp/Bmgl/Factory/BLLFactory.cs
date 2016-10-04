namespace Aisino.Fwkp.Bmgl.Factory
{
    using System;

    public class BLLFactory
    {
        public static T CreateInstant<T>(string TypeName)
        {
            T local = default(T);
            try
            {
                typeof(string).ToString();
                return (T) Activator.CreateInstance(Type.GetType("Aisino.Fwkp.Bmgl.BLL." + TypeName));
            }
            catch
            {
                return local;
            }
        }
    }
}

