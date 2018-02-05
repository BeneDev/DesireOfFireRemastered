using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Script, setting the camera arm to the transform of the player
/// </summary>

public class CameraController : MonoBehaviour {

    GameObject player;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        // sticks to the player's position
        transform.position = player.transform.position;
	}
}
