namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal interface IBMKHManager
    {
        string AddCustomer(BMKHModel customer);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string AutoNodeLogic();
        string DeleteCustomer(string customerCode);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCusTaxCode(string CusTaxCode, string OldBM = "NoOldBM");
        bool ExistCustomer(BMKHModel customer);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetKH(string BM);
        int GetSuggestBMLen(string SJBM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyCustomer(BMKHModel customer, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMCAndSJBM(string MC, string SJBM);
        DataTable QueryByMCAndSJBMAndSHEmpty(string MC, string SJBM);
        DataTable QueryBySHAndSJBM(string SH, string SJBM);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryCustomer(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMKHModel customer, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

