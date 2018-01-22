using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rigid;
    float forward, right;
    public float acceleration;
    public float maxSpeed;
    public float steeringForce;
    public float stickToGroundForce;
    public float rotateToGroundForce;
    public AnimationCurve steeringForceOverSpeed;
    public LayerMask groundMask;
    float defaultDrag, defaultAngularDrag;
    float inputGravityScale = 3f;
    public float steeringSmoothDamp = 0.2f;
    float currentForward, currentRight;
    bool grounded;
    public bool AI;
    public bool canDrive;
    bool supressDrag;
    float supressTimer;
    float currentSpeed;

    public bool isGrounded()
    {
        return grounded;
    }

    public void SupressDrag(float supressTime)
    {
        rigid.drag = 1f;
        supressTimer = supressTime;
        supressDrag = true;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        defaultDrag = rigid.drag;
        defaultAngularDrag = rigid.angularDrag;
    }

    void Update()
    {
        if(supressDrag)
        {
            supressTimer -= Time.deltaTime;
            if(supressTimer <= 0)
            {
                supressDrag = false;
            }
        }
        if (Physics.Raycast(transform.position, -transform.up, 1.5f, groundMask))
        {
            grounded = true;
            if(!supressDrag)
            rigid.drag = defaultDrag;
            rigid.angularDrag = defaultAngularDrag;
        }
        else
        {
            grounded = false;
            if ((transform.eulerAngles.x > 89 && transform.eulerAngles.x < 271) || (transform.eulerAngles.z > 89 && transform.eulerAngles.z < 271))
            {
                rigid.drag = defaultDrag;
                rigid.angularDrag = defaultAngularDrag;
            }
            else
            {
                rigid.drag = 0;
                rigid.angularDrag = 0;
            }
        }
        if (AI)
            return;
        if (!canDrive)
            return;
        if (Input.GetAxis("Vertical") != 0)
        {
            forward = Input.GetAxis("Vertical");
        }
        else
        {
            forward = 0;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            right = Input.GetAxis("Horizontal");
        }
        else
        {
            right = Mathf.SmoothDamp(right, 0, ref currentRight, steeringSmoothDamp);
        }
        currentSpeed = rigid.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            if (forward != 0)
            {
                if (forward > 0)
                {
                    rigid.AddForce(transform.forward * forward * acceleration);
                }
                else
                {
                    rigid.AddForce(transform.forward * forward * acceleration * 0.2f);
                }
            }
            if (right != 0)
            {
                if (forward > 0)
                {
                    rigid.MoveRotation(
                        transform.rotation * Quaternion.FromToRotation(transform.forward, transform.forward + transform.right * right * steeringForce
                        * steeringForceOverSpeed.Evaluate(rigid.velocity.magnitude / maxSpeed))
                        );
                }
                else if (forward < 0)
                {
                    rigid.MoveRotation(
                       transform.rotation * Quaternion.FromToRotation(transform.forward, transform.forward + transform.right * -right * steeringForce
                       * steeringForceOverSpeed.Evaluate(rigid.velocity.magnitude / maxSpeed))
                       );
                }
            }
            if (rigid.velocity.magnitude > maxSpeed)
            {
                rigid.velocity = rigid.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rigid.AddForce(Vector3.down * stickToGroundForce);
            //RaycastHit hit;
            //if(Physics.Raycast(transform.position, Vector3.down, out hit, 1000))
            //{
            //    transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y, 0), 0.001f);
            //}
        }
        rigid.AddForce(Vector3.down * stickToGroundForce);
    }

    public void Turn(float nextTurn)
    {
        right = nextTurn;
    }

    public void Accelerate(float nextAccelerate)
    {
        forward = nextAccelerate;
    }
}
