using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BloodSplash : MonoBehaviour
{
    [Range(0,100)]
    public float health;
    public float pulseStrength;
    public float maxPulseSpeed,minPulseSpeed;
    public float pulseStartAmount = 50;

    float pulseTimer;
    float minAlpha,maxAlpha;

    float currentMax;
    Image image;

    public Volume postProcessing;

    [Header("Volume Settings")]
    public HPVolumeSettings bloomValues;
    public HPVolumeSettings vignetteValues;
    public HPVolumeSettings chromaticAberrationValues;
    public HPVolumeSettings motionBlurValues;
    public HPVolumeSettings dofValues;
    public HPVolumeSettings colorAdjustmentsValues;
    public HPVolumeSettings lensDistortionIntensityValues;
    public HPVolumeSettings lensDistortionScaleValues;

    Bloom bloom;
    Vignette vignette;
    ChromaticAberration chromaticAberration;
    MotionBlur motionBlur;
    DepthOfField depthOfField;
    ColorAdjustments colorAdjustments;
    LensDistortion lensDistortion;

    public float hp { get
        {
            return Mathf.InverseLerp(pulseStartAmount, 0, health);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        postProcessing.profile.TryGet(out bloom);
        postProcessing.profile.TryGet(out vignette);
        postProcessing.profile.TryGet(out motionBlur);
        postProcessing.profile.TryGet(out depthOfField);
        postProcessing.profile.TryGet(out chromaticAberration);
        postProcessing.profile.TryGet(out colorAdjustments);
        postProcessing.profile.TryGet(out lensDistortion);
    }

    // Update is called once per frame
    void Update()
    {
        pulseTimer-= Time.deltaTime;

        if(pulseTimer <= 0)
        {
            currentMax = Mathf.Lerp(minPulseSpeed, maxPulseSpeed, hp);
            pulseTimer = currentMax;

            maxAlpha = Mathf.Lerp(0, 1, hp);
            minAlpha = maxAlpha - pulseStrength;

        }
        
        image.color = Color.Lerp(new(1,0,0,minAlpha),new(1,0,0,maxAlpha),Mathf.InverseLerp(0,currentMax,pulseTimer));

        UpdatePostProcessing();
    }

    void UpdatePostProcessing()
    {
        //Bloom
        bloom.intensity.value = bloomValues.Evaluate(hp);

        //Vignette
        vignette.intensity.value = vignetteValues.Evaluate(hp);

        //Motion blur
        motionBlur.intensity.value = motionBlurValues.Evaluate(hp); 

        //Chromatic Abberation
        chromaticAberration.intensity.value = chromaticAberrationValues.Evaluate(hp);

        //Depth of field
        depthOfField.gaussianStart.value = dofValues.Evaluate(hp);

        //Color Adjustments
        colorAdjustments.saturation.value = colorAdjustmentsValues.Evaluate(hp);

        //Lens Distortion
        lensDistortion.intensity.value = lensDistortionIntensityValues.Evaluate(hp);
        lensDistortion.scale.value = lensDistortionScaleValues.Evaluate(hp);
    }

    [Serializable]
    public struct HPVolumeSettings
    {
        public float fullHP;
        public float noHP;
        public float startHP;

        public float Evaluate(float hp)
        {
            return Mathf.Lerp(fullHP, noHP, Mathf.Min(startHP, hp));
        }
    }
}
