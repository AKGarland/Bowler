using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreMaster {

    public static List<int> ScoreFrames (List<int> rolls)
    {
        List<int> frameList = new List<int>( );

        frameList.Add(rolls.ElementAt(rolls.Count-1));

        return frameList;
    }

}
