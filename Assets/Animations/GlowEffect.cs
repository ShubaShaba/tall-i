using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public Material targetMaterial; // Assign the material in the Inspector
    public Color baseColor = Color.red; // The base color to adjust
    public float intensitySpeed = 3.0f; // Speed of intensity change
    public float minIntensity = 0.0f; // Minimum color intensity
    public float maxIntensity = 3.0f; // Maximum color intensity

    private float intensity = 1.0f;
    private bool increasing = true;
    public bool change = false;

    void Update()
    {
        if (increasing && change)
        {
            intensity += intensitySpeed * Time.deltaTime;
            if (intensity >= maxIntensity)
            {
                intensity = maxIntensity;
                increasing = false;
            }
        }

        if (increasing == false && change == false)
        {

            {
                intensity -= intensitySpeed * Time.deltaTime;
                if (intensity <= minIntensity)
                {
                    intensity = minIntensity;
                    increasing = true;
                }
            }
        }
        // Set the color with adjusted intensity
        targetMaterial.color = baseColor * intensity;
    }



    public void PlayAnimation()
    {
        change = true;
        increasing = true;
    }

    public void CancelAnimation()
    {
        change = false;
        increasing = false;
    }
}
