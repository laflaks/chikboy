using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Initialize
    [SerializeField, Range(1, 50), Tooltip("Переменная для скорости")] private float _speed = 5f;
    [SerializeField, Range(1, 50), Tooltip("Переменная для силы импульса")] private float _forcePower = 5f;
    [SerializeField] private GameObject _attackPrefab_r;
    [SerializeField] private GameObject _attackPrefab_l;
    [SerializeField] private GameObject _image;
    [SerializeField] private GameObject _superPowerButton;
    [SerializeField] private GameObject _spawnerEagles;
    [SerializeField] private GameObject _spawnersBoxes;
    [SerializeField] private BoxCollider2D _colider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rbody;
    private float _horizontalInput;
    private bool _rightMove;
    private bool _leftMove;
    private bool _gameOver;
    private bool _onGround;
    private float _temp1;
    private float _temp2;
    private float _temp3;
    public static bool timestoped;
    public static bool timeslowed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
        timestoped = false;
        _onGround = true;
        _rightMove = false;
        _leftMove = false;
        StartCoroutine(PlayIntro());
        
    }
    #endregion

    #region Movement
    private void Update()
    {
        _gameOver = body.gameOver;
        if (!_gameOver)
        {
            Move();
            _rbody.velocity = new Vector2(_horizontalInput, _rbody.velocity.y);
        }

        else
        {
            _rbody.velocity = new Vector2(0, 0);
        }
    }

    
    public void MoveLeftD()
    {
        _leftMove = true;
        _spriteRenderer.flipX = true;
    }

    public void MoveLeftU()
    {
        _leftMove = false;
    }


    public void MoveRightD()
    {
        _rightMove = true;
        _spriteRenderer.flipX = false;
    }

    public void MoveRightU()
    {
        _rightMove = false;
    }

    private void Move()
    {
        if (_leftMove && !_gameOver)
        {
            _horizontalInput = -_speed;
        }

        else if (_rightMove && !_gameOver)
        {
            _horizontalInput = _speed;
        }

        else
        {
            _horizontalInput = 0;
        }
    }
    #endregion

    #region Jump
    public void Jump()
    {
        if (_onGround && !_gameOver)
        {
            _rbody.velocity = Vector2.up * _forcePower;
        }

        else if (_gameOver)
        {
            _rbody.velocity = new Vector2(0, 0);
        }
        _onGround = false;
    }
    #endregion

    #region SuperPower
    public void SuperPower()
    {
        if (!_gameOver && body.superPower)
        {
            StartCoroutine(Abillity());
        }
        
    }

    IEnumerator Abillity()
    {
        if (shopManager.defPut == 2)
        {
            _speed *= 2;
            _image.GetComponent<Image>().color = new Color32(255, 0, 0, 45);
            _superPowerButton.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(5);
            _speed /= 2;
            _image.GetComponent<Image>().color = new Color32(255, 0, 0, 0);
            body.superPower = false;

        }

        else if (shopManager.samuraiPut == 2)
        {
            timeslowed = true;
            _temp2 = simpleBox._gravity;
            eagle._speed /= 3f;
            simpleBox._gravity = 0f;
            spikeBoxMove._horizontalInput /= 3f;
            _speed /= 3f;
            _image.GetComponent<Image>().color = new Color32(0, 0, 255, 45);
            _superPowerButton.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(5);
            eagle._speed *= 3f;
            simpleBox._gravity = _temp2;
            spikeBoxMove._horizontalInput *= 3f;
            _speed *= 3f;
            _image.GetComponent<Image>().color = new Color32(0, 0, 255, 0);
            timeslowed = false;
            body.superPower = false;
        }

        else if (shopManager.dioPut == 2)
        {
            timestoped = true;
            _spawnerEagles.GetComponent<spawnerEagles>().enabled = false;
            _spawnersBoxes.GetComponent<spawner>().enabled = false;
            _temp1 = eagle._speed;
            _temp2 = simpleBox._gravity;
            _temp3 = spikeBoxMove._horizontalInput;
            eagle._speed = 0;
            simpleBox._gravity = 0;
            spikeBoxMove._horizontalInput = 0;
            simpleBox._rbodyBox.Sleep();
            _image.GetComponent<Image>().color = new Color32(255, 255, 0, 45);
            _superPowerButton.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(5);
            _image.GetComponent<Image>().color = new Color32(255, 255, 0, 0);
            _spawnerEagles.GetComponent<spawnerEagles>().enabled = true;
            _spawnersBoxes.GetComponent<spawner>().enabled = true;
            eagle._speed = _temp1;
            simpleBox._gravity = _temp2;
            spikeBoxMove._horizontalInput = _temp3;
            simpleBox._rbodyBox.WakeUp();
            body.superPower = false;
            timestoped = false;
        }

        else if (shopManager.secretPut == 2)
        {
            _image.GetComponent<Image>().color = new Color32(0, 255, 0, 45);
            _superPowerButton.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            _colider.enabled = false;
            yield return new WaitForSeconds(5);
            _colider.enabled = true;
            _image.GetComponent<Image>().color = new Color32(0, 255, 0, 0);
            body.superPower = false;
        }

    }
    #endregion

    #region Attack
    public void PlayerAttack() 
    {
        if (!_gameOver && _spriteRenderer.flipX == false)
        {
            Instantiate(_attackPrefab_r, transform.position, _attackPrefab_r.transform.rotation);
        }

        else if (!_gameOver && _spriteRenderer.flipX == true)
        {
            Instantiate(_attackPrefab_l, transform.position, _attackPrefab_l.transform.rotation);
        }
    }
    #endregion

    #region Intro
    IEnumerator PlayIntro()
    {
        body.gameOver = true;
        _animator.Play("appear");
        yield return new WaitForSeconds(1);
        if (shopManager.defPut == 2)
        {
            _animator.Play("chikboyidle");
        }

        else if(shopManager.samuraiPut == 2)
        {
            _animator.Play("Samuraiidle");
        }

        else if (shopManager.dioPut == 2)
        {
            _animator.Play("Dioidle");
        }

        else if (shopManager.secretPut == 2)
        {
            _animator.Play("Secretidle");
        }
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _onGround = true;
        }

        if (collision.gameObject.CompareTag("ambitLeft"))
        {
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        }

        if (collision.gameObject.CompareTag("ambitRight"))
        {
            transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
        }
    }
    #endregion
}
