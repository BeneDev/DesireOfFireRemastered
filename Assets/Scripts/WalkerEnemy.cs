using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemy : EnemyAI
{
    // inspector fields for the designer to manipulate the enemies attributes
    [SerializeField] int desLevel = 1;
    [SerializeField] int desHealth = 100;
    [SerializeField] int desExpToGive = 3;
    [SerializeField] int desAttack = 5;
    [SerializeField] int desDefense = 0;
    [SerializeField] float desLookDistance = 15;

    // Sets the, in the EnemyAI declared, attributes to the ones chosen here from the designer
    public void Reset()
    {
        level = desLevel;
        health = desHealth;
        expToGive = desExpToGive;
        attack = desAttack;
        lookDistance = desLookDistance;
    }

    // Use this for initialization
    void Awake ()
    {
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Behavior();
    }
}
