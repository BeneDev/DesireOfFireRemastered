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
    [SerializeField] float cooldownTime = 1f;
    [SerializeField] GameObject eProjectile;
    private bool shootable = true;

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
        if (shootable == true)
        {
            AttackPattern(0);
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        shootable = true;
    }

    void AttackPattern(float angle)
    {
        GameObject projectile = Instantiate(eProjectile, transform.position + transform.forward.normalized, transform.rotation);
        projectile.GetComponent<ProjectileController>().ofEnemy = gameObject;
        /*
        projectile = Instantiate(eProjectile, transform.position + transform.forward.normalized, Quaternion.Euler(transform.rotation.x, transform.rotation.y + angle, transform.rotation.z));
        projectile.GetComponent<ProjectileController>().ofEnemy = gameObject;
        projectile = Instantiate(eProjectile, transform.position + transform.forward.normalized, Quaternion.Euler(transform.rotation.x, transform.rotation.y - angle, transform.rotation.z));
        projectile.GetComponent<ProjectileController>().ofEnemy = gameObject;
        */
        shootable = false;
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
            Attack();
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