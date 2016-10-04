namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Windows.Forms;

    public class NoPasteText : AisinoTXT
    {
        public NoPasteText()
        {
            
        }

        protected override void WndProc(ref Message m)
        {
            if (((m.Msg != 0x204) && (m.Msg != 0x7b)) && (m.Msg != 770))
            {
                base.WndProc(ref m);
            }
        }
    }
}

