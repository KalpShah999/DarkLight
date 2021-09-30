using System.Collections; 
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInteractions : MonoBehaviour
{
    public Text levelText;
    public Text mainText;
    private GameObject player;
    private GameObject centerHole;

    public static bool gamePaused;
    public static GameObject pauseMenuUI;
    public static GameObject mainCanvas;

    public GameObject coin; 
    private bool animationActive = false;
    private Animator coinAnimator;
    private bool coinIsUp = false;
    public GameObject[] animEnvironment;

    private void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI = GameObject.FindWithTag("Pause");
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        coin = GameObject.FindWithTag("Coin");
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        levelText = GameObject.FindWithTag("LevelText").GetComponent<Text>();
        mainText = GameObject.FindWithTag("MainText").GetComponent<Text>();
        mainText.text = ("");
        player = GameObject.FindWithTag("Player");
        centerHole = GameObject.FindWithTag("CenterHole");
        coinAnimator = GameObject.FindWithTag("Exit").GetComponent<Animator>();
        StartCoroutine(LevelIntroduction());
    }

    public void Update()
    {
        if (Input.GetButtonUp("Menu"))
        {
            if (animationActive == false)
            {
                if (gamePaused == true)
                {
                    Resume();
                }
                else if (gamePaused == false)
                {
                    Pause();
                    PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
                    /*
                    PlayerPrefs.SetFloat("MouseSensitivity", NewMouseLook.mouseSensitivity);
                    PlayerPrefs.Save();
                    */
                }
            }
        }

        //Making the coin face you when up
        if (coinIsUp)
        {
            coin.transform.LookAt(player.transform);
        }
    }

    public void Resume()
    {
        mainCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //GetComponent<MouseLook>().enabled = true;
        GetComponent<NewMouseLook>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        //GetComponent<PlayerController>().enabled = true;
        GetComponent<NewPlayerMovement>().enabled = true;
    }

    public void Pause()
    {
        if (mainCanvas != null)
        {
            mainCanvas.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //GetComponent<MouseLook>().enabled = false;
        GetComponent<NewMouseLook>().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        //GetComponent<PlayerController>().enabled = false;
        GetComponent<NewPlayerMovement>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "DoorExit1Light1")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(1));
        }
        else if (collision.collider.name == "DoorExit1Light2")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(2));
        }
        else if (collision.collider.name == "DoorExit1Light3")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(3));
        }
        else if (collision.collider.name == "DoorExit1Light4")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(4));
        }
        else if (collision.collider.name == "DoorExit1Light5")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(5));
        }
        else if (collision.collider.name == "DoorExit2Light1")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(7));
        }
        else if (collision.collider.name == "DoorExit2Light2")
        {
            collision.collider.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DoorIntroduction(8));
        }

        if (collision.collider.name == "Level1Waiting")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Level1Waiting());
        }

        if (collision.collider.name == "Level2DarkIntroduction")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Level2DarkIntroductionText());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ExitSphere")
        {
            if (coinIsUp == false)
            {
                coinAnimator.enabled = true;
                coinAnimator.SetInteger("CoinUp", 1);
                StartCoroutine(TurnOffCoin());
                coinIsUp = true;
            } 
        }

        if (other.tag == "Exit")
        {
            int nextLevel = int.Parse(SceneManager.GetActiveScene().name);
            nextLevel += 1;
            string levelToLoad = nextLevel.ToString();
            SceneManager.LoadScene(levelToLoad);
        }

        if (other.name == "StartAnim4")
        {
            StartCoroutine(StartAnim4());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ExitSphere")
        {
            if (coinIsUp == true)
            {
                coinAnimator.enabled = true;
                coinAnimator.SetInteger("CoinUp", 0);
                StartCoroutine(TurnOffCoin());
                coinIsUp = false;
            }
        }
    }

    IEnumerator StartAnim4()
    {
        animEnvironment = GameObject.FindGameObjectsWithTag("AnimationEnvironment");
        player.GetComponent<Animator>().enabled = true;
        foreach (GameObject thing in animEnvironment)
        {
            thing.GetComponent<Animator>().enabled = true;
            thing.GetComponent<Animator>().SetBool("New Bool", true);
        }
        player.GetComponent<Animator>().SetBool("New Bool", true);
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.8f);
        int nextLevel = int.Parse(SceneManager.GetActiveScene().name);
        nextLevel += 1;
        string levelToLoad = nextLevel.ToString();
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator TurnOffCoin()
    {
        yield return new WaitForSeconds(1.55f);
        coinAnimator.SetInteger("CoinUp", 2);
    }

    IEnumerator Level1Waiting()
    {
        yield return new WaitForSeconds(15f);
        mainText.text = ("Doctor: Well, what are you waiting for? Go through the portal and get this test over with.");
        yield return new WaitForSeconds(4f);
    }

    IEnumerator Level2DarkIntroductionText()
    {
        mainText.text = ("Well, that doesn't seem like it is going to work.");
        yield return new WaitForSeconds(5f);
        mainText.text = ("RIGHT CLICK TO SWITCH LENSES");
    }

    IEnumerator LevelIntroduction()
    {
        if (SceneManager.GetActiveScene().name == "1")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<NewMouseLook>().enabled = false;
            animationActive = true;
            StartCoroutine(Level1());
            yield return new WaitForSeconds(0.00001f);
            player.GetComponent<NewPlayerMovement>().enabled = false;
            centerHole.SetActive(false);
            yield return new WaitForSeconds(33.5f);
            player.GetComponent<Animator>().enabled = false;
            player.GetComponent<NewPlayerMovement>().enabled = true;
            player.GetComponent<NewMouseLook>().enabled = true;
            centerHole.SetActive(true);
            animationActive = false;
        } else if (SceneManager.GetActiveScene().name == "2")
        {
            levelText.text = ("Dark");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        }
    }

    IEnumerator DoorIntroduction(int levelNumber)
    {
        if (levelNumber == 1)
        {
            levelText.text = ("Light");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        }  else if (levelNumber == 2)
        {
            levelText.text = ("Jump");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
            StartCoroutine(Level2());
        } else if (levelNumber == 3)
        {
            levelText.text = ("Move");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        } else if (levelNumber == 4)
        {
            levelText.text = ("Interact");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        } else if (levelNumber == 5)
        {
            levelText.text = ("Stop");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        } else if (levelNumber == 7)
        {
            levelText.text = ("Again");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        } else if (levelNumber == 8)
        {
            levelText.text = ("Drop");
            yield return new WaitForSeconds(2f);
            levelText.text = ("");
        }
    }

    IEnumerator Level1()
    {
        mainText.text = ("");
        yield return new WaitForSeconds(4f);
        mainText.text = ("Ugh...");
      yield return new WaitForSeconds(1.5f);
        mainText.text = ("");
        yield return new WaitForSeconds(2.5f);
        mainText.text = ("Assistant: Operation Succesful. Patient 7043 has regained consciousness.");
        yield return new WaitForSeconds(3f);
        mainText.text = ("Doctor: YES! Finally, that one - that one took a while. Phew.");
        yield return new WaitForSeconds(3f);
        mainText.text = ("Doctor: Welcome to the Light World. I understand you might be confused as to why I am not beside you at the moment.");
        yield return new WaitForSeconds(4f);
        mainText.text = ("Doctor: That would be because I have had one of my previous patients explode on me and I much rather not take the chance to go through that again, so I will be watching from that camera up there.");
        yield return new WaitForSeconds(5f);
        mainText.text = ("Doctor: Alright great, could you try standing up for me?");
        yield return new WaitForSeconds(2f);
        mainText.text = ("");
        yield return new WaitForSeconds(5f);
        mainText.text = ("Doctor: Perfect! Now walk on up ahead and I'll see you on the other side.");
        yield return new WaitForSeconds(3f);
        mainText.text = ("");
    }

    IEnumerator Level2()
    {
        mainText.text = ("Doctor: Welcome to the other side!");
        yield return new WaitForSeconds(2f);
        mainText.text = ("");
        yield return new WaitForSeconds(3f);
        mainText.text = ("Doctor: By the way, you did know I wasn't actually going to be there when I said 'See you on the other side' right?");
        yield return new WaitForSeconds(4f);
        mainText.text = ("I'm still just in the camera.");
        yield return new WaitForSeconds(2f);
        mainText.text = ("Doctor: Oh well. Just keep doing what I tell you to.");
        yield return new WaitForSeconds(3f);
        mainText.text = ("");
    }
}