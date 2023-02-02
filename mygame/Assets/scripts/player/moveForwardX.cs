using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForwardX : MonoBehaviour
{
    #region Initialize
    [SerializeField] private float _speed;
    [SerializeField] private bool _gameOver;
    private GameObject _player;
    private Rigidbody2D _rbody;
    private float _horizontalInput;

    private void Awake()
    {
        _horizontalInput = _speed;
        _player = GameObject.Find("player");
        _rbody = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Update
    private void Update()
    {
        _rbody.velocity = new Vector2(_horizontalInput, _rbody.velocity.y);
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ambit"))
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
