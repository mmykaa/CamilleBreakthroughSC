using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    [SerializeField] private GameObject lastCheckpointReached = null;

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.CompareTag("Player"))
        {
            lastCheckpointReached = gameObject;
            SendLastCheckpoint();
        }
    }

    private void SendLastCheckpoint()
    {
        GameManager.Instance.ReceiveLastCheckPointReached(lastCheckpointReached);
    }
}
