namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Threading;

    public class NavigateMenuNodeCollection : IEnumerable
    {
        private ArrayList arrayList_0;

        public event EventHandler Changed;

        internal NavigateMenuNodeCollection()
        {
            
            this.arrayList_0 = new ArrayList();
        }

        public int Add(NavigateMenuNode navigateMenuNode_0)
        {
            if (this.Contains(navigateMenuNode_0))
            {
                return -1;
            }
            int num = this.arrayList_0.Add(navigateMenuNode_0);
            this.method_0();
            return num;
        }

        public void Clear()
        {
            while (this.Count > 0)
            {
                this.RemoveAt(0);
            }
        }

        public bool Contains(NavigateMenuNode navigateMenuNode_0)
        {
            return this.arrayList_0.Contains(navigateMenuNode_0);
        }

        public IEnumerator GetEnumerator()
        {
            return this.arrayList_0.GetEnumerator();
        }

        public int IndexOf(NavigateMenuNode navigateMenuNode_0)
        {
            return this.arrayList_0.IndexOf(navigateMenuNode_0);
        }

        public void Insert(int int_0, NavigateMenuNode navigateMenuNode_0)
        {
            this.arrayList_0.Insert(int_0, navigateMenuNode_0);
            this.method_0();
        }

        private void method_0()
        {
            if (this.Changed != null)
            {
                this.Changed(this, null);
            }
        }

        public void Remove(NavigateMenuNode navigateMenuNode_0)
        {
            this.arrayList_0.Remove(navigateMenuNode_0);
            this.method_0();
        }

        public void RemoveAt(int int_0)
        {
            this.arrayList_0.RemoveAt(int_0);
            this.method_0();
        }

        public int Count
        {
            get
            {
                return this.arrayList_0.Count;
            }
        }

        public NavigateMenuNode this[int int_0]
        {
            get
            {
                return (NavigateMenuNode) this.arrayList_0[int_0];
            }
            set
            {
                this.arrayList_0[int_0] = value;
            }
        }
    }
}

