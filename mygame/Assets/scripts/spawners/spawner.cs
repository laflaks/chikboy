using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    #region Initialize
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private float _xRange;
    private bool _gameOver;

    private void Awake()
    {
        _gameOver = body.gameOver;
    }
    #endregion

    #region Update
    private void Update() =>_gameOver = body.gameOver;
    #endregion

    #region Spawn
    void Start() => InvokeRepeating("SpawnBoxForward", 1, 1);
    private void SpawnBoxForward()
    {
        if (!_gameOver && !PlayerController.timestoped && !PlayerController.timeslowed)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-_xRange, _xRange), 6);
            Vector2 spawnRot = new Vector2(0, 180);
            Instantiate(_boxPrefab, spawnPos, Quaternion.Euler(spawnRot));
        }
    }
    #endregion
}
