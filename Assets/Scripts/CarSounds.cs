using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public CarController carController;
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    private Rigidbody carRb;
    private AudioSource carAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    void Start()
    {
        carAudio = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        if (carController.activePlayer == carController.playerCar)
        {
            currentSpeed = carRb.velocity.magnitude;
            pitchFromCar = carRb.velocity.magnitude / 50f;

            if (currentSpeed < minSpeed)
            {
                carAudio.pitch = minPitch;
            }
            if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
            {
                carAudio.pitch = minPitch + pitchFromCar;
            }
            if (currentSpeed > maxSpeed)
            {
                carAudio.pitch = maxPitch;
            }
        }
       
    }
}
