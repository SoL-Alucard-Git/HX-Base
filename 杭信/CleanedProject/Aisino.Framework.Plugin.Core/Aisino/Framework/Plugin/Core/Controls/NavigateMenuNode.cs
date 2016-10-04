namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Tree;
    using System;
    using System.Drawing;

    public class NavigateMenuNode
    {
        private Bitmap bitmap_0;
        private bool bool_0;
        private NavigateMenuNodeCollection navigateMenuNodeCollection_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private TreeNodeCommand treeNodeCommand_0;

        public NavigateMenuNode()
        {
            
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.string_3 = string.Empty;
            this.navigateMenuNodeCollection_0 = new NavigateMenuNodeCollection();
        }

        public NavigateMenuNode(string string_4, string string_5, Bitmap bitmap_1) : this()
        {
            
            this.string_1 = string_4;
            this.string_2 = string_5;
            this.bitmap_0 = bitmap_1;
        }

        public NavigateMenuNode(string string_4, string string_5, Bitmap bitmap_1, TreeNodeCommand treeNodeCommand_1) : this(string_4, string_5, bitmap_1)
        {
            
            this.treeNodeCommand_0 = treeNodeCommand_1;
        }

        public NavigateMenuNode(string string_4, string string_5, Bitmap bitmap_1, string string_6) : this()
        {
            
            this.string_1 = string_4;
            this.string_2 = string_5;
            this.bitmap_0 = bitmap_1;
            this.string_0 = string_6;
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

        public string ImageKey
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public bool IsExpand
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
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

        public NavigateMenuNodeCollection Node
        {
            get
            {
                return this.navigateMenuNodeCollection_0;
            }
        }

        public TreeNodeCommand NodeCommand
        {
            get
            {
                return this.treeNodeCommand_0;
            }
            set
            {
                this.treeNodeCommand_0 = value;
            }
        }

        public string SimpleText
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

