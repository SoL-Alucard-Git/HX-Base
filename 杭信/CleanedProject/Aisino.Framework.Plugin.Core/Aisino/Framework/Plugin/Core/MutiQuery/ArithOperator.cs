namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;
    using System.ComponentModel;

    public enum ArithOperator
    {
        [Description("<>")]
        不等于 = 5,
        [Description(">")]
        大于 = 1,
        [Description(">=")]
        大于等于 = 3,
        [Description("=")]
        等于 = 0,
        [Description("<")]
        小于 = 2,
        [Description("<=")]
        小于等于 = 4
    }
}

