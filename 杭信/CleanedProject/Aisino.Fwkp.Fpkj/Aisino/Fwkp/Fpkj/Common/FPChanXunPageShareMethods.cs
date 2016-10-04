namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Framework.Plugin.Core.Util;
    internal sealed class FPChanXunPageShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPChanXunPageShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray2;
            XXFP xxfp = new XXFP(false);
            object[] objArray = new object[1];
            Dictionary<string, object> condition = new Dictionary<string, object>();
            try
            {
                if ((param == null) || (param.Length < 2))
                {
                    this.loger.Debug("发票上传下载传递参数个数有误！");
                    condition = null;
                    return null;
                }
                int result = 1;
                int num2 = 100;
                string str = "0";
                string str2 = "3";
                int num3 = 0;
                int.TryParse(param[0].ToString(), out result);
                int.TryParse(param[1].ToString(), out num2);
                if (param.Length >= 3)
                {
                    str = (param[2] == null) ? "0" : param[2].ToString();
                }
                if (param.Length >= 4)
                {
                    str2 = (param[3] == null) ? "3" : param[3].ToString();
                }
                num3 = 0;
                if (param.Length >= 5)
                {
                    num3 = Tool.ObjectToInt((param[4] == null) ? "0" : param[4].ToString());
                }
                switch (num3)
                {
                    case 0:
                    {
                        if (result <= 0)
                        {
                            result = 1;
                        }
                        if (num2 <= 0)
                        {
                            num2 = 100;
                        }
                        this.loger.Error(string.Concat(new object[] { "上传下载传入参数,传入参数：PageNo: ", result.ToString(), "PageSize: ", num2 }));
                        string str3 = (((result - 1) * num2) + 1).ToString();
                        string str4 = (result * num2).ToString();
                        condition.Add("beginNO", str3);
                        condition.Add("endNO", str4);
                        condition.Add("BSZT1", str);
                        condition.Add("BSZT2", str2);
                        List<Fpxx> list = new XXFP(false).SelectFpxxPage(condition);
                        this.loger.Error("上传下载传入参数,实际查询票数：" + ((list == null) ? "0" : list.Count.ToString()));
                        objArray[0] = list;
                        return objArray;
                    }
                    case 1:
                    {
                        Fpxx fpxx = xxfp.GetModel(param[0].ToString(), param[1].ToString(), Tool.ObjectToInt(param[2]), "");
                        objArray[0] = fpxx;
                        return objArray;
                    }
                    case 2:
                    {
                        List<Fpxx> list2 = new XXFP(false).SelectFpxxPatch(param[5].ToString());
                        this.loger.Error("上传下载传入参数,实际查询票数：" + ((list2 == null) ? "0" : list2.Count.ToString()));
                        objArray[0] = list2;
                        return objArray;
                    }
                }
                objArray2 = null;
            }
            catch (Exception exception)
            {
                this.loger.Error("Excepiton:" + exception.ToString());
                objArray2 = objArray;
            }
            finally
            {
                xxfp = null;
                condition = null;
            }
            return objArray2;
        }
    }
}

