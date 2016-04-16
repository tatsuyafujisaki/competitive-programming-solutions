using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ParenthesesDiv2Medium
{
    public int[] correct(string s)
    {
        var cs = s.ToCharArray();
        var count = 0;
        var flips = new List<int>();

        for (var i = 0; i < cs.Length; i++)
        {
            if (cs[i] == '(')
            {
                var closeCount = cs.Skip(i).Count(c => c == ')');

                if (count > closeCount)
                {
                    flips.Add(i);
                    count--;
                }
                else
                {
                    count++;
                }
            }
            else
            {
                if (count == 0)
                {
                    flips.Add(i);
                    count++;
                }
                else
                {
                    count--;
                }
            }
        }

        return flips.ToArray();
    }
}