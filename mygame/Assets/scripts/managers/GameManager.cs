using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Optimization
    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 120;
    }
    #endregion
}
