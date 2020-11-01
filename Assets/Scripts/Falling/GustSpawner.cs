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

    public TerrainCollider terrain;
    public float borderBuffer;
    MapBorder borders;

    bool playerOutsideBounds;
    float outsideBoundsSpawnCooldown = .25f;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerFalling>().transform;
    }
    void Start()
    {
        gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);

        borders = CalculateBorders();
    }

   
    void Update()
    {
        CheckIfPlayerOutsideBounds();

        if (!playerTransform.GetComponent<PlayerFalling>().withinDistanceThreshold && !playerOutsideBounds)
        {
            timeSinceGust += Time.deltaTime;
            if (timeSinceGust >= gustTimeThreshold)
            {
                SpawnGust();
                gustTimeThreshold = Random.Range(timeBetweenGustsMinMax.x, timeBetweenGustsMinMax.y);
                timeSinceGust = 0;
            }
        }
        if (playerOutsideBounds) {
            outsideBoundsSpawnCooldown -= Time.deltaTime;
        
        }
    }

    void SpawnGust()
    {
        Vector3 direction = new Vector3(Random.Range(randomDirectionMin.x, randomDirectionMax.x), Random.Range(randomDirectionMin.y, randomDirectionMax.y), Random.Range(randomDirectionMin.z, randomDirectionMax.z));
        Vector3 spawnPoint = playerTransform.position + (playerTransform.forward * 4);
        WindGust newGust = Instantiate(gustPrefab, spawnPoint, Quaternion.Euler(direction));
        newGust.gustSpeed = Random.Range(gustSpeedMinMax.x, gustSpeedMinMax.y);
        newGust.gustDirection = direction;
        newGust.transform.parent = transform;

        //CreateParticleEffect(spawnPoint, newGust.gustDirection, newGust.gustSpeed);

        Destroy(newGust.gameObject, 5f);

    }

    public void SpawnGustInDirection(Vector3 direction)
    {
        SpawnGustInDirection(direction, gustSpeedMinMax.y);
    }

    public void SpawnGustInDirection(Vector3 direction, float velocity)
    {
        Vector3 spawnPoint = playerTransform.position + (playerTransform.forward * 4);

        WindGust newGust = Instantiate(gustPrefab, spawnPoint, Quaternion.Euler(direction));
        newGust.gustSpeed = velocity;
        newGust.gustDirection = direction;
        newGust.transform.parent = transform;

        //CreateParticleEffect(spawnPoint, direction, newGust.gustSpeed);

        Destroy(newGust.gameObject, 5f);

    }

    public void SpawnUpdraft()
    {
        Vector3 spawnPoint = playerTransform.position + (playerTransform.forward * 2);
        WindGust newGust = Instantiate(gustPrefab, spawnPoint, playerTransform.rotation);
        newGust.gustSpeed = gustSpeedMinMax.y * 2;
        newGust.gustDirection = (Vector3.up * 3) + Vector3.back;
        newGust.transform.parent = transform;
        Destroy(newGust.gameObject, 5f);

    }

    void CreateParticleEffect(Vector3 spawnPoint, Vector3 direction, float gustSpeed) {
        GameObject newWindParticle = Instantiate(windParticles[Random.Range(0, windParticles.Count)], spawnPoint, Quaternion.LookRotation(direction));
        newWindParticle.transform.eulerAngles = new Vector3(
            newWindParticle.transform.eulerAngles.x,
            newWindParticle.transform.eulerAngles.y - 90,
            newWindParticle.transform.eulerAngles.z);

        windParticleSystem = newWindParticle.GetComponent<ParticleSystem>();
        windParticleSystem.Play();
        var windparticleVelocity = windParticleSystem.velocityOverLifetime;
        windparticleVelocity.speedModifier = (gustSpeed + 2) / 10;

        Destroy(newWindParticle, 5f);
    }



    MapBorder CalculateBorders() {
        Vector3 bottomLeft = new Vector3(terrain.bounds.min.x, 0, terrain.bounds.min.z);
        Vector3 topLeft = new Vector3(terrain.bounds.min.x, 0, terrain.bounds.max.z);
        Vector3 bottomRight = new Vector3(terrain.bounds.max.x, 0, terrain.bounds.min.z);
        Vector3 topRight = new Vector3(terrain.bounds.max.x, 0, terrain.bounds.max.z);
        return new MapBorder(bottomLeft, topLeft, bottomRight, topRight, borderBuffer);
    }

    void CheckIfPlayerOutsideBounds() { 
        if (playerTransform.position.x > borders.left && playerTransform.position.x < borders.right && playerTransform.position.z > borders.bottom && playerTransform.position.z < borders.top) {
            playerOutsideBounds = false;
        }
        else {
            playerOutsideBounds = true;
            if (outsideBoundsSpawnCooldown < 0)
            {
                if (playerTransform.position.x < borders.left)
                {
                    //out on left
                    //print("outide on side");
                    SpawnGustInDirection(Vector3.right);
                }
                if (playerTransform.position.x > borders.right)
                {
                    //out on right
                    //print("outide on side");
                    SpawnGustInDirection(Vector3.left);
                }
                if (playerTransform.position.z < borders.bottom)
                {
                    //out on bottom
                    //print("outide on bottom");
                    SpawnGustInDirection(Vector3.forward);
                }
                if (playerTransform.position.z > borders.top)
                {
                    //out on top
                    //print("outide on top");
                    SpawnGustInDirection(Vector3.back);
                }
                outsideBoundsSpawnCooldown = .25f;
            }
        }

    }

    public struct MapBorder
    {
        public Vector3 bottomLeft;
        public Vector3 topLeft;
        public Vector3 bottomRight;
        public Vector3 topRight;

        public float top;
        public float bottom;
        public float left;
        public float right;

        float borderBuffer;

        public MapBorder(Vector3 _bottomLeft, Vector3 _topLeft, Vector3 _bottomRight, Vector3 _topRight, float _borderBuffer)
        {
            bottomLeft = _bottomLeft;
            topLeft = _topLeft;
            bottomRight = _bottomRight;
            topRight = _topRight;

            borderBuffer = _borderBuffer;
            top = topLeft.z - borderBuffer;
            bottom = bottomLeft.z + borderBuffer;
            left = bottomLeft.x + borderBuffer;
            right = bottomRight.x - borderBuffer;

        }
    }

    private void OnDrawGizmos()
    {
    /*    Gizmos.color = Color.red;
        Gizmos.DrawLine(borders.bottomLeft, borders.topLeft);
        Gizmos.DrawLine(borders.topLeft, borders.topRight);
        Gizmos.DrawLine(borders.topRight, borders.bottomRight);
        Gizmos.DrawLine(borders.bottomRight, borders.bottomLeft);
        Gizmos.DrawSphere(terrain.bounds.min, 1);
        Gizmos.DrawSphere(terrain.bounds.max, 1);*/
    }


}
