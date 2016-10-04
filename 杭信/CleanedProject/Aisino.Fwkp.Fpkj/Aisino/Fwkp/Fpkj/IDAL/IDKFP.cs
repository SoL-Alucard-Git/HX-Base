namespace Aisino.Fwkp.Fpkj.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpkj.Model;
    using System;
    using System.Collections.Generic;

    public interface IDKFP
    {
        bool Add(DKFP model);
        bool Delete(string FPDM, int FPHM);
        DKFP GetModel(string FPDM, int FPHM);
        AisinoDataSet SelectDkfplist(int page, int count, Dictionary<string, object> dict);
    }
}

