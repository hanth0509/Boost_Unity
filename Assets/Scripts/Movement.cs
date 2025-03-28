using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThurterParticles;
    [SerializeField] ParticleSystem rightThurterParticles;
    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThurst();
        ProcessRotation();
    }
    void ProcessThurst() //phát âm thanh 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();//phát hiệu ứng
        }
    }

    void StopRotating()
    {
        rightThurterParticles.Stop();
        leftThurterParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightThurterParticles.isPlaying)
        {
            rightThurterParticles.Play();//phát hiệu ứng
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();// dung hieu ung
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }



    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftThurterParticles.isPlaying)
        {
            leftThurterParticles.Play();//phát hiệu ứng
        }
    }

    private void ApplyRotation(float rotationThisFrame) //quay goc bao nhieu tuy chinh
    {
        rb.freezeRotation = true;//dong bang vong quay de manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
