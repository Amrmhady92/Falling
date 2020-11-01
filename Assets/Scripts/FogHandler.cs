using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    public bool spawnFog;
    public GameObject fogPrefab;
    public Terrain terrain;

    public GameManager gameManager;

    public Transform playerTransform;
    public float viewDistance = 5;
    public LayerMask fogMask;
    public LayerMask groundMask;

    public int objectsPerAxis = 8;
    float width;
    float height;
    int chunkSizeX;
    int chunkSizeZ;

    public float fogCutoffHeight;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        width = terrain.terrainData.size.x;
        height = terrain.terrainData.size.z;

        chunkSizeX = (int)width / objectsPerAxis;
        chunkSizeZ = (int)height / objectsPerAxis;
        playerTransform = FindObjectOfType<PlayerFalling>().transform;
        gameManager.SetActiveFogSpawnPositions(objectsPerAxis);
        if (spawnFog)
        {
            SpawnFog();
        }
    }

    void Update()
    {
        if (spawnFog)
        {
            Vector3 samplePosition = new Vector3(playerTransform.position.x, HeightAtPoint(playerTransform.position), playerTransform.position.z);
            Collider[] objectsInRange = Physics.OverlapSphere(samplePosition, viewDistance, fogMask, QueryTriggerInteraction.Collide);
            foreach (Collider c in objectsInRange)
            {
                RemoveFog(c.GetComponent<Fog>());
            }
        }
    }

    void SpawnFog() { 
        for (int x = 0; x < objectsPerAxis; x++) {
            for (int y = 0; y < objectsPerAxis; y++)
            {
                Vector3 pos = new Vector3 ((transform.position.x - width/2) + chunkSizeX/2 + chunkSizeX * x, 0, (transform.position.z - height / 2) + chunkSizeZ / 2 + chunkSizeZ * y);
                pos.y = HeightAtPoint(pos) + 5;

                if (pos.y < fogCutoffHeight && gameManager.activeFogSpawnPositions[x, y] == true) { 
                    GameObject newFog = Instantiate(fogPrefab, pos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360))));
                    newFog.transform.parent = transform;
                    newFog.GetComponent<Fog>().SetPosValues(x, y);
                    if (gameManager.activeFogSpawnPositions[x,y] == false) {
                        Destroy(newFog);
                        print("fog destroyed at " + x + y);
                    }
                }
                else {
                    continue;
                }
            }
        }
    }

    void RemoveFog(Fog fog) {
        fog.RemoveFog();
        gameManager.activeFogSpawnPositions[fog.xPos, fog.yPos] = false;
    }

    float HeightAtPoint(Vector3 pointToSample) {
        RaycastHit hit;
        Physics.Raycast(new Vector3(pointToSample.x, 50, pointToSample.z), Vector3.down, out hit, 100, groundMask);
        return 50 - hit.distance;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(new Vector3(playerTransform.position.x, HeightAtPoint(playerTransform.position), playerTransform.position.z), viewDistance);

    /*    Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position, transform.localScale);

        for (int x = 0; x < objectsPerAxis; x++)
        {
            for (int y = 0; y < objectsPerAxis; y++)
            {
                Vector3 pos = new Vector3((transform.position.x - width / 2) + chunkSizeX / 2 + chunkSizeX * x, 0, (transform.position.z - height / 2) + chunkSizeZ / 2 + chunkSizeZ * y);
                pos.y = HeightAtPoint(pos);
                if (pos.y > fogCutoffHeight) {
                    Gizmos.color = Color.green;
                }
                else {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawWireCube(pos, new Vector3((float)chunkSizeX, 10, (float)chunkSizeZ));
            }
        }*/
    }
}
