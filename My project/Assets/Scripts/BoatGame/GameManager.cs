using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;
    private int time = 365;
    //private int time = 25;
    private int remainTime;
    [SerializeField]
    private Text timeText;

/*    [SerializeField]
    private Text finalTimeText1;
    [SerializeField]
    private Text finalTimeText2;
    [SerializeField]
    private Text finalTimeText3;*/

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;

    private int initialState = 0;
    private string messageOutMaze = "";
    private string sceneToLoad = "";
    private int numberOfStars;
    // game with 4 x 4 pieces.
    private void CreateGamePieces(float gapThickness)
    {
        
        float width = 1 / (float)size; //for each tile
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                // game board from -1 to +1.
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width,
                                                  0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                // empty space
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    // map the UV coordinates
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // UV coord : (0, 1), (1, 1), (0, 0), (1, 0) -top left, top right, bt left, bt right
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    // Assign the UVs to the mesh and the image is placed all over the space.
                    mesh.uv = uv;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfStars = PlayerPrefs.GetInt("Stars", 0);
        pieces = new List<Transform>();
        size = 4;
        CreateGamePieces(0.01f);
       // time = 25;
        time = 365;//360
        timeText.text = "Remain Time: " + time;
        StartCoroutine(CountTimer());
    }
    IEnumerator CountTimer()
    {
        while (true)
        {
            time -= 1;
            timeText.text = "Time: " + time;
            yield return new WaitForSeconds(1);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (CheckCompletion() == true && initialState == 1)
        {
            //remainTime = 25 - time;
            remainTime = 360 - time;
            Debug.Log(remainTime);

            

                if (time >= 200) // cel putin 5 sec ramase
                {
                    messageOutMaze = "3 stele";
                    sceneToLoad = "RiverWin3Game";
                    numberOfStars += 3;
                  //  finalTimeText1.text = "Time: " + remainTime; //time 2
            }
                else if (time >= 50)  // intre 2 si 4 sec ramase
                {
                    messageOutMaze = "2 stele";
                    sceneToLoad = "RiverWin2Game";
                    numberOfStars += 2;
                   // finalTimeText2.text = "Time: " + remainTime;//time 3

            }
                else if (time > 20) // intre 1 si 2
                {
                    messageOutMaze = "1 stea";
                    sceneToLoad = "RiverWin1Game";
                    numberOfStars += 1;
                   // finalTimeText3.text = "Time: " + remainTime;//time 4
            }

            //finalTimeText.text = "Number of stars: " + messageOutMaze; //"Time: " + remainTime;
            PlayerPrefs.SetInt("Stars", numberOfStars);
            SceneManager.LoadScene(sceneToLoad);
        }
        if (time == 0)
        {
            if (CheckCompletion() == false)
            {
                SceneManager.LoadScene("RiverLoseGame");
            }
        }
        // Check for completion.
        if (!shuffling && CheckCompletion() && initialState == 0)
        {
            Debug.Log(initialState);
            shuffling = true;
            //if (time == 24)
             if (time == 364)
            {

                StartCoroutine(WaitShuffle(5f));
                

            }
        }

        // On click send out ray to see if we click a piece.
        if (Input.GetMouseButtonDown(0)) // 0 == left mouse
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); //the mouse coord
            if (hit)
            {
                // Go through the list, the index tells us the position.
                for (int i = 0; i < pieces.Count; i++)
                {
                    if(pieces[i] != hit.transform) //what piece I clicked
                    {
                        continue;
                    }
                    if (pieces[i] == hit.transform) //what piece I clicked
                    {
                        // Check each direction to see if valid move.
                        // We break out on success so we don't carry on and swap back again.
                        if (SwapIfValid(i, -size, size)) { break; }  //-size = tile up
                        if (SwapIfValid(i, +size, size)) { break; }  //-size = tile down
                        if (SwapIfValid(i, -1, 0)) { break; }        //left , 0 = we are on left side
                        if (SwapIfValid(i, +1, size - 1)) { break; } // right, size - 1 = we are on the right side
                    }
                }
            }
            if (!hit)
            {
                return;
            }
        }

        if (!Input.GetMouseButtonDown(0)) // 0 == left mouse
        {
            return;
        }
    }

    // colCheck is used to stop horizontal moves wrapping.
    private bool SwapIfValid(int i, int offset, int colCheck) 
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation)) //&& the locating index is the empty location
        {
            // Swap in game
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            // Update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }

    // We name the pieces in order so we can use this to check completion.
    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
        initialState = 1;
    }

    // Brute force
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            
            int rnd = Random.Range(0, size * size);// random index
   
            if (rnd == last) { continue; }
            last = emptyLocation; //last location of he empty piece

            // pieces looking for valid move.
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }
}