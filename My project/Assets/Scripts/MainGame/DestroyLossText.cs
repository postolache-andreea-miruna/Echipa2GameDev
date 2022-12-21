using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLossText : MonoBehaviour
{
    [SerializeField] 
    private float secondsToDestroy = 1f;

    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }

}
