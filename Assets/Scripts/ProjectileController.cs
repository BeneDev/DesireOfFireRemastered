using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    Vector3 direction;
    Rigidbody rb;
    [SerializeField] float speed;

    private void Awake()
    {
        if(transform.rotation == Quaternion.Euler(0, -90f, 0))
        {
            direction = new Vector3(-1f, 0, 0);
        }
        else if (transform.rotation == Quaternion.Euler(0, 90f, 0))
        {
            direction = new Vector3(1f, 0, 0);
        }
        else if (transform.rotation == Quaternion.Euler(0, -180f, 0))
        {
            direction = new Vector3(0, 0, -1f);
        }
        else if (transform.rotation == Quaternion.Euler(0, 360f, 0))
        {
            direction = new Vector3(0, 0, 1f);
        }
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
