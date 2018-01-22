using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject weapon;
    public float delay;
    public float charges = 3f;
    float timer;
	
	void Update () {
        timer += Time.deltaTime;
	}

    public void FireWeapon()
    {
        if (timer >= delay && charges > 0)
        {
            Instantiate(weapon, transform.position, transform.rotation);
            charges--;
            timer = 0;
        }
    }
}
