namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal interface IBMSFHRManager
    {
        string AddCustomer(BMSFHRModel customer);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string DeleteCustomer(string customerCode);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCusTaxCode(string CusTaxCode, string OldBM = "NoOldBM");
        bool ExistCustomer(BMSFHRModel customer);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetKH(string BM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyCustomer(BMSFHRModel customer, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryCustomer(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMSFHRModel customer, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

