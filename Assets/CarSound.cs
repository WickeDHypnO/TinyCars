using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour {

    public AudioSource engineSound;
    public AudioSource tireSound;
    public float minPitch, maxPitch;
    CarMovement carMovement;
    Rigidbody rigid;

	void Start () {
        rigid = GetComponent<Rigidbody>();
        carMovement = GetComponent<CarMovement>();
	}
	
	void Update () {
		engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, rigid.velocity.magnitude / carMovement.maxSpeed);
	}

    public void PlayTireScrech(bool play)
    {
        if(play)
        {
            tireSound.Play();
        }
        else
        {
            tireSound.Stop();
        }
    }
}
