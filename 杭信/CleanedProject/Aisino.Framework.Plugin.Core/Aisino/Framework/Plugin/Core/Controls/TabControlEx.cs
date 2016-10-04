namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class TabControlEx : TabControlSkin
    {
        private IContainer icontainer_1;

        public TabControlEx()
        {
            
            this.method_6();
        }

        public void AddNewPage(TabPageEx tabPageEx_0)
        {
            base.TabPages.Add(tabPageEx_0);
            IEnumerator enumerator = base.TabPages.GetEnumerator();
            {
                TabPage current;
                while (enumerator.MoveNext())
                {
                    current = (TabPage) enumerator.Current;
                    if ((current.Name == tabPageEx_0.Name) && (current.Text == tabPageEx_0.Text))
                    {
                        goto Label_0057;
                    }
                }
                return;
            Label_0057:
                ((TabPageEx) current).FormClose += new FormClosedEventHandler(this.method_7);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_6()
        {
            this.icontainer_1 = new Container();
        }

        private void method_7(object sender, FormClosedEventArgs e)
        {
            TabPage page = sender as TabPage;
            if (page != null)
            {
                base.TabPages.Remove(page);
            }
        }

        protected override void OnCloseMethod(object sender, CancelEventArgs e)
        {
            TabPage page = sender as TabPage;
            if (page != null)
            {
                foreach (TabPage page2 in base.TabPages)
                {
                    if ((page2.Name == page.Name) && (page2.Text == page.Text))
                    {
                        Form formInstance = ((TabPageEx) page2).FormInstance as Form;
                        if (formInstance != null)
                        {
                            formInstance.Close();
                        }
                    }
                }
            }
            base.OnCloseMethod(sender, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}

