using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playerControl : MonoBehaviour
{
    public GameObject levelUp;
    public GameObject eatPrefab;
    public CharacterController controller;
    private interacter interact;
    private attackListener attackListener;

    public static int level = 1;
    public static int strength = 1;
    public float speed = 2f;
    public float gravity = -3f;
    public Transform groundCheck;
    public float groundDistance = .4f;
    public float jumpHeight = .5f;
    public LayerMask groundMask;
    bool isGrounded;

    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<float, float> OnXpChanged;
    public event System.Action<int> questChange;
    public float health = 5f;
    public float max = 5f;
    public float xp = 0;
    public float maxXp = 5;
    public TMP_Text levelText;
    public AudioSource lvlUp;
    public AudioSource gethit;
    public AudioSource lose;

    Vector3 velocity;
    public Animator anim;

    public cam cam;
    public Transform respawnPoint;
    public Vector3 move;
    public Canvas ui;
    public Canvas deathScreen;
    public bool dead = false;

    List<GameObject> listofenemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        deathScreen.enabled = false;
        interact = gameObject.GetComponent<interacter>();
        attackListener = gameObject.GetComponent<attackListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            /*if (isGrounded && velocity.y < 0)
            {
                //velocity.y = -2f;
            }*/
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
            if (Input.GetButtonDown("Jump") && isGrounded && velocity.y <= 0)
            {
                velocity.y = jumpHeight;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("attack");

            }
            
            /*if (Input.GetKeyDown("p"))
            {
                grow();
            }*/
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if (Input.GetKeyDown("r"))
            {
                Die();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
       
    }
    public void Add(int num)
    {
        takeDamage(-1);
        xp += num;
        if(xp >= maxXp)
        {
            xp = xp - maxXp;
            grow();
            
        }
        GameObject hi = Instantiate(eatPrefab, groundCheck.transform);
        Destroy(hi, 2f);
        OnXpChanged(maxXp, xp);
    }
    public void grow()
    {
        lvlUp.Play();
        if (level == 1)
        {
            max += 1;
            gameObject.transform.localScale += new Vector3(.2f, .2f, .2f);
            jumpHeight += .4f;
            speed += .25f;
            maxXp += 3;
            gravity -= .5f;
            interact.playerActiveDistance += .2f;
            strength += 1;
            levelText.text = "Level 2";
        }
        if(level == 2)
        {
            if (gameHandler.questNum == 2)
            {
                questChange(3);
                gameHandler.questNum++;
            }
            max += 2;
            gameObject.transform.localScale += new Vector3(.4f, .4f, .4f);
            jumpHeight += .8f;
            speed += .8f;
            maxXp += 3;
            gravity -= 1f;
            interact.playerActiveDistance += .4f;
            strength += 1;
            levelText.text = "Level 3";
        }
        if (level == 3)
        {
            max += 3;
            gameObject.transform.localScale += new Vector3(.8f, .8f, .8f);
            jumpHeight += 1.5f;
            speed += 1.75f;
            maxXp += 5;
            gravity -= 3f;
            interact.playerActiveDistance += .8f;
            strength += 1;
            groundDistance += .2f;
            levelText.text = "Level 4";
        }
        if (level == 4)
        {
            max += 4;
            gameObject.transform.localScale += new Vector3(1.6f, 1.6f, 1.6f);
            jumpHeight += 2f;
            speed += 1.8f;
            maxXp += 5;
            gravity -= 3f;
            interact.playerActiveDistance += 2f;
            strength += 1;
            levelText.text = "Level 5";
            listofenemies.AddRange(GameObject.FindGameObjectsWithTag("enemy"));
            foreach(GameObject enem in listofenemies)
            {
                enem.GetComponent<enemy>().cower(5);
            }
        }
        if (level == 5)
        {
            max += 6;
            gameObject.transform.localScale += new Vector3(3.2f, 3.2f, 3.2f);
            jumpHeight += 3f;
            speed += 3f;
            maxXp += 5;
            gravity -= 3f;
            interact.playerActiveDistance += 3f;
            strength += 1;
            groundDistance += .1f;
            levelText.text = "Level 6";
            gameHandler.req++;
        }
        if (level == 6)
        {
            xp = maxXp;
            max += 6;
            gameObject.transform.localScale += new Vector3(6.4f, 6.4f, 6.4f);
            SetGameLayerRecursive(this.gameObject, 10);
            jumpHeight += 3f;
            speed += 6f;
            maxXp += 999999999;
            gravity -= 3f;
            interact.playerActiveDistance += 6f;
            strength += 1;
            groundDistance += .1f;
            levelText.text = "Max  ";
            gameHandler.req++;
        }

        GameObject hi = Instantiate(levelUp, groundCheck.transform);
        Destroy(hi, 1f);
        level += 1;
        health = max;
        OnHealthChanged(max, health);
        
       
    }
    private void SetGameLayerRecursive(GameObject _go, int _layer)
    {
        _go.layer = _layer;
        foreach (Transform child in _go.transform)
        {
            child.gameObject.layer = _layer;

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);

        }
    }
    public void takeDamage(float amount)
    {
        if(amount > 0)
            gethit.Play();
        health -= amount;
        if (health > max)
            health = max;
        if (OnHealthChanged != null)
            OnHealthChanged(max, health);

        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        this.transform.position = respawnPoint.position;
        lose.Play();
        dead = true;
        cam.dead = true;
        ui.enabled = false;
        deathScreen.enabled = true;
        health = max;
        OnHealthChanged(max, health);
        Cursor.lockState = CursorLockMode.Confined;
  
    }
    /*public void respawn()
    {
        this.transform.position = respawnPoint.position;
        dead = false;
    }*/
    
    
}
