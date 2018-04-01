using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels
{
    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;
    public GameObject backLeftWheel;
    public GameObject backRightWheel;
}


public class CarModel : MonoBehaviour {

    public MeshFilter model;
    public MeshRenderer renderer;
    public Wheels wheels;

	void Start () {
        var carProps = FindObjectOfType<GameManager>().car;
        model.mesh = carProps.model;
        renderer.material = carProps.material;
        model.transform.localScale = new Vector3(carProps.scale, carProps.scale, carProps.scale);
        model.GetComponentInParent<BoxCollider>().size = carProps.colliderBounds;
        model.transform.localPosition = carProps.offset;
        wheels.frontRightWheel.transform.localPosition = carProps.frontWheelOffset;
        wheels.frontLeftWheel.transform.localPosition = new Vector3(-carProps.frontWheelOffset.x, carProps.frontWheelOffset.y, carProps.frontWheelOffset.z);
        wheels.backRightWheel.transform.localPosition = carProps.backWheelOffset;
        wheels.backLeftWheel.transform.localPosition = new Vector3(-carProps.backWheelOffset.x, carProps.backWheelOffset.y, carProps.backWheelOffset.z);
        wheels.frontLeftWheel.GetComponent<MeshFilter>().mesh = carProps.wheelModel;
        wheels.frontRightWheel.GetComponent<MeshFilter>().mesh = carProps.wheelModel;
        wheels.backLeftWheel.GetComponent<MeshFilter>().mesh = carProps.wheelModel;
        wheels.backRightWheel.GetComponent<MeshFilter>().mesh = carProps.wheelModel;
        wheels.frontLeftWheel.transform.localScale = carProps.wheelScale;
        wheels.frontRightWheel.transform.localScale = carProps.wheelScale;
        wheels.backLeftWheel.transform.localScale = carProps.wheelScale;
        wheels.backRightWheel.transform.localScale = carProps.wheelScale;
    }
	
	void Update () {
		
	}
}
