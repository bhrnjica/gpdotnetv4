using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GPdotNETLib
{
    public static class GPLibUtil
    {
        public static void ParallelForWithStep(int fromInclusive, int toExclusive, int step, Action<int> body)
        {
            if (step < 1)
            {
                throw new ArgumentOutOfRangeException("step");
            }
            else if (step == 1)
            {
                Parallel.For(fromInclusive, toExclusive, body);
            }
            else // step > 1
            {
                int len = (int)Math.Ceiling((toExclusive - fromInclusive) / (double)step);
                Parallel.For(0, len, i => body(fromInclusive + (i * step)));
            }
        }

    }
    public class AlphaCharEnum
    {
        char[] alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                          'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                          'U', 'V', 'W', 'X', 'Y', 'Z' };

        public string AlphabetFromIndex(int index)
        {
            if (index == 0)
                return "";
            int firstLetter = index / 26;
            int secondLetter = index % 26;
            if (firstLetter == 0)
            {
                return alphabet[secondLetter-1].ToString();
            }
            else
            {
                if (firstLetter > 26)
                    return "";//Not support number
                else if(secondLetter==0 && firstLetter==1)
                    return alphabet[25].ToString();
                else if (secondLetter == 0 && firstLetter <1)
                    return alphabet[firstLetter - 1].ToString();
                else if(secondLetter == 0 && firstLetter >1)
                    return alphabet[firstLetter - 2].ToString()+"Z";
                else
                    return alphabet[firstLetter-1].ToString() + alphabet[secondLetter-1].ToString();
            }

        }
    }
}
