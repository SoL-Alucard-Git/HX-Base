namespace ns11
{
    using Aisino.Framework.Plugin.Core.Menu;
    using Aisino.Framework.Plugin.Core.Plugin;
    using System;
    using System.Collections;

    internal sealed class Class128 : Interface3
    {
        public Class128()
        {
            
        }

        public object imethod_0(object object_0, Function function_0, ArrayList arrayList_0)
        {
            string str2 = function_0.Properties.ContainsKey("type") ? function_0.Properties["type"] : "Command";
            switch (str2)
            {
                case "Menu":
                    return new MenuLabel(function_0, object_0, arrayList_0);

                case "Command":
                    return new MenuCommand(function_0, object_0);

                case "Separator":
                    return new Class138(function_0, object_0);

                case "AisinoCHK":
                    return new MenuCheckBox(function_0, object_0);
            }
            throw new NotSupportedException("不支持的菜单类型 : " + str2);
        }
    }
}

