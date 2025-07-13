using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPosition;
    float shakeDuration;
    float leftTime;
    float shakeStrength;
    float currentShakeStrength;

    private void Start()
    {
        leftTime = 0;
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            currentShakeStrength = shakeStrength * (leftTime / shakeDuration);

            float x = (Mathf.PerlinNoise(Time.time * 10f, 0) * 2 - 1) * currentShakeStrength;
            float y = (Mathf.PerlinNoise(0, Time.time * 10f) * 2 - 1) * currentShakeStrength;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            leftTime -= Time.deltaTime;
        }
        else if (transform.localPosition != originalPosition)
        {
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera(float duration, float strength)
    {
        shakeDuration = duration;
        shakeStrength = strength;
        leftTime = shakeDuration;
        originalPosition = transform.localPosition;
    }
}
