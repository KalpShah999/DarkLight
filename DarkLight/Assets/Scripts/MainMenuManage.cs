using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManage : MonoBehaviour
{
    public GameObject mainCanvas;

    public GameObject resumeGameButton;
    public GameObject newGameButton;
    public GameObject settingButtonMainMenu;
    public GameObject exitButtonMainMenu;
    public GameObject startingNewGame; 

    public GameObject player;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject pauseMenuSettings;
    public GameObject pauseMenuSettingsController;
    public Slider playerSensitivitySlider;
    public Toggle playerCameraSway; 

    public AudioSource menuMusicPlayer; 
    public AudioClip menuSelection;
    public AudioClip menuBack;
    public AudioClip menuWarning;
    public AudioSource backgroundMusicPlayer; 
    public AudioClip darkLightStartingOffInGameBackgroundMusic; 

    private string saveFile; 
    public float soundTime = 1f;

    //public GameObject settingsMenu;

    public static bool gamePadConnected; 

    public void Start()
    {
        if (newGameButton != null)
        {
            resumeGameButton.SetActive(false);
            newGameButton.SetActive(true);
            settingButtonMainMenu.SetActive(true);
            exitButtonMainMenu.SetActive(true);
            startingNewGame.SetActive(false);
        }

        mainCanvas = GameObject.FindWithTag("MainCanvas");
        player = GameObject.FindWithTag("Player");

        Debug.Log(PlayerPrefs.HasKey("CurrentLevel") + " " + PlayerPrefs.HasKey("MouseSensitivity") + " " + PlayerPrefs.HasKey("IsCameraSwaying"));


        saveFile = PlayerPrefs.GetString("CurrentLevel", "nothing");
        Debug.Log(saveFile);
        NewMouseLook.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity", 100f) + "");
        Debug.Log(NewMouseLook.mouseSensitivity + "");
        NewMouseLook.isCameraSwaying = PlayerPrefs.GetInt("IsCameraSwaying", 1);
        playerSensitivitySlider.value = NewMouseLook.mouseSensitivity / 100f;
        if (NewMouseLook.isCameraSwaying == 1)
        {
            playerCameraSway.isOn = true;
        } else
        {
            playerCameraSway.isOn = false;
        }

        if (saveFile != "nothing")
        {
            if (resumeGameButton != null)
            {
                resumeGameButton.SetActive(true);
            }
        }
    }

    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;
    public Text frameRateText;
    private bool showFrameRate; 

    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            PlayerPrefs.DeleteAll();
        }

        //For Frame Rate 
        if (Input.GetKeyDown("l"))
        {
            if (showFrameRate)
            {
                showFrameRate = false; 
            } else if (!showFrameRate)
            {
                showFrameRate = true;
            }
        }

        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }

        if (showFrameRate)
        {
            frameRateText.text = "Frame Rate: " + m_lastFramerate;
        } else
        {
            frameRateText.text = "";
        }
    }

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
            pauseMenuSettings.SetActive(false);
            pauseMenuSettingsController.SetActive(false);
        } else
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(true);
            pauseMenuSettings.SetActive(false);
            pauseMenuSettingsController.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "1")
        {
            backgroundMusicPlayer.clip = darkLightStartingOffInGameBackgroundMusic;
            backgroundMusicPlayer.Play(); 
        }
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine());
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void YesNewGame()
    {
        StartCoroutine(YesNewGameRoutine());
    }

    public void NoNewGame()
    {
        StartCoroutine(NoNewGameRoutine());
    }

    public void PauseSettings()
    {
        StartCoroutine(PauseSettingsRoutine());
    }

    public void PauseSettingController()
    {
        StartCoroutine(PauseSettingsControllerRoutine());
    }

    public void GoBackToPause()
    {
        StartCoroutine(BackToPauseRoutine());
    }

    public void GoBackToPauseSettings()
    {
        StartCoroutine(BackToPauseSettingsRoutine());
    }

    public void Exit()
    {   
        StartCoroutine(ExitRoutine());
    }

    public void Resume()
    {
        StartCoroutine(ResumeRoutine());
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuRoutine());
    }

    public IEnumerator LoadGameRoutine()
    {
        if (saveFile != "nothing")
        {
            Time.timeScale = 1f;
            menuMusicPlayer.clip = menuSelection;
            menuMusicPlayer.Play();
            yield return new WaitForSeconds(soundTime);
            SceneManager.LoadScene(saveFile);
        }
    }

    public IEnumerator StartGameRoutine()
    {
        if (saveFile != "nothing")
        {
            startingNewGame.SetActive(true);
            resumeGameButton.SetActive(false);
            newGameButton.SetActive(false);
            settingButtonMainMenu.SetActive(false);
            exitButtonMainMenu.SetActive(false);
            menuMusicPlayer.clip = menuWarning;
            menuMusicPlayer.Play();
            yield return new WaitForSeconds(soundTime);
        } else
        {
            Time.timeScale = 1f;
            menuMusicPlayer.clip = menuSelection;
            menuMusicPlayer.Play();
            yield return new WaitForSeconds(soundTime);
            SceneManager.LoadScene("1");
        }
    }

    public IEnumerator YesNewGameRoutine()
    {
        Time.timeScale = 1f;
        menuMusicPlayer.clip = menuSelection;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("1");
    }

