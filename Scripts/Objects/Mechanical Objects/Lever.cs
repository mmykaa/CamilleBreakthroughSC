using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject myTarget = null;

    [SerializeField] private Sprite activatedLever;

    private SpriteRenderer mySpriteRenderer;

    private bool isEnabled = true;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateLeverFunction()
    {
        myTarget.GetComponent<MovedByLever>().enabled = true;
        mySpriteRenderer.sprite = activatedLever;
        DisableButton();
    }

    private void DisableButton()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        isEnabled = false;
    }
}
