using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject checkpointToSpawn;

    [SerializeField] private GameObject player;

    [SerializeField] private List<GameObject> collectiblesInScene;

    private int collectiblesFound = 0;

    public static GameManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        collectiblesInScene.AddRange(GameObject.FindGameObjectsWithTag("Collectible"));
    }

    public void ReceiveLastCheckPointReached(GameObject checkpoint)
    {
        checkpointToSpawn = checkpoint;
    }

    public void OnDeathReposition()
    {
        player.transform.position = checkpointToSpawn.transform.position;
    }

    public void CollectibleFound() //called by playerinteractions
    {
        collectiblesFound++;
        CheckAmountOfCollectibles();
        UIManager.Instance.UpdateCollectiblesUI();
    }

    public void GetAllCollectibles() //called by debugcommand
    {
        for (int i = 0; i < collectiblesInScene.Count; i++)
        {
            CollectibleFound();
        }

        collectiblesInScene.Clear();
    }

    public void CheckAmountOfCollectibles()
    {
        if (collectiblesFound == collectiblesInScene.Count)
        {
            Debug.Log("Achievement");
        }
    }

    public int GetCollectiblesAmountInScene()
    {
        return collectiblesInScene.Count;
    }

    public int GetNumberCollectiblesAlreadyFound()
    {
        return collectiblesFound;
    }
}
