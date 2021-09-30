using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /*
    public float xMovement = 0f;
    public float yMovement = 0f;
    public float zMovement = 0f;
    public float travelTime = 0f;
    public float waitTime = 0f;

    private int goingForward = 1;

    private void Start()
    {
        StartCoroutine(Repeat());
    }

    void Update()
    {
        if (goingForward == 1)
        {
            transform.Translate(xMovement * Time.deltaTime, yMovement * Time.deltaTime, zMovement * Time.deltaTime, Space.World);
        }

        if (goingForward == 0)
        {
            transform.Translate(-xMovement * Time.deltaTime, -yMovement * Time.deltaTime, -zMovement * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = transform; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = null;
        }
    }

    IEnumerator Repeat()
    {
        while (true)
        {
            goingForward = 1;
            yield return new WaitForSeconds(travelTime);
            goingForward = 2;
            yield return new WaitForSeconds(waitTime);
            goingForward = 0;
            yield return new WaitForSeconds(travelTime);
            goingForward = 2;
            yield return new WaitForSeconds(waitTime);
        }
    }
    */

    public float startingXPosition;
    public float startingYPosition;
    public float startingZPosition;
    public float endingXPosition;
    public float endingYPosition;
    public float endingZPosition;
    public float speed;

    private Vector3 startingPosition;
    private Vector3 endingPosition;
    private Vector3 targetPosition;
    private int movingTowardsEnd = 1;

    private void Start()
    {
        startingPosition = new Vector3(startingXPosition, startingYPosition, startingZPosition);
        endingPosition = new Vector3(endingXPosition, endingYPosition, endingZPosition);
        targetPosition = endingPosition;
    }

    private void Update()
    {
        if (startingPosition != endingPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (movingTowardsEnd == 1)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.0001f)
            {
                targetPosition = startingPosition;
                movingTowardsEnd = 0;
            }
        }
        else if (movingTowardsEnd == 0)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.0001f)
            {
                targetPosition = endingPosition;
                movingTowardsEnd = 1;
            }
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.parent = null;
        }
    }
    */
}


