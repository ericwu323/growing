using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject player;
    public playerControl playerControl;
    public Transform respawnPoint;
    public Canvas ui;
    public Canvas respawnScreen;
    public cam cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void respawning()

    {
        
        
        ui.enabled = true;
        respawnScreen.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerControl.dead = false;
        cam.dead = false;
    }
}
