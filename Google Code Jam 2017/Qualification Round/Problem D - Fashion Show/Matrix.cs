namespace Gcj
{
    static class Matrix
    {
        internal static bool[,] ToBoolMatrix(char[,] matrix, char charForTrue)
        {
            var n = matrix.GetLength(0);

            var boolMatrix = new bool[n, n];

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    if (matrix[rowIndex, columnIndex] == charForTrue)
                    {
                        boolMatrix[rowIndex, columnIndex] = true;
                    }
                }
            }

            return boolMatrix;
        }

        internal static char[,] ToCharMatrix(bool[,] matrix, char charForTrue, char charForFalse)
        {
            var n = matrix.GetLength(0);

            var charMatrix = new char[n, n];

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    charMatrix[rowIndex, columnIndex] = matrix[rowIndex, columnIndex] ? charForTrue : charForFalse;
                }
            }

            return charMatrix;
        }
    }
}