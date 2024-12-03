using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void EnterTutorial()
    {
        SceneManager.LoadSceneAsync("_Level_0");
    }

    public void EnterLevelOne()
    {
        SceneManager.LoadSceneAsync("_Level_1");
    }

    public void EnterLevelTwo()
    {
        SceneManager.LoadSceneAsync("_Level_2");
    }

    public void EnterLevelThree()
    {
        SceneManager.LoadSceneAsync("_Level_3");
    }
}
