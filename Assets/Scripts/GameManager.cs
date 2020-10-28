using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string landingPlaceName;
    public Vector3 restartPosition;

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
                ReturnToMainScene(landingPlaceName);
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
        restartPosition = scene.transform.position + (Vector3.up * 20);
        StartCoroutine(FindObjectOfType<UIFade>().FadeIn(delay));

        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(scene.placeName);
    }

    public void ReturnToMainScene(string placeName)
    {
        SceneManager.LoadScene("FallingScene");

        Invoke("ResetPlayerPosition", .05f);

      
    }

    void ResetPlayerPosition() {
        FindObjectOfType<PlayerFalling>().transform.position = restartPosition;
        landingPlaceName = "";
    }
}
