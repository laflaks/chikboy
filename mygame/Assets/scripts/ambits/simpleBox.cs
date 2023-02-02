using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleBox : MonoBehaviour
{
    #region Initialize
    private Animator _animator; 
    public static Rigidbody2D _rbodyBox;
    [SerializeField] private float _randint;
    [SerializeField] private GameObject _chips;
    public static float _gravity;
    private void Awake()
    {
        _gravity = 2f;
        _animator = GetComponent<Animator>();
        _rbodyBox = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Update
    private void Update()
    {
        if (body.gameOver)
        {
            _rbodyBox.gravityScale = 0;
            _rbodyBox.velocity = new Vector2(0, 0);
            _rbodyBox.Sleep();
        }

        else
        {
            _rbodyBox.gravityScale = _gravity;
        }
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            _animator.Play("destroy");
            _rbodyBox.velocity = new Vector2(0, 0);
            if (body.isCanCreate && !body.superPower)
            {
                Vector2 spawnRot = new Vector2(0, 0);
                Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y + 0.2f);
                _randint = Random.Range(1, 10);
                if (_randint == 1)
                {
                    body.chips = Instantiate(_chips, spawnPos, Quaternion.Euler(spawnRot));
                    body.isCanCreate = false;
                }

            }
            Destroy(this.gameObject, 0.07f);
            
            
        }


    }
    #endregion

}
