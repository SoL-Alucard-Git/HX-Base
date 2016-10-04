namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SysArgsDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<SysArgsDAL>();

        public void InsertGGLFPMXSysArgs(Hashtable hash)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("MKDM", "PUB");
            dictionary.Add("CSDM", hash["DM"]);
            dictionary.Add("CSNR", hash["NR"]);
            dictionary.Add("XGSJ", DateTime.Now);
            dictionary.Add("XGYH", "a");
            try
            {
                this.baseDAO.updateSQL("aisino.fwkp.bsgl.insertSysArgs", dictionary);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
        }

        public Hashtable SelectCGLFPMXSysArgs()
        {
            Hashtable hashtable = new Hashtable();
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("aisino.fwkp.bsgl.selectSysArgs", null))
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    if (dictionary != null)
                    {
                        hashtable.Add(dictionary["CS_DM"], dictionary["CS_NR"]);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return hashtable;
        }

        public void UpdateCGLFPMXSysArgs(Hashtable hash)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", hash["DM"]);
            dictionary.Add("NR", hash["NR"]);
            try
            {
                this.baseDAO.updateSQL("aisino.fwkp.bsgl.updateSysArgs", dictionary);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
        }
    }
}

