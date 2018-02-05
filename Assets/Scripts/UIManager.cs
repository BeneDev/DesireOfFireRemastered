using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Script, managing the UI for gameplay, including health, exp and level
/// </summary>

public class UIManager : MonoBehaviour {

    GameObject player;
    [SerializeField] Text uiHealth;
    [SerializeField] Text uiLevel;
    [SerializeField] Text uiExp;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // updates the text fields with the right values
        uiHealth.text = player.GetComponent<PlayerController>().health.ToString();
        uiLevel.text = player.GetComponent<PlayerController>().level.ToString();
        uiExp.text = player.GetComponent<PlayerController>().exp.ToString() + " / " + player.GetComponent<PlayerController>().expToNextLevel.ToString();
    }
}
