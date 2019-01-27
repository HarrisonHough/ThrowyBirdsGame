using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : GenericSingleton<GameManager> 
{
    public GameState CurrentState;
    private Level currentLevel;

    private int birdsDestroyed;
    private int enemiesKilled;

    private bool levelComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        //Reset();
    }

    public void OnLevelStart(Level level)
    {
        currentLevel = level;
       
        Reset();
    }

    private void Reset()
    {
        birdsDestroyed = 0;
        enemiesKilled = 0;
        levelComplete = false;
        StartCoroutine(GameLoop());
    }


    public void DestroyBird()
    {
        birdsDestroyed++;
    }

    public void KillEnemy()
    {
        enemiesKilled++;
    }

    IEnumerator GameLoop()
    {
        CurrentState = GameState.InGame;

        yield return GameRoutine();
        if (!levelComplete)
        {
            yield return LevelFailedRoutine();
        }
        else
        {
            yield return LevelCompleteRoutine();
        }

    }

    IEnumerator GameRoutine()
    {
        while (birdsDestroyed < currentLevel.Birds.Length && enemiesKilled < currentLevel.Enemies.Length)
        {
            yield return null;
        }

        if (enemiesKilled == currentLevel.Enemies.Length)
        {
            //level complete
            levelComplete = true;
        }

        CurrentState = GameState.GameOver;
    }

    IEnumerator LevelFailedRoutine()
    {
        //show gameOver UI
        currentLevel.UIControl.ToggleLevelFailed(true);
        yield return null;
    }

    IEnumerator LevelCompleteRoutine()
    {
        //show gameOver UI
        currentLevel.UIControl.ToggleLevelComplete(true);
        yield return null;
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
