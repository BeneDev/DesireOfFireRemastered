using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Script, defining the Crawler enemy type, inheriting from EnemyAI
/// </summary>

public class CrawlerEnemy : EnemyAI
{
    // give the designer the choice to manipulate attributes
    [SerializeField] int desLevel = 1;
    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] float desLookDistance = 15;

    AudioSource aS;
    [SerializeField] AudioClip[] audioClip;

    // Overwrites the attributes given of the parent class with the designer choices
    public void Reset()
    {
        level = desLevel;
        health = desHealth;
        expToGive = desExpToGive;
        attack = desAttack;
        lookDistance = desLookDistance;
        
	}

    void PlaySound(int clip)
    {
        aS.clip = audioClip[clip];
        aS.Play();
    }

    void Awake()
    {
        Reset();
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

    public override void TakeDamage(int damage)
    {
        PlaySound(0);
        base.TakeDamage(damage);
    }

    
    public override void HandleDying()
    {
        PlaySound(1);
        base.HandleDying();
    }
}
