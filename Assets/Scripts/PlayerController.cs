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
    private int healthRegen = 10;
    float attackCounter = 1;
    
    [SerializeField] float speed = 1f;
    
    private Vector3 moveDirection;

    Rigidbody rb;
    float turnAmount;
    float forwardAmount;

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
        playerState = State.idling;
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
        int overExp = exp - expToNextLevel;
        level++;
        exp = overExp;
        // TODO make ui for choosing which one you want to upgrade
        damage = baseDamage + level*3;
        defense = baseDefense + level*3;
        expToNextLevel = expToNextLevel ^ 2;
        if (health + healthRegen < maxHealth)
        {
            health += healthRegen;
        }

    }

    public void TakeDamage(int p_damage)
    {
        health -= p_damage;
    }

    private void LateUpdate()
    {

    }

    IEnumerator CountDownAttackCounter()
    {
        yield return new WaitForSeconds(attackCounter);
        playerState = State.idling;
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
            playerState = State.attacking;
            StartCoroutine(CountDownAttackCounter());
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
            playerState = State.attacking;
            StartCoroutine(CountDownAttackCounter());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
            playerState = State.attacking;
            StartCoroutine(CountDownAttackCounter());
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 360f, 0);
            GameObject projectile = Instantiate(projectilePrefab, player.transform.position + fwd.normalized, player.transform.rotation);
            projectile.GetComponent<ProjectileController>().SetDirection(fwd);
            playerState = State.attacking;
            StartCoroutine(CountDownAttackCounter());
        }
    }

    private void FixedUpdate()
    {
        fwd = transform.forward;
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            playerState = State.idling;
        }
        if(playerState == State.idling)
        {
            if(h != 0 || v != 0)
            {
                playerState = State.walking;
            }
        }

        if (playerState == State.walking)
        {
            TurnDirection(h, v);
        }

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
