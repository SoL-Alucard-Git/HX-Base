namespace Aisino.Fwkp.Bmgl.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.DAL;
    using Aisino.Fwkp.Bmgl.IBLL;
    using System;

    internal sealed class BMCDManager : IBMCDManager
    {
        private int _currentPage = 1;
        private BMCDManager bmcdDAL = new BMCDManager();

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            return this.bmcdDAL.QueryByKey(KeyWord, pagesize, pageno);
        }

        public AisinoDataSet QueryCD(int pagesize, int pageno)
        {
            return this.bmcdDAL.QueryCD(pagesize, pageno);
        }

        public AisinoDataSet QueryTable(int pagesize, int pageno)
        {
            return this.bmcdDAL.QueryTable(pagesize, pageno);
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                int num;
                string s = PropertyUtil.GetValue("pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 20;
                    PropertyUtil.SetValue("pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("pagesize", value.ToString());
            }
        }
    }
}

