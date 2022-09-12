using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public static float xSens = 300;
    public static float ySens = 350;
    public static float volume { get { return AudioListener.volume; } }

    public static void SetXSens(float sensitvitiy) => xSens = sensitvitiy;
    public static void SetYSens(float sensitivity) => ySens = sensitivity;
    public static void SetVolume(float _volume) => AudioListener.volume = _volume;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
