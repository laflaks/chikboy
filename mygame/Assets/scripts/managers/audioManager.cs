using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{
    #region Initialize
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _volumeButton;
    [SerializeField] private Sprite _volumeOnButton;
    [SerializeField] private Sprite _volumeOffButton;
    private bool _work; 
    private void Start()
    {
        _work = true;
        _audioSource.Play();
    }
    #endregion

    #region Methods
    public void VolumeOn()
    {
        if (_work)
        {
            _audioSource.volume = 0.5f;
            _volumeButton.GetComponent<Image>().sprite = _volumeOnButton;
            _work = !_work;
        }

        else if (!_work)
        {
            _audioSource.volume = 0f;
            _volumeButton.GetComponent<Image>().sprite = _volumeOffButton;
            _work = !_work;
        }

    }
    #endregion
}
