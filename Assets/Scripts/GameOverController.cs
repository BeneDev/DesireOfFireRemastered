using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Script, controlling the logic behind the gameover screen
/// </summary>

public class GameOverController : MonoBehaviour {

    // lets the player try again
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

    // loads the main menu again
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
