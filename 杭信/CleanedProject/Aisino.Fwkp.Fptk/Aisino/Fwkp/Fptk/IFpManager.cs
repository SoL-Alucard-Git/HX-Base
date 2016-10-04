namespace Aisino.Fwkp.Fptk
{
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Runtime.InteropServices;

    internal interface IFpManager
    {
        bool CanInvoice(FPLX fplx);
        bool ChargeAllInfo(Fpxx fpxx, bool iswm, bool isdzfp = false);
        bool CheckCJH(string cjh);
        bool CheckRedNum(string redNum, FPLX fplx);
        bool CheckRevBlue(string redNum);
        string Code();
        string[] CodeParams();
        bool CopyDjfp(Fpxx djfp, Invoice fpxx, bool RedFlag = false);
        bool CopyHYRedNotice(Fpxx redNotice, Invoice fpxx);
        bool CopyRedNotice(Fpxx redNotice, Invoice fpxx);
        bool CopyRevBlueNotice(Fpxx revBlueNotice, Invoice fpxx);
        string DecodeIDEAFile(string fileName, int DecodeType);
        string[] GetCurrent(FPLX fplx);
        string GetErrorTip();
        string GetJskClock();
        string GetMachineNum();
        decimal GetTotalRedJe(string blueFpdm, string blueFphm);
        string GetXfdh();
        string GetXfdz();
        string GetXfdzdh();
        string GetXfmc();
        string GetXfsh();
        string GetXfyhzh();
        Fpxx GetXxfp(FPLX fpzl, string fpdm, int fphm);
        string[] GetZgswjg();
        bool IsSWDK();
        bool ParseBz(Fpxx _fpxx);
        Fpxx ProcessHYHZXXBxml(string fileXML);
        Fpxx ProcessHYRedNotice(string fileName);
        Fpxx ProcessHZTZDxml(string fileXML);
        Fpxx ProcessRedNotice(string fileName);
        string QueryXzqy(string fpdm);
        bool SaveXxfp(Fpxx fp);
        void SetErrorTip(string tmp);
        void WSPZ_dr15slv(Fpxx fpxx, FPLX fplx);
    }
}

