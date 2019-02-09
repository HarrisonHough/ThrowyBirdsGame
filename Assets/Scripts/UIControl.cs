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
    private GameObject _levelFailedPanel;
    [SerializeField]
    private GameObject _levelCompletePanel;
    [SerializeField]
    private Text _finalScore;

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
        _finalScore.text = score.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable"></param>
    public void ToggleLevelFailed(bool enable)
    {
        _levelFailedPanel.SetActive(enable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable"></param>
    public void ToggleLevelComplete(bool enable)
    {
        _levelCompletePanel.SetActive(enable);
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
