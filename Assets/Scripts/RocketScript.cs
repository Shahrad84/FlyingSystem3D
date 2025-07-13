using System.Collections;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameObject explosionVFX;

    bool move = false;
    public float maxMovementSpeed;
    float speed;
    public float speedAcceleration;
    public GameObject jet;

    float passedDistance = 0;
    public float range;

    public GameObject camera;
    public float rocketExplosionDuration;

    public GameObject smokeVFX;
    public float smokeDuration;

    private void Update()
    {
        if (move)
        {
            if (speed < maxMovementSpeed)
            {
                speed += Time.deltaTime * speedAcceleration;
            }

            transform.position += transform.forward * speed * Time.deltaTime;

            if (passedDistance < range)
            {
                passedDistance += Time.deltaTime * speed;
            }
            else
            {
                move = false;
                StartCoroutine(ExplodeRocket(false));
            }
        }

    }

    public void startShooting(Vector3 shootTargetPosition)
    {
        transform.LookAt(shootTargetPosition);
        move = true;

        transform.localScale *= 10;

        speed = jet.GetComponent<AirPlaneController>().forwardSpeed;
    }

    IEnumerator ExplodeRocket(bool isTargetDown)
    {
        explosionVFX.SetActive(true);
        float distaceBetween_cameraAndExposion = (camera.transform.position - transform.position).magnitude;
        if (isTargetDown)
        {
            camera.GetComponent<CameraShake>().ShakeCamera(rocketExplosionDuration, 220 / distaceBetween_cameraAndExposion);
        }
        else
        {
            camera.GetComponent<CameraShake>().ShakeCamera(rocketExplosionDuration * 0.7f, (220 / distaceBetween_cameraAndExposion) * 0.65f);
            explosionVFX.transform.localScale *= 0.2f;
        }

        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        speed = 0;
        StartCoroutine(ExplodeRocket(true));
    }
}
