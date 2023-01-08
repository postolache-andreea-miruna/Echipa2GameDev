using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingLeaderboard : MonoBehaviour
{
    private float score;
    private int time;
    private int coins;
    private TextMeshProUGUI textMesh;
    private void Awake()
    {
        time = PlayerPrefs.GetInt("Time",0);
        coins = PlayerPrefs.GetInt("Coins",0);
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        score = 100* coins / time;
        
        int i = 1;
        for (i = 1; i <= 10; i++)
        {
            Debug.Log(PlayerPrefs.GetFloat(i.ToString(), 0));
        }
        for (i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.GetFloat(i.ToString(), 0) <= score)
            {
                for(int j = 10;j>i;j--)
                {
                    PlayerPrefs.SetFloat(j.ToString(), PlayerPrefs.GetFloat((j-1).ToString(), 0));
                }
                PlayerPrefs.SetFloat(i.ToString(), score);
                i = 10;
            }
        }
        textMesh.text = "";
        for (i = 1; i <= 10; i++)
        {
            textMesh.text = textMesh.text + i.ToString() + ".  " + PlayerPrefs.GetFloat(i.ToString(), 0) + "\n";
        }
        
        
        
    }
    private void Update()
    {
        
    }
}
