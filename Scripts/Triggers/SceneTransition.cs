using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private int levelToLoad = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeIn();
        }
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
