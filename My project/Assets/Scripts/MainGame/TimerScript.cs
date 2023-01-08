using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private int time = 0;
    
    [SerializeField]
    private Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time = PlayerPrefs.GetInt("Time", time);
        timeText.text = "Time: " + time;
        StartCoroutine(Timer());
    }



    IEnumerator Timer()
    {
        while(true){
            time += 1;
            PlayerPrefs.SetInt("Time", time);
            timeText.text = "Time: " + time;
            yield return new WaitForSeconds(1);
        }
    }
}
