using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    private playerControl player;
    public int lvl = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
