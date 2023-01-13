using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    private TextMeshProUGUI textPlay;
    private TextMeshProUGUI textQuit;
    private TextMeshProUGUI textInd;
    public GameObject indications;
    public GameObject play;
    public GameObject quit;
    [SerializeField]
    public GameObject input;
    private string name;
    private void Start()
    {
        indications = GameObject.Find("indications");
        play = GameObject.Find("PlayButton");
        quit = GameObject.Find("QuitGame");
    }
    public void RestartGame()
    {
        //EventSystemManager.currentSystem.SetSelectedGameObject(input.gameObject, null);
        PlayerPrefs.DeleteKey("pos_x");
        PlayerPrefs.DeleteKey("pos_y");
        PlayerPrefs.DeleteKey("LoadTime");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("Stars");
        PlayerPrefs.DeleteKey("Time");
        // pentru reinitializare
        /*PlayerPrefs.DeleteKey("Name1");
        PlayerPrefs.DeleteKey("Name2");
        PlayerPrefs.DeleteKey("Name3");
        PlayerPrefs.DeleteKey("Name4");
        PlayerPrefs.DeleteKey("Name5");
        PlayerPrefs.DeleteKey("Name6");
        PlayerPrefs.DeleteKey("Name7");
        PlayerPrefs.DeleteKey("Name8");
        PlayerPrefs.DeleteKey("Name9");
        PlayerPrefs.DeleteKey("Name10");

        PlayerPrefs.DeleteKey("1");
        PlayerPrefs.DeleteKey("2");
        PlayerPrefs.DeleteKey("3");
        PlayerPrefs.DeleteKey("4");
        PlayerPrefs.DeleteKey("5");
        PlayerPrefs.DeleteKey("6");
        PlayerPrefs.DeleteKey("7");
        PlayerPrefs.DeleteKey("8");
        PlayerPrefs.DeleteKey("9");
        PlayerPrefs.DeleteKey("10");*/
        //
        play.SetActive(false);
        quit.SetActive(false);
        indications.SetActive(false);
        input.SetActive(true);
        TMP_InputField text = input.GetComponent<TMP_InputField>();
        text.Select();
    }

    public void QuitGame()
    {
       // Application.Quit(); //when is built
        UnityEditor.EditorApplication.isPlaying = false; //when no built
    }
    public void ReadStringInput(string s)
    {
        name = s;
        PlayerPrefs.SetString("Name", name);
        SceneManager.LoadScene("MainGame");
    }
}
