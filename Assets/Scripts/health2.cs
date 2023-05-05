using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class health2 : MonoBehaviour
{
    public GameObject uiPreFab;
    public Transform target;

    Transform ui;
    Image healthSlider;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPreFab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        GetComponent<hog>().OnHealthChanged += OnHealthChanged;
    }
    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }

    }
}