namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DJZFCRdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        public ArrayList GetBillState(string strBillNum)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", strBillNum);
            return this.baseDAO.querySQL("aisino.Fwkp.Wbjk.QueryStateXSDJ", dictionary);
        }

        public int MakeBillInvalid(string strBillNum)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", strBillNum);
            return this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.UpdateStateXSDJ", dictionary);
        }

        public bool RecordIsExist(string strBillNum)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("BH", strBillNum);
            return (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Wbjk.RecordIsExistXSDJ", dictionary) > 0);
        }
    }
}

