namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class TabPagePw : TabPage
    {
        private Form form_0;
        private IContainer icontainer_0;
        private string string_0;

        public event FormClosedEventHandler FormClose;

        public TabPagePw()
        {
            
            this.method_1();
        }

        public void AddForm(Form form_1)
        {
            form_1.StartPosition = FormStartPosition.Manual;
            form_1.DesktopLocation = new Point(0, 0);
            form_1.Location = new Point(0, 0);
            this.string_0 = form_1.GetType().FullName;
            this.form_0 = form_1;
            this.form_0.FormClosed += new FormClosedEventHandler(this.form_0_FormClosed);
            base.Controls.Add(form_1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void form_0_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.method(this, e);
        }

        private void method(object sender, FormClosedEventArgs e)
        {
            if (this.FormClose != null)
            {
                this.FormClose(sender, e);
            }
        }

        private void method_1()
        {
            this.icontainer_0 = new Container();
        }

        public object FormInstance
        {
            get
            {
                return this.form_0;
            }
        }

        public string FormType
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

