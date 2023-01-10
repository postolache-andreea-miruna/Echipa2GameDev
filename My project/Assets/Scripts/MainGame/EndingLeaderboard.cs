using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingLeaderboard : MonoBehaviour
{
    private float score;
    private int time;
    private int coins;
    private int stars;
    private string name;
    private TextMeshProUGUI textMesh;
    private void Awake()
    {
        time = PlayerPrefs.GetInt("Time",0);
        coins = PlayerPrefs.GetInt("Coins",0);
        stars = PlayerPrefs.GetInt("Stars", 0);
        name = PlayerPrefs.GetString("Name", "none");
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        score = 100 * stars / time;
        
        int i = 1;
        for (i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.GetFloat(i.ToString(), 0) <= score)
            {
                for(int j = 10;j>i;j--)
                {
                    PlayerPrefs.SetFloat(j.ToString(), PlayerPrefs.GetFloat((j-1).ToString(), 0));
                    PlayerPrefs.SetString("Name"+j.ToString(), PlayerPrefs.GetString("Name"+(j - 1).ToString(), "none"));
                }
                PlayerPrefs.SetFloat(i.ToString(), score);
                PlayerPrefs.SetString("Name"+i.ToString(), name);
                i = 10;
            }
        }
        textMesh.text = "";
        for (i = 1; i <= 10; i++)
        {
            textMesh.text = textMesh.text + PlayerPrefs.GetString("Name"+i.ToString(), "none") + ".  " + PlayerPrefs.GetFloat(i.ToString(), 0) + "\n";
        }
        
        
        
    }
    private void Update()
    {
        
    }
}
