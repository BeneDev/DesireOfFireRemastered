using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

    protected GameObject player;
    protected NavMeshAgent nav;
    protected int health = 100;
    protected int expToGive = 3;
    protected int attack = 5;
    protected float lookDistance = 15;
    protected Vector3 distance;
    Camera cam;

    // Use this for initialization
    protected void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    protected void HandleDying()
    {
        Destroy(gameObject);
        player.GetComponent<PlayerController>().GainExp(expToGive);
    }

    protected void TakeDamage(int damage)
    {
        health -= damage;
        cam.GetComponent<CameraShake>().shakeDuration = 0.2f;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            print("destroy");
            TakeDamage(player.GetComponent<PlayerController>().damage);
            Destroy(other.transform.parent.gameObject);
        }
    }

    public virtual void Attack()
    {
        Vector3 knockDir = player.transform.position - transform.position;
        player.GetComponent<PlayerController>().TakeDamage(attack, knockDir);
    }

    public virtual void Behavior()
    {
        distance = player.transform.position - transform.position;
        if (distance.magnitude <= lookDistance)
        {
            nav.destination = player.transform.position;
        }
        if (health <= 0)
        {
            HandleDying();
        }
        if (distance.magnitude <= 1.5)
        {
            Attack();
        }
    }
}
