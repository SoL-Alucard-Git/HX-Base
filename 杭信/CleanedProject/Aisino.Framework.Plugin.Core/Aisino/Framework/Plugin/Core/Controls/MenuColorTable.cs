namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;

    public class MenuColorTable
    {
        private Color color_0;
        private Color color_1;
        private Color color_10;
        private Color color_11;
        private Color color_12;
        private Color color_13;
        private Color color_14;
        private Color color_15;
        private Color color_16;
        private Color color_2;
        private Color color_3;
        private Color color_4;
        private Color color_5;
        private Color color_6;
        private Color color_7;
        private Color color_8;
        private Color color_9;

        public MenuColorTable()
        {
            
            this.color_0 = ColorTranslator.FromHtml("#f2f4f5");
            this.color_1 = ColorTranslator.FromHtml("#9EA2A6");
            this.color_2 = ColorTranslator.FromHtml("#DAE9F3");
            this.color_3 = ColorTranslator.FromHtml("#E4EBF0");
            this.color_4 = ColorTranslator.FromHtml("#D3E1EB");
            this.color_5 = ColorTranslator.FromHtml("#A7BECF");
            this.color_6 = ColorTranslator.FromHtml("#8db0d2");
            this.color_7 = ColorTranslator.FromHtml("#D2E0EA");
            this.color_8 = ColorTranslator.FromHtml("#9EA2A6");
            this.color_9 = ColorTranslator.FromHtml("#B8CBD9");
            this.color_10 = Color.Black;
            this.color_11 = ColorTranslator.FromHtml("#8db0d2");
            this.color_12 = ColorTranslator.FromHtml("#FDFDFD");
            this.color_13 = ColorTranslator.FromHtml("#ECECEC");
            this.color_14 = ColorTranslator.FromHtml("#B8CBD9");
            this.color_15 = ColorTranslator.FromHtml("#9EA2A6");
            this.color_16 = ColorTranslator.FromHtml("#A0B7C8");
        }

        public virtual Color BackColor
        {
            get
            {
                return this.color_0;
            }
        }

        public virtual Color MenuBackColor
        {
            get
            {
                return this.color_3;
            }
        }

        public virtual Color MenuBoderInternal
        {
            get
            {
                return this.color_2;
            }
        }

        public virtual Color MenuBoderOuter
        {
            get
            {
                return this.color_1;
            }
        }

        public virtual Color MenuDarkBackColor
        {
            get
            {
                return this.color_9;
            }
        }

        public virtual Color MenuFont
        {
            get
            {
                return this.color_10;
            }
        }

        public virtual Color MenuMoreHover
        {
            get
            {
                return this.color_11;
            }
        }

        public virtual Color MenuMouseHover
        {
            get
            {
                return this.color_6;
            }
        }

        public virtual Color MenuMousePress
        {
            get
            {
                return this.color_7;
            }
        }

        public virtual Color MenuSplitBottom
        {
            get
            {
                return this.color_5;
            }
        }

        public virtual Color MenuSplitTop
        {
            get
            {
                return this.color_4;
            }
        }

        public virtual Color MenuVertualLine
        {
            get
            {
                return this.color_8;
            }
        }

        public virtual Color ToolBorderColor
        {
            get
            {
                return this.color_15;
            }
        }

        public virtual Color ToolBottomBackColor
        {
            get
            {
                return this.color_13;
            }
        }

        public virtual Color ToolHideBarBackColor
        {
            get
            {
                return this.color_16;
            }
        }

        public virtual Color ToolMouseHover
        {
            get
            {
                return this.color_14;
            }
        }

        public virtual Color ToolTopBackColor
        {
            get
            {
                return this.color_12;
            }
        }
    }
}

