using UnityEngine;
using System;
using Unity.Mathematics;

public class FlyAndForceSystem : MonoBehaviour
{
    [Header("Refrences")]
    Rigidbody rb;
    public bool isGrounded;


    [Header("Forces & Physics")]
    public float maxForwardForce;
    public float maxBackwardForce;
    float forwardForce;
    Vector3 forces_without_airResistance;
    private float airResistanceCoefficient;
    public float groundFriction = 50f;

    [Header("Velocity")]
    public float maxVelocity;
    public float takeOffVelocity = 20;
    public float landVelocity = 10;


    float WS_input;
    float thrustForce;
    float jahesh;
    float inputPitch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forwardForce = 0;
        airResistanceCoefficient = maxForwardForce / (maxVelocity * maxVelocity);
    }

    private void Update()
    {
        if (WS_input > 0)
        {
            forwardForce = Mathf.SmoothDamp(forwardForce, maxForwardForce, ref jahesh, (1 - (forwardForce / maxForwardForce)) * 12);
        }
        else if (WS_input == 0)
        {
            forwardForce = Mathf.SmoothDamp(forwardForce, 0, ref jahesh, (forwardForce / maxForwardForce) * 6);
        }
        else if (isGrounded)
        {
            forwardForce = Mathf.SmoothDamp(forwardForce, -maxBackwardForce, ref jahesh, 1);
        }
    }

    private void FixedUpdate()
    {
        WS_input = Input.GetAxis("Vertical");
        inputPitch = Input.GetAxis("PgUp&Down");

        if (!isGrounded)
        {
            WS_input = Mathf.Clamp(WS_input, 0, 1);
        }

        if (!isGrounded || rb.linearVelocity.magnitude >= takeOffVelocity)
        {
            HandleFlyMovement();
        }
        else if (isGrounded)
        {
            HandleGroundedMovement();
        }

        //Debug.Log(rb.linearVelocity);
    }

    void HandleGroundedMovement()
    {
        if (Mathf.Abs(forwardForce) > 0.1f)
        {
            float airRes_Z = forwardForce * (rb.linearVelocity.z / maxVelocity) * -1;
            rb.AddForce((forwardForce + airRes_Z) * transform.forward * Time.fixedDeltaTime * 50);
        }
        else
        {
            Vector3 horizontalVelocity = rb.linearVelocity;
            horizontalVelocity.y = 0;

            if (horizontalVelocity.magnitude > 5f)
            {
                Vector3 frictionForce = -horizontalVelocity.normalized * groundFriction;
                rb.AddForce(frictionForce * Time.fixedDeltaTime * 50);
            }
            else
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
        }
    }

    void HandleFlyMovement()
    {
        thrustForce = Physics.gravity.y * -1 * rb.mass * WS_input;
        forces_without_airResistance = thrustForce * Vector3.up + transform.forward * forwardForce;
        rb.AddForce(forces_without_airResistance * Time.fixedDeltaTime * 50);


        if (rb.linearVelocity.magnitude >= landVelocity)
        {
            float airResistanceMagnitude = rb.linearVelocity.sqrMagnitude * airResistanceCoefficient;
            Vector3 airResistanceForce = -rb.linearVelocity.normalized * airResistanceMagnitude;
            rb.AddForce(airResistanceForce * Time.fixedDeltaTime * 50);
        }

    }

    void OnGUI()
    {
        GUILayout.Label($"Speed: {rb.linearVelocity.magnitude:F1} m/s");
        //GUILayout.Label($"Acceleration: {_currentAcceleration.magnitude:F1} m/s�");
    }
}
