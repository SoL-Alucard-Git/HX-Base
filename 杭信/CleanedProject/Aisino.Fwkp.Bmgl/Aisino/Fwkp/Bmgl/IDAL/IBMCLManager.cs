namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMCLManager
    {
        string AddCustomer(BMCLModel car);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string AutoNodeLogic(string DLMC);
        string DeleteCustomer(string carCode);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCustomer(BMCLModel car);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetKH(string BM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyCustomer(BMCLModel car, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByMC(string MC);
        DataTable QueryByMCAndSJBM(string MC, string SJBM);
        AisinoDataSet QueryCustomer(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMCLModel car, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

