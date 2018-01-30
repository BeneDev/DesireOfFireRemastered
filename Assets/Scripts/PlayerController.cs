using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 fwd;

    public GameObject projectilePrefab;
    public GameObject player;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E){
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        }
	}

    private void FixedUpdate()
    {
        fwd = transform.forward;
    }
}
