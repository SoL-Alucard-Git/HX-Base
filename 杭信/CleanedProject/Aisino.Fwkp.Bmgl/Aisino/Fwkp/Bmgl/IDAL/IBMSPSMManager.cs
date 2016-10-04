namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal interface IBMSPSMManager
    {
        string AddGoodsTax(BMSPSMModel spsmEntity);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string deleteFenLei(string flBM);
        string DeleteGoodsTax(string spsmEntityCode, string SZ);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCusTaxCode(string CusTaxCode);
        bool ExistGoodsTax(BMSPSMModel spsmEntity);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetBMSPSM(string BM, string SZ = "");
        DataTable GetExportData();
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyGoodsTax(BMSPSMModel spsmEntity, string OldSZ, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryGoodsTax(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMSPSMModel spsmEntity, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

