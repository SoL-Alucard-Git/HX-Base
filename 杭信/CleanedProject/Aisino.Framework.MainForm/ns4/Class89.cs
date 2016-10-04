namespace ns4
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class Class89
    {
        public Class89()
        {
            
        }

        public Dictionary<string, object> method_0(DataRow dataRow_0)
        {
            if (dataRow_0 == null)
            {
                return null;
            }
            Class101.smethod_0("getDictWithStatus   FPDM:" + dataRow_0["FPDM"].ToString() + "  FPHM:" + dataRow_0["FPNO"].ToString() + "  fpzl:" + dataRow_0["Fplx"].ToString() + "  BSZT:" + dataRow_0["FPStatus"].ToString());
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (dataRow_0["FPStatus"].ToString() == "999")
            {
                dictionary.Add("BSZT", "2");
            }
            else
            {
                dictionary.Add("BSZT", dataRow_0["FPStatus"]);
            }
            dictionary.Add("FPHM", int.Parse(dataRow_0["FPNO"].ToString()));
            dictionary.Add("FPDM", dataRow_0["FPDM"]);
            dictionary.Add("SCSJ", dataRow_0["FpUploadTime"]);
            string str = string.Empty;
            if (dataRow_0["Fplx"].ToString().Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "s";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "c";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "f";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "j";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "p";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "q";
            }
            else
            {
                str = dataRow_0["Fplx"].ToString();
            }
            dictionary.Add("FPZL", str);
            string str2 = string.Empty;
            if (Convert.ToBoolean(dataRow_0["ZFBZ"]))
            {
                str2 = "1";
            }
            else
            {
                str2 = "0";
            }
            dictionary.Add("ZFBZ", str2);
            return dictionary;
        }

        public Dictionary<string, object> method_1(DataRow dataRow_0)
        {
            if (dataRow_0 == null)
            {
                return null;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPDM", dataRow_0["FPDM"]);
            dictionary.Add("FPHM", int.Parse(dataRow_0["FPNO"].ToString()));
            dictionary.Add("FPSLH", dataRow_0["FPSLH"]);
            dictionary.Add("SCSJ", dataRow_0["FpUploadTime"]);
            string str = string.Empty;
            if (dataRow_0["Fplx"].ToString().Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "s";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "c";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "f";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "j";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "p";
            }
            else if (dataRow_0["Fplx"].ToString().Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "q";
            }
            else
            {
                str = dataRow_0["Fplx"].ToString();
            }
            dictionary.Add("FPZL", str);
            string str2 = string.Empty;
            if (Convert.ToBoolean(dataRow_0["ZFBZ"]))
            {
                str2 = "1";
            }
            else
            {
                str2 = "0";
            }
            dictionary.Add("ZFBZ", str2);
            return dictionary;
        }

        public Dictionary<string, object> method_2(DataRow dataRow_0)
        {
            if (dataRow_0 == null)
            {
                return null;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPHM", dataRow_0["FPNO"].ToString());
            dictionary.Add("FPDM", dataRow_0["FPDM"]);
            dictionary.Add("BSZT", dataRow_0["FPStatus"]);
            dictionary.Add("SCSJ", dataRow_0["FpUploadTime"]);
            dictionary.Add("FPZL", dataRow_0["Fplx"].ToString());
            string str = string.Empty;
            if (Convert.ToBoolean(dataRow_0["ZFBZ"]))
            {
                str = "1";
            }
            else
            {
                str = "0";
            }
            dictionary.Add("ZFBZ", str);
            return dictionary;
        }

        public void method_3(Dictionary<string, object> dict)
        {
            if ((dict != null) && (dict.Count >= 1))
            {
                Class100 class2 = new Class100();
                Class96 class3 = class2.method_3(Class97.dataTable_0, dict["FPHM"].ToString().PadLeft(8, '0'), dict["FPDM"].ToString());
                if (class3 != null)
                {
                    if (dict["BSZT"].ToString() == "3")
                    {
                        class3.Boolean_0 = true;
                        class2.method_1(class3, Enum10.Update);
                    }
                    else
                    {
                        class2.method_1(class3, Enum10.Delete);
                    }
                }
            }
        }

        public Dictionary<string, string> method_4(DataRow dataRow_0, string string_0)
        {
            if (dataRow_0 == null)
            {
                return null;
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                dictionary.Add("FPDM", dataRow_0["FPDM"].ToString());
                dictionary.Add("FPHM", dataRow_0["FPNO"].ToString());
                dictionary.Add("BSZT", dataRow_0["FPStatus"].ToString());
                if (string_0 == "up")
                {
                    dictionary.Add("BSRZ", this.method_5(dataRow_0["FPDM"].ToString(), int.Parse(dataRow_0["FPNO"].ToString()).ToString()));
                }
                else
                {
                    dictionary.Add("BSRZ", this.method_6(dataRow_0["FPDM"].ToString(), int.Parse(dataRow_0["FPNO"].ToString()).ToString()));
                }
                string str = string.Empty;
                if (dataRow_0["Fplx"].ToString().Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "s";
                }
                else if (dataRow_0["Fplx"].ToString().Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "c";
                }
                else if (dataRow_0["Fplx"].ToString().Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "f";
                }
                else if (dataRow_0["Fplx"].ToString().Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "j";
                }
                else if (dataRow_0["Fplx"].ToString().Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "p";
                }
                else if (dataRow_0["Fplx"].ToString().Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "q";
                }
                else
                {
                    str = dataRow_0["Fplx"].ToString();
                }
                dictionary.Add("FPZL", str);
                Class101.smethod_0("组装Linux开票服务器dict：FPDM-" + dataRow_0["FPDM"].ToString() + "  FPHM-" + dataRow_0["FPNO"].ToString() + "   fpzl-" + str + "  bszt-" + dataRow_0["FPStatus"].ToString() + "   bsrz-" + dictionary["BSRZ"]);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("组装Linux开票服务器dict异常：" + exception.ToString());
            }
            return dictionary;
        }

        public string method_5(string string_0, string string_1)
        {
            string str = "";
            try
            {
                int num = 0;
                while (num < Class87.list_0.Count)
                {
                    if ((Class87.list_0[num]["FPDM"].ToString() == string_0) && (Class87.list_0[num]["FPHM"].ToString() == string_1))
                    {
                        goto Label_0061;
                    }
                    num++;
                }
                return str;
            Label_0061:
                str = Class87.list_0[num]["BSRZ"].ToString();
            }
            catch (Exception)
            {
            }
            return str;
        }

        public string method_6(string string_0, string string_1)
        {
            string str = "";
            try
            {
                int num = 0;
                while (num < Class87.list_1.Count)
                {
                    if ((Class87.list_1[num]["FPDM"].ToString() == string_0) && (Class87.list_1[num]["FPHM"].ToString() == string_1))
                    {
                        goto Label_0061;
                    }
                    num++;
                }
                return str;
            Label_0061:
                str = Class87.list_1[num]["BSRZ"].ToString();
            }
            catch (Exception)
            {
            }
            return str;
        }
    }
}

