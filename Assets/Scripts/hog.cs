using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class hog : MonoBehaviour
{
    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<int> questChange;
    public GameObject snakeSpawn;
    public Collider hitbox;
    Transform target;
    public float max, agroDist;
    public float health;
    public float furthestDist = 0;
    public Transform[] points;
    private Transform furthestPoint;
    public playerControl player;
    public float waitTime = 3;
    public Animator anim;
    private float dist, patroldist, attackdist;
    public float moveSpeed = 1;
    public float howclose = 2;
    public float leashRange = 4;
    public float closeEnoughRange = .5f;
    int charge;
    bool stop, deady, chargeAttack;
    public CharacterController controller;
    bool done = false;
    Vector3 vect;
    public TMP_Text signText;
    public AudioSource hitSound;
    public AudioSource cartoonTalk;
    // Start is called before the first frame update
    void Start()
    {

        chargeAttack = false;
        deady = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>(); ;
        furthestPoint = points[0];
        target = player.transform;
        vect = target.position;
       // hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        patroldist = Vector3.Distance(furthestPoint.position, transform.position);
        attackdist = Vector3.Distance(vect, transform.position);
        agroDist = Vector3.Distance(player.transform.position, transform.position);
        if (agroDist < leashRange)
        {
            if (done == false)
            {
                done = true;
                StartCoroutine("Sign");
            }
            if (!deady)
            {
                if (chargeAttack == true)
                {
                    if (attackdist > closeEnoughRange)
                    {
                        transform.LookAt(target);
                        anim.SetTrigger("roll");
                        Vector3 move = (vect - transform.position).normalized;
                        controller.SimpleMove(move * moveSpeed * 1.5f);
                    }
                    else
                    {
                        //hitbox.enabled = false;
                        chargeAttack = false;
                    }
                }
                else if (patroldist > closeEnoughRange && !stop)
                {
                    transform.LookAt(furthestPoint);
                    anim.SetTrigger("roll");
                    Vector3 move = (furthestPoint.position - transform.position).normalized;
                    controller.SimpleMove(move * moveSpeed);


                }
                else if (!stop)
                {
                    transform.LookAt(player.transform);
                    furthestDist = 0;
                    StartCoroutine("Stop");


                }
            }
        }
    }
    IEnumerator Sign()
    {
        signText.enabled = true;
        signText.text = "Reg: How dare you trespass on my domain!";
        cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.text = "I will crush you!";
        cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.enabled = false;
    }
    IEnumerator Sign2()
    {
        signText.enabled = true;
        signText.text = "Reg: You will pay for this...";
        //cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.enabled = false;
        Destroy(gameObject);
    }
    IEnumerator Stop()
    {
        charge++;
        if(charge % 2 == 0)
        {
            chargeAttack = true;
            target = player.transform;
            vect = target.position;
            hitbox.enabled = true;
        }
        stop = true;
        yield return new WaitForSeconds(waitTime);
        stop = false;
        int i = 0;
        foreach (Transform p in points)
        {
            
            dist = Vector3.Distance(p.position, player.transform.position);
            if (dist >= furthestDist)
            {
                furthestDist = dist;
                furthestPoint = p;
            }
            i++;
        }
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        hitSound.Play();
        OnHealthChanged(max, health);
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine("Sign2");
        deady = true;
        anim.ResetTrigger("roll");
        anim.SetTrigger("die");
        gameHandler.questNum++;
        questChange(12);
        player.Add(30);
    }
    private void Destroy()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerControl hi = other.GetComponent<playerControl>();
            hi.takeDamage(3);
            //hitbox.enabled = false;
        }
    }

}
