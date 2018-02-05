using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The script, containing the logic behind the Main menu screen
/// </summary>

public class MenuController : MonoBehaviour {

    // loads the scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
