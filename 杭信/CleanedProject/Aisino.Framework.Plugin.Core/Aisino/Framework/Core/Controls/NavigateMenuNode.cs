namespace Aisino.Framework.Core.Controls
{
    using System;
    using System.Drawing;

    public class NavigateMenuNode
    {
        private Bitmap bitmap_0;
        private string string_0;
        private string string_1;

        public NavigateMenuNode()
        {
            
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
        }

        public void Add(NavigateMenuNode navigateMenuNode_0)
        {
        }

        public Bitmap Icon
        {
            get
            {
                return this.bitmap_0;
            }
            set
            {
                this.bitmap_0 = value;
            }
        }

        public string Name
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

        public string Text
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

