using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Pig Class
*/

public class Pig : MonoBehaviour {

    private AudioSource audioSource;

    public float health = 150f;
    public Sprite spriteShownWhenHurt;
    public float changeSpriteHealth;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        changeSpriteHealth = health - 30f;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }

        if (collision.gameObject.tag == "Bird")
        {
            audioSource.Play();
            Destroy(gameObject);
        }
        else
        {
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;

            if (damage >= 10)
            {
                audioSource.Play();
            }
            health -= damage;

            if(health < changeSpriteHealth)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteShownWhenHurt;
            }

            if (health <= 0)
            {
                //TODO add recycle logic (object pooling)
                Destroy(gameObject);
            }
        }
    }


}
