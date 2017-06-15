using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PinSetter : MonoBehaviour {

    public GameObject pinSet;

    private Animator ani;
    private PinCounter pinCounter;

    private void Start( )
    {
        ani = GetComponent<Animator>( );
        pinCounter = FindObjectOfType<PinCounter>( );
    } 

    public void ActionDecision(ActionMaster.Action action)
    {
        switch (action) // RaisePins(), LowerPins(), & RenewPins() are called by the animator 
        {
            case ActionMaster.Action.Tidy:
                ani.SetTrigger("tidyTrigger"); // RaisePins, the sweeper object wipes away fallen pins, LowerPins()
                break;
            case ActionMaster.Action.EndTurn:
                ani.SetTrigger("resetTrigger"); // The sweeper pushes all pins out of the boundary which destroys them & RenewPins()
                pinCounter.LastSettledCount(10);
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Don't know how to handle end game yet.");  // This exception should only be thrown now if something goes very wrong
            case ActionMaster.Action.Reset:
                ani.SetTrigger("resetTrigger"); 
                pinCounter.LastSettledCount(10);
                break;
        }
    }

    public void RaisePins( )
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>( ))
        {
            pin.Raise( );
        }
    }

    public void LowerPins( )
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>( ))
        {
            pin.Lower( );
        }
    }

    public void RenewPins( )
    {
        GameObject newPins = Instantiate(pinSet);
        newPins.transform.position += new Vector3(0, 0,0);
    }
}
