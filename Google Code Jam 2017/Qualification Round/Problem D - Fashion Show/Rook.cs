namespace Gcj
{
    static class Rook
    {
        const char RookSign = 'x';
        const char RookPower = '!';
        const char BlankSign = '*';

        internal static bool[,] GetRooksWithExtra(char[,] rookMatrix)
        {
            var n = rookMatrix.GetLength(0);

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    if (rookMatrix[rowIndex, columnIndex] == RookSign)
                    {
                        FillRookControl(rookMatrix, rowIndex, columnIndex);
                    }
                }
            }

            while (true)
            {
                var isRookAdded = false;

                for (var rowIndex = 0; rowIndex < n; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < n; columnIndex++)
                    {
                        if (rookMatrix[rowIndex, columnIndex] != BlankSign)
                        {
                            continue;
                        }

                        rookMatrix[rowIndex, columnIndex] = RookSign;

                        isRookAdded = true;

                        FillRookControl(rookMatrix, rowIndex, columnIndex);
                    }
                }

                if (!isRookAdded)
                {
                    break;
                }
            }

            return Matrix.ToBoolMatrix(rookMatrix, RookSign);
        }

        static void FillRookControl(char[,] matrix, int rowIndex, int columnIndex)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[rowIndex, i] == BlankSign)
                {
                    matrix[rowIndex, i] = RookPower;
                }

                if (matrix[i, columnIndex] == BlankSign)
                {
                    matrix[i, columnIndex] = RookPower;
                }
            }
        }
    }
}
