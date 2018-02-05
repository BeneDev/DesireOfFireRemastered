using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    Vector3 direction;
    Rigidbody rb;
    [SerializeField] float speed;
    // stores the enemy which instantiated the instance of projectile
    public GameObject ofEnemy;

    private void Awake()
    {
        // gets a vector in the direction the projectile is facing
        direction = transform.forward.normalized;
    }
    
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        // sets the direction of travel and deletes after some period of time
        rb.velocity = direction.normalized * speed;
        StartCoroutine(DeleteAfterTime());
	}

    // deletes the projectile after 2 seconds
    IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // deletes the bullet and damages the player if the bullet was instantiated from an enemy
    private void OnTriggerEnter(Collider other)
    {
        if (ofEnemy == null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(gameObject);
            }
            if (other.gameObject.tag == "Environment")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // damages the player
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerController>().TakeDamage(ofEnemy.GetComponent<EnemyAI>().attack, other.transform.position - ofEnemy.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
