using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class J_Wheels : MonoBehaviour
{

    public WheelCollider front_dr, front_p;
    public WheelCollider back_dr, back_p;

    public Transform frontDr, frontP;
    public Transform backDr, backP;

    public float _speerAngle;

    public float _motorForce;

    public float speerAngle;

    float h, v;

    private void Start()
    {
        
    }

    private void Update()
    {
        Inputs();
        Drive();
        SteerCar();

        UpdateWheelPos(front_dr, frontDr);
        UpdateWheelPos(front_p, frontP);

        //---------------------------------------------------------
        UpdateWheelPos(back_dr, backDr);
        UpdateWheelPos(back_p, backP);

    }

    void Inputs()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void Drive()
    {
        back_dr.motorTorque = v * _motorForce;
        back_p.motorTorque = v * _motorForce;
    }

    void SteerCar()
    {
        speerAngle = _speerAngle * h;
        front_dr.steerAngle = speerAngle;
        front_p.steerAngle = speerAngle;
    }

    void UpdateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);

        t.position = pos;
        t.rotation = rot;
    }
}