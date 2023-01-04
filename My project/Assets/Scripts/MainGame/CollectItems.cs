using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    //private int strawberry = 0;
    public int strawberry = 0;
    [SerializeField]
    public TextMeshProUGUI _strawberryTxt;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private GameObject _lossPrefab;

 
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
        _strawberryTxt.text = strawberry +"  <sprite=0>";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Strawberry"))
        {
            Destroy(collision.gameObject); // destroy the strawberry
            strawberry++;
            //strawberryMoneySave();
            PlayerPrefs.SetInt("Coins", strawberry);
            _strawberryTxt.text = strawberry + "  <sprite=0>";
        }
        if ((collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Saw")) && strawberry > 0)
        {
            ShowLoss();
            strawberry--;
            PlayerPrefs.SetInt("Coins", strawberry);
            _strawberryTxt.text = strawberry + "  <sprite=0>";
        }
    }

    private void ShowLoss()
    {
        GameObject lossPrefab = Instantiate(_lossPrefab);
        lossPrefab.transform.SetParent(_canvas.transform, false);
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
