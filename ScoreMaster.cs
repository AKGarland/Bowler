using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreMaster
{
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>( );
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            if (cumulativeScores.Count <= 9)
            {
                runningTotal += frameScore;
                cumulativeScores.Add(runningTotal);
            }

        }

        return cumulativeScores;
    }


    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>( );

        for (int i = 1; i < rolls.Count; i += 2)
        {
            if (frameList.Count == 10)
            {
                break;
            }

            if (rolls[i - 1] + rolls[i] < 10) // If neither strike nor spare in frame
            {
                frameList.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls.Count - i <= 1)
            {
                break;
            }

            if (rolls[i - 1] == 10) // If strike in first frame BONUS
            {
                i--;
                frameList.Add(10 + rolls[i + 1] + rolls[i + 2]);
            }
            else if (rolls[i - 1] + rolls[i] == 10) // If spare
            {
                frameList.Add(10 + rolls[i + 1]);
            }
        }
        return frameList;
    }
}
