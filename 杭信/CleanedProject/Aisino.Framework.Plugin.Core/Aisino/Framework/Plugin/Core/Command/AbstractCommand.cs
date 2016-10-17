namespace Aisino.Framework.Plugin.Core.Command
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using ns10;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public abstract class AbstractCommand : Interface1, Interface2
    {
        private object object_0;
        private object[] object_1;

        protected AbstractCommand()
        {
            
        }

        private void method_0(object sender, FormClosedEventArgs e)
        {
            string str = PropertyUtil.GetValue("MAIN_FORMSTYLE");
            if ((!string.IsNullOrEmpty(str) && (this.object_0 != null)) && ((str.ToUpper() == "OLD") && (this.object_0 is DockForm)))
            {
                ((DockForm) this.object_0).SetTSBStates(sender);
            }
        }

        void Interface1.imethod_0()
        {
            this.RunCommand();
        }

        bool Interface1.imethod_3()
        {
            //逻辑修改:测试时无需校验直接通过
            if(InternetWare.Config.Constants.IsTest)
                return true;
            return this.SetValid();
        }

        protected virtual void RunCommand()
        {
        }

        protected virtual bool SetValid()
        {
            return true;
        }

        protected DockForm ShowForm<T>()
        {
            string str = PropertyUtil.GetValue("MAIN_FORMSTYLE");
            if (!string.IsNullOrEmpty(str))
            {
                if (!(str.ToUpper() == "OLD"))
                {
                    if (!(str.ToUpper() == "NEW"))
                    {
                        goto Label_01F7;
                    }
                    TabControlEx ex = (TabControlEx) this.object_0;
                    IEnumerator enumerator = ex.TabPages.GetEnumerator();
                    {
                        TabPage current;
                        while (enumerator.MoveNext())
                        {
                            current = (TabPage) enumerator.Current;
                            if ((current is TabPageEx) && (((TabPageEx) current).FormType == typeof(T).FullName))
                            {
                                goto Label_0192;
                            }
                        }
                        System.Type type2 = typeof(T);
                        DockForm form5 = (DockForm) Activator.CreateInstance(type2);
                        form5.Show(ex);
                        return form5;
                    Label_0192:
                        ((TabControlEx) this.object_0).SelectedTab = current;
                        return (((TabPageEx) current).FormInstance as DockForm);
                    }
                }
                System.Type type = typeof(T);
                DockForm form = (DockForm) Activator.CreateInstance(type);
                Form form2 = this.object_0 as Form;
                if (form2 != null)
                {
                    Form[] formArray = form2.MdiChildren;
                    if (formArray != null)
                    {
                        foreach (Form form3 in formArray)
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
                    foreach (Form form4 in mdiChildren)
                    {
                        if (form4 is DockForm)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    form.MdiParent = form2;
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.MinimizeBox = false;
                    form.WindowState = FormWindowState.Maximized;
                    if (form2 is DockForm)
                    {
                        ((DockForm) form2).vmethod_0("  关闭  ");
                    }
                    form.FormClosed += new FormClosedEventHandler(this.method_0);
                    form.Show();
                    return form;
                }
            }
        Label_01F7:
            return null;
        }

        public object imethod_1()
        {
            return this.object_0;
        }

        public void imethod_2(object object_0)
        {
            this.object_0 = object_0;
        }

        //object Interface1.Owner
        //{
        //    get
        //    {
        //        return this.object_0;
        //    }
        //    set
        //    {
        //        this.object_0 = value;
        //    }
        //}
    }
}

