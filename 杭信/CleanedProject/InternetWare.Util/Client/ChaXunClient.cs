using Aisino.Fwkp.Fpkj.Form.FPCX;
using IntetnetWare.Lodging.Args;
using System;
using System.Collections.Generic;
using System.Data;

namespace InternetWare.Util.Client
{
    internal class ChaXunClient : BaseClient
    {
        private ChaXunArgs _args;
        public ChaXunClient(ChaXunArgs args)
        {
            _args = args;
        }

        internal override object DoService()
        {
            FaPiaoChaXun form = new FaPiaoChaXun();
            DateTime DateStart = new DateTime(_args.Year, _args.Month < 1 ? 1 : _args.Month, 1);
            DateTime DateEnd = new DateTime(_args.Year, _args.Month < 1 ? 12 : _args.Month, DateTime.DaysInMonth(_args.Year, _args.Month < 1 ? 12 : _args.Month));
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("KsRq", string.Format("{0} 00:00:00", DateStart.ToString("yyyy-MM-dd")));
            dict.Add("JsRq", string.Format("{0} 23:59:59", DateEnd.ToString("yyyy-MM-dd")));
            dict.Add("FPZL", _args.FPZL);
            dict.Add("AdminBz", 1);
            dict.Add("Admin", "管理员");
            dict.Add("BSZT1", _args.WeiBaoSongChecked ? 0 : -1);
            dict.Add("BSZT2", _args.YanQianShiBaiChecked ? 4 : -1);
            dict.Add("FPDM", $"%{_args.MathStr}%");
            dict.Add("GFMC", $"%{_args.MathStr}%");
            dict.Add("GFSH", $"%{_args.MathStr}%");
            dict.Add("FPHM", string.IsNullOrEmpty(_args.MathStr) ?  $"%请输入检索关键字...%" : $"%{_args.MathStr}%");
            DataTable table = form.xxfpChaXunBll.SelectPage(1, 30, 0, dict, 1, DateTime.Now, -1, 1).Data;
            return table;
        }
    }
}
