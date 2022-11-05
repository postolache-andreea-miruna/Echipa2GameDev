using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayer : MonoBehaviour
{//daca playerul loveste platforma atunci se va misca cu platforma in acelasi timp

    private void OnTriggerEnter2D(Collider2D collision) // cel de la suprafata
    {
        if (collision.gameObject.name == "PlayerGame")
        {
            //se pune jucatorul ca copil al platformei
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerGame")
        {
            //se scoate jucatorul ca copil al platformei
            collision.gameObject.transform.SetParent(null);
        }
    }
}
