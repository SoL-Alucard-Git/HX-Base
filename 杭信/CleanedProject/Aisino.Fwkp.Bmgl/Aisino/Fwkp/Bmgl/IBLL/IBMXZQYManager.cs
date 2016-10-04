namespace Aisino.Fwkp.Bmgl.IBLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMXZQYManager
    {
        string AddDistrict(BMXZQYModel xzqEntity);
        string AddDistrictKP(BMXZQYModel xzqEntity, int Addtype);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string CheckDistrict(BMXZQYModel xzqEntity);
        string DeleteDistrict(string xzqEntityCode);
        string DeleteDistrictFenLei(string FenLeiCodeBM);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        string ExportDistrict(string pathFile, string separator);
        DataTable GetBMXZQY(string BM);
        BMXZQYModel GetModel(string BM);
        int GetXZQYSJBM(string BM);
        ImportResult ImportDistrict(string codeFile);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string searchid);
        string ModifyDistrict(BMXZQYModel xzqEntity, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryDistrict(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMXZQYModel xzqEntity, string YuanBM);

        int CurrentPage { get; set; }

        int Pagesize { get; set; }
    }
}

