namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;

    public class TreeNodeTemp
    {
        private int int_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public TreeNodeTemp()
        {
            
        }

        public string BM
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public int Foldfile
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string ParentBM
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

