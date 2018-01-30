using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 fwd;

    [SerializeField] GameObject projectilePrefab;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
        }
	}

    private void FixedUpdate()
    {
        fwd = transform.forward;
    }
}
