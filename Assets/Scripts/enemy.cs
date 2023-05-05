using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public event System.Action<float, float> OnHealthChanged;
    public float max;
    public float health;
    public Transform[] points;
    private Transform player;
    private playerControl p;
    public Animator anim;
    private float dist, patroldist;
    public float moveSpeed = 1;
    public float howclose = 2;
    public float leashRange = 4;
    public float closeEnoughRange = .1f;
    int current;
    bool stop, deady;
    public CharacterController controller;
    public Transform spawnpoint;
    private Vector3 playerGround;
    public GameObject hitbox;
    public Transform attackspot;
    public AudioSource hitSound;
    // Start is called before the first frame update
    private Vector3 vect;
    public int courage = 4;
    void Start()
    {
        deady = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerGround = player.position;
        playerGround.y = transform.position.y;
        if (!deady)
        {
            dist = Vector3.Distance(player.position, transform.position);
            patroldist = Vector3.Distance(points[current].position, transform.position);
            if (dist <= howclose)
            {
                transform.LookAt(player);
                anim.ResetTrigger("run");
                anim.SetTrigger("attack");

            }
            else if (dist <= leashRange)
            {
                transform.LookAt(player);
                anim.SetTrigger("run");
                Vector3 move = (player.position - transform.position).normalized;
                controller.SimpleMove(move * moveSpeed);
                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
          
        
            }
            else
            {
                if (patroldist > closeEnoughRange && !stop)
                {
                    transform.LookAt(points[current]);
                    anim.SetTrigger("run");
                    Vector3 move = (points[current].position - transform.position).normalized;
                    controller.SimpleMove(move * moveSpeed);
                    //transform.position = Vector3.MoveTowards(transform.position, points[current].position, moveSpeed * Time.deltaTime);

                }
                else if(!stop)
                {

                    anim.ResetTrigger("run");
                    StartCoroutine("Stop");
                    current = (current + 1) % (points.Length);

                }
            }
        }
    }
    IEnumerator Stop()
    {
        stop = true;
        //anim.SetTrigger("Look Around");
        yield return new WaitForSeconds(3.0f);
        //anim.ResetTrigger("Look Around");
        stop = false;
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        
        OnHealthChanged(max, health);
        
        hitSound.Play();
        if(health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        p.Add(courage / 2);
        deady = true;
        anim.ResetTrigger("run");
        anim.ResetTrigger("attack");
        anim.SetTrigger("die");
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    void damage()
    {
        GameObject hi = Instantiate(hitbox, attackspot);
        Destroy(hi, 1f);
    }
    public void cower(int lvl)
    {
        if (lvl > courage)
        {
            leashRange = 0;
            howclose = 0;
        }
    }
    void snakeSignal()
    {
        gameHandler.snakeSig++;
    }
}
