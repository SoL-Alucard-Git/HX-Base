namespace Aisino.Fwkp.Wbjk.Model
{
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Data;

    public class WenBenItem
    {
        public static DataTable Items()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("key");
            table.Columns.Add("ShuJuXiang");
            table.Columns.Add("BiXuanShuXin");
            table.Rows.Add(new object[] { 1, "DanJuHaoMa", "单据号码", "必选" });
            table.Rows.Add(new object[] { 2, "GouFangMingCheng", "购方名称", "必选" });
            table.Rows.Add(new object[] { 3, "GouFangShuiHao", "购方税号", "非必选" });
            table.Rows.Add(new object[] { 4, "GouFangDiZhiDianHua", "购方地址电话", "非必选" });
            table.Rows.Add(new object[] { 5, "GouFangYinHangZhangHao", "购方银行账号", "非必选" });
            table.Rows.Add(new object[] { 6, "BeiZhu", "备注", "非必选" });
            table.Rows.Add(new object[] { 7, "FuHeRen", "复核人", "非必选" });
            table.Rows.Add(new object[] { 8, "ShouKuanRen", "收款人", "非必选" });
            table.Rows.Add(new object[] { 9, "QingDanHangShangPinMingCheng", "清单商品名称", "非必选" });
            table.Rows.Add(new object[] { 10, "DanJuRiQi", "单据日期", "非必选" });
            table.Rows.Add(new object[] { 11, "XiaoFangYinHangZhangHao", "销方银行账号", "非必选" });
            table.Rows.Add(new object[] { 12, "XiaoFangDiZhiDianHua", "销方地址电话", "非必选" });
            table.Rows.Add(new object[] { 13, "ShenFenZhengJiaoYan", "身份证校验标志", "非必选" });
            table.Rows.Add(new object[] { 14, "HaiYangShiYou", "中外合作油气田标志", "非必选" });
            table.Rows.Add(new object[] { 15, "HuoWuMingCheng", "货物名称", "必选" });
            table.Rows.Add(new object[] { 0x10, "JiLiangDanWei", "计量单位", "非必选" });
            table.Rows.Add(new object[] { 0x11, "GuiGe", "规格型号", "非必选" });
            table.Rows.Add(new object[] { 0x12, "ShuLiang", "数量", "非必选" });
            table.Rows.Add(new object[] { 0x13, "BuHanShuiJinE", "金额", "非必选" });
            table.Rows.Add(new object[] { 20, "ShuiLv", "税率", "非必选" });
            table.Rows.Add(new object[] { 0x15, "ShangPinShuiMu", "商品税目", "非必选" });
            table.Rows.Add(new object[] { 0x16, "ZheKouJinE", "折扣金额", "非必选" });
            table.Rows.Add(new object[] { 0x17, "ShuiE", "税额", "非必选" });
            table.Rows.Add(new object[] { 0x18, "ZheKouShuiE", "折扣税额", "非必选" });
            table.Rows.Add(new object[] { 0x19, "ZheKouLv", "折扣率", "非必选" });
            table.Rows.Add(new object[] { 0x1a, "DanJia", "单价", "非必选" });
            table.Rows.Add(new object[] { 0x1b, "JiaGeFangShi", "价格方式", "非必选" });
            table.Rows.Add(new object[] { 0x1c, "ShouPiaoFangMC", "实际受票方名称", "非必选" });
            table.Rows.Add(new object[] { 0x1d, "ShouPiaoFangSH", "实际受票方税号", "非必选" });
            table.Rows.Add(new object[] { 30, "ShouHuoRenMC", "收货人名称", "非必选" });
            table.Rows.Add(new object[] { 0x1f, "ShouHuoRenSH", "收货人税号", "非必选" });
            table.Rows.Add(new object[] { 0x20, "FaHuoRenMC", "发货人名称", "非必选" });
            table.Rows.Add(new object[] { 0x21, "FaHuoRenSH", "发货人税号", "非必选" });
            table.Rows.Add(new object[] { 0x22, "ShuiLv-HY", "税率-货运", "非必选" });
            table.Rows.Add(new object[] { 0x23, "QiYouDaoDa", "起运地、经由、到达地", "非必选" });
            table.Rows.Add(new object[] { 0x24, "CheChongCheHao", "车种车号", "非必选" });
            table.Rows.Add(new object[] { 0x25, "CheChuanDunWei", "车船吨位", "非必选" });
            table.Rows.Add(new object[] { 0x26, "YunShuHuoWuXX", "运输货物信息", "非必选" });
            table.Rows.Add(new object[] { 0x27, "BeiZhu-HY", "备注-货运", "非必选" });
            table.Rows.Add(new object[] { 40, "FuHeRen-HY", "复核人-货运", "非必选" });
            table.Rows.Add(new object[] { 0x29, "ShouKuanRen-HY", "收款人-货运", "非必选" });
            table.Rows.Add(new object[] { 0x2a, "DanJuRiQi-HY", "单据日期-货运", "非必选" });
            table.Rows.Add(new object[] { 0x2b, "HuoWuMingCheng-HY", "费用项目", "非必选" });
            table.Rows.Add(new object[] { 0x2c, "JinE-HY", "金额-货运", "非必选" });
            table.Rows.Add(new object[] { 0x2d, "GouHuoDanWei", "购货单位（人）", "非必选" });
            table.Rows.Add(new object[] { 0x2e, "ShenFenZhengHaoMa", "身份证号码/组织机构代码", "非必选" });
            table.Rows.Add(new object[] { 0x2f, "JinE-JDC", "价税合计", "非必选" });
            table.Rows.Add(new object[] { 0x30, "ShuiLv-JDC", "增值税税率或征收率", "非必选" });
            table.Rows.Add(new object[] { 0x31, "CheLiangLeiXing", "车辆类型", "非必选" });
            table.Rows.Add(new object[] { 50, "ChangPaiXingHao", "厂牌型号", "非必选" });
            table.Rows.Add(new object[] { 0x33, "ChanDi", "产地", "非必选" });
            table.Rows.Add(new object[] { 0x34, "ShengChanChangJiaMC", "生产企业名称", "非必选" });
            table.Rows.Add(new object[] { 0x35, "HeGeZhengHao", "合格证号", "非必选" });
            table.Rows.Add(new object[] { 0x36, "JinKouZhengMingShuHao", "进口证明书号", "非必选" });
            table.Rows.Add(new object[] { 0x37, "ShangJianDanHao", "商检单号", "非必选" });
            table.Rows.Add(new object[] { 0x38, "FaDongJiHaoMa", "发动机号码", "非必选" });
            table.Rows.Add(new object[] { 0x39, "CheLiangShiBieDM", "车辆识别代号/车架号码", "非必选" });
            table.Rows.Add(new object[] { 0x3a, "DianHua", "电话", "非必选" });
            table.Rows.Add(new object[] { 0x3b, "ZhangHao", "账号", "非必选" });
            table.Rows.Add(new object[] { 60, "DiZhi", "地址", "非必选" });
            table.Rows.Add(new object[] { 0x3d, "KaiHuYinHang", "开户银行", "非必选" });
            table.Rows.Add(new object[] { 0x3e, "DunWei", "吨位", "非必选" });
            table.Rows.Add(new object[] { 0x3f, "XianChengRenShu", "限乘人数", "非必选" });
            table.Rows.Add(new object[] { 0x40, "DanJuRiQi-JDC", "单据日期-机动车", "非必选" });
            table.Rows.Add(new object[] { 0x41, "BeiZhu-JDC", "备注-机动车", "非必选" });
            table.Rows.Add(new object[] { 0x42, "NaShuiRenShiBieHao", "纳税人识别号", "非必选" });
            if (CommonTool.isCEZS())
            {
                table.Rows.Add(new object[] { 0x48, "KouChuE", "扣除额(差额征税)", "非必选" });
            }
            return table.DefaultView.ToTable();
        }

        public class MapItem
        {
            public string BiXuanShuXin = "";
            public int ID = 0;
            public string Key = "";
            public string ShuJuXiang = "";
        }

        public class YingShe
        {
            public int column = 0;
            public string key = string.Empty;
            public int table = 0;
        }
    }
}

