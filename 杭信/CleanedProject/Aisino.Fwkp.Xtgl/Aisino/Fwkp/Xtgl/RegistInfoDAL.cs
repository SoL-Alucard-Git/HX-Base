namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    public class RegistInfoDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<RegistInfoDAL>();

        public List<VersionInfo> SelectRegistFileName()
        {
            List<VersionInfo> list = new List<VersionInfo>();
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("Aisino.Fwkp.Xtgl.SelectRegistFileName", null))
                {
                    Dictionary<string, object> dictionary = obj2 as Dictionary<string, object>;
                    if (dictionary != null)
                    {
                        VersionInfo item = new VersionInfo();
                        item.Type = dictionary["BBZL"].ToString();
                        item.Code = dictionary["DM"].ToString();
                        item.Sign = dictionary["BBBS"].ToString();
                        item.Name = dictionary["MC"].ToString();
                        item.Description = dictionary["SM"].ToString();
                        if ((dictionary["DCBZ"] != null) && (dictionary["DCBZ"].ToString() != ""))
                        {
                            item.ExportFlag = Convert.ToBoolean(dictionary["DCBZ"].ToString());
                        }
                        list.Add(item);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.UserMessage + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public void UpdateRegFileName(string file, string verFlag)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("ZCWJ", file);
            dictionary.Add("BBBS", verFlag);
            try
            {
                this.baseDAO.未确认DAO方法2_疑似updateSQL("Aisino.Fwkp.Xtgl.UpdateRegistFileName", dictionary);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.UserMessage + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
        }
    }
}

