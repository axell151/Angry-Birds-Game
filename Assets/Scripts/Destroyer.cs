using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if(tag == "Bird" || tag == "Enemy" || tag == "Obstacle")
        {
            Destroy(collider.gameObject);
        }
    }
}
