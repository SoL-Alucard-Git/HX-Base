namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ShowMessage : BaseForm
    {
        private AisinoBTN btn_OK;
        private IContainer components;
        private AisinoRTX lbl_Message;
        private ILog loger = LogUtil.GetLogger<ShowMessage>();
        private XmlComponentLoader xmlComponentLoader1;

        public ShowMessage()
        {
            try
            {
                this.Initialize();
                this.btn_OK.Click += new EventHandler(this.btnOK_Click);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                MessageManager.ShowMsgBox(exception2.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                base.Close();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                MessageManager.ShowMsgBox(exception2.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.btn_OK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_OK");
            this.lbl_Message = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("lbl_Message");
            this.lbl_Message.AutoSize = false;
            this.lbl_Message.ReadOnly = true;
            this.lbl_Message.BorderStyle = BorderStyle.None;
            base.FormClosing += new FormClosingEventHandler(this.ShowMessage_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ShowMessage));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x163, 200);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.ShowMessage\Aisino.Fwkp.Fpkj.Form.FPCX.ShowMessage.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x163, 200);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ShowMessage";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "报送日志显示窗口";
            base.ResumeLayout(false);
        }

        public bool setValue(string Text)
        {
            try
            {
                this.lbl_Message.Text = "  " + Text;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
                return false;
            }
            return true;
        }

        private void ShowMessage_FormClosing(object sender, EventArgs e)
        {
            this.loger = null;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x4e:
                case 13:
                case 14:
                case 20:
                    base.WndProc(ref m);
                    return;

                case 0x84:
                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() != 1)
                    {
                        break;
                    }
                    m.Result = new IntPtr(2);
                    return;

                case 0xa3:
                    Console.WriteLine(base.WindowState);
                    return;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}

