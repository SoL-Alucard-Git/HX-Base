namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMXZQYManager
    {
        string AddDistrict(BMXZQYModel xzqEntity);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string DeleteDistrict(string xzqEntityCode);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistCusTaxCode(string CusTaxCode);
        bool ExistDistrict(BMXZQYModel xzqEntity);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetBMXZQY(string BM);
        DataTable GetExportData();
        int GetXZQYSJBM(string BM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyDistrict(BMXZQYModel xzqEntity, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByTaxCode(string TaxCode);
        AisinoDataSet QueryDistrict(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMXZQYModel xzqEntity, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

