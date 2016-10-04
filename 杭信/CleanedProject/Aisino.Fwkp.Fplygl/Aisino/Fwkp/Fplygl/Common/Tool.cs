namespace Aisino.Fwkp.Fplygl.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;

    internal sealed class Tool : DockForm
    {
        private static ILog _Loger = LogUtil.GetLogger<Tool>();
        private static Tool _Tool = new Tool();

        public string GetDriverVersion()
        {
            try
            {
                return Instance().TaxCardInstance.get_StateInfo().DriverVersion;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        public string GetSoftVersion()
        {
            try
            {
                return Instance().TaxCardInstance.get_SoftVersion();
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return string.Empty;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return string.Empty;
            }
        }

        public static Tool Instance()
        {
            try
            {
                return _Tool;
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return _Tool;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return _Tool;
            }
        }

        public bool IsChaoBaoQi()
        {
            try
            {
                Instance().TaxCardInstance.GetInvStock();
                if (0 < Instance().TaxCardInstance.get_RetCode())
                {
                    MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                    return true;
                }
                return this.IsJskHaveDate(base.TaxCardInstance.GetInvStock());
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        public bool IsJskHaveDate(List<InvVolumeApp> ListModel)
        {
            try
            {
                if ((ListModel == null) || (ListModel.Count <= 0))
                {
                    MessageManager.ShowMsgBox("INP-441201");
                    return true;
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return false;
        }
    }
}

