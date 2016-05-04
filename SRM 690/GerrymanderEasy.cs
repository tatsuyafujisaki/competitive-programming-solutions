public class GerrymanderEasy
{
    public double getmax(int[] A, int[] B, int K)
    {
        long n = A.Length;
        var maxRatio = 0.0;

        for (var startIndex = 0; startIndex < n; startIndex++)
        {
            for (var k = K; startIndex + k <= n; k++)
            {
                var denominator = 0.0;
                var numerator = 0.0;

                for (var i = startIndex; i < startIndex + k; i++)
                {
                    numerator += B[i];
                    denominator += A[i];
                }

                var ratio = numerator / denominator;

                if (maxRatio < ratio)
                {
                    maxRatio = ratio;
                }
            }
        }

        return maxRatio;
    }
}