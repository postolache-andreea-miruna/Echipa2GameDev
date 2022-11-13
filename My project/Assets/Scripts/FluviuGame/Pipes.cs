using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public float[] _correctRotation;
    public int _rotationPos = 1;
    [SerializeField]
    bool _correctPlace = false;
    GameManager _game;

    private void Awake()
    {
        _game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    private void  Start()
    {
        _rotationPos = _correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0f, 0f, rotations[rand]);
       if(_rotationPos > 1)
        {
            if (transform.eulerAngles.z == _correctRotation[0] || transform.eulerAngles.z == _correctRotation[1])
            {
                _correctPlace = true;
                _game.Correct();
            }
        }
       else
        {
            if(transform.eulerAngles.z == _correctRotation[0] )
            {
                _correctPlace = true;
                _game.Correct();
            }
        }
    }

    // Update is called once per frame
    private void  OnMouseDown()
    {
        transform.Rotate(0, 0, 90);
        transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z));
        if (_rotationPos > 1)
        {
            if (transform.eulerAngles.z == _correctRotation[0]  || transform.eulerAngles.z == _correctRotation[1]  && _correctPlace == false)
            {
                _correctPlace = true;
                _game.Correct();

            }
            else if (_correctPlace == true)
            {
                _correctPlace = false;
                _game.Wrong();

            }
        }
        else
        {
            if (transform.eulerAngles.z == _correctRotation[0]  && _correctPlace == false)
            {
                _correctPlace = true;
                _game.Correct();

            }
            else if (_correctPlace == true)
            {
                _correctPlace = false;
                _game.Wrong();
            }
        }


    }
}
