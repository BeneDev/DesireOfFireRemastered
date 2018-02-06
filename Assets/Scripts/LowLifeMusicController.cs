using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Scripts soley task is to play the heartbeat sound when the player is on low health
/// </summary>

public class LowLifeMusicController : MonoBehaviour {

    GameObject player;
    AudioSource aS;

	void Start () {
        aS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Plays the heartbeat sound if player health is below 30 and stops if above
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
