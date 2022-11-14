using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public List<Wire> Wires;
    public AudioClip SuccessSound;
    
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
    }

    void Update()
    {
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
            ResetWires();
        }
    }

    public void ResetWires()
    {
        foreach (Wire w in Wires)
        {
            w.SetConnected(false);
        }
        ShuffleWires();
    }
}
