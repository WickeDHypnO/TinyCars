using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSmoke : MonoBehaviour {

    public List<ParticleSystem> wheelSmokeParticles;
    public bool emitting;
    public bool Emitting
    {
        get
        {
            return emitting;
        }
        set
        {
            foreach(ParticleSystem ps in wheelSmokeParticles)
            {
                if (value == false)
                {
                    var emit = ps.emission;
                    emit.enabled = false;
                }
                else
                {
                    var emit = ps.emission;
                    emit.enabled = true;
                }

            }
            carSound.PlayTireScrech(value);
            emitting = value;
        }
    }
    public Rigidbody rigid;
    public CarMovement carMovement;
    public CarSound carSound;
    public float minAngle;
    public float minSpeed;
	
	void Update () {
		if(Vector3.Angle(rigid.velocity, transform.forward) > minAngle && rigid.velocity.magnitude > minSpeed && carMovement.isGrounded())
        {
            if (!Emitting)
            {
                Emitting = true;
            }
        }
        else
        {
            if(Emitting)
            {
                Emitting = false;
            }
        }
	}
}
