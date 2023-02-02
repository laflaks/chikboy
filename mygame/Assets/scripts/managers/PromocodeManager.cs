using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromocodeManager : MonoBehaviour
{
    #region Initialize
    [SerializeField] private Text promocode;
    [SerializeField] private Text _statusText;
    [SerializeField] private Text _idText;
    [SerializeField] private GameObject _boxchel;
    [SerializeField] private GameObject _boxchelSad;
    private TouchScreenKeyboard _keyboard;
    private string _promo;
    private int _promoUsed;
    private string _strId;

    private void Awake()
    {
        if (PlayerPrefsSafe.HasKey("promoused"))
        {
            _promoUsed = PlayerPrefsSafe.GetInt("promoused", _promoUsed);
        }

        else
        {
            PlayerPrefsSafe.SetInt("promoused", 1);
        }

        if (PlayerPrefs.HasKey("ID"))
        {
            _strId = PlayerPrefs.GetString("ID", _strId);
            _idText.text = $"ID: {_strId}";
            _promo = crypter.ToHexString(_strId).Substring(0, 10);
            Debug.Log(_promo);
        }

        else
        {
            reload();
        }
    }
    #endregion

    #region Methods
    public void openKeyboard()
    {
        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void reload()
    {
        if (_promoUsed == 0)
        {
            _strId = crypter.GenerateId();
            PlayerPrefs.SetString("ID", _strId);
            _idText.text = $"ID: {_strId}";
            _promo = crypter.ToHexString(_strId).Substring(0, 10);
            Debug.Log(_promo);
            PlayerPrefsSafe.SetInt("promoused", 1);
            _promoUsed = PlayerPrefsSafe.GetInt("promoused", _promoUsed);
        }
            
    }

    public void enter()
    {
        if (promocode.text == _promo && _promoUsed == 1)
        {
            _statusText.text = "promocode entered :)";
            _statusText.color = Color.yellow;
            _boxchel.SetActive(true);
            _boxchelSad.SetActive(false);
            shopManager.coins += 100;
            PlayerPrefsSafe.SetInt("Coins", shopManager.coins);
            PlayerPrefsSafe.SetInt("promoused", 0);
            _promoUsed = PlayerPrefsSafe.GetInt("promoused", _promoUsed);
        }

        else if (promocode.text == "reset")
        {
            PlayerPrefsSafe.SetInt("promoused", 0);
            _promoUsed = PlayerPrefsSafe.GetInt("promoused", _promoUsed);
            shopManager.coins = 0;
            PlayerPrefsSafe.SetInt("Coins", shopManager.coins);
            shopManager.SetDefault();
            _statusText.text = "RESETED";
            _statusText.color = Color.red;
        }

        else if (promocode.text == "getmoney2k")
        {
            shopManager.coins += 2000;
            PlayerPrefsSafe.SetInt("Coins", shopManager.coins);
            _statusText.text = "SETED";
            _statusText.color = Color.black;
        }

        else
        {
            _statusText.text = "wrong promocode >:(";
            _statusText.color = Color.red;
        }
        
    }
    #endregion
}
