using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionCircle : MonoBehaviour
{
    public bool active, canPickup;

    public Animator mainCircle, pulsingCircle;
    public TextMeshProUGUI keyText;
    public void SetState(bool active, bool canPickup, KeyCode key)
    {
        this.active = active;
        this.canPickup = canPickup;

        mainCircle.SetBool("Active", active);
        pulsingCircle.SetBool("Active", active && !canPickup);

        keyText.text = active && canPickup ? key.ToString() : " ";
    }
}
