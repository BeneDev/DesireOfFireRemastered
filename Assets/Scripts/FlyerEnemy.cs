using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerEnemy : EnemyAI
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

    // Use this for initialization
    void Awake()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Behavior();
    }

    public override void Attack()
    {
        // TODO make projectiles fly towards the player in an angle
    }

    public override void Behavior()
    {
        distance = player.transform.position - transform.position;
        if (distance.magnitude <= lookDistance)
        {
            nav.destination = player.transform.position;
        }
        if (distance.magnitude <= lookDistance / 2)
        {
            //Attack();
            nav.destination = transform.position;
        }
        if (distance.magnitude <= lookDistance / 4)
        {
            nav.destination = transform.position + (-distance.normalized) * 4;
        }
        if (health <= 0)
        {
            HandleDying();
        }
    }
}