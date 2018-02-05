using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The base Script from which all enemies inherit
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

    protected GameObject player;
    protected NavMeshAgent nav;
    protected int health = 100;
    protected int expToGive = 3;
    public int attack = 5;
    protected int level = 1;
    protected float lookDistance = 15;
    protected Vector3 distance;
    Camera cam;

    // Use this for initialization
    void Start () {
        // finds the needed components and saves them into variables
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        // calculates the attributes given the level of the enemy instance
        health *= level;
        expToGive *= level;
        attack *= level;
	}

    // destroys the enemy and gives the player exp
    protected void HandleDying()
    {
        Destroy(gameObject);
        player.GetComponent<PlayerController>().GainExp(expToGive);
    }

    // substracts a given parameter from the health value and shakes the camera
    protected void TakeDamage(int damage)
    {
        health -= damage;
        cam.GetComponent<CameraShake>().shakeDuration = 0.2f;
    }

    // checks if hit by a projectiles and, if so, takes damage and destroys the bullet
    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            TakeDamage(player.GetComponent<PlayerController>().damage);
            Destroy(other.transform.parent.gameObject);
        }
    }

    // the base method for attacking, only damaging the player and applying knockback
    public virtual void Attack()
    {
        Vector3 knockDir = player.transform.position - transform.position;
        player.GetComponent<PlayerController>().TakeDamage(attack, knockDir);
    }

    // the base method for behaving in general, walking to the player if he is close enough and attacking when getting onto him. Also dies if health falls down to 0 or below
    public virtual void Behavior()
    {
        distance = player.transform.position - transform.position;
        // sets the player as destination if he is close enough
        if (distance.magnitude <= lookDistance)
        {
            nav.destination = player.transform.position;
        }
        // dies if health is 0 or below
        if (health <= 0)
        {
            HandleDying();
        }
        // attacks player since he is close enough
        if (distance.magnitude <= 1.5)
        {
            Attack();
        }
    }
}
