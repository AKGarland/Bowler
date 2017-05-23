using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private PinSetter pinSetter;
    private int lastSettledCount = 10;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private GameManager gameManager;

    public Text standingDisplay;
    public bool ballLeftBox = false;

    public void Start( )
    {
        pinSetter = FindObjectOfType<PinSetter>( );
        gameManager = FindObjectOfType<GameManager>( );
        standingDisplay.text = PinCount( ).ToString( );
    }

    public void Update( )
    {
        standingDisplay.text = PinCount( ).ToString( );
    }

    public void OnTriggerExit(Collider collider)
    {
        GameObject thingHit = collider.gameObject;
        if (thingHit.GetComponent<ball>( ))
        {
            ballLeftBox = true;
            standingDisplay.color = Color.red;
        }
    }

    public int PinCount( )
    {
        int pinCount = 0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>( ))
        {
            if (pin.IsStanding( ))
            {
                pinCount++;
            }
        }
        return pinCount;
    }

    public int PinFall( )
    {
        int standing = PinCount( );  
        print("standing: " + standing);
        int pinFall = lastSettledCount - standing; 
        lastSettledCount = standing; 
        print("pinFall in PinFall: " + pinFall);
        return pinFall; 
    }

    public void UpdateStandingCountAndSettle( )
    {
        int currentStanding = PinCount( );

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = PinCount( );
        }

        if (lastChangeTime <= (Time.time - 3f))     // Checks that the lastChangeTime was over 3s ago;
        {
            PinsHaveSettled( );
        }
    }

    public void PinsHaveSettled( )
    {
        gameManager.Bowl(PinFall());
        standingDisplay.color = Color.green;
        lastStandingCount = -1;                     // Indicates new frame -- starting anew. Pins have settled and ball not back in box;
        ballLeftBox = false;
    }

    public void LastSettledCount(int count)
    {
        lastSettledCount = count;
    }
}
