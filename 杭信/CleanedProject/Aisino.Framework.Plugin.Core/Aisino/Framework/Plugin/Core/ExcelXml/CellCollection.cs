namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Collections.Generic;

    public class CellCollection : List<Cell>
    {
        public CellCollection()
        {
            
        }

        public void Add(Range range_0)
        {
            foreach (Cell cell in range_0)
            {
                base.Add(cell);
            }
        }

        public void Add(Row row_0)
        {
            foreach (Cell cell in row_0)
            {
                base.Add(cell);
            }
        }

        public void Add(Worksheet worksheet_0)
        {
            foreach (Cell cell in worksheet_0)
            {
                base.Add(cell);
            }
        }

        public void Add(Cell cell_0, Predicate<Cell> filterCondition)
        {
            if (filterCondition(cell_0))
            {
                base.Add(cell_0);
            }
        }

        public void Add(Range range_0, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in range_0)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }

        public void Add(Row row_0, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in row_0)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }

        public void Add(Worksheet worksheet_0, Predicate<Cell> filterCondition)
        {
            foreach (Cell cell in worksheet_0)
            {
                if (filterCondition(cell))
                {
                    base.Add(cell);
                }
            }
        }
    }
}

