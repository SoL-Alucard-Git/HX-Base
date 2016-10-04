namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using ns4;
    using System;
    public class SPFLService
    {
        public bool isAuto;

        public SPFLService()
        {
            
        }

        public string GetMaxBMBBBH()
        {
            Class92 class2 = new Class92();
            return class2.method_0();
        }

        public void UpdateSPFL()
        {
            try
            {
                MessageHelper.MsgWait("正在同步" + Class95.string_2 + "信息，请耐心等待...");
                Class84 class2 = new Class84();
                string str = class2.method_31();
                string str2 = "";
                Class101.smethod_0("商品分类信息同步，发送给局端数据：" + str);
                if (HttpsSender.SendMsg("0037", str, out str2) != 0)
                {
                    Class101.smethod_1("商品分类更新失败：" + str2);
                    if (!this.isAuto)
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：" + str2);
                    }
                }
                else
                {
                    Class101.smethod_1("商品分类信息同步，局端返回数据：" + str2);
                    class2.method_32(str2, this.isAuto);
                }
                MessageHelper.MsgWait();
            }
            catch (Exception exception)
            {
                MessageHelper.MsgWait();
                Class101.smethod_1("UpdateSPFL:" + exception.ToString());
                if (!this.isAuto)
                {
                    Class86.smethod_7("商品和服务税收分类编码更新失败，请选择手动更新或去税局下载更新包导入更新：" + exception.Message);
                }
            }
        }

        public int UpdateBMBBBH(string string_0)
        {
            Class101.smethod_0("接口UpdateBMBBBH开始：" + string_0);
            int num = 0;
            num = new Class92().method_3(string_0);
            Class101.smethod_0("接口UpdateBMBBBH结束：" + string_0);
            return num;
        }
    }
}

