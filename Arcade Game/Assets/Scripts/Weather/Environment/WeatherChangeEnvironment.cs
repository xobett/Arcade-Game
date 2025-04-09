using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeatherChangeEnvironment : MonoBehaviour
{
    [SerializeField] private VolumeProfile volumeProfile;

    [Header("BLOOM SETTINGS")]
    private Bloom profileBloom;
    private Color bloomTintColor;

    [Header("VIGNETTE SETTINGS")]
    private Vignette profileVignette;
    private Color vignetteTintColor;

    private float vignetteIntensity;
    private float vignetteSmoothness;

    [SerializeField, Range(0.1f, 1f)] private float transitionSpeed;

    private void Start()
    {
        SetEnvironmenEffectsDefault();
    }

    public void SetEnvironmentValues(float actualTemp)
    {
        Color weatherColor = Color.clear;

        switch (actualTemp)
        {
            case var color when actualTemp <= 0:
                {
                    bloomTintColor = new Color(0, 110, 255);

                    vignetteTintColor = new Color(0, 7, 70);
                    vignetteIntensity = 0.16f;
                    vignetteSmoothness = 0.3f;

                    break;
                }

            case var color when actualTemp <= 10:
                {
                    bloomTintColor = Color.cyan;

                    vignetteTintColor = new Color(12, 124, 238);
                    vignetteIntensity = 0.15f;
                    vignetteSmoothness = 0.2f;

                    break;
                }

            case var color when actualTemp <= 20:
                {
                    bloomTintColor = new Color(255, 232, 0);

                    vignetteTintColor = new Color(3, 130, 0);
                    vignetteIntensity = 0.12f;
                    vignetteSmoothness = 0.15f;

                    break;
                }

            case var color when actualTemp <= 30:
                {
                    bloomTintColor = new Color(255, 163, 0);

                    vignetteTintColor = new Color(147, 11, 0);
                    vignetteIntensity = 0.11f;
                    vignetteSmoothness = 0.5f;

                    break;
                }
        }

        StartCoroutine(ChangeEnvironment());
    }

    private IEnumerator ChangeEnvironment()
    {
        float time = 0;

        while (time < 1)
        {
            //Cambia el valor del tinte de bloom a uno creado dependiendo de la temperatura actual.
            profileBloom.tint.value = Color.Lerp(profileBloom.tint.value, bloomTintColor, time);

            profileVignette.color.value = Color.Lerp(profileVignette.color.value, vignetteTintColor, time);
            //Cambia el valor del tinte de la viñeta a uno creado dependiendo de la temperatura normal.
            profileVignette.intensity.value = Mathf.Lerp(profileVignette.intensity.value, vignetteIntensity, time);
            profileVignette.smoothness.value = Mathf.Lerp(profileVignette.smoothness.value, vignetteSmoothness, time);

            time += Time.deltaTime * transitionSpeed;
            yield return null;
        }
        profileBloom.tint.value = bloomTintColor;

        profileVignette.color.value = vignetteTintColor;
        profileVignette.intensity.value = vignetteIntensity;
        profileVignette.smoothness.value = vignetteSmoothness;

        Debug.Log("Environment Changed");

        yield return null;
    }
    private void SetEnvironmenEffectsDefault()
    {
        volumeProfile.TryGet<Bloom>(out profileBloom);

        profileBloom.tint.value = Color.white;

        volumeProfile.TryGet<Vignette>(out profileVignette);

        profileVignette.color.value = Color.white;
        profileVignette.smoothness.value = 0f;
        profileVignette.intensity.value = 0f;
    }
}
