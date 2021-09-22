using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public GameObject Parent;
    public Rigidbody2D RgBd;
    public CircleCollider2D collider;
    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };
    public BirdState State { get { return state; } }

    private BirdState state;
    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    void OnDestroy()
    {
        if(state == BirdState.Thrown || state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        state = BirdState.HitSomething;
    }

    void Start()
    {
        RgBd.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
        state = BirdState.Idle;
    }

    void FixedUpdate()
    {
        if(state == BirdState.Idle && RgBd.velocity.sqrMagnitude >= minVelocity)
        {
            state = BirdState.Thrown;
        }
        if(state == BirdState.Thrown || state == BirdState.HitSomething && RgBd.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        collider.enabled = true;
        RgBd.bodyType = RigidbodyType2D.Dynamic;
        RgBd.velocity = velocity * speed * distance;
        OnBirdShot(this);
    }

    public virtual void OnTap()
    {

    }
}
