using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private GameObject displayMenu;
    [SerializeField] private ResolutionType[] resolutions;
    [SerializeField] private int selectedResolution;
    [SerializeField] private Text resolutionText;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;

    [SerializeField] private GameObject soundMenu;

    [SerializeField] private GameObject mainMenuRequest;

    [SerializeField] private Image fadeImage;

    private GameObject lastselect;

    private int levelToLoad = 0;

    private void Start()
    {
        lastselect = new GameObject();

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

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OpenDisplay()
    {
        optionsMenu.SetActive(false);
        displayMenu.SetActive(true);
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
        optionsMenu.SetActive(true);
        displayMenu.SetActive(false);
    }

    public void OpenSound()
    {
        optionsMenu.SetActive(false);
        soundMenu.SetActive(true);
    }

    public void CloseSound()
    {
        optionsMenu.SetActive(true);
        soundMenu.SetActive(false);
    }

    public void MainMenuRequest()
    {
        pauseMenu.SetActive(false);
        mainMenuRequest.SetActive(true);
    }

    public void MainMenuDeclined()
    {
        pauseMenu.SetActive(true);
        mainMenuRequest.SetActive(false);
    }

    public void MainMenuConfirmation()
    {
        Time.timeScale = 1;
        FadeIn();
    }

    private void OnEnable()
    {
        Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        PlayerController.Instance.SetScript(true);
    }

    private void FadeIn()
    {
        float duration = 0.5f;
        fadeImage.CrossFadeAlpha(1, duration, false);
        Invoke("SwitchToMainMenu", duration);
    }

    private void SwitchToMainMenu()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
