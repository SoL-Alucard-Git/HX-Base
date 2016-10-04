namespace Aisino.Fwkp.Fpzpz.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpzpz.Common;
    using Microsoft.Win32;
    using System;

    public sealed class InsertDQBM_To_KHBM : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            try
            {
                bool flag = false;
                if (param == null)
                {
                    flag = true;
                }
                else if (0 >= param.Length)
                {
                    flag = false;
                }
                else
                {
                    flag = (bool) param[0];
                }
                if (!flag)
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\航天信息\新版开票\安装", true);
                    if (key != null)
                    {
                        string str = (string) key.GetValue("FullInstall", string.Empty);
                        if (string.IsNullOrEmpty(str) || str.Equals("1"))
                        {
                            DingYiZhiFuChuan.Initialize();
                            Tool.AddAreaIDToCusrTBL();
                            DingYiZhiFuChuan.Initialize();
                        }
                    }
                }
                else
                {
                    DingYiZhiFuChuan.Initialize();
                    Tool.AddAreaIDToCusrTBL();
                    DingYiZhiFuChuan.Initialize();
                }
                return new object[] { 1 };
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }
    }
}

