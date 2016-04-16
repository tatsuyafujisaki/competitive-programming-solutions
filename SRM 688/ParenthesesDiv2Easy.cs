public class ParenthesesDiv2Easy
{
    public int getDepth(string s)
    {
        var count = 0;
        var maxCount = 0;

        foreach (var c in s)
        {
            if (c == '(')
            {
                if (maxCount < ++count)
                {
                    maxCount = count;
                }
            }
            else
            {
                count--;
            }
        }

        return maxCount;
    }
}