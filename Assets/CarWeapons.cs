using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeapons : MonoBehaviour {

    public Weapon front;
	
	void Start () {
		if(GetComponent<CarMovement>().AI)
        {
            enabled = false;
        }
	}
	
	void Update () {
		if(Input.GetButtonDown("FrontWeapon"))
        {
            front.FireWeapon();
        }
	}
}
