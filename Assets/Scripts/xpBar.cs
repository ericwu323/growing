using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpBar : MonoBehaviour
{
    public playerControl player;
    private Image xp1;
    // Start is called before the first frame update
    void Start()
    {
        xp1 = GetComponent<Image>();
        player.OnXpChanged += OnXpChanged;
    }
    void OnXpChanged(float maxXp, float xp)
    {
        float xpPercent = xp / (float)maxXp;
        xp1.fillAmount = xpPercent;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
