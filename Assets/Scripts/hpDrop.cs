using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpDrop : MonoBehaviour
{
    public playerControl player;
    private Image hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Image>();
        player.OnHealthChanged += OnHealthChanged;
    }
    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = currentHealth / (float)maxHealth;
        hp.fillAmount = healthPercent;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
