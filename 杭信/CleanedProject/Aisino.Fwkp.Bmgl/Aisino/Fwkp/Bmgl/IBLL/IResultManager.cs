namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Fwkp.Bmgl.Model;
    using System;

    internal interface IResultManager
    {
        string SaveTxt(string pathFile, ImportResult ImResult);
    }
}

