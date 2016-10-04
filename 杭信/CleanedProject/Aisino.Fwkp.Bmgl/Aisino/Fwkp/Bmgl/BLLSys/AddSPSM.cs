namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Model;
    using log4net;
    using System;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;

    internal sealed class AddSPSM : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddSPSM>();
        private BMSPSMManager spsmManager = new BMSPSMManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 6)
            {
                throw new ArgumentException("参数错误,至少有6个参数");
            }
            string str = (param[0] as string).Trim();
            string str2 = (param[1] as string).Trim();
            string str3 = (param[2] as string).Trim();
            string s = (param[3] as string).Trim();
            string str5 = (param[4] as string).Trim();
            string str6 = (param[5] as string).Trim();
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            if (param.Length > 6)
            {
                str7 = (param[6] as string).Trim();
            }
            if (param.Length > 7)
            {
                str8 = (param[7] as string).Trim();
            }
            if (param.Length > 8)
            {
                str9 = (param[8] as string).Trim();
            }
            if (param.Length > 9)
            {
                str10 = (param[9] as string).Trim();
            }
            if (str.Length == 0)
            {
                MessageBoxHelper.Show("请输入税种名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str2.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目编码", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str3.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (s.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目税率", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            BMSPSMModel spsmEntity = new BMSPSMModel {
                SZ = str,
                MC = str3,
                BM = str2,
                SLV = double.Parse(s),
                ZSL = double.Parse(str5),
                SLJS = byte.Parse(str6),
                JSDW = str7,
                SE = double.Parse(str8),
                MDXS = double.Parse(str9),
                FHDBZ = bool.Parse(str10)
            };
            string str11 = this.spsmManager.AddGoodsTaxKP(spsmEntity, 1);
            if (str11 == "0")
            {
                str11 = "OK";
                return new object[] { str11 };
            }
            this.log.Info("新增商品税目失败:" + str11);
            return new object[] { "Error:", str11 };
        }
    }
}

