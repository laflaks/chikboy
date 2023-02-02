using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    //не мой код
    [SerializeField] private string _androidGameId = "4777821";
    [SerializeField] private string _IOSGameId = "4777820";
    [SerializeField] private bool _testMode = true;
    private string _gameId;
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization failed: {error.ToString()} - {message}");
    }

    private void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _IOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);

    }
}
