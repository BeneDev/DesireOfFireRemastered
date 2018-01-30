using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

    GameObject player;
    NavMeshAgent nav;
    float health = 100;
    [SerializeField] int expToGive;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        nav.destination = player.transform.position;
        HandleDying();
    }

    private void HandleDying()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        player.GetComponent<PlayerController>().GainExp(expToGive);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            TakeDamage(player.GetComponent<PlayerController>().damage);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
