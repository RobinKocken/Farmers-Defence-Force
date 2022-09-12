using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFade : MonoBehaviour
{
    static Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public static void Fade(bool fadeIn)
    {
        if (fadeIn) animator.SetBool("FadeIn", fadeIn);
        animator.SetTrigger("Fade");
    }
}
