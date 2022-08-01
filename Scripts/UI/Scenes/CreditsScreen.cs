
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour
{
    private int levelToLoad = 0;
    private float timeAfterKeyPress = 0.5f;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Text creditsText;
    private int dist;

    private void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            FadeIn();
            Invoke("LoadScene", timeAfterKeyPress);
        }
    }



    private void LoadScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 0.5f, false);
    }
}
