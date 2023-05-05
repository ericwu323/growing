using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public Transform[] points;
    public float waitTime = 3;
    public Animator anim;
    private float dist, patroldist;
    public float moveSpeed = 1;
    public float howclose = 2;
    public float leashRange = 4;
    public float closeEnoughRange = .5f;
    int current;
    bool stop;
    public CharacterController controller;
    public GameObject ridePoint;
    public GameObject stepBox;
    public Transform leftStepBox, rightStepBox;
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
    
}
