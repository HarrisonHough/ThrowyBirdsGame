using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
