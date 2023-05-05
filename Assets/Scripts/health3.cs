using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health3 : MonoBehaviour
{
    public golem golem;
    public Image hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Image>();
        golem.OnHealthChanged += OnHealthChanged;
    }
    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = currentHealth / (float)maxHealth;
        if(hp != null)
            hp.fillAmount = healthPercent;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
