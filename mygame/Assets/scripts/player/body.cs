using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class body : MonoBehaviour
{
    #region Initialize
    private GameObject _player;
    private GameObject _cherry;
    private GameObject _platform;
    [SerializeField] private GameObject _deathBar;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private GameObject _cherry_pr;
    [SerializeField] private GameObject _platform_pr;
    [SerializeField] private GameObject _superPowerButton;
    private Animator _animator;
    private Animator _cherryAnimator;
    private Animator _chipsAnimator;
    public InterstitialAds ads;
    public static bool gameOver;
    private int _score;
    public static GameObject chips;
    public static bool superPower;
    public static bool isCanCreate = true;
    public static int AdCounter = 0;
    private void Awake()
    {
        superPower = false;
        _player = GameObject.Find("player");
        _platform = GameObject.Find("platform");
        _cherry = GameObject.Find("pickupCherry");
        _animator = _player.GetComponent<Animator>();
        _cherryAnimator = _cherry.GetComponent<Animator>();
        gameOver = false;
        if (PlayerPrefsSafe.HasKey("AdCounter"))
        {
            AdCounter = PlayerPrefsSafe.GetInt("AdCounter", AdCounter);
        }

        else
        {
            PlayerPrefsSafe.SetInt("AdCounter", 0);
            AdCounter = PlayerPrefsSafe.GetInt("AdCounter", AdCounter);
        }
    }
    #endregion

    #region Movement
    private void FixedUpdate()
    {
        transform.position = _player.transform.position;
    }
    #endregion

    #region Update
    private void Update()
    {
        _cherryAnimator = _cherry.GetComponent<Animator>();
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle") && !gameOver)
        {
            gameOver = true;
            PlayerController.timeslowed = false;
            PlayerController.timestoped = false;
            _animator.Play("dead");
            Destroy(_player, 1);
            StartCoroutine(Countdown());
            Debug.Log(AdCounter);
            if (AdCounter == 5)
            {
                ads.ShowAd();
                PlayerPrefsSafe.SetInt("AdCounter", 0);
                AdCounter = PlayerPrefsSafe.GetInt("AdCounter", AdCounter);
            }

            else
            {
                AdCounter += 1;
                PlayerPrefsSafe.SetInt("AdCounter", AdCounter);
            }
        }

        else if (collision.CompareTag("cherry") && !gameOver)
        {
            _cherryAnimator.Play("collected");
            Destroy(_cherry, 0.5f);
            Destroy(_platform);
            _score += 1;
            _scoreText.GetComponent<Text>().text = $"{_score}";
            if (!PlayerController.timestoped && !PlayerController.timeslowed)
            {
                eagle._speed += 0.3f;
                simpleBox._gravity += 0.2f;
                if (spikeBoxMove._horizontalInput > 0)
                {
                    spikeBoxMove._horizontalInput += 0.25f;
                }

                else
                {
                    spikeBoxMove._horizontalInput -= 0.25f;
                }
            }
            Vector2 spawnPos = new Vector2(Random.Range(-9, 9), -0.5f);
            _platform = Instantiate(_platform_pr, spawnPos, Quaternion.identity);
            Vector2 spawnPos1 = new Vector2(_platform.transform.position.x + Random.Range(-0.2f, 0.2f), _platform.transform.position.y + 1f);
            _cherry = Instantiate(_cherry_pr, spawnPos1, Quaternion.identity);
        }

        else if (collision.CompareTag("chips") && !gameOver)
        {  
            _chipsAnimator = chips.GetComponent<Animator>();
            _chipsAnimator.Play("collected");
            Destroy(chips, 0.5f);
            isCanCreate = true;
            superPower = true;
            _superPowerButton.GetComponent<Image>().color = new Color32(0, 225, 0, 255);
        }
    }
    #endregion

    #region Coroutine
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.7f);
        _scoreText.SetActive(false);
        _deathBar.SetActive(true);
        shopManager.coins += _score;
        PlayerPrefsSafe.SetInt("Coins", shopManager.coins);
        _coinsText.text = $"earned coins: {_score}";
    }
    #endregion
}
