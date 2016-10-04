namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.IO;

    public class CreateIniFile
    {
        public static void Create()
        {
            string path = ConfigFile.GetIniConfigPath.Remove(ConfigFile.GetIniConfigPath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "[File]", "File1Path=", "File2Path=", "TableInFile1=Sheet1", "TableInFile2=Sheet1", "[FieldCon]", "FileNumber=1", "IsSeted=0", "Invtype=Common", "DanJuHaoMa=1.1", "GouFangMingCheng=0.0", "GouFangShuiHao=0.0", "GouFangDiZhiDianHua=0.0", "GouFangYinHangZhangHao=0.0", "BeiZhu=0.0", "FuHeRen=0.0", 
                "ShouKuanRen=0.0", "QingDanHangShangPinMingCheng=0.0", "DanJuRiQi=0.0", "XiaoFangYinHangZhangHao=0.0", "XiaoFangDiZhiDianHua=0.0", "HuoWuMingCheng=0.0", "JiLiangDanWei=0.0", "GuiGe=0.0", "ShuLiang=0.0", "BuHanShuiJinE=0.0", "ShuiLv=0.0", "ShangPinShuiMu=0.0", "ZheKouJinE=0.0", "ShuiE=0.0", "ZheKouShuiE=0.0", "ZheKouLv=0.0", 
                "DanJia=0.0", "DefaultFuHeRen=", "DefaultShouKuanRen=", "DefaultShuiLv=11", "ShenFenZhengJiaoYan=0.0", "HaiYangShiYou=0.0", "JiaGeFangShi=0.0", "ShouPiaoFangMC=0.0", "ShouPiaoFangSH=0.0", "ShouHuoRenMC=0.0", "ShouHuoRenSH=0.0", "FaHuoRenMC=0.0", "FaHuoRenSH=0.0", "ShuiLv-HY=0.0", "QiYouDaoDa=0.0", "CheChongCheHao=0.0", 
                "CheChuanDunWei=0.0", "YunShuHuoWuXX=0.0", "BeiZhu-HY=0.0", "FuHeRen-HY=0.0", "ShouKuanRen-HY=0.0", "DanJuRiQi-HY=0.0", "HuoWuMingCheng-HY=0.0", "JinE-HY=0.0", "GouHuoDanWei=0.0", "ShenFenZhengHaoMa=0.0", "JinE-JDC=0.0", "ShuiLv-JDC=0.0", "CheLiangLeiXing=0.0", "ChangPaiXingHao=0.0", "ChanDi=0.0", "ShengChanChangJiaMC=0.0", 
                "HeGeZhengHao=0.0", "JinKouZhengMingShuHao=0.0", "ShangJianDanHao=0.0", "FaDongJiHaoMa=0.0", "CheLiangShiBieDM=0.0", "DianHua=0.0", "ZhangHao=0.0", "DiZhi=0.0", "KaiHuYinHang=0.0", "DunWei=0.0", "XianChengRenShu=0.0", "DanJuRiQi-JDC=0.0", "BeiZhu-JDC=0.0", "NaShuiRenShiBieHao=0.0", "XiaoFangMingCheng=0.0", "XiaoFangShuiHao=0.0", 
                "NongChanPinBiaoZhi=0.0", "[TableCon]", "MainTableField=1", "AssistantTableField=1", "MainTableIgnoreRow=1", "AssistantTableIgnoreRow=1"
             };
            File.WriteAllLines(ConfigFile.GetIniConfigPath, contents, ToolUtil.GetEncoding());
        }

        public static void Create_1()
        {
            string path = ConfigFile.GetIniConfigPath.Remove(ConfigFile.GetIniConfigPath_1.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "[File]", "File1Path=", "File2Path=", "TableInFile1=Sheet1", "TableInFile2=Sheet1", "[FieldCon]", "FileNumber=1", "IsSeted=0", "Invtype=Special", "DanJuHaoMa=1.1", "GouFangMingCheng=0.0", "GouFangShuiHao=0.0", "GouFangDiZhiDianHua=0.0", "GouFangYinHangZhangHao=0.0", "BeiZhu=0.0", "FuHeRen=0.0", 
                "ShouKuanRen=0.0", "QingDanHangShangPinMingCheng=0.0", "DanJuRiQi=0.0", "XiaoFangYinHangZhangHao=0.0", "XiaoFangDiZhiDianHua=0.0", "HuoWuMingCheng=0.0", "JiLiangDanWei=0.0", "GuiGe=0.0", "ShuLiang=0.0", "BuHanShuiJinE=0.0", "ShuiLv=0.0", "ShangPinShuiMu=0.0", "ZheKouJinE=0.0", "ShuiE=0.0", "ZheKouShuiE=0.0", "ZheKouLv=0.0", 
                "DanJia=0.0", "DefaultFuHeRen=", "DefaultShouKuanRen=", "DefaultShuiLv=11", "ShenFenZhengJiaoYan=0.0", "HaiYangShiYou=0.0", "JiaGeFangShi=0.0", "ShouPiaoFangMC=0.0", "ShouPiaoFangSH=0.0", "ShouHuoRenMC=0.0", "ShouHuoRenSH=0.0", "FaHuoRenMC=0.0", "FaHuoRenSH=0.0", "ShuiLv-HY=0.0", "QiYouDaoDa=0.0", "CheChongCheHao=0.0", 
                "CheChuanDunWei=0.0", "YunShuHuoWuXX=0.0", "BeiZhu-HY=0.0", "FuHeRen-HY=0.0", "ShouKuanRen-HY=0.0", "DanJuRiQi-HY=0.0", "HuoWuMingCheng-HY=0.0", "JinE-HY=0.0", "GouHuoDanWei=0.0", "ShenFenZhengHaoMa=0.0", "JinE-JDC=0.0", "ShuiLv-JDC=0.0", "CheLiangLeiXing=0.0", "ChangPaiXingHao=0.0", "ChanDi=0.0", "ShengChanChangJiaMC=0.0", 
                "HeGeZhengHao=0.0", "JinKouZhengMingShuHao=0.0", "ShangJianDanHao=0.0", "FaDongJiHaoMa=0.0", "CheLiangShiBieDM=0.0", "DianHua=0.0", "ZhangHao=0.0", "DiZhi=0.0", "KaiHuYinHang=0.0", "DunWei=0.0", "XianChengRenShu=0.0", "DanJuRiQi-JDC=0.0", "BeiZhu-JDC=0.0", "NaShuiRenShiBieHao=0.0", "XiaoFangMingCheng=0.0", "XiaoFangShuiHao=0.0", 
                "NongChanPinBiaoZhi=0.0", "KouChuE=0.0", "[TableCon]", "MainTableField=1", "AssistantTableField=1", "MainTableIgnoreRow=1", "AssistantTableIgnoreRow=1"
             };
            File.WriteAllLines(ConfigFile.GetIniConfigPath_1, contents, ToolUtil.GetEncoding());
        }

        public static void Create_2()
        {
            string path = ConfigFile.GetIniConfigPath.Remove(ConfigFile.GetIniConfigPath_2.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "[File]", "File1Path=", "File2Path=", "TableInFile1=Sheet1", "TableInFile2=Sheet1", "[FieldCon]", "FileNumber=1", "IsSeted=0", "Invtype=transportation", "DanJuHaoMa=1.1", "GouFangMingCheng=0.0", "GouFangShuiHao=0.0", "GouFangDiZhiDianHua=0.0", "GouFangYinHangZhangHao=0.0", "BeiZhu=0.0", "FuHeRen=0.0", 
                "ShouKuanRen=0.0", "QingDanHangShangPinMingCheng=0.0", "DanJuRiQi=0.0", "XiaoFangYinHangZhangHao=0.0", "XiaoFangDiZhiDianHua=0.0", "HuoWuMingCheng=0.0", "JiLiangDanWei=0.0", "GuiGe=0.0", "ShuLiang=0.0", "BuHanShuiJinE=0.0", "ShuiLv=0.0", "ShangPinShuiMu=0.0", "ZheKouJinE=0.0", "ShuiE=0.0", "ZheKouShuiE=0.0", "ZheKouLv=0.0", 
                "DanJia=0.0", "DefaultFuHeRen=", "DefaultShouKuanRen=", "DefaultShuiLv=11", "ShenFenZhengJiaoYan=0.0", "HaiYangShiYou=0.0", "JiaGeFangShi=0.0", "ShouPiaoFangMC=0.0", "ShouPiaoFangSH=0.0", "ShouHuoRenMC=0.0", "ShouHuoRenSH=0.0", "FaHuoRenMC=0.0", "FaHuoRenSH=0.0", "ShuiLv-HY=0.0", "QiYouDaoDa=0.0", "CheChongCheHao=0.0", 
                "CheChuanDunWei=0.0", "YunShuHuoWuXX=0.0", "BeiZhu-HY=0.0", "FuHeRen-HY=0.0", "ShouKuanRen-HY=0.0", "DanJuRiQi-HY=0.0", "HuoWuMingCheng-HY=0.0", "JinE-HY=0.0", "GouHuoDanWei=0.0", "ShenFenZhengHaoMa=0.0", "JinE-JDC=0.0", "ShuiLv-JDC=0.0", "CheLiangLeiXing=0.0", "ChangPaiXingHao=0.0", "ChanDi=0.0", "ShengChanChangJiaMC=0.0", 
                "HeGeZhengHao=0.0", "JinKouZhengMingShuHao=0.0", "ShangJianDanHao=0.0", "FaDongJiHaoMa=0.0", "CheLiangShiBieDM=0.0", "DianHua=0.0", "ZhangHao=0.0", "DiZhi=0.0", "KaiHuYinHang=0.0", "DunWei=0.0", "XianChengRenShu=0.0", "DanJuRiQi-JDC=0.0", "BeiZhu-JDC=0.0", "NaShuiRenShiBieHao=0.0", "XiaoFangMingCheng=0.0", "XiaoFangShuiHao=0.0", 
                "NongChanPinBiaoZhi=0.0", "KouChuE=0.0", "[TableCon]", "MainTableField=1", "AssistantTableField=1", "MainTableIgnoreRow=1", "AssistantTableIgnoreRow=1"
             };
            File.WriteAllLines(ConfigFile.GetIniConfigPath_2, contents, ToolUtil.GetEncoding());
        }

        public static void Create_3()
        {
            string path = ConfigFile.GetIniConfigPath.Remove(ConfigFile.GetIniConfigPath_3.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "[File]", "File1Path=", "File2Path=", "TableInFile1=Sheet1", "TableInFile2=Sheet1", "[FieldCon]", "FileNumber=1", "IsSeted=0", "Invtype=vehiclesales", "DanJuHaoMa=1.1", "GouFangMingCheng=0.0", "GouFangShuiHao=0.0", "GouFangDiZhiDianHua=0.0", "GouFangYinHangZhangHao=0.0", "BeiZhu=0.0", "FuHeRen=0.0", 
                "ShouKuanRen=0.0", "QingDanHangShangPinMingCheng=0.0", "DanJuRiQi=0.0", "XiaoFangYinHangZhangHao=0.0", "XiaoFangDiZhiDianHua=0.0", "HuoWuMingCheng=0.0", "JiLiangDanWei=0.0", "GuiGe=0.0", "ShuLiang=0.0", "BuHanShuiJinE=0.0", "ShuiLv=0.0", "ShangPinShuiMu=0.0", "ZheKouJinE=0.0", "ShuiE=0.0", "ZheKouShuiE=0.0", "ZheKouLv=0.0", 
                "DanJia=0.0", "DefaultFuHeRen=", "DefaultShouKuanRen=", "DefaultShuiLv=11", "ShenFenZhengJiaoYan=0.0", "HaiYangShiYou=0.0", "JiaGeFangShi=0.0", "ShouPiaoFangMC=0.0", "ShouPiaoFangSH=0.0", "ShouHuoRenMC=0.0", "ShouHuoRenSH=0.0", "FaHuoRenMC=0.0", "FaHuoRenSH=0.0", "ShuiLv-HY=0.0", "QiYouDaoDa=0.0", "CheChongCheHao=0.0", 
                "CheChuanDunWei=0.0", "YunShuHuoWuXX=0.0", "BeiZhu-HY=0.0", "FuHeRen-HY=0.0", "ShouKuanRen-HY=0.0", "DanJuRiQi-HY=0.0", "HuoWuMingCheng-HY=0.0", "JinE-HY=0.0", "GouHuoDanWei=0.0", "ShenFenZhengHaoMa=0.0", "JinE-JDC=0.0", "ShuiLv-JDC=0.0", "CheLiangLeiXing=0.0", "ChangPaiXingHao=0.0", "ChanDi=0.0", "ShengChanChangJiaMC=0.0", 
                "HeGeZhengHao=0.0", "JinKouZhengMingShuHao=0.0", "ShangJianDanHao=0.0", "FaDongJiHaoMa=0.0", "CheLiangShiBieDM=0.0", "DianHua=0.0", "ZhangHao=0.0", "DiZhi=0.0", "KaiHuYinHang=0.0", "DunWei=0.0", "XianChengRenShu=0.0", "DanJuRiQi-JDC=0.0", "BeiZhu-JDC=0.0", "NaShuiRenShiBieHao=0.0", "XiaoFangMingCheng=0.0", "XiaoFangShuiHao=0.0", 
                "NongChanPinBiaoZhi=0.0", "KouChuE=0.0", "[TableCon]", "MainTableField=1", "AssistantTableField=1", "MainTableIgnoreRow=1", "AssistantTableIgnoreRow=1"
             };
            File.WriteAllLines(ConfigFile.GetIniConfigPath_3, contents, ToolUtil.GetEncoding());
        }
    }
}

