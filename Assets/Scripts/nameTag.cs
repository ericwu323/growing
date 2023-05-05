using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class nameTag : MonoBehaviour
{
    public string nametag;
    public Color color;
    public GameObject target;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            target.transform.forward = -cam.forward;
            target.transform.Rotate(0, 180, 0);
            //target.GetComponent<TMP_Text>().color = color;
            //target.GetComponent<TMP_Text>().text = nametag;
        }
    }
}
