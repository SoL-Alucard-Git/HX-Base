namespace Aisino.FTaxBase
{
    using System;
    using System.Windows.Forms;

    public static class MessageBoxSys
    {
        public static DialogResult Show(string string_0)
        {
            MsgBoxInfo info = new MsgBoxInfo {
                Text = string_0
            };
            MessageBoxST xst = new MessageBoxST(info);
            return xst.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1)
        {
            MsgBoxInfo info = new MsgBoxInfo {
                Text = string_0,
                Caption = string_1
            };
            MessageBoxST xst = new MessageBoxST(info);
            return xst.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0)
        {
            MsgBoxInfo info = new MsgBoxInfo {
                Text = string_0,
                Caption = string_1,
                MessageBoxButton = messageBoxButtons_0
            };
            MessageBoxST xst = new MessageBoxST(info);
            return xst.ShowDialog();
        }

        public static DialogResult Show(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0)
        {
            MsgBoxInfo info = new MsgBoxInfo {
                Text = string_0,
                Caption = string_1,
                MessageBoxButton = messageBoxButtons_0,
                MessageBoxIcon = messageBoxIcon_0
            };
            MessageBoxST xst = new MessageBoxST(info);
            return xst.ShowDialog();
        }
    }
}

