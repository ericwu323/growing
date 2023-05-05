using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    public Transform[] points;
    public float waitTime = 3;
    public Animator anim;
    private float  patroldist;
    public float moveSpeed = 1;
    public float howclose = 2;
    public float leashRange = 4;
    public float closeEnoughRange = .5f;
    int current;
    bool stop;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {

        current = 0;

    }

    // Update is called once per frame
    void Update()
    {



        patroldist = Vector3.Distance(points[current].position, transform.position);




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
    IEnumerator Stop()
    {
        //anim.SetTrigger("look");
        stop = true;
        yield return new WaitForSeconds(waitTime);
        stop = false;

    }
}
