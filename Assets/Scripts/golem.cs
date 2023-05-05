using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class golem : MonoBehaviour
{
    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<int> questChange;
    private Transform player;
    public Transform attackspot;
    public Transform[] fireworkSpots;
    public Transform[] points;
    public float waitTime = 3;
    public Animator anim;
    private float dist, patroldist;
    public float moveSpeed = 1;
    public float howclose = 2;
    public float health = 300;
    public float max = 300;
    public float leashRange = 4;
    public float closeEnoughRange = .5f;
    int current, fireworkCurrent;
    bool stop, deady;
    public CharacterController controller;
    public GameObject stepBox;
    public Transform leftStepBox, rightStepBox;
    public AudioSource hitSound;
    public GameObject hitbox;
    public GameObject bossui;
    public GameObject fireworks;
    public bool attackmode;
    Vector3 vect = new Vector3(0, 7, 0);
    // Start is called before the first frame update
    void Start()
    {
        attackmode = false;
        current = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fireworkCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {



        patroldist = Vector3.Distance(points[current].position, transform.position);
        dist = Vector3.Distance(player.position, transform.position);

        if (!deady)
        {
            if (attackmode == false)
            {
                if (patroldist > closeEnoughRange && !stop)
                {
                    transform.LookAt(points[current]);
                    anim.SetTrigger("run");
                    Vector3 move = (points[current].position - transform.position).normalized;
                    controller.SimpleMove(move * moveSpeed);


                }
                else if (!stop)
                {

                    anim.ResetTrigger("run");
                    StartCoroutine("Stop");
                    current = (current + 1);
                    if (current == points.Length)
                        current = 0;

                }
            }
            else
            {
                if (dist < howclose)
                {
                    transform.LookAt(player);
                    anim.ResetTrigger("run");
                    anim.SetTrigger("Punch");
                }
                else if (dist < leashRange)
                {
                    transform.LookAt(player);
                    anim.SetTrigger("run");
                    Vector3 move = (player.position - transform.position).normalized;
                    controller.SimpleMove(move * moveSpeed);
                }
            }

        }
        if (deady)
        {
            GameObject hi = Instantiate(fireworks, fireworkSpots[fireworkCurrent].position + vect + Random.insideUnitSphere*30, Quaternion.identity);
            Destroy(hi, 2f);
            fireworkCurrent++;
            if(fireworkCurrent >= fireworkSpots.Length-1)
            {
                fireworkCurrent = 0;
            }
        }

    }
    IEnumerator Stop()
    {
        //anim.SetTrigger("look");
        stop = true;
        yield return new WaitForSeconds(waitTime);
        stop = false;

    }
    void leftStep()
    {
        GameObject hi = Instantiate(stepBox, leftStepBox);
        Destroy(hi, 2f);
    }
    void rightStep()
    {
        GameObject hi = Instantiate(stepBox, rightStepBox);
        Destroy(hi, 2f);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        OnHealthChanged(max, health);

        hitSound.Play();
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        deady = true;
        anim.ResetTrigger("run");
        anim.SetTrigger("Die");
    }
    private void Destroy()
    {
        anim.enabled = false;
        Destroy(bossui);
        StartCoroutine("wait");

    }
    IEnumerator wait()
    {
        questChange(14);
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(4);
    }
    void damage()
    {
        GameObject hi = Instantiate(hitbox, attackspot);
        Destroy(hi, 1f);
    }
}
