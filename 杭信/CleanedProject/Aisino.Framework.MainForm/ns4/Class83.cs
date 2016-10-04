namespace ns4
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal class Class83
    {
        public Class83()
        {
            
        }

        public bool method_0(ref Dictionary<string, object> dictionary_0)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryDZDZInfo", new object[0]);
                if ((objArray != null) && (objArray.Length >= 1))
                {
                    dictionary_0 = objArray[0] as Dictionary<string, object>;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("get Dzdzxx info failed! " + exception.ToString());
                return false;
            }
            return true;
        }

        public string method_1()
        {
            string str = "";
            try
            {
                byte[] inArray = TaxCard.CreateInstance(CTaxCardType.const_7).ReadCompanyInfo();
                if ((inArray != null) && (inArray.Length >= 1))
                {
                    str = Convert.ToBase64String(inArray);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("获取企业信息失败！" + exception.ToString());
            }
            return str;
        }

        public bool method_2(string string_0)
        {
            if (string.IsNullOrEmpty(string_0))
            {
                if (Class87.bool_3)
                {
                    Class87.dictionary_0["UpdateBZ"] = "2";
                    Class87.dictionary_0["Message"] = "企业信息无变化！";
                }
                Class95.bool_0 = false;
                Class95.string_0 = "企业信息无变化！";
                Class101.smethod_0("企业信息无变化，无需更新！    ");
                return true;
            }
            TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
            try
            {
                Class101.smethod_0("企业参数同步前局端信息：" + string_0);
                byte[] buffer = Convert.FromBase64String(string_0);
                if ((buffer != null) && (buffer.Length >= 1))
                {
                    Class101.smethod_0("开始企业参数同步");
                    card.UpdateCompanyInfo(buffer, Convert.ToUInt32(buffer.Length));
                    Class101.smethod_0("企业参数同步结束");
                    string location = Assembly.GetExecutingAssembly().Location;
                    string path = location.Substring(0, location.LastIndexOf(@"\")) + @"\" + AttributeName.QYXXFileName;
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    if (card.RetCode == 0)
                    {
                        Class101.smethod_0("更新企业信息成功！    ");
                        if (Class87.bool_3)
                        {
                            Class87.dictionary_0["UpdateBZ"] = "3";
                            Class87.dictionary_0["Message"] = "更新底层信息成功！";
                        }
                        Class95.string_0 = "更新企业信息成功！";
                        Class95.bool_0 = true;
                        return true;
                    }
                    if (Class87.bool_3)
                    {
                        Class87.dictionary_0["UpdateBZ"] = "4";
                        Class87.dictionary_0["Message"] = "更新企业信息失败！";
                    }
                    Class95.bool_0 = true;
                    Class95.string_0 = "更新企业信息失败！    ErrorCode=" + card.RetCode;
                    Class101.smethod_1("更新企业信息失败！    ErrorCode=" + card.RetCode);
                }
                return false;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("更新设备企业信息失败！" + exception.ToString());
                Class95.bool_0 = true;
                Class95.string_0 = "更新设备企业信息失败！" + exception.Message;
            }
            return false;
        }

        public bool method_3(string string_0)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(string_0))
            {
                return false;
            }
            Class81 class2 = new Class81();
            if (class2.method_4().Trim().Equals(string_0.Trim()))
            {
                return true;
            }
            try
            {
                string[] strArray = string_0.Split(new char[] { ',' });
                if (strArray != null)
                {
                    object[] objArray;
                    if (strArray.Length != 3)
                    {
                        return flag;
                    }
                    if ((strArray[0] == "0") && (strArray[1] == "0"))
                    {
                        objArray = new object[] { true, false, strArray[0], false, strArray[1], strArray[2] };
                    }
                    else if ((strArray[0] != "0") && (strArray[1] == "0"))
                    {
                        objArray = new object[] { false, true, strArray[0], false, strArray[1], strArray[2] };
                    }
                    else if ((strArray[0] == "0") && (strArray[1] != "0"))
                    {
                        objArray = new object[] { false, false, strArray[0], true, strArray[1], strArray[2] };
                    }
                    else
                    {
                        objArray = new object[] { false, true, strArray[0], true, strArray[1], strArray[2] };
                    }
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.UpdateDZDZInfo", objArray);
                    if ((objArray2 != null) && (objArray2.Length >= 1))
                    {
                        return Convert.ToBoolean(objArray2[0]);
                    }
                }
                return flag;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("更新上传方式失败！" + exception.ToString());
            }
            return flag;
        }
    }
}

