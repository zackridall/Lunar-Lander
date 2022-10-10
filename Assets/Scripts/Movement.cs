using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] public float thrustForce = 1000;
    [SerializeField] public float rotateSpeed = 100;
    [SerializeField] public AudioClip mainEngine;
    [SerializeField] public ParticleSystem rotateRightParticles;
    [SerializeField] public ParticleSystem rotateLeftParticles;
    [SerializeField] public ParticleSystem thrustParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    private KeyCode _thrustKey = KeyCode.Space;
    private KeyCode _rotateLeftKey = KeyCode.A;
    private KeyCode _rotateRightKey = KeyCode.D;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        var thrusting = Input.GetKey(_thrustKey);
        var thrustForce = Vector3.up * Time.deltaTime * this.thrustForce;

        if (thrusting)
        {
            _startThrusting(thrustForce);
        }
        else
        {
            _stopThrusting();
        }
    }
    void ProcessRotation()
    {
        var rotatingLeft = Input.GetKey(_rotateLeftKey);
        var rotatingRight = Input.GetKey(_rotateRightKey);


        if (rotatingLeft)
        {
            _rotateLeft();
        }
        else if (rotatingRight)
        {
            _rotateRight();
        }
        else
        {
            _stopRotation();
        }
    }

    //-------------------------------------------------------------------------

    private void _startThrusting(Vector3 thrustForce)
    {
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        rb.AddRelativeForce(thrustForce);
    }
    private void _stopThrusting()
    {
        thrustParticles.Stop();
        audioSource.Stop();
    }

    private void _rotateLeft()
    {
        _applyRotation(Vector3.forward);

        if (!rotateLeftParticles.isPlaying)
        {
            rotateLeftParticles.Play();
        }
    }
    private void _rotateRight()
    {
        _applyRotation(Vector3.back);
        if (!rotateRightParticles.isPlaying)
        {
            rotateRightParticles.Play();
        }
    }
    private void _stopRotation()
    {
        rotateRightParticles.Stop();
        rotateLeftParticles.Stop();
    }

    private void _applyRotation(Vector3 direction)
    {
        var rotationForce = Time.deltaTime * rotateSpeed;
        
        // Freeze location so we can manually rotate
        rb.freezeRotation = true;
        transform.Rotate(direction * rotationForce);

        // Give control back to the physics system
        rb.freezeRotation = false;
    }
}
