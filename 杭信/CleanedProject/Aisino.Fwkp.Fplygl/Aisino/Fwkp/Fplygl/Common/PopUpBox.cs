namespace Aisino.Fwkp.Fplygl.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using log4net;
    using System;

    public class PopUpBox
    {
        private static ILog loger = LogUtil.GetLogger<PopUpBox>();

        public static bool NoHaveFen_KaiPiaoJi(TaxCard taxcard, TaxStateInfo taxStateInfo)
        {
            try
            {
                if (taxStateInfo.IsWithChild == 0)
                {
                    return false;
                }
                if (0 >= taxStateInfo.MachineNumber)
                {
                    return false;
                }
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }

        public static bool NoIsZhu_KaiPiaoJi(TaxCard taxcard, TaxStateInfo taxStateInfo)
        {
            try
            {
                if (taxStateInfo.IsMainMachine == 0)
                {
                    return false;
                }
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return true;
        }
    }
}

