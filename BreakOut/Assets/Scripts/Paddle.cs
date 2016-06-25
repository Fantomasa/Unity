using UnityEngine;
using System.Collections;
using CnControls;

public class Paddle : MonoBehaviour
{


    public float paddleSpeed = 1f;

    private Vector3 playerPosition = new Vector3(0, -9.5f, 0);

    void Update()
    {
        float xPosition = transform.position.x + (CnInputManager.GetAxis("Horizontal") * paddleSpeed);
        playerPosition = new Vector3(Mathf.Clamp(xPosition, -8f, 8f), -9.5f, 0f);
        transform.position = playerPosition;
    }
}
