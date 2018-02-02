﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Camera cam;

    Vector3 fwd;
    public int baseDamage = 10;
    [HideInInspector] public int damage;
    public int baseDefense = 0;
    [Range(0, 75)][HideInInspector] public int defense;
    [HideInInspector] public int health;
    [SerializeField] int maxHealth = 100;
    [HideInInspector] public int level = 1;
    [HideInInspector] public int exp = 0;
    [HideInInspector] public int expToNextLevel = 10;
    [SerializeField] GameObject projectilePrefab;
    private GameObject player;
    private int healthRegen = 10;
    float attackCounter = 1;
    
    [SerializeField] float speed = 1f;
    [Range(100, 1000)] [SerializeField] float knockbackMultiplier = 300f;

    [SerializeField] GameObject levelupPanel;
    LevelUpController lvlup;

    private Vector3 moveDirection;

    Rigidbody rb;

    enum State
    {
        attacking,
        walking,
        idling,
    }
    State playerState;

    private void Awake()
    {
        health = maxHealth;
        damage = baseDamage;
        defense = baseDefense;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        lvlup = levelupPanel.transform.parent.gameObject.GetComponent<LevelUpController>();
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    void TurnDirection(float p_h, float p_v)
    {
        if(Input.GetKey(KeyCode.D) && transform.rotation != Quaternion.Euler(0, 360f, 0))
        {
            transform.rotation = Quaternion.Euler(0, 360f, 0);
        }
        else if(Input.GetKey(KeyCode.A) && transform.rotation != Quaternion.Euler(0, -180f, 0))
        {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        if(Input.GetKey(KeyCode.W) && transform.rotation != Quaternion.Euler(0, -90f, 0))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        else if(Input.GetKey(KeyCode.S) && transform.rotation != Quaternion.Euler(0, 90f, 0))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
        }
    }

    private void LevelUp()
    {
        cam.GetComponent<CameraShake>().shakeDuration = 0;
        Time.timeScale = 0f;
        int overExp = exp - expToNextLevel;
        level++;
        exp = overExp;
        levelupPanel.SetActive(true);
        expToNextLevel = expToNextLevel ^ 2;
        if (health + healthRegen < maxHealth)
        {
            health += healthRegen;
        }

    }

    public void TakeDamage(int p_damage, Vector3 knockDir)
    {
        int substract = p_damage;
        if(defense < substract)
        {
            substract = p_damage - defense;
        }
        else
        {
            substract = 1;
        }
        rb.AddForce(knockDir.normalized*(substract*knockbackMultiplier));
        health -= substract;
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            fwd = transform.forward;
            Quaternion proRot = Quaternion.Euler(0, -90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, proRot);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            fwd = transform.forward;
            Quaternion proRot = Quaternion.Euler(0, 90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, proRot);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
            fwd = transform.forward;
            Quaternion proRot = Quaternion.Euler(0, -180f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, proRot);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 360f, 0);
            fwd = transform.forward;
            Quaternion proRot = Quaternion.Euler(0, 360f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, proRot);
        }
    }

    private void FixedUpdate()
    {
        fwd = transform.forward;
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Shooting();

        moveDirection = new Vector3(-v, 0, h);
        // pass all parameters to the character control script
        Move(moveDirection);
    }

    public void GainExp(int p_exp)
    {
        exp += p_exp;
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();

        rb.velocity = new Vector3(move.x * speed, 0, move.z * speed);
    }
}
