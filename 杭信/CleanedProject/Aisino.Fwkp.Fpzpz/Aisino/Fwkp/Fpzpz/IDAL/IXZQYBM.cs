namespace Aisino.Fwkp.Fpzpz.IDAL
{
    using System;
    using System.Collections.Generic;

    internal interface IXZQYBM
    {
        List<XZQYBMModal> SelectXZQYBM_BM(string strBM);
        bool UpdateXZQYBM(string strSET, string strWHERE);
    }
}

