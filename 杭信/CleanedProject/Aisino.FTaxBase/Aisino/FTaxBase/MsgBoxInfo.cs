namespace Aisino.FTaxBase
{
    using System;
    using System.Windows.Forms;

    public class MsgBoxInfo
    {
        private MessageBoxButtons messageBoxButtons_0;
        private System.Windows.Forms.MessageBoxIcon messageBoxIcon_0;
        private string string_0;
        private string string_1;

        public MsgBoxInfo()
        {
            
            this.messageBoxIcon_0 = System.Windows.Forms.MessageBoxIcon.Asterisk;
        }

        public string Caption
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public MessageBoxButtons MessageBoxButton
        {
            get
            {
                return this.messageBoxButtons_0;
            }
            set
            {
                this.messageBoxButtons_0 = value;
            }
        }

        public System.Windows.Forms.MessageBoxIcon MessageBoxIcon
        {
            get
            {
                return this.messageBoxIcon_0;
            }
            set
            {
                this.messageBoxIcon_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

