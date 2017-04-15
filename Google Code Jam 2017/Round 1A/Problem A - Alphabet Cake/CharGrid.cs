using System;
using System.Collections.Generic;
using System.Linq;

namespace Gcj
{
    sealed class CharGrid : Grid<char>
    {
        const char Blank = '?';

        CharGrid(Grid<char> grid)
        {
            Xss = grid.ToTwoDimensionalArray();
        }

        internal CharGrid(IReadOnlyList<string> ss)
        {
            Xss = new char[ss.Count, ss[0].Length];

            for (var ri = 0; ri < Xss.GetLength(0); ri++)
            {
                var cs = ss[ri].ToCharArray();

                for (var ci = 0; ci < Xss.GetLength(1); ci++)
                {
                    Xss[ri, ci] = cs[ci];
                }
            }
        }

        internal CharGrid CreateCharSubgrid(int startRowIndex, int startColumnIndex, int rowLength = -1, int columnLength = -1)
        {
            return new CharGrid(CreateSubgrid(startRowIndex, startColumnIndex, rowLength, columnLength));
        }

        internal void SetSubgrid(CharGrid xss, int startRowIndex = 0, int startColumnIndex = 0)
        {
            for (var ri = 0; ri < xss.RowLength; ri++)
            {
                for (var ci = 0; ci < xss.ColumnLength; ci++)
                {
                    Xss[startRowIndex + ri, startColumnIndex + ci] = xss[ri, ci];
                }
            }
        }

        internal bool MoreThanTwoColumnsHaveNonBlank()
        {
            var hs = new HashSet<int>();

            for (var ri = 0; ri < RowLength; ri++)
            {
                for (var ci = 0; ci < ColumnLength; ci++)
                {
                    if (Xss[ri, ci] == Blank)
                    {
                        continue;
                    }

                    if (hs.Any() && hs.First() != ci)
                    {
                        return true;
                    }

                    hs.Add(ci);
                }
            }

            return false;
        }

        internal Tuple<int, int> GetIndexOfLeftmostNonBlank()
        {
            for (var ci = 0; ci < ColumnLength; ci++)
            {
                for (var ri = 0; ri < RowLength; ri++)
                {
                    if (Xss[ri, ci] != Blank)
                    {
                        return Tuple.Create(ri, ci);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        internal void FillWithNonBlank()
        {
            for (var ri = 0; ri < RowLength; ri++)
            {
                for (var ci = 0; ci < ColumnLength; ci++)
                {
                    if (Xss[ri, ci] != Blank)
                    {
                        Fill(Xss[ri, ci]);
                        return;
                    }
                }
            }

            throw new InvalidOperationException();
        }
    }
}