public IEnumerator NoNewGameRoutine()
{
    startingNewGame.SetActive(false);
    resumeGameButton.SetActive(true);
    newGameButton.SetActive(true);
    settingButtonMainMenu.SetActive(true);
    exitButtonMainMenu.SetActive(true);
        menuMusicPlayer.clip = menuBack;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
}

public IEnumerator PauseSettingsRoutine()
{
        pauseMenu.SetActive(false);
        pauseMenuSettings.SetActive(true);
        menuMusicPlayer.clip = menuSelection;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
}

public IEnumerator PauseSettingsControllerRoutine()
{
        pauseMenuSettings.SetActive(false);
        pauseMenuSettingsController.SetActive(true);
        menuMusicPlayer.clip = menuSelection;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
}

public IEnumerator BackToPauseSettingsRoutine()
{
        pauseMenuSettingsController.SetActive(false);
        pauseMenuSettings.SetActive(true);
        PlayerPrefs.SetFloat("MouseSensitivity", NewMouseLook.mouseSensitivity);
        Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity", 3.14f));
        PlayerPrefs.SetInt("IsCameraSwaying", NewMouseLook.isCameraSwaying);

        Debug.Log(PlayerPrefs.GetString("CurrentLevel", "nothing") + " " + PlayerPrefs.GetFloat("MouseSensitivity", 69f) + " " + PlayerPrefs.GetInt("IsCameraSwaying", 1));

        PlayerPrefs.Save();
        menuMusicPlayer.clip = menuBack;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
}

public IEnumerator BackToPauseRoutine()
{
        pauseMenuSettings.SetActive(false);
        pauseMenu.SetActive(true);
        menuMusicPlayer.clip = menuBack;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
} 

public IEnumerator ExitRoutine()
{
        Time.timeScale = 1f;
        menuMusicPlayer.clip = menuBack;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
        Application.Quit();
}

public IEnumerator ResumeRoutine()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //player.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<NewMouseLook>().enabled = true;
        LevelInteractions.pauseMenuUI.SetActive(false);
        LevelInteractions.gamePaused = false;
        //player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<NewPlayerMovement>().enabled = true;
        mainCanvas.SetActive(true);
        menuMusicPlayer.clip = menuSelection;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
    }

public IEnumerator MainMenuRoutine()
    {
        Time.timeScale = 1f;
        menuMusicPlayer.clip = menuBack;
        menuMusicPlayer.Play();
        yield return new WaitForSeconds(soundTime);
        SceneManager.LoadScene("MainMenu");
        LevelInteractions.pauseMenuUI.SetActive(false);
        LevelInteractions.gamePaused = false;
        //player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<NewPlayerMovement>().enabled = true;
        mainCanvas.SetActive(true);
    }
}

