using System;
using System.Collections.Generic;
using System.IO;

namespace Gcj
{
    class Grid<T> where T : IComparable
    {
        protected T[,] Xss;

        internal int Length => Xss.Length;
        internal int RowLength => Xss.GetLength(0);
        internal int ColumnLength => Xss.GetLength(1);
        internal T this[int ri, int ci] => Xss[ri, ci];

        protected Grid() { }

        Grid(T[,] xss)
        {
            Xss = xss;
        }

        internal int Count(T item)
        {
            var count = 0;

            for (var ri = 0; ri < RowLength; ri++)
            {
                for (var ci = 0; ci < ColumnLength; ci++)
                {
                    if (Xss[ri, ci].CompareTo(item) == 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        internal Grid<T> CreateSubgrid(int startRowIndex, int startColumnIndex, int rowLength = -1, int columnLength = -1)
        {
            if (rowLength == -1)
            {
                rowLength = RowLength - startRowIndex;
            }

            if (columnLength == -1)
            {
                columnLength = ColumnLength - startColumnIndex;
            }

            var xss = new T[rowLength, columnLength];

            for (var ri = 0; ri < rowLength; ri++)
            {
                for (var ci = 0; ci < columnLength; ci++)
                {
                    xss[ri, ci] = Xss[startRowIndex + ri, startColumnIndex + ci];
                }
            }

            return new Grid<T>(xss);
        }

        internal void Fill(T item)
        {
            for (var ri = 0; ri < RowLength; ri++)
            {
                for (var ci = 0; ci < ColumnLength; ci++)
                {
                    Xss[ri, ci] = item;
                }
            }
        }

        internal T[,] ToTwoDimensionalArray()
        {
            return Xss;
        }

        internal void Write(StreamWriter sw)
        {
            for (var ri = 0; ri < RowLength; ri++)
            {
                for (var ci = 0; ci < ColumnLength; ci++)
                {
                    sw.Write(Xss[ri, ci]);
                }

                sw.WriteLine();
            }
        }
    }
}