using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    Vector3 direction;
    Rigidbody rb;
    [SerializeField] float speed;
    
    public void SetDirection(Vector3 p_direction)
    {
        direction = p_direction;
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
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject);
        }
    }
}
