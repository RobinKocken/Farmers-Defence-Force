using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DeathScreen : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Die()
    {
        animator.SetBool("Dead", true);
    }

    public void Respawn()
    {
        animator.SetBool("Dead", false);
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(DeathScreen))]
public class DeathScreenEditor : Editor
{
    DeathScreen screen;
    public override void OnInspectorGUI()
    {
        screen = target as DeathScreen;
        base.OnInspectorGUI();
        if (!Application.isPlaying) return;

        if (GUILayout.Button("TestDie"))
        {
            screen.Die();
        }
        if (GUILayout.Button("TestRespawn"))
        {
            screen.Respawn();
        }
    }
}
#endif
