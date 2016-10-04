namespace Aisino.Fwkp.Fplygl.IBLL
{
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal interface ILYGL_PSXX
    {
        bool CheckExist(AddressInfo addrInfo);
        int CountSynAddrItems();
        bool DeleteAddrInfo(AddressInfo addrInfo);
        void DeleteSynAddrInfos();
        bool InsertAddrInfo(AddressInfo addrInfo, bool isDefault);
        void SelectAddrInfos(out List<AddressInfo> addrInfoList, out List<bool> isDefaultList);
        AddressInfo SelectDefaultAddr();
        bool UpdateAddrDefault(AddressInfo oldAddr, AddressInfo addrInfo);
    }
}

