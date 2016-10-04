namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMCLManager
    {
        string AddCustomer(BMCLModel car);
        string AddCustomerKP(BMCLModel car, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string CheckCustomer(BMCLModel car);
        string ChildDetermine(string BM, string SJBM);
        string DeleteCustomer(string carCode);
        string DeleteCustomerFenLei(string FenLeiCodeBM);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportCustomer(string pathFile, string separator);
        DataTable GetCL(string BM);
        BMCLModel GetModel(string BM);
        int GetSuggestBMLen(string SJBM);
        ImportResult ImportCustomer(string codeFile);
        ImportResult ImportCustomerZC(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyCustomer(BMCLModel car, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMC(string MC);
        AisinoDataSet QueryCustomer(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMCLModel car, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

