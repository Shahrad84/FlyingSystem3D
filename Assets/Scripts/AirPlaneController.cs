using System;
using UnityEngine;
using UnityEngine.Windows;


public class AirPlaneController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    float forwardCoefficient;

    [Header("Rotation Coefficients")]
    public float yawCoefficient = 1f;
    public float pitchCoefficient = 1f;
    public float rollCoefficient = 1f;

    [Header("Damping Settings")]
    public float pitchDampingTime = 0.5f;
    public float yawDampingTime = 0.5f;
    public float rollDampingTime = 0.5f;
    public float maxAngularSpeed = 5f;

    private float inputYaw, inputPitch, inputRoll;
    private Rigidbody rb;
    private float pitchVel, yawVel, rollVel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputRoll = UnityEngine.Input.GetAxis("PgRight&Left");
        inputYaw = UnityEngine.Input.GetAxis("Horizontal");
        inputPitch = UnityEngine.Input.GetAxis("PgUp&Down");
        //forwardCoefficient = (UnityEngine.Input.GetKey(KeyCode.LeftShift)) ? 2.5f : 1;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * forwardSpeed;

        rb.AddTorque(transform.right * -inputPitch * pitchCoefficient, ForceMode.Acceleration);
        rb.AddTorque(transform.up * inputYaw * yawCoefficient, ForceMode.Acceleration);
        rb.AddTorque(transform.forward * -inputRoll * rollCoefficient, ForceMode.Acceleration);

        ApplyAxisSpecificDamping();
    }

    private void ApplyAxisSpecificDamping()
    {
        Vector3 localAngularVelocity = transform.InverseTransformDirection(rb.angularVelocity);

        localAngularVelocity = Vector3.ClampMagnitude(localAngularVelocity, maxAngularSpeed);

        if (Mathf.Abs(inputPitch) < 0.1f)
        {
            localAngularVelocity.x = Mathf.SmoothDamp(
                localAngularVelocity.x,
                0f,
                ref pitchVel,
                pitchDampingTime,
                maxAngularSpeed,
                Time.fixedDeltaTime
            );
        }
        else
        {
            pitchVel = 0;
        }

        if (Mathf.Abs(inputYaw) < 0.1f)
        {
            localAngularVelocity.y = Mathf.SmoothDamp(
                localAngularVelocity.y,
                0f,
                ref yawVel,
                yawDampingTime,
                maxAngularSpeed,
                Time.fixedDeltaTime
            );
        }
        else
        {
            yawVel = 0;
        }

        if (Mathf.Abs(inputRoll) < 0.1f)
        {
            localAngularVelocity.z = Mathf.SmoothDamp(
                localAngularVelocity.z,
                0f,
                ref rollVel,
                rollDampingTime,
                maxAngularSpeed,
                Time.fixedDeltaTime
            );
        }
        else
        {
            rollVel = 0;
        }

        rb.angularVelocity = transform.TransformDirection(localAngularVelocity);
    }
}
