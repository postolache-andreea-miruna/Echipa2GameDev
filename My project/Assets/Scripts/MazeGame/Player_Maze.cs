using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_Maze : MonoBehaviour
{
    public float speed = 2;
    private float Points = 0f;

    private string messageOutMaze = "";

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
        
        //try to make a logic with interval points/stars
        if (((int)Points >= 45 && (int)Points < 54) || (int)Points <= 45){
            //Debug.Log((int)Points);
            messageOutMaze = "3 stele";
           // Debug.Log("3 stele");
        }

       else if ((int)Points >= 54 && (int)Points < 60) {
            //Debug.Log((int)Points);
            //Debug.Log("2 stele");
            messageOutMaze = "2 stele";
        }

        else if ((int)Points >= 60 && (int)Points < 65){
          //  Debug.Log((int)Points);
           // Debug.Log("1 stea");
            messageOutMaze = "1 stea";
        }
        else if ((int)Points >= 65){
           // Debug.Log((int)Points);
            //Debug.Log("tb sa reiei jocul daca nu ai capsune suficiente");
            int coinMustHave = (int)Points - 65;
            //Debug.Log(coinMustHave);

            messageOutMaze = "tb sa reiei jocul daca nu ai capsune suficiente"+coinMustHave;
        }
        Debug.Log(messageOutMaze);
    }

}
