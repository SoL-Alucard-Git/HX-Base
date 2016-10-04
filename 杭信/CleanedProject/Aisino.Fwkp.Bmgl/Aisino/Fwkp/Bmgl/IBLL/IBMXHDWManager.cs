namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMXHDWManager
    {
        string AddCustomer(BMXHDWModel customer);
        string AddCustomerKP(BMXHDWModel customer, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string CheckCustomer(BMXHDWModel customer);
        string ChildDetermine(string BM, string SJBM);
        string DeleteCustomer(string customerCode);
        string DeleteCustomerFenLei(string FenLeiCodeBM);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportCustomer(string pathFile, string separator);
        DataTable GetKH(string BM);
        BMXHDWModel GetModel(string BM);
        int GetSuggestBMLen(string SJBM);
        ImportResult ImportCustomer(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyCustomer(BMXHDWModel customer, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMCAndSJBM(string MC, string SJBM);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryCustomer(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMXHDWModel customer, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

