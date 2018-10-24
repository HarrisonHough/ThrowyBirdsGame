using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Game Manager Class
*/


public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public CameraFollow cameraFollow;

    int currentBirdIndex;

    public Slingshot slingshot;

    [HideInInspector]
    public static GameState gamestate;

    private List<GameObject> bricks;
    private List<GameObject> birds;
    private List<GameObject> pigs;

    // Use this for initialization
    void Awake () {
        if (Instance == null)
            Instance = this;

        gamestate = GameState.Start;
        slingshot.enabled = false;

        slingshot.slingshotLineRenderer1.enabled = false;
        slingshot.slingshotLineRenderer2.enabled = false;


        //TODO optimize later
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
    }

    void OnEnable()
    {
        //TODO double check for bugs
        slingshot.birdthrown += SlingshotBirdThrown;
    }

    private void OnDisable()
    {
        //TODO double check for bugs
        slingshot.birdthrown -= SlingshotBirdThrown;
    }

    // Update is called once per frame
    void Update () {
        switch (gamestate)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.Playing:
                //TODO Simplify if possible
                if (slingshot.slingshotState == SlingshotState.BirdFlying 
                    && (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.timeSinceThrown > 5f))
                {
                    slingshot.enabled = false;

                    slingshot.slingshotLineRenderer1.enabled = false;
                    slingshot.slingshotLineRenderer2.enabled = false;

                    AnimateCameraToStartPosition();
                    gamestate = GameState.BirdMovingToSlingshot;

                }
                break;
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonDown(0))
                {
                    //TODO cleanup / upgrade
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //Application.LoadLevel(Application.loadedLevelName);
                }
                break;

        }
	}

    void AnimateBirdToSlingshot()
    {
        gamestate = GameState.BirdMovingToSlingshot;

        //TODO clean this up
        birds[currentBirdIndex].transform.positionTo(
            Vector2.Distance(birds[currentBirdIndex].transform.position / 10,
            slingshot.birdWaitPosition.position) / 10, slingshot.birdWaitPosition.position).
            setOnCompleteHandler((x)=> {
                x.complete();
                x.destroy();

                gamestate = GameState.Playing;
                slingshot.enabled = true;

                slingshot.slingshotLineRenderer1.enabled = true;
                slingshot.slingshotLineRenderer2.enabled = true;

                slingshot.birdToThrow = birds[currentBirdIndex];
            });
    }

    bool BricksBirdsPigsStoppedMoving()
    {
        //TODO clean up this mess
        foreach (var item in bricks.Union(birds).Union(pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > GameVariables.MinVelocity) {
                return false;
            } 
        }
        return true;
    }

    private bool AllPigsAreDestroyed()
    {
        //TODO Clean up
        return pigs.All(x => x == null);
    }

    private void AnimateCameraToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.startingPosition) / 10f;
        if (duration == 0.0f)
            duration = 0.1f;

        //TODO Cleanup
        Camera.main.transform.positionTo(duration, 
            cameraFollow.startingPosition).setOnBeginHandler((x) => 
            {
                cameraFollow.isFollowing = false;
                if (AllPigsAreDestroyed())
                {
                    gamestate = GameState.Won;
                }
                else if (currentBirdIndex == birds.Count - 1)
                {
                    gamestate = GameState.Lost;
                }
                else
                {
                    slingshot.slingshotState = SlingshotState.Idle;
                    currentBirdIndex++;
                    AnimateBirdToSlingshot();
                }
            }
            );
    }

    private void SlingshotBirdThrown()
    {
        cameraFollow.birdToFollow = birds[currentBirdIndex].transform;
        cameraFollow.isFollowing = true;
    }
}
