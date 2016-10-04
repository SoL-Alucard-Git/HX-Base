namespace Aisino.Fwkp.Wbjk.Service
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.IO;

    public class CreateIniFile
    {
        public static void Create()
        {
            string path = GetXmlcfg.IniConfig.Remove(GetXmlcfg.IniConfig.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] contents = new string[] { 
                "[File]", "File1Path=", "File2Path=", "TableInFile1=Sheet1", "TableInFile2=Sheet1", "[FieldCon]", "FileNumber=1", "DanJuHaoMa=1.1", "GouFangMingCheng=1.2", "GouFangShuiHao=1.3", "GouFangDiZhiDianHua=0.0", "GouFangYinHangZhangHao=0.0", "BeiZhu=0.0", "FuHeRen=0.0", "ShouKuanRen=0.0", "QingDanHangShangPinMingCheng=0.0", 
                "DanJuRiQi=0.0", "XiaoFangYinHangZhangHao=0.0", "XiaoFangDiZhiDianHua=0.0", "HuoWuMingCheng=1.4", "JiLiangDanWei=0.0", "GuiGe=0.0", "ShuLiang=1.8", "BuHanShuiJinE=1.5", "ShuiLv=1.6", "ShangPinShuiMu=0.0", "ZheKouJinE=1.7", "ShuiE=0.0", "ZheKouShuiE=0.0", "ZheKouLv=0.0", "DanJia=0.0", "DefaultFuHeRen=", 
                "DefaultShouKuanRen=", "DefaultShuiLv=11", "[TableCon]", "MainTableField=1", "AssistantTableField=1", "MainTableIgnoreRow=1", "AssistantTableIgnoreRow=1"
             };
            File.WriteAllLines(GetXmlcfg.IniConfig, contents, ToolUtil.GetEncoding());
        }
    }
}

