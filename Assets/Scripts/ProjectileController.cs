using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    Vector3 direction;
    Rigidbody rb;
    [SerializeField] float speed;
    public GameObject ofEnemy;

    private void Awake()
    {
        direction = transform.forward.normalized;
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = direction.normalized * speed;
        StartCoroutine(DeleteAfterTime());
	}

    IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

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
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerController>().TakeDamage(ofEnemy.GetComponent<EnemyAI>().attack, other.transform.position - ofEnemy.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
