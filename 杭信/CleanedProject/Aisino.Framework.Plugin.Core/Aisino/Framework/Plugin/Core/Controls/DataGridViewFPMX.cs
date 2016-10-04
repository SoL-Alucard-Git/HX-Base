namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataGridViewFPMX : DataGridView
    {
        private List<int> list_0;

        public DataGridViewFPMX()
        {
            
            this.list_0 = new List<int>();
            this.method_0();
        }

        private void method_0()
        {
            base.BackgroundColor = SystemColors.ButtonHighlight;
            base.BorderStyle = BorderStyle.None;
            base.CellBorderStyle = DataGridViewCellBorderStyle.None;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            base.GridColor = Color.Black;
            base.RowHeadersVisible = false;
            base.ScrollBars = ScrollBars.Vertical;
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex < (base.Columns.Count - 1))
                {
                    this.list_0.Add(e.CellBounds.Right);
                }
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.CellBounds);
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            base.OnCellPainting(e);
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.Invalidate();
            base.OnColumnWidthChanged(e);
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.Invalidate();
            base.OnCurrentCellChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            using (Pen pen = new Pen(Color.Black, 1f))
            {
                int num = 0;
                if (this.list_0.Count > 0)
                {
                    num = this.list_0.Count - 1;
                }
                for (int i = 0; i < num; i++)
                {
                    graphics.DrawLine(pen, this.list_0[i], 0, this.list_0[i], base.Height);
                }
            }
            this.list_0.Clear();
        }

        protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
        {
            base.Invalidate();
            base.OnRowHeightChanged(e);
        }
    }
}

