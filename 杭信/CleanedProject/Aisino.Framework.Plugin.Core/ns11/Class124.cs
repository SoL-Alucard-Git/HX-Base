namespace ns11
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Toolbar;
    using ns9;
    using System;
    using System.Collections;

    internal sealed class Class124 : Interface3
    {
        public Class124()
        {
            
        }

        public object imethod_0(object object_0, Function function_0, ArrayList arrayList_0)
        {
            string str = function_0.Properties.ContainsKey("type") ? function_0.Properties["type"] : "Item";
            switch (str)
            {
                case "Item":
                    return new ToolBarCommand(function_0, object_0);

                case "Separator":
                    return new Class130(object_0);

                case "Button":
                    return new ToolBarButton(function_0, object_0);

                case "Label":
                    return new ToolBarLabel(function_0, object_0);

                case "DropButton":
                    return new Class114(function_0, object_0, arrayList_0);
            }
            throw new NotSupportedException("不支持的工具栏按钮类型: " + str);
        }
    }
}

