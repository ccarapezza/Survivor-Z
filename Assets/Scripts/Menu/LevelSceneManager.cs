using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneManager : MonoBehaviour
{
    private static LevelSceneManager instance;
    public static LevelSceneManager Instance { get { return instance; } }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private int currentLevel;
    public void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    /*public void CambiarEscenaOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void CambiarEscenaCreditos()
    {
        SceneManager.LoadScene("Credits");
    }*/

    public void CambiarEscenaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /*public void CambiarEscenaLevels()
    {
        SceneManager.LoadScene("PreGame");
    }*/

    public void NextLevel() {
        currentLevel++;
        if (currentLevel == 1)
            ChangeLevel1();
        if (currentLevel == 2)
            ChangeLevel2();
        if (currentLevel == 3)
            ChangeLevel3();

        if (currentLevel < 3)
            CambiarEscenaMainMenu();
    }

    public void ChangeLevel1()
    {
        currentLevel = 1;
        SceneManager.LoadScene("Valley");
    }
    public void ChangeLevel2()
    {
        currentLevel = 2;
        SceneManager.LoadScene("Cementery");
    }
    public void ChangeLevel3()
    {
        currentLevel = 3;
        SceneManager.LoadScene("CityTown");
    }
}
