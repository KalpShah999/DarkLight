using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour
{
    public Material fade;
    private bool startCutscene; 
    private bool startSwitch;
    private float amount; 

    void Start()
    {
        startCutscene = false;
        startSwitch = false;
        amount = 1.2f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "5")
        {
            if (startSwitch == true)
            {
                amount -= 1.4f * Time.deltaTime;
            }
            else if (!startCutscene)
            {
                StartCoroutine(Five());
            }

            fade.SetFloat("_Vector3", amount);
        }
    }

    public IEnumerator Five()
    {
        startCutscene = true;
        yield return new WaitForSeconds(3);
        startSwitch = true;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerPrefs.DeleteKey("CurrentLevel");
    }
}
