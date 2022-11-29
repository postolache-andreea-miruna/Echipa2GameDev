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
            posX = PlayerPrefs.GetFloat("pos_x");
            posY = PlayerPrefs.GetFloat("pos_y");

            player.transform.position = new Vector2(posX,posY);
            PlayerPrefs.SetInt("LoadTime",0);
            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPositionSave() //saving player position
    {
        PlayerPrefs.SetFloat("pos_x", player.transform.position.x+4.0f);
        PlayerPrefs.SetFloat("pos_y", player.transform.position.y);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void PlayerPositionLoad()
    {
        PlayerPrefs.SetInt("LoadTime", 1);
        PlayerPrefs.Save();
    }
    public void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(283, 1, 0); //put the player back on the ground after hitting the water
    }
}
