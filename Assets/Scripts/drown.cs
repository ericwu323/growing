using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drown : MonoBehaviour
{
    public playerControl player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("wait");
        player.Die();
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
