using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerEnemy : EnemyAI
{
    // give the designer the choice to manipulate attributes
    [SerializeField] int desLevel = 1;
    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] int desDefense = 0;
    [SerializeField] float desLookDistance = 15;

    // Overwrites the attributes given of the parent class with the designer choices
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

	void Update ()
    {
        Behavior();
    }

    // makes the crawler attack, but also die after attacking
    public override void Attack()
    {
        base.Attack();
        HandleDying();
    }

}
