using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private LayerMask obstaculeLayerMask = 8;
    [SerializeField] private bool isInsideCameraFieldOfView = false;
    [SerializeField] private GameObject cameraLight;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isInsideCameraFieldOfView)
        {
            GameManager.Instance.OnDeathReposition();
            isInsideCameraFieldOfView = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Physics2D.Linecast(transform.position, player.transform.position, obstaculeLayerMask))
            {
                isInsideCameraFieldOfView = false;
            }
            else
            {
                isInsideCameraFieldOfView = true;
            }
        }
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
        cameraLight.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
        cameraLight.SetActive(false);
    }
}
