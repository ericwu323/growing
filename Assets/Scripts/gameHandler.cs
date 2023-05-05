using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public playerControl player;
    public interacter interacter;
    public whale whale;
    public hog hog;
    public GameObject fern;
    public sign fernando;
    public sign gary;
    public Transform garySpot, fernandoSpot;
    public TMP_Text quest;
    public TMP_Text signText;
    public static int questNum = 1;
    public int questTester = 0;
    public GameObject snake;
    public Transform snake1, snake2, snake3;
    public Transform t1, t2, t3, t4, t5;
    public static int snakeSig = 0;
    public static int req = 1;
    public AudioSource hits;
    public GameObject bossUI;
    public golem golem;
    public attackListener sword;
    void Start()
    { 
        interacter.questChange += questChange;
        player.questChange += questChange;
        whale.questChange += questChange;
        hog.questChange += questChange;
        golem.questChange += questChange;
        if(questTester >= 0)
        {
            questNum = questTester;
            questChange(questTester);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(snakeSig == 3)
        {
            if(questNum == 8)
            {
                questNum++;
                questChange(9);
            }
        }
    }
    void questChange(int questNum)
    {
        if(questNum == 2)
        {
            quest.text = "Level up and grow bigger by eating fruit!\nReach level 3\n\nHint: Look on plants and on top of rocks, leveling up increases your stats as well!";
        }
        if(questNum == 3)
        {
            quest.text = "Talk to Carlos the crab\n\nHint: Carlos is located at the entrace of the bay, look for the two pink magnolia trees!";
        }
        if(questNum == 4)
        {
            quest.text = "Defeat the Orca King Oliver\n\nHint: He's located past Carlos the crab, hitch a ride on the turtle to avoid drowning!";
        }
        if(questNum == 5)
        {
            quest.text = "Talk to Carlos and claim your reward!";
        }
        if(questNum == 6)
        {
            quest.text = "Talk to Fernando regarding your victory over Oliver";
            fernando.text = "Fernando: My gosh, you've grown so much taller and stronger since I last saw you!";
            fernando.text2 = "I can't believe you defeated THE Oliver, your father would be so proud of you";
            fernando.text3 = "It feels like just a minute ago you were a small helpless toddler, look at you now!";
            fernando.text4 = "Your victory is all the animal kingdom has been talking about!";
            fernando.text5 = "Anyway, my friend Gary the Gazelle said he has a favor to ask from you. He left me this note for you to find him, can you help him out please?";
        
        }
        if(questNum == 7)
        {
            quest.text = "Talk with Gary\n\nGary's note: Your majesty, I'm waiting for you across the path from Buttercup the cow, please hurry!";
            gary.text = "Gary: Thank god you're here, ever since your victory, evil snakes have been showing up out of nowhere!";
            gary.text2 = "A group of them have even driven me out of my home next to my beloved mushrooms";
            gary.text3 = "This must be the doing of Reg, the ruler of the Rainbow Forest";
            gary.text4 = "He must be angry after his friend Oliver was defeated, those snakes he released are native to the forest and they're terrifying";
            gary.text5 = "Please drive the snakes away and reclaim my home, you're the only one who can!";
            gary.transform.position = garySpot.position;
            
        }
        if(questNum == 8)
        {
            quest.text = "Defeat the three snakes in Gary's home. His home is in the mushroom fields under the gold trees\n\n Recommended level: 5";
            GameObject s1 = Instantiate(snake, snake1.position, Quaternion.identity);
            s1.GetComponent<enemy>().points[0] = t1;
            s1.GetComponent<enemy>().points[1] = t2;
            s1.GetComponent<enemy>().points[2] = t3;
            s1.GetComponent<enemy>().points[3] = t4;
            s1.GetComponent<enemy>().points[4] = t5;
            s1.GetComponent<enemy>().hitSound = hits;
            GameObject s2 = Instantiate(snake, snake2.position, Quaternion.identity);
            s2.GetComponent<enemy>().points[0] = t2;
            s2.GetComponent<enemy>().points[1] = t5;
            s2.GetComponent<enemy>().points[2] = t1;
            s2.GetComponent<enemy>().points[3] = t4;
            s2.GetComponent<enemy>().points[4] = t3;
            s2.GetComponent<enemy>().hitSound = hits;
            GameObject s3 = Instantiate(snake, snake3.position, Quaternion.identity);
            s3.GetComponent<enemy>().points[0] = t4;
            s3.GetComponent<enemy>().points[1] = t3;
            s3.GetComponent<enemy>().points[2] = t2;
            s3.GetComponent<enemy>().points[3] = t5;
            s3.GetComponent<enemy>().points[4] = t1;
            s3.GetComponent<enemy>().hitSound = hits;
        }
        if (questNum == 9)
        {
            quest.text = "Report back to Gary";
            gary.text = "Gary: I am forever grateful to you, now I can go back to eating mushrooms in peace!";
            gary.text2 = "As a token of my gratitude, take this jumbo banana peeler! Due to it's size, you have to be very strong to use it.";
            gary.text3 = "I have a feeling it will come in handy soon, goodbye!";
            gary.text4 = "";
            gary.text5 = "";
            
        }
        if(questNum == 10)
        {
            gary.transform.position = t1.position;
            quest.text = "Eat the giant banana at the entrance to the Rainbow Forest\n\nLevel Requirement: 6";
            gary.text = "Gary: The grass here tastes so good!I could eat this all day, and I do !";
            gary.text2 = "omnomnomnom";
            gary.text3 = "";
            gary.text4 = "";
            gary.text5 = "";
            req++;
        }
        if (questNum == 11)
        {
            quest.text = "Defeat Howard, Ruler of the Rainbow Forest";
        }
        if (questNum == 12)
        {
            quest.text = "Talk to Fernando at the entrance of the Rainbow Forest";
            fern.transform.position = fernandoSpot.position;
            fern.transform.LookAt(player.transform);
            fernando.text = "I have terrible news! The Golem is out to get you!";
            fernando.text2 = "Now's your chance! Save the animal kingdom by defeating The Golem once and for all!";
            fernando.text3 = "";
            fernando.text4 = "";
            fernando.text5 = "";

        }
        if (questNum == 13)
        {
            quest.text = "Defeat The Golem";
            bossUI.SetActive(true);
            golem.attackmode = true;
            sword.hitbox.layer = 10;
        }
        if(questNum == 14)
        {
            quest.text = "Watch the fireworks!\n\nCredits will roll in 20 seconds\nThanks for playing!";
        }
    }
}
//FERNANDO KIDNAPPED
