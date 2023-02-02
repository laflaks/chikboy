using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : MonoBehaviour
{
    #region Initialize
    public static float _speed;
    [SerializeField] private bool _gameOver;
    private Animator _animator;
    private Rigidbody2D _rbody;

    private void Awake()
    {
        _speed = 2f;
        _animator = GetComponent<Animator>();
        _gameOver = body.gameOver;
        _rbody = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Update
    private void Update()
    {
        _animator = GetComponent<Animator>();
        _gameOver = body.gameOver;
        if (!_gameOver)
        {
            _rbody.velocity = new Vector2(_speed, _rbody.velocity.y);
        }

        else
        {
            _rbody.velocity = new Vector2(0, 0);
        }
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ambit"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("egg"))
        {
            _animator.Play("eagle_death");
            Destroy(this.gameObject, 0.4f);
        }
    }
    #endregion
}
