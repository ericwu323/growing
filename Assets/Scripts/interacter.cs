using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interacter : MonoBehaviour
{
    public Transform cam;
    public event System.Action<int> questChange;
    public float playerActiveDistance;
    bool active = false;
    public TMP_Text eatText;
    public TMP_Text rideText;
    public TMP_Text signText;
    public TMP_Text readSignText;
    public playerControl player;
    public AudioSource cartoonTalk;
    public AudioSource eat;
    private bool riding = false;
    //private GameObject ridePoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<playerControl>();
        signText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!riding)
        {
            RaycastHit hit;
            active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActiveDistance);
            if (active == true)
            {
                if (hit.transform.gameObject.tag == "food")
                {
                    food nutrients = hit.transform.GetComponent<food>();
                    eatText.enabled = true;
                    if (Input.GetKeyDown(KeyCode.F) && gameHandler.req > nutrients.required)
                    {

                        Destroy(hit.transform.gameObject);
                        eat.Play();
                        player.Add(nutrients.levels);
                        if (nutrients.levels == 4 && gameHandler.questNum == 10)
                        {
                            gameHandler.questNum++;
                            questChange(11);
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "sign" && signText.enabled == false)
                {
                    readSignText.enabled = true;
                    sign hi = hit.transform.GetComponent<sign>();
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        StartCoroutine(sign(hi));
                    }
                }
            }
            else
            {
                eatText.enabled = false;
                readSignText.enabled = false;
            }
        }
      
    }
    IEnumerator sign(sign sign)
    {
        signText.enabled = true;
        if (sign.text != "")
        {
            cartoonTalk.Play();
            signText.text = sign.text;
            //yield return new WaitForSeconds(sign.waitTime);
            if (sign.text == "Fernando: Greetings your majesty, where have you been?")
            {
                if (gameHandler.questNum == 1)
                {
                    gameHandler.questNum = 2;
                    questChange(2);
                }
                    
            }
        }
        if (sign.text != "")
        {
            cartoonTalk.Play();
            yield return new WaitForSeconds(sign.waitTime);
            signText.text = sign.text2;
            if (sign.text2 == "Now's your chance! Save the animal kingdom by defeating The Golem once and for all!")
            {
                if (gameHandler.questNum == 12)
                {
                    gameHandler.questNum = 13;
                    questChange(13);
                }

            }
            //yield return new WaitForSeconds(sign.waitTime);
        }
        if (sign.text2 != "")
        {
            cartoonTalk.Play();
            yield return new WaitForSeconds(sign.waitTime);
            signText.text = sign.text3;
            //yield return new WaitForSeconds(sign.waitTime);
            if (sign.text3 == "If you do, we'll reward you with all the sea fruits you could ever want!")
            {
                if (gameHandler.questNum == 3)
                {
                    gameHandler.questNum = 4;
                    questChange(4);
                }
            }
            else if (sign.text3 == "I have a feeling it will come in handy soon, goodbye!")
            {
                if (gameHandler.questNum == 9)
                {
                    gameHandler.questNum = 10;
                    questChange(10);
                }

            }
        }
        if (sign.text3 != "")
        {
            cartoonTalk.Play();
            yield return new WaitForSeconds(sign.waitTime);
            signText.text = sign.text4;
            //yield return new WaitForSeconds(sign.waitTime);
            if (sign.text4 == "Take care!")
            {
                    gameHandler.questNum = 6;
                    questChange(6);
            }
        }
        if (sign.text4 != "")
        {
            cartoonTalk.Play();
            yield return new WaitForSeconds(sign.waitTime);
            
            
        }
        if(sign.text5 != "")
        {
            signText.text = sign.text5;
            cartoonTalk.Play();
            yield return new WaitForSeconds(sign.waitTime);
            if (sign.text5 == "Anyway, my friend Gary the Gazelle said he has a favor to ask from you. He left me this note for you to find him, can you help him out please?")
            {
                if (gameHandler.questNum == 6)
                {
                    gameHandler.questNum = 7;
                    questChange(7);
                }
            }
            else if (sign.text5 == "Please drive the snakes away and reclaim my home, you're the only one who can!")
            {
                if (gameHandler.questNum == 7)
                {
                    gameHandler.questNum = 8;
                    questChange(8);
                }
            }
        }
        signText.enabled = false;
        
    }
}
