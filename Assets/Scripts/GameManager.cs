using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string landingPlaceName;
    public Vector3 restartPosition;


    public bool[,] activeFogSpawnPositions;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
         if (landingPlaceName != "") { 
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(ReturnToMainScene());
            }
        }
    }

    public void LoadNewScene(LandablePlace scene) {
        landingPlaceName = scene.placeName;
        restartPosition = scene.transform.position + (Vector3.up *20);

        SceneManager.LoadScene(scene.placeName);
    }

    public IEnumerator LoadNewScenes(LandablePlace scene, float delay)
    {
        landingPlaceName = scene.placeName;
        restartPosition = scene.takeOffPoint.position;
        //restartPosition = scene.transform.position + (Vector3.up * 20);
        StartCoroutine(FindObjectOfType<UIFade>().FadeIn(delay));
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(scene.placeName);
    }

    public IEnumerator ReturnToMainScene()
    {
        StartCoroutine(FindObjectOfType<UIFade>().FadeIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("FallingScene");

        List<LandablePlace> places = new List<LandablePlace>(FindObjectsOfType<LandablePlace>());
        foreach (LandablePlace place in places) { 
            if(place.placeName == landingPlaceName) {
                place.landable = false;
            }
        }
        Invoke("ResetPlayerPosition", .05f);
    }

    void ResetPlayerPosition() {
        FindObjectOfType<PlayerFalling>().transform.position = restartPosition;
        landingPlaceName = "";
    }

    public void SetActiveFogSpawnPositions(int arraySize) {
        if (activeFogSpawnPositions == null)
        {
            print("setting");
            activeFogSpawnPositions = new bool[arraySize, arraySize];
            for (int x = 0; x < arraySize; x++)
            {
                for (int y = 0; y < arraySize; y++)
                {
                    activeFogSpawnPositions[x, y] = true;
                }
            }
        }

    }
}
