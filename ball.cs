using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour 
{
    public Vector3 intialVelocity;
    public bool inPlay = false;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private float posX;
    private Vector3 startPos;

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>( );
        rigidBody.useGravity = false;
        startPos = transform.position;
    }

    public void Launch(Vector3 velocity)
    {
        inPlay = true;
        rigidBody.velocity = velocity;
        rigidBody.useGravity = true;

        audioSource = GetComponent<AudioSource>( );
        audioSource.Play( );
    }

    void Update () {
        posX = Mathf.Clamp(transform.position.x, -60f, 60f);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public void Reset( )
    {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        inPlay = false;
    }
}
