using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //TODO error check if needed
    public void PlayGame()
    {
        
        SceneManager.LoadScene("LevelMenu");
    }
}
