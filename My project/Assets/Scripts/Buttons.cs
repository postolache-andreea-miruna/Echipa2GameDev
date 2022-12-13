using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitFame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void RestartGameMaze()
    {
        SceneManager.LoadScene("LabirintMeniu");
    }

    public void RestartGameRiver()
    {
        SceneManager.LoadScene("RiverMeniu");
    }

    public void RestartGameFishing()
    {
        SceneManager.LoadScene("FishingMeniu");
    }

    public void RestartGameWires()
    {
        SceneManager.LoadScene("BridgeMeniu");
    }
}
