using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController TrailController;
    public List<Bird> birds;
    public List<Enemy> enemies;
    public BoxCollider2D tapCollider;
    private Bird shotBird;


    private bool isGameEnded = false;

    void Start()
    {
        for(int i=0; i<birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
            birds[i].OnBirdShot += AssignTrail;
        }
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        tapCollider.enabled = false;
        slingShooter.InitiateBird(birds[0]);
        shotBird = birds[0];
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;
        if(isGameEnded)
        {
            return;
        }
        birds.RemoveAt(0);
        if(birds.Count > 0)
        {
            slingShooter.InitiateBird(birds[0]);
            shotBird = birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }
        if(enemies.Count == 0)
        {
            isGameEnded = true;
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if(shotBird != null)
        {
            shotBird.OnTap();
        }
    }
}
