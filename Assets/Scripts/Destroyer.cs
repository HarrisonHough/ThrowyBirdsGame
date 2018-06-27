using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO possibly move into switch statement
        if (other.tag == "Bird" || other.tag == "Pig" || other.tag == "Brick")
        {

            //TODO implement recycle/object pooling
            Destroy(other.gameObject);
        }
    }
}
