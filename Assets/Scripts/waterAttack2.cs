using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterAttack2 : MonoBehaviour
{
    public float speed = 10;
    public float damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("die");
    }

    // Update is called once per frame
    void Update()
    {
    
         transform.position += transform.forward * Time.deltaTime * speed;
       
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerControl ply = other.GetComponent<playerControl>();
            ply.takeDamage(1);
            Destroy(this.gameObject);

        }
        

    }
}
