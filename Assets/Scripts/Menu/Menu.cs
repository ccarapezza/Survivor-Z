using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void CambiarEscenaOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void CambiarEscenaCreditos()
    {
        SceneManager.LoadScene("Credits");
    }

    public void CambiarEscenaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CambiarEscenaLevels()
    {
        SceneManager.LoadScene("PreGame");
    }


    public void ChangeLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ChangeLevel2()
    {
        SceneManager.LoadScene("Level2");

    }

    public void ChangeLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}
