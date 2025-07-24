using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Wheel") || contact.thisCollider.CompareTag("Wheel"))
            {
                gameObject.GetComponent<FlyAndForceSystem>().isGrounded = true;
                return;
            }
        }

        gameObject.GetComponent<JetExplosion>().Explode();
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Wheel") || contact.thisCollider.CompareTag("Wheel"))
            {
                gameObject.GetComponent<FlyAndForceSystem>().isGrounded = false;
                return;
            }
        }
    }
}
