namespace Aisino.Fwkp.Bmgl.IDAL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal interface IBMSPManager
    {
        string AddMerchandise(BMSPModel merchandise);
        DataTable AppendByKey(string KeyWord, int TopNo, double slv, string specialSP, string specialFlag);
        string ChildDetermine(string BM, string SJBM);
        bool ContainXTSP(string BM);
        void DelCommonSPByName(string MC);
        string deleteFenLei(string flBM);
        string DeleteMerchandise(string merchandiseBM);
        string deleteNodes(string searchid);
        void DeleteXTSP();
        string ExecZengJianWei(string BM, bool isZengWei);
        bool ExistMerchandise(BMSPModel customer);
        bool ExistSameMCXH(BMSPModel customer, string OldBM = "");
        bool ExistXTSP();
        bool FenLeiHasChild(string fenLeiBM);
        DataTable GetExportData();
        DataTable GetSP(string BM);
        string[] GetSPXXByName(string name);
        int GetSuggestBMLen(string SJBM);
        string GetXTCodeByName(string name);
        string InsertMerchandises(BMSPModel[] merchandises);
        bool JianWeiVerify(string BM);
        List<TreeNodeTemp> listNodes(string BM);
        List<TreeNodeTemp> listNodesISHIDE(string BM);
        string ModifyMerchandise(BMSPModel merchandise, string OldBM);
        DataTable QueryAllXTSP();
        AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno);
        DataTable QueryByKeyAndSlv(string KeyWord, double Slv, int WJ, int Top);
        DataTable QueryByKeyAndSlvSEL(string KeyWord, double Slv, int WJ, int Top);
        DataTable QueryByKeyAndSpecialSPSEL(string KeyWord, string specialSP, int WJ, int Top);
        AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, int Pagesize, int Pageno);
        AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno);
        AisinoDataSet QueryByKeyDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno);
        AisinoDataSet QueryByKeySEL(string KeyWord, int pagesize, int pageno);
        AisinoDataSet QueryMerchandise(int pagesize, int pageno);
        AisinoDataSet SelectNodeDisplay(string selectedBM, int Pagesize, int Pageno);
        AisinoDataSet SelectNodeDisplay(string selectedBM, double slv, int Pagesize, int Pageno);
        AisinoDataSet SelectNodeDisplaySEL(string selectedBM, int Pagesize, int Pageno);
        AisinoDataSet SelectNodeDisplaySEL(string selectedBM, double slv, int Pagesize, int Pageno);
        AisinoDataSet SelectNodeDisplaySEL(string selectedBM, string specialSP, int Pagesize, int Pageno);
        string TuiJianBM(string bm);
        void UpdateSPIsHide(string bm, bool isHide);
        string UpdateSubNodesSJBM(BMSPModel merchandise, string YuanBM);
        void UpdateXTIsHide(string sjbm, string hide);
        bool ZengWeiVerify(string BM);
    }
}

