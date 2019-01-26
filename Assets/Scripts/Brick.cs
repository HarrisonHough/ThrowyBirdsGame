using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private float health = 70f;

    private float damageMultiplier = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject target)
    {
        float damage = target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * damageMultiplier;

        if (damage > 20)
        {
            //audioSource.Play();
            Debug.Log("High Damage!");
        }

        health -= damage;

        if (health <= 0)
        {
            //TODO possibly change
            gameObject.SetActive(false);
        }
            
    }

}
