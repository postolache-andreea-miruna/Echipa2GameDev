using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public List<Wire> Wires;
    public AudioClip SuccessSound;

    private string messageOutMaze = "";

    [SerializeField] 
    Text countdownText;
/*    [SerializeField]
    Text finalText1;
    [SerializeField]
    Text finalText2;
    [SerializeField]
    Text finalText3;*/
    float currentTime= 0f;
    float startingTime = 10f;
    private string sceneToLoad = "";
    private int numberOfStars;
    void ShuffleWires()
    {
        List<Vector3> endWirePositions = new List<Vector3>();
        foreach(Wire w in Wires)
        {
            Vector3 position = w.EndWire.position;
            endWirePositions.Add(position);
        }
        foreach (Wire w in Wires)
        {
            int randomIndex = Random.Range(0, endWirePositions.Count);
            w.EndWire.position = endWirePositions[randomIndex];
            endWirePositions.RemoveAt(randomIndex);
        }

    }

    void Start()
    {
        ShuffleWires();
        currentTime = startingTime;
        numberOfStars = PlayerPrefs.GetInt("Stars", 0);
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = "Time left = " + (int)System.Math.Floor(currentTime) + "s";


        int connectedWires = 0;
        foreach (Wire w in Wires)
        {
            if (w.IsConnected())
            {
                connectedWires++;
            }
        }

        if(connectedWires == Wires.Count)
        {
        
            if (currentTime >= 5f) // cel putin 5 sec ramase
            {
                messageOutMaze = "3 stele";
                sceneToLoad = "BridgeWin3Case";
                numberOfStars += 3; 
                //finalText1.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
            }
            else if (currentTime >= 2f)  // intre 2 si 4 sec ramase
            {
                messageOutMaze = "2 stele";
                sceneToLoad = "BridgeWin2Case";
                    numberOfStars += 2;
               // finalText2.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
            }
            else if (currentTime > 0f) // intre 1 si 2
            {
                messageOutMaze = "1 stea";
                sceneToLoad = "BridgeWin1Case";
                numberOfStars += 1;
               // finalText3.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
            }
            PlayerPrefs.SetInt("Stars", numberOfStars);
            AudioSource.PlayClipAtPoint(SuccessSound, Camera.main.transform.position);
            SceneManager.LoadScene(sceneToLoad);
          //  finalText.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds" + "\n";// "Number of stars: " + messageOutMaze; //

        }

        //try to make a logic with interval points/stars
        
        if(currentTime <= 0) // 0 sau mai putin
        {
            SceneManager.LoadScene("BridgeLoseCase");
        }
        
    }

}
