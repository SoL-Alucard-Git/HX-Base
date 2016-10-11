using System;
using System.Collections.Generic;
using InternetWare.Lodging.Data;
using Aisino.Fwkp.Fpkj.Form.FPZF;
using System.Data;

namespace InternetWare.Util.Client
{
    internal class ZuoFeiChaXunClient : BaseClient
    {
        private ZuoFeiChaXunArgs _args;
        public ZuoFeiChaXunClient(ZuoFeiChaXunArgs args)
        {
            _args = args;
        }

        internal override ResultBase DoService()
        {
            FaPiaoZuoFei_YiKai form = new FaPiaoZuoFei_YiKai();
            int TiaojianChaXun = 2;
            int SortWay = 1;
            int sqlType = 0; // 0是编辑，1是修改
            DateTime DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime DateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("ZFBZ", 0);
            dict.Add("KsRq", string.Format("{0} 00:00:00", DateStart.ToString("yyyy-MM-dd")));
            dict.Add("JsRq", string.Format("{0} 23:59:59", DateEnd.ToString("yyyy-MM-dd")));
            dict.Add("FPZL", string.Empty);
            dict.Add("AdminBz", 1);
            dict.Add("Admin", "管理员");
            dict.Add("BSZT", _args.YanQianShiBaiChecked ? 4 : -1);
            dict.Add("FPDM", $"%{_args.MathStr}%");
            dict.Add("GFMC", $"%{_args.MathStr}%");
            dict.Add("GFSH", $"%{_args.MathStr}%");
            dict.Add("FPHM", string.IsNullOrEmpty(_args.MathStr) ? $"%请输入检索关键字...%" : $"%{_args.MathStr}%");
            DataTable table = form.Dal.SelectPage(1, 30, TiaojianChaXun, dict, SortWay, DateTime.Now, -1, sqlType)?.Data;
            return new ResultBase(_args, table, false);
        }
    }
}
