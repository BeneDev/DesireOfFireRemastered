using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>The class defining the Flyer enemy Type, inheriting from the EnemyAI class</summary>

public class FlyerEnemy : EnemyAI
{
    // gives the designer the coice to set attributes of the enemy
    [SerializeField] int desLevel = 1;
    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] float desLookDistance = 15;
    [SerializeField] float cooldownTime = 1f;
    [SerializeField] GameObject eProjectile;
    private bool shootable = true;
    
    // overwrites the attributes given by the parent class
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
    
    void Update()
    {
        Behavior();
    }

    // overwrites the attack method of the parent class
    public override void Attack()
    {
        if (shootable == true)
        {
            AttackPattern(0);
            StartCoroutine(Cooldown());
        }
    }

    // counts down the cooldown time and enables the enemy to shoot again after that time
    IEnumerator Cooldown()
    {
        print("actually cooling down");
        yield return new WaitForSeconds(cooldownTime);
        shootable = true;
    }

    // actually instantiates the projectiles getting shot
    void AttackPattern(float angle)
    {
        GameObject projectile = Instantiate(eProjectile, transform.position + transform.forward.normalized, transform.rotation);
        projectile.GetComponent<ProjectileController>().ofEnemy = gameObject;
        shootable = false;
    }

    // overwrites the behavior method of the parent class
    public override void Behavior()
    {
        // walks towards the player if close enough
        distance = player.transform.position - transform.position;
        if (distance.magnitude <= lookDistance)
        {
            nav.destination = player.transform.position;
        }
        // attacks if player close enough for that
        if (distance.magnitude <= lookDistance / 2)
        {
            nav.destination = transform.position;
            Attack();
        }
        // walks away if player is too close
        if (distance.magnitude <= lookDistance / 4)
        {
            nav.destination = transform.position + (-distance.normalized) * 4;
        }
        // dies if health is 0 or below
        if (health <= 0)
        {
            HandleDying();
        }
    }
}