namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Row : Styles, IEnumerable<Cell>, IEnumerable
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private double double_0;
        internal int int_0;
        internal List<Cell> list_0;
        internal Worksheet worksheet_0;

        internal Row(Worksheet worksheet_1, int int_1)
        {
            
            if (worksheet_1 == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.list_0 = new List<Cell>();
            this.worksheet_0 = worksheet_1;
            this.Height = 0.0;
            this.int_0 = int_1;
            if (worksheet_1.Style != null)
            {
                base.Style = worksheet_1.Style;
            }
        }

        public Cell AddCell()
        {
            return this[this.list_0.Count];
        }

        public void Delete()
        {
            this.worksheet_0.DeleteRow(this);
        }

        public void DeleteCell(Cell cell_0)
        {
            Cell cell = cell_0;
            if (cell != null)
            {
                this.DeleteCells(this.list_0.FindIndex(r => r == cell), 1, true);
            }
        }

        public void DeleteCell(int int_1)
        {
            this.DeleteCells(int_1, 1, true);
        }

        public void DeleteCell(Cell cell_0, bool bool_1)
        {
            Cell cell = cell_0;
            if (cell != null)
            {
                this.DeleteCells(this.list_0.FindIndex(r => r == cell), 1, bool_1);
            }
        }

        public void DeleteCell(int int_1, bool bool_1)
        {
            this.DeleteCells(int_1, 1, bool_1);
        }

        public void DeleteCells(Cell cell_0, int int_1)
        {
            Cell cell = cell_0;
            if (cell != null)
            {
                this.DeleteCells(this.list_0.FindIndex(r => r == cell), int_1, true);
            }
        }

        public void DeleteCells(int int_1, int int_2)
        {
            this.DeleteCells(int_1, int_2, true);
        }

        public void DeleteCells(Cell cell_0, int int_1, bool bool_1)
        {
            Cell cell = cell_0;
            if (cell != null)
            {
                this.DeleteCells(this.list_0.FindIndex(r => r == cell), int_1, bool_1);
            }
        }

        public void DeleteCells(int int_1, int int_2, bool bool_1)
        {
            if ((int_2 >= 0) && ((int_1 >= 0) && (int_1 < this.list_0.Count)))
            {
                if ((int_1 + int_2) > this.list_0.Count)
                {
                    int_2 = this.list_0.Count - int_1;
                }
                for (int i = int_1; i < (int_1 + int_2); i++)
                {
                    this.list_0[int_1].method_6(!bool_1);
                    if (bool_1)
                    {
                        this.list_0.RemoveAt(int_1);
                    }
                }
                if (bool_1)
                {
                    this.method_6(int_1);
                }
            }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            int iteratorVariable0 = 0;
            while (true)
            {
                if (iteratorVariable0 > this.worksheet_0.int_0)
                {
                    yield break;
                }
                yield return this[iteratorVariable0];
                iteratorVariable0++;
            }
        }

        public Cell InsertCellAfter(Cell cell_0)
        {
            Cell cell = cell_0;
            return this.InsertCellAfter(this.list_0.FindIndex(r => r == cell));
        }

        public Cell InsertCellAfter(int int_1)
        {
            if (int_1 < 0)
            {
                return this.AddCell();
            }
            if (int_1 >= (this.list_0.Count - 1))
            {
                return this[int_1 + 1];
            }
            this.InsertCellsAfter(int_1, 1);
            return this.list_0[int_1];
        }

        public Cell InsertCellBefore(Cell cell_0)
        {
            Cell cell = cell_0;
            return this.InsertCellBefore(this.list_0.FindIndex(r => r == cell));
        }

        public Cell InsertCellBefore(int int_1)
        {
            if (int_1 < 0)
            {
                return this.AddCell();
            }
            if (int_1 >= this.list_0.Count)
            {
                return this[int_1];
            }
            this.InsertCellsBefore(int_1, 1);
            return this.list_0[int_1];
        }

        public void InsertCellsAfter(Cell cell_0, int int_1)
        {
            Cell cell = cell_0;
            if (cell != null)
            {
                this.InsertCellsAfter(this.list_0.FindIndex(r => r == cell), int_1);
            }
        }

        public void InsertCellsAfter(int int_1, int int_2)
        {
            if (((int_2 >= 0) && (int_1 >= 0)) && (int_1 < (this.list_0.Count - 1)))
            {
                for (int i = int_1; i < (int_1 + int_2); i++)
                {
                    Cell item = new Cell(this, int_1);
                    this.list_0.Insert(int_1 + 1, item);
                }
                this.method_6(int_1);
            }
        }

        public void InsertCellsBefore(Cell cell_0, int int_1)
        {
            Cell cell = cell_0;
            this.InsertCellsBefore(this.list_0.FindIndex(r => r == cell), int_1);
        }

        public void InsertCellsBefore(int int_1, int int_2)
        {
            if (((int_2 >= 0) && (int_1 >= 0)) && (int_1 < this.list_0.Count))
            {
                for (int i = int_1; i < (int_1 + int_2); i++)
                {
                    Cell item = new Cell(this, int_1);
                    this.list_0.Insert(int_1, item);
                }
                this.method_6(int_1);
            }
        }

        private Cell method_5(int int_1)
        {
            if (int_1 < 0)
            {
                throw new ArgumentOutOfRangeException("colIndex");
            }
            if ((int_1 + 1) > this.list_0.Count)
            {
                for (int i = this.list_0.Count; i <= int_1; i++)
                {
                    this.list_0.Add(new Cell(this, i));
                }
            }
            this.worksheet_0.int_0 = Math.Max(int_1, this.worksheet_0.int_0);
            return this.list_0[int_1];
        }

        internal void method_6(int int_1)
        {
            for (int i = int_1; i < this.list_0.Count; i++)
            {
                this.list_0[i].int_0 = i;
            }
        }

        internal void method_7()
        {
            this.worksheet_0 = null;
            this.list_0.Clear();
            this.list_0 = null;
        }

        internal void method_8(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Row");
            if ((!base.StyleID.IsNullOrEmpty() && (this.worksheet_0.StyleID != base.StyleID)) && (base.StyleID != "Default"))
            {
                xmlWriter_0.WriteAttributeString("ss", "StyleID", null, base.StyleID);
            }
            if (this.Height != 0.0)
            {
                xmlWriter_0.WriteAttributeString("ss", "AutoFitHeight", null, "0");
                xmlWriter_0.WriteAttributeString("ss", "Height", null, this.Height.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Hidden)
            {
                xmlWriter_0.WriteAttributeString("ss", "Hidden", null, "1");
            }
            bool flag = false;
            foreach (Cell cell in this.list_0)
            {
                if (cell.IsEmpty() && !cell.bool_0)
                {
                    flag = true;
                }
                else
                {
                    cell.method_7(xmlWriter_0, flag);
                    flag = false;
                }
            }
            xmlWriter_0.WriteEndElement();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal override ExcelXmlWorkbook vmethod_0()
        {
            return this.worksheet_0.excelXmlWorkbook_0;
        }

        internal override void vmethod_1(Styles.Delegate36 delegate36_0)
        {
        }

        internal override Cell vmethod_2()
        {
            return null;
        }

        public int CellCount
        {
            get
            {
                return this.list_0.Count;
            }
        }

        public double Height
        {
            [CompilerGenerated]
            get
            {
                return this.double_0;
            }
            [CompilerGenerated]
            set
            {
                this.double_0 = value;
            }
        }

        public bool Hidden
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

        public Cell this[int int_1]
        {
            get
            {
                return this.method_5(int_1);
            }
        }

    }
}

