namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.FTaxBase;
    using System;

    public sealed class WbjkEntry : AbstractCommand
    {
        public static bool RegFlag_JS;
        public static bool RegFlag_JT;
        public static bool RegFlag_KT;
        public static bool RegFlag_ST;

        protected override void RunCommand()
        {
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            try
            {
                RegFlag_JT = RegisterManager.CheckRegFile("JIJT");
                RegFlag_JS = RegisterManager.CheckRegFile("JIJS");
                RegFlag_ST = RegisterManager.CheckRegFile("JIST");
                RegFlag_KT = RegisterManager.CheckRegFile("JIKT");
                return ((card.get_TaxMode() == 2) && (((RegFlag_JT || RegFlag_JS) || RegFlag_ST) || RegFlag_KT));
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

