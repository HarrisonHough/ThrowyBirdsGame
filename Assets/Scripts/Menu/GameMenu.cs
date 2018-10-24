using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Game Menu Class
*/

public class GameMenu : MonoBehaviour {

    public void BackToLevelMenu()
    {
        SceneManager.LoadScene(1);
    }
}
