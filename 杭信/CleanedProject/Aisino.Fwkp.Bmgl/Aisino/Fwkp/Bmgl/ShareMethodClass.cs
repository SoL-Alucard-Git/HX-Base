namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using System;

    public class ShareMethodClass : AbstractCommand
    {
        public ShareMethodClass()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddKH", "Aisino.Fwkp.Bmgl.dll", typeof(AddKH).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddKHAuto", "Aisino.Fwkp.Bmgl.dll", typeof(AddKHAuto).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetKH", "Aisino.Fwkp.Bmgl.dll", typeof(GetKH).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetKHbySH", "Aisino.Fwkp.Bmgl.dll", typeof(GetKHbySH).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetSP", "Aisino.Fwkp.Bmgl.dll", typeof(GetSP).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddSP", "Aisino.Fwkp.Bmgl.dll", typeof(AddSP).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.GetKhbyMcAndShMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetKhbyMcAndShMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetKHMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetKHMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetSPMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetSPMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", "Aisino.Fwkp.Bmgl.dll", typeof(IsNeedImportXTSP).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMImportXTSP", "Aisino.Fwkp.Bmgl.dll", typeof(ImportXTSP).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetXTCode", "Aisino.Fwkp.Bmgl.dll", typeof(GetXTCode).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", "Aisino.Fwkp.Bmgl.dll", typeof(CheckXTSP).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetXTSPXX", "Aisino.Fwkp.Bmgl.dll", typeof(GetXTSPXX).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMCheckXTSPByName", "Aisino.Fwkp.Bmgl.dll", typeof(CheckXTSPByName).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetSPSM", "Aisino.Fwkp.Bmgl.dll", typeof(GetSPSM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMAddSPSM", "Aisino.Fwkp.Bmgl.dll", typeof(AddSPSM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMDeleteSPSM", "Aisino.Fwkp.Bmgl.dll", typeof(DeleteSPSM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMUpdateSPSM", "Aisino.Fwkp.Bmgl.dll", typeof(UpdateSPSM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetSPSMMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetSPSMMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetXZQY", "Aisino.Fwkp.Bmgl.dll", typeof(GetXZQY).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMAddXZQY", "Aisino.Fwkp.Bmgl.dll", typeof(AddXZQY).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMDeleteXZQY", "Aisino.Fwkp.Bmgl.dll", typeof(DeleteXZQY).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMUpdateXZQY", "Aisino.Fwkp.Bmgl.dll", typeof(UpdateXZQY).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddFYXM", "Aisino.Fwkp.Bmgl.dll", typeof(AddFYXM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetFYXM", "Aisino.Fwkp.Bmgl.dll", typeof(GetFYXM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetFYXMMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetFYXMMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddGHDW", "Aisino.Fwkp.Bmgl.dll", typeof(AddGHDW).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddGHDWAuto", "Aisino.Fwkp.Bmgl.dll", typeof(AddGHDWAuto).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetGHDW", "Aisino.Fwkp.Bmgl.dll", typeof(GetGHDW).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetGHDWbySH", "Aisino.Fwkp.Bmgl.dll", typeof(GetGHDWbySH).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetGHDWMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetGHDWMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddSFHR", "Aisino.Fwkp.Bmgl.dll", typeof(AddSFHR).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetSFHR", "Aisino.Fwkp.Bmgl.dll", typeof(GetSFHR).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetSFHRMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetSFHRMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddCL", "Aisino.Fwkp.Bmgl.dll", typeof(AddCL).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetCL", "Aisino.Fwkp.Bmgl.dll", typeof(GetCL).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetCLMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetCLMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLAddXHDW", "Aisino.Fwkp.Bmgl.dll", typeof(AddXHDW).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetXHDW", "Aisino.Fwkp.Bmgl.dll", typeof(GetXHDW).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGLGetXHDWMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetXHDWMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.AdjustTopBM", "Aisino.Fwkp.Bmgl.dll", typeof(AdjustTopBM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.BMGetSPFLMore", "Aisino.Fwkp.Bmgl.dll", typeof(GetSPFLMore).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.GetSLV_BY_BM", "Aisino.Fwkp.Bmgl.dll", typeof(GetSLV_BY_BM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.CanUseThisSPFLBM", "Aisino.Fwkp.Bmgl.dll", typeof(CanUseThisSPFLBM).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.SPFLSelect", "Aisino.Fwkp.Bmgl.dll", typeof(SPFLSelect).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Bmgl.UpdateDatabaseBySPFL", "Aisino.Fwkp.Bmgl.dll", typeof(UpdateDatabaseBySPFL).FullName, null);
        }
    }
}

