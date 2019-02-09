using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: GameManager Class
*/

/// <summary>
/// 
/// </summary>
public class GameManager : GenericSingleton<GameManager> 
{
    public GameState CurrentState;
    private Level _currentLevel;

    private int _birdsDestroyed;
    private int _enemiesKilled;

    private bool _levelComplete = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //Reset();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    public void OnLevelStart(Level level)
    {
        _currentLevel = level;
       
        Reset();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Reset()
    {
        _birdsDestroyed = 0;
        _enemiesKilled = 0;
        _levelComplete = false;
        StartCoroutine(GameLoop());
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroyBird()
    {
        _birdsDestroyed++;
    }

    /// <summary>
    /// 
    /// </summary>
    public void KillEnemy()
    {
        _enemiesKilled++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator GameLoop()
    {
        CurrentState = GameState.InGame;

        yield return GameRoutine();
        if (!_levelComplete)
        {
            yield return LevelFailedRoutine();
        }
        else
        {
            yield return LevelCompleteRoutine();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator GameRoutine()
    {
        while (_birdsDestroyed < _currentLevel.Birds.Length && _enemiesKilled < _currentLevel.Enemies.Length)
        {
            yield return null;
        }

        if (_enemiesKilled == _currentLevel.Enemies.Length)
        {
            //level complete
            _levelComplete = true;
        }

        CurrentState = GameState.GameOver;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelFailedRoutine()
    {
        //show gameOver UI
        _currentLevel.UIControl.ToggleLevelFailed(true);
        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelCompleteRoutine()
    {
        //show gameOver UI
        _currentLevel.UIControl.ToggleLevelComplete(true);
        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
