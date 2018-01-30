using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 fwd;
    private int baseDamage = 10;
    public int damage;
    public int baseDefense = 5;
    public int defense;
    private int health;
    private int maxHealth = 100;
    private int level = 1;
    private int exp = 0;
    private int expToNextLevel = 10;
    [SerializeField] GameObject projectilePrefab;
    private GameObject player;
    
    [SerializeField] float speed = 1f;
    
    private Vector3 moveDirection;

    Rigidbody rb;
    float turnAmount;
    float forwardAmount;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(exp >= expToNextLevel)
        {
            int overExp = exp - expToNextLevel;
            level++;
            exp = overExp;
        }
        //damage = baseDamage * level;
        //defense = baseDefense * level;
    }

    private void LateUpdate()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 360f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
        }
    }

    private void FixedUpdate()
    {
        fwd = transform.forward;

        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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
