using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("pos_x");
        PlayerPrefs.DeleteKey("pos_y");
        PlayerPrefs.DeleteKey("LoadTime");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("Coins");
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
       // Application.Quit(); //when is built
        UnityEditor.EditorApplication.isPlaying = false; //when no built
    }
}
