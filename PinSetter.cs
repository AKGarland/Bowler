﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public bool ballLeftBox = false;
    public GameObject pinSet;

    private int lastStandingCount = -1;
    private float lastChangeTime;
    private int lastSettledCount = 10;  
    private ball ball;
    private Animator ani;
    private PinCounter pinCounter;

    ActionMaster actionMaster = new ActionMaster( ); // This needs to be here so there is only one consistent instance.

    private void Start( )
    {
        pinCounter = FindObjectOfType<PinCounter>( );
        standingDisplay.text = pinCounter.PinCount( ).ToString( );
        ball = GameObject.FindObjectOfType<ball>( );
        ani = GetComponent<Animator>( );
    } 

	// Update is called once per frame
	void Update () {
        standingDisplay.text = pinCounter.PinCount( ).ToString( );

        if (ballLeftBox)
        {
            UpdateStandingCountAndSettle( );
        }
    }

    void UpdateStandingCountAndSettle( )
    {
        int currentStanding = pinCounter.PinCount( );

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = pinCounter.PinCount( );
        }

        if (lastChangeTime <= (Time.time - 3f)) // Checks that the lastChangeTime was over 3s ago.
        {
            PinsHaveSettled( );
        }
    }

    void PinsHaveSettled( )
    {
        int standing = pinCounter.PinCount( );
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log(action + " " + pinFall);

        switch (action)
        {
            case ActionMaster.Action.Tidy:
                ani.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.EndTurn:
                ani.SetTrigger("resetTrigger");
                lastSettledCount = 10;
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Don't know how to handle end game yet.");
            case ActionMaster.Action.Reset:
                ani.SetTrigger("resetTrigger");
                lastSettledCount = 10;
                break;
        }

        standingDisplay.color = Color.green;
        lastStandingCount = -1; // Indicates new frame -- starting anew. Pins have settled and ball not back in box.
        ballLeftBox = false;
        ball.Reset( );
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