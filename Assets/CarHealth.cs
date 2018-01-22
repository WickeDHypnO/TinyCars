using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHealth : MonoBehaviour
{

    public Slider healthSlider;
    public float maxHealth;
    float health;

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthSlider.transform.eulerAngles = new Vector3(45, -45, 0);
    }

    public void GetHit(float damage)
    {
        health -= damage;
        healthSlider.value = health / maxHealth;
        if (health <= 0)
        {
            //death
        }
    }
}
