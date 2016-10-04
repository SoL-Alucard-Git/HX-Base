namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;
    using System.ComponentModel;

    public enum FieldType
    {
        [Description("是/否")]
        Bool = 1,
        [Description("查询对象")]
        DataField = 4,
        [Description("日期时间")]
        DateTime = 3,
        [Description("整数")]
        Int = 0,
        [Description("字符串")]
        String = 2
    }
}

