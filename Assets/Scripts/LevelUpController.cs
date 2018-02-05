using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Script, controlling the panel which shows up when the player leveled up
/// </summary>

public class LevelUpController : MonoBehaviour {

    public bool chooseAttack; //a boolean if the player wants to upgrade attack or defense
    [SerializeField] GameObject panel; //the panel for the level up overlay
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Attack(); 
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            Defense();
        }
    }

    //upgrade the attack attribute of the player
    public void Attack()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        player.damage += 3;
    }

    //upgrade the defense attribute of the player
    public void Defense()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        player.defense += 3;
    }



}
