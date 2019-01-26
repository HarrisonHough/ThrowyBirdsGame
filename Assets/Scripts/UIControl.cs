using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedPanel;
    [SerializeField]
    private GameObject levelCompletePanel;
    [SerializeField]
    private Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetFinalScore(int score)
    {
        finalScore.text = score.ToString();
    }

    public void ToggleLevelFailed(bool enable)
    {
        levelFailedPanel.SetActive(enable);
    }

    public void ToggleLevelComplete(bool enable)
    {
        levelCompletePanel.SetActive(enable);
    }

    public void PauseButtonPress()
    {

    }

    public void RestartButtonPress()
    {
        GameManager.Instance.ReloadCurrentScene();
    }

    public void NextButtonPress()
    {
        //go to next level
        GameManager.Instance.LoadNextScene();
    }

    public void LoadScene(int index)
    {
        //load home menu screen
        GameManager.Instance.LoadScene(index);
    }
}
