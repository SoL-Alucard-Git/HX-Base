namespace Aisino.Fwkp.Fplygl.BLL
{
    using Aisino.Framework.Dao;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using Aisino.Fwkp.Fplygl.IBLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal class LYGL_PSXX : ILYGL_PSXX
    {
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        public bool CheckExist(AddressInfo addrInfo)
        {
            this.dict.Clear();
            this.dict.Add("SJR", addrInfo.receiverName);
            this.dict.Add("YZBM", addrInfo.postcode);
            this.dict.Add("DZ", addrInfo.address);
            this.dict.Add("GDDH", addrInfo.landline);
            this.dict.Add("YDDH", addrInfo.cellphone);
            this.dict.Add("BZ", addrInfo.memo);
            return (this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.PSInfo.CheckExist", this.dict).Rows.Count > 0);
        }

        public int CountSynAddrItems()
        {
            this.dict.Clear();
            return this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.PSInfo.CountSynItems", this.dict).Rows.Count;
        }

        public bool DeleteAddrInfo(AddressInfo addrInfo)
        {
            this.dict.Clear();
            this.dict.Add("SJR", addrInfo.receiverName);
            this.dict.Add("YZBM", addrInfo.postcode);
            this.dict.Add("DZ", addrInfo.address);
            this.dict.Add("GDDH", addrInfo.landline);
            this.dict.Add("YDDH", addrInfo.cellphone);
            this.dict.Add("BZ", addrInfo.memo);
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.PSInfo.Delete", this.dict) != 1)
            {
                return false;
            }
            return true;
        }

        public void DeleteSynAddrInfos()
        {
            this.dict.Clear();
            this.baseDao.updateSQL("aisino.Fwkp.Fplygl.PSInfo.DeleteSyn", this.dict);
        }

        public bool InsertAddrInfo(AddressInfo addrInfo, bool isDefault)
        {
            this.dict.Clear();
            this.dict.Add("SFMR", isDefault);
            this.dict.Add("SJR", addrInfo.receiverName);
            this.dict.Add("YZBM", addrInfo.postcode);
            this.dict.Add("DZ", addrInfo.address);
            this.dict.Add("GDDH", addrInfo.landline);
            this.dict.Add("YDDH", addrInfo.cellphone);
            this.dict.Add("BZ", addrInfo.memo);
            this.dict.Add("SFTB", addrInfo.isSyn);
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.PSInfo.Insert", this.dict) != 1)
            {
                return false;
            }
            return true;
        }

        public void SelectAddrInfos(out List<AddressInfo> addrInfoList, out List<bool> isDefaultList)
        {
            this.dict.Clear();
            DataTable table = this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.PSInfo.SelectList", this.dict);
            if (table.Rows.Count <= 0)
            {
                addrInfoList = null;
                isDefaultList = null;
            }
            else
            {
                List<AddressInfo> list = new List<AddressInfo>();
                List<bool> list2 = new List<bool>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    AddressInfo item = new AddressInfo();
                    bool flag = false;
                    item.address = row["DZ"].ToString();
                    item.cellphone = row["YDDH"].ToString();
                    item.landline = row["GDDH"].ToString();
                    item.postcode = row["YZBM"].ToString();
                    item.memo = row["BZ"].ToString();
                    item.receiverName = row["SJR"].ToString();
                    flag = Convert.ToBoolean(row["SFMR"]);
                    list.Add(item);
                    list2.Add(flag);
                }
                addrInfoList = list;
                isDefaultList = list2;
            }
        }

        public AddressInfo SelectDefaultAddr()
        {
            this.dict.Clear();
            DataTable table = this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.PSInfo.SelectDefault", this.dict);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = table.Rows[0];
            return new AddressInfo { address = row["DZ"].ToString(), cellphone = row["YDDH"].ToString(), landline = row["GDDH"].ToString(), postcode = row["YZBM"].ToString(), memo = row["BZ"].ToString(), receiverName = row["SJR"].ToString() };
        }

        public bool UpdateAddrDefault(AddressInfo oldAddr, AddressInfo addrInfo)
        {
            if (oldAddr != null)
            {
                this.dict.Clear();
                this.dict.Add("SFMR", false);
                this.dict.Add("SJR", oldAddr.receiverName);
                this.dict.Add("YZBM", oldAddr.postcode);
                this.dict.Add("DZ", oldAddr.address);
                this.dict.Add("GDDH", oldAddr.landline);
                this.dict.Add("YDDH", oldAddr.cellphone);
                this.dict.Add("BZ", oldAddr.memo);
                if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.PSInfo.UpdateDefault", this.dict) != 1)
                {
                    return false;
                }
            }
            this.dict.Clear();
            this.dict.Add("SFMR", true);
            this.dict.Add("SJR", addrInfo.receiverName);
            this.dict.Add("YZBM", addrInfo.postcode);
            this.dict.Add("DZ", addrInfo.address);
            this.dict.Add("GDDH", addrInfo.landline);
            this.dict.Add("YDDH", addrInfo.cellphone);
            this.dict.Add("BZ", addrInfo.memo);
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.PSInfo.UpdateDefault", this.dict) != 1)
            {
                return false;
            }
            return true;
        }
    }
}

