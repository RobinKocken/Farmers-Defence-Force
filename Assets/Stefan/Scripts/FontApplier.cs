#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;


public class FontApplier : MonoBehaviour
{
    public TMP_FontAsset currentFontAsset;
    public List<TMP_FontAsset> previousFonts = new();

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Loaded a new level!");
        ApplyFont();
    }
    public void ApplyFont()
    {
        var texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];

        if (texts.Length > 0)
        {
            if (!previousFonts.Contains(texts[0].font))
            {
                previousFonts.Add(texts[0].font);
            }
        }

        foreach(var text in texts)
        {
            text.font = currentFontAsset;
        }
        Debug.Log($"The Font '{currentFontAsset}' has been assigned to all TextMeshPro Text Elements! Total amount of text elements changed: { texts.Length}");
    }
}

[CustomEditor(typeof(FontApplier))]
[CanEditMultipleObjects]
public class FontApplierEditor : Editor
{
    FontApplier applier;

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        if (GUILayout.Button("Apply Font"))
        {
            applier = target as FontApplier;

            applier.ApplyFont();
        }
    }
}
#endif