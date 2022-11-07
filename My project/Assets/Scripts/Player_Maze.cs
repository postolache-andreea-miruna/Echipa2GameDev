using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_Maze : MonoBehaviour
{
    public float speed = 2;
    private float Points = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed*Time.deltaTime,
                transform.position.z);
                Points += speed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - speed*Time.deltaTime,
                transform.position.z);
                Points += speed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position = new Vector3(
                transform.position.x - speed*Time.deltaTime,
                transform.position.y,
                transform.position.z);
                Points += speed*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position = new Vector3(
                transform.position.x + speed*Time.deltaTime,
                transform.position.y,
                transform.position.z);
                Points += speed*Time.deltaTime;
        }
    }
}
