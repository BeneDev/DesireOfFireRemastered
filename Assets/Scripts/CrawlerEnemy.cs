using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerEnemy : EnemyAI {

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
        if (distance.magnitude <= 5)
        {
            Attack();
        }
    }
}
