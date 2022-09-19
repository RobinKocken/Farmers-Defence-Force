using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public static float xSens = 300;
    public static float ySens = 350;
    public static float volume { get { return AudioListener.volume; } }

    /// <summary>
    /// Delta time berekenen omdat de buttons niet werken volgens Time.deltaTime als de game gepauzeerd is
    /// </summary>
    public static float deltaTime { get; private set; }


    DateTime tp1,tp2;

    private void Start()
    {
        tp1 = DateTime.Now;
        tp2 = DateTime.Now;
    }
    private void Update()
    {
        tp2 = DateTime.Now;
        deltaTime = (float)((tp2.Ticks - tp1.Ticks) / 10000000.0);
        tp1 = tp2;
    }

    public static void SetXSens(float sensitvitiy) => xSens = sensitvitiy;
    public static void SetYSens(float sensitivity) => ySens = sensitivity;
    public static void SetVolume(float _volume) => AudioListener.volume = _volume;

}
