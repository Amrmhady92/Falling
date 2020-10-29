using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustSpawner : MonoBehaviour
{
    float timeSinceGust;
    float gustTimeThreshold;
    public Vector2 timeBetweenGustsMinMax;
    public Vector2 gustSpeedMinMax;
    public Vector3 randomDirectionMin;
    public Vector3 randomDirectionMax;

    public WindGust gustPrefab;
    public List<GameObject> windParticles = new List<GameObject>();
    private ParticleSystem windParticleSystem;
    Transform playerTransform;

    void Start()
    {
        gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);
        playerTransform = FindObjectOfType<PlayerFalling>().transform;
    }

   
    void Update()
    {
        timeSinceGust += Time.deltaTime;

        if (timeSinceGust >= gustTimeThreshold)
        {
            SpawnGust();
            gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);
            timeSinceGust = 0;
        }
    }

    void SpawnGust()
    {
        Vector3 spawnPoint = playerTransform.position + (playerTransform.forward * 4);
        WindGust newGust = Instantiate(gustPrefab, spawnPoint, playerTransform.rotation);
        newGust.gustSpeed = Random.Range(gustSpeedMinMax.x, gustSpeedMinMax.y);
        newGust.gustDirection = new Vector3(Random.Range(randomDirectionMin.x, randomDirectionMax.x), Random.Range(randomDirectionMin.y, randomDirectionMax.y), Random.Range(randomDirectionMin.z, randomDirectionMax.z));
        newGust.transform.parent = transform;

        GameObject newWindParticle = Instantiate(windParticles[Random.Range(0, windParticles.Count)], spawnPoint, Quaternion.LookRotation(newGust.gustDirection));
        newWindParticle.transform.eulerAngles = new Vector3(
            newWindParticle.transform.eulerAngles.x,
            newWindParticle.transform.eulerAngles.y - 90,
            newWindParticle.transform.eulerAngles.z);

        windParticleSystem = newWindParticle.GetComponent<ParticleSystem>();
        windParticleSystem.Play();
        var windparticleVelocity = windParticleSystem.velocityOverLifetime;
        windparticleVelocity.speedModifier = Random.Range(gustSpeedMinMax.x + 2, gustSpeedMinMax.y + 2) / 10;

        Destroy(newGust.gameObject, 5f);
        Destroy(newWindParticle, 5f);
    }

    public void SpawnUpdraft() {
        Vector3 spawnPoint = playerTransform.position + (playerTransform.forward * 2);
        WindGust newGust = Instantiate(gustPrefab, spawnPoint, playerTransform.rotation);
        newGust.gustSpeed = gustSpeedMinMax.y;
        newGust.gustDirection = (Vector3.up * 3) + Vector3.back;
        newGust.transform.parent = transform;
        Destroy(newGust.gameObject, 5f);

    }
}
