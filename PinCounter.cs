using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private int lastSettledCount = 10;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private GameManager gameManager;

    public Text standingDisplay;
    public bool ballLeftBox = false;  // This is public as GameManager needs access

    public void Start( )
    {
        gameManager = FindObjectOfType<GameManager>( );
        standingDisplay.text = PinCount( ).ToString( );
    }

    public void Update( )
    {
        standingDisplay.text = PinCount( ).ToString( );
    }

    public void OnTriggerExit(Collider collider) 
    {
        // Will collide as soon as the ball exits the non-pin "lane" space, whether it has exited in the direction of the pins or backwards off the edge
        // In the case of the latter, the turn is wasted and the next bowl is triggered so the ball is reset
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
        int pinFall = lastSettledCount - standing; 
        lastSettledCount = standing; 
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

        if (lastChangeTime <= (Time.time - 3f))     // Checks that the lastChangeTime was over 3s ago, tweak if necessary
        {
            PinsHaveSettled( );
        }
    }

    public void PinsHaveSettled( )
    {
        gameManager.Bowl(PinFall());
        standingDisplay.color = Color.green;
        lastStandingCount = -1;                     // Indicates new frame -- starting anew. Pins have settled and ball not back in box.
        ballLeftBox = false;
    }

    public void LastSettledCount(int count)
    {
        lastSettledCount = count;
    }
}
