using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject boxIndicator;
    [SerializeField] private GameObject rectangleIndicator;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Text collectiblesCount;

    public static UIManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ClearIndicators();
        fadeImage.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            PlayerController.Instance.SetScript(false);
        }
    }

    public void ChangeToBoxIndicator()
    {
        boxIndicator.SetActive(true);
        rectangleIndicator.SetActive(false);
    }

    public void ChangeToRectangleIndicator()
    {
        boxIndicator.SetActive(false);
        rectangleIndicator.SetActive(true);
    }

    public void ClearIndicators()
    {
        boxIndicator.SetActive(false);
        rectangleIndicator.SetActive(false);
    }

    public void UpdateCollectiblesUI()
    {
        collectiblesCount.text = ": " + GameManager.Instance.GetNumberCollectiblesAlreadyFound() + " / " + GameManager.Instance.GetCollectiblesAmountInScene();
    }
}
