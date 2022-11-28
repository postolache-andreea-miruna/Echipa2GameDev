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
    
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            Points += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            Points += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            Points += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            Points += speed * Time.deltaTime;
        }

        //try to make a logic with interval points/stars
        int pointsAsInt = (int)Points;
        if (pointsAsInt < 54)
        {
            messageOutMaze = "3 stele";
        }
        else if (pointsAsInt < 60)
        {
            messageOutMaze = "2 stele";
        }
        else if (pointsAsInt < 65)
        {
            messageOutMaze = "1 stea";
        }
        else
        {
            int coinMustHave = pointsAsInt - 65;
            messageOutMaze = "tb sa reiei jocul daca nu ai capsune suficiente" + coinMustHave;
        }
        Debug.Log(messageOutMaze);
    }

}
