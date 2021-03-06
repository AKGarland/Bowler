﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 30f;
    public float distToRaise = 40f;

    private Rigidbody rigidBody;

    void Start () {
        rigidBody = GetComponent<Rigidbody>( );
	}

    public bool IsStanding( )
    {
        // The pins are imported from blender on their side so "upright" is actually 90 degrees.
        float tiltInX = (transform.eulerAngles.x < 90f) ? transform.eulerAngles.x : 270 - transform.eulerAngles.x;
        float tiltInZ = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : 360 - transform.eulerAngles.z;

        // standingThreshold to tweak in Inspector
        if (tiltInX > standingThreshold || tiltInZ > standingThreshold)
        { 
            return false;
        }
        else
        {
           return true;
        }
    }

    public void Raise( )
    {
        // Raise standing pins only by distToRaise (tuned in Inspector), rotation figure due to Blender import offset
        if (IsStanding( ))
        {
            rigidBody.useGravity = false;
            transform.rotation = Quaternion.Euler(270f,0,0);
            transform.Translate(new Vector3(0f,distToRaise,0f), Space.World) ;
            transform.rotation = Quaternion.Euler(270f, 0, 0);
        }
    }

    public void Lower( )
    {
        // Returns pins to floor level 
        rigidBody.useGravity = true;
        transform.position -= Vector3.up * distToRaise;
    }
}
