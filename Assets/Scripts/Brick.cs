using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2019
* VERSION: 1.0
* SCRIPT: Brick Class
*/

/// <summary>
/// 
/// </summary>
public class Brick : MonoBehaviour
{
    [SerializeField]
    private float health = 70f;

    private float damageMultiplier = 10f;

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
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        HandleCollision(collision.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    private void HandleCollision(GameObject target)
    {
        float damage = target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * damageMultiplier;

        //check for high damage
        if (damage > 20)
        {
            //audioSource.Play();
            Debug.Log("High Damage!");
        }

        //decrease health
        health -= damage;

        //check health
        if (health <= 0)
        {
            //TODO possibly change
            gameObject.SetActive(false);
        }
            
    }

}
