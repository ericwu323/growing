using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
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
        if(other.tag == "enemy")
        {
            enemy enemy = other.GetComponent<enemy>();
            if(enemy.health > 0)
                enemy.TakeDamage(playerControl.strength);
        }
        if (other.tag == "hog")
        {
            hog hog = other.GetComponent<hog>();
            if (hog.health > 0)
                hog.TakeDamage(playerControl.strength);
        }
        if (other.tag == "golem" && gameHandler.questNum == 13)
        {
            golem golem = other.GetComponent<golem>();
            if (golem.health > 0)
                golem.TakeDamage(playerControl.strength);
        }
        Destroy(this.gameObject);
    }
}
