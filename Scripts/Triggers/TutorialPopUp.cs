using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField] private string textToWrite;
    private string currentText = "";
    [SerializeField] private float typeSpeed = 0.05f;
    [SerializeField] private Text uIText;

    [SerializeField] private List<bool> keyboardKeysToShow;

    [SerializeField] private List<Image> keyboardKeys;

    [SerializeField] private List<Sprite> keyboardKeysSprites;
    // a - 0 | d -1 | LA - 2 | RA - 3 | UA - 4 | Z - 5| X - 6| C - 7 | Space - 8 //

    private void CheckKeysToDisplay()
    {
        for (int i = 0; i < keyboardKeysToShow.Count; i++)
        {
            if (keyboardKeysToShow[i] == true)
            {
                DisplayKey(i);
                SetImage(i);
            }
        }
    }

    private void SetImage(int i)
    {
        if (keyboardKeysToShow[i])
        {
            keyboardKeys[i].GetComponent<Image>().sprite = keyboardKeysSprites[i];
        }
    }

    private void DisplayKey(int i)
    {
        keyboardKeys[i].gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckKeysToDisplay();
            uIText.enabled = true;
            StartCoroutine(EffectTypeWritter());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ClearKeysToDisplay();
            uIText.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void ClearKeysToDisplay()
    {
        for (int i = 0; i < keyboardKeysToShow.Count; i++)
        {
            if (keyboardKeysToShow[i] == true)
            {
                ClearDisplayKey(i);
            }
        }
    }

    private void ClearDisplayKey(int i)
    {
        ResetImage(i);
        keyboardKeys[i].gameObject.SetActive(false);
    }

    private void ResetImage(int i)
    {
        keyboardKeys[i].GetComponent<Image>().sprite = null;
    }

    private IEnumerator EffectTypeWritter()
    {
        for (int i = 0; i < textToWrite.Length; i++)
        {
            currentText = textToWrite.Substring(0, i);
            uIText.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
