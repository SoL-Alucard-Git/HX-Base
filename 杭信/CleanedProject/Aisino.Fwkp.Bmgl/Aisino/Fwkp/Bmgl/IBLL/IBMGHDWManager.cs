namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMGHDWManager
    {
        bool AddCustomerToAuto(BMGHDWModel customer, string SJBM);
        string AddPurchase(BMGHDWModel purchase);
        string AddPurchaseKP(BMGHDWModel purchase, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string AutoNodeLogic();
        string CheckPurchase(BMGHDWModel purchase);
        string ChildDetermine(string BM, string SJBM);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string DeletePurchase(string purchaseCode);
        string DeletePurchaseFenLei(string FenLeiCodeBM);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportPurchase(string pathFile, string separator);
        DataTable GetGHDW(string BM);
        BMGHDWModel GetModel(string BM);
        int GetSuggestBMLen(string SJBM);
        ImportResult ImportPurchase(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyPurchase(BMGHDWModel purchase, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMCAndSJBM(string MC, string SJBM);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryPurchase(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMGHDWModel purchase, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

