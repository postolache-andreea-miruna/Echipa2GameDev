using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public List<Wire> Wires;
    public AudioClip SuccessSound;

    [SerializeField] 
    Text countdownText;
    [SerializeField]
    Text finalText;
    float currentTime= 0f;
    float startingTime = 10f;

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
            AudioSource.PlayClipAtPoint(SuccessSound, Camera.main.transform.position);
            SceneManager.LoadScene("BridgeWinCase");
            finalText.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
        }
        if(currentTime <= 0)
        {
            SceneManager.LoadScene("BridgeLoseCase");
        }
    }

}
