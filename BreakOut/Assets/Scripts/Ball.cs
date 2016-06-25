using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public float ballInitialVelocity = 10f;

    private Rigidbody rigibody;
    private bool ballInPlay;

    void Awake()
    {
        rigibody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !ballInPlay)
        {
            transform.parent = null;
            ballInPlay = true;
            rigibody.isKinematic = false;
            rigibody.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
        }
    }
}
