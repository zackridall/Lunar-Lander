using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    private KeyCode _thrustKey = KeyCode.Space;
    private KeyCode _rotateLeft = KeyCode.A;
    private KeyCode _rotateRight = KeyCode.D;
    private Rigidbody rb;

    [SerializeField] public float ThrustForce = 1000;
    [SerializeField] public float RotateSpeed = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        var thrustForce = Vector3.up * Time.deltaTime * ThrustForce;

        if (thrusting)
        {
            rb.AddRelativeForce(thrustForce);
           
        }
     
    }

    void ProcessRotation()
    {
        var rotatingLeft = Input.GetKey(_rotateLeft);
        var rotatingRight = Input.GetKey(_rotateRight);
       

        if (rotatingLeft)
        {
            ApplyRotation(Vector3.forward);
        }
        else if (rotatingRight)
        {
            transform.Rotate(Vector3.back);          
        }
    }

    private void ApplyRotation(Vector3 direction)
    {
        var rotationForce = Time.deltaTime * RotateSpeed;
        
        // Freeze location so we can manually rotate
        rb.freezeRotation = true;
        transform.Rotate(direction * rotationForce);

        // Give control back to the physics system
        rb.freezeRotation = false;
    }
}
