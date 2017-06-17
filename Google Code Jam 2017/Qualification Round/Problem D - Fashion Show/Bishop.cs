using System;
using System.Collections.Generic;
using System.Linq;

namespace Gcj
{
    static class Bishop
    {
        const char BishopSign = '+';
        const char BishopControl = '!';
        const char BlankSign = '*';

        internal static bool[,] GetBishopsWithExtra(char[,] bishopMatrix)
        {
            var n = bishopMatrix.GetLength(0);

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    if (bishopMatrix[rowIndex, columnIndex] == BishopSign)
                    {
                        FillBishopControl(bishopMatrix, GetMainDiagonalId(rowIndex, columnIndex), GetAntidiagonalId(rowIndex, columnIndex));
                    }
                }
            }

            while (true)
            {
                var blankCells = new SortedDictionary<int, HashSet<(int, int)>>();

                for (var rowIndex = 0; rowIndex < n; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < n; columnIndex++)
                    {
                        if (bishopMatrix[rowIndex, columnIndex] != BlankSign)
                        {
                            continue;
                        }

                        var v = rowIndex + columnIndex;
                        var key = Math.Min(v, 2 * n - 2 - v);

                        if (blankCells.ContainsKey(key))
                        {
                            blankCells[key].Add((rowIndex, columnIndex));
                        }
                        else
                        {
                            blankCells.Add(key, new HashSet<(int, int)> { (rowIndex, columnIndex) });
                        }
                    }
                }

                if (!blankCells.Any())
                {
                    break;
                }

                var (rowIndex1, columnIndex1) = blankCells.First().Value.First();

                bishopMatrix[rowIndex1, columnIndex1] = BishopSign;

                FillBishopControl(bishopMatrix, GetMainDiagonalId(rowIndex1, columnIndex1), GetAntidiagonalId(rowIndex1, columnIndex1));
            }

            return Matrix.ToBoolMatrix(bishopMatrix, BishopSign);
        }

        static void FillBishopControl(char[,] matrix, int mainDiagonalId, int antiDiagonalId)
        {
            var n = matrix.GetLength(0);

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    if (matrix[rowIndex, columnIndex] == BishopSign)
                    {
                        continue;
                    }

                    if (mainDiagonalId == GetMainDiagonalId(rowIndex, columnIndex))
                    {
                        matrix[rowIndex, columnIndex] = BishopControl;
                    }
                    else if (antiDiagonalId == GetAntidiagonalId(rowIndex, columnIndex))
                    {
                        matrix[rowIndex, columnIndex] = BishopControl;
                    }
                }
            }
        }

        static int GetMainDiagonalId(int rowIndex, int columnIndex) => rowIndex - columnIndex;
        static int GetAntidiagonalId(int rowIndex, int columnIndex) => rowIndex + columnIndex;
    }
}
