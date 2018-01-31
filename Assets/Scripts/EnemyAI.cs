using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {

    GameObject player;
    NavMeshAgent nav;
    [SerializeField] float health = 100;
    [SerializeField] int expToGive = 3;
    [SerializeField] int attack = 2;
    [SerializeField] int defense = 0;
    [SerializeField] float lookDistance = 5;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 distance = player.transform.position - transform.position;
        if (distance.magnitude <= lookDistance)
        {
            nav.destination = player.transform.position;
        }
        if (health <= 0)
        {
            HandleDying();
        }
    }

    private void HandleDying()
    {
        Destroy(gameObject);
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

    public virtual void Attack()
    {

    }
}
