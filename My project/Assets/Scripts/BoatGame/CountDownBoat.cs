using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownBoat : MonoBehaviour
{  
    private int time = 360;

    [SerializeField]
    private Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time = 360;
        timeText.text = "Remain Time: " + time;
        StartCoroutine(CountTimer());
    }

    IEnumerator CountTimer()
    {
        while (true)
        {
            time -= 1;
            timeText.text = "Remain Time: " + time;
            yield return new WaitForSeconds(1);
        }
    }
}

