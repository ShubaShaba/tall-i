using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    [SerializeField, Range(0, 1)] private float paleAmount = 0.4f;
    private MaterialPropertyBlock block;
    private Renderer selectableRenderer;
    private static readonly int EmissionID = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        // Saving original color
        selectableRenderer = GetComponent<Renderer>();
        if (selectableRenderer == null) return;

        block = new MaterialPropertyBlock();
        selectableRenderer.GetPropertyBlock(block);
        selectableRenderer.material.EnableKeyword("_EMISSION");
    }
    
    public void Highlight()
    {
        if (selectableRenderer == null) return;
        block.SetColor(EmissionID, Color.white * paleAmount);
        selectableRenderer.SetPropertyBlock(block);
    }

    public void ResetColor()
    {
        if (selectableRenderer == null) return;
        block.Clear();
        selectableRenderer.SetPropertyBlock(block);
    }
}
