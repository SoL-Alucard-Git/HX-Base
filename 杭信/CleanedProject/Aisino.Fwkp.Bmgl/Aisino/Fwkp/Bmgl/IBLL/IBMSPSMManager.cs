namespace Aisino.Fwkp.Bmgl.IBLL
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
        string AddGoodsTaxKP(BMSPSMModel spsmEntity, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string CheckGoodsTax(BMSPSMModel spsmEntity);
        string deleteFenLei(string flBM);
        string DeleteGoodsTax(string spsmEntityCode, string SZ);
        string DeleteGoodsTaxFenLei(string FenLeiCodeBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportGoodsTax(string pathFile, string separator);
        DataTable GetBMSPSM(string BM);
        BMSPSMModel GetModel(string BM, string SZ = "");
        ImportResult ImportGoodsTax(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyGoodsTax(BMSPSMModel spsmEntity, string OldSZ, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryGoodsTax(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMSPSMModel spsmEntity, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

