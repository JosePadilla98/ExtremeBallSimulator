using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class GeneralUtils
    {
        public static string ScoreFormat(string output)
        {
            //OLD METHOD
            //int j = 0;
            //for (int i = output.Length - 1; i >= 0; i--)
            //{
            //    j++;
            //    if (j % 3 == 0)
            //    {
            //        output = output.Insert(i, ",");
            //        i--;
            //    }
            //}

            int numberOfCommas = output.Length / 3;
            if (output.Length % 3 == 0)
                numberOfCommas--;

            for (int i = output.Length - 1; numberOfCommas > 0;)
            {
                i -= 3;
                output = output.Insert(i + 1, ",");
                numberOfCommas--;
            }
            return output;
        }
    }
}
