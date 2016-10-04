namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.BLLSys;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Forms;

    public sealed class BMSPFLManagerEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<BMSPFLManagerEntry>();
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        protected override void RunCommand()
        {
            base.ShowForm<BMSPFL>();
        }

        protected override bool SetValid()
        {
            if (!Flbm.IsYM())
            {
                return false;
            }
            try
            {
                try
                {
                    string str = "aisino.Fwkp.Bmgl.BMSPFL.BMSPFLISEMPTY";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    if (this.baseDAO.queryValueSQL<int>(str, dictionary) == 0)
                    {
                        string codeFile = Path.Combine(PropertyUtil.GetValue("MAIN_PATH") + @"\Bin", "spfwssflbm.xml");
                        new BMSPFLManager().AutoImportDatabase(codeFile);
                    }
                    this.baseDAO.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Bmgl.BMSP.ModifyBMSPLSLVBSbyYHZCMC", dictionary);
                }
                catch (Exception exception)
                {
                    this._Loger.Debug(exception.ToString());
                }
                return true;
            }
            catch (BaseException exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            catch (Exception exception3)
            {
                this._Loger.Error(exception3.Message);
                ExceptionHandler.HandleError(exception3);
                return false;
            }
        }
    }
}

