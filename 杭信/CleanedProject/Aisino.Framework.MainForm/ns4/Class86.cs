namespace ns4
{
    using Aisino.Framework.Plugin.Core;
    using System;
    using System.Windows.Forms;

    internal static class Class86
    {
        public static bool bool_0;

        static Class86()
        {
            
        }

        public static void smethod_0(object object_0, object object_1)
        {
            MessageBoxHelper.Show("发票(发票代码：" + object_0 + "   发票号码：" + object_1 + ")验签失败，请手工作废该发票！", "发票验签失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static DialogResult smethod_1()
        {
            return MessageBoxHelper.Show("获取和更新企业配置信息失败！", "企业配置信息验证", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static DialogResult smethod_2(string string_0, string string_1)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                return MessageBoxHelper.Show("获取和更新企业配置信息失败！" + Environment.NewLine + "错误描述：" + string_1, "企业配置信息验证", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return MessageBoxHelper.Show("获取和更新企业配置信息失败！" + Environment.NewLine + "错误描述：【" + string_0 + "】" + string_1, "企业配置信息验证", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static DialogResult smethod_3()
        {
            return MessageBoxHelper.Show("获取和更新企业配置信息成功！", "企业配置信息验证", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void smethod_4(object object_0, object object_1)
        {
            MessageBoxHelper.Show("电子发票(发票代码：" + object_0 + "   发票号码：" + object_1 + ")验签失败，系统已作废该发票，稍后系统会重新上传该张发票！", "电子发票作废", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void smethod_5(object object_0, object object_1)
        {
            MessageBoxHelper.Show("电子发票(发票代码：" + object_0 + "   发票号码：" + object_1 + ")验签失败，该发票已跨月或者已抄税，系统无法作废该发票，请开红票冲销该张发票！", "电子发票验签失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void smethod_6(string string_0)
        {
            MessageBoxHelper.Show(string_0, "异常票", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void smethod_7(string string_0)
        {
            MessageBoxHelper.Show(string_0, "异常", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void smethod_8(string string_0)
        {
            MessageBoxHelper.Show(string_0, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static DialogResult smethod_9()
        {
            if (!Class95.bool_0 && !Class95.bool_1)
            {
                return DialogResult.Cancel;
            }
            string str = string.Empty;
            if (Class95.bool_0 && Class95.bool_1)
            {
                str = "1、" + Class95.string_0 + Environment.NewLine + "2、" + Class95.string_1;
            }
            else if (Class95.bool_0)
            {
                str = Class95.string_0;
            }
            else if (Class95.bool_1)
            {
                str = Class95.string_1;
            }
            if (bool_0)
            {
                str = str + " 请手工重启开票软件。";
            }
            return MessageBoxHelper.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}

