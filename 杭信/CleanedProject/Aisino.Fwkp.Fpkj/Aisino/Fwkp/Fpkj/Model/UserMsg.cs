namespace Aisino.Fwkp.Fpkj.Model
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Startup.Login;
    using System;

    public class UserMsg : DockForm
    {
        private string _dzdh;
        private bool _IAdmin;
        private int _kpjh;
        private string _mc;
        private string _sh;
        private string _yhzh;

        public UserMsg()
        {
            this.Refresh();
            this._mc = (UserInfo.Yhmc == null) ? "" : UserInfo.Yhmc.Trim();
            string str = base.TaxCardInstance.TaxCode;
            this._sh = (str == null) ? "" : str.Trim();
            string str2 = base.TaxCardInstance.Telephone;
            string str3 = base.TaxCardInstance.Address;
            this._dzdh = ((str2 == null) || (str3 == null)) ? "" : (str2.Trim() + " " + str3.Trim());
            string str4 = base.TaxCardInstance.BankAccount;
            this._yhzh = (str4 == null) ? "" : str4.Trim();
            int num = base.TaxCardInstance.Machine;
            this._kpjh = num;
            this._IAdmin = UserInfo.IsAdmin;
            this.Refresh();
        }

        public string DZDH
        {
            get
            {
                return this._dzdh;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this._IAdmin;
            }
        }

        public int KPJH
        {
            get
            {
                return this._kpjh;
            }
        }

        public string MC
        {
            get
            {
                return this._mc;
            }
        }

        public string SH
        {
            get
            {
                return this._sh;
            }
        }

        public string YHZH
        {
            get
            {
                return this._yhzh;
            }
        }
    }
}

