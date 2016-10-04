namespace Aisino.Framework.Plugin.Core.Controls.OutlookBar
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Threading;

    public class OutlookBarNodeCollection : IEnumerable
    {
        private ArrayList arrayList_0;

        public event EventHandler Changed;

        public OutlookBarNodeCollection()
        {
            
            this.arrayList_0 = new ArrayList();
        }

        public int Add(OutlookBarNode outlookBarNode_0)
        {
            if (this.Contains(outlookBarNode_0))
            {
                return -1;
            }
            int num = this.arrayList_0.Add(outlookBarNode_0);
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

        public bool Contains(OutlookBarNode outlookBarNode_0)
        {
            return this.arrayList_0.Contains(outlookBarNode_0);
        }

        public IEnumerator GetEnumerator()
        {
            return this.arrayList_0.GetEnumerator();
        }

        public int IndexOf(OutlookBarNode outlookBarNode_0)
        {
            return this.arrayList_0.IndexOf(outlookBarNode_0);
        }

        public void Insert(int int_0, OutlookBarNode outlookBarNode_0)
        {
            this.arrayList_0.Insert(int_0, outlookBarNode_0);
            this.method_0();
        }

        private void method_0()
        {
            if (this.Changed != null)
            {
                this.Changed(this, null);
            }
        }

        public void Remove(OutlookBarNode outlookBarNode_0)
        {
            this.arrayList_0.Remove(outlookBarNode_0);
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

        public OutlookBarNode this[int int_0]
        {
            get
            {
                return (OutlookBarNode) this.arrayList_0[int_0];
            }
            set
            {
                this.arrayList_0[int_0] = value;
            }
        }
    }
}

