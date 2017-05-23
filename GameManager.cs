using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static PinCounter pinCounter;

    private ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    ActionMaster actionMaster = new ActionMaster( );
    List<int> rolls = new List<int>( );

    void Start( ) {
        scoreDisplay = FindObjectOfType<ScoreDisplay>( );
        pinCounter = FindObjectOfType<PinCounter>( );
        pinSetter = FindObjectOfType<PinSetter>( );
        ball = GameObject.FindObjectOfType<ball>( );
    }


    void Update( )
    {
        if (pinCounter.ballLeftBox)
        {
           pinCounter.UpdateStandingCountAndSettle( );
        }
	}

    public void Bowl(int pinFall)
    {
        try
        {
            rolls.Add(pinFall);

            pinSetter.ActionDecision(ActionMaster.NextAction(rolls));
            Debug.Log(" " + rolls.ElementAt(rolls.Count - 1));
            scoreDisplay.FillRollCard(rolls);
            ball.Reset( );
        } catch { Debug.LogWarning("Something went wrong in Bowl()"); }

        try
        {
            scoreDisplay.TotalScore(ScoreMaster.ScoreCumulative(rolls));
        }
        catch { Debug.LogWarning("Something went wrong in scoring"); }
    }
}
