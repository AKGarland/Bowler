using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PinSetter : MonoBehaviour {

    public GameObject pinSet;

    private Animator ani;
    private GameManager gameManager;
    private PinCounter pinCounter;

    ActionMaster actionMaster = new ActionMaster( ); // This needs to be here so there is only one consistent instance;

    private void Start( )
    {
        ani = GetComponent<Animator>( );
        gameManager = FindObjectOfType<GameManager>( );
        pinCounter = FindObjectOfType<PinCounter>( );
    } 

	// Update is called once per frame;
	void Update () {
        
    }

    public void ActionDecision(ActionMaster.Action action)
    {
        switch (action)
        {
            case ActionMaster.Action.Tidy:
                ani.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.EndTurn:
                ani.SetTrigger("resetTrigger");
                pinCounter.LastSettledCount(10);
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Don't know how to handle end game yet.");
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
        newPins.transform.position += new Vector3(0, 7f,0);
    }
}
