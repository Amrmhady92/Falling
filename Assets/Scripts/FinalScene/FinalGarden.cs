using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGarden : MonoBehaviour
{

    [Header("Testing")]
    public bool spawnDesert;
    public bool spawnForest;
    public bool spawnCliffs;
    public bool spawnPlains;


    public Transform[] plantLocations;
    public GameObject desertPlant;
    public GameObject forestPlant;
    public GameObject hillPlant;
    public GameObject plainsPlant;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player.desertSeedsCount.Value > 0 || spawnDesert) {
            GameObject newPlant = Instantiate(desertPlant, plantLocations[0].position,Quaternion.identity, transform);
            GameObject newPlantTwo = Instantiate(desertPlant, plantLocations[4].position, Quaternion.identity, transform);
        }
        if (player.forestSeedsCount.Value > 0 || spawnForest)
        {
            GameObject newPlant = Instantiate(forestPlant, plantLocations[1].position, Quaternion.identity, transform);
            GameObject newPlantTwo = Instantiate(forestPlant, plantLocations[5].position, Quaternion.identity, transform);

        }
        if (player.rockySeedsCount.Value > 0 || spawnCliffs)
        {
            GameObject newPlant = Instantiate(hillPlant, plantLocations[2].position, Quaternion.identity, transform);
            GameObject newPlantTwo = Instantiate(hillPlant, plantLocations[6].position, Quaternion.identity, transform);

        }
        if (player.plainsSeedsCount.Value > 0 || spawnPlains)
        {
            GameObject newPlant = Instantiate(plainsPlant, plantLocations[3].position, Quaternion.identity, transform);
            GameObject newPlantTwo = Instantiate(plainsPlant, plantLocations[7].position, Quaternion.identity, transform);

        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in plantLocations) {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(t.position, .1f);
            }
    }
}
