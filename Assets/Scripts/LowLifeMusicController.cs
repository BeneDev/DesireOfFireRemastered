using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowLifeMusicController : MonoBehaviour {

    GameObject player;
    AudioSource aS;

	// Use this for initialization
	void Start () {
        aS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<PlayerController>().health <= 30 && !aS.isPlaying)
        {
            aS.Play();
        }
        else if(player.GetComponent<PlayerController>().health > 30 && aS.isPlaying)
        {
            aS.Stop();
        }
	}
}
