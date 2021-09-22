using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D collider;
    public LineRenderer trajectory;
    private Vector2 startPos;
    private Bird bird;

    [SerializeField] private float radius = 0.75f;
    [SerializeField] private float throwSpeed = 30f;

    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseUp()
    {
        collider.enabled = false;
        Vector2 velocity = startPos - (Vector2)transform.position;
        float distance = Vector2.Distance(startPos, transform.position);
        bird.Shoot(velocity, distance, throwSpeed);
        gameObject.transform.position = startPos;
        trajectory.enabled = false;
    }

    public void InitiateBird(Bird _bird)
    {
        bird = _bird;
        bird.MoveTo(gameObject.transform.position, gameObject);
        collider.enabled = true;
    }

    void OnMouseDrag()
    {
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = p - startPos;
        if(dir.sqrMagnitude > radius)
        {
            dir = dir.normalized * radius;
        }
        transform.position = startPos + dir;
        float distance = Vector2.Distance(startPos, transform.position);
        if(!trajectory.enabled)
        {
            trajectory.enabled = true;
        }
        DisplayTrajectory(distance);
    }

    void DisplayTrajectory(float distance)
    {
        if(bird == null)
        {
            return;
        }
        Vector2 velocity = startPos - (Vector2)transform.position;
        int segmentCount = 5;
        Vector2[] segments = new Vector2[segmentCount];
        segments[0] = transform.position;
        Vector2 segmentVel = velocity * throwSpeed * distance;
        for(int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segmentVel * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }
        trajectory.positionCount = segmentCount;
        for(int i = 0; i < segmentCount; i++)
        {
            trajectory.SetPosition(i, segments[i]);
        }
    }
}
