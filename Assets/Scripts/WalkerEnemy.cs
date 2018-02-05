using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Script, defining the walker enemy type, inheriting from the EnemyUI class
/// </summary>

public class WalkerEnemy : EnemyAI
{
    // Use this for initialization
    void Awake ()
    {
        aS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Behavior();
    }
}
