using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public string fallingScene = "";
    bool loading = false;
    public void LoadFallingScene()
    {
        if (loading) return;
        loading = true;
        SceneManager.LoadScene(fallingScene);
    }


}
