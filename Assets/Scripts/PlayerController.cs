using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Script, controlling the player, reading input, managing attributes, shooting, leveling up, taking damage, gaining exp and dying
/// </summary>

public class PlayerController : MonoBehaviour {

    Camera cam;

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
    // the amount of health getting regenerated when leveling up
    [Range(1, 100)] [SerializeField] int healthRegen = 10;
    // the speed of the player
    [SerializeField] float speed = 1f;
    // the amount of knockback applied to the player
    [Range(100, 1000)] [SerializeField] float knockbackMultiplier = 300f;

    //the panel which gets called when leveling up
    [SerializeField] GameObject levelupPanel;

    Vector3 moveDirection;
    Vector3 fwd;
    Rigidbody rb;
    AudioSource aS;

    [SerializeField] AudioClip[] audioClip;

    private void Awake()
    {
        // setting the explicit values to the base values
        health = maxHealth;
        damage = baseDamage;
        defense = baseDefense;
    }

    public void PlaySound(int clip)
    {
        aS.clip = audioClip[clip];
        aS.Play();
    }

    // Use this for initialization
    void Start () {
        // gets the right components loaded into the right variables
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        aS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // if the player has gained enough exp, he will level up
        if(exp >= expToNextLevel)
        {
            LevelUp();
        }
        if(health <= 0)
        {
            LoadGameOver();
        }
        if(health <= 30)
        {
            //PlaySound(3);
        }
    }

    void LoadGameOver()
    {
        health = 0;
        SceneManager.LoadScene(2);
    }

    // A method for turning to the direction of travel instead of the direction of shooting
    // TODO combine this with the direction of shooting, so the player uses this when not attacking and direction of attack when doing so
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
    

    // Manipulates the player's attributes regarding the level up
    private void LevelUp()
    {
        PlaySound(0);
        // stops the camera from shaking
        cam.GetComponent<CameraShake>().shakeDuration = 0;
        // stops the game from running while choosing an attribute to upgrade
        Time.timeScale = 0f;
        //calculating the exp overshooting the level cap
        int overExp = exp - expToNextLevel;
        level++;
        exp = overExp;
        // showing the level up overlay UI
        levelupPanel.SetActive(true);
        // exponentially increasing the exp needed for a level up
        expToNextLevel = (int)Mathf.Pow((float)expToNextLevel, 1.3f);
        //adding the health regeneration value to player health
        if (health + healthRegen < maxHealth)
        {
            health += healthRegen;
        }
        else
        {
            health = maxHealth;
        }

    }

    // a method allowing other objects to damage the player
    public void TakeDamage(int p_damage, Vector3 knockDir)
    {
        // a variable to store the damage taken
        int substract = p_damage;
        // substracts the defense if the result is over 0 still, otherwise the damage is 1
        PlaySound(1);
        if(defense < substract)
        {
            substract = p_damage - defense;
        }
        else
        {
            substract = 1;
        }
        // applies the knockback
        rb.AddForce(knockDir.normalized*(substract*knockbackMultiplier));
        // actually substracts the damage from the player's health
        health -= substract;
    }

    //Lets the player shoot
    private void Shooting()
    {
        // reads the input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // stores the right direction for the projectiles and player
            Quaternion proRot = Quaternion.Euler(0, -90f, 0);
            //rotates the player to the right direction 
            transform.rotation = proRot;
            // updates the forward vector
            fwd = transform.forward;
            // intantiates the projectile
            Instantiate(projectilePrefab, transform.position + fwd.normalized, proRot);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Quaternion proRot = Quaternion.Euler(0, 90f, 0);
            transform.rotation = proRot;
            fwd = transform.forward;
            Instantiate(projectilePrefab, transform.position + fwd.normalized, proRot);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Quaternion proRot = Quaternion.Euler(0, -180f, 0);
            transform.rotation = proRot;
            fwd = transform.forward;
            Instantiate(projectilePrefab, transform.position + fwd.normalized, proRot);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Quaternion proRot = Quaternion.Euler(0, 360f, 0);
            transform.rotation = proRot;
            fwd = transform.forward;
            Instantiate(projectilePrefab, transform.position + fwd.normalized, proRot);
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

    // enables other objects to give the player exp
    public void GainExp(int p_exp)
    {
        exp += p_exp;
    }

    // takes in i vector3 and moves the player along 
    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();

        rb.velocity = new Vector3(move.x * speed, 0, move.z * speed);
    }
}
