using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ride : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    void Start()
    {
        target = null;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    void OnTriggerExit(Collider col)
    {
        target = null;
       
    }
    void LateUpdate()
    {
        if (target != null)
        {
            playerControl player = target.GetComponent<playerControl>();
            Debug.Log("RIDING");
            if (player.move.x == 0 && player.move.z == 0 && !Input.GetKeyDown("space"))
                target.transform.position = transform.position + offset;
        }
    }
}
