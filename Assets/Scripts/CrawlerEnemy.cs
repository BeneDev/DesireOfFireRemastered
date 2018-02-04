using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerEnemy : EnemyAI
{
    [SerializeField] int desLevel = 1;
    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] int desDefense = 0;
    [SerializeField] float desLookDistance = 15;

    // Use this for initialization
    public void Reset()
    {
        level = desLevel;
        health = desHealth;
        expToGive = desExpToGive;
        attack = desAttack;
        lookDistance = desLookDistance;
	}

    void Awake()
    {
        Reset();
    }

	// Update is called once per frame
	void Update ()
    {
        Behavior();
    }

    public override void Attack()
    {
        base.Attack();
        HandleDying();
    }

}
