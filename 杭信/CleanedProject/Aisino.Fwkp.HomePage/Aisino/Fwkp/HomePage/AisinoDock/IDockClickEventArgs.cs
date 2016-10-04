namespace Aisino.Fwkp.HomePage.AisinoDock
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class IDockClickEventArgs : EventArgs
    {
        public event EventHandler Click;

        public void OnClick(object sender, EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(sender, e);
            }
        }

        public System.Drawing.Image Image { get; set; }

        internal Rectangle Rect { get; set; }
    }
}

