using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Brick Class
*/


public class Brick : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private float health = 70f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        

        float damage = target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;

        if (damage > 10)
        {
            audioSource.Play();
        }

        if (target.gameObject.tag == "Bird")
        {

        }

        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }


}
