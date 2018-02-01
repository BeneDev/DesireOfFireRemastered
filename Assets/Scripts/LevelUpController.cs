using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour {

    public bool chooseAttack;
    [SerializeField] GameObject panel;
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

    public void Attack()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        player.damage += 3;
    } 

    public void Defense()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        player.defense += 3;
    }



}
