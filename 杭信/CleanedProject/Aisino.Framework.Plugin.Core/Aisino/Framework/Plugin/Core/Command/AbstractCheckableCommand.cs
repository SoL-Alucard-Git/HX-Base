namespace Aisino.Framework.Plugin.Core.Command
{
    using ns10;
    using System;

    public abstract class AbstractCheckableCommand : AbstractCommand, Interface1, Interface5
    {
        private bool bool_0;

        protected AbstractCheckableCommand()
        {
            
        }

        //bool Interface5.Checked
        //{
        //    get
        //    {
        //        return this.bool_0;
        //    }
        //    set
        //    {
        //        this.bool_0 = value;
        //    }
        //}

        public bool imethod_4()
        {
            return this.bool_0;
        }

        public void imethod_5(bool bool_0)
        {
            this.bool_0 = bool_0;
        }
    }
}

