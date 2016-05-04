public class GerrymanderEasy
{
    public double getmax(int[] A, int[] B, int K)
    {
        long n = A.Length;
        var maxRatio = 0.0;

        for (var i = 0; i < n; i++)
        {
            var denominator = 0.0;
            var numerator = 0.0;

            for (var j = i; j < n; j++)
            {
                numerator += B[j];
                denominator += A[j];

                if (K <= j - i + 1)
                {
                    var ratio = numerator / denominator;

                    if (maxRatio < ratio)
                    {
                        maxRatio = ratio;
                    }
                }
            }
        }

        return maxRatio;
    }
}
