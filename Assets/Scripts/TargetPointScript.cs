using Unity.VisualScripting;
using UnityEngine;

public class TargetPointScript : MonoBehaviour
{
    float rocketRange;

    public RectTransform targetPointer;
    public Transform camera;
    public Transform jet;

    public GameObject rocket;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        rocketRange = rocket.GetComponent<RocketScript>().range;
    }

    void Shoot()
    {
        Vector3 shootStartPosition = getWorldPosition_of_targetPointer();
        Vector3 shootVector = jet.forward * rocketRange;

        Vector3 shootEndPosition = shootVector + shootStartPosition;

        GameObject newRocket = Instantiate(rocket, rocket.transform.position, rocket.transform.rotation);
        newRocket.SetActive(true);
        newRocket.gameObject.GetComponent<RocketScript>().startShooting(shootEndPosition);

        camera.GetComponent<CameraShake>().ShakeCamera(0.1f, 0.05f);

    }

    Vector3 getWorldPosition_of_targetPointer()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            targetPointer.GetComponentInParent<Canvas>().worldCamera,
            targetPointer.position
        );

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return ray.GetPoint(0);
    }
}
