namespace Aisino.Fwkp.Xtsz.Factory
{
    using System;

    public class BLLFactorey
    {
        public static T CreateInstant<T>(string TypeName)
        {
            T local = default(T);
            try
            {
                typeof(string).ToString();
                return (T) Activator.CreateInstance(Type.GetType("Aisino.Fwkp.Xtsz.BLL." + TypeName));
            }
            catch
            {
                return local;
            }
        }
    }
}

