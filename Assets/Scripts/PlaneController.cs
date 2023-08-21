using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    public float throttleIncrement = 0.1f;
    public float maxThrust = 200f;
    public float responsiveness = 10f;

    public float throotle; //percantage of maximum engine thrust currently being used.
    private float roll;  // tilting left to right
    private float pitch; // tilting front to back
    private float yaw;   // turning left to right

    [SerializeField] Transform propella;
    public GameManager gameManager;
    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    private Rigidbody rb;
    //AudioSource engineSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //engineSound = GetComponent<AudioSource>();  
        
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
        {
            throotle += throttleIncrement;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            throotle -= throttleIncrement;
        }
        throotle = Mathf.Clamp(throotle, 0f, 100f);
    }

    private void Update()
    {
        HandleInputs();
        propella.Rotate(Vector3.right * throotle);
        //engineSound.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Final"))
        {
            gameManager.WinPlayer();
        } 
    }
    

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrust * throotle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * -roll * responseModifier);
    }


}
