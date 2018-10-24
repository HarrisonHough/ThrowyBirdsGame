using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Main Menu Class
*/

public class MainMenu : MonoBehaviour {

    //TODO error check if needed
    public void PlayGame()
    {
        
        SceneManager.LoadScene(1);
    }
}
