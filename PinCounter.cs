using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private PinSetter pinSetter;
    private int lastSettledCount = 10;

    public Text standingDisplay;
    public bool ballLeftBox = false;

    public void Start( )
    {
        pinSetter = FindObjectOfType<PinSetter>( );
        standingDisplay.text = PinCount( ).ToString( );
    }

    public void Update( )
    {
        standingDisplay.text = PinCount( ).ToString( );
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

    public void OnTriggerExit(Collider collider)
    {
        GameObject thingHit = collider.gameObject;
        if (thingHit.GetComponent<ball>( ))
        {
            ballLeftBox = true;
            standingDisplay.color = Color.red;
        }
    }

    public void LastSettledCount(int count)
    {
        lastSettledCount = count;
    }
}
