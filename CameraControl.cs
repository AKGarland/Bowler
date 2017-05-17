using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public ball ball;
    public PinSetter pinSetter;

    private Vector3 offset;

	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>( );

        // Find distance between camera and ball on Start
        offset = transform.position - ball.transform.position;
	}
	
	void Update () {
        // Maintain aforementioned distance on every frame (camera tracks ball)
        // Stop when z = 1750f
        if (!pinSetter.ballLeftBox) // In front of head pin (transform.position.z <= 1750f )
        {
            transform.position = ball.transform.position + offset;
        }  
	}
}
