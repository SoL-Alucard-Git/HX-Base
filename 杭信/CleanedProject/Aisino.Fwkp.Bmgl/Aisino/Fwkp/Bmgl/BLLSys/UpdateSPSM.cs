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

    internal sealed class UpdateSPSM : AbstractService
    {
        private ILog log = LogUtil.GetLogger<AddSPSM>();
        private BMSPSMManager spsmManager = new BMSPSMManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length < 8)
            {
                throw new ArgumentException("参数错误,至少有8个参数");
            }
            string oldSZ = param[0] as string;
            string oldBM = param[1] as string;
            string str3 = (param[2] as string).Trim();
            string str4 = (param[3] as string).Trim();
            string str5 = (param[4] as string).Trim();
            string s = (param[5] as string).Trim();
            string str7 = (param[6] as string).Trim();
            string str8 = (param[7] as string).Trim();
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            if (param.Length > 8)
            {
                str9 = (param[8] as string).Trim();
            }
            if (param.Length > 9)
            {
                str10 = (param[9] as string).Trim();
            }
            if (param.Length > 10)
            {
                str11 = (param[10] as string).Trim();
            }
            if (param.Length > 11)
            {
                str12 = (param[11] as string).Trim();
            }
            if (str3.Length == 0)
            {
                MessageBoxHelper.Show("请输入税种名称", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str4.Length == 0)
            {
                MessageBoxHelper.Show("请输入商品税目编码", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return new object[] { "Cancel" };
            }
            if (str5.Length == 0)
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
                SZ = str3,
                MC = str5,
                BM = str4,
                SLV = double.Parse(s),
                ZSL = double.Parse(str7),
                SLJS = byte.Parse(str8),
                JSDW = str9,
                SE = double.Parse(str10),
                MDXS = double.Parse(str11),
                FHDBZ = bool.Parse(str12)
            };
            string str13 = this.spsmManager.ModifyGoodsTax(spsmEntity, oldSZ, oldBM);
            if (str13 == "0")
            {
                str13 = "OK";
                return new object[] { str13 };
            }
            this.log.Info("新增商品税目失败:" + str13);
            return new object[] { "Error:", str13 };
        }
    }
}

