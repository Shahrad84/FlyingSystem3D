using System.Linq;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public WheelCollider rightBack_wheelColider;
    public WheelCollider leftBack_wheelColider;
    public WheelCollider rightFront_wheelColider;
    public WheelCollider leftFront_wheelColider;
    public WheelCollider[] wheelCollidersArray;

    public float motorForce;
    public float breakForce;
    public float maxSteerAngle;

    bool hit;
    RaycastHit raycastHit;
    public LayerMask terrainLayerMask;

    private void Start()
    {

    }

    private void Update()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        rightBack_wheelColider.motorTorque = z * motorForce;
        leftBack_wheelColider.motorTorque = z * motorForce;

        rightFront_wheelColider.steerAngle = x * maxSteerAngle;
        leftFront_wheelColider.steerAngle = x * maxSteerAngle;


        if (z == 0)
        {
            rightBack_wheelColider.brakeTorque = breakForce;
            leftBack_wheelColider.brakeTorque = breakForce;
        }
        else
        {
            rightBack_wheelColider.brakeTorque = 0;
            leftBack_wheelColider.brakeTorque = 0;
        }

        IsWheelTouchEarth();
    }

    void IsWheelTouchEarth()
    {
        foreach (WheelCollider collider in wheelCollidersArray)
        {
            if (Physics.Raycast(collider.gameObject.transform.position, new Vector3(0, -1, 0), out raycastHit, collider.radius * 1.2f))
            {
                gameObject.GetComponent<FlyAndForceSystem>().isGrounded = true;
                return;
            }
        }

        gameObject.GetComponent<FlyAndForceSystem>().isGrounded = false;
    }
}
