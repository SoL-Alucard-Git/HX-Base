namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IBMBaseManager
    {
        string ChildDetermine(string BM, string SJBM);
        string DeleteData(string customerCode);
        string DeleteDataFenLei(string FenLeiCodeBM);
        string deleteFenLei(string flBM);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportData(string pathFile, string separator, DataTable khTable = null);
        BMBaseModel GetModel(string BM);
        int GetSuggestBMLen(string SJBM);
        ImportResult ImportData(string codeFile);
        List<TreeNodeTemp> listNodes(string searchid);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryData(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

