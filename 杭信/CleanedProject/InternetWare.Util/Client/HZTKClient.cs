
using Aisino.Framework.Plugin.Core.Controls;
using Aisino.Fwkp.Hzfp;
using InternetWare.Lodging.Data;
using System.Collections.Generic;

namespace InternetWare.Util.Client
{
    internal class HZTKClient : BaseClient
    {
        private InternetWare.Lodging.Data.HZTKArgs _args;
        public HZTKClient(InternetWare.Lodging.Data.HZTKArgs args)
        {
            _args = args;
        }

        internal override ResultBase DoService()
        {
            ResultBase result = new ResultBase();
            //switch (_args.hztype)
            //{
            //    case HZType.GFYDK:
            //        result = GFYDKMethod();
            //        break;
            //    case HZType.GFWDK:
            //        result = GFWDKMethod();
            //        break;
            //    case HZType.XF:
            //        result = XFHZMethod();
            //        break;
            //}
            getsplist();
            return result;
        }

        #region 获取销售方名称列表
        private AisinoDataSet getxfmc()
        {
            Aisino.Fwkp.Bmgl.Forms.BMKHSelect selet = new Aisino.Fwkp.Bmgl.Forms.BMKHSelect();
            AisinoDataSet dataset = selet.GetKHData("",10, 1);
            return dataset;
        }
        #endregion

        #region 获取货物信息列表
        private AisinoDataSet getsplist()
        {
            Aisino.Fwkp.Bmgl.Forms.BMSPSelect selet = new Aisino.Fwkp.Bmgl.Forms.BMSPSelect("",-1.0,0,"");
            AisinoDataSet dataset = selet.GetSPData("", 10, 1);
            return dataset;
        }
        #endregion

        #region 销售方红字
        private ResultBase XFHZMethod()
        {
            //初始化
            Aisino.Fwkp.Hzfp.Form.SqdTianKai sqdtiankai = new Aisino.Fwkp.Hzfp.Form.SqdTianKai();
            List<object> selectinfo = new List<object>() { "3100153130", "28046319", "s", "0000000110", "1" };
            sqdtiankai.InitSqdMx(Aisino.Fwkp.Hzfp.Form.InitSqdMxType.Add, selectinfo);
            //购方信息
            sqdtiankai.inv.Gfmc = "北京东方威力股份有限公司";
            sqdtiankai.inv.Gfsh = "110102251328333";
            //货物信息
            object[] hwxinxi = new object[] { "021", "条码打印纸", "", 0.17, "", "", "", 0.1, false, System.DBNull.Value, false, "106010502", "否", "", System.DBNull.Value, "" };
            sqdtiankai.spmcbt(hwxinxi);
            sqdtiankai.inv.SetSL(0, "-6");
            //保存
            sqdtiankai.dayingbt();
            return new ResultBase();
        }
        #endregion

        #region 购方申请未抵扣
        private ResultBase GFWDKMethod()
        {
            //初始化
            Aisino.Fwkp.Hzfp.Form.SqdTianKai sqdtiankai = new Aisino.Fwkp.Hzfp.Form.SqdTianKai();
            List<object> selectinfo = new List<object>() { "3100153130", "28046316", "s", "1011000000", "0" };
            sqdtiankai.InitSqdMx(Aisino.Fwkp.Hzfp.Form.InitSqdMxType.Add, selectinfo);
            //销售方信息
            sqdtiankai.inv.Xfmc = "北京畅联电子有限公司";
            sqdtiankai.inv.Xfsh = "110101251328321";
            //货物信息
            object[] hwxinxi = new object[] { "021", "条码打印纸", "", 0.17, "", "", "", 0.1, false, System.DBNull.Value, false, "106010502", "否", "", System.DBNull.Value, "" };
            sqdtiankai.spmcbt(hwxinxi);
            sqdtiankai.inv.SetSL(0, "-3");
            //保存
            sqdtiankai.dayingbt();
            return new ResultBase();
        }
        #endregion

        #region 购方申请已抵扣
        private ResultBase GFYDKMethod()
        {
            //初始化
            Aisino.Fwkp.Hzfp.Form.SqdTianKai sqdtiankai = new Aisino.Fwkp.Hzfp.Form.SqdTianKai();
            List<object> selectinfo = new List<object>() { "", "", "s", "1100000000", "0" };
            sqdtiankai.InitSqdMx(Aisino.Fwkp.Hzfp.Form.InitSqdMxType.Add, selectinfo);
            //销售方信息
            sqdtiankai.inv.Xfmc = "北京畅联电子有限公司";
            sqdtiankai.inv.Xfsh = "110101251328321";
            //货物信息
            object[] hwxinxi = new object[] { "021", "条码打印纸", "", 0.17, "", "", "", 0.1, false, System.DBNull.Value, false, "106010502", "否", "", System.DBNull.Value, "" };
            sqdtiankai.spmcbt(hwxinxi);
            sqdtiankai.inv.SetSL(0, "-2");
            //保存
            sqdtiankai.dayingbt();
            return new ResultBase();
        }
        #endregion
    }
}
