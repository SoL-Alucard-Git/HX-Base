namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using System;
    using System.Windows.Forms;

    public class MessageBoxHelper
    {
        public MessageBoxHelper()
        {
            
        }

        public static DialogResult Show(string string_0)
        {
            //SysMessageBox box = new SysMessageBox("信息提示", string_0, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            //return box.ShowDialog();
            return MessageBox.Show( string_0, "发生异常", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string string_0, string string_1)
        {
            SysMessageBox box = new SysMessageBox(string_1, string_0, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            return box.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0)
        {
            MessageBoxIcon asterisk;
            switch (messageBoxButtons_0)
            {
                case MessageBoxButtons.OK:
                    asterisk = MessageBoxIcon.Asterisk;
                    break;

                case MessageBoxButtons.OKCancel:
                    asterisk = MessageBoxIcon.Asterisk;
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    asterisk = MessageBoxIcon.Hand;
                    break;

                case MessageBoxButtons.YesNoCancel:
                    asterisk = MessageBoxIcon.Asterisk;
                    break;

                case MessageBoxButtons.YesNo:
                    asterisk = MessageBoxIcon.Asterisk;
                    break;

                case MessageBoxButtons.RetryCancel:
                    asterisk = MessageBoxIcon.Hand;
                    break;

                default:
                    asterisk = MessageBoxIcon.Asterisk;
                    break;
            }
            SysMessageBox box = new SysMessageBox(string_1, string_0, messageBoxButtons_0, asterisk, MessageBoxDefaultButton.Button1);
            return box.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0)
        {
            SysMessageBox box = new SysMessageBox(string_1, string_0, messageBoxButtons_0, messageBoxIcon_0, MessageBoxDefaultButton.Button1);
            return box.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0, MessageBoxDefaultButton messageBoxDefaultButton_0)
        {
            SysMessageBox box = new SysMessageBox(string_1, string_0, messageBoxButtons_0, messageBoxIcon_0, messageBoxDefaultButton_0);
            return box.ShowDialog();
        }
    }
}

