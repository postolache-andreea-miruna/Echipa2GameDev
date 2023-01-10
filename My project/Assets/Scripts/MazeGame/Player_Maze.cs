using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player_Maze : MonoBehaviour
{
    public GameObject playerMaze;
    public float speed = 2;
    private float Points = 0f;

    private string messageOutMaze = "";
    private string sceneToLoad = "";

    [SerializeField]
    private Text finalStarText;
    private int numberOfStars;
    private bool addedStars = false;
    // Start is called before the first frame update
    void Start()
    {
        numberOfStars = PlayerPrefs.GetInt("Stars", 0);
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
            sceneToLoad = "MazeWin3Game";
            if (addedStars == false)
            {
                numberOfStars += 3;
                PlayerPrefs.SetInt("Stars", numberOfStars);
                addedStars = true;
            }
        }
        else if (pointsAsInt < 60)
        {
            messageOutMaze = "2 stele";
            sceneToLoad = "MazeWin2Game";
            if (addedStars == false)
            {
                numberOfStars += 2;
                PlayerPrefs.SetInt("Stars", numberOfStars);
                addedStars = true;
            }
        }
        else if (pointsAsInt < 65)
        {
            messageOutMaze = "1 stea";
            sceneToLoad = "MazeWin1Game";
            if (addedStars == false)
            {
                numberOfStars += 1;
                PlayerPrefs.SetInt("Stars", numberOfStars);
                addedStars = true;
            }
        }
        else
        {
            int coinMustHave = pointsAsInt - 65;
            messageOutMaze = "tb sa reiei jocul daca nu ai capsune suficiente" + coinMustHave;
        }

        float posX = playerMaze.transform.position.x;
        float posY = playerMaze.transform.position.y;
        if (posX > 5.88 && posX < 5.89)
        //if (posX == 5.884998 && posY < -4)
        {
            if (posY < -4)
            {
                //Debug.Log(pointsAsInt);
                //next scene
                if (pointsAsInt >= 65)
                {
                    //lose
                    SceneManager.LoadScene("MazeLoseGame");
                }
                else
                {
                    //win
                    SceneManager.LoadScene(sceneToLoad);
                    finalStarText.text = "Number of stars: " + messageOutMaze;
                    Debug.Log(finalStarText.text);
                    //SceneManager.LoadScene("MazeWinGame");
                }
            }
        }
        Debug.Log(messageOutMaze);
    }

}
