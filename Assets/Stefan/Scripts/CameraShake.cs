using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraShake : MonoBehaviour
{
    public static CameraShake camShake;

    public float posMultiplier, rotMultiplier;

    public float minExplosionRadius;
    public float closestExplosionRadius;

    public float testDuration, testStrength;

    private void Awake()
    {
        if(camShake == null)
        {
            camShake = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Explode(Vector3 origin)
    {
        float strength = testStrength;

        float dst = Vector3.Distance(transform.position, origin);

        strength *= Mathf.InverseLerp(minExplosionRadius, closestExplosionRadius, dst);


        StartCoroutine(Shake(testDuration, strength));
    }

    public IEnumerator Shake(float duration, float strength)
    {
        while(duration >= 0)
        {
            //Prevents constant shaking while the game is paused
            while (PauseManager.Paused)
            {
                yield return null;
            }

            var newpos = new Vector3
            {
                x = (Random.value - 0.5f) * 2 * posMultiplier * Mathf.Min(duration * duration, 1) * strength,
                y = (Random.value - 0.5f) * 2 * posMultiplier * Mathf.Min(duration * duration, 1) * strength,
                z = (Random.value - 0.5f) * 2 * posMultiplier * Mathf.Min(duration * duration, 1) * strength
            };
            transform.localPosition = newpos * Options.cameraShakeAmount;

            var newRot = new Vector3
            {
                x = (Random.value - 0.5f) * 2 * rotMultiplier * strength * Mathf.Min(duration * duration, 1),
                y = (Random.value - 0.5f) * 2 * rotMultiplier * strength * Mathf.Min(duration * duration, 1),
                z = (Random.value - 0.5f) * 2 * rotMultiplier * strength * Mathf.Min(duration * duration, 1)
            };
            transform.localEulerAngles = newRot * Options.cameraShakeAmount;

            yield return null;
            duration -= Time.deltaTime;
        }

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraShake))]
[CanEditMultipleObjects]
public class CameraShakeEditor : Editor
{
    CameraShake cameraShake;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (!Application.isPlaying) return;

        cameraShake = target as CameraShake;

        if (GUILayout.Button("Shake!"))
        {
            cameraShake.StartCoroutine(cameraShake.Shake(cameraShake.testDuration,cameraShake.testStrength));
        }
    }
}
#endif
