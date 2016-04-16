using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ParenthesesDiv2Hard
{
    public int minSwaps(string s, int[] L, int[] R)
    {
        var openToClose = 0;
        var closeToOpen = 0;

        for (var i = 0; i < L.Length; i++)
        {
            var l = L[i];
            var r = R[i];

            if ((r - l) % 2 == 0)
            {
                return -1;
            }

            var cs2 = s.Substring(l, r - l + 1).ToCharArray();

            var count = 0;

            for (var j = 0; j < cs2.Length; j++)
            {
                if (cs2[j] == '(')
                {
                    if (count > cs2.Skip(j).Count(c => c == ')'))
                    {
                        openToClose++;
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
                        closeToOpen++;
                        count++;
                    }
                    else
                    {
                        count--;
                    }
                }
            }
        }

        var used = new bool[s.Length];

        for (var i = 0; i < L.Length; i++)
        {
            for (var j = L[i]; j <= R[i]; j++)
            {
                used[j] = true;
            }
        }

        if (openToClose == closeToOpen)
        {
            return openToClose;
        }

        var cs = s.ToCharArray();

        var unusedOpen = 0;
        var unusedClose = 0;

        for (var i = 0; i < s.Length; i++)
        {
            if (!used[i])
            {
                if (cs[i] == '(')
                {
                    unusedOpen++;
                }
                else
                {
                    unusedClose++;
                }
            }
        }

        if (openToClose > closeToOpen)
        {
            if (openToClose - closeToOpen <= unusedClose)
            {
                return openToClose;
            }
        }

        if (closeToOpen > openToClose)
        {
            if (closeToOpen - openToClose <= unusedOpen)
            {
                return closeToOpen;
            }

        }

        return -1;
    }
}
