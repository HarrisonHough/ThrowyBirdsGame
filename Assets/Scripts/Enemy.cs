using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageState
{
    public Sprite sprite;
    public float healthThreshold;
}


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health = 150f;

    [SerializeField]
    private DamageState damageState1;
    [SerializeField]
    private DamageState DamageState2;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;
        if (collision.gameObject.tag == "Player")
        {
            //play audio
            //destroy/hide object
            Destroy(gameObject);
        }
        else
        {
            HandleCollision(collision.gameObject);
        }
    }

    private void HandleCollision(GameObject target)
    {
        float damage = target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;
        if (damage >= 10)
        {
            //play death audio
            //audioSource.Play();
        }
            

        health -= damage;

        //TODO implement damage states
        //if (health < changeSpriteHealth)
            //gameObject.GetComponent<SpriteRenderer>().sprite = spriteShownWhenHurt;

        if (health <= 0)
        {
            GameManager.Instance.KillEnemy();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
            
    }

}
