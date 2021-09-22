using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private bool isHit = false;

    void OnDestroy()
    {
        if(isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }
        if(collision.gameObject.tag == "Bird")
        {
            isHit = true;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;
            if(Health <= 0)
            {
                isHit = true;
                Destroy(gameObject);
            }
        }
    }
}
