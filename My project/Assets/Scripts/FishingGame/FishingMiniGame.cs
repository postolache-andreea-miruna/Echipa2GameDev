using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * In unity all the settings are made, now all you have to do is put these two lines where the win part is happenings
 * SceneManager.LoadScene("FishingWinCase");
   finalText.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
 */
public class FishingMiniGame : MonoBehaviour
{
    [Header("Fishing Area")]
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;

    [Header("Fishing Settings")]
    [SerializeField] Transform fish;
    [SerializeField] float smoothMotion = 3f;
    [SerializeField] float fishTimeRandomizer = 3f;
    float fishPosition;
    float fishSpeed;
    float fishTimer;
    float fishTargetPosition;

    [SerializeField] Transform hook;
    [SerializeField] float hookSize = .18f;
    [SerializeField] float hookSpeed = .1f;
    [SerializeField] float hookGravity = .05f;
    float hookPosition;
    float hookPullVelocity;

    [Header("Progress Bar Settings")]
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float hookPower;
    [SerializeField] float progressBarDecay;
    float catchProgress;

    [SerializeField] Text countdownText;
/*    [SerializeField] Text finalText1;
    [SerializeField] Text finalText2;
    [SerializeField] Text finalText3;*/
    float currentTime = 0f;
    float startingTime = 30f;

    private string messageOutMaze = "";
    private string sceneToLoad = "";
    int numberOfStars;
    void Start()
    {
        currentTime = startingTime;
        numberOfStars = PlayerPrefs.GetInt("Stars", 0);
    }

    private void FixedUpdate()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = "Time left = " + (int)System.Math.Floor(currentTime) + "s";

        MoveFish();
        MoveHook();
        CheckProgress();

    }
    private void MoveFish()
    {
        fishTimer -= Time.deltaTime;
        if(fishTimer<0)
        {
            fishTimer = Random.value * fishTimeRandomizer;
            fishTargetPosition = Random.value;
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishTargetPosition, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomBounds.position, topBounds.position, fishPosition);
    }
    private void MoveHook()
    {

        if(Input.GetMouseButton(0))
        {
            hookPullVelocity += hookSpeed * Time.deltaTime;
        }
        hookPullVelocity -= hookGravity * Time.deltaTime;
        hookPosition += hookPullVelocity;
        if (hookPosition - hookSize / 2 <= 0 && hookPullVelocity < 0)
            hookPullVelocity = 0;
        if (hookPosition + hookSize / 2 >= 1 && hookPullVelocity > 0)
            hookPullVelocity = 0;

        hookPosition += hookPullVelocity;
        hookPosition = Mathf.Clamp(hookPosition, hookSize/2, 1-hookSize/2);
        hook.position = Vector3.Lerp(bottomBounds.position, topBounds.position, hookPosition);

    }
    private void CheckProgress()
    {
        Vector3 progressBarScale = progressBarContainer.localScale;
        progressBarScale.y = catchProgress;
        progressBarContainer.localScale = progressBarScale;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;
        
        if (min<fishPosition && fishPosition<max)
        {
            catchProgress += hookPower * Time.deltaTime;
            if (catchProgress >= 1 && currentTime < 27f)
            {
                if (currentTime >= 5f) // cel putin 5 sec ramase
                {
                    messageOutMaze = "3 stele";
                    sceneToLoad = "FishingWin3Case";
                    numberOfStars += 3;
                    //finalText1.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds"; //in time 1 1
                }
                else if (currentTime >= 2f)  // intre 2 si 4 sec ramase
                {
                    messageOutMaze = "2 stele";
                    sceneToLoad = "FishingWin2Case";
                    numberOfStars += 2; 
                    //finalText2.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds"; //time 1 2
                }
                else if (currentTime > 0f) // intre 1 si 2
                {
                    messageOutMaze = "1 stea";
                    sceneToLoad = "FishingWin1Case";
                    numberOfStars += 1;
                    //finalText3.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";//time 1 3
                }
                PlayerPrefs.SetInt("Stars", numberOfStars);
                SceneManager.LoadScene(sceneToLoad);
                //"Number of stars: " + messageOutMaze; //
            }
                
        }
        else
        {
            catchProgress -= progressBarDecay * Time.deltaTime;
            if (catchProgress <= 0)
                Debug.Log("You lose");
        }
        catchProgress = Mathf.Clamp(catchProgress, 0, 1);

        if (currentTime <= 0)
        {
            SceneManager.LoadScene("FishingLoseCase");
        }
    }

}
