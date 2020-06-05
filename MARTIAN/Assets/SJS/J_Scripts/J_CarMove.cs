using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_CarMove : MonoBehaviour
{
    public float MotorF;
    public float steerF;
    public WheelCollider wheelBR;
    public WheelCollider wheelBL;


    public WheelCollider wheelFR;
    public WheelCollider wheelFL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical") * MotorF;
        float h = Input.GetAxis("Horizontal") * steerF;
        wheelBR.motorTorque = v;
        wheelBL.motorTorque = v;

        wheelFR.steerAngle = h;
        wheelFL.steerAngle = h;
    }
}
