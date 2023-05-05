using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class whale : MonoBehaviour
{
    public event System.Action<float, float> OnHealthChanged;
    public event System.Action<int> questChange;
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
    public GameObject waterAttack;
    public GameObject waterAttack2;
    public Transform attackspot;
    private Transform cam;
    public TMP_Text signText;
    bool started = false;
    bool done = false;
    int count;
    public AudioSource cartoonTalk;
    public sign carlosText;
    public AudioSource hitSound;
    public GameObject reward1;
    public GameObject reward2;
    public GameObject reward3;
    public GameObject reward4;
    public GameObject reward5;
    // Start is called before the first frame update
    private Vector3 vect;
    void Start()
    {
        deady = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>();
        current = 0;
        count = 1;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        reward1.SetActive(false);
        reward2.SetActive(false);
        reward3.SetActive(false);
        reward4.SetActive(false);
        reward5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(started == true && done == false)
        {
            done = true;
            StartCoroutine("Sign");
        }
        if (!deady)
        {
            dist = Vector3.Distance(player.position, transform.position);
            patroldist = Vector3.Distance(points[current].position, transform.position);
            if (dist <= howclose)
            {
                transform.LookAt(player);
                anim.ResetTrigger("run");
                anim.SetTrigger("attack");
                count++;
                started = true;
            }
            else
            {
                if (patroldist > closeEnoughRange && !stop)
                {
                    transform.LookAt(points[current]);
                    anim.SetTrigger("run");
                    Vector3 move = (points[current].position - transform.position).normalized;
                    controller.Move(move * moveSpeed * Time.deltaTime);


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
    IEnumerator Sign()
    {
        signText.enabled = true;
        signText.text = "Orca King Oliver: You dare challenge me?";
        cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.text = "Prepare to die!";
        cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.enabled = false;
    }
    IEnumerator Sign2()
    {
        signText.enabled = true;
        signText.text = "Oliver: Impossible...";
        //cartoonTalk.Play();
        yield return new WaitForSeconds(4f);
        signText.enabled = false;
        Destroy(this.gameObject);
    }
    public void TakeDamage(float amount)
    {
        hitSound.Play();
        health -= amount;
        OnHealthChanged(max, health);
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        p.Add(6);
        deady = true;
        anim.ResetTrigger("run");
        anim.ResetTrigger("attack");
        anim.SetTrigger("die");
    }
    private void Destroy()
    {
        
        StartCoroutine("wait");
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(.75f);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine("Sign2");
        gameHandler.questNum = 5;
        questChange(5);
        carlosText.text = "That was amazing! I knew you had what it takes to defeat Oliver.";
        carlosText.text2 = "You've earned the gratitude and support of us sea-folk forever!";
        carlosText.text3 = "As promised, here is your reward";
        carlosText.text4 = "Take care!";
        reward1.SetActive(true);
        reward2.SetActive(true);
        reward3.SetActive(true);
        reward4.SetActive(true);
        reward5.SetActive(true);

    }
    void launch()
    {
        if (count % 3 != 0)
        {
            Instantiate(waterAttack, attackspot.position, Quaternion.identity).transform.LookAt(cam);
           
        }
        else
        {
            Instantiate(waterAttack2, attackspot.position, Quaternion.identity).transform.LookAt(cam);

        }

    }
    
}
