namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;

    internal class EventArgs0 : EventArgs
    {
        private NavigateMenuNode navigateMenuNode_0;

        public EventArgs0()
        {
            
        }

        public EventArgs0(NavigateMenuNode navigateMenuNode_1)
        {
            
            this.navigateMenuNode_0 = navigateMenuNode_1;
        }

        public NavigateMenuNode method_0()
        {
            return this.navigateMenuNode_0;
        }

        public void method_1(NavigateMenuNode navigateMenuNode_1)
        {
            this.navigateMenuNode_0 = navigateMenuNode_1;
        }
    }
}

