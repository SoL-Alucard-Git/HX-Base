namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        [CompilerGenerated]
        private bool bool_0;

        public DataGridViewDisableCheckBoxCell()
        {
            
            this.Enabled = true;
        }

        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell) base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (!this.Enabled)
            {
                Point point = new Point();
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush brush = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(brush, cellBounds);
                    brush.Dispose();
                }
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }
                Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.MixedDisabled);
                point = new Point(cellBounds.X, cellBounds.Y) {
                    X = point.X + ((cellBounds.Width - glyphSize.Width) / 2),
                    Y = point.Y + ((cellBounds.Height - glyphSize.Height) / 2)
                };
                CheckBoxRenderer.DrawCheckBox(graphics, point, CheckBoxState.MixedDisabled);
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }

        public bool Enabled
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }
    }
}

