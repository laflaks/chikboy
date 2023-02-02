using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    #region Initialize
    [SerializeField] private GameObject _pauseBar;
    [SerializeField] private GameObject _deathBar;

    private void Awake()
    {
        shopManager.CheckSkinAll();
    }

    #endregion

    #region Methods
    public void Scenes(string name)
    {
        SceneManager.LoadScene(name);
        body.gameOver = false;
        body.isCanCreate = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        body.gameOver = false;
        body.isCanCreate = true;
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Pause()
    {
        if (!body.gameOver)
        {
            _pauseBar.SetActive(true);
            body.gameOver = true;
        }
    }

    public void Resume()
    {
        _pauseBar.SetActive(false);
        _deathBar.SetActive(false);
        body.gameOver = false;
    }
    #endregion
}
