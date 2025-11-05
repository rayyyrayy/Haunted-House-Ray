using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    // The Light component you are controlling
    private Light targetLight;

    // Control the speed and amount of flicker
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f; // How quickly the intensity changes

    void Start()
    {
        // Get the Light component attached to this GameObject
        targetLight = GetComponent<Light>();
        
        // Start the coroutine for the flickering effect
        StartCoroutine(DoFlicker());
    }

    IEnumerator DoFlicker()
    {
        while (true)
        {
            // Randomly pick a new intensity value between min and max
            float targetIntensity = Random.Range(minIntensity, maxIntensity);

            // Smoothly move the light's intensity towards the target value
            while (Mathf.Abs(targetLight.intensity - targetIntensity) > 0.01f)
            {
                targetLight.intensity = Mathf.Lerp(
                    targetLight.intensity, 
                    targetIntensity, 
                    Time.deltaTime / flickerSpeed
                );
                yield return null; // Wait for the next frame
            }

            // Wait a short, random amount of time before picking a new target
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}