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
    [SerializeField] Text finalText;
    float currentTime = 0f;
    float startingTime = 30f;

    void Start()
    {
        currentTime = startingTime;
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
        if(min<fishPosition && fishPosition<max)
        {
            catchProgress += hookPower * Time.deltaTime;
            if (catchProgress >= 1 && currentTime < 27f)
            {
                SceneManager.LoadScene("FishingWinCase");
                finalText.text = "Time: " + (int)System.Math.Floor(startingTime - currentTime) + " seconds";
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
