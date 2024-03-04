using System.Collections;
using UnityEngine;

public class FlickeringLightbulb : MonoBehaviour
{
    //Script generated using AI tools

    public float fullIntensityDuration = 2f;
    public float flickerDuration = 0.01f;
    public float flickerIntensity = 2f;
    public float minFlickerInterval = 1f;     //sets the parameters for the flicker
    public float maxFlickerInterval = 5f;
    public float minIntensity = 5f;
    public float maxIntensity = 10f;
    public Color flickerColor = new Color(1f, 0.8f, 0.6f);
    public Material bulbMaterial;

    private Light lightBulb;
    private Color originalColor;

    void Start()
    {
        lightBulb = GetComponent<Light>();

        // Store the original color of the material
        if (bulbMaterial != null)
        {
            originalColor = bulbMaterial.GetColor("_EmissionColor") / lightBulb.intensity;
        }

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // Full intensity mode
            lightBulb.intensity = Random.Range(minIntensity, maxIntensity);
            bulbMaterial.SetColor("_EmissionColor", flickerColor * lightBulb.intensity);

            // Wait for full intensity duration
            yield return new WaitForSeconds(fullIntensityDuration);

            // Flickering mode
            float flickerIntensity = Random.Range(this.flickerIntensity, maxIntensity);
            lightBulb.intensity = flickerIntensity;
            bulbMaterial.SetColor("_EmissionColor", flickerColor * flickerIntensity);

            // Wait for a short flicker duration
            yield return new WaitForSeconds(flickerDuration);

            // Back to full intensity mode
            lightBulb.intensity = Random.Range(minIntensity, maxIntensity);
            bulbMaterial.SetColor("_EmissionColor", flickerColor * lightBulb.intensity);

            // Wait for a random interval before the next flicker
            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval));
        }
    }
}
