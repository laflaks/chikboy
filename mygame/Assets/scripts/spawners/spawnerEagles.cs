using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerEagles : MonoBehaviour
{
    #region Initialize
    [SerializeField] private GameObject[] _eaglesPrefab;
    private bool _gameOver;
    private float _xPos;
    private int _side;

    private void Awake()
    {
        _gameOver = body.gameOver;
    }
    #endregion

    #region Update
    private void Update() => _gameOver = body.gameOver;
    #endregion

    #region Spawn
    void Start() => InvokeRepeating("SpawnEaglesForward", 1, 13);
    
    private void SpawnEaglesForward()
    {
        if (!_gameOver)
        {
            _side = Random.Range(0, 1);
            if (_side == 0)
            {
                _xPos = -13;
            }

            else if (_side == 1)
            {
                _xPos = 13;
            }
            Vector2 spawnPos = new Vector2(_xPos, Random.Range(0.6f, 0.8f));
            Vector2 spawnRot = new Vector2(0f, 0f);
            Instantiate(_eaglesPrefab[_side], spawnPos, Quaternion.Euler(spawnRot));
        }
    }
    #endregion
}
