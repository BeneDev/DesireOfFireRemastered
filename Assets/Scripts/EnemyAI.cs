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
    protected int defense = 0;
    protected float lookDistance = 15;
    protected Vector3 distance;

    // Use this for initialization
    protected void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
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
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            TakeDamage(player.GetComponent<PlayerController>().damage);
            Destroy(other.transform.parent.gameObject);
        }
    }

    public virtual void Attack()
    {   
        player.GetComponent<PlayerController>().TakeDamage(attack);
    }
}
