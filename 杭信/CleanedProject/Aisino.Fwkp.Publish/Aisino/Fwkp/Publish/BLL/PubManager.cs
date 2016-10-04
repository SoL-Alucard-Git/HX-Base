namespace Aisino.Fwkp.Publish.BLL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.PubData.Message_S2C;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    internal class PubManager : IPubManager
    {
        private ILog log = LogUtil.GetLogger<PubManager>();

        public HtmlMessage QueryPub(string xh)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("XH", xh);
                Dictionary<string, object> dictionary2 = BaseDAOFactory.GetBaseDAOSQLite().querySQL("pubQueryOne", dictionary)[0] as Dictionary<string, object>;
                HtmlMessage message = new HtmlMessage(dictionary2["xh"].ToString(), dictionary2["bt"].ToString(), dictionary2["nr"].ToString(), dictionary2["lx"].ToString(), 10);
                this.log.Info("查询公告明细信息");
                return message;
            }
            catch (Exception exception)
            {
                this.log.Error("查询公告明细信息异常：" + exception.ToString());
                return null;
            }
        }

        public AisinoDataSet QueryPub(string start, string end, int pageSize, int pageNo)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("START", start);
                dictionary.Add("END", end);
                this.log.Info("查询公告信息列表");
                return BaseDAOFactory.GetBaseDAOSQLite().querySQLDataSet("pubQuery", dictionary, pageSize, pageNo);
            }
            catch (Exception exception)
            {
                this.log.Error("查询公告信息列表异常：" + exception.ToString());
                return null;
            }
        }

        public bool SavePub(HtmlMessage mess)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("XH", mess.Id);
                dictionary.Add("BT", mess.Title);
                dictionary.Add("NR", mess.Message);
                dictionary.Add("LX", mess.Type);
                int num = BaseDAOFactory.GetBaseDAOSQLite().未确认DAO方法2_疑似updateSQL("pubSave", dictionary);
                this.log.Info("保存公告信息");
                return (num == 1);
            }
            catch (Exception exception)
            {
                this.log.Error("保存公告信息异常：" + exception.ToString());
                return false;
            }
        }
    }
}

