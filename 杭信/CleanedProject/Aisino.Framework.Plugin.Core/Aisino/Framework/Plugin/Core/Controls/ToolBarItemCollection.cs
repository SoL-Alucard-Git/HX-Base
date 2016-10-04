namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Threading;

    public class ToolBarItemCollection : IEnumerable
    {
        private ArrayList arrayList_0;

        public event EventHandler Changed;

        public ToolBarItemCollection()
        {
            
            this.arrayList_0 = new ArrayList();
        }

        public int Add(ToolBarItem toolBarItem_0)
        {
            if (this.Contains(toolBarItem_0))
            {
                return -1;
            }
            int num = this.arrayList_0.Add(toolBarItem_0);
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

        public bool Contains(ToolBarItem toolBarItem_0)
        {
            return this.arrayList_0.Contains(toolBarItem_0);
        }

        public IEnumerator GetEnumerator()
        {
            return this.arrayList_0.GetEnumerator();
        }

        public int IndexOf(ToolBarItem toolBarItem_0)
        {
            return this.arrayList_0.IndexOf(toolBarItem_0);
        }

        public void Insert(int int_0, ToolBarItem toolBarItem_0)
        {
            this.arrayList_0.Insert(int_0, toolBarItem_0);
            this.method_0();
        }

        private void method_0()
        {
            if (this.Changed != null)
            {
                this.Changed(this, null);
            }
        }

        public void Remove(ToolBarItem toolBarItem_0)
        {
            this.arrayList_0.Remove(toolBarItem_0);
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

        public ToolBarItem this[int int_0]
        {
            get
            {
                return (ToolBarItem) this.arrayList_0[int_0];
            }
            set
            {
                this.arrayList_0[int_0] = value;
            }
        }
    }
}

