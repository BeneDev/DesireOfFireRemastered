using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Script, defining the Crawler enemy type, inheriting from EnemyAI
/// </summary>

public class CrawlerEnemy : EnemyAI
{
    void Awake()
    {
        aS = GetComponent<AudioSource>();
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
