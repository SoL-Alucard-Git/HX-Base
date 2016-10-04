namespace Aisino.Fwkp.Fpkj.Entry.ZDCSEntry
{
    using Aisino.Fwkp.Fpkj.Form.FPCX;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public class ZDCSEntry
    {
        private int _Month;
        private int _Year;
        private ILog loger = LogUtil.GetLogger<Aisino.Fwkp.Fpkj.Entry.ZDCSEntry.ZDCSEntry>();

        public bool FaPiaoChaXun()
        {
            try
            {
                new SelectTime().ShowDialog();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
            return true;
        }

        public void SetYearMonth(int Year, int Month)
        {
            try
            {
                this._Month = Month;
                this._Year = Year;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }
    }
}

