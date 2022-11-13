using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
    [SerializeField]
    private int _total = 0;
    [SerializeField]
    private int _goodPipes = 0;
    void Start()
    {
        _total = PipesHolder.transform.childCount;
        Pipes = new GameObject[_total];
        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
   public void Correct()
    {
        _goodPipes += 1;
        Debug.Log("Miscare corecta");
        if(_goodPipes == _total)
        {
            Debug.Log("Ai castigat");
        }
    }

    public void Wrong()
    {
        _total -= 1;
    }
}
