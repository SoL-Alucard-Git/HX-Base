namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMGHDWManager
    {
        string AddPurchase(BMGHDWModel purchase);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string AutoNodeLogic();
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string DeletePurchase(string purchaseCode);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCusTaxCode(string CusTaxCode);
        bool ExistPurchase(BMGHDWModel purchase);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetGHDW(string BM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyPurchase(BMGHDWModel purchase, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMCAndSJBM(string MC, string SJBM);
        DataTable QueryByMCAndSJBMAndSHEmpty(string MC, string SJBM);
        DataTable QueryBySHAndSJBM(string SH, string SJBM);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryPurchase(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMGHDWModel purchase, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

