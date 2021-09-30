using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject cam;
    public static float x;
    public static float z;
    public static float speed = 6f;
    public float slowDownRate = 2;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public static bool isGrounded;


    //From the other script 
    public bool lightWorldActive = true;
    public GameObject lightWorld;
    public GameObject darkWorld;
    private GameObject[] lightObjects;
    private GameObject[] darkObjects;
    public Material lightWorldMaterial;
    public Material darkWorldMaterial;
    public Material coinMaterial;
    public float changingValue = 0.075f;
    private float lightMaterialValue = -1.2f;
    private float darkMaterialValue = 1.2f;
    public GameObject centerHole;
    public Text mainText;

    public GameObject levelExit;
    public bool levelFinished = false;

    //For the different levels 
    private bool Level3SwitchComplete = false;


    void Awake()
    {
        lightWorld = GameObject.FindWithTag("Light");
        lightObjects = GameObject.FindGameObjectsWithTag("LightObject");
        darkWorld = GameObject.FindWithTag("Dark");
        centerHole = GameObject.FindWithTag("CenterHole");
        mainText = GameObject.FindWithTag("MainText").GetComponent<Text>();
        levelExit = GameObject.FindWithTag("Exit");
        if (darkWorld != null)
        {
            darkObjects = GameObject.FindGameObjectsWithTag("DarkObject");
            foreach (GameObject child in darkObjects)
            {
                //child.GetComponent<BoxCollider>().enabled = false;
                child.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }

    void Update()
    {
        //For getting on and off moving platforms
       if(isGrounded)
        {
            Ray ray = new Ray(groundCheck.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0.4f))
            {
                if (hit.collider.tag == "MovingPlatform")
                {

                    transform.parent = hit.collider.transform;
                }
            }
        } else
        {
            transform.parent = null;
        }


        //For switching between the dark and light worlds 
        if (Input.GetButtonDown("Switch"))
        {
            if (darkWorld != null)
            {
                if (lightWorldActive == true)
                {
                    lightWorldActive = false;
                    /*
                    if (levelFinished == false)
                    {
                        levelExit.SetActive(false);
                    }
                    */

                    foreach (GameObject child in darkObjects)
                    {
                        //child.GetComponent<BoxCollider>().enabled = true;
                        child.GetComponent<MeshCollider>().enabled = true;
                    }

                    foreach (GameObject child in lightObjects)
                    {
                        //child.GetComponent<BoxCollider>().enabled = false;
                        child.GetComponent<MeshCollider>().enabled = false;
                    }

                    //StartCoroutine(Switching());

                    centerHole.GetComponent<Image>().color = Color.white;
                }
                else if (lightWorldActive == false)
                {
                    lightWorldActive = true;
                    /*
                    if (levelFinished == false)
                    {
                        levelExit.SetActive(true);
                    }
                    */

                    foreach (GameObject child in lightObjects)
                    {
                        //child.GetComponent<BoxCollider>().enabled = true;
                        child.GetComponent<MeshCollider>().enabled = true;
                    }

                    foreach (GameObject child in darkObjects)
                    {
                        //child.GetComponent<BoxCollider>().enabled = false;
                        child.GetComponent<MeshCollider>().enabled = false;
                    }

                    //StartCoroutine(Switching());

                    centerHole.GetComponent<Image>().color = Color.black;
                }

                //FOR THE DIFFERENT LEVELS
                if (SceneManager.GetActiveScene().name == "3" && Level3SwitchComplete == false)
                {
                    Level3SwitchComplete = true;
                    StartCoroutine(Level3Monologue());
                }
            }
        }

        if (lightMaterialValue < 1.2f && lightWorldActive == false)
        {
            darkMaterialValue -= changingValue * Time.deltaTime;
            lightMaterialValue += changingValue * Time.deltaTime;
        }

        if (darkMaterialValue < 1.2f && lightWorldActive == true)
        {
            darkMaterialValue += changingValue * Time.deltaTime;
            lightMaterialValue -= changingValue * Time.deltaTime;
        }

        lightWorldMaterial.SetFloat("_Vector1", lightMaterialValue);
        darkWorldMaterial.SetFloat("_Vector2", darkMaterialValue);
        coinMaterial.SetFloat("_Vector3", lightMaterialValue);

        if (Input.GetKeyDown("m"))
        {
            SceneManager.LoadScene("MainMenu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void FixedUpdate()
    {
        //General isGrounded check 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Normal Movement 
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetButton("Sprint") && speed < 12f)
        {
            speed += 10f * slowDownRate * Time.fixedDeltaTime; //0.05
        }
        else if (speed > 6f)
        {
            speed -= 10f * slowDownRate * Time.fixedDeltaTime; //0.075
        }
    }

    /*
    IEnumerator Switching()
    {
        while (lightMaterialValue < 1.2f && lightWorldActive == false)
        {
            darkMaterialValue -= changingValue;
            lightMaterialValue += changingValue;
            yield return new WaitForSeconds(0.009f);
        }

        while (darkMaterialValue < 1.2f && lightWorldActive == true)
        {
            darkMaterialValue += changingValue;
            lightMaterialValue -= changingValue;
            yield return new WaitForSeconds(0.009f);
        }
    }
    */

    IEnumerator Level3Monologue()
    {
        mainText.text = ("");
        yield return new WaitForSeconds(2f);
        mainText.text = ("Robot: \"Ummm...sir. Something seems to be wrong. The eyes of patient 7043 have turned black. I repeat, BLACK!\"");
        yield return new WaitForSeconds(7f);
        mainText.text = ("");
    }

    void checkGrounded()
    {
        float rayLength = 1.5f;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            isGrounded = true;
        }
    }
}
