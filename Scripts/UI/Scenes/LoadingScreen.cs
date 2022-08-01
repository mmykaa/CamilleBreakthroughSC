using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{

    public void DisableLoadingScreen() //called by pop out animation
    {
        gameObject.SetActive(false);
    }
}
