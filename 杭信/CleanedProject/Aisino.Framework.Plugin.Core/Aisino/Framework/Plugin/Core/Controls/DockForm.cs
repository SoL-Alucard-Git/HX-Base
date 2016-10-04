namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class DockForm : BaseForm
    {
        protected TabControlEx _owner;
        protected Form _parent;
        protected static FormStyle _showStyle;
        protected string _tabText;
        private IContainer icontainer_2;

        public DockForm()
        {
            
            this.method_2();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_2 != null))
            {
                this.icontainer_2.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((_showStyle == FormStyle.Old) && (this._parent != null))
            {
                ((DockForm) this._parent).SetTSBStates(this);
            }
        }

        public bool HasShow()
        {
            if (_showStyle == FormStyle.New)
            {
                if (this._owner != null)
                {
                    TabPage page = this._owner.TabPages[base.Name];
                    if (page != null)
                    {
                        this._owner.SelectedTab = page;
                        return true;
                    }
                }
                return false;
            }
            bool flag = false;
            if (this._parent != null)
            {
                Form[] mdiChildren = this._parent.MdiChildren;
                if (mdiChildren == null)
                {
                    return flag;
                }
                foreach (Form form in mdiChildren)
                {
                    if (form.Name == base.Name)
                    {
                        return true;
                    }
                }
            }
            return flag;
        }

        private void method_1()
        {
            if (this._owner != null)
            {
                TabPage page = this._owner.TabPages[base.Name];
                if (page != null)
                {
                    page.Text = this._tabText;
                }
            }
        }

        private void method_2()
        {
            this.icontainer_2 = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "DockForm";
        }

        public virtual void SetTSBStates(object object_0)
        {
        }

        public void Show(Control control_0)
        {
            if (_showStyle != FormStyle.New)
            {
                this._owner = null;
                Form form2 = control_0 as Form;
                this._parent = form2;
                if (form2 != null)
                {
                    Form[] formArray4 = form2.MdiChildren;
                    if (formArray4 != null)
                    {
                        foreach (Form form3 in formArray4)
                        {
                            if (form3 is DockForm)
                            {
                                form3.Close();
                            }
                        }
                    }
                }
                Form[] mdiChildren = form2.MdiChildren;
                bool flag = true;
                if (mdiChildren != null)
                {
                    foreach (Form form in mdiChildren)
                    {
                        if (form is DockForm)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    base.MdiParent = form2;
                    base.CenterToParent();
                    base.MinimizeBox = false;
                    base.WindowState = FormWindowState.Maximized;
                    if (form2 is DockForm)
                    {
                        ((DockForm) form2).vmethod_0("  关闭  ");
                    }
                    base.FormClosed += new FormClosedEventHandler(this.DockForm_FormClosed);
                    base.Show();
                }
                return;
            }
            this._parent = null;
            this._owner = control_0 as TabControlEx;
            TabPage page = null;
            IEnumerator enumerator = this._owner.TabPages.GetEnumerator();
            {
                TabPage current;
                while (enumerator.MoveNext())
                {
                    current = (TabPage) enumerator.Current;
                    if ((current.Name == base.Name) && (current.Text == this.Text))
                    {
                        goto Label_0073;
                    }
                }
                goto Label_008C;
            Label_0073:
                page = current;
            }
        Label_008C:
            if (page == null)
            {
                this.Text = this.Text.Trim() + "    ";
                TabPageEx ex = new TabPageEx {
                    Name = base.Name,
                    Text = this.Text
                };
                this._owner.AddNewPage(ex);
                this._owner.SelectedIndex = this._owner.TabCount - 1;
                base.TopLevel = false;
                base.FormBorderStyle = FormBorderStyle.None;
                ex.AddForm(this);
                this.Dock = DockStyle.Fill;
                base.Show();
            }
            else
            {
                this._owner.SelectedTab = page;
            }
        }

        public virtual void vmethod_0(string string_0)
        {
        }

        public string TabText
        {
            get
            {
                if (_showStyle == FormStyle.New)
                {
                    return this._tabText;
                }
                return this.Text;
            }
            set
            {
                if (_showStyle == FormStyle.New)
                {
                    this._tabText = value;
                    this.method_1();
                }
                else
                {
                    this.Text = value;
                }
            }
        }
    }
}

