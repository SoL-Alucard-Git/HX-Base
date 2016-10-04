namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core.Const;
    using System;
    using System.Windows.Forms;

    public class ControlStyleUtil
    {
        public ControlStyleUtil()
        {
            
        }

        public static void SetToolStripStyle(ToolStrip toolStrip_0)
        {
            toolStrip_0.AutoSize = false;
            toolStrip_0.Height = SystemColor.TOOLSCRIPT_HEIGHT;
            toolStrip_0.BackColor = SystemColor.BACKCOLOR;
            toolStrip_0.RightToLeft = RightToLeft.Yes;
            toolStrip_0.Font = SystemColor.TOOLSCRIPT_BOLD_FONT;
            foreach (ToolStripItem item in toolStrip_0.Items)
            {
                item.AutoSize = false;
                item.Height = SystemColor.TOOLSCRIPT_CONTROL_HEIGHT;
                item.Font = SystemColor.TOOLSCRIPT_BOLD_FONT;
                item.RightToLeft = RightToLeft.No;
                if ((item is ToolStripComboBox) || (item is ToolStripTextBox))
                {
                    item.BackColor = SystemColor.GRID_ALTROW_BACKCOLOR;
                    item.Font = SystemColor.TOOLSCRIPT_FONT;
                }
                else if ((item is ToolStripButton) || (item is ToolStripLabel))
                {
                    if (item.Text.Length == 2)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_TWO;
                        }
                        else
                        {
                            item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_TWO;
                        }
                    }
                    else if (item.Text.Length == 3)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_THREE;
                        }
                        else
                        {
                            item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_THREE;
                        }
                    }
                    else if (item.Text.Length == 4)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_FOUR;
                        }
                        else
                        {
                            item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_FOUR;
                        }
                    }
                    else if (item.Text.Length == 5)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_FIVE;
                        }
                        else
                        {
                            item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_FIVE;
                        }
                    }
                    else if (item.Text.Length == 6)
                    {
                        if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                        {
                            item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_SIX;
                        }
                        else
                        {
                            item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_SIX;
                        }
                    }
                    else if (item.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                    {
                        item.Width = SystemColor.TOOLSCRIPT_FONT_WIDTH_TWO;
                    }
                    else
                    {
                        item.Width = SystemColor.TOOLSCRIPT_LABEL_WIDTH_TWO;
                    }
                }
            }
        }
    }
}

