using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretWaningIcons : MonoBehaviour
{
    public Transform canvas;

    public GameObject warningPrefab;
    
    public Sprite ammo, gas;

    public bool ShowIcons;
    public float scaleSmoothTime;
    public float timeToSwitchScale;
    bool growing;
    float switchScaleTimer;
    [Range(0,0.5f)]public float growAmount;
    public Transform activeAmmoWarning, activeGasWarning;
    Vector3 ammoVelocity, gasVelocity;
    Vector3 targetScale;
    public float targetSize;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam == null) return;
        if (ShowIcons)
        {
            //activeGasWarning.gameObject.SetActive(true);
            //activeAmmoWarning.gameObject.SetActive(true);

            activeAmmoWarning.localScale = Vector3.SmoothDamp(activeAmmoWarning.localScale, targetScale, ref ammoVelocity, scaleSmoothTime);
            activeGasWarning.localScale = Vector3.SmoothDamp(activeGasWarning.localScale, targetScale, ref gasVelocity, scaleSmoothTime);

            activeAmmoWarning.LookAt(mainCam.transform);
            activeGasWarning.LookAt(mainCam.transform);

            switchScaleTimer -= Time.deltaTime;
            if (switchScaleTimer <= 0)
            {
                switchScaleTimer = timeToSwitchScale;
                growing = !growing;

                if (growing)
                    targetScale = new(targetSize + growAmount, targetSize + growAmount, targetSize + growAmount);
                else
                    targetScale = new(targetSize, targetSize, targetSize);
            }

        }
        else
        {
            if (activeAmmoWarning)
            {
                activeAmmoWarning.localScale = Vector3.SmoothDamp(activeAmmoWarning.localScale,Vector3.zero,ref ammoVelocity,scaleSmoothTime);
                if (activeAmmoWarning.localScale.x <= 0.001f)
                {
                    activeAmmoWarning.gameObject.SetActive(false);
                }
            }

            if (activeGasWarning)
            {
                activeGasWarning.localScale = Vector3.SmoothDamp(activeGasWarning.localScale, Vector3.zero, ref gasVelocity, scaleSmoothTime);
                if (activeGasWarning.localScale.x <= 0.001f)
                {
                    activeGasWarning.gameObject.SetActive(false);
                }
            }
        }
    }
}
