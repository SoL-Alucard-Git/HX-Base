namespace ns4
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class Class92
    {
        private IBaseDAO ibaseDAO_0;

        public Class92()
        {
            
            this.ibaseDAO_0 = BaseDAOFactory.GetBaseDAOSQLite();
        }

        public string method_0()
        {
            string str = "0.0";
            try
            {
                string str2 = "Aisino.Framework.MainForm.UpDown.GetSPBMBBBH";
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                str = this.ibaseDAO_0.queryValueSQL<string>(str2, parameter);
                if ((str != null) && !(str == ""))
                {
                    return str;
                }
                str = "0.0";
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetSBBPBBH:" + exception.ToString());
            }
            return str;
        }

        public bool method_1(List<string> sqlID, List<Dictionary<string, object>> param, bool bool_0)
        {
            bool flag = false;
            if (((sqlID != null) && (sqlID.Count >= 1)) && ((param != null) && (param.Count >= 1)))
            {
                try
                {
                    int num = this.ibaseDAO_0.未确认DAO方法1(sqlID.ToArray(), param);
                    if (num > 0)
                    {
                        flag = true;
                    }
                    if (!bool_0 && (num > 0))
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_8(Class95.string_2 + "更新成功，请及时维护" + Class95.string_2 + "库。");
                        Class95.bool_1 = true;
                        Class95.string_1 = Class95.string_2 + "更新成功，请及时维护" + Class95.string_2 + "库。";
                        return flag;
                    }
                    if (!bool_0 && (num <= 0))
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新。");
                        Class95.bool_1 = true;
                        Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新。";
                        return flag;
                    }
                    if (num <= 0)
                    {
                        Class95.bool_1 = true;
                        Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新。";
                        return flag;
                    }
                    Class95.bool_1 = true;
                    Class95.string_1 = Class95.string_2 + "更新成功，请及时维护" + Class95.string_2 + "库。";
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("UpdateSPFLTable异常：" + exception.ToString());
                    if (!bool_0)
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：" + exception.Message);
                    }
                    Class95.bool_1 = true;
                    Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：" + exception.Message;
                }
                return flag;
            }
            Class101.smethod_0("sqlid为空或者param为空");
            return flag;
        }

        public DataTable method_2()
        {
            DataTable table = new DataTable();
            try
            {
                string str = "SELECT SJBM FROM BM_SPFL";
                table = this.ibaseDAO_0.querySQLDataTable(str);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetSJBM:" + exception.ToString());
            }
            return table;
        }

        public int method_3(string string_0)
        {
            int num = 0;
            Class101.smethod_0("UpdateBMBBBH version:" + string_0);
            if (string_0 != "")
            {
                try
                {
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("VERSION", string_0);
                    num = this.ibaseDAO_0.未确认DAO方法2_疑似updateSQL("Aisino.Framework.MainForm.UpDown.ReplaceSPBMBBBH", parameter);
                    Class101.smethod_0("UpdateBMBBBH version结束:" + num);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("UpdateBMBBBH:" + exception.ToString());
                }
            }
            return num;
        }

        public int method_4()
        {
            int num = 0;
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.GetTotalRowCountFromSPFL", parameter);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("UpdateBMBBBH:" + exception.ToString());
            }
            return num;
        }
    }
}

