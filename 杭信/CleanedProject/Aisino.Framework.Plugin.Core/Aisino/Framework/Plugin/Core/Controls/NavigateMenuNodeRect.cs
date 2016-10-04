namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;

    public class NavigateMenuNodeRect
    {
        private NavigateMenuNode navigateMenuNode_0;
        private Rectangle rectangle_0;

        public NavigateMenuNodeRect(NavigateMenuNode navigateMenuNode_1, Rectangle rectangle_1)
        {
            
            this.navigateMenuNode_0 = navigateMenuNode_1;
            this.rectangle_0 = rectangle_1;
        }

        public NavigateMenuNode Node
        {
            get
            {
                return this.navigateMenuNode_0;
            }
            set
            {
                this.navigateMenuNode_0 = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return this.rectangle_0;
            }
            set
            {
                this.rectangle_0 = value;
            }
        }
    }
}

