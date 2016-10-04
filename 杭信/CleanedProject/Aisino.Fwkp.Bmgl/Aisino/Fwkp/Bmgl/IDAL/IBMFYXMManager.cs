namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal interface IBMFYXMManager
    {
        string AddExpense(BMFYXMModel feiyong);
        DataTable AppendByKey(string KeyWord, int TopNo);
        DataTable AppendByKeyWJ(string KeyWord, int TopNo);
        string DeleteExpense(string feiyongCode);
        string deleteFenLei(string flBM);
        string deleteNodes(string searchid);
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistExpense(BMFYXMModel feiyong);
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetFYXM(string BM);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        string ModifyExpense(BMFYXMModel feiyong, string OldBM);
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryExpense(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectBM, int Pagesize, int Pageno);
        string TuiJianBM(string searchid);
        string UpdateSubNodesSJBM(BMFYXMModel feiyong, string YuanBM);
        bool ZengWeiVerify(string BM);
    }
}

