using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    private int strawberry = 0;

    [SerializeField]
    private Text _strawberryTxt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Strawberry"))
        {
            Destroy(collision.gameObject); // destroy the cherry
            strawberry = strawberry + 1;
            _strawberryTxt.text = "Strawberries nr = " + strawberry;
        }
    }
}
