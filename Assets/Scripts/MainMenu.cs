using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public IntField desertSeedsCount;
    public IntField forestSeedsCount;
    public IntField rockySeedsCount;
    public IntField plainsSeedsCount;

    public string fallingScene = "";
    bool loading = false;
    public void LoadFallingScene()
    {
        if (loading) return;
        loading = true;

        desertSeedsCount.Value = 0;
        forestSeedsCount.Value = 0;
        rockySeedsCount.Value = 0;
        plainsSeedsCount.Value = 0;

        SceneManager.LoadScene(fallingScene);



    }


}
