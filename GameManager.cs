using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static PinCounter pinCounter;

    private int lastStandingCount = -1;
    private float lastChangeTime;

    private ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    ActionMaster actionMaster = new ActionMaster( );
    ScoreMaster scoreMaster = new ScoreMaster( );

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
            UpdateStandingCountAndSettle( );
        }
	}


    public static List<int> pinFalls(int pin)
    {
        List<int> pinFallList = new List<int>( );

        pinFallList.Add(pin);

        return pinFallList;
    }

    void UpdateStandingCountAndSettle( )
    {
        int currentStanding = pinCounter.PinCount( );

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = pinCounter.PinCount( );
        }

        if (lastChangeTime <= (Time.time - 3f)) // Checks that the lastChangeTime was over 3s ago;
        {
            PinsHaveSettled( );
        }
    }

    public void PinsHaveSettled( )
    {
        int pinFall = pinCounter.PinFall( );

        //List<int> scoresList = new List<int>( );
        //scoresList = ScoreMaster.ScoreFrames(pinFalls(pinFall));

        ActionMaster.Action action = actionMaster.Bowl(pinFalls(pinFall));
        Debug.Log(action + " " + pinFall);

        pinSetter.ActionDecision(action);

        pinCounter.standingDisplay.color = Color.green;

        lastStandingCount = -1; // Indicates new frame -- starting anew. Pins have settled and ball not back in box;
        pinCounter.ballLeftBox = false;
        ball.Reset( );
    }
}
