using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    private int strawberry = 0;

    [SerializeField]
    private Text _strawberryTxt;
    //public GameObject strawberryMoney;
    /*    private void Awake()
        {
            strawberryMoneyonLoad();
        }
        void Start()
        {
            if (PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("LoadTime") == 1)
            {
                int coinCounter = strawberry;
                coinCounter = PlayerPrefs.GetInt("Coins");
                strawberry = coinCounter;
                PlayerPrefs.SetInt("LoadTime", 0);
                PlayerPrefs.Save();
            }

        }*/

    private void Start()
    {
        strawberry = PlayerPrefs.GetInt("Coins", strawberry);
        _strawberryTxt.text = "Strawberries nr = " + strawberry;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            Destroy(collision.gameObject); // destroy the strawberry
            strawberry = strawberry + 1;
            //strawberryMoneySave();
            PlayerPrefs.SetInt("Coins", strawberry);
            _strawberryTxt.text = "Strawberries nr = " + strawberry;
        }
    }


   /* public void strawberryMoneySave() //saving player position
    {
        PlayerPrefs.SetInt("Coins", strawberry);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void strawberryMoneyonLoad()
    {
        PlayerPrefs.SetInt("LoadTime", 1);
        PlayerPrefs.Save();
    }*/

}
