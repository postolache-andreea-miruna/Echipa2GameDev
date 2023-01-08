using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Buttons : MonoBehaviour
{

    //public CollectItems stro;
    private int strawberry;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitFame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void GoToLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
    public void StrawberryWire()
    {
        strawberry = PlayerPrefs.GetInt("Coins", 0);
        // Debug.Log(strawberry);
        if (strawberry >= 17)
        {
            strawberry -= 17;
            SceneManager.LoadScene("BridgeWinGame");
            PlayerPrefs.SetInt("Coins", strawberry);
        }
        else
        {
            bool popup = EditorUtility.DisplayDialog("Message", "Not enough strawberries", "OK");
        }
    }

    public void StrawberryFishing()
    {
        strawberry = PlayerPrefs.GetInt("Coins", 0);
        // Debug.Log(strawberry);
        if (strawberry >= 15)
        {
            strawberry -= 15;
            SceneManager.LoadScene("FishingWinCase");
            PlayerPrefs.SetInt("Coins", strawberry);
        }
        else
        {
            bool popup = EditorUtility.DisplayDialog("Message", "Not enough strawberries", "OK");
        }
    }
    public void StrawberryBoat()
    {
        strawberry = PlayerPrefs.GetInt("Coins", 0);
       // Debug.Log(strawberry);
        if (strawberry >= 15)
        {
            strawberry -= 15;
            SceneManager.LoadScene("RiverWinGame");
            PlayerPrefs.SetInt("Coins", strawberry);
        }
        else
        {
            bool popup = EditorUtility.DisplayDialog("Message", "Not enough strawberries", "OK");
        }
    }

    public void StrawberryMaze()
    {
        strawberry = PlayerPrefs.GetInt("Coins", 0);
       // Debug.Log(strawberry);
        if (strawberry >= 10)
        {
            strawberry -= 10;
            SceneManager.LoadScene("MazeWinGame");
            PlayerPrefs.SetInt("Coins", strawberry);
        }
        else
        {
            bool popup = EditorUtility.DisplayDialog("Message", "Not enough strawberries", "OK");
        }
    }
}
