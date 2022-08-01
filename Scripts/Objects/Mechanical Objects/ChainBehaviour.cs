using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBehaviour : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    [SerializeField] private GameObject[] destinationPoints;
    private int choosedDestination;
    private Vector2 destinationToTravel;



    private void Start()
    {
        ChooseDestinationPoint();
    }

    private void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, destinationToTravel, travelSpeed * Time.deltaTime);

        if (transform.position == destinationPoints[choosedDestination].transform.position)
        {
            ChooseDestinationPoint();
        }
    }

    private void ChooseDestinationPoint()
    {
        choosedDestination = Random.Range(0, destinationPoints.Length);
        travelSpeed = Random.Range(1.5f, 2.5f);
        destinationToTravel.x = destinationPoints[choosedDestination].transform.position.x;
        destinationToTravel.y = destinationPoints[choosedDestination].transform.position.y;

    }

    private void OnBecameVisible()
    {
        this.enabled = true;
        ChooseDestinationPoint();
    }

    private void OnBecameInvsible()
    {
        this.enabled = false;
    }
}
