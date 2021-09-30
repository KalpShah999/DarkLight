using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public GameObject platform5;
    public GameObject platform6;
    public GameObject platform7;
    public GameObject platform8;
    public GameObject platform9;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "InteractableCube1")
        {
            if (transform.name == "PressurePlate1")
            {
                platform1.GetComponent<MovingPlatform>().enabled = true;
                platform2.GetComponent<MovingPlatform>().enabled = true;
                platform3.GetComponent<MovingPlatform>().enabled = true;
            }
        }
        else if (collision.collider.name == "InteractableCube2")
        {
            if (transform.name == "PressurePlate2")
            {
                platform4.GetComponent<MovingPlatform>().enabled = true;
                platform5.GetComponent<MovingPlatform>().enabled = true;
                platform6.GetComponent<MovingPlatform>().enabled = true;
            }
        }
        else if (collision.collider.name == "InteractableCube3")
        {
            if (transform.name == "PressurePlate3")
            {
                platform7.GetComponent<MovingPlatform>().enabled = true;
                platform8.GetComponent<MovingPlatform>().enabled = true;
                platform9.GetComponent<MovingPlatform>().enabled = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "InteractableCube1")
        {
            if (transform.name == "PressurePlate1")
            {
                platform1.GetComponent<MovingPlatform>().enabled = false;
                platform2.GetComponent<MovingPlatform>().enabled = false;
                platform3.GetComponent<MovingPlatform>().enabled = false;
            }
        } else if (collision.collider.name == "InteractableCube2")
        {
            if (transform.name == "PressurePlate2")
            {
                platform4.GetComponent<MovingPlatform>().enabled = false;
                platform5.GetComponent<MovingPlatform>().enabled = false;
                platform6.GetComponent<MovingPlatform>().enabled = false;
            }
        }
        else if (collision.collider.name == "InteractableCube3")
        {
            if (transform.name == "PressurePlate3")
            {
                platform7.GetComponent<MovingPlatform>().enabled = false;
                platform8.GetComponent<MovingPlatform>().enabled = false;
                platform9.GetComponent<MovingPlatform>().enabled = false;
            }
        }
    }
}
