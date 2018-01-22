using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public GameObject deathParticles;
    Rigidbody rigid;
    public float speed;
    public float explosionForce;
    public float damage = 20f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        rigid.MovePosition(transform.position + transform.forward * speed);
	}

    private void OnDestroy()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            if(other.GetComponent<CarHealth>())
            {
                other.GetComponent<CarMovement>().SupressDrag(1f);
                other.GetComponent<CarHealth>().GetHit(damage);
                other.GetComponent<Rigidbody>().AddForceAtPosition((other.transform.position - transform.position)*explosionForce, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
