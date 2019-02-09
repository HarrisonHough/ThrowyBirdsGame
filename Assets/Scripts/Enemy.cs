using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: Enemy Class
*/


/// <summary>
/// 
/// </summary>
public struct DamageState
{
    public Sprite sprite;
    public float healthThreshold;
}


/// <summary>
/// 
/// </summary>
public class Enemy : MonoBehaviour
{
    //adjustable variable for enemy health
    [SerializeField]
    private float _health = 150f;

    //References to different damage states
    [SerializeField]
    private DamageState _damageState1;
    [SerializeField]
    private DamageState _damageState2;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check collision object for rigidbody
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        //check if player
        if (collision.gameObject.tag == "Bird")
        {
            //play audio
            //destroy/hide object
            GameManager.Instance.KillEnemy();
            gameObject.SetActive(false);
        }
        else
        {
            HandleCollision(collision.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    private void HandleCollision(GameObject target)
    {
        //calculate damage
        float damage = target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;
        if (damage >= 10)
        {
            //play death audio
            //audioSource.Play();
        }
            
        //decrease health
        _health -= damage;

        //TODO implement damage states
        //if (health < changeSpriteHealth)
            //gameObject.GetComponent<SpriteRenderer>().sprite = spriteShownWhenHurt;
            
        //check health
        if (_health <= 0)
        {
            //tell GameManeger enemy has died
            GameManager.Instance.KillEnemy();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
            
    }

}
