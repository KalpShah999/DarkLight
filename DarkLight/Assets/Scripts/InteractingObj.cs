using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingObj : MonoBehaviour
{
    GameObject mainCamera;
    bool carrying = false;
    GameObject carriedObject;
    public float distance = 4.5f;
    public float smooth = 4f;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (carrying == true)
        {
            carry(carriedObject);
            checkDrop();
        } else
        {
            pickup();
        }
    }

    void carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, new Vector3(mainCamera.transform.position.x - 0.5f, mainCamera.transform.position.y - 0.5f, mainCamera.transform.position.z + 0.5f) + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        Vector3 distanceAway = new Vector3(Mathf.Abs(o.transform.position.x - mainCamera.transform.position.x), Mathf.Abs(o.transform.position.y - mainCamera.transform.position.y), Mathf.Abs(o.transform.position.z - mainCamera.transform.position.z));
        smooth = (distanceAway.x + distanceAway.y + distanceAway.z) / 2;
        Debug.Log(smooth + "");
    }

    void pickup()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, 4.5f))
            {
                if (hit.collider.tag == "Interactable")
                {
                    carrying = true;
                    carriedObject = hit.collider.gameObject;
                    hit.collider.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.GetComponent<Rigidbody>().freezeRotation = true;
                    carriedObject.GetComponent<Collider>().GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }
            }
        }
    }

    void checkDrop()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            dropObject(); 
        }
    }

    void dropObject()
    {
        carrying = false;
        carriedObject.GetComponent<Collider>().GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Collider>().GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject.GetComponent<Collider>().GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        carriedObject = null;
    }
}
