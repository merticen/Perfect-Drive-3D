using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CarController : MonoBehaviour
{
    public enum Axe1
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public Axe1 axe1;
    }

    public CameraFollow cameraFollow;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSens = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    public GameObject playerCar;
    public GameObject playerPlane;
    public GameObject activePlayer;

    public GameManager gameManager;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
        activePlayer = playerCar;
        
    }
    void Update()
    {
        GetInput();
        AnimateWheels();
        //WheelEffects();
    }
    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }
    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axe1 == Axe1.Front)
            {
                var _steerAngle = steerInput * turnSens * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.rotation = rot;
            //wheel.wheelModel.transform.position = pos;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("SwapController"))
        {
            if (activePlayer == playerCar)
            {
                gameManager.swapEffect.Play();
                playerCar.SetActive(false);
                playerPlane.SetActive(true);
                activePlayer = playerPlane;
                Transform newTarget = playerPlane.transform;
                cameraFollow.ChangeTarget(newTarget);
                playerPlane.SetActive(true);
            }
           
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.KillPlayer();
        }
    }



    //void WheelEffects()
    //{
    //    foreach (var wheel in wheels)
    //    {
    //        if (Input.GetKey(KeyCode.Space) && wheel.axe1 == Axe1.Rear)
    //        {
    //            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
    //        }
    //        else
    //        {
    //            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
    //        }
    //    }
    //}


}
