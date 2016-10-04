namespace Aisino.Fwkp.Fplygl.IBLL
{
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal interface ILYGL_JPXX
    {
        bool DeleteSingleVolumn(InvVolumeApp invVolumn);
        string GetSpecificFormat(InvVolumeApp invVolume);
        bool InsertSingleVolumn(InvVolumeApp invVolumn, string formatCode);
        bool InsertVolumnList(Dictionary<InvVolumeApp, string> volumnFormat);
        Dictionary<InvVolumeApp, string> SelectVolumnList();
        void SelectVolumnList(out List<InvVolumeApp> invList, out List<string> typeList);
        bool UpdateSingleVolumn(InvVolumeApp invVolumn, string formatCode);
        bool UpdateVolumnList(Dictionary<InvVolumeApp, string> volumnFormat);
    }
}

