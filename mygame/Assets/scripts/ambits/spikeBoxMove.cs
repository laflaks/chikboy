using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBoxMove : MonoBehaviour
{
    #region Initialize
    private Rigidbody2D _rbody;
    public static float _horizontalInput;
    private bool _gameOver;

    private void Awake()
    {
        _gameOver = body.gameOver;
        _rbody = GetComponent<Rigidbody2D>();
        _horizontalInput = -2f;
    }
    #endregion

    #region Update
    private void Update()
    {
        _gameOver = body.gameOver;
        if (!_gameOver)
        {
            _rbody.velocity = new Vector2(_horizontalInput, _rbody.velocity.y);
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
        if (collision.CompareTag("ambitLeft"))
        {
            _horizontalInput = -_horizontalInput;
        }

        else if (collision.CompareTag("ambitRight"))
        {
            _horizontalInput = -_horizontalInput;
        }
    }
    #endregion
}
