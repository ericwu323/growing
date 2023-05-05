using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterAttack : MonoBehaviour
{
    public float speed = 10;
    public float damage = 3;
    public bool turned = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("die");
    }

    // Update is called once per frame
    void Update()
    {
        if (turned == false)
            transform.position += transform.forward * Time.deltaTime * speed;
        else
        {
            transform.position -= transform.forward * Time.deltaTime * speed * 2;
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerControl ply = other.GetComponent<playerControl>();
            ply.takeDamage(1);
            Destroy(this.gameObject);
        }
        if(other.tag == "swordAttack")
        {
            turned = true;
        }
        if(other.tag == "whale")
        {
            whale whale = other.GetComponent<whale>();
            whale.TakeDamage(damage);
            Destroy(this.gameObject);

        }
      
        
    }
}
