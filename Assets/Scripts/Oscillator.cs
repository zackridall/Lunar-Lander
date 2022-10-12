using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Comparing floats with '==' is unpredictable
        // A better way is Math.Abs((A - B)) < Mathf.Epsilon
        if (period <= Mathf.Epsilon) { return; }

        const float TAU = Mathf.PI * 2;
        float cycles = Time.time / period;
        float oscillationFactor = (Mathf.Sin(cycles * TAU) + 1) / 2f; // oscillate from 0 -> 1

        Vector3 offset = movementVector * movementFactor * oscillationFactor;
        transform.position = startingPosition + offset;
        
    }
}
