using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: UIControl Class
*/

/// <summary>
/// 
/// </summary>
public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedPanel;
    [SerializeField]
    private GameObject levelCompletePanel;
    [SerializeField]
    private Text finalScore;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="score"></param>
    public void SetFinalScore(int score)
    {
        finalScore.text = score.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable"></param>
    public void ToggleLevelFailed(bool enable)
    {
        levelFailedPanel.SetActive(enable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable"></param>
    public void ToggleLevelComplete(bool enable)
    {
        levelCompletePanel.SetActive(enable);
    }

    /// <summary>
    /// 
    /// </summary>
    public void PauseButtonPress()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void RestartButtonPress()
    {
        GameManager.Instance.ReloadCurrentScene();
    }

    /// <summary>
    /// 
    /// </summary>
    public void NextButtonPress()
    {
        //go to next level
        GameManager.Instance.LoadNextScene();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void LoadScene(int index)
    {
        //load home menu screen
        GameManager.Instance.LoadScene(index);
    }
}
