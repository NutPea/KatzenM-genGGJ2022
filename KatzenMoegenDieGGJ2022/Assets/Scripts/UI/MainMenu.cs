using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int firstSceneIndex;
    public GameObject menuObject;
    public GameObject levelObject;

    public void ShowMenu()
    {
        menuObject.SetActive(true);
        levelObject.SetActive(false);
    }

    public void ShowLevelMenu()
    {
        menuObject.SetActive(false);
        levelObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LoadScene(firstSceneIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
