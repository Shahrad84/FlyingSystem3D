using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPosition;   // Stores camera initial position
    float shakeDuration;
    float leftTime;   // remaining time for shake
    float shakeStrength;   
    float currentShakeStrength;   // current shake intensity
    public float noiseFrequency = 10f;   // how fast the shake jitters

    private void Start()
    {
        leftTime = 0;   // initialize to avoid unintended shakes.
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (leftTime > 0)
        {
            // calculating current strength
            currentShakeStrength = shakeStrength * (leftTime / shakeDuration);


            // Time.time * noiseFrequency speeds up the noise sampling for rapid movement.
            float x = (Mathf.PerlinNoise(Time.time * noiseFrequency, 0) * 2 - 1) * currentShakeStrength;
            float y = (Mathf.PerlinNoise(Time.time * noiseFrequency, 0) * 2 - 1) * currentShakeStrength;


            // apply shake offset while preserving the original position.
            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            leftTime -= Time.deltaTime;

        }
        else if (transform.localPosition != originalPosition)
        {
            // reset to original localPosition when shake ends
            transform.localPosition = originalPosition;
        }
    }

    public void StartShakeCamera(float duration, float strength)
    {
        shakeDuration = duration;
        shakeStrength = strength;
        leftTime = shakeDuration;
        originalPosition = transform.localPosition;
    }
}
