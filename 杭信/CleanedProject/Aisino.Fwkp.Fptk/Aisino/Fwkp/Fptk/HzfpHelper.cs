namespace Aisino.Fwkp.Fptk
{
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Text;

    public class HzfpHelper
    {
        public static string GetInvMainInfo(FPLX fplx, Fpxx blueFpxx)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if (((((int)fplx == 2) || ((int)fplx == 0x33)) || ((int)fplx == 0x29)) && (blueFpxx.Zyfplx == (ZYFP_LX)9))
            {
                str2 = "销方名称：";
                str3 = "销方税号：";
                str4 = "销方地址电话：";
                str5 = "销方银行账号：";
            }
            else
            {
                str2 = "购方名称：";
                str3 = "购方税号：";
                str4 = "购方地址电话：";
                str5 = "购方银行账号：";
            }
            if ((int)fplx <= 11)
            {
                if (((int)fplx != 0) && ((int)fplx != 2))
                {
                    if ((int)fplx != 11)
                    {
                        return str;
                    }
                    return new StringBuilder().Append("类别代码：").Append(blueFpxx.fpdm).Append(Environment.NewLine).Append("发票号码：").Append(blueFpxx.fphm).Append(Environment.NewLine).Append("实际受票方名称：").Append(blueFpxx.spfmc).Append(Environment.NewLine).Append("实际受票方税号：").Append(blueFpxx.spfnsrsbh).Append(Environment.NewLine).Append("收货人名称：").Append(blueFpxx.shrmc).Append(Environment.NewLine).Append("收货人税号：").Append(blueFpxx.shrnsrsbh).Append(Environment.NewLine).Append("发货人名称：").Append(blueFpxx.fhrmc).Append(Environment.NewLine).Append("发货人税号：").Append(blueFpxx.fhrnsrsbh).Append(Environment.NewLine).Append("车种车号：").Append(blueFpxx.czch).Append(Environment.NewLine).Append("车船吨位：").Append(blueFpxx.ccdw).Append(Environment.NewLine).Append("主管税务机关名称：").Append(blueFpxx.zgswjgmc).Append(Environment.NewLine).Append("主管税务机关代码：").Append(blueFpxx.zgswjgdm).Append(Environment.NewLine).Append("合计金额（不含税）：").Append(blueFpxx.je).Append(Environment.NewLine).Append("合计税额：").Append(blueFpxx.se).Append(Environment.NewLine).Append("开票日期：").Append(blueFpxx.kprq).Append(Environment.NewLine).Append("开票人：").Append(blueFpxx.kpr).Append(Environment.NewLine).Append("作废标志：").Append(blueFpxx.zfbz ? "是" : "否").ToString();
                }
            }
            else
            {
                if ((int)fplx == 12)
                {
                    string str6 = decimal.Add(decimal.Parse(blueFpxx.je), decimal.Parse(blueFpxx.se)).ToString("F2");
                    string str7 = "";
                    if (blueFpxx.isNewJdcfp)
                    {
                        str7 = string.Format("纳税人识别号：{0}", blueFpxx.gfsh) + Environment.NewLine;
                    }
                    return new StringBuilder().Append("类别代码：").Append(blueFpxx.fpdm).Append(Environment.NewLine).Append("发票号码：").Append(blueFpxx.fphm).Append(Environment.NewLine).Append("购货单位(人)：").Append(blueFpxx.gfmc).Append(Environment.NewLine).Append(str7).Append("身份证号码/组织机构代码：").Append(blueFpxx.sfzhm).Append(Environment.NewLine).Append("车辆类型：").Append(blueFpxx.cllx).Append(Environment.NewLine).Append("厂牌型号：").Append(blueFpxx.cpxh).Append(Environment.NewLine).Append("产地：").Append(blueFpxx.cd).Append(Environment.NewLine).Append("合格证号：").Append(blueFpxx.hgzh).Append(Environment.NewLine).Append("进口证明书号：").Append(blueFpxx.jkzmsh).Append(Environment.NewLine).Append("商检单号：").Append(blueFpxx.sjdh).Append(Environment.NewLine).Append("发动机号码：").Append(blueFpxx.fdjhm).Append(Environment.NewLine).Append("车辆识别代号/车架号码：").Append(blueFpxx.clsbdh).Append(Environment.NewLine).Append("吨位：").Append(blueFpxx.dw).Append(Environment.NewLine).Append("限乘人数：").Append(blueFpxx.xcrs).Append(Environment.NewLine).Append("主管税务机关名称：").Append(blueFpxx.zgswjgmc).Append(Environment.NewLine).Append("主管税务机关代码：").Append(blueFpxx.zgswjgdm).Append(Environment.NewLine).Append("合计金额（不含税）：").Append(blueFpxx.je).Append(Environment.NewLine).Append("价税合计：").Append(str6).Append(Environment.NewLine).Append("合计税额：").Append(blueFpxx.se).Append(Environment.NewLine).Append("开票日期：").Append(blueFpxx.kprq).Append(Environment.NewLine).Append("开票人：").Append(blueFpxx.kpr).Append(Environment.NewLine).Append("作废标志：").Append(blueFpxx.zfbz ? "是" : "否").ToString();
                }
                if ((int)fplx == 0x29)
                {
                    return new StringBuilder().Append("类别代码：").Append(blueFpxx.fpdm).Append(Environment.NewLine).Append("发票号码：").Append(blueFpxx.fphm).Append(Environment.NewLine).Append(str2).Append(blueFpxx.gfmc).Append(Environment.NewLine).Append(str3).Append(blueFpxx.gfsh).Append(Environment.NewLine).Append("合计金额：").Append(blueFpxx.je).Append(Environment.NewLine).Append("合计税额：").Append(blueFpxx.se).Append(Environment.NewLine).Append("开票日期：").Append(blueFpxx.kprq).Append(Environment.NewLine).Append("收款员：").Append(blueFpxx.skr).Append(Environment.NewLine).Append("作废标志：").Append(blueFpxx.zfbz ? "是" : "否").Append(Environment.NewLine).ToString();
                }
                if ((int)fplx != 0x33)
                {
                    return str;
                }
            }
            return new StringBuilder().Append("类别代码：").Append(blueFpxx.fpdm).Append(Environment.NewLine).Append("发票号码：").Append(blueFpxx.fphm).Append(Environment.NewLine).Append(str2).Append(blueFpxx.gfmc).Append(Environment.NewLine).Append(str3).Append(blueFpxx.gfsh).Append(Environment.NewLine).Append(str4).Append(blueFpxx.gfdzdh).Append(Environment.NewLine).Append(str5).Append(blueFpxx.gfyhzh).Append(Environment.NewLine).Append("合计金额（不含税）：").Append(blueFpxx.je).Append(Environment.NewLine).Append("合计税额：").Append(blueFpxx.se).Append(Environment.NewLine).Append("开票日期：").Append(blueFpxx.kprq).Append(Environment.NewLine).Append("开票人：").Append(blueFpxx.kpr).Append(Environment.NewLine).Append("作废标志：").Append(blueFpxx.zfbz ? "是" : "否").Append(Environment.NewLine).ToString();
        }
    }
}

