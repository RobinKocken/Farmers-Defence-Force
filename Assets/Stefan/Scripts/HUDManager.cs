using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public PauseManager pauseManager;

    public bool canToggleMenu;
    void Update()
    {
        if (Input.GetButtonUp("Escape") && canToggleMenu)
        {
            pauseManager.TogglePause();
        }
    }
}
