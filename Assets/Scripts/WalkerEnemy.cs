using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemy : EnemyAI {


    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] int desDefense = 0;
    [SerializeField] float desLookDistance = 15;

    // Use this for initialization
    public void Reset()
    {
        health = desHealth;
        expToGive = desExpToGive;
        attack = desAttack;
        defense = desDefense;
        lookDistance = desLookDistance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
