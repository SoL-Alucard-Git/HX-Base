namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.DAL;
    using System;

    internal class DJZKHZbll
    {
        private int _currentPage = 1;
        private DJHBdal djhbDAL = new DJHBdal();
        private DJZKHZdal hzDAL = new DJZKHZdal();

        public AisinoDataSet MXAfterZKHuiZong(int pagesize, int pageno)
        {
            return this.djhbDAL.GetMingXi(pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJ(string DJMonth, string DJType, string KeyWord, int pagesize, int pageno, int type)
        {
            return this.hzDAL.QueryXSDJ(DJMonth, DJType, KeyWord, pagesize, pageno, type);
        }

        public AisinoDataSet QueryXSDJMX(string SelectedBH, int pagesize, int pageno)
        {
            return this.hzDAL.QueryXSDJMX(SelectedBH, pagesize, pageno);
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
                return PropValue.Pagesize;
            }
            set
            {
                PropValue.Pagesize = value;
            }
        }
    }
}

