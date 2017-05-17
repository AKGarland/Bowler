using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ball))]
public class DragLaunch : MonoBehaviour {

    private ball ball;
    private float dragStartTime, dragEndTime;
    private Vector3 dragStartPos, dragEndPos;
    
	// Use this for initialization
	void Start () {
        ball = GetComponent<ball>( );
	}
	
    public void DragStart( )
    {
        if (!ball.inPlay)
        {
            // Capture time & position of drag start
            dragStartTime = Time.timeSinceLevelLoad;
            dragStartPos = Input.mousePosition;
        }
    }

    public void DragEnd( )
    {
        if (!ball.inPlay)
        {
            dragEndTime = Time.timeSinceLevelLoad;
            dragEndPos = Input.mousePosition;

            float launchSpeedX = (dragEndPos.x - dragStartPos.x) / (dragEndTime - dragStartTime);
            float launchSpeedZ = (dragEndPos.y - dragStartPos.y) / (dragEndTime - dragStartTime);

            Vector3 dragVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
            // Launch the ball
            ball.Launch(dragVelocity);
        }
    }

    public void MoveStart(float xNudge)
    {
        if (!ball.inPlay)
        {
            ball.transform.Translate(new Vector3(xNudge,0,0));
        }
    }
}
