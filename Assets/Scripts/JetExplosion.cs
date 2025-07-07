using System.Collections;
using UnityEngine;

public class JetExplosion : MonoBehaviour
{
    public float shakeDuration;
    public float shakeStrength;
    public GameObject camera;

    public GameObject ExplosionVFX;
    public Transform ExplosionVFX_Place;
    bool isExplode;

    public GameObject WASTED_page;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        isExplode = true;

        // camea shake handle
        camera.GetComponent<CameraShake>().enabled = true;
        camera.GetComponent<CameraShake>().ShakeCamera(shakeDuration, shakeStrength);

        // explosion VFX handle
        ExplosionVFX.SetActive(true);
        StartCoroutine(Show_WASTED_page());
    }

    private void Update()
    {
        if (isExplode) {
            ExplosionVFX.transform.position = ExplosionVFX_Place.position;
        }
    }

    IEnumerator Show_WASTED_page()
    {
        yield return new WaitForSeconds(shakeDuration - 0.1f);
        WASTED_page.SetActive(true);
        Time.timeScale = 0;
    }
}
