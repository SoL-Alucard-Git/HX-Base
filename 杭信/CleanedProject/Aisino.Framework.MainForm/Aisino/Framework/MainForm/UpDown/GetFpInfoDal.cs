namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using ns4;
    using System;
    using System.Collections.Generic;

    public class GetFpInfoDal
    {
        public GetFpInfoDal()
        {
            
        }

        public List<Fpxx> GetFpInfo(string string_0, string string_1, UpdateTransMethod updateTransMethod_0, string string_2, string string_3, string string_4, string string_5)
        {
            if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
            {
                try
                {
                    object[] objArray = new object[5];
                    if (((string.IsNullOrEmpty(string_3) || string.IsNullOrEmpty(string_2)) || string.IsNullOrEmpty(string_4)) && string.IsNullOrEmpty(string_5))
                    {
                        string str = string.Empty;
                        string str2 = string.Empty;
                        if (updateTransMethod_0 == UpdateTransMethod.WBS)
                        {
                            str = "0";
                            str2 = "0";
                        }
                        else if (updateTransMethod_0 == UpdateTransMethod.BSZ)
                        {
                            str = "3";
                            str2 = "3";
                        }
                        else if (updateTransMethod_0 == UpdateTransMethod.YBS)
                        {
                            str = "1";
                            str2 = "1";
                        }
                        else if (updateTransMethod_0 == UpdateTransMethod.BSSB)
                        {
                            str = "2";
                            str2 = "2";
                        }
                        else
                        {
                            str = "0";
                            str2 = "3";
                        }
                        objArray = new object[] { string_0, string_1, str, str2 };
                    }
                    else if (string.IsNullOrEmpty(string_5))
                    {
                        int result = 0;
                        int.TryParse(string_2, out result);
                        objArray = new object[] { string_4, string_3, result, string.Empty, "1" };
                    }
                    else
                    {
                        objArray = new object[] { string.Empty, string.Empty, string.Empty, string.Empty, "2", string_5 };
                    }
                    Class101.smethod_0(string.Concat(new object[] { "(上传线程)发票查询接口调用前--", objArray[0], ",", objArray[1], ",", objArray[2], ",", objArray[3] }));
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunPageShareMethods", objArray);
                    if ((objArray2 != null) && (objArray2.Length > 0))
                    {
                        if ((objArray.Length > 4) && string.IsNullOrEmpty(string_5))
                        {
                            return new List<Fpxx> { (objArray2[0] as Fpxx) };
                        }
                        return (objArray2[0] as List<Fpxx>);
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票信息失败！" + exception.ToString());
                }
            }
            return null;
        }
    }
}

