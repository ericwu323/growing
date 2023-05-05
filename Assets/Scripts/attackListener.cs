using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackListener : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform hitboxLocation;
    public GameObject hitbox;
    public AudioSource swing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void playSound()
    {
        swing.Play();
    }
    void attack()
    {
        GameObject hi = Instantiate(hitbox, hitboxLocation);
        Destroy(hi, 1f);
    }
}
