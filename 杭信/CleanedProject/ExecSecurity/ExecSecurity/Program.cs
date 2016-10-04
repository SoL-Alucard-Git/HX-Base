namespace ExecSecurity
{
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Reflection;
    internal class Program
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("Security.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi)]
        public static extern int FileCryptEx(int nIndex, string strDllPath, string strTaxCode);
        private static string GetDistrictCode()
        {
            string str2 = string.Empty;
            string localTaxCode = "";
            try
            {
                localTaxCode = GetLocalTaxCode();
                if (localTaxCode.Length == 15)
                {
                    return localTaxCode.Substring(0, 6);
                }
                localTaxCode = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", "orgcode", "").ToString();
            }
            catch (Exception)
            {
                str2 = "";
            }
            return str2;
        }

        private static string GetLocalTaxCode()
        {
            string str2;
            try
            {
                return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", "code", "").ToString();
            }
            catch (Exception)
            {
                str2 = "";
            }
            return str2;
        }

        private static int Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AddedRealTax.dll");
            string localTaxCode = GetLocalTaxCode();
            if (!File.Exists(path))
            {
                return -1;
            }
            try
            {
                int num = FileCryptEx(2, path, localTaxCode);
                logger.Info("Security taxCode:" + localTaxCode);
                logger.Info("Security result:" + num.ToString());
                return num;
            }
            catch (Exception exception)
            {
                logger.Info(exception.Message);
                return -1;
            }
        }
    }
}

