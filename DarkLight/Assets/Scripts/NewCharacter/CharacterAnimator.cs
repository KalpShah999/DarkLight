using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAnimator : MonoBehaviour
{
    public Animator player; 
    public RuntimeAnimatorController[] animatorControllers;

    public GameObject[] animEnvironment;

    private void Start()
    {
        animEnvironment = GameObject.FindGameObjectsWithTag("AnimationEnvironment");
        player.enabled = true;
        foreach (GameObject thing in animEnvironment)
        {
            thing.GetComponent<Animator>().enabled = true;
            thing.GetComponent<Animator>().SetBool("New Bool", true);
        }
        player.SetBool("New Bool", true);
        SceneManager.LoadScene("5");
    }

    void Update()
    {
        /*
        if (NewPlayerMovement.isGrounded == false)
        {
            player.runtimeAnimatorController = animatorControllers[1];
        }
        else if (NewPlayerMovement.z > 0 && NewPlayerMovement.speed > 11.5f)
        {
            player.runtimeAnimatorController = animatorControllers[4];
        }
        else if (NewPlayerMovement.z > 0 && NewPlayerMovement.speed < 12f && NewPlayerMovement.speed > 6f)
        {
            player.runtimeAnimatorController = animatorControllers[2];
        }
        else if (NewPlayerMovement.z > 0 && NewPlayerMovement.speed < 6.000001f || NewPlayerMovement.z > 0 && NewPlayerMovement.speed > 5.99999f)
        {
            player.runtimeAnimatorController = animatorControllers[5];
        }
        else if (NewPlayerMovement.z == 0)
        {
            player.runtimeAnimatorController = animatorControllers[0];
        }
        else if (NewPlayerMovement.z < 0)
        {
            player.runtimeAnimatorController = animatorControllers[3];
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "StartAnim4")
        {
            Destroy(collision.collider.gameObject);
            animEnvironment = GameObject.FindGameObjectsWithTag("AnimationEnvironment");
            player.enabled = true;
            foreach (GameObject thing in animEnvironment)
            {
                thing.GetComponent<Animator>().enabled = true;
                thing.GetComponent<Animator>().SetBool("New Bool", true);
            }
            player.SetBool("New Bool", true);
        }
    }
}
