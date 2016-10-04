namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using Aisino.Fwkp.HomePage.AisinoDock.Docks;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ButtonControlExEventArgs : EventArgs
    {
        public Image Icon { get; set; }

        public string IconName { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public ButtonClickStyle Style { get; set; }
    }
}

