namespace Aisino.Fwkp.Publish.BLL
{
    using System;
    using System.Collections.Generic;

    internal interface IConfigManager
    {
        void QueryConfig();
        bool SaveConfig(Dictionary<string, string> cfg);
    }
}

