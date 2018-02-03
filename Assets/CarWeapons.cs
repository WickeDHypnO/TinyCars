using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeapons : MonoBehaviour
{

    public Weapon front;
    bool AI;
    float timer;
    float currentDelay;
    public float delay;
    public float randomDelay;
    public float raycastDistance;

    void Start()
    {
        if (GetComponent<CarMovement>().AI)
        {
            AI = true;
            currentDelay = delay + Random.Range(0f, 1f) * randomDelay;
        }
    }

    void Update()
    {
        if (AI)
        {
            timer += Time.deltaTime;
            if(timer >= currentDelay)
            {
                if(Physics.Raycast(transform.position, transform.forward, raycastDistance, LayerMask.GetMask( "Player", "AI")))
                {
                    front.FireWeapon();
                }
                timer = 0;
                currentDelay = delay + Random.Range(0f, 1f) * randomDelay;
            }
        }
        else
        {
            if (Input.GetButtonDown("FrontWeapon"))
            {
                front.FireWeapon();
            }
        }
    }
}
