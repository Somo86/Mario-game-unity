using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFollower : MonoBehaviour
{
    public Transform Mario;
    public Transform EndLeft;
    public Transform EndRight;
    public float distLimitLeft = 0;
    public float distLimitRight = 9.6f;

    private Vector3 offset;

    private void Start()
    {
        offset = Mario.transform.position - transform.position;
    }

    private void Update()
    {
        // No follow if Mario is close
        // to limit left or right
        if(isAtLimitLeft() || isAtLimitRight())
        {
            StopFollow();
        } else {
            Follow();
        }
                 
    }

    private bool isAtLimitLeft()
    {
        float dist = Mario.transform.position.x - EndLeft.transform.position.x;
        return dist <= distLimitLeft;
    }

    private bool isAtLimitRight()
    {
        float dist = EndRight.transform.position.x - Mario.transform.position.x;
        return dist <= distLimitRight;
    }

    private void StopFollow()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Camera follow X axis
    private void Follow()
    {
        var cameraPosition = Mario.position - offset;
        transform.position = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z);
    }
}
