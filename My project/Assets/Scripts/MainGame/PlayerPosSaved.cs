using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosSaved : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("LoadTime") == 1)
        {
            float posX = player.transform.position.x;
            float posY = player.transform.position.y;
            //float posZ = player.transform.position.z;

            posX = PlayerPrefs.GetFloat("pos_x");
            posY = PlayerPrefs.GetFloat("pos_y");
            //posZ = PlayerPrefs.GetFloat("pos_z");

            player.transform.position = new Vector3(posX,posY);
            PlayerPrefs.SetInt("LoadTime" ,0);
            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPositionSave()
    {
        PlayerPrefs.SetFloat("pos_x", player.transform.position.x);
        PlayerPrefs.SetFloat("pos_y", player.transform.position.y);
        PlayerPrefs.SetFloat("pos_z", player.transform.position.z);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void PlayerPositionLoad()
    {
        PlayerPrefs.SetInt("LoadTime", 1);
        PlayerPrefs.Save();
    }

}
