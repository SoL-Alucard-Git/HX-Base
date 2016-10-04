namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMFYXMManager
    {
        string AddExpense(BMFYXMModel feiyong);
        string AddExpenseKP(BMFYXMModel feiyong, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string CheckExpense(BMFYXMModel feiyong);
        string ChildDetermine(string BM, string SJBM);
        string DeleteExpense(string feiyongCode);
        string DeleteExpenseFenLei(string FenLeiCodeBM);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportExpense(string pathFile, string separator);
        DataTable GetFYXM(string BM);
        BMFYXMModel GetModel(string BM);
        int GetSuggestBMLen(string SJBM);
        ImportResult ImportExpense(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyExpense(BMFYXMModel feiyong, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryExpense(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMFYXMModel feiyong, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

