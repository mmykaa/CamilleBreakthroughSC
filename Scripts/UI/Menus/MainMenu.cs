using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int levelToLoadGame = 1;
    private int levelToLoadCredits = 2;
    private float transitionTime = 0.6f;

    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject displayOptionsMenu;
    [SerializeField] private ResolutionType[] resolutions;
    [SerializeField] private int selectedResolution;
    [SerializeField] private Text resolutionText;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;

    [SerializeField] private GameObject soundOptionsMenu;
    [SerializeField] private GameObject masterSlider;
    [SerializeField] private GameObject musicSlider;
    [SerializeField] private GameObject effectsSlider;

    [SerializeField] private GameObject controlsOptionsMenu;

    [SerializeField] private GameObject exitConfirmationWindow;

    [SerializeField] private GameObject backGroundAnimator;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;
    [SerializeField] private bool isSwitchingToGame = false;
    [SerializeField] private bool isSwitchingToCredits = false;

    [SerializeField] private Image fadeImage;

    private AsyncOperation async;

    private GameObject lastselect;

    private void Start()
    {
        FadeOut();

        lastselect = new GameObject();

        if (fadeImage != null)
        {
            fadeImage.canvasRenderer.SetAlpha(1.0f);
        }

        Cursor.visible = false;

        for (int i = 0; i < resolutions.Length; ++i)
        {
            if (resolutions[i].GetWidth() == Screen.width && resolutions[i].GetHeight() == Screen.height)
            {
                selectedResolution = i;
                resolutionText.text = resolutions[selectedResolution].GetWidth().ToString() + " x " + resolutions[selectedResolution].GetHeight().ToString();
            }
        }

        fullscreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }
    }

    void Update()
    {              //make them stop clicking with the mouse away from the buttons...
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void CheckForNextScene()
    {
        if (isSwitchingToGame)
        {
            StartCoroutine(SwitchToGame());
        }

        if (isSwitchingToCredits)
        {
            StartCoroutine(SwitchToCredits());
        }
    }

    public void NewGame()
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<MainMenu>().ChangeToGameScene();
    }

    public void ChangeToGameScene()
    {
        isSwitchingToGame = true;
        isSwitchingToCredits = false;
    }

    IEnumerator SwitchToGame()
    {
        async = SceneManager.LoadSceneAsync(levelToLoadGame);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            progressBar.value = async.progress;

            if (async.progress == 0.9f)
            {
                progressBar.value = 1f;
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void EnableMainMenuButtons()
    {
        mainMenu.SetActive(true);
    }

    private void FadeOut()
    {
        if (fadeImage != null)
        {
            fadeImage.CrossFadeAlpha(0, 0.5f, false);
        }
    }

    #region Options
    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("OpenOptions");
        Invoke("EnableOptionsButtons", transitionTime);
    }

    private void EnableOptionsButtons()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("CloseOptions");
        Invoke("EnableMainMenuButtons", transitionTime);
    }
    #endregion

    #region Display
    public void OpenDisplay()
    {
        optionsMenu.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("OpenDisplay");
        Invoke("EnableDisplayButtons", transitionTime);
    }

    private void EnableDisplayButtons()
    {
        displayOptionsMenu.SetActive(true);
    }

    public void ResolutionLeft()
    {
        if (selectedResolution > 0)
        {
            selectedResolution--;
        }

        resolutionText.text = resolutions[selectedResolution].GetWidth().ToString() + " x " + resolutions[selectedResolution].GetHeight().ToString();

        SetResolution();
    }

    public void ResolutionRight()
    {
        if (selectedResolution < resolutions.Length - 1)
        {
            selectedResolution++;
        }

        resolutionText.text = resolutions[selectedResolution].GetWidth().ToString() + " x " + resolutions[selectedResolution].GetHeight().ToString();

        SetResolution();
    }


    public void SetResolution()
    {
        Screen.SetResolution(resolutions[selectedResolution].GetWidth(), resolutions[selectedResolution].GetHeight(), fullscreenToggle.isOn);
    }

    public void ApplyFullscreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    public void ApplyVSync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1; //EveryVBlank
        }
        else
        {
            QualitySettings.vSyncCount = 0; //Don't Sync
        }
    }

    public void CloseDisplay()
    {
        displayOptionsMenu.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("CloseDisplay");
        Invoke("EnableOptionsButtons", transitionTime);
    }

    #endregion

    #region Sound 
    public void OpenSound()
    {
        optionsMenu.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("OpenSound");
        Invoke("EnableSoundButtons", transitionTime);
        masterSlider.SetActive(true);
        musicSlider.SetActive(true);
        effectsSlider.SetActive(true);
    }

    private void EnableSoundButtons()
    {
        soundOptionsMenu.SetActive(true);
    }

    public void CloseSound()
    {
        backGroundAnimator.GetComponent<Animator>().SetTrigger("CloseSound");
        soundOptionsMenu.SetActive(false);
        masterSlider.SetActive(false);
        musicSlider.SetActive(false);
        effectsSlider.SetActive(false);
        Invoke("EnableOptionsButtons", transitionTime);
    }

    #endregion

    #region Controls
    public void OpenControls()
    {
        optionsMenu.SetActive(false);
        controlsOptionsMenu.SetActive(true);
    }

    public void CloseControls()
    {
        optionsMenu.SetActive(true);
        controlsOptionsMenu.SetActive(false);
    }
    #endregion

    #region Credits
    public void OpenCredits()
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<MainMenu>().ChangeToCreditsScene();
    }

    public void ChangeToCreditsScene()
    {
        isSwitchingToGame = false;
        isSwitchingToCredits = true;
    }

    IEnumerator SwitchToCredits()
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(levelToLoadCredits);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            progressBar.value = async.progress;

            if (async.progress == 0.9f)
            {
                progressBar.value = 1f;
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    #endregion

    #region Exit
    public void QuitRequest()
    {
        mainMenu.SetActive(false);
        exitConfirmationWindow.SetActive(true);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("OpenQuit");
    }

    public void QuitCancelation()
    {
        mainMenu.SetActive(true);
        exitConfirmationWindow.SetActive(false);
        backGroundAnimator.GetComponent<Animator>().SetTrigger("CloseQuit");
    }

    public void QuitConfirmation()
    {
        Application.Quit();
    }
    #endregion
}