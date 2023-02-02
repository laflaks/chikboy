using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{
    #region Initialize
    [SerializeField] private Text _coinsText;

    [SerializeField] private GameObject _samuraiButton;
    [SerializeField] private GameObject _dioButton;
    [SerializeField] private GameObject _secretButton;

    [SerializeField] private Text _samuraiButtonPut;
    [SerializeField] private Text _dioButtonPut;
    [SerializeField] private Text _defButtonPut;
    [SerializeField] private Text _secretButtonPut;

    public static int coins;
    public static int samurai;
    public static int dio;
    public static int def;
    public static int secret;

    public static int coinsPut;
    public static int samuraiPut;
    public static int dioPut;
    public static int defPut;
    public static int secretPut;
    

    /* 
     1 - не куплен
     2 - куплен
    */

    /* 
     1 - не одет
     2 - одет
    */

    private void Awake()
    {
        CheckSkinAll();
        CheckButtonsBuyAll();
        CheckButtonsPutAll();
        CheckCoins();
    }
    #endregion

    #region Buy
    public void BuySamurai()
    {
        BuySkin("samurai", samurai, 200);
    }

    public void BuyDio()
    {
        BuySkin("dio", dio, 400);
    }

    public void BuySecret()
    {
        BuySkin("secret", secret, 1000);
    }
    #endregion

    #region Put
    public void PutSamurai()
    {
        samurai = CheckSkin("samurai", samurai);
        Debug.Log(samurai);
        if (samurai == 2)
        {
            PutSkin("samuraiPut", "dioPut", "defPut", "secretPut", samuraiPut, dioPut, defPut, secretPut);
        }
        
    }

    public void PutDio()
    {
        dio = CheckSkin("dio", dio);
        if (dio == 2)
        {
            PutSkin("dioPut", "samuraiPut", "defPut", "secretPut", dioPut, samuraiPut, defPut, secretPut);
        }
        
    }

    public void PutSecret()
    {
        secret = CheckSkin("secret", secret);
        if (secret == 2)
        {
            PutSkin("secretPut", "samuraiPut", "defPut", "dioPut", secretPut, samuraiPut, defPut, dioPut);
        }
        
    }

    public void PutDef()
    {
        def = CheckSkin("def", def);
        if (def == 2)
        {
            PutSkin("defPut", "samuraiPut", "secretPut", "dioPut", defPut, samuraiPut, secretPut, dioPut);
        }
        
    }
    #endregion

    #region Buy/Put Methods
    private void BuySkin(string key, int skin, int price)
    {
        skin = PlayerPrefsSafe.GetInt(key, skin);
        CheckCoins();
        if (skin == 1 && coins >= price)
        {
            skin = 2;
            PlayerPrefsSafe.SetInt(key, skin);
            coins -= price;
            PlayerPrefsSafe.SetInt("Coins", coins);
            CheckCoins();
            CheckButtonsBuyAll();
        }
    }

    private void PutSkin(string key, string key1, string key2, string key3, int skin, int skin1, int skin2, int skin3)
    {
        PlayerPrefsSafe.SetInt(key, 2);
        PlayerPrefsSafe.SetInt(key1, 1);
        PlayerPrefsSafe.SetInt(key2, 1);
        PlayerPrefsSafe.SetInt(key3, 1);
        skin = PlayerPrefsSafe.GetInt(key, skin);
        skin1 = PlayerPrefsSafe.GetInt(key1, skin1);
        skin2 = PlayerPrefsSafe.GetInt(key2, skin2);
        skin3 = PlayerPrefsSafe.GetInt(key3, skin3);
        CheckButtonsPutAll();
    }
    #endregion

    #region Checkers
    private void CheckCoins()
    {
        if (PlayerPrefsSafe.HasKey("Coins"))
        {
            coins = PlayerPrefsSafe.GetInt("Coins", coins);
            _coinsText.text = $"Coins: {coins}";
        }

        else
        {
            PlayerPrefsSafe.SetInt("Coins", 0);
        }
    }

    public static int CheckSkin(string key, int skin)
    {
        if (PlayerPrefsSafe.HasKey(key))
        {
            skin = PlayerPrefsSafe.GetInt(key, skin);
        }

        else
        {
            if (key == "defPut")
            {
                PlayerPrefsSafe.SetInt(key, 2);
                skin = PlayerPrefsSafe.GetInt(key, skin);
            }

            else
            {
                PlayerPrefsSafe.SetInt(key, 1);
                skin = PlayerPrefsSafe.GetInt(key, skin);
            }
            
        }
        return skin;
    }

    public static void CheckSkinAll()
    {
        PlayerPrefsSafe.SetInt("def", 2);
        def = PlayerPrefsSafe.GetInt("def", def);
        samurai = CheckSkin("samurai", samurai);
        dio = CheckSkin("dio", dio);
        secret = CheckSkin("secret", secret);
        samuraiPut = CheckSkin("samuraiPut", samuraiPut);
        dioPut = CheckSkin("dioPut", dioPut);
        secretPut = CheckSkin("secretPut", secretPut);
        defPut = CheckSkin("defPut", defPut);
    }

    private void CheckButtonsBuy(string key, int skin, GameObject but)
    {
        skin = PlayerPrefsSafe.GetInt(key, skin);
        if (skin == 2)
        {
            but.SetActive(false);
        }
    }

    private void CheckButtonsBuyAll()
    {
        CheckButtonsBuy("samurai", samurai, _samuraiButton);
        CheckButtonsBuy("dio", dio, _dioButton);
        CheckButtonsBuy("secret", secret, _secretButton);
    }

    private void CheckButtonsPut(string key, int skin, Text but)
    {
        Debug.Log("CHECK BUTTONS PUT");
        skin = PlayerPrefsSafe.GetInt(key, skin);
        if (skin == 2)
        {
            but.text = "Puted On";
            Debug.Log("CHECK BUTTONS PUT skin2");
        }

        else if (skin == 1)
        {
            but.text = "Put On";
            Debug.Log("CHECK BUTTONS PUT skin1");
        }
    }

    private void CheckButtonsPutAll()
    {
        Debug.Log("CHECK BUTTONS PUT ALL");
        CheckButtonsPut("defPut", defPut, _defButtonPut);
        CheckButtonsPut("samuraiPut", samuraiPut, _samuraiButtonPut);
        CheckButtonsPut("dioPut", dioPut, _dioButtonPut);
        CheckButtonsPut("secretPut", secretPut, _secretButtonPut);
    }

    public static void SetDefault()
    {
        PlayerPrefsSafe.DeleteKey("samurai");
        PlayerPrefsSafe.DeleteKey("def");
        PlayerPrefsSafe.DeleteKey("dio");
        PlayerPrefsSafe.DeleteKey("secret");
        PlayerPrefsSafe.DeleteKey("samuraiPut");
        PlayerPrefsSafe.DeleteKey("defPut");
        PlayerPrefsSafe.DeleteKey("dioPut");
        PlayerPrefsSafe.DeleteKey("secretPut");
    }
    #endregion
}
