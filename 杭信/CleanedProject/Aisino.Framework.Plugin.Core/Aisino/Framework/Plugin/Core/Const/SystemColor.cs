namespace Aisino.Framework.Plugin.Core.Const
{
    using System;
    using System.Drawing;

    public class SystemColor
    {
        public static readonly Color BACKCOLOR;
        public static readonly Font FONT;
        public static readonly Color FONT_COLOR;
        public static readonly Color GRID_ALTROW_BACKCOLOR;
        public static readonly Color GRID_BACKCOLOR;
        public static readonly Font GRID_BODY_FONT;
        public static readonly Color GRID_BODY_FONTCOLOR;
        public static readonly Color GRID_CONTROL_BACKCOLOR;
        public static readonly Color GRID_ROW_BACKCOLOR;
        public static readonly int GRID_ROW_HEIGHT;
        public static readonly Color GRID_ROWHOVER_BACKCOLOR;
        public static readonly Color GRID_TITLE_BACKCOLOR;
        public static readonly Font GRID_TITLE_FONT;
        public static readonly Color GRID_TITLE_FONTCOLOR;
        public static readonly int GRID_TITLE_HEIGHT;
        public static readonly Color GROUP_BACKCOLOR;
        public static readonly Color INVGRID_ALTROW_BACKCOLOR;
        public static readonly Color INVGRID_BACKCOLOR;
        public static readonly Font INVGRID_BODY_FONT;
        public static readonly Color INVGRID_BODY_FONTCOLOR;
        public static readonly Color INVGRID_CONTROL_BACKCOLOR;
        public static readonly Color INVGRID_ROW_BACKCOLOR;
        public static readonly int INVGRID_ROW_HEIGHT;
        public static readonly int INVGRID_ROWHEAD_WIDTH;
        public static readonly Color INVGRID_ROWHOVER_BACKCOLOR;
        public static readonly Color INVGRID_TITLE_BACKCOLOR;
        public static readonly Font INVGRID_TITLE_FONT;
        public static readonly Color INVGRID_TITLE_FONTCOLOR;
        public static readonly int INVGRID_TITLE_HEIGHT;
        public static readonly Font TOOLSCRIPT_BOLD_FONT;
        public static readonly int TOOLSCRIPT_CONTROL_HEIGHT;
        public static readonly Font TOOLSCRIPT_FONT;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_FIVE;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_FOUR;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_MORE_SIX;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_SIX;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_THREE;
        public static readonly int TOOLSCRIPT_FONT_WIDTH_TWO;
        public static readonly int TOOLSCRIPT_HEIGHT;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_FIVE;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_FOUR;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_MORE_SIX;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_SIX;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_THREE;
        public static readonly int TOOLSCRIPT_LABEL_WIDTH_TWO;

        static SystemColor()
        {
            
            FONT_COLOR = Color.Black;
            BACKCOLOR = Color.White;
            FONT = new Font("宋体", 9f);
            GROUP_BACKCOLOR = Color.FromArgb(60, 0, 0x62);
            GRID_TITLE_HEIGHT = 0x24;
            GRID_ROW_HEIGHT = 30;
            GRID_BACKCOLOR = ColorTranslator.FromHtml("#FFFFFF");
            GRID_CONTROL_BACKCOLOR = ColorTranslator.FromHtml("#F0FAFF");
            GRID_TITLE_BACKCOLOR = ColorTranslator.FromHtml("#1587CA");
            GRID_ALTROW_BACKCOLOR = ColorTranslator.FromHtml("#F0FAFF");
            GRID_ROW_BACKCOLOR = ColorTranslator.FromHtml("#DCE6F0");
            GRID_ROWHOVER_BACKCOLOR = ColorTranslator.FromHtml("#7D96B9");
            GRID_TITLE_FONTCOLOR = ColorTranslator.FromHtml("#FFFFFF");
            GRID_BODY_FONTCOLOR = ColorTranslator.FromHtml("#000000");
            GRID_TITLE_FONT = new Font("微软雅黑", 10f);
            GRID_BODY_FONT = new Font("宋体", 10f);
            TOOLSCRIPT_BOLD_FONT = new Font("微软雅黑", 10f);
            TOOLSCRIPT_FONT = new Font("微软雅黑", 10f);
            TOOLSCRIPT_HEIGHT = 50;
            TOOLSCRIPT_CONTROL_HEIGHT = 0x20;
            TOOLSCRIPT_FONT_WIDTH_TWO = 60;
            TOOLSCRIPT_FONT_WIDTH_THREE = 70;
            TOOLSCRIPT_FONT_WIDTH_FOUR = 90;
            TOOLSCRIPT_FONT_WIDTH_FIVE = 100;
            TOOLSCRIPT_FONT_WIDTH_SIX = 110;
            TOOLSCRIPT_FONT_WIDTH_MORE_SIX = 130;
            TOOLSCRIPT_LABEL_WIDTH_TWO = 40;
            TOOLSCRIPT_LABEL_WIDTH_THREE = 50;
            TOOLSCRIPT_LABEL_WIDTH_FOUR = 60;
            TOOLSCRIPT_LABEL_WIDTH_FIVE = 70;
            TOOLSCRIPT_LABEL_WIDTH_SIX = 90;
            TOOLSCRIPT_LABEL_WIDTH_MORE_SIX = 130;
            INVGRID_TITLE_HEIGHT = 0x18;
            INVGRID_ROW_HEIGHT = 20;
            INVGRID_ROWHEAD_WIDTH = 0x12;
            INVGRID_BACKCOLOR = ColorTranslator.FromHtml("#FFFFFF");
            INVGRID_CONTROL_BACKCOLOR = ColorTranslator.FromHtml("#F0FAFF");
            INVGRID_TITLE_BACKCOLOR = ColorTranslator.FromHtml("#FFFFFF");
            INVGRID_ALTROW_BACKCOLOR = ColorTranslator.FromHtml("#F0FAFF");
            INVGRID_ROW_BACKCOLOR = ColorTranslator.FromHtml("#DCE6F0");
            INVGRID_ROWHOVER_BACKCOLOR = ColorTranslator.FromHtml("#7D96B9");
            INVGRID_TITLE_FONTCOLOR = ColorTranslator.FromHtml("#000000");
            INVGRID_BODY_FONTCOLOR = ColorTranslator.FromHtml("#000000");
            INVGRID_TITLE_FONT = new Font("宋体", 9f);
            INVGRID_BODY_FONT = new Font("宋体", 9f);
        }

        public SystemColor()
        {
            
        }
    }
}

