namespace Aisino.Fwkp.Fplygl.BLL
{
    using Aisino.Framework.Dao;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.IBLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    internal class LYGL_JPXX : ILYGL_JPXX
    {
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        public bool DeleteSingleVolumn(InvVolumeApp invVolumn)
        {
            this.dict.Clear();
            this.dict.Add("LBDM", invVolumn.TypeCode);
            this.dict.Add("JQSH", ShareMethods.GetVolumnStartNum(invVolumn));
            this.dict.Add("JZZH", ShareMethods.GetVolumnEndNum(invVolumn));
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.JPInfo.DeleteSingle", this.dict) != 1)
            {
                return false;
            }
            return true;
        }

        public string GetSpecificFormat(InvVolumeApp invVolume)
        {
            this.dict.Clear();
            this.dict.Add("LBDM", invVolume.TypeCode);
            this.dict.Add("JQSH", ShareMethods.GetVolumnStartNum(invVolume));
            this.dict.Add("JZZH", ShareMethods.GetVolumnEndNum(invVolume));
            DataTable table = this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.JPInfo.GetSpecificFormat", this.dict);
            string str = string.Empty;
            if ((table != null) && (table.Rows.Count > 0))
            {
                DataRow row = table.Rows[0];
                str = row["JPGG"].ToString();
            }
            return str;
        }

        public bool InsertSingleVolumn(InvVolumeApp invVolumn, string formatCode)
        {
            this.dict.Clear();
            this.dict.Add("LBDM", invVolumn.TypeCode);
            this.dict.Add("JQSH", ShareMethods.GetVolumnStartNum(invVolumn));
            this.dict.Add("JZZH", ShareMethods.GetVolumnEndNum(invVolumn));
            this.dict.Add("LGZS", invVolumn.BuyNumber);
            this.dict.Add("QSHM", invVolumn.HeadCode);
            this.dict.Add("SYZS", invVolumn.Number);
            this.dict.Add("KPXE", 0);
            this.dict.Add("LGRQ", invVolumn.BuyDate);
            this.dict.Add("JPGG", formatCode);
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.JPInfo.InsertSingle", this.dict) != 1)
            {
                return false;
            }
            return true;
        }

        public bool InsertVolumnList(Dictionary<InvVolumeApp, string> volumnFormat)
        {
            int num = 0;
            foreach (InvVolumeApp app in volumnFormat.Keys)
            {
                if (this.InsertSingleVolumn(app, volumnFormat[app]))
                {
                    num++;
                }
            }
            if (num != volumnFormat.Count)
            {
                return false;
            }
            return true;
        }

        public Dictionary<InvVolumeApp, string> SelectVolumnList()
        {
            Dictionary<InvVolumeApp, string> dictionary = new Dictionary<InvVolumeApp, string>();
            DataTable table = this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.JPInfo.SelectList", null);
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    InvVolumeApp key = new InvVolumeApp {
                        TypeCode = row["LBDM"].ToString(),
                        BuyNumber = Convert.ToInt32(row["LGZS"]),
                        HeadCode = Convert.ToUInt32(row["QSHM"]),
                        Number = Convert.ToUInt16(row["SYZS"]),
                        BuyDate = Convert.ToDateTime(row["LGRQ"])
                    };
                    string str = row["JPGG"].ToString();
                    dictionary.Add(key, str);
                }
            }
            return dictionary;
        }

        public void SelectVolumnList(out List<InvVolumeApp> invList, out List<string> typeList)
        {
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            List<string> list2 = new List<string>();
            DataTable table = this.baseDao.querySQLDataTable("aisino.Fwkp.Fplygl.JPInfo.SelectList", null);
            if ((table != null) && (table.Rows.Count > 0))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    InvVolumeApp item = new InvVolumeApp {
                        TypeCode = row["LBDM"].ToString(),
                        BuyNumber = Convert.ToInt32(row["LGZS"]),
                        HeadCode = Convert.ToUInt32(row["QSHM"]),
                        Number = Convert.ToUInt16(row["SYZS"]),
                        BuyDate = Convert.ToDateTime(row["LGRQ"])
                    };
                    string str = row["JPGG"].ToString();
                    list.Add(item);
                    list2.Add(str);
                }
            }
            invList = list;
            typeList = list2;
        }

        public bool UpdateSingleVolumn(InvVolumeApp invVolumn, string formatCode)
        {
            this.dict.Clear();
            this.dict.Add("LBDM", invVolumn.TypeCode);
            this.dict.Add("JQSH", ShareMethods.GetVolumnStartNum(invVolumn));
            this.dict.Add("JZZH", ShareMethods.GetVolumnEndNum(invVolumn));
            this.dict.Add("JPGG", formatCode);
            if (this.baseDao.updateSQL("aisino.Fwkp.Fplygl.JPInfo.UpdateSingle", this.dict) != 1)
            {
                return false;
            }
            return true;
        }

        public bool UpdateVolumnList(Dictionary<InvVolumeApp, string> volumnFormat)
        {
            int num = 0;
            foreach (InvVolumeApp app in volumnFormat.Keys)
            {
                if (this.UpdateSingleVolumn(app, volumnFormat[app]))
                {
                    num++;
                }
            }
            if (num != volumnFormat.Count)
            {
                return false;
            }
            return true;
        }
    }
}

