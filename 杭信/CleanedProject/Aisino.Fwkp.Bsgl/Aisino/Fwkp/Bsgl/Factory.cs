namespace Aisino.Fwkp.Bsgl
{
    using System;

    public class Factory
    {
        public static T CreateInstant<T>(string TypeName)
        {
            T local = default(T);
            try
            {
                typeof(string).ToString();
                return (T) Activator.CreateInstance(Type.GetType("Aisino.Fwkp.Bsgl." + TypeName));
            }
            catch
            {
                return local;
            }
        }

        public static T CreateInstant<T>(string TypeName, object[] Args)
        {
            T local = default(T);
            try
            {
                typeof(string).ToString();
                return (T) Activator.CreateInstance(Type.GetType("Aisino.Fwkp.Bsgl." + TypeName), Args);
            }
            catch
            {
                return local;
            }
        }
    }
}

