using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour
{
    private PinSetter pinSetter;

    public void Start( )
    {
        pinSetter = FindObjectOfType<PinSetter>( );
    }

    public void OnTriggerExit(Collider collider)
    {
        GameObject thingHit = collider.gameObject;
        if (thingHit.GetComponent<ball>( ))
        {
            pinSetter.ballLeftBox = true;
            pinSetter.standingDisplay.color = Color.red;
        }
            
    }

}
