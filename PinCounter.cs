using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinCounter : MonoBehaviour {

    private PinSetter pinSetter;

    public void Start( )
    {
        pinSetter = FindObjectOfType<PinSetter>( );
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

    public int LastPinCount( )
    {
        int lastPinCount = 0;
        if (!pinSetter.ballLeftBox)
        {
            foreach (Pin pin in GameObject.FindObjectsOfType<Pin>( ))
            {
                if (pin.IsStanding( ))
                {
                    lastPinCount++;
                }
            }
        }
        return lastPinCount;
    }

    public int PinFall( )
    {
        int pinFall = LastPinCount() - PinCount();
        return pinFall;
    }



}